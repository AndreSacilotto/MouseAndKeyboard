using System.Net;
using System.Net.Sockets;
using System.Text;

namespace MouseAndKeyboard.Network;

public class UDPSocketReceiver : UDPSocket
{
	private byte[] stateBuffer;

	public delegate void NetReceive(int bytes, byte[] data);
	public event NetReceive OnReceive;

	public UDPSocketReceiver(bool clientCanHost = false) : base()
	{
		if (clientCanHost)
			MySocket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, true);
	}

	protected override void InternalStart(IPEndPoint hostEndPoint)
	{
		stateBuffer = new byte[MySocket.SendBufferSize];
		MySocket.Bind(hostEndPoint);
		BeginReceiveNextPacket();
	}

	private async void BeginReceiveNextPacket()
	{
		try
		{
			while (!closed)
			{
				//var buffer = new byte[MySocket.SendBufferSize];
				var received = await MySocket.ReceiveAsync(stateBuffer, SocketFlags.None);
				OnReceive?.Invoke(received, stateBuffer);
			}
		}
		catch (SocketException)
		{
			if (!closed)
				throw;
		}
	}

}