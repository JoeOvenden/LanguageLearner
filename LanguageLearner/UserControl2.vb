Imports System.Data.SqlClient, System.Linq

Public Enum Translation_Direction
    Forward
    Backwards
End Enum

Public Class UserControl2
    Dim myForm As Form1
    Dim translationsToPractice As New List(Of Translation)()
    Dim random As New Random()
    Dim currentTranslation As Translation
    Dim translationDirection As Translation_Direction = Translation_Direction.Forward

    Private Sub UserControl2_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Make sure that the parent form is available
        If TypeOf Me.ParentForm Is Form1 Then
            myForm = CType(Me.ParentForm, Form1)
        Else
            MessageBox.Show("Parent form is not yet available.")
        End If

        translationsToPractice.Clear()
        For Each fileName As String In myForm.filesToPractice
            For Each trans As Translation In myForm.fileContentsDictionary(fileName)
                translationsToPractice.Add(trans)
            Next
        Next
        SetNextTranslation()
    End Sub

    Private Sub SetNextTranslation()
        Dim index As Integer = random.Next(translationsToPractice.Count)
        currentTranslation = translationsToPractice(index)
        lblWord2.Hide()
        lblWord2.ForeColor = Color.Black
        txtBoxTranslation.Clear()

        If translationDirection = Translation_Direction.Forward Then
            lblWord1.Text = currentTranslation.Word
            lblWord2.Text = currentTranslation.TranslatedWord
        Else
            lblWord1.Text = currentTranslation.TranslatedWord
            lblWord2.Text = currentTranslation.Word
        End If
    End Sub

    Private Sub btnMenu_Click(sender As Object, e As EventArgs) Handles btnMenu.Click
        Dim uc1 As UserControl1 = myForm.uc1
        uc1.Dock = DockStyle.Fill
        myForm.panelContainer.Controls.Clear()
        myForm.panelContainer.Controls.Add(uc1)
    End Sub

    Private Sub btnNextWord_Click(sender As Object, e As EventArgs) Handles btnNextWord.Click
        SetNextTranslation()
    End Sub

    Private Sub ShowTranslation()
        lblWord2.Show()
    End Sub

    Private Sub VerifyTranslation()
        ' Check if the user correctly translated the word or phrase
        Dim answer As String
        If translationDirection = Translation_Direction.Forward Then
            answer = currentTranslation.TranslatedWord
        Else
            answer = currentTranslation.Word
        End If

        If txtBoxTranslation.Text = answer Then
            lblWord2.ForeColor = Color.Green
            Using connection As SqlConnection = myForm.GetSqlConnection()
                Dim cmdStr As String = "UPDATE Translations SET date_last_practiced = @date WHERE id=@id"
                Dim updateTranslationCommand As SqlCommand = New SqlCommand(cmdStr, connection)
                updateTranslationCommand.Parameters.AddWithValue("@date", DateTime.Now)
                updateTranslationCommand.Parameters.AddWithValue("@id", currentTranslation.Id)
                myForm.ExecuteNonQuery(connection, updateTranslationCommand)
            End Using
        End If
    End Sub

    Private Sub txtBoxTranslation_KeyDown(sender As Object, e As KeyEventArgs) Handles txtBoxTranslation.KeyDown
        If e.KeyCode = Keys.Enter Then
            If lblWord2.Visible = True Then
                SetNextTranslation()
            Else
                ShowTranslation()
                VerifyTranslation()
            End If
            e.Handled = True
            e.SuppressKeyPress = True
        End If
    End Sub

    Private Sub btnSwitchMode_Click(sender As Object, e As EventArgs) Handles btnSwitchMode.Click
        translationDirection = Not translationDirection
        lblWord1.Show()
        lblWord2.Show()
        SetNextTranslation()
    End Sub
End Class
