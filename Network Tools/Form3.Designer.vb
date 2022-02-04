<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form3
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Form3))
        Me.webMail = New System.Windows.Forms.WebBrowser()
        Me.SuspendLayout()
        '
        'webMail
        '
        Me.webMail.Dock = System.Windows.Forms.DockStyle.Fill
        Me.webMail.Location = New System.Drawing.Point(0, 0)
        Me.webMail.MinimumSize = New System.Drawing.Size(20, 20)
        Me.webMail.Name = "webMail"
        Me.webMail.ScriptErrorsSuppressed = True
        Me.webMail.Size = New System.Drawing.Size(970, 724)
        Me.webMail.TabIndex = 0
        '
        'Form3
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(970, 724)
        Me.Controls.Add(Me.webMail)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "Form3"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Message Viewer (By David Sanculane)"
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents webMail As System.Windows.Forms.WebBrowser
End Class
