using Others;

namespace MouseKeyboard.Network
{
    public class ConfigXML
    {
        public const string FILE_NAME = "config.xml";

        public string ip = "127.0.0.1";
        public int port = 7777;
        public bool isReceiver;

        public static ConfigXML Default => new ConfigXML()
        {
            ip = "127.0.0.1",
            port = 7777,
            isReceiver = false,
        };

        public static string GetPath() => XMLerialization.CurrentDirectory() + FILE_NAME;

        public override string ToString()
        {
            string str = $"{base.ToString()} | {ip} | {port} | {isReceiver}";
            return str;
        }

    }
}