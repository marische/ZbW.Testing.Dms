using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
using ZbW.Testing.Dms.Client.Model;

namespace ZbW.Testing.Dms.Client.Services
{
    class FileService
    {

        public String GetContentFileName(Guid documentId, String extension)
        {
            var filename = String.Concat(documentId, "_Content", extension);
            return filename;
        }

        public String GetMetadataFileName(Guid documentId)
        {
            var mdfilename = String.Concat(documentId,"_Metadata.xml");
            return mdfilename;
        }

        public String SeralizeMetadataItem(MetadataItem metadataItem)
        {
            XmlSerializer xmlserializer = new XmlSerializer(typeof(MetadataItem));
            StringWriter stringWriter = new StringWriter();
            XmlWriter writer = XmlWriter.Create(stringWriter);

            xmlserializer.Serialize(writer, metadataItem);

            var serializeXml = stringWriter.ToString();

            writer.Close();

            return serializeXml;
        }

        public MetadataItem DeserializeMetadataItem(String path)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(MetadataItem));

            StreamReader reader = new StreamReader(path);
            var metadataItem = (MetadataItem)serializer.Deserialize(reader);
            reader.Close();

            return metadataItem;
        }

        public void openFile(MetadataItem metadataItem)
        {
            Process.Start(metadataItem.FilePath);
        }
    }
}
