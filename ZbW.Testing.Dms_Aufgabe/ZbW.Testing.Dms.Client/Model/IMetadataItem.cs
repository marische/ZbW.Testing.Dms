using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZbW.Testing.Dms.Client.Services;

namespace ZbW.Testing.Dms.Client.Model
{
    interface IMetadataItem
    {
        String RepoYear { get; set; }
        void AddFile(MetadataItem metadataItem, bool deleteFile);

        List<KeyValuePair<string, List<MetadataItem>>> GetYearItems();

        void LoadMetadata(List<KeyValuePair<string, List<MetadataItem>>> yearItems);

        MetadataItem Create(String Benutzer, String Bezeichnung, String filePath, Boolean isRemoveFileEnabled,
            String stichwoerter, String selectedTypItem, DateTime valutadatum, DateTime addingdate);
    }
}
