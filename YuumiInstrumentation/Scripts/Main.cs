using System;
using System.Net;
using System.Windows.Forms;
using MouseKeyboardPacket;

using static System.Console;

public class Main : ApplicationContext
{
    //private readonly InputListener inputListener;

    public Main()
    {
        ThreadExit += OnExit;

        //inputListener = new InputListener(false);

        var config = ConfigXML.FromXML(ConfigXML.GetPath());
        WriteLine(config);

        var ip = IPAddress.Parse("127.0.0.1");
        var port = 27000;

        var s = new UDPSocketHost(true);
        s.Start(ip, port);
        s.OnReceive += OnReceive;

        var c = new UDPSocketClient();
        c.Start(ip, port);

        var p = new MKPacket();
        //p.WriteMouseMove(100, 100);
        p.WriteMouseClick(MouseButtons.Middle);

        c.Send(p.GetPacket, x => Console.WriteLine(x));
    }

    private void OnReceive(int bytes, byte[] data)
    {
        MKPacket.ReadAll(data).Print();
        Console.WriteLine(bytes);
    }

    private void OnExit(object sender, EventArgs e)
    {
        //inputListener.Dispose();
        ThreadExit -= OnExit;
    }
}
