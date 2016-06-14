Option Explicit On

Imports System.Net
Imports System.IO
Imports System.Text

Public Class frmPrincipal
  Public Id_Aud As Integer
  Public Id_Bodega As Integer
  Dim rdr As System.Data.SqlServerCe.SqlCeDataReader
  Dim adaptadorLocal As New System.Data.SqlServerCe.SqlCeDataAdapter
  Dim lsClave As String = "uind3|1j0fe2nlkjñcdsa9U0'E|31RO9JSD21JKÑXWQJ9XD31UIPXEWQC8JD|1WXIND1|309RE31|NCD3|2801|23"
  Public Sub New()

    ' This call is required by the Windows Form Designer.
    InitializeComponent()

    Llenar()
  End Sub

  Public Sub Llenar()
    RemoveHandler cboNumero.SelectedIndexChanged, AddressOf cboNumero_SelectedIndexChanged
    'LlenarComboBoxL(cboNumero, "Id_Aud", "Id_Aud")
    LlenarComboBox(cboNumero, "Id_Aud", "Id_Aud")
    AddHandler cboNumero.SelectedIndexChanged, AddressOf cboNumero_SelectedIndexChanged

    If cboNumero.Items.Count > 0 Then
      mnuCapturar.Enabled = True
      mnuPropiedades.Enabled = True
      mnuRefrescar.Enabled = True
      'mnuSincronizar.Enabled = True
      mnuSincronizarCodes.Enabled = True
      cboNumero.SelectedIndex = cboNumero.Items.Count - 1
    Else
      cboBodega.Enabled = False
      'grdDetalle.DataSource = Nothing
      mnuCapturar.Enabled = False
      mnuPropiedades.Enabled = False
      mnuRefrescar.Enabled = False
      mnuEnviarDatos.Enabled = False
      'mnuSincronizar.Enabled = False
      mnuSincronizarCodes.Enabled = False
    End If

  End Sub

  Private Sub frmPrincipal_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

  End Sub

  Private Sub mnuSalir_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuSalir.Click
    Me.Close()
  End Sub

  Private Sub mnuCapturar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuCapturar.Click
    frmCapturarLote.ShowDialog()
    LlenarGrid()
  End Sub

  Private Sub frmPrincipal_Activated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Activated
    If cnxLocal.State = Data.ConnectionState.Open Then
      'LlenarGrid()
    Else
      ConectarLocal()
    End If
  End Sub

  Private Sub cboNumero_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboNumero.SelectedIndexChanged
    Id_Aud = cboNumero.SelectedValue.ToString

    RemoveHandler cboBodega.SelectedIndexChanged, AddressOf cboBodega_SelectedIndexChanged
    LlenarComboBoxB(cboBodega, "Bodega", "Id_Bodega", Id_Aud)
    AddHandler cboBodega.SelectedIndexChanged, AddressOf cboBodega_SelectedIndexChanged

    If cboBodega.Items.Count > 0 Then
      mnuCapturar.Enabled = True
      mnuPropiedades.Enabled = True
      mnuEnviarDatos.Enabled = True
      'mnuSincronizar.Enabled = True
      mnuSincronizarCodes.Enabled = True
      cboBodega.SelectedIndex = cboBodega.Items.Count - 1
    Else
      mnuCapturar.Enabled = False
      mnuPropiedades.Enabled = False
      mnuEnviarDatos.Enabled = False
      'mnuSincronizar.Enabled = False
      mnuSincronizarCodes.Enabled = False
      grdDetalle.DataSource = Nothing
    End If
  End Sub

  Private Sub cboBodega_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboBodega.SelectedIndexChanged
    LlenarGrid()
  End Sub

  Public Sub LlenarGrid()
    Dim lsQuery As String
    Dim ldatDataset1 As New System.Data.DataSet
    Dim iLimite As Integer 'i,
    Dim Estilos1 As New DataGridTableStyle
    Dim Estilo1, Estilo2, Estilo3, Estilo4, Estilo5, Estilo6 As New DataGridTextBoxColumn

    Id_Aud = cboNumero.SelectedValue.ToString
    grdDetalle.TableStyles.Clear()

    lsQuery = "SELECT * FROM proArticulosAuditoria WHERE Id_Aud=" & Id_Aud & " AND Existencia > 0 OR Ajuste > 0 Order By Nombre"
    cmdComandoLocal = New System.Data.SqlServerCe.SqlCeCommand(lsQuery, cnxLocal)

    Dim adaptadorLocal As New System.Data.SqlServerCe.SqlCeDataAdapter(cmdComandoLocal)
    adaptadorLocal.Fill(ldatDataset1)
    iLimite = ldatDataset1.Tables(0).Rows.Count

    If iLimite > 0 Then

      Estilos1.MappingName = ldatDataset1.Tables(0).TableName

      With Estilo1
        .MappingName = "Id_Aud"
        .HeaderText = "Aud"
        .Width = 0
      End With
      With Estilo2
        .MappingName = "Id_Art"
        .HeaderText = "Art"
        .Width = 0
      End With
      With Estilo3
        .MappingName = "Nombre"
        .HeaderText = "Nombre"
        .Width = 147
      End With
      With Estilo4
        .MappingName = "Existencia"
        .HeaderText = "Existencia"
        .Width = 0
      End With
      With Estilo5
        .MappingName = "Cantidad"
        .HeaderText = "Cantidad"
        .Width = 60
      End With
      With Estilo6
        .MappingName = "Ajuste"
        .HeaderText = "Ajuste"
        .Width = 0
      End With
      Estilos1.GridColumnStyles.Add(Estilo1)
      Estilos1.GridColumnStyles.Add(Estilo2)
      Estilos1.GridColumnStyles.Add(Estilo3)
      Estilos1.GridColumnStyles.Add(Estilo4)
      Estilos1.GridColumnStyles.Add(Estilo5)
      Estilos1.GridColumnStyles.Add(Estilo6)

      grdDetalle.TableStyles.Add(Estilos1)
      grdDetalle.RowHeadersVisible = False

      grdDetalle.DataSource = ldatDataset1.Tables(0)
      mnuSincronizar.Enabled = False

    End If

  End Sub

  Private Sub mnuSincronizar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuSincronizar.Click
    Sincronizar()
    Llenar()
    mnuSincronizar.Enabled = False
  End Sub

  'Private Sub MenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
  'ProArticulosAuditoriaTableAdapter1.InsertQuery(1, 500, "Biofol", 200, 0, 0, "2014-09-01", "12:50", 1, "Quimica Sagal", 15, "Tapachula", 1, "Admin", "Capturado", 1)
  'End Sub

  Private Sub mnuPropiedades_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuPropiedades.Click
    Dim lsFechaInicio, lsHoraInicio, lsRealiza, lsEstatus, lsBodega As String
    Dim lsCondicion As String
    Dim lsMensaje As String
    Id_Aud = cboNumero.SelectedValue.ToString
    Id_Bodega = cboBodega.SelectedValue.ToString
    lsCondicion = "Id_Aud=" & Id_Aud & " And Id_Bodega=" & Id_Bodega

    lsFechaInicio = ObtenerValorLocal("proArticulosAuditoria", "FechaInicio", lsCondicion)
    lsHoraInicio = ObtenerValorLocal("proArticulosAuditoria", "HoraInicio", lsCondicion)
    lsRealiza = ObtenerValorLocal("proArticulosAuditoria", "Realizo", lsCondicion)
    lsEstatus = ObtenerValorLocal("proArticulosAuditoria", "Estatus", lsCondicion)
    lsBodega = ObtenerValorLocal("proArticulosAuditoria", "Bodega", lsCondicion)
    'lsMensaje = "Fecha: " & lsFechaInicio & vbCrLf & "Hora: " & lsHoraInicio & vbCrLf & "Estatus: " & lsEstatus & vbCrLf & "Realiza: " & lsRealiza & vbCrLf & "Bodega: " & lsBodega
    lsMensaje = "Fecha: " & lsFechaInicio & " - " & lsHoraInicio & vbCrLf & "Estatus: " & lsEstatus & vbCrLf & "Realiza: " & lsRealiza & vbCrLf & "Bodega: " & lsBodega
    MsgBox(lsMensaje, MsgBoxStyle.Information)

  End Sub

  Private Sub mnuRefrescar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuRefrescar.Click
    'Dim lsUsuario, lsContraseña, lsSisUsuario, lsSisContraseña As String
    LlenarGrid()
  End Sub

  Private Sub mnuEnviarDatos_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuEnviarDatos.Click
    Dim lsQuery, lsCondicion As String
    Dim ldatDataset1 As New System.Data.DataSet
    Dim Id_Aud, Id_Art, Id_Lte, Cantidad, i, iLimite As Integer
    Dim lsQuery1, lsCantidad As String
    Dim lsId_Lote As String
    Cursor.Current = Cursors.WaitCursor
    Conectar()

    lsCondicion = "Id_Aud=" & cboNumero.Text & " AND Id_Est=1"
    If ObtenerValorRemoto("proAuditoria", "Id_Est", "Id_Est", lsCondicion) = "proAuditoria" Then
      MsgBox("Auditoria Cerrada o Cancelada", MsgBoxStyle.Critical, "Error")
      Exit Sub
    End If

    lsQuery = "SELECT * FROM proArticuloAuditoriaLote WHERE Id_Aud=" & cboNumero.Text
    cmdComandoLocal = New System.Data.SqlServerCe.SqlCeCommand(lsQuery, cnxLocal)

    Dim adaptadorLocal As New System.Data.SqlServerCe.SqlCeDataAdapter(cmdComandoLocal)
    adaptadorLocal.Fill(ldatDataset1)
    iLimite = ldatDataset1.Tables(0).Rows.Count

    If iLimite > 0 Then
      lsQuery = "INSERT INTO proArticuloAuditoriaLote(Id_Aud, Id_Art, Id_Lte, Cantidad) VALUES"
      For i = 0 To iLimite - 1
        Id_Aud = ldatDataset1.Tables(0).Rows(i).Item(0).ToString
        Id_Art = ldatDataset1.Tables(0).Rows(i).Item(1).ToString
        Id_Lte = ldatDataset1.Tables(0).Rows(i).Item(2).ToString
        Cantidad = ldatDataset1.Tables(0).Rows(i).Item(3).ToString
        lsCondicion = "Id_Aud=" & Id_Aud & " AND Id_Art=" & Id_Art & " AND Id_Lte=" & Id_Lte
        lsId_Lote = Id_Lte
        'If lsId_Lote = "1111" Or lsId_Lote = "0" Then
        'Else
        '  lsId_Lote = ObtenerValorRemoto("proExistenciasAlmcLotes", "Id_Art", "Id_Art", "Id_Art=" & Id_Art & " AND Id_Lte=" & Id_Lte)
        'End If

        'If lsId_Lote = "proExistenciasAlmcLotes" Then
        '  Dim producto = ObtenerValorLocal("proArticulosAuditoria", "Nombre", "Id_Aud=" & Id_Aud & " AND Id_Art=" & Id_Art)
        '  MsgBox("El lote: " & Id_Lte & " del producto: " & producto & " no fue encontrado")
        'Else
        lsCantidad = ObtenerValorRemoto("proArticuloAuditoriaLote", "Cantidad", "Cantidad", lsCondicion)
        If lsCantidad = "proArticuloAuditoriaLote" Then
          'If i = iLimite - 1 Then '- 1
          '  lsQuery = lsQuery + "(" & Id_Aud & "," & Id_Art & "," & Id_Lte & "," & Cantidad & ");"
          'Else
          lsQuery = lsQuery + "(" & Id_Aud & "," & Id_Art & "," & Id_Lte & "," & Cantidad & "), "
          'End If
        Else
          If lsCantidad = Cantidad Then
          Else
            lsQuery1 = "UPDATE proArticuloAuditoriaLote SET Cantidad=" & Cantidad & " WHERE " & lsCondicion
            cmdComando.CommandText = lsQuery1
            cmdComando.ExecuteNonQuery()
          End If
        End If
        'End If
      Next
      Cursor.Current = Cursors.Default
      'valor = lsQuery
      If lsQuery = "INSERT INTO proArticuloAuditoriaLote(Id_Aud, Id_Art, Id_Lte, Cantidad) VALUES" Then
        MsgBox("Sin cambios que realizar", MsgBoxStyle.Information, )
        CerrarAuditoria()
      Else
        lsQuery1 = lsQuery.Substring(0, lsQuery.Length - 2)
        lsQuery = lsQuery1 + ";"
        cmdComando.CommandText = lsQuery
        cmdComando.ExecuteNonQuery()
        MsgBox("Completado", MsgBoxStyle.Information, )
        CerrarAuditoria(Id_Aud)
      End If

    Else
      Cursor.Current = Cursors.Default
      CerrarAuditoria()
    End If
  End Sub

  Public Function EncryptString(ByVal UserKey As String, ByVal Text As String, ByVal Action As Single) As String

    Dim UserKeyASCIIS() As Integer
    Dim TextASCIIS() As Integer
    Dim i As Integer
    Dim j As Integer
    Dim Temp As Integer
    Dim n As Integer
    Dim rtn As String = ""

    '//Get UserKey characters
    n = Len(UserKey)
    ReDim UserKeyASCIIS(0 To n)
    For i = 1 To n
      UserKeyASCIIS(i) = Asc(Mid$(UserKey, i, 1))
    Next

    '//Get Text characters
    ReDim TextASCIIS(Len(Text))
    For i = 1 To Len(Text)
      TextASCIIS(i) = Asc(Mid$(Text, i, 1))
    Next

    '//Encryption/Decryption
    If Action = 1 Then
      For i = 1 To Len(Text)
        j = IIf(j + 1 >= n, 1, j + 1)
        Temp = TextASCIIS(i) + UserKeyASCIIS(j)
        If Temp > 255 Then
          Temp = Temp - 255
        End If
        rtn = rtn + Chr(Temp)
      Next
    ElseIf Action = 2 Then
      For i = 1 To Len(Text)
        j = IIf(j + 1 >= n, 1, j + 1)
        Temp = TextASCIIS(i) - UserKeyASCIIS(j)
        If Temp < 0 Then
          Temp = Temp + 255
        End If
        rtn = rtn + Chr(Temp)
      Next
    End If

    '//Return
    EncryptString = rtn

  End Function
  Private Sub CerrarAuditoria(Optional ByVal psAuditoria As String = "1")
    Dim lsQuery, lsCondicion As String
    Dim lsUsuario, lsContraseña, lsSisUsuario, lsSisContraseña As String
    Dim response As MsgBoxResult
    Dim lsFecha As String = Date.Now.Year.ToString & "-" & Date.Now.Month.ToString & "-" & Date.Now.Day.ToString
    Dim lsHora As String = Now.TimeOfDay.Hours & ":" & Now.TimeOfDay.Minutes & ":" & Now.TimeOfDay.Seconds

    response = MsgBox("Deseas continuar con la Auditoria?", MsgBoxStyle.YesNo, )
    If response = MsgBoxResult.Yes Then   ' User chose Yes.
      ' Perform some action.
    Else
      response = MsgBox("Todos los datos seran borrados, deseas continuar?", MsgBoxStyle.YesNo, )
      If response = MsgBoxResult.Yes Then
        frmSesion.ShowDialog()

        lsUsuario = frmSesion.lblUser.Text
        lsContraseña = frmSesion.lblPass.Text

        If lsUsuario <> "" And lsContraseña <> "" Then
          lsSisUsuario = ObtenerValorRemoto("sisUsuario", "password", "password", "clave='" & lsUsuario & "'")
          If lsSisUsuario <> "sisUsuario" Then
            lsSisContraseña = EncryptString(lsClave, lsSisUsuario, 2)
            If lsContraseña = lsSisContraseña Then
              'MsgBox(lsContraseña & " - " & lsSisContraseña)
              lsQuery = "DELETE FROM proArticulosAuditoria"
              cmdComandoLocal.CommandText = lsQuery
              cmdComandoLocal.ExecuteNonQuery()
              lsQuery = "DELETE FROM proArticuloEmpaque"
              cmdComandoLocal.CommandText = lsQuery
              cmdComandoLocal.ExecuteNonQuery()
              lsQuery = "DELETE FROM proArticuloAuditoriaLote"
              cmdComandoLocal.CommandText = lsQuery
              cmdComandoLocal.ExecuteNonQuery()
              '----
              If psAuditoria = "1" Then
              Else
                lsSisUsuario = ObtenerValorRemoto("sisUsuario", "id_usr", "id_usr", "clave='" & lsUsuario & "'") '- Id_usr
                lsCondicion = "WHERE Id_Aud=" & psAuditoria
                lsQuery = "UPDATE proAuditoria SET Id_Est=2, Id_UsrAut= " & lsSisUsuario & ", FechaFin='" & lsFecha & "', HoraFin='" & lsHora & "'  WHERE " & lsCondicion
                cmdComando.CommandText = lsQuery
                cmdComando.ExecuteNonQuery()
              End If
              '----
              MsgBox("Toda los datos han sido borrados con éxito, la aplicación se cerrará", MsgBoxStyle.Information)
              Me.Close()
            Else
              'MsgBox(lsContraseña & " - " & lsSisContraseña)
              MsgBox("La contraseña no es correcta", MsgBoxStyle.Exclamation)
            End If
          Else
            MsgBox("Usuario no encontrado", MsgBoxStyle.Exclamation)
          End If
        Else
          MsgBox("No se ingreso usuario o contraseña", MsgBoxStyle.Exclamation)
        End If
      Else ' en caso de que elija no borrar datos
      End If

    End If
    'response = MsgBox("Todos los datos seran borrados, deseas continuar?", MsgBoxStyle.YesNo, )
    'If response = MsgBoxResult.Yes Then
    '  lsQuery = "DELETE FROM proArticulosAuditoria"
    '  cmdComandoLocal.CommandText = lsQuery
    '  cmdComandoLocal.ExecuteNonQuery()
    '  lsQuery = "DELETE FROM proArticuloEmpaque"
    '  cmdComandoLocal.CommandText = lsQuery
    '  cmdComandoLocal.ExecuteNonQuery()
    '  lsQuery = "DELETE FROM proArticuloAuditoriaLote"
    '  cmdComandoLocal.CommandText = lsQuery
    '  cmdComandoLocal.ExecuteNonQuery()
    '  Me.Close()
    'Else
    'End If
    ' Get URL and proxy
    ' from the text boxes.
  End Sub

  Private Sub mnuSincronizarCodes_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuSincronizarCodes.Click
    Dim lsQuery, lsQuery1 As String
    Dim ldatDataSet As New System.Data.DataSet()
    Dim liEmpaque As Integer
    Dim datDataAdapter1 As New Devart.Data.PostgreSql.PgSqlDataAdapter
    Dim lsId_Mvm, lsId_Art, lsId_Prst, lsCantidad, lsCodigo As String

    'If cnxConexion.State = Data.ConnectionState.Open Then
    'Else
    Conectar()
    'End If
    MsgBox("Asegurate de que la terminal este conectada", MsgBoxStyle.Exclamation, "Atención")
    Cursor.Current = Cursors.WaitCursor

    Dim cmdComandoLocal As New System.Data.SqlServerCe.SqlCeCommand()
    cmdComandoLocal.CommandText = "DELETE FROM proArticuloEmpaque"
    cmdComandoLocal.Connection = cnxLocal
    cmdComandoLocal.ExecuteNonQuery()

    lsQuery = "SELECT * FROM proArticuloEmpaque Order By Id_Art" ' WHERE Id_Est="
    cmdComando1.CommandText = lsQuery
    datDataAdapter1.SelectCommand = cmdComando1

    datDataAdapter1.Fill(ldatDataSet, "proArticuloEmpaque")
    liEmpaque = ldatDataSet.Tables("proArticuloEmpaque").Rows.Count()

    If liEmpaque > 0 Then
      For i = 0 To liEmpaque - 1
        lsId_Mvm = ldatDataSet.Tables("proArticuloEmpaque").Rows(i).Item("Id_Mvm").ToString()
        lsId_Art = ldatDataSet.Tables("proArticuloEmpaque").Rows(i).Item("Id_Art").ToString()
        lsId_Prst = ldatDataSet.Tables("proArticuloEmpaque").Rows(i).Item("Id_Prst").ToString()
        lsCantidad = ldatDataSet.Tables("proArticuloEmpaque").Rows(i).Item("Cantidad").ToString()
        lsCodigo = ldatDataSet.Tables("proArticuloEmpaque").Rows(i).Item("Codigo").ToString()
        lsQuery1 = "INSERT INTO proArticuloEmpaque (Id_Mvm, Id_Art, Id_Prst, Cantidad, Codigo) "
        lsQuery1 = lsQuery1 & "VALUES (" & lsId_Mvm & ", " & lsId_Art & ",'" & lsId_Prst & "', " & lsCantidad & ",'" & lsCodigo & "')"
        cmdComandoLocal.CommandText = lsQuery1
        cmdComandoLocal.ExecuteNonQuery()
      Next
      Cursor.Current = Cursors.Default
      MsgBox("Códigos de Barras Actualizados", MsgBoxStyle.Information, "Completado")
    Else

    End If
  End Sub

  Private Sub ContextMenu1_Popup(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ContextMenu1.Popup
    Dim lsCondicion
    Dim lsQuery, lsLote, lsCantidad As String
    Dim mnuItemLte As New MenuItem()
    Dim ldatDataSet1 As New System.Data.DataSet()
    Dim liArticulos As Integer

    If ContextMenu1.MenuItems.Count > 0 Then ContextMenu1.MenuItems.Clear()

    Id_Aud = cboNumero.SelectedValue.ToString
    Id_Bodega = cboBodega.SelectedValue.ToString
    lsCondicion = "Id_Aud=" & Id_Aud & " AND Id_Art=" & grdDetalle.Item(grdDetalle.CurrentCell.RowNumber, 1).ToString()

    If grdDetalle.Item(grdDetalle.CurrentCell.RowNumber, 4).ToString() <> "0" Then
      lsQuery = "SELECT * FROM proArticuloAuditoriaLote WHERE " & lsCondicion
      cmdComandoLocal = New System.Data.SqlServerCe.SqlCeCommand(lsQuery, cnxLocal)
      adaptadorLocal.SelectCommand = cmdComandoLocal
      adaptadorLocal.Fill(ldatDataSet1)
      liArticulos = ldatDataSet1.Tables(0).Rows.Count()
      mnuItemLte.Text = "Lotes"
      ContextMenu1.MenuItems.Add(mnuItemLte)
      For i = 0 To liArticulos - 1
        lsLote = ldatDataSet1.Tables(0).Rows(i).Item("Id_Lte").ToString()
        lsCantidad = ldatDataSet1.Tables(0).Rows(i).Item("Cantidad").ToString()
        Dim mnuItemNew As New MenuItem()
        mnuItemNew.Text = lsLote & " - " & lsCantidad
        ContextMenu1.MenuItems.Add(mnuItemNew)
      Next
    End If
  End Sub

  Private Sub grdDetalle_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles grdDetalle.DoubleClick
    ContextMenu1.Show(grdDetalle, New System.Drawing.Point(45, grdDetalle.GetCellBounds(grdDetalle.CurrentCell.RowNumber, grdDetalle.CurrentCell.ColumnNumber).Y))
  End Sub

  Private Sub MenuItem3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItem3.Click
    Dim lsValor, lsValor1 As String
    lsValor = EncryptString(lsClave, "default", 1)
    MsgBox(lsValor)
    lsValor1 = EncryptString(lsClave, lsValor, 2)
    MsgBox(lsValor1)
    'Dim lsQuery As String
    'Dim lsValores As String = ""
    'Conectar()
    'lsQuery = "INSERT INTO proCodigoAsciiVb(Id, Codigo) VALUES "
    'For i = 1 To 255
    '  If i = 39 Then
    '    lsValores = lsValores + "(" & i & " , '" & Chr(i) & "''), "
    '  Else
    '    lsValores = lsValores + "(" & i & " , '" & Chr(i) & "'), "
    '  End If

    'Next
    'lsValores = lsValores.Substring(0, lsValores.Length - 2)
    'lsQuery = lsQuery + lsValores + ";"
    'cmdComando.CommandText = lsQuery
    'cmdComando.ExecuteNonQuery()
    'MsgBox("Completado", MsgBoxStyle.Information, )

  End Sub
End Class
