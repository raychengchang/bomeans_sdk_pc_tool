using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BomeansPCTool.Properties;

namespace BomeansPCTool
{
    class AppSettings
    {
        private string mApiKey;
        private bool mUseChinaServer;

        public string ApiKey
        {
            get { return mApiKey; }
            set { mApiKey = value; IsModified = true; }
        }

        public bool UseChinaServer
        {
            get { return mUseChinaServer; }
            set { mUseChinaServer = value; IsModified = true; }
        }

        public AppSettings()
        {
            LoadSettings();
        }

        private void LoadSettings()
        {
            mApiKey = Settings.Default.API_KEY;
            mUseChinaServer = Settings.Default.USE_CHINA_SERVER;

            IsModified = false;
        }

        public bool IsModified
        {
            get; set;
        }

        public void Save()
        {
            Settings.Default.API_KEY = mApiKey;
            Settings.Default.USE_CHINA_SERVER = mUseChinaServer;
            Settings.Default.Save();

            IsModified = false;
        }
    }
}
