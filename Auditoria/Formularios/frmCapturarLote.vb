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
    oDecodeAssembly.Dispose()
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
        ElseIf psCodigo.Substring(0, 1) = 7 Then
          ActualizarDatosGral(psCodigo.Substring(0, 12), 1)
        End If

      Else

      End If

    ElseIf psCodigo.Length = 14 Then
      If psCodigo.Substring(0, 1) = 1 Then
        ActualizarDatosGral(psCodigo.Substring(0, 13), 1)
      End If

    Else

    End If

      txtCantidad.Text = String.Empty
      txtCantidad.Text = 1
      txtCantidad.Focus()

      '--- play an SDK provided audible sound ---
      oDecodeAssembly.Sound.Play(Sound.SoundTypes.Success)
  End Sub

  Private Sub btnScan_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnScan.Click
    Scaneo(txtCodigo.Text)
  End Sub

  Private Sub frmCapturarLote_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Activated

    '--- add a handler for the Decode Event ---
    oDecodeAssembly = New DecodeAssembly
    AddHandler oDecodeAssembly.DecodeEvent, AddressOf oDecodeAssembly_DecodeEvent

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
    txtCantidad.Text = 1
    lblNombreArticulo.Text = ""
    LblCantidadTotal.Text = ""
  End Sub

  Public Sub LlenarDataGrid()
    Dim lsQuery As String
    Dim ldatDataset1 As New System.Data.DataSet
    Dim iLimite As Integer 'i,
    Dim Estilos1 As New DataGridTableStyle
    Dim Estilo1, Estilo2, Estilo3, Estilo4, Estilo5, Estilo6 As New DataGridTextBoxColumn

    grdDetalle.TableStyles.Clear()

    lsQuery = "SELECT * FROM proArticuloAuditoriaLote WHERE Id_Aud=" & Id_Aud.Text & "AND Id_Art=" & Id_Art.Text
    cmdComandoLocal = New System.Data.SqlServerCe.SqlCeCommand(lsQuery, cnxLocal)

    Dim adaptadorLocal As New System.Data.SqlServerCe.SqlCeDataAdapter(cmdComandoLocal)
    adaptadorLocal.Fill(ldatDataset1)
    iLimite = ldatDataset1.Tables(0).Rows.Count

    If iLimite > 0 Then
      Estilos1.MappingName = ldatDataset1.Tables(0).TableName

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

      grdDetalle.DataSource = ldatDataset1.Tables(0) 'DbAuditoriaDataSet1.proArticuloAuditoriaLote
    End If

  End Sub

