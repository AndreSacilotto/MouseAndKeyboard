using MouseAndKeyboard.Native;
using System.Threading.Tasks;

namespace MouseAndKeyboard.InputSimulation;

public static partial class Mouse
{
    public static void GetCursorPosition(out int x, out int y)
    {
        MouseNativeMethods.GetCursorPos(out var point);
        x = point.X;
        y = point.Y;
    }
    private static Point GetAbsoluteScreenPoint(int x, int y)
    {
        var screen = ScreenUtil.PrimaryScreenSize;
        return new(x * screen.Ratio.X + 1, y * screen.Ratio.Y + 1);
    }

    #region Inputs
    internal static InputStruct MoveRelativeInput(int x, int y)
    {
        var union = new InputUnion(mi: new(new(x, y), MouseEventF.Move));
        var input = new InputStruct(InputType.Mouse, union);
        return input;
    }
    internal static InputStruct MoveAbsoluteInput(int x, int y)
    {
        var union = new InputUnion(mi: new(GetAbsoluteScreenPoint(x, y), MouseEventF.Move | MouseEventF.Absolute));
        //MouseEventF.VirtualDesk - Dont worth the trouble
        return new InputStruct(InputType.Mouse, union);
    }
    internal static InputStruct ScrollInput(short scroll)
    {
        var union = new InputUnion(mi: new(Point.Zero, MouseEventF.Wheel, scroll));
        return new InputStruct(InputType.Mouse, union);
    }
    internal static InputStruct ClickInput(MouseEventF dwFlags, MouseDataXButton mouseData = MouseDataXButton.None)
    {
        var union = new InputUnion(mi: new(Point.Zero, dwFlags, (short)mouseData));
        return new InputStruct(InputType.Mouse, union);
    }
    internal static InputStruct MoveAndClickRelativeInput(int x, int y, MouseEventF dwFlags, MouseDataXButton mouseData = MouseDataXButton.None)
    {
        var union = new InputUnion(mi: new(new(x, y), MouseEventF.Move | dwFlags, (short)mouseData));
        var input = new InputStruct(InputType.Mouse, union);
        return input;
    }
    internal static InputStruct MoveAndClickAbsoluteInput(int x, int y, MouseEventF dwFlags, MouseDataXButton mouseData = MouseDataXButton.None)
    {
        var union = new InputUnion(mi: new(GetAbsoluteScreenPoint(x, y), MouseEventF.Move | MouseEventF.Absolute | dwFlags, (short)mouseData));
        return new InputStruct(InputType.Mouse, union);
    }
    #endregion

    public static void MoveRelative(int x, int y) => InputSender.SendInput(MoveRelativeInput(x, y));
    public static void MoveAbsolute(int x, int y) => InputSender.SendInput(MoveAbsoluteInput(x, y));

    internal static void MouseClick(MouseEventF dwFlags, MouseDataXButton mouseData = MouseDataXButton.None) => InputSender.SendInput(ClickInput(dwFlags, mouseData));
    internal static void MouseMultClick(MouseEventF dwFlags, MouseDataXButton mouseData = MouseDataXButton.None, int numberOfClicks = 2)
    {
        var inputs = new InputStruct[numberOfClicks];
        var input = ClickInput(dwFlags, mouseData);
        for (int i = 0; i < numberOfClicks; i++)
            inputs[i] = input;
        InputSender.SendInput(inputs);
    }

    /// <param name="wheelDelta">Scroll quantity. 120 is the Windows default</param>
    public static void ScrollWheel(short wheelDelta = 120) => InputSender.SendInput(ScrollInput(wheelDelta));

    #region Combinations
    public static void DragAndDrop(int endX, int endY)
    {
        Task.Run(async () =>
        {
            MouseClick(MouseEventF.LeftDown);
            await Task.Delay(15);
            MoveAbsolute(endX, endY);
            await Task.Delay(15);
            MouseClick(MouseEventF.LeftUp);
        });
    }

    internal static void MoveAndClick(int x, int y, MouseEventF dwFlags) => InputSender.SendInput(MoveAndClickAbsoluteInput(x, y, dwFlags));
    internal static void MoveAndClick(int x, int y, MouseEventF dwFlags, int numberOfClicks = 2)
    {
        var inputs = new InputStruct[numberOfClicks + 1];
        inputs[0] = MoveAbsoluteInput(x, y);
        var input = ClickInput(dwFlags);
        for (int i = numberOfClicks; i > 0; i--)
            inputs[i] = input;
        InputSender.SendInput(inputs);
    }

    #endregion

    #region Click by Enum

