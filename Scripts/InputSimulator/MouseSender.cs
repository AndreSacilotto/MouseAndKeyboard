using MouseAndKeyboard.Native;
using System.Threading.Tasks;

namespace MouseAndKeyboard.InputSimulator;

public static partial class MouseSender
{
    #region Inputs
    internal static InputStruct MoveRelativeInput(int x, int y)
    {
        MouseInput mi = new(x, y, MouseEventF.Move);
        return InputStruct.NewInput(mi);
    }
    internal static InputStruct MoveAbsoluteInput(int x, int y)
    {
        var absPt = PrimaryMonitor.Instance.CoordsToAbsolute(x, y);
        MouseInput mi = new(absPt, MouseEventF.Move | MouseEventF.Absolute);
        return InputStruct.NewInput(mi);
    }
    internal static InputStruct MoveVirtualDeskInput(int x, int y)
    {
#if DEBUG
        if (x < 0 || x > ushort.MaxValue || y < 0 || y > ushort.MaxValue)
            throw new Exception();
#endif
        MouseInput mi = new(x+1, y+1, MouseEventF.Move | MouseEventF.Absolute | MouseEventF.VirtualDesk);
        return InputStruct.NewInput(mi);
    }
    internal static InputStruct ScrollInput(int scroll)
    {
        MouseInput mi = new(Point.Zero, MouseEventF.Wheel, scroll);
        return InputStruct.NewInput(mi);
    }
    internal static InputStruct ScrollHInput(int scroll)
    {
        MouseInput mi = new(Point.Zero, MouseEventF.HWheel, scroll);
        return InputStruct.NewInput(mi);
    }
    internal static InputStruct ClickInput(MouseEventF dwFlags)
    {
        MouseInput mi = new(Point.Zero, dwFlags);
        return InputStruct.NewInput(mi);
    }
    internal static InputStruct ClickInput(MouseEventF dwFlags, MouseDataXButton mouseData)
    {
        MouseInput mi = new(Point.Zero, dwFlags, (int)mouseData);
        return InputStruct.NewInput(mi);
    }
    #endregion

    public static void MoveRelative(int x, int y) => InputSender.SendInput(MoveRelativeInput(x, y));
    public static void MoveAbsolute(int x, int y) => InputSender.SendInput(MoveAbsoluteInput(x, y));
    public static void MoveVirtualDesk(int x, int y) => InputSender.SendInput(MoveVirtualDeskInput(x, y));

    internal static void Click(MouseEventF dwFlags) => InputSender.SendInput(ClickInput(dwFlags));
    internal static void Click(MouseEventF dwFlags, MouseDataXButton mouseData) => InputSender.SendInput(ClickInput(dwFlags, mouseData));
    internal static void Click(MouseDataXButton mouseData) => InputSender.SendInput(ClickInput(0, mouseData));

    internal static void MultClick(MouseEventF dwFlags, int numberOfClicks = 2)
    {
        var inputs = new InputStruct[numberOfClicks];
        var input = ClickInput(dwFlags);
        for (int i = 0; i < numberOfClicks; i++)
            inputs[i] = input;
        InputSender.SendInput(inputs);
    }
    internal static void MultClick(MouseEventF dwFlags, MouseDataXButton mouseData, int numberOfClicks = 2)
    {
        var inputs = new InputStruct[numberOfClicks];
        var input = ClickInput(dwFlags, mouseData);
        for (int i = 0; i < numberOfClicks; i++)
            inputs[i] = input;
        InputSender.SendInput(inputs);
    }

    /// <param name="wheelDelta">Scroll amount - Windows default is 120</param>
    public static void ScrollWheel(int wheelDelta = 120) => InputSender.SendInput(ScrollInput(wheelDelta));
    /// <param name="wheelDelta">Scroll amount - Windows default is 120</param>
    public static void ScrollHWheel(int wheelDelta = 120) => InputSender.SendInput(ScrollHInput(wheelDelta));

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

    #endregion

    #region Click by Enum

