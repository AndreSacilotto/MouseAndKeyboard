using System;
using System.Net;
using System.Net.Sockets;

public class UDPSocketClient : UDPSocket
{
    protected override void InternalStart(IPEndPoint hostEndPoint)
    {
        MySocket.Connect(hostEndPoint);
        //Receive();
    }

    public void Send(Packet packet, Action<int> callback = null)
    {
        if (MySocket.Connected)
            MySocket.BeginSend(packet.GetBuffer, 0, packet.Length, SocketFlags.None, (aResult) =>
            {
                int bytes = MySocket.EndSend(aResult);
                callback?.Invoke(bytes);
            }, null);
    }
}
