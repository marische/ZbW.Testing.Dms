using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Threading.Tasks;
using System.Windows;
using System.Xml;
using System.Xml.Serialization;
using ZbW.Testing.Dms.Client.Model;

namespace ZbW.Testing.Dms.Client.Services
{
    [ExcludeFromCodeCoverage] //Serializer muss nicht getestet werden
    public class SerializeTestable
    {
        private XmlSerializer xmlserializer;
       
       
        public virtual String SerializeMetadataItem(SerializeTestable serializeTestable, MetadataItem metadataItem)
        {
            
            xmlserializer = new XmlSerializer(typeof(MetadataItem));
            StringWriter stringWriter = new StringWriter();
            XmlWriter writer = XmlWriter.Create(stringWriter);

            xmlserializer.Serialize(writer, metadataItem);

            var serializeXml = stringWriter.ToString();

            writer.Close();

            return serializeXml;
        }

        public virtual MetadataItem DeserializeMetadataItem(String path)
        {
            xmlserializer = new XmlSerializer(typeof(MetadataItem));

            StreamReader reader = new StreamReader(path);
            var metadataItem = (MetadataItem) xmlserializer.Deserialize(reader);
            reader.Close();

            return metadataItem;
        }

        public virtual void OpenFile(MetadataItem metadataItem)
        {
            Process.Start(metadataItem.FilePath);
        }

        public virtual void WriteMetaData(String targetDir, MetadataItem metadataItem, String metadataFileName)
        {
            var xmlSerializer = new XmlSerializer(typeof(MetadataItem));

            if (!Directory.Exists(targetDir))
            {
                Directory.CreateDirectory(targetDir);
            }

            var streamWriter = new StreamWriter(Path.Combine(targetDir, metadataFileName));
            xmlSerializer.Serialize(streamWriter, metadataItem);
            streamWriter.Flush();
            streamWriter.Close();
        }

        public virtual void MoveFile(MetadataItem metadataItem, String targetDir, String contentFileName, bool deleteFile)
        {

            File.Copy(metadataItem.OriginalPath, Path.Combine(targetDir, contentFileName));

            if (deleteFile)
            {
                var task = Task.Factory.StartNew(
                    () =>
                    {
                        Task.Delay(500);
                        File.Delete(metadataItem.OriginalPath);
                    });
                try
                {
                    Task.WaitAll(task);
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message);
                }
            }
        }
    }
}