Imports System
Imports System.Runtime.CompilerServices
Imports System.Runtime.InteropServices
Imports Network_Tools.YT

Friend Class PrecisionTimer
    ' Methods
    Public Sub Create(ByVal dueTime As UInt32, ByVal period As UInt32, ByVal callback As TimerDelegate)
        If Not Me._Enabled Then
            Me.TimerCallback = callback
            Dim flag As Boolean = PrecisionTimer.CreateTimerQueueTimer(Me.Handle, IntPtr.Zero, Me.TimerCallback, IntPtr.Zero, dueTime, period, 0)
            If Not flag Then
                Me.ThrowNewException("CreateTimerQueueTimer")
            End If
            Me._Enabled = flag
        End If
    End Sub

    <DllImport("kernel32.dll")> _
    Private Shared Function CreateTimerQueueTimer(ByRef handle As IntPtr, ByVal queue As IntPtr, ByVal callback As TimerDelegate, ByVal state As IntPtr, ByVal dueTime As UInt32, ByVal period As UInt32, ByVal flags As UInt32) As Boolean
    End Function

    Public Sub Delete()
        If Me._Enabled Then
            Dim flag As Boolean = PrecisionTimer.DeleteTimerQueueTimer(IntPtr.Zero, Me.Handle, IntPtr.Zero)
            If Not (flag OrElse (Marshal.GetLastWin32Error = &H3E5)) Then
            End If
            Me._Enabled = Not flag
        End If
    End Sub

    <DllImport("kernel32.dll")> _
    Private Shared Function DeleteTimerQueueTimer(ByVal queue As IntPtr, ByVal handle As IntPtr, ByVal callback As IntPtr) As Boolean
    End Function

    Public Sub Dispose()
        Me.Delete()
    End Sub

    Private Sub ThrowNewException(ByVal name As String)
        Throw New Exception(String.Format("{0} failed. Win32Error: {1}", name, Marshal.GetLastWin32Error))
    End Sub


    ' Properties
    Public ReadOnly Property Enabled As Boolean
        Get
            Return Me._Enabled
        End Get
    End Property


    ' Fields
    Private _Enabled As Boolean
    Private Handle As IntPtr
    Private TimerCallback As TimerDelegate

    ' Nested Types
    Public Delegate Sub TimerDelegate(ByVal r1 As IntPtr, ByVal r2 As Boolean)
End Class


