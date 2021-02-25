Public NotInheritable Class frmSplash

    'TODO: This form can easily be set as the splash screen for the application by going to the "Application" tab of the Project Designer ("Properties" under the "Project" menu).

    Private Sub frmSplash_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load


        If Microsoft.VisualBasic.Command.ToString.ToUpper.Contains("SILENT") Then

            'suppress splash screen if command line contains the word 'silent'
            Me.Close()

        Else

            Me.Opacity = 1   ' Opacity set to 0 in form so it will not be visible until set below (aka not an silent run)

            ApplicationTitle.Text = My.Application.Info.Title

            Version.Text = "version " & My.Application.Info.Version.ToString

            Copyright.Text = My.Application.Info.Copyright

        End If

    End Sub

    Private Sub ClickToClose(sender As Object, e As EventArgs) Handles ApplicationTitle.Click, Version.Click, Copyright.Click, Me.Click, MainLayoutPanel.Click

        Me.Close()

    End Sub

End Class
