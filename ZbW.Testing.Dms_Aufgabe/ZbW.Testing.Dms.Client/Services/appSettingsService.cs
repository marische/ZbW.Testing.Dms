using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZbW.Testing.Dms.Client.Services
{
    class appSettingsService
    {
        private string repositoryDir;

        public string GetRepositoryDir()
        {
            repositoryDir = ConfigurationManager.AppSettings.Get("RepositoryDir").ToString();
            return repositoryDir;
        }
        
    }
}
