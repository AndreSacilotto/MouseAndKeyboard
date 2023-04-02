using MouseAndKeyboard.Native;

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
        MouseInput mi = new(x + 1, y + 1, MouseEventF.Move | MouseEventF.Absolute | MouseEventF.VirtualDesk);
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

    #region Sender Move

    public static void MoveRelative(int x, int y) => InputSender.SendInput(MoveRelativeInput(x, y));
    public static void MoveAbsolute(int x, int y) => InputSender.SendInput(MoveAbsoluteInput(x, y));
    public static void MoveVirtualDesk(int x, int y) => InputSender.SendInput(MoveVirtualDeskInput(x, y));

    /// <param name="wheelDelta">ScrollLock amount - Windows default is 120</param>
    public static void ScrollWheel(int wheelDelta = 120) => InputSender.SendInput(ScrollInput(wheelDelta));
    /// <param name="wheelDelta">ScrollLock amount - Windows default is 120</param>
    public static void ScrollHWheel(int wheelDelta = 120) => InputSender.SendInput(ScrollHInput(wheelDelta));

    #endregion

    #region Sender Base

    public static void SendButton(MouseEventF dwFlags) => InputSender.SendInput(ClickInput(dwFlags));
    public static void SendButton(MouseEventF dwFlags, MouseDataXButton mouseData) => InputSender.SendInput(ClickInput(dwFlags, mouseData));

    public static void SendButton(MouseEventF dwFlags, int numberOfClicks)
    {
        var inputs = new InputStruct[numberOfClicks];
        var input = ClickInput(dwFlags);
        for (int i = 0; i < numberOfClicks; i++)
            inputs[i] = input;
        InputSender.SendInput(inputs);
    }
    public static void SendButton(MouseEventF dwFlags, MouseDataXButton mouseData, int numberOfClicks)
    {
        var inputs = new InputStruct[numberOfClicks];
        var input = ClickInput(dwFlags, mouseData);
        for (int i = 0; i < numberOfClicks; i++)
            inputs[i] = input;
        InputSender.SendInput(inputs);
    }

    #endregion

    #region Sender
    public static void SendButtonDown(MouseButtonsF mb)
    {
        switch (mb)
        {
            case MouseButtonsF.Left:
            SendButton(MouseEventF.LeftDown);
            break;
            case MouseButtonsF.Right:
            SendButton(MouseEventF.RightDown);
            break;
            case MouseButtonsF.Middle:
            SendButton(MouseEventF.MiddleDown);
            break;
            case MouseButtonsF.XButton1:
            SendButton(MouseEventF.XDown, MouseDataXButton.XButton1);
            break;
            case MouseButtonsF.XButton2:
            SendButton(MouseEventF.XDown, MouseDataXButton.XButton2);
            break;
        }
    }

    public static void SendButtonUp(MouseButtonsF mb)
    {
        switch (mb)
        {
            case MouseButtonsF.Left:
            SendButton(MouseEventF.LeftUp);
            break;
            case MouseButtonsF.Right:
            SendButton(MouseEventF.RightUp);
            break;
            case MouseButtonsF.Middle:
            SendButton(MouseEventF.MiddleUp);
            break;
            case MouseButtonsF.XButton1:
            SendButton(MouseEventF.XUp, MouseDataXButton.XButton1);
            break;
            case MouseButtonsF.XButton2:
            SendButton(MouseEventF.XUp, MouseDataXButton.XButton2);
            break;
        }

    }

    public static void SendButtonClick(MouseButtonsF mb)
    {
        switch (mb)
        {
            case MouseButtonsF.Left:
            SendButton(MouseEventF.LeftClick);
            break;
            case MouseButtonsF.Right:
            SendButton(MouseEventF.RightClick);
            break;
            case MouseButtonsF.Middle:
            SendButton(MouseEventF.MiddleClick);
            break;
            case MouseButtonsF.XButton1:
            SendButton(MouseEventF.XClick, MouseDataXButton.XButton1);
            break;
            case MouseButtonsF.XButton2:
            SendButton(MouseEventF.XClick, MouseDataXButton.XButton2);
            break;
        }
    }

    public static void SendButtonMultClick(MouseButtonsF mb, int clicks = 2)
    {
        switch (mb)
        {
            case MouseButtonsF.Left:
            SendButton(MouseEventF.LeftClick, clicks);
            break;
            case MouseButtonsF.Right:
            SendButton(MouseEventF.RightClick, clicks);
            break;
            case MouseButtonsF.Middle:
            SendButton(MouseEventF.MiddleClick, clicks);
            break;
            case MouseButtonsF.XButton1:
            SendButton(MouseEventF.XClick, MouseDataXButton.XButton1, clicks);
            break;
            case MouseButtonsF.XButton2:
            SendButton(MouseEventF.XClick, MouseDataXButton.XButton2, clicks);
            break;
        }
    }

    #endregion

}