using System.Security.Cryptography;
using FlowFusion.Action.Main;
using FlowFusion.Action.Main.Action;
using FlowFusion.Action.Main.Variable;
using Encoding = FlowFusion.Action.Cryptography.HashTextWithKeyBase.Encoding;
using HashAlgorithm = FlowFusion.Action.Cryptography.HashTextWithKeyBase.HashAlgorithm;

namespace FlowFusion.Action.Cryptography;

public class HashTextWithKey : IAction //XXXXXXXXXXXX
{
    public string Name => "Hash text with key";

    public HashAlgorithm HashAlgorithm { get; set; }
    public Encoding Encoding { get; set; }
    public ActionInput TextToHash { get; set; }
    public ActionInput HashKey { get; set; }
    public Variable HashedText { get; set; }

    public HashTextWithKey()
    {
        HashAlgorithm = HashAlgorithm.HamcSha256;
        Encoding = Encoding.Unicode;
        TextToHash = new ActionInput();
        HashKey = new ActionInput();
        HashedText = new Variable();
    }

    public async Task Execute(SandBox sandBox)
    {
        var textToHashValue = await sandBox.EvaluateActionInput<string>(TextToHash);
        var hashKeyValue = await sandBox.EvaluateActionInput<string>(HashKey);

        var key = global::System.Text.Encoding.UTF8.GetBytes(hashKeyValue);

        var realEncoding = Encoding switch
        {
            Encoding.Ascii => global::System.Text.Encoding.ASCII,
            Encoding.BigEndianUnicode => global::System.Text.Encoding.BigEndianUnicode,
            Encoding.SystemDefault => global::System.Text.Encoding.Default,
            Encoding.Unicode => global::System.Text.Encoding.Unicode,
            Encoding.Utf8 => global::System.Text.Encoding.UTF8,
            _ => global::System.Text.Encoding.Unicode
        };

        var data = realEncoding.GetBytes(textToHashValue);

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