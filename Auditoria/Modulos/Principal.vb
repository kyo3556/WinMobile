Option Explicit On

Imports System.Data

Imports Devart.Data.PostgreSql
Imports System.Net
Imports System.IO

Module Principal
  'Dim adaptadorVista As New Auditoria.dbAuditoriaDataSetTableAdapters.proArticulosAuditoriaTableAdapter
  'Dim adaptadorVista1 As New Auditoria.dbAuditoriaDataSetTableAdapters.proArticuloEmpaqueTableAdapter
  Public cmdComando, cmdComando1 As New PgSqlCommand
  Public cnxConexion As PgSqlConnection
  Public datDataAdapter As New PgSqlDataAdapter
  Public datDataAdapter1 As New PgSqlDataAdapter

  Public cnxLocal As New System.Data.SqlServerCe.SqlCeConnection
  Public cmdComandoLocal As New System.Data.SqlServerCe.SqlCeCommand
  Public adaptadorLocal As New System.Data.SqlServerCe.SqlCeDataAdapter



  Public Sub Main()
    'Dim path1 As String = Path.GetFullPath("\Archivos de programa\Auditoria")
    Try
      'Dim Source As String = "\Archivos de programa\Auditoria\DbInterna\dbAuditoria.sdf"
      'Dim Destination As String = "\Archivos de programa\Auditoria\dbAuditoria.sdf"
      'System.IO.File.Copy(Source, Destination, True)
      frmPrincipal.ShowDialog()
    Catch ex As Exception
      MsgBox("Error: " & ex.Message, MsgBoxStyle.Critical, "Error")

    End Try

  End Sub
 
  Public Sub LlenarComboBox(ByVal pcCombo As ComboBox, ByVal psMostrar As String, ByVal psEnlace As String)
    Dim ldatDataSet As New System.Data.DataSet
    Dim i As Integer
    Dim lsQuery As String

    pcCombo.DataSource = Nothing
    lsQuery = "SELECT DISTINCT Id_Aud FROM proArticulosAuditoria WHERE Id_Est = 1"

    If cnxLocal.State <> ConnectionState.Open Then
      ConectarLocal()
    End If

    cmdComandoLocal = New System.Data.SqlServerCe.SqlCeCommand(lsQuery, cnxLocal)

    Dim adaptadorLocal As New System.Data.SqlServerCe.SqlCeDataAdapter(cmdComandoLocal)
    adaptadorLocal.Fill(ldatDataSet)
    i = ldatDataSet.Tables(0).Rows.Count

    If i > 0 Then
      pcCombo.DataSource = ldatDataSet.Tables(0)
      pcCombo.DisplayMember = ldatDataSet.Tables(0).Columns(psMostrar).ToString
      pcCombo.ValueMember = ldatDataSet.Tables(0).Columns(psEnlace).ToString

      pcCombo.SelectedIndex = -1
    Else
      MsgBox("Es necesario sincronizar, error: no se lleno cboNumero", MsgBoxStyle.Exclamation, "Atención")
    End If

  End Sub

  Public Sub LlenarComboBoxB(ByVal pcCombo As ComboBox, ByVal psMostrar As String, ByVal psEnlace As String, ByVal piAuditoria As Integer)
    Dim ldatDataSet As New System.Data.DataSet
    Dim i As Integer
    Dim lsQuery As String

    pcCombo.DataSource = Nothing
    lsQuery = "SELECT DISTINCT Id_Bodega, Bodega FROM proArticulosAuditoria WHERE Id_Aud =" & piAuditoria

    If cnxLocal.State <> ConnectionState.Open Then
      ConectarLocal()
    End If

    cmdComandoLocal = New System.Data.SqlServerCe.SqlCeCommand(lsQuery, cnxLocal)

    Dim adaptadorLocal As New System.Data.SqlServerCe.SqlCeDataAdapter(cmdComandoLocal)
    adaptadorLocal.Fill(ldatDataSet)
    i = ldatDataSet.Tables(0).Rows.Count

    If i > 0 Then
      pcCombo.DataSource = ldatDataSet.Tables(0)
      pcCombo.DisplayMember = ldatDataSet.Tables(0).Columns(psMostrar).ToString
      pcCombo.ValueMember = ldatDataSet.Tables(0).Columns(psEnlace).ToString

      pcCombo.SelectedIndex = -1
    Else
      MsgBox("Es necesario sincronizar, error: no se lleno cboBodega", MsgBoxStyle.Exclamation, "Atención")
    End If

  End Sub
  Public Sub Conectar()
    cmdComando = New PgSqlCommand
    cmdComando1 = New PgSqlCommand
    cnxConexion = New PgSqlConnection
    Try
      With cnxConexion
        .UserId = "admin"
        .Password = "mreloaded"
        .Host = "25.215.148.18" '"" '"10.10.10.3" ' '
        .Port = "5433"
        .Database = "sgl" '"sgl"
        .Schema = "public"
        .Unicode = True
        .Open()
      End With

      If cnxConexion.State Then
        cmdComando.Connection = cnxConexion
        cmdComando1.Connection = cnxConexion
        'frmPrincipal.ShowDialog()
      Else
        MsgBox("No fue posible establecer la conexión", MsgBoxStyle.Critical, "Error Conexión")
      End If
    Catch ex As Exception
      MsgBox("Error: " & ex.Message, MsgBoxStyle.Critical, "Error")
    End Try

  End Sub

  Public Sub Sincronizar()
    Dim lsQuery, lsQuery1 As String
    Dim id_aud, id_art, id_bodega, id_usrrlz, id_est, empaque As Integer
    Dim id_mvm, id_art1, id_prst As Integer
    Dim codigo As String
    Dim existencia, ajuste, cantidad, cantidad1 As Double
    Dim fechainicio, horainicio As String
    Dim nombre, usuario, estatus, bodega As String
    Dim ldatDataSet, ldatDataSet1, ldatDataSet2 As New System.Data.DataSet()
    Dim liArticulo, i, a, liAuditoria, liEmpaque As Integer

    'ConectarLocal()
    'cmdComandoLocal.CommandText = "DELETE FROM proArticulosAuditoria"
    'cmdComandoLocal.ExecuteNonQuery()

    Conectar()
    lsQuery1 = "SELECT * FROM proArticulosAuditoria"
    cmdComandoLocal = New System.Data.SqlServerCe.SqlCeCommand(lsQuery1, cnxLocal)

    adaptadorLocal.SelectCommand = cmdComandoLocal
    adaptadorLocal.Fill(ldatDataSet1)
    liAuditoria = ldatDataSet1.Tables(0).Rows.Count()

    If liAuditoria > 0 Then
      lsQuery = "SELECT * FROM proArticulosAuditoria WHERE Id_Est=1"
      cmdComando.CommandText = lsQuery
      datDataAdapter.SelectCommand = cmdComando

      datDataAdapter.Fill(ldatDataSet, "proArticulosAuditoria")
      liArticulo = ldatDataSet.Tables(0).Rows.Count()


      If liArticulo = liAuditoria Then

      Else

      End If

    Else

      lsQuery1 = "SELECT * FROM proArticuloEmpaque Order By Id_Art" ' WHERE Id_Est="
      cmdComando1.CommandText = lsQuery1
      datDataAdapter1.SelectCommand = cmdComando1

      datDataAdapter1.Fill(ldatDataSet2, "proArticuloEmpaque")
      liEmpaque = ldatDataSet2.Tables("proArticuloEmpaque").Rows.Count()

      lsQuery = "SELECT * FROM proArticulosAuditoria WHERE Id_Est=1 Order By Id_Art"
      cmdComando.CommandText = lsQuery
      datDataAdapter.SelectCommand = cmdComando

      datDataAdapter.Fill(ldatDataSet, "proArticulosAuditoria")
      liArticulo = ldatDataSet.Tables(0).Rows.Count()

      If liArticulo > 0 Then 'And liEmpaque > 0
        Cursor.Current = Cursors.WaitCursor

        For i = 0 To liArticulo - 1
          id_aud = ldatDataSet.Tables(0).Rows(i).Item("Id_Aud").ToString()
          id_art = ldatDataSet.Tables(0).Rows(i).Item("Id_Art").ToString()
          nombre = ldatDataSet.Tables(0).Rows(i).Item("Nombre").ToString()
          existencia = CDbl(ldatDataSet.Tables(0).Rows(i).Item("Existencia").ToString())
          cantidad = 0
          ajuste = 0
          fechainicio = FormatDateTime(ldatDataSet.Tables(0).Rows(i).Item("FechaInicio").ToString(), DateFormat.ShortDate)
          horainicio = FormatDateTime(ldatDataSet.Tables(0).Rows(i).Item("HoraInicio").ToString(), DateFormat.ShortTime)
          id_bodega = ldatDataSet.Tables(0).Rows(i).Item("Id_Bodega").ToString()
          bodega = ldatDataSet.Tables(0).Rows(i).Item("Bodega").ToString()
          id_usrrlz = ldatDataSet.Tables(0).Rows(i).Item("Id_UsrRlz").ToString()
          usuario = ldatDataSet.Tables(0).Rows(i).Item("Realizo").ToString()
          id_est = ldatDataSet.Tables(0).Rows(i).Item("Id_Est").ToString()
          estatus = ldatDataSet.Tables(0).Rows(i).Item("Estatus").ToString()
          empaque = ldatDataSet.Tables(0).Rows(i).Item("Empaque").ToString()
          'adaptadorVista.InsertQuery(id_aud, id_art, nombre, existencia, 0, 0, fechainicio, horainicio, bodega, id_usrrlz, estatus, id_est, empaque, id_bodega, usuario)
          lsQuery1 = "INSERT INTO proArticulosAuditoria (Id_Aud, Id_Art, Nombre, Existencia, Cantidad, Ajuste, FechaInicio, HoraInicio, Bodega, Id_UsrRlz, Estatus, Id_Est, Empaque, Id_Bodega, Realizo) VALUES "
          lsQuery1 = lsQuery1 & "(" & id_aud & ", " & id_art & ", '" & nombre & "' , " & existencia & ", " & cantidad & ", " & ajuste & " , '" & fechainicio & "' , '" & horainicio & "' , '" & bodega & "', " & id_usrrlz & ", '" & estatus & "' , " & id_est & ", " & empaque & " , " & id_bodega & ", '" & usuario & "')" '+ ", "
          cmdComandoLocal.CommandText = lsQuery1
          cmdComandoLocal.ExecuteNonQuery()
        Next
        'If i = liArticulo Then
        '  lsQuery1 = lsQuery1.Substring(0, lsQuery1.Length - 2) '+ ";"
        '  cmdComandoLocal.CommandText = lsQuery1
        '  cmdComandoLocal.ExecuteNonQuery()
        'End If

        a = 0

        If liEmpaque > 0 Then
          For a = 0 To liEmpaque - 1
            id_mvm = ldatDataSet2.Tables("proArticuloEmpaque").Rows(a).Item("Id_Mvm").ToString()
            id_art1 = ldatDataSet2.Tables("proArticuloEmpaque").Rows(a).Item("Id_Art").ToString()
            id_prst = ldatDataSet2.Tables("proArticuloEmpaque").Rows(a).Item("Id_Prst").ToString()
            cantidad1 = ldatDataSet2.Tables("proArticuloEmpaque").Rows(a).Item("Cantidad").ToString()
            codigo = ldatDataSet2.Tables("proArticuloEmpaque").Rows(a).Item("Codigo").ToString()
            lsQuery1 = "INSERT INTO proArticuloEmpaque (Id_Mvm, Id_Art, Id_Prst, Cantidad, Codigo) VALUES "
            lsQuery1 = lsQuery1 & "(" & id_mvm & ", " & id_art1 & ", " & id_prst & " , " & cantidad1 & ", '" & codigo & "')" '+ ", "
            cmdComandoLocal.CommandText = lsQuery1
            cmdComandoLocal.ExecuteNonQuery()
          Next
        End If
        'If a = liEmpaque Then
        '  lsQuery1 = lsQuery1.Substring(0, lsQuery1.Length - 2) '+ ";"
        '  cmdComandoLocal.CommandText = lsQuery1
        '  cmdComandoLocal.ExecuteNonQuery()
        'End If

        If i = liArticulo Then
          MsgBox("Tarea completada", MsgBoxStyle.Information, "Sincronizar")
          Cursor.Current = Cursors.Default
          frmPrincipal.mnuSincronizar.Enabled = False
          frmPrincipal.mnuSincronizarCodes.Enabled = False
          frmPrincipal.mnuEnviarDatos.Enabled = True
        Else
          MsgBox("Error: No Se Completo Tarea", MsgBoxStyle.Critical, "Sincronizar")
          Cursor.Current = Cursors.Default
          frmPrincipal.mnuSincronizar.Enabled = True
          frmPrincipal.mnuSincronizarCodes.Enabled = False
          frmPrincipal.mnuEnviarDatos.Enabled = False
        End If

      Else

      End If
    End If

  End Sub

  Public Sub ConectarLocal()
    cnxLocal = New System.Data.SqlServerCe.SqlCeConnection
    cmdComandoLocal = New System.Data.SqlServerCe.SqlCeCommand

    Try
      cnxLocal.ConnectionString = "Data Source=" + System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase) + "\\dbAuditoria.sdf;Persist Security Info=False;"
      '"Data Source=dbAuditoria.sdf;Persist Security Info=False;" '"Data Source=\Archivos de programa\auditoria\dbAuditoria.sdf;Persist Security Info=False;"
      cnxLocal.Open()

      If cnxLocal.State Then
        cmdComandoLocal.Connection = cnxLocal
      Else
        MsgBox("No fue posible establecer la conexión", MsgBoxStyle.Critical, "Error Conexión")
      End If
    Catch ex As Exception
      MsgBox("Error:" & ex.Message)
    End Try
    
  End Sub

  Public Function ObtenerValorLocal(ByVal psTabla As String, ByVal psCampo As String, ByVal psCondicion As String) As String
    Dim ldatDataset As New System.Data.DataSet
    Dim lsQuery As String
    Dim lsValor As String = psTabla
    Dim i As Integer
    'cmdComandoLocal = New SqlServerCe.SqlCeCommand

    If psCondicion <> "" Then
      lsQuery = "SELECT " & psCampo & " FROM " & psTabla & " WHERE " & psCondicion
    Else
      lsQuery = "SELECT " & psCampo & " FROM " & psTabla
    End If

    cmdComandoLocal.Connection = cnxLocal
    cmdComandoLocal.CommandText = lsQuery
    Dim adaptadorLocal1 As New SqlServerCe.SqlCeDataAdapter(cmdComandoLocal)
    adaptadorLocal1.Fill(ldatDataset)
    i = ldatDataset.Tables(0).Rows.Count

    If i > 0 Then
      lsValor = ldatDataset.Tables(0).Rows(0).Item(psCampo).ToString
    Else
      lsValor = psTabla
    End If

    ObtenerValorLocal = lsValor
  End Function

  Public Function ObtenerValorLocalP(ByVal psTabla As String, ByVal psCampoValor As String, ByVal psCampoMostrar As String, ByVal psCondicion As String) As String
    Dim ldatDataset As New System.Data.DataSet
    Dim lsQuery As String
    Dim lsValor As String = psTabla
    Dim i As Integer
    'cmdComandoLocal = New SqlServerCe.SqlCeCommand

    If psCondicion <> "" Then
      lsQuery = "SELECT " & psCampoValor & " FROM " & psTabla & " WHERE " & psCondicion
    Else
      lsQuery = "SELECT " & psCampoValor & " FROM " & psTabla
    End If

    cmdComandoLocal.Connection = cnxLocal
    cmdComandoLocal.CommandText = lsQuery
    Dim adaptadorLocal1 As New SqlServerCe.SqlCeDataAdapter(cmdComandoLocal)
    adaptadorLocal1.Fill(ldatDataset)
    i = ldatDataset.Tables(0).Rows.Count

    If i > 0 Then
      lsValor = ldatDataset.Tables(0).Rows(0).Item(psCampoMostrar).ToString
    Else
      lsValor = psTabla
    End If

    ObtenerValorLocalP = lsValor
  End Function
  Public Function ObtenerValorRemoto(ByVal psTabla As String, ByVal psCampoValor As String, ByVal psCampoMostrar As String, ByVal psCondicion As String) As String
    Dim ldatDataset As New System.Data.DataSet
    Dim lsQuery As String
    Dim lsValor As String = psTabla
    Dim i As Integer
    'cmdComandoLocal = New SqlServerCe.SqlCeCommand

    If psCondicion <> "" Then
      lsQuery = "SELECT " & psCampoValor & " FROM " & psTabla & " WHERE " & psCondicion
    Else
      lsQuery = "SELECT " & psCampoValor & " FROM " & psTabla
    End If

    cmdComando.CommandText = lsQuery
    datDataAdapter = New PgSqlDataAdapter(cmdComando)

    datDataAdapter.Fill(ldatDataset)
    i = ldatDataset.Tables(0).Rows.Count

    If i > 0 Then
      lsValor = ldatDataset.Tables(0).Rows(0).Item(psCampoMostrar).ToString
    Else
      lsValor = psTabla
    End If

    ObtenerValorRemoto = lsValor
  End Function

End Module

