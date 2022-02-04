<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Demo
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Demo))
        Me.passwordbox = New System.Windows.Forms.TextBox()
        Me.loginbox = New System.Windows.Forms.TextBox()
        Me.portNum = New System.Windows.Forms.NumericUpDown()
        Me.auth_lbl = New System.Windows.Forms.Label()
        Me.int_type_lbl = New System.Windows.Forms.Label()
        Me.password_lbl = New System.Windows.Forms.Label()
        Me.login_lbl = New System.Windows.Forms.Label()
        Me.port_lbl = New System.Windows.Forms.Label()
        Me.host_lbl = New System.Windows.Forms.Label()
        Me.authentificationCombo = New System.Windows.Forms.ComboBox()
        Me.interactionCombo = New System.Windows.Forms.ComboBox()
        Me.hostBox = New System.Windows.Forms.TextBox()
        Me.startAuthorization = New System.Windows.Forms.Button()
        Me.closeButton = New System.Windows.Forms.Button()
        Me.target = New Email.Net.Pop3.Pop3Client(Me.components)
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        CType(Me.portNum, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'passwordbox
        '
        Me.passwordbox.Enabled = False
        Me.passwordbox.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.passwordbox.Location = New System.Drawing.Point(113, 86)
        Me.passwordbox.Name = "passwordbox"
        Me.passwordbox.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.passwordbox.Size = New System.Drawing.Size(150, 20)
        Me.passwordbox.TabIndex = 43
        '
        'loginbox
        '
        Me.loginbox.Enabled = False
        Me.loginbox.Location = New System.Drawing.Point(113, 60)
        Me.loginbox.Name = "loginbox"
        Me.loginbox.Size = New System.Drawing.Size(150, 20)
        Me.loginbox.TabIndex = 42
        '
        'portNum
        '
        Me.portNum.Enabled = False
        Me.portNum.Location = New System.Drawing.Point(113, 32)
        Me.portNum.Maximum = New Decimal(New Integer() {10000, 0, 0, 0})
        Me.portNum.Name = "portNum"
        Me.portNum.Size = New System.Drawing.Size(150, 20)
        Me.portNum.TabIndex = 33
        Me.portNum.Value = New Decimal(New Integer() {995, 0, 0, 0})
        '
        'auth_lbl
        '
        Me.auth_lbl.AutoSize = True
        Me.auth_lbl.Location = New System.Drawing.Point(5, 144)
        Me.auth_lbl.Name = "auth_lbl"
        Me.auth_lbl.Size = New System.Drawing.Size(78, 13)
        Me.auth_lbl.TabIndex = 41
        Me.auth_lbl.Text = "Authentication:"
        '
        'int_type_lbl
        '
        Me.int_type_lbl.AutoSize = True
        Me.int_type_lbl.Location = New System.Drawing.Point(5, 117)
        Me.int_type_lbl.Name = "int_type_lbl"
        Me.int_type_lbl.Size = New System.Drawing.Size(83, 13)
        Me.int_type_lbl.TabIndex = 40
        Me.int_type_lbl.Text = "Interaction type:"
        '
        'password_lbl
        '
        Me.password_lbl.AutoSize = True
        Me.password_lbl.Location = New System.Drawing.Point(5, 89)
        Me.password_lbl.Name = "password_lbl"
        Me.password_lbl.Size = New System.Drawing.Size(56, 13)
        Me.password_lbl.TabIndex = 39
        Me.password_lbl.Text = "Password:"
        '
        'login_lbl
        '
        Me.login_lbl.AutoSize = True
        Me.login_lbl.Location = New System.Drawing.Point(5, 63)
        Me.login_lbl.Name = "login_lbl"
        Me.login_lbl.Size = New System.Drawing.Size(36, 13)
        Me.login_lbl.TabIndex = 38
        Me.login_lbl.Text = "Login:"
        '
        'port_lbl
        '
        Me.port_lbl.AutoSize = True
        Me.port_lbl.Location = New System.Drawing.Point(5, 34)
        Me.port_lbl.Name = "port_lbl"
        Me.port_lbl.Size = New System.Drawing.Size(29, 13)
        Me.port_lbl.TabIndex = 37
        Me.port_lbl.Text = "Port:"
        '
        'host_lbl
        '
        Me.host_lbl.AutoSize = True
        Me.host_lbl.Location = New System.Drawing.Point(5, 9)
        Me.host_lbl.Name = "host_lbl"
        Me.host_lbl.Size = New System.Drawing.Size(32, 13)
        Me.host_lbl.TabIndex = 36
        Me.host_lbl.Text = "Host:"
        '
        'authentificationCombo
        '
        Me.authentificationCombo.Enabled = False
        Me.authentificationCombo.FormattingEnabled = True
        Me.authentificationCombo.Location = New System.Drawing.Point(113, 141)
        Me.authentificationCombo.Name = "authentificationCombo"
        Me.authentificationCombo.Size = New System.Drawing.Size(150, 21)
        Me.authentificationCombo.TabIndex = 35
        '
        'interactionCombo
        '
        Me.interactionCombo.Enabled = False
        Me.interactionCombo.FormattingEnabled = True
        Me.interactionCombo.Location = New System.Drawing.Point(113, 114)
        Me.interactionCombo.Name = "interactionCombo"
        Me.interactionCombo.Size = New System.Drawing.Size(150, 21)
        Me.interactionCombo.TabIndex = 34
        '
        'hostBox
        '
        Me.hostBox.Enabled = False
        Me.hostBox.Location = New System.Drawing.Point(113, 6)
        Me.hostBox.Name = "hostBox"
        Me.hostBox.Size = New System.Drawing.Size(150, 20)
        Me.hostBox.TabIndex = 32
        Me.hostBox.Text = "Type Email "
        '
        'startAuthorization
        '
        Me.startAuthorization.Location = New System.Drawing.Point(53, 175)
        Me.startAuthorization.Name = "startAuthorization"
        Me.startAuthorization.Size = New System.Drawing.Size(75, 23)
        Me.startAuthorization.TabIndex = 30
        Me.startAuthorization.Text = "Check"
        Me.ToolTip1.SetToolTip(Me.startAuthorization, "Checks your account details")
        Me.startAuthorization.UseVisualStyleBackColor = True
        '
        'closeButton
        '
        Me.closeButton.Location = New System.Drawing.Point(134, 175)
        Me.closeButton.Name = "closeButton"
        Me.closeButton.Size = New System.Drawing.Size(75, 23)
        Me.closeButton.TabIndex = 31
        Me.closeButton.Text = "Close"
        Me.closeButton.UseVisualStyleBackColor = True
        '
        'target
        '
        Me.target.AuthenticationType = Email.Net.Common.Configurations.EAuthenticationType.[Auto]
        Me.target.Host = "localhost"
        Me.target.Password = ""
        Me.target.Port = CType(25US, UShort)
        Me.target.ProxyHost = ""
        Me.target.ProxyPassword = ""
        Me.target.ProxyPort = CType(0US, UShort)
        Me.target.ProxyType = Email.Net.Common.Configurations.EProxyType.No
        Me.target.ProxyUser = ""
        Me.target.ReceiveTimeout = 10000
        Me.target.SendTimeout = 10000
        Me.target.SSLInteractionType = Email.Net.Common.Configurations.EInteractionType.Plain
        Me.target.Username = ""
        '
        'ToolTip1
        '
        Me.ToolTip1.AutoPopDelay = 10000
        Me.ToolTip1.InitialDelay = 500
        Me.ToolTip1.ReshowDelay = 100
        '
        'Demo
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(268, 204)
        Me.Controls.Add(Me.passwordbox)
        Me.Controls.Add(Me.loginbox)
        Me.Controls.Add(Me.portNum)
        Me.Controls.Add(Me.auth_lbl)
        Me.Controls.Add(Me.int_type_lbl)
        Me.Controls.Add(Me.password_lbl)
        Me.Controls.Add(Me.login_lbl)
        Me.Controls.Add(Me.port_lbl)
        Me.Controls.Add(Me.host_lbl)
        Me.Controls.Add(Me.authentificationCombo)
        Me.Controls.Add(Me.interactionCombo)
        Me.Controls.Add(Me.hostBox)
        Me.Controls.Add(Me.startAuthorization)
        Me.Controls.Add(Me.closeButton)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "Demo"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Email Verifier"
        CType(Me.portNum, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents passwordbox As System.Windows.Forms.TextBox
    Friend WithEvents loginbox As System.Windows.Forms.TextBox
    Private WithEvents portNum As System.Windows.Forms.NumericUpDown
    Private WithEvents auth_lbl As System.Windows.Forms.Label
    Private WithEvents int_type_lbl As System.Windows.Forms.Label
    Private WithEvents password_lbl As System.Windows.Forms.Label
    Private WithEvents login_lbl As System.Windows.Forms.Label
    Private WithEvents port_lbl As System.Windows.Forms.Label
    Private WithEvents host_lbl As System.Windows.Forms.Label
    Private WithEvents authentificationCombo As System.Windows.Forms.ComboBox
    Private WithEvents interactionCombo As System.Windows.Forms.ComboBox
    Private WithEvents hostBox As System.Windows.Forms.TextBox
    Private WithEvents startAuthorization As System.Windows.Forms.Button
    Private WithEvents closeButton As System.Windows.Forms.Button
    Private WithEvents target As Email.Net.Pop3.Pop3Client
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
End Class
