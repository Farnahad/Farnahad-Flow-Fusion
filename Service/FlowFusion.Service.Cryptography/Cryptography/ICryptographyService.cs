namespace FlowFusion.Service.Cryptography.Cryptography;

public interface ICryptographyService
{
    Task<string> EncryptTextWithAes(string textToEncrypt, string encryptionCode, Base.Encoding encoding);
    Task<string> EncryptFromFileWithAes(string fileToEncrypt, string encryptionKey, Base.Encoding encoding);
    Task<string> DecryptTextWithAes(string textToDecrypt, string decryptionKey, Base.Encoding encoding);
    Task<string> DecryptToFileWithAes(string textToDecrypt, string decryptionKey, string decryptToFile,
        Base.Encoding encoding, Base.IfFileExists ifFileExists);
    string HashText(string textToHash, Base.HashAlgorithm hashAlgorithm, Base.Encoding encoding);
    string HashFromFile(string fileToHash, Base.HashAlgorithm hashAlgorithm, Base.Encoding encoding);
    string HashTextWithKey(string textToHash, string hashKey, Base.HamcHashAlgorithm hamcHashAlgorithm, Base.Encoding encoding);
    string HashFromFileWithKey(string fileToHash, string hashKey, Base.HamcHashAlgorithm hamcHashAlgorithm, Base.Encoding encoding);
}