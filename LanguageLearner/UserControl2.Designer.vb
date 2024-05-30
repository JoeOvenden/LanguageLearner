<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class UserControl2
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
        Me.lblWord1 = New System.Windows.Forms.Label()
        Me.btnMenu = New System.Windows.Forms.Button()
        Me.btnNextWord = New System.Windows.Forms.Button()
        Me.labelArrow = New System.Windows.Forms.Label()
        Me.lblWord2 = New System.Windows.Forms.Label()
        Me.txtBoxTranslation = New System.Windows.Forms.TextBox()
        Me.btnSwitchMode = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'lblWord1
        '
        Me.lblWord1.AutoSize = True
        Me.lblWord1.Font = New System.Drawing.Font("Yu Gothic", 36.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblWord1.Location = New System.Drawing.Point(107, 206)
        Me.lblWord1.Name = "lblWord1"
        Me.lblWord1.Size = New System.Drawing.Size(178, 62)
        Me.lblWord1.TabIndex = 0
        Me.lblWord1.Text = "Label1"
        '
        'btnMenu
        '
        Me.btnMenu.Location = New System.Drawing.Point(118, 42)
        Me.btnMenu.Name = "btnMenu"
        Me.btnMenu.Size = New System.Drawing.Size(106, 50)
        Me.btnMenu.TabIndex = 1
        Me.btnMenu.Text = "Back To Menu"
        Me.btnMenu.UseVisualStyleBackColor = True
        '
        'btnNextWord
        '
        Me.btnNextWord.Location = New System.Drawing.Point(249, 42)
        Me.btnNextWord.Name = "btnNextWord"
        Me.btnNextWord.Size = New System.Drawing.Size(110, 50)
        Me.btnNextWord.TabIndex = 2
        Me.btnNextWord.Text = "Next"
        Me.btnNextWord.UseVisualStyleBackColor = True
        '
        'labelArrow
        '
        Me.labelArrow.AutoSize = True
        Me.labelArrow.Font = New System.Drawing.Font("Yu Gothic", 36.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.labelArrow.Location = New System.Drawing.Point(442, 206)
        Me.labelArrow.Name = "labelArrow"
        Me.labelArrow.Size = New System.Drawing.Size(83, 62)
        Me.labelArrow.TabIndex = 3
        Me.labelArrow.Text = "->"
        '
        'lblWord2
        '
        Me.lblWord2.AutoSize = True
        Me.lblWord2.Font = New System.Drawing.Font("Yu Gothic", 36.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblWord2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblWord2.Location = New System.Drawing.Point(628, 206)
        Me.lblWord2.Name = "lblWord2"
        Me.lblWord2.Size = New System.Drawing.Size(178, 62)
        Me.lblWord2.TabIndex = 4
        Me.lblWord2.Text = "Label2"
        '
        'txtBoxTranslation
        '
        Me.txtBoxTranslation.Font = New System.Drawing.Font("Yu Gothic", 20.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtBoxTranslation.Location = New System.Drawing.Point(118, 297)
        Me.txtBoxTranslation.Name = "txtBoxTranslation"
        Me.txtBoxTranslation.Size = New System.Drawing.Size(194, 51)
        Me.txtBoxTranslation.TabIndex = 5
        '
        'btnSwitchMode
        '
        Me.btnSwitchMode.Location = New System.Drawing.Point(382, 42)
        Me.btnSwitchMode.Name = "btnSwitchMode"
        Me.btnSwitchMode.Size = New System.Drawing.Size(108, 50)
        Me.btnSwitchMode.TabIndex = 6
        Me.btnSwitchMode.Text = "Switch Mode"
        Me.btnSwitchMode.UseVisualStyleBackColor = True
        '
        'UserControl2
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.btnSwitchMode)
        Me.Controls.Add(Me.txtBoxTranslation)
        Me.Controls.Add(Me.lblWord2)
        Me.Controls.Add(Me.labelArrow)
        Me.Controls.Add(Me.btnNextWord)
        Me.Controls.Add(Me.btnMenu)
        Me.Controls.Add(Me.lblWord1)
        Me.Name = "UserControl2"
        Me.Size = New System.Drawing.Size(977, 553)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents lblWord1 As Label
    Friend WithEvents btnMenu As Button
    Friend WithEvents btnNextWord As Button
    Friend WithEvents labelArrow As Label
    Friend WithEvents lblWord2 As Label
    Friend WithEvents txtBoxTranslation As TextBox
    Friend WithEvents btnSwitchMode As Button
End Class
