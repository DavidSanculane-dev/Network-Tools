Imports System
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Runtime.InteropServices
Imports System.Net.NetworkInformation
Imports System.Threading
Imports System.Text
Imports Network_Tools.Wlan
Imports Network_Tools.WlanClient
Public Class WlanClient
    ' Methods
    Public Sub New()
        Try
            Wlan.ThrowIfError(Wlan.WlanOpenHandle(1, IntPtr.Zero, Me.negotiatedVersion, Me.clientHandle))
        Catch ex As Win32Exception
            MsgBox("There is no Wi-Fi adapter detected in your computer , the program will terminate...", MsgBoxStyle.Critical, "Error...")
            End
        End Try
        Try
            Dim prevSrc As WlanNotificationSource
            Me.wlanNotificationCallback = New WlanNotificationCallbackDelegate(AddressOf Me.OnWlanNotification)
            Wlan.ThrowIfError(Wlan.WlanRegisterNotification(Me.clientHandle, WlanNotificationSource.All, False, Me.wlanNotificationCallback, IntPtr.Zero, IntPtr.Zero, prevSrc))
        Catch
            Wlan.WlanCloseHandle(Me.clientHandle, IntPtr.Zero)
            Throw
        End Try
    End Sub
    Protected Overrides Sub Finalize()
        Try
            Wlan.WlanCloseHandle(Me.clientHandle, IntPtr.Zero)
        Finally
            MyBase.Finalize()
        End Try
    End Sub

    Public Function GetStringForReasonCode(ByVal reasonCode As WlanReasonCode) As String
        Dim sb As New StringBuilder(&H400)
        Wlan.ThrowIfError(Wlan.WlanReasonCodeToString(reasonCode, sb.Capacity, sb, IntPtr.Zero))
        Return sb.ToString
    End Function
    Private Sub OnWlanNotification(ByRef notifyData As WlanNotificationData, ByVal context As IntPtr)
        Try
            Dim connNotifyData As WlanConnectionNotificationData?
            Dim wlanIface As WlanInterface = IIf(Me.ifaces.ContainsKey(notifyData.interfaceGuid), Me.ifaces.Item(notifyData.interfaceGuid), Nothing)
            Select Case notifyData.notificationSource
                Case WlanNotificationSource.ACM
                    Select Case DirectCast(notifyData.notificationCode, WlanNotificationCodeAcm)
                        Case WlanNotificationCodeAcm.ScanFail
                            Try
                                Dim expectedSize As Integer = Marshal.SizeOf(GetType(WlanReasonCode))
                                If (notifyData.dataSize >= expectedSize) Then
                                    Dim reasonCode As WlanReasonCode = DirectCast(Marshal.ReadInt32(notifyData.dataPtr), WlanReasonCode)
                                    If (Not wlanIface Is Nothing) Then
                                        wlanIface.OnWlanReason(notifyData, reasonCode)
                                    End If
                                End If
                                GoTo Label_0183
                            Catch ex As ArgumentException
                                MsgBox("An error occured while trying to scan for available Wi-Fi networks", MsgBoxStyle.Exclamation, "Warning...")
                            End Try
                        Case WlanNotificationCodeAcm.ConnectionStart, WlanNotificationCodeAcm.ConnectionComplete, WlanNotificationCodeAcm.ConnectionAttemptFail, WlanNotificationCodeAcm.Disconnecting, WlanNotificationCodeAcm.Disconnected
                            connNotifyData = Me.ParseWlanConnectionNotification((notifyData))
                            If (connNotifyData.HasValue AndAlso (Not wlanIface Is Nothing)) Then
                                wlanIface.OnWlanConnection(notifyData, connNotifyData.Value)
                            End If
                            GoTo Label_0183
                    End Select
                    Exit Select
                Case WlanNotificationSource.MSM
                    Select Case notifyData.notificationCode
                        Case 1, 2, 3, 4, 5, 6, 9, 10, 11, 12, 13
                            connNotifyData = Me.ParseWlanConnectionNotification((notifyData))
                            If (connNotifyData.HasValue AndAlso (Not wlanIface Is Nothing)) Then
                                wlanIface.OnWlanConnection(notifyData, connNotifyData.Value)
                            End If
                            GoTo Label_0183
                        Case 7, 8
                            GoTo Label_0183
                    End Select
                    GoTo Label_0183
            End Select
