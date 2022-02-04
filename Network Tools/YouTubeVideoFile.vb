Imports System
Imports System.Runtime.CompilerServices
Imports Network_Tools.YT
Class YouTubeVideoFile
    Private formatCode As Byte
    ' Methods
    Public Sub New(ByVal url As String, ByVal formatCode As Byte)
        Me.VideoUrlk__BackingField = url
        Me.formatCode = formatCode
    End Sub
    ' Properties
    Public Property ddd As Byte
        Get
            Return formatCode
        End Get
        Set(ByVal value As Byte)
            Try
                formatCode = value
            Catch ex As Exception

            End Try
            Select Case value
                Case 5, 6, &H22, &H23
                    Me.VideoFormatk__BackingField = YouTubeVideoType.Flash
                    Return
                Case 13, &H11, &H24
                    Me.VideoFormatk__BackingField = YouTubeVideoType.Mobile
                    Return
                Case &H12, &H25, &H26, &H16, &H52, &H54
                    Me.VideoFormatk__BackingField = YouTubeVideoType.MP4
                    Return
                Case &H2B, &H2D, &H2E
                    Me.VideoFormatk__BackingField = YouTubeVideoType.WebM
                    Return
            End Select
            Me.VideoFormatk__BackingField = YouTubeVideoType.Unknown
        End Set
    End Property
    Property VideoFormat As YouTubeVideoType
        Get
            Return Me.VideoFormatk__BackingField
        End Get
        Private Set(ByVal value As YouTubeVideoType)
            Me.VideoFormatk__BackingField = value
        End Set
    End Property
    Public Property VideoSize As Long
        Get
            Return Me.VideoSizek__BackingField
        End Get
        Set(ByVal value As Long)
            Me.VideoSizek__BackingField = value
        End Set
    End Property
    Public Property VideoUrl As String
        <CompilerGenerated()> _
        Get
            Return Me.VideoUrlk__BackingField
        End Get
        <CompilerGenerated()> _
        Set(ByVal value As String)
            Me.VideoUrlk__BackingField = value
        End Set
    End Property
    Private VideoFormatk__BackingField As YouTubeVideoType
    Private VideoSizek__BackingField As Long
    Private VideoUrlk__BackingField As String

End Class

