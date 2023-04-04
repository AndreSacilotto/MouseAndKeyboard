using MouseAndKeyboard.InputSimulator;
using MouseAndKeyboard.Native;
using MouseAndKeyboard.Util;

namespace YuumiInstrumentation;

partial class YuumiSlave
{
    public static void ReadAll(YummiPacket packet)
    {
        packet.Get(out Command cmd);
        switch (cmd)
        {
            case Command.MouseMove:
            {
                packet.ReadMouseMove(out var x, out var y);
                MouseMove(x, y);
                break;
            }
            case Command.MouseScroll:
            {
                packet.ReadMouseScroll(out var scrollDelta, out var isHorizontal);
                MouseScroll(scrollDelta, isHorizontal);
                break;
            }
            case Command.MouseClick:
            {
                packet.ReadMousePress(out var mouseButton, out var pressedState);
                MousePress(mouseButton, pressedState);
                break;
            }
            case Command.KeyPress:
            {
                packet.ReadKeyPress(out var key, out var pressedState);
                KeyPress(key, pressedState);
                break;
            }
            case Command.KeyPressWithModifier:
            {
                packet.ReadKeyPressWithModifier(out var key, out var mods, out var pressedState);
                KeyWithModifierPress(key, mods, pressedState);
                break;
            }
            case Command.Screen:
            {
                packet.ReadScreen(out var width, out var height);
                Screen(width, height);
                break;
            }
        }
    }

    private static void Screen(int width, int height)
    {
        Logger.WriteLine($"RECEIVE: Screen {width} {height}");
        MasterScreenSize = new(width, height);
    }

    private static void MouseMove(int x, int y)
    {
        x = Interpolation.Remap(MasterScreenSize.X, PrimaryMonitor.Instance.Width, x);
        y = Interpolation.Remap(MasterScreenSize.Y, PrimaryMonitor.Instance.Height, y);

        Logger.WriteLine($"RECEIVE: MMove {x} {y}");
        MouseSender.MoveAbsolute(x, y);
    }

    private static void MouseScroll(int scrollDelta, bool isHorizontal)
    {
        if (isHorizontal)
        {
            Logger.WriteLine($"RECEIVE: MHScroll {scrollDelta}");
            MouseSender.ScrollHWheel(scrollDelta);
        }
        else
        {
            Logger.WriteLine($"RECEIVE: MScroll {scrollDelta}");
            MouseSender.ScrollWheel(scrollDelta);
        }
    }

    private static void MousePress(MouseButtonsF mouseButton, PressState pressedState)
    {
        Logger.WriteLine($"RECEIVE: MClick {mouseButton} {pressedState}");

        if (pressedState == PressState.Click)
            MouseSender.SendButtonClick(mouseButton);
        else if (pressedState == PressState.Down)
            MouseSender.SendButtonDown(mouseButton);
        else if (pressedState == PressState.Up)
            MouseSender.SendButtonUp(mouseButton);
    }

    private static void KeyPress(VirtualKey key, PressState pressedState)
    {
        Logger.WriteLine($"RECEIVE: KKey {key} {pressedState}");

        if (pressedState == PressState.Click)
            KeyboardSender.SendKeyClick(key);
        else if (pressedState == PressState.Down)
            KeyboardSender.SendKeyDown(key);
        else if (pressedState == PressState.Up)
            KeyboardSender.SendKeyUp(key);
    }

    private static void KeyWithModifierPress(VirtualKey key, InputModifiers modifiers, PressState pressedState)
    {
        Logger.WriteLine($"RECEIVE: KMKey {key} {modifiers} {pressedState}");

        if (pressedState == PressState.Click)
            KeyboardSender.SendKeyClick(key, modifiers.ModifiersToVirtualKey());
        else if (pressedState == PressState.Down)
            KeyboardSender.SendKeyDown(key, modifiers.ModifiersToVirtualKey());
        else if (pressedState == PressState.Up)
            KeyboardSender.SendKeyUp(key, modifiers.ModifiersToVirtualKey());
    }

}