using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Bomeans;
using Bomeans.IRNet;
using Dolinay;
using BomeansPCTool.Properties;

namespace BomeansPCTool
{
    public partial class FormMain : Form
    {
        // http://www.codeproject.com/Articles/18062/Detecting-USB-Drive-Removal-in-a-C-Program
        private DriveDetector mDriveDetector = null;

        private IrEasy mMyIrEasy = new IrEasy();

        public FormMain()
        {
            InitializeComponent();
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            InitBomeansAPI();

            mDriveDetector = new DriveDetector();
            mDriveDetector.DeviceArrived += new DriveDetectorEventHandler(OnDriveArrived);
            mDriveDetector.DeviceRemoved += new DriveDetectorEventHandler(OnDriveRemoved);

            initializeLearningTab();

            // form not show in foreground?
            this.Visible = false;
            this.WindowState = FormWindowState.Minimized;
            this.Show();
            this.WindowState = FormWindowState.Normal;
            this.Visible = true;
        }

        private void FormMain_Shown(object sender, EventArgs e)
        {
            Boolean bHasApiKey = false;

            AppSettings appSettings = new AppSettings();
            if (appSettings.ApiKey.Length == 0)
            {
                if (DialogResult.Yes == Program.ShowQuestion(Resources.Q_SETUP_API))
                {
                    FormAPIKey dlg = new FormAPIKey(appSettings.ApiKey, appSettings.UseChinaServer);
                    if (DialogResult.OK == dlg.ShowDialog())
                    {
                        if (dlg.APIKey.Length > 0)
                        {
                            bHasApiKey = true;
                        }
                    }
                }
            }
            else
            {
                bHasApiKey = true;
            }
            
            // do initial download
            if (bHasApiKey)
            {
                FormInitLoading form = new FormInitLoading();
                form.ShowDialog();
            }

            if (null != mMyIrEasy)
            {
                if (Settings.Default.UART_COM_PORT.Length > 0)
                {
                    mMyIrEasy.Initialize(Settings.Default.UART_COM_PORT);
                }
            }

            UpdateTitleText();
        }

        private void UpdateTitleText()
        {
            if (null == mMyIrEasy)
            {
                this.Text = Program.SoftwareName;
            }
            else
            {
                this.Text = String.Format("{0} {1}", Program.SoftwareName,
                    mMyIrEasy.isConnection() == ConstValue.BIROK ? "(Connected)" : "(Disconnected!)");
            }
        }

        private void InitBomeansAPI()
        {
            AppSettings settings = new AppSettings();

            Kit.setup(settings.ApiKey, settings.UseChinaServer, mMyIrEasy);
        }

        // Called by DriveDetector when removable device in inserted
        private void OnDriveArrived(object sender, DriveDetectorEventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;

            if (null != mMyIrEasy)
            {
                if (Settings.Default.UART_COM_PORT.Length > 0)
                {
                    mMyIrEasy.Initialize(Settings.Default.UART_COM_PORT);
                }
            }

            UpdateTitleText();

            Cursor.Current = Cursors.Default;
        }

        // Called by DriveDetector after removable device has been unplugged
        private void OnDriveRemoved(object sender, DriveDetectorEventArgs e)
        {
            //CheckDeviceFirmwareVersion();
            UpdateTitleText();
        }

        private void irEasySettingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormRS232 form = new FormRS232(mMyIrEasy);
            form.ShowDialog();
            UpdateTitleText();
        }

