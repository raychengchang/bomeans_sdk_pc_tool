using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using System.ComponentModel;
using System.Threading;
using BomeansPCTool.Properties;
using Bomeans.IRNet;

namespace BomeansPCTool
{
    public partial class FormMain : Form
    {
        private IRReader mMyIrReader;

        private int mCurrentDefaultKeySetIndex = 0;
        private Boolean mBmsFileNotSaved = true;
        private Dictionary<String, KeyName> mKeyNameTable = new Dictionary<String, KeyName>();
        private TypeItem[] mTypes;
        private Boolean mIsLearning = false;
        private Object mLock = new Object();

        private MyReaderFormatMatchCallback mReaderFormatMatchCallback;
        private MyReaderMatchResult mCurrentMatchResult;

        private void initializeLearningTab()
        {
            // read key name mapping table
            KeyName[] keys = SettingFiles.LoadKeyListFromFile();
            foreach (KeyName key in keys)
            {
                mKeyNameTable.Add(key.Id, key);
            }

            // init key name list view
            lvKeyList.Columns.Add(Resources.TITLE_KEY_ID, 200);
            lvKeyList.Columns.Add(Resources.TITLE_KEY_NAME, 100);
            lvKeyList.Columns.Add(Resources.TITLE_FORMAT_ID, 200);
            lvKeyList.Columns.Add(Resources.TITLE_CUSTOM_CODE, 100);
            lvKeyList.Columns.Add(Resources.TITLE_KEY_CODE, 100);

            progressBarLearning.Visible = false;   // hide initially

            // init learning result list view
            lvResultList.Columns.Add(Resources.TITLE_FORMAT_ID, 150);
            lvResultList.Columns.Add(Resources.TITLE_CUSTOM_CODE, 80);
            lvResultList.Columns.Add(Resources.TITLE_KEY_CODE, -2);

            // add default keys
            AddDefaultLearningKeys();
            if (lvKeyList.Items.Count > 0 && lvKeyList.SelectedIndices.Count == 0)
            {
                lvKeyList.Items[0].Selected = true;
            }

            mMyIrReader = Kit.createIRReader(Settings.Default.ALWAYS_PULL_DATA_FROM_CLOUD);
        }

        private void AddDefaultLearningKeys()
        {
            mBmsFileNotSaved = true;

            List<String[]> defaultKeySetList = new List<String[]>();

            defaultKeySetList.Add(new String[] {
                "IR_KEY_POWER_TOGGLE",
                "IR_KEY_CHANNEL_UP",
                "IR_KEY_CHANNEL_DOWN",
                "IR_KEY_VOLUME_UP",
                "IR_KEY_VOLUME_DOWN",
                "IR_KEY_MUTING",
                "IR_KEY_MENU",
                "IR_KEY_CURSOR_UP",
                "IR_KEY_CURSOR_DOWN",
                "IR_KEY_CURSOR_LEFT",
                "IR_KEY_CURSOR_RIGHT",
                "IR_KEY_OK",
                "IR_KEY_DIG_1",
                "IR_KEY_DIG_2",
                "IR_KEY_DIG_3",
                "IR_KEY_DIG_4",
                "IR_KEY_DIG_5",
                "IR_KEY_DIG_6",
                "IR_KEY_DIG_7",
                "IR_KEY_DIG_8",
                "IR_KEY_DIG_9",
                "IR_KEY_DIG_0",
                "IR_KEY_RED",
                "IR_KEY_GREEN",
                "IR_KEY_YELLOW",
                "IR_KEY_BLUE",
                "IR_KEY_BACK",
                "IR_KEY_RETURN",
                "IR_KEY_EXIT",
                "IR_KEY_INPUT",
                "IR_KEY_PLAY",
                "IR_KEY_PAUSE",
                "IR_KEY_STOP",
                "IR_KEY_SKIP_FORWARD",
                "IR_KEY_SKIP_REVERSE",
                "IR_KEY_NEXT",
                "IR_KEY_PREVIOUS",
                "IR_KEY_RECORD",
                "IR_KEY_INFO",
                "IR_KEY_EPG",
                "IR_KEY_HOME",
                "IR_KEY_SAP_CC",
                "IR_KEY_MAIL",
                "IR_KEY_INTERACTION"
            });

            AddDefaultLearningKeys(defaultKeySetList[mCurrentDefaultKeySetIndex]);
            mCurrentDefaultKeySetIndex++;
            mCurrentDefaultKeySetIndex %= defaultKeySetList.Count;
        }

