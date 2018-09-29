using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Windows;
using System.Xml;
using System.Xml.Serialization;
using ZbW.Testing.Dms.Client.Services;

namespace ZbW.Testing.Dms.Client.Model
{
    public class MetadataItem
    {
        public String Testergebnis { get; set; }
        public String OriginalPath { get; set; }
        public DateTime ValutaDatum { get; set; }

        public String ContentFileExtension { get; set; }
        public String ContentFileName { get; set; }

        public String MetadataFileName { get; set; }

        public Guid DocumentId { get; set; }

        public String RepoYear { get; set; }

        public String Tag { get; set; }

        public String Description { get; set; }

        public DateTime AddingDate { get; set; }

        public String Type {get; set; }

        public String User { get; set; }

        public bool IsRemoveFileEnabled { get; set; }

        public String FilePath { get; set; }


        private FileService _fileNameGenerator;
        public SearchService _searchService;
        public SerializeTestable _serializeTestable;
        
        public MetadataItem(SerializeTestable serializeTestable)
        {
            _fileNameGenerator = new FileService();
            _searchService = new SearchService();
            _serializeTestable = serializeTestable;

        }

        public MetadataItem()
        {
            _fileNameGenerator = new FileService();
            _searchService = new SearchService();
            _serializeTestable = new SerializeTestable();
        }

        public MetadataItem Create(String Benutzer, String Bezeichnung, String filePath, Boolean isRemoveFileEnabled, String stichwoerter, String selectedTypItem, DateTime valutadatum, DateTime addingdate)
        {
            var metadataItem = new MetadataItem(_serializeTestable);
            metadataItem.User = Benutzer;
            metadataItem.Description = Bezeichnung;
            metadataItem.OriginalPath = filePath;
            metadataItem.IsRemoveFileEnabled = isRemoveFileEnabled;
            metadataItem.Tag = stichwoerter;
            if (metadataItem.Tag == null)
            {
                metadataItem.Tag = "";
            }
            metadataItem.Type = selectedTypItem;
            metadataItem.ValutaDatum = valutadatum;
            metadataItem.AddingDate = addingdate;

            return metadataItem;
        }

        private void LoadMetadata(List<KeyValuePair<string, List<MetadataItem>>> yearItems)
        {
            foreach (var yearItem in yearItems)
            {
                var metadataFiles = Directory.GetFiles(yearItem.Key, "*_Metadata.xml");

                foreach (var metadataFile in metadataFiles)
                {
                    var xmlSerializer = new XmlSerializer(typeof(MetadataItem));
                    var streamReader = new StreamReader(metadataFile);
                    var metadataItem = (MetadataItem) xmlSerializer.Deserialize(streamReader);

                    yearItem.Value.Add(metadataItem);
                }
            }
        }

        private List<KeyValuePair<string, List<MetadataItem>>> GetYearItems()
        {
            var yearItems = new List<KeyValuePair<string, List<MetadataItem>>>();
            return yearItems;
        }

        public void AddFile(MetadataItem metadataItem, bool deleteFile)
        {
            var repositoryDir = _searchService.GetRepositoryDir(_searchService._directoryTestable);
            var year = metadataItem.ValutaDatum.Year;
            var documentId = Guid.NewGuid();
            var extension = Path.GetExtension(metadataItem.OriginalPath);
            var contentFileName = _fileNameGenerator.GetContentFileName(documentId, extension);
            var metadataFileName = _fileNameGenerator.GetMetadataFileName(documentId);

            var targetDir = Path.Combine(repositoryDir, year.ToString());
            

            metadataItem.ContentFileExtension = extension;
            metadataItem.ContentFileName = contentFileName;
            metadataItem.MetadataFileName = metadataFileName;
            metadataItem.DocumentId = documentId;
            metadataItem.RepoYear = year.ToString();
            metadataItem.FilePath = Path.Combine(targetDir, contentFileName);

            //write metadata
            _serializeTestable.WriteMetaData(targetDir, metadataItem, metadataFileName);

            //move File

            _serializeTestable.MoveFile(metadataItem, targetDir, contentFileName, deleteFile);
            Testergebnis = "Success";


        }
    }
}