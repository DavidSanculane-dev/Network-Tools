Option Explicit on
Imports System
Imports Network_Tools.YT

Public Class BitConverterBE
    Public Shared Function GetBytes(ByVal value As UInt16) As Byte()
        Return New Byte() {CByte((value >> 8)), CByte(value)}
    End Function

    Public Shared Function GetBytes(ByVal value As UInt32) As Byte()
        Return New Byte() {CByte((value >> &H18)), CByte((value >> &H10)), CByte((value >> 8)), CByte(value)}
    End Function

    Public Shared Function GetBytes(ByVal value As UInt64) As Byte()
        Return New Byte() {CByte((value >> &H38)), CByte((value >> &H30)), CByte((value >> 40)), CByte((value >> &H20)), CByte((value >> &H18)), CByte((value >> &H10)), CByte((value >> 8)), CByte(value)}
    End Function

    Public Shared Function ToUInt16(ByVal value As Byte(), ByVal startIndex As Integer) As UInt16
        Return CUShort(((value(startIndex) << 8) Or value((startIndex + 1))))
    End Function

    Public Shared Function ToUInt32(ByVal value As Byte(), ByVal startIndex As Integer) As UInt32
        Return (((((value(startIndex) << &H18) Or (value((startIndex + 1)) << &H10)) Or (value((startIndex + 2)) << 8)) Or value((startIndex + 3))))
    End Function

    Public Shared Function ToUInt64(ByVal value As Byte(), ByVal startIndex As Integer) As UInt64
        Return CULng(((((((((value(startIndex) << &H38) Or (value((startIndex + 1)) << &H30)) Or (value((startIndex + 2)) << 40)) Or (value((startIndex + 3)) << &H20)) Or (value((startIndex + 4)) << &H18)) Or (value((startIndex + 5)) << &H10)) Or (value((startIndex + 6)) << 8)) Or value((startIndex + 7))))
    End Function

End Class