using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;

namespace MouseAndKeyboard.Network;

//Base code: https://gist.github.com/darkguy2008/413a6fea3a5b4e67e5e0d96f750088a9
public class UDPSocket : IDisposable
{
    public delegate void NetReceive(int recievedBytes, byte[] data);

    public event NetReceive? OnReceive;
    public event Action? OnConnect;

    private readonly Socket socket = new(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);

    private byte[]? buffer;
    private Memory<byte> memBuffer;

    private CancellationTokenSource? cancelToken;

    private bool isClient, isServer;

    public bool IsClient => isClient;
    public bool IsServer => isServer;

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
        isServer = false;
        isClient = false;
        OnReceive = null;
        OnConnect = null;
        if (cancelToken != null)
        {
            cancelToken.Cancel();
            cancelToken.Dispose();
        }
        MySocket.Close(); // Calls Dispose internally
        GC.SuppressFinalize(this);
        Logger.WriteLine("[Dispose Socket]");
    }

    public void Stop()
    {
        isServer = false;
        isClient = false;
        MySocket.Disconnect(true);
    }

    #region Client

    public void StartClient(IPEndPoint remoteEP)
    {
        if (MySocket.Connected)
            return;
        isClient = true;
        MySocket.Connect(remoteEP);
        OnConnect?.Invoke();
    }

    #endregion


    #region Server
    public void StartServer(int port)
    {
        if (MySocket.IsBound)
            return;
        isServer = true;
        buffer = new byte[MySocket.SendBufferSize];
        memBuffer = buffer;
        cancelToken = new();
        MySocket.Bind(new IPEndPoint(IPAddress.Any, port));
        OnConnect?.Invoke();
        ResumeServer();
    }

    public void ResumeServer()
    {
        if (!MySocket.IsBound || cancelToken == null || cancelToken.IsCancellationRequested)
            return;
        Task.Run(BeginReceiveNextPacket, cancelToken.Token);
    }

    public void PauseServer()
    {
        if (!MySocket.IsBound || cancelToken == null || cancelToken.IsCancellationRequested)
            return;
        cancelToken.Cancel();
    }

    private async Task BeginReceiveNextPacket()
    {
        Logger.WriteLine("[Server Running]");
        while (MySocket.IsBound)
        {
            var receivedBytes = await MySocket.ReceiveAsync(memBuffer, SocketFlags.None, cancelToken!.Token);
            OnReceive?.Invoke(receivedBytes, buffer!);
        }
    }

    #endregion


}
