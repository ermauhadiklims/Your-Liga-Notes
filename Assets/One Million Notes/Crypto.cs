using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Security.Cryptography;

public class Crypto
{
    
    private const string Suffics = "INUF38fhjinrd";
    private static string IV = "0827847378381134";
    private static string Key = GetNormalKey();
  //  private static string Key = config.bundle+Suffics;

    private static string GetNormalKey()
    {
        string _key = config.bundle + Suffics;
        int maxLenght = 16;
        _key = _key.Substring(0, maxLenght);
        return _key;
    }

    public static string Encrypt(string text)
    {
        byte[] plaintextbytes = System.Text.Encoding.ASCII.GetBytes(text);
        AesCryptoServiceProvider aes = new AesCryptoServiceProvider();
        aes.BlockSize = 128;
        aes.KeySize = 128;
        aes.Key = System.Text.Encoding.ASCII.GetBytes(Key);
        aes.IV = System.Text.Encoding.ASCII.GetBytes(IV);
        aes.Padding = PaddingMode.PKCS7;
        aes.Mode = CipherMode.CBC;
        ICryptoTransform crypto = aes.CreateEncryptor(aes.Key, aes.IV);
        byte[] encrypted = crypto.TransformFinalBlock(plaintextbytes, 0, plaintextbytes.Length);
        crypto.Dispose();
        return Convert.ToBase64String(encrypted);
    }
    //расшифровка
    public static string Decrypt(string encrypted)
    {
        byte[] encryptedbytes = Convert.FromBase64String(encrypted);
        AesCryptoServiceProvider aes = new AesCryptoServiceProvider();
        aes.BlockSize = 128;
        aes.KeySize = 128;
        aes.Key = System.Text.Encoding.ASCII.GetBytes(Key);
        aes.IV = System.Text.Encoding.ASCII.GetBytes(IV);
        aes.Padding = PaddingMode.PKCS7;
        aes.Mode = CipherMode.CBC;
        ICryptoTransform crypto = aes.CreateDecryptor(aes.Key, aes.IV);
        byte[] secret = crypto.TransformFinalBlock(encryptedbytes, 0, encryptedbytes.Length);
        crypto.Dispose();
        return System.Text.Encoding.ASCII.GetString(secret);

    }
}
