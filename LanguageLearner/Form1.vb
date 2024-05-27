Imports System.IO
Imports System.Text
Imports System.Data.SqlClient

Public Class Form1
    Dim fileContentsDictionary As New Dictionary(Of String, Dictionary(Of String, String))()

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        SetupVocabDataGrid()
        ListFiles()
    End Sub

    Private Sub SetupVocabDataGrid()
        dataGridVocab.Columns.Add("Word", "Word")
        dataGridVocab.Columns.Add("Translation", "Translation")
        dataGridVocab.DefaultCellStyle.Font = New Font("Arial", 20)
        dataGridVocab.Columns("Translation").DefaultCellStyle.Font = New Font("Arial", 28)
        dataGridVocab.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells
        dataGridVocab.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells
    End Sub

    Private Function GetTopDirectory() As String
        Return Directory.GetParent(Directory.GetParent(Application.StartupPath).FullName).FullName
    End Function

    Private Function GetVocabDirectory() As String
        Dim parentDirectory As String = GetTopDirectory()
        Return Path.Combine(parentDirectory, "vocab-files\")
    End Function

    Private Sub ListFiles()
        ' Clear the list box containing file names
        checkedListBoxFiles.Items.Clear()

        ' Get reader for files
        Dim connection As SqlConnection = GetSqlConnection()
        connection.Open()
        Dim selectFilesCommand As SqlCommand = New SqlCommand("SELECT * FROM Files;", connection)
        Dim fileDictionary As Dictionary(Of Integer, String) = New Dictionary(Of Integer, String)

        Dim fileReader As SqlDataReader = GetSqlReader(connection, selectFilesCommand)
        ' Get a dictionary mapping file ids to file names for all files
        While fileReader.Read()

            Dim fileId As Integer = fileReader("id")
            Dim fileName As String = fileReader("name")
            fileDictionary.Add(fileId, fileName)

        End While
        fileReader.Close()

        For Each fileId As Integer In fileDictionary.Keys

            Dim fileName As String = fileDictionary(fileId)

            ' Add the file name to the list box of file names
            checkedListBoxFiles.Items.Add(fileName)

            ' Get an sql reader containing al of the translation from the current file
            Dim selectTranslationsCommand As SqlCommand = New SqlCommand("SELECT * FROM Translations WHERE file_id=@fileId;", connection)
            selectTranslationsCommand.Parameters.AddWithValue("fileId", fileId)

            ' If the fileContentsDictionary hasn't loaded the translation from the file, then load them in
            If Not fileContentsDictionary.ContainsKey(fileName) Then

                ' Create new translation dictionary for the file
                Dim translationDict As New Dictionary(Of String, String)
                Dim translationsReader As SqlDataReader = GetSqlReader(connection, selectTranslationsCommand)

                ' Go through all translations and add them to the dictionary
                While translationsReader.Read()

                    Dim word As String = translationsReader("word")
                    Dim translation As String = translationsReader("translation")

                    translationDict.Add(word, translation)

                End While

                ' Add the translation dictionary
                fileContentsDictionary.Add(fileName, translationDict)
                translationsReader.Close()

            End If

        Next

        connection.Close()

    End Sub

    Private Sub displayVocab(fileName As String)
        dataGridVocab.Rows.Clear()
        ' Add rows for each word and translation
        For Each kvp As KeyValuePair(Of String, String) In fileContentsDictionary(fileName)
            Dim word As String = kvp.Key
            Dim translation As String = kvp.Value
            dataGridVocab.Rows.Add(word, translation)
        Next
    End Sub

    Private Sub CheckedListBoxFiles_SelectedIndexChanged(sender As Object, e As EventArgs) Handles checkedListBoxFiles.SelectedIndexChanged
        ' Check if an item is selected in the ListBox
        If checkedListBoxFiles.SelectedIndex <> -1 Then
            ' Display the contects of the vocab file
            Dim fileName As String = checkedListBoxFiles.SelectedItem.ToString()
            displayVocab(fileName)
        End If
    End Sub



    Private Sub btnUploadVocab_Click(sender As Object, e As EventArgs) Handles btnUploadVocab.Click
        ' Display the OpenFileDialog to allow the user to select a file
        OpenFileDialog1.Filter = "CSV Files (*.csv)|*.csv"
        OpenFileDialog1.Title = "Select a CSV File"

        If OpenFileDialog1.ShowDialog() = DialogResult.OK Then
            ' Read the selected CSV file
            Dim filePath As String = OpenFileDialog1.FileName
            Dim fileContent As String() = File.ReadAllLines(filePath)
            Dim fileName As String = Path.GetFileName(filePath)

            SaveFileContent(fileContent, fileName)
            ListFiles()

        End If
    End Sub

    Private Sub SaveFileContent(content As String(), fileName As String)
        ' Create file record
        Dim connection As SqlConnection = GetSqlConnection()
        Dim insertFileCommand As SqlCommand = New SqlCommand("INSERT INTO Files (name) VALUES (@fileName);", connection)
        insertFileCommand.Parameters.AddWithValue("@fileName", fileName)
        ExecuteNonQuery(connection, insertFileCommand)

        ' Get the id of the new file record
        Dim selectFileCommand As SqlCommand = New SqlCommand("SELECT id FROM Files WHERE name=@fileName ORDER BY id DESC;", connection)
        selectFileCommand.Parameters.AddWithValue("@fileName", fileName)
        Dim reader As SqlDataReader = GetSqlReader(connection, selectFileCommand)
        Dim fileId As Integer
        If reader.Read() Then
            fileId = reader("id")
        Else
            MessageBox.Show("Error: no file found with name: " & fileName)
            Return
        End If
        reader.Close()

        ' Create a translation record for each translation within the file
        If connection.State = ConnectionState.Closed Then
            connection.Open()
        End If

        For Each translation As String In content
            Dim words As String() = translation.Split(",")
            Dim insertTranslationQuery As String = "INSERT INTO Translations (file_id, word, translation) 
                VALUES (@fileId, @word, @translation)"

            Dim insertTranslationCommand As SqlCommand = New SqlCommand(insertTranslationQuery, connection)
            insertTranslationCommand.Parameters.AddWithValue("@fileId", fileId)
            insertTranslationCommand.Parameters.AddWithValue("@word", words(0))
            insertTranslationCommand.Parameters.AddWithValue("@translation", words(1))
            ExecuteNonQuery(connection, insertTranslationCommand, False)
        Next
        connection.Close()

    End Sub


    Private Function GetSqlConnection() As SqlConnection
        Return New SqlConnection("Data Source=(LocalDB)\MSSQLLocalDB;
            AttachDbFilename=C:\Users\joeov\source\repos\LanguageLearner\LanguageLearner\languageLearner.db.mdf;
            Integrated Security=True;Connect Timeout=30")
    End Function


    Private Function GetSqlReader(connection, command) As SqlDataReader
        Try
            If connection.state = ConnectionState.Closed Then
                connection.Open()
            End If
            Return command.ExecuteReader()
        Catch ex As Exception
            MessageBox.Show(ex.ToString())
        End Try
    End Function


    Private Sub ExecuteNonQueryMultiple(connection As SqlConnection, commands As SqlCommand())
        If connection.State = ConnectionState.Closed Then
            connection.Open()
        End If
        For Each command As SqlCommand In commands
            ExecuteNonQuery(connection, command, False)
        Next
        connection.Close()
    End Sub


    Private Sub ExecuteNonQuery(connection As SqlConnection, command As SqlCommand, Optional closeConnection As Boolean = True)
        Try
            If (connection.State = ConnectionState.Closed) Then
                connection.Open()
            End If
            command.ExecuteNonQuery()
            MessageBox.Show("Success: " & command.ToString(), "SQL Result", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Catch ex As Exception
            MessageBox.Show(ex.ToString())
        Finally
            If (closeConnection = True AndAlso connection.State = ConnectionState.Open) Then
                connection.Close()
            End If
        End Try
    End Sub

    Private Sub btnExecuteSqlFile_Click(sender As Object, e As EventArgs) Handles btnExecuteSqlFile.Click
        ' Display the OpenFileDialog to allow the user to select a file
        OpenFileDialog2.Filter = "SQL Files (*.sql)|*.sql"
        OpenFileDialog2.Title = "Select a SQL File"

        If OpenFileDialog2.ShowDialog() = DialogResult.OK Then
            ' Read the selected CSV file
            Dim filePath As String = OpenFileDialog2.FileName
            Dim fileContent As String() = File.ReadAllLines(filePath)
            Dim fileName As String = Path.GetFileName(filePath)

            ExecuteSqlFile(GetSqlConnection(), filePath)
        Else
            MessageBox.Show("Invalid file.")
        End If
    End Sub

    Public Sub ExecuteSqlFile(connection As SqlConnection, filePath As String)
        ' Read SQL statements from the file
        Dim sqlStatements As String = File.ReadAllText(filePath)

        ' Split SQL statements by semicolon to separate them
        Dim statements As String() = sqlStatements.Split({";"}, StringSplitOptions.RemoveEmptyEntries)

        If connection.State = ConnectionState.Closed Then
            connection.Open()
        End If

        ' Create SqlCommand for each statement and execute it
        For Each statement As String In statements
            Using command As New SqlCommand(statement, connection)
                command.ExecuteNonQuery()
            End Using
        Next
    End Sub
End Class
