using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using WindowsDisplayAPI.Exceptions;
using WindowsDisplayAPI.Native;
using WindowsDisplayAPI.Native.DisplayConfig;
using WindowsDisplayAPI.Native.DisplayConfig.Structures;
using WindowsDisplayAPI.Native.Structures;

namespace WindowsDisplayAPI.DisplayConfig
{
    /// <summary>
    ///     Represents a display path target (Display Device)
    /// </summary>
    public class PathDisplayTarget : IEquatable<PathDisplayTarget>
    {
        /// <summary>
        ///     Creates a new PathDisplayTarget
        /// </summary>
        /// <param name="adapter">Display adapter</param>
        /// <param name="targetId">Display target identification</param>
        public PathDisplayTarget(PathDisplayAdapter adapter, uint targetId) : this(adapter, targetId, false)
        {
            IsAvailable = GetDisplayTargets().Any(target => target == this);
        }

        internal PathDisplayTarget(PathDisplayAdapter adapter, uint targetId, bool isAvailable)
        {
            Adapter = adapter;
            TargetId = targetId;
            IsAvailable = isAvailable;
        }

        /// <summary>
        ///     Gets the path display adapter
        /// </summary>
        public PathDisplayAdapter Adapter { get; }

        /// <summary>
        ///     Sets the display boot persistence for the target display device
        /// </summary>
        /// <exception cref="TargetNotAvailableException"></exception>
        /// <exception cref="Win32Exception"></exception>
        public bool BootPersistence
        {
            set
            {
                if (!IsAvailable)
                {
                    throw new TargetNotAvailableException("Extra information about the target is not available.",
                        Adapter.AdapterId, TargetId);
                }

                var targetPersistence = new DisplayConfigSetTargetPersistence(Adapter.AdapterId, TargetId, value);
                var result = DisplayConfigApi.DisplayConfigSetDeviceInfo(ref targetPersistence);

                if (result != Win32Status.Success)
                {
                    throw new Win32Exception((int) result);
                }
            }
        }

        /// <summary>
        ///     Gets the one-based instance number of this particular target only when the adapter has multiple targets of this
        ///     type. The connector instance is a consecutive one-based number that is unique within each adapter. If this is the
        ///     only target of this type on the adapter, this value is zero.
        /// </summary>
        /// <exception cref="Win32Exception">Error code can be retrieved from Win32Exception.NativeErrorCode property</exception>
        /// <exception cref="TargetNotAvailableException">The target is not available</exception>
        public int ConnectorInstance
        {
            get
            {
                if (!IsAvailable)
                {
                    throw new TargetNotAvailableException("Extra information about the target is not available.",
                        Adapter.AdapterId, TargetId);
                }

                var targetName = new DisplayConfigTargetDeviceName(Adapter.AdapterId, TargetId);
                var result = DisplayConfigApi.DisplayConfigGetDeviceInfo(ref targetName);

                if (result == Win32Status.Success)
                {
                    return (int) targetName.ConnectorInstance;
                }

                throw new Win32Exception((int) result);
            }
        }

        /// <summary>
        ///     Gets the display device path
        /// </summary>
        /// <exception cref="Win32Exception">Error code can be retrieved from Win32Exception.NativeErrorCode property</exception>
        /// <exception cref="TargetNotAvailableException">The target is not available</exception>
        public string DevicePath
        {
            get
            {
                if (!IsAvailable)
                {
                    throw new TargetNotAvailableException("Extra information about the target is not available.",
                        Adapter.AdapterId, TargetId);
                }

                var targetName = new DisplayConfigTargetDeviceName(Adapter.AdapterId, TargetId);
                var result = DisplayConfigApi.DisplayConfigGetDeviceInfo(ref targetName);

                if (result == Win32Status.Success)
                {
                    return targetName.MonitorDevicePath;
                }

                throw new Win32Exception((int) result);
            }
        }

