using System.Security.Cryptography;
using System.Text;
using FarnahadFlowFusion.Action.Main;
using FarnahadFlowFusion.Action.Main.Variable;
using FarnahadFlowFusion.Service.Scripting.CSharp;
using Encoding = FarnahadFlowFusion.Action.Cryptography.HashTextBase.Encoding;
using HashAlgorithm = FarnahadFlowFusion.Action.Cryptography.HashTextBase.HashAlgorithm;

namespace FarnahadFlowFusion.Action.Cryptography;

public class HashText : IAction
{
    private readonly CSharpService _cSharpService;

    public string Name => "Hash text";

    public HashAlgorithm HashAlgorithm { get; set; }
    public Encoding Encoding { get; set; }
    public ActionInput TextToHash { get; set; }
    public Variable HashedText { get; set; }

    public HashText()
    {
        _cSharpService = new CSharpService();

        HashAlgorithm = HashAlgorithm.Sha256;
        Encoding = Encoding.Unicode;
        TextToHash = new ActionInput();
        HashedText = new Variable();
    }

    public async Task Execute(SandBox sandBox)
    {
        var textToHashValue = await _cSharpService.EvaluateActionInput<string>(sandBox, TextToHash);

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

        var hash = HashAlgorithm switch
        {
            HashAlgorithm.Sha256 => SHA256.Create().ComputeHash(data),
            HashAlgorithm.Sha384 => SHA384.Create().ComputeHash(data),
            HashAlgorithm.Sha512 => SHA512.Create().ComputeHash(data),
            _ => default
        };

        var stringBuilder = new StringBuilder();
        if (hash != null)
        {
            foreach (var b in hash)
                stringBuilder.Append(b.ToString("x2"));
        }

        HashedText.Value = stringBuilder.ToString();

        sandBox.Variables.Add(HashedText);
    }
}