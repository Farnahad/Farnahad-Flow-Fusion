﻿namespace FlowFusion.Service.File.File.Base;

public enum RenameSchema
{
    /// <summary>
    /// Add datetime
    /// </summary>
    AddDatetime,
    /// <summary>
    /// Add text
    /// </summary>
    AddText,
    /// <summary>
    /// Change extension
    /// </summary>
    ChangeExtension,
    /// <summary>
    /// Make sequential
    /// </summary>
    MakeSequential,
    /// <summary>
    /// Remove text
    /// </summary>
    RemoveText,
    /// <summary>
    /// Replace text
    /// </summary>
    ReplaceText,
    /// <summary>
    /// Set new name
    /// </summary>
    SetNewName
}