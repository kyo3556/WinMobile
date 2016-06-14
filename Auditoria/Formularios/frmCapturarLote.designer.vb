<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Public Class frmCapturarLote
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
  Private mnuCapturarLote As System.Windows.Forms.MainMenu

  'NOTE: The following procedure is required by the Windows Form Designer
  'It can be modified using the Windows Form Designer.  
  'Do not modify it using the code editor.
  <System.Diagnostics.DebuggerStepThrough()> _
  Private Sub InitializeComponent()
    Me.mnuCapturarLote = New System.Windows.Forms.MainMenu
    Me.mnuRegresar = New System.Windows.Forms.MenuItem
    Me.lblNombreDestino = New System.Windows.Forms.Label
    Me.lblDestino = New System.Windows.Forms.Label
    Me.Id_Aud = New System.Windows.Forms.Label
    Me.lblFolio = New System.Windows.Forms.Label
    Me.lblCantidad = New System.Windows.Forms.Label
    Me.txtCantidad = New System.Windows.Forms.TextBox
    Me.lblCodigo = New System.Windows.Forms.Label
    Me.lblNombreArticulo = New System.Windows.Forms.Label
    Me.lblArticulo = New System.Windows.Forms.Label
    Me.Id_Lte = New System.Windows.Forms.Label
    Me.Id_Art = New System.Windows.Forms.Label
    Me.txtCodigo = New System.Windows.Forms.TextBox
    Me.btnScan = New System.Windows.Forms.Button
    Me.grdDetalle = New System.Windows.Forms.DataGrid
    Me.Id_Drc = New System.Windows.Forms.Label
    Me.LblCantidadTotal = New System.Windows.Forms.Label
    Me.SuspendLayout()
    '
    'mnuCapturarLote
    '
    Me.mnuCapturarLote.MenuItems.Add(Me.mnuRegresar)
    '
    'mnuRegresar
    '
    Me.mnuRegresar.Text = "Regresar"
    '
    'lblNombreDestino
    '
    Me.lblNombreDestino.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Regular)
    Me.lblNombreDestino.Location = New System.Drawing.Point(69, 24)
    Me.lblNombreDestino.Name = "lblNombreDestino"
    Me.lblNombreDestino.Size = New System.Drawing.Size(165, 16)
    Me.lblNombreDestino.Text = "[Nombre Bodega]"
    '
    'lblDestino
    '
    Me.lblDestino.Font = New System.Drawing.Font("Tahoma", 10.0!, System.Drawing.FontStyle.Regular)
    Me.lblDestino.Location = New System.Drawing.Point(3, 23)
    Me.lblDestino.Name = "lblDestino"
    Me.lblDestino.Size = New System.Drawing.Size(60, 16)
    Me.lblDestino.Text = "Bodega:"
    '
    'Id_Aud
    '
    Me.Id_Aud.Font = New System.Drawing.Font("Tahoma", 10.0!, System.Drawing.FontStyle.Regular)
    Me.Id_Aud.Location = New System.Drawing.Point(69, 4)
    Me.Id_Aud.Name = "Id_Aud"
    Me.Id_Aud.Size = New System.Drawing.Size(91, 16)
    Me.Id_Aud.Text = "[Id_Aud]"
    '
    'lblFolio
    '
    Me.lblFolio.Font = New System.Drawing.Font("Tahoma", 10.0!, System.Drawing.FontStyle.Regular)
    Me.lblFolio.Location = New System.Drawing.Point(3, 4)
    Me.lblFolio.Name = "lblFolio"
    Me.lblFolio.Size = New System.Drawing.Size(63, 16)
    Me.lblFolio.Text = "Folio:"
    '
    'lblCantidad
    '
    Me.lblCantidad.Font = New System.Drawing.Font("Tahoma", 10.0!, System.Drawing.FontStyle.Regular)
    Me.lblCantidad.Location = New System.Drawing.Point(3, 47)
    Me.lblCantidad.Name = "lblCantidad"
    Me.lblCantidad.Size = New System.Drawing.Size(63, 16)
    Me.lblCantidad.Text = "Cantidad:"
    '
    'txtCantidad
    '
    Me.txtCantidad.Font = New System.Drawing.Font("Tahoma", 10.0!, System.Drawing.FontStyle.Regular)
    Me.txtCantidad.Location = New System.Drawing.Point(69, 44)
    Me.txtCantidad.Name = "txtCantidad"
    Me.txtCantidad.Size = New System.Drawing.Size(55, 23)
    Me.txtCantidad.TabIndex = 10
    Me.txtCantidad.Text = "1"
    '
    'lblCodigo
    '
    Me.lblCodigo.Font = New System.Drawing.Font("Tahoma", 10.0!, System.Drawing.FontStyle.Regular)
    Me.lblCodigo.Location = New System.Drawing.Point(3, 72)
    Me.lblCodigo.Name = "lblCodigo"
    Me.lblCodigo.Size = New System.Drawing.Size(63, 16)
    Me.lblCodigo.Text = "Código:"
    '
    'lblNombreArticulo
    '
    Me.lblNombreArticulo.Font = New System.Drawing.Font("Tahoma", 10.0!, System.Drawing.FontStyle.Regular)
    Me.lblNombreArticulo.Location = New System.Drawing.Point(69, 97)
    Me.lblNombreArticulo.Name = "lblNombreArticulo"
    Me.lblNombreArticulo.Size = New System.Drawing.Size(165, 16)
    Me.lblNombreArticulo.Text = "[Nombre Articulo]"
    '
    'lblArticulo
    '
    Me.lblArticulo.Font = New System.Drawing.Font("Tahoma", 10.0!, System.Drawing.FontStyle.Regular)
    Me.lblArticulo.Location = New System.Drawing.Point(3, 96)
    Me.lblArticulo.Name = "lblArticulo"
    Me.lblArticulo.Size = New System.Drawing.Size(63, 16)
    Me.lblArticulo.Text = "Artículo:"
    '
    'Id_Lte
    '
    Me.Id_Lte.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Regular)
    Me.Id_Lte.Location = New System.Drawing.Point(189, 47)
    Me.Id_Lte.Name = "Id_Lte"
    Me.Id_Lte.Size = New System.Drawing.Size(44, 16)
    Me.Id_Lte.Text = "[Id_Lte]"
    Me.Id_Lte.TextAlign = System.Drawing.ContentAlignment.TopCenter
    Me.Id_Lte.Visible = False
    '
    'Id_Art
    '
    Me.Id_Art.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Regular)
    Me.Id_Art.Location = New System.Drawing.Point(4, 246)
    Me.Id_Art.Name = "Id_Art"
    Me.Id_Art.Size = New System.Drawing.Size(44, 16)
    Me.Id_Art.Text = "[Id_Art]"
    Me.Id_Art.TextAlign = System.Drawing.ContentAlignment.TopCenter
    Me.Id_Art.Visible = False
    '
    'txtCodigo
    '
    Me.txtCodigo.Font = New System.Drawing.Font("Tahoma", 10.0!, System.Drawing.FontStyle.Regular)
    Me.txtCodigo.Location = New System.Drawing.Point(69, 71)
    Me.txtCodigo.Name = "txtCodigo"
    Me.txtCodigo.Size = New System.Drawing.Size(123, 23)
    Me.txtCodigo.TabIndex = 16
    '
    'btnScan
    '
    Me.btnScan.Font = New System.Drawing.Font("Tahoma", 7.0!, System.Drawing.FontStyle.Regular)
    Me.btnScan.Location = New System.Drawing.Point(197, 71)
    Me.btnScan.Name = "btnScan"
    Me.btnScan.Size = New System.Drawing.Size(37, 22)
    Me.btnScan.TabIndex = 17
    Me.btnScan.Text = "Scan"
    '
    'grdDetalle
    '
    Me.grdDetalle.BackgroundColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer))
    Me.grdDetalle.Font = New System.Drawing.Font("Tahoma", 7.0!, System.Drawing.FontStyle.Regular)
    Me.grdDetalle.Location = New System.Drawing.Point(4, 116)
    Me.grdDetalle.Name = "grdDetalle"
    Me.grdDetalle.RowHeadersVisible = False
    Me.grdDetalle.Size = New System.Drawing.Size(232, 127)
    Me.grdDetalle.TabIndex = 28
    '
    'Id_Drc
    '
    Me.Id_Drc.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Regular)
    Me.Id_Drc.Location = New System.Drawing.Point(189, 24)
    Me.Id_Drc.Name = "Id_Drc"
    Me.Id_Drc.Size = New System.Drawing.Size(45, 16)
    Me.Id_Drc.Text = "[Id_Drc]"
    Me.Id_Drc.TextAlign = System.Drawing.ContentAlignment.TopCenter
    Me.Id_Drc.Visible = False
    '
    'LblCantidadTotal
    '
    Me.LblCantidadTotal.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Regular)
    Me.LblCantidadTotal.Location = New System.Drawing.Point(54, 246)
    Me.LblCantidadTotal.Name = "LblCantidadTotal"
    Me.LblCantidadTotal.Size = New System.Drawing.Size(186, 16)
    Me.LblCantidadTotal.Text = "[Cantidad]"
    '
    'frmCapturarLote
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
    Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
    Me.AutoScroll = True
    Me.AutoValidate = System.Windows.Forms.AutoValidate.EnableAllowFocusChange
    Me.ClientSize = New System.Drawing.Size(240, 268)
    Me.ControlBox = False
    Me.Controls.Add(Me.LblCantidadTotal)
    Me.Controls.Add(Me.Id_Drc)
    Me.Controls.Add(Me.grdDetalle)
    Me.Controls.Add(Me.btnScan)
    Me.Controls.Add(Me.txtCodigo)
    Me.Controls.Add(Me.Id_Lte)
    Me.Controls.Add(Me.Id_Art)
    Me.Controls.Add(Me.lblNombreArticulo)
    Me.Controls.Add(Me.lblArticulo)
    Me.Controls.Add(Me.lblCodigo)
    Me.Controls.Add(Me.txtCantidad)
    Me.Controls.Add(Me.lblCantidad)
    Me.Controls.Add(Me.Id_Aud)
    Me.Controls.Add(Me.lblFolio)
    Me.Controls.Add(Me.lblNombreDestino)
    Me.Controls.Add(Me.lblDestino)
    Me.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Regular)
    Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
    Me.KeyPreview = True
    Me.Menu = Me.mnuCapturarLote
    Me.MinimizeBox = False
    Me.Name = "frmCapturarLote"
    Me.Text = "Captura de Lotes"
    Me.ResumeLayout(False)

  End Sub
  Friend WithEvents mnuRegresar As System.Windows.Forms.MenuItem
  Friend WithEvents lblNombreDestino As System.Windows.Forms.Label
  Friend WithEvents lblDestino As System.Windows.Forms.Label
  Friend WithEvents Id_Aud As System.Windows.Forms.Label
  Friend WithEvents lblFolio As System.Windows.Forms.Label
  Friend WithEvents lblCantidad As System.Windows.Forms.Label
  Friend WithEvents txtCantidad As System.Windows.Forms.TextBox
  Friend WithEvents lblCodigo As System.Windows.Forms.Label
  Friend WithEvents lblNombreArticulo As System.Windows.Forms.Label
  Friend WithEvents lblArticulo As System.Windows.Forms.Label
  Friend WithEvents Id_Lte As System.Windows.Forms.Label
  Friend WithEvents Id_Art As System.Windows.Forms.Label
  Friend WithEvents txtCodigo As System.Windows.Forms.TextBox
  Friend WithEvents btnScan As System.Windows.Forms.Button
  Friend WithEvents grdDetalle As System.Windows.Forms.DataGrid
  Friend WithEvents Id_Drc As System.Windows.Forms.Label
  Friend WithEvents LblCantidadTotal As System.Windows.Forms.Label
End Class
