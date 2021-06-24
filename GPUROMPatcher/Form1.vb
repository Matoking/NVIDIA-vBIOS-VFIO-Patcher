Imports System.ComponentModel
Imports System.IO
Public Class patcherForm
    Dim newRegex As String = "" 'Regex string
    Dim romPath As String = "" 'Path to source ROM
    Dim patchPath As String = "" 'Path to output ROM
    Dim scriptPath As String = Environment.CurrentDirectory + "\patchervb.py"
    Dim dfs As String = "" 'Disable footer strip argument, for old GPUs
    Private Sub PatchBut_Click(sender As Object, e As EventArgs) Handles PatchBut.Click
        System.IO.File.WriteAllBytes(scriptPath, My.Resources.patchervb)
        Dim patcher As New Process
        patcher.StartInfo.Arguments = "-i " + romPath & " " & "-o " + patchPath & " " & "-r " + newRegex + dfs
        patcher.StartInfo.FileName = scriptPath
        patcher.Start()
        dfs = "" 'reset dfs to nothing in case you patch another rom after the first
    End Sub
    Private Sub FirstBox_SelectedIndexChanged(sender As Object, e As EventArgs) Handles firstBox.SelectedIndexChanged
        Select Case firstBox.Text 'Sets regex based on input from GPU box
            Case "RTX 3000 Series"
                newRegex = "564e(([a-z]|[0-9]){636})(4e504453)(([a-z]|[0-9]){56})(4e504445)"
            Case "RTX 2000 Series"
                newRegex = "564e(([a-z]|[0-9]){476})(4e504453)(([a-z]|[0-9]){56})(4e504445)"
            Case "GTX 1000 Series"
                newRegex = "564e(([a-z]|[0-9]){348})(4e504453)(([a-z]|[0-9]){56})(4e504445)"
            Case "GTX 900 Series"
                newRegex = "564e(([a-z]|[0-9]){188})(4e504453)(([a-z]|[0-9]){56})(4e504445)"
            Case "GTX 700 Series"
                newRegex = "564e(([a-z]|[0-9]){124})(4e504453)(([a-z]|[0-9]){56})(4e504445)"
            Case "GTX 600 Series"
                newRegex = "564e(([a-z]|[0-9]){124})(4e504453)(([a-z]|[0-9]){56})(4e504445)"
            Case "GTX 500 Series"
                newRegex = "564e(([a-z]|[0-9]){348})(4e504453)(([a-z]|[0-9]){56})(4e504445)"
                dfs = " --disable-footer-strip"
            Case "GTX 400 Series"
                newRegex = "564e(([a-z]|[0-9]){348})(4e504453)(([a-z]|[0-9]){56})(4e504445)"
                dfs = " --disable-footer-strip"
        End Select
    End Sub
    Private Sub exitBut_Click(sender As Object, e As EventArgs) Handles exitBut.Click
        Me.Close()
    End Sub
    Private Sub sauceBut_Click(sender As Object, e As EventArgs) Handles sauceBut.Click
        openRomDialog.Title = "Select source GPU ROM file."
        openRomDialog.Filter = "|*.rom|All Files|*.*"
        openRomDialog.ShowDialog()
        romPath = openRomDialog.FileName
        patchPath = romPath.Substring(0, romPath.Length - 4) + "patch.rom" 'Set output file to name of the input file + patch.rom
    End Sub
    Private Sub Form1_Closing(sender As Object, e As CancelEventArgs) Handles Me.Closing
        If File.Exists(scriptPath) Then 'Remove script from directory
            File.Delete(scriptPath)
        End If
    End Sub
End Class
