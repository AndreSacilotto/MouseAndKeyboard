﻿using MouseKeyboard.Network;
using System;
using System.Text;
using System.Windows.Forms;

public class Main : ApplicationContext
{
    private readonly NetworkManager networkManager;
    private readonly MKInputListen mkListener;

    public Main()
    {
        ThreadExit += OnExit;

        var filePath = ConfigXML.GetPath();
        var config = XMLerialization.FromXMLFile<ConfigXML>(filePath);
        if (config == null)
        {
            Console.WriteLine($"No {ConfigXML.FILE_NAME} File Found");
            XMLerialization.ToXMLFile<ConfigXML>(filePath, Encoding.ASCII, ConfigXML.Default);
            return;
        }

        networkManager = new NetworkManager(config.ip, config.port, config.sender, config.listener);
        
        if (config.listener && !config.sender)
        {
            networkManager.Listener.MySocket.SendBufferSize = MKPacketWriter.MAX_PACKET_BYTE_SIZE;
            var senderConfig = new MKInputSenderConfig();

            networkManager.Listener.OnReceive += OnReceive;
        }

        if (!config.listener && config.sender)
        {
            bool scrollLock = Control.IsKeyLocked(Keys.Scroll);
            mkListener = new MKInputListen(networkManager.Sender, scrollLock) {
                enablingKey = Keys.Scroll,
            };
        }

        networkManager.Start();
    }

    private void OnReceive(int bytes, byte[] data)
    {
        var mkContent = MKPacketReader.ReadAll(data);

        Console.WriteLine("RECEIVE: " + mkContent.command);

        //if(MKInputSender.TryGetFunc(mkContent.command, out var mkfunc))
        //    mkfunc(mkContent);

        MKInputSender.GetFunc(mkContent.command)(mkContent);
    }

    private void OnExit(object sender, EventArgs e)
    {
        ThreadExit -= OnExit;
        mkListener?.Dispose();
        networkManager?.Stop();
    }
}
