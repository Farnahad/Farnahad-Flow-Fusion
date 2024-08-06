using System.Security.Cryptography;
using FlowFusion.Action.Cryptography.EncryptTextWithAesBase;
using FlowFusion.Action.Main;
using FlowFusion.Action.Main.Action;
using FlowFusion.Action.Main.Variable;

namespace FlowFusion.Action.Cryptography;

public class EncryptTextWithAes : IAction
{
    public string Name => "Encrypt text with AES";

    public Encoding Encoding { get; set; }
    public ActionInput TextToEncrypt { get; set; }
    public ActionInput EncryptionCode { get; set; }
    public Variable EncryptedText { get; set; }

    public EncryptTextWithAes()
    {
        Encoding = EncryptTextWithAesBase.Encoding.Unicode;
        TextToEncrypt = new ActionInput();
        EncryptionCode = new ActionInput();
        EncryptedText = new Variable();
    }

    public async Task Execute(SandBox sandBox)
    {
        var textToEncryptValue = await sandBox.EvaluateActionInput<string>(TextToEncrypt);
        var encryptionCodeValue = await sandBox.EvaluateActionInput<string>(EncryptionCode);

        var realEncoding = Encoding switch
        {
            Encoding.Ascii => global::System.Text.Encoding.ASCII,
            Encoding.BigEndianUnicode => global::System.Text.Encoding.BigEndianUnicode,
            Encoding.SystemDefault => global::System.Text.Encoding.Default,
            Encoding.Unicode => global::System.Text.Encoding.Unicode,
            Encoding.Utf8 => global::System.Text.Encoding.UTF8,
            _ => global::System.Text.Encoding.ASCII
        };

        var data = realEncoding.GetBytes(textToEncryptValue);

        byte[] encryptedBytes;

#pragma warning disable SYSLIB0021
        using var aesAlg = new AesCryptoServiceProvider();
#pragma warning restore SYSLIB0021

        aesAlg.Key = global::System.Text.Encoding.UTF8.GetBytes(encryptionCodeValue);
        aesAlg.IV = new byte[aesAlg.BlockSize / 8];

        using var encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

        using var memoryStream = new MemoryStream();
        await using var cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write);

        cryptoStream.Write(data, 0, data.Length);

        await cryptoStream.FlushFinalBlockAsync();

        EncryptedText.Value = realEncoding.GetString(memoryStream.ToArray());

        sandBox.SetVariable(EncryptedText);
    }
}