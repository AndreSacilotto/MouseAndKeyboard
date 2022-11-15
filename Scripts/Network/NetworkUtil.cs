using System.Net;

namespace MouseAndKeyboard.Network;

public static class NetworkUtil
{
	public static IPAddress ToAddress(string ip) => IPAddress.Parse(ip);
	public static IPEndPoint ToAddress(string ip, int port) => new(IPAddress.Parse(ip), port);
}