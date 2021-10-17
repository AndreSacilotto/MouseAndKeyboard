using System;
using System.Net;
using System.Windows.Forms;
using MouseKeyboardPacket;

using static System.Console;

public class Main : ApplicationContext
{
    private readonly NetworkManager networkManager;
    private readonly InputListener inputListener;
    private readonly MKPacket mkPacket = new MKPacket();

    public Main()
    {
        ThreadExit += OnExit;

        var config = ConfigXML.FromXML(ConfigXML.GetPath());
        networkManager = new NetworkManager(config.ip, config.port, config.sender, config.listener);

        if (config.listener)
        {
            networkManager.Host.MySocket.SendBufferSize = MKPacket.MAX_PACKET_BYTE_SIZE;
            networkManager.Host.OnReceive += OnReceive;
        }

        if (config.sender)
        {
            inputListener = new InputListener();
            inputListener.OnMoveMove += OnMoveMove;
            inputListener.OnMouseScroll += OnMouseScroll;
            inputListener.OnMouseDown += OnMouseDown;
            inputListener.OnMouseDoubleClick += OnMouseDoubleClick;

            inputListener.OnKeyDown += OnKeyDown;
            inputListener.OnKeyUp += OnKeyUp;
        }


        networkManager.Start();
    }

    private void OnReceive(int bytes, byte[] data)
    {
        WriteLine("RECIVE");
        var mk = MKPacket.ReadAll(data);
        mk.Print();
        WriteLine(bytes);
    }

    private void OnMoveMove(object sender, MouseEventArgs e)
    {
        //InputListenerUtil.Print(e);
        //mkPacket.Reset();
        //mkPacket.WriteMouseMove(e.X, e.Y);
        //networkManager.Client.Send(mkPacket.GetPacket);
    }

    private void OnMouseScroll(object sender, MouseEventArgs e)
    {
        //InputListenerUtil.Print(e);
        //mkPacket.Reset();
        //mkPacket.WriteMouseScroll(e.Delta);
        //networkManager.Client.Send(mkPacket.GetPacket);
    }

    private void OnMouseDown(object sender, MouseEventArgs e)
    {
        //InputListenerUtil.Print(e);
        //mkPacket.Reset();
        //mkPacket.WriteMouseClick(e.Button);
        //networkManager.Client.Send(mkPacket.GetPacket);
    }

    private void OnMouseDoubleClick(object sender, MouseEventArgs e)
    {
        //InputListenerUtil.Print(e);
        //mkPacket.Reset();
        //mkPacket.WriteDoubleMouseClick(e.Button, 2);
        //networkManager.Client.Send(mkPacket.GetPacket);
    }

    private void OnKeyDown(object sender, KeyEventArgs e)
    {
        InputListenerUtil.Print(e);
        //mkPacket.Reset();
        //mkPacket.WriteKeyDown(e.KeyCode);
        //networkManager.Client.Send(mkPacket.GetPacket);
    }

    private void OnKeyUp(object sender, KeyEventArgs e)
    {
        InputListenerUtil.Print(e);
        //mkPacket.Reset();
        //mkPacket.WriteKeyUp(e.Button);
        //networkManager.Client.Send(mkPacket.GetPacket);
    }

    private void OnExit(object sender, EventArgs e)
    {
        inputListener.Dispose();
        networkManager.Stop();
        ThreadExit -= OnExit;
    }
}
