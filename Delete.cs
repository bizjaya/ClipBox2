// Decompiled with JetBrains decompiler
// Type: ClipBox2.Delete
// Assembly: ClipBox2, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 34961685-4A96-42BF-84C2-2E889F2EA09D
// Assembly location: D:\Sync\LOGX\CB2\ClipBox2.exe

using System.ComponentModel;
using System.Windows.Forms;

namespace ClipBox2
{
  public class Delete : Form
  {
    private IContainer components;

    public Delete() => this.InitializeComponent();

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      this.components = (IContainer) new System.ComponentModel.Container();
      this.AutoScaleMode = AutoScaleMode.Font;
      this.Text = nameof (Delete);
    }
  }
}
