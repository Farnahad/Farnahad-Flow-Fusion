namespace FarnahadFlowFusion.Action.Email.ProcessEmailMessagesBase;

public enum Operation
{
    /// <summary>
    /// Delete email messages from server
    /// </summary>
    DeleteEmailMessagesFromServer,
    /// <summary>
    /// Make email messages as unread
    /// </summary>
    MakeEmailMessagesAsUnread,
    /// <summary>
    /// Mark email messages as unread and move to mail folder
    /// </summary>
    MarkEmailMessagesAsUnreadAndMoveToMailFolder,
    /// <summary>
    /// Move email messages to mail folder
    /// </summary>
    MoveEmailMessagesToMailFolder
}