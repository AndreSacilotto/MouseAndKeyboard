using MouseAndKeyboard.Native;
using MouseAndKeyboard.Network;

namespace YuumiInstrumentation;

public partial class YuumiSlave : IDisposable
{
    public static Point MasterScreenSize { get; private set; } = PrimaryMonitor.Instance.GetSize();

    private readonly UDPSocket socket;

    private bool enabled;

    public UDPSocket Socket => socket;

    public YuumiSlave()
    {
#if DEBUG
        socket = new UDPSocket(true, YummiPacket.MAX_PACKET_BYTE_SIZE);
#else
		socket = new UDPSocket(false);
#endif
    }

    private void OnReceive(int bytes, byte[] data) => ReadAll(new YummiPacket(data));

    public bool Enabled
    {
        get => enabled;
        set
        {
            if (value == enabled)
                return;
            if (value)
                socket.OnReceive += OnReceive;
            else
                socket.OnReceive -= OnReceive;
            enabled = value;
        }
    }

    public void Dispose()
    {
        Enabled = false;
        socket.Dispose();
        GC.SuppressFinalize(this);
    }

}