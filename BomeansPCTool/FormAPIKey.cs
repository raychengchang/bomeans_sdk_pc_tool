using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BomeansPCTool.Properties;

namespace BomeansPCTool
{
    public partial class FormAPIKey : Form
    {
        private String mCurrentApiKey = "";
        private Boolean mUseChinaServer = false;

        public String APIKey {  get { return mCurrentApiKey; } }
        public Boolean UseChinaServer {  get { return mUseChinaServer; } }

        public FormAPIKey(String apiKey, Boolean useChinaServer)
        {
            InitializeComponent();

            mCurrentApiKey = apiKey;
            mUseChinaServer = false;
        }

        private void FormAPIKey_Load(object sender, EventArgs e)
        {
            txtApiKey.Text = mCurrentApiKey;
            chkUseChinaServer.Checked = mUseChinaServer ? true : false;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            mUseChinaServer = chkUseChinaServer.Checked;

            mCurrentApiKey = txtApiKey.Text.Trim();
            if (mCurrentApiKey.Length == 0)
            {
                Program.ShowError(Resources.E_EMPTY_APIKEY);
                return;
            }

            DialogResult = DialogResult.OK;
            Close();
        }

        private void lnkInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            lnkInfo.LinkVisited = true;
            System.Diagnostics.Process.Start("http://www.bomeans.com/Mainpage/Apply/apikey");
        }
    }
}
