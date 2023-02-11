'Copyright 2022, Rob Latour

' For best performance
' Compile options - uncheck Prefer 32 bit
' runs as release

' ref: https://www.nuget.org/packages/Rackspace
' ref: https://betterdashboards.wordpress.com/2009/02/04/display-percentages-on-a-pie-chart/

Imports System.ComponentModel
Imports System.Globalization
Imports System.IO
Imports System.IO.Compression  ' also, add refernce to: System.IO.Compression.FileSystem
Imports System.Text
Imports System.Windows.Forms.DataVisualization.Charting
Imports net.openstack.Core.Domain
Imports net.openstack.Providers.Rackspace
Imports Npgsql

Public Class frmMain

#Region "Constants and Variables"

    ' Reporting for Rackspace assume a PostgresQL database loaded with the lite (free) version of MaxMind GEOIP data
    ' ref: https://www.postgresql.org/
    ' ref: https://dev.maxmind.com/geoip?lang=en
    ' ref: https://dev.maxmind.com/geoip/importing-databases?lang=en

    ' the pre compile version of available on GitHub assumes the following valuescompiled values
    ' future enhancement may be to add them to the settings screen

    Const MaxMindHost As String = "localhost"
    Const MaxMindDatabaseName As String = "MaxMind"
    Const MaxMindDatabaseUserID As String = "postgres"
    Const MaxMindDatabasePassword As String = "MaxMind"  ' use "MaxMind" for general release

    ' ***********************************************************************************************************************

    ' Work in progress global constants and variables

    Const gCurrentVersionURL As String = "https://www.rlatour.com/r4r/version/currentversion.txt"

    Const cThresholdSecondsForACombine As Integer = 300

    Const cCombineFileName As String = "Combined.txt"
    Const cMasterFileOfDownloadedFilesFileName As String = "Master.txt"

    Private gRackSpaceUsername As String = String.Empty
    Private gRackSpaceAPIKey = String.Empty

    Private gDownloadFolder As String = String.Empty
    Private gDeCompressedFolder As String = String.Empty
    Private gCombinedFolder As String = String.Empty
    Private gIPv4Folder As String = String.Empty
    Private gIPv6Folder As String = String.Empty

    Private gIncludeExcludeFlag As String = String.Empty
    Private gFilesToInclude As String = String.Empty
    Private gFilesToExclude As String = String.Empty

    Private gShowHeaderChecks As Boolean = True
    Private gIgnore400Requests As Boolean = True
    Private gEquatePartialDownloads As Boolean = False

    Private gDeleteDownloadedLogsFromCloud As Boolean = False

    Private gZoom As Boolean = False
    Private gZoomName As String = String.Empty

    Private gStartDate As Date
    Private gEndDate As Date

    Dim LogFilesPreviousilyDownloaded As New HashSet(Of String)
    Dim LogFilesDownloadedButNotDeletedFromCloudInThisSession As New HashSet(Of String)

    '************************************************************************************************************************************************************************************************************
    Enum GetOrCheck As Integer
        GetFile = 0
        CheckFile = -1
    End Enum
    Structure LogContent

        Dim IPAddress As String
        Dim LogDateTime As Date
        Dim Action As GetOrCheck
        Dim Filename As String
        Dim ReferingPage As String
        Dim DataTransferred As Long
        Dim ReturnCode As Integer

    End Structure

    Dim LogTableIndex As Integer = 0
    Dim LogTableSize As Integer = 1
    Dim LogTable(LogTableSize) As LogContent


    '************************************************************************************************************************************************************************************************************
    Structure TotalReportContent

        Dim FileName As String
        Dim CheckCount As Integer
        Dim CompletedDownloadCount As Integer
        Dim CompletedDownloadsBasedOnEquivalentPartialDownloadCount As Integer
        Dim PartialDownloadCount As Integer
        Dim DataTransferred As Long

        Sub New(ByVal _FileName As String, ByVal _CheckCount As Integer, ByVal _DownloadCount As Integer, ByVal _CompletedDownloadsBasedOnEquivalentPartialDownloadCount As Integer, ByVal _PartialDownloadCount As Integer, ByVal _DataTransferred As Long)
            FileName = _FileName
            CheckCount = _CheckCount
            CompletedDownloadCount = _DownloadCount
            CompletedDownloadsBasedOnEquivalentPartialDownloadCount = _CompletedDownloadsBasedOnEquivalentPartialDownloadCount
            PartialDownloadCount = _PartialDownloadCount
            DataTransferred = _DataTransferred
        End Sub

    End Structure

    Dim TotalReportTableIndex As Integer = 0
    Dim TotalReportTable(LogTableSize) As TotalReportContent

    '************************************************************************************************************************************************************************************************************
    Structure FileNameByDateTableContent

        Dim FileName As String
        Dim DownloadDate As Date
        Dim DownloadCount As Integer
        Dim DataTransferred As Long

        Sub New(ByVal _FileName As String, ByVal _DownloadDate As Date, ByVal _DownloadCount As Integer, ByVal _DataTransferred As Long)
            FileName = _FileName
            DownloadDate = _DownloadDate
            DownloadCount = _DownloadCount
            DataTransferred = _DataTransferred
        End Sub

    End Structure

    Dim FilenameByDateTableIndex As Integer = 0
    Const FilenameByDateTableIncrement As Integer = 100
    Dim FilenameByDateTableMaxSize As Integer = FilenameByDateTableIncrement
    Dim FileNameByDateTable(FilenameByDateTableMaxSize) As FileNameByDateTableContent

    '************************************************************************************************************************************************************************************************************
    Structure ProgramsAndThereMaximumFileSizesContent

        Dim FileName As String
        Dim FileSize As Long
        Dim FileSizeToBeConsideredAsACompletedDownload As Long

        Sub New(ByVal _FileName As String, ByVal _FileSize As Long, ByVal _FileSizeToBeConsideredAsACompletedDownload As Long)
            FileName = _FileName
            FileSize = _FileSize
            FileSizeToBeConsideredAsACompletedDownload = _FileSizeToBeConsideredAsACompletedDownload
        End Sub

    End Structure

    Private ListOFProgramsAndThereMaximumFileSize() As ProgramsAndThereMaximumFileSizesContent

    '************************************************************************************************************************************************************************************************************
    Structure Top10CountriesContent

        Dim Country As String
        Dim TotalDownloads As Integer

        Sub New(ByVal _Country As String, ByVal _CompletedDownloads As Integer)
            Country = _Country
            TotalDownloads = _CompletedDownloads
        End Sub

    End Structure

    Dim Top10Countries As New List(Of Top10CountriesContent)

    '************************************************************************************************************************************************************************************************************
    Structure RefererTableContent

        Dim ReferingPage As String
        Dim Count As Integer

    End Structure

    Private ReferrerTable(0) As RefererTableContent

    '************************************************************************************************************************************************************************************************************
    Structure IPTableContent

        Dim IP As String
        Dim IPi As Long
        Dim Count As Integer

    End Structure

    Private IPTable(0) As IPTableContent

    '************************************************************************************************************************************************************************************************************
    Structure CountryCodeTableContent

        Dim CountryCode As String
        Dim FullNameOfCountry As String

        Sub New(ByVal _CountryCode As String, ByVal _FullNameOfCountry As String)
            CountryCode = _CountryCode
            FullNameOfCountry = _FullNameOfCountry
        End Sub

    End Structure

    Dim CountryCodeTable As New HashSet(Of CountryCodeTableContent)

    '************************************************************************************************************************************************************************************************************

    Structure IPCountryTotalDownloadsContent
        Dim IP As String
        Dim CountryCode As String
        Dim TotalDownloads As Integer
    End Structure

    Structure CountryTotalDownloadsContent
        Dim CountryCode As String
        Dim TotalDownloads As Integer
    End Structure


    '************************************************************************************************************************************************************************************************************
    Structure CalculatedDownLoadTableContent

        Dim IP As String
        Dim DateAndTime As DateTime
        Dim Filename As String
        Dim DataTransferred As Integer

        Sub New(ByVal _IP As String, ByVal _DaeteAndTime As DateTime, ByVal _BytesDownloaded As Integer)
            IP = _IP
            DateAndTime = _DaeteAndTime
            DataTransferred = _BytesDownloaded
        End Sub

    End Structure

    Dim CalculatedDownLoadList As New List(Of CalculatedDownLoadTableContent)

    '************************************************************************************************************************************************************************************************************

#End Region

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles Me.Load


        Dim frmSplash = New frmSplash
        frmSplash.Show()
        frmSplash.BringToFront()

        modSecurity.EncryptDecryptClass.MakePassPhraseUniqueToThisPC()

        SetupConnectionToMaxMindDatabase()

        If Microsoft.VisualBasic.Command.ToString.Trim.ToUpper.Contains("SILENT") Then

            ' update datafiles and shutdown

            ReLoadGlobalVariablesFromSettings()

            If Now > My.Settings.LastCheck.AddMinutes(5) Then
                Initialize()
                MainDriver(True, True)
            End If

            If (Application.MessageLoop) Then
                Application.Exit()
            Else
                Environment.Exit(1)
            End If

        Else

            Me.Opacity = 1
            MainDriver(False, False)
            Me.Size = My.Settings.StartingSize

            Me.Location = My.Settings.StartingLocation
            If (Me.Location.X < -10000) OrElse (Me.Location.Y < -10000) Then
                Me.Location = New Point(0, 0)
                My.Settings.StartingLocation = New Point(0, 0)
            End If

            Me.Icon = My.Resources.logo
            Me.Text = My.Application.Info.Title

            If My.Settings.FirstTimeSetup Then
                My.Settings.FirstTimeSetup = False
                My.Settings.Save()
                System.Diagnostics.Process.Start("https://www.rlatour.com/r4r/help.html#setup")
            End If

        End If

        Try
            frmSplash.Close()
        Catch ex As Exception

        End Try

    End Sub

    Private Sub frmMain_Closing(sender As Object, e As CancelEventArgs) Handles Me.Closing

        My.Settings.StartingSize = Me.Size
        My.Settings.StartingLocation = Me.Location
        My.Settings.Save()

    End Sub
    Private Sub Exit_Click(sender As Object, e As EventArgs) Handles ExitToolStripMenuItem.Click

        Me.Close()

    End Sub

    Private Sub RefreshToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles RefreshToolStripMenuItem.Click

        If My.Settings.LastCheck = Nothing Then
        Else
            If My.Settings.LastCheck.AddHours(1) > Now Then

                Beep()

                If MsgBox("Your last refresh was less than an hour ago." & vbCrLf & vbCrLf & "Do you really want to refresh?", vbYesNo + vbDefaultButton2 + vbQuestion, "Reporting for Rackspace") = vbNo Then
                    Exit Sub
                End If

            End If
        End If

        MainDriver(True, False)

    End Sub

    Private Sub MainDriver(ByVal DownloadFiles As Boolean, ByVal AutoShutdown As Boolean)

        If DownloadFiles Then

            Me.ToolStripStatusLabel.Text = "Refreshing ..."
            Me.Refresh()

            InventoryExistingDownloadedAndDecompressedLogFiles()
            DownloadLogFiles()
            DecompressDownloadFolder()
            DeleteFiles(LogFilesDownloadedButNotDeletedFromCloudInThisSession)

        End If

        If AutoShutdown Then
        Else
            RunReports() ' these seems required, otherwise country pie graph doesn't generate with country names when reports are run after startup?? Not sure why
        End If

    End Sub

    Private Sub RunReports()

        FileToolStripMenuItem.Enabled = False

        Me.Cursor = Cursors.WaitCursor
        Application.DoEvents()

        ReLoadGlobalVariablesFromSettings()
        Initialize()
        CombineAllDecompressedFiles()
        ProcessCombinedFile()
        TallyTableEntries()
        CreateGraphsAndReports()
        RefreshStatusBar()

        FileToolStripMenuItem.Enabled = True

        Me.Cursor = Cursors.Default

    End Sub

    Private Sub ReportToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ReportToolStripMenuItem.Click

        Application.DoEvents()
        RunReports()

    End Sub

    Private Sub SettingsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SettingsToolStripMenuItem.Click

        Dim HoldRemoveDownloadedLogsFromCloud As Boolean = gDeleteDownloadedLogsFromCloud

        Dim frmSettings As New frmSettings
        frmSettings.ShowDialog()
        frmSettings.Dispose()

        If gSettingsClosedWithAnOK Then

            RunReports()

            If gDeleteDownloadedLogsFromCloud Then

                If HoldRemoveDownloadedLogsFromCloud Then
                    ' files were already being deleted so don't worry about it
                Else
                    DeleteFilesFromServerThatHaveAlreadyBeenDownloaded()
                End If

            End If

        End If

    End Sub


