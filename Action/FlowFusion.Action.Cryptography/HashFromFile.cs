using System.Security.Cryptography;
using FlowFusion.Action.Cryptography.HashFromFileBase;
using FlowFusion.Action.Main;
using FlowFusion.Action.Main.Action;
using FlowFusion.Action.Main.Variable;
using HashAlgorithm = FlowFusion.Action.Cryptography.HashFromFileBase.HashAlgorithm;

namespace FlowFusion.Action.Cryptography;

public class HashFromFile : IAction //XXXXXXXXXXXX
{
    public string Name => "Hash from file";

    public HashAlgorithm HashAlgorithm { get; set; }
    public Encoding Encoding { get; set; }
    public ActionInput FileToHash { get; set; }
    public Variable HashedText { get; set; }

    public HashFromFile()
    {
        HashAlgorithm = HashAlgorithm.Sha256;
        Encoding = Encoding.Unicode;
        FileToHash = new ActionInput();
        HashedText = new Variable();
    }

    public async Task Execute(SandBox sandBox)
    {
        var fileToHashValue = await sandBox.EvaluateActionInput<string>(FileToHash);

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

        HashedText.Value = HashAlgorithm switch
        {
            HashAlgorithm.Sha256 => realEncoding.GetString(SHA256.Create().ComputeHash(data)),
            HashAlgorithm.Sha384 => realEncoding.GetString(SHA384.Create().ComputeHash(data)),
            HashAlgorithm.Sha512 => realEncoding.GetString(SHA512.Create().ComputeHash(data)),
            _ => HashedText.Value
        };

        sandBox.SetVariable(HashedText);
    }
}