using System.Security.Cryptography;
using FarnahadFlowFusion.Action.Main;
using FarnahadFlowFusion.Action.Main.Variable;
using FarnahadFlowFusion.Service.Scripting.CSharp;
using Encoding = FarnahadFlowFusion.Action.Cryptography.HashFromFileWithKeyBase.Encoding;
using HashAlgorithm = FarnahadFlowFusion.Action.Cryptography.HashFromFileWithKeyBase.HashAlgorithm;

namespace FarnahadFlowFusion.Action.Cryptography;

public class HashFromFileWithKey : IAction
{
    private readonly CSharpService _cSharpService;

    public string Name => "Hash from file with key";

    public HashAlgorithm HashAlgorithm { get; set; }
    public Encoding Encoding { get; set; }
    public ActionInput FileToHash { get; set; }
    public ActionInput HashKey { get; set; }
    public Variable HashedText { get; set; }

    public HashFromFileWithKey()
    {
        _cSharpService = new CSharpService();

        HashAlgorithm = HashAlgorithm.HamcSha256;
        Encoding = Encoding.Unicode;
        FileToHash = new ActionInput();
        HashKey = new ActionInput();
        HashedText = new Variable();
    }

    public async Task Execute(SandBox sandBox)
    {
        var fileToHashValue = await _cSharpService.EvaluateActionInput<string>(sandBox, FileToHash);
        var hashKeyValue = await _cSharpService.EvaluateActionInput<string>(sandBox, HashKey);

        var realEncoding = Encoding switch
        {
            Encoding.Ascii => global::System.Text.Encoding.ASCII,
            Encoding.BigEndianUnicode => global::System.Text.Encoding.BigEndianUnicode,
            Encoding.SystemDefault => global::System.Text.Encoding.Default,
            Encoding.Unicode => global::System.Text.Encoding.Unicode,
            Encoding.Utf8 => global::System.Text.Encoding.UTF8,
            _ => global::System.Text.Encoding.UTF8
        };

        var data = realEncoding.GetBytes(fileToHashValue);
        var key = global::System.Text.Encoding.UTF8.GetBytes(hashKeyValue);

        HashedText.Value = HashAlgorithm switch
        {
            HashAlgorithm.HamcSha256 => realEncoding.GetString(new HMACSHA256(key).ComputeHash(data)),
            HashAlgorithm.HamcSha384 => realEncoding.GetString(new HMACSHA384(key).ComputeHash(data)),
            HashAlgorithm.HamcSha512 => realEncoding.GetString(new HMACSHA512(key).ComputeHash(data)),
            _ => HashedText.Value
        };

        sandBox.Variables.Add(HashedText);
    }
}