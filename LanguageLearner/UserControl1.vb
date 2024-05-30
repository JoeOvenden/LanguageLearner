Imports System.Data.SqlClient
Imports System.Diagnostics.Eventing
Imports System.IO

Public Class UserControl1
    Dim myForm As Form1
    Dim fileContentsDictionary As Dictionary(Of String, List(Of Translation))

    Private Sub UserControl1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Make sure that the parent form is available
        If TypeOf Me.ParentForm Is Form1 Then
            myForm = CType(Me.ParentForm, Form1)
            fileContentsDictionary = myForm.fileContentsDictionary
            SetupVocabDataGrid()
            ListFiles()
        Else
            MessageBox.Show("Parent form is not yet available.")
        End If
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
        Dim connection As SqlConnection = myForm.GetSqlConnection()
        connection.Open()
        Dim selectFilesCommand As SqlCommand = New SqlCommand("SELECT * FROM Files ORDER BY Name;", connection)
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
                Dim translationsReader As SqlDataReader = GetSqlReader(connection, selectTranslationsCommand)
                Dim translationList As List(Of Translation) = New List(Of Translation)()

                ' Go through all translations and add them to the translation list
                While translationsReader.Read()
                    Dim id As String = translationsReader("id")
                    Dim word As String = translationsReader("word")
                    Dim translatedWord As String = translationsReader("translation")
                    Dim practicedCorrectlyCount As Integer = translationsReader("practiced_correctly_last_month")
                    Dim suspended As Boolean = translationsReader("suspended")
                    Dim dateLastPracticed As Date
                    Try
                        dateLastPracticed = translationsReader("date_last_practiced")
                    Catch ex As Exception
                        dateLastPracticed = Nothing
                    End Try

                    translationList.Add(New Translation(id, fileName, word, translatedWord, suspended, practicedCorrectlyCount, dateLastPracticed))
                End While

                ' Add the translation dictionary
                fileContentsDictionary.Add(fileName, translationList)
                translationsReader.Close()

            End If

        Next

        connection.Close()

    End Sub

    Private Sub displayVocab(fileName As String)
        dataGridVocab.Rows.Clear()
        ' Add rows for each word and translation
        For Each trans In fileContentsDictionary(fileName)
            dataGridVocab.Rows.Add(trans.Word, trans.TranslatedWord)
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
            Dim fileExists As Boolean = CheckFileExists(fileName)

            ' If the file exists check if the user wishes to overwrite it.
            ' If they do then delete the associated old files, otherwise exit the sub.
            If fileExists Then
                If Not UserWishesToOverwrite(fileName) Then
                    MessageBox.Show("File not overwritten.")
                    Return
                Else
                    DeleteTranslationsFromFile(fileName)
                End If
            End If

            ' Save the file content and show the new listed files
            SaveFileContent(fileContent, fileName)
            ListFiles()
        End If
    End Sub

    Private Sub DeleteTranslationsFromFile(fileName As String)
        ' Delete the translation records
        Dim connection As SqlConnection = myForm.GetSqlConnection()
        Dim deleteTranslationsString As String = "DELETE FROM Translations 
                                                   WHERE file_id IN (SELECT id FROM Files
	                                                                 WHERE name=@name);"
        Using command As SqlCommand = New SqlCommand(deleteTranslationsString, connection)
            command.Parameters.AddWithValue("@name", fileName)
            myForm.ExecuteNonQuery(connection, command)
        End Using

        ' Delete file records
        Using command As SqlCommand = New SqlCommand("DELETE FROM Files WHERE name=@name;", connection)
            command.Parameters.AddWithValue("@name", fileName)
            myForm.ExecuteNonQuery(connection, command)
        End Using

        ' Remove the dictionary from fileContentsDictionary
        fileContentsDictionary.Remove(fileName)
    End Sub

    Private Function UserWishesToOverwrite(fileName As String) As Boolean
        Dim result As DialogResult = MessageBox.Show(fileName & " already exists. Do you want to overwrite it?", "Confirm overwriting", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
        Return result = DialogResult.Yes
    End Function

    Private Function CheckFileExists(fileName As String) As Boolean
        Dim connection As SqlConnection = myForm.GetSqlConnection()
        Dim command As SqlCommand = New SqlCommand("SELECT * FROM Files WHERE name=@name;", connection)
        command.Parameters.AddWithValue("@name", fileName)
        Dim reader As SqlDataReader = GetSqlReader(connection, command)
        Return reader.Read()
    End Function

    Private Sub SaveFileContent(content As String(), fileName As String)
        ' Create file record
        Dim connection As SqlConnection = myForm.GetSqlConnection()
        Dim insertFileCommand As SqlCommand = New SqlCommand("INSERT INTO Files (name) VALUES (@fileName);", connection)
        insertFileCommand.Parameters.AddWithValue("@fileName", fileName)
        myForm.ExecuteNonQuery(connection, insertFileCommand)

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
            myForm.ExecuteNonQuery(connection, insertTranslationCommand, False)
        Next
        connection.Close()
        MessageBox.Show("File saved.")

    End Sub

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

    Private Sub ExecuteNonQuery(connection As SqlConnection, command As SqlCommand, Optional closeConnection As Boolean = True)
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

    Private Sub btnExecuteSqlFile_Click(sender As Object, e As EventArgs) Handles btnExecuteSqlFile.Click
        ' Display the OpenFileDialog to allow the user to select a file
        OpenFileDialog2.Filter = "SQL Files (*.sql)|*.sql"
        OpenFileDialog2.Title = "Select a SQL File"
        Dim result As DialogResult = OpenFileDialog2.ShowDialog()
        If result = DialogResult.OK Then
            ' Read the selected CSV file
            Dim filePath As String = OpenFileDialog2.FileName
            Dim fileContent As String() = File.ReadAllLines(filePath)
            Dim fileName As String = Path.GetFileName(filePath)

            ExecuteSqlFile(myForm.GetSqlConnection(), filePath)
        ElseIf result = DialogResult.Cancel Then

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

    Private Sub btnPractice_Click(sender As Object, e As EventArgs) Handles btnPractice.Click
        myForm.SaveFilesToPractice()
        If myForm.filesToPractice.Count = 0 Then
            MessageBox.Show("Must add at least 1 file to be practiced.")
            Return
        End If
        Dim uc2 As UserControl2 = myForm.uc2
        uc2.Dock = DockStyle.Fill
        myForm.panelContainer.Controls.Clear()
        myForm.panelContainer.Controls.Add(uc2)
    End Sub

    Private Sub SetAllCheckedListBox(listBox As CheckedListBox, checked As Boolean)
        ' Set all the items in the CheckedListBox to either be checked or unchecked
        For i As Integer = 0 To listBox.Items.Count - 1
            listBox.SetItemChecked(i, checked)
        Next
    End Sub

    Private Sub btnCheckAllFiles_Click(sender As Object, e As EventArgs) Handles btnCheckAllFiles.Click
        SetAllCheckedListBox(checkedListBoxFiles, True)
    End Sub

    Private Sub btnUncheckAll_Click(sender As Object, e As EventArgs) Handles btnUncheckAll.Click
        SetAllCheckedListBox(checkedListBoxFiles, False)
    End Sub
End Class
