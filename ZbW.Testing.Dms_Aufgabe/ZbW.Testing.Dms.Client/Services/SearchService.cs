using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prism.Mvvm;
using ZbW.Testing.Dms.Client.Model;

namespace ZbW.Testing.Dms.Client.Services
{
    public class SearchService : BindableBase
    {
        public List<MetadataItem> _metadataItems;
        public List<MetadataItem> _filteredItems;
        private FileService _fileService;
        public DirectoryTestable _directoryTestable { get; set; }
        private String _targetPath;

        public SearchService()
        {
            _fileService = new FileService();
            _directoryTestable = new DirectoryTestable();
            _filteredItems = new List<MetadataItem>();
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

            if (String.IsNullOrEmpty(type))
            {
                type = "";
            }

            var filteredItems = this.MetadataItems.Where(item =>
            {
                  return (item.Description.ToLower().Contains(searchParam.ToLower()) ||
                          item.Tag.ToLower().Contains(searchParam.ToLower()) ||
                          item.AddingDate.ToString().Contains(searchParam) ||
                          item.ValutaDatum.ToString().Contains(searchParam)) &&
                       (String.IsNullOrEmpty(type) || item.Type.Equals(type));
            }).Cast<MetadataItem>().ToList();
           
            return filteredItems;
        }

        public List<MetadataItem> GetAllMetadataItems(DirectoryTestable directoryTestable)
        {
            _targetPath = directoryTestable.GetRepositoryDir();
            var folderPaths = this.GetAllFolderPaths(directoryTestable, this._targetPath);
            ArrayList xmlPathsFromAllFolders = new ArrayList();
            ArrayList metadataItemList = new ArrayList();

            foreach (string folderPath in folderPaths)
            {
                var xmlPathsFromOneFolder = this.GetAllXmlPaths(_directoryTestable, folderPath);
                xmlPathsFromAllFolders.AddRange(xmlPathsFromOneFolder);
            }

            foreach (var xmlPath in xmlPathsFromAllFolders)
            {

                metadataItemList.Add(this._fileService.DeserializeMetadataItem(_fileService.serializeTestable, (String)xmlPath));
            }

            this.MetadataItems = metadataItemList.Cast<MetadataItem>().ToList();
            return this.MetadataItems;
        }

        public String[] GetAllFolderPaths(DirectoryTestable directoryTestable, String targetPath)
        {
            var result = directoryTestable.GetAllFolderPaths(targetPath);
            return result;
        }

        public ArrayList GetAllXmlPaths(DirectoryTestable directoryTestable, String folderPath)
        {
            var result = directoryTestable.GetAllXmlPaths(folderPath);
            return result;
        }

        public String GetRepositoryDir(DirectoryTestable directoryTestable)
        {
            return directoryTestable.GetRepositoryDir();
        }
    }
}
