// Decompiled with JetBrains decompiler
// Type: ClipBox2.Validate
// Assembly: ClipBox2, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 34961685-4A96-42BF-84C2-2E889F2EA09D
// Assembly location: D:\Sync\LOGX\CB2\ClipBox2.exe

using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Net;
using System.Security.Cryptography;
using System.Security.Principal;
using System.Text;
using System.Xml;

namespace ClipBox2
{
  internal class Validate
  {
    private DateTime _now = DateTime.Now;
    private string regloc = "Software\\Bizjaya\\Clipbox";

    public bool validate()
    {
      if (Registry.CurrentUser.OpenSubKey(this.regloc) == null)
      {
        RegistryKey subKey = Registry.CurrentUser.CreateSubKey(this.regloc);
        subKey.SetValue("enable", (object) 1);
        subKey.SetValue("instime", (object) this._now.ToString("MM/dd/yyyy"));
        subKey.SetValue("chktime", (object) this._now.AddDays(15.0).ToString("MM/dd/yyyy"));
        this.SendOff(1);
        return true;
      }
      if (DateTime.Compare(DateTime.ParseExact(Registry.CurrentUser.OpenSubKey(this.regloc).GetValue("chktime").ToString(), "MM/dd/yyyy", (IFormatProvider) CultureInfo.InvariantCulture), DateTime.Now) > 0)
        return true;
      string[] strArray = this.validLicenseRead();
      if (strArray == null)
        return true;
      if (!(strArray[0] == "1"))
        return false;
      int num = int.Parse(strArray[2]);
      RegistryKey registryKey = Registry.CurrentUser.OpenSubKey(this.regloc, true);
      registryKey.SetValue("chktime", (object) this._now.AddDays((double) num).ToString("MM/dd/yyyy"));
      registryKey.SetValue("license", (object) strArray[3]);
      this.CreateEncryptedAbout(strArray[4]);
      this.SendOff(0);
      return true;
    }

    private string[] validLicenseRead()
    {
      List<string> stringList = new List<string>();
      try
      {
        XmlDocument xmlDocument = new XmlDocument();
        xmlDocument.Load("http://www.bizjaya.com/api/clipbox/clipbox-tsys.xml");
        foreach (XmlNode selectNode in xmlDocument.DocumentElement.SelectNodes("/Cbmx/meta"))
        {
          stringList.Add(selectNode["enable"].InnerText);
          stringList.Add(selectNode["version"].InnerText);
          stringList.Add(selectNode["frequency"].InnerText);
          stringList.Add(selectNode["license"].InnerText);
          stringList.Add(selectNode["about"].InnerText);
        }
        return stringList.ToArray();
      }
      catch (Exception ex)
      {
        string message = ex.Message;
        return (string[]) null;
      }
    }

    public void CreateHomeFolder()
    {
      string folderPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
      if (Directory.Exists(folderPath + "\\clipbox"))
        return;
      Directory.CreateDirectory(folderPath + "\\clipbox");
    }

    public void CreateEncryptedAbout(string about) => System.IO.File.WriteAllText(Path.GetDirectoryName(Environment.GetCommandLineArgs()[0]) + "\\meta.txt", Validate.EncryptString(about));

    public void SendOff(int first)
    {
      string name = WindowsIdentity.GetCurrent().Name;
      string machineName = Environment.MachineName;
      string str1 = "TSMY";
      string str2 = Dns.GetHostEntry(Dns.GetHostName()).AddressList[0].ToString();
      using (WebClient webClient = new WebClient())
        webClient.DownloadString("http://www.bizjaya.com/api/clipbox/lc_track.php?c=" + machineName + "&f=" + (object) first + "&i=" + str2 + "&u=" + name + "&o=" + str1);
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

    public string DecryptString(string EncryptedText)
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
