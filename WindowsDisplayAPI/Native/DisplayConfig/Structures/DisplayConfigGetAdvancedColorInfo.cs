using System.Runtime.InteropServices;
using WindowsDisplayAPI.Native.Structures;

namespace WindowsDisplayAPI.Native.DisplayConfig.Structures
{
    [StructLayout(LayoutKind.Sequential)]
    internal struct DisplayConfigGetAdvancedColorInfo
    {
        [MarshalAs(UnmanagedType.Struct)] private readonly DisplayConfigDeviceInfoHeader _Header;
        [MarshalAs(UnmanagedType.U4)] private readonly uint _Value;
        [MarshalAs(UnmanagedType.U4)] private readonly DisplayConfigColorEncoding _ColorEncoding;
        [MarshalAs(UnmanagedType.U4)] private readonly uint _BitsPerColorChannel;

        public bool AdvancedColorSupported => (_Value & 0x1) != 0;
        public bool AdvancedColorEnabled => (_Value & 0x2) != 0;
        public bool WideColorEnforced => (_Value & 0x4) != 0;
        public bool AdvancedColorForceDisabled => (_Value & 0x8) != 0;
        public DisplayConfigColorEncoding ColorEncoding => _ColorEncoding;
        public uint BitsPerColorChannel => _BitsPerColorChannel;

        public DisplayConfigGetAdvancedColorInfo(LUID adapter, uint targetId) : this()
        {
            _Header = new DisplayConfigDeviceInfoHeader(adapter, targetId, GetType(),
                DisplayConfigDeviceInfoType.GetAdvancedColorInfo);
        }
    }
}