        /// <summary>
        ///     Gets the display manufacture 3 character code from the display EDID manufacture identification
        /// </summary>
        /// <exception cref="TargetNotAvailableException">The target is not available</exception>
        /// <exception cref="Win32Exception">Error code can be retrieved from Win32Exception.NativeErrorCode property</exception>
        /// <exception cref="InvalidEDIDInformation">The EDID information does not contain this value</exception>
        public string EDIDManufactureCode
        {
            get
            {
                var edidCode = EDIDManufactureId;
                edidCode = ((edidCode & 0xff00) >> 8) | ((edidCode & 0x00ff) << 8);
                var byte1 = (byte) 'A' + (edidCode & 0x1f) - 1;
                var byte2 = (byte) 'A' + ((edidCode >> 5) & 0x1f) - 1;
                var byte3 = (byte) 'A' + ((edidCode >> 10) & 0x1f) - 1;

                return $"{Convert.ToChar(byte3)}{Convert.ToChar(byte2)}{Convert.ToChar(byte1)}";
            }
        }

        /// <summary>
        ///     Gets the display manufacture identification from the display EDID information
        /// </summary>
        /// <exception cref="TargetNotAvailableException">The target is not available</exception>
        /// <exception cref="Win32Exception">Error code can be retrieved from Win32Exception.NativeErrorCode property</exception>
        /// <exception cref="InvalidEDIDInformation">The EDID information does not contain this value</exception>
        public int EDIDManufactureId
        {
            get
            {
                if (!IsAvailable)
                {
                    throw new TargetNotAvailableException("Extra information about the target is not available.",
                        Adapter.AdapterId, TargetId);
                }

                var targetName = new DisplayConfigTargetDeviceName(Adapter.AdapterId, TargetId);
                var result = DisplayConfigApi.DisplayConfigGetDeviceInfo(ref targetName);

                if (result == Win32Status.Success)
                {
                    if (targetName.Flags.HasFlag(DisplayConfigTargetDeviceNameFlags.EDIDIdsValid))
                    {
                        return targetName.EDIDManufactureId;
                    }

                    throw new InvalidEDIDInformation("EDID does not contain necessary information.");
                }

                throw new Win32Exception((int) result);
            }
        }

        /// <summary>
        ///     Gets the display product identification from the display EDID information
        /// </summary>
        /// <exception cref="TargetNotAvailableException">The target is not available</exception>
        /// <exception cref="Win32Exception">Error code can be retrieved from Win32Exception.NativeErrorCode property</exception>
        /// <exception cref="InvalidEDIDInformation">The EDID information does not contain this value</exception>
        public int EDIDProductCode
        {
            get
            {
                if (!IsAvailable)
                {
                    throw new TargetNotAvailableException("Extra information about the target is not available.",
                        Adapter.AdapterId, TargetId);
                }

                var targetName = new DisplayConfigTargetDeviceName(Adapter.AdapterId, TargetId);
                var result = DisplayConfigApi.DisplayConfigGetDeviceInfo(ref targetName);

                if (result == Win32Status.Success)
                {
                    if (targetName.Flags.HasFlag(DisplayConfigTargetDeviceNameFlags.EDIDIdsValid))
                    {
                        return targetName.EDIDProductCodeId;
                    }

                    throw new InvalidEDIDInformation("EDID does not contain necessary information.");
                }

                throw new Win32Exception((int) result);
            }
        }

        /// <summary>
        ///     Gets the display friendly name from the display EDID information
        /// </summary>
        /// <exception cref="TargetNotAvailableException">The target is not available</exception>
        /// <exception cref="Win32Exception">Error code can be retrieved from Win32Exception.NativeErrorCode property</exception>
        public string FriendlyName
        {
            get
            {
                if (!IsAvailable)
                {
                    throw new TargetNotAvailableException("Extra information about the target is not available.",
                        Adapter.AdapterId, TargetId);
                }

                var targetName = new DisplayConfigTargetDeviceName(Adapter.AdapterId, TargetId);
                var result = DisplayConfigApi.DisplayConfigGetDeviceInfo(ref targetName);

                if (result == Win32Status.Success)
                {
                    return targetName.MonitorFriendlyDeviceName;
                }

                throw new Win32Exception((int) result);
            }
        }

        /// <summary>
        ///     Gets the display video output technology (connector type)
        /// </summary>
        /// <exception cref="TargetNotAvailableException">The target is not available</exception>
        /// <exception cref="Win32Exception">Error code can be retrieved from Win32Exception.NativeErrorCode property</exception>
        public DisplayConfigVideoOutputTechnology OutputTechnology
        {
            get
            {
                if (!IsAvailable)
                {
                    throw new TargetNotAvailableException("Extra information about the target is not available.",
                        Adapter.AdapterId, TargetId);
                }

                var targetName = new DisplayConfigTargetDeviceName(Adapter.AdapterId, TargetId);
                var result = DisplayConfigApi.DisplayConfigGetDeviceInfo(ref targetName);

                if (result == Win32Status.Success)
                {
                    return targetName.OutputTechnology;
                }

                throw new Win32Exception((int) result);
            }
        }

