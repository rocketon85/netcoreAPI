﻿using Microsoft.Extensions.Options;
using netcoreAPI.Options;
using netcoreAPI.Services;
using System.Security.Cryptography;
using System.Text;

namespace netcoreAPI.Helper
{
    public class EncryptorHelper: IEncryptorHelper
    {
        private readonly SecurityOption _securityOption;
        private readonly IAzureKeyVaultService _azureKeyVaultService;

        public EncryptorHelper(IOptions<SecurityOption> securityOption, IAzureKeyVaultService azureKeyVaultService)
        {
            _securityOption = securityOption.Value;
            _azureKeyVaultService = azureKeyVaultService;
#if !DEBUG
            _securityOption.Key = _azureKeyVaultService.GetSecret(AzureSecrets.EncryptKey);
#endif
        }

        public string EncryptString(string text)
        {
            var key = Encoding.UTF8.GetBytes(_securityOption.Key);

            using (var aesAlg = Aes.Create())
            {
                using (var encryptor = aesAlg.CreateEncryptor(key, aesAlg.IV))
                {
                    using (var msEncrypt = new MemoryStream())
                    {
                        using (var csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                        using (var swEncrypt = new StreamWriter(csEncrypt))
                        {
                            swEncrypt.Write(text);
                        }

                        var iv = aesAlg.IV;

                        var decryptedContent = msEncrypt.ToArray();

                        var result = new byte[iv.Length + decryptedContent.Length];

                        Buffer.BlockCopy(iv, 0, result, 0, iv.Length);
                        Buffer.BlockCopy(decryptedContent, 0, result, iv.Length, decryptedContent.Length);

                        return Convert.ToBase64String(result);
                    }
                }
            }
        }

        public string DecryptString(string cipherText)
        {
            var fullCipher = Convert.FromBase64String(cipherText);

            var iv = new byte[16];
            var cipher = new byte[16];

            Buffer.BlockCopy(fullCipher, 0, iv, 0, iv.Length);
            Buffer.BlockCopy(fullCipher, iv.Length, cipher, 0, iv.Length);
            var key = Encoding.UTF8.GetBytes(_securityOption.Key);

            using (var aesAlg = Aes.Create())
            {
                using (var decryptor = aesAlg.CreateDecryptor(key, iv))
                {
                    string result;
                    using (var msDecrypt = new MemoryStream(cipher))
                    {
                        using (var csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                        {
                            using (var srDecrypt = new StreamReader(csDecrypt))
                            {
                                result = srDecrypt.ReadToEnd();
                            }
                        }
                    }

                    return result;
                }
            }
        }
    }
}
