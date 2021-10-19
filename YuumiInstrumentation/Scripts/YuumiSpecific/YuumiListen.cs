﻿using System;
using System.Collections.Generic;
using System.Windows.Forms;

using MouseKeyboard.MKInput;
using MouseKeyboard.Network;
using InputSimulation;

public class YuumiListen : MKInputListen
{
    private readonly YummiPacket mkPacket;
    private readonly UDPSocketShipper client;

    private Keys enablingKey = Keys.Scroll;

    public YuumiListen(UDPSocketShipper client) : base()
    {
        this.client = client;
        this.mkPacket = new YummiPacket();

        inputEvents.KeyDown += OnKeyDown;
        if (Control.IsKeyLocked(enablingKey))
            Subscribe();
        else
            this.enabled = false;
    }
    public override void Dispose()
    {
        inputEvents.KeyDown -= OnKeyDown;
        base.Dispose();
    }

    #region SUB
    public override void Subscribe()
    {
        base.Subscribe();

        inputEvents.MouseMove += OnMouseMove;
        inputEvents.MouseDown += OnMouseDown;
        inputEvents.MouseUp += OnMouseUp;
        inputEvents.MouseDoubleClick += OnMouseDoubleClick;
        inputEvents.MouseWheel += OnMouseScroll;

        //inputEvents.KeyDown += OnKeyDown;
        inputEvents.KeyUp += OnKeyUp;
    }

    public override void Unsubscribe()
    {
        base.Unsubscribe();

        inputEvents.MouseMove -= OnMouseMove;
        inputEvents.MouseDown -= OnMouseDown;
        inputEvents.MouseDoubleClick -= OnMouseDoubleClick;
        inputEvents.MouseWheel -= OnMouseScroll;

        //inputEvents.KeyDown -= OnKeyDown;
        inputEvents.KeyUp -= OnKeyUp;
    }
    #endregion

    #region UTIL

    private void SendPacket()
    {
        client.Send(mkPacket.GetPacket);
        mkPacket.Reset();
    }

    private void WriteMouse(MouseButtons mouseButtons, PressedState pressedState)
    {
        mkPacket.WriteMouseClick(mouseButtons, pressedState);
        SendPacket();
    }

    private void WriteKey(Keys key, PressedState pressedState)
    {
        mkPacket.WriteKey(key, pressedState);
        SendPacket();
    }

    private void EnableFunc()
    {
        if (enabled)
            Unsubscribe();
        else
            Subscribe();
        Console.WriteLine("Enabled: " + enabled);
    }

    #endregion

    #region EVENTS
    private void OnMouseMove(object sender, MouseEventArgs e)
    {
        //MKEventHandleUtil.Print(e);
        //Console.WriteLine("SEND: " + e.X + " " + e.Y);

        mkPacket.WriteMouseMove(e.X, e.Y);
        SendPacket();
    }

    private void OnMouseScroll(object sender, MouseEventArgs e)
    {
        //MKEventHandleUtil.Print(e);
        //Console.WriteLine("SEND Delta: " + e.Delta);

        mkPacket.WriteMouseScroll(e.Delta);
        SendPacket();
    }

    private void OnMouseDown(object sender, MouseEventArgs e)
    {
        Console.WriteLine(Control.ModifierKeys);
        //MKEventHandleUtil.Print(e);
        UnifyMouse(e, PressedState.Down);
    }

    private void OnMouseUp(object sender, MouseEventArgs e)
    {
        //MKEventHandleUtil.Print(e);
        UnifyMouse(e, PressedState.Up);
    }

    private void OnMouseDoubleClick(object sender, MouseEventArgs e)
    {
        //MKEventHandleUtil.Print(e);
        Console.WriteLine($"SEND {"2", 5}: " + e.Button);
        mkPacket.WriteDoubleMouseClick(e.Button, 2);
        SendPacket();
    }

    private void OnKeyDown(object sender, KeyEventArgs e)
    {
        //MKEventHandleUtil.Print(e);
        if (e.KeyCode == enablingKey)
            EnableFunc();
        else
            UnifyKey(e, PressedState.Down);
    }

    private void OnKeyUp(object sender, KeyEventArgs e)
    {
        if (e.KeyCode == enablingKey)
            return;
        else
            UnifyKey(e, PressedState.Up);    
    }
    #endregion

    #region Unify

    private void UnifyMouse(MouseEventArgs e, PressedState pressed)
    {
        if (MouseButtonExplicit.Ctrl && mouseWithControlToKey.TryGetValue(e.Button, out var key))
        {
            Console.WriteLine($"SEND M{pressed,5}: {e.Button} => {key} | Shift");
            WriteKey(key, pressed);
        }
        if (mouseToKey.TryGetValue(e.Button, out key))
        {
            Console.WriteLine($"SEND M{pressed,5}: {e.Button} => {key}");
            WriteKey(key, pressed);
        }
        else
        {
            Console.WriteLine($"SEND M{pressed,5}: {e.Button}");
            WriteMouse(e.Button, pressed);
        }
    }


    private void UnifyKey(KeyEventArgs e, PressedState pressed)
    {
        if (alwaysAllowedKeys.Contains(e.KeyCode))
        {
            Console.WriteLine($"SEND K{pressed,5}: {e.KeyCode}");
            WriteKey(e.KeyCode, pressed);
        }
        else if (MouseButtonExplicit.Ctrl && allowedWithControlKeys.Contains(e.KeyCode))
        {
            Console.WriteLine($"SEND K{pressed,5}: {e.KeyCode} | Shift");
            WriteKey(e.KeyCode, pressed);
        }
        else if (allowedKeys.TryGetValue(e.KeyCode, out var key))
        {
            Console.WriteLine($"SEND K{pressed,5}: {e.KeyCode} => {key}");
            WriteKey(key, pressed);
        }
    }

    #endregion

    //Q W E R - Skills 4
    //D F - Spells 2 (DONE)
    //Space D1 D2 D4 - Itens 4
    //Y P - HUD 2 (DONE)

    private static Dictionary<MouseButtons, Keys> mouseToKey = new Dictionary<MouseButtons, Keys> {
        { MouseButtons.XButton1, Keys.Q },
        { MouseButtons.XButton2, Keys.E },
    };

    private static Dictionary<MouseButtons, Keys> mouseWithControlToKey = new Dictionary<MouseButtons, Keys> {
        { MouseButtons.XButton1, Keys.D },
        { MouseButtons.XButton2, Keys.F },
    };

    private static HashSet<Keys> alwaysAllowedKeys = new HashSet<Keys> {
        Keys.LShiftKey,
        Keys.LControlKey,
        Keys.LMenu,
    };

    private static Dictionary<Keys, Keys> allowedKeys = new Dictionary<Keys, Keys> {
        { Keys.Up, Keys.W },
        { Keys.OemBackslash, Keys.R },
    };

    private static HashSet<Keys> allowedWithControlKeys = new HashSet<Keys> {
        Keys.Space,
        Keys.D1,
        Keys.D2,
        Keys.D4,
        Keys.Y,
        Keys.P,
    };

}