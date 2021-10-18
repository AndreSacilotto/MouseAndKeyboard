using System;

using static InputSimulation.Mouse;

namespace InputSimulation
{
    public static class MouseButtonExplicit
    {

        #region Left (MB 0)
        public static void ClickLeftDown()
        {
            var dwFlags = MouseEventF.LeftDown;
            Click(dwFlags);
        }

        public static void ClickLeftUp()
        {
            var dwFlags = MouseEventF.LeftUp;
            Click(dwFlags);
        }

        public static void ClickLeftFull()
        {
            var dwFlags = MouseEventF.LeftClick;
            Click(dwFlags);
        }

        public static void ClickLeftDown(int numberOfClicks = 2)
        {
            var dwFlags = MouseEventF.LeftDown;
            Click(dwFlags, numberOfClicks);
        }

        public static void ClickLeftUp(int numberOfClicks = 2)
        {
            var dwFlags = MouseEventF.LeftUp;
            Click(dwFlags, numberOfClicks);
        }

        public static void ClickLeftFull(int numberOfClicks = 2)
        {
            var dwFlags = MouseEventF.LeftClick;
            Click(dwFlags, numberOfClicks);
        }

        #endregion

        #region Middle (MB 1)
        public static void ClickMiddleDown()
        {
            var dwFlags = MouseEventF.MiddleDown;
            Click(dwFlags);
        }

        public static void ClickMiddleUp()
        {
            var dwFlags = MouseEventF.MiddleUp;
            Click(dwFlags);
        }

        public static void ClickMiddleFull()
        {
            var dwFlags = MouseEventF.MiddleClick;
            Click(dwFlags);
        }

        public static void ClickMiddleDown(int numberOfClicks = 2)
        {
            var dwFlags = MouseEventF.MiddleDown;
            Click(dwFlags, numberOfClicks);
        }

        public static void ClickMiddleUp(int numberOfClicks = 2)
        {
            var dwFlags = MouseEventF.MiddleUp;
            Click(dwFlags, numberOfClicks);
        }

        public static void ClickMiddleFull(int numberOfClicks = 2)
        {
            var dwFlags = MouseEventF.MiddleClick;
            Click(dwFlags, numberOfClicks);
        }
        #endregion

        #region Right (MB 2)
        public static void ClickRightDown()
        {
            var dwFlags = MouseEventF.RightDown;
            Click(dwFlags);
        }

        public static void ClickRightUp()
        {
            var dwFlags = MouseEventF.RightUp;
            Click(dwFlags);
        }

        public static void ClickRightFull()
        {
            var dwFlags = MouseEventF.RightClick;
            Click(dwFlags);
        }

        public static void ClickRightDown(int numberOfClicks = 2)
        {
            var dwFlags = MouseEventF.RightDown;
            Click(dwFlags, numberOfClicks);
        }

        public static void ClickRightUp(int numberOfClicks = 2)
        {
            var dwFlags = MouseEventF.RightUp;
            Click(dwFlags, numberOfClicks);
        }

        public static void ClickRightFull(int numberOfClicks = 2)
        {
            var dwFlags = MouseEventF.RightClick;
            Click(dwFlags, numberOfClicks);
        }
        #endregion


        #region Side1 or XB1 (MB 3)

        public static void ClickXB1Down()
        {
            var dwFlags = MouseEventF.XDown;
            var mouseData = MouseDataXButton.XButton1;
            Click(dwFlags, mouseData);
        }

        public static void ClickXB1Up()
        {
            var dwFlags = MouseEventF.XUp;
            var mouseData = MouseDataXButton.XButton1;
            Click(dwFlags, mouseData);
        }

        public static void ClickXB1Full()
        {
            var dwFlags = MouseEventF.XClick;
            var mouseData = MouseDataXButton.XButton1;
            Click(dwFlags, mouseData);
        }

        public static void ClickXB1Down(int numberOfClicks = 2)
        {
            var dwFlags = MouseEventF.XDown;
            var mouseData = MouseDataXButton.XButton1;
            Click(dwFlags, mouseData, numberOfClicks);
        }

        public static void ClickXB1Up(int numberOfClicks = 2)
        {
            var dwFlags = MouseEventF.XUp;
            var mouseData = MouseDataXButton.XButton1;
            Click(dwFlags, mouseData, numberOfClicks);
        }

        public static void ClickXB1Full(int numberOfClicks = 2)
        {
            var dwFlags = MouseEventF.XClick;
            var mouseData = MouseDataXButton.XButton1;
            Click(dwFlags, mouseData, numberOfClicks);
        }

        #endregion

        #region Side2 or XB2  (MB 4)
        public static void ClickXB2Down()
        {
            var dwFlags = MouseEventF.XDown;
            var mouseData = MouseDataXButton.XButton2;
            Click(dwFlags, mouseData);
        }

        public static void ClickXB2Up()
        {
            var dwFlags = MouseEventF.XUp;
            var mouseData = MouseDataXButton.XButton2;
            Click(dwFlags, mouseData);
        }

        public static void ClickXB2Full()
        {
            var dwFlags = MouseEventF.XClick;
            var mouseData = MouseDataXButton.XButton2;
            Click(dwFlags, mouseData);
        }

        public static void ClickXB2Down(int numberOfClicks = 2)
        {
            var dwFlags = MouseEventF.XDown;
            var mouseData = MouseDataXButton.XButton2;
            Click(dwFlags, mouseData, numberOfClicks);
        }

        public static void ClickXB2Up(int numberOfClicks = 2)
        {
            var dwFlags = MouseEventF.XUp;
            var mouseData = MouseDataXButton.XButton2;
            Click(dwFlags, mouseData, numberOfClicks);
        }

        public static void ClickXB2Full(int numberOfClicks = 2)
        {
            var dwFlags = MouseEventF.XClick;
            var mouseData = MouseDataXButton.XButton2;
            Click(dwFlags, mouseData, numberOfClicks);
        }
        #endregion
    }
}