namespace FarnahadFlowFusion.Action.File.WriteTextToFileBase;

public enum Encoding
{
    /// <summary>
    /// ASCII
    /// </summary>
    Ascii,
    /// <summary>
    /// System default
    /// </summary>
    SystemDefault,
    /// <summary>
    /// Unicode
    /// </summary>
    Unicode,
    /// <summary>
    /// Unicode (big-endian)
    /// </summary>
    UnicodeBigEndian,
    /// <summary>
    /// Unicode (no byte order mark)
    /// </summary>
    UnicodeNoByteOrderMark,
    /// <summary>
    /// UTF-8
    /// </summary>
    Utf8,
    /// <summary>
    /// UTF-8 (no byte order mark)
    /// </summary>
    Utf8NoByteOrderMark
}