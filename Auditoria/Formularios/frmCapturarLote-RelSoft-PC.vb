Option Explicit On

Imports HandHeldProducts.Embedded.Decoding
Imports HandHeldProducts.Embedded.Hardware
Imports System.Text

Public Class frmCapturarLote
  Dim iId_Lte As Integer
  Dim oDecodeAssembly As DecodeAssembly
  Dim adaptadorLocal As New System.Data.SqlServerCe.SqlCeDataAdapter
  Dim cmdComandoL As New System.Data.SqlServerCe.SqlCeCommand

  Private Sub mnuRegresar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuRegresar.Click
    txtCodigo.Text = String.Empty
    LblCantidadTotal.Text = String.Empty
    txtCantidad.Text = String.Empty
    lblNombreArticulo.Text = String.Empty
    grdDetalle.DataSource = Nothing
    'oDecodeAssembly.Dispose()
    frmPrincipal.Show()
    'Me.Hide()
    Me.Close()
  End Sub

  Private Sub Scaneo(ByVal psCodigo As String)
    Dim ldatDataSet As New Auditoria.dbAuditoriaDataSet
    Dim lsNombre, lsCondicion As String
    lsCondicion = "Id_Aud=" & Id_Aud.Text & "And Id_Art=" & Id_Art.Text
    lsNombre = "Nombre"
    'Dim lsExistencia, lsCantidad As String
    ' Dim i As Integer

    If psCodigo.Length = 12 Then

      If IsNumeric(psCodigo) Then
        'ActualizarDatosGTIN(psCodigo)
        ActualizarDatosGral(psCodigo, 1)
      Else
        Id_Lte.Text = psCodigo.Substring(1, 5)
        Id_Art.Text = psCodigo.Substring(7, 5)

        Id_Lte.Text = Id_Lte.Text.ToString.TrimStart("0")
        Id_Art.Text = Id_Art.Text.ToString.TrimStart("0")


        lsNombre = ObtenerValorLocal("proArticulosAuditoria", "Nombre", lsCondicion)
        lblNombreArticulo.Text = lsNombre

        If lsNombre = "Nombre" Then
          lblNombreArticulo.Text = "[No Disponible]"
          LblCantidadTotal.Text = "[No Disponible]"
        Else
          ActualizarDatosGral(, )
        End If
      End If

    ElseIf psCodigo.Length = 13 Then
      If IsNumeric(psCodigo) Then
        If psCodigo.Substring(0, 1) = 1 Then
          'ActualizarDatosGTIN(psCodigo)
          ActualizarDatosGral(psCodigo, 1)
        End If

      Else

      End If
    Else

    End If

    txtCantidad.Text = String.Empty
    txtCantidad.Focus()

    '--- play an SDK provided audible sound ---
    'oDecodeAssembly.Sound.Play(Sound.SoundTypes.Success)
  End Sub

  Private Function ActualizarDatos() As Boolean
    Dim i As Integer
    Dim liCantidad As Integer
    Dim lsCantidad, lsEmpaque, lsExistencia, lsCondicion As String
    Try
      lsCondicion = "Id_Aud=" & Id_Aud.Text & " And Id_Art=" & Id_Art.Text
      '--verificar si existe el lote
      i = ProArticuloAuditoriaLoteTableAdapter1.GetDataByArticuloLote(Id_Aud.Text, Id_Art.Text, Id_Lte.Text).Rows.Count
      ProArticulosAuditoriaTableAdapter1.gObtenerNombre(Id_Aud.Text, Id_Art.Text)

      If i > 0 Then '- En caso de que si update
        If txtCantidad.Text.Length > 0 Then
          lsEmpaque = ObtenerValorLocal("proArticulosAuditoria", "empaque", lsCondicion) 'obtenerValorArticulo(Id_Aud.Text, Id_Art.Text, "empaque")
          liCantidad = CLng(lsEmpaque) * CLng(txtCantidad.Text)
          ProArticuloAuditoriaLoteTableAdapter1.ActualizarCantidadLote(liCantidad, Id_Aud.Text, Id_Art.Text, Id_Lte.Text)
          liCantidad = ProArticuloAuditoriaLoteTableAdapter1.ConteoArticuloLote(Id_Aud.Text, Id_Art.Text)
          ProArticulosAuditoriaTableAdapter1.ActualizarConteo(liCantidad, Id_Aud.Text, Id_Art.Text)
          ProArticulosAuditoriaTableAdapter1.ActualizarAjuste(Id_Aud.Text, Id_Art.Text)
        Else
          MsgBox("Debes ingresar una cantidad ", MsgBoxStyle.Exclamation, "Atención")
        End If

      Else '--En caso de que no insert
        If txtCantidad.Text.Length > 0 Then
          lsEmpaque = ObtenerValorLocal("proArticulosAuditoria", "empaque", lsCondicion) 'obtenerValorArticulo(Id_Aud.Text, Id_Art.Text, "empaque")
          liCantidad = CLng(lsEmpaque) * CLng(txtCantidad.Text)
          ProArticuloAuditoriaLoteTableAdapter1.Insert(Id_Aud.Text, Id_Art.Text, Id_Lte.Text, liCantidad)
          liCantidad = ProArticuloAuditoriaLoteTableAdapter1.ConteoArticuloLote(Id_Aud.Text, Id_Art.Text)
          ProArticulosAuditoriaTableAdapter1.ActualizarConteo(liCantidad, Id_Aud.Text, Id_Art.Text)
          ProArticulosAuditoriaTableAdapter1.ActualizarAjuste(Id_Aud.Text, Id_Art.Text)
        Else
          MsgBox("Debes ingresar una cantidad ", MsgBoxStyle.Exclamation, "Atención")
        End If
      End If
      lblNombreArticulo.Text = ObtenerValorLocal("proArticulosAuditoria", "Nombre", lsCondicion)
      lsCantidad = obtenerValorArticulo(Id_Aud.Text, Id_Art.Text, "Cantidad") 'ObtenerValorLocal("proArticulosAuditoria", "Cantidad", lsCondicion) 'obtenerValorArticulo(Id_Aud.Text, Id_Art.Text, "Cantidad") 
      lsExistencia = ObtenerValorLocal("proArticulosAuditoria", "Existencia", lsCondicion) 'obtenerValorArticulo(Id_Aud.Text, Id_Art.Text, "Existencia")
      LblCantidadTotal.Text = "E: " & lsExistencia & " - C: " & lsCantidad & "" '& CInt(lsCantidad) - CInt(lsExistencia)

      txtCantidad.Text = String.Empty
      LlenarDataGrid()

    Catch ex As Exception
      MsgBox("Error: " & ex.Message, MsgBoxStyle.Critical, "Error")
    End Try

  End Function

  Private Sub btnScan_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnScan.Click
    Scaneo(txtCodigo.Text)
  End Sub

  Private Sub frmCapturarLote_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Activated

    '--- add a handler for the Decode Event ---
    'oDecodeAssembly = New DecodeAssembly
    'AddHandler oDecodeAssembly.DecodeEvent, AddressOf oDecodeAssembly_DecodeEvent

    Id_Aud.Text = frmPrincipal.cboNumero.SelectedValue.ToString
    Id_Drc.Text = frmPrincipal.cboBodega.SelectedValue.ToString '150 '
    lblNombreDestino.Text = frmPrincipal.cboBodega.Text.ToString '"Ciudad Mante" '

    'txtCantidad.Text = String.Empty
    'lblNombreArticulo.Text = String.Empty

    'LblCantidadTotal.Text = "Son: - Van: - Faltan: Pzas"
    If cnxLocal.State = Data.ConnectionState.Open Then
    Else
      ConectarLocal()
    End If

  End Sub

  Private Sub frmCapturarLote_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

  End Sub

  Public Sub LlenarDataGrid()
    Dim Estilos1 As New DataGridTableStyle
    Dim Estilo1, Estilo2 As New DataGridTextBoxColumn

    grdDetalle.TableStyles.Clear()
    ProArticuloAuditoriaLoteTableAdapter1.FillByArticuloLote(DbAuditoriaDataSet1.proArticuloAuditoriaLote, Id_Aud.Text, Id_Art.Text, Id_Lte.Text)
    Estilos1.MappingName = DbAuditoriaDataSet1.proArticuloAuditoriaLote.TableName

    With Estilo1
      .MappingName = "Id_Lte"
      .HeaderText = "Lote"
      .Width = 55
    End With
    With Estilo2
      .MappingName = "Cantidad"
      .HeaderText = "Cantidad"
      .Width = 60
    End With

    Estilos1.GridColumnStyles.Add(Estilo1)
    Estilos1.GridColumnStyles.Add(Estilo2)

    grdDetalle.TableStyles.Add(Estilos1)
    grdDetalle.RowHeadersVisible = False

    grdDetalle.DataSource = DbAuditoriaDataSet1.proArticuloAuditoriaLote

  End Sub

