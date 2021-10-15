using System;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using static InputSender;

public static class Mouse
{
    #region Get Mouse Position

    [DllImport("user32.dll")]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static extern bool GetCursorPos(out MousePoint lpMousePoint);

    [StructLayout(LayoutKind.Sequential)]
    private struct MousePoint
    {
        internal int x;
        internal int y;
    }
    public static void GetCursorPosition(out int x, out int y)
    {
        GetCursorPos(out var point);
        x = point.x;
        y = point.y;
    }

    #endregion

    private static InputStruct MouseInput => new InputStruct(InputType.Mouse);

    #region Movement
    private const int ABSOLUTE_MAX = ushort.MaxValue + 1;

    public static int PositionToAbsolute(int coord, int widthOrHeight)
    {
        return (coord * ABSOLUTE_MAX / widthOrHeight) + (coord < 0 ? -1 : 1);
    }

    public static string PositionToAbsolutePrint(int x, int y)
    {
        var screen = ScreenUtil.GetPrimaryScreenSize;
        return $"{PositionToAbsolute(x, screen.width)} : {PositionToAbsolute(y, screen.height)}";
    }

    /// <summary>Move based on current mouse position</summary>
    public static InputStruct MoveRelativeInput(int x, int y)
    {
        var input = MouseInput;
        input.union.mi.dx = x;
        input.union.mi.dy = y;
        input.union.mi.dwFlags = MouseEventF.Move;
        return input;
    }

    private static InputStruct MoveAbsoluteInput(int x, int y)
    {
        var screen = ScreenUtil.GetPrimaryScreenSize;
        var input = MouseInput;
        input.union.mi.dx = (x * ABSOLUTE_MAX / screen.width) + 1;
        input.union.mi.dy = (y * ABSOLUTE_MAX / screen.height) + 1;
        input.union.mi.dwFlags = MouseEventF.Move | MouseEventF.Absolute;
        //MouseEventF.VirtualDesk - Dont worth the trouble
        return input;
    }

    public static void MoveRelative(int x, int y) => SendInput(MoveRelativeInput(x, y));
    public static void MoveAbsolute(int x, int y) => SendInput(MoveAbsoluteInput(x, y));
    public static void MoveAbsolute(float x, float y) => SendInput(MoveAbsoluteInput((int)x, (int)y));

    #endregion

    #region Gradual Movement

    public const float MoveEpsilon = 0.2f;
    public static void PixelGradualMove(int x1, int y1, int pixelSpeed = 32, int delay = 10)
    {
        GetCursorPosition(out var x0, out var y0);
        Task.Run(async () =>
        {
            int xSpeed = Math.Abs(x1 - x0) / pixelSpeed;
            int ySpeed = Math.Abs(y1 - y0) / pixelSpeed;
            while (x0 != x1 || y0 != y1)
            {
                if (x0 < x1)
                {
                    x0 += xSpeed;
                    if (x0 > x1)
                        x0 = x1;
                }
                else if (x1 < x0)
                {
                    x1 -= xSpeed;
                    if (x1 < x0)
                        x1 = x0;
                }

                if (y0 < y1)
                {
                    y0 += ySpeed;
                    if (y0 > y1)
                        y0 = y1;
                }
                else if (y1 < y0)
                {
                    y1 -= ySpeed;
                    if (y1 < y0)
                        y1 = y0;
                }

                MoveAbsolute(x0, y0);
                await Task.Delay(delay);
            }
        });
    }
    public static void LinearGradualMove(int x1, int y1, int steps = 32, int delay = 100)
    {
        GetCursorPosition(out var x0, out var y0);
        Task.Run(async () =>
        {
            float N = steps;
            for (int i = 0; i < steps; i++)
            {
                var v = i / N;
                var x = (x1 * v) + (x0 * (1f - v));
                var y = (y1 * v) + (y0 * (1f - v));
                MoveAbsolute(x, y);
                await Task.Delay(delay);
            }
        });
    }

    public static void LerpGradualMove(int x1, int y1, float t = 0.1f, int delay = 10)
    {
        GetCursorPosition(out var x0, out var y0);
        Task.Run(async () =>
        {
            float x = x0;
            float y = y0;
            while (Math.Abs(x - x1) > MoveEpsilon || Math.Abs(y - y1) > MoveEpsilon)
            {
                x = Interpolation.Lerp(x, x1, t);
                y = Interpolation.Lerp(y, y1, t);

                MoveAbsolute(x, y);
                await Task.Delay(delay);
            }
        });
    }



    #endregion

    #region MButton (Click)

    private static InputStruct ClickInput(MouseEventF dwFlags)
    {
        var input = MouseInput;
        input.union.mi.dwFlags = dwFlags;
        return input;
    }
    /// <param name="dwFlags">Need to be XDown or XUp</param>
    private static InputStruct ClickInput(MouseEventF dwFlags, MouseDataXButton mouseData)
    {
        var input = MouseInput;
        input.union.mi.mouseData = (int)mouseData;
        input.union.mi.dwFlags = dwFlags;
        return input;
    }

    public static void Click(MouseEventF dwFlags) => SendInput(ClickInput(dwFlags));
    public static void Click(MouseEventF dwFlags, int numberOfClicks = 2)
    {
        var inputs = new InputStruct[numberOfClicks];
        var input = ClickInput(dwFlags);
        for (int i = 0; i < numberOfClicks; i++)
            inputs[i] = input;
        SendInput(inputs);
    }
    /// <param name="dwFlags">Need to be XDown or XUp</param>
    public static void Click(MouseEventF dwFlags, MouseDataXButton mouseData) => SendInput(ClickInput(dwFlags, mouseData));
    /// <param name="dwFlags">Need to be XDown or XUp</param>
    public static void Click(MouseEventF dwFlags, MouseDataXButton mouseData, int numberOfClicks = 2)
    {
        var inputs = new InputStruct[numberOfClicks];
        var input = ClickInput(dwFlags, mouseData);
        for (int i = 0; i < numberOfClicks; i++)
            inputs[i] = input;
        SendInput(inputs);
    }

    #endregion

    #region Wheel

    private static InputStruct ScrollWheelInput(int wheelDelta = 120)
    {
        var input = MouseInput;
        input.union.mi.dwFlags = MouseEventF.Wheel;
        input.union.mi.mouseData = wheelDelta;
        return input;
    }

    /// <param name="wheelDelta">Scroll quantity. 120 is the Windows default</param>
    public static void ScrollWheel(int wheelDelta = 120) =>
        SendInput(ScrollWheelInput(wheelDelta));

    #endregion

    #region Combinations
    public static void DragAndDrop(int endX, int endY)
    {
        Task.Run(async () =>
        {
            Click(MouseEventF.LeftDown);
            await Task.Delay(15);
            MoveAbsolute(endX, endY);
            await Task.Delay(15);
            Click(MouseEventF.LeftUp);
        });
    }

    public static void MoveAndClick(MouseEventF dwFlags, int x, int y)
    {
        var input = MoveAbsoluteInput(x, y);
        input.union.mi.dwFlags |= dwFlags;
        SendInput(input);
    }
    public static void MoveAndClick(MouseEventF dwFlags, int x, int y, int numberOfClicks = 2)
    {
        var inputs = new InputStruct[numberOfClicks + 1];
        inputs[0] = MoveAbsoluteInput(x, y);
        var input = ClickInput(dwFlags);
        for (int i = numberOfClicks; i > 0; i--)
            inputs[i] = input;
        SendInput(inputs);
    }

    #endregion

}
