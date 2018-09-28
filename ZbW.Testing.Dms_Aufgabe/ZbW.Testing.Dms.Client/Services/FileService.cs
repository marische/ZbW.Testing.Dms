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
        internal SerializeTestable serializeTestable { get; set; }
        public FileService()
        {
            serializeTestable = new SerializeTestable();
        }

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

        public String SerializeMetadataItem(SerializeTestable serializeTestable, MetadataItem metadataItem)
        {
            var result = serializeTestable.SerializeMetadataItem(serializeTestable,metadataItem);
            return result;
        }

        public MetadataItem DeserializeMetadataItem(SerializeTestable serializeTestable, String path)
        { 
            var result = serializeTestable.DeserializeMetadataItem(path);
            return result;
        }

        public void OpenFile(SerializeTestable serializeTestable, MetadataItem metadataItem)
        {
            serializeTestable.OpenFile(metadataItem);
        }
    }
}