#Region "Decode Events"
  Private Sub frmCapturarLote_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
    '---------------------------------------------------------------------------------
    '--- frmMain KeyPreview = True; check for SCAN key pressed to start scanning ---
    '---------------------------------------------------------------------------------
    Try

      If e.KeyCode = CType(42, Keys) Then

        '--- remove Event Handler to prevent the key from invoking multiple scans ---
        RemoveHandler Me.KeyDown, AddressOf frmCapturarLote_KeyDown
        '--- clear data from textbox --
        'txtBoxLote.Text = String.Empty
        '--- scan bar code ---
        oDecodeAssembly.ScanBarcode()
        '--- key was handled ---
        e.Handled = True
      End If

    Catch ex As Exception
      MessageBox.Show(ex.Message, "Exception")
    End Try

  End Sub

  Private Sub frmCapturarLote_KeyUp(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyUp
    '---------------------------------------------------------------------------------
    '--- frmMain KeyPreview = True; check for SCAN key released to end scanning ---
    '---------------------------------------------------------------------------------
    Try

      If e.KeyCode = CType(42, Keys) Then

        '--- cancel scan bar code ---
        oDecodeAssembly.CancelScanBarcode()
        '--- restore Event Handler to scan key ---
        AddHandler Me.KeyDown, AddressOf frmCapturarLote_KeyDown
        '--- key was handled --
        e.Handled = True

      End If

    Catch ex As Exception
      MessageBox.Show(ex.Message, "Exception")
    End Try

  End Sub

  Public Sub oDecodeAssembly_DecodeEvent(ByVal sender As System.Object, ByVal e As DecodeAssembly.DecodeEventArgs)
    '---------------------------------------------------------------------------------
    '--- retrieve the decoded bar code data (event is raised by ScanBarcode method) ---
    '---------------------------------------------------------------------------------
    Try
      If e.ResultCode = DecodeAssembly.ResultCodes.Success Then
        Scaneo(e.Message)
      Else

        '--- play an SDK provided audible sound ---
        oDecodeAssembly.Sound.Play(Sound.SoundTypes.Failure)

        If Not (e.DecodeException Is Nothing) Then
          '--- Async Decode Exception ---
          Select Case (e.DecodeException.ResultCode)
            Case DecodeAssembly.ResultCodes.Cancel 'async Decode was canceled
              Return
            Case DecodeAssembly.ResultCodes.NoDecode 'scan timeout
              MessageBox.Show("Scan Timeout Exceeded")
            Case Else
              MessageBox.Show(e.DecodeException.Message)
          End Select

        Else
          '--- Generic Async Exception ---
          MessageBox.Show(e.Exception.Message)

        End If
        '--- restore Event Handler to scan key ---
        'AddHandler Me.KeyDown, AddressOf frmMain_KeyDown
      End If

    Catch ex As Exception
      MessageBox.Show(ex.Message, "Exception")
    End Try

  End Sub
#End Region
  Private Function ActualizarDatosGTIN(ByVal psCodigo As String) As Boolean
    Dim i, liCantidad As Integer
    Dim lsCodigo
    Dim lsLote, lsCantidad, lsExistencia, lsNombre, lsCondicion As String

    lsCodigo = psCodigo + VerificarCodigoGTIN(psCodigo)
    Id_Art.Text = obtenerValorArticuloEmpaque("Id_Art", lsCodigo)
    lsCondicion = "Id_Aud=" & Id_Aud.Text & " And Id_Art=" & Id_Art.Text

    If Id_Art.Text = "Id_Art" Then
      MsgBox("No se encontro el código: " & psCodigo, MsgBoxStyle.Exclamation, "Atención")
      lblNombreArticulo.Text = "[No Disponible]"
      LblCantidadTotal.Text = "[No Disponible]"
    Else
      lsNombre = obtenerValorArticulo(Id_Aud.Text, Id_Art.Text, "Nombre")
      lblNombreArticulo.Text = lsNombre

      lsLote = InputBox("Ingresa el numero de lote:", "Lote", "")
      If lsLote <> "" Then
        Id_Lte.Text = lsLote
        '--verificar si existe el lote
        ProArticuloAuditoriaLoteTableAdapter1.FillByArticuloLote(DbAuditoriaDataSet1.proArticuloAuditoriaLote, Id_Aud.Text, Id_Art.Text, lsLote)
        i = DbAuditoriaDataSet1.proArticuloAuditoriaLote.Rows.Count

        If i > 0 Then '- En caso de que si update
          If txtCantidad.Text <> String.Empty Then
            liCantidad = CLng(txtCantidad.Text)
            ProArticuloAuditoriaLoteTableAdapter1.ActualizarCantidadLote(liCantidad, Id_Aud.Text, Id_Art.Text, CInt(lsLote))
            liCantidad = ProArticuloAuditoriaLoteTableAdapter1.ConteoArticuloLote(Id_Aud.Text, Id_Art.Text)
            ProArticulosAuditoriaTableAdapter1.ActualizarConteo(liCantidad, Id_Aud.Text, Id_Art.Text)
            ProArticulosAuditoriaTableAdapter1.ActualizarAjuste(Id_Aud.Text, Id_Art.Text)
          Else
            MsgBox("Debes ingresar una cantidad ", MsgBoxStyle.Exclamation, "Atención")
          End If
        Else '--En caso de que no insert
          If txtCantidad.Text <> String.Empty Then
            liCantidad = CLng(txtCantidad.Text)
            ProArticuloAuditoriaLoteTableAdapter1.Insert(Id_Aud.Text, Id_Art.Text, CInt(lsLote), liCantidad)
            liCantidad = ProArticuloAuditoriaLoteTableAdapter1.ConteoArticuloLote(Id_Aud.Text, Id_Art.Text)
            ProArticulosAuditoriaTableAdapter1.ActualizarConteo(liCantidad, Id_Aud.Text, Id_Art.Text)
            ProArticulosAuditoriaTableAdapter1.ActualizarAjuste(Id_Aud.Text, Id_Art.Text)
          Else
            MsgBox("Debes ingresar una cantidad ", MsgBoxStyle.Exclamation, "Atención")
          End If
        End If
        lblNombreArticulo.Text = ObtenerValorLocal("proArticulosAuditoria", "Nombre", lsCondicion)
        lsCantidad = obtenerValorArticulo(Id_Aud.Text, Id_Art.Text, "Cantidad")
        lsExistencia = ObtenerValorLocal("proArticulosAuditoria", "Existencia", lsCondicion) 'obtenerValorArticulo(Id_Aud.Text, Id_Art.Text, "Existencia")
        LblCantidadTotal.Text = "E: " & lsExistencia & " - C: " & lsCantidad & ""
        LlenarDataGrid()
        'txtCantidad.Text = String.Empty
      Else
        MsgBox("Debes ingresar un lote valido", MsgBoxStyle.Exclamation, "Atención")
      End If
    End If

  End Function

  Private Function ActualizarDatosGral(Optional ByVal psCodigo As String = "75022599", Optional ByVal Modo As Integer = 0) As Boolean
    Dim lsQuery, lsQuery1, lsCondicion, lsCondicionLote As String
    Dim lsEmpaque, lsExistencia, lsCantidad, lsCodigo, lsLote, lsNombre As String
    Dim ldatDataset1 As New System.Data.DataSet
    Dim iLimite, liCantidad As Integer
    Dim lscondicionl As String = "1"
    cmdComandoL = New System.Data.SqlServerCe.SqlCeCommand
    Try
      lsCondicion = "Id_Aud=" & Id_Aud.Text & " And Id_Art=" & Id_Art.Text
      lsCondicionLote = "Id_Aud=" & Id_Aud.Text & " AND Id_Art=" & Id_Art.Text & " AND Id_Lte=" = Id_Lte.Text

      If Modo = 0 And psCodigo = "75022599" Then
        lsEmpaque = ObtenerValorLocal("proArticulosAuditoria", "empaque", lsCondicion) 'obtenerValorArticulo(Id_Aud.Text, Id_Art.Text, "empaque")
        liCantidad = CLng(lsEmpaque) * CLng(txtCantidad.Text)
      ElseIf Modo = 1 Then
        lsCodigo = psCodigo + VerificarCodigoGTIN(psCodigo)
        Id_Art.Text = ObtenerValorLocal("proArticuloEmpaque", "Id_Art", "Codigo=" & lsCodigo) 'obtenerValorArticuloEmpaque("Id_Art", lsCodigo)

        If Id_Art.Text = "proArticuloEmpaque" Then
          MsgBox("No se encontro el código: " & psCodigo, MsgBoxStyle.Exclamation, "Atención")
          lblNombreArticulo.Text = "[No Disponible]"
          LblCantidadTotal.Text = "[No Disponible]"
          Exit Function
        Else
          lsNombre = obtenerValorArticulo(Id_Aud.Text, Id_Art.Text, "Nombre")
          lblNombreArticulo.Text = lsNombre
          lsLote = InputBox("Ingresa el numero de lote:", "Lote", "")
          If lsLote = "" Then
            Exit Function
          Else
            Id_Lte.Text = lsLote
            lsCondicionLote = lsCondicion & " AND Id_Lte=" & Id_Lte.Text
          End If

        End If
      End If

      lsQuery = "SELECT * FROM proArticuloAuditoriaLote WHERE Id_Art=" & Id_Art.Text & " AND Id_Lte=" & Id_Lte.Text
      cmdComandoLocal = New System.Data.SqlServerCe.SqlCeCommand(lsQuery, cnxLocal)

      Dim adaptadorLocal As New System.Data.SqlServerCe.SqlCeDataAdapter(cmdComandoLocal)
      adaptadorLocal.Fill(ldatDataset1)
      iLimite = ldatDataset1.Tables(0).Rows.Count

      If iLimite > 0 Then
        If txtCantidad.Text.Length > 0 Then

          lsQuery1 = "UPDATE proArticuloAuditoriaLote SET Cantidad = Cantidad +" & liCantidad & " WHERE "
          lsQuery1 = lsQuery1 & "Id_Art=" & Id_Art.Text & " AND Id_Lte=" & Id_Lte.Text
          cmdComandoLocal.CommandText = lsQuery1
          cmdComandoLocal.ExecuteNonQuery() 'ActualizarCantidadLote(liCantidad, Id_Aud.Text, Id_Art.Text, Id_Lte.Text)
          liCantidad = ObtenerValorLocalP("proArticuloAuditoriaLote", "SUM(Cantidad) as Cantidad", "Cantidad", lsCondicion) 'liCantidad = ConteoArticuloLote(Id_Aud.Text, Id_Art.Text)'ProArticuloAuditoriaLoteTableAdapter1.ConteoArticuloLote(Id_Aud.Text, Id_Art.Text) '
          lsQuery1 = "UPDATE proArticulosAuditoria SET Cantidad= " & liCantidad & ", Ajuste = Cantidad - Existencia WHERE "
          lsQuery1 = lsQuery1 + lsCondicion
          cmdComandoLocal.CommandText = lsQuery1
          cmdComandoLocal.ExecuteNonQuery() 'ActualizarConteo(liCantidad, Id_Aud.Text, Id_Art.Text) 'ActualizarAjuste(Id_Aud.Text, Id_Art.Text)
        Else
          MsgBox("Debes ingresar una cantidad ", MsgBoxStyle.Exclamation, "Atención")
        End If
      Else
        If txtCantidad.Text.Length > 0 Then
          lsQuery1 = "INSERT INTO proArticuloAuditoriaLote (Id_Aud, Id_Art, Id_Lte, Cantidad)"
          lsQuery1 = lsQuery1 & " VALUES (" & Id_Aud.Text & ", " & Id_Art.Text & ", " & Id_Lte.Text & ", " & liCantidad & ")"
          cmdComandoLocal.CommandText = lsQuery1
          cmdComandoLocal.ExecuteNonQuery() 'ActualizarCantidadLote(liCantidad, Id_Aud.Text, Id_Art.Text, Id_Lte.Text)
          liCantidad = ObtenerValorLocalP("proArticuloAuditoriaLote", "SUM(Cantidad) as Cantidad", "Cantidad", lsCondicion) 'liCantidad = ConteoArticuloLote(Id_Aud.Text, Id_Art.Text)'ProArticuloAuditoriaLoteTableAdapter1.ConteoArticuloLote(Id_Aud.Text, Id_Art.Text) '
          'lsQuery1 = "UPDATE proArticulosAuditoria SET Cantidad= " & liCantidad & ", Ajuste = Cantidad - Existencia WHERE " + lsCondicion
          lsQuery1 = "UPDATE proArticulosAuditoria SET Cantidad= " & liCantidad & " WHERE " + lsCondicion
          cmdComandoLocal.ExecuteNonQuery() 'ActualizarConteo(liCantidad, Id_Aud.Text, Id_Art.Text) 'ActualizarAjuste(Id_Aud.Text, Id_Art.Text)
          'lsQuery1 = "UPDATE proArticulosAuditoria SET Cantidad= " & liCantidad & ", Ajuste = Cantidad - Existencia WHERE " + lsCondicion 
          cmdComandoLocal.ExecuteNonQuery() 'ActualizarConteo(liCantidad, Id_Aud.Text, Id_Art.Text) 'ActualizarAjuste(Id_Aud.Text, Id_Art.Text)
        Else
          MsgBox("Debes ingresar una cantidad ", MsgBoxStyle.Exclamation, "Atención")
        End If
      End If
      lblNombreArticulo.Text = ObtenerValorLocal("proArticulosAuditoria", "Nombre", lsCondicion)
      lsCantidad = ObtenerValorLocal("proArticulosAuditoria", "Cantidad", lsCondicion) 'obtenerValorArticulo(Id_Aud.Text, Id_Art.Text, "Cantidad") ' 'obtenerValorArticulo(Id_Aud.Text, Id_Art.Text, "Cantidad") 
      lsExistencia = ObtenerValorLocal("proArticulosAuditoria", "Existencia", lsCondicion) 'obtenerValorArticulo(Id_Aud.Text, Id_Art.Text, "Existencia")
      LblCantidadTotal.Text = "E: " & lsExistencia & " - C: " & lsCantidad '& "" & CInt(lsCantidad) - CInt(lsExistencia)

      txtCantidad.Text = String.Empty
      LlenarDataGrid()

    Catch ex As Exception
      MsgBox("Error: " & ex.Message, MsgBoxStyle.Critical, "Error")
    End Try
  End Function

  Private Function VerificarCodigoGTIN(ByVal psCodigo As String) As String
    Dim sNum As String
    Dim i As Integer
    Dim dSum As Double
    Dim dTest As Double
    Dim iDigito As Integer
    Dim iLimite As Integer

    iLimite = psCodigo.Length
    dSum = 0.0

    For i = 0 To iLimite - 1
      sNum = psCodigo.Substring(i, 1)
      dTest = CInt(i + 1) Mod 2
      If dTest = 0 Then
        Select Case iLimite 'psCodigo.Length
          Case 12
            dSum = dSum + (CInt(sNum) * 3)
          Case 13
            dSum = dSum + CInt(sNum)
        End Select
      Else
        Select Case iLimite 'psCodigo.Length
          Case 12
            dSum = dSum + CInt(sNum)
          Case 13
            dSum = dSum + (CInt(sNum) * 3)
        End Select
      End If
    Next

    iDigito = (10 - (dSum Mod 10)) Mod 10
    VerificarCodigoGTIN = iDigito

  End Function

End Class