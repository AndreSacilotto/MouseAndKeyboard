using System;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using static InputSender;

public static class Mouse
{
    #region Get Mouse Position

    [DllImport("user32.dll")]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static extern bool GetCursorPos(out MousePoint lpMousePoint);
    private struct MousePoint
    {
        internal uint x;
        internal uint y;
    }
    public static void GetCursorPosition(out uint x, out uint y)
    {
        GetCursorPos(out var point);
        x = point.x;
        y = point.y;
    }

    #endregion

    private static InputStruct MouseInput => new InputStruct(InputType.Mouse);

    #region Movement
    /// <summary>Move based on current mouse position</summary>
    public static InputStruct MoveRelativeInput(int x, int y)
    {
        var input = MouseInput;
        input.union.mi.dx = x;
        input.union.mi.dy = y;
        input.union.mi.dwFlags = MouseEventF.Move;
        return input;
    }
    private const int ABSOLUTE_MAX = ushort.MaxValue + 1;

    private static InputStruct MoveAbsoluteInput(int x, int y)
    {
        var screen = ScreenUtil.GetPrimaryScreenSize;
        var input = MouseInput;
        input.union.mi.dx = (x * ABSOLUTE_MAX / screen.width) + 1;
        input.union.mi.dy = (y * ABSOLUTE_MAX / screen.height) + 1;
        input.union.mi.dwFlags = MouseEventF.Move | MouseEventF.Absolute;
        return input;
    }

    private static InputStruct MoveAbsoluteVirtualDeskInput(int x, int y)
    {
        var screen = ScreenUtil.GetVirtualScreenSize;
        var input = MouseInput;
        input.union.mi.dx = x * ABSOLUTE_MAX / (screen.width - 1);
        input.union.mi.dy = y * ABSOLUTE_MAX / (screen.height - 1);
        input.union.mi.dwFlags = MouseEventF.Move | MouseEventF.Absolute | MouseEventF.VirtualDesk;
        return input;
    }

    public static void MoveRelative(int x, int y) => SendInput(MoveRelativeInput(x, y));
    public static void MoveAbsolute(int x, int y) => SendInput(MoveAbsoluteInput(x, y));
    public static void MoveAbsoluteVirtual(int x, int y) => SendInput(MoveAbsoluteVirtualDeskInput(x, y));

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
        Task.Run(async () => {
            Click(MouseEventF.LeftDown);
            await Task.Delay(15);
            MoveAbsolute(endX, endX);
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
