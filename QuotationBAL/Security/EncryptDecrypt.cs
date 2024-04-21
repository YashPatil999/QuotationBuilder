using Org.BouncyCastle.Crypto.Engines;
using Org.BouncyCastle.Crypto.Modes;
using Org.BouncyCastle.Crypto.Parameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace QuotationBAL.Security
{
    public class EncryptDecrypt
    {
        public static string Encrypt(string plainText)
        {
            var passphrase = "encryption_il_ozone_agcm_encrypt";//ApplicationConfig.GetInstance.AIRTELPB_ENCRYPTION_KEY;
            return EncryptAES256GCMAPB(plainText, Convert.ToBase64String(UTF8Encoding.UTF8.GetBytes(passphrase)));
        }

        public static string Decrypt(string cipherText)
        {
            var passphrase = "encryption_il_ozone_agcm_encrypt"; //e78538a1-9bae-4a3e-9bc9-
            return DecryptAES256GCMAPB(cipherText, Convert.ToBase64String(UTF8Encoding.UTF8.GetBytes(passphrase)));
        }

        public static string EncryptAES256GCMAPB(string plainText, string base64Key)
        {
            string sR = string.Empty;
            try
            {
                byte[] plainBytes = Encoding.UTF8.GetBytes(plainText);
                byte[] key = Convert.FromBase64String(base64Key);
                byte[] iv = Convert.FromBase64String("TG9tYmFyZEAxMjM0");

                GcmBlockCipher cipher = new GcmBlockCipher(new AesFastEngine());
                AeadParameters parameters =
                             new AeadParameters(new KeyParameter(key), 128, iv, null);

                cipher.Init(true, parameters);

                byte[] encryptedBytes = new byte[cipher.GetOutputSize(plainBytes.Length)];
                Int32 retLen = cipher.ProcessBytes
                               (plainBytes, 0, plainBytes.Length, encryptedBytes, 0);
                cipher.DoFinal(encryptedBytes, retLen);
                byte[] tag = cipher.GetMac();
                byte[] final = new byte[iv.Length + encryptedBytes.Length];
                Buffer.BlockCopy(iv, 0, final, 0, iv.Length);
                Buffer.BlockCopy(encryptedBytes, 0, final, iv.Length, encryptedBytes.Length);
                sR = Convert.ToBase64String(final, Base64FormattingOptions.None);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
            }

            return sR;
        }

        public static string DecryptAES256GCMAPB(string encryptedText, string base64Key)
        {
            string sR = string.Empty;
            byte[] key = Convert.FromBase64String(base64Key);
            try
            {
                byte[] IVnEncryptedBytes = Convert.FromBase64String(encryptedText);
                byte[] iv = new byte[12];
                byte[] encryptedBytes = new byte[IVnEncryptedBytes.Length - iv.Length];
                Buffer.BlockCopy(IVnEncryptedBytes, 0, iv, 0, iv.Length);
                Buffer.BlockCopy(IVnEncryptedBytes, 12, encryptedBytes, 0, encryptedBytes.Length);
                GcmBlockCipher cipher = new GcmBlockCipher(new AesFastEngine());
                AeadParameters parameters =
                          new AeadParameters(new KeyParameter(key), 128, iv, null);

                cipher.Init(false, parameters);
                byte[] plainBytes = new byte[cipher.GetOutputSize(encryptedBytes.Length)];
                Int32 retLen = cipher.ProcessBytes
                               (encryptedBytes, 0, encryptedBytes.Length, plainBytes, 0);
                cipher.DoFinal(plainBytes, retLen);

                sR = Encoding.UTF8.GetString(plainBytes).TrimEnd("\r\n\0".ToCharArray());
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
            }

            return sR;
        }
    }
}