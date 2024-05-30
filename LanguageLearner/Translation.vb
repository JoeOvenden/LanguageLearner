Public Class Translation
    Public Property Id As Integer
    Public Property FileName As String
    Public Property Word As String
    Public Property TranslatedWord As String
    Public Property Suspended As Boolean
    Public Property practicedCorrectlyCount As Integer
    Public Property DateLastPracticed As Date

    Public Sub New(id As Integer, fileName As String, word As String, translatedWord As String, suspended As Boolean, practicedCorrectlyCount As Integer, dateLastPracticed As Date)
        Me.Id = id
        Me.FileName = fileName
        Me.Word = word
        Me.TranslatedWord = translatedWord
        Me.Suspended = suspended
        Me.practicedCorrectlyCount = practicedCorrectlyCount
        Me.DateLastPracticed = dateLastPracticed
    End Sub

End Class
