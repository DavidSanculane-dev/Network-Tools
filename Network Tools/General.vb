Imports System

Public Class General
    ' Methods
    Public Shared Sub CopyBytes(ByVal dst As Byte(), ByVal dstOffset As Integer, ByVal src As Byte())
        Buffer.BlockCopy(src, 0, dst, dstOffset, src.Length)
    End Sub
End Class
