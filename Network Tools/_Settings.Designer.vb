<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class _Settings
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
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(_Settings))
        Me.Btn_edit = New System.Windows.Forms.Button()
        Me.Label1006 = New System.Windows.Forms.Label()
        Me.Label1005 = New System.Windows.Forms.Label()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.ComboBox3 = New System.Windows.Forms.ComboBox()
        Me.Label1007 = New System.Windows.Forms.Label()
        Me.ComboBox2 = New System.Windows.Forms.ComboBox()
        Me.txtbox1005 = New System.Windows.Forms.TextBox()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.serverbox = New System.Windows.Forms.ComboBox()
        Me.Label1004 = New System.Windows.Forms.Label()
        Me.Label1003 = New System.Windows.Forms.Label()
        Me.Label1002 = New System.Windows.Forms.Label()
        Me.Btn_del = New System.Windows.Forms.Button()
        Me.Btn_load = New System.Windows.Forms.Button()
        Me.btn_save = New System.Windows.Forms.Button()
        Me.password = New System.Windows.Forms.TextBox()
        Me.email = New System.Windows.Forms.TextBox()
        Me.Label1000 = New System.Windows.Forms.Label()
        Me.cboprof = New System.Windows.Forms.ComboBox()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.SuspendLayout()
        '
        'Btn_edit
        '
        Me.Btn_edit.Enabled = False
        Me.Btn_edit.Font = New System.Drawing.Font("Tahoma", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Btn_edit.Location = New System.Drawing.Point(44, 313)
        Me.Btn_edit.Name = "Btn_edit"
        Me.Btn_edit.Size = New System.Drawing.Size(133, 45)
        Me.Btn_edit.TabIndex = 72
        Me.Btn_edit.Text = "Edit Profile"
        Me.ToolTip1.SetToolTip(Me.Btn_edit, "Edits the selected saved Profile")
        Me.Btn_edit.UseVisualStyleBackColor = True
        '
        'Label1006
        '
        Me.Label1006.AutoSize = True
        Me.Label1006.Font = New System.Drawing.Font("Tahoma", 15.0!)
        Me.Label1006.Location = New System.Drawing.Point(57, 114)
        Me.Label1006.Name = "Label1006"
        Me.Label1006.Size = New System.Drawing.Size(137, 24)
        Me.Label1006.TabIndex = 71
        Me.Label1006.Text = "Profile Name :"
        '
        'Label1005
        '
        Me.Label1005.AutoSize = True
        Me.Label1005.Font = New System.Drawing.Font("Tahoma", 12.0!)
        Me.Label1005.Location = New System.Drawing.Point(179, 65)
        Me.Label1005.Name = "Label1005"
        Me.Label1005.Size = New System.Drawing.Size(136, 19)
        Me.Label1005.TabIndex = 70
        Me.Label1005.Text = "Or &Select a profile"
        '
        'Button1
        '
        Me.Button1.Font = New System.Drawing.Font("Tahoma", 10.0!)
        Me.Button1.Location = New System.Drawing.Point(44, 58)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(133, 36)
        Me.Button1.TabIndex = 69
        Me.Button1.Text = "Create New Profle"
        Me.ToolTip1.SetToolTip(Me.Button1, "Click to create a new Profile")
        Me.Button1.UseVisualStyleBackColor = True
        '
        'ComboBox3
        '
        Me.ComboBox3.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ComboBox3.Enabled = False
        Me.ComboBox3.FormattingEnabled = True
        Me.ComboBox3.Items.AddRange(New Object() {"gmx.com", "gmx.us"})
        Me.ComboBox3.Location = New System.Drawing.Point(256, 153)
        Me.ComboBox3.Name = "ComboBox3"
        Me.ComboBox3.Size = New System.Drawing.Size(100, 21)
        Me.ComboBox3.TabIndex = 68
        Me.ComboBox3.Visible = False
        '
        'Label1007
        '
        Me.Label1007.AutoSize = True
        Me.Label1007.Location = New System.Drawing.Point(230, 157)
        Me.Label1007.Name = "Label1007"
        Me.Label1007.Size = New System.Drawing.Size(19, 13)
        Me.Label1007.TabIndex = 67
        Me.Label1007.Text = "-->"
        Me.Label1007.Visible = False
        '
        'ComboBox2
        '
        Me.ComboBox2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ComboBox2.FormattingEnabled = True
        Me.ComboBox2.Items.AddRange(New Object() {"Hotmail", "Live", "Outlook", "Msn"})
        Me.ComboBox2.Location = New System.Drawing.Point(256, 153)
        Me.ComboBox2.Name = "ComboBox2"
        Me.ComboBox2.Size = New System.Drawing.Size(100, 21)
        Me.ComboBox2.TabIndex = 66
        Me.ComboBox2.Visible = False
        '
        'txtbox1005
        '
        Me.txtbox1005.Location = New System.Drawing.Point(256, 192)
        Me.txtbox1005.Name = "txtbox1005"
        Me.txtbox1005.ReadOnly = True
        Me.txtbox1005.Size = New System.Drawing.Size(100, 20)
        Me.txtbox1005.TabIndex = 65
        Me.txtbox1005.Text = "Select Provider"
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Font = New System.Drawing.Font("Tahoma", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.Location = New System.Drawing.Point(200, 115)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(94, 23)
        Me.Label10.TabIndex = 64
        Me.Label10.Text = ".............."
        '
        'serverbox
        '
        Me.serverbox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.serverbox.Enabled = False
        Me.serverbox.FormattingEnabled = True
        Me.serverbox.Items.AddRange(New Object() {"smtp.gmail.com", "smtp.live.com", "smtp.aol.com", "smtp.mail.yahoo.com", "smtp.gmx.com", "smtp.mail.com"})
        Me.serverbox.Location = New System.Drawing.Point(100, 153)
        Me.serverbox.Name = "serverbox"
        Me.serverbox.Size = New System.Drawing.Size(125, 21)
        Me.serverbox.TabIndex = 63
        '
        'Label1004
        '
        Me.Label1004.AutoSize = True
        Me.Label1004.Font = New System.Drawing.Font("Tahoma", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1004.Location = New System.Drawing.Point(15, 153)
        Me.Label1004.Name = "Label1004"
        Me.Label1004.Size = New System.Drawing.Size(70, 18)
        Me.Label1004.TabIndex = 62
        Me.Label1004.Text = "Provider :"
        '
        'Label1003
        '
        Me.Label1003.AutoSize = True
        Me.Label1003.Font = New System.Drawing.Font("Tahoma", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1003.Location = New System.Drawing.Point(15, 228)
        Me.Label1003.Name = "Label1003"
        Me.Label1003.Size = New System.Drawing.Size(79, 18)
        Me.Label1003.TabIndex = 61
        Me.Label1003.Text = "Password :"
        '
        'Label1002
        '
        Me.Label1002.AutoSize = True
        Me.Label1002.Font = New System.Drawing.Font("Tahoma", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1002.Location = New System.Drawing.Point(15, 191)
        Me.Label1002.Name = "Label1002"
        Me.Label1002.Size = New System.Drawing.Size(51, 18)
        Me.Label1002.TabIndex = 60
        Me.Label1002.Text = "Email :"
        '
        'Btn_del
        '
        Me.Btn_del.Enabled = False
        Me.Btn_del.Font = New System.Drawing.Font("Tahoma", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Btn_del.Location = New System.Drawing.Point(190, 313)
        Me.Btn_del.Name = "Btn_del"
        Me.Btn_del.Size = New System.Drawing.Size(133, 45)
        Me.Btn_del.TabIndex = 59
        Me.Btn_del.Text = "Delete Profile"
        Me.ToolTip1.SetToolTip(Me.Btn_del, "Deletes Saved Profile")
        Me.Btn_del.UseVisualStyleBackColor = True
        '
        'Btn_load
        '
        Me.Btn_load.Enabled = False
        Me.Btn_load.Font = New System.Drawing.Font("Tahoma", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Btn_load.Location = New System.Drawing.Point(190, 262)
        Me.Btn_load.Name = "Btn_load"
        Me.Btn_load.Size = New System.Drawing.Size(133, 45)
        Me.Btn_load.TabIndex = 58
        Me.Btn_load.Text = "Load Profile"
        Me.ToolTip1.SetToolTip(Me.Btn_load, "Loads the profile on the program")
        Me.Btn_load.UseVisualStyleBackColor = True
        '
        'btn_save
        '
        Me.btn_save.Enabled = False
        Me.btn_save.Font = New System.Drawing.Font("Tahoma", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btn_save.Location = New System.Drawing.Point(44, 262)
        Me.btn_save.Name = "btn_save"
        Me.btn_save.Size = New System.Drawing.Size(133, 45)
        Me.btn_save.TabIndex = 57
        Me.btn_save.Text = "Save Profile"
        Me.ToolTip1.SetToolTip(Me.btn_save, "Save E-mail Profile")
        Me.btn_save.UseVisualStyleBackColor = True
        '
        'password
        '
        Me.password.Location = New System.Drawing.Point(100, 229)
        Me.password.Name = "password"
        Me.password.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.password.ReadOnly = True
        Me.password.Size = New System.Drawing.Size(150, 20)
        Me.password.TabIndex = 56
        Me.ToolTip1.SetToolTip(Me.password, "Password of your E-mail")
        '
        'email
        '
        Me.email.Location = New System.Drawing.Point(100, 192)
        Me.email.Name = "email"
        Me.email.ReadOnly = True
        Me.email.Size = New System.Drawing.Size(150, 20)
        Me.email.TabIndex = 55
        Me.ToolTip1.SetToolTip(Me.email, "Your E-mail without (@provider.com)")
        '
        'Label1000
        '
        Me.Label1000.AutoSize = True
        Me.Label1000.Font = New System.Drawing.Font("Tahoma", 15.75!, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1000.ForeColor = System.Drawing.Color.Black
        Me.Label1000.Location = New System.Drawing.Point(13, 14)
        Me.Label1000.Name = "Label1000"
        Me.Label1000.Size = New System.Drawing.Size(78, 25)
        Me.Label1000.TabIndex = 54
        Me.Label1000.Text = "Profile:"
        '
        'cboprof
        '
        Me.cboprof.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboprof.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboprof.FormattingEnabled = True
        Me.cboprof.Location = New System.Drawing.Point(100, 12)
        Me.cboprof.MaxLength = 15
        Me.cboprof.Name = "cboprof"
        Me.cboprof.Size = New System.Drawing.Size(256, 27)
        Me.cboprof.TabIndex = 53
        Me.ToolTip1.SetToolTip(Me.cboprof, "Saved E-mails Profile Names")
        '
        'ToolTip1
        '
        Me.ToolTip1.AutoPopDelay = 5000
        Me.ToolTip1.InitialDelay = 500
        Me.ToolTip1.ReshowDelay = 100
        '
        '_Settings
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(368, 370)
        Me.Controls.Add(Me.Btn_edit)
        Me.Controls.Add(Me.Label1006)
        Me.Controls.Add(Me.Label1005)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.ComboBox3)
        Me.Controls.Add(Me.Label1007)
        Me.Controls.Add(Me.ComboBox2)
        Me.Controls.Add(Me.txtbox1005)
        Me.Controls.Add(Me.Label10)
        Me.Controls.Add(Me.serverbox)
        Me.Controls.Add(Me.Label1004)
        Me.Controls.Add(Me.Label1003)
        Me.Controls.Add(Me.Label1002)
        Me.Controls.Add(Me.Btn_del)
        Me.Controls.Add(Me.Btn_load)
        Me.Controls.Add(Me.btn_save)
        Me.Controls.Add(Me.password)
        Me.Controls.Add(Me.email)
        Me.Controls.Add(Me.Label1000)
        Me.Controls.Add(Me.cboprof)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.Name = "_Settings"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Email Loader"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Btn_edit As System.Windows.Forms.Button
    Friend WithEvents Label1006 As System.Windows.Forms.Label
    Friend WithEvents Label1005 As System.Windows.Forms.Label
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents ComboBox3 As System.Windows.Forms.ComboBox
    Friend WithEvents Label1007 As System.Windows.Forms.Label
    Friend WithEvents ComboBox2 As System.Windows.Forms.ComboBox
    Friend WithEvents txtbox1005 As System.Windows.Forms.TextBox
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents serverbox As System.Windows.Forms.ComboBox
    Friend WithEvents Label1004 As System.Windows.Forms.Label
    Friend WithEvents Label1003 As System.Windows.Forms.Label
    Friend WithEvents Label1002 As System.Windows.Forms.Label
    Friend WithEvents Btn_del As System.Windows.Forms.Button
    Friend WithEvents Btn_load As System.Windows.Forms.Button
    Friend WithEvents btn_save As System.Windows.Forms.Button
    Friend WithEvents password As System.Windows.Forms.TextBox
    Friend WithEvents email As System.Windows.Forms.TextBox
    Friend WithEvents Label1000 As System.Windows.Forms.Label
    Friend WithEvents cboprof As System.Windows.Forms.ComboBox
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
End Class
