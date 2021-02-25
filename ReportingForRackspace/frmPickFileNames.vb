Public Class frmPickFileNames
    Private Sub frmPickFileNames_Load(sender As Object, e As EventArgs) Handles Me.Load

        Me.Icon = My.Resources.logo
        Me.Text = My.Application.Info.Title & " - which files to report"

        'add entries to list of checkbox items
        For Each entry In gUniqueFilenames

            If entry.Trim.Length > 0 Then
                Me.CheckedListBox1.Items.Add(entry)
            End If

        Next

        'check them off as needed
        For x = 0 To CheckedListBox1.Items.Count - 1

            If gFilesToIncludeOrExclude.Contains(CheckedListBox1.Items(x).ToString) Then
                Me.CheckedListBox1.SetItemChecked(x, True)
            End If

        Next

    End Sub

    Private Sub btnSelectAll_Click(sender As Object, e As EventArgs) Handles btnSelectAll.Click, btnDeSelectAll.Click

        Dim value As Boolean = (sender.name = "btnSelectAll")

        For x = 0 To CheckedListBox1.Items.Count - 1

            Me.CheckedListBox1.SetItemChecked(x, value)

        Next

    End Sub

    Private Sub btnOK_Click(sender As Object, e As EventArgs) Handles btnOK.Click

        gFilesToIncludeOrExclude = String.Empty

        For x = 0 To CheckedListBox1.Items.Count - 1

            If Me.CheckedListBox1.GetItemChecked(x) Then
                gFilesToIncludeOrExclude &= Me.CheckedListBox1.Items(x).ToString & " "
            End If

        Next

        gFilesToIncludeOrExclude = gFilesToIncludeOrExclude.Trim

        Me.Close()

    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click

        Me.Close()

    End Sub

    Private Sub CheckedListBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles CheckedListBox1.SelectedIndexChanged

        For x = 0 To CheckedListBox1.Items.Count - 1

            If sender.selecteditem.ToString = Me.CheckedListBox1.Items(x).ToString Then
                Me.CheckedListBox1.SetItemChecked(x, Not Me.CheckedListBox1.GetItemChecked(x))
            End If

        Next

    End Sub
End Class