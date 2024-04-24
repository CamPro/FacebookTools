namespace reg._.dcom.driver
{
     partial class Form1
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
               System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
               this.toolStrip1 = new System.Windows.Forms.ToolStrip();
               this.tbarStartAutoView = new System.Windows.Forms.ToolStripButton();
               this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
               this.tabControl1 = new System.Windows.Forms.TabControl();
               this.tabPage1 = new System.Windows.Forms.TabPage();
               this.dataGridView1 = new System.Windows.Forms.DataGridView();
               this.tabPage2 = new System.Windows.Forms.TabPage();
               this.dataGridView2 = new System.Windows.Forms.DataGridView();
               this.about = new System.Windows.Forms.TabPage();
               this.pictureBox1 = new System.Windows.Forms.PictureBox();
               this.statusStrip1 = new System.Windows.Forms.StatusStrip();
               this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
               this.label1 = new System.Windows.Forms.Label();
               this.linkLabel1 = new System.Windows.Forms.LinkLabel();
               this.label2 = new System.Windows.Forms.Label();
               this.label3 = new System.Windows.Forms.Label();
               this.label4 = new System.Windows.Forms.Label();
               this.toolStrip1.SuspendLayout();
               this.tabControl1.SuspendLayout();
               this.tabPage1.SuspendLayout();
               ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
               this.tabPage2.SuspendLayout();
               ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).BeginInit();
               this.about.SuspendLayout();
               ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
               this.statusStrip1.SuspendLayout();
               this.SuspendLayout();
               // 
               // toolStrip1
               // 
               this.toolStrip1.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
               this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tbarStartAutoView,
            this.toolStripLabel1});
               this.toolStrip1.Location = new System.Drawing.Point(0, 0);
               this.toolStrip1.Name = "toolStrip1";
               this.toolStrip1.Size = new System.Drawing.Size(839, 28);
               this.toolStrip1.TabIndex = 1;
               this.toolStrip1.Text = "toolStrip1";
               // 
               // tbarStartAutoView
               // 
               this.tbarStartAutoView.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
               this.tbarStartAutoView.Image = ((System.Drawing.Image)(resources.GetObject("tbarStartAutoView.Image")));
               this.tbarStartAutoView.ImageTransparentColor = System.Drawing.Color.Magenta;
               this.tbarStartAutoView.Name = "tbarStartAutoView";
               this.tbarStartAutoView.Size = new System.Drawing.Size(133, 25);
               this.tbarStartAutoView.Text = "Start AutoView";
               this.tbarStartAutoView.Click += new System.EventHandler(this.tbarStartAutoView_Click);
               // 
               // toolStripLabel1
               // 
               this.toolStripLabel1.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
               this.toolStripLabel1.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
               this.toolStripLabel1.Name = "toolStripLabel1";
               this.toolStripLabel1.RightToLeft = System.Windows.Forms.RightToLeft.No;
               this.toolStripLabel1.Size = new System.Drawing.Size(480, 25);
               this.toolStripLabel1.Text = "Contact:  fb.com/vinguyet6666 , Email: autofarmer1999@gmail.com";
               this.toolStripLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
               this.toolStripLabel1.Click += new System.EventHandler(this.toolStripLabel1_Click);
               // 
               // tabControl1
               // 
               this.tabControl1.Controls.Add(this.tabPage1);
               this.tabControl1.Controls.Add(this.tabPage2);
               this.tabControl1.Controls.Add(this.about);
               this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
               this.tabControl1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
               this.tabControl1.Location = new System.Drawing.Point(0, 28);
               this.tabControl1.Name = "tabControl1";
               this.tabControl1.SelectedIndex = 0;
               this.tabControl1.Size = new System.Drawing.Size(839, 381);
               this.tabControl1.TabIndex = 2;
               // 
               // tabPage1
               // 
               this.tabPage1.Controls.Add(this.dataGridView1);
               this.tabPage1.Location = new System.Drawing.Point(4, 27);
               this.tabPage1.Name = "tabPage1";
               this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
               this.tabPage1.Size = new System.Drawing.Size(831, 350);
               this.tabPage1.TabIndex = 0;
               this.tabPage1.Text = "view";
               this.tabPage1.UseVisualStyleBackColor = true;
               // 
               // dataGridView1
               // 
               this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
               this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
               this.dataGridView1.Location = new System.Drawing.Point(3, 3);
               this.dataGridView1.Name = "dataGridView1";
               this.dataGridView1.Size = new System.Drawing.Size(825, 344);
               this.dataGridView1.TabIndex = 0;
               // 
               // tabPage2
               // 
               this.tabPage2.Controls.Add(this.dataGridView2);
               this.tabPage2.Location = new System.Drawing.Point(4, 27);
               this.tabPage2.Name = "tabPage2";
               this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
               this.tabPage2.Size = new System.Drawing.Size(831, 350);
               this.tabPage2.TabIndex = 1;
               this.tabPage2.Text = "log";
               this.tabPage2.UseVisualStyleBackColor = true;
               // 
               // dataGridView2
               // 
               this.dataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
               this.dataGridView2.Dock = System.Windows.Forms.DockStyle.Fill;
               this.dataGridView2.Location = new System.Drawing.Point(3, 3);
               this.dataGridView2.Name = "dataGridView2";
               this.dataGridView2.Size = new System.Drawing.Size(825, 344);
               this.dataGridView2.TabIndex = 0;
               // 
               // about
               // 
               this.about.Controls.Add(this.label4);
               this.about.Controls.Add(this.label3);
               this.about.Controls.Add(this.label2);
               this.about.Controls.Add(this.linkLabel1);
               this.about.Controls.Add(this.label1);
               this.about.Controls.Add(this.pictureBox1);
               this.about.Location = new System.Drawing.Point(4, 27);
               this.about.Name = "about";
               this.about.Padding = new System.Windows.Forms.Padding(3);
               this.about.Size = new System.Drawing.Size(831, 350);
               this.about.TabIndex = 2;
               this.about.Text = "about";
               this.about.UseVisualStyleBackColor = true;
               // 
               // pictureBox1
               // 
               this.pictureBox1.Image = global::reg.dcom.driver.Properties.Resources._3;
               this.pictureBox1.Location = new System.Drawing.Point(26, 49);
               this.pictureBox1.Name = "pictureBox1";
               this.pictureBox1.Size = new System.Drawing.Size(314, 265);
               this.pictureBox1.TabIndex = 0;
               this.pictureBox1.TabStop = false;
               // 
               // statusStrip1
               // 
               this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1});
               this.statusStrip1.Location = new System.Drawing.Point(0, 409);
               this.statusStrip1.Name = "statusStrip1";
               this.statusStrip1.Size = new System.Drawing.Size(839, 22);
               this.statusStrip1.TabIndex = 3;
               this.statusStrip1.Text = "statusStrip1";
               // 
               // toolStripStatusLabel1
               // 
               this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
               this.toolStripStatusLabel1.Size = new System.Drawing.Size(118, 17);
               this.toolStripStatusLabel1.Text = "toolStripStatusLabel1";
               // 
               // label1
               // 
               this.label1.AutoSize = true;
               this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
               this.label1.Location = new System.Drawing.Point(382, 81);
               this.label1.Name = "label1";
               this.label1.Size = new System.Drawing.Size(92, 25);
               this.label1.TabIndex = 1;
               this.label1.Text = "Contact:";
               // 
               // linkLabel1
               // 
               this.linkLabel1.AutoSize = true;
               this.linkLabel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
               this.linkLabel1.Location = new System.Drawing.Point(411, 121);
               this.linkLabel1.Name = "linkLabel1";
               this.linkLabel1.Size = new System.Drawing.Size(397, 25);
               this.linkLabel1.TabIndex = 2;
               this.linkLabel1.TabStop = true;
               this.linkLabel1.Text = "https://www.facebook.com/vinguyet6666";
               this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
               // 
               // label2
               // 
               this.label2.AutoSize = true;
               this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
               this.label2.Location = new System.Drawing.Point(413, 157);
               this.label2.Name = "label2";
               this.label2.Size = new System.Drawing.Size(245, 24);
               this.label2.TabIndex = 3;
               this.label2.Text = "autofarmer1999@gmail.com";
               // 
               // label3
               // 
               this.label3.AutoSize = true;
               this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
               this.label3.Location = new System.Drawing.Point(414, 195);
               this.label3.Name = "label3";
               this.label3.Size = new System.Drawing.Size(176, 25);
               this.label3.TabIndex = 4;
               this.label3.Text = "+84 39-438-6880";
               // 
               // label4
               // 
               this.label4.AutoSize = true;
               this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
               this.label4.Location = new System.Drawing.Point(416, 231);
               this.label4.Name = "label4";
               this.label4.Size = new System.Drawing.Size(100, 20);
               this.label4.TabIndex = 5;
               this.label4.Text = "Version 1.0";
               // 
               // Form1
               // 
               this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
               this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
               this.ClientSize = new System.Drawing.Size(839, 431);
               this.Controls.Add(this.tabControl1);
               this.Controls.Add(this.statusStrip1);
               this.Controls.Add(this.toolStrip1);
               this.Name = "Form1";
               this.Text = "Auto view/sub youtube";
               this.Load += new System.EventHandler(this.Form1_Load);
               this.toolStrip1.ResumeLayout(false);
               this.toolStrip1.PerformLayout();
               this.tabControl1.ResumeLayout(false);
               this.tabPage1.ResumeLayout(false);
               ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
               this.tabPage2.ResumeLayout(false);
               ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).EndInit();
               this.about.ResumeLayout(false);
               this.about.PerformLayout();
               ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
               this.statusStrip1.ResumeLayout(false);
               this.statusStrip1.PerformLayout();
               this.ResumeLayout(false);
               this.PerformLayout();

          }

          #endregion
          private System.Windows.Forms.ToolStrip toolStrip1;
          private System.Windows.Forms.ToolStripButton tbarStartAutoView;
          private System.Windows.Forms.TabControl tabControl1;
          private System.Windows.Forms.TabPage tabPage1;
          private System.Windows.Forms.TabPage tabPage2;
          private System.Windows.Forms.DataGridView dataGridView1;
          private System.Windows.Forms.DataGridView dataGridView2;
          private System.Windows.Forms.ToolStripLabel toolStripLabel1;
          private System.Windows.Forms.StatusStrip statusStrip1;
          private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
          private System.Windows.Forms.TabPage about;
          private System.Windows.Forms.PictureBox pictureBox1;
          private System.Windows.Forms.Label label3;
          private System.Windows.Forms.Label label2;
          private System.Windows.Forms.LinkLabel linkLabel1;
          private System.Windows.Forms.Label label1;
          private System.Windows.Forms.Label label4;
     }
}

