using FlowFusion.Action.Main;
using FlowFusion.Action.Main.Action;
using FlowFusion.Action.Main.Variable;
using FlowFusion.Service.Cryptography.Cryptography;
using Encoding = FlowFusion.Service.Cryptography.Cryptography.Base.Encoding;

namespace FlowFusion.Action.Cryptography;

public class EncryptTextWithAes(ICryptographyService cryptographyService) : IAction
{
    public string Name => "Encrypt text with AES";

    public Encoding Encoding { get; set; } = Encoding.Unicode;
    public ActionInput TextToEncrypt { get; set; } = new();
    public ActionInput EncryptionCode { get; set; } = new();
    public Variable EncryptedText { get; set; } = new();

    public async Task Execute(SandBox sandBox)
    {
        var textToEncryptValue = await sandBox.EvaluateActionInput<string>(TextToEncrypt);
        var encryptionCodeValue = await sandBox.EvaluateActionInput<string>(EncryptionCode);

        EncryptedText.Value = await cryptographyService.EncryptTextWithAes(textToEncryptValue, encryptionCodeValue, Encoding);

        sandBox.SetVariable(EncryptedText);
    }
}