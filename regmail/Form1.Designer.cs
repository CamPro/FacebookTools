namespace regmail
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
               this.tbarRun = new System.Windows.Forms.ToolStripButton();
               this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
               this.toolStrip1.SuspendLayout();
               this.SuspendLayout();
               // 
               // toolStrip1
               // 
               this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tbarRun,
            this.toolStripButton1});
               this.toolStrip1.Location = new System.Drawing.Point(0, 0);
               this.toolStrip1.Name = "toolStrip1";
               this.toolStrip1.Size = new System.Drawing.Size(712, 32);
               this.toolStrip1.TabIndex = 0;
               this.toolStrip1.Text = "toolStrip1";
               // 
               // tbarRun
               // 
               this.tbarRun.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
               this.tbarRun.Image = ((System.Drawing.Image)(resources.GetObject("tbarRun.Image")));
               this.tbarRun.ImageTransparentColor = System.Drawing.Color.Magenta;
               this.tbarRun.Name = "tbarRun";
               this.tbarRun.Size = new System.Drawing.Size(65, 29);
               this.tbarRun.Text = "Run";
               this.tbarRun.Click += new System.EventHandler(this.tbarRun_Click);
               // 
               // toolStripButton1
               // 
               this.toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
               this.toolStripButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton1.Image")));
               this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
               this.toolStripButton1.Name = "toolStripButton1";
               this.toolStripButton1.Size = new System.Drawing.Size(23, 29);
               this.toolStripButton1.Text = "toolStripButton1";
               this.toolStripButton1.Click += new System.EventHandler(this.toolStripButton1_Click);
               // 
               // Form1
               // 
               this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
               this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
               this.ClientSize = new System.Drawing.Size(712, 552);
               this.Controls.Add(this.toolStrip1);
               this.Name = "Form1";
               this.Text = "Form1";
               this.Load += new System.EventHandler(this.Form1_Load);
               this.toolStrip1.ResumeLayout(false);
               this.toolStrip1.PerformLayout();
               this.ResumeLayout(false);
               this.PerformLayout();

          }

          #endregion

          private System.Windows.Forms.ToolStrip toolStrip1;
          private System.Windows.Forms.ToolStripButton tbarRun;
          private System.Windows.Forms.ToolStripButton toolStripButton1;
     }
}