#Region "Initialize"

    Private Sub Initialize()

        Static Dim DoOnce As Boolean = True

        If DoOnce Then

            AddCopyFunctionToRichTextBoxReports()

            SetSettingsVersion()
            SetWorkingFolders()

            DoOnce = False

        End If

        ReLoadGlobalVariablesFromSettings()

        RefreshStatusBar()

        LogFilesPreviousilyDownloaded.Clear()
        LogFilesDownloadedButNotDeletedFromCloudInThisSession.Clear()

        LogTableIndex = 0
        TotalReportTableIndex = 0
        ReDim TotalReportTable(LogTableSize)

        FilenameByDateTableIndex = 0
        FilenameByDateTableMaxSize = FilenameByDateTableIncrement
        ReDim FileNameByDateTable(FilenameByDateTableMaxSize)

        ResetAllCharts()

        Me.Refresh()

    End Sub

    Private Sub ReLoadGlobalVariablesFromSettings()

        gRackSpaceUsername = My.Settings.UserID
        gRackSpaceAPIKey = My.Settings.APIKey
        gDeleteDownloadedLogsFromCloud = My.Settings.DeleteDowloadLogsFromCloud

        gIncludeExcludeFlag = My.Settings.IncludeExcludeFlag
        gFilesToInclude = My.Settings.FilesToInclude.ToUpper
        gFilesToExclude = My.Settings.FilesToExclude.ToUpper

        gIgnore400Requests = My.Settings.Ignore400Requests
        gShowHeaderChecks = My.Settings.ShowHeaderChecks
        gEquatePartialDownloads = My.Settings.EquatePartialDownloads

        gStartDate = My.Settings.StartDate
        gEndDate = My.Settings.EndDate

    End Sub

    Private Sub ResetAllCharts()

        ResetChart(Me.ChartOfDownloadsByDate)
        ResetChart(Me.ChartOfDataTransferredByDate)
        ResetChart(Me.ChartOfDownloadsByFileName)
        ResetChart(Me.ChartOfDataTransferred)
        ResetChart(Me.ChartOfCompletedDownloadsByCountry)

        Me.rtbReferrers.Rtf = String.Empty
        Me.rtbIP.Rtf = String.Empty

    End Sub

    Private Sub ResetChart(ByRef Chart As Chart)

        Chart.Titles(0).Text = ""

        For Each IndividualSeries As Series In Chart.Series
            IndividualSeries.Points.Clear()
            IndividualSeries.IsVisibleInLegend = False
        Next

        Chart.Refresh()

    End Sub

    Private Sub SetSettingsVersion()

        Dim a As System.Reflection.Assembly = System.Reflection.Assembly.GetExecutingAssembly()
        Dim appVersion As Version = a.GetName().Version
        Dim appVersionString As String = appVersion.ToString

        If My.Settings.ApplicationVersion <> appVersion.ToString Then

            My.Settings.Reload()
            My.Settings.Upgrade()
            My.Settings.ApplicationVersion = appVersionString
            My.Settings.Save()

        End If

        If My.Settings.NextTimeIPvDataCanBeDownloaded = Nothing Then
            My.Settings.NextTimeIPvDataCanBeDownloaded = Now.AddYears(-1)
            My.Settings.Save()
        End If

    End Sub

    Private Sub SetWorkingFolders()

        Dim RootFolder As String = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)

        gDataDirectory = RootFolder & "\" & Me.ProductName

        gDownloadFolder = gDataDirectory & "\Downloaded"
        gDeCompressedFolder = gDataDirectory & "\Decompressed"
        gCombinedFolder = gDataDirectory & "\Combined"
        gIPv4Folder = gDataDirectory & "\IPv4"
        gIPv6Folder = gDataDirectory & "\IPv6"

        If Not System.IO.Directory.Exists(gDataDirectory) Then System.IO.Directory.CreateDirectory(gDataDirectory)
        If Not System.IO.Directory.Exists(gDownloadFolder) Then System.IO.Directory.CreateDirectory(gDownloadFolder)
        If Not System.IO.Directory.Exists(gDeCompressedFolder) Then System.IO.Directory.CreateDirectory(gDeCompressedFolder)
        If Not System.IO.Directory.Exists(gCombinedFolder) Then System.IO.Directory.CreateDirectory(gCombinedFolder)
        If Not System.IO.Directory.Exists(gIPv4Folder) Then System.IO.Directory.CreateDirectory(gIPv4Folder)
        If Not System.IO.Directory.Exists(gIPv6Folder) Then System.IO.Directory.CreateDirectory(gIPv6Folder)

    End Sub

#End Region

#Region "Get country data from MaxMind PostgresSQL database base"

    Private conn As NpgsqlConnection

    Private Async Sub SetupConnectionToMaxMindDatabase()

        Dim connString As String = "Host=" & MaxMindHost & ";Database=" & MaxMindDatabaseName & ";Username=" & MaxMindDatabaseUserID & ";Password=" & MaxMindDatabasePassword & ";"

        conn = New NpgsqlConnection(connString)

        Await conn.OpenAsync()

    End Sub


    Private KnownIPs = New List(Of String)
    Private KnownIPsCountryCodes = New List(Of String)

    Private KnownCountryCodes = New List(Of String)
    Private KnownCountryNames = New List(Of String)

    Function LookupKnownIPs(IP) As String

        Dim index As Int64 = KnownIPs.indexof(IP)

        If index >= 0 Then
            Return KnownIPsCountryCodes(index)
        Else
            Return ""
        End If

    End Function

    Private Function GetCountry(IP As String) As String

        If IP = "" Then Return ""

        ' for performance reasons if the current IP being requested is the same as the last IP requested, return the same results as before
        Static Dim LastIP As String = ""
        Static Dim LastReturnValue As String = ""

        If IP = LastIP Then
            If LastReturnValue > "" Then
                Return LastReturnValue
            End If
        End If
        LastIP = IP

        ' for performance reasons, look in a list of known IP values to see if this IP has already been retrieved from the database
        'If it has Then Return the previousily stored results
        Dim ExistingValue As String = LookupKnownIPs(IP)

        If ExistingValue > "" Then
            Return ExistingValue
        End If

        Dim ReturnValue As String = ""

        Dim SQLQuery As String = "select registered_country_geoname_id from geoip2_network where network >> '" & IP & "';"

        Try

            Using cmd As New NpgsqlCommand(SQLQuery, conn)

                Using reader As NpgsqlDataReader = cmd.ExecuteReader()

                    While reader.Read()

                        ReturnValue = reader.GetValue(0).ToString

                    End While

                End Using

            End Using

        Catch ex As Exception

            ReturnValue = "unknown"

        End Try

        ' for performance reasons
        ' add results to an in memory table of already retrieved IP addresses

        'If IP > "" Then

        '    KnownIPsCountryCodes.add(ReturnValue)
        '    KnownIPs.add(IP)

        'End If

        LastReturnValue = ReturnValue

        Return ReturnValue

    End Function

    Function LookupKnownCountryCodes(CountryCode) As String

        Dim index As Int64 = KnownCountryCodes.indexof(CountryCode)

        If index >= 0 Then
            Return KnownCountryNames(index)
        Else
            Return ""
        End If

    End Function

    Private Function GetRealCountryName(country_code As String) As String

        If country_code = "" Then Return ""

        ' try searching names that have already been retrieved from the database
        Dim ExistingValue As String = LookupKnownCountryCodes(country_code)

        If ExistingValue > "" Then Return ExistingValue

        ' could not find a name that was already retrieved from the database
        ' so query the databse to get the name 

        Dim ReturnValue As String = ""

        Dim SQLQuery As String = "select country_name from geoip2_location where geoname_id = " & country_code & ";"

        Using cmd As New NpgsqlCommand(SQLQuery, conn)

            Using reader As NpgsqlDataReader = cmd.ExecuteReader()

                While reader.Read()

                    ReturnValue = reader.GetValue(0).ToString

                End While

            End Using

        End Using

        KnownCountryCodes.add(country_code)
        KnownCountryNames.add(ReturnValue)

        Console.WriteLine(ReturnValue)

        Return ReturnValue

    End Function

#End Region


