<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class UserControl1
    Inherits System.Windows.Forms.UserControl

    'UserControl overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.btnUploadVocab = New System.Windows.Forms.Button()
        Me.checkedListBoxFiles = New System.Windows.Forms.CheckedListBox()
        Me.dataGridVocab = New System.Windows.Forms.DataGridView()
        Me.btnExecuteSqlFile = New System.Windows.Forms.Button()
        Me.btnPractice = New System.Windows.Forms.Button()
        Me.OpenFileDialog1 = New System.Windows.Forms.OpenFileDialog()
        Me.OpenFileDialog2 = New System.Windows.Forms.OpenFileDialog()
        Me.OpenFileDialog3 = New System.Windows.Forms.OpenFileDialog()
        Me.btnCheckAllFiles = New System.Windows.Forms.Button()
        Me.btnUncheckAll = New System.Windows.Forms.Button()
        CType(Me.dataGridVocab, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'btnUploadVocab
        '
        Me.btnUploadVocab.Location = New System.Drawing.Point(49, 49)
        Me.btnUploadVocab.Name = "btnUploadVocab"
        Me.btnUploadVocab.Size = New System.Drawing.Size(208, 46)
        Me.btnUploadVocab.TabIndex = 1
        Me.btnUploadVocab.Text = "Upload Vocab File"
        Me.btnUploadVocab.UseVisualStyleBackColor = True
        '
        'checkedListBoxFiles
        '
        Me.checkedListBoxFiles.FormattingEnabled = True
        Me.checkedListBoxFiles.Location = New System.Drawing.Point(49, 166)
        Me.checkedListBoxFiles.Name = "checkedListBoxFiles"
        Me.checkedListBoxFiles.Size = New System.Drawing.Size(208, 334)
        Me.checkedListBoxFiles.TabIndex = 2
        '
        'dataGridVocab
        '
        Me.dataGridVocab.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dataGridVocab.Location = New System.Drawing.Point(306, 136)
        Me.dataGridVocab.Name = "dataGridVocab"
        Me.dataGridVocab.Size = New System.Drawing.Size(552, 364)
        Me.dataGridVocab.TabIndex = 3
        '
        'btnExecuteSqlFile
        '
        Me.btnExecuteSqlFile.Location = New System.Drawing.Point(745, 49)
        Me.btnExecuteSqlFile.Name = "btnExecuteSqlFile"
        Me.btnExecuteSqlFile.Size = New System.Drawing.Size(113, 46)
        Me.btnExecuteSqlFile.TabIndex = 4
        Me.btnExecuteSqlFile.Text = "Execute SQL File"
        Me.btnExecuteSqlFile.UseVisualStyleBackColor = True
        '
        'btnPractice
        '
        Me.btnPractice.Location = New System.Drawing.Point(306, 49)
        Me.btnPractice.Name = "btnPractice"
        Me.btnPractice.Size = New System.Drawing.Size(135, 46)
        Me.btnPractice.TabIndex = 5
        Me.btnPractice.Text = "Practice"
        Me.btnPractice.UseVisualStyleBackColor = True
        '
        'OpenFileDialog1
        '
        Me.OpenFileDialog1.FileName = "OpenFileDialog1"
        '
        'OpenFileDialog2
        '
        Me.OpenFileDialog2.FileName = "OpenFileDialog2"
        '
        'OpenFileDialog3
        '
        Me.OpenFileDialog3.FileName = "OpenFileDialog3"
        '
        'btnCheckAllFiles
        '
        Me.btnCheckAllFiles.Location = New System.Drawing.Point(49, 137)
        Me.btnCheckAllFiles.Name = "btnCheckAllFiles"
        Me.btnCheckAllFiles.Size = New System.Drawing.Size(98, 23)
        Me.btnCheckAllFiles.TabIndex = 6
        Me.btnCheckAllFiles.Text = "Check All"
        Me.btnCheckAllFiles.UseVisualStyleBackColor = True
        '
        'btnUncheckAll
        '
        Me.btnUncheckAll.Location = New System.Drawing.Point(159, 136)
        Me.btnUncheckAll.Name = "btnUncheckAll"
        Me.btnUncheckAll.Size = New System.Drawing.Size(98, 23)
        Me.btnUncheckAll.TabIndex = 7
        Me.btnUncheckAll.Text = "Uncheck All"
        Me.btnUncheckAll.UseVisualStyleBackColor = True
        '
        'UserControl1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.btnUncheckAll)
        Me.Controls.Add(Me.btnCheckAllFiles)
        Me.Controls.Add(Me.btnPractice)
        Me.Controls.Add(Me.btnExecuteSqlFile)
        Me.Controls.Add(Me.dataGridVocab)
        Me.Controls.Add(Me.checkedListBoxFiles)
        Me.Controls.Add(Me.btnUploadVocab)
        Me.Name = "UserControl1"
        Me.Size = New System.Drawing.Size(996, 590)
        CType(Me.dataGridVocab, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents btnUploadVocab As Button
    Friend WithEvents checkedListBoxFiles As CheckedListBox
    Friend WithEvents dataGridVocab As DataGridView
    Friend WithEvents btnExecuteSqlFile As Button
    Friend WithEvents btnPractice As Button
    Friend WithEvents OpenFileDialog1 As OpenFileDialog
    Friend WithEvents OpenFileDialog2 As OpenFileDialog
    Friend WithEvents OpenFileDialog3 As OpenFileDialog
    Friend WithEvents btnCheckAllFiles As Button
    Friend WithEvents btnUncheckAll As Button
End Class
