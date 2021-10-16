using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

public class UDPSocketClient : UDPSocket
{

    protected override void InternalStart(IPEndPoint hostEndPoint)
    {
        Socket.Connect(hostEndPoint);
        Receive();
    }
}
