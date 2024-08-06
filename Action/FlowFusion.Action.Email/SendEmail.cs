using System.Net;
using System.Net.Mail;
using FlowFusion.Action.Main;
using FlowFusion.Action.Main.Action;

namespace FlowFusion.Action.Email;

public class SendEmail : IAction
{
    public string Name => "Send email";

    public ActionInput From { get; set; }
    public ActionInput SenderDisplayName { get; set; }
    public ActionInput To { get; set; }
    public ActionInput Cc { get; set; }
    public ActionInput Bcc { get; set; }
    public ActionInput Subject { get; set; }
    public ActionInput Body { get; set; }
    public bool BodyIsHtml { get; set; }
    public ActionInput Attachments { get; set; }
    public ActionInput SmtpServer { get; set; }
    public ActionInput ServerPort { get; set; }
    public bool EnableSsl { get; set; }
    public bool SmtpServerNeedsAuthentication { get; set; }
    public ActionInput UserName { get; set; }
    public ActionInput Password { get; set; }
    public bool AcceptUntrustedCertificates { get; set; }

    public SendEmail()
    {
        From = new ActionInput();
        SenderDisplayName = new ActionInput();
        To = new ActionInput();
        Cc = new ActionInput();
        Bcc = new ActionInput();
        Subject = new ActionInput();
        Body = new ActionInput();
        Bcc = new ActionInput();
        BodyIsHtml = false;
        Attachments = new ActionInput();
        SmtpServer = new ActionInput();
        ServerPort = new ActionInput();
        EnableSsl = false;
        SmtpServerNeedsAuthentication = false;
        AcceptUntrustedCertificates = false;
    }

    public async Task Execute(SandBox sandBox)
    {
        var fromValue = await sandBox.EvaluateActionInput<string>(From);
        var senderDisplayNameValue = await sandBox.EvaluateActionInput<string>(SenderDisplayName);
        var toValue = await sandBox.EvaluateActionInput<string>(To);
        var ccValue = await sandBox.EvaluateActionInput<string>(Cc);
        var bccValue = await sandBox.EvaluateActionInput<string>(Bcc);
        var subjectValue = await sandBox.EvaluateActionInput<string>(Subject);
        var bodyValue = await sandBox.EvaluateActionInput<string>(Body);
        var attachmentsValue = await sandBox.EvaluateActionInput<List<string>>(Attachments);
        var smtpServerValue = await sandBox.EvaluateActionInput<string>(SmtpServer);
        var serverPortValue = await sandBox.EvaluateActionInput<int>(ServerPort);
        var userNameValue = await sandBox.EvaluateActionInput<string>(UserName);
        var passwordValue = await sandBox.EvaluateActionInput<string>(Password);

        var smtpClient = new SmtpClient(smtpServerValue, serverPortValue)
        {
            EnableSsl = EnableSsl,
            Credentials = SmtpServerNeedsAuthentication
                ? new NetworkCredential(userNameValue, passwordValue) : null,
            DeliveryMethod = SmtpDeliveryMethod.Network,
        };

        var mailMessage = new MailMessage
        {
            From = new MailAddress(fromValue, senderDisplayNameValue),
            Subject = subjectValue,
            Body = bodyValue,
            IsBodyHtml = BodyIsHtml,
        };

        mailMessage.To.Add(toValue);
        if (!string.IsNullOrWhiteSpace(ccValue))
            mailMessage.CC.Add(ccValue);
        if (!string.IsNullOrWhiteSpace(bccValue))
            mailMessage.Bcc.Add(bccValue);

        foreach (var attachment in attachmentsValue)
            mailMessage.Attachments.Add(new Attachment(attachment));

        smtpClient.Send(mailMessage);
    }
}