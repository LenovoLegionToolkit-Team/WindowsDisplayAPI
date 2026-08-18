using System.Runtime.InteropServices;
using WindowsDisplayAPI.Native.Structures;

namespace WindowsDisplayAPI.Native.DisplayConfig.Structures
{
    [StructLayout(LayoutKind.Sequential)]
    internal struct DisplayConfigSetAdvancedColorState
    {
        [MarshalAs(UnmanagedType.Struct)] private readonly DisplayConfigDeviceInfoHeader _Header;
        [MarshalAs(UnmanagedType.U4)] private readonly uint _Value;

        public bool EnableAdvancedColor => (_Value & 0x1) != 0;

        public DisplayConfigSetAdvancedColorState(LUID adapter, uint targetId, bool enableAdvancedColor) : this()
        {
            _Value = enableAdvancedColor ? 1u : 0u;
            _Header = new DisplayConfigDeviceInfoHeader(adapter, targetId, GetType(),
                DisplayConfigDeviceInfoType.SetAdvancedColorState);
        }
    }
}
