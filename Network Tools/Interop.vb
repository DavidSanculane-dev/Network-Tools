Imports System
Imports System.Runtime.InteropServices
Imports System.Net.NetworkInformation
Imports System.Text
Imports System.Diagnostics
Imports System.ComponentModel
Imports Network_Tools.Wlan
Imports Network_Tools.WlanClient
Public Class Wlan
    ' Methods
    <DebuggerStepThrough()> _
    Friend Shared Sub ThrowIfError(ByVal win32ErrorCode As Integer)
        If (win32ErrorCode <> 0) Then
            Throw New Win32Exception(win32ErrorCode)
            'MsgBox(ErrorToString(win32ErrorCode), MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "Erreur")
        End If
    End Sub
    <DllImport("wlanapi.dll")> _
    Public Shared Function WlanCloseHandle(<[In]()> ByVal clientHandle As IntPtr, <[In](), Out()> ByVal pReserved As IntPtr) As Integer
    End Function
    <DllImport("wlanapi.dll")> _
    Public Shared Function WlanConnect(<[In]()> ByVal clientHandle As IntPtr, <[In](), MarshalAs(UnmanagedType.LPStruct)> ByVal interfaceGuid As Guid, <[In]()> ByRef connectionParameters As WlanConnectionParameters, ByVal pReserved As IntPtr) As Integer
    End Function
    <DllImport("wlanapi.dll")> _
    Public Shared Function WlanDeleteProfile(<[In]()> ByVal clientHandle As IntPtr, <[In](), MarshalAs(UnmanagedType.LPStruct)> ByVal interfaceGuid As Guid, <[In](), MarshalAs(UnmanagedType.LPWStr)> ByVal profileName As String, ByVal reservedPtr As IntPtr) As Integer
    End Function
    <DllImport("wlanapi.dll")> _
    Public Shared Function WlanEnumInterfaces(<[In]()> ByVal clientHandle As IntPtr, <[In](), Out()> ByVal pReserved As IntPtr, <Out()> ByRef ppInterfaceList As IntPtr) As Integer
    End Function
    <DllImport("wlanapi.dll")> _
    Public Shared Sub WlanFreeMemory(ByVal pMemory As IntPtr)
    End Sub
    <DllImport("wlanapi.dll")> _
    Public Shared Function WlanGetAvailableNetworkList(<[In]()> ByVal clientHandle As IntPtr, <[In](), MarshalAs(UnmanagedType.LPStruct)> ByVal interfaceGuid As Guid, <[In]()> ByVal flags As WlanGetAvailableNetworkFlags, <[In](), Out()> ByVal reservedPtr As IntPtr, <Out()> ByRef availableNetworkListPtr As IntPtr) As Integer
    End Function
    <DllImport("wlanapi.dll")> _
    Public Shared Function WlanGetNetworkBssList(<[In]()> ByVal clientHandle As IntPtr, <[In](), MarshalAs(UnmanagedType.LPStruct)> ByVal interfaceGuid As Guid, <[In]()> ByVal dot11SsidInt As IntPtr, <[In]()> ByVal dot11BssType As Dot11BssType, <[In]()> ByVal securityEnabled As Boolean, ByVal reservedPtr As IntPtr, <Out()> ByRef wlanBssList As IntPtr) As Integer
    End Function
    <DllImport("wlanapi.dll")> _
    Public Shared Function WlanGetProfile(<[In]()> ByVal clientHandle As IntPtr, <[In](), MarshalAs(UnmanagedType.LPStruct)> ByVal interfaceGuid As Guid, <[In](), MarshalAs(UnmanagedType.LPWStr)> ByVal profileName As String, <[In]()> ByVal pReserved As IntPtr, <Out()> ByRef profileXml As IntPtr, <Out()> Optional ByRef flags As WlanProfileFlags = WlanProfileFlags.AllUser, <Out()> Optional ByRef grantedAccess As WlanAccess = WlanAccess.ExecuteAccess) As Integer
    End Function
    <DllImport("wlanapi.dll")> _
    Public Shared Function WlanGetProfileList(<[In]()> ByVal clientHandle As IntPtr, <[In](), MarshalAs(UnmanagedType.LPStruct)> ByVal interfaceGuid As Guid, <[In]()> ByVal pReserved As IntPtr, <Out()> ByRef profileList As IntPtr) As Integer
    End Function
    <DllImport("wlanapi.dll")> _
    Public Shared Function WlanOpenHandle(<[In]()> ByVal clientVersion As UInt32, <[In](), Out()> ByVal pReserved As IntPtr, <Out()> ByRef negotiatedVersion As UInt32, <Out()> ByRef clientHandle As IntPtr) As Integer
    End Function
    <DllImport("wlanapi.dll")> _
    Public Shared Function WlanQueryInterface(<[In]()> ByVal clientHandle As IntPtr, <[In](), MarshalAs(UnmanagedType.LPStruct)> ByVal interfaceGuid As Guid, <[In]()> ByVal opCode As WlanIntfOpcode, <[In](), Out()> ByVal pReserved As IntPtr, <Out()> ByRef dataSize As Integer, <Out()> ByRef ppData As IntPtr, <Out()> ByRef wlanOpcodeValueType As WlanOpcodeValueType) As Integer
    End Function
    <DllImport("wlanapi.dll")> _
    Public Shared Function WlanReasonCodeToString(<[In]()> ByVal reasonCode As WlanReasonCode, <[In]()> ByVal bufferSize As Integer, <[In](), Out()> ByVal stringBuffer As StringBuilder, ByVal pReserved As IntPtr) As Integer
    End Function
    <DllImport("wlanapi.dll")> _
    Public Shared Function WlanRegisterNotification(<[In]()> ByVal clientHandle As IntPtr, <[In]()> ByVal notifSource As WlanNotificationSource, <[In]()> ByVal ignoreDuplicate As Boolean, <[In]()> ByVal funcCallback As WlanNotificationCallbackDelegate, <[In]()> ByVal callbackContext As IntPtr, <[In]()> ByVal reserved As IntPtr, <Out()> ByRef prevNotifSource As WlanNotificationSource) As Integer
    End Function
    <DllImport("wlanapi.dll")> _
    Public Shared Function WlanScan(<[In]()> ByVal clientHandle As IntPtr, <[In](), MarshalAs(UnmanagedType.LPStruct)> ByVal interfaceGuid As Guid, <[In]()> ByVal pDot11Ssid As IntPtr, <[In]()> ByVal pIeData As IntPtr, <[In](), Out()> ByVal pReserved As IntPtr) As Integer
    End Function
    <DllImport("wlanapi.dll")> _
    Public Shared Function WlanSetInterface(<[In]()> ByVal clientHandle As IntPtr, <[In](), MarshalAs(UnmanagedType.LPStruct)> ByVal interfaceGuid As Guid, <[In]()> ByVal opCode As WlanIntfOpcode, <[In]()> ByVal dataSize As UInt32, <[In]()> ByVal pData As IntPtr, <[In](), Out()> ByVal pReserved As IntPtr) As Integer
    End Function
    <DllImport("wlanapi.dll")> _
    Public Shared Function WlanSetProfile(<[In]()> ByVal clientHandle As IntPtr, <[In](), MarshalAs(UnmanagedType.LPStruct)> ByVal interfaceGuid As Guid, <[In]()> ByVal flags As WlanProfileFlags, <[In](), MarshalAs(UnmanagedType.LPWStr)> ByVal profileXml As String, <[In](), MarshalAs(UnmanagedType.LPWStr)> ByVal allUserProfileSecurity As String, <[In]()> ByVal overwrite As Boolean, <[In]()> ByVal pReserved As IntPtr, <Out()> ByRef reasonCode As WlanReasonCode) As Integer
    End Function
    <DllImport("wlanapi.dll")> _
    Public Shared Function WlanDisconnect(<[In]()> ByVal clientHandle As IntPtr, <[In](), MarshalAs(UnmanagedType.LPStruct)> ByVal interfaceGuid As Guid, <Out()> ByVal pReserved As IntPtr) As Integer
    End Function
    ' Fields
    Public Const WLAN_CLIENT_VERSION_LONGHORN As UInt32 = 2
    Public Const WLAN_CLIENT_VERSION_XP_SP2 As UInt32 = 1
    ' Nested Types
    Public Enum Dot11AuthAlgorithm As Int32
        ' Fields
        IEEE80211_Open = 1
        IEEE80211_SharedKey = 2
        IHV_End = &HFFFFFFFF
        IHV_Start = &H80000000
        RSNA = 6
        RSNA_PSK = 7
        WPA = 3
        WPA_None = 5
        WPA_PSK = 4
    End Enum
    Public Enum Dot11BssType
        ' Fields
        Any = 3
        Independent = 2
        Infrastructure = 1
    End Enum
    Public Enum Dot11CipherAlgorithm As Int32
        ' Fields
        CCMP = 4
        IHV_End = &HFFFFFFFF
        IHV_Start = &H80000000
        None = 0
        RSN_UseGroup = &H100
        TKIP = 2
        WEP = &H101
        WEP104 = 5
        WEP40 = 1
        WPA_UseGroup = &H100
    End Enum
    <Flags()> _
    Public Enum Dot11OperationMode As Int32
        ' Fields
        AP = 2
        ExtensibleStation = 4
        NetworkMonitor = &H80000000
        Station = 1
        Unknown = 0
    End Enum
    Public Enum Dot11PhyType As Int32
        ' Fields
        Any = 0
        DSSS = 2
        ERP = 6
        FHSS = 1
        HRDSSS = 5
        IHV_End = &HFFFFFFFF
        IHV_Start = &H80000000
        IrBaseband = 3
        OFDM = 4
        Unknown = 0
    End Enum
    <StructLayout(LayoutKind.Sequential)> _
    Public Structure Dot11Ssid
        Public SSIDLength As UInt32
        <MarshalAs(UnmanagedType.ByValArray, SizeConst:=&H20)> _
        Public SSID As Byte()
    End Structure
    <Flags()> _
    Public Enum WlanAccess
        ' Fields
        ExecuteAccess = &H20021
        ReadAccess = &H20001
        WriteAccess = &H70023
    End Enum
    Public Enum WlanAdhocNetworkState
        ' Fields
        Connected = 1
        Formed = 0
    End Enum
    <StructLayout(LayoutKind.Sequential)> _
    Public Structure WlanAssociationAttributes
        Public dot11Ssid As Dot11Ssid
        Public dot11BssType As Dot11BssType
        <MarshalAs(UnmanagedType.ByValArray, SizeConst:=6)> _
        Public dot11Bssid As Byte()
        Public dot11PhyType As Dot11PhyType
        Public dot11PhyIndex As UInt32
        Public wlanSignalQuality As UInt32
        Public rxRate As UInt32
        Public txRate As UInt32
        Public ReadOnly Property Dot11Bssid2() As PhysicalAddress
            Get
                Return New PhysicalAddress(Me.dot11Bssid)
            End Get
        End Property
    End Structure
    <StructLayout(LayoutKind.Sequential, CharSet:=CharSet.Unicode)> _
    Public Structure WlanAvailableNetwork
        <MarshalAs(UnmanagedType.ByValTStr, SizeConst:=&H100)> _
        Public profileName As String
        Public dot11Ssid As Dot11Ssid
        Public dot11BssType As Dot11BssType
        Public numberOfBssids As UInt32
        Public networkConnectable As Boolean
        Public wlanNotConnectableReason As WlanReasonCode
        Private numberOfPhyTypes As UInt32
        <MarshalAs(UnmanagedType.ByValArray, SizeConst:=8)> _
        Private dot11PhyTypes As Dot11PhyType()
        Public morePhyTypes As Boolean
        Public wlanSignalQuality As UInt32
        Public securityEnabled As Boolean
        Public dot11DefaultAuthAlgorithm As Dot11AuthAlgorithm
        Public dot11DefaultCipherAlgorithm As Dot11CipherAlgorithm
        Public flags As WlanAvailableNetworkFlags
        Private reserved As UInt32
        Public ReadOnly Property Dot11PhyTypes2() As Dot11PhyType()
            Get
                Dim ret As Dot11PhyType() = New Dot11PhyType(Me.numberOfPhyTypes - 1) {}
                Array.Copy(Me.dot11PhyTypes, ret, CLng(Me.numberOfPhyTypes))
                Return ret
            End Get
        End Property
    End Structure
    <Flags()> _
    Public Enum WlanAvailableNetworkFlags
        ' Fields
        Connected = 1
        HasProfile = 2
    End Enum
    <StructLayout(LayoutKind.Sequential)> _
    Friend Structure WlanAvailableNetworkListHeader
        Public numberOfItems As UInt32
        Public index As UInt32
    End Structure
    <StructLayout(LayoutKind.Sequential)> _
    Public Structure WlanBssEntry
        Public dot11Ssid As Dot11Ssid
        Public phyId As UInt32
        <MarshalAs(UnmanagedType.ByValArray, SizeConst:=6)> _
        Public dot11Bssid As Byte()
        Public dot11BssType As Dot11BssType
        Public dot11BssPhyType As Dot11PhyType
        Public rssi As Integer
        Public linkQuality As UInt32
        Public inRegDomain As Boolean
        Public beaconPeriod As UInt16
        Public timestamp As UInt64
        Public hostTimestamp As UInt64
        Public capabilityInformation As UInt16
        Public chCenterFrequency As UInt32
        Public wlanRateSet As WlanRateSet
        Public ieOffset As UInt32
        Public ieSize As UInt32
    End Structure
    <StructLayout(LayoutKind.Sequential)> _
    Friend Structure WlanBssListHeader
        Friend totalSize As UInt32
        Friend numberOfItems As UInt32
    End Structure
    <StructLayout(LayoutKind.Sequential, CharSet:=CharSet.Unicode)> _
    Public Structure WlanConnectionAttributes
        Public isState As WlanInterfaceState
        Public wlanConnectionMode As WlanConnectionMode
        <MarshalAs(UnmanagedType.ByValTStr, SizeConst:=&H100)> _
        Public profileName As String
        Public wlanAssociationAttributes As WlanAssociationAttributes
        Public wlanSecurityAttributes As WlanSecurityAttributes
    End Structure
    <Flags()> _
    Public Enum WlanConnectionFlags
        ' Fields
        AdhocJoinOnly = 2
        EapolPassthrough = 8
        HiddenNetwork = 1
        IgnorePrivacyBit = 4
    End Enum
    Public Enum WlanConnectionMode
        ' Fields
        [Auto] = 4
        DiscoverySecure = 2
        DiscoveryUnsecure = 3
        Invalid = 5
        Profile = 0
        TemporaryProfile = 1
    End Enum
    <StructLayout(LayoutKind.Sequential, CharSet:=CharSet.Unicode)> _
    Public Structure WlanConnectionNotificationData
        Public wlanConnectionMode As WlanConnectionMode
        <MarshalAs(UnmanagedType.ByValTStr, SizeConst:=&H20)> _
        Public profileName As String
        Public dot11Ssid As Dot11Ssid
        Public dot11BssType As Dot11BssType
        Public securityEnabled As Boolean
        Public wlanReasonCode As WlanReasonCode
        <MarshalAs(UnmanagedType.ByValTStr, SizeConst:=1)> _
        Public profileXml As String
    End Structure
    <StructLayout(LayoutKind.Sequential)> _
    Public Structure WlanConnectionParameters
        Public wlanConnectionMode As WlanConnectionMode
        <MarshalAs(UnmanagedType.LPWStr)> _
        Public profile As String
        Public dot11SsidPtr As IntPtr
        Public desiredBssidListPtr As IntPtr
        Public dot11BssType As Dot11BssType
        Public flags As WlanConnectionFlags
    End Structure
    Public Class WlanException
        Inherits Exception
        ' Methods
        'Private Sub New(ByVal reasonCode As WlanReasonCode)
        'Me.ReasonCode = reasonCode
        'End Sub
        ' Properties
        Public Overrides ReadOnly Property Message() As String
            Get
                Dim sb As New StringBuilder(&H400)
                Dim err As WlanReasonCode
                If (Wlan.WlanReasonCodeToString(err, sb.Capacity, sb, IntPtr.Zero) = 0) Then
                    Return sb.ToString
                End If
                Return String.Empty
            End Get
        End Property
        '        Public ReadOnly Property ReasonCode() As WlanReasonCode
        '           Get
        '              Return Me.ReasonCode.ToString 
        '         End Get
        '        End Property
        ' Fields
        'Private reasonCode As WlanReasonCode
    End Class
    <Flags()> _
    Public Enum WlanGetAvailableNetworkFlags
        ' Fields
        IncludeAllAdhocProfiles = 1
        IncludeAllManualHiddenProfiles = 2
    End Enum
    <StructLayout(LayoutKind.Sequential, CharSet:=CharSet.Unicode)> _
    Public Structure WlanInterfaceInfo
        Public interfaceGuid As Guid
        <MarshalAs(UnmanagedType.ByValTStr, SizeConst:=&H100)> _
        Public interfaceDescription As String
        Public isState As WlanInterfaceState
    End Structure
    <StructLayout(LayoutKind.Sequential)> _
    Friend Structure WlanInterfaceInfoListHeader
        Public numberOfItems As UInt32
        Public index As UInt32
    End Structure
    Public Enum WlanInterfaceState
        ' Fields
        AdHocNetworkFormed = 2
        Associating = 5
        Authenticating = 7
        Connected = 1
        Disconnected = 4
        Disconnecting = 3
        Discovering = 6
        NotReady = 0
    End Enum
    Public Enum WlanIntfOpcode
        ' Fields
        AutoconfEnabled = 1
        BackgroundScanEnabled = 2
        BssType = 5
        ChannelNumber = 8
        CurrentConnection = 7
        CurrentOperationMode = 12
        IhvEnd = &H3FFFFFFF
        IhvStart = &H30000000
        InterfaceState = 6
        MediaStreamingMode = 3
        RadioState = 4
        RSSI = &H10000102
        SecurityEnd = &H2FFFFFFF
        SecurityStart = &H20010000
        Statistics = &H10000101
        SupportedAdhocAuthCipherPairs = 10
        SupportedCountryOrRegionStringList = 11
        SupportedInfrastructureAuthCipherPairs = 9
    End Enum
    Public Delegate Sub WlanNotificationCallbackDelegate(ByRef notificationData As WlanNotificationData, ByVal context As IntPtr)
    Public Enum WlanNotificationCodeAcm
        ' Fields
        AdhocNetworkStateChange = &H16
        AutoconfDisabled = 2
        AutoconfEnabled = 1
        BackgroundScanDisabled = 4
        BackgroundScanEnabled = 3
        BssTypeChange = 5
        ConnectionAttemptFail = 11
        ConnectionComplete = 10
        ConnectionStart = 9
        Disconnected = &H15
        Disconnecting = 20
        FilterListChange = 12
        InterfaceArrival = 13
        InterfaceRemoval = 14
        NetworkAvailable = &H13
        NetworkNotAvailable = &H12
        PowerSettingChange = 6
        ProfileChange = 15
        ProfileNameChange = &H10
        ProfilesExhausted = &H11
        ScanComplete = 7
        ScanFail = 8
    End Enum
    Public Enum WlanNotificationCodeMsm
        ' Fields
        AdapterOperationModeChange = 14
        AdapterRemoval = 13
        Associated = 2
        Associating = 1
        Authenticating = 3
        Connected = 4
        Disassociating = 9
        Disconnected = 10
        PeerJoin = 11
        PeerLeave = 12
        RadioStateChange = 7
        RoamingEnd = 6
        RoamingStart = 5
        SignalQualityChange = 8
    End Enum
    <StructLayout(LayoutKind.Sequential)> _
    Public Structure WlanNotificationData
        Public notificationSource As WlanNotificationSource
        Public notificationCode As Integer
        Public interfaceGuid As Guid
        Public dataSize As Integer
        Public dataPtr As IntPtr
        Public ReadOnly Property NotificationCode2() As Object
            Get
                If (Me.notificationSource = WlanNotificationSource.MSM) Then
                    Return DirectCast(Me.notificationCode, WlanNotificationCodeMsm)
                End If
                If (Me.notificationSource = WlanNotificationSource.ACM) Then
                    Return DirectCast(Me.notificationCode, WlanNotificationCodeAcm)
                End If
                Return Me.notificationCode
            End Get
        End Property
    End Structure
    <Flags()> _
    Public Enum WlanNotificationSource
        ' Fields
        ACM = 8
        All = &HFFFF
        IHV = &H40
        MSM = &H10
        None = 0
        Security = &H20
    End Enum
    Public Enum WlanOpcodeValueType
        ' Fields
        Invalid = 3
        QueryOnly = 0
        SetByGroupPolicy = 1
        SetByUser = 2
    End Enum
    <Flags()> _
    Public Enum WlanProfileFlags
        ' Fields
        AllUser = 0
        GroupPolicy = 1
        User = 2
    End Enum
    <StructLayout(LayoutKind.Sequential, CharSet:=CharSet.Unicode)> _
    Public Structure WlanProfileInfo
        <MarshalAs(UnmanagedType.ByValTStr, SizeConst:=&H100)> _
        Public profileName As String
        Public profileFlags As WlanProfileFlags
    End Structure
    <StructLayout(LayoutKind.Sequential)> _
    Friend Structure WlanProfileInfoListHeader
        Public numberOfItems As UInt32
        Public index As UInt32
    End Structure
    <StructLayout(LayoutKind.Sequential)> _
    Public Structure WlanRateSet
        Private rateSetLength As UInt32
        <MarshalAs(UnmanagedType.ByValArray, SizeConst:=&H7E)> _
        Private rateSet As UInt16()
        Public ReadOnly Property Rates() As UInt16()
            Get
                Rates = New UInt16((Me.rateSetLength / 2) - 1) {}
                Array.Copy(Me.rateSet, Rates, Rates.Length)
                Return Rates
            End Get
        End Property
        Public Function GetRateInMbps(ByVal rate As Integer) As Double
            Return ((Me.rateSet(rate) And &H7FFF) * 0.5)
        End Function
    End Structure
    Public Enum WlanReasonCode
        ' Fields
        AC_BASE = &H20000
        AC_CONNECT_BASE = &H28000
        AC_END = &H2FFFF
        ADHOC_SECURITY_FAILURE = &H3800A
        ASSOCIATION_FAILURE = &H38002
        ASSOCIATION_TIMEOUT = &H38003
        AUTO_SWITCH_SET_FOR_ADHOC = &H80010
        AUTO_SWITCH_SET_FOR_MANUAL_CONNECTION = &H80011
        BASE = &H20000
        BSS_TYPE_NOT_ALLOWED = &H28005
        BSS_TYPE_UNMATCH = &H30003
        CONFLICT_SECURITY = &H8000B
        CONNECT_CALL_FAIL = &H28009
        DATARATE_UNMATCH = &H30005
        DISCONNECT_TIMEOUT = &H3800F
        DRIVER_DISCONNECTED = &H3800B
        DRIVER_OPERATION_FAILURE = &H3800C
        GP_DENIED = &H28003
        IHV_NOT_AVAILABLE = &H3800D
        IHV_NOT_RESPONDING = &H3800E
        IHV_OUI_MISMATCH = &H80008
        IHV_OUI_MISSING = &H80009
        IHV_SECURITY_NOT_SUPPORTED = &H80007
        IHV_SECURITY_ONEX_MISSING = &H80012
        IHV_SETTINGS_MISSING = &H8000A
        IN_BLOCKED_LIST = &H28007
        IN_FAILED_LIST = &H28006
        INTERNAL_FAILURE = &H38010
        INVALID_ADHOC_CONNECTION_MODE = &H8000E
        INVALID_BSS_TYPE = &H8000D
        INVALID_PHY_TYPE = &H80005
        INVALID_PROFILE_NAME = &H80003
        INVALID_PROFILE_SCHEMA = &H80001
        INVALID_PROFILE_TYPE = &H80004
        KEY_MISMATCH = &H2800D
        MSM_BASE = &H30000
        MSM_CONNECT_BASE = &H38000
        MSM_END = &H3FFFF
        MSM_SECURITY_MISSING = &H80006
        MSMSEC_AUTH_START_TIMEOUT = &H48002
        MSMSEC_AUTH_SUCCESS_TIMEOUT = &H48003
        MSMSEC_BASE = &H40000
        MSMSEC_CANCELLED = &H48011
        MSMSEC_CAPABILITY_DISCOVERY = &H40015
        MSMSEC_CAPABILITY_NETWORK = &H40012
        MSMSEC_CAPABILITY_NIC = &H40013
        MSMSEC_CAPABILITY_PROFILE = &H40014
        MSMSEC_CAPABILITY_PROFILE_AUTH = &H4001E
        MSMSEC_CAPABILITY_PROFILE_CIPHER = &H4001F
        MSMSEC_CONNECT_BASE = &H48000
        MSMSEC_DOWNGRADE_DETECTED = &H48013
        MSMSEC_END = &H4FFFF
        MSMSEC_FORCED_FAILURE = &H48015
        MSMSEC_G1_MISSING_GRP_KEY = &H4800D
        MSMSEC_G1_MISSING_KEY_DATA = &H4800C
        MSMSEC_KEY_FORMAT = &H48012
        MSMSEC_KEY_START_TIMEOUT = &H48004
        MSMSEC_KEY_SUCCESS_TIMEOUT = &H48005
        MSMSEC_M3_MISSING_GRP_KEY = &H48008
        MSMSEC_M3_MISSING_IE = &H48007
        MSMSEC_M3_MISSING_KEY_DATA = &H48006
        MSMSEC_MAX = &H4FFFF
        MSMSEC_MIN = &H40000
        MSMSEC_MIXED_CELL = &H40019
        MSMSEC_NIC_FAILURE = &H48010
        MSMSEC_NO_AUTHENTICATOR = &H4800F
        MSMSEC_NO_PAIRWISE_KEY = &H4800B
        MSMSEC_PEER_INDICATED_INSECURE = &H4800E
        MSMSEC_PR_IE_MATCHING = &H48009
        MSMSEC_PROFILE_AUTH_TIMERS_INVALID = &H4001A
        MSMSEC_PROFILE_DUPLICATE_AUTH_CIPHER = &H40007
        MSMSEC_PROFILE_INVALID_AUTH_CIPHER = &H40009
        MSMSEC_PROFILE_INVALID_GKEY_INTV = &H4001B
        MSMSEC_PROFILE_INVALID_KEY_INDEX = &H40001
        MSMSEC_PROFILE_INVALID_PMKCACHE_MODE = &H4000C
        MSMSEC_PROFILE_INVALID_PMKCACHE_SIZE = &H4000D
        MSMSEC_PROFILE_INVALID_PMKCACHE_TTL = &H4000E
        MSMSEC_PROFILE_INVALID_PREAUTH_MODE = &H4000F
        MSMSEC_PROFILE_INVALID_PREAUTH_THROTTLE = &H40010
        MSMSEC_PROFILE_KEY_LENGTH = &H40003
        MSMSEC_PROFILE_KEY_UNMAPPED_CHAR = &H4001D
        MSMSEC_PROFILE_KEYMATERIAL_CHAR = &H40017
        MSMSEC_PROFILE_NO_AUTH_CIPHER_SPECIFIED = &H40005
        MSMSEC_PROFILE_ONEX_DISABLED = &H4000A
        MSMSEC_PROFILE_ONEX_ENABLED = &H4000B
        MSMSEC_PROFILE_PASSPHRASE_CHAR = &H40016
        MSMSEC_PROFILE_PREAUTH_ONLY_ENABLED = &H40011
        MSMSEC_PROFILE_PSK_LENGTH = &H40004
        MSMSEC_PROFILE_PSK_PRESENT = &H40002
        MSMSEC_PROFILE_RAWDATA_INVALID = &H40008
        MSMSEC_PROFILE_TOO_MANY_AUTH_CIPHER_SPECIFIED = &H40006
        MSMSEC_PROFILE_WRONG_KEYTYPE = &H40018
        MSMSEC_PSK_MISMATCH_SUSPECTED = &H48014
        MSMSEC_SEC_IE_MATCHING = &H4800A
        MSMSEC_SECURITY_UI_FAILURE = &H48016
        MSMSEC_TRANSITION_NETWORK = &H4001C
        MSMSEC_UI_REQUEST_FAILURE = &H48001
        NETWORK_NOT_AVAILABLE = &H2800B
        NETWORK_NOT_COMPATIBLE = &H20001
        NO_AUTO_CONNECTION = &H28001
        NON_BROADCAST_SET_FOR_ADHOC = &H8000F
        NOT_VISIBLE = &H28002
        PHY_TYPE_UNMATCH = &H30004
        PRE_SECURITY_FAILURE = &H38004
        PROFILE_BASE = &H80000
        PROFILE_CHANGED_OR_DELETED = &H2800C
        PROFILE_CONNECT_BASE = &H88000
        PROFILE_END = &H8FFFF
        PROFILE_MISSING = &H80002
        PROFILE_NOT_COMPATIBLE = &H20002
        PROFILE_SSID_INVALID = &H80013
        RANGE_SIZE = &H10000
        ROAMING_FAILURE = &H38008
        ROAMING_SECURITY_FAILURE = &H38009
        SCAN_CALL_FAIL = &H2800A
        SECURITY_FAILURE = &H38006
        SECURITY_MISSING = &H8000C
        SECURITY_TIMEOUT = &H38007
        SSID_LIST_TOO_LONG = &H28008
        START_SECURITY_FAILURE = &H38005
        Success = 0
        TOO_MANY_SECURITY_ATTEMPTS = &H38012
        TOO_MANY_SSID = &H80014
        UI_REQUEST_TIMEOUT = &H38011
        UNKNOWN = &H10001
        UNSUPPORTED_SECURITY_SET = &H30002
        UNSUPPORTED_SECURITY_SET_BY_OS = &H30001
        USER_CANCELLED = &H38001
        USER_DENIED = &H28004
        USER_NOT_RESPOND = &H2800E
    End Enum
    <StructLayout(LayoutKind.Sequential)> _
    Public Structure WlanSecurityAttributes
        <MarshalAs(UnmanagedType.Bool)> _
        Public securityEnabled As Boolean
        <MarshalAs(UnmanagedType.Bool)> _
        Public oneXEnabled As Boolean
        Public dot11AuthAlgorithm As Dot11AuthAlgorithm
        Public dot11CipherAlgorithm As Dot11CipherAlgorithm
    End Structure
End Class
