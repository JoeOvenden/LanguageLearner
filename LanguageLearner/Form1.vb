Imports System.IO
Imports System.Text
Imports System.Data.SqlClient

Public Class Form1
    Public Shared fileContentsDictionary As New Dictionary(Of String, List(Of Translation))()
    Public Shared filesToPractice As New List(Of String)()
    Public Shared uc1 As New UserControl1()
    Public Shared uc2 As New UserControl2()

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        SetupPanelContainer()
    End Sub

    Public Sub SaveFilesToPractice()
        filesToPractice.Clear()
        uc1.Update()
        For Each fileName As String In uc1.checkedListBoxFiles.CheckedItems
            filesToPractice.Add(fileName)
        Next
    End Sub

    Private Sub SetupPanelContainer()
        uc1.Dock = DockStyle.Fill
        panelContainer.Controls.Add(uc1)
    End Sub

    Public Sub ExecuteNonQuery(connection As SqlConnection, command As SqlCommand, Optional closeConnection As Boolean = True)
        Try
            If (connection.State = ConnectionState.Closed) Then
                connection.Open()
            End If
            command.ExecuteNonQuery()
        Catch ex As Exception
            MessageBox.Show(ex.ToString())
        Finally
            If (closeConnection = True AndAlso connection.State = ConnectionState.Open) Then
                connection.Close()
            End If
        End Try
    End Sub

    Public Function GetSqlConnection() As SqlConnection
        Return New SqlConnection("Data Source=(LocalDB)\MSSQLLocalDB;
            AttachDbFilename=C:\Users\joeov\source\repos\LanguageLearner\LanguageLearner\languageLearner.db.mdf;
            Integrated Security=True;Connect Timeout=30")
    End Function
End Class
