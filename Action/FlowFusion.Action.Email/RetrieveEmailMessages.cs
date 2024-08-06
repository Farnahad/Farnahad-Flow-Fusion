using FlowFusion.Action.Email.RetrieveEmailMessagesBase;
using FlowFusion.Action.Main;
using FlowFusion.Action.Main.Action;
using FlowFusion.Action.Main.Variable;
using MailKit;
using MailKit.Net.Imap;
using MailKit.Search;
using MimeKit;

namespace FlowFusion.Action.Email;

public class RetrieveEmailMessages : IAction
{
    public string Name => "Retrieve email messages";

    public ActionInput ImapServer { get; set; }
    public ActionInput Port { get; set; }
    public bool EnableSsl { get; set; }
    public ActionInput Username { get; set; }
    public ActionInput Password { get; set; }
    public bool AcceptUntrustedCertificates { get; set; }
    public ActionInput MainFolder { get; set; }
    public Retrieve Retrieve { get; set; }
    public bool MarkAsRead { get; set; }
    public ActionInput FromFieldContains { get; set; }
    public ActionInput ToFieldContains { get; set; }
    public ActionInput SubjectFieldContains { get; set; }
    public ActionInput BodyFieldContains { get; set; }
    public SaveAttachments SaveAttachments { get; set; }
    public Variable RetrievedEmails { get; set; }

    public RetrieveEmailMessages()
    {

        ImapServer = new ActionInput();
        Port = new ActionInput();
        EnableSsl = true;
        Username = new ActionInput();
        Password = new ActionInput();
        AcceptUntrustedCertificates = false;
        MainFolder = new ActionInput();
        Retrieve = Retrieve.AllEmailMessages;
        MarkAsRead = true;
        FromFieldContains = new ActionInput();
        ToFieldContains = new ActionInput();
        SubjectFieldContains = new ActionInput();
        BodyFieldContains = new ActionInput();
        SaveAttachments = SaveAttachments.DoNotSaveAttachments;
        RetrievedEmails = new Variable();
    }

    public async Task Execute(SandBox sandBox)
    {
        var minimumVaXXXXXXXXXXXXlue = await sandBox.EvaluateActionInput<int>(XXXXXXXXXXXX);
        var XXXXXXXXXXXX = await sandBox.EvaluateActionInput<int>(XXXXXXXXXXXX);

        using (var client = new ImapClient(new ProtocolLogger("imap.log")))
        {
            try
            {
                // Connect to the server
                client.Connect(ImapServer.Value, int.Parse(Port.Value), EnableSsl);

                // Authenticate
                client.Authenticate(Username.Value, Password.Value);

                // Select the mailbox
                var mailbox = client.GetFolder(MainFolder.Value);
                mailbox.Open(FolderAccess.ReadWrite);

                // Define search criteria
                var searchQuery = GetSearchQuery();

                // Retrieve email messages
                foreach (var uid in mailbox.Search(SearchOptions.All, searchQuery))
                {
                    var message = mailbox.GetMessage(uid);
                    Console.WriteLine($"Subject: {message.Subject}");

                    if (MarkAsRead)
                    {
                        mailbox.AddFlags(uid, MessageFlags.Seen, true);
                    }

                    // Save attachments if required
                    if (SaveAttachments == SaveAttachments.SaveAttachments)
                    {
                        SaveEmailAttachments(message);
                    }
                }

                Console.WriteLine("Email retrieval completed.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            finally
            {
                client.Disconnect(true);
            }
        }





        XXXXXXXXXXXX.Value = new Random().Next(XXXXXXXXXXXX, XXXXXXXXXXXX);

        sandBox.SetVariable(XXXXXXXXXXXX);
    }

    private string GetSearchQuery()
    {
        var searchQuery = "ALL"; // Default to retrieving all messages
        List<string> conditions = new List<string>();

        if (Retrieve == Retrieve.ReadEmailMessagesOnly)
            conditions.Add("SEEN");
        else if (Retrieve == Retrieve.UnreadEmailMessagesOnly)
            conditions.Add("UNSEEN");

        if (!string.IsNullOrWhiteSpace(FromFieldContains?.Value))
            conditions.Add($"FROM \"{FromFieldContains.Value}\"");

        if (!string.IsNullOrWhiteSpace(ToFieldContains?.Value))
            conditions.Add($"TO \"{ToFieldContains.Value}\"");

        if (!string.IsNullOrWhiteSpace(SubjectFieldContains?.Value))
            conditions.Add($"SUBJECT \"{SubjectFieldContains.Value}\"");

        if (!string.IsNullOrWhiteSpace(BodyFieldContains?.Value))
            conditions.Add($"BODY \"{BodyFieldContains.Value}\"");

        if (conditions.Count > 0)
            searchQuery = string.Join(" ", conditions);

        return searchQuery;
    }

    private void SaveEmailAttachments(MimeMessage message)
    {
        foreach (var attachment in message.Attachments)
        {
            if (attachment is MimePart part)
            {
                string filePath = Path.Combine("Attachments", part.FileName);
                using (var stream = File.Create(filePath))
                {
                    part.Content.DecodeTo(stream);
                }
                Console.WriteLine($"Attachment saved: {filePath}");
            }
        }
    }

}