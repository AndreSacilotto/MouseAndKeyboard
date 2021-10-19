using System;
using MouseKeyboard.Network;
using System.Windows.Forms;
using Yuumi.Network;

public class YuumiListen : MKInputListen
{
    private readonly YummiPacket mkPacket;
    private readonly UDPSocketShipper client;

    private Keys enablingKey = Keys.Scroll;

    public YuumiListen(UDPSocketShipper client) : base()
    {
        this.client = client;
        this.mkPacket = new YummiPacket();

        if (Control.IsKeyLocked(enablingKey))
            Subscribe();
        else
            this.enabled = false;
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

        inputEvents.KeyDown += OnKeyDown;
        inputEvents.KeyUp += OnKeyUp;
    }

    public override void Unsubscribe()
    {
        base.Unsubscribe();

        inputEvents.MouseMove -= OnMouseMove;
        inputEvents.MouseDown -= OnMouseDown;
        inputEvents.MouseDoubleClick -= OnMouseDoubleClick;
        inputEvents.MouseWheel -= OnMouseScroll;

        inputEvents.KeyDown -= OnKeyDown;
        inputEvents.KeyUp -= OnKeyUp;
    }
    #endregion

    #region SEND

    private void SendPacket()
    {
        client.Send(mkPacket.GetPacket);
        mkPacket.Reset();
    }

    private void OnMouseMove(object sender, MouseEventArgs e)
    {
        //MKEventHandleUtil.Print(e);
        Console.WriteLine("SEND: " + e.X + " " + e.Y);

        mkPacket.WriteMouseMove(e.X, e.Y);
        SendPacket();
    }

    private void OnMouseScroll(object sender, MouseEventArgs e)
    {
        //MKEventHandleUtil.Print(e);
        Console.WriteLine("SEND: " + e.Delta);

        mkPacket.WriteMouseScroll(e.Delta);
        SendPacket();
    }

    private void OnMouseDown(object sender, MouseEventArgs e)
    {
        //MKEventHandleUtil.Print(e);
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
            //MKEventHandleUtil.Print(e);
            mkPacket.WriteKey(e.KeyCode, InputSimulation.PressedState.Down);
            SendPacket();
        }

    }

    private void OnKeyUp(object sender, KeyEventArgs e)
    {
        //MKEventHandleUtil.Print(e);
        Console.WriteLine("SEND: " + e.KeyCode + " UP");

        mkPacket.WriteKey(e.KeyCode, InputSimulation.PressedState.Up);
        SendPacket();
    }

    #endregion

}