        private void AddDefaultLearningKeys(String[] defaultKeys)
        {
            List<ListViewItem> existingKeyList = new List<ListViewItem>();
            foreach (ListViewItem item in lvKeyList.Items)
            {
                existingKeyList.Add(item);
            }

            List<ListViewItem> allKeys = new List<ListViewItem>();
            Boolean bFound;
            ListViewItem newItem;
            foreach (String newKeyId in defaultKeys)
            {
                bFound = false;
                foreach (ListViewItem existingItem in existingKeyList)
                {
                    if (newKeyId.Equals(existingItem.Text))
                    {
                        bFound = true;
                        break;
                    }
                }

                if (!bFound)
                {
                    if (mKeyNameTable.ContainsKey(newKeyId))
                    {
                        newItem = new ListViewItem();
                        newItem.Text = newKeyId;
                        newItem.SubItems.Add(mKeyNameTable[newKeyId].LocalizedName);
                        allKeys.Add(newItem);
                    }
                }
            }

            lvKeyList.Items.AddRange(allKeys.ToArray<ListViewItem>());
        }

        private void lvKeyList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lvKeyList.SelectedItems.Count > 0)
            {
                ListViewItem item = lvKeyList.SelectedItems[0];
                String keyId = item.Text;

                txtKeyId.Text = keyId;
                KeyName keyData;
                if (mKeyNameTable.TryGetValue(keyId, out keyData))
                {
                    lblKeyName.Text = keyData.LocalizedName;
                }
                else
                {
                    lblKeyName.Text = "";
                }

                ShowMatchResult(lvResultList, (MyReaderMatchResult)item.Tag);
            }
        }
        
        private void ShowMatchResult(ListView lv, MyReaderMatchResult matchResult)
        {
            lv.Items.Clear();

            // remember the current match result for later use (if needed)
            mCurrentMatchResult = matchResult;

            if (null != matchResult)
            {
                ListViewItem lvItem = new ListViewItem();

                if (matchResult.MatchResult == null)
                {
                    if (matchResult.RawLearningData != null)
                    {
                        lvItem.Text = Properties.Resources.UNRECOGNIZED_FORMAT;
                        lvItem.UseItemStyleForSubItems = false;
                        lvItem.SubItems[0].ForeColor = Color.Red;
                        lv.Items.Add(lvItem);
                        lv.Items[0].Selected = true;
                    }
                }
                else
                {
                    lvItem.Text = matchResult.MatchResult.formatId;
                    if (matchResult.MatchResult.customCode != -1)
                    {
                        lvItem.SubItems.Add(String.Format("0x{0:X}", matchResult.MatchResult.customCode));
                    }
                    else
                    {
                        lvItem.SubItems.Add("");
                    }
                    if (matchResult.MatchResult.keyCode != -1)
                    {
                        lvItem.SubItems.Add(String.Format("0x{0:X}", matchResult.MatchResult.keyCode));
                    }

                    lv.Items.Add(lvItem);
                    lv.Items[0].Selected = true;
                }
            }
        }

        private void btnPickKey_Click(object sender, EventArgs e)
        {
            List<String> existingKeys = new List<String>();
            for (int i = 0; i < lvKeyList.Items.Count; i++)
            {
                existingKeys.Add(lvKeyList.Items[i].Text);
            }

            FormKeyPicker form = new FormKeyPicker(mKeyNameTable, existingKeys);
            if (DialogResult.OK == form.ShowDialog())
            {
                List<String> selectedKeys = form.getSelectedKeys();

                Boolean bDuplicate = false;
                foreach (String newKeyId in selectedKeys)
                {
                    bDuplicate = false;
                    foreach (String existingKeyId in existingKeys)
                    {
                        if (existingKeyId.Equals(newKeyId))
                        {
                            bDuplicate = true;
                            break;
                        }
                    }

                    if (!bDuplicate)
                    {
                        existingKeys.Add(newKeyId);

                        mBmsFileNotSaved = true;
                    }
                }
                RefreshLearningKeyList(existingKeys);
            }
        }

        private void RefreshLearningKeyList(List<String> newKeyIdList)
        {/*
            // remember keys that have associated with learnt data
            Dictionary<String, MatchResult> matchResultMap = new Dictionary<string, MatchResult>();
            for (int i = 0; i < lvKeyList.Items.Count; i++)
            {
                matchResultMap.Add(lvKeyList.Items[i].Text, (MatchResult)lvKeyList.Items[i].Tag);
            }

            lvKeyList.Items.Clear();
            MatchResult matchResult;
            foreach (String keyId in newKeyIdList)
            {
                if (matchResultMap.TryGetValue(keyId, out matchResult))
                {
                    lvKeyList.Items.Add(GetKeyListViewItem(keyId, matchResult));
                }
                else
                {
                    lvKeyList.Items.Add(GetKeyListViewItem(keyId, null));
                }
            }*/
        }

        private void btnLearn_Click(object sender, EventArgs e)
        {
            mIsLearning = !mIsLearning;

            if (mIsLearning)
            {
                StartLearning();
                btnLearn.Text = Resources.STOP_LEARNING;

                btnSave.Enabled = true;
            }
            else
            {
                StopLearning();
                btnLearn.Text = Resources.START_LEARNING;

                btnSave.Enabled = false;
            }
        }

        private void StartLearning()
        {
            lblLearningResult.Text = "";
            lblLearningResult.BackColor = Color.Transparent;

            if ((null == mMyIrEasy) || (mMyIrEasy.isConnection() != ConstValue.BIROK))
            {
                Program.ShowError(Resources.E_UART_NOT_OPEN);
                return;
            }

            // flush internal buffer for a new reading session
            mMyIrEasy.Flush();

            mReaderFormatMatchCallback = new MyReaderFormatMatchCallback(mLock);
            mMyIrReader.startLearningAndGetData(PREFER_REMOTE_TYPE.TV, mReaderFormatMatchCallback);

            BackgroundWorker bgWorker = new BackgroundWorker();
            bgWorker.WorkerReportsProgress = true;
            bgWorker.DoWork += new DoWorkEventHandler(RunLearningTimer);
            bgWorker.ProgressChanged += new ProgressChangedEventHandler(LearningTimerShowProgress);
            bgWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(LearningTimerCompleted);

            progressBarLearning.Minimum = 0;
            progressBarLearning.Maximum = 99;
            progressBarLearning.Show();

            lblLearningResult.Text = Resources.WAITING_IR_SIGNAL;
            lblLearningResult.ForeColor = Color.White;
            lblLearningResult.BackColor = Color.Gray;

            bgWorker.RunWorkerAsync();
        }

        private Boolean StopLearning()
        {
            if ((null == mMyIrEasy) || (mMyIrEasy.isConnection() != ConstValue.BIROK))
            {
                Program.ShowError(Resources.E_UART_NOT_OPEN);
                return false;
            }

            mMyIrReader.stopLearning();

            return true;
        }

        private void RunLearningTimer(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = (BackgroundWorker)sender;

            int counter = 0;
            while ((counter < 700) && mIsLearning)
            {
                if (mReaderFormatMatchCallback.IsCompleted)
                {
                    break;
                }

                worker.ReportProgress(counter / 6);
                counter++;
                Thread.Sleep(11);
            }
        }

        private void LearningTimerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            progressBarLearning.Hide();

            ShowLearningResult(mReaderFormatMatchCallback.MatchResult);

            if (mIsLearning)
            {
                StartLearning();
            }
            else
            {
                //mCurrentMatchResult = null;
                lvResultList.Items.Clear();

                lblLearningResult.Text = "";    //"IR Signal parsing error!";
                lblLearningResult.BackColor = Color.Transparent;    //Color.Red;
                lblLearningResult.ForeColor = Color.White;
            }

            ((BackgroundWorker)sender).Dispose();
        }

        private void LearningTimerShowProgress(object sender, ProgressChangedEventArgs e)
        {
            if (e.ProgressPercentage >= progressBarLearning.Maximum)
            {
                progressBarLearning.Value = progressBarLearning.Maximum;
            }
            else if (e.ProgressPercentage <= progressBarLearning.Minimum)
            {
                progressBarLearning.Value = progressBarLearning.Minimum;
            }
            else
            {
                progressBarLearning.Value = e.ProgressPercentage;
            }
        }

        private void ShowLearningResult(MyReaderMatchResult matchResult)
        {
            lvResultList.Items.Clear();

            // remember the current match result for later use (if needed)
            mCurrentMatchResult = matchResult;

            if (null == matchResult)
            {
                return;
            }

            ListViewItem lvItem = new ListViewItem();

            if (matchResult.MatchResult == null)
            {
                if (matchResult.RawLearningData != null)
                {
                    lvItem.Text = Properties.Resources.UNRECOGNIZED_FORMAT;
                    lvItem.UseItemStyleForSubItems = false;
                    lvItem.SubItems[0].ForeColor = Color.Red;
                    lvResultList.Items.Add(lvItem);
                    lvResultList.Items[0].Selected = true;
                }
            }
            else
            {
                lvItem.Text = matchResult.MatchResult.formatId;

                if (matchResult.MatchResult.customCode >= 0)
                {
                    lvItem.SubItems.Add(String.Format("0x{0:X}", matchResult.MatchResult.customCode));
                }
                else
                {
                    lvItem.SubItems.Add("");
                }

                if (matchResult.MatchResult.keyCode >= 0)
                {
                    lvItem.SubItems.Add(String.Format("0x{0:X}", matchResult.MatchResult.keyCode));
                }
                else
                {
                    lvItem.SubItems.Add("");
                }

                lvResultList.Items.Add(lvItem);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            // make sure we have decoded data to add
            if (lvResultList.Items.Count == 0 || null == mCurrentMatchResult)
            {
                Program.ShowError(Resources.E_NO_LEARNING_DATA);
                return;
            }
            else
            {
                String keyId = txtKeyId.Text.Trim();
                ListViewItem selectedItem = lvKeyList.SelectedItems[0];
                if (selectedItem.Text.Equals(keyId))
                {
                    // save the match result to ListViewItem's Tag
                    selectedItem.Tag = mCurrentMatchResult;

                    // update the list view item content
                    UpdateKeyListViewItem(selectedItem, keyId, mCurrentMatchResult);

                    // move to next item
                    if (lvKeyList.SelectedIndices[0] < lvKeyList.Items.Count - 1)
                    {
                        lvKeyList.Items[lvKeyList.SelectedIndices[0] + 1].Selected = true;
                        lvKeyList.EnsureVisible(lvKeyList.SelectedIndices[0]);
                    }

                    // check all learning result for possible learning error 
                    CheckLearningKeyList();

                    // clear the current result
                    mCurrentMatchResult = null;
                    lvResultList.Items.Clear();
                }
            }

            mBmsFileNotSaved = true;
        }

        private void UpdateKeyListViewItem(ListViewItem item, String keyId, MyReaderMatchResult matchResult)
        {
            ListViewItem tmpItem = GetKeyListViewItem(keyId, matchResult);

            item.SubItems.Clear();
            item.Tag = matchResult;
            for (int i = 0; i < tmpItem.SubItems.Count; i++)
            {
                if (i == 0)
                {
                    item.SubItems[i] = tmpItem.SubItems[i];
                }
                else
                {
                    item.SubItems.Add(tmpItem.SubItems[i]);
                }
            }
        }

        private ListViewItem GetKeyListViewItem(String keyId, MyReaderMatchResult matchResult)
        {
            ListViewItem lvItem = new ListViewItem();
            lvItem.UseItemStyleForSubItems = false;

            // Key ID
            lvItem.Text = keyId;

            // Key Name
            KeyName keyData;
            if (mKeyNameTable.TryGetValue(keyId, out keyData))
            {
                lvItem.SubItems.Add(keyData.LocalizedName);
                lvItem.SubItems[0].ForeColor = Color.Black;
            }
            else
            {
                lvItem.SubItems.Add("");
                lvItem.SubItems[0].ForeColor = Color.Red;
            }

            lvItem.Tag = matchResult;

            // IR format
            if (matchResult != null)
            {
                if (null != matchResult.MatchResult)
                {
                    lvItem.SubItems.Add(matchResult.MatchResult.formatId);
                    lvItem.SubItems.Add(
                        matchResult.MatchResult.customCode == -1 ? "" :
                        String.Format("0x{0:X}", matchResult.MatchResult.customCode));
                    lvItem.SubItems.Add(
                        matchResult.MatchResult.keyCode == -1 ? "" :
                        String.Format("0x{0:X}", matchResult.MatchResult.keyCode));
                }
                else
                {
                    if (null != matchResult.RawLearningData)
                    {
                        lvItem.SubItems.Add(Resources.UNRECOGNIZED_FORMAT);
                    }
                }
            }

            return lvItem;
        }

        /// <summary>
        /// check the formats of the learning keys, mark those formats which might potentially be error with colors
        /// </summary>
        private void CheckLearningKeyList()
        {
            Dictionary<String, int> formatCounter = CalculateFormatCountForLearningKeys();

            // get the format id of most used
            String mainFormatId = "";
            int maxCount = 0;
            foreach (KeyValuePair<String, int> pair in formatCounter)
            {
                if (pair.Value > maxCount)
                {
                    mainFormatId = pair.Key;
                    maxCount = pair.Value;
                }
            }

            List<ListViewItem> itemList = new List<ListViewItem>();
            foreach (ListViewItem lvItem in lvKeyList.Items)
            {
                lvItem.UseItemStyleForSubItems = false;

                if (lvItem.Tag == null)
                {
                    continue;
                }

                if (lvItem.SubItems.Count < 3)
                {
                    continue;
                }

                if (lvItem.SubItems[2].Text.Equals(mainFormatId))
                {
                    lvItem.SubItems[2].ForeColor = Color.Black;
                    itemList.Add(lvItem);
                }
                else
                {
                    lvItem.SubItems[2].ForeColor = Color.Red;
                }
            }

            Dictionary<String, int> customCodeCounter = CalculateCustomCodeCount(itemList);
            String mainCustomCode = "";
            maxCount = 0;
            foreach (KeyValuePair<String, int> pair in customCodeCounter)
            {
                if (pair.Value > maxCount)
                {
                    mainCustomCode = pair.Key;
                    maxCount = pair.Value;
                }
            }
            foreach (ListViewItem lvItem in itemList)
            {
                if (lvItem.SubItems.Count >= 4)
                {
                    if (lvItem.SubItems[3].Text.Equals(mainCustomCode))
                    {
                        lvItem.SubItems[3].ForeColor = Color.Black;
                    }
                    else
                    {
                        lvItem.SubItems[3].ForeColor = Color.Red;
                    }
                }
            }
        }

        private Dictionary<String, int> CalculateCustomCodeCount(List<ListViewItem> itemList)
        {
            Dictionary<String, int> customCodeCounter = new Dictionary<String, int>();

            String tmpCustomCode;
            Boolean bFound;
            foreach (ListViewItem lvItem in itemList)
            {
                bFound = false;

                if (lvItem.SubItems.Count >= 4)
                {
                    tmpCustomCode = lvItem.SubItems[3].Text;
                    foreach (KeyValuePair<String, int> pair in customCodeCounter)
                    {
                        if (pair.Key.Equals(tmpCustomCode))
                        {
                            customCodeCounter[tmpCustomCode] = customCodeCounter[tmpCustomCode] + 1;
                            bFound = true;
                            break;
                        }
                    }

                    if (!bFound)
                    {
                        customCodeCounter.Add(tmpCustomCode, 1);
                    }
                }
            }

            return customCodeCounter;
        }

        private Dictionary<String, int> CalculateFormatCountForLearningKeys()
        {
            Dictionary<String, int> formatCounter = new Dictionary<String, int>();

            MyReaderMatchResult matchResult;
            String tmpFormatId;
            Boolean bFound;
            foreach (ListViewItem lvItem in lvKeyList.Items)
            {
                matchResult = (MyReaderMatchResult)lvItem.Tag;
                if (null == matchResult)
                {
                    continue;
                }

                if (lvItem.SubItems.Count < 3)
                {
                    continue;
                }

                tmpFormatId = lvItem.SubItems[2].Text;
                if (tmpFormatId.Length == 0)
                {
                    continue;
                }
                bFound = false;
                foreach (KeyValuePair<String, int> pair in formatCounter)
                {
                    if (pair.Key.Equals(tmpFormatId))
                    {
                        formatCounter[tmpFormatId] = formatCounter[tmpFormatId] + 1;
                        bFound = true;
                        break;
                    }
                }
                if (!bFound)
                {
                    formatCounter.Add(tmpFormatId, 1);
                }
            }

            return formatCounter;
        }

        class MyReaderFormatMatchCallback : ReaderFormatMatchCallback
        {
            private Object mLock;
            private Boolean mCallbackCompleted;
            private MyReaderMatchResult mResult;

            public MyReaderFormatMatchCallback(Object lockObj)
            {
                mLock = lockObj;
                mResult = new MyReaderMatchResult();

                lock (mLock)
                {
                    mCallbackCompleted = false;
                }
            }

            /// <summary>
            /// get the match result of learning data, or null if the learning process is not yet completed
            /// </summary>
            public MyReaderMatchResult MatchResult
            {
                get
                {
                    lock (mLock)
                    {
                        if (mCallbackCompleted)
                        {
                            return mResult;
                        }
                        else
                        {
                            return null;
                        }
                    }
                }
            }

            public Boolean IsCompleted
            {
                get
                {
                    lock (mLock)
                    {
                        return mCallbackCompleted;
                    }
                }
            }

            public void onFormatMatchFailed(FormatParsingErrorCode errorCode)
            {
                mResult.MatchResult = null;

                lock (mLock)
                {
                    mCallbackCompleted = true;
                }
            }

            public void onFormatMatchSucceeded(ReaderMatchResult formatMatchResult)
            {
                // note: we only handle TV-like remote controllers, so ignore the AC IR format result.
                mResult.MatchResult = 
                    formatMatchResult.isAc() ? null : formatMatchResult;

                lock (mLock)
                {
                    mCallbackCompleted = true;
                }
            }

            public void onLearningDataFailed(LearningErrorCode errorCode)
            {
                mResult.RawLearningData = null;
            }

            public void onLearningDataReceived(byte[] learningData)
            {
                mResult.RawLearningData = learningData;
            }
        }
    }

    
}