    public static void Click(PressedState pressState, MouseButtons mouseButton)
    {
        switch (mouseButton)
        {
            case MouseButtons.Left:
                if (pressState == PressedState.Click)
                    MouseClick(MouseEventF.LeftClick);
                else if (pressState == PressedState.Down)
                    MouseClick(MouseEventF.LeftDown);
                else if (pressState == PressedState.Up)
                    MouseClick(MouseEventF.LeftUp);
                break;
            case MouseButtons.Middle:
                if (pressState == PressedState.Click)
                    MouseClick(MouseEventF.MiddleClick);
                else if (pressState == PressedState.Down)
                    MouseClick(MouseEventF.MiddleDown);
                else if (pressState == PressedState.Up)
                    MouseClick(MouseEventF.MiddleUp);
                break;
            case MouseButtons.Right:
                if (pressState == PressedState.Click)
                    MouseClick(MouseEventF.RightClick);
                else if (pressState == PressedState.Down)
                    MouseClick(MouseEventF.RightDown);
                else if (pressState == PressedState.Up)
                    MouseClick(MouseEventF.RightUp);
                break;
            case MouseButtons.XButton1:
                if (pressState == PressedState.Click)
                    MouseClick(MouseEventF.XClick, MouseDataXButton.XButton1);
                else if (pressState == PressedState.Down)
                    MouseClick(MouseEventF.XDown, MouseDataXButton.XButton1);
                else if (pressState == PressedState.Up)
                    MouseClick(MouseEventF.XUp, MouseDataXButton.XButton1);
                break;
            case MouseButtons.XButton2:
                if (pressState == PressedState.Click)
                    MouseClick(MouseEventF.XClick, MouseDataXButton.XButton2);
                else if (pressState == PressedState.Down)
                    MouseClick(MouseEventF.XDown, MouseDataXButton.XButton2);
                else if (pressState == PressedState.Up)
                    MouseClick(MouseEventF.XUp, MouseDataXButton.XButton2);
                break;
        }
    }

    public static void Click(PressedState pressState, MouseButtons mouseButton, int numberOfClicks = 2)
    {
        switch (mouseButton)
        {
            case MouseButtons.Left:
                if (pressState == PressedState.Click)
                    MouseMultClick(MouseEventF.LeftClick, MouseDataXButton.None, numberOfClicks);
                else if (pressState == PressedState.Down)
                    MouseMultClick(MouseEventF.LeftDown, MouseDataXButton.None, numberOfClicks);
                else if (pressState == PressedState.Up)
                    MouseMultClick(MouseEventF.LeftUp, MouseDataXButton.None, numberOfClicks);
                break;
            case MouseButtons.Middle:
                if (pressState == PressedState.Click)
                    MouseMultClick(MouseEventF.MiddleClick, MouseDataXButton.None, numberOfClicks);
                else if (pressState == PressedState.Down)
                    MouseMultClick(MouseEventF.MiddleDown, MouseDataXButton.None, numberOfClicks);
                else if (pressState == PressedState.Up)
                    MouseMultClick(MouseEventF.MiddleUp, MouseDataXButton.None, numberOfClicks);
                break;
            case MouseButtons.Right:
                if (pressState == PressedState.Click)
                    MouseMultClick(MouseEventF.RightClick, MouseDataXButton.None, numberOfClicks);
                else if (pressState == PressedState.Down)
                    MouseMultClick(MouseEventF.RightDown, MouseDataXButton.None, numberOfClicks);
                else if (pressState == PressedState.Up)
                    MouseMultClick(MouseEventF.RightUp, MouseDataXButton.None, numberOfClicks);
                break;
            case MouseButtons.XButton1:
                if (pressState == PressedState.Click)
                    MouseMultClick(MouseEventF.XClick, MouseDataXButton.XButton1, numberOfClicks);
                else if (pressState == PressedState.Down)
                    MouseMultClick(MouseEventF.XDown, MouseDataXButton.XButton1, numberOfClicks);
                else if (pressState == PressedState.Up)
                    MouseMultClick(MouseEventF.XUp, MouseDataXButton.XButton1, numberOfClicks);
                break;
            case MouseButtons.XButton2:
                if (pressState == PressedState.Click)
                    MouseMultClick(MouseEventF.XClick, MouseDataXButton.XButton2, numberOfClicks);
                else if (pressState == PressedState.Down)
                    MouseMultClick(MouseEventF.XDown, MouseDataXButton.XButton2, numberOfClicks);
                else if (pressState == PressedState.Up)
                    MouseMultClick(MouseEventF.XUp, MouseDataXButton.XButton2, numberOfClicks);
                break;
        }
    }

    #endregion

}