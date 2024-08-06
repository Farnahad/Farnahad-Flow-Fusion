using FlowFusion.Action.Main;
using FlowFusion.Action.Main.Action;
using FlowFusion.Action.Main.Variable;
using FlowFusion.Service.Cryptography.Cryptography;
using Encoding = FlowFusion.Service.Cryptography.Cryptography.Base.Encoding;

namespace FlowFusion.Action.Cryptography;

public class EncryptFromFileWithAes(ICryptographyService cryptographyService) : IAction
{
    public string Name => "Encrypt from file with AES";

    public Encoding Encoding { get; set; } = Encoding.Unicode;
    public ActionInput FileToEncrypt { get; set; } = new();
    public ActionInput EncryptionKey { get; set; } = new();
    public Variable EncryptedText { get; set; } = new();

    public async Task Execute(SandBox sandBox)
    {
        var fileToEncryptValue = await sandBox.EvaluateActionInput<string>(FileToEncrypt);
        var encryptionKeyValue = await sandBox.EvaluateActionInput<string>(EncryptionKey);

        EncryptedText.Value = await cryptographyService.EncryptFromFileWithAes(fileToEncryptValue, encryptionKeyValue, Encoding);

        sandBox.SetVariable(EncryptedText);
    }
}