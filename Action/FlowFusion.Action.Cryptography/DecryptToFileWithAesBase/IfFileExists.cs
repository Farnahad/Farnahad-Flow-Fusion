namespace FlowFusion.Action.Cryptography.DecryptToFileWithAesBase;

public enum IfFileExists
{
    /// <summary>
    /// Add sequential suffix
    /// </summary>
    AddSequentialSuffix,
    /// <summary>
    /// Don't decrypt to file
    /// </summary>
    DoNotDecryptToFile,
    /// <summary>
    /// Overwrite
    /// </summary>
    Overwrite
}