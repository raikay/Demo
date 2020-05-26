namespace Lb.BackDb
{
    partial class Settings
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Settings));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cbClearMonth = new System.Windows.Forms.ComboBox();
            this.cbBakYear = new System.Windows.Forms.ComboBox();
            this.label13 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.ckIsNewFolder = new System.Windows.Forms.CheckBox();
            this.txtStartDate = new System.Windows.Forms.DateTimePicker();
            this.txtCreateNameRegulation = new System.Windows.Forms.TextBox();
            this.txtSaveDirectory = new System.Windows.Forms.TextBox();
            this.txtTimeInterval = new System.Windows.Forms.MaskedTextBox();
            this.btn_SetDirectory = new System.Windows.Forms.Button();
            this.txtDbPassword = new System.Windows.Forms.TextBox();
            this.txtDbLoginName = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txtServiceIp = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.listDb = new System.Windows.Forms.ListBox();
            this.txtDbName = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.btn_DelDbName = new System.Windows.Forms.Button();
            this.btn_AddDbName = new System.Windows.Forms.Button();
            this.GetDirPathDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnTestConnection = new System.Windows.Forms.Button();
            this.btnSaveConfig = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cbClearMonth);
            this.groupBox1.Controls.Add(this.cbBakYear);
            this.groupBox1.Controls.Add(this.label13);
            this.groupBox1.Controls.Add(this.label11);
            this.groupBox1.Controls.Add(this.ckIsNewFolder);
            this.groupBox1.Controls.Add(this.txtStartDate);
            this.groupBox1.Controls.Add(this.txtCreateNameRegulation);
            this.groupBox1.Controls.Add(this.txtSaveDirectory);
            this.groupBox1.Controls.Add(this.txtTimeInterval);
            this.groupBox1.Controls.Add(this.btn_SetDirectory);
            this.groupBox1.Controls.Add(this.txtDbPassword);
            this.groupBox1.Controls.Add(this.txtDbLoginName);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.txtServiceIp);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(444, 214);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Setting";
            // 
            // cbClearMonth
            // 
            this.cbClearMonth.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbClearMonth.FormattingEnabled = true;
            this.cbClearMonth.Items.AddRange(new object[] {
            "不清理",
            "不清理第1月",
            "不清理前2月",
            "不清理前3月"});
            this.cbClearMonth.Location = new System.Drawing.Point(170, 168);
            this.cbClearMonth.Name = "cbClearMonth";
            this.cbClearMonth.Size = new System.Drawing.Size(156, 20);
            this.cbClearMonth.TabIndex = 13;
            // 
            // cbBakYear
            // 
            this.cbBakYear.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbBakYear.FormattingEnabled = true;
            this.cbBakYear.Items.AddRange(new object[] {
            "1年",
            "2年",
            "3年"});
            this.cbBakYear.Location = new System.Drawing.Point(170, 190);
            this.cbBakYear.Name = "cbBakYear";
            this.cbBakYear.Size = new System.Drawing.Size(72, 20);
            this.cbBakYear.TabIndex = 12;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(77, 193);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(89, 12);
            this.label13.TabIndex = 11;
            this.label13.Text = "备份保存时长：";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(28, 173);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(155, 12);
            this.label11.TabIndex = 11;
            this.label11.Text = "清理冗余备份(月保留1份)：";
            // 
            // ckIsNewFolder
            // 
            this.ckIsNewFolder.AutoSize = true;
            this.ckIsNewFolder.ForeColor = System.Drawing.Color.Red;
            this.ckIsNewFolder.Location = new System.Drawing.Point(169, 152);
            this.ckIsNewFolder.Name = "ckIsNewFolder";
            this.ckIsNewFolder.Size = new System.Drawing.Size(36, 16);
            this.ckIsNewFolder.TabIndex = 10;
            this.ckIsNewFolder.Text = "是";
            this.ckIsNewFolder.UseVisualStyleBackColor = true;
            // 
            // txtStartDate
            // 
            this.txtStartDate.CustomFormat = "yyyy-MM-dd HH:mm:ss";
            this.txtStartDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.txtStartDate.Location = new System.Drawing.Point(90, 74);
            this.txtStartDate.Name = "txtStartDate";
            this.txtStartDate.Size = new System.Drawing.Size(151, 21);
            this.txtStartDate.TabIndex = 4;
            // 
            // txtCreateNameRegulation
            // 
            this.txtCreateNameRegulation.Location = new System.Drawing.Point(90, 127);
            this.txtCreateNameRegulation.Name = "txtCreateNameRegulation";
            this.txtCreateNameRegulation.Size = new System.Drawing.Size(287, 21);
            this.txtCreateNameRegulation.TabIndex = 9;
            this.txtCreateNameRegulation.Text = "{DB}-yyyyMMddHHmmssfff";
            // 
            // txtSaveDirectory
            // 
            this.txtSaveDirectory.Location = new System.Drawing.Point(90, 101);
            this.txtSaveDirectory.Name = "txtSaveDirectory";
            this.txtSaveDirectory.Size = new System.Drawing.Size(287, 21);
            this.txtSaveDirectory.TabIndex = 7;
            // 
            // txtTimeInterval
            // 
            this.txtTimeInterval.Location = new System.Drawing.Point(313, 76);
            this.txtTimeInterval.Mask = "99天99小时99分钟";
            this.txtTimeInterval.Name = "txtTimeInterval";
            this.txtTimeInterval.Size = new System.Drawing.Size(125, 21);
            this.txtTimeInterval.TabIndex = 6;
            this.txtTimeInterval.Text = "000300";
            // 
            // btn_SetDirectory
            // 
            this.btn_SetDirectory.Location = new System.Drawing.Point(375, 100);
            this.btn_SetDirectory.Name = "btn_SetDirectory";
            this.btn_SetDirectory.Size = new System.Drawing.Size(58, 23);
            this.btn_SetDirectory.TabIndex = 8;
            this.btn_SetDirectory.Text = "浏览...";
            this.btn_SetDirectory.UseVisualStyleBackColor = true;
            this.btn_SetDirectory.Click += new System.EventHandler(this.btn_SetDirectory_Click);
            // 
            // txtDbPassword
            // 
            this.txtDbPassword.Location = new System.Drawing.Point(313, 46);
            this.txtDbPassword.Name = "txtDbPassword";
            this.txtDbPassword.Size = new System.Drawing.Size(125, 21);
            this.txtDbPassword.TabIndex = 3;
            // 
            // txtDbLoginName
            // 
            this.txtDbLoginName.Location = new System.Drawing.Point(90, 46);
            this.txtDbLoginName.Name = "txtDbLoginName";
            this.txtDbLoginName.Size = new System.Drawing.Size(151, 21);
            this.txtDbLoginName.TabIndex = 2;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.ForeColor = System.Drawing.Color.Red;
            this.label8.Location = new System.Drawing.Point(247, 20);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(167, 12);
            this.label8.TabIndex = 5;
            this.label8.Text = "(IP地址，本地可以使用.代替)";
            // 
            // txtServiceIp
            // 
            this.txtServiceIp.Location = new System.Drawing.Point(90, 17);
            this.txtServiceIp.Name = "txtServiceIp";
            this.txtServiceIp.Size = new System.Drawing.Size(151, 21);
            this.txtServiceIp.TabIndex = 1;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(17, 153);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(149, 12);
            this.label5.TabIndex = 1;
            this.label5.Text = "每个数据库创建子文件夹：";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(19, 131);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(65, 12);
            this.label7.TabIndex = 1;
            this.label7.Text = "命名规则：";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(19, 105);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(65, 12);
            this.label6.TabIndex = 1;
            this.label6.Text = "备份路径：";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(19, 79);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(65, 12);
            this.label9.TabIndex = 1;
            this.label9.Text = "首次备份：";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(242, 80);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(65, 12);
            this.label4.TabIndex = 1;
            this.label4.Text = "备份间隔：";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(266, 50);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 12);
            this.label3.TabIndex = 1;
            this.label3.Text = "密码：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(19, 50);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(77, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "数据库账号：";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(19, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "服务器地址：";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.groupBox4);
            this.groupBox2.Controls.Add(this.txtDbName);
            this.groupBox2.Controls.Add(this.label10);
            this.groupBox2.Controls.Add(this.btn_DelDbName);
            this.groupBox2.Controls.Add(this.btn_AddDbName);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(0, 214);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(444, 282);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "需要备份数据库";
            // 
            // groupBox4
            // 
            this.groupBox4.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox4.Controls.Add(this.listDb);
            this.groupBox4.Location = new System.Drawing.Point(0, 40);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(444, 194);
            this.groupBox4.TabIndex = 4;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "数据库列表";
            // 
            // listDb
            // 
            this.listDb.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listDb.FormattingEnabled = true;
            this.listDb.ItemHeight = 12;
            this.listDb.Location = new System.Drawing.Point(3, 17);
            this.listDb.Name = "listDb";
            this.listDb.Size = new System.Drawing.Size(438, 174);
            this.listDb.TabIndex = 13;
            // 
            // txtDbName
            // 
            this.txtDbName.Location = new System.Drawing.Point(81, 13);
            this.txtDbName.Name = "txtDbName";
            this.txtDbName.Size = new System.Drawing.Size(199, 21);
            this.txtDbName.TabIndex = 10;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(19, 16);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(65, 12);
            this.label10.TabIndex = 2;
            this.label10.Text = "数据库名：";
            // 
            // btn_DelDbName
            // 
            this.btn_DelDbName.Location = new System.Drawing.Point(367, 11);
            this.btn_DelDbName.Name = "btn_DelDbName";
            this.btn_DelDbName.Size = new System.Drawing.Size(75, 23);
            this.btn_DelDbName.TabIndex = 12;
            this.btn_DelDbName.Text = "删  除";
            this.btn_DelDbName.UseVisualStyleBackColor = true;
            this.btn_DelDbName.Click += new System.EventHandler(this.btn_DelDbName_Click);
            // 
            // btn_AddDbName
            // 
            this.btn_AddDbName.Location = new System.Drawing.Point(286, 11);
            this.btn_AddDbName.Name = "btn_AddDbName";
            this.btn_AddDbName.Size = new System.Drawing.Size(75, 23);
            this.btn_AddDbName.TabIndex = 11;
            this.btn_AddDbName.Text = "添  加";
            this.btn_AddDbName.UseVisualStyleBackColor = true;
            this.btn_AddDbName.Click += new System.EventHandler(this.btn_AddDbName_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.btnCancel);
            this.groupBox3.Controls.Add(this.btnTestConnection);
            this.groupBox3.Controls.Add(this.btnSaveConfig);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.groupBox3.Location = new System.Drawing.Point(0, 455);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(444, 41);
            this.groupBox3.TabIndex = 4;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "操作";
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(367, 12);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 16;
            this.btnCancel.Text = "取 消";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnTestConnection
            // 
            this.btnTestConnection.Location = new System.Drawing.Point(169, 12);
            this.btnTestConnection.Name = "btnTestConnection";
            this.btnTestConnection.Size = new System.Drawing.Size(93, 23);
            this.btnTestConnection.TabIndex = 14;
            this.btnTestConnection.Text = "测 试 连 接";
            this.btnTestConnection.UseVisualStyleBackColor = true;
            this.btnTestConnection.Click += new System.EventHandler(this.btnTestConnection_Click);
            // 
            // btnSaveConfig
            // 
            this.btnSaveConfig.Location = new System.Drawing.Point(268, 12);
            this.btnSaveConfig.Name = "btnSaveConfig";
            this.btnSaveConfig.Size = new System.Drawing.Size(93, 23);
            this.btnSaveConfig.TabIndex = 15;
            this.btnSaveConfig.Text = "保 存 配 置";
            this.btnSaveConfig.UseVisualStyleBackColor = true;
            this.btnSaveConfig.Click += new System.EventHandler(this.btnSaveConfig_Click);
            // 
            // Settings
            // 
            this.AcceptButton = this.btnTestConnection;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(444, 496);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Settings";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Backup configuration Settings";
            this.Load += new System.EventHandler(this.Settings_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.MaskedTextBox txtTimeInterval;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtCreateNameRegulation;
        private System.Windows.Forms.TextBox txtSaveDirectory;
        private System.Windows.Forms.Button btn_SetDirectory;
        private System.Windows.Forms.TextBox txtDbPassword;
        private System.Windows.Forms.TextBox txtDbLoginName;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtServiceIp;
        private System.Windows.Forms.Button btn_DelDbName;
        private System.Windows.Forms.Button btn_AddDbName;
        private System.Windows.Forms.FolderBrowserDialog GetDirPathDialog;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnSaveConfig;
        private System.Windows.Forms.DateTimePicker txtStartDate;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtDbName;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.ListBox listDb;
        private System.Windows.Forms.Button btnTestConnection;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.CheckBox ckIsNewFolder;
        private System.Windows.Forms.ComboBox cbBakYear;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.ComboBox cbClearMonth;
    }
}