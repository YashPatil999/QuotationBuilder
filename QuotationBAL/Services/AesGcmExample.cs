using System;
using System.Text;
using Org.BouncyCastle.Crypto.Engines;
using Org.BouncyCastle.Crypto.Modes;
using Org.BouncyCastle.Crypto.Parameters;
using QuotationModels.Models;

public class AesGcmExample
{
    public String DeData(UserMasterModel user)
    {
        string IV = user.IV;
        string Key = user.EncryptedKey;
        string EData = user.EncryptedPassword;
        string DData = DecryptAES256GCMAPB(EData, Key,IV);
        return DData;
    }

    public static byte[] HexToByteArray(string hex)
    {
        if (hex.Length % 2 != 0)
            throw new ArgumentException("Invalid hex string");

        int byteCount = hex.Length / 2;
        byte[] bytes = new byte[byteCount];

        for (int i = 0; i < byteCount; i++)
        {
            string byteValue = hex.Substring(i * 2, 2);
            bytes[i] = Convert.ToByte(byteValue, 16);
        }

        return bytes;
    }

    public static string ByteArrayToHex(byte[] bytes)
    {
        StringBuilder hex = new StringBuilder(bytes.Length * 2);
        foreach (byte b in bytes)
            hex.AppendFormat("{0:x2}", b);

        return hex.ToString();
    }

    public static string DecodeBytes(byte[] bytes)
    {
        Encoding encoding = Encoding.UTF8;
        return encoding.GetString(bytes);
    }

    /*public static byte[] Decrypt(byte[] cipherText, byte[] key, byte[] iv)
    {
        GcmBlockCipher cipher = new GcmBlockCipher(new AesEngine());
        AeadParameters parameters = new AeadParameters(new KeyParameter(key), 128, iv, null);
        cipher.Init(false, parameters);

        byte[] decryptedText = new byte[cipher.GetOutputSize(cipherText.Length)];
        int len = cipher.ProcessBytes(cipherText, 0, cipherText.Length, decryptedText, 0);
        cipher.DoFinal(decryptedText, len);

        return decryptedText;
    }*/
    public static string DecryptAES256GCMAPB(string encryptedText, string base64Key, string IV)
    {
        string sR = string.Empty;
        byte[] key = HexToByteArray(base64Key);
        try
        {
            byte[] iv = HexToByteArray(IV);
            byte[] encryptedBytes = HexToByteArray(encryptedText);
            /*Buffer.BlockCopy(IVnEncryptedBytes, 0, iv, 0, iv.Length);
            Buffer.BlockCopy(IVnEncryptedBytes, 12, encryptedBytes, 0, encryptedBytes.Length);*/
            GcmBlockCipher cipher = new GcmBlockCipher(new AesFastEngine());
            
            AeadParameters parameters =
                      new AeadParameters(new KeyParameter(key), 128, iv, null);

            cipher.Init(false, parameters);
            byte[] plainBytes = new byte[cipher.GetOutputSize(encryptedBytes.Length)];
            Int32 retLen = cipher.ProcessBytes
                           (encryptedBytes, 0, encryptedBytes.Length, plainBytes, 0);
            cipher.DoFinal(plainBytes, retLen);

            sR = Encoding.UTF8.GetString(plainBytes).TrimEnd("".ToCharArray());
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            Console.WriteLine(ex.StackTrace);
        }

        return sR;
    }
}