        /// <summary>
        ///     Gets a boolean value indicating whether the video output device connects internally to a display device
        /// </summary>
        public bool IsInternal
        {
            get
            {
                try
                {
                    var tech = OutputTechnology;
                    return tech == DisplayConfigVideoOutputTechnology.Internal ||
                           tech == DisplayConfigVideoOutputTechnology.DisplayPortEmbedded ||
                           tech == DisplayConfigVideoOutputTechnology.UDIEmbedded;
                }
                catch
                {
                    return false;
                }
            }
        }

        /// <summary>
        ///     Gets a boolean value indicating whether the video output is an external DisplayPort
        /// </summary>
        public bool IsExternalDisplayPort
        {
            get
            {
                try
                {
                    return OutputTechnology == DisplayConfigVideoOutputTechnology.DisplayPortExternal;
                }
                catch
                {
                    return false;
                }
            }
        }

        /// <summary>
        ///     Gets a boolean value indicating whether the video output is an indirect (virtual or USB-wired) display
        /// </summary>
        public bool IsIndirect
        {
            get
            {
                try
                {
                    var tech = OutputTechnology;
                    return tech == DisplayConfigVideoOutputTechnology.IndirectWired ||
                           tech == DisplayConfigVideoOutputTechnology.IndirectVirtual;
                }
                catch
                {
                    return false;
                }
            }
        }

        /// <summary>
        ///     Gets a boolean value indicating the device availability
        /// </summary>
        public bool IsAvailable { get; }

        /// <summary>
        ///     Gets the display device preferred resolution
        /// </summary>
        /// <exception cref="TargetNotAvailableException">The target is not available</exception>
        /// <exception cref="Win32Exception">Error code can be retrieved from Win32Exception.NativeErrorCode property</exception>
        public Size PreferredResolution
        {
            get
            {
                if (!IsAvailable)
                {
                    throw new TargetNotAvailableException("Extra information about the target is not available.",
                        Adapter.AdapterId, TargetId);
                }

                var targetPreferredMode = new DisplayConfigTargetPreferredMode(Adapter.AdapterId, TargetId);
                var result = DisplayConfigApi.DisplayConfigGetDeviceInfo(ref targetPreferredMode);

                if (result == Win32Status.Success)
                {
                    return new Size((int) targetPreferredMode.Width, (int) targetPreferredMode.Height);
                }

                throw new Win32Exception((int) result);
            }
        }

        /// <summary>
        ///     Gets the display device preferred signal information
        /// </summary>
        /// <exception cref="TargetNotAvailableException">The target is not available</exception>
        /// <exception cref="Win32Exception">Error code can be retrieved from Win32Exception.NativeErrorCode property</exception>
        public PathTargetSignalInfo PreferredSignalMode
        {
            get
            {
                if (!IsAvailable)
                {
                    throw new TargetNotAvailableException("Extra information about the target is not available.",
                        Adapter.AdapterId, TargetId);
                }

                var targetPreferredMode = new DisplayConfigTargetPreferredMode(Adapter.AdapterId, TargetId);
                var result = DisplayConfigApi.DisplayConfigGetDeviceInfo(ref targetPreferredMode);

                if (result == Win32Status.Success)
                {
                    return new PathTargetSignalInfo(targetPreferredMode.TargetMode.TargetVideoSignalInfo);
                }

                throw new Win32Exception((int) result);
            }
        }

        /// <summary>
        ///     Gets the target identification
        /// </summary>
        public uint TargetId { get; }

