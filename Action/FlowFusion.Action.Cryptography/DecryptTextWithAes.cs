using FlowFusion.Action.Main;
using FlowFusion.Action.Main.Action;
using FlowFusion.Action.Main.Variable;
using Encoding = FlowFusion.Service.Cryptography.Cryptography.Base.Encoding;
using FlowFusion.Service.Cryptography.Cryptography;

namespace FlowFusion.Action.Cryptography;

public class DecryptTextWithAes(ICryptographyService cryptographyService) : IAction
{
    public string Name => "Decrypt text with AES";

    public Encoding Encoding { get; set; } = Encoding.Unicode;
    public ActionInput TextToDecrypt { get; set; } = new();
    public ActionInput DecryptionKey { get; set; } = new();
    public Variable DecryptedText { get; set; } = new();

    public async Task Execute(SandBox sandBox)
    {
        var textToDecryptValue = await sandBox.EvaluateActionInput<string>(TextToDecrypt);
        var decryptionKeyValue = await sandBox.EvaluateActionInput<string>(DecryptionKey);

        DecryptedText.Value = await cryptographyService.DecryptTextWithAes(textToDecryptValue, decryptionKeyValue, Encoding);

        sandBox.SetVariable(DecryptedText);
    }
}