Imports System.ComponentModel
Imports System.IO
Imports System.Net
Imports System.Text
Imports System.Text.RegularExpressions
Imports System.Threading
Imports System
Imports System.Diagnostics
Imports System.Reflection
Imports System.Runtime.CompilerServices
Imports System.Runtime.InteropServices
Imports System.Runtime.Versioning
Imports System.Json
Imports Microsoft.VisualBasic.CompilerServices
Public Class Form1
    Private currentThread As List(Of Thread) = New List(Of Thread)()
    Dim namefb As String = """text"":""(.*?)"""
    Dim UIDFB As String = """uid"":""(.*?)"""
    Dim cookiefb As String
    Dim linkhuongdan As String
    Dim Thread1 As System.Threading.Thread
    Dim StopThread As Boolean = False
    Sub New()

        InitializeComponent()
        Control.CheckForIllegalCrossThreadCalls = False

        'source code pub on github : https://github.com/msc0d3
        'chúc ai phát triển lại mã nguôn này phát triển tốt nhé <3
        '31/08/2019
        ' Add any initialization after the InitializeComponent() call.

    End Sub
    Private Sub Brnmolisst_Click(sender As Object, e As EventArgs) Handles brnmolisst.Click
        On Error Resume Next
        Dim openfile As New OpenFileDialog
        openfile.Title = "Text |*.txt"
        openfile.ShowDialog()
        Dim txtline() As String = IO.File.ReadAllLines(openfile.FileName)
        ListBox1.Items.AddRange(txtline)
        lballmail.Text = ListBox1.Items.Count
        'ListBox1.SelectedIndex += 1
        If checkboxid.Checked = True Then
            My.Settings.idtrang = txtid.Text
            My.Settings.Save()
        Else

        End If
        If checkboxck.Checked = True Then
            My.Settings.cookie = txtcookie.Text
            My.Settings.Save()
        Else

        End If
    End Sub

    Private Sub Btnxoalist_Click(sender As Object, e As EventArgs) Handles btnxoalist.Click
        ListBox1.Items.Clear()
    End Sub

    Private Sub Btnstart_Click(sender As Object, e As EventArgs) Handles btnstart.Click
        Try
            StopThread = False
            Thread1 = New System.Threading.Thread(AddressOf GETID)
            Thread1.Start()
        Catch ex As Exception

        End Try
    End Sub
    Private Sub GETID()
        Dim ketqua As String
        'Application.DoEvents()
        While True
            Try
                ListBox1.SelectedIndex += 1
                Dim request As HttpWebRequest = DirectCast(WebRequest.Create("https://m.facebook.com/presma/user_search_typeahead/?search_mode=ANYONE_EXCEPT_VERIFIED_ACCOUNT&av=" + txtid.Text + "&eav=&q=" + ListBox1.SelectedItem + "&session_id=undefined&m_sess=&fb_dtsg_ag="), HttpWebRequest)
                request.Method = "GET"
                request.Headers.Add("Cookie", cookiefb)
                request.UserAgent = "Mozilla/5.0 (Windows NT 6.1; rv:47.0) Gecko/20100101 Firefox/47.0"
                request.ContentType = "application/x-www-form-urlencoded"
                Dim reader As New StreamReader(request.GetResponse.GetResponseStream, Encoding.UTF8)
                Dim input As String = reader.ReadToEnd
                ketqua = input
                Dim Name As Match = Regex.Match(input, namefb, RegexOptions.IgnoreCase)
                Dim ID As Match = Regex.Match(input, UIDFB, RegexOptions.IgnoreCase)
                Dim namfb As String
                Dim idfb As String
                namfb = Name.Groups(1).Value & " " & Name.Groups(2).Value
                idfb = ID.Groups(1).Value & " " & ID.Groups(2).Value
                If idfb = " " Then
                    idfb = "> có lỗi khi get id từ mail này !"
                    txtmailerror.AppendText(ListBox1.SelectedItem + " | " + "> có lỗi khi get uid này" + " | " + namfb + vbCrLf)
                    lbmailerror.Text = txtmailerror.Lines.Count

                Else
                    txtmailvauid.AppendText(ListBox1.SelectedItem + " | " + idfb + " | " + namfb + vbCrLf)
                    lbmailok.Text = txtmailvauid.Lines.Count
                End If
            Catch ex As Exception
                Try
                    Thread1.Abort()
                Catch ex1 As Exception
                End Try


            End Try
        End While
    End Sub


    Private Sub Btnstop_Click(sender As Object, e As EventArgs) Handles btnstop.Click
        StopThread = True
        Try
            Thread1.Abort()
        Catch ex As Exception
        End Try
    End Sub

    Private Sub Btncopy_Click(sender As Object, e As EventArgs) Handles btncopy.Click
        My.Computer.Clipboard.SetText(txtmailvauid.Text)
        MsgBox("ĐÃ COPY :D")
    End Sub

    Private Sub Btnxoalistkq_Click(sender As Object, e As EventArgs) Handles btnxoalistkq.Click
        txtmailvauid.Text = ""
    End Sub

    Private Sub Btnreset_Click(sender As Object, e As EventArgs) Handles btnreset.Click
        txtmailvauid.Text = ""
        ListBox1.Items.Clear()
        lballmail.Text = "000"
        lbmailerror.Text = "000"
        lbmailok.Text = "000"
    End Sub

    Private Sub Txtcookie_TextChanged(sender As Object, e As EventArgs) Handles txtcookie.TextChanged
        cookiefb = txtcookie.Text
    End Sub

    Private Sub LinkLabel1_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel1.LinkClicked
        Process.Start(TextBox1.Text)
    End Sub

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        txtid.Text = My.Settings.idtrang
        txtcookie.Text = My.Settings.cookie
        Try
            '------------------------------------ check key----------------------
            Dim accounts = New WebClient().DownloadString("https://pastebin.com/raw/79k6N4vc").Split(vbLf)
            TextBox1.Text = accounts(New Random().[Next](0, accounts.Length))

            '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''


        Catch ex As Exception

        End Try
    End Sub

    Private Sub Form1_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        ProjectData.EndApp()
    End Sub

    Private Sub Btnabout_Click(sender As Object, e As EventArgs) Handles btnabout.Click
        MsgBox("Tool code By Nguyễn Đắc Tài
web : https://tienichmmo.net
Facebook : fb.com/tai.nguyendac16201
groups : fb.com/groups/tienichmmo")
    End Sub

    Private Sub ListBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ListBox1.SelectedIndexChanged
        If ListBox1.SelectedIndex = lballmail.Text Then
            For Each item As Thread In Me.currentThread
                item.Abort()
            Next
        End If
    End Sub
End Class
