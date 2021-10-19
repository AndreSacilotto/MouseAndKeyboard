using System.Windows.Forms;

namespace InputSimulation
{
    public static class MouseButtonExplicit
    {
        //public static bool Shift => Control.ModifierKeys == Keys.Shift;
        //public static bool Ctrl => Control.ModifierKeys == Keys.Shift;
        //public static bool Alt => Control.ModifierKeys == Keys.Alt;
        //public static bool ShiftControl => Control.ModifierKeys == (Keys.Shift | Keys.Control);
        //public static bool ShiftAlt => Control.ModifierKeys == (Keys.Shift | Keys.Alt);
        //public static bool ControAlt => Control.ModifierKeys == (Keys.Control | Keys.Alt);
        //public static bool ControAltShift => Control.ModifierKeys == (Keys.Control | Keys.Shift | Keys.Alt);

        public static void Click(PressedState pressState, MouseButtons mouseButton)
        {
            switch (mouseButton)
            {
                case MouseButtons.Left:
                    if (pressState == PressedState.Click)
                        Mouse.Click(MouseEventF.LeftClick);
                    else if (pressState == PressedState.Down)
                        Mouse.Click(MouseEventF.LeftDown);
                    else if (pressState == PressedState.Up)
                        Mouse.Click(MouseEventF.LeftUp);
                    break;
                case MouseButtons.Middle:
                    if (pressState == PressedState.Click)
                        Mouse.Click(MouseEventF.MiddleClick);
                    else if (pressState == PressedState.Down)
                        Mouse.Click(MouseEventF.MiddleDown);
                    else if (pressState == PressedState.Up)
                        Mouse.Click(MouseEventF.MiddleUp);
                    break;
                case MouseButtons.Right:
                    if (pressState == PressedState.Click)
                        Mouse.Click(MouseEventF.RightClick);
                    else if (pressState == PressedState.Down)
                        Mouse.Click(MouseEventF.RightDown);
                    else if (pressState == PressedState.Up)
                        Mouse.Click(MouseEventF.RightUp);
                    break;
                case MouseButtons.XButton1:
                    if (pressState == PressedState.Click)
                        Mouse.Click(MouseEventF.XClick, MouseDataXButton.XButton1);
                    else if (pressState == PressedState.Down)
                        Mouse.Click(MouseEventF.XDown, MouseDataXButton.XButton1);
                    else if (pressState == PressedState.Up)
                        Mouse.Click(MouseEventF.XUp, MouseDataXButton.XButton1);
                    break;
                case MouseButtons.XButton2:
                    if (pressState == PressedState.Click)
                        Mouse.Click(MouseEventF.XClick, MouseDataXButton.XButton2);
                    else if (pressState == PressedState.Down)
                        Mouse.Click(MouseEventF.XDown, MouseDataXButton.XButton2);
                    else if (pressState == PressedState.Up)
                        Mouse.Click(MouseEventF.XUp, MouseDataXButton.XButton2);
                    break;
            }
        }

        public static void Click(PressedState pressState, MouseButtons mouseButton, int numberOfClicks = 2)
        {
            switch (mouseButton)
            {
                case MouseButtons.Left:
                    if (pressState == PressedState.Click)
                        Mouse.Click(MouseEventF.LeftClick, numberOfClicks);
                    else if (pressState == PressedState.Down)
                        Mouse.Click(MouseEventF.LeftDown, numberOfClicks);
                    else if (pressState == PressedState.Up)
                        Mouse.Click(MouseEventF.LeftUp, numberOfClicks);
                    break;
                case MouseButtons.Middle:
                    if (pressState == PressedState.Click)
                        Mouse.Click(MouseEventF.MiddleClick, numberOfClicks);
                    else if (pressState == PressedState.Down)
                        Mouse.Click(MouseEventF.MiddleDown, numberOfClicks);
                    else if (pressState == PressedState.Up)
                        Mouse.Click(MouseEventF.MiddleUp, numberOfClicks);
                    break;
                case MouseButtons.Right:
                    if (pressState == PressedState.Click)
                        Mouse.Click(MouseEventF.RightClick, numberOfClicks);
                    else if (pressState == PressedState.Down)
                        Mouse.Click(MouseEventF.RightDown, numberOfClicks);
                    else if (pressState == PressedState.Up)
                        Mouse.Click(MouseEventF.RightUp, numberOfClicks);
                    break;
                case MouseButtons.XButton1:
                    if (pressState == PressedState.Click)
                        Mouse.Click(MouseEventF.XClick, MouseDataXButton.XButton1, numberOfClicks);
                    else if (pressState == PressedState.Down)
                        Mouse.Click(MouseEventF.XDown, MouseDataXButton.XButton1, numberOfClicks);
                    else if (pressState == PressedState.Up)
                        Mouse.Click(MouseEventF.XUp, MouseDataXButton.XButton1, numberOfClicks);
                    break;
                case MouseButtons.XButton2:
                    if (pressState == PressedState.Click)
                        Mouse.Click(MouseEventF.XClick, MouseDataXButton.XButton2, numberOfClicks);
                    else if (pressState == PressedState.Down)
                        Mouse.Click(MouseEventF.XDown, MouseDataXButton.XButton2, numberOfClicks);
                    else if (pressState == PressedState.Up)
                        Mouse.Click(MouseEventF.XUp, MouseDataXButton.XButton2, numberOfClicks);
                    break;
            }
        }

    }
}