using System.Net;
using System.Net.Mail;
using FarnahadFlowFusion.Action.Main;
using FarnahadFlowFusion.Service.Scripting.CSharp;

namespace FarnahadFlowFusion.Action.Email;

public class SendEmail : IAction
{
    private readonly CSharpService _cSharpService;

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
        _cSharpService = new CSharpService();

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
        var fromValue = await _cSharpService.EvaluateActionInput<string>(sandBox, From);
        var senderDisplayNameValue = await _cSharpService.EvaluateActionInput<string>(sandBox, SenderDisplayName);
        var toValue = await _cSharpService.EvaluateActionInput<string>(sandBox, To);
        var ccValue = await _cSharpService.EvaluateActionInput<string>(sandBox, Cc);
        var bccValue = await _cSharpService.EvaluateActionInput<string>(sandBox, Bcc);
        var subjectValue = await _cSharpService.EvaluateActionInput<string>(sandBox, Subject);
        var bodyValue = await _cSharpService.EvaluateActionInput<string>(sandBox, Body);
        var attachmentsValue = await _cSharpService.EvaluateActionInput<List<string>>(sandBox, Attachments);
        var smtpServerValue = await _cSharpService.EvaluateActionInput<string>(sandBox, SmtpServer);
        var serverPortValue = await _cSharpService.EvaluateActionInput<int>(sandBox, ServerPort);
        var userNameValue = await _cSharpService.EvaluateActionInput<string>(sandBox, UserName);
        var passwordValue = await _cSharpService.EvaluateActionInput<string>(sandBox, Password);

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