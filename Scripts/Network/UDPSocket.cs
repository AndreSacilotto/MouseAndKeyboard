using System.Net;
using System.Net.Sockets;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ToolTip;

namespace MouseAndKeyboard.Network;


//Base code: https://gist.github.com/darkguy2008/413a6fea3a5b4e67e5e0d96f750088a9

public abstract class UDPSocket
{
	protected bool closed = false;

	public Socket MySocket { get; } = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
	public IPEndPoint HostEndPoint { get; private set; }

	protected EndPoint senderEndPoint = new IPEndPoint(IPAddress.Any, 0);
	protected AsyncCallback recv = null;

	public void Stop()
	{
		closed = true;
		MySocket.Close();
	}

	public void Start(IPEndPoint endPoint)
	{
		if (MySocket.Connected)
			return;
		HostEndPoint = endPoint;
		InternalStart(endPoint);
	}
	public void Start(IPAddress address, int port)
	{
		if (MySocket.Connected)
			return;
		HostEndPoint = new IPEndPoint(address, port);
		InternalStart(HostEndPoint);
	}

	protected abstract void InternalStart(IPEndPoint hostEndPoint);

	public static IPAddress ToAddress(string ip) => IPAddress.Parse(ip);
	public static IPEndPoint ToAddress(string ip, int port) => new(IPAddress.Parse(ip), port);

}