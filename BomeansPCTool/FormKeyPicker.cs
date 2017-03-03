using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using Bomeans.IRNet;
using BomeansPCTool.Properties;

namespace BomeansPCTool
{
    public partial class FormKeyPicker : Form
    {
        private Dictionary<String, KeyName> mKeyTable;
        private List<String> mExistingKeys; // IDs
        private List<String> mSelectedKeys = new List<String>(); // IDs
        private String fAppPath = Path.GetDirectoryName(Application.ExecutablePath);
        private TypeItem[] mTypeList;

        private const String GENERAL_TYPE_ID = "999";
        private Color CHECKED_FORE_COLOR = Color.White;
        private Color CHECKED_BACK_COLOR = Color.Gray;

        public FormKeyPicker(Dictionary<String, KeyName> keyTable, List<String> existingKeys)
        {
            InitializeComponent();

            mKeyTable = keyTable;

            mExistingKeys = existingKeys;
            foreach (String key in existingKeys)
            {
                mSelectedKeys.Add(key);
            }
        }

        private void FormKeyPicker_Load(object sender, EventArgs e)
        {
            // load category list file
            mTypeList = SettingFiles.LoadTypeListFromFile();

            lvKeyList.Columns.Add("Key ID", 200);
            lvKeyList.Columns.Add("Key Name", -2);

            // init combobox
            TypeItem[] types = SettingFiles.LoadTypeListFromFile();
            cboCategory.Items.Add(new TypeItem(GENERAL_TYPE_ID, Resources.ALL_CATEGORY, "A"));
            foreach (TypeItem type in types)
            {
                cboCategory.Items.Add(type);
            }

            String selectedCategoryId = Settings.Default.M_SELECTED_CATEGORY_ID;
            TypeItem tmpCategory;
            Boolean bFound = false;
            for (int i = 0; i < cboCategory.Items.Count; i++)
            {
                tmpCategory = (TypeItem)cboCategory.Items[i];
                if (tmpCategory.Id.Equals(selectedCategoryId))
                {
                    cboCategory.SelectedIndex = i;
                    bFound = true;
                    break;
                }
            }
            if (!bFound)
            {
                cboCategory.SelectedIndex = 0;
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            RefreshCurrentSelectedKeys();
            /*
            foreach (ListViewItem item in lvKeyList.Items)
            {
                if (item.Checked)
                {
                    mSelectedKeys.Add(item.Text);
                }
            }*/
        }

        private void RefreshCurrentSelectedKeys()
        {
            List<String> tmpList = new List<String>();
            Boolean bDuplicate = false;
            foreach (ListViewItem item in lvKeyList.Items)
            {
                if (item.Checked)
                {
                    bDuplicate = false;
                    foreach (String tmpKey in mSelectedKeys)
                    {
                        if (tmpKey.Equals(item.Text))
                        {
                            bDuplicate = true;
                            break;
                        }
                    }

                    if (!bDuplicate)
                    {
                        mSelectedKeys.Add(item.Text);
                    }
                }
            }
        }

        public List<String> getSelectedKeys()
        {
            return mSelectedKeys;
        }

        private void cboCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            RefreshKeyList();
        }

        private void RefreshKeyList()
        {
            // remember the current checked items
            RefreshCurrentSelectedKeys();

            lvKeyList.Items.Clear();

            TypeItem selectedCategory = (TypeItem)cboCategory.SelectedItem;
            if (null != selectedCategory)
            {
                String typeId = selectedCategory.Id;
                ListViewItem item;
                String filterText;
                foreach (KeyValuePair<String, KeyName> pair in mKeyTable)
                {
                    if (!typeId.Equals(GENERAL_TYPE_ID) && !pair.Value.TypeId.Equals(GENERAL_TYPE_ID))
                    {
                        if (!pair.Value.TypeId.Equals(typeId))
                        {
                            continue;
                        }
                    }

                    // filter
                    filterText = txtFilter.Text.Trim().ToUpper();
                    if (!pair.Key.ToUpper().Contains(filterText) &&
                        !pair.Value.LocalizedName.ToUpper().Contains(filterText))
                    {
                        continue;
                    }

                    item = new ListViewItem();
                    item.Text = pair.Key;
                    item.SubItems.Add(pair.Value.LocalizedName);

                    foreach (String existingKeyId in mSelectedKeys)
                    {
                        if (existingKeyId.Equals(pair.Key))
                        {
                            item.Checked = true;
                            item.BackColor = CHECKED_BACK_COLOR;
                            item.ForeColor = CHECKED_FORE_COLOR;
                            break;
                        }
                    }
                    lvKeyList.Items.Add(item);
                }

                lvKeyList.Sort();
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            FormInitLoading form = new FormInitLoading(true);
            form.ShowDialog();
        }

        // This event handler cancels the backgroundworker, fired from Cancel button in AlertForm.
        private void buttonCancel_Click(object sender, EventArgs e)
        {
            /*
            if (backgroundWorkerKeyListDownloader.WorkerSupportsCancellation == true)
            {
                // Cancel the asynchronous operation.
                backgroundWorkerKeyListDownloader.CancelAsync();

                // Close the AlertForm
                fDownloaderForm.Close();
            }*/
        }

        private void txtFilter_TextChanged(object sender, EventArgs e)
        {
            RefreshKeyList();
        }

        private void lvKeyList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lvKeyList.SelectedItems.Count > 0)
            {
                foreach (ListViewItem lvItem in lvKeyList.SelectedItems)
                {
                    lvItem.Checked = !lvItem.Checked;
                }
            }
        }

        private void lvKeyList_ItemChecked(object sender, ItemCheckedEventArgs e)
        {
            ListViewItem lvItem = e.Item;

            lvItem.ForeColor = lvItem.Checked ? CHECKED_FORE_COLOR : Color.Black;
            lvItem.BackColor = lvItem.Checked ? CHECKED_BACK_COLOR : Color.White;
        }
    }
}
