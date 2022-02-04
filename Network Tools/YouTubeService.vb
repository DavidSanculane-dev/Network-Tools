Imports System
Imports System.Collections.Generic
Imports System.Collections.Specialized
Imports System.IO
Imports System.Net
Imports System.Runtime.CompilerServices
Imports System.Text
Imports System.Text.RegularExpressions
Imports System.Web
Imports Network_Tools.YT
Class YouTubeService
    Public Shared Function Create(ByVal youTubeVideoUrl As String) As YouTubeService
        Dim service As New YouTubeService
        service.VideoUrl = youTubeVideoUrl
        service.videoId = HttpUtility.ParseQueryString(New Uri(service.VideoUrl).Query).Item("v")
        service.GetVideoInfo()
        service.GetVideoTitle()
        service.PopulateAvailableVideoFormatList()
        Return service
    End Function
    Private Function DownloadString(ByVal url As String) As String
        Dim response As HttpWebResponse = TryCast(WebRequest.Create(url).GetResponse, HttpWebResponse)
        Dim str As String = New StreamReader(response.GetResponseStream, Encoding.UTF8).ReadToEnd
        response.Close()
        Return str
    End Function
    Private Sub GetVideoInfo()
        Dim query As String = Me.DownloadString(String.Format("http://www.youtube.com/get_video_info?&video_id={0}&el=detailpage&ps=default&eurl=&gl=US&hl=en", Me.videoId))
        Me.videoInfoCollection = HttpUtility.ParseQueryString(query)
    End Sub
    Private Sub GetVideoTitle()
        Me.VideoTitle = Me.videoInfoCollection.Item("title")
        Me.VideoTitle = Regex.Replace(Me.VideoTitle, "[:\*\?""\<\>\|]", String.Empty)
        Me.VideoTitle = Me.VideoTitle.Replace("\", "-").Replace("/", "-").Trim
        If String.IsNullOrEmpty(Me.VideoTitle) Then
            Me.VideoTitle = Me.videoId
        End If
    End Sub
    Private Sub PopulateAvailableVideoFormatList()
        Dim input As String = Me.videoInfoCollection.Item("url_encoded_fmt_stream_map")
        If (input <> String.Empty) Then
            Dim ooo As New List(Of String)(Regex.Split(input, ","))
            ooo.ForEach(Function(format As String)
                            If Not String.IsNullOrEmpty(format.Trim) Then
                                Dim values As NameValueCollection = HttpUtility.ParseQueryString(format)
                                Dim str As String = values.Item("url")
                                Dim s As String = values.Item("itag")
                                Dim str3 As String = values.Item("sig")
                                Dim str4 As String = values.Item("fallback_host")
                                Dim formatCode As Byte = Byte.Parse(s)
                                Dim uri As New Uri(HttpUtility.UrlDecode(HttpUtility.UrlDecode(String.Format("{0}&fallback_host={1}&signature={2}", str, str4, str3))))
                                Me.availableVideoFormat.Add(New YouTubeVideoFile(uri.ToString, formatCode))
                            End If
                            Return format
                        End Function)
        End If
    End Sub
    Public VideoTitle As String
    Public VideoUrl As String
    Public availableVideoFormat As List(Of YouTubeVideoFile) = New List(Of YouTubeVideoFile)
    Public videoId As String
    Public videoInfoCollection As NameValueCollection
    Public Const VideoInfoPageUrl As String = "http://www.youtube.com/get_video_info?&video_id={0}&el=detailpage&ps=default&eurl=&gl=US&hl=en"
    Public Const VideoUrlsSeparator As String = ","
End Class


