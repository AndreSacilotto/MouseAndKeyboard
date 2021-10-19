using InputSimulation;
using System.Windows.Forms;

namespace YuumiInstrumentation
{
    public interface IYuumiPacketReceiver
    {
        void MouseMove(int x, int y);
        void MouseScroll(int scrollDelta);
        void MouseClick(MouseButtons mouseButton, PressedState pressedState);
        void Key(Keys keys, PressedState pressedState);
        void KeyModifier(Keys keys, Keys modifier, PressedState pressedState);
    }
}