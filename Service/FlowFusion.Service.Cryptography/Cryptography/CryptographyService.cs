using System.Security.Cryptography;
using System.Text;

namespace FlowFusion.Service.Cryptography.Cryptography;

public class CryptographyService : ICryptographyService
{
    public async Task<string> DecryptTextWithAes(string textToDecrypt, string decryptionKey, Base.Encoding encoding)
    {
        var realEncoding = GetEncoding(encoding);

        var data = realEncoding.GetBytes(textToDecrypt);

        byte[] encryptedBytes;

#pragma warning disable SYSLIB0021
        using var aesAlg = new AesCryptoServiceProvider();
#pragma warning restore SYSLIB0021

        aesAlg.Key = global::System.Text.Encoding.UTF8.GetBytes(decryptionKey);
        aesAlg.IV = new byte[aesAlg.BlockSize / 8];

        using var encryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

        using var memoryStream = new MemoryStream();
        await using var cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write);

        cryptoStream.Write(data, 0, data.Length);

        await cryptoStream.FlushFinalBlockAsync();

        return realEncoding.GetString(memoryStream.ToArray());
    }

    public async Task<string> DecryptToFileWithAes(string textToDecrypt, string decryptionKey,
        string decryptToFile, Base.Encoding encoding, Base.IfFileExists ifFileExists)
    {
        var realEncoding = GetEncoding(encoding);

        var data = realEncoding.GetBytes(textToDecrypt);

#pragma warning disable SYSLIB0021
        using var aesAlg = new AesCryptoServiceProvider();
#pragma warning restore SYSLIB0021

        using var encryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

        using var memoryStream = new MemoryStream();
        await using var cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write);

        cryptoStream.Write(data, 0, data.Length);

        await cryptoStream.FlushFinalBlockAsync();

        var fileInfo = new FileInfo(decryptToFile);

        switch (ifFileExists)
        {
            case Base.IfFileExists.AddSequentialSuffix:
                if (fileInfo.Exists)
                {
                    var counter = 1;

                    while (true)
                    {
                        var tempFileInfo = new FileInfo(Path.Combine(fileInfo.FullName,
                           fileInfo.Name.Replace(fileInfo.Extension, ""), $" {counter}", fileInfo.Extension));

                        if (tempFileInfo.Exists == false)
                        {

                            await global::System.IO.File.WriteAllTextAsync(tempFileInfo.FullName, decryptToFile);
                            return tempFileInfo.FullName;
                            break;
                        }

                        counter++;
                    }
                }
                else
                {
                    await global::System.IO.File.WriteAllTextAsync(fileInfo.FullName, decryptToFile);
                }
                break;
            case Base.IfFileExists.DoNotDecryptToFile:
                if (fileInfo.Exists == false)
                    await global::System.IO.File.WriteAllTextAsync(fileInfo.FullName, decryptToFile);
                return fileInfo.FullName;
                break;
            case Base.IfFileExists.Overwrite:
                await global::System.IO.File.WriteAllTextAsync(fileInfo.FullName, decryptToFile);
                return fileInfo.FullName;
                break;
        }

        return null;
    }

    public async Task<string> EncryptFromFileWithAes(string fileToEncrypt, string encryptionKey, Base.Encoding encoding)
    {
        var realEncoding = GetEncoding(encoding);

        var text = await global::System.IO.File.ReadAllTextAsync(fileToEncrypt, realEncoding);

#pragma warning disable SYSLIB0021
        using var aesAlg = new AesCryptoServiceProvider();
#pragma warning restore SYSLIB0021

        aesAlg.Key = global::System.Text.Encoding.UTF8.GetBytes(encryptionKey);
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

        return Convert.ToBase64String(memoryStream.ToArray());
    }

    public async Task<string> EncryptTextWithAes(string textToEncrypt, string encryptionCode, Base.Encoding encoding)
    {
        var realEncoding = GetEncoding(encoding);

        var data = realEncoding.GetBytes(textToEncrypt);

        byte[] encryptedBytes;

#pragma warning disable SYSLIB0021
        using var aesAlg = new AesCryptoServiceProvider();
#pragma warning restore SYSLIB0021

        aesAlg.Key = global::System.Text.Encoding.UTF8.GetBytes(encryptionCode);
        aesAlg.IV = new byte[aesAlg.BlockSize / 8];

        using var encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

        using var memoryStream = new MemoryStream();
        await using var cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write);

        cryptoStream.Write(data, 0, data.Length);

        await cryptoStream.FlushFinalBlockAsync();

        return realEncoding.GetString(memoryStream.ToArray());
    }

    public string HashFromFile(string fileToHash, Base.HashAlgorithm hashAlgorithm, Base.Encoding encoding)
    {
        var realEncoding = GetEncoding(encoding);

        var data = realEncoding.GetBytes(fileToHash);

        return hashAlgorithm switch
        {
            Base.HashAlgorithm.Sha256 => realEncoding.GetString(SHA256.Create().ComputeHash(data)),
            Base.HashAlgorithm.Sha384 => realEncoding.GetString(SHA384.Create().ComputeHash(data)),
            Base.HashAlgorithm.Sha512 => realEncoding.GetString(SHA512.Create().ComputeHash(data)),
            _ => throw new ArgumentOutOfRangeException(nameof(hashAlgorithm), hashAlgorithm, null)
        };
    }

    public string HashFromFileWithKey(string fileToHash, string hashKey, Base.HamcHashAlgorithm hamcHashAlgorithm, Base.Encoding encoding)
    {
        var realEncoding = GetEncoding(encoding);

        var data = realEncoding.GetBytes(fileToHash);
        var key = global::System.Text.Encoding.UTF8.GetBytes(hashKey);

        return hamcHashAlgorithm switch
        {
            Base.HamcHashAlgorithm.HamcSha256 => realEncoding.GetString(new HMACSHA256(key).ComputeHash(data)),
            Base.HamcHashAlgorithm.HamcSha384 => realEncoding.GetString(new HMACSHA384(key).ComputeHash(data)),
            Base.HamcHashAlgorithm.HamcSha512 => realEncoding.GetString(new HMACSHA512(key).ComputeHash(data)),
            _ => throw new ArgumentOutOfRangeException(nameof(hamcHashAlgorithm), hamcHashAlgorithm, null)
        };
    }

    public string HashText(string textToHash, Base.HashAlgorithm hashAlgorithm, Base.Encoding encoding)
    {
        var realEncoding = GetEncoding(encoding);

        var data = realEncoding.GetBytes(textToHash);

        var hash = hashAlgorithm switch
        {
            Base.HashAlgorithm.Sha256 => SHA256.Create().ComputeHash(data),
            Base.HashAlgorithm.Sha384 => SHA384.Create().ComputeHash(data),
            Base.HashAlgorithm.Sha512 => SHA512.Create().ComputeHash(data),
            _ => throw new ArgumentOutOfRangeException(nameof(hashAlgorithm), hashAlgorithm, null)
        };

        var stringBuilder = new StringBuilder();
        foreach (var b in hash)
            stringBuilder.Append(b.ToString("x2"));

        return stringBuilder.ToString();
    }

    public string HashTextWithKey(string textToHash, string hashKey, Base.HamcHashAlgorithm hamcHashAlgorithm, Base.Encoding encoding)
    {
        var key = global::System.Text.Encoding.UTF8.GetBytes(hashKey);

        var realEncoding = GetEncoding(encoding);

        var data = realEncoding.GetBytes(textToHash);

        return hamcHashAlgorithm switch
        {
            Base.HamcHashAlgorithm.HamcSha256 => realEncoding.GetString(new HMACSHA256(key).ComputeHash(data)),
            Base.HamcHashAlgorithm.HamcSha384 => realEncoding.GetString(new HMACSHA384(key).ComputeHash(data)),
            Base.HamcHashAlgorithm.HamcSha512 => realEncoding.GetString(new HMACSHA512(key).ComputeHash(data)),
            _ => throw new ArgumentOutOfRangeException(nameof(hamcHashAlgorithm), hamcHashAlgorithm, null)
        };
    }

    private System.Text.Encoding GetEncoding(Base.Encoding encoding)
    {
        return encoding switch
        {
            Base.Encoding.Ascii => global::System.Text.Encoding.ASCII,
            Base.Encoding.BigEndianUnicode => global::System.Text.Encoding.BigEndianUnicode,
            Base.Encoding.SystemDefault => global::System.Text.Encoding.Default,
            Base.Encoding.Unicode => global::System.Text.Encoding.Unicode,
            Base.Encoding.Utf8 => global::System.Text.Encoding.UTF8,
            _ => global::System.Text.Encoding.Unicode
        };
    }
}