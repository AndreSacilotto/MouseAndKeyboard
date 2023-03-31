using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace MouseAndKeyboard.Network;

public class UDPSocketShipper : UDPSocket
{
    protected override void StartInternal(IPEndPoint hostEndPoint)
    {
        MySocket.Connect(hostEndPoint);
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