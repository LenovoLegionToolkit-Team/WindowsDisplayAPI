namespace WindowsDisplayAPI.Native.DisplayConfig
{
    internal enum DisplayConfigDeviceInfoType
    {
        SetSourceDPIScale = -4,
        GetSourceDPIScale = -3,
        GetSourceName = 1,
        GetTargetName = 2,
        GetTargetPreferredMode = 3,
        GetAdapterName = 4,
        SetTargetPersistence = 5,
        GetTargetBaseType = 6,
        GetSupportVirtualResolution = 7,
        SetSupportVirtualResolution = 8,
        GetAdvancedColorInfo = 9,
        SetAdvancedColorState = 10,
        GetSdrWhiteLevel = 11,
        GetMonitorSpecialization = 12,
        SetMonitorSpecialization = 13,
        SetReserved1 = 14,
        GetAdvancedColorInfo2 = 15,
        SetHdrState = 16
    }
}