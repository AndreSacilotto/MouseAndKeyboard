﻿using MouseAndKeyboard.InputSimulator;
using MouseAndKeyboard.Native;
using MouseAndKeyboard.Network;
using MouseAndKeyboard.Util;

namespace YuumiInstrumentation;

partial class YuumiSlave
{
    public static void ReadAll(YummiPacket packet)
    {
        packet.ReadCommand(out var cmd);
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
                packet.ReadMouseClick(out var mouseButton, out var pressedState);
                MouseClick(mouseButton, pressedState);
                break;
            }
            case Command.Key:
            {
                packet.ReadKeyPress(out var keys, out var pressedState);
                KeyPress(keys, pressedState);
                break;
            }
            case Command.KeyWithModifier:
            {
                packet.ReadKeyWithModifier(out var keys, out var mods, out var pressedState);
                KeyWithModifierPress(keys, mods, pressedState);
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

    private static void MouseClick(MouseButtons mouseButton, PressState pressedState)
    {
        Logger.WriteLine($"RECEIVE: MClick {mouseButton} {pressedState}");
        MouseSender.MouseButtonPress(mouseButton, pressedState);
    }

    private static void KeyPress(Keys keys, PressState pressedState)
    {
        Logger.WriteLine($"RECEIVE: KKey {keys} {pressedState}");
        KeyboardSender.KeyboardKeyPress(keys, pressedState);
    }

    private static void KeyWithModifierPress(Keys keys, Keys modifier, PressState pressedState)
    {
        Logger.WriteLine($"RECEIVE: KKeyMod {keys} {pressedState}");
        KeyboardSender.KeyboardKeyPress(keys, modifier, pressedState);
    }

}