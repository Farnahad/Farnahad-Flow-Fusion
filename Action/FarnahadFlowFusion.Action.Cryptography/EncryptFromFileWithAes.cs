﻿using System.Security.Cryptography;
using FarnahadFlowFusion.Action.Cryptography.EncryptFromFileWithAesBase;
using FarnahadFlowFusion.Action.Main;
using FarnahadFlowFusion.Service.Scripting.CSharp;

namespace FarnahadFlowFusion.Action.Cryptography;

public class EncryptFromFileWithAes : IAction
{
    private readonly CSharpService _cSharpService;

    public string Name => "Encrypt from file with AES";

    public Encoding Encoding { get; set; }
    public ActionInput FileToEncrypt { get; set; }
    public ActionInput EncryptionKey { get; set; }
    public Variable EncryptedText { get; set; }

    public EncryptFromFileWithAes()
    {
        _cSharpService = new CSharpService();

        Encoding = Encoding.Unicode;
        FileToEncrypt = new ActionInput();
        EncryptionKey = new ActionInput();
        EncryptedText = new Variable();
    }

    public async Task Execute(SandBox sandBox)
    {
        var fileToEncryptValue = await _cSharpService.EvaluateActionInput<string>(sandBox, FileToEncrypt);
        var encryptionKeyValue = await _cSharpService.EvaluateActionInput<string>(sandBox, EncryptionKey);

        var realEncoding = Encoding switch
        {
            Encoding.Ascii => global::System.Text.Encoding.ASCII,
            Encoding.BigEndianUnicode => global::System.Text.Encoding.BigEndianUnicode,
            Encoding.SystemDefault => global::System.Text.Encoding.Default,
            Encoding.Unicode => global::System.Text.Encoding.Unicode,
            Encoding.Utf8 => global::System.Text.Encoding.UTF8,
            _ => global::System.Text.Encoding.ASCII
        };

        var text = await global::System.IO.File.ReadAllTextAsync(fileToEncryptValue, realEncoding);

#pragma warning disable SYSLIB0021
        using var aesAlg = new AesCryptoServiceProvider();
#pragma warning restore SYSLIB0021

        aesAlg.Key = global::System.Text.Encoding.UTF8.GetBytes(encryptionKeyValue);
        aesAlg.IV = new byte[aesAlg.BlockSize / 8];

        using var encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

        using var memoryStream = new MemoryStream();
        await using (var cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write))
        {
            await using (var streamWriter = new StreamWriter(cryptoStream, realEncoding))
            {
                await streamWriter.WriteAsync(text);
            }
        }

        EncryptedText.Value = Convert.ToBase64String(memoryStream.ToArray());
        sandBox.Variables.Add(EncryptedText);
    }
}