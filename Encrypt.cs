// Decompiled with JetBrains decompiler
// Type: ClipBox2.Encrypt
// Assembly: ClipBox2, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 34961685-4A96-42BF-84C2-2E889F2EA09D
// Assembly location: D:\Sync\LOGX\CB2\ClipBox2.exe

using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace ClipBox2
{
  internal class Encrypt
  {
    private const string password = "admin";
    private const string Mkey = "075155";
    private static SymmetricAlgorithm encryption;

    private static void Init()
    {
      Encrypt.encryption = (SymmetricAlgorithm) new RijndaelManaged();
      Rfc2898DeriveBytes rfc2898DeriveBytes = new Rfc2898DeriveBytes("admin", Encoding.ASCII.GetBytes("075155"));
      Encrypt.encryption.Key = rfc2898DeriveBytes.GetBytes(Encrypt.encryption.KeySize / 8);
      Encrypt.encryption.IV = rfc2898DeriveBytes.GetBytes(Encrypt.encryption.BlockSize / 8);
      Encrypt.encryption.Padding = PaddingMode.PKCS7;
    }

    public static string EncryptString(string ClearText)
    {
      byte[] bytes1 = Encoding.UTF8.GetBytes(ClearText);
      SymmetricAlgorithm symmetricAlgorithm = SymmetricAlgorithm.Create();
      MemoryStream memoryStream = new MemoryStream();
      byte[] bytes2 = Encoding.ASCII.GetBytes("ryojvlzmdalyglrj");
      byte[] bytes3 = Encoding.ASCII.GetBytes("hcxilkqbbhczfeultgbskdmaunivmfuo");
      CryptoStream cryptoStream = new CryptoStream((Stream) memoryStream, symmetricAlgorithm.CreateEncryptor(bytes3, bytes2), CryptoStreamMode.Write);
      cryptoStream.Write(bytes1, 0, bytes1.Length);
      cryptoStream.Close();
      return Convert.ToBase64String(memoryStream.ToArray());
    }

    public static string DecryptString(string EncryptedText)
    {
      byte[] buffer = Convert.FromBase64String(EncryptedText);
      MemoryStream memoryStream = new MemoryStream();
      SymmetricAlgorithm symmetricAlgorithm = SymmetricAlgorithm.Create();
      byte[] bytes1 = Encoding.ASCII.GetBytes("ryojvlzmdalyglrj");
      byte[] bytes2 = Encoding.ASCII.GetBytes("hcxilkqbbhczfeultgbskdmaunivmfuo");
      CryptoStream cryptoStream = new CryptoStream((Stream) memoryStream, symmetricAlgorithm.CreateDecryptor(bytes2, bytes1), CryptoStreamMode.Write);
      cryptoStream.Write(buffer, 0, buffer.Length);
      cryptoStream.Close();
      return Encoding.UTF8.GetString(memoryStream.ToArray());
    }
  }
}
