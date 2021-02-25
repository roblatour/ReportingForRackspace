Public Class frmAbout

    Private Sub frmAbout_Load(sender As Object, e As EventArgs) Handles Me.Load

        Me.Icon = My.Resources.logo
        Me.Text = My.Application.Info.Title & " - About"

        lblProductName.Text = My.Application.Info.Title
        lblVersion.Text = "version " & My.Application.Info.Version.ToString
        lblCopyright.Text = My.Application.Info.Copyright

    End Sub

    Private Sub btnOK_Click(sender As Object, e As EventArgs) Handles btnOK.Click

        Me.Close()

    End Sub

    Private Sub LinkLabelLicense_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabelLicense.LinkClicked, LinkLableCountryDatabase.LinkClicked, LinkLabelDonate.LinkClicked

        sender.LinkVisited = True

        System.Diagnostics.Process.Start(sender.Tag.ToString)

    End Sub

End Class