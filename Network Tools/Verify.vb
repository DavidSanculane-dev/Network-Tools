Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Data
Imports System.Drawing
Imports System.Text
Imports System.Windows.Forms
Imports Email.Net.Pop3.Command
Imports Email.Net.Pop3
Imports Email.Net.Common
Imports Email.Net.Common.Configurations

Partial Public Class Demo
    Inherits Form
    Public Sub New()
        InitializeComponent()
        interactionCombo.Items.AddRange(New Object() {EInteractionType.Plain, EInteractionType.SSLPort, EInteractionType.StartTLS})
        interactionCombo.DropDownStyle = ComboBoxStyle.DropDownList
        authentificationCombo.DropDownStyle = ComboBoxStyle.DropDownList
        authentificationCombo.Items.AddRange(New Object() {EAuthenticationType.None, EAuthenticationType.Auto, EAuthenticationType.Plain, EAuthenticationType.CramMD5, EAuthenticationType.DigestMD5, EAuthenticationType.Login})
        interactionCombo.SelectedIndex = 1
        interactionCombo.Refresh()
        authentificationCombo.SelectedIndex = 1
        interactionCombo.Refresh()
    End Sub

    Private Sub close_Click(ByVal sender As Object, ByVal e As EventArgs) Handles closeButton.Click
        Close()
    End Sub

    Private Sub startAuthorization_Click(ByVal sender As Object, ByVal e As EventArgs) Handles startAuthorization.Click
        'Create POP3 client, with parameters needed
        'URL of host to connect to
        target.Host = hostBox.Text
        'TCP port for connection
        target.Port = CUShort(Math.Truncate(portNum.Value))
        'Username to login to the POP3 server
        target.Username = loginBox.Text
        'Password to login to the POP3 server
        target.Password = passwordBox.Text
        target.SSLInteractionType = CType(interactionCombo.SelectedItem, EInteractionType)
        target.AuthenticationType = CType(authentificationCombo.SelectedItem, EAuthenticationType)
        'Login to the server
        Dim response As Pop3Response = target.Login()
        If response.Type = EPop3ResponseType.OK Then
            MessageBox.Show("This Email is CORRECT.", "Email Verifier (David Sanculane)", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Me.Close()
        Else
            MessageBox.Show("This Email is INCORRECT.", "Email Verifier (David Sanculane)", MessageBoxButtons.OK, MessageBoxIcon.Stop)
            Me.Close()
        End If
        'Logout from the server
        target.Logout()
    End Sub

    Private Sub Demo_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        If Form1.pvdr.Text = "smtp.aol.com" Then
            hostBox.Text = "pop.aol.com"
        End If
        If Form1.pvdr.Text = "smtp.gmail.com" Then
            hostBox.Text = "pop.gmail.com"
        End If
        If Form1.pvdr.Text = "smtp.live.com" Then
            hostBox.Text = "pop3.live.com"
        End If
        If Form1.pvdr.Text = "smtp.mail.yahoo.com" Then
            hostBox.Text = "pop.mail.yahoo.com"
        End If
        If Form1.pvdr.Text = "smtp.gmx.com" Then
            hostBox.Text = "pop.gmx.com"
        End If
        If Form1.pvdr.Text = "smtp.mail.com" Then
            hostBox.Text = "pop.mail.com"
        End If
        passwordbox.Text = Form1.emailPass.Text
        loginbox.Text = Form1.from.Text + Form1.from_pt2.Text
    End Sub
End Class