        private void loadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveBMSFile();
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog dlg = new SaveFileDialog();
            dlg.Filter = Resources.FILTER_BMS;
            if (DialogResult.OK == dlg.ShowDialog())
            {
                String fileName = dlg.FileName;

                if (!SaveBMSFile(fileName))
                {
                    mBmsFileNotSaved = true;
                    Program.ShowError(String.Format(Resources.E_FAILED_TO_SAVE_TO, fileName));
                }
                else
                {
                    mBmsFileNotSaved = false;
                }
            }
        }

        private void SaveBMSFile()
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = Resources.FILTER_BMS;
            if (DialogResult.OK == dlg.ShowDialog())
            {
                String fileName = dlg.FileName;

                Dictionary<String, MyReaderMatchResult> resultList = LoadBMSFile(fileName);
                if (null == resultList)
                {
                    Program.ShowError(String.Format(Resources.E_FAILED_TO_LOAD_FROM, fileName));
                }

                // refresh key list view
                lvKeyList.Items.Clear();
                foreach (KeyValuePair<String, MyReaderMatchResult> keyEntry in resultList)
                {
                    lvKeyList.Items.Add(GetKeyListViewItem(keyEntry.Key, keyEntry.Value));
                }

                mBmsFileNotSaved = false;
            }
        }

        private Dictionary<String, MyReaderMatchResult> LoadBMSFile(String filePath)
        {
            BMSFile bmsFile = new BMSFile();
            if (!bmsFile.Load(filePath))
            {
                return null;
            }

            return bmsFile.GetLearningResult();
        }

        private Boolean SaveBMSFile(String fileName)
        {
            Dictionary<String, MyReaderMatchResult> learningKeysData = new Dictionary<String, MyReaderMatchResult>();

            foreach (ListViewItem item in lvKeyList.Items)
            {
                learningKeysData.Add(item.Text, (MyReaderMatchResult)item.Tag);
            }

            if (learningKeysData.Count == 0)
            {
                Program.ShowError(Resources.E_NO_LEARNING_DATA_TO_SAVE);
                return false;
            }

            BMSFile bmsFile = new BMSFile(learningKeysData);
            return bmsFile.Save(fileName);
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddDefaultLearningKeys();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnMoveUp_Click(object sender, EventArgs e)
        {
            keyMoveUp();
        }

        private void btnMoveDown_Click(object sender, EventArgs e)
        {
            keyMoveDown();
        }

        private void keyMoveUp()
        {
            if (lvKeyList.SelectedItems.Count > 0)
            {
                mBmsFileNotSaved = true;

                int selIdx = lvKeyList.SelectedIndices[0];
                if (selIdx == 0)
                {
                    return;
                }

                ListViewItem lvItemToMoveUp = lvKeyList.Items[selIdx];
                ListViewItem lvItemToMoveDown = lvKeyList.Items[selIdx - 1];

                ListViewItem temp = GetKeyListViewItem(lvItemToMoveDown.Text, (MyReaderMatchResult)lvItemToMoveDown.Tag);

                UpdateKeyListViewItem(lvItemToMoveDown, lvItemToMoveUp.Text, (MyReaderMatchResult)lvItemToMoveUp.Tag);
                UpdateKeyListViewItem(lvItemToMoveUp, temp.Text, (MyReaderMatchResult)temp.Tag);

                lvKeyList.Items[selIdx - 1].Selected = true;
            }
        }

        private void keyMoveDown()
        {
            if (lvKeyList.SelectedItems.Count > 0)
            {
                mBmsFileNotSaved = true;

                int selIdx = lvKeyList.SelectedIndices[0];
                if (selIdx == lvKeyList.Items.Count - 1)
                {
                    return;
                }

                ListViewItem lvItemToMoveUp = lvKeyList.Items[selIdx + 1];
                ListViewItem lvItemToMoveDown = lvKeyList.Items[selIdx];

                ListViewItem temp = GetKeyListViewItem(lvItemToMoveDown.Text, (MyReaderMatchResult)lvItemToMoveDown.Tag);

                UpdateKeyListViewItem(lvItemToMoveDown, lvItemToMoveUp.Text, (MyReaderMatchResult)lvItemToMoveUp.Tag);
                UpdateKeyListViewItem(lvItemToMoveUp, temp.Text, (MyReaderMatchResult)temp.Tag);

                lvKeyList.Items[selIdx + 1].Selected = true;
            }
        }

        private void btnClearAll_Click(object sender, EventArgs e)
        {
            ClearAllLearningData();
        }

        private void btnReTransmit_Click(object sender, EventArgs e)
        {
            ReTransmitIRData();
        }

        private void btnDeleteOne_Click(object sender, EventArgs e)
        {
            DeleteCurrentKey();
        }

        private void DeleteCurrentKey()
        {
            if (lvKeyList.SelectedItems.Count > 0)
            {
                mBmsFileNotSaved = true;

                int selIdx = lvKeyList.SelectedIndices[0];
                ListViewItem lvItem = lvKeyList.SelectedItems[0];
                if (DialogResult.Yes == Program.ShowQuestion(String.Format("Remove key: {0}({1})?", lvItem.SubItems[1].Text, lvItem.Text)))
                {
                    lvKeyList.Items.Remove(lvKeyList.SelectedItems[0]);

                    if (lvKeyList.Items.Count <= selIdx)
                    {
                        lvKeyList.Items[lvKeyList.Items.Count - 1].Selected = true;
                    }
                    else
                    {
                        lvKeyList.Items[selIdx].Selected = true;
                    }
                }
            }
        }

        private void ClearAllLearningData()
        {
            if (DialogResult.Yes == Program.ShowQuestion(Properties.Resources.Q_DELETE_ALL_LEARNING_DATA))
            {
                mBmsFileNotSaved = true;

                foreach (ListViewItem lvItem in lvKeyList.Items)
                {
                    UpdateKeyListViewItem(lvItem, lvItem.Text, null);
                }
            }
        }

        private void ReTransmitIRData()
        {
            if (lvKeyList.SelectedItems.Count > 0)
            {
                if (mIsLearning)
                {
                    if (DialogResult.Yes != Program.ShowQuestion(Resources.Q_STOP_LEARNING_NOW))
                    {
                        return;
                    }

                    StopLearning();
                }

                if (lvKeyList.SelectedItems.Count == 0)
                {
                    Program.ShowError(Resources.E_NO_SELECTED_KEY);
                    return;
                }

                int selIdx = lvKeyList.SelectedIndices[0];
                ListViewItem lvItem = lvKeyList.SelectedItems[0];

                MyReaderMatchResult matchResult = (MyReaderMatchResult)lvItem.Tag;
                if (null != matchResult && null != matchResult.RawLearningData)
                {
                    if (mMyIrReader != null)
                    {
                        mMyIrReader.sendLearningData(matchResult.RawLearningData);
                    }
                }
                else
                {
                    Program.ShowError(Resources.E_NO_LEARNING_DATA);
                    return;
                }        
            }
        }

        private void aPIKeyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AppSettings appSettings = new AppSettings();
            FormAPIKey dlg = new FormAPIKey(appSettings.ApiKey, appSettings.UseChinaServer);
            if (DialogResult.OK == dlg.ShowDialog())
            {
                appSettings.ApiKey = dlg.APIKey;
                appSettings.UseChinaServer = dlg.UseChinaServer;
                appSettings.Save();

                // need to setup the api again since we'd changed the settings
                InitBomeansAPI();
            }
        }

        private void downloadIRDataToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormInitLoading dlg = new FormInitLoading(true);
            dlg.ShowDialog();
        }
    }
}
