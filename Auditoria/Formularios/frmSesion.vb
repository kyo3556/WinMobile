Public Class frmSesion

  Private Sub BtnCancelar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnCancelar.Click
    UtxtContraseña.Text = String.Empty
    utxtUsuario.Text = String.Empty
    Me.Close()
  End Sub

  Private Sub BtnAceptar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnAceptar.Click

    If UtxtContraseña.Text <> "" And utxtUsuario.Text <> "" Then
      lblPass.Text = UtxtContraseña.Text
      lblUser.Text = utxtUsuario.Text
      UtxtContraseña.Text = String.Empty
      utxtUsuario.Text = String.Empty
      Me.Close()
    Else
      MsgBox("Te falto ingresar usuario o contraseña", MsgBoxStyle.Exclamation)
    End If

  End Sub
End Class