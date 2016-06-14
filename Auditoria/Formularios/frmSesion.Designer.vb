<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Public Class frmSesion
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing AndAlso components IsNot Nothing Then
            components.Dispose()
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer
    Private mainMenu1 As System.Windows.Forms.MainMenu

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
    Me.mainMenu1 = New System.Windows.Forms.MainMenu
    Me.Label1 = New System.Windows.Forms.Label
    Me.LblUsuario = New System.Windows.Forms.Label
    Me.LblContraseña = New System.Windows.Forms.Label
    Me.utxtUsuario = New System.Windows.Forms.TextBox
    Me.UtxtContraseña = New System.Windows.Forms.TextBox
    Me.BtnAceptar = New System.Windows.Forms.Button
    Me.BtnCancelar = New System.Windows.Forms.Button
    Me.lblUser = New System.Windows.Forms.Label
    Me.lblPass = New System.Windows.Forms.Label
    Me.SuspendLayout()
    '
    'Label1
    '
    Me.Label1.Font = New System.Drawing.Font("Tahoma", 8.7!, System.Drawing.FontStyle.Regular)
    Me.Label1.Location = New System.Drawing.Point(0, 4)
    Me.Label1.Name = "Label1"
    Me.Label1.Size = New System.Drawing.Size(236, 19)
    Me.Label1.Text = "Ingresa tu usuario y contraseña del sistema"
    '
    'LblUsuario
    '
    Me.LblUsuario.Location = New System.Drawing.Point(0, 30)
    Me.LblUsuario.Name = "LblUsuario"
    Me.LblUsuario.Size = New System.Drawing.Size(80, 20)
    Me.LblUsuario.Text = "Usuario:"
    '
    'LblContraseña
    '
    Me.LblContraseña.Location = New System.Drawing.Point(0, 60)
    Me.LblContraseña.Name = "LblContraseña"
    Me.LblContraseña.Size = New System.Drawing.Size(80, 20)
    Me.LblContraseña.Text = "Contraseña: "
    '
    'utxtUsuario
    '
    Me.utxtUsuario.Location = New System.Drawing.Point(86, 31)
    Me.utxtUsuario.Name = "utxtUsuario"
    Me.utxtUsuario.Size = New System.Drawing.Size(114, 21)
    Me.utxtUsuario.TabIndex = 3
    '
    'UtxtContraseña
    '
    Me.UtxtContraseña.Location = New System.Drawing.Point(86, 58)
    Me.UtxtContraseña.Name = "UtxtContraseña"
    Me.UtxtContraseña.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
    Me.UtxtContraseña.Size = New System.Drawing.Size(113, 21)
    Me.UtxtContraseña.TabIndex = 4
    '
    'BtnAceptar
    '
    Me.BtnAceptar.Location = New System.Drawing.Point(86, 88)
    Me.BtnAceptar.Name = "BtnAceptar"
    Me.BtnAceptar.Size = New System.Drawing.Size(72, 20)
    Me.BtnAceptar.TabIndex = 5
    Me.BtnAceptar.Text = "Aceptar"
    '
    'BtnCancelar
    '
    Me.BtnCancelar.Location = New System.Drawing.Point(164, 88)
    Me.BtnCancelar.Name = "BtnCancelar"
    Me.BtnCancelar.Size = New System.Drawing.Size(72, 20)
    Me.BtnCancelar.TabIndex = 6
    Me.BtnCancelar.Text = "Cancelar"
    '
    'lblUser
    '
    Me.lblUser.Location = New System.Drawing.Point(201, 32)
    Me.lblUser.Name = "lblUser"
    Me.lblUser.Size = New System.Drawing.Size(35, 20)
    Me.lblUser.Text = "Clave"
    Me.lblUser.Visible = False
    '
    'lblPass
    '
    Me.lblPass.Location = New System.Drawing.Point(201, 59)
    Me.lblPass.Name = "lblPass"
    Me.lblPass.Size = New System.Drawing.Size(35, 20)
    Me.lblPass.Text = "Pass"
    Me.lblPass.Visible = False
    '
    'frmSesion
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
    Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
    Me.AutoScroll = True
    Me.ClientSize = New System.Drawing.Size(240, 120)
    Me.Controls.Add(Me.lblPass)
    Me.Controls.Add(Me.lblUser)
    Me.Controls.Add(Me.BtnCancelar)
    Me.Controls.Add(Me.BtnAceptar)
    Me.Controls.Add(Me.UtxtContraseña)
    Me.Controls.Add(Me.utxtUsuario)
    Me.Controls.Add(Me.LblContraseña)
    Me.Controls.Add(Me.LblUsuario)
    Me.Controls.Add(Me.Label1)
    Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
    Me.MinimizeBox = False
    Me.Name = "frmSesion"
    Me.Text = "Sesión Conexión"
    Me.ResumeLayout(False)

  End Sub
  Friend WithEvents Label1 As System.Windows.Forms.Label
  Friend WithEvents LblUsuario As System.Windows.Forms.Label
  Friend WithEvents LblContraseña As System.Windows.Forms.Label
  Friend WithEvents utxtUsuario As System.Windows.Forms.TextBox
  Friend WithEvents UtxtContraseña As System.Windows.Forms.TextBox
  Friend WithEvents BtnAceptar As System.Windows.Forms.Button
  Friend WithEvents BtnCancelar As System.Windows.Forms.Button
  Friend WithEvents lblUser As System.Windows.Forms.Label
  Friend WithEvents lblPass As System.Windows.Forms.Label
End Class
