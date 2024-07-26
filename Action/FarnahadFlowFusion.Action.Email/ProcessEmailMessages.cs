using FarnahadFlowFusion.Action.Email.ProcessEmailMessagesBase;
using FarnahadFlowFusion.Action.Main;
using FarnahadFlowFusion.Service.Scripting.CSharp;
using MailKit;
using MailKit.Net.Imap;
using UniqueId = System.Xml.UniqueId;

namespace FarnahadFlowFusion.Action.Email;

public class ProcessEmailMessages : IAction
{
    private readonly CSharpService _cSharpService;

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
        _cSharpService = new CSharpService();

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
        var emailMessagesToProcessValues = await _cSharpService.EvaluateActionInput<List<string>>(sandBox, EmailMessagesToProcess);
        var mailFolderValue = await _cSharpService.EvaluateActionInput<string>(sandBox, MailFolder);
        var imapServerValue = await _cSharpService.EvaluateActionInput<string>(sandBox, ImapServer);
        var portValue = await _cSharpService.EvaluateActionInput<int>(sandBox, Port);
        var usernameValue = await _cSharpService.EvaluateActionInput<string>(sandBox, Username);
        var passwordValue = await _cSharpService.EvaluateActionInput<string>(sandBox, Password);

        using (var client = new ImapClient())
        {
            await client.ConnectAsync(imapServerValue, portValue, EnableSsl);
            await client.AuthenticateAsync(usernameValue, passwordValue);

            foreach (var uid in emailMessagesToProcessValues)
            {
                var mailbox = client.GetFolder(Environment.SpecialFolder.All);
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