Label_0183:
            If (Not wlanIface Is Nothing) Then
                wlanIface.OnWlanNotification(notifyData)
            End If
        Catch ex As KeyNotFoundException
        End Try
    End Sub

    Private Function ParseWlanConnectionNotification(ByRef notifyData As WlanNotificationData) As WlanConnectionNotificationData?
        Dim expectedSize As Integer = Marshal.SizeOf(GetType(WlanConnectionNotificationData))
        If (notifyData.dataSize < expectedSize) Then
            Return Nothing
        End If
        Dim connNotifyData As WlanConnectionNotificationData = DirectCast(Marshal.PtrToStructure(notifyData.dataPtr, GetType(WlanConnectionNotificationData)), WlanConnectionNotificationData)
        If (connNotifyData.wlanReasonCode = WlanReasonCode.Success) Then
            Dim profileXmlPtr As New IntPtr((notifyData.dataPtr.ToInt64 + Marshal.OffsetOf(GetType(WlanConnectionNotificationData), "profileXml").ToInt64))
            connNotifyData.profileXml = Marshal.PtrToStringUni(profileXmlPtr)
        End If
        Return New WlanConnectionNotificationData?(connNotifyData)
    End Function
    ' Properties
    Public ReadOnly Property Interfaces() As WlanInterface()
        Get
            Dim ifaceList As IntPtr
            Dim CS As WlanInterface()
            Wlan.ThrowIfError(Wlan.WlanEnumInterfaces(Me.clientHandle, IntPtr.Zero, ifaceList))
            Try
                Dim header As WlanInterfaceInfoListHeader = DirectCast(Marshal.PtrToStructure(ifaceList, GetType(WlanInterfaceInfoListHeader)), WlanInterfaceInfoListHeader)
                Dim listIterator As Long = (ifaceList.ToInt64 + Marshal.SizeOf(header))
                Dim interfacess As WlanInterface() = New WlanInterface(header.numberOfItems - 1) {}
                Dim currentIfaceGuids As New List(Of Guid)
                Dim i As Integer
                For i = 0 To header.numberOfItems - 1
                    Dim wlanIface As WlanInterface
                    Dim info As WlanInterfaceInfo = DirectCast(Marshal.PtrToStructure(New IntPtr(listIterator), GetType(WlanInterfaceInfo)), WlanInterfaceInfo)
                    listIterator = (listIterator + Marshal.SizeOf(info))
                    currentIfaceGuids.Add(info.interfaceGuid)
                    If Me.ifaces.ContainsKey(info.interfaceGuid) Then
                        wlanIface = Me.ifaces.Item(info.interfaceGuid)
                    Else
                        wlanIface = New WlanInterface(Me, info)
                    End If
                    interfacess(i) = wlanIface
                    Me.ifaces.Item(info.interfaceGuid) = wlanIface
                Next i
                Dim deadIfacesGuids As New Queue(Of Guid)
                Dim ifaceGuid As Guid
                For Each ifaceGuid In Me.ifaces.Keys
                    If Not currentIfaceGuids.Contains(ifaceGuid) Then
                        deadIfacesGuids.Enqueue(ifaceGuid)
                    End If
                Next
                Do While (deadIfacesGuids.Count <> 0)
                    Dim deadIfaceGuid As Guid = deadIfacesGuids.Dequeue
                    Me.ifaces.Remove(deadIfaceGuid)
                Loop
                CS = interfacess
            Finally
                Wlan.WlanFreeMemory(ifaceList)
            End Try
            Return CS
        End Get
    End Property
    ' Fields
    Private clientHandle As IntPtr
    Private ifaces As Dictionary(Of Guid, WlanInterface) = New Dictionary(Of Guid, WlanInterface)
    Private negotiatedVersion As UInt32
    Private wlanNotificationCallback As WlanNotificationCallbackDelegate
    ' Nested Types
    Public Class WlanInterface
        ' Events
        Public Event WlanConnectionNotification As WlanConnectionNotificationEventHandler
        Public Event WlanNotification As WlanNotificationEventHandler
        Public Event WlanReasonNotification As WlanReasonNotificationEventHandler
        ' Methods
        Friend Sub New(ByVal client As WlanClient, ByVal info As WlanInterfaceInfo)
            Me.client = client
            Me.info = info
        End Sub

        Protected Sub Connect(ByVal connectionParams As WlanConnectionParameters)
            Wlan.ThrowIfError(Wlan.WlanConnect(Me.client.clientHandle, Me.info.interfaceGuid, (connectionParams), IntPtr.Zero))
        End Sub
        Public Sub Disconnect()
            Wlan.ThrowIfError(Wlan.WlanDisconnect(Me.client.clientHandle, Me.info.interfaceGuid, IntPtr.Zero))
        End Sub
        Public Sub Connect(ByVal connectionMode As WlanConnectionMode, ByVal bssType As Dot11BssType, ByVal profile As String)
            Dim connectionParams As New WlanConnectionParameters
            connectionParams.wlanConnectionMode = connectionMode
            connectionParams.profile = profile
            connectionParams.dot11BssType = bssType
            connectionParams.flags = 0
            Me.Connect(connectionParams)
        End Sub
        Public Sub Connect(ByVal connectionMode As WlanConnectionMode, ByVal bssType As Dot11BssType, ByVal ssid As Dot11Ssid, ByVal flags As WlanConnectionFlags)
            Dim connectionParams As New WlanConnectionParameters
            connectionParams.wlanConnectionMode = connectionMode
            connectionParams.dot11SsidPtr = Marshal.AllocHGlobal(Marshal.SizeOf(ssid))
            Marshal.StructureToPtr(ssid, connectionParams.dot11SsidPtr, False)
            connectionParams.dot11BssType = bssType
            connectionParams.flags = flags
            Me.Connect(connectionParams)
            Marshal.DestroyStructure(connectionParams.dot11SsidPtr, ssid.GetType)
            Marshal.FreeHGlobal(connectionParams.dot11SsidPtr)
        End Sub
        Public Function ConnectSynchronously(ByVal connectionMode As WlanConnectionMode, ByVal bssType As Dot11BssType, ByVal profile As String, ByVal connectTimeout As Integer) As Boolean
            Me.queueEvents = True
            Try
                Me.Connect(connectionMode, bssType, profile)
                Do While (Me.queueEvents AndAlso Me.eventQueueFilled.WaitOne(connectTimeout, True))
                    SyncLock Me.eventQueue
                        Do While (Me.eventQueue.Count <> 0)
                            Dim e As Object = Me.eventQueue.Dequeue
                            If TypeOf e Is WlanConnectionNotificationEventData Then
                                Dim wlanConnectionData As WlanConnectionNotificationEventData = DirectCast(e, WlanConnectionNotificationEventData)
                                If (((wlanConnectionData.notifyData.notificationSource = WlanNotificationSource.ACM) AndAlso (wlanConnectionData.notifyData.notificationCode = 10)) AndAlso (wlanConnectionData.connNotifyData.profileName = profile)) Then
                                    Return True
                                End If
                                GoTo Label_00CA
                            End If
                        Loop
                    End SyncLock
