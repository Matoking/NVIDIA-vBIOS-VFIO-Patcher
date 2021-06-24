<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class patcherForm
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
        Me.PatchBut = New System.Windows.Forms.Button()
        Me.firstBox = New System.Windows.Forms.ComboBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.exitBut = New System.Windows.Forms.Button()
        Me.sauceBut = New System.Windows.Forms.Button()
        Me.openRomDialog = New System.Windows.Forms.OpenFileDialog()
        Me.SuspendLayout()
        '
        'PatchBut
        '
        Me.PatchBut.Location = New System.Drawing.Point(90, 106)
        Me.PatchBut.Name = "PatchBut"
        Me.PatchBut.Size = New System.Drawing.Size(75, 23)
        Me.PatchBut.TabIndex = 0
        Me.PatchBut.Text = "Patch ROM"
        Me.PatchBut.UseVisualStyleBackColor = True
        '
        'firstBox
        '
        Me.firstBox.FormattingEnabled = True
        Me.firstBox.Items.AddRange(New Object() {"RTX 3000 Series", "RTX 2000 Series", "GTX 1000 Series", "GTX 900 Series", "GTX 700 Series", "GTX 600 Series", "GTX 500 Series", "GTX 400 Series"})
        Me.firstBox.Location = New System.Drawing.Point(71, 36)
        Me.firstBox.Name = "firstBox"
        Me.firstBox.Size = New System.Drawing.Size(121, 21)
        Me.firstBox.TabIndex = 1
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(102, 9)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(63, 13)
        Me.Label1.TabIndex = 2
        Me.Label1.Text = "Select GPU"
        '
        'exitBut
        '
        Me.exitBut.Location = New System.Drawing.Point(90, 139)
        Me.exitBut.Name = "exitBut"
        Me.exitBut.Size = New System.Drawing.Size(75, 23)
        Me.exitBut.TabIndex = 3
        Me.exitBut.Text = "Exit"
        Me.exitBut.UseVisualStyleBackColor = True
        '
        'sauceBut
        '
        Me.sauceBut.Location = New System.Drawing.Point(71, 63)
        Me.sauceBut.Name = "sauceBut"
        Me.sauceBut.Size = New System.Drawing.Size(121, 23)
        Me.sauceBut.TabIndex = 5
        Me.sauceBut.Text = "Select Source ROM"
        Me.sauceBut.UseVisualStyleBackColor = True
        '
        'openRomDialog
        '
        Me.openRomDialog.InitialDirectory = "C:\"
        '
        'patcherForm
        '
        Me.AcceptButton = Me.PatchBut
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Me.exitBut
        Me.ClientSize = New System.Drawing.Size(269, 174)
        Me.Controls.Add(Me.sauceBut)
        Me.Controls.Add(Me.exitBut)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.firstBox)
        Me.Controls.Add(Me.PatchBut)
        Me.MaximumSize = New System.Drawing.Size(285, 213)
        Me.MinimumSize = New System.Drawing.Size(285, 213)
        Me.Name = "patcherForm"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "ROM Patcher GUI"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents PatchBut As Button
    Friend WithEvents firstBox As ComboBox
    Friend WithEvents Label1 As Label
    Friend WithEvents exitBut As Button
    Friend WithEvents sauceBut As Button
    Friend WithEvents openRomDialog As OpenFileDialog
End Class
