<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Help
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Help))
        Me.understand_btn = New System.Windows.Forms.Button()
        Me.instructions = New System.Windows.Forms.Label()
        Me.title_follow = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'understand_btn
        '
        Me.understand_btn.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.understand_btn.Location = New System.Drawing.Point(228, 311)
        Me.understand_btn.Name = "understand_btn"
        Me.understand_btn.Size = New System.Drawing.Size(169, 23)
        Me.understand_btn.TabIndex = 8
        Me.understand_btn.Text = "I Understand The Rules"
        Me.understand_btn.UseVisualStyleBackColor = True
        '
        'instructions
        '
        Me.instructions.AutoSize = True
        Me.instructions.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.instructions.Location = New System.Drawing.Point(11, 49)
        Me.instructions.Name = "instructions"
        Me.instructions.Size = New System.Drawing.Size(260, 285)
        Me.instructions.TabIndex = 7
        Me.instructions.Text = resources.GetString("instructions.Text")
        '
        'title_follow
        '
        Me.title_follow.AutoSize = True
        Me.title_follow.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.title_follow.Location = New System.Drawing.Point(9, 13)
        Me.title_follow.Name = "title_follow"
        Me.title_follow.Size = New System.Drawing.Size(384, 19)
        Me.title_follow.TabIndex = 6
        Me.title_follow.Text = "Follow these instructions in order to send an E-mail :"
        '
        'Help
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(407, 347)
        Me.Controls.Add(Me.understand_btn)
        Me.Controls.Add(Me.instructions)
        Me.Controls.Add(Me.title_follow)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.Name = "Help"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Help"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents understand_btn As System.Windows.Forms.Button
    Friend WithEvents instructions As System.Windows.Forms.Label
    Friend WithEvents title_follow As System.Windows.Forms.Label
End Class