Label_00CA:
                Loop
            Finally
                Me.queueEvents = False
                Me.eventQueue.Clear()
            End Try
            Return False
        End Function

        Private Function ConvertAvailableNetworkListPtr(ByVal availNetListPtr As IntPtr) As WlanAvailableNetwork()
            Dim availNetListHeader As WlanAvailableNetworkListHeader = DirectCast(Marshal.PtrToStructure(availNetListPtr, GetType(WlanAvailableNetworkListHeader)), WlanAvailableNetworkListHeader)
            Dim availNetListIt As Long = (availNetListPtr.ToInt64 + Marshal.SizeOf(GetType(WlanAvailableNetworkListHeader)))
            Dim availNets As WlanAvailableNetwork() = New WlanAvailableNetwork(availNetListHeader.numberOfItems - 1) {}
            Dim i As Integer
            For i = 0 To availNetListHeader.numberOfItems - 1
                availNets(i) = DirectCast(Marshal.PtrToStructure(New IntPtr(availNetListIt), GetType(WlanAvailableNetwork)), WlanAvailableNetwork)
                availNetListIt = (availNetListIt + Marshal.SizeOf(GetType(WlanAvailableNetwork)))
            Next i
            Return availNets
        End Function

        Private Function ConvertBssListPtr(ByVal bssListPtr As IntPtr) As WlanBssEntry()
            Dim bssListHeader As WlanBssListHeader = DirectCast(Marshal.PtrToStructure(bssListPtr, GetType(WlanBssListHeader)), WlanBssListHeader)
            Dim bssListIt As Long = (bssListPtr.ToInt64 + Marshal.SizeOf(GetType(WlanBssListHeader)))
            Dim bssEntries As WlanBssEntry() = New WlanBssEntry(bssListHeader.numberOfItems - 1) {}
            Dim i As Integer
            For i = 0 To bssListHeader.numberOfItems - 1
                bssEntries(i) = DirectCast(Marshal.PtrToStructure(New IntPtr(bssListIt), GetType(WlanBssEntry)), WlanBssEntry)
                bssListIt = (bssListIt + Marshal.SizeOf(GetType(WlanBssEntry)))
            Next i
            Return bssEntries
        End Function

        Public Sub DeleteProfile(ByVal profileName As String)
            Wlan.ThrowIfError(Wlan.WlanDeleteProfile(Me.client.clientHandle, Me.info.interfaceGuid, profileName, IntPtr.Zero))
        End Sub
        Private Sub EnqueueEvent(ByVal queuedEvent As Object)
            SyncLock Me.eventQueue
                Me.eventQueue.Enqueue(queuedEvent)
            End SyncLock
            Me.eventQueueFilled.Set()
        End Sub
        Public Function GetAvailableNetworkList(ByVal flags As WlanGetAvailableNetworkFlags) As WlanAvailableNetwork()
            Dim availNetListPtr As IntPtr
            Dim CS As WlanAvailableNetwork()
            'Try
            Wlan.ThrowIfError(Wlan.WlanGetAvailableNetworkList(Me.client.clientHandle, Me.info.interfaceGuid, flags, IntPtr.Zero, availNetListPtr))
            'Finally
            'MsgBox(Err.Description)
            'End Try
            Try
                CS = Me.ConvertAvailableNetworkListPtr(availNetListPtr)
            Finally
                Wlan.WlanFreeMemory(availNetListPtr)
            End Try
            Return CS
        End Function
        Private Function GetInterfaceInt(ByVal opCode As WlanIntfOpcode) As Integer
            Dim valuePtr As IntPtr
            Dim valueSize As Integer
            Dim opcodeValueType As WlanOpcodeValueType
            Dim CS As Integer
            Try
                Wlan.ThrowIfError(Wlan.WlanQueryInterface(Me.client.clientHandle, Me.info.interfaceGuid, opCode, IntPtr.Zero, valueSize, valuePtr, opcodeValueType))
            Catch ex As Win32Exception
                MsgBox("Your Wi-Fi adapter is busy", MsgBoxStyle.Exclamation, "Warning...")
            End Try
            Try
                CS = Marshal.ReadInt32(valuePtr)
            Catch ex As AccessViolationException
                MsgBox("Your Wi-Fi adapter is busy", MsgBoxStyle.Exclamation, "Warning...")
            Finally
                Wlan.WlanFreeMemory(valuePtr)
            End Try
            Return CS
        End Function
        Public Function GetNetworkBssList() As WlanBssEntry()
            Dim bssListPtr As IntPtr
            Dim CS As WlanBssEntry()
            Wlan.ThrowIfError(Wlan.WlanGetNetworkBssList(Me.client.clientHandle, Me.info.interfaceGuid, IntPtr.Zero, Dot11BssType.Any, False, IntPtr.Zero, bssListPtr))
            Try
                CS = Me.ConvertBssListPtr(bssListPtr)
            Finally
                Wlan.WlanFreeMemory(bssListPtr)
            End Try
            Return CS
        End Function
        Public Function GetNetworkBssList(ByVal ssid As Dot11Ssid, ByVal bssType As Dot11BssType, ByVal securityEnabled As Boolean) As WlanBssEntry()
            Dim CS As WlanBssEntry()
            Dim ssidPtr As IntPtr = Marshal.AllocHGlobal(Marshal.SizeOf(ssid))
            Marshal.StructureToPtr(ssid, ssidPtr, False)
            Try
                Dim bssListPtr As IntPtr
                Wlan.ThrowIfError(Wlan.WlanGetNetworkBssList(Me.client.clientHandle, Me.info.interfaceGuid, ssidPtr, bssType, securityEnabled, IntPtr.Zero, bssListPtr))
                Try
                    CS = Me.ConvertBssListPtr(bssListPtr)
                Finally
                    Wlan.WlanFreeMemory(bssListPtr)
                End Try
            Finally
                Marshal.FreeHGlobal(ssidPtr)
            End Try
            Return CS
        End Function
        Public Function GetProfiles() As WlanProfileInfo()
            Dim profileListPtr As IntPtr
            Dim CS As WlanProfileInfo()
            Wlan.ThrowIfError(Wlan.WlanGetProfileList(Me.client.clientHandle, Me.info.interfaceGuid, IntPtr.Zero, profileListPtr))
            Try
                Dim header As WlanProfileInfoListHeader = DirectCast(Marshal.PtrToStructure(profileListPtr, GetType(WlanProfileInfoListHeader)), WlanProfileInfoListHeader)
                Dim profileInfos As WlanProfileInfo() = New WlanProfileInfo(header.numberOfItems - 1) {}
                Dim profileListIterator As Long = (profileListPtr.ToInt64 + Marshal.SizeOf(header))
                Dim i As Integer
                For i = 0 To header.numberOfItems - 1
                    Dim profileInfo As WlanProfileInfo = DirectCast(Marshal.PtrToStructure(New IntPtr(profileListIterator), GetType(WlanProfileInfo)), WlanProfileInfo)
                    profileInfos(i) = profileInfo
                    profileListIterator = (profileListIterator + Marshal.SizeOf(profileInfo))
                Next i
                CS = profileInfos
            Finally
                Wlan.WlanFreeMemory(profileListPtr)
            End Try
            Return CS
        End Function
        Public Function GetProfileXml(ByVal profileName As String) As String
            Dim profileXmlPtr As IntPtr
            Dim flags As WlanProfileFlags
            Dim access As WlanAccess
            Dim CS As String
            Wlan.ThrowIfError(Wlan.WlanGetProfile(Me.client.clientHandle, Me.info.interfaceGuid, profileName, IntPtr.Zero, profileXmlPtr, flags, access))
            Try
                CS = Marshal.PtrToStringUni(profileXmlPtr)
            Finally
                Wlan.WlanFreeMemory(profileXmlPtr)
            End Try
            Return CS
        End Function
        Friend Sub OnWlanConnection(ByVal notifyData As WlanNotificationData, ByVal connNotifyData As WlanConnectionNotificationData)
            On Error Resume Next
            'If (Not WlanConnectionNotification Is Nothing) Then
            RaiseEvent WlanConnectionNotification(notifyData, connNotifyData)
            'End If
            If Me.queueEvents Then
                Dim queuedEvent As New WlanConnectionNotificationEventData
                queuedEvent.notifyData = notifyData
                queuedEvent.connNotifyData = connNotifyData
                Me.EnqueueEvent(queuedEvent)
            End If
        End Sub
        Friend Sub OnWlanNotification(ByVal notifyData As WlanNotificationData)
            On Error Resume Next
            'If (Not Me.WlanNotification Is Nothing) Then
            RaiseEvent WlanNotification(notifyData)
            'End If
        End Sub
        Friend Sub OnWlanReason(ByVal notifyData As WlanNotificationData, ByVal reasonCode As WlanReasonCode)
            On Error Resume Next
            'If (Not WlanReasonNotification Is Nothing) Then
            RaiseEvent WlanReasonNotification(notifyData, reasonCode)
            'End If
            If Me.queueEvents Then
                Dim queuedEvent As New WlanReasonNotificationData
                queuedEvent.notifyData = notifyData
                queuedEvent.reasonCode = reasonCode
                Me.EnqueueEvent(queuedEvent)
            End If
        End Sub
        Public Sub Scan()
            Wlan.ThrowIfError(Wlan.WlanScan(Me.client.clientHandle, Me.info.interfaceGuid, IntPtr.Zero, IntPtr.Zero, IntPtr.Zero))
        End Sub
        Private Sub SetInterfaceInt(ByVal opCode As WlanIntfOpcode, ByVal value As Integer)
            Dim valuePtr As IntPtr = Marshal.AllocHGlobal(4)
            Marshal.WriteInt32(valuePtr, value)
            Try
                Wlan.ThrowIfError(Wlan.WlanSetInterface(Me.client.clientHandle, Me.info.interfaceGuid, opCode, 4, valuePtr, IntPtr.Zero))
            Finally
                Marshal.FreeHGlobal(valuePtr)
            End Try
        End Sub
        Public Function SetProfile(ByVal flags As WlanProfileFlags, ByVal profileXml As String, ByVal overwrite As Boolean) As WlanReasonCode
            Dim reasonCode As WlanReasonCode
            Wlan.ThrowIfError(Wlan.WlanSetProfile(Me.client.clientHandle, Me.info.interfaceGuid, flags, profileXml, Nothing, overwrite, IntPtr.Zero, reasonCode))
            Return reasonCode
        End Function
        ' Properties
        Public Property Autoconf() As Boolean
            Get
                Return (Me.GetInterfaceInt(WlanIntfOpcode.AutoconfEnabled) <> 0)
            End Get
            Set(ByVal value As Boolean)
                Me.SetInterfaceInt(WlanIntfOpcode.AutoconfEnabled, IIf(value, 1, 0))
            End Set
        End Property
        Public Property BssType() As Dot11BssType
            Get
                Return DirectCast(Me.GetInterfaceInt(WlanIntfOpcode.BssType), Dot11BssType)
            End Get
            Set(ByVal value As Dot11BssType)
                Me.SetInterfaceInt(WlanIntfOpcode.BssType, CInt(value))
            End Set
        End Property
        Public ReadOnly Property Channel() As Integer
            Get
                Return Me.GetInterfaceInt(WlanIntfOpcode.ChannelNumber)
            End Get
        End Property
        Public ReadOnly Property CurrentConnection() As WlanConnectionAttributes
            Get
                Dim valueSize As Integer
                Dim valuePtr As IntPtr
                Dim opcodeValueType As WlanOpcodeValueType
                Dim CS As WlanConnectionAttributes
                Try
                    Wlan.ThrowIfError(Wlan.WlanQueryInterface(Me.client.clientHandle, Me.info.interfaceGuid, WlanIntfOpcode.CurrentConnection, IntPtr.Zero, valueSize, valuePtr, opcodeValueType))
                Catch ex As Win32Exception
                    MsgBox("Your Wi-Fi adapter is trying to connect to another saved Wi-Fi network", MsgBoxStyle.Exclamation, "Warning...")
                End Try
                Try
                    CS = DirectCast(Marshal.PtrToStructure(valuePtr, GetType(WlanConnectionAttributes)), WlanConnectionAttributes)
                Finally
                    Wlan.WlanFreeMemory(valuePtr)
                End Try
                Return CS
            End Get
        End Property
        Public ReadOnly Property CurrentOperationMode() As Dot11OperationMode
            Get
                Return DirectCast(Me.GetInterfaceInt(WlanIntfOpcode.CurrentOperationMode), Dot11OperationMode)
            End Get
        End Property
        Public ReadOnly Property InterfaceDescription() As String
            Get
                Return Me.info.interfaceDescription
            End Get
        End Property
        Public ReadOnly Property InterfaceGuid() As Guid
            Get
                Return Me.info.interfaceGuid
            End Get
        End Property
        Public ReadOnly Property InterfaceName() As String
            Get
                Return NetworkInterface.Name
            End Get
        End Property
        Public ReadOnly Property InterfaceState() As WlanInterfaceState
            Get
                Return DirectCast(Me.GetInterfaceInt(WlanIntfOpcode.InterfaceState), WlanInterfaceState)
            End Get
        End Property
        Public ReadOnly Property NetworkInterface() As NetworkInterface
            Get
                Dim netIface As NetworkInterface
                For Each netIface In NetworkInterface.GetAllNetworkInterfaces
                    Dim netIfaceGuid As New Guid(netIface.Id)
                    If netIfaceGuid.Equals(Me.info.interfaceGuid) Then
                        Return netIface
                    End If
                Next
                Return Nothing
            End Get
        End Property
        Public ReadOnly Property RSSI() As Integer
            Get
                Return Me.GetInterfaceInt(WlanIntfOpcode.RSSI)
            End Get
        End Property
        ' Fields
        Private client As WlanClient
        Private eventQueue As Queue(Of Object) = New Queue(Of Object)
        Private eventQueueFilled As AutoResetEvent = New AutoResetEvent(False)
        Private info As WlanInterfaceInfo
        Private queueEvents As Boolean
        ' Nested Types
        <StructLayout(LayoutKind.Sequential)> _
        Private Structure WlanConnectionNotificationEventData
            Public notifyData As WlanNotificationData
            Public connNotifyData As WlanConnectionNotificationData
        End Structure
        Public Delegate Sub WlanConnectionNotificationEventHandler(ByVal notifyData As WlanNotificationData, ByVal connNotifyData As WlanConnectionNotificationData)
        Public Delegate Sub WlanNotificationEventHandler(ByVal notifyData As WlanNotificationData)
        <StructLayout(LayoutKind.Sequential)> _
        Private Structure WlanReasonNotificationData
            Public notifyData As WlanNotificationData
            Public reasonCode As WlanReasonCode
        End Structure
        Public Delegate Sub WlanReasonNotificationEventHandler(ByVal notifyData As WlanNotificationData, ByVal reasonCode As WlanReasonCode)
    End Class
End Class


