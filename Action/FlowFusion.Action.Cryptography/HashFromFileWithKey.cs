using System.Security.Cryptography;
using FlowFusion.Action.Main;
using FlowFusion.Action.Main.Action;
using FlowFusion.Action.Main.Variable;
using Encoding = FlowFusion.Action.Cryptography.HashFromFileWithKeyBase.Encoding;
using HashAlgorithm = FlowFusion.Action.Cryptography.HashFromFileWithKeyBase.HashAlgorithm;

namespace FlowFusion.Action.Cryptography;

public class HashFromFileWithKey : IAction
{
    public string Name => "Hash from file with key";

    public HashAlgorithm HashAlgorithm { get; set; }
    public Encoding Encoding { get; set; }
    public ActionInput FileToHash { get; set; }
    public ActionInput HashKey { get; set; }
    public Variable HashedText { get; set; }

    public HashFromFileWithKey()
    {
        HashAlgorithm = HashAlgorithm.HamcSha256;
        Encoding = Encoding.Unicode;
        FileToHash = new ActionInput();
        HashKey = new ActionInput();
        HashedText = new Variable();
    }

    public async Task Execute(SandBox sandBox)
    {
        var fileToHashValue = await sandBox.EvaluateActionInput<string>(FileToHash);
        var hashKeyValue = await sandBox.EvaluateActionInput<string>(HashKey);

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

        sandBox.SetVariable(HashedText);
    }
}