        /// <summary>
        ///     Gets or sets the device virtual resolution support
        /// </summary>
        /// <exception cref="TargetNotAvailableException">The target is not available</exception>
        /// <exception cref="Win32Exception">Error code can be retrieved from Win32Exception.NativeErrorCode property</exception>
        public bool VirtualResolutionSupport
        {
            get
            {
                if (!IsAvailable)
                {
                    throw new TargetNotAvailableException("Extra information about the target is not available.",
                        Adapter.AdapterId, TargetId);
                }

                var targetSupportVirtualResolution = new DisplayConfigSupportVirtualResolution(Adapter.AdapterId,
                    TargetId);
                var result = DisplayConfigApi.DisplayConfigGetDeviceInfo(ref targetSupportVirtualResolution);

                if (result == Win32Status.Success)
                {
                    return !targetSupportVirtualResolution.DisableMonitorVirtualResolution;
                }

                throw new Win32Exception((int) result);
            }
            set
            {
                if (!IsAvailable)
                {
                    throw new TargetNotAvailableException("Extra information about the target is not available.",
                        Adapter.AdapterId, TargetId);
                }

                var targetSupportVirtualResolution = new DisplayConfigSupportVirtualResolution(Adapter.AdapterId,
                    TargetId, !value);
                var result = DisplayConfigApi.DisplayConfigSetDeviceInfo(ref targetSupportVirtualResolution);

                if (result != Win32Status.Success)
                {
                    throw new Win32Exception((int) result);
                }
            }
        }

        /// <summary>
        ///     Gets the Advanced Color (HDR / WCG) information for this display target.
        /// </summary>
        /// <returns>An instance of <see cref="DisplayAdvancedColorInfo"/> containing the capabilities and current state.</returns>
        /// <exception cref="TargetNotAvailableException">The target is not available.</exception>
        /// <exception cref="Win32Exception">Win32 error occurred querying device info.</exception>
        public DisplayAdvancedColorInfo GetAdvancedColorInfo()
        {
            if (!IsAvailable)
            {
                throw new TargetNotAvailableException("Extra information about the target is not available.",
                    Adapter.AdapterId, TargetId);
            }

            var colorInfo2 = new DisplayConfigGetAdvancedColorInfo2(Adapter.AdapterId, TargetId);
            var result2 = DisplayConfigApi.DisplayConfigGetDeviceInfo(ref colorInfo2);

            if (result2 == Win32Status.Success)
            {
                var advancedColorSupported = colorInfo2.AdvancedColorSupported;
                var advancedColorEnabled = colorInfo2.AdvancedColorEnabled;
                var hdrSupported = colorInfo2.HighDynamicRangeSupported;
                var hdrEnabled = colorInfo2.HighDynamicRangeUserEnabled && colorInfo2.ActiveColorMode == DisplayConfigAdvancedColorMode.Hdr;
                var acmSupported = colorInfo2.AutoColorManagementSupported;
                var acmEnabled = colorInfo2.AutoColorManagementEnabled;
                var wideColorEnforced = colorInfo2.WideColorGamutSupported;
                var advancedColorForceDisabled = false;

                return new DisplayAdvancedColorInfo(
                    advancedColorSupported,
                    advancedColorEnabled,
                    wideColorEnforced,
                    advancedColorForceDisabled,
                    colorInfo2.ColorEncoding,
                    colorInfo2.BitsPerColorChannel,
                    colorInfo2.ActiveColorMode,
                    hdrSupported,
                    hdrEnabled,
                    acmSupported,
                    acmEnabled
                );
            }

            var colorInfo = new DisplayConfigGetAdvancedColorInfo(Adapter.AdapterId, TargetId);
            var result = DisplayConfigApi.DisplayConfigGetDeviceInfo(ref colorInfo);

            if (result == Win32Status.Success)
            {
                var activeColorMode = colorInfo.AdvancedColorEnabled
                    ? DisplayConfigAdvancedColorMode.Hdr
                    : DisplayConfigAdvancedColorMode.Sdr;

                var acmSupported = colorInfo.WideColorEnforced;
                var acmEnabled = acmSupported && colorInfo.AdvancedColorEnabled;
                var hdrSupported = colorInfo.AdvancedColorSupported && !acmSupported;
                var hdrEnabled = hdrSupported && colorInfo.AdvancedColorEnabled;

                return new DisplayAdvancedColorInfo(
                    colorInfo.AdvancedColorSupported,
                    colorInfo.AdvancedColorEnabled,
                    colorInfo.WideColorEnforced,
                    colorInfo.AdvancedColorForceDisabled,
                    colorInfo.ColorEncoding,
                    colorInfo.BitsPerColorChannel,
                    activeColorMode,
                    hdrSupported,
                    hdrEnabled,
                    acmSupported,
                    acmEnabled
                );
            }

            throw new Win32Exception((int) result);
        }

        /// <summary>
        ///     Sets the Advanced Color state for this display target.
        /// </summary>
        /// <param name="enable">true to enable Advanced Color; false to disable.</param>
        /// <exception cref="TargetNotAvailableException">The target is not available.</exception>
        /// <exception cref="Win32Exception">Win32 error occurred setting device info.</exception>
        public void SetAdvancedColorState(bool enable)
        {
            if (!IsAvailable)
            {
                throw new TargetNotAvailableException("Extra information about the target is not available.",
                    Adapter.AdapterId, TargetId);
            }

            var setColorState = new DisplayConfigSetAdvancedColorState(Adapter.AdapterId, TargetId, enable);
            var resultColor = DisplayConfigApi.DisplayConfigSetDeviceInfo(ref setColorState);

            if (resultColor != Win32Status.Success)
            {
                throw new Win32Exception((int) resultColor);
            }
        }

        /// <summary>
        ///     Sets the HDR state for this display target.
        /// </summary>
        /// <param name="enable">true to enable HDR; false to disable.</param>
        /// <exception cref="TargetNotAvailableException">The target is not available.</exception>
        /// <exception cref="Win32Exception">Win32 error occurred setting device info.</exception>
        public void SetHdrState(bool enable)
        {
            if (!IsAvailable)
            {
                throw new TargetNotAvailableException("Extra information about the target is not available.",
                    Adapter.AdapterId, TargetId);
            }

            var setHdrState = new DisplayConfigSetHdrState(Adapter.AdapterId, TargetId, enable);
            var resultHdr = DisplayConfigApi.DisplayConfigSetDeviceInfo(ref setHdrState);

            if (resultHdr == Win32Status.Success)
            {
                return;
            }

            SetAdvancedColorState(enable);
        }

        /// <summary>
        ///     Sets the Auto Color Management (ACM) / WCG state for this display target.
        /// </summary>
        /// <param name="enable">true to enable ACM / WCG; false to disable.</param>
        /// <exception cref="TargetNotAvailableException">The target is not available.</exception>
        /// <exception cref="Win32Exception">Win32 error occurred setting device info.</exception>
        public void SetWcgState(bool enable)
        {
            if (!IsAvailable)
            {
                throw new TargetNotAvailableException("Extra information about the target is not available.",
                    Adapter.AdapterId, TargetId);
            }

            var setWcgState = new DisplayConfigSetWcgState(Adapter.AdapterId, TargetId, enable);
            var resultWcg = DisplayConfigApi.DisplayConfigSetDeviceInfo(ref setWcgState);

            if (resultWcg == Win32Status.Success)
            {
                return;
            }

            SetAdvancedColorState(enable);
        }

        /// <summary>
        ///     Gets the current SDR white level for this display target (raw value in 1/1000th of 80 nits).
        /// </summary>
        /// <returns>The raw SDR white level.</returns>
        /// <exception cref="TargetNotAvailableException">The target is not available.</exception>
        /// <exception cref="Win32Exception">Win32 error occurred querying device info.</exception>
        public uint GetSdrWhiteLevel()
        {
            if (!IsAvailable)
            {
                throw new TargetNotAvailableException("Extra information about the target is not available.",
                    Adapter.AdapterId, TargetId);
            }

            var sdrWhiteLevel = new DisplayConfigGetSdrWhiteLevel(Adapter.AdapterId, TargetId);
            var result = DisplayConfigApi.DisplayConfigGetDeviceInfo(ref sdrWhiteLevel);

            if (result == Win32Status.Success)
            {
                return sdrWhiteLevel.SdrWhiteLevel;
            }

            throw new Win32Exception((int) result);
        }

        /// <summary>
        ///     Gets the current SDR white level in nits (calculated as (SdrWhiteLevel / 1000.0) * 80.0).
        /// </summary>
        /// <returns>The SDR white level in nits.</returns>
        public float GetSdrWhiteLevelInNits()
        {
            var raw = GetSdrWhiteLevel();
            return (raw / 1000.0f) * 80.0f;
        }

        /// <inheritdoc />
        public bool Equals(PathDisplayTarget other)
        {
            if (ReferenceEquals(null, other))
            {
                return false;
            }

            if (ReferenceEquals(this, other))
            {
                return true;
            }

            return Adapter == other.Adapter && TargetId == other.TargetId;
        }

