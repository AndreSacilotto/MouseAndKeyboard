using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace MouseAndKeyboard.Network;

public static class NetworkUtil
{
    public const int DEFAULT_BUFFER_SIZE = 1 << 13; //8192

    public static IPAddress ToAddress(string ip) => IPAddress.Parse(ip);
    public static IPEndPoint ToAddress(string ip, int port) => new(IPAddress.Parse(ip), port);

    public static async Task<int> SendAsync(this Socket socket, Packet packet, SocketFlags flags = SocketFlags.None) =>
        await socket.SendAsync(packet.MemoryBuffer, flags);

    public static int Send(this Socket socket, Packet packet, out SocketError error, SocketFlags flags = SocketFlags.None) =>
        socket.Send(packet.Buffer, 0, packet.Pointer, flags, out error);
}