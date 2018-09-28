using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using ZbW.Testing.Dms.Client.Model;

namespace ZbW.Testing.Dms.Client.Services
{
    [ExcludeFromCodeCoverage] //Serializer muss nicht getestet werden
    internal class SerializeTestable
    {
        public virtual String SerializeMetadataItem(SerializeTestable serializeTestable, MetadataItem metadataItem)
        {
            XmlSerializer xmlserializer = new XmlSerializer(typeof(MetadataItem));
            StringWriter stringWriter = new StringWriter();
            XmlWriter writer = XmlWriter.Create(stringWriter);

            xmlserializer.Serialize(writer, metadataItem);

            var serializeXml = stringWriter.ToString();

            writer.Close();

            return serializeXml;
        }

        public virtual MetadataItem DeserializeMetadataItem(String path)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(MetadataItem));

            StreamReader reader = new StreamReader(path);
            var metadataItem = (MetadataItem)serializer.Deserialize(reader);
            reader.Close();

            return metadataItem;
        }

        public virtual void OpenFile(MetadataItem metadataItem)
        {
            Process.Start(metadataItem.FilePath);
        }
    }
}
