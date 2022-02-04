Imports System
Imports Network_Tools.YT

Public Class BitConverterLE
    ' Methods
    Public Shared Function GetBytes(ByVal value As UInt16) As Byte()
        Return New Byte() {CByte(value), CByte((value >> 8))}
    End Function

    Public Shared Function GetBytes(ByVal value As UInt32) As Byte()
        Return New Byte() {CByte(value), CByte((value >> 8)), CByte((value >> &H10)), CByte((value >> &H18))}
    End Function

    Public Shared Function GetBytes(ByVal value As UInt64) As Byte()
        Return New Byte() {CByte(value), CByte((value >> 8)), CByte((value >> &H10)), CByte((value >> &H18)), CByte((value >> &H20)), CByte((value >> 40)), CByte((value >> &H30)), CByte((value >> &H38))}
    End Function

End Class
