namespace WindowsDisplayAPI.Native.DisplayConfig
{
    /// <summary>
    ///     Specifies the active advanced color mode for a display target.
    /// </summary>
    public enum DisplayConfigAdvancedColorMode : uint
    {
        /// <summary>
        ///     Standard Dynamic Range (SDR) mode.
        /// </summary>
        Sdr = 0,

        /// <summary>
        ///     Wide Color Gamut (WCG) mode.
        /// </summary>
        Wcg = 1,

        /// <summary>
        ///     High Dynamic Range (HDR) mode.
        /// </summary>
        Hdr = 2
    }
}
