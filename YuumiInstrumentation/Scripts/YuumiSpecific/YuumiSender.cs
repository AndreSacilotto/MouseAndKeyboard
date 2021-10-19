using System;
using System.Collections.Generic;
using System.Windows.Forms;
using InputSimulation;
using MouseKeyboard.Network;
using Yuumi.Network;

public class YuumiSender : MKInputSender
{
    UDPSocketReceiver listener;

    private static Dictionary<Commands, Action<YummiPacketContent>> dict = new Dictionary<Commands, Action<YummiPacketContent>> {
        { Commands.MouseMove, MouseMove },
        { Commands.MouseScroll, MouseScroll },
        { Commands.MouseClick, MouseClick },
        { Commands.MouseDoubleClick, MouseDoubleClick },
        { Commands.Key, Key },
    };

    public YuumiSender(UDPSocketReceiver listener)
    {
        this.listener = listener;
        listener.OnReceive += OnReceive;
    }

    private void OnReceive(int bytes, byte[] data)
    {
        var mkContent = YummiPacket.ReadAll(data);

        Console.WriteLine("RECEIVE: " + mkContent.command);

        dict[mkContent.command](mkContent);
    }

    public static void MouseMove(YummiPacketContent content)
    {
        Mouse.MoveAbsolute(content.x, content.y);
    }

    public static void MouseScroll(YummiPacketContent content)
    {
        Mouse.ScrollWheel(content.quant);
    }

    public static void MouseClick(YummiPacketContent content)
    {
        MouseButtonExplicit.Click(content.pressedState, content.mouseButton);
    }

    public static void MouseDoubleClick(YummiPacketContent content)
    {
        MouseButtonExplicit.Click(content.pressedState, content.mouseButton, content.quant);
    }

    public static void Key(YummiPacketContent content)
    {
        if (content.pressedState == PressedState.Down)
            KeyboardVK.SendKeyDown(content.keys);
        else if (content.pressedState == PressedState.Up)
            KeyboardVK.SendKeyUp(content.keys);
        else
            KeyboardVK.SendFull(content.keys);
    }

    public override void Dispose()
    {
        listener.OnReceive -= OnReceive;
    }
}
