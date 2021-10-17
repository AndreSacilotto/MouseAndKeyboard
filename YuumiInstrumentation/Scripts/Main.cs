using MouseKeyboard.Network;
using System;
using System.Windows.Forms;

public class Main : ApplicationContext
{
    private readonly NetworkManager networkManager;
    private readonly MKInputListener mkListener;

    public Main()
    {
        ThreadExit += OnExit;

        var config = ConfigXML.FromXML(ConfigXML.GetPath());
        if (config == null)
        {
            ExitThread();
            return;
        }

        networkManager = new NetworkManager(config.ip, config.port, config.sender, config.listener);

        if (config.listener && !config.sender)
        {
            networkManager.Host.MySocket.SendBufferSize = MKPacket.MAX_PACKET_BYTE_SIZE;
            networkManager.Host.OnReceive += OnReceive;
        }

        if (!config.listener && config.sender)
        {
            bool scrollLock = Control.IsKeyLocked(Keys.Scroll);
            mkListener = new MKInputListener(networkManager.Client, scrollLock) {
                enablingKey = Keys.Scroll,
                pingKey = Keys.NumPad0,
                shutdownKey = Keys.NumPad1,
            };
        }

        networkManager.Start();
    }

    private void OnReceive(int bytes, byte[] data)
    {
        var mkContent = MKPacket.ReadAll(data);

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
