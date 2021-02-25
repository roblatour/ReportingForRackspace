<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmSettings
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
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
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.btnOK = New System.Windows.Forms.Button()
        Me.btnCancel = New System.Windows.Forms.Button()
        Me.cbDeleteDownloadedLogsFromCloud = New System.Windows.Forms.CheckBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.groupbox = New System.Windows.Forms.GroupBox()
        Me.tbAPIKey = New System.Windows.Forms.TextBox()
        Me.tbUserID = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.GroupBox4 = New System.Windows.Forms.GroupBox()
        Me.cbShowHeaderChecks = New System.Windows.Forms.CheckBox()
        Me.cbEquate = New System.Windows.Forms.CheckBox()
        Me.cbIgnore400Series = New System.Windows.Forms.CheckBox()
        Me.gbFiles = New System.Windows.Forms.GroupBox()
        Me.cbFiles = New System.Windows.Forms.ComboBox()
        Me.tbFilesToIncludeOrExclude = New System.Windows.Forms.TextBox()
        Me.btnOfferIncludeFiles = New System.Windows.Forms.Button()
        Me.GroupBox3 = New System.Windows.Forms.GroupBox()
        Me.cbDateRange = New System.Windows.Forms.ComboBox()
        Me.lblTo = New System.Windows.Forms.Label()
        Me.dtpEndDate = New System.Windows.Forms.DateTimePicker()
        Me.dtpStartDate = New System.Windows.Forms.DateTimePicker()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.tbDataDirectory = New System.Windows.Forms.TextBox()
        Me.groupbox.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox4.SuspendLayout()
        Me.gbFiles.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.SuspendLayout()
        '
        'btnOK
        '
        Me.btnOK.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnOK.Location = New System.Drawing.Point(653, 459)
        Me.btnOK.Name = "btnOK"
        Me.btnOK.Size = New System.Drawing.Size(94, 30)
        Me.btnOK.TabIndex = 0
        Me.btnOK.Text = "&OK"
        Me.btnOK.UseVisualStyleBackColor = True
        '
        'btnCancel
        '
        Me.btnCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnCancel.Location = New System.Drawing.Point(21, 459)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(94, 30)
        Me.btnCancel.TabIndex = 4
        Me.btnCancel.Text = "&Cancel"
        Me.btnCancel.UseVisualStyleBackColor = True
        '
        'cbDeleteDownloadedLogsFromCloud
        '
        Me.cbDeleteDownloadedLogsFromCloud.AutoSize = True
        Me.cbDeleteDownloadedLogsFromCloud.Location = New System.Drawing.Point(67, 88)
        Me.cbDeleteDownloadedLogsFromCloud.Name = "cbDeleteDownloadedLogsFromCloud"
        Me.cbDeleteDownloadedLogsFromCloud.Size = New System.Drawing.Size(221, 17)
        Me.cbDeleteDownloadedLogsFromCloud.TabIndex = 4
        Me.cbDeleteDownloadedLogsFromCloud.Text = "&Delete downloaded logs from Rackspace"
        Me.cbDeleteDownloadedLogsFromCloud.UseVisualStyleBackColor = True
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(9, 25)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(43, 13)
        Me.Label2.TabIndex = 0
        Me.Label2.Text = "User ID"
        '
        'groupbox
        '
        Me.groupbox.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.groupbox.Controls.Add(Me.cbDeleteDownloadedLogsFromCloud)
        Me.groupbox.Controls.Add(Me.tbAPIKey)
        Me.groupbox.Controls.Add(Me.tbUserID)
        Me.groupbox.Controls.Add(Me.Label3)
        Me.groupbox.Controls.Add(Me.Label2)
        Me.groupbox.Location = New System.Drawing.Point(21, 11)
        Me.groupbox.Name = "groupbox"
        Me.groupbox.Size = New System.Drawing.Size(726, 116)
        Me.groupbox.TabIndex = 1
        Me.groupbox.TabStop = False
        Me.groupbox.Text = "Rackspace"
        '
        'tbAPIKey
        '
        Me.tbAPIKey.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.tbAPIKey.Font = New System.Drawing.Font("Wingdings", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(2, Byte))
        Me.tbAPIKey.Location = New System.Drawing.Point(67, 56)
        Me.tbAPIKey.Name = "tbAPIKey"
        Me.tbAPIKey.PasswordChar = Global.Microsoft.VisualBasic.ChrW(61548)
        Me.tbAPIKey.Size = New System.Drawing.Size(638, 20)
        Me.tbAPIKey.TabIndex = 2
        '
        'tbUserID
        '
        Me.tbUserID.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.tbUserID.Location = New System.Drawing.Point(67, 22)
        Me.tbUserID.Name = "tbUserID"
        Me.tbUserID.Size = New System.Drawing.Size(638, 20)
        Me.tbUserID.TabIndex = 1
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(9, 58)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(45, 13)
        Me.Label3.TabIndex = 1
        Me.Label3.Text = "API Key"
        '
        'GroupBox1
        '
        Me.GroupBox1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox1.Controls.Add(Me.GroupBox4)
        Me.GroupBox1.Controls.Add(Me.gbFiles)
        Me.GroupBox1.Controls.Add(Me.GroupBox3)
        Me.GroupBox1.Location = New System.Drawing.Point(21, 130)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(726, 262)
        Me.GroupBox1.TabIndex = 2
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Filters"
        '
        'GroupBox4
        '
        Me.GroupBox4.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox4.Controls.Add(Me.cbShowHeaderChecks)
        Me.GroupBox4.Controls.Add(Me.cbEquate)
        Me.GroupBox4.Controls.Add(Me.cbIgnore400Series)
        Me.GroupBox4.Location = New System.Drawing.Point(12, 148)
        Me.GroupBox4.Name = "GroupBox4"
        Me.GroupBox4.Size = New System.Drawing.Size(693, 95)
        Me.GroupBox4.TabIndex = 14
        Me.GroupBox4.TabStop = False
        Me.GroupBox4.Text = "Misc."
        '
        'cbShowHeaderChecks
        '
        Me.cbShowHeaderChecks.AutoSize = True
        Me.cbShowHeaderChecks.Location = New System.Drawing.Point(56, 19)
        Me.cbShowHeaderChecks.Name = "cbShowHeaderChecks"
        Me.cbShowHeaderChecks.Size = New System.Drawing.Size(127, 17)
        Me.cbShowHeaderChecks.TabIndex = 0
        Me.cbShowHeaderChecks.Text = "Show &header checks"
        Me.cbShowHeaderChecks.UseVisualStyleBackColor = True
        '
        'cbEquate
        '
        Me.cbEquate.AutoSize = True
        Me.cbEquate.Location = New System.Drawing.Point(56, 72)
        Me.cbEquate.Name = "cbEquate"
        Me.cbEquate.Size = New System.Drawing.Size(244, 17)
        Me.cbEquate.TabIndex = 2
        Me.cbEquate.Text = "&Show fully and adjusted completed downloads"
        Me.cbEquate.UseVisualStyleBackColor = True
        '
        'cbIgnore400Series
        '
        Me.cbIgnore400Series.AutoSize = True
        Me.cbIgnore400Series.Location = New System.Drawing.Point(56, 45)
        Me.cbIgnore400Series.Name = "cbIgnore400Series"
        Me.cbIgnore400Series.Size = New System.Drawing.Size(169, 17)
        Me.cbIgnore400Series.TabIndex = 1
        Me.cbIgnore400Series.Text = "&Ignore 400 series return codes"
        Me.cbIgnore400Series.UseVisualStyleBackColor = True
        '
        'gbFiles
        '
        Me.gbFiles.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.gbFiles.Controls.Add(Me.cbFiles)
        Me.gbFiles.Controls.Add(Me.tbFilesToIncludeOrExclude)
        Me.gbFiles.Controls.Add(Me.btnOfferIncludeFiles)
        Me.gbFiles.Location = New System.Drawing.Point(12, 87)
        Me.gbFiles.Name = "gbFiles"
        Me.gbFiles.Size = New System.Drawing.Size(693, 58)
        Me.gbFiles.TabIndex = 13
        Me.gbFiles.TabStop = False
        Me.gbFiles.Text = "Files"
        '
        'cbFiles
        '
        Me.cbFiles.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbFiles.FormattingEnabled = True
        Me.cbFiles.Items.AddRange(New Object() {"All files", "Include specific files only", "Exclude specific files"})
        Me.cbFiles.Location = New System.Drawing.Point(56, 19)
        Me.cbFiles.Name = "cbFiles"
        Me.cbFiles.Size = New System.Drawing.Size(142, 21)
        Me.cbFiles.TabIndex = 0
        '
        'tbFilesToIncludeOrExclude
        '
        Me.tbFilesToIncludeOrExclude.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.tbFilesToIncludeOrExclude.Location = New System.Drawing.Point(228, 18)
        Me.tbFilesToIncludeOrExclude.Multiline = True
        Me.tbFilesToIncludeOrExclude.Name = "tbFilesToIncludeOrExclude"
        Me.tbFilesToIncludeOrExclude.Size = New System.Drawing.Size(391, 24)
        Me.tbFilesToIncludeOrExclude.TabIndex = 1
        '
        'btnOfferIncludeFiles
        '
        Me.btnOfferIncludeFiles.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnOfferIncludeFiles.Location = New System.Drawing.Point(638, 19)
        Me.btnOfferIncludeFiles.Name = "btnOfferIncludeFiles"
        Me.btnOfferIncludeFiles.Size = New System.Drawing.Size(37, 22)
        Me.btnOfferIncludeFiles.TabIndex = 3
        Me.btnOfferIncludeFiles.Text = "..."
        Me.btnOfferIncludeFiles.UseVisualStyleBackColor = True
        '
        'GroupBox3
        '
        Me.GroupBox3.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox3.Controls.Add(Me.cbDateRange)
        Me.GroupBox3.Controls.Add(Me.lblTo)
        Me.GroupBox3.Controls.Add(Me.dtpEndDate)
        Me.GroupBox3.Controls.Add(Me.dtpStartDate)
        Me.GroupBox3.Location = New System.Drawing.Point(14, 19)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(690, 62)
        Me.GroupBox3.TabIndex = 12
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = "Date"
        '
        'cbDateRange
        '
        Me.cbDateRange.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbDateRange.FormattingEnabled = True
        Me.cbDateRange.Items.AddRange(New Object() {"All dates", "Specific range", "Today", "Yesterday", "Today + last 7 days", "This week", "This month", "This year", "Last week", "Last month", "Last year", ""})
        Me.cbDateRange.Location = New System.Drawing.Point(54, 23)
        Me.cbDateRange.Name = "cbDateRange"
        Me.cbDateRange.Size = New System.Drawing.Size(142, 21)
        Me.cbDateRange.TabIndex = 0
        '
        'lblTo
        '
        Me.lblTo.AutoSize = True
        Me.lblTo.Location = New System.Drawing.Point(432, 28)
        Me.lblTo.Name = "lblTo"
        Me.lblTo.Size = New System.Drawing.Size(16, 13)
        Me.lblTo.TabIndex = 9
        Me.lblTo.Text = "to"
        '
        'dtpEndDate
        '
        Me.dtpEndDate.Location = New System.Drawing.Point(454, 24)
        Me.dtpEndDate.Name = "dtpEndDate"
        Me.dtpEndDate.Size = New System.Drawing.Size(200, 20)
        Me.dtpEndDate.TabIndex = 2
        '
        'dtpStartDate
        '
        Me.dtpStartDate.Location = New System.Drawing.Point(226, 24)
        Me.dtpStartDate.Name = "dtpStartDate"
        Me.dtpStartDate.Size = New System.Drawing.Size(200, 20)
        Me.dtpStartDate.TabIndex = 1
        '
        'GroupBox2
        '
        Me.GroupBox2.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox2.Controls.Add(Me.Button1)
        Me.GroupBox2.Controls.Add(Me.tbDataDirectory)
        Me.GroupBox2.Location = New System.Drawing.Point(21, 398)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(726, 51)
        Me.GroupBox2.TabIndex = 3
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Data Directory"
        '
        'Button1
        '
        Me.Button1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Button1.Location = New System.Drawing.Point(650, 19)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(37, 22)
        Me.Button1.TabIndex = 1
        Me.Button1.Text = "..."
        Me.Button1.UseVisualStyleBackColor = True
        '
        'tbDataDirectory
        '
        Me.tbDataDirectory.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.tbDataDirectory.Location = New System.Drawing.Point(12, 21)
        Me.tbDataDirectory.Name = "tbDataDirectory"
        Me.tbDataDirectory.ReadOnly = True
        Me.tbDataDirectory.Size = New System.Drawing.Size(619, 20)
        Me.tbDataDirectory.TabIndex = 0
        '
        'frmSettings
        '
        Me.AcceptButton = Me.btnOK
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.CancelButton = Me.btnCancel
        Me.ClientSize = New System.Drawing.Size(759, 501)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.groupbox)
        Me.Controls.Add(Me.btnCancel)
        Me.Controls.Add(Me.btnOK)
        Me.MaximumSize = New System.Drawing.Size(2048, 2048)
        Me.MinimumSize = New System.Drawing.Size(775, 540)
        Me.Name = "frmSettings"
        Me.Text = "Settings"
        Me.groupbox.ResumeLayout(False)
        Me.groupbox.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox4.ResumeLayout(False)
        Me.GroupBox4.PerformLayout()
        Me.gbFiles.ResumeLayout(False)
        Me.gbFiles.PerformLayout()
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox3.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents btnOK As Button
    Friend WithEvents btnCancel As Button
    Friend WithEvents cbDeleteDownloadedLogsFromCloud As CheckBox
    Friend WithEvents Label2 As Label
    Friend WithEvents groupbox As GroupBox
    Friend WithEvents tbUserID As TextBox
    Friend WithEvents Label3 As Label
    Friend WithEvents tbAPIKey As TextBox
    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents tbFilesToIncludeOrExclude As TextBox
    Friend WithEvents GroupBox2 As GroupBox
    Friend WithEvents tbDataDirectory As TextBox
    Friend WithEvents cbIgnore400Series As CheckBox
    Friend WithEvents lblTo As Label
    Friend WithEvents dtpEndDate As DateTimePicker
    Friend WithEvents dtpStartDate As DateTimePicker
    Friend WithEvents btnOfferIncludeFiles As Button
    Friend WithEvents GroupBox4 As GroupBox
    Friend WithEvents gbFiles As GroupBox
    Friend WithEvents GroupBox3 As GroupBox
    Friend WithEvents Button1 As Button
    Friend WithEvents cbDateRange As ComboBox
    Friend WithEvents cbFiles As ComboBox
    Friend WithEvents cbEquate As CheckBox
    Friend WithEvents cbShowHeaderChecks As CheckBox
End Class
