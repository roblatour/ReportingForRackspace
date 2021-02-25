<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmMain
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
        Dim ChartArea1 As System.Windows.Forms.DataVisualization.Charting.ChartArea = New System.Windows.Forms.DataVisualization.Charting.ChartArea()
        Dim Legend1 As System.Windows.Forms.DataVisualization.Charting.Legend = New System.Windows.Forms.DataVisualization.Charting.Legend()
        Dim Series1 As System.Windows.Forms.DataVisualization.Charting.Series = New System.Windows.Forms.DataVisualization.Charting.Series()
        Dim ChartArea2 As System.Windows.Forms.DataVisualization.Charting.ChartArea = New System.Windows.Forms.DataVisualization.Charting.ChartArea()
        Dim Legend2 As System.Windows.Forms.DataVisualization.Charting.Legend = New System.Windows.Forms.DataVisualization.Charting.Legend()
        Dim Series2 As System.Windows.Forms.DataVisualization.Charting.Series = New System.Windows.Forms.DataVisualization.Charting.Series()
        Dim Series3 As System.Windows.Forms.DataVisualization.Charting.Series = New System.Windows.Forms.DataVisualization.Charting.Series()
        Dim Series4 As System.Windows.Forms.DataVisualization.Charting.Series = New System.Windows.Forms.DataVisualization.Charting.Series()
        Dim Title1 As System.Windows.Forms.DataVisualization.Charting.Title = New System.Windows.Forms.DataVisualization.Charting.Title()
        Dim ChartArea3 As System.Windows.Forms.DataVisualization.Charting.ChartArea = New System.Windows.Forms.DataVisualization.Charting.ChartArea()
        Dim Legend3 As System.Windows.Forms.DataVisualization.Charting.Legend = New System.Windows.Forms.DataVisualization.Charting.Legend()
        Dim Series5 As System.Windows.Forms.DataVisualization.Charting.Series = New System.Windows.Forms.DataVisualization.Charting.Series()
        Dim Title2 As System.Windows.Forms.DataVisualization.Charting.Title = New System.Windows.Forms.DataVisualization.Charting.Title()
        Dim ChartArea4 As System.Windows.Forms.DataVisualization.Charting.ChartArea = New System.Windows.Forms.DataVisualization.Charting.ChartArea()
        Dim Legend4 As System.Windows.Forms.DataVisualization.Charting.Legend = New System.Windows.Forms.DataVisualization.Charting.Legend()
        Dim Title3 As System.Windows.Forms.DataVisualization.Charting.Title = New System.Windows.Forms.DataVisualization.Charting.Title()
        Dim ChartArea5 As System.Windows.Forms.DataVisualization.Charting.ChartArea = New System.Windows.Forms.DataVisualization.Charting.ChartArea()
        Dim Legend5 As System.Windows.Forms.DataVisualization.Charting.Legend = New System.Windows.Forms.DataVisualization.Charting.Legend()
        Dim Title4 As System.Windows.Forms.DataVisualization.Charting.Title = New System.Windows.Forms.DataVisualization.Charting.Title()
        Dim ChartArea6 As System.Windows.Forms.DataVisualization.Charting.ChartArea = New System.Windows.Forms.DataVisualization.Charting.ChartArea()
        Dim Legend6 As System.Windows.Forms.DataVisualization.Charting.Legend = New System.Windows.Forms.DataVisualization.Charting.Legend()
        Dim Series6 As System.Windows.Forms.DataVisualization.Charting.Series = New System.Windows.Forms.DataVisualization.Charting.Series()
        Dim Title5 As System.Windows.Forms.DataVisualization.Charting.Title = New System.Windows.Forms.DataVisualization.Charting.Title()
        Me.StatusStrip = New System.Windows.Forms.StatusStrip()
        Me.ToolStripStatusLabel = New System.Windows.Forms.ToolStripStatusLabel()
        Me.ChartZoom = New System.Windows.Forms.DataVisualization.Charting.Chart()
        Me.ChartOfDownloadsByFileName = New System.Windows.Forms.DataVisualization.Charting.Chart()
        Me.ChartOfCompletedDownloadsByCountry = New System.Windows.Forms.DataVisualization.Charting.Chart()
        Me.ChartOfDownloadsByDate = New System.Windows.Forms.DataVisualization.Charting.Chart()
        Me.ChartOfDataTransferredByDate = New System.Windows.Forms.DataVisualization.Charting.Chart()
        Me.ChartOfDataTransferred = New System.Windows.Forms.DataVisualization.Charting.Chart()
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.rtbReferrers = New System.Windows.Forms.RichTextBox()
        Me.rtbIP = New System.Windows.Forms.RichTextBox()
        Me.rtbZoom = New System.Windows.Forms.RichTextBox()
        Me.FileToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.SettingsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItem2 = New System.Windows.Forms.ToolStripSeparator()
        Me.RefreshToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItem1 = New System.Windows.Forms.ToolStripSeparator()
        Me.ExitToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.HelpToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.OnlineHelpToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.WebsiteToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItem4 = New System.Windows.Forms.ToolStripSeparator()
        Me.CheckForUpdatesToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.AboutToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.MainMenu = New System.Windows.Forms.MenuStrip()
        Me.StatusStrip.SuspendLayout()
        CType(Me.ChartZoom, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ChartOfDownloadsByFileName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ChartOfCompletedDownloadsByCountry, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ChartOfDownloadsByDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ChartOfDataTransferredByDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ChartOfDataTransferred, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TableLayoutPanel1.SuspendLayout()
        Me.MainMenu.SuspendLayout()
        Me.SuspendLayout()
        '
        'StatusStrip
        '
        Me.StatusStrip.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.StatusStrip.AutoSize = False
        Me.StatusStrip.Dock = System.Windows.Forms.DockStyle.None
        Me.StatusStrip.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripStatusLabel})
        Me.StatusStrip.Location = New System.Drawing.Point(0, 559)
        Me.StatusStrip.Name = "StatusStrip"
        Me.StatusStrip.Size = New System.Drawing.Size(849, 22)
        Me.StatusStrip.SizingGrip = False
        Me.StatusStrip.TabIndex = 7
        Me.StatusStrip.Text = "StatusStrip1"
        '
        'ToolStripStatusLabel
        '
        Me.ToolStripStatusLabel.Margin = New System.Windows.Forms.Padding(17, 3, 0, 2)
        Me.ToolStripStatusLabel.Name = "ToolStripStatusLabel"
        Me.ToolStripStatusLabel.Size = New System.Drawing.Size(39, 17)
        Me.ToolStripStatusLabel.Text = "Status"
        '
        'ChartZoom
        '
        ChartArea1.Name = "ChartArea1"
        Me.ChartZoom.ChartAreas.Add(ChartArea1)
        Legend1.Name = "Legend1"
        Me.ChartZoom.Legends.Add(Legend1)
        Me.ChartZoom.Location = New System.Drawing.Point(269, 597)
        Me.ChartZoom.Name = "ChartZoom"
        Series1.ChartArea = "ChartArea1"
        Series1.Legend = "Legend1"
        Series1.Name = "Series1"
        Me.ChartZoom.Series.Add(Series1)
        Me.ChartZoom.Size = New System.Drawing.Size(270, 364)
        Me.ChartZoom.TabIndex = 12
        Me.ChartZoom.Text = "Chart1"
        Me.ChartZoom.Visible = False
        '
        'ChartOfDownloadsByType
        '
        Me.ChartOfDownloadsByFileName.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        ChartArea2.Name = "ChartArea1"
        Me.ChartOfDownloadsByFileName.ChartAreas.Add(ChartArea2)
        Legend2.Docking = System.Windows.Forms.DataVisualization.Charting.Docking.Bottom
        Legend2.Name = "Legend1"
        Me.ChartOfDownloadsByFileName.Legends.Add(Legend2)
        Me.ChartOfDownloadsByFileName.Location = New System.Drawing.Point(420, 267)
        Me.ChartOfDownloadsByFileName.Name = "ChartOfDownloadsByType"
        Me.ChartOfDownloadsByFileName.Palette = System.Windows.Forms.DataVisualization.Charting.ChartColorPalette.None
        Series2.ChartArea = "ChartArea1"
        Series2.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Bar
        Series2.Legend = "Legend1"
        Series2.Name = "Completed Downloads"
        Series3.ChartArea = "ChartArea1"
        Series3.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Bar
        Series3.Legend = "Legend1"
        Series3.Name = "Partial Downloads"
        Series4.ChartArea = "ChartArea1"
        Series4.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Bar
        Series4.Legend = "Legend1"
        Series4.Name = "Header checks"
        Me.ChartOfDownloadsByFileName.Series.Add(Series2)
        Me.ChartOfDownloadsByFileName.Series.Add(Series3)
        Me.ChartOfDownloadsByFileName.Series.Add(Series4)
        Me.ChartOfDownloadsByFileName.Size = New System.Drawing.Size(411, 126)
        Me.ChartOfDownloadsByFileName.TabIndex = 3
        Me.ChartOfDownloadsByFileName.Text = "Chart1"
        Title1.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Title1.Name = "Title1"
        Title1.Text = "Logged activities"
        Me.ChartOfDownloadsByFileName.Titles.Add(Title1)
        '
        'ChartOfCompletedDownloadsByCountry
        '
        Me.ChartOfCompletedDownloadsByCountry.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        ChartArea3.Name = "ChartArea1"
        Me.ChartOfCompletedDownloadsByCountry.ChartAreas.Add(ChartArea3)
        Legend3.Docking = System.Windows.Forms.DataVisualization.Charting.Docking.Bottom
        Legend3.Name = "Legend1"
        Legend3.TextWrapThreshold = 30
        Me.ChartOfCompletedDownloadsByCountry.Legends.Add(Legend3)
        Me.ChartOfCompletedDownloadsByCountry.Location = New System.Drawing.Point(420, 3)
        Me.ChartOfCompletedDownloadsByCountry.Name = "ChartOfCompletedDownloadsByCountry"
        Me.ChartOfCompletedDownloadsByCountry.Palette = System.Windows.Forms.DataVisualization.Charting.ChartColorPalette.None
        Series5.ChartArea = "ChartArea1"
        Series5.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Pie
        Series5.Legend = "Legend1"
        Series5.Name = "Data transferred (GBs)"
        Me.ChartOfCompletedDownloadsByCountry.Series.Add(Series5)
        Me.ChartOfCompletedDownloadsByCountry.Size = New System.Drawing.Size(411, 126)
        Me.ChartOfCompletedDownloadsByCountry.TabIndex = 4
        Me.ChartOfCompletedDownloadsByCountry.Text = "Chart2"
        Title2.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Title2.Name = "Title1"
        Title2.Text = "Completed Downloads by Country"
        Me.ChartOfCompletedDownloadsByCountry.Titles.Add(Title2)
        '
        'ChartOfDownloadsByDate
        '
        Me.ChartOfDownloadsByDate.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        ChartArea4.Name = "ChartArea1"
        Me.ChartOfDownloadsByDate.ChartAreas.Add(ChartArea4)
        Legend4.Docking = System.Windows.Forms.DataVisualization.Charting.Docking.Bottom
        Legend4.Name = "Legend1"
        Me.ChartOfDownloadsByDate.Legends.Add(Legend4)
        Me.ChartOfDownloadsByDate.Location = New System.Drawing.Point(3, 3)
        Me.ChartOfDownloadsByDate.Name = "ChartOfDownloadsByDate"
        Me.ChartOfDownloadsByDate.Palette = System.Windows.Forms.DataVisualization.Charting.ChartColorPalette.None
        Me.ChartOfDownloadsByDate.Size = New System.Drawing.Size(411, 126)
        Me.ChartOfDownloadsByDate.TabIndex = 1
        Me.ChartOfDownloadsByDate.Text = "Chart1"
        Title3.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Title3.Name = "Title1"
        Title3.Text = "Completed downloads by date"
        Me.ChartOfDownloadsByDate.Titles.Add(Title3)
        '
        'ChartOfDataTransferredByDate
        '
        Me.ChartOfDataTransferredByDate.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        ChartArea5.Name = "ChartArea1"
        Me.ChartOfDataTransferredByDate.ChartAreas.Add(ChartArea5)
        Legend5.Docking = System.Windows.Forms.DataVisualization.Charting.Docking.Bottom
        Legend5.Name = "Legend1"
        Me.ChartOfDataTransferredByDate.Legends.Add(Legend5)
        Me.ChartOfDataTransferredByDate.Location = New System.Drawing.Point(3, 135)
        Me.ChartOfDataTransferredByDate.Name = "ChartOfDataTransferredByDate"
        Me.ChartOfDataTransferredByDate.Palette = System.Windows.Forms.DataVisualization.Charting.ChartColorPalette.None
        Me.ChartOfDataTransferredByDate.Size = New System.Drawing.Size(411, 126)
        Me.ChartOfDataTransferredByDate.TabIndex = 2
        Me.ChartOfDataTransferredByDate.Text = "Chart1"
        Title4.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Title4.Name = "Title1"
        Title4.Text = "Data transfered by date"
        Me.ChartOfDataTransferredByDate.Titles.Add(Title4)
        '
        'ChartOfDataTransferred
        '
        Me.ChartOfDataTransferred.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        ChartArea6.Name = "ChartArea1"
        Me.ChartOfDataTransferred.ChartAreas.Add(ChartArea6)
        Legend6.Docking = System.Windows.Forms.DataVisualization.Charting.Docking.Bottom
        Legend6.Name = "Legend1"
        Legend6.TextWrapThreshold = 30
        Me.ChartOfDataTransferred.Legends.Add(Legend6)
        Me.ChartOfDataTransferred.Location = New System.Drawing.Point(420, 135)
        Me.ChartOfDataTransferred.Name = "ChartOfDataTransferred"
        Me.ChartOfDataTransferred.Palette = System.Windows.Forms.DataVisualization.Charting.ChartColorPalette.None
        Series6.ChartArea = "ChartArea1"
        Series6.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Pie
        Series6.Legend = "Legend1"
        Series6.Name = "Data transferred (GBs)"
        Me.ChartOfDataTransferred.Series.Add(Series6)
        Me.ChartOfDataTransferred.Size = New System.Drawing.Size(411, 126)
        Me.ChartOfDataTransferred.TabIndex = 3
        Me.ChartOfDataTransferred.Text = "Chart2"
        Title5.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Title5.Name = "Title1"
        Title5.Text = "Data transferred"
        Me.ChartOfDataTransferred.Titles.Add(Title5)
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TableLayoutPanel1.AutoSize = True
        Me.TableLayoutPanel1.ColumnCount = 2
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel1.Controls.Add(Me.ChartOfDataTransferred, 1, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.ChartOfDownloadsByDate, 0, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.ChartOfCompletedDownloadsByCountry, 1, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.ChartOfDataTransferredByDate, 0, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.ChartOfDownloadsByFileName, 1, 2)
        Me.TableLayoutPanel1.Controls.Add(Me.rtbReferrers, 0, 2)
        Me.TableLayoutPanel1.Controls.Add(Me.rtbIP, 1, 3)
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(12, 27)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 4
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25.0!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(834, 529)
        Me.TableLayoutPanel1.TabIndex = 11
        '
        'rtbReferrers
        '
        Me.rtbReferrers.AcceptsTab = True
        Me.rtbReferrers.BackColor = System.Drawing.Color.White
        Me.rtbReferrers.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.rtbReferrers.Dock = System.Windows.Forms.DockStyle.Fill
        Me.rtbReferrers.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rtbReferrers.Location = New System.Drawing.Point(3, 267)
        Me.rtbReferrers.Name = "rtbReferrers"
        Me.rtbReferrers.ReadOnly = True
        Me.TableLayoutPanel1.SetRowSpan(Me.rtbReferrers, 2)
        Me.rtbReferrers.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None
        Me.rtbReferrers.Size = New System.Drawing.Size(411, 259)
        Me.rtbReferrers.TabIndex = 5
        Me.rtbReferrers.Text = ""
        Me.rtbReferrers.WordWrap = False
        '
        'rtbIP
        '
        Me.rtbIP.AcceptsTab = True
        Me.rtbIP.BackColor = System.Drawing.Color.White
        Me.rtbIP.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.rtbIP.Dock = System.Windows.Forms.DockStyle.Fill
        Me.rtbIP.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rtbIP.Location = New System.Drawing.Point(420, 399)
        Me.rtbIP.Name = "rtbIP"
        Me.rtbIP.ReadOnly = True
        Me.rtbIP.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None
        Me.rtbIP.Size = New System.Drawing.Size(411, 127)
        Me.rtbIP.TabIndex = 6
        Me.rtbIP.Text = ""
        Me.rtbIP.WordWrap = False
        '
        'rtbZoom
        '
        Me.rtbZoom.Location = New System.Drawing.Point(610, 714)
        Me.rtbZoom.Name = "rtbZoom"
        Me.rtbZoom.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None
        Me.rtbZoom.Size = New System.Drawing.Size(183, 93)
        Me.rtbZoom.TabIndex = 13
        Me.rtbZoom.Text = ""
        Me.rtbZoom.Visible = False
        Me.rtbZoom.WordWrap = False
        '
        'FileToolStripMenuItem
        '
        Me.FileToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.SettingsToolStripMenuItem, Me.ToolStripMenuItem2, Me.RefreshToolStripMenuItem, Me.ToolStripMenuItem1, Me.ExitToolStripMenuItem})
        Me.FileToolStripMenuItem.Name = "FileToolStripMenuItem"
        Me.FileToolStripMenuItem.Size = New System.Drawing.Size(37, 20)
        Me.FileToolStripMenuItem.Text = "&File"
        '
        'SettingsToolStripMenuItem
        '
        Me.SettingsToolStripMenuItem.Name = "SettingsToolStripMenuItem"
        Me.SettingsToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F2
        Me.SettingsToolStripMenuItem.Size = New System.Drawing.Size(135, 22)
        Me.SettingsToolStripMenuItem.Text = "Settings"
        '
        'ToolStripMenuItem2
        '
        Me.ToolStripMenuItem2.Name = "ToolStripMenuItem2"
        Me.ToolStripMenuItem2.Size = New System.Drawing.Size(132, 6)
        '
        'RefreshToolStripMenuItem
        '
        Me.RefreshToolStripMenuItem.Name = "RefreshToolStripMenuItem"
        Me.RefreshToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F5
        Me.RefreshToolStripMenuItem.Size = New System.Drawing.Size(135, 22)
        Me.RefreshToolStripMenuItem.Text = "&Refresh"
        '
        'ToolStripMenuItem1
        '
        Me.ToolStripMenuItem1.Name = "ToolStripMenuItem1"
        Me.ToolStripMenuItem1.Size = New System.Drawing.Size(132, 6)
        '
        'ExitToolStripMenuItem
        '
        Me.ExitToolStripMenuItem.Name = "ExitToolStripMenuItem"
        Me.ExitToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F12
        Me.ExitToolStripMenuItem.Size = New System.Drawing.Size(135, 22)
        Me.ExitToolStripMenuItem.Text = "&Exit"
        '
        'HelpToolStripMenuItem
        '
        Me.HelpToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.WebsiteToolStripMenuItem, Me.OnlineHelpToolStripMenuItem, Me.ToolStripMenuItem4, Me.CheckForUpdatesToolStripMenuItem, Me.ToolStripSeparator1, Me.AboutToolStripMenuItem})
        Me.HelpToolStripMenuItem.Name = "HelpToolStripMenuItem"
        Me.HelpToolStripMenuItem.Size = New System.Drawing.Size(44, 20)
        Me.HelpToolStripMenuItem.Text = "&Help"
        '
        'OnlineHelpToolStripMenuItem
        '
        Me.OnlineHelpToolStripMenuItem.Name = "OnlineHelpToolStripMenuItem"
        Me.OnlineHelpToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F1
        Me.OnlineHelpToolStripMenuItem.Size = New System.Drawing.Size(175, 22)
        Me.OnlineHelpToolStripMenuItem.Tag = "http://www.rlatour.com/r4r/help.html"
        Me.OnlineHelpToolStripMenuItem.Text = "&Online help"
        '
        'WebsiteToolStripMenuItem
        '
        Me.WebsiteToolStripMenuItem.Name = "WebsiteToolStripMenuItem"
        Me.WebsiteToolStripMenuItem.Size = New System.Drawing.Size(175, 22)
        Me.WebsiteToolStripMenuItem.Tag = "http://www.rlatour.com/r4r/index.html"
        Me.WebsiteToolStripMenuItem.Text = "&Website main page"
        '
        'ToolStripMenuItem4
        '
        Me.ToolStripMenuItem4.Name = "ToolStripMenuItem4"
        Me.ToolStripMenuItem4.Size = New System.Drawing.Size(172, 6)
        '
        'CheckForUpdatesToolStripMenuItem
        '
        Me.CheckForUpdatesToolStripMenuItem.Name = "CheckForUpdatesToolStripMenuItem"
        Me.CheckForUpdatesToolStripMenuItem.Size = New System.Drawing.Size(175, 22)
        Me.CheckForUpdatesToolStripMenuItem.Text = "&Check for updates"
        '
        'ToolStripSeparator1
        '
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        Me.ToolStripSeparator1.Size = New System.Drawing.Size(172, 6)
        '
        'AboutToolStripMenuItem
        '
        Me.AboutToolStripMenuItem.Name = "AboutToolStripMenuItem"
        Me.AboutToolStripMenuItem.Size = New System.Drawing.Size(175, 22)
        Me.AboutToolStripMenuItem.Text = "&About"
        '
        'MainMenu
        '
        Me.MainMenu.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.FileToolStripMenuItem, Me.HelpToolStripMenuItem})
        Me.MainMenu.Location = New System.Drawing.Point(0, 0)
        Me.MainMenu.Name = "MainMenu"
        Me.MainMenu.Size = New System.Drawing.Size(858, 24)
        Me.MainMenu.TabIndex = 0
        Me.MainMenu.Text = "MenuStrip2"
        '
        'frmMain
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(858, 581)
        Me.Controls.Add(Me.rtbZoom)
        Me.Controls.Add(Me.ChartZoom)
        Me.Controls.Add(Me.TableLayoutPanel1)
        Me.Controls.Add(Me.StatusStrip)
        Me.Controls.Add(Me.MainMenu)
        Me.MinimumSize = New System.Drawing.Size(800, 600)
        Me.Name = "frmMain"
        Me.Opacity = 0R
        Me.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show
        Me.Text = "Reporting for Rackspace"
        Me.StatusStrip.ResumeLayout(False)
        Me.StatusStrip.PerformLayout()
        CType(Me.ChartZoom, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ChartOfDownloadsByFileName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ChartOfCompletedDownloadsByCountry, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ChartOfDownloadsByDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ChartOfDataTransferredByDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ChartOfDataTransferred, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.MainMenu.ResumeLayout(False)
        Me.MainMenu.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents StatusStrip As StatusStrip
    Friend WithEvents ToolStripStatusLabel As ToolStripStatusLabel
    Friend WithEvents ChartZoom As DataVisualization.Charting.Chart
    Friend WithEvents ChartOfDownloadsByFileName As DataVisualization.Charting.Chart
    Friend WithEvents ChartOfCompletedDownloadsByCountry As DataVisualization.Charting.Chart
    Friend WithEvents ChartOfDownloadsByDate As DataVisualization.Charting.Chart
    Friend WithEvents ChartOfDataTransferredByDate As DataVisualization.Charting.Chart
    Friend WithEvents ChartOfDataTransferred As DataVisualization.Charting.Chart
    Friend WithEvents TableLayoutPanel1 As TableLayoutPanel
    Friend WithEvents rtbReferrers As RichTextBox
    Friend WithEvents rtbZoom As RichTextBox
    Friend WithEvents rtbIP As RichTextBox
    Friend WithEvents FileToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents SettingsToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem2 As ToolStripSeparator
    Friend WithEvents RefreshToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem1 As ToolStripSeparator
    Friend WithEvents ExitToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents HelpToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents OnlineHelpToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem4 As ToolStripSeparator
    Friend WithEvents AboutToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents MainMenu As MenuStrip
    Friend WithEvents WebsiteToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents CheckForUpdatesToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ToolStripSeparator1 As ToolStripSeparator
End Class
