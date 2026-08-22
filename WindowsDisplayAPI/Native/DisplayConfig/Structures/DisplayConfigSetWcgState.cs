using System.Runtime.InteropServices;
using WindowsDisplayAPI.Native.Structures;

namespace WindowsDisplayAPI.Native.DisplayConfig.Structures
{
    [StructLayout(LayoutKind.Sequential)]
    internal struct DisplayConfigSetWcgState
    {
        [MarshalAs(UnmanagedType.Struct)] private readonly DisplayConfigDeviceInfoHeader _Header;
        [MarshalAs(UnmanagedType.U4)] private readonly uint _Value;

        public bool EnableWcg => (_Value & 0x1) != 0;

        public DisplayConfigSetWcgState(LUID adapter, uint targetId, bool enableWcg) : this()
        {
            _Value = enableWcg ? 1u : 0u;
            _Header = new DisplayConfigDeviceInfoHeader(adapter, targetId, GetType(),
                DisplayConfigDeviceInfoType.SetWcgState);
        }
    }
}
