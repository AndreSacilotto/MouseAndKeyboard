using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace MouseAndKeyboard.Network;

public class UDPSocketShipper : UDPSocket
{
    public event Action<UDPSocketShipper>? OnConnect;

    public UDPSocketShipper(bool clientCanHost = true) : base()
    {
        ReuseAddress = clientCanHost;
    }

    public void Start(IPEndPoint hostEndPoint)
    {
        if (MySocket.Connected)
            return;
        MySocket.Connect(hostEndPoint);
        OnConnect?.Invoke(this);
    }

    public async Task<int> SendAsync(Packet packet)
    {
        if (MySocket.Connected)
            return await MySocket.SendAsync(packet.MemoryBuffer, SocketFlags.None);
        return 0;
    }

    public int Send(Packet packet)
    {
        if (MySocket.Connected)
            return MySocket.Send(packet.ReadOnlySpan, SocketFlags.None);
        return 0;
    }

}