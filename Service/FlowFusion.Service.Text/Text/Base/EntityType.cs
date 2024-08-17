namespace FlowFusion.Service.Text.Text.Base;

public enum EntityType
{
    Currency,
    Datetime,
    Dimension,
    Email,
    Guid,
    /// <summary>
    /// Hash tag
    /// </summary>
    HashTag,
    /// <summary>
    /// IP address
    /// </summary>
    IpAddress,
    Mention,
    Number,
    /// <summary>
    /// Number range
    /// </summary>
    NumberRange,
    Ordinal,
    Percentage,
    /// <summary>
    /// Phone number
    /// </summary>
    PhoneNumber,
    QuotedText,
    /// <summary>
    /// Quoted text
    /// </summary>
    Temperature,
    Url
}