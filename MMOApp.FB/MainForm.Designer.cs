namespace MMOApp.FB
{
    partial class MainForm
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
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.splitContainer3 = new System.Windows.Forms.SplitContainer();
            this.txtAccounts = new System.Windows.Forms.RichTextBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnGetAllVia = new System.Windows.Forms.Button();
            this.btnGetNewVia = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.txtAccountsTrusted = new System.Windows.Forms.RichTextBox();
            this.panel3 = new System.Windows.Forms.Panel();
            this.btnGetViaTrusted = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.splitContainer4 = new System.Windows.Forms.SplitContainer();
            this.splitContainer5 = new System.Windows.Forms.SplitContainer();
            this.txtAccountsLive = new System.Windows.Forms.RichTextBox();
            this.panel4 = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.txtAccountsCP = new System.Windows.Forms.RichTextBox();
            this.panel5 = new System.Windows.Forms.Panel();
            this.btnGetViaCP = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.txtLog = new System.Windows.Forms.RichTextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnUnlockCPAccounts = new System.Windows.Forms.Button();
            this.btnBackupData = new System.Windows.Forms.Button();
            this.btnUnlock = new System.Windows.Forms.Button();
            this.btnBackUpProfile = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).BeginInit();
            this.splitContainer3.Panel1.SuspendLayout();
            this.splitContainer3.Panel2.SuspendLayout();
            this.splitContainer3.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer4)).BeginInit();
            this.splitContainer4.Panel1.SuspendLayout();
            this.splitContainer4.Panel2.SuspendLayout();
            this.splitContainer4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer5)).BeginInit();
            this.splitContainer5.Panel1.SuspendLayout();
            this.splitContainer5.Panel2.SuspendLayout();
            this.splitContainer5.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel5.SuspendLayout();
            this.panel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.splitContainer1.IsSplitterFixed = true;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.splitContainer2);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.panel1);
            this.splitContainer1.Size = new System.Drawing.Size(1101, 655);
            this.splitContainer1.SplitterDistance = 856;
            this.splitContainer1.TabIndex = 0;
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.splitContainer3);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.splitContainer4);
            this.splitContainer2.Size = new System.Drawing.Size(856, 655);
            this.splitContainer2.SplitterDistance = 291;
            this.splitContainer2.TabIndex = 0;
            // 
            // splitContainer3
            // 
            this.splitContainer3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer3.Location = new System.Drawing.Point(0, 0);
            this.splitContainer3.Name = "splitContainer3";
            // 
            // splitContainer3.Panel1
            // 
            this.splitContainer3.Panel1.Controls.Add(this.txtAccounts);
            this.splitContainer3.Panel1.Controls.Add(this.panel2);
            // 
            // splitContainer3.Panel2
            // 
            this.splitContainer3.Panel2.Controls.Add(this.txtAccountsTrusted);
            this.splitContainer3.Panel2.Controls.Add(this.panel3);
            this.splitContainer3.Size = new System.Drawing.Size(856, 291);
            this.splitContainer3.SplitterDistance = 419;
            this.splitContainer3.TabIndex = 2;
            // 
            // txtAccounts
            // 
            this.txtAccounts.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtAccounts.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtAccounts.Location = new System.Drawing.Point(0, 39);
            this.txtAccounts.Name = "txtAccounts";
            this.txtAccounts.Size = new System.Drawing.Size(419, 252);
            this.txtAccounts.TabIndex = 1;
            this.txtAccounts.Text = "";
            this.txtAccounts.WordWrap = false;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.btnGetAllVia);
            this.panel2.Controls.Add(this.btnGetNewVia);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(419, 39);
            this.panel2.TabIndex = 2;
            // 
            // btnGetAllVia
            // 
            this.btnGetAllVia.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnGetAllVia.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGetAllVia.Location = new System.Drawing.Point(309, 5);
            this.btnGetAllVia.Name = "btnGetAllVia";
            this.btnGetAllVia.Size = new System.Drawing.Size(105, 30);
            this.btnGetAllVia.TabIndex = 2;
            this.btnGetAllVia.Text = "Get ALL";
            this.btnGetAllVia.UseVisualStyleBackColor = true;
            this.btnGetAllVia.Click += new System.EventHandler(this.btnGetAllVia_Click);
            // 
            // btnGetNewVia
            // 
            this.btnGetNewVia.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnGetNewVia.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGetNewVia.Location = new System.Drawing.Point(198, 5);
            this.btnGetNewVia.Name = "btnGetNewVia";
            this.btnGetNewVia.Size = new System.Drawing.Size(105, 30);
            this.btnGetNewVia.TabIndex = 1;
            this.btnGetNewVia.Text = "Get NEW";
            this.btnGetNewVia.UseVisualStyleBackColor = true;
            this.btnGetNewVia.Click += new System.EventHandler(this.btnGetVia_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(17, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(97, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "Account List";
            // 
            // txtAccountsTrusted
            // 
            this.txtAccountsTrusted.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtAccountsTrusted.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtAccountsTrusted.Location = new System.Drawing.Point(0, 39);
            this.txtAccountsTrusted.Name = "txtAccountsTrusted";
            this.txtAccountsTrusted.Size = new System.Drawing.Size(433, 252);
            this.txtAccountsTrusted.TabIndex = 4;
            this.txtAccountsTrusted.Text = "";
            this.txtAccountsTrusted.WordWrap = false;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.btnGetViaTrusted);
            this.panel3.Controls.Add(this.label2);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(433, 39);
            this.panel3.TabIndex = 3;
            // 
            // btnGetViaTrusted
            // 
            this.btnGetViaTrusted.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnGetViaTrusted.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGetViaTrusted.Location = new System.Drawing.Point(316, 5);
            this.btnGetViaTrusted.Name = "btnGetViaTrusted";
            this.btnGetViaTrusted.Size = new System.Drawing.Size(105, 30);
            this.btnGetViaTrusted.TabIndex = 2;
            this.btnGetViaTrusted.Text = "Get Accs";
            this.btnGetViaTrusted.UseVisualStyleBackColor = true;
            this.btnGetViaTrusted.Click += new System.EventHandler(this.btnGetViaTrusted_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(17, 10);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(134, 20);
            this.label2.TabIndex = 0;
            this.label2.Text = "Trusted Accounts";
            // 
            // splitContainer4
            // 
            this.splitContainer4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer4.Location = new System.Drawing.Point(0, 0);
            this.splitContainer4.Name = "splitContainer4";
            this.splitContainer4.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer4.Panel1
            // 
            this.splitContainer4.Panel1.Controls.Add(this.splitContainer5);
            // 
            // splitContainer4.Panel2
            // 
            this.splitContainer4.Panel2.Controls.Add(this.txtLog);
            this.splitContainer4.Size = new System.Drawing.Size(856, 360);
            this.splitContainer4.SplitterDistance = 247;
            this.splitContainer4.TabIndex = 3;
            // 
            // splitContainer5
            // 
            this.splitContainer5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer5.Location = new System.Drawing.Point(0, 0);
            this.splitContainer5.Name = "splitContainer5";
            // 
            // splitContainer5.Panel1
            // 
            this.splitContainer5.Panel1.Controls.Add(this.txtAccountsLive);
            this.splitContainer5.Panel1.Controls.Add(this.panel4);
            // 
            // splitContainer5.Panel2
            // 
            this.splitContainer5.Panel2.Controls.Add(this.txtAccountsCP);
            this.splitContainer5.Panel2.Controls.Add(this.panel5);
            this.splitContainer5.Size = new System.Drawing.Size(856, 247);
            this.splitContainer5.SplitterDistance = 418;
            this.splitContainer5.TabIndex = 0;
            // 
            // txtAccountsLive
            // 
            this.txtAccountsLive.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtAccountsLive.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtAccountsLive.Location = new System.Drawing.Point(0, 41);
            this.txtAccountsLive.Name = "txtAccountsLive";
            this.txtAccountsLive.Size = new System.Drawing.Size(418, 206);
            this.txtAccountsLive.TabIndex = 3;
            this.txtAccountsLive.Text = "";
            this.txtAccountsLive.WordWrap = false;
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.label3);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel4.Location = new System.Drawing.Point(0, 0);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(418, 41);
            this.panel4.TabIndex = 4;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(17, 11);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(108, 20);
            this.label3.TabIndex = 0;
            this.label3.Text = "Account LIVE";
            // 
            // txtAccountsCP
            // 
            this.txtAccountsCP.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtAccountsCP.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtAccountsCP.Location = new System.Drawing.Point(0, 41);
            this.txtAccountsCP.Name = "txtAccountsCP";
            this.txtAccountsCP.Size = new System.Drawing.Size(434, 206);
            this.txtAccountsCP.TabIndex = 3;
            this.txtAccountsCP.Text = "";
            this.txtAccountsCP.WordWrap = false;
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.btnGetViaCP);
            this.panel5.Controls.Add(this.label4);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel5.Location = new System.Drawing.Point(0, 0);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(434, 41);
            this.panel5.TabIndex = 4;
            // 
            // btnGetViaCP
            // 
            this.btnGetViaCP.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnGetViaCP.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGetViaCP.Location = new System.Drawing.Point(319, 6);
            this.btnGetViaCP.Name = "btnGetViaCP";
            this.btnGetViaCP.Size = new System.Drawing.Size(105, 30);
            this.btnGetViaCP.TabIndex = 3;
            this.btnGetViaCP.Text = "Get Accs";
            this.btnGetViaCP.UseVisualStyleBackColor = true;
            this.btnGetViaCP.Click += new System.EventHandler(this.btnGetViaCP_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(17, 11);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(178, 20);
            this.label4.TabIndex = 0;
            this.label4.Text = "Account CHECK PONIT";
            // 
            // txtLog
            // 
            this.txtLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtLog.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtLog.Location = new System.Drawing.Point(0, 0);
            this.txtLog.Name = "txtLog";
            this.txtLog.Size = new System.Drawing.Size(856, 109);
            this.txtLog.TabIndex = 2;
            this.txtLog.Text = "";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(241, 655);
            this.panel1.TabIndex = 0;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnBackUpProfile);
            this.groupBox1.Controls.Add(this.btnUnlockCPAccounts);
            this.groupBox1.Controls.Add(this.btnBackupData);
            this.groupBox1.Controls.Add(this.btnUnlock);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(217, 353);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "CONTROL";
            // 
            // btnUnlockCPAccounts
            // 
            this.btnUnlockCPAccounts.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnUnlockCPAccounts.Location = new System.Drawing.Point(21, 203);
            this.btnUnlockCPAccounts.Name = "btnUnlockCPAccounts";
            this.btnUnlockCPAccounts.Size = new System.Drawing.Size(173, 62);
            this.btnUnlockCPAccounts.TabIndex = 2;
            this.btnUnlockCPAccounts.Text = "UNLOCK CP ACCOUNTS";
            this.btnUnlockCPAccounts.UseVisualStyleBackColor = true;
            this.btnUnlockCPAccounts.Click += new System.EventHandler(this.btnUnlockCPAccounts_Click);
            // 
            // btnBackupData
            // 
            this.btnBackupData.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBackupData.Location = new System.Drawing.Point(21, 44);
            this.btnBackupData.Name = "btnBackupData";
            this.btnBackupData.Size = new System.Drawing.Size(173, 62);
            this.btnBackupData.TabIndex = 1;
            this.btnBackupData.Text = "BACKUP DATA";
            this.btnBackupData.UseVisualStyleBackColor = true;
            this.btnBackupData.Click += new System.EventHandler(this.btnBackupData_Click);
            // 
            // btnUnlock
            // 
            this.btnUnlock.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnUnlock.Location = new System.Drawing.Point(21, 271);
            this.btnUnlock.Name = "btnUnlock";
            this.btnUnlock.Size = new System.Drawing.Size(173, 62);
            this.btnUnlock.TabIndex = 0;
            this.btnUnlock.Text = "UNLOCK ALL ACCOUNTS";
            this.btnUnlock.UseVisualStyleBackColor = true;
            this.btnUnlock.Click += new System.EventHandler(this.btnUnlock_Click);
            // 
            // btnBackUpProfile
            // 
            this.btnBackUpProfile.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBackUpProfile.Location = new System.Drawing.Point(21, 112);
            this.btnBackUpProfile.Name = "btnBackUpProfile";
            this.btnBackUpProfile.Size = new System.Drawing.Size(173, 62);
            this.btnBackUpProfile.TabIndex = 3;
            this.btnBackUpProfile.Text = "BACKUP PROFILE";
            this.btnBackUpProfile.UseVisualStyleBackColor = true;
            this.btnBackUpProfile.Click += new System.EventHandler(this.btnBackUpProfile_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1101, 655);
            this.Controls.Add(this.splitContainer1);
            this.Name = "MainForm";
            this.Text = "FACEBOOK CHECKPOINT UNLOCKER";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.splitContainer3.Panel1.ResumeLayout(false);
            this.splitContainer3.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).EndInit();
            this.splitContainer3.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.splitContainer4.Panel1.ResumeLayout(false);
            this.splitContainer4.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer4)).EndInit();
            this.splitContainer4.ResumeLayout(false);
            this.splitContainer5.Panel1.ResumeLayout(false);
            this.splitContainer5.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer5)).EndInit();
            this.splitContainer5.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnUnlock;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.RichTextBox txtAccounts;
        private System.Windows.Forms.RichTextBox txtLog;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.SplitContainer splitContainer3;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.RichTextBox txtAccountsTrusted;
        private System.Windows.Forms.SplitContainer splitContainer4;
        private System.Windows.Forms.SplitContainer splitContainer5;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.RichTextBox txtAccountsLive;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.RichTextBox txtAccountsCP;
        private System.Windows.Forms.Button btnGetNewVia;
        private System.Windows.Forms.Button btnGetViaTrusted;
        private System.Windows.Forms.Button btnGetViaCP;
        private System.Windows.Forms.Button btnBackupData;
        private System.Windows.Forms.Button btnUnlockCPAccounts;
        private System.Windows.Forms.Button btnGetAllVia;
        private System.Windows.Forms.Button btnBackUpProfile;
    }
}

