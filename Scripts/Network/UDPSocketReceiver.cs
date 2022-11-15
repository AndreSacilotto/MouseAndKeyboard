using System.Net;
using System.Net.Sockets;

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

	void BeginReceiveNextPacket() =>
		MySocket.BeginReceive(stateBuffer, 0, stateBuffer.Length, SocketFlags.None, EndReceiveNextPacket, stateBuffer);

	void EndReceiveNextPacket(IAsyncResult result)
	{
		try
		{
			int bytes = MySocket.EndReceive(result);
			OnReceive?.Invoke(bytes, (byte[])result.AsyncState);
			BeginReceiveNextPacket();
		}
		catch (SocketException)
		{
			if (!closed)
				throw;
		}
	}

}