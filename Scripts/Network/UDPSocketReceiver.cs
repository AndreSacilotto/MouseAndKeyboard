using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;

namespace MouseAndKeyboard.Network;

public class UDPSocketReceiver : UDPSocket
{
    public delegate void NetReceive(int recievedBytes, byte[] data);
    public event NetReceive? OnReceive;

    private byte[]? buffer;
    private Memory<byte> memBuffer;

    private CancellationTokenSource? cancellationToken;

    public UDPSocketReceiver(bool clientCanHost = true) : base()
    {
        ReuseAddress = clientCanHost;
    }

    public void Start(int port)
    {
        if (MySocket.IsBound)
            return;
        buffer = new byte[MySocket.SendBufferSize];
        memBuffer = buffer;
        cancellationToken = new();
        MySocket.Bind(new IPEndPoint(IPAddress.Any, port));
        Task.Run(BeginReceiveNextPacket);
    }

    public override void Dispose()
    {
        cancellationToken?.Cancel();
        GC.SuppressFinalize(this);
        base.Dispose();
    }

    private async Task BeginReceiveNextPacket()
    {
        //try
        //{
        while (MySocket.IsBound && !cancellationToken!.IsCancellationRequested)
        {
            var receivedBytes = await MySocket.ReceiveAsync(memBuffer, SocketFlags.None, cancellationToken.Token);
            OnReceive?.Invoke(receivedBytes, buffer!);
        }
        //}
        //catch (SocketException)
        //{
        //    if (!MySocket.Connected)
        //        throw;
        //}
    }

}