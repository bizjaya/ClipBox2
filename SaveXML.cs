// Decompiled with JetBrains decompiler
// Type: ClipBox2.SaveXML
// Assembly: ClipBox2, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 34961685-4A96-42BF-84C2-2E889F2EA09D
// Assembly location: D:\Sync\LOGX\CB2\ClipBox2.exe

using System;
using System.IO;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace ClipBox2
{
  //internal class SaveXML
  //{
  //  public static void SaveData(object obj, string filename)
  //  {
  //    if (Environment.GetEnvironmentVariable("encrypt") == "1")
  //    {
  //      string ClearText;
  //      using (StringWriter stringWriter = new StringWriter())
  //      {
  //        new XmlSerializer(obj.GetType()).Serialize((TextWriter) stringWriter, obj);
  //        ClearText = stringWriter.ToString();
  //      }
  //      string contents = Encrypt.EncryptString(ClearText);
  //      File.WriteAllText(filename, contents);
  //    }
  //    else
  //    {
  //      XmlSerializer xmlSerializer = new XmlSerializer(obj.GetType());
  //      TextWriter textWriter = (TextWriter) new StreamWriter(filename);
  //      xmlSerializer.Serialize(textWriter, obj);
  //      textWriter.Close();
  //    }
  //  }

  //  public static string GetString(string filename)
  //  {
  //    StreamReader streamReader = new StreamReader(filename);
  //    string str = Encrypt.DecryptString(streamReader.ReadToEnd());
  //    streamReader.Close();
  //    return str;
  //  }

  //  public static Info GetData(string filename)
  //  {
  //    StreamReader streamReader = new StreamReader(filename);
  //    string end = streamReader.ReadToEnd();
  //    string s;
  //    if (!end.Contains("bizjaya.com"))
  //    {
  //      s = Encrypt.DecryptString(end);
  //      Environment.SetEnvironmentVariable("encrypt", "1");
  //      (Application.OpenForms["Form1"] as Form1).chkEncrypt();
  //    }
  //    else
  //    {
  //      s = end;
  //      Environment.SetEnvironmentVariable("encrypt", "0");
  //      (Application.OpenForms["Form1"] as Form1).chkEncrypt();
  //    }
  //    Info data;
  //    using (TextReader textReader = (TextReader) new StringReader(s))
  //      data = (Info) new XmlSerializer(typeof (Info)).Deserialize(textReader);
  //    streamReader.Close();
  //    return data;
  //  }
  //}
}