#Region "Decode Events"
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
#End Region

  Private Function ActualizarDatosGral(Optional ByVal psCodigo As String = "75022599", Optional ByVal Modo As Integer = 0) As Boolean
    Dim lsQuery, lsQuery1, lsCondicion, lsCondicionLote As String
    Dim lsEmpaque, lsCantidad, lsCodigo, lsLote, lsNombre As String 'lsExistencia
    Dim ldatDataset1 As New System.Data.DataSet
    Dim iLimite, liCantidad, liId_Art, liId_Prst As Integer
    Dim lscondicionl As String = "1", lsLabel As String
    Dim ldCajas, ldLitros As Double
    cmdComandoL = New System.Data.SqlServerCe.SqlCeCommand
    Try
      lsCondicion = "Id_Aud=" & Id_Aud.Text & " And Id_Art=" & Id_Art.Text
      lsCondicionLote = "Id_Aud=" & Id_Aud.Text & " AND Id_Art=" & Id_Art.Text & " AND Id_Lte=" = Id_Lte.Text

      If Modo = 0 And psCodigo = "75022599" Then
        lsEmpaque = ObtenerValorLocal("proArticulosAuditoria", "empaque", lsCondicion) 'obtenerValorArticulo(Id_Aud.Text, Id_Art.Text, "empaque")
        liCantidad = CLng(lsEmpaque) * CLng(txtCantidad.Text)
      ElseIf Modo = 1 Then
        lsCodigo = psCodigo + VerificarCodigoGTIN(psCodigo)
        'Id_Art.Text = ObtenerValorLocal("proArticuloEmpaque", "Id_Art", "Codigo=" & lsCodigo) 'obtenerValorArticuloEmpaque("Id_Art", lsCodigo)
        Id_Art.Text = ObtenerValorLocalP("proArticuloEmpaque", "Id_Art", "Id_Art", "Codigo='" & lsCodigo & "'")
        lsLabel = Id_Art.Text
        If lsLabel = "proArticuloEmpaque" Then
          MsgBox("No se encontro el código: " & psCodigo, MsgBoxStyle.Exclamation, "Atención")
          lblNombreArticulo.Text = "[No Disponible]"
          LblCantidadTotal.Text = "[No Disponible]"
          Exit Function
        Else
          lsNombre = ObtenerValorLocal("ProArticulosAuditoria", "Nombre", lsCondicion) 'obtenerValorArticulo(Id_Aud.Text, Id_Art.Text, "Nombre")
          lblNombreArticulo.Text = lsNombre
          lsLote = InputBox("Ingresa el numero de lote:", "Lote", "")
          'lsLote = InputBox("Artículo:" & lsNombre & vbCrLf & " Ingresa el numero de lote:", "Lote", "")
          If lsLote = "" Then
            Exit Function
          Else
            Id_Art.Text = ObtenerValorLocal("proArticuloEmpaque", "Id_Art", "Codigo='" & lsCodigo & "'")
            Id_Lte.Text = lsLote
            lsCondicionLote = lsCondicion & " AND Id_Lte=" & Id_Lte.Text
            '-
            liId_Prst = ObtenerValorLocal("proArticuloEmpaque", "Id_Prst", "Codigo='" & lsCodigo & "'")
            Select Case liId_Prst
              Case 1, 2, 4
                liCantidad = CDbl(txtCantidad.Text) * CDbl(ObtenerValorLocal("proArticuloEmpaque", "Cantidad", "Codigo='" & lsCodigo & "'"))
              Case 12
                liCantidad = CDbl(txtCantidad.Text) * CDbl(4)
              Case 62
                lsCondicion = "Id_Art=" & Id_Art.Text
                lsEmpaque = ObtenerValorLocal("proArticuloEmpaque", "Cantidad", lsCondicion)
                If CLng(lsEmpaque) = 12 Then
                  liCantidad = CDbl(txtCantidad.Text) * CDbl(1)
                ElseIf CLng(lsEmpaque) = 20 Then
                  If CInt(Id_Art.Text) = 930 Or 693 Or 694 Or 702 Then
                    liCantidad = CDbl(txtCantidad.Text) * CDbl(10)
                  Else
                    liCantidad = CDbl(txtCantidad.Text) * CDbl(20)
                  End If
                End If
                'If CInt(Id_Art.Text) = 662 Or 1068 Or 953 Or 553 Then
                '  liCantidad = CDbl(txtCantidad.Text) * CDbl(1)
                'End If

              Case Else
                liCantidad = CDbl(txtCantidad.Text)
            End Select

            '-
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
          liId_Art = Id_Art.Text
          lsCondicion = "Id_Aud=" & Id_Aud.Text & " AND Id_Art=" & liId_Art
          liCantidad = ObtenerValorLocalP("proArticuloAuditoriaLote", "SUM(Cantidad) AS Cantidad", "Cantidad", lsCondicion) 'liCantidad = ConteoArticuloLote(Id_Aud.Text, Id_Art.Text)
          'lsQuery1 = "UPDATE proArticulosAuditoria SET Cantidad= " & liCantidad & ", Ajuste = Cantidad - Existencia WHERE " & lsCondicion
          lsQuery1 = "UPDATE proArticulosAuditoria SET Cantidad= " & liCantidad & " WHERE " & lsCondicion
          cmdComandoLocal.CommandText = lsQuery1
          cmdComandoLocal.ExecuteNonQuery() 'ActualizarConteo(liCantidad, Id_Aud.Text, Id_Art.Text) 'ActualizarAjuste(Id_Aud.Text, Id_Art.Text)
          lsQuery1 = "UPDATE proArticulosAuditoria SET Ajuste = Cantidad - Existencia WHERE " & lsCondicion
          cmdComandoLocal.CommandText = lsQuery1
          cmdComandoLocal.ExecuteNonQuery()
        Else
          MsgBox("Debes ingresar una cantidad ", MsgBoxStyle.Exclamation, "Atención")
        End If
      Else
        If txtCantidad.Text.Length > 0 Then
          lsQuery1 = "INSERT INTO proArticuloAuditoriaLote (Id_Aud, Id_Art, Id_Lte, Cantidad)"
          lsQuery1 = lsQuery1 & " VALUES (" & Id_Aud.Text & ", " & Id_Art.Text & ", " & Id_Lte.Text & ", " & liCantidad & ")"
          cmdComandoLocal.CommandText = lsQuery1
          cmdComandoLocal.ExecuteNonQuery() 'ActualizarCantidadLote(liCantidad, Id_Aud.Text, Id_Art.Text, Id_Lte.Text)
          liId_Art = Id_Art.Text
          lsCondicion = "Id_Aud=" & Id_Aud.Text & " AND Id_Art=" & liId_Art
          liCantidad = ObtenerValorLocalP("proArticuloAuditoriaLote", "SUM(Cantidad) AS Cantidad", "Cantidad", lsCondicion) 'liCantidad = ConteoArticuloLote(Id_Aud.Text, Id_Art.Text)
          'lsQuery1 = "UPDATE proArticulosAuditoria SET Cantidad= " & liCantidad & ", Ajuste = Cantidad - Existencia WHERE " & lsCondicion
          lsQuery1 = "UPDATE proArticulosAuditoria SET Cantidad= " & liCantidad & " WHERE " & lsCondicion      'lsQuery1 = "UPDATE proArticulosAuditoria SET Cantidad= " & liCantidad & ", Ajuste = Cantidad - Existencia WHERE " & lsCondicion
          cmdComandoLocal.CommandText = lsQuery1
          cmdComandoLocal.ExecuteNonQuery() 'ActualizarConteo(liCantidad, Id_Aud.Text, Id_Art.Text) 'ActualizarAjuste(Id_Aud.Text, Id_Art.Text)
          lsQuery1 = "UPDATE proArticulosAuditoria SET Ajuste = Cantidad - Existencia WHERE " & lsCondicion
          cmdComandoLocal.CommandText = lsQuery1
          cmdComandoLocal.ExecuteNonQuery()
        Else
          MsgBox("Debes ingresar una cantidad ", MsgBoxStyle.Exclamation, "Atención")
        End If
      End If
      lblNombreArticulo.Text = ObtenerValorLocal("proArticulosAuditoria", "Nombre", lsCondicion)
      lsCantidad = ObtenerValorLocal("proArticulosAuditoria", "Cantidad", lsCondicion) 'obtenerValorArticulo(Id_Aud.Text, Id_Art.Text, "Cantidad") ' 'obtenerValorArticulo(Id_Aud.Text, Id_Art.Text, "Cantidad") 
      'lsExistencia = ObtenerValorLocal("proArticulosAuditoria", "Existencia", lsCondicion) 'obtenerValorArticulo(Id_Aud.Text, Id_Art.Text, "Existencia")
      lsEmpaque = ObtenerValorLocal("proArticulosAuditoria", "empaque", lsCondicion)
      ldCajas = CDbl(lsCantidad) / CDbl(lsEmpaque)
      liCantidad = CInt(ldCajas)
      ldLitros = ldCajas - liCantidad
      If ldLitros = 0 Then
        LblCantidadTotal.Text = "Cantidad: " & lsCantidad & " - Piezas: " & ldCajas
      Else
        If ldLitros >= lsEmpaque Then
          ldLitros = ldLitros * lsEmpaque
          ldLitros = lsEmpaque - (ldLitros * -1)
          LblCantidadTotal.Text = "Cantidad: " & lsCantidad & " - P: " & Int(ldCajas) & " - " & "U: " & Format(ldLitros, "##,##0.00") 'CDbl(lsEmpaque) - CDbl(ldLitros)
        Else
          If ldCajas < 1 Then
            LblCantidadTotal.Text = "Cantidad: " & lsCantidad & " - P: " & Int(ldCajas) & " - " & "U: " & lsCantidad
          Else
            LblCantidadTotal.Text = "Cantidad: " & lsCantidad & " - P: " & Int(ldCajas) & " - " & "U: " & 0
          End If

        End If

      End If
      'txtCantidad.Text = String.Empty
      txtCantidad.Text = 1
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