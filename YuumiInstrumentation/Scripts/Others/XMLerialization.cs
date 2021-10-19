using System;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace Yuumi.Others
{

    //Desk = @"D:\Defaults\Desktop\test.xml

    public static class XMLerialization
    {
        public static string ToXMLString<T>(object objSerialize)
        {
            var serializer = new XmlSerializer(typeof(T));
            StringBuilder sb = new StringBuilder();

            using (var writer = new StringWriter(sb))
            using (var xmlWriter = new XmlTextWriter(writer) { Formatting = Formatting.Indented })
                serializer.Serialize(xmlWriter, objSerialize);
            return sb.ToString();
        }

        public static void ToXMLFile<T>(string path, Encoding encoding, object objSerialize)
        {
            var serializer = new XmlSerializer(typeof(T));
            using (var xmlWriter = new XmlTextWriter(path, encoding) { Formatting = Formatting.Indented })
                serializer.Serialize(xmlWriter, objSerialize);
        }

        public static T FromXMLString<T>(string text) where T : class
        {
            var serializer = new XmlSerializer(typeof(T));

            using (var reader = new StringReader(text))
            using (var xmlReader = new XmlTextReader(reader))
                return (T)serializer.Deserialize(xmlReader);
        }

        public static T FromXMLFile<T>(string path) where T : class
        {
            if (!File.Exists(path))
                return null;

            var serializer = new XmlSerializer(typeof(T));
            using (var xmlReader = new XmlTextReader(path))
                return (T)serializer.Deserialize(xmlReader);
        }

        public static string CurrentDirectory()
        {
            //return Assembly.GetExecutingAssembly().Location;
            //return new FileInfo(Assembly.GetEntryAssembly().Location).DirectoryName;
            return AppDomain.CurrentDomain.BaseDirectory;
        }
    }
}