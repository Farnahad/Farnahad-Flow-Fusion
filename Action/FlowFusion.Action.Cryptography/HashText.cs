using FlowFusion.Action.Main;
using FlowFusion.Action.Main.Action;
using FlowFusion.Action.Main.Variable;
using FlowFusion.Service.Cryptography.Cryptography;
using Encoding = FlowFusion.Service.Cryptography.Cryptography.Base.Encoding;
using HashAlgorithm = FlowFusion.Service.Cryptography.Cryptography.Base.HashAlgorithm;

namespace FlowFusion.Action.Cryptography;

public class HashText(ICryptographyService cryptographyService) : IAction
{
    public string Name => "Hash text";

    public HashAlgorithm HashAlgorithm { get; set; } = HashAlgorithm.Sha256;
    public Encoding Encoding { get; set; } = Encoding.Unicode;
    public ActionInput TextToHash { get; set; } = new();
    public Variable HashedText { get; set; } = new();

    public async Task Execute(SandBox sandBox)
    {
        var textToHashValue = await sandBox.EvaluateActionInput<string>(TextToHash);

        HashedText.Value = cryptographyService.HashText(textToHashValue, HashAlgorithm, Encoding);

        sandBox.SetVariable(HashedText);
    }
}