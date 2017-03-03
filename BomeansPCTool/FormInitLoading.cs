using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Globalization;
using Bomeans.IRNet;
using BomeansPCTool.Properties;

namespace BomeansPCTool
{
    public partial class FormInitLoading : Form
    {
        private Boolean mGetNew = false;

        private BackgroundWorker mWorker;

        public FormInitLoading(Boolean getNew = false)
        {
            InitializeComponent();

            mGetNew = getNew;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FormInitLoading_Load(object sender, EventArgs e)
        {
            mWorker = new BackgroundWorker();
            mWorker.WorkerReportsProgress = true;
            mWorker.WorkerSupportsCancellation = true;
            mWorker.DoWork += new DoWorkEventHandler(DoInitDownload);
            mWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(DoInitDownloadCompleted);
            mWorker.ProgressChanged += new ProgressChangedEventHandler(InitDownloadProgressChanged);

            mWorker.RunWorkerAsync(CultureInfo.CurrentCulture.Name);
        }

        private void DoInitDownload(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = (BackgroundWorker)sender;

            String languageCode = (String)e.Argument;

            int currentProgress = 0;
            worker.ReportProgress(currentProgress, Resources.I_DOWNLOADING_TYPES);
            
            if (worker.CancellationPending)
            {
                e.Cancel = true;
                return;
            }

            // get types
            TypeItem[] types = Web.getTypeList(languageCode, mGetNew, null);
            SettingFiles.SaveTypeListToFile(types);

            // get brands
            int progressPartForBrand = 40 / types.Length;
            int progressPartForKey = 40 / types.Length;
            int progressPartForIrReader = 20;
            
            BrandItem[] brands;
            Dictionary<String, KeyName> keyMap = new Dictionary<String, KeyName>();
            for (int i = 0; i < types.Length; i++)
            {
                if (worker.CancellationPending)
                {
                    e.Cancel = true;
                    return;
                }

                currentProgress += progressPartForBrand;
                worker.ReportProgress(currentProgress,
                    String.Format(Resources.I_DOWNLOADING_BRANDS + " {0}/{1}", i + 1, types.Length));

                brands = Web.getBrandList(types[i].Id, 0, 2000, languageCode, null, mGetNew, null);
                if (null != brands)
                {
                    SettingFiles.SaveBrandListToFile(types[i].Id, brands);
                }
                else
                {
                    e.Result = false;
                    return;
                }

                if (worker.CancellationPending)
                {
                    e.Cancel = true;
                    return;
                }

                currentProgress += progressPartForKey;
                worker.ReportProgress(currentProgress, 
                    String.Format(Resources.I_DOWNLOADING_KEYS + "  {0}/{1}", i + 1, types.Length));

                KeyName[] keys = Web.getKeyName(types[i].Id, languageCode, mGetNew, null);
                if (null != keys)
                {
                    foreach (KeyName key in keys)
                    {
                        if (!keyMap.ContainsKey(key.Id))
                        {
                            keyMap.Add(key.Id, key);
                        }
                    }
                }
                else
                {
                    e.Result = false;
                    return;
                }
            }

            if (worker.CancellationPending)
            {
                e.Cancel = true;
                return;
            }

            currentProgress += progressPartForIrReader / 2;
            worker.ReportProgress(currentProgress, Resources.I_DOWNLOADING_FORMATS);
            // IRReader
            IRReader irReader = Kit.createIRReader(mGetNew);
            if (null == irReader)
            {
                e.Result = false;
                return;
            }
            worker.ReportProgress(100, Resources.COMPLETED);

            List<KeyName> keyList = new List<KeyName>();
            foreach(KeyValuePair<String, KeyName> entry in keyMap)
            {
                keyList.Add(entry.Value);
            }

            if (keyList.Count > 0)
            {
                SettingFiles.SaveKeyListToFile(keyList.ToArray());
            }
            else
            {
                e.Result = false;
                return;
            }

            e.Result = true;
        }

        private void DoInitDownloadCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Cancelled)
            {
                Program.ShowMessage(Resources.E_USER_CANCELLED);
            }
            else
            { 
                Boolean result = (Boolean)e.Result;
                if (!result)
                {
                    Program.ShowMessage(Resources.E_DOWNLOAD_FAILED);
                }
            }

            ((BackgroundWorker)sender).Dispose();

            Close();
        }

        private void InitDownloadProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            progressBar.Value = e.ProgressPercentage;
            lblMessage.Text = e.UserState == null ? "" : (String)e.UserState;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            if (mWorker.WorkerSupportsCancellation)
            {
                if (DialogResult.Yes == Program.ShowQuestion(Resources.Q_CONFIRM_CANCELLATION))
                {
                    mWorker.CancelAsync();
                }
            }
        }
    }
}
