using System.Security.Cryptography;
using FarnahadFlowFusion.Action.Cryptography.DecryptTextWithAesBase;
using FarnahadFlowFusion.Action.Main;
using FarnahadFlowFusion.Action.Main.Action;
using FarnahadFlowFusion.Action.Main.Variable;

namespace FarnahadFlowFusion.Action.Cryptography;

public class DecryptTextWithAes : IAction
{
    public string Name => "Decrypt text with AES";

    public Encoding Encoding { get; set; }
    public ActionInput TextToDecrypt { get; set; }
    public ActionInput DecryptionKey { get; set; }
    public Variable DecryptedText { get; set; }

    public DecryptTextWithAes()
    {
        Encoding = Encoding.Unicode;
        TextToDecrypt = new ActionInput();
        DecryptionKey = new ActionInput();
        DecryptedText = new Variable();
    }

    public async Task Execute(SandBox sandBox)
    {
        var textToDecryptValue = await sandBox.EvaluateActionInput<string>(TextToDecrypt);
        var decryptionKeyValue = await sandBox.EvaluateActionInput<string>(DecryptionKey);

        var realEncoding = Encoding switch
        {
            Encoding.Ascii => global::System.Text.Encoding.ASCII,
            Encoding.BigEndianUnicode => global::System.Text.Encoding.BigEndianUnicode,
            Encoding.SystemDefault => global::System.Text.Encoding.Default,
            Encoding.Unicode => global::System.Text.Encoding.Unicode,
            Encoding.Utf8 => global::System.Text.Encoding.UTF8,
            _ => global::System.Text.Encoding.ASCII
        };

        var data = realEncoding.GetBytes(textToDecryptValue);

        byte[] encryptedBytes;

#pragma warning disable SYSLIB0021
        using var aesAlg = new AesCryptoServiceProvider();
#pragma warning restore SYSLIB0021

        aesAlg.Key = global::System.Text.Encoding.UTF8.GetBytes(decryptionKeyValue);
        aesAlg.IV = new byte[aesAlg.BlockSize / 8];

        using var encryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

        using var memoryStream = new MemoryStream();
        await using var cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write);

        cryptoStream.Write(data, 0, data.Length);

        await cryptoStream.FlushFinalBlockAsync();

        DecryptedText.Value = realEncoding.GetString(memoryStream.ToArray());

        sandBox.SetVariable(DecryptedText);
    }
}