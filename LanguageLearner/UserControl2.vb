﻿Imports System.Data.SqlClient, System.Linq

Public Enum Translation_Direction
    Forward
    Backwards
End Enum

Public Class UserControl2
    Dim myForm As Form1
    Dim translationsToPractice As New List(Of Translation)()
    Dim translationQueue As Queue(Of Translation)
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
        SetupTranslationQueue()
        SetNextTranslation()
    End Sub

    Private Sub SetupTranslationQueue()
        translationsToPractice = translationsToPractice.OrderByDescending(Function(t) t.TimeSinceLastPracticed).ToList()
        translationQueue = New Queue(Of Translation)(translationsToPractice)
    End Sub

    Private Sub SetNextTranslation()
        ' Dim index As Integer = random.Next(translationsToPractice.Count)
        ' currentTranslation = translationsToPractice(index)

        If translationQueue.Count = 0 Then
            SetupTranslationQueue()
        End If
        currentTranslation = translationQueue.Dequeue()
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
            MarkTranslationAsCorrect()
        End If
    End Sub

    Private Sub MarkTranslationAsCorrect()
        lblWord2.ForeColor = Color.Green
        currentTranslation.updateTimeLastCorrectlyPracticed(myForm)
    End Sub

    Private Sub txtBoxTranslation_KeyDown(sender As Object, e As KeyEventArgs) Handles txtBoxTranslation.KeyDown
        ' If the user presses ctrl + q after seeing the answer, it marks it as correct
        If e.KeyCode = Keys.Q And (Control.ModifierKeys And Keys.Control) = Keys.Control Then
            If lblWord2.Visible = True Then
                MarkTranslationAsCorrect()
            End If
        End If

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
