<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Form1
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
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
        Me.OpenFileDialog1 = New System.Windows.Forms.OpenFileDialog()
        Me.dataGridVocab = New System.Windows.Forms.DataGridView()
        Me.ColorDialog1 = New System.Windows.Forms.ColorDialog()
        Me.checkedListBoxFiles = New System.Windows.Forms.CheckedListBox()
        Me.OpenFileDialog2 = New System.Windows.Forms.OpenFileDialog()
        Me.btnExecuteSqlFile = New System.Windows.Forms.Button()
        CType(Me.dataGridVocab, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'btnUploadVocab
        '
        Me.btnUploadVocab.Location = New System.Drawing.Point(33, 21)
        Me.btnUploadVocab.Name = "btnUploadVocab"
        Me.btnUploadVocab.Size = New System.Drawing.Size(170, 50)
        Me.btnUploadVocab.TabIndex = 0
        Me.btnUploadVocab.Text = "Upload Vocabulary File"
        Me.btnUploadVocab.UseVisualStyleBackColor = True
        '
        'OpenFileDialog1
        '
        Me.OpenFileDialog1.FileName = "OpenFileDialog1"
        '
        'dataGridVocab
        '
        Me.dataGridVocab.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight
        Me.dataGridVocab.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dataGridVocab.Location = New System.Drawing.Point(232, 108)
        Me.dataGridVocab.Name = "dataGridVocab"
        Me.dataGridVocab.Size = New System.Drawing.Size(548, 304)
        Me.dataGridVocab.TabIndex = 3
        '
        'checkedListBoxFiles
        '
        Me.checkedListBoxFiles.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.checkedListBoxFiles.FormattingEnabled = True
        Me.checkedListBoxFiles.Location = New System.Drawing.Point(33, 108)
        Me.checkedListBoxFiles.Name = "checkedListBoxFiles"
        Me.checkedListBoxFiles.Size = New System.Drawing.Size(170, 298)
        Me.checkedListBoxFiles.TabIndex = 4
        '
        'OpenFileDialog2
        '
        Me.OpenFileDialog2.FileName = "OpenFileDialog2"
        '
        'btnExecuteSqlFile
        '
        Me.btnExecuteSqlFile.Location = New System.Drawing.Point(668, 21)
        Me.btnExecuteSqlFile.Name = "btnExecuteSqlFile"
        Me.btnExecuteSqlFile.Size = New System.Drawing.Size(112, 50)
        Me.btnExecuteSqlFile.TabIndex = 5
        Me.btnExecuteSqlFile.Text = "Execute SQL File"
        Me.btnExecuteSqlFile.UseVisualStyleBackColor = True
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(812, 450)
        Me.Controls.Add(Me.btnExecuteSqlFile)
        Me.Controls.Add(Me.checkedListBoxFiles)
        Me.Controls.Add(Me.dataGridVocab)
        Me.Controls.Add(Me.btnUploadVocab)
        Me.Name = "Form1"
        Me.Text = "Form1"
        CType(Me.dataGridVocab, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents btnUploadVocab As Button
    Friend WithEvents OpenFileDialog1 As OpenFileDialog
    Friend WithEvents dataGridVocab As DataGridView
    Friend WithEvents ColorDialog1 As ColorDialog
    Friend WithEvents checkedListBoxFiles As CheckedListBox
    Friend WithEvents OpenFileDialog2 As OpenFileDialog
    Friend WithEvents btnExecuteSqlFile As Button
End Class
