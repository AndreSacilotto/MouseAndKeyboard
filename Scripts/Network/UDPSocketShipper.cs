using System.Net;
using System.Net.Sockets;

namespace MouseAndKeyboard.Network;

public class UDPSocketShipper : UDPSocket
{
	protected override void InternalStart(IPEndPoint hostEndPoint)
	{
		MySocket.Connect(hostEndPoint);
	}

	public async void Send(Packet packet, Action<int> callback)
	{
		if (MySocket.Connected)
		{
			var bytes = await MySocket.SendAsync(packet.GetBuffer, SocketFlags.None);
			callback?.Invoke(bytes);
		}
	}

	public async void Send(Packet packet)
	{
		if (MySocket.Connected)
			await MySocket.SendAsync(packet.GetBuffer, SocketFlags.None);
	}

}