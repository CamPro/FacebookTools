using System.Windows.Forms;

namespace addFriend_FB
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        private Button btnRun;

        private TextBox txtCookies;

        private TextBox txtUserAgent;

        private RichTextBox txtListUid;

        private Label label1;

        private Label label2;

        private Label label3;

        private Label lbResult;

        private Label lbOk;

        private Label lbNot;

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

            this.btnRun = new System.Windows.Forms.Button();
            this.txtCookies = new System.Windows.Forms.TextBox();
            this.txtUserAgent = new System.Windows.Forms.TextBox();
            this.txtListUid = new System.Windows.Forms.RichTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lbResult = new System.Windows.Forms.Label();
            this.lbOk = new System.Windows.Forms.Label();
            this.lbNot = new System.Windows.Forms.Label();
            base.SuspendLayout();
            this.btnRun.Location = new System.Drawing.Point(328, 439);
            this.btnRun.Name = "btnRun";
            this.btnRun.Size = new System.Drawing.Size(90, 54);
            this.btnRun.TabIndex = 0;
            this.btnRun.Text = "Run";
            this.btnRun.UseVisualStyleBackColor = true;
            this.btnRun.Click += new System.EventHandler(BtnRun_Click);
            this.txtCookies.Location = new System.Drawing.Point(87, 11);
            this.txtCookies.Name = "txtCookies";
            this.txtCookies.Size = new System.Drawing.Size(645, 20);
            this.txtCookies.TabIndex = 1;
            this.txtUserAgent.Location = new System.Drawing.Point(87, 41);
            this.txtUserAgent.Name = "txtUserAgent";
            this.txtUserAgent.Size = new System.Drawing.Size(645, 20);
            this.txtUserAgent.TabIndex = 2;
            this.txtListUid.Location = new System.Drawing.Point(27, 118);
            this.txtListUid.Name = "txtListUid";
            this.txtListUid.Size = new System.Drawing.Size(705, 295);
            this.txtListUid.TabIndex = 3;
            this.txtListUid.Text = "";
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(24, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(40, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Cookie";
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(24, 45);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(60, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "User-Agent";
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(24, 98);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(130, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "List uid (Mỗi uid một dòng)";
            this.lbResult.AutoSize = true;
            this.lbResult.Font = new System.Drawing.Font("Microsoft Sans Serif", 10f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            this.lbResult.Location = new System.Drawing.Point(24, 416);
            this.lbResult.Name = "lbResult";
            this.lbResult.Size = new System.Drawing.Size(20, 17);
            this.lbResult.TabIndex = 6;
            this.lbResult.Text = "...";
            this.lbOk.AutoSize = true;
            this.lbOk.Font = new System.Drawing.Font("Microsoft Sans Serif", 10f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            this.lbOk.ForeColor = System.Drawing.Color.FromArgb(0, 64, 0);
            this.lbOk.Location = new System.Drawing.Point(160, 96);
            this.lbOk.Name = "lbOk";
            this.lbOk.Size = new System.Drawing.Size(20, 17);
            this.lbOk.TabIndex = 6;
            this.lbOk.Text = "...";
            this.lbNot.AutoSize = true;
            this.lbNot.Font = new System.Drawing.Font("Microsoft Sans Serif", 10f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            this.lbNot.ForeColor = System.Drawing.Color.Red;
            this.lbNot.Location = new System.Drawing.Point(463, 96);
            this.lbNot.Name = "lbNot";
            this.lbNot.Size = new System.Drawing.Size(20, 17);
            this.lbNot.TabIndex = 6;
            this.lbNot.Text = "...";
            base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
            base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            base.ClientSize = new System.Drawing.Size(747, 505);
            base.Controls.Add(this.lbResult);
            base.Controls.Add(this.lbNot);
            base.Controls.Add(this.lbOk);
            base.Controls.Add(this.label3);
            base.Controls.Add(this.label2);
            base.Controls.Add(this.label1);
            base.Controls.Add(this.txtListUid);
            base.Controls.Add(this.txtUserAgent);
            base.Controls.Add(this.txtCookies);
            base.Controls.Add(this.btnRun);
            base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            base.MaximizeBox = false;
            base.MinimizeBox = false;
            base.Name = "Form1";
            base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Add Friend Facebook - Anh Tuấn";
            base.ResumeLayout(false);
            base.PerformLayout();
        }

        #endregion
    }
}

