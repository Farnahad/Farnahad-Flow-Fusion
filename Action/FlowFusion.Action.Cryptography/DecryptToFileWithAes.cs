using System.Security.Cryptography;
using FlowFusion.Action.Cryptography.DecryptToFileWithAesBase;
using FlowFusion.Action.Main;
using FlowFusion.Action.Main.Action;
using FlowFusion.Action.Main.Variable;

namespace FlowFusion.Action.Cryptography;

public class DecryptToFileWithAes : IAction //XXXXXXXXXXXX
{
    public string Name => "Decrypt to file with AES";

    public Encoding Encoding { get; set; }
    public ActionInput TextToDecrypt { get; set; }
    public ActionInput DecryptionKey { get; set; }
    public ActionInput DecryptToFile { get; set; }
    public IfFileExists IfFileExists { get; set; }
    public Variable DecryptedFile { get; set; }

    public DecryptToFileWithAes()
    {
        Encoding = Encoding.Unicode;
        TextToDecrypt = new ActionInput();
        DecryptionKey = new ActionInput();
        DecryptToFile = new ActionInput();
        IfFileExists = IfFileExists.AddSequentialSuffix;
        DecryptedFile = new Variable();
    }

    public async Task Execute(SandBox sandBox)
    {
        var textToDecryptValue = await sandBox.EvaluateActionInput<string>(TextToDecrypt);
        var decryptionKeyValue = await sandBox.EvaluateActionInput<string>(DecryptionKey);
        var decryptToFileValue = await sandBox.EvaluateActionInput<string>(DecryptToFile);

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

#pragma warning disable SYSLIB0021
        using var aesAlg = new AesCryptoServiceProvider();
#pragma warning restore SYSLIB0021

        using var encryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

        using var memoryStream = new MemoryStream();
        await using var cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write);

        cryptoStream.Write(data, 0, data.Length);

        await cryptoStream.FlushFinalBlockAsync();

        var fileInfo = new FileInfo(decryptToFileValue);

        switch (IfFileExists)
        {
            case IfFileExists.AddSequentialSuffix:
                if (fileInfo.Exists)
                {
                    var counter = 1;

                    while (true)
                    {
                        var tempFileInfo = new FileInfo(Path.Combine(fileInfo.FullName,
                           fileInfo.Name.Replace(fileInfo.Extension, ""), $" {counter}", fileInfo.Extension));

                        if (tempFileInfo.Exists == false)
                        {

                            await global::System.IO.File.WriteAllTextAsync(tempFileInfo.FullName, decryptToFileValue);
                            DecryptedFile.Value = tempFileInfo.FullName;
                            break;
                        }

                        counter++;
                    }
                }
                else
                {
                    await global::System.IO.File.WriteAllTextAsync(fileInfo.FullName, decryptToFileValue);
                }
                break;
            case IfFileExists.DoNotDecryptToFile:
                if (fileInfo.Exists == false)
                    await global::System.IO.File.WriteAllTextAsync(fileInfo.FullName, decryptToFileValue);
                DecryptedFile.Value = fileInfo.FullName;
                break;
            case IfFileExists.Overwrite:
                await global::System.IO.File.WriteAllTextAsync(fileInfo.FullName, decryptToFileValue);
                DecryptedFile.Value = fileInfo.FullName;
                break;
        }

        sandBox.SetVariable(DecryptedFile);
    }
}