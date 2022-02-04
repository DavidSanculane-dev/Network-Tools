Public NotInheritable Class AboutBox1
   Private Sub LinkLabel1_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles LinkLabel1.LinkClicked
        Dim webAddress As String = "http://www.vs2005.tk"
        Process.Start(webAddress)
        Me.Close()
    End Sub

    Private Sub AboutBox1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        CloseTimer.Start()
    End Sub

    Private Sub CloseTimer_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CloseTimer.Tick
        Me.Close()
        CloseTimer.Stop()
    End Sub

    Private Sub LinkLabel2_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles LinkLabel2.LinkClicked
        Clipboard.SetText(LinkLabel2.Text)
    End Sub
End Class
