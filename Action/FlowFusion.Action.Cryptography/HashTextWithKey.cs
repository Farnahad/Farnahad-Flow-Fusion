using FlowFusion.Action.Main;
using FlowFusion.Action.Main.Action;
using FlowFusion.Action.Main.Variable;
using FlowFusion.Service.Cryptography.Cryptography;
using Encoding = FlowFusion.Service.Cryptography.Cryptography.Base.Encoding;
using HamcHashAlgorithm = FlowFusion.Service.Cryptography.Cryptography.Base.HamcHashAlgorithm;

namespace FlowFusion.Action.Cryptography;

public class HashTextWithKey(ICryptographyService cryptographyService) : IAction
{
    public string Name => "Hash text with key";

    public HamcHashAlgorithm HashAlgorithm { get; set; } = HamcHashAlgorithm.HamcSha256;
    public Encoding Encoding { get; set; } = Encoding.Unicode;
    public ActionInput TextToHash { get; set; } = new();
    public ActionInput HashKey { get; set; } = new();
    public Variable HashedText { get; set; } = new();

    public async Task Execute(SandBox sandBox)
    {
        var textToHashValue = await sandBox.EvaluateActionInput<string>(TextToHash);
        var hashKeyValue = await sandBox.EvaluateActionInput<string>(HashKey);

        HashedText.Value = cryptographyService.HashTextWithKey(textToHashValue, hashKeyValue, HashAlgorithm, Encoding);

        sandBox.SetVariable(HashedText);
    }
}