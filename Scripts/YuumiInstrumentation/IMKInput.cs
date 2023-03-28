using MouseAndKeyboard.Network;

namespace YuumiInstrumentation;

public interface IMKInput : IDisposable
{
    UDPSocket Socket { get; }
    bool Enabled { get; }
}
