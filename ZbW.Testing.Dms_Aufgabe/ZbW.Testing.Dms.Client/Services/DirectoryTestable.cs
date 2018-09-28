using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZbW.Testing.Dms.Client.Services
{
    [ExcludeFromCodeCoverage] // Directory muss nicht getestet werden.
    internal class DirectoryTestable
    {
        public virtual String[] GetAllFolderPaths(String targetPath)
        {
            return Directory.GetDirectories(targetPath);
        }

        public virtual ArrayList GetAllXmlPaths(String folderPath)
        {
            ArrayList xmlPaths = new ArrayList();
            foreach (string file in Directory.EnumerateFiles(folderPath, "*.xml"))
            {
                xmlPaths.Add(file);
            }

            return xmlPaths;
        }

        public virtual String GetRepositoryDir()
        {
            var repositoryDir = ConfigurationManager.AppSettings.Get("RepositoryDir").ToString();
            return repositoryDir;
        }
    }
}
