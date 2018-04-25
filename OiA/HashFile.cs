using System.Security.Cryptography;
using System.Text;

namespace OiA
{
    public class HashFile
    {
        public string GenerateHash(byte[] input, HashType hashType = HashType.MD5)
        {
            using (var hashAlgorithm = GetHashAlgorithm(hashType))
            {
                var hash = hashAlgorithm.ComputeHash(input);
                return GetStringFromHash(hash);
            }
        }

        private HashAlgorithm GetHashAlgorithm(HashType hashType)
        {
            return (HashAlgorithm)CryptoConfig.CreateFromName(hashType.ToString());
        }

        private string GetStringFromHash(byte[] hash)
        {
            StringBuilder result = new StringBuilder();
            for (int i = 0; i < hash.Length; i++)
            {
                result.Append(hash[i].ToString("X2"));
            }
            return result.ToString();
        }
    }
}