    public static void Click(PressedState pressState, MouseButtons mouseButton)
    {
        switch (mouseButton)
        {
            case MouseButtons.Left:
                if (pressState == PressedState.Click)
                    Click(MouseEventF.LeftClick);
                else if (pressState == PressedState.Down)
                    Click(MouseEventF.LeftDown);
                else if (pressState == PressedState.Up)
                    Click(MouseEventF.LeftUp);
                break;
            case MouseButtons.Middle:
                if (pressState == PressedState.Click)
                    Click(MouseEventF.MiddleClick);
                else if (pressState == PressedState.Down)
                    Click(MouseEventF.MiddleDown);
                else if (pressState == PressedState.Up)
                    Click(MouseEventF.MiddleUp);
                break;
            case MouseButtons.Right:
                if (pressState == PressedState.Click)
                    Click(MouseEventF.RightClick);
                else if (pressState == PressedState.Down)
                    Click(MouseEventF.RightDown);
                else if (pressState == PressedState.Up)
                    Click(MouseEventF.RightUp);
                break;
            case MouseButtons.XButton1:
                if (pressState == PressedState.Click)
                    Click(MouseEventF.XClick, MouseDataXButton.XButton1);
                else if (pressState == PressedState.Down)
                    Click(MouseEventF.XDown, MouseDataXButton.XButton1);
                else if (pressState == PressedState.Up)
                    Click(MouseEventF.XUp, MouseDataXButton.XButton1);
                break;
            case MouseButtons.XButton2:
                if (pressState == PressedState.Click)
                    Click(MouseEventF.XClick, MouseDataXButton.XButton2);
                else if (pressState == PressedState.Down)
                    Click(MouseEventF.XDown, MouseDataXButton.XButton2);
                else if (pressState == PressedState.Up)
                    Click(MouseEventF.XUp, MouseDataXButton.XButton2);
                break;
        }
    }

    public static void Click(PressedState pressState, MouseButtons mouseButton, int numberOfClicks = 2)
    {
        switch (mouseButton)
        {
            case MouseButtons.Left:
                if (pressState == PressedState.Click)
                    MultClick(MouseEventF.LeftClick, MouseDataXButton.None, numberOfClicks);
                else if (pressState == PressedState.Down)
                    MultClick(MouseEventF.LeftDown, MouseDataXButton.None, numberOfClicks);
                else if (pressState == PressedState.Up)
                    MultClick(MouseEventF.LeftUp, MouseDataXButton.None, numberOfClicks);
                break;
            case MouseButtons.Middle:
                if (pressState == PressedState.Click)
                    MultClick(MouseEventF.MiddleClick, MouseDataXButton.None, numberOfClicks);
                else if (pressState == PressedState.Down)
                    MultClick(MouseEventF.MiddleDown, MouseDataXButton.None, numberOfClicks);
                else if (pressState == PressedState.Up)
                    MultClick(MouseEventF.MiddleUp, MouseDataXButton.None, numberOfClicks);
                break;
            case MouseButtons.Right:
                if (pressState == PressedState.Click)
                    MultClick(MouseEventF.RightClick, MouseDataXButton.None, numberOfClicks);
                else if (pressState == PressedState.Down)
                    MultClick(MouseEventF.RightDown, MouseDataXButton.None, numberOfClicks);
                else if (pressState == PressedState.Up)
                    MultClick(MouseEventF.RightUp, MouseDataXButton.None, numberOfClicks);
                break;
            case MouseButtons.XButton1:
                if (pressState == PressedState.Click)
                    MultClick(MouseEventF.XClick, MouseDataXButton.XButton1, numberOfClicks);
                else if (pressState == PressedState.Down)
                    MultClick(MouseEventF.XDown, MouseDataXButton.XButton1, numberOfClicks);
                else if (pressState == PressedState.Up)
                    MultClick(MouseEventF.XUp, MouseDataXButton.XButton1, numberOfClicks);
                break;
            case MouseButtons.XButton2:
                if (pressState == PressedState.Click)
                    MultClick(MouseEventF.XClick, MouseDataXButton.XButton2, numberOfClicks);
                else if (pressState == PressedState.Down)
                    MultClick(MouseEventF.XDown, MouseDataXButton.XButton2, numberOfClicks);
                else if (pressState == PressedState.Up)
                    MultClick(MouseEventF.XUp, MouseDataXButton.XButton2, numberOfClicks);
                break;
        }
    }

    #endregion

}