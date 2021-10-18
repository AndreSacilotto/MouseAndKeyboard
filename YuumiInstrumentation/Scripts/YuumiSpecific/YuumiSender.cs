using System;
using System.Collections.Generic;
using System.Windows.Forms;
using MouseKeyboard.Network;

public class YuumiSender : MKInputSenderConfig
{

    private void OnMouseDown(object sender, MouseEventArgs e)
    {
        MKEventHandleUtil.Print(e);
        Console.WriteLine("SEND: " + e.Button + " DOWN");

        mkPacket.WriteMouseClick(e.Button, InputSimulation.PressedState.Down);
        SendPacket();
    }

    private void OnMouseUp(object sender, MouseEventArgs e)
    {
        //MKEventHandleUtil.Print(e);
        Console.WriteLine("SEND: " + e.Button + " UP");

        mkPacket.WriteMouseClick(e.Button, InputSimulation.PressedState.Up);
        SendPacket();
    }

    private void OnMouseDoubleClick(object sender, MouseEventArgs e)
    {
        //MKEventHandleUtil.Print(e);
        Console.WriteLine("SEND: " + e.Button + " 2");

        mkPacket.WriteDoubleMouseClick(e.Button, 2);
        SendPacket();
    }

    private void OnKeyDown(object sender, KeyEventArgs e)
    {
        //MKEventHandleUtil.Print(e);
        Console.WriteLine("SEND: " + e.KeyCode + " DOWN");

        if (e.KeyCode == enablingKey)
        {
            if (enabled)
                Unsubscribe();
            else
                Subscribe();
            Console.WriteLine("Enabled: " + enabled);
        }
        else
        {
            MKEventHandleUtil.Print(e);
        }

        mkPacket.WriteKey(e.KeyCode, InputSimulation.PressedState.Down);
        SendPacket();
    }

    private void OnKeyUp(object sender, KeyEventArgs e)
    {
        //MKEventHandleUtil.Print(e);
        Console.WriteLine("SEND: " + e.KeyCode + " UP");

        mkPacket.WriteKey(e.KeyCode, InputSimulation.PressedState.Up);
        SendPacket();
    }
}
