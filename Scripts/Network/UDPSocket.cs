using System.Net.Sockets;

//Base code: https://gist.github.com/darkguy2008/413a6fea3a5b4e67e5e0d96f750088a9

namespace MouseAndKeyboard.Network;

public abstract class UDPSocket : IDisposable
{
    protected bool ReuseAddress
    {
        get => (bool)MySocket.GetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress)!;
        set => MySocket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, value);
    }

    public Socket MySocket { get; } = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);

    public virtual void Dispose()
    {
        MySocket.Close(); // Calls Dispose internally
        GC.SuppressFinalize(this);
    }

}
