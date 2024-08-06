using FlowFusion.Action.Main;
using FlowFusion.Action.Main.Action;
using FlowFusion.Action.Main.Variable;
using FlowFusion.Service.Cryptography.Cryptography;
using Encoding = FlowFusion.Service.Cryptography.Cryptography.Base.Encoding;
using IfFileExists = FlowFusion.Service.Cryptography.Cryptography.Base.IfFileExists;

namespace FlowFusion.Action.Cryptography;

public class DecryptToFileWithAes(ICryptographyService cryptographyService) : IAction
{
    public string Name => "Decrypt to file with AES";

    public Encoding Encoding { get; set; } = Encoding.Unicode;
    public ActionInput TextToDecrypt { get; set; } = new();
    public ActionInput DecryptionKey { get; set; } = new();
    public ActionInput DecryptToFile { get; set; } = new();
    public IfFileExists IfFileExists { get; set; } = IfFileExists.AddSequentialSuffix;
    public Variable DecryptedFile { get; set; } = new();

    public async Task Execute(SandBox sandBox)
    {
        var textToDecryptValue = await sandBox.EvaluateActionInput<string>(TextToDecrypt);
        var decryptionKeyValue = await sandBox.EvaluateActionInput<string>(DecryptionKey);
        var decryptToFileValue = await sandBox.EvaluateActionInput<string>(DecryptToFile);

        DecryptedFile.Value = cryptographyService.DecryptToFileWithAes(
            textToDecryptValue, decryptionKeyValue, decryptToFileValue, Encoding, IfFileExists);

        sandBox.SetVariable(DecryptedFile);
    }
}