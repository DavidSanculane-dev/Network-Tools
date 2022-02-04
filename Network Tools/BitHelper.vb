Imports System
Imports Network_Tools.YT

Public Class BitHelper
    Public Shared Function CopyBlock(ByVal bytes As Byte(), ByVal offset As Integer, ByVal length As Integer) As Byte()
        Dim srcOffset As Integer = (offset / 8)
        Dim num2 As Integer = (((offset + length) - 1) / 8)
        Dim num3 As Integer = (offset Mod 8)
        Dim num4 As Integer = (8 - num3)
        Dim dst As Byte() = New Byte(((length + 7) / 8) - 1) {}
        If (num3 = 0) Then
            Buffer.BlockCopy(bytes, srcOffset, dst, 0, dst.Length)
        Else
            Dim index As Integer = 0
            Do While (index < (num2 - srcOffset))
                dst(index) = CByte(((bytes((srcOffset + index)) << num3) Or (bytes(((srcOffset + index) + 1)) >> num4)))
                index += 1
            Loop
            If (index < dst.Length) Then
                dst(index) = CByte((bytes((srcOffset + index)) << num3))
            End If
        End If
        dst((dst.Length - 1)) = CByte((dst((dst.Length - 1)) And CByte((CInt(&HFF) << ((dst.Length * 8) - length)))))
        Return dst
    End Function

    Public Shared Function Read(ByRef x As UInt64, ByVal length As Integer) As Integer
        Dim num As Integer = CInt((x >> ((&H40 - length) And &H3F)))
        x = (x << (length And &H3F))
        Return num
    End Function

    Public Shared Function Read(ByVal bytes As Byte(), ByRef offset As Integer, ByVal length As Integer) As Integer
        Dim num As Integer = (offset / 8)
        Dim num2 As Integer = (((offset + length) - 1) / 8)
        Dim num3 As Integer = (offset Mod 8)
        Dim x As UInt64 = 0
        Dim i As Integer = 0
        Do While (i <= Math.Min((num2 - num), 7))
            x = (x Or (bytes((num + i)) << (&H38 - (i * 8))))
            i += 1
        Loop
        If (num3 <> 0) Then
            BitHelper.Read(x, num3)
        End If
        offset = (offset + length)
        Return BitHelper.Read(x, length)
    End Function

    Public Shared Sub Write(ByRef x As UInt64, ByVal length As Integer, ByVal value As Integer)
        Dim num As UInt64 = (CULng(1) >> (&H40 - length))
        x = ((x << (length And &H3F)) Or (value And num))
    End Sub

End Class
