using System;
using System.Collections.Generic;
using System.Windows.Forms;
using YuumiInstrumentation.Scripts.ConfigFile;

namespace MouseKeyboard.Network
{
    public class ConfigXML
    {
        public const string FILE_NAME = "config.xml";

        public string ip = "127.0.0.1";
        public int port = 7777;
        public bool sender;
        public bool listener;

        public static ConfigXML Default => new ConfigXML()
        {
            ip = "127.0.0.1",
            port = 7777,
            sender = false,
            listener = false,
        };

        public static string GetPath() => XMLerialization.CurrentDirectory() + FILE_NAME;

        public override string ToString()
        {
            string str = $"{base.ToString()} | {ip} | {port} | {sender} | {listener}";
            return str;
        }

    }
}