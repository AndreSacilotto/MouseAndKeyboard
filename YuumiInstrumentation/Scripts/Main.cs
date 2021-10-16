using System;
using System.Net;
using System.Threading.Tasks;
using System.Windows.Forms;

public class Main : ApplicationContext
{
    private readonly InputListener inputListener;
    //private readonly NetworkManager network;

    public Main()
    {
        ThreadExit += OnExit;

        inputListener = new InputListener(false);

        var ip = IPAddress.Parse("127.0.0.1");
        var port = 27000;

        var s = new UDPSocketHost(true);
        s.Start(ip, port);

        var c = new UDPSocketClient();
        c.Start(ip, port);
        c.Send("TEST!");
    }

    private void OnExit(object sender, EventArgs e)
    {
        inputListener.Dispose();
        ThreadExit -= OnExit;
    }
}
