namespace WindowsDisplayAPI.Native.DisplayConfig
{
    /// <summary>
    ///     Possible advanced color encoding formats.
    /// </summary>
    public enum DisplayConfigColorEncoding : uint
    {
        /// <summary>
        ///     RGB color encoding.
        /// </summary>
        Rgb = 0,

        /// <summary>
        ///     YCbCr 4:4:4 color encoding.
        /// </summary>
        YCbCr444 = 1,

        /// <summary>
        ///     YCbCr 4:2:2 color encoding.
        /// </summary>
        YCbCr422 = 2,

        /// <summary>
        ///     YCbCr 4:2:0 color encoding.
        /// </summary>
        YCbCr420 = 3,

        /// <summary>
        ///     Intensity color encoding.
        /// </summary>
        Intensity = 4
    }
}
