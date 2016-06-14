<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Public Class frmPrincipal
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
  Private mMenu As System.Windows.Forms.MainMenu

  'NOTE: The following procedure is required by the Windows Form Designer
  'It can be modified using the Windows Form Designer.  
  'Do not modify it using the code editor.
  <System.Diagnostics.DebuggerStepThrough()> _
  Private Sub InitializeComponent()
    Me.mMenu = New System.Windows.Forms.MainMenu
    Me.mItem = New System.Windows.Forms.MenuItem
    Me.mnuCapturar = New System.Windows.Forms.MenuItem
    Me.mnuRefrescar = New System.Windows.Forms.MenuItem
    Me.mnuPropiedades = New System.Windows.Forms.MenuItem
    Me.MenuItem1 = New System.Windows.Forms.MenuItem
    Me.mnuSincronizar = New System.Windows.Forms.MenuItem
    Me.mnuSincronizarCodes = New System.Windows.Forms.MenuItem
    Me.mnuEnviarDatos = New System.Windows.Forms.MenuItem
    Me.mnuSalir = New System.Windows.Forms.MenuItem
    Me.cboNumero = New System.Windows.Forms.ComboBox
    Me.LblFolio = New System.Windows.Forms.Label
    Me.cboBodega = New System.Windows.Forms.ComboBox
    Me.grdDetalle = New System.Windows.Forms.DataGrid
    Me.ContextMenu1 = New System.Windows.Forms.ContextMenu
    Me.MenuItem2 = New System.Windows.Forms.MenuItem
    Me.MenuItem3 = New System.Windows.Forms.MenuItem
    Me.SuspendLayout()
    '
    'mMenu
    '
    Me.mMenu.MenuItems.Add(Me.mItem)
    Me.mMenu.MenuItems.Add(Me.mnuSalir)
    '
    'mItem
    '
    Me.mItem.MenuItems.Add(Me.mnuCapturar)
    Me.mItem.MenuItems.Add(Me.mnuRefrescar)
    Me.mItem.MenuItems.Add(Me.mnuPropiedades)
    Me.mItem.MenuItems.Add(Me.MenuItem1)
    Me.mItem.MenuItems.Add(Me.mnuSincronizar)
    Me.mItem.MenuItems.Add(Me.mnuSincronizarCodes)
    Me.mItem.MenuItems.Add(Me.mnuEnviarDatos)
    Me.mItem.MenuItems.Add(Me.MenuItem3)
    Me.mItem.Text = "Menu"
    '
    'mnuCapturar
    '
    Me.mnuCapturar.Text = "Capturar"
    '
    'mnuRefrescar
    '
    Me.mnuRefrescar.Text = "Refrescar"
    '
    'mnuPropiedades
    '
    Me.mnuPropiedades.Text = "Propiedades"
    '
    'MenuItem1
    '
    Me.MenuItem1.Text = "-"
    '
    'mnuSincronizar
    '
    Me.mnuSincronizar.Text = "Sincronizar"
    '
    'mnuSincronizarCodes
    '
    Me.mnuSincronizarCodes.Text = "Sincronizar Códigos"
    '
    'mnuEnviarDatos
    '
    Me.mnuEnviarDatos.Text = "Enviar Datos"
    '
    'mnuSalir
    '
    Me.mnuSalir.Text = "Salir"
    '
    'cboNumero
    '
    Me.cboNumero.Location = New System.Drawing.Point(38, 3)
    Me.cboNumero.Name = "cboNumero"
    Me.cboNumero.Size = New System.Drawing.Size(52, 22)
    Me.cboNumero.TabIndex = 1
    '
    'LblFolio
    '
    Me.LblFolio.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Regular)
    Me.LblFolio.Location = New System.Drawing.Point(1, 4)
    Me.LblFolio.Name = "LblFolio"
    Me.LblFolio.Size = New System.Drawing.Size(31, 20)
    Me.LblFolio.Text = "Folio:"
    '
    'cboBodega
    '
    Me.cboBodega.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Regular)
    Me.cboBodega.Location = New System.Drawing.Point(94, 3)
    Me.cboBodega.Name = "cboBodega"
    Me.cboBodega.Size = New System.Drawing.Size(142, 20)
    Me.cboBodega.TabIndex = 4
    '
    'grdDetalle
    '
    Me.grdDetalle.BackgroundColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer))
    Me.grdDetalle.Location = New System.Drawing.Point(4, 29)
    Me.grdDetalle.Name = "grdDetalle"
    Me.grdDetalle.Size = New System.Drawing.Size(232, 236)
    Me.grdDetalle.TabIndex = 9
    '
    'ContextMenu1
    '
    Me.ContextMenu1.MenuItems.Add(Me.MenuItem2)
    '
    'MenuItem2
    '
    Me.MenuItem2.Text = "Lotes"
    '
    'MenuItem3
    '
    Me.MenuItem3.Text = "Enviar Codigo"
    '
    'frmPrincipal
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
    Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
    Me.AutoScroll = True
    Me.ClientSize = New System.Drawing.Size(240, 268)
    Me.Controls.Add(Me.grdDetalle)
    Me.Controls.Add(Me.cboBodega)
    Me.Controls.Add(Me.LblFolio)
    Me.Controls.Add(Me.cboNumero)
    Me.Menu = Me.mMenu
    Me.Name = "frmPrincipal"
    Me.Text = "Auditoria"
    Me.ResumeLayout(False)

  End Sub
  Friend WithEvents mItem As System.Windows.Forms.MenuItem
  Friend WithEvents mnuSalir As System.Windows.Forms.MenuItem
  Friend WithEvents cboNumero As System.Windows.Forms.ComboBox
  Friend WithEvents LblFolio As System.Windows.Forms.Label
  Friend WithEvents mnuCapturar As System.Windows.Forms.MenuItem
  Friend WithEvents mnuPropiedades As System.Windows.Forms.MenuItem
  Friend WithEvents mnuSincronizar As System.Windows.Forms.MenuItem
  Friend WithEvents cboBodega As System.Windows.Forms.ComboBox
  Friend WithEvents grdDetalle As System.Windows.Forms.DataGrid
  Friend WithEvents mnuRefrescar As System.Windows.Forms.MenuItem
  Friend WithEvents mnuEnviarDatos As System.Windows.Forms.MenuItem
  Friend WithEvents MenuItem1 As System.Windows.Forms.MenuItem
  Friend WithEvents mnuSincronizarCodes As System.Windows.Forms.MenuItem
  Friend WithEvents ContextMenu1 As System.Windows.Forms.ContextMenu
  Friend WithEvents MenuItem2 As System.Windows.Forms.MenuItem
  Friend WithEvents MenuItem3 As System.Windows.Forms.MenuItem

End Class
