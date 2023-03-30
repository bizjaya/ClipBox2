// Decompiled with JetBrains decompiler
// Type: ClipBox2.Info
// Assembly: ClipBox2, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 34961685-4A96-42BF-84C2-2E889F2EA09D
// Assembly location: D:\Sync\LOGX\CB2\ClipBox2.exe

using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace ClipBox2
{
  [Serializable]
  public class Info
  {
    [XmlArrayItem("col")]
    [XmlArray("cols")]
    public List<string> cols = new List<string>();
    [XmlArrayItem("str")]
    [XmlArray("strs")]
    public List<List<string>> strs = new List<List<string>>();

    [XmlAttribute("cbmz")]
    public string cbmz { get; set; }

    [XmlAttribute(Namespace = "http://bizjaya.com")]
    public string cbname { get; set; }
  }
}