#Region "Download and combine log files"

    Private Sub InventoryExistingDownloadedAndDecompressedLogFiles()

        Dim MasterFilename As String = gDownloadFolder & "\" & cMasterFileOfDownloadedFilesFileName

        LogFilesPreviousilyDownloaded.Clear()

        If File.Exists(MasterFilename) Then

            Dim ws() As String = My.Computer.FileSystem.ReadAllText(MasterFilename).Split(vbLf)

            For Each line In ws
                If line.Length > 0 Then
                    LogFilesPreviousilyDownloaded.Add(line)
                End If
            Next

        End If

    End Sub

    Private Sub DownloadLogFiles()

        Dim MasterFilename As String = gDownloadFolder & "\" & cMasterFileOfDownloadedFilesFileName

        Try

            '*** Set Identity
            Dim cloudIdentity As New CloudIdentity() With {.Username = gRackSpaceUsername, .APIKey = modSecurity.EncryptDecryptClass.Decrypt(gRackSpaceAPIKey)}
            Dim cloudIdentityProvider As New CloudIdentityProvider(cloudIdentity)

            '*** List Root Containers, make sure log file container is one of them
            Const cLogContainer As String = ".CDN_ACCESS_LOGS"

            Dim cfp As New CloudFilesProvider(cloudIdentity)
            Dim RootLevelContainers As System.Collections.IEnumerable

            Try

                RootLevelContainers = cfp.ListContainers()

            Catch ex As Exception

                Call MsgBox("Could not connect to Rackspace, perhaps your network is down?", vbOK Or vbExclamation, "Reporting for Rackspace - Alert")
                Exit Sub

            End Try

            Dim LogContainerFound As Boolean = False
            For Each entry As net.openstack.Core.Domain.Container In RootLevelContainers

                If entry.Name.ToString = cLogContainer Then
                    LogContainerFound = True
                    Exit For
                End If

            Next

            If LogContainerFound Then

                Dim DownloadedFilesCounter As Integer = 0

                Dim LogFileContainer As System.Collections.IEnumerable = cfp.ListObjects(cLogContainer)

                For Each entry As net.openstack.Core.Domain.ContainerObject In LogFileContainer

                    ' example file name on server:
                    ' Released/2017/01/07/04/68c3a9e9199f3a729b39ec967dd24679.log.gz

                    Dim SourceContainer As String = cLogContainer & "\" & System.IO.Path.GetDirectoryName(entry.Name)
                    Dim SourceFileName As String = System.IO.Path.GetFileName(entry.Name)
                    Dim TargetFileName As String = entry.Name.Replace("/", "_")

                    If LogFilesPreviousilyDownloaded.Contains(TargetFileName) Then

                        ' already downloaded - it will not be downloaded again

                    Else

                        If gDeleteDownloadedLogsFromCloud Then

                            Try

                                cfp.GetObjectSaveToFile(cLogContainer, gDownloadFolder, entry.Name, TargetFileName)
                                LogFilesDownloadedButNotDeletedFromCloudInThisSession.Add(TargetFileName)
                                cfp.DeleteObject(cLogContainer, entry.Name)
                                LogFilesDownloadedButNotDeletedFromCloudInThisSession.Remove(TargetFileName)
                                Me.ToolStripStatusLabel.Text = "Downloaded and removed from cloud: " & entry.Name
                                Me.StatusStrip.Refresh()

                            Catch ex As Exception

                                Me.ToolStripStatusLabel.Text = "Problem removing from cloud: " & entry.Name

                            End Try

                        Else

                            Try

                                Try

                                    cfp.GetObjectSaveToFile(cLogContainer, gDownloadFolder, entry.Name, TargetFileName)
                                    LogFilesDownloadedButNotDeletedFromCloudInThisSession.Add(TargetFileName)

                                    Me.ToolStripStatusLabel.Text = "Downloaded: " & entry.Name

                                Catch ex1 As System.Net.WebException

                                    Me.ToolStripStatusLabel.Text = "Could not find: " & entry.Name

                                Catch ex2 As net.openstack.Core.Exceptions.Response.ItemNotFoundException

                                    Me.ToolStripStatusLabel.Text = "Could not find: " & entry.Name

                                Catch ex3 As Exception

                                    Me.ToolStripStatusLabel.Text = "Could not find: " & entry.Name

                                End Try

                            Catch ex4 As Exception

                                Me.ToolStripStatusLabel.Text = "Could not find: " & entry.Name

                            End Try

                            Me.StatusStrip.Refresh()

                        End If

                        DownloadedFilesCounter += 1

                    End If

                Next

                If LogFilesDownloadedButNotDeletedFromCloudInThisSession.Count > 0 Then

                    ' update master list with Log files downloaded but not deleted from cloud in this session

                    Dim ws As String = String.Empty
                    For Each line In LogFilesDownloadedButNotDeletedFromCloudInThisSession
                        ws &= line & vbLf
                    Next

                    If My.Computer.FileSystem.FileExists(MasterFilename) Then
                        My.Computer.FileSystem.WriteAllText(MasterFilename, ws, True)
                    Else
                        My.Computer.FileSystem.WriteAllText(MasterFilename, ws, False)
                    End If

                End If

                My.Settings.LastCheck = Now
                My.Settings.Save()

            Else

                MsgBox("{0} not found, make sure logging is enabled.", cLogContainer)

            End If

        Catch ex As Exception

            MsgBox(ex.ToString)

        End Try

    End Sub

    Private Sub DeleteFilesFromServerThatHaveAlreadyBeenDownloaded()

        'this deletes logs that have already been downloaded

        Dim HoldStatus As String = Me.ToolStripStatusLabel.Text

        Try

            InventoryExistingDownloadedAndDecompressedLogFiles()

            If LogFilesPreviousilyDownloaded.Count > 0 Then

                '*** Set Identity
                Dim cloudIdentity As New CloudIdentity() With {.Username = gRackSpaceUsername, .APIKey = modSecurity.EncryptDecryptClass.Decrypt(gRackSpaceAPIKey)}
                Dim cloudIdentityProvider As New CloudIdentityProvider(cloudIdentity)

                '*** List Root Containers, make sure log file container is one of them
                Const cLogContainer As String = ".CDN_ACCESS_LOGS"

                Dim cfp As New CloudFilesProvider(cloudIdentity)
                Dim RootLevelContainers As System.Collections.IEnumerable = cfp.ListContainers()

                Dim LogContainerFound As Boolean = False
                For Each entry As net.openstack.Core.Domain.Container In RootLevelContainers

                    If entry.Name.ToString = cLogContainer Then
                        LogContainerFound = True
                        Exit For
                    End If

                Next

                If LogContainerFound Then

                    Dim LogFileContainer As System.Collections.IEnumerable = cfp.ListObjects(cLogContainer)
                    For Each entry As net.openstack.Core.Domain.ContainerObject In LogFileContainer

                        'Released/2017/01/07/04/68c3a9e9199f3a729b39ec967dd24679.log.gz

                        Dim SourceContainer As String = cLogContainer & "\" & System.IO.Path.GetDirectoryName(entry.Name)
                        Dim SourceFileName As String = System.IO.Path.GetFileName(entry.Name)
                        Dim TargetFileName As String = entry.Name.Replace("/", "_")

                        If LogFilesPreviousilyDownloaded.Contains(TargetFileName) Then

                            cfp.DeleteObject(cLogContainer, entry.Name)

                            Me.ToolStripStatusLabel.Text = "Removed from cloud: " & entry.Name
                            Me.StatusStrip.Refresh()

                        End If

                    Next

                Else

                    MsgBox("{0} not found, make sure logging is enabled.", cLogContainer)

                End If

            End If

            'at this point all the files in the master log file of downloaded files have been deleted, so the master log file can be deleted too

            Dim MasterFilename As String = gDownloadFolder & "\" & cMasterFileOfDownloadedFilesFileName
            If My.Computer.FileSystem.FileExists(MasterFilename) Then My.Computer.FileSystem.DeleteFile(MasterFilename)
            LogFilesPreviousilyDownloaded.Clear()

        Catch ex As Exception

            MsgBox(ex.ToString)

        End Try

        Me.ToolStripStatusLabel.Text = HoldStatus

    End Sub

    Private Sub DecompressDownloadFolder()

        Try

            Dim di As New IO.DirectoryInfo(gDownloadFolder)
            Dim fi As IO.FileInfo() = di.GetFiles()

            System.Threading.Tasks.Parallel.ForEach(fi, Sub(dra)
                                                            If dra.FullName.EndsWith(".gz") Then

                                                                Decompress(dra)
                                                                My.Computer.FileSystem.DeleteFile(dra.FullName)

                                                            End If
                                                        End Sub)

        Catch ex As Exception
            Console.WriteLine(ex.ToString)
        End Try

    End Sub

    Private Sub Decompress(ByVal FileToDecompress As FileInfo)

        Using OriginalFileStream As FileStream = FileToDecompress.OpenRead()

            Dim CurrentFileName As String = FileToDecompress.FullName

            Dim newFileName = gDeCompressedFolder & "\" & Path.GetFileName(CurrentFileName.Remove(CurrentFileName.Length - FileToDecompress.Extension.Length))

            If System.IO.File.Exists(newFileName) Then

                'Decompressed file already exists, it will not be overridden

            Else

                Using decompressedFileStream As FileStream = File.Create(newFileName)
                    Using decompressionStream = New GZipStream(OriginalFileStream, CompressionMode.Decompress)
                        decompressionStream.CopyTo(decompressedFileStream)
                    End Using
                End Using

            End If

        End Using

    End Sub

    Private Sub DeleteFiles(ByVal ListOfFiles As HashSet(Of String))

        Dim HoldStatus As String = Me.ToolStripStatusLabel.Text

        Dim filename As String = String.Empty

        For Each entry In ListOfFiles

            filename = gDownloadFolder & "\" & entry
            If My.Computer.FileSystem.FileExists(filename) Then
                My.Computer.FileSystem.DeleteFile(filename)
                Me.ToolStripStatusLabel.Text = "Deleted: " & filename
                Me.StatusStrip.Refresh()
            End If

        Next

        Me.ToolStripStatusLabel.Text = HoldStatus

    End Sub

    Private Sub CombineAllDecompressedFiles()

        Dim OriginalFileExistedWhenWeStarted As Boolean
        Dim OriginalfileName As String = gCombinedFolder & "\" & cCombineFileName
        Dim BackupFilename As String = gCombinedFolder & "\~Backup_" & cCombineFileName
        Dim NewFilename As String = gCombinedFolder & "\~New_" & cCombineFileName

        Dim CombineSucceeded As Boolean = False

        Try

            If My.Computer.FileSystem.FileExists(BackupFilename) Then
                Beep()
                Call MsgBox("Found backup file, it should not exist - please investigate")
                Application.Exit()
            End If

            If My.Computer.FileSystem.FileExists(NewFilename) Then
                Beep()
                Call MsgBox("Found new file name, it should not exist - please investigate")
                Application.Exit()
            End If


            If My.Computer.FileSystem.FileExists(OriginalfileName) Then
                OriginalFileExistedWhenWeStarted = True
                My.Computer.FileSystem.CopyFile(OriginalfileName, BackupFilename)
            Else
                OriginalFileExistedWhenWeStarted = False
            End If


            'Read original combined File into memory
            Dim MasterCombinedFile As New StringBuilder
            MasterCombinedFile.Clear()
            If OriginalFileExistedWhenWeStarted Then
                MasterCombinedFile.Append(System.IO.File.ReadAllText(OriginalfileName))
            End If


            'Accumlate decompressed downlowed files into memory

            Dim AccumulationFile As New StringBuilder
            AccumulationFile.Clear()

            Dim di As New IO.DirectoryInfo(gDeCompressedFolder)
            Dim fi As IO.FileInfo() = di.GetFiles()

            If fi.Count > 0 Then

                For Each dra As IO.FileInfo In fi

                    AccumulationFile.Append(System.IO.File.ReadAllText(dra.FullName))

                Next

                'Create a new temp file combining the orignal combined file with the accumulation of all the new files
                MasterCombinedFile.Append(AccumulationFile)

            End If

            File.WriteAllText(NewFilename, MasterCombinedFile.ToString)

            'clean up 
            If OriginalFileExistedWhenWeStarted Then My.Computer.FileSystem.DeleteFile(OriginalfileName)
            My.Computer.FileSystem.RenameFile(NewFilename, cCombineFileName)
            If OriginalFileExistedWhenWeStarted Then My.Computer.FileSystem.DeleteFile(BackupFilename)

            CombineSucceeded = True

        Catch ex As Exception

            CombineSucceeded = False

            If OriginalFileExistedWhenWeStarted Then
                If My.Computer.FileSystem.FileExists(BackupFilename) Then
                    My.Computer.FileSystem.DeleteFile(OriginalfileName)
                    My.Computer.FileSystem.RenameFile(BackupFilename, cCombineFileName)
                End If
            End If

            Beep()
            Call MsgBox("Problem with combine" & vbCrLf & ex.ToString)

        End Try

        Try

            If CombineSucceeded Then

                Dim di As New IO.DirectoryInfo(gDeCompressedFolder)
                Dim fi As IO.FileInfo() = di.GetFiles()

                For Each dra As IO.FileInfo In fi

                    My.Computer.FileSystem.DeleteFile(dra.FullName)

                Next

            End If

        Catch ex As Exception
            Console.WriteLine(ex.ToString)
        End Try


    End Sub

