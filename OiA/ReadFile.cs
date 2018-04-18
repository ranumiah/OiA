using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using OiA.Repository;

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

        public static string GenerateMd5Hash(byte[] input)
        {
            using (var hash = MD5.Create())
            {
                var result = hash.ComputeHash(input);
                return Convert.ToBase64String(result);
            }
        }

        public static string GenerateSHA256String(byte[] inputString)
        {
            SHA256 sha256 = SHA256.Create();
            byte[] hash = sha256.ComputeHash(inputString);
            return GetStringFromHash(hash);
        }

        public static string GenerateSHA512String(byte[] inputString)
        {
            SHA512 sha512 = SHA512.Create();
            byte[] hash = sha512.ComputeHash(inputString);
            return GetStringFromHash(hash);
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
            var fileInfo = new FileInfo(file);
            var fileDetail = new FileDetail();
            fileDetail.FileName = fileInfo.Name;
            fileDetail.FileLength = fileInfo.Length;
            fileDetail.FileFullName = fileInfo.FullName;
            fileDetail.FileExtension = fileInfo.Extension;
            fileDetail.FileCreationTimeUtc = fileInfo.CreationTimeUtc;
            fileDetail.FileLastWriteTimeUtc = fileInfo.LastWriteTimeUtc;
            fileDetail.FileLastAccessTimeUtc = fileInfo.LastAccessTimeUtc;

            return fileDetail;
        }
    }
   
}
