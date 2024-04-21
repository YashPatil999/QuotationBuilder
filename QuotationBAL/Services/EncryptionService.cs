using QuotationModels.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace QuotationBAL.Services
{
    public class EncryptionService
    {
        public String getpassworddata(UserMasterModel user)
        {
            string data = user.EncryptedPassword;
            string key = GenerateRandomKey();
            string encryptedData = EncryptData(data, key);
            user.EncryptedKey = EncryptKey(key);
            string DKey = user.EncryptedKey;
            string DecryptedKey = DecryptKey(DKey);
            string decryptedData = DecryptData(encryptedData, key);
            return encryptedData;

            /*string decryptedData = DecryptData(encryptedData, key);
            Console.WriteLine("Decrypted Data: " + decryptedData);*/
        }

        public String getpassworddata2(UserMasterModel user)
        {
            string data = user.EncryptedPassword;
            string key = DecryptKey(user.EncryptedKey);
            string EncryptedData = EncryptData(data, key);
            return EncryptedData;
        }

        public static string EncryptKey(string data)
        {
            StringBuilder encryptedText = new StringBuilder();

            foreach (char c in data)
            {
                char encryptedChar = (char)(c + 1); // Shift each character by 1 position
                encryptedText.Append(encryptedChar);
            }

            return encryptedText.ToString();
        }

        public static string DecryptKey(string encryptedData)
        {
            StringBuilder decryptedText = new StringBuilder();

            foreach (char c in encryptedData)
            {
                char decryptedChar = (char)(c - 1); // Reverse the shift by 1 position
                decryptedText.Append(decryptedChar);
            }

            return decryptedText.ToString();
        }
        public static string EncryptData(string data, string key)
        {
            byte[] encryptedBytes;
            RijndaelManaged rijndael = new RijndaelManaged();

            try
            {
                rijndael.KeySize = 256; // Set the key size to 256 bits
                rijndael.BlockSize = 128; // Set the block size to 128 bits
                rijndael.Mode = CipherMode.CBC; // Set the cipher mode to CBC
                rijndael.Padding = PaddingMode.PKCS7; // Set the padding mode to PKCS7

                rijndael.GenerateIV(); // Generate a random IV

                rijndael.Key = Encoding.UTF8.GetBytes(key);

                ICryptoTransform encryptor = rijndael.CreateEncryptor(rijndael.Key, rijndael.IV);

                using (var ms = new System.IO.MemoryStream())
                {
                    using (var cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
                    {
                        byte[] dataBytes = Encoding.UTF8.GetBytes(data);
                        cs.Write(dataBytes, 0, dataBytes.Length);
                        cs.FlushFinalBlock();
                    }

                    encryptedBytes = ms.ToArray();
                }

                byte[] encryptedData = new byte[encryptedBytes.Length + 16]; // Include space for IV

                Array.Copy(rijndael.IV, 0, encryptedData, 0, 16); // Copy IV to the beginning
                Array.Copy(encryptedBytes, 0, encryptedData, 16, encryptedBytes.Length); // Copy encrypted data after IV

                // Convert to Base64 string for easier storage or transmission
                string encryptedDataString = Convert.ToBase64String(encryptedData);
                return encryptedDataString;
            }
            finally
            {
                rijndael.Dispose(); // Dispose the Rijndael object
            }
        }

        public static string DecryptData(string encryptedDataString, string key)
        {
            byte[] encryptedData = Convert.FromBase64String(encryptedDataString);
            RijndaelManaged rijndael = new RijndaelManaged();

            try
            {
                rijndael.KeySize = 256; // Set the key size to 256 bits
                rijndael.BlockSize = 128; // Set the block size to 128 bits
                rijndael.Mode = CipherMode.CBC; // Set the cipher mode to CBC
                rijndael.Padding = PaddingMode.PKCS7; // Set the padding mode to PKCS7

                byte[] iv = new byte[16]; // Extract IV from the encrypted data
                Array.Copy(encryptedData, 0, iv, 0, 16);

                rijndael.Key = Encoding.UTF8.GetBytes(key);
                rijndael.IV = iv;

                ICryptoTransform decryptor = rijndael.CreateDecryptor(rijndael.Key, rijndael.IV);

                using (var ms = new System.IO.MemoryStream())
                {
                    using (var cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Write))
                    {
                        cs.Write(encryptedData, 16, encryptedData.Length - 16); // Exclude IV from decryption
                        cs.FlushFinalBlock();
                    }

                    byte[] decryptedBytes = ms.ToArray();
                    string decryptedData = Encoding.UTF8.GetString(decryptedBytes);
                    return decryptedData;
                }
            }
            finally
            {
                rijndael.Dispose(); // Dispose the Rijndael object
            }
        }
        public static string GenerateRandomKey()
        {
            using (RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider())
            {
                byte[] keyBytes = new byte[16]; // Assuming key length is 32 bytes (256 bits)
                rng.GetBytes(keyBytes);
                string key = Convert.ToBase64String(keyBytes);
                return key;
            }
        }
    }
}
