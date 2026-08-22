using System;
using WindowsDisplayAPI.Native.DisplayConfig;

namespace WindowsDisplayAPI.DisplayConfig
{
    /// <summary>
    ///     Contains Advanced Color and High Dynamic Range (HDR) information for a display target.
    /// </summary>
    public struct DisplayAdvancedColorInfo : IEquatable<DisplayAdvancedColorInfo>
    {
        /// <summary>
        ///     Gets whether Advanced Color / HDR is supported by the display device.
        /// </summary>
        public bool AdvancedColorSupported { get; }

        /// <summary>
        ///     Gets whether Advanced Color / HDR is currently enabled.
        /// </summary>
        public bool AdvancedColorEnabled { get; }

        /// <summary>
        ///     Gets whether Wide Color Gamut (WCG) is enforced or limited by policy.
        /// </summary>
        public bool WideColorEnforced { get; }

        /// <summary>
        ///     Gets whether Advanced Color is force disabled.
        /// </summary>
        public bool AdvancedColorForceDisabled { get; }

        /// <summary>
        ///     Gets the active color encoding format.
        /// </summary>
        public DisplayConfigColorEncoding ColorEncoding { get; }

        /// <summary>
        ///     Gets the number of bits per color channel.
        /// </summary>
        public uint BitsPerColorChannel { get; }

        /// <summary>
        ///     Gets the active color mode (SDR, WCG, HDR).
        /// </summary>
        public DisplayConfigAdvancedColorMode ActiveColorMode { get; }

        /// <summary>
        ///     Gets whether High Dynamic Range (HDR) is supported by the display device.
        /// </summary>
        public bool HighDynamicRangeSupported { get; }

        /// <summary>
        ///     Gets whether High Dynamic Range (HDR) is currently enabled.
        /// </summary>
        public bool HighDynamicRangeEnabled { get; }

        /// <summary>
        ///     Gets whether Auto Color Management (ACM) / Wide Color Gamut is supported by the display device.
        /// </summary>
        public bool AutoColorManagementSupported { get; }

        /// <summary>
        ///     Gets whether Auto Color Management (ACM) / Wide Color Gamut is currently enabled.
        /// </summary>
        public bool AutoColorManagementEnabled { get; }

        /// <summary>
        ///     Initializes a new instance of the <see cref="DisplayAdvancedColorInfo"/> struct.
        /// </summary>
        /// <param name="advancedColorSupported">Whether Advanced Color is supported.</param>
        /// <param name="advancedColorEnabled">Whether Advanced Color is enabled.</param>
        /// <param name="wideColorEnforced">Whether Wide Color is enforced.</param>
        /// <param name="advancedColorForceDisabled">Whether Advanced Color is force disabled.</param>
        /// <param name="colorEncoding">Active color encoding.</param>
        /// <param name="bitsPerColorChannel">Bits per color channel.</param>
        /// <param name="activeColorMode">Active color mode.</param>
        /// <param name="highDynamicRangeSupported">Whether HDR is supported.</param>
        /// <param name="highDynamicRangeEnabled">Whether HDR is enabled.</param>
        /// <param name="autoColorManagementSupported">Whether Auto Color Management is supported.</param>
        /// <param name="autoColorManagementEnabled">Whether Auto Color Management is enabled.</param>
        public DisplayAdvancedColorInfo(
            bool advancedColorSupported,
            bool advancedColorEnabled,
            bool wideColorEnforced,
            bool advancedColorForceDisabled,
            DisplayConfigColorEncoding colorEncoding = DisplayConfigColorEncoding.Rgb,
            uint bitsPerColorChannel = 8,
            DisplayConfigAdvancedColorMode activeColorMode = DisplayConfigAdvancedColorMode.Sdr,
            bool highDynamicRangeSupported = false,
            bool highDynamicRangeEnabled = false,
            bool autoColorManagementSupported = false,
            bool autoColorManagementEnabled = false)
        {
            AdvancedColorSupported = advancedColorSupported;
            AdvancedColorEnabled = advancedColorEnabled;
            WideColorEnforced = wideColorEnforced;
            AdvancedColorForceDisabled = advancedColorForceDisabled;
            ColorEncoding = colorEncoding;
            BitsPerColorChannel = bitsPerColorChannel;
            ActiveColorMode = activeColorMode;
            HighDynamicRangeSupported = highDynamicRangeSupported;
            HighDynamicRangeEnabled = highDynamicRangeEnabled;
            AutoColorManagementSupported = autoColorManagementSupported;
            AutoColorManagementEnabled = autoColorManagementEnabled;
        }

        /// <inheritdoc />
        public bool Equals(DisplayAdvancedColorInfo other)
        {
            return AdvancedColorSupported == other.AdvancedColorSupported &&
                   AdvancedColorEnabled == other.AdvancedColorEnabled &&
                   WideColorEnforced == other.WideColorEnforced &&
                   AdvancedColorForceDisabled == other.AdvancedColorForceDisabled &&
                   ColorEncoding == other.ColorEncoding &&
                   BitsPerColorChannel == other.BitsPerColorChannel &&
                   ActiveColorMode == other.ActiveColorMode &&
                   HighDynamicRangeSupported == other.HighDynamicRangeSupported &&
                   HighDynamicRangeEnabled == other.HighDynamicRangeEnabled &&
                   AutoColorManagementSupported == other.AutoColorManagementSupported &&
                   AutoColorManagementEnabled == other.AutoColorManagementEnabled;
        }

        /// <inheritdoc />
        public override bool Equals(object obj)
        {
            return obj is DisplayAdvancedColorInfo other && Equals(other);
        }

        /// <inheritdoc />
        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = AdvancedColorSupported.GetHashCode();
                hashCode = (hashCode * 397) ^ AdvancedColorEnabled.GetHashCode();
                hashCode = (hashCode * 397) ^ WideColorEnforced.GetHashCode();
                hashCode = (hashCode * 397) ^ AdvancedColorForceDisabled.GetHashCode();
                hashCode = (hashCode * 397) ^ (int) ColorEncoding;
                hashCode = (hashCode * 397) ^ (int) BitsPerColorChannel;
                hashCode = (hashCode * 397) ^ (int) ActiveColorMode;
                hashCode = (hashCode * 397) ^ HighDynamicRangeSupported.GetHashCode();
                hashCode = (hashCode * 397) ^ HighDynamicRangeEnabled.GetHashCode();
                hashCode = (hashCode * 397) ^ AutoColorManagementSupported.GetHashCode();
                hashCode = (hashCode * 397) ^ AutoColorManagementEnabled.GetHashCode();
                return hashCode;
            }
        }

        /// <summary>
        ///     Checks for equality of two DisplayAdvancedColorInfo instances.
        /// </summary>
        public static bool operator ==(DisplayAdvancedColorInfo left, DisplayAdvancedColorInfo right)
        {
            return left.Equals(right);
        }

        /// <summary>
        ///     Checks for inequality of two DisplayAdvancedColorInfo instances.
        /// </summary>
        public static bool operator !=(DisplayAdvancedColorInfo left, DisplayAdvancedColorInfo right)
        {
            return !left.Equals(right);
        }

        /// <inheritdoc />
        public override string ToString()
        {
            return $"Supported: {AdvancedColorSupported}, Enabled: {AdvancedColorEnabled}, HDR: {HighDynamicRangeSupported} (Enabled: {HighDynamicRangeEnabled}), ACM: {AutoColorManagementSupported} (Enabled: {AutoColorManagementEnabled}), Mode: {ActiveColorMode}, {BitsPerColorChannel}bpc {ColorEncoding}";
        }
    }
}
