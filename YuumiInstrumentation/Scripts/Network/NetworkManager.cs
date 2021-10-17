using System.Net;

public class NetworkManager
{
    public UDPSocketClient Client { get; }
    public UDPSocketHost Host { get; }

    private IPEndPoint ep;

    public NetworkManager(string ip, int port, bool isSender, bool isListener)
    {
        ep = new IPEndPoint(IPAddress.Parse(ip), port);
        if (isSender)
            Client = new UDPSocketClient();

        if (isListener)
            Host = new UDPSocketHost(true);
    }

    public void Start()
    {
        Client?.Start(ep);
        Host?.Start(ep);
    }

    public void Stop()
    {
        Client?.Stop();
        Host?.Stop();
    }
}
