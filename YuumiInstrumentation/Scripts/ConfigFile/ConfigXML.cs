using System;
using System.IO;
using System.Reflection;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

//Desk = @"D:\Defaults\Desktop\test.xml

public class ConfigXML
{
    public const  string FILE_NAME = "config.xml";

    public string ip;
    public int port;
    public bool sender;
    public bool listener;

    public string ToXML()
    {
        var serializer = new XmlSerializer(typeof(ConfigXML));
        StringBuilder sb = new StringBuilder();

        using (var writer = new StringWriter(sb))
            using (var xmlWriter = new XmlTextWriter(writer) { Formatting = Formatting.Indented })
                serializer.Serialize(xmlWriter, this);
        return sb.ToString();
    }

    public void ToXMLFile(string path, Encoding encoding)
    {
        var serializer = new XmlSerializer(typeof(ConfigXML));

        using (var xmlWriter = new XmlTextWriter(path, encoding) { Formatting = Formatting.Indented })
            serializer.Serialize(xmlWriter, this);
    }

    public static ConfigXML FromXML(string path)
    {
        var serializer = new XmlSerializer(typeof(ConfigXML));
        using (var xmlReader = new XmlTextReader(path))
            return (ConfigXML)serializer.Deserialize(xmlReader);
    }

    public static string CurrentDirectory()
    {
        //return Assembly.GetExecutingAssembly().Location;
        //return new FileInfo(Assembly.GetEntryAssembly().Location).DirectoryName;
        return AppDomain.CurrentDomain.BaseDirectory;
    }
    public static string GetPath() => CurrentDirectory() + FILE_NAME;


    public override string ToString()
    {
        return $"{base.ToString()} | {ip} | {port} | {sender} | {listener}";
    }

}
