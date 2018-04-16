using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace OiA
{
    public static class HashFile
    {
        public static string GenerateMd5Hash(string input)
        {
            using (var hash = MD5.Create())
            {
                var result = hash.ComputeHash(Encoding.UTF8.GetBytes(input));
                return Encoding.ASCII.GetString(result);
            }
        }

        public static string GenerateSHA256String(string inputString)
        {
            SHA256 sha256 = SHA256.Create();
            byte[] bytes = Encoding.UTF8.GetBytes(inputString);
            byte[] hash = sha256.ComputeHash(bytes);
            return GetStringFromHash(hash);
        }

        public static string GenerateSHA512String(string inputString)
        {
            SHA512 sha512 = SHA512.Create();
            byte[] bytes = Encoding.UTF8.GetBytes(inputString);
            byte[] hash = sha512.ComputeHash(bytes);
            return GetStringFromHash(hash);
        }

        private static string GetStringFromHash(byte[] hash)
        {
            StringBuilder result = new StringBuilder();
            for (int i = 0; i < hash.Length; i++)
            {
                result.Append(hash[i].ToString("X2"));
            }
            return result.ToString();
        }

    }

    public class ReadFile
    {
        public FileDetail GetFileDetails(string file)
        {
            return new FileDetail(file);
        }
    }

    public class FileDetail
    {

        public FileDetail(string filePath)
        {
            var file = new FileInfo(filePath);
            FileName = file.Name;
            FileLength = file.Length;
            FileFullName = file.FullName;
            FileExtension = file.Extension;
            FileCreationTimeUtc = file.CreationTimeUtc;
            FileLastWriteTimeUtc = file.LastWriteTimeUtc;
            FileLastAccessTimeUtc = file.LastAccessTimeUtc;


        }

        public string FileName { get; }
        public long FileLength { get; }
        public string FileFullName { get; }
        public string FileExtension { get; }
        public DateTime FileCreationTimeUtc { get; }
        public DateTime FileLastWriteTimeUtc { get; }
        public DateTime FileLastAccessTimeUtc { get; }
    }
}
