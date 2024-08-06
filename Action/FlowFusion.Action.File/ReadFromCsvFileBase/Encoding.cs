namespace FlowFusion.Action.File.ReadFromCsvFileBase;

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
    /// UTF-8
    /// </summary>
    Utf8
}