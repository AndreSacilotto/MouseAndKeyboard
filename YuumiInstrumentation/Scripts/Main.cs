using MouseKeyboard.Network;
using System;
using System.Text;
using System.Windows.Forms;
using Yuumi.Config;
using Yuumi.Network;
using YuumiInstrumentation.Scripts.ConfigFile;

public class Main : ApplicationContext
{
    private readonly NetworkManager networkManager;

    private readonly MKInputSender mkSender;
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
            networkManager.Listener.MySocket.SendBufferSize = YummiPacket.MAX_PACKET_BYTE_SIZE;
            MKInputSender listen = new YuumiSender(networkManager.Listener);
        }

        if (!config.listener && config.sender)
        {
            MKInputListen listen = new YuumiListen(networkManager.Sender);
        }

        networkManager.Start();
    }
    private void OnExit(object sender, EventArgs e)
    {
        ThreadExit -= OnExit;
        mkSender?.Dispose();
        mkListener?.Dispose();
        networkManager?.Stop();
    }
}
