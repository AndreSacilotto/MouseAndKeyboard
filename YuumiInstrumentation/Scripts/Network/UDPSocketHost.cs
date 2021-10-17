﻿using System.Net;
using System.Net.Sockets;

public class UDPSocketHost : UDPSocket
{
    private byte[] stateBuffer;

    public delegate void NetReceive(int bytes, byte[] data);
    public event NetReceive OnReceive;

    public UDPSocketHost(bool clientCanHost = false) : base()
    {
        if (clientCanHost)
            MySocket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, true);
    }

    protected override void InternalStart(IPEndPoint hostEndPoint)
    {
        stateBuffer = new byte[MySocket.SendBufferSize];
        MySocket.Bind(hostEndPoint);
        StartReceiving();
    }

    protected void StartReceiving()
    {
        MySocket.BeginReceive(stateBuffer, 0, stateBuffer.Length, SocketFlags.None, recv = (aResult) =>
        {
            var ar = (byte[])aResult.AsyncState;
            int bytes = MySocket.EndReceive(aResult);
            OnReceive?.Invoke(bytes, ar);
            MySocket.BeginReceive(stateBuffer, 0, stateBuffer.Length, SocketFlags.None, recv, ar);
        }, stateBuffer);
    }
}
