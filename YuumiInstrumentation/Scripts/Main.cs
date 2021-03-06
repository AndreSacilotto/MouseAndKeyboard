using MouseKeyboard.MKInput;
using MouseKeyboard.Network;
using Others;
using System;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using YuumiInstrumentation;

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
            ExitThread();
            return;
        }

        networkManager = new NetworkManager(config.ip, config.port, config.isReceiver, !config.isReceiver);

        if (config.isReceiver)
            mkSender = new YuumiSender(networkManager.Listener);
        else
            mkListener = new YuumiListen(networkManager.Sender);

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
