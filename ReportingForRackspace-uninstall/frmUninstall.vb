Imports Microsoft.Win32
Imports System.Runtime.InteropServices

Public Class frmUninstall

    Inherits System.Windows.Forms.Form

    <DllImport("user32")> Private Shared Function FindWindow(ByVal lpClassName As String, ByVal lpWindowName As String) As IntPtr
    End Function

    Private Sub Form1_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Me.Hide()

        'Kill boss and worker if they are running

        Dim CoreProcess() As Process = Process.GetProcessesByName("ReportingForRackspace")


        If (CoreProcess.Length > 0) Then

            Dim ret As MsgBoxResult = MsgBox("Reporting For Rackspace is still running." & vbCrLf &
                                             "Do you want to have it automatically stopped and uninstalled?", MsgBoxStyle.YesNo, "Reporting For Rackspace - Uninstall")
            If ret = MsgBoxResult.No Then
                AllDone()
            End If

        End If

        If CoreProcess.Length > 0 Then
            For Each p As Process In CoreProcess
                p.Kill()
            Next
        End If

        Dim ProcID As Integer = Shell("unins000.exe", AppWinStyle.NormalFocus)

        AllDone()

    End Sub

    Private Sub AllDone()

        Process.GetCurrentProcess.Kill()

    End Sub

End Class
