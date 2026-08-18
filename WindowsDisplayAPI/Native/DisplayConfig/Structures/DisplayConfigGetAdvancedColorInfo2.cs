using System.Runtime.InteropServices;
using WindowsDisplayAPI.Native.Structures;

namespace WindowsDisplayAPI.Native.DisplayConfig.Structures
{
    [StructLayout(LayoutKind.Sequential)]
    internal struct DisplayConfigGetAdvancedColorInfo2
    {
        [MarshalAs(UnmanagedType.Struct)] private readonly DisplayConfigDeviceInfoHeader _Header;
        [MarshalAs(UnmanagedType.U4)] private readonly uint _Value;
        [MarshalAs(UnmanagedType.U4)] private readonly DisplayConfigColorEncoding _ColorEncoding;
        [MarshalAs(UnmanagedType.U4)] private readonly uint _BitsPerColorChannel;
        [MarshalAs(UnmanagedType.U4)] private readonly DisplayConfigAdvancedColorMode _ActiveColorMode;

        public bool HighDynamicRangeSupported => (_Value & 0x1) != 0;
        public bool HighDynamicRangeUserEnabled => (_Value & 0x2) != 0;
        public bool WideColorGamutSupported => (_Value & 0x4) != 0;
        public bool WideColorGamutUserEnabled => (_Value & 0x8) != 0;
        public bool AdvancedColorLimitedByPolicy => (_Value & 0x10) != 0;
        public bool AutoDynamicRangeSupported => (_Value & 0x20) != 0;
        public bool AutoDynamicRangeUserEnabled => (_Value & 0x40) != 0;

        public DisplayConfigColorEncoding ColorEncoding => _ColorEncoding;
        public uint BitsPerColorChannel => _BitsPerColorChannel;
        public DisplayConfigAdvancedColorMode ActiveColorMode => _ActiveColorMode;

        public DisplayConfigGetAdvancedColorInfo2(LUID adapter, uint targetId) : this()
        {
            _Header = new DisplayConfigDeviceInfoHeader(adapter, targetId, GetType(),
                DisplayConfigDeviceInfoType.GetAdvancedColorInfo2);
        }
    }
}
