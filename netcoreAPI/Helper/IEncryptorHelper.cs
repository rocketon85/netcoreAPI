namespace netcoreAPI.Helper
{
    public interface IEncryptorHelper
    {

        public string EncryptString(string text);
        public string DecryptString(string cipherText);
    }
}