        /// <summary>
        ///     Retrieving a list of all display targets from the currently active and inactive paths
        /// </summary>
        /// <returns>An array of PathDisplayTarget instances</returns>
        public static PathDisplayTarget[] GetDisplayTargets()
        {
            var targets = new Dictionary<Tuple<LUID, uint>, PathDisplayTarget>();

            foreach (var pathInfo in PathInfo.GetAllPaths())
            foreach (var pathTargetInfo in pathInfo.TargetsInfo.Where(info => info.DisplayTarget.IsAvailable))
            {
                var key = Tuple.Create(
                    pathTargetInfo.DisplayTarget.Adapter.AdapterId,
                    pathTargetInfo.DisplayTarget.TargetId
                );

                if (!pathTargetInfo.DisplayTarget.Adapter.IsInvalid && !targets.ContainsKey(key))
                {
                    targets.Add(key, pathTargetInfo.DisplayTarget);
                }
            }

            return targets.Values.ToArray();
        }

        /// <summary>
        ///     Checks for equality of two PathDisplayTarget instances
        /// </summary>
        /// <param name="left">The first instance</param>
        /// <param name="right">The second instance</param>
        /// <returns>true if both instances are equal, otherwise false</returns>
        public static bool operator ==(PathDisplayTarget left, PathDisplayTarget right)
        {
            return Equals(left, right) || left?.Equals(right) == true;
        }

        /// <summary>
        ///     Checks for inequality of two PathDisplayTarget instances
        /// </summary>
        /// <param name="left">The first instance</param>
        /// <param name="right">The second instance</param>
        /// <returns>true if both instances are not equal, otherwise false</returns>
        public static bool operator !=(PathDisplayTarget left, PathDisplayTarget right)
        {
            return !(left == right);
        }

        /// <inheritdoc />
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
            {
                return false;
            }

            if (ReferenceEquals(this, obj))
            {
                return true;
            }

            return obj.GetType() == GetType() && Equals((PathDisplayTarget) obj);
        }

        /// <inheritdoc />
        public override int GetHashCode()
        {
            unchecked
            {
                return ((Adapter != null ? Adapter.GetHashCode() : 0) * 397) ^ (int) TargetId;
            }
        }

        /// <inheritdoc />
        public override string ToString()
        {
            return FriendlyName;
        }

#if !NETSTANDARD
        /// <summary>
        ///     Opens the registry key of the Windows PnP manager for this display target
        /// </summary>
        /// <returns>A RegistryKey instance for successful call, otherwise null</returns>
        public Microsoft.Win32.RegistryKey OpenDevicePnPKey()
        {
            if (string.IsNullOrWhiteSpace(DevicePath)) {
                return null;
            }

            var path = DevicePath;
            if (path.StartsWith("\\\\?\\"))
            {
                path = path.Substring(4).Replace("#", "\\");
                if (path.EndsWith("}"))
                {
                    var guidIndex = path.LastIndexOf("{", StringComparison.InvariantCulture);
                    if (guidIndex > 0) {
                        path = path.Substring(0, guidIndex);
                    }
                }
            }

            return Microsoft.Win32.Registry.LocalMachine.OpenSubKey(
                "SYSTEM\\CurrentControlSet\\Enum\\" + path,
                Microsoft.Win32.RegistryKeyPermissionCheck.ReadSubTree
            );
        }
#endif

        /// <summary>
        ///     Returns the corresponding <see cref="DisplayDevice"/> instance
        /// </summary>
        /// <returns>An instance of <see cref="DisplayDevice"/>, or null</returns>
        public DisplayDevice ToDisplayDevice()
        {
            return
                DisplayAdapter.GetDisplayAdapters()
                    .SelectMany(adapter => adapter.GetDisplayDevices())
                    .FirstOrDefault(device => device.DevicePath.Equals(DevicePath));
        }

