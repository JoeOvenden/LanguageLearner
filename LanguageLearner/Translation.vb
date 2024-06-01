Imports System.Data.SqlClient

Public Class Translation
    Public Property Id As Integer
    Public Property FileName As String
    Public Property Word As String
    Public Property TranslatedWord As String
    Public Property Suspended As Boolean
    Public Property PracticedCorrectlyCount As Integer
    Public Property DateLastPracticed As Date
    Public Property TimeSinceLastPracticed As TimeSpan

    Public Sub New(id As Integer, fileName As String, word As String, translatedWord As String, suspended As Boolean, practicedCorrectlyCount As Integer, dateLastPracticed As Date)
        Me.Id = id
        Me.FileName = fileName
        Me.Word = word
        Me.TranslatedWord = translatedWord
        Me.Suspended = suspended
        Me.PracticedCorrectlyCount = practicedCorrectlyCount
        Me.DateLastPracticed = dateLastPracticed

        If Me.DateLastPracticed = Nothing Then
            Me.TimeSinceLastPracticed = TimeSpan.MaxValue
        Else
            Me.TimeSinceLastPracticed = DateTime.Now - Me.DateLastPracticed
        End If
    End Sub

    Public Sub updateTimeLastCorrectlyPracticed(myForm As Form1)
        ' Update class properties
        Me.DateLastPracticed = DateTime.Now
        Me.TimeSinceLastPracticed = DateTime.Now - Me.DateLastPracticed

        ' Update the translation record in the database
        Dim connection As SqlConnection = myForm.GetSqlConnection()
        Dim cmdStr As String = "UPDATE Translations SET date_last_practiced = @date WHERE id=@id"
        Dim updateTranslationCommand As SqlCommand = New SqlCommand(cmdStr, connection)
        updateTranslationCommand.Parameters.AddWithValue("@date", Me.DateLastPracticed)
        updateTranslationCommand.Parameters.AddWithValue("@id", Me.Id)
        myForm.ExecuteNonQuery(connection, updateTranslationCommand)
    End Sub

End Class
