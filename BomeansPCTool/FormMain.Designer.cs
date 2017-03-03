namespace BomeansPCTool
{
    partial class FormMain
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMain));
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabScanTV = new System.Windows.Forms.TabPage();
            this.btnDeleteOne = new System.Windows.Forms.Button();
            this.btnReTransmit = new System.Windows.Forms.Button();
            this.btnMoveDown = new System.Windows.Forms.Button();
            this.btnMoveUp = new System.Windows.Forms.Button();
            this.btnPickKey = new System.Windows.Forms.Button();
            this.btnClearAll = new System.Windows.Forms.Button();
            this.lvKeyList = new System.Windows.Forms.ListView();
            this.grpLearnKey = new System.Windows.Forms.GroupBox();
            this.lblLearningResult = new System.Windows.Forms.Label();
            this.lblKeyName = new System.Windows.Forms.Label();
            this.progressBarLearning = new System.Windows.Forms.ProgressBar();
            this.btnLearn = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.lvResultList = new System.Windows.Forms.ListView();
            this.txtKeyId = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.toolStripMenuItemFile = new System.Windows.Forms.ToolStripMenuItem();
            this.newToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemSettings = new System.Windows.Forms.ToolStripMenuItem();
            this.irEasySettingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aPIKeyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemHelp = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
            this.downloadIRDataToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tabControl1.SuspendLayout();
            this.tabScanTV.SuspendLayout();
            this.grpLearnKey.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabScanTV);
            this.tabControl1.Location = new System.Drawing.Point(8, 22);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(828, 500);
            this.tabControl1.TabIndex = 4;
            // 
            // tabScanTV
            // 
            this.tabScanTV.Controls.Add(this.btnDeleteOne);
            this.tabScanTV.Controls.Add(this.btnReTransmit);
            this.tabScanTV.Controls.Add(this.btnMoveDown);
            this.tabScanTV.Controls.Add(this.btnMoveUp);
            this.tabScanTV.Controls.Add(this.btnPickKey);
            this.tabScanTV.Controls.Add(this.btnClearAll);
            this.tabScanTV.Controls.Add(this.lvKeyList);
            this.tabScanTV.Controls.Add(this.grpLearnKey);
            this.tabScanTV.Location = new System.Drawing.Point(4, 22);
            this.tabScanTV.Name = "tabScanTV";
            this.tabScanTV.Size = new System.Drawing.Size(820, 474);
            this.tabScanTV.TabIndex = 2;
            this.tabScanTV.Text = "Learn TV Remote";
            this.tabScanTV.UseVisualStyleBackColor = true;
            // 
            // btnDeleteOne
            // 
            this.btnDeleteOne.Image = ((System.Drawing.Image)(resources.GetObject("btnDeleteOne.Image")));
            this.btnDeleteOne.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnDeleteOne.Location = new System.Drawing.Point(787, 163);
            this.btnDeleteOne.Name = "btnDeleteOne";
            this.btnDeleteOne.Size = new System.Drawing.Size(28, 31);
            this.btnDeleteOne.TabIndex = 15;
            this.btnDeleteOne.UseVisualStyleBackColor = true;
            this.btnDeleteOne.Click += new System.EventHandler(this.btnDeleteOne_Click);
            // 
            // btnReTransmit
            // 
            this.btnReTransmit.Image = global::BomeansPCTool.Properties.Resources.play;
            this.btnReTransmit.Location = new System.Drawing.Point(787, 237);
            this.btnReTransmit.Name = "btnReTransmit";
            this.btnReTransmit.Size = new System.Drawing.Size(28, 31);
            this.btnReTransmit.TabIndex = 14;
            this.btnReTransmit.UseVisualStyleBackColor = true;
            this.btnReTransmit.Click += new System.EventHandler(this.btnReTransmit_Click);
            // 
            // btnMoveDown
            // 
            this.btnMoveDown.Image = global::BomeansPCTool.Properties.Resources.down;
            this.btnMoveDown.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnMoveDown.Location = new System.Drawing.Point(787, 335);
            this.btnMoveDown.Name = "btnMoveDown";
            this.btnMoveDown.Size = new System.Drawing.Size(28, 27);
            this.btnMoveDown.TabIndex = 13;
            this.btnMoveDown.UseVisualStyleBackColor = true;
            this.btnMoveDown.Click += new System.EventHandler(this.btnMoveDown_Click);
            // 
            // btnMoveUp
            // 
            this.btnMoveUp.Image = global::BomeansPCTool.Properties.Resources.up;
            this.btnMoveUp.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnMoveUp.Location = new System.Drawing.Point(787, 301);
            this.btnMoveUp.Name = "btnMoveUp";
            this.btnMoveUp.Size = new System.Drawing.Size(28, 28);
            this.btnMoveUp.TabIndex = 12;
            this.btnMoveUp.UseVisualStyleBackColor = true;
            this.btnMoveUp.Click += new System.EventHandler(this.btnMoveUp_Click);
            // 
            // btnPickKey
            // 
            this.btnPickKey.Image = ((System.Drawing.Image)(resources.GetObject("btnPickKey.Image")));
            this.btnPickKey.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnPickKey.Location = new System.Drawing.Point(787, 126);
            this.btnPickKey.Name = "btnPickKey";
            this.btnPickKey.Size = new System.Drawing.Size(28, 31);
            this.btnPickKey.TabIndex = 8;
            this.btnPickKey.UseVisualStyleBackColor = true;
            this.btnPickKey.Click += new System.EventHandler(this.btnPickKey_Click);
            // 
            // btnClearAll
            // 
            this.btnClearAll.Image = ((System.Drawing.Image)(resources.GetObject("btnClearAll.Image")));
            this.btnClearAll.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnClearAll.Location = new System.Drawing.Point(787, 200);
            this.btnClearAll.Name = "btnClearAll";
            this.btnClearAll.Size = new System.Drawing.Size(28, 31);
            this.btnClearAll.TabIndex = 2;
            this.btnClearAll.UseVisualStyleBackColor = true;
            this.btnClearAll.Click += new System.EventHandler(this.btnClearAll_Click);
            // 
            // lvKeyList
            // 
            this.lvKeyList.FullRowSelect = true;
            this.lvKeyList.GridLines = true;
            this.lvKeyList.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.lvKeyList.HideSelection = false;
            this.lvKeyList.LabelEdit = true;
            this.lvKeyList.Location = new System.Drawing.Point(3, 126);
            this.lvKeyList.MultiSelect = false;
            this.lvKeyList.Name = "lvKeyList";
            this.lvKeyList.Size = new System.Drawing.Size(779, 345);
            this.lvKeyList.TabIndex = 1;
            this.lvKeyList.UseCompatibleStateImageBehavior = false;
            this.lvKeyList.View = System.Windows.Forms.View.Details;
            this.lvKeyList.SelectedIndexChanged += new System.EventHandler(this.lvKeyList_SelectedIndexChanged);
            // 
            // grpLearnKey
            // 
            this.grpLearnKey.Controls.Add(this.lblLearningResult);
            this.grpLearnKey.Controls.Add(this.lblKeyName);
            this.grpLearnKey.Controls.Add(this.progressBarLearning);
            this.grpLearnKey.Controls.Add(this.btnLearn);
            this.grpLearnKey.Controls.Add(this.btnSave);
            this.grpLearnKey.Controls.Add(this.lvResultList);
            this.grpLearnKey.Controls.Add(this.txtKeyId);
            this.grpLearnKey.Controls.Add(this.label3);
            this.grpLearnKey.Controls.Add(this.label2);
            this.grpLearnKey.Location = new System.Drawing.Point(4, 4);
            this.grpLearnKey.Name = "grpLearnKey";
            this.grpLearnKey.Size = new System.Drawing.Size(813, 116);
            this.grpLearnKey.TabIndex = 0;
            this.grpLearnKey.TabStop = false;
            // 
            // lblLearningResult
            // 
            this.lblLearningResult.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblLearningResult.Location = new System.Drawing.Point(500, 65);
            this.lblLearningResult.Name = "lblLearningResult";
            this.lblLearningResult.Size = new System.Drawing.Size(185, 19);
            this.lblLearningResult.TabIndex = 9;
            this.lblLearningResult.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblKeyName
            // 
            this.lblKeyName.BackColor = System.Drawing.Color.Black;
            this.lblKeyName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblKeyName.Font = new System.Drawing.Font("Microsoft JhengHei", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lblKeyName.ForeColor = System.Drawing.SystemColors.Info;
            this.lblKeyName.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblKeyName.Location = new System.Drawing.Point(499, 14);
            this.lblKeyName.Name = "lblKeyName";
            this.lblKeyName.Size = new System.Drawing.Size(186, 45);
            this.lblKeyName.TabIndex = 8;
            this.lblKeyName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // progressBarLearning
            // 
            this.progressBarLearning.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.progressBarLearning.Location = new System.Drawing.Point(502, 96);
            this.progressBarLearning.Name = "progressBarLearning";
            this.progressBarLearning.Size = new System.Drawing.Size(183, 13);
            this.progressBarLearning.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.progressBarLearning.TabIndex = 7;
            // 
            // btnLearn
            // 
            this.btnLearn.Font = new System.Drawing.Font("Microsoft JhengHei", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.btnLearn.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnLearn.Location = new System.Drawing.Point(691, 14);
            this.btnLearn.Name = "btnLearn";
            this.btnLearn.Size = new System.Drawing.Size(108, 45);
            this.btnLearn.TabIndex = 6;
            this.btnLearn.Text = "Start Learning";
            this.btnLearn.UseVisualStyleBackColor = true;
            this.btnLearn.Click += new System.EventHandler(this.btnLearn_Click);
            // 
            // btnSave
            // 
            this.btnSave.Enabled = false;
            this.btnSave.Font = new System.Drawing.Font("Microsoft JhengHei", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.btnSave.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnSave.Location = new System.Drawing.Point(691, 65);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(108, 42);
            this.btnSave.TabIndex = 4;
            this.btnSave.Text = "Add";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // lvResultList
            // 
            this.lvResultList.FullRowSelect = true;
            this.lvResultList.GridLines = true;
            this.lvResultList.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.lvResultList.HideSelection = false;
            this.lvResultList.Location = new System.Drawing.Point(104, 42);
            this.lvResultList.MultiSelect = false;
            this.lvResultList.Name = "lvResultList";
            this.lvResultList.Size = new System.Drawing.Size(387, 67);
            this.lvResultList.TabIndex = 3;
            this.lvResultList.UseCompatibleStateImageBehavior = false;
            this.lvResultList.View = System.Windows.Forms.View.Details;
            // 
            // txtKeyId
            // 
            this.txtKeyId.Location = new System.Drawing.Point(104, 14);
            this.txtKeyId.Name = "txtKeyId";
            this.txtKeyId.Size = new System.Drawing.Size(387, 22);
            this.txtKeyId.TabIndex = 2;
            // 
            // label3
            // 
            this.label3.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label3.Location = new System.Drawing.Point(11, 44);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(86, 23);
            this.label3.TabIndex = 1;
            this.label3.Text = "Learning Result";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label2
            // 
            this.label2.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label2.Location = new System.Drawing.Point(9, 14);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(89, 23);
            this.label2.TabIndex = 0;
            this.label2.Text = "Key ID";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItemFile,
            this.toolStripMenuItemSettings,
            this.toolStripMenuItemHelp});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(3, 1, 0, 1);
            this.menuStrip1.Size = new System.Drawing.Size(839, 24);
            this.menuStrip1.TabIndex = 5;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // toolStripMenuItemFile
            // 
            this.toolStripMenuItemFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newToolStripMenuItem,
            this.loadToolStripMenuItem,
            this.saveToolStripMenuItem,
            this.toolStripMenuItem1,
            this.exitToolStripMenuItem});
            this.toolStripMenuItemFile.Name = "toolStripMenuItemFile";
            this.toolStripMenuItemFile.Size = new System.Drawing.Size(38, 22);
            this.toolStripMenuItemFile.Text = "&File";
            // 
            // newToolStripMenuItem
            // 
            this.newToolStripMenuItem.Name = "newToolStripMenuItem";
            this.newToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.newToolStripMenuItem.Text = "&New";
            this.newToolStripMenuItem.Click += new System.EventHandler(this.newToolStripMenuItem_Click);
            // 
            // loadToolStripMenuItem
            // 
            this.loadToolStripMenuItem.Name = "loadToolStripMenuItem";
            this.loadToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.loadToolStripMenuItem.Text = "&Load...";
            this.loadToolStripMenuItem.Click += new System.EventHandler(this.loadToolStripMenuItem_Click);
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.saveToolStripMenuItem.Text = "&Save...";
            this.saveToolStripMenuItem.Click += new System.EventHandler(this.saveToolStripMenuItem_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(149, 6);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.exitToolStripMenuItem.Text = "E&xit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // toolStripMenuItemSettings
            // 
            this.toolStripMenuItemSettings.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.irEasySettingsToolStripMenuItem,
            this.aPIKeyToolStripMenuItem,
            this.toolStripMenuItem2,
            this.downloadIRDataToolStripMenuItem});
            this.toolStripMenuItemSettings.Name = "toolStripMenuItemSettings";
            this.toolStripMenuItemSettings.Size = new System.Drawing.Size(64, 22);
            this.toolStripMenuItemSettings.Text = "&Settings";
            // 
            // irEasySettingsToolStripMenuItem
            // 
            this.irEasySettingsToolStripMenuItem.Name = "irEasySettingsToolStripMenuItem";
            this.irEasySettingsToolStripMenuItem.Size = new System.Drawing.Size(209, 22);
            this.irEasySettingsToolStripMenuItem.Text = "&IrEasy Settings...";
            this.irEasySettingsToolStripMenuItem.Click += new System.EventHandler(this.irEasySettingsToolStripMenuItem_Click);
            // 
            // aPIKeyToolStripMenuItem
            // 
            this.aPIKeyToolStripMenuItem.Name = "aPIKeyToolStripMenuItem";
            this.aPIKeyToolStripMenuItem.Size = new System.Drawing.Size(209, 22);
            this.aPIKeyToolStripMenuItem.Text = "&API Settings...";
            this.aPIKeyToolStripMenuItem.Click += new System.EventHandler(this.aPIKeyToolStripMenuItem_Click);
            // 
            // toolStripMenuItemHelp
            // 
            this.toolStripMenuItemHelp.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutToolStripMenuItem});
            this.toolStripMenuItemHelp.Name = "toolStripMenuItemHelp";
            this.toolStripMenuItemHelp.Size = new System.Drawing.Size(46, 22);
            this.toolStripMenuItemHelp.Text = "&Help";
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.aboutToolStripMenuItem.Text = "&About...";
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(206, 6);
            // 
            // downloadIRDataToolStripMenuItem
            // 
            this.downloadIRDataToolStripMenuItem.Name = "downloadIRDataToolStripMenuItem";
            this.downloadIRDataToolStripMenuItem.Size = new System.Drawing.Size(209, 22);
            this.downloadIRDataToolStripMenuItem.Text = "&Download Cloud Data...";
            this.downloadIRDataToolStripMenuItem.Click += new System.EventHandler(this.downloadIRDataToolStripMenuItem_Click);
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(839, 529);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.menuStrip1);
            this.Font = new System.Drawing.Font("PMingLiU", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "FormMain";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.FormMain_Load);
            this.Shown += new System.EventHandler(this.FormMain_Shown);
            this.tabControl1.ResumeLayout(false);
            this.tabScanTV.ResumeLayout(false);
            this.grpLearnKey.ResumeLayout(false);
            this.grpLearnKey.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabScanTV;
        private System.Windows.Forms.Button btnMoveDown;
        private System.Windows.Forms.Button btnMoveUp;
        private System.Windows.Forms.Button btnPickKey;
        private System.Windows.Forms.Button btnClearAll;
        private System.Windows.Forms.ListView lvKeyList;
        private System.Windows.Forms.GroupBox grpLearnKey;
        private System.Windows.Forms.Label lblLearningResult;
        private System.Windows.Forms.Label lblKeyName;
        private System.Windows.Forms.ProgressBar progressBarLearning;
        private System.Windows.Forms.Button btnLearn;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.ListView lvResultList;
        private System.Windows.Forms.TextBox txtKeyId;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemSettings;
        private System.Windows.Forms.ToolStripMenuItem irEasySettingsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aPIKeyToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemHelp;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemFile;
        private System.Windows.Forms.ToolStripMenuItem loadToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.Button btnReTransmit;
        private System.Windows.Forms.Button btnDeleteOne;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem downloadIRDataToolStripMenuItem;
    }
}

