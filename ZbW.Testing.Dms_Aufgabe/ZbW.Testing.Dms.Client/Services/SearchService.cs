using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZbW.Testing.Dms.Client.Model;

namespace ZbW.Testing.Dms.Client.Services
{
    class SearchService
    {
        private List<MetadataItem> _metadataItems;
        private appSettingsService _appSettingsService;
        private FileService _fileService;

        private String _targetPath;

        public SearchService()
        {
            _appSettingsService = new appSettingsService();
            _targetPath = _appSettingsService.GetRepositoryDir();
            _fileService = new FileService();
        }

        public List<MetadataItem> MetadataItems
        {
            get { return _metadataItems; }
            set { _metadataItems = value; }
        }

        public List<MetadataItem> FilterMetadataItems(string type, string searchParam)
        {
            if (String.IsNullOrEmpty(searchParam) && String.IsNullOrEmpty(type))
            {
                return this.MetadataItems;
            }

            if (String.IsNullOrEmpty(searchParam))
            {
                searchParam = "";
            }

            var filteredItems = this.MetadataItems.Where(item =>
            {
                return (item.Description.Contains(searchParam) ||
                        item.Tag.Contains(searchParam) ||
                        item.AddingDate.ToString().Contains(searchParam) ||
                        item.ValutaDatum.ToString().Contains(searchParam)) &&
                       (String.IsNullOrEmpty(type) || item.Type.Equals(type));
            }).ToList();

            return filteredItems;
        }

        public List<MetadataItem> GetAllMetadataItems()
        {
            var folderPaths = this.GetAllFolderPaths(this._targetPath);
            ArrayList xmlPathsFromAllFolders = new ArrayList();
            ArrayList metadataItemList = new ArrayList();

            foreach (string folderPath in folderPaths)
            {
                var xmlPathsFromOneFolder = this.GetAllXmlPaths(folderPath);
                xmlPathsFromAllFolders.AddRange(xmlPathsFromOneFolder);
            }

            foreach (var xmlPath in xmlPathsFromAllFolders)
            {

                metadataItemList.Add(this._fileService.DeserializeMetadataItem(_fileService.serializeTestable, (String)xmlPath));
            }

            this.MetadataItems = metadataItemList.Cast<MetadataItem>().ToList();
            return this.MetadataItems;
        }

        private String[] GetAllFolderPaths(String targetPath)
        {
            return Directory.GetDirectories(targetPath);
        }

        private ArrayList GetAllXmlPaths(String folderPath)
        {
            ArrayList xmlPaths = new ArrayList();
            foreach (string file in Directory.EnumerateFiles(folderPath, "*.xml"))
            {
                xmlPaths.Add(file);
            }

            return xmlPaths;
        }
    }
}