        /// <summary>
        ///     Calculates the dynamic refresh rate low (idle) frequency for a given maximum frequency and available frequencies.
        /// </summary>
        /// <param name="maxFrequency">The maximum refresh rate of the display.</param>
        /// <param name="availableFrequencies">The collection of available frequencies supported by the display.</param>
        /// <returns>The low frequency to pair with the maximum frequency, or 0 if unsupported.</returns>
        public static int GetDynamicLowFrequency(int maxFrequency, IEnumerable<int> availableFrequencies)
        {
            if (maxFrequency < 120 || availableFrequencies == null)
            {
                return 0;
            }

            var frequencies = availableFrequencies as int[] ?? availableFrequencies.ToArray();

            var halfRate = maxFrequency / 2;
            var halfRateMatch = frequencies.FirstOrDefault(f => Math.Abs(f - halfRate) <= 1);
            if (halfRateMatch > 0)
            {
                return halfRateMatch;
            }

            if (maxFrequency % 60 == 0 || maxFrequency % 2 != 0)
            {
                var base60Match = frequencies.FirstOrDefault(f => Math.Abs(f - 60) <= 1);
                if (base60Match > 0)
                {
                    return base60Match;
                }
            }

            return 0;
        }

#if WINDOWS10_0_18362_0_OR_GREATER || NET9_0_WINDOWS10_0_26100_0_OR_GREATER
        private static readonly Guid DynamicRefreshRatePropertyGuid = new("D2D490B1-4861-4D69-912C-EEE5590E1980");
#endif

        /// <summary>
        ///     Gets a boolean value indicating whether Dynamic Refresh Rate (DRR) is supported by this target.
        /// </summary>
        public bool IsDynamicRefreshRateSupported
        {
            get
            {
                if (!IsAvailable)
                {
                    return false;
                }

#if WINDOWS10_0_18362_0_OR_GREATER || NET9_0_WINDOWS10_0_26100_0_OR_GREATER
                try
                {
                    using var manager = Windows.Devices.Display.Core.DisplayManager.Create(Windows.Devices.Display.Core.DisplayManagerOptions.None);
                    var result = manager.TryReadCurrentStateForAllTargets();
                    if (result.ErrorCode == Windows.Devices.Display.Core.DisplayManagerResult.Success && result.State != null)
                    {
                        var state = result.State;

                        foreach (var target in state.Targets)
                        {
                            if (!target.IsConnected || target.AdapterRelativeId != TargetId)
                            {
                                continue;
                            }

                            if (target.Adapter.Id.LowPart != Adapter.AdapterId.LowPart ||
                                target.Adapter.Id.HighPart != Adapter.AdapterId.HighPart)
                            {
                                continue;
                            }

                            var path = state.GetPathForTarget(target);
                            if (path == null)
                            {
                                continue;
                            }

                            var modes = path.FindModes(Windows.Devices.Display.Core.DisplayModeQueryOptions.None);

                            foreach (var mode in modes)
                            {
                                if (mode.IsInterlaced)
                                {
                                    continue;
                                }

                                var presDenom = mode.PresentationRate.VerticalSyncRate.Denominator;
                                var physDenom = mode.PhysicalPresentationRate.VerticalSyncRate.Denominator;
                                if (presDenom == 0 || physDenom == 0)
                                {
                                    continue;
                                }

                                var presRate = (double)mode.PresentationRate.VerticalSyncRate.Numerator / presDenom;
                                var physRate = (double)mode.PhysicalPresentationRate.VerticalSyncRate.Numerator / physDenom;

                                if (Math.Abs(physRate - 2.0 * presRate) < 0.1 || (Math.Abs(presRate - 60.0) < 0.5 && physRate >= 119.0))
                                {
                                    return true;
                                }

                                if (physRate >= 119.0 && mode.Properties.TryGetValue(DynamicRefreshRatePropertyGuid, out var prop) && prop != null)
                                {
                                    return true;
                                }
                            }
                        }
                    }
                }
                catch
                {
                }
#endif

                try
                {
                    var device = ToDisplayDevice();
                    if (device?.DisplayScreen != null)
                    {
                        var possibleSettings = device.DisplayScreen.GetPossibleSettings();
                        if (possibleSettings != null)
                        {
                            var currentSettings = device.DisplayScreen.CurrentSetting;
                            var matchingFreqs = possibleSettings
                                .Where(s => s.Resolution == currentSettings.Resolution && !s.IsInterlaced)
                                .Select(s => s.Frequency)
                                .Distinct()
                                .ToArray();

                            if (matchingFreqs.Length > 0)
                            {
                                var maxFreq = matchingFreqs.Max();
                                return GetDynamicLowFrequency(maxFreq, matchingFreqs) > 0;
                            }
                        }
                    }
                }
                catch
                {
                }

                return false;
            }
        }
    }
}