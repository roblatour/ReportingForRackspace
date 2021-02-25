<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmAbout
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmAbout))
        Me.btnOK = New System.Windows.Forms.Button()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.LinkLabelLicense = New System.Windows.Forms.LinkLabel()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.lblCopyright = New System.Windows.Forms.Label()
        Me.lblVersion = New System.Windows.Forms.Label()
        Me.lblProductName = New System.Windows.Forms.Label()
        Me.GroupBox3 = New System.Windows.Forms.GroupBox()
        Me.LinkLableCountryDatabase = New System.Windows.Forms.LinkLabel()
        Me.GroupBox4 = New System.Windows.Forms.GroupBox()
        Me.LinkLabelDonate = New System.Windows.Forms.LinkLabel()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        Me.GroupBox4.SuspendLayout()
        Me.SuspendLayout()
        '
        'btnOK
        '
        Me.btnOK.Location = New System.Drawing.Point(416, 319)
        Me.btnOK.Name = "btnOK"
        Me.btnOK.Size = New System.Drawing.Size(94, 30)
        Me.btnOK.TabIndex = 0
        Me.btnOK.Text = "&Ok"
        Me.btnOK.UseVisualStyleBackColor = True
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.LinkLabelLicense)
        Me.GroupBox1.Location = New System.Drawing.Point(12, 161)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(498, 51)
        Me.GroupBox1.TabIndex = 1
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Reporting for Rackspace license"
        '
        'LinkLabelLicense
        '
        Me.LinkLabelLicense.AutoSize = True
        Me.LinkLabelLicense.Location = New System.Drawing.Point(10, 21)
        Me.LinkLabelLicense.Name = "LinkLabelLicense"
        Me.LinkLabelLicense.Size = New System.Drawing.Size(26, 13)
        Me.LinkLabelLicense.TabIndex = 0
        Me.LinkLabelLicense.TabStop = True
        Me.LinkLabelLicense.Tag = "https://www.rlatour.com/r4r/license.txt"
        Me.LinkLabelLicense.Text = "MIT"
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.Panel1)
        Me.GroupBox2.Controls.Add(Me.lblCopyright)
        Me.GroupBox2.Controls.Add(Me.lblVersion)
        Me.GroupBox2.Controls.Add(Me.lblProductName)
        Me.GroupBox2.Location = New System.Drawing.Point(12, 15)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(498, 137)
        Me.GroupBox2.TabIndex = 2
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Version"
        '
        'Panel1
        '
        Me.Panel1.BackgroundImage = CType(resources.GetObject("Panel1.BackgroundImage"), System.Drawing.Image)
        Me.Panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel1.Location = New System.Drawing.Point(16, 26)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(90, 92)
        Me.Panel1.TabIndex = 4
        '
        'lblCopyright
        '
        Me.lblCopyright.AutoSize = True
        Me.lblCopyright.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCopyright.Location = New System.Drawing.Point(104, 103)
        Me.lblCopyright.Name = "lblCopyright"
        Me.lblCopyright.Size = New System.Drawing.Size(65, 16)
        Me.lblCopyright.TabIndex = 3
        Me.lblCopyright.Text = "Copyright"
        '
        'lblVersion
        '
        Me.lblVersion.AutoSize = True
        Me.lblVersion.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblVersion.Location = New System.Drawing.Point(105, 79)
        Me.lblVersion.Name = "lblVersion"
        Me.lblVersion.Size = New System.Drawing.Size(52, 16)
        Me.lblVersion.TabIndex = 2
        Me.lblVersion.Text = "version"
        '
        'lblProductName
        '
        Me.lblProductName.AutoSize = True
        Me.lblProductName.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblProductName.Location = New System.Drawing.Point(104, 47)
        Me.lblProductName.Name = "lblProductName"
        Me.lblProductName.Size = New System.Drawing.Size(216, 24)
        Me.lblProductName.TabIndex = 0
        Me.lblProductName.Text = "Reporting for Rackspace"
        '
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.LinkLableCountryDatabase)
        Me.GroupBox3.Location = New System.Drawing.Point(12, 221)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(498, 54)
        Me.GroupBox3.TabIndex = 2
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = "Country database"
        '
        'LinkLableCountryDatabase
        '
        Me.LinkLableCountryDatabase.AutoSize = True
        Me.LinkLableCountryDatabase.Location = New System.Drawing.Point(9, 23)
        Me.LinkLableCountryDatabase.Name = "LinkLableCountryDatabase"
        Me.LinkLableCountryDatabase.Size = New System.Drawing.Size(409, 13)
        Me.LinkLableCountryDatabase.TabIndex = 0
        Me.LinkLableCountryDatabase.TabStop = True
        Me.LinkLableCountryDatabase.Tag = "http://software77.net/geo-ip/"
        Me.LinkLableCountryDatabase.Text = "IP address to country matching with thanks to WebNet77.net donationware database"
        '
        'GroupBox4
        '
        Me.GroupBox4.Controls.Add(Me.LinkLabelDonate)
        Me.GroupBox4.Location = New System.Drawing.Point(12, 282)
        Me.GroupBox4.Name = "GroupBox4"
        Me.GroupBox4.Size = New System.Drawing.Size(381, 67)
        Me.GroupBox4.TabIndex = 3
        Me.GroupBox4.TabStop = False
        Me.GroupBox4.Text = "Donate"
        '
        'LinkLabelDonate
        '
        Me.LinkLabelDonate.AutoSize = True
        Me.LinkLabelDonate.Location = New System.Drawing.Point(12, 29)
        Me.LinkLabelDonate.Name = "LinkLabelDonate"
        Me.LinkLabelDonate.Size = New System.Drawing.Size(316, 13)
        Me.LinkLabelDonate.TabIndex = 0
        Me.LinkLabelDonate.TabStop = True
        Me.LinkLabelDonate.Tag = "http://www.rlatour.com/r4r/donate.html"
        Me.LinkLabelDonate.Text = "To help keep Reporting for Rackspace alive, please donate now!"
        '
        'frmAbout
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(530, 366)
        Me.Controls.Add(Me.GroupBox4)
        Me.Controls.Add(Me.GroupBox3)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.btnOK)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Name = "frmAbout"
        Me.Text = "Reporting for Rackspace"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox3.PerformLayout()
        Me.GroupBox4.ResumeLayout(False)
        Me.GroupBox4.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents btnOK As Button
    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents GroupBox2 As GroupBox
    Friend WithEvents lblProductName As Label
    Friend WithEvents lblCopyright As Label
    Friend WithEvents lblVersion As Label
    Friend WithEvents GroupBox3 As GroupBox
    Friend WithEvents GroupBox4 As GroupBox
    Friend WithEvents Panel1 As Panel
    Friend WithEvents LinkLabelLicense As LinkLabel
    Friend WithEvents LinkLableCountryDatabase As LinkLabel
    Friend WithEvents LinkLabelDonate As LinkLabel
End Class
