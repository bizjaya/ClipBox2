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
  public static class SaveXML
  {
    public static void SaveData(object obj, string filename)
    {
      if (Environment.GetEnvironmentVariable("encrypt") == "1")
      {
        string ClearText;
        using (StringWriter stringWriter = new StringWriter())
        {
          new XmlSerializer(obj.GetType()).Serialize((TextWriter)stringWriter, obj);
          ClearText = stringWriter.ToString();
        }
        string contents = Encrypt.EncryptString(ClearText);
        File.WriteAllText(filename, contents);
      }
      else
      {
        XmlSerializer xmlSerializer = new XmlSerializer(obj.GetType());
        TextWriter textWriter = (TextWriter)new StreamWriter(filename);
        xmlSerializer.Serialize(textWriter, obj);
        textWriter.Close();
      }
    }

    public static string GetString(string filename)
    {
      StreamReader streamReader = new StreamReader(filename);
      string str = Encrypt.DecryptString(streamReader.ReadToEnd());
      streamReader.Close();
      return str;
    }

    public static Info GetData(string filename)
    {
      try
      {
        // First try to read as unencrypted XML
        try
        {
          using (StreamReader streamReader = new StreamReader(filename))
          {
            string content = streamReader.ReadToEnd();
            if (content.Contains("<?xml")) // Simple check for XML format
            {
              using (TextReader textReader = new StringReader(content))
              {
                return (Info)new XmlSerializer(typeof(Info)).Deserialize(textReader);
              }
            }
          }
        }
        catch
        {
          // If unencrypted read fails, try encrypted
        }

        // Try to read as encrypted
        using (StreamReader streamReader = new StreamReader(filename))
        {
          string encryptedContent = streamReader.ReadToEnd();
          string decryptedContent = Encrypt.DecryptString(encryptedContent);
          
          using (TextReader textReader = new StringReader(decryptedContent))
          {
            return (Info)new XmlSerializer(typeof(Info)).Deserialize(textReader);
          }
        }
      }
      catch (Exception ex)
      {
        MessageBox.Show($"Error reading file {Path.GetFileName(filename)}: {ex.Message}",
          "Read Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        return null;
      }
    }
  }
}