#End Region

    Private Sub ProcessCombinedFile()

        Try

            Dim filename As String = gCombinedFolder & "\" & cCombineFileName

            Const ValidReturnCodes As String = " 200; 206; 301; 302; 304; 400; 401; 403; 404; 408; 410; 414; "

            Dim Index As Integer = 0

            Dim lStartDate, lEndDate As Date
            Dim lAllDates As Boolean
            SetStartAndEndDates(lStartDate, lEndDate, lAllDates)

            Dim lineCount As Integer = File.ReadLines(filename).Count()
            ReDim Preserve LogTable(lineCount)

            Parallel.ForEach(System.IO.File.ReadLines(filename), Sub(ContentLine As String)

                                                                     If ContentLine.Trim.Length > 0 Then

                                                                         Try

                                                                             Dim wa() As String
                                                                             wa = ContentLine.Split(" ")

                                                                             ' ************** Load Date and Time
                                                                             Dim ws As String = wa(3).Remove(0, 1)

                                                                             Dim wsDateTime As String = ws.Remove(10) & " " & ws.Remove(0, 11)

                                                                             Dim wdt As DateTime = DateTime.ParseExact(wsDateTime, "dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture)

                                                                             If lAllDates Then
                                                                             Else
                                                                                 If (wdt < lStartDate) OrElse (wdt > lEndDate) Then
                                                                                     Exit Try ' skip record
                                                                                 End If
                                                                             End If

                                                                             ' ************** Load Data Transferred
                                                                             Dim wDataTransfered As Long = CType(wa(9).Trim, Long)

                                                                             ' ************** Load Return Code
                                                                             '200 - OK; indicates a successful request resulting in a file being returned.
                                                                             '206 - Partial Content; indicates that a file was only partially downloaded. The download could have been interrupted by someone leaving the page before it's fully loaded (in the case of embedded images) or cancelling a download (in the case of PDF, MP3 and similar file types).
                                                                             '301 - Moved Permanently; the Server has indicated that the requested file Is now located at a New address. Search engines should update their index by removing the old address And replacing it (PR intact) with the New one.
                                                                             '302 - Found ; the User has been redirected, but as it's not a Permanent redirect no further action needs to be taken. This could be as simple as the server adding a / to the end of the request, or the result of a header command in PHP.
                                                                             '304 - Not Modified; an intelligent user agent (browser) has made a request for a file which Is already present in it's cache. A 304 indicates that the cached version has the same timestamp as the 'live' version of the file so they don't need to download it. If the 'live' file was newer then the response would instead be a 200.
                                                                             '400 - Bad Request; the Server couldn't make sense of the request.
                                                                             '401 - Unauthorised (password required); an attempt has been made to access a directory Or file that requires authentication (username And password). Subsequent requests would normally contain a username And password, resulting in either a 200 (user has been authenticated) Or 401 (authentication failed).
                                                                             '403 - Forbidden; the Server has blocked access to a directory Or file. This typically applies to requests that would otherwise result in a directory listing being displayed.
                                                                             '404 - Not Found; the requested file does Not exist on the server. This normally indicates a broken link (internal Or external).
                                                                             '408 - Request Timeout; the client / Server connection process was so slow that the server decided to 'hang up'.
                                                                             '410 - Gone; the Server has indicated that the requested file used to exist but has now been permanently removed. Search engines should remove the address from their index
                                                                             '414 - Request-URI Too Long; the request was too long. This normally indicates an attempt to compromise the server using a buffer overflow exploit.
                                                                             ws = wa(8).Trim

                                                                             Dim wReturnCode As Integer

                                                                             If ValidReturnCodes.Contains(" " & ws & "; ") Then
                                                                                 wReturnCode = CType(ws, Integer)

                                                                             Else

                                                                                 'if an invalid return code is received and there was data transferred, assume it is for a partial content request, otherwise mark it as a bad request

                                                                                 If wDataTransfered > 0 Then
                                                                                     wReturnCode = 206
                                                                                 Else
                                                                                     wReturnCode = 400
                                                                                 End If

                                                                             End If

                                                                             If gIgnore400Requests AndAlso (wReturnCode > 399) AndAlso (wReturnCode < 500) Then
                                                                                 Exit Try 'skip record
                                                                             End If

                                                                             ' ************** Load Filename
                                                                             ws = wa(6).Remove(0, 1)
                                                                             ws = ws.Remove(0, ws.IndexOf("/") + 1)
                                                                             Dim wi As Integer = ws.IndexOf("?")

                                                                             Dim wFileName As String

                                                                             If wi = -1 Then
                                                                                 wFileName = ws.ToLower
                                                                             Else
                                                                                 wFileName = ws.Remove(wi).ToLower
                                                                             End If

                                                                             ' If wFileName = "arulersetup.exe" Then xxx += 1

                                                                             Dim wCheckFilename As String = wFileName
                                                                             If wCheckFilename.Contains("?") Then wCheckFilename = wCheckFilename.Remove(wCheckFilename.IndexOf("?"))

                                                                             If (wReturnCode > 399) AndAlso (wReturnCode < 500) Then
                                                                             Else
                                                                                 'if a 400 series return code, don'tadd the filename to the list of unique filenames (even if the option to ignor the 400 series return codes is not checked)
                                                                                 SyncLock gUniqueFilenames
                                                                                     gUniqueFilenames.Add(wCheckFilename)
                                                                                 End SyncLock

                                                                             End If

                                                                             wCheckFilename = wCheckFilename.ToUpper

                                                                             If wCheckFilename = String.Empty Then
                                                                                 Exit Try 'skip record
                                                                             ElseIf (gIncludeExcludeFlag = "Include specific files only") AndAlso (Not (gFilesToInclude.Contains(wCheckFilename))) Then
                                                                                 Exit Try 'skip record
                                                                             ElseIf (gIncludeExcludeFlag = "Exclude specific files") AndAlso (gFilesToExclude.Contains(wCheckFilename)) Then
                                                                                 Exit Try 'skip record
                                                                             End If


                                                                             ' ************** Load IP Address
                                                                             Dim wIPAddress As String = wa(0)


                                                                             ' ************** Load Action
                                                                             Dim wAction As GetOrCheck
                                                                             If wa(5).Contains("GET") Then
                                                                                 wAction = GetOrCheck.GetFile
                                                                             Else
                                                                                 wAction = GetOrCheck.CheckFile
                                                                             End If


                                                                             ' ************** Load ReferingPage
                                                                             Dim wReferingPage As String = wa(10).Replace("""", "")

                                                                             If wReferingPage.ToUpper.EndsWith("INDEX.HTML") Then
                                                                                 wReferingPage = wReferingPage.Remove(wReferingPage.Length - 10)
                                                                             End If


                                                                             SyncLock LogTable

                                                                                 LogTable(Index).LogDateTime = wdt
                                                                                 LogTable(Index).DataTransferred = wDataTransfered
                                                                                 LogTable(Index).ReturnCode = wReturnCode
                                                                                 LogTable(Index).Filename = wFileName.Replace("%2E", ".")
                                                                                 LogTable(Index).IPAddress = wIPAddress
                                                                                 LogTable(Index).Action = wAction
                                                                                 LogTable(Index).ReferingPage = wReferingPage

                                                                                 Index += 1

                                                                             End SyncLock


                                                                         Catch ex As Exception

                                                                         End Try

                                                                     End If

                                                                 End Sub)


            LogTableSize = Index - 1

            ReDim Preserve LogTable(LogTableSize)

        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try

    End Sub

    Private Sub SetStartAndEndDates(ByRef StartDate As Date, ByRef EndDate As Date, ByRef AllDates As Boolean)

        AllDates = False

        Dim RightNow As Date = Now.Date

        Select Case My.Settings.DateRange

            Case Is = "Today"

                StartDate = RightNow
                EndDate = RightNow

            Case Is = "Yesterday"

                StartDate = RightNow.AddDays(-1)
                EndDate = RightNow.AddDays(-1)

            Case Is = "This week"

                Dim DayOfWeek = CType(Now.DayOfWeek, Integer)
                Dim BackupToStartOfWeek = -1 * (DayOfWeek - 1)
                Dim EndOfThisWeek = BackupToStartOfWeek + 6

                StartDate = RightNow.AddDays(BackupToStartOfWeek)
                EndDate = RightNow.AddDays(EndOfThisWeek)

            Case Is = "Last week"

                Dim DayOfWeek = CType(Now.DayOfWeek, Integer)
                Dim BackupToEndOfLastWeek = -1 * (DayOfWeek - 1) - 1
                Dim BackupToStartOfLastWeek = BackupToEndOfLastWeek - 6

                StartDate = RightNow.AddDays(BackupToStartOfLastWeek)
                EndDate = RightNow.AddDays(BackupToEndOfLastWeek)

            Case Is = "This month"

                StartDate = New Date(RightNow.Year, RightNow.Month, 1)
                EndDate = StartDate.AddMonths(1).AddDays(-1)

            Case Is = "Last month"

                Dim LastMonth As Date = RightNow.AddMonths(-1)
                StartDate = New Date(LastMonth.Year, LastMonth.Month, 1)
                EndDate = StartDate.AddMonths(1).AddDays(-1)

            Case Is = "This year"

                StartDate = New Date(RightNow.Year, 1, 1)
                EndDate = StartDate.AddYears(1).AddDays(-1)

            Case Is = "Last year"

                StartDate = New Date(RightNow.Year - 1, 1, 1)
                EndDate = StartDate.AddYears(1).AddDays(-1)

            Case Is = "Today + last 7 days"
                StartDate = RightNow.AddDays(-7)
                EndDate = RightNow

            Case Is = "Specific range"

                StartDate = gStartDate.Date
                EndDate = gEndDate.Date

            Case Is = "All dates"

                AllDates = True

        End Select

        StartDate = StartDate.Date
        EndDate = EndDate.Date.AddDays(1).AddSeconds(-1) ' v1.6 corrected end date calculation

    End Sub

    Private Function TallyTableEntries() As Task

        If gEquatePartialDownloads Then

            CreateListOfProgramsAndTheirFileSizes()

            ' if file was tagged as completed but it was too small change it to a partial download
            For index As Integer = 0 To LogTable.Count - 1

                If (LogTable(index).Action = GetOrCheck.GetFile) AndAlso
                   (LogTable(index).ReturnCode = 200) AndAlso
                   (LogTable(index).DataTransferred < GetFileSizeToBeConsideredAsACompletedDownload(LogTable(index).Filename)) Then

                    LogTable(index).ReturnCode = 206

                End If

            Next

        End If

        CreateTotalReportTable()
        CreateFileNameByDateTable()
        CreateTop10CountriesTable()
        CreateRefererTable()
        CreateIPTable()

    End Function
    Private Sub CreateTotalReportTable()

        ReDim Preserve TotalReportTable(LogTableSize)

        Parallel.ForEach(LogTable, Sub(logentry As LogContent)

                                       For x As Integer = 0 To LogTableSize

                                           If TotalReportTable(x).FileName = String.Empty Then

                                               SyncLock (TotalReportTable)
                                                   TotalReportTable(x).FileName = logentry.Filename
                                                   TotalReportTable(x).CompletedDownloadCount = 0
                                                   TotalReportTable(x).CompletedDownloadsBasedOnEquivalentPartialDownloadCount = 0
                                                   TotalReportTable(x).PartialDownloadCount = 0
                                                   TotalReportTable(x).CheckCount = 0
                                                   TotalReportTable(x).DataTransferred = 0
                                               End SyncLock

                                           End If

                                           If TotalReportTable(x).FileName = logentry.Filename Then

                                               SyncLock (TotalReportTable)
                                                   If logentry.Action = GetOrCheck.CheckFile Then
                                                       TotalReportTable(x).CheckCount += 1
                                                   Else
                                                       If logentry.ReturnCode = 200 Then
                                                           TotalReportTable(x).CompletedDownloadCount += 1
                                                       Else
                                                           If logentry.ReturnCode = 206 Then
                                                               TotalReportTable(x).PartialDownloadCount += 1
                                                           End If
                                                       End If
                                                   End If

                                                   TotalReportTable(x).DataTransferred += logentry.DataTransferred
                                               End SyncLock

                                               Exit For

                                           End If

                                       Next

                                   End Sub)


        For x As Integer = 0 To LogTableSize

            If TotalReportTable(x).FileName = String.Empty Then
                LogTableSize = x - 1
                Exit For
            End If

        Next

        ReDim Preserve TotalReportTable(LogTableSize)

        If gEquatePartialDownloads Then
            AddEquivalentCompletedDownloadsToTotalReportTable()
        End If

    End Sub

    Private Sub AddEquivalentCompletedDownloadsToTotalReportTable()

        ' the following calculates the number of equivalent completed downloads for each filename for which there is at least one complete download

        For Each Entry In ListOFProgramsAndThereMaximumFileSize

            ' add the combined partial counts into the total count
            For x As Integer = 0 To TotalReportTable.Length - 1
                If TotalReportTable(x).FileName = Entry.FileName Then
                    TotalReportTable(x).CompletedDownloadsBasedOnEquivalentPartialDownloadCount = CalculateNumberOfEquivalentCompletedDownloads(Entry.FileName)
                    Exit For
                End If
            Next

        Next

    End Sub

    Private Function CalculateNumberOfEquivalentCompletedDownloads(ByVal FileName As String) As Integer

        Dim ReturnValue As Integer = 0

        Try

            Dim RunningTotal As Integer = 0
            Dim TimeDifference As Integer = 0
            Dim CombineFlag As Boolean = False

            ' Create a list combining all partial downloads based on which IP address downloaded them and when

            Dim NewEntry As New CalculatedDownLoadTableContent

            CalculatedDownLoadList.Clear()

            Dim FilteredLogTable() As LogContent = LogTable.Where(Function(c) (c.Filename = FileName) AndAlso (c.Action = GetOrCheck.GetFile) AndAlso (c.ReturnCode = "206")).OrderBy(Function(c) c.Filename).ThenBy(Function(c) c.IPAddress).ThenBy(Function(c) c.LogDateTime).ToArray

            ''******************

            For x = 0 To FilteredLogTable.Count - 2

                TimeDifference = (FilteredLogTable(x + 1).LogDateTime - FilteredLogTable(x).LogDateTime).TotalSeconds

                If (FilteredLogTable(x).IPAddress = FilteredLogTable(x + 1).IPAddress) AndAlso (FilteredLogTable(x).Filename = FilteredLogTable(x + 1).Filename) AndAlso (TimeDifference >= 0) AndAlso (TimeDifference <= cThresholdSecondsForACombine) Then

                    RunningTotal += FilteredLogTable(x).DataTransferred
                    CombineFlag = True

                Else

                    NewEntry.IP = FilteredLogTable(x).IPAddress

                    If CombineFlag Then
                        NewEntry.DataTransferred = FilteredLogTable(x).DataTransferred + RunningTotal
                        RunningTotal = 0
                        CombineFlag = False
                    Else
                        NewEntry.DataTransferred = FilteredLogTable(x).DataTransferred
                    End If

                    If NewEntry.DataTransferred > 0 Then CalculatedDownLoadList.Add(NewEntry)

                End If

            Next

            If FilteredLogTable.Count > 0 Then

                Dim x = FilteredLogTable.Count - 1

                NewEntry.IP = FilteredLogTable(x).IPAddress

                If CombineFlag Then
                    NewEntry.DataTransferred = FilteredLogTable(x).DataTransferred + RunningTotal
                Else
                    NewEntry.DataTransferred = FilteredLogTable(x).DataTransferred
                End If

                If NewEntry.DataTransferred > 0 Then CalculatedDownLoadList.Add(NewEntry)

            End If

            '******************

            ' compare entries in the calculated download list and based on the filesize of a completed download calculate the equivalent number of completed downloads

            For Each Entry In CalculatedDownLoadList
                ReturnValue += GetEquivalentDownloads(FileName, Entry.DataTransferred)
            Next


        Catch ex As Exception

            ReturnValue = 0
            Console.WriteLine(ex.ToString)

        End Try

        Return ReturnValue

    End Function
    Private Sub CreateFileNameByDateTable()

        System.Threading.Tasks.Parallel.ForEach(LogTable, Sub(LogEntry As LogContent)

                                                              Dim MatchingDate As Date = LogEntry.LogDateTime.Date

                                                              For x As Integer = 0 To FilenameByDateTableIndex

                                                                  If (FileNameByDateTable(x).FileName = String.Empty) AndAlso (FileNameByDateTable(x).DownloadDate = Nothing) Then

                                                                      SyncLock (FileNameByDateTable)

                                                                          FileNameByDateTable(x).FileName = LogEntry.Filename
                                                                          FileNameByDateTable(x).DownloadDate = MatchingDate

                                                                          If (LogEntry.Action = GetOrCheck.GetFile) AndAlso (LogEntry.ReturnCode = 200) Then FileNameByDateTable(x).DownloadCount += 1

                                                                          FileNameByDateTable(x).DataTransferred = LogEntry.DataTransferred

                                                                          FilenameByDateTableIndex += 1

                                                                      End SyncLock

                                                                      Exit For

                                                                  End If

                                                                  If (FileNameByDateTable(x).FileName = LogEntry.Filename) AndAlso (FileNameByDateTable(x).DownloadDate = MatchingDate) Then

                                                                      If (LogEntry.Action = GetOrCheck.GetFile) AndAlso (LogEntry.ReturnCode = 200) Then
                                                                          SyncLock (FileNameByDateTable)
                                                                              FileNameByDateTable(x).DownloadCount += 1
                                                                          End SyncLock
                                                                      End If

                                                                      SyncLock (FileNameByDateTable)
                                                                          FileNameByDateTable(x).DataTransferred += LogEntry.DataTransferred
                                                                      End SyncLock

                                                                      Exit For

                                                                  End If

                                                              Next

                                                              If FilenameByDateTableIndex = FilenameByDateTableMaxSize Then

                                                                  SyncLock (FileNameByDateTable)
                                                                      FilenameByDateTableMaxSize += FilenameByDateTableIncrement
                                                                      ReDim Preserve FileNameByDateTable(FilenameByDateTableMaxSize)
                                                                  End SyncLock

                                                              End If

                                                          End Sub)

        If FileNameByDateTable(FilenameByDateTableIndex).FileName = String.Empty Then FilenameByDateTableIndex -= 1

        ReDim Preserve FileNameByDateTable(FilenameByDateTableIndex)

        If gEquatePartialDownloads Then AddEquivalentCompletedDownloadsToFilenameByDateTable()

    End Sub
    Private Sub AddEquivalentCompletedDownloadsToFilenameByDateTable()

        Dim TimeDifference As Integer = 0
        Dim CombineFlag As Boolean = False
        Dim RunningTotal As Long = 0

        ' Create a list combining all partial downloads based on which IP address downloaded them and when

        Dim NewEntry As New FileNameByDateTableContent

        Dim CalculatedFileNameByDate As New List(Of FileNameByDateTableContent)

        CalculatedFileNameByDate.Clear()

        Dim FilteredLogTable() As LogContent = LogTable.Where(Function(c) (c.Action = GetOrCheck.GetFile) AndAlso (c.ReturnCode = "206")).OrderBy(Function(c) c.Filename).ThenBy(Function(c) c.IPAddress).ThenBy(Function(c) c.LogDateTime).ToArray

        For x = 0 To FilteredLogTable.Count - 2

            TimeDifference = (FilteredLogTable(x + 1).LogDateTime - FilteredLogTable(x).LogDateTime).TotalSeconds

            If (FilteredLogTable(x).IPAddress = FilteredLogTable(x + 1).IPAddress) AndAlso (FilteredLogTable(x).Filename = FilteredLogTable(x + 1).Filename) AndAlso (TimeDifference >= 0) AndAlso (TimeDifference <= cThresholdSecondsForACombine) Then

                RunningTotal += FilteredLogTable(x).DataTransferred
                CombineFlag = True

            Else

                NewEntry.FileName = FilteredLogTable(x).Filename
                NewEntry.DownloadDate = FilteredLogTable(x).LogDateTime.Date

                If CombineFlag Then
                    NewEntry.DownloadCount = GetEquivalentDownloads(FilteredLogTable(x).Filename, FilteredLogTable(x).DataTransferred + RunningTotal)
                    RunningTotal = 0
                    CombineFlag = False
                Else
                    NewEntry.DownloadCount = GetEquivalentDownloads(FilteredLogTable(x).Filename, FilteredLogTable(x).DataTransferred)
                End If

                If NewEntry.DownloadCount > 0 Then CalculatedFileNameByDate.Add(NewEntry)

            End If

        Next

        If FilteredLogTable.Count > 0 Then
            NewEntry.FileName = FilteredLogTable(FilteredLogTable.Count - 1).Filename
            NewEntry.DownloadDate = FilteredLogTable(FilteredLogTable.Count - 1).LogDateTime.Date

            If CombineFlag Then
                NewEntry.DownloadCount = GetEquivalentDownloads(FilteredLogTable(FilteredLogTable.Count - 1).Filename, FilteredLogTable(FilteredLogTable.Count - 1).DataTransferred + RunningTotal)
            Else
                NewEntry.DownloadCount = GetEquivalentDownloads(FilteredLogTable(FilteredLogTable.Count - 1).Filename, FilteredLogTable(FilteredLogTable.Count - 1).DataTransferred)
            End If

            If NewEntry.DownloadCount > 0 Then CalculatedFileNameByDate.Add(NewEntry)

        End If


        '++++++++++++++++++++++++++++++++++++

        Dim index As Integer = 0

        ReDim Preserve FileNameByDateTable(FilenameByDateTableIndex + CalculatedFileNameByDate.Count)

        For Each entry In CalculatedFileNameByDate

            If entry.DownloadCount > 0 Then

                index = Array.FindIndex(FileNameByDateTable, Function(c) (c.FileName = entry.FileName) AndAlso (c.DownloadDate = entry.DownloadDate.Date))

                If index >= 0 Then
                    FileNameByDateTable(index).DownloadCount += entry.DownloadCount
                Else
                    FilenameByDateTableIndex += 1
                    FileNameByDateTable(FilenameByDateTableIndex).FileName = entry.FileName
                    FileNameByDateTable(FilenameByDateTableIndex).DownloadDate = entry.DownloadDate
                    FileNameByDateTable(FilenameByDateTableIndex).DownloadCount = entry.DownloadCount
                End If

            End If

        Next

        ReDim Preserve FileNameByDateTable(FilenameByDateTableIndex)

    End Sub


    Private Async Function CreateTop10CountriesTable() As Task


        ' getting the Country Code from the IP address takes a long time
        ' so this routine is geared to indentifying unique ip addresses in the log so each ip address only needs to be looked up once
        ' note: I tried parallel processing in the first two steps, it doesn't help with performance

        '***********************************************************************************************************

        Dim UniqueIPAddresses As New HashSet(Of String)
        Dim IPAddressesWithACompletedDownload As New List(Of String)

        For Each LogTableEntry As LogContent In LogTable
            If (LogTableEntry.ReturnCode = 200) AndAlso (LogTableEntry.Action = GetOrCheck.GetFile) Then
                IPAddressesWithACompletedDownload.Add(LogTableEntry.IPAddress)
                UniqueIPAddresses.Add(LogTableEntry.IPAddress)
            End If
        Next

        IPAddressesWithACompletedDownload.Sort()

        '***********************************************************************************************************

        Dim IPCountriesTotalDownloads As New List(Of IPCountryTotalDownloadsContent)

        'System.Threading.Tasks.Parallel.For(0, UniqueIPAddresses.Count,
        '                                    Async Sub(x)


        '                                        Dim IP As String = UniqueIPAddresses(x)
        '                                        Dim IPCourntriesTotalDownloadsEntry As IPCountryTotalDownloadsContent

        '                                        IPCourntriesTotalDownloadsEntry.IP = IP
        '                                        IPCourntriesTotalDownloadsEntry.TotalDownloads = IPAddressesWithACompletedDownload.LastIndexOf(IP) - IPAddressesWithACompletedDownload.IndexOf(IP) + 1
        '                                        'IPCourntriesTotalDownloadsEntry.CountryCode = GetCountry(IP)
        '                                        IPCourntriesTotalDownloadsEntry.CountryCode = Await GetCountry(IP)


        '                                        SyncLock (IPCountriesTotalDownloads)
        '                                            IPCountriesTotalDownloads.Add(IPCourntriesTotalDownloadsEntry)
        '                                        End SyncLock

        '                                    End Sub)


        For x = 0 To UniqueIPAddresses.Count - 1

            Dim IP As String = UniqueIPAddresses(x)
            Dim IPCourntriesTotalDownloadsEntry As IPCountryTotalDownloadsContent

            IPCourntriesTotalDownloadsEntry.IP = IP
            IPCourntriesTotalDownloadsEntry.TotalDownloads = IPAddressesWithACompletedDownload.LastIndexOf(IP) - IPAddressesWithACompletedDownload.IndexOf(IP) + 1
            IPCourntriesTotalDownloadsEntry.CountryCode = GetCountry(IP)
            IPCountriesTotalDownloads.Add(IPCourntriesTotalDownloadsEntry)

        Next

        '***********************************************************************************************************
        ' create list of all countries

        IPCountriesTotalDownloads = IPCountriesTotalDownloads.OrderBy(Function(c) c.CountryCode).ToList

        If IPCountriesTotalDownloads.Count = 0 Then
            Top10Countries.Clear()
            Exit Function
        End If

        Dim CountryTotalDownloads As New List(Of CountryTotalDownloadsContent)
        Dim CountryTotalDownloadsEntry As CountryTotalDownloadsContent

        Dim PriorCountry As String = IPCountriesTotalDownloads(0).CountryCode
        Dim RunningTotal As Integer = IPCountriesTotalDownloads(0).TotalDownloads
        Dim FullTotal As Integer = 0

        For x = 1 To IPCountriesTotalDownloads.Count - 1

            If PriorCountry = IPCountriesTotalDownloads(x).CountryCode Then

                RunningTotal += IPCountriesTotalDownloads(x).TotalDownloads

            Else

                CountryTotalDownloadsEntry.CountryCode = PriorCountry
                CountryTotalDownloadsEntry.TotalDownloads = RunningTotal
                CountryTotalDownloads.Add(CountryTotalDownloadsEntry)

                FullTotal += RunningTotal

                PriorCountry = IPCountriesTotalDownloads(x).CountryCode
                RunningTotal = IPCountriesTotalDownloads(x).TotalDownloads

            End If

        Next

        CountryTotalDownloadsEntry.CountryCode = PriorCountry
        CountryTotalDownloadsEntry.TotalDownloads = RunningTotal
        CountryTotalDownloads.Add(CountryTotalDownloadsEntry)

        FullTotal += RunningTotal

        '***********************************************************************************************************
        'create list of top 10 countries

        CountryTotalDownloads = CountryTotalDownloads.OrderByDescending(Function(c) c.TotalDownloads).ToList

        Top10Countries.Clear()
        Dim Top10Entry As New Top10CountriesContent

        Dim Remainder As Integer = FullTotal


        Try

            For x = 0 To Math.Min(9, CountryTotalDownloads.Count - 1)

                Top10Entry.Country = CountryTotalDownloads(x).CountryCode
                Top10Entry.TotalDownloads = CountryTotalDownloads(x).TotalDownloads
                Top10Countries.Add(Top10Entry)

                Remainder -= CountryTotalDownloads(x).TotalDownloads

            Next

        Catch ex As Exception

        End Try

        '***********************************************************************************************************
        'Replace two digit country code with RealCountry Name

        Dim Top10CountriesTemp As New List(Of Top10CountriesContent)

        For Each entry In Top10Countries

            If entry.Country = String.Empty Then
            Else
                Top10Entry.Country = GetRealCountryName(entry.Country) ' (From c In CountryCodeTable Where c.CountryCode = entry.Country).Single.FullNameOfCountry
                Top10Entry.TotalDownloads = entry.TotalDownloads
                Top10CountriesTemp.Add(Top10Entry)
            End If

        Next

        Top10Countries.Clear()
        Top10Countries = Top10CountriesTemp

        '***********************************************************************************************************
        'sort countries in alphabetical order and then add 'All Others' at the bottom of the list

        Top10Countries = Top10Countries.OrderBy(Function(c) c.Country).ToList


        If Remainder > 0 Then

            Top10Entry.Country = "All Others"
            Top10Entry.TotalDownloads = Remainder
            Top10Countries.Add(Top10Entry)

        End If


        If gEquatePartialDownloads Then AddEquivalentCompletedDownloadsToTop10Countries()

    End Function

    '************************************************************************************************************************************************************************************************************

    Structure CalculatedDownLoadListForTop10Countries

        Dim CountryFullName As String
        Dim EqivalentDownloads As Integer

    End Structure
    Private Async Sub AddEquivalentCompletedDownloadsToTop10Countries()

        Dim CalculatedDownLoadListForTop10Countries As New List(Of CalculatedDownLoadListForTop10Countries)

        Dim EquivalentDownloadCount As Long = 0

        Try

            Dim TimeDifference As Integer = 0
            Dim CombineFlag As Boolean = False
            Dim NewEntry As New CalculatedDownLoadListForTop10Countries
            Dim RunningTotal As Long = 0

            CalculatedDownLoadListForTop10Countries.Clear()

            Dim FilteredLogTable() As LogContent = LogTable.Where(Function(c) (c.Action = GetOrCheck.GetFile) AndAlso (c.ReturnCode = "206")).OrderBy(Function(c) c.Filename).ThenBy(Function(c) c.IPAddress).ThenBy(Function(c) c.LogDateTime).ToArray

            For x = 0 To FilteredLogTable.Count - 2

                TimeDifference = (FilteredLogTable(x + 1).LogDateTime - FilteredLogTable(x).LogDateTime).TotalSeconds

                If (FilteredLogTable(x).IPAddress = FilteredLogTable(x + 1).IPAddress) AndAlso (FilteredLogTable(x).Filename = FilteredLogTable(x + 1).Filename) AndAlso (TimeDifference >= 0) AndAlso (TimeDifference <= cThresholdSecondsForACombine) Then

                    RunningTotal += FilteredLogTable(x).DataTransferred
                    CombineFlag = True

                Else

                    Dim UniqueCountryNumber As String = GetCountry(FilteredLogTable(x).IPAddress)
                    NewEntry.CountryFullName = GetRealCountryName(UniqueCountryNumber)


                    If CombineFlag Then
                        NewEntry.EqivalentDownloads = GetEquivalentDownloads(FilteredLogTable(x).Filename, FilteredLogTable(x).DataTransferred + RunningTotal)
                        RunningTotal = 0
                        CombineFlag = False
                    Else
                        NewEntry.EqivalentDownloads = GetEquivalentDownloads(FilteredLogTable(x).Filename, FilteredLogTable(x).DataTransferred)
                    End If

                    If NewEntry.EqivalentDownloads > 0 Then CalculatedDownLoadListForTop10Countries.Add(NewEntry)

                End If

                Application.DoEvents()

            Next

            If FilteredLogTable.Count > 0 Then

                Dim x = FilteredLogTable.Count - 1

                NewEntry.CountryFullName = GetCountry(FilteredLogTable(x).IPAddress)

                If CombineFlag Then
                    NewEntry.EqivalentDownloads = GetEquivalentDownloads(FilteredLogTable(x).Filename, FilteredLogTable(x).DataTransferred + RunningTotal)
                Else
                    NewEntry.EqivalentDownloads = GetEquivalentDownloads(FilteredLogTable(x).Filename, FilteredLogTable(x).DataTransferred)
                End If

                If NewEntry.EqivalentDownloads > 0 Then CalculatedDownLoadListForTop10Countries.Add(NewEntry)

            End If


            '******************

            ' create a replacement list for the Top10Countries list - it being equal to the original list + additions for equivalent downloads

            Dim CountryCode As String
            Dim AdditionsForAParticularCountry As Integer = 0
            Dim AdditionsForAllOtherCountries As Integer = CalculatedDownLoadListForTop10Countries.Sum(Function(c) c.EqivalentDownloads)

            Dim ReplacementList As New List(Of Top10CountriesContent)

            For Each entry In Top10Countries

                If entry.Equals(Top10Countries.Last) Then

                    entry.TotalDownloads += AdditionsForAllOtherCountries
                    ReplacementList.Add(entry)

                Else

                    AdditionsForAParticularCountry = CalculatedDownLoadListForTop10Countries.Where(Function(c) c.CountryFullName = entry.Country).Count

                    entry.TotalDownloads += AdditionsForAParticularCountry
                    AdditionsForAllOtherCountries -= AdditionsForAParticularCountry

                    ReplacementList.Add(entry)

                End If

            Next

            Top10Countries = ReplacementList

        Catch ex As Exception

            MsgBox(ex.ToString)

        End Try

    End Sub

    Private Sub CreateRefererTable()

        ReDim ReferrerTable(LogTable.Count)

        Dim ReferrerTableIndex As Integer = 0

        System.Threading.Tasks.Parallel.ForEach(LogTable, Sub(Entry As LogContent)

                                                              If (Entry.ReturnCode = 200) AndAlso (Entry.Action = GetOrCheck.GetFile) Then

                                                                  If (Entry.ReferingPage.Length > 0) Then

                                                                      SyncLock (ReferrerTable)

                                                                          Dim x As Integer = Array.FindIndex(ReferrerTable, Function(c) c.ReferingPage = Entry.ReferingPage)

                                                                          If x = -1 Then
                                                                              ReferrerTable(ReferrerTableIndex).ReferingPage = Entry.ReferingPage
                                                                              ReferrerTable(ReferrerTableIndex).Count = 1
                                                                              ReferrerTableIndex += 1
                                                                          Else
                                                                              ReferrerTable(x).Count += 1
                                                                          End If

                                                                      End SyncLock

                                                                  End If

                                                              End If

                                                          End Sub)

        ReDim Preserve ReferrerTable(ReferrerTableIndex - 1)

        ReferrerTable = ReferrerTable.OrderByDescending(Function(c) c.Count).ThenBy(Function(c) c.ReferingPage).ToArray

    End Sub

    Private Function GetNumericIP(IP As String) As Long

        Const x256 As Long = 256
        Const x65536 As Long = 65536
        Const x16777216 As Long = 16777216

        Dim sections() As String = Split(IP, ".")

        If sections.Count = 4 Then
            Return CType(sections(0), Long) * x16777216 + CType(sections(1), Long) * x65536 + CType(sections(2), Long) * x256 + CType(sections(3), Long)
        Else
            Return 0
        End If

    End Function

    Private Sub CreateIPTable()

        ReDim IPTable(LogTable.Count)

        Dim IPTableIndex As Integer = 0

        System.Threading.Tasks.Parallel.ForEach(LogTable, Sub(Entry As LogContent)

                                                              If (Entry.ReturnCode = 200) AndAlso (Entry.Action = GetOrCheck.GetFile) Then

                                                                  If (Entry.ReferingPage.Length > 0) Then

                                                                      SyncLock (IPTable)

                                                                          Dim x As Integer = Array.FindIndex(IPTable, 0, IPTableIndex + 1, Function(c) c.IP = Entry.IPAddress)

                                                                          If x = -1 Then

                                                                              IPTable(IPTableIndex).IP = Entry.IPAddress
                                                                              IPTable(IPTableIndex).IPi = GetNumericIP(Entry.IPAddress)
                                                                              IPTable(IPTableIndex).Count = 1
                                                                              IPTableIndex += 1

                                                                          Else

                                                                              IPTable(x).Count += 1

                                                                          End If

                                                                      End SyncLock


                                                                  End If

                                                              End If

                                                          End Sub)


        If IPTable(IPTableIndex).IP = String.Empty Then IPTableIndex -= 1
        ReDim Preserve IPTable(IPTableIndex)

        IPTable = IPTable.OrderByDescending(Function(c) c.Count).ThenBy(Function(c) c.IPi).ThenBy(Function(c) c.IP).ToArray

    End Sub

    Private Sub CreateListOfProgramsAndTheirFileSizes()

        ' The following creates an array of all downloaded files + their maximum file size 

        Try

            Dim workingListOFPrograms = (From c In LogTable Select c.Filename, c.DataTransferred, c.ReturnCode, c.Action).Where(Function(c) (c.ReturnCode = "200" AndAlso c.Action = GetOrCheck.GetFile)).OrderBy(Function(c) c.Filename).ThenByDescending(Function(c) c.DataTransferred).Distinct().ToList
            Dim WorkingListOFProgramsAndThereMaximumFileSize = (From c In workingListOFPrograms Select c.Filename, c.DataTransferred).ToList

            Dim x As Integer = 0
            Dim LastEntry As String = String.Empty

            ReDim ListOFProgramsAndThereMaximumFileSize(WorkingListOFProgramsAndThereMaximumFileSize.Count)

            For Each entry In WorkingListOFProgramsAndThereMaximumFileSize

                If entry.Filename <> LastEntry Then

                    ListOFProgramsAndThereMaximumFileSize(x).FileName = entry.Filename
                    ListOFProgramsAndThereMaximumFileSize(x).FileSize = entry.DataTransferred
                    ListOFProgramsAndThereMaximumFileSize(x).FileSizeToBeConsideredAsACompletedDownload = entry.DataTransferred * 0.99 'allow for a 1% variation for when a program is versioned and its file size marginally changes
                    LastEntry = entry.Filename
                    x += 1

                End If

            Next

            ReDim Preserve ListOFProgramsAndThereMaximumFileSize(x - 1)

            workingListOFPrograms = Nothing
            WorkingListOFProgramsAndThereMaximumFileSize = Nothing

        Catch ex As Exception

            'Debug.WriteLine(ex.ToString)

        End Try

    End Sub

    Private Function GetEquivalentDownloads(ByVal filename As String, ByVal RunningTotalOfBytesTransferred As Long) As Integer

        Dim ReturnValue As Integer = 0

        Dim x As Integer = Array.FindIndex(ListOFProgramsAndThereMaximumFileSize, Function(c) (c.FileName = filename))

        If x > -1 Then
            ReturnValue = Math.Floor(RunningTotalOfBytesTransferred / ListOFProgramsAndThereMaximumFileSize(x).FileSizeToBeConsideredAsACompletedDownload)
        End If

        Return ReturnValue

    End Function

    Private Function GetMaxFileSize(ByVal filename As String) As Integer

        Return ListOFProgramsAndThereMaximumFileSize(Array.FindIndex(ListOFProgramsAndThereMaximumFileSize, Function(c) (c.FileName = filename))).FileSize

    End Function

    Private Function GetFileSizeToBeConsideredAsACompletedDownload(ByVal filename As String) As Integer

        Return ListOFProgramsAndThereMaximumFileSize(Array.FindIndex(ListOFProgramsAndThereMaximumFileSize, Function(c) (c.FileName = filename))).FileSizeToBeConsideredAsACompletedDownload

    End Function

    Private Sub CreateGraphsAndReports()

        CreateChartOfDownloadsByDate()
        CreateChartOfDataTransferredByDate()
        CreateChartOfDownloadsByFileName()
        CreateChartOfDataTransferred()
        CreateChartOfCompletedDownloadsByCountry()
        CreateListOfReferrers()
        CreateListOfIPs()

        If gZoom Then
            If gZoomName.StartsWith("Chart") Then
                RestoreOriginalChartFromZoom()
            Else
                RestoreOriginalRPTPanelFromZoom()
            End If
        End If

    End Sub

    Private Sub CreateChartOfDownloadsByDate()

        'Set look of Graph ********************************************************************************************************************

        With Me.ChartOfDownloadsByDate

            .ChartAreas(0).AxisX.MajorGrid.Enabled = False
            .ChartAreas(0).AxisY.MajorGrid.Enabled = False

            .ChartAreas(0).AxisX.LabelStyle.Format = "MM-dd"

            .Legends(0).Enabled = True
            .Legends(0).Docking = Docking.Right
            .Legends(0).Alignment = System.Drawing.StringAlignment.Center

            .Series.Clear()

            .Titles(0).Text = "Completed downloads by date"

        End With

        'Populate Graph ***********************************************************************************************************************

        Dim x As Integer = 0
        Dim TotalDownloaded As Integer = 0
        Dim lastfilename As String = String.Empty

        FileNameByDateTable = FileNameByDateTable.OrderBy(Function(c) c.FileName).ToArray

        With Me.ChartOfDownloadsByDate

            For Each entry As FileNameByDateTableContent In FileNameByDateTable

                If entry.FileName = String.Empty Then
                Else

                    If entry.FileName = lastfilename Then
                        .Series(x - 1).Points.AddXY(entry.DownloadDate.ToOADate, entry.DownloadCount)
                    Else
                        .Series.Add(New System.Windows.Forms.DataVisualization.Charting.Series())
                        .Series(x).Name = entry.FileName
                        .Series(x).Points.AddXY(entry.DownloadDate.ToOADate, entry.DownloadCount)
                        .Series(x).XValueType = ChartValueType.DateTime
                        x += 1
                    End If

                    TotalDownloaded += entry.DownloadCount

                    lastfilename = entry.FileName

                End If

            Next

            If gEquatePartialDownloads Then
                .Titles(0).Text = "Fully and adjusted completed downloads by date" & vbCrLf & "( " & Format(TotalDownloaded, "N0") & " in total )"
            Else
                .Titles(0).Text = "Completed downloads by date" & vbCrLf & "( " & Format(TotalDownloaded, "N0") & " in total )"
            End If

        End With

    End Sub

    Private Sub CreateChartOfDataTransferredByDate()

        'Set look of Graph *******************************************************************************************************************

        With Me.ChartOfDataTransferredByDate

            .ChartAreas(0).AxisX.MajorGrid.Enabled = False
            .ChartAreas(0).AxisY.MajorGrid.Enabled = False

            .ChartAreas(0).AxisX.LabelStyle.Format = "MM-dd"

            .Legends(0).Enabled = True
            .Legends(0).Docking = Docking.Right
            .Legends(0).Alignment = System.Drawing.StringAlignment.Center

            .Series.Clear()

            .Titles(0).Text = "Data transferred by date"

        End With

        'Populate Graph **********************************************************************************************************************

        Const OneGigaByte As Integer = 1073741824

        Dim x As Integer = 0
        Dim lastfilename As String = String.Empty

        Dim TotalDataTransferred As Long = 0

        FileNameByDateTable = FileNameByDateTable.OrderBy(Function(c) c.FileName).ToArray

        With Me.ChartOfDataTransferredByDate

            For Each entry As FileNameByDateTableContent In FileNameByDateTable

                If entry.FileName = String.Empty Then
                Else

                    If entry.FileName = lastfilename Then
                        .Series(x - 1).Points.AddXY(entry.DownloadDate.ToOADate, entry.DataTransferred / OneGigaByte)
                    Else
                        .Series.Add(New System.Windows.Forms.DataVisualization.Charting.Series())
                        .Series(x).Name = entry.FileName
                        .Series(x).Points.AddXY(entry.DownloadDate.ToOADate, entry.DataTransferred / OneGigaByte)
                        .Series(x).XValueType = ChartValueType.DateTime
                        x += 1
                    End If

                    lastfilename = entry.FileName

                    TotalDataTransferred += entry.DataTransferred

                End If

            Next

            .Titles(0).Text = "Data transferred by date" & vbCrLf & "( " & Format(TotalDataTransferred / OneGigaByte, "N2") & " GBs in total )"

        End With

    End Sub
    Private Sub CreateChartOfDownloadsByFileName()

        'Set look of Graph ********************************************************************************************************************

        With Me.ChartOfDownloadsByFileName

            .ChartAreas(0).AxisX.MajorGrid.Enabled = False
            .ChartAreas(0).AxisY.MajorGrid.Enabled = False

            .ChartAreas(0).AxisX.LabelStyle.Interval = 1

            .PaletteCustomColors = New Color() {Color.Blue, Color.LightBlue, Color.LightCyan}

            .Series(0).ChartType = SeriesChartType.StackedBar
            .Series(1).ChartType = SeriesChartType.StackedBar
            .Series(2).ChartType = SeriesChartType.StackedBar

            .Titles(0).Text = "Logged activities"

        End With

        'Populate Graph **********************************************************************************************************************

        Dim TotalFull As Integer = 0
        Dim TotalEquivalent As Integer = 0
        Dim TotalPartial As Integer = 0
        Dim TotalChecks As Integer = 0

        TotalReportTable = TotalReportTable.OrderByDescending(Function(c) c.FileName).ToArray  ' Sort Reporting Table

        With Me.ChartOfDownloadsByFileName

            .Legends(0).TextWrapThreshold = 300

            .Series(0).IsVisibleInLegend = True
            .Series(1).IsVisibleInLegend = True

            For Each entry As TotalReportContent In TotalReportTable

                If entry.FileName = String.Empty Then
                Else

                    .Series(0).Points.AddXY(entry.FileName, entry.CompletedDownloadCount)

                    TotalFull += entry.CompletedDownloadCount

                    If gEquatePartialDownloads Then
                        .Series(1).Points.AddXY(entry.FileName, entry.CompletedDownloadsBasedOnEquivalentPartialDownloadCount)
                        TotalEquivalent += entry.CompletedDownloadsBasedOnEquivalentPartialDownloadCount
                    Else
                        .Series(1).Points.AddXY(entry.FileName, entry.PartialDownloadCount)
                        TotalPartial += entry.PartialDownloadCount
                    End If

                    If gShowHeaderChecks Then
                        .Series(2).Points.AddXY(entry.FileName, entry.CheckCount)
                        TotalChecks += entry.CheckCount
                    End If

                End If

            Next


            If gEquatePartialDownloads Then
                .Series(0).Name = "Fully completed downloads " & Format(TotalFull, "N0")
                .Series(1).Name = "Adjusted completed downloads " & Format(TotalEquivalent, "N0")
            Else
                .Series(0).Name = "Completed downloads " & Format(TotalFull, "N0")
                .Series(1).Name = "Partial downloads " & Format(TotalPartial, "N0")
            End If

            If gShowHeaderChecks Then
                .Series(2).Name = "Header checks " & Format(TotalChecks, "N0")
                .Series(2).IsVisibleInLegend = True
            Else
                .Series(2).IsVisibleInLegend = False
            End If

        End With

        Dim TotalShown As Integer = TotalFull

        If gEquatePartialDownloads Then
            TotalShown += TotalEquivalent
        Else
            TotalShown += TotalPartial
        End If

        If gShowHeaderChecks Then
            TotalShown += TotalChecks
        End If

        If TotalShown = 0 Then

            Me.ChartOfDownloadsByFileName.Titles(0).Text &= vbCrLf & "(none)"

            For Each series As Series In Me.ChartOfDownloadsByFileName.Series
                series.IsVisibleInLegend = False
            Next

        End If

    End Sub
    Private Sub CreateChartOfDataTransferred()

        'Set look of Graph *******************************************************************************************************************

        With Me.ChartOfDataTransferred

            .ChartAreas(0).AxisX.MajorGrid.Enabled = False
            .ChartAreas(0).AxisY.MajorGrid.Enabled = False

            .DataManipulator.Sort(PointSortOrder.Descending, .Series(0))

            .Series(0)("PieLabelStyle") = "Disabled"

            .Series(0).LegendText = "#VALX (#PERCENT{P1})"
            ' also see Sub ChartOfDataTransferred_CustomizeLegend below - which removes the space before the percent sign in the legend

            .Legends(0).TextWrapThreshold = 200
            .Legends(0).Enabled = True
            .Legends(0).Docking = Docking.Right
            .Legends(0).Alignment = System.Drawing.StringAlignment.Center

            .Series(0).BorderWidth = 1
            .Series(0).BorderColor = System.Drawing.Color.FromArgb(26, 59, 105)

            .Titles(0).Text = "Data transferred"

        End With

        'Populate Graph *********************************************************************************************************************

        Const OneGigaByte As Integer = 1073741824

        Dim TotalDataTransferred As Long = 0

        TotalReportTable = TotalReportTable.OrderBy(Function(c) c.FileName).ToArray  ' Sort Reporting Table

        With Me.ChartOfDataTransferred

            For Each entry As TotalReportContent In TotalReportTable

                If entry.FileName = String.Empty Then
                Else

                    .Series(0).Points.AddXY(entry.FileName, entry.DataTransferred / OneGigaByte)

                    TotalDataTransferred += entry.DataTransferred

                End If

            Next

            For Each IndividualSeries As Series In .Series
                IndividualSeries.IsVisibleInLegend = True
            Next

            .Titles(0).Text = "Data transferred" & vbCrLf & "( " & Format(TotalDataTransferred / OneGigaByte, "N2") & " GBs in total )"

        End With

    End Sub

    Private Sub CreateChartOfCompletedDownloadsByCountry()

        'Set look of Graph *******************************************************************************************************************

        With Me.ChartOfCompletedDownloadsByCountry

            .ChartAreas(0).AxisX.MajorGrid.Enabled = False
            .ChartAreas(0).AxisY.MajorGrid.Enabled = False

            .DataManipulator.Sort(PointSortOrder.Descending, .Series(0))

            ' also see Sub ChartOfDataTransferred_CustomizeLegend below - which removes the space before the percent sign in the legend

            .Legends.Clear()             ' testing here
            .Legends.Add(New Legend)
            .Legends(0).Position.Auto = True

            .Legends(0).TextWrapThreshold = 200
            .Legends(0).Enabled = True
            .Legends(0).Docking = Docking.Right
            .Legends(0).Alignment = System.Drawing.StringAlignment.Center

            .Series(0)("PieLabelStyle") = "Outside" ' "Disabled"
            .Series(0).LegendText = "#VALX (#PERCENT{P1})"
            .Series(0).BorderWidth = 1
            .Series(0).BorderColor = System.Drawing.Color.FromArgb(26, 59, 105)

            If gEquatePartialDownloads Then
                .Titles(0).Text = "Fully and adjusted completed downloads by country"
            Else
                .Titles(0).Text = "Completed downloads by country"
            End If


            For Each IndividualSeries As Series In .Series
                IndividualSeries.IsVisibleInLegend = True
            Next

        End With

        'Populate Graph *********************************************************************************************************************

        Dim TotalCompletedDownloads As Integer = 0

        With Me.ChartOfCompletedDownloadsByCountry

            For Each entry As Top10CountriesContent In Top10Countries

                .Series(0).Points.AddXY(entry.Country, entry.TotalDownloads)
                TotalCompletedDownloads += entry.TotalDownloads

            Next

            '.DataManipulator.Sort(PointSortOrder.Descending, Me.ChartOfCompletedDownloadsByCountry.Series(0))

            If gEquatePartialDownloads Then
                .Titles(0).Text = "Fully and adjusted completed downloads by country" & vbCrLf & "( " & TotalCompletedDownloads.ToString("N0") & " in total )"
            Else
                .Titles(0).Text = "Completed downloads by country" & vbCrLf & "( " & TotalCompletedDownloads.ToString("N0") & " in total )"
            End If


        End With

    End Sub
    Private Sub CreateListOfReferrers()

        Dim TextToAdd As New StringBuilder
        TextToAdd.Clear()

        If ReferrerTable.Count = 0 Then

            TextToAdd.Append("{\rtf1\ansi")
            TextToAdd.Append("\pard\qc Referrals  \par")
            TextToAdd.Append("\pard\qc (none)     \par")
            TextToAdd.Append("}")

        Else

            TextToAdd.Append("{\rtf1\ansi")
            TextToAdd.Append("\pard\qc Referrals  \par")
            TextToAdd.Append("\pard    \par")
            TextToAdd.Append("\pard            Visits" & vbTab & "Referrer\par")

            Dim padding As String

            Dim NoReferrals As Integer = 0
            Dim TotalDownloads As Integer = 0

            For Each entry In ReferrerTable

                If entry.ReferingPage = "-" Then

                    NoReferrals = entry.Count

                Else

                    'hack to right align number 
                    padding = StrDup(Math.Max((9 - entry.Count.ToString("N0").Length), 0) * 2, " ")
                    TextToAdd.Append("\pard  " & padding & entry.Count.ToString("N0") & vbTab & entry.ReferingPage & "\par")

                End If

                TotalDownloads += entry.Count

            Next

            padding = StrDup(Math.Max((9 - NoReferrals.ToString("N0").Length), 0) * 2, " ")
            TextToAdd.Append("\pard  " & padding & "\ul " & NoReferrals.ToString("N0") & "\ul0 " & vbTab & "\ul \i (no referrals) \ul0 \i0 \par")
            TextToAdd.Append("\pard    \par")
            padding = StrDup(Math.Max((9 - TotalDownloads.ToString("N0").Length), 0) * 2, " ")
            TextToAdd.Append("\pard  " & padding & TotalDownloads.ToString("N0") & vbTab & "Total \par")
            TextToAdd.Append("}")

        End If

        rtbReferrers.Rtf = TextToAdd.ToString

    End Sub
    Private Sub CreateListOfIPs()

        Dim TextToAdd As New StringBuilder
        TextToAdd.Clear()

        If IPTable.Count = 0 Then

            TextToAdd.Append("{\rtf1\ansi")
            TextToAdd.Append("\pard\qc IP Addresses  \par")
            TextToAdd.Append("\pard\qc (none)     \par")
            TextToAdd.Append("}")

        Else

            TextToAdd.Append("{\rtf1\ansi")
            TextToAdd.Append("\pard\qc IP Addresses \par")
            TextToAdd.Append("\pard\qc (Total Unique $$$) \par")
            TextToAdd.Append("\pard    \par")
            TextToAdd.Append("\pard           Visits" & vbTab & "IP Addresses\par")

            Dim padding As String

            Dim TotalDownloads As Integer = 0

            For Each entry In IPTable

                'hack to right align number 
                padding = StrDup(Math.Max((9 - entry.Count.ToString.Length), 0) * 2, " ")
                TextToAdd.Append("\pard  " & padding & entry.Count.ToString("N0") & vbTab & "{\field{\*\fldinst HYPERLINK " & entry.IP & "}{\fldrslt " & entry.IP & "}}" & " \par")

                TotalDownloads += entry.Count

            Next

            TextToAdd.Append("\pard            \ul          \ul0 " & vbTab & "\ul                           \ul0 \i0 \par")

            padding = StrDup(Math.Max((9 - TotalDownloads.ToString("N0").Length), 0) * 2, " ")
            TextToAdd.Append("\pard  " & padding & TotalDownloads.ToString("N0") & vbTab & "Total \super(*)\nosupersub \par")
            TextToAdd.Append("\pard    \par")
            TextToAdd.Append("\pard  " & padding & vbTab & vbTab & "\super (*)\nosupersub  " & IPTable.Count.ToString("N0") & " are unique \par")
            TextToAdd.Append("\pard    \par")
            TextToAdd.Append("}")

            TextToAdd.Replace("$$$", IPTable.Count.ToString("N0"))

        End If

        rtbIP.Rtf = TextToAdd.ToString

    End Sub

    Private Sub ChartOfDataTransferred_CustomizeLegend(sender As Object, e As CustomizeLegendEventArgs) Handles ChartOfDataTransferred.CustomizeLegend

        For Each li As LegendItem In e.LegendItems
            li.Cells(1).Text = li.Cells(1).Text.Replace(" %", "%")
        Next

    End Sub

    Private Sub RefreshStatusBar()

        Dim ws As String

        If My.Settings.LastCheck = Nothing Then
            ws = "Never updated.  "
        Else
            ws = "Last updated " & My.Settings.LastCheck.ToString("yyyy/MM/dd h:mm:ss tt").ToLower & ".  "
        End If

        Dim lStartDate, lEndDate As Date
        Dim lAllDates As Boolean
        SetStartAndEndDates(lStartDate, lEndDate, lAllDates)

        If lAllDates Then
            ws &= "Reporting all dates, "
        Else
            ws &= "Reporting from " & lStartDate.ToString("yyyy/MM/dd") & " to " & lEndDate.ToString("yyyy/MM/dd") & ", "
        End If

        If gIgnore400Requests Then ws &= "ignoring 400 series return codes, "

        If Not gShowHeaderChecks Then ws &= "ignoring header checks, "

        If gEquatePartialDownloads Then ws &= "showing fully and adjusted completed downloads, "

        Select Case gIncludeExcludeFlag
            Case = "All files"
                ws &= "including all files."
            Case = "Include specific files only"
                ws &= "including only" & PunctuateAStringOfThings(My.Settings.FilesToInclude)
            Case = "Exclude specific files"
                ws &= "excluding" & PunctuateAStringOfThings(My.Settings.FilesToExclude)
        End Select

        Me.ToolStripStatusLabel.Text = ws

        Me.Refresh()

    End Sub

    Private Function PunctuateAStringOfThings(ByVal input As String) As String

        Dim ws As String
        Dim CommaCount As Integer
        Dim LastCommaPosition As Integer

        ws = input.Trim.Replace(",", " ").Replace("  ", " ").Replace(" ", ", ")
        CommaCount = ws.Count(Function(c) c = ",")

        LastCommaPosition = ws.LastIndexOf(",")

        If LastCommaPosition = -1 Then
        Else
            If CommaCount > 1 Then
                ws = ws.Remove(LastCommaPosition) & ", and" & ws.Remove(0, LastCommaPosition + 1)
            Else
                ws = ws.Remove(LastCommaPosition) & " and" & ws.Remove(0, LastCommaPosition + 1)
            End If
        End If

        If CommaCount > 1 Then
            ws = ": " & ws & "."
        Else
            ws = " " & ws & "."
        End If

        Return ws

    End Function

    Friend WithEvents WorkingChart As DataVisualization.Charting.Chart
    Private Sub ZoomChart_Click(sender As Object, e As EventArgs) Handles ChartOfDownloadsByDate.DoubleClick, ChartOfDataTransferred.DoubleClick, ChartOfDataTransferredByDate.DoubleClick, ChartOfDownloadsByFileName.DoubleClick, ChartOfCompletedDownloadsByCountry.DoubleClick, ChartZoom.DoubleClick

        gZoom = Not gZoom

        If gZoom Then

            gZoomName = sender.name

            Dim mystream = New System.IO.MemoryStream()
            sender.Serializer.Save(mystream)
            ChartZoom.Serializer.Load(mystream)

            TableLayoutPanel1.Visible = False

            ChartZoom.Location = TableLayoutPanel1.Location
            ChartZoom.Size = TableLayoutPanel1.Size
            ChartZoom.Visible = True
            ChartZoom.BringToFront()

        Else

            TableLayoutPanel1.Visible = True
            ChartZoom.Visible = False

            TableLayoutPanel1.BringToFront()

        End If

    End Sub

    Private Sub Zoom2_Click(sender As Object, e As EventArgs) Handles rtbReferrers.DoubleClick, rtbZoom.DoubleClick, rtbIP.DoubleClick

        gZoom = Not gZoom

        If gZoom Then

            gZoomName = sender.name

            TableLayoutPanel1.Visible = False

            rtbZoom.Rtf = sender.rtf
            rtbZoom.Location = TableLayoutPanel1.Location
            rtbZoom.Size = TableLayoutPanel1.Size
            rtbZoom.Visible = True
            rtbZoom.BringToFront()

        Else

            RestoreOriginalRPTPanelFromZoom()

        End If

    End Sub

    Private Sub frmMain_Resize(sender As Object, e As EventArgs) Handles Me.Resize

        If gZoom Then

            If gZoomName.StartsWith("Chart") Then
                ChartZoom.Size = Me.TableLayoutPanel1.Size
            Else
                rtbZoom.Size = Me.TableLayoutPanel1.Size

            End If
        End If
    End Sub


    Private Sub RestoreOriginalChartFromZoom()

        Dim mystream = New System.IO.MemoryStream()

        Select Case gZoomName

            Case Is = "ChartOfDownloadsByDate"
                ChartOfDownloadsByDate.Serializer.Save(mystream)

            Case Is = "ChartOfDataTransferredByDate"
                ChartOfDataTransferredByDate.Serializer.Save(mystream)

            Case Is = "ChartOfDownloadsByType"
                ChartOfDownloadsByFileName.Serializer.Save(mystream)

            Case Is = "ChartOfDataTransferred"
                ChartOfDataTransferred.Serializer.Save(mystream)

            Case Is = "ChartOfCompletedDownloadsByCountry"
                ChartOfCompletedDownloadsByCountry.Serializer.Save(mystream)

        End Select

        ChartZoom.Serializer.Load(mystream)

        ChartZoom.Size = TableLayoutPanel1.Size

    End Sub

    Private Sub RestoreOriginalRPTPanelFromZoom()

        TableLayoutPanel1.Visible = True
        rtbZoom.Visible = False

        TableLayoutPanel1.BringToFront()

    End Sub

    Private Sub rtbTop20Referrers_MouseEnter(sender As Object, e As EventArgs) Handles rtbReferrers.MouseEnter, rtbZoom.MouseEnter, rtbZoom.MouseHover, rtbReferrers.MouseHover, rtbIP.MouseEnter, rtbIP.MouseHover

        sender.ScrollBars = RichTextBoxScrollBars.Both

        If sender.name = "rtbIP" Then
            rtbReferrers.ScrollBars = RichTextBoxScrollBars.None
        ElseIf sender.name = "rtbReferrers" Then
            rtbIP.ScrollBars = RichTextBoxScrollBars.None
        End If

    End Sub

    Private Sub rtbTop20Referrers_MouseLeave(sender As Object, e As EventArgs) Handles rtbReferrers.MouseLeave, rtbZoom.MouseLeave, StatusStrip.MouseEnter, rtbIP.MouseLeave, Me.MouseEnter, Me.MouseLeave,
        ChartOfCompletedDownloadsByCountry.MouseEnter, ChartOfDataTransferred.MouseEnter, ChartOfDataTransferredByDate.MouseEnter, ChartOfDownloadsByDate.MouseEnter, ChartOfDownloadsByFileName.MouseEnter

        rtbReferrers.ScrollBars = RichTextBoxScrollBars.None
        rtbIP.ScrollBars = RichTextBoxScrollBars.None

    End Sub

    Private Sub AboutToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AboutToolStripMenuItem.Click

        Dim frmAbout As New frmAbout
        frmAbout.ShowDialog()

    End Sub

    Private Sub rtbTop20Referrers_LinkClicked(sender As Object, e As LinkClickedEventArgs) Handles rtbReferrers.LinkClicked, rtbZoom.LinkClicked

        System.Diagnostics.Process.Start(e.LinkText)

    End Sub


#Region "manage copy function to rich text box"

    Private Sub AddCopyFunctionToRichTextBoxReports()

        Dim ContextMenu = New System.Windows.Forms.ContextMenu()
        Dim MenuItem As New MenuItem("&Copy")

        AddHandler(MenuItem.Click), AddressOf CopyAction

        ContextMenu.MenuItems.Add(MenuItem)
        rtbIP.ContextMenu = ContextMenu
        rtbReferrers.ContextMenu = ContextMenu
        rtbZoom.ContextMenu = ContextMenu

    End Sub

    Private rtbHoldingBucket As New RichTextBox

    Private Sub richTextBox1_MouseUp(sender As Object, e As MouseEventArgs) Handles rtbIP.MouseUp, rtbReferrers.MouseUp, rtbZoom.MouseUp

        If e.Button = System.Windows.Forms.MouseButtons.Right Then

            rtbHoldingBucket = sender ' put entire rtb into a holding bucket so it can be pulled out later if the copy button is clicked
            rtbIP.ContextMenu.Show(sender, e.Location)

        End If

    End Sub

    Private Sub CopyAction(sender As Object, e As EventArgs)

        On Error Resume Next

        Dim rtb As New RichTextBox
        rtb.Rtf = rtbHoldingBucket.SelectedRtf.ToString 'copy only the selected text to a temporary rtb

        My.Computer.Clipboard.SetText(rtb.Text.Trim) ' copy the selected text as text into the clipboard

        rtb.Dispose()

    End Sub

    Private Sub OnlineHelpToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles OnlineHelpToolStripMenuItem.Click, WebsiteToolStripMenuItem.Click

        System.Diagnostics.Process.Start(sender.tag)

    End Sub

    Private Sub CheckForUpdatesToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CheckForUpdatesToolStripMenuItem.Click

        Cursor.Current = Cursors.WaitCursor

        CheckForUpdatesToolStripMenuItem.Enabled = False

        Try

            Dim NewVersionFound As Boolean = False
            Dim ProblemWithCheckingProcess As Boolean = False
            Dim ProblemWithFileSize As Boolean = False
            Dim ProblemWithFileLicense As Boolean = False

            Dim BetaFound As Boolean = False

            Const msgboxtitle As String = "Reporting for Rackspace - Version Check"

            CheckWebsiteForNewVersion(NewVersionFound, BetaFound, ProblemWithCheckingProcess)

            If ProblemWithCheckingProcess Then


                Call MsgBox("Problems were encountered trying to check for a more current version, please try again later.", MsgBoxStyle.Information, msgboxtitle)

            Else

                If NewVersionFound Then

                    If MsgBox("A newer version of Reporting for Rackspace is now available." & vbCrLf &
                              "Would you like to visit the Reporting for Rackspace download webpage now?", MsgBoxStyle.Question Or MsgBoxStyle.YesNo, msgboxtitle) = MsgBoxResult.Yes Then
                        System.Diagnostics.Process.Start("http://www.rlatour.com/r4r/download.html")
                    End If

                Else

                    If BetaFound Then
                        Call MsgBox("You are running a beta version of the next release of Reporting for Rackspace.", MsgBoxStyle.Information, msgboxtitle)
                    Else
                        Call MsgBox("You are running the most current version of Reporting for Rackspace", MsgBoxStyle.Information, msgboxtitle)
                    End If

                End If

            End If

        Catch ex As Exception

        End Try

        Cursor.Current = Cursors.Default

    End Sub


    Friend Shared Sub CheckWebsiteForNewVersion(ByRef NewVersionFound As Boolean, ByRef BetaVersionFound As Boolean, ByRef ProblemWithCheckingProcess As Boolean)

        NewVersionFound = False
        BetaVersionFound = False
        ProblemWithCheckingProcess = False

        Dim CurrentVersionRunning As String = Microsoft.VisualBasic.Left(Application.ProductVersion, 8).Trim

        Try

            Dim CurrentVersionFileContents As String = String.Empty

            Dim myWebClient As New System.Net.WebClient

            Dim file As New System.IO.StreamReader(myWebClient.OpenRead(gCurrentVersionURL))
            CurrentVersionFileContents = file.ReadToEnd()
            file.Close()
            file.Dispose()
            myWebClient.Dispose()

            Dim Entries() As String = Split(CurrentVersionFileContents, vbCrLf)

            Dim CurrentVersionAccordingToWebsite As String = Entries(0).Trim

            If CurrentVersionAccordingToWebsite.Length < 7 Then
                ProblemWithCheckingProcess = True
                Exit Try
            End If

            If CurrentVersionAccordingToWebsite = CurrentVersionRunning Then

                NewVersionFound = False
                Exit Try

            Else

                Dim cv() As String = CurrentVersionRunning.Split(".")
                Dim wv() As String = CurrentVersionAccordingToWebsite.Split(".")

                ReDim Preserve cv(3)
                ReDim Preserve wv(3)
                Dim cvi(3) As Integer
                Dim wvi(3) As Integer

                For x As Integer = 0 To 3

                    If cv(x) Is Nothing Then
                        cvi(x) = 0
                    Else
                        cvi(x) = CType(cv(x), Integer)
                    End If

                    If wv(x) Is Nothing Then
                        wvi(x) = 0
                    Else
                        wvi(x) = CType(wv(x), Integer)
                    End If

                Next

                Dim cviExpanded As Integer
                Dim wviExpanded As Integer

                cviExpanded = 10000000
                wviExpanded = 10000000

                cviExpanded += cvi(0) * 100000
                wviExpanded += wvi(0) * 100000


                cviExpanded += cvi(1) * 1000
                wviExpanded += wvi(1) * 1000

                cviExpanded += cvi(2) * 10
                wviExpanded += wvi(2) * 10

                cviExpanded += cvi(3)
                wviExpanded += wvi(3)

                NewVersionFound = (wviExpanded > cviExpanded)
                BetaVersionFound = (cviExpanded > wviExpanded)

            End If

            Entries = Nothing

        Catch ex As Exception
            ProblemWithCheckingProcess = True
        End Try

    End Sub


#End Region

End Class
