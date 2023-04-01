using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;

namespace MouseAndKeyboard.Network;

//Base code: https://gist.github.com/darkguy2008/413a6fea3a5b4e67e5e0d96f750088a9
public class UDPSocket : IDisposable
{
    public delegate void NetReceive(int recievedBytes, byte[] data);
    public delegate void NetConnect(bool isServer);

    public event NetReceive? OnReceive;
    public event NetConnect? OnConnect;

    private readonly Socket socket = new(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);

    private byte[]? buffer;
    private Memory<byte> memBuffer;

    private CancellationTokenSource? cancelToken;

    public UDPSocket(bool clientCanHost = true, int bufferSize = NetworkUtil.DEFAULT_BUFFER_SIZE) : base()
    {
        ReuseAddress = clientCanHost;
        MySocket.SendBufferSize = bufferSize;
        MySocket.ReceiveBufferSize = bufferSize;
    }

    public Socket MySocket => socket;

    public bool ReuseAddress
    {
        get => (bool)MySocket.GetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress)!;
        set => MySocket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, value);
    }

    public virtual void Dispose()
    {
        OnReceive = null;
        OnConnect = null;
        cancelToken?.Cancel();
        MySocket.Close(); // Calls Dispose internally
        GC.SuppressFinalize(this);

        Logger.WriteLine("[Dispose Socket]");
    }

    public void Stop() => MySocket.Disconnect(true);

    #region Client

    public void StartClient(IPEndPoint remoteEP)
    {
        if (MySocket.Connected)
            return;
        MySocket.Connect(remoteEP);
        OnConnect?.Invoke(false);
    }

    #endregion


    #region Server
    private bool isServerRunning;

    public void StartServer(int port)
    {
        if (MySocket.IsBound)
            return;
        buffer = new byte[MySocket.SendBufferSize];
        memBuffer = buffer;
        cancelToken = new();
        MySocket.Bind(new IPEndPoint(IPAddress.Any, port));
        OnConnect?.Invoke(true);
        ResumeServer();
    }

    public void ResumeServer()
    {
        if (isServerRunning || !MySocket.IsBound || cancelToken == null || cancelToken.IsCancellationRequested)
            return;
        Task.Run(BeginReceiveNextPacket);
    }

    public void PauseServer()
    {
        if (!isServerRunning || !MySocket.IsBound || cancelToken == null || cancelToken.IsCancellationRequested)
            return;
        cancelToken?.Cancel();
    }

    private async Task BeginReceiveNextPacket()
    {
        isServerRunning = true;
        Logger.WriteLine("[Server Running]");
        while (MySocket.IsBound && !cancelToken!.IsCancellationRequested)
        {
            var receivedBytes = await MySocket.ReceiveAsync(memBuffer, SocketFlags.None, cancelToken.Token);
            OnReceive?.Invoke(receivedBytes, buffer!);
        }
        Logger.WriteLine("[Server Stop]");
        isServerRunning = false;
    }

    #endregion


}
