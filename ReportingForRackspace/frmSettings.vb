Imports System.ComponentModel

Public Class frmSettings

    Private lFilesToInclude As String
    Private lFilesToExclude As String
    Private Sub frmSettings_Load(sender As Object, e As EventArgs) Handles Me.Load

        Me.Icon = My.Resources.logo
        Me.Text = My.Application.Info.Title & " - Settings"

        Me.tbUserID.Text = My.Settings.UserID
        Me.tbAPIKey.Text = modSecurity.EncryptDecryptClass.Decrypt(My.Settings.APIKey)

        Me.cbDeleteDownloadedLogsFromCloud.Checked = My.Settings.DeleteDowloadLogsFromCloud

        Me.cbDateRange.Text = My.Settings.DateRange

        Me.dtpStartDate.Visible = cbDateRange.Text = "Specific range"
        Me.dtpEndDate.Visible = Me.dtpStartDate.Visible
        Me.lblTo.Visible = Me.dtpEndDate.Visible

        Me.dtpStartDate.Value = My.Settings.StartDate
        Me.dtpEndDate.Value = My.Settings.EndDate

        Me.cbFiles.Text = My.Settings.IncludeExcludeFlag

        Me.tbFilesToIncludeOrExclude.Visible = (cbFiles.Text <> "All files")
        Me.btnOfferIncludeFiles.Visible = tbFilesToIncludeOrExclude.Visible

        Select Case Me.cbFiles.Text
            Case = "All files"
                tbFilesToIncludeOrExclude.Text = String.Empty
            Case = "Include specific files only"
                tbFilesToIncludeOrExclude.Text = My.Settings.FilesToInclude
            Case = "Exclude specific files"
                tbFilesToIncludeOrExclude.Text = My.Settings.FilesToExclude
        End Select

        lFilesToInclude = My.Settings.FilesToInclude
        lFilesToExclude = My.Settings.FilesToExclude

        Me.cbShowHeaderChecks.Checked = My.Settings.ShowHeaderChecks
        Me.cbIgnore400Series.Checked = My.Settings.Ignore400Requests
        Me.cbEquate.Checked = My.Settings.EquatePartialDownloads

        Me.tbDataDirectory.Text = gDataDirectory

        gSettingsClosedWithAnOK = False

    End Sub

    Private Sub btnOK_Click(sender As Object, e As EventArgs) Handles btnOK.Click

        If (dtpStartDate.Value > dtpEndDate.Value) OrElse (dtpEndDate.Value < dtpStartDate.Value) Then

            Beep()
            Call MsgBox("Start date must be less than or equal to end date!", vbExclamation, "Reporting for Rackspace")

        ElseIf (cbFiles.Text = "Include specific files only") AndAlso (tbFilesToIncludeOrExclude.Text.Trim.Length = 0) Then

            Beep()
            Call MsgBox("When the option 'Include specific files only' is selected, the list of files to report may not be blank!", vbExclamation, "Reporting for Rackspace")

        Else

            If SomethingChanged() Then

                My.Settings.UserID = tbUserID.Text
                My.Settings.APIKey = modSecurity.EncryptDecryptClass.Encrypt(tbAPIKey.Text)
                My.Settings.DeleteDowloadLogsFromCloud = cbDeleteDownloadedLogsFromCloud.Checked

                My.Settings.IncludeExcludeFlag = cbFiles.Text

                My.Settings.FilesToInclude = lFilesToInclude
                My.Settings.FilesToExclude = lFilesToExclude

                My.Settings.DateRange = cbDateRange.Text

                My.Settings.StartDate = dtpStartDate.Value
                My.Settings.EndDate = dtpEndDate.Value

                My.Settings.ShowHeaderChecks = cbShowHeaderChecks.Checked
                My.Settings.Ignore400Requests = cbIgnore400Series.Checked
                My.Settings.EquatePartialDownloads = cbEquate.Checked

                My.Settings.Save()

                gSettingsClosedWithAnOK = True

            End If

            Me.Close()

        End If

    End Sub


    Private Function SomethingChanged() As Boolean

        Return (My.Settings.UserID <> tbUserID.Text) OrElse
            (My.Settings.APIKey <> modSecurity.EncryptDecryptClass.Encrypt(tbAPIKey.Text)) OrElse
            (My.Settings.DeleteDowloadLogsFromCloud <> cbDeleteDownloadedLogsFromCloud.Checked) OrElse
            (My.Settings.IncludeExcludeFlag <> cbFiles.Text) OrElse
            (My.Settings.FilesToInclude <> lFilesToInclude) OrElse
            (My.Settings.FilesToExclude <> lFilesToExclude) OrElse
            (My.Settings.DateRange <> cbDateRange.Text) OrElse
            (My.Settings.StartDate <> dtpStartDate.Value) OrElse
            (My.Settings.EndDate <> dtpEndDate.Value) OrElse
            (My.Settings.ShowHeaderChecks <> cbShowHeaderChecks.Checked) OrElse
            (My.Settings.Ignore400Requests <> cbIgnore400Series.Checked) OrElse
            (My.Settings.EquatePartialDownloads <> cbEquate.Checked)

    End Function

    Private Sub cbDateRange_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbDateRange.SelectedIndexChanged

        Me.dtpStartDate.Visible = cbDateRange.Text = "Specific range"
        Me.dtpEndDate.Visible = Me.dtpStartDate.Visible
        Me.lblTo.Visible = Me.dtpEndDate.Visible

    End Sub

    Private Sub btnOfferIncludeFiles_Click(sender As Object, e As EventArgs) Handles btnOfferIncludeFiles.Click

        gFilesToIncludeOrExclude = tbFilesToIncludeOrExclude.Text

        Dim frmPickFileNames As New frmPickFileNames
        frmPickFileNames.ShowDialog()
        frmPickFileNames.Dispose()

        tbFilesToIncludeOrExclude.Text = gFilesToIncludeOrExclude

    End Sub

    Private Sub cbFiles_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbFiles.SelectedIndexChanged

        If cbFiles.Text = "All files" Then
            tbFilesToIncludeOrExclude.Text = String.Empty

        ElseIf cbFiles.Text = "Include specific files only" Then
            tbFilesToIncludeOrExclude.Text = lFilesToInclude

        Else
            tbFilesToIncludeOrExclude.Text = lFilesToExclude

        End If

        tbFilesToIncludeOrExclude.Visible = (cbFiles.Text <> "All files")
        btnOfferIncludeFiles.Visible = tbFilesToIncludeOrExclude.Visible

    End Sub

    Private Sub tbFilesToIncludeOrExclude_TextChanged(sender As Object, e As EventArgs) Handles tbFilesToIncludeOrExclude.TextChanged

        Select Case cbFiles.Text
            Case Is = "Include specific files only"
                lFilesToInclude = tbFilesToIncludeOrExclude.Text
            Case Is = "Exclude specific files"
                lFilesToExclude = tbFilesToIncludeOrExclude.Text
        End Select

    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click

        Me.Close()

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Process.Start(gDataDirectory)
    End Sub

End Class