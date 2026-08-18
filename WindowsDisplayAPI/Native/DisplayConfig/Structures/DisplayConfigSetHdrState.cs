using System.Runtime.InteropServices;
using WindowsDisplayAPI.Native.Structures;

namespace WindowsDisplayAPI.Native.DisplayConfig.Structures
{
    [StructLayout(LayoutKind.Sequential)]
    internal struct DisplayConfigSetHdrState
    {
        [MarshalAs(UnmanagedType.Struct)] private readonly DisplayConfigDeviceInfoHeader _Header;
        [MarshalAs(UnmanagedType.U4)] private readonly uint _Value;

        public bool EnableHdr => (_Value & 0x1) != 0;

        public DisplayConfigSetHdrState(LUID adapter, uint targetId, bool enableHdr) : this()
        {
            _Value = enableHdr ? 1u : 0u;
            _Header = new DisplayConfigDeviceInfoHeader(adapter, targetId, GetType(),
                DisplayConfigDeviceInfoType.SetHdrState);
        }
    }
}
