using FlowFusion.Action.Main;
using FlowFusion.Action.Main.Action;
using FlowFusion.Action.Main.Variable;
using FlowFusion.Service.Cryptography.Cryptography;
using Encoding = FlowFusion.Service.Cryptography.Cryptography.Base.Encoding;
using HashAlgorithm = FlowFusion.Service.Cryptography.Cryptography.Base.HashAlgorithm;

namespace FlowFusion.Action.Cryptography;

public class HashFromFile(ICryptographyService cryptographyService) : IAction
{
    public string Name => "Hash from file";

    public HashAlgorithm HashAlgorithm { get; set; } = HashAlgorithm.Sha256;
    public Encoding Encoding { get; set; } = Encoding.Unicode;
    public ActionInput FileToHash { get; set; } = new();
    public Variable HashedText { get; set; } = new();

    public async Task Execute(SandBox sandBox)
    {
        var fileToHashValue = await sandBox.EvaluateActionInput<string>(FileToHash);

        HashedText.Value = cryptographyService.HashFromFile(fileToHashValue, HashAlgorithm, Encoding);

        sandBox.SetVariable(HashedText);
    }
}