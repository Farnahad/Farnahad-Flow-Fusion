﻿namespace FlowFusion.Service.File.File.Base;

public enum ReadEncoding
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