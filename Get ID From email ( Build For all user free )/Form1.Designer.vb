<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form1
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Form1))
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txtcookie = New System.Windows.Forms.TextBox()
        Me.ListBox1 = New System.Windows.Forms.ListBox()
        Me.brnmolisst = New System.Windows.Forms.Button()
        Me.btnxoalist = New System.Windows.Forms.Button()
        Me.btnstart = New System.Windows.Forms.Button()
        Me.txtmailvauid = New System.Windows.Forms.TextBox()
        Me.btncopy = New System.Windows.Forms.Button()
        Me.btnxoalistkq = New System.Windows.Forms.Button()
        Me.btnreset = New System.Windows.Forms.Button()
        Me.btnabout = New System.Windows.Forms.Button()
        Me.btnstop = New System.Windows.Forms.Button()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.lbmailok = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.lbmailerror = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.lballmail = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.txtthread = New System.Windows.Forms.TextBox()
        Me.txtdelay = New System.Windows.Forms.TextBox()
        Me.txtmailerror = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.txtid = New System.Windows.Forms.TextBox()
        Me.LinkLabel1 = New System.Windows.Forms.LinkLabel()
        Me.TextBox1 = New System.Windows.Forms.TextBox()
        Me.checkboxid = New System.Windows.Forms.CheckBox()
        Me.checkboxck = New System.Windows.Forms.CheckBox()
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.SystemColors.Control
        Me.Label1.Location = New System.Drawing.Point(14, 52)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(402, 15)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "nhập cookie trung gian vào đây ( cookie có page nha, page ảo cũng được )"
        '
        'txtcookie
        '
        Me.txtcookie.Location = New System.Drawing.Point(17, 70)
        Me.txtcookie.Name = "txtcookie"
        Me.txtcookie.Size = New System.Drawing.Size(423, 20)
        Me.txtcookie.TabIndex = 1
        '
        'ListBox1
        '
        Me.ListBox1.BackColor = System.Drawing.Color.FromArgb(CType(CType(46, Byte), Integer), CType(CType(50, Byte), Integer), CType(CType(51, Byte), Integer))
        Me.ListBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.ListBox1.ForeColor = System.Drawing.Color.White
        Me.ListBox1.FormattingEnabled = True
        Me.ListBox1.Location = New System.Drawing.Point(17, 96)
        Me.ListBox1.Name = "ListBox1"
        Me.ListBox1.Size = New System.Drawing.Size(510, 93)
        Me.ListBox1.TabIndex = 2
        '
        'brnmolisst
        '
        Me.brnmolisst.BackColor = System.Drawing.Color.FromArgb(CType(CType(38, Byte), Integer), CType(CType(50, Byte), Integer), CType(CType(74, Byte), Integer))
        Me.brnmolisst.FlatAppearance.BorderSize = 0
        Me.brnmolisst.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.brnmolisst.ForeColor = System.Drawing.SystemColors.Control
        Me.brnmolisst.Location = New System.Drawing.Point(17, 195)
        Me.brnmolisst.Name = "brnmolisst"
        Me.brnmolisst.Size = New System.Drawing.Size(75, 23)
        Me.brnmolisst.TabIndex = 3
        Me.brnmolisst.Text = "mở list email"
        Me.brnmolisst.UseVisualStyleBackColor = False
        '
        'btnxoalist
        '
        Me.btnxoalist.BackColor = System.Drawing.Color.FromArgb(CType(CType(38, Byte), Integer), CType(CType(50, Byte), Integer), CType(CType(74, Byte), Integer))
        Me.btnxoalist.FlatAppearance.BorderSize = 0
        Me.btnxoalist.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnxoalist.ForeColor = System.Drawing.SystemColors.Control
        Me.btnxoalist.Location = New System.Drawing.Point(98, 195)
        Me.btnxoalist.Name = "btnxoalist"
        Me.btnxoalist.Size = New System.Drawing.Size(75, 23)
        Me.btnxoalist.TabIndex = 4
        Me.btnxoalist.Text = "xóa list email"
        Me.btnxoalist.UseVisualStyleBackColor = False
        '
        'btnstart
        '
        Me.btnstart.BackColor = System.Drawing.Color.FromArgb(CType(CType(38, Byte), Integer), CType(CType(50, Byte), Integer), CType(CType(74, Byte), Integer))
        Me.btnstart.FlatAppearance.BorderSize = 0
        Me.btnstart.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnstart.ForeColor = System.Drawing.SystemColors.Control
        Me.btnstart.Location = New System.Drawing.Point(179, 195)
        Me.btnstart.Name = "btnstart"
        Me.btnstart.Size = New System.Drawing.Size(173, 23)
        Me.btnstart.TabIndex = 5
        Me.btnstart.Text = "Start"
        Me.btnstart.UseVisualStyleBackColor = False
        '
        'txtmailvauid
        '
        Me.txtmailvauid.BackColor = System.Drawing.Color.FromArgb(CType(CType(36, Byte), Integer), CType(CType(39, Byte), Integer), CType(CType(38, Byte), Integer))
        Me.txtmailvauid.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtmailvauid.ForeColor = System.Drawing.SystemColors.Info
        Me.txtmailvauid.Location = New System.Drawing.Point(17, 228)
        Me.txtmailvauid.Multiline = True
        Me.txtmailvauid.Name = "txtmailvauid"
        Me.txtmailvauid.Size = New System.Drawing.Size(262, 132)
        Me.txtmailvauid.TabIndex = 6
        '
        'btncopy
        '
        Me.btncopy.BackColor = System.Drawing.Color.FromArgb(CType(CType(38, Byte), Integer), CType(CType(50, Byte), Integer), CType(CType(74, Byte), Integer))
        Me.btncopy.FlatAppearance.BorderSize = 0
        Me.btncopy.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btncopy.ForeColor = System.Drawing.SystemColors.Control
        Me.btncopy.Location = New System.Drawing.Point(17, 366)
        Me.btncopy.Name = "btncopy"
        Me.btncopy.Size = New System.Drawing.Size(114, 23)
        Me.btncopy.TabIndex = 7
        Me.btncopy.Text = "copy list email + uid"
        Me.btncopy.UseVisualStyleBackColor = False
        '
        'btnxoalistkq
        '
        Me.btnxoalistkq.BackColor = System.Drawing.Color.FromArgb(CType(CType(38, Byte), Integer), CType(CType(50, Byte), Integer), CType(CType(74, Byte), Integer))
        Me.btnxoalistkq.FlatAppearance.BorderSize = 0
        Me.btnxoalistkq.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnxoalistkq.ForeColor = System.Drawing.SystemColors.Control
        Me.btnxoalistkq.Location = New System.Drawing.Point(137, 366)
        Me.btnxoalistkq.Name = "btnxoalistkq"
        Me.btnxoalistkq.Size = New System.Drawing.Size(114, 23)
        Me.btnxoalistkq.TabIndex = 8
        Me.btnxoalistkq.Text = "xóa list email + uid"
        Me.btnxoalistkq.UseVisualStyleBackColor = False
        '
        'btnreset
        '
        Me.btnreset.BackColor = System.Drawing.Color.FromArgb(CType(CType(38, Byte), Integer), CType(CType(50, Byte), Integer), CType(CType(74, Byte), Integer))
        Me.btnreset.FlatAppearance.BorderSize = 0
        Me.btnreset.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnreset.ForeColor = System.Drawing.SystemColors.Control
        Me.btnreset.Location = New System.Drawing.Point(257, 366)
        Me.btnreset.Name = "btnreset"
        Me.btnreset.Size = New System.Drawing.Size(114, 23)
        Me.btnreset.TabIndex = 9
        Me.btnreset.Text = "reset all"
        Me.btnreset.UseVisualStyleBackColor = False
        '
        'btnabout
        '
        Me.btnabout.BackColor = System.Drawing.Color.FromArgb(CType(CType(38, Byte), Integer), CType(CType(50, Byte), Integer), CType(CType(74, Byte), Integer))
        Me.btnabout.FlatAppearance.BorderSize = 0
        Me.btnabout.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnabout.ForeColor = System.Drawing.SystemColors.Control
        Me.btnabout.Location = New System.Drawing.Point(377, 366)
        Me.btnabout.Name = "btnabout"
        Me.btnabout.Size = New System.Drawing.Size(150, 23)
        Me.btnabout.TabIndex = 10
        Me.btnabout.Text = "about tools"
        Me.btnabout.UseVisualStyleBackColor = False
        '
        'btnstop
        '
        Me.btnstop.BackColor = System.Drawing.Color.FromArgb(CType(CType(38, Byte), Integer), CType(CType(50, Byte), Integer), CType(CType(74, Byte), Integer))
        Me.btnstop.FlatAppearance.BorderSize = 0
        Me.btnstop.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnstop.ForeColor = System.Drawing.SystemColors.Control
        Me.btnstop.Location = New System.Drawing.Point(358, 195)
        Me.btnstop.Name = "btnstop"
        Me.btnstop.Size = New System.Drawing.Size(169, 23)
        Me.btnstop.TabIndex = 11
        Me.btnstop.Text = "Stop"
        Me.btnstop.UseVisualStyleBackColor = False
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(135, Byte), Integer), CType(CType(83, Byte), Integer))
        Me.Panel1.Controls.Add(Me.lbmailok)
        Me.Panel1.Controls.Add(Me.Label5)
        Me.Panel1.Controls.Add(Me.lbmailerror)
        Me.Panel1.Controls.Add(Me.Label4)
        Me.Panel1.Controls.Add(Me.lballmail)
        Me.Panel1.Controls.Add(Me.Label2)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel1.Location = New System.Drawing.Point(0, 399)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(541, 25)
        Me.Panel1.TabIndex = 12
        '
        'lbmailok
        '
        Me.lbmailok.AutoSize = True
        Me.lbmailok.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!)
        Me.lbmailok.ForeColor = System.Drawing.SystemColors.Control
        Me.lbmailok.Location = New System.Drawing.Point(394, 4)
        Me.lbmailok.Name = "lbmailok"
        Me.lbmailok.Size = New System.Drawing.Size(32, 17)
        Me.lbmailok.TabIndex = 5
        Me.lbmailok.Text = "000"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!)
        Me.Label5.ForeColor = System.Drawing.SystemColors.Control
        Me.Label5.Location = New System.Drawing.Point(313, 4)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(74, 17)
        Me.Label5.TabIndex = 4
        Me.Label5.Text = "Email OK :"
        '
        'lbmailerror
        '
        Me.lbmailerror.AutoSize = True
        Me.lbmailerror.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!)
        Me.lbmailerror.ForeColor = System.Drawing.SystemColors.Control
        Me.lbmailerror.Location = New System.Drawing.Point(243, 4)
        Me.lbmailerror.Name = "lbmailerror"
        Me.lbmailerror.Size = New System.Drawing.Size(32, 17)
        Me.lbmailerror.TabIndex = 3
        Me.lbmailerror.Text = "000"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!)
        Me.Label4.ForeColor = System.Drawing.SystemColors.Control
        Me.Label4.Location = New System.Drawing.Point(158, 4)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(85, 17)
        Me.Label4.TabIndex = 2
        Me.Label4.Text = "Email error :"
        '
        'lballmail
        '
        Me.lballmail.AutoSize = True
        Me.lballmail.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!)
        Me.lballmail.ForeColor = System.Drawing.SystemColors.Control
        Me.lballmail.Location = New System.Drawing.Point(86, 4)
        Me.lballmail.Name = "lballmail"
        Me.lballmail.Size = New System.Drawing.Size(32, 17)
        Me.lballmail.TabIndex = 1
        Me.lballmail.Text = "000"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!)
        Me.Label2.ForeColor = System.Drawing.SystemColors.Control
        Me.Label2.Location = New System.Drawing.Point(3, 4)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(85, 17)
        Me.Label2.TabIndex = 0
        Me.Label2.Text = "Total email :"
        '
        'txtthread
        '
        Me.txtthread.Location = New System.Drawing.Point(567, 122)
        Me.txtthread.Name = "txtthread"
        Me.txtthread.Size = New System.Drawing.Size(98, 20)
        Me.txtthread.TabIndex = 13
        Me.txtthread.Text = "1"
        '
        'txtdelay
        '
        Me.txtdelay.Location = New System.Drawing.Point(567, 161)
        Me.txtdelay.Name = "txtdelay"
        Me.txtdelay.Size = New System.Drawing.Size(98, 20)
        Me.txtdelay.TabIndex = 14
        Me.txtdelay.Text = "1"
        '
        'txtmailerror
        '
        Me.txtmailerror.BackColor = System.Drawing.Color.FromArgb(CType(CType(36, Byte), Integer), CType(CType(39, Byte), Integer), CType(CType(38, Byte), Integer))
        Me.txtmailerror.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtmailerror.ForeColor = System.Drawing.Color.DeepPink
        Me.txtmailerror.Location = New System.Drawing.Point(285, 228)
        Me.txtmailerror.Multiline = True
        Me.txtmailerror.Name = "txtmailerror"
        Me.txtmailerror.Size = New System.Drawing.Size(242, 132)
        Me.txtmailerror.TabIndex = 15
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.ForeColor = System.Drawing.SystemColors.Control
        Me.Label3.Location = New System.Drawing.Point(14, 9)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(294, 15)
        Me.Label3.TabIndex = 16
        Me.Label3.Text = "nhập id trang ( id trang mà cookie đó làm admin nha )"
        '
        'txtid
        '
        Me.txtid.Location = New System.Drawing.Point(17, 27)
        Me.txtid.Name = "txtid"
        Me.txtid.Size = New System.Drawing.Size(423, 20)
        Me.txtid.TabIndex = 17
        '
        'LinkLabel1
        '
        Me.LinkLabel1.AutoSize = True
        Me.LinkLabel1.LinkColor = System.Drawing.Color.Aqua
        Me.LinkLabel1.Location = New System.Drawing.Point(440, 9)
        Me.LinkLabel1.Name = "LinkLabel1"
        Me.LinkLabel1.Size = New System.Drawing.Size(87, 13)
        Me.LinkLabel1.TabIndex = 18
        Me.LinkLabel1.TabStop = True
        Me.LinkLabel1.Text = "How to use tool?"
        '
        'TextBox1
        '
        Me.TextBox1.Location = New System.Drawing.Point(567, 207)
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.Size = New System.Drawing.Size(98, 20)
        Me.TextBox1.TabIndex = 19
        Me.TextBox1.Text = "1"
        '
        'checkboxid
        '
        Me.checkboxid.AutoSize = True
        Me.checkboxid.ForeColor = System.Drawing.SystemColors.Control
        Me.checkboxid.Location = New System.Drawing.Point(446, 30)
        Me.checkboxid.Name = "checkboxid"
        Me.checkboxid.Size = New System.Drawing.Size(74, 17)
        Me.checkboxid.TabIndex = 20
        Me.checkboxid.Text = "Save ID ?"
        Me.checkboxid.UseVisualStyleBackColor = True
        '
        'checkboxck
        '
        Me.checkboxck.AutoSize = True
        Me.checkboxck.ForeColor = System.Drawing.SystemColors.Control
        Me.checkboxck.Location = New System.Drawing.Point(446, 72)
        Me.checkboxck.Name = "checkboxck"
        Me.checkboxck.Size = New System.Drawing.Size(92, 17)
        Me.checkboxck.TabIndex = 21
        Me.checkboxck.Text = "Save cookie?"
        Me.checkboxck.UseVisualStyleBackColor = True
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(36, Byte), Integer), CType(CType(39, Byte), Integer), CType(CType(38, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(541, 424)
        Me.Controls.Add(Me.checkboxck)
        Me.Controls.Add(Me.checkboxid)
        Me.Controls.Add(Me.TextBox1)
        Me.Controls.Add(Me.LinkLabel1)
        Me.Controls.Add(Me.txtid)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.txtmailerror)
        Me.Controls.Add(Me.txtdelay)
        Me.Controls.Add(Me.txtthread)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.btnstop)
        Me.Controls.Add(Me.btnabout)
        Me.Controls.Add(Me.btnreset)
        Me.Controls.Add(Me.btnxoalistkq)
        Me.Controls.Add(Me.btncopy)
        Me.Controls.Add(Me.txtmailvauid)
        Me.Controls.Add(Me.btnstart)
        Me.Controls.Add(Me.btnxoalist)
        Me.Controls.Add(Me.brnmolisst)
        Me.Controls.Add(Me.ListBox1)
        Me.Controls.Add(Me.txtcookie)
        Me.Controls.Add(Me.Label1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.Name = "Form1"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Tool lấy ID từ mail ( bản free ) By Nguyễn Đắc Tài"
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Label1 As Label
    Friend WithEvents txtcookie As TextBox
    Friend WithEvents ListBox1 As ListBox
    Friend WithEvents brnmolisst As Button
    Friend WithEvents btnxoalist As Button
    Friend WithEvents btnstart As Button
    Friend WithEvents txtmailvauid As TextBox
    Friend WithEvents btncopy As Button
    Friend WithEvents btnxoalistkq As Button
    Friend WithEvents btnreset As Button
    Friend WithEvents btnabout As Button
    Friend WithEvents btnstop As Button
    Friend WithEvents Panel1 As Panel
    Friend WithEvents lbmailok As Label
    Friend WithEvents Label5 As Label
    Friend WithEvents lbmailerror As Label
    Friend WithEvents Label4 As Label
    Friend WithEvents lballmail As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents txtthread As TextBox
    Friend WithEvents txtdelay As TextBox
    Friend WithEvents txtmailerror As TextBox
    Friend WithEvents Label3 As Label
    Friend WithEvents txtid As TextBox
    Friend WithEvents LinkLabel1 As LinkLabel
    Friend WithEvents TextBox1 As TextBox
    Friend WithEvents checkboxid As CheckBox
    Friend WithEvents checkboxck As CheckBox
End Class
