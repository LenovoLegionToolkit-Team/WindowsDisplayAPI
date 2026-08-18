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
        ///     Initializes a new instance of the <see cref="DisplayAdvancedColorInfo"/> struct.
        /// </summary>
        /// <param name="advancedColorSupported">Whether Advanced Color is supported.</param>
        /// <param name="advancedColorEnabled">Whether Advanced Color is enabled.</param>
        /// <param name="wideColorEnforced">Whether Wide Color is enforced.</param>
        /// <param name="advancedColorForceDisabled">Whether Advanced Color is force disabled.</param>
        /// <param name="colorEncoding">Active color encoding.</param>
        /// <param name="bitsPerColorChannel">Bits per color channel.</param>
        /// <param name="activeColorMode">Active color mode.</param>
        public DisplayAdvancedColorInfo(
            bool advancedColorSupported,
            bool advancedColorEnabled,
            bool wideColorEnforced,
            bool advancedColorForceDisabled,
            DisplayConfigColorEncoding colorEncoding = DisplayConfigColorEncoding.Rgb,
            uint bitsPerColorChannel = 8,
            DisplayConfigAdvancedColorMode activeColorMode = DisplayConfigAdvancedColorMode.Sdr)
        {
            AdvancedColorSupported = advancedColorSupported;
            AdvancedColorEnabled = advancedColorEnabled;
            WideColorEnforced = wideColorEnforced;
            AdvancedColorForceDisabled = advancedColorForceDisabled;
            ColorEncoding = colorEncoding;
            BitsPerColorChannel = bitsPerColorChannel;
            ActiveColorMode = activeColorMode;
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
                   ActiveColorMode == other.ActiveColorMode;
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
            return $"Supported: {AdvancedColorSupported}, Enabled: {AdvancedColorEnabled}, Mode: {ActiveColorMode}, {BitsPerColorChannel}bpc {ColorEncoding}";
        }
    }
}
