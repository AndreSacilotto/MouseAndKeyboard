using System.Net;

public class NetworkManager
{
    public UDPSocketSender Sender { get; }
    public UDPSocketListener Listener { get; }

    private IPEndPoint ep;

    public NetworkManager(string ip, int port, bool isSender, bool isListener)
    {
        ep = new IPEndPoint(IPAddress.Parse(ip), port);
        if (isSender)
            Sender = new UDPSocketSender();

        if (isListener)
            Listener = new UDPSocketListener(true);
    }

    public void Start()
    {
        Sender?.Start(ep);
        Listener?.Start(ep);
    }

    public void Stop()
    {
        Sender?.Stop();
        Listener?.Stop();
    }
}
