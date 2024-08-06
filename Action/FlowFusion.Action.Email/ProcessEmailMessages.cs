using FlowFusion.Action.Email.ProcessEmailMessagesBase;
using FlowFusion.Action.Main;
using FlowFusion.Action.Main.Action;
using MailKit;
using MailKit.Net.Imap;

namespace FlowFusion.Action.Email;

public class ProcessEmailMessages : IAction //XXXXXXXXXXXX
{
    public string Name => "Process email messages";

    public ActionInput EmailMessagesToProcess { get; set; }
    public Operation Operation { get; set; }
    public ActionInput MailFolder { get; set; }
    public ActionInput ImapServer { get; set; }
    public ActionInput Port { get; set; }
    public bool EnableSsl { get; set; }
    public ActionInput Username { get; set; }
    public ActionInput Password { get; set; }
    public bool AcceptUntrustedCertificates { get; set; }

    public ProcessEmailMessages()
    {
        EmailMessagesToProcess = new ActionInput();
        Operation = Operation.MoveEmailMessagesToMailFolder;
        MailFolder = new ActionInput();
        ImapServer = new ActionInput();
        Port = new ActionInput();
        EnableSsl = true;
        Username = new ActionInput();
        Password = new ActionInput();
        AcceptUntrustedCertificates = false;
    }

    public async Task Execute(SandBox sandBox)
    {
        var emailMessagesToProcessValues = await sandBox.EvaluateActionInput<List<string>>(EmailMessagesToProcess);
        var mailFolderValue = await sandBox.EvaluateActionInput<string>(MailFolder);
        var imapServerValue = await sandBox.EvaluateActionInput<string>(ImapServer);
        var portValue = await sandBox.EvaluateActionInput<int>(Port);
        var usernameValue = await sandBox.EvaluateActionInput<string>(Username);
        var passwordValue = await sandBox.EvaluateActionInput<string>(Password);

        using (var client = new ImapClient())
        {
            await client.ConnectAsync(imapServerValue, portValue, EnableSsl);
            await client.AuthenticateAsync(usernameValue, passwordValue);

            foreach (var uid in emailMessagesToProcessValues)
            {
                var mailbox = client.GetFolder(SpecialFolder.All);
                var mailFolder = await client.GetFolderAsync(mailFolderValue);

                switch (Operation)
                {
                    case Operation.DeleteEmailMessagesFromServer:
                        await mailbox.AddFlagsAsync(new UniqueId(UInt32.Parse(uid)), MessageFlags.Deleted, true);
                        break;
                    case Operation.MakeEmailMessagesAsUnread:
                        await mailbox.RemoveFlagsAsync(new UniqueId(UInt32.Parse(uid)), MessageFlags.Seen, true);
                        break;
                    case Operation.MarkEmailMessagesAsUnreadAndMoveToMailFolder:
                        await mailbox.RemoveFlagsAsync(new UniqueId(UInt32.Parse(uid)), MessageFlags.Seen, true);
                        await mailbox.MoveToAsync(new UniqueId(UInt32.Parse(uid)), mailFolder);
                        break;
                    case Operation.MoveEmailMessagesToMailFolder:
                        await mailbox.MoveToAsync(new UniqueId(UInt32.Parse(uid)), mailFolder);
                        break;
                }
            }
        }
    }
}