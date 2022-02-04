' This form helps you to save E-mail accounts and load them easily.
Public Class _Settings
    Public MySettings(,) As String
    Dim intSettings1 As Integer
    Private Sub _Settings_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Combo holds profiles names
        cboprof.Items.Clear()
        If cboprof.Text = "" Then
            Btn_load.Enabled = False
            Btn_del.Enabled = False
            btn_save.Enabled = False
            Btn_edit.Enabled = False
        End If
    End Sub
    Private Sub btn_save_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_save.Click
        SaveSetting("EmailProfiles", cboprof.Text, "Combobox2", Me.ComboBox2.Text)
        SaveSetting("EmailProfiles", cboprof.Text, "textbox5", txtbox1005.Text)
        SaveSetting("EmailProfiles", cboprof.Text, "Combobox3", Me.ComboBox3.Text)
        SaveSetting("EmailProfiles", "ProfileName", cboprof.Text, cboprof.Text)
        SaveSetting("EmailProfiles", cboprof.Text, "HostName", serverbox.Text)
        SaveSetting("EmailProfiles", cboprof.Text, "UserName", email.Text)
        SaveSetting("EmailProfiles", cboprof.Text, "PassWord", password.Text)
        MsgBox("Saved Successfully", MsgBoxStyle.Information, "Saving....")
        btn_save.Enabled = False
        cboprof.DropDownStyle = ComboBoxStyle.DropDownList
        serverbox.Enabled = False
        ComboBox2.Enabled = False
        ComboBox3.Enabled = False
        email.ReadOnly = True
        password.ReadOnly = True
        Btn_edit.Enabled = True
    End Sub
    Private Sub CboProf_DropDown(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboprof.DropDown
        On Error Resume Next
        MySettings = GetAllSettings("EmailProfiles", "ProfileName")
        cboprof.Items.Clear()
        For Me.intSettings1 = LBound(MySettings, 1) To UBound(MySettings, 1)
            cboprof.Items.Add(MySettings(intSettings1, 1))
        Next intSettings1
    End Sub
    Private Sub CboProf_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboprof.SelectedIndexChanged
        serverbox.Text = GetSetting("EmailProfiles", cboprof.Text, "HostName")
        email.Text = GetSetting("EmailProfiles", cboprof.Text, "username")
        password.Text = GetSetting("EmailProfiles", cboprof.Text, "password")
        SaveSetting("EmailProfiles", cboprof.Text, "Combobox2", ComboBox2.Text)
        SaveSetting("EmailProfiles", cboprof.Text, "Combobox3", ComboBox3.Text)
        Label10.Text = cboprof.Text
        ComboBox2.Text = GetSetting("EmailProfiles", cboprof.Text, "Combobox2", ComboBox2.SelectedItem)
        ComboBox3.Text = GetSetting("EmailProfiles", cboprof.Text, "Combobox3", ComboBox3.SelectedItem)
        txtbox1005.Text = GetSetting("EmailProfiles", cboprof.Text, "textbox5", txtbox1005.Text)
    End Sub
    ' The following code copies all written information in the settings form and loads them
    ' in the main form (Form2)
    Private Sub btn_load_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Btn_load.Click
        If String.IsNullOrEmpty(cboprof.Text) Or cboprof.Text = "" Then Exit Sub
        SaveSetting("EmailProfiles", "Selected", "Name", cboprof.Text)
        Form1.pvdr.SelectedItem = Me.serverbox.Text
        Form1.from.Text = Me.email.Text
        Form1.emailPass.Text = Me.password.Text
        Form1.outlook.SelectedItem = Me.ComboBox2.SelectedItem
        Form1.gmx.SelectedItem = Me.ComboBox3.SelectedItem
        Form1.from_pt2.Text = Me.txtbox1005.Text
        Me.Dispose()
    End Sub
    Private Sub btn_del_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Btn_del.Click
        If String.IsNullOrEmpty(cboprof.Text) Or cboprof.Text = "" Then Exit Sub
        DeleteSetting("EmailProfiles", cboprof.Text)
        DeleteSetting("EmailProfiles", "ProfileName", cboprof.Text)
        For Each CNTL As Control In Me.Controls
            If TypeOf CNTL Is TextBox Then
                CNTL.Text = ""
            End If
        Next
        MsgBox("Deleted Successfully", MsgBoxStyle.Information, "deleting....")
    End Sub
    Private Sub serverbox_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles serverbox.SelectedIndexChanged
        If serverbox.SelectedItem = "smtp.gmail.com" Then
            txtbox1005.Text = "@gmail.com"
            ComboBox2.Hide()
            ComboBox3.Hide()
            Label1007.Hide()
        End If
        If serverbox.SelectedItem = "smtp.live.com" Then
            ComboBox2.Show()
            If btn_save.Enabled = True Then
                ComboBox2.Enabled = True
            Else
                ComboBox2.Enabled = False
            End If
            ComboBox3.Hide()
            txtbox1005.Clear()
            Label1007.Show()
            txtbox1005.Text = "Select Provider"
        End If
        If serverbox.SelectedItem = "smtp.live.com" And ComboBox2.Text = "Hotmail" Then
            txtbox1005.Text = "@hotmail.com"
        End If
        If serverbox.SelectedItem = "smtp.live.com" And ComboBox2.Text = "Live" Then
            txtbox1005.Text = "@live.com"
        End If
        If serverbox.SelectedItem = "smtp.live.com" And ComboBox2.Text = "Outlook" Then
            txtbox1005.Text = "@outlook.com"
        End If
        If serverbox.SelectedItem = "smtp.live.com" And ComboBox2.Text = "Msn" Then
            txtbox1005.Text = "@msn.com"
        End If
        If serverbox.SelectedItem = "smtp.aol.com" Then
            txtbox1005.Text = "@aol.com"
            ComboBox2.Hide()
            ComboBox3.Hide()
            Label1007.Hide()
        End If
        If serverbox.SelectedItem = "smtp.mail.yahoo.com" Then
            ComboBox2.Hide()
            ComboBox3.Hide()
            Label1007.Hide()
            txtbox1005.Text = "@yahoo.com"
        End If
        If serverbox.SelectedItem = "smtp.gmx.com" Then
            txtbox1005.Clear()
            ComboBox2.Hide()
            ComboBox3.Show()
            If btn_save.Enabled = True Then
                ComboBox3.Enabled = True
            Else
                ComboBox3.Enabled = False
            End If
            Label1007.Show()
            txtbox1005.Text = "Select Provider"
        End If
        If serverbox.SelectedItem = "smtp.gmx.com" And ComboBox3.Text = "gmx.com" Then
            txtbox1005.Text = "@gmx.com"
        End If
        If serverbox.SelectedItem = "smtp.gmx.com" And ComboBox3.Text = "gmx.us" Then
            txtbox1005.Text = "@gmx.us"
        End If
        If serverbox.SelectedItem = "smtp.mail.com" Then
            txtbox1005.Text = "@mail.com"
            ComboBox2.Hide()
            ComboBox3.Hide()
            Label1007.Hide()
        End If
    End Sub
    Private Sub ComboBox3_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBox3.SelectedIndexChanged
        If ComboBox3.Text = "gmx.com" Then
            txtbox1005.Text = "@gmx.com"
        End If
        If ComboBox3.Text = "gmx.us" Then
            txtbox1005.Text = "@gmx.us"
        End If
        If serverbox.SelectedItem = "smtp.gmx.com" And ComboBox3.Text = "gmx.com" Then
            txtbox1005.Text = "@gmx.com"
        End If
        If serverbox.SelectedItem = "smtp.gmx.com" And ComboBox3.Text = "gmx.us" Then
            txtbox1005.Text = "gmx.us"
        End If
    End Sub
    Private Sub ComboBox2_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBox2.SelectedIndexChanged
        If serverbox.SelectedItem = "smtp.live.com" And ComboBox2.Text = "Hotmail" Then
            txtbox1005.Text = "@hotmail.com"
        End If
        If serverbox.SelectedItem = "smtp.live.com" And ComboBox2.Text = "Live" Then
            txtbox1005.Text = "@live.com"
        End If
        If serverbox.SelectedItem = "smtp.live.com" And ComboBox2.Text = "Outlook" Then
            txtbox1005.Text = "@outlook.com"
        End If
        If serverbox.SelectedItem = "smtp.live.com" And ComboBox2.Text = "Msn" Then
            txtbox1005.Text = "@msn.com"
        End If
        If ComboBox2.SelectedItem = "Hotmail" Then
            txtbox1005.Text = "@hotmail.com"
        End If
        If ComboBox2.SelectedItem = "Live" Then
            txtbox1005.Text = "@live.com"
        End If
        If ComboBox2.SelectedItem = "Outlook" Then
            txtbox1005.Text = "@outlook.com"
        End If
        If ComboBox2.SelectedItem = "Msn" Then
            txtbox1005.Text = "@msn.com"
        End If
    End Sub
    Private Sub TextBox5_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtbox1005.TextChanged
        If txtbox1005.Text = "@hotmail.com" Then
            ComboBox2.Text = "Hotmail"
        End If
        If txtbox1005.Text = "@live.com" Then
            ComboBox2.Text = "Live"
        End If
        If txtbox1005.Text = "@msn.com" Then
            ComboBox2.Text = "Msn"
        End If
    End Sub
    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        btn_save.Enabled = True
        Btn_edit.Enabled = False
        Btn_load.Enabled = False
        Btn_del.Enabled = False
        cboprof.DropDownStyle = ComboBoxStyle.Simple
        cboprof.Text = "New Profile"
        serverbox.Enabled = True
        serverbox.Text = "Select"
        ComboBox2.Enabled = True
        ComboBox3.Enabled = True
        Label10.Text = "New Profile"
        email.ReadOnly = False
        email.Clear()
        password.ReadOnly = False
        password.Clear()
    End Sub
    Private Sub cboprof_SelectedIndexchanged_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboprof.SelectedIndexChanged
        If cboprof.Text = "" Then
            Btn_load.Enabled = False
            Btn_del.Enabled = False
            Btn_edit.Enabled = False
        Else
            Btn_load.Enabled = True
            Btn_del.Enabled = True
            Btn_edit.Enabled = True
        End If
    End Sub
    Private Sub cboprof_textchanged_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboprof.TextChanged
        Label10.Text = cboprof.Text
        If cboprof.Text = "" Then
            Btn_edit.Enabled = False
            Btn_load.Enabled = False
            Btn_del.Enabled = False
        Else
            Btn_edit.Enabled = True
            Btn_load.Enabled = True
            Btn_del.Enabled = True
        End If
    End Sub
    Private Sub Btn_edit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Btn_edit.Click
        btn_save.Enabled = True
        Btn_edit.Enabled = False
        cboprof.DropDownStyle = ComboBoxStyle.DropDown
        serverbox.Enabled = True
        ComboBox2.Enabled = True
        ComboBox3.Enabled = True
        email.ReadOnly = False
        password.ReadOnly = False
    End Sub
End Class