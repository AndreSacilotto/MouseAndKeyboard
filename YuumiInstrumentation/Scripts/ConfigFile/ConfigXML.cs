using System;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

public class ConfigXML
{
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

}
