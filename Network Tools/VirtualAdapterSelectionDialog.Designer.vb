<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class VirtualAdapterSelectionDialog
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
        Me.IcsVirtualAdapterIdComboBox = New System.Windows.Forms.ComboBox()
        Me.selectButton = New System.Windows.Forms.Button()
        Me.SelectConnectionLabel = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'IcsVirtualAdapterIdComboBox
        '
        Me.IcsVirtualAdapterIdComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.IcsVirtualAdapterIdComboBox.FormattingEnabled = True
        Me.IcsVirtualAdapterIdComboBox.Location = New System.Drawing.Point(21, 43)
        Me.IcsVirtualAdapterIdComboBox.Name = "IcsVirtualAdapterIdComboBox"
        Me.IcsVirtualAdapterIdComboBox.Size = New System.Drawing.Size(270, 21)
        Me.IcsVirtualAdapterIdComboBox.Sorted = True
        Me.IcsVirtualAdapterIdComboBox.TabIndex = 11
        '
        'selectButton
        '
        Me.selectButton.Location = New System.Drawing.Point(218, 70)
        Me.selectButton.Name = "selectButton"
        Me.selectButton.Size = New System.Drawing.Size(73, 23)
        Me.selectButton.TabIndex = 12
        Me.selectButton.Text = "&Select"
        Me.selectButton.UseVisualStyleBackColor = True
        '
        'SelectConnectionLabel
        '
        Me.SelectConnectionLabel.AutoSize = True
        Me.SelectConnectionLabel.Location = New System.Drawing.Point(18, 14)
        Me.SelectConnectionLabel.Name = "SelectConnectionLabel"
        Me.SelectConnectionLabel.Size = New System.Drawing.Size(225, 26)
        Me.SelectConnectionLabel.TabIndex = 10
        Me.SelectConnectionLabel.Text = "Multiple virtual network adapters are detected!" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Select any one from the list: "
        '
        'VirtualAdapterSelectionDialog
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(309, 106)
        Me.Controls.Add(Me.IcsVirtualAdapterIdComboBox)
        Me.Controls.Add(Me.selectButton)
        Me.Controls.Add(Me.SelectConnectionLabel)
        Me.MaximizeBox = False
        Me.MaximumSize = New System.Drawing.Size(325, 145)
        Me.MinimizeBox = False
        Me.MinimumSize = New System.Drawing.Size(325, 145)
        Me.Name = "VirtualAdapterSelectionDialog"
        Me.ShowIcon = False
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Multiple Virtual Adapter(s)"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Private WithEvents IcsVirtualAdapterIdComboBox As System.Windows.Forms.ComboBox
    Public WithEvents selectButton As System.Windows.Forms.Button
    Private WithEvents SelectConnectionLabel As System.Windows.Forms.Label
End Class
