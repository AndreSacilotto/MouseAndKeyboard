using MouseAndKeyboard.Network;

namespace YuumiInstrumentation;

public interface IMKInput
{
	UDPSocket Socket { get; }
	bool Enabled { get; set; }

	void Stop();
}
