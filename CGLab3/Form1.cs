using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using OpenTK.Graphics.OpenGL;


namespace CGLab3
{

  public partial class Form1 : Form
  {
    public Form1()
    {
      InitializeComponent();
      bin = new Bin();
      view = new View(0, 2000);
      currentLayer = 0;
      loaded = false;
      needReload = false;
      mode = false;
    }

    private OpenTK.GLControl glControl1;
    private MenuStrip menuStrip1;
    private ToolStripMenuItem открытьToolStripMenuItem;
    private Bin bin;
    private View view;
    private bool loaded;
    private bool needReload;
    private int currentLayer;
    private bool mode;
    int FrameCount;
    DateTime nextFPSUpdate = DateTime.Now.AddSeconds(1);

    void displayFPS()
    {
      if (DateTime.Now >= nextFPSUpdate)
      {
        this.Text = String.Format("CT Visualizer (fps={0})", FrameCount);
        nextFPSUpdate = DateTime.Now.AddSeconds(1);
        FrameCount = 0;
      }
      FrameCount++;
    }

    private void Form1_Load(object sender, EventArgs e)
    {
      Application.Idle += Application_Idle;
    }

    private void InitializeComponent()
    {
      this.glControl1 = new OpenTK.GLControl();
      this.menuStrip1 = new System.Windows.Forms.MenuStrip();
      this.открытьToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.trackBar1 = new System.Windows.Forms.TrackBar();
      this.radioButton1 = new System.Windows.Forms.RadioButton();
      this.radioButton2 = new System.Windows.Forms.RadioButton();
      this.trackBar2 = new System.Windows.Forms.TrackBar();
      this.trackBar3 = new System.Windows.Forms.TrackBar();
      this.menuStrip1.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.trackBar2)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.trackBar3)).BeginInit();
      this.SuspendLayout();
      // 
      // glControl1
      // 
      this.glControl1.BackColor = System.Drawing.Color.Black;
      this.glControl1.Location = new System.Drawing.Point(12, 27);
      this.glControl1.Name = "glControl1";
      this.glControl1.Size = new System.Drawing.Size(619, 472);
      this.glControl1.TabIndex = 0;
      this.glControl1.VSync = false;
      this.glControl1.Paint += new System.Windows.Forms.PaintEventHandler(this.glControl1_Paint);
      // 
      // menuStrip1
      // 
      this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.открытьToolStripMenuItem});
      this.menuStrip1.Location = new System.Drawing.Point(0, 0);
      this.menuStrip1.Name = "menuStrip1";
      this.menuStrip1.Size = new System.Drawing.Size(644, 24);
      this.menuStrip1.TabIndex = 1;
      this.menuStrip1.Text = "menuStrip1";
      // 
      // открытьToolStripMenuItem
      // 
      this.открытьToolStripMenuItem.Name = "открытьToolStripMenuItem";
      this.открытьToolStripMenuItem.Size = new System.Drawing.Size(66, 20);
      this.открытьToolStripMenuItem.Text = "Открыть";
      this.открытьToolStripMenuItem.Click += new System.EventHandler(this.открытьToolStripMenuItem_Click);
      // 
      // trackBar1
      // 
      this.trackBar1.Location = new System.Drawing.Point(12, 505);
      this.trackBar1.Name = "trackBar1";
      this.trackBar1.Size = new System.Drawing.Size(220, 45);
      this.trackBar1.TabIndex = 2;
      this.trackBar1.Scroll += new System.EventHandler(this.trackBar1_Scroll);
      // 
      // radioButton1
      // 
      this.radioButton1.AutoSize = true;
      this.radioButton1.Location = new System.Drawing.Point(238, 533);
      this.radioButton1.Name = "radioButton1";
      this.radioButton1.Size = new System.Drawing.Size(61, 17);
      this.radioButton1.TabIndex = 3;
      this.radioButton1.Text = "Texture";
      this.radioButton1.UseVisualStyleBackColor = true;
      this.radioButton1.CheckedChanged += new System.EventHandler(this.radioButton1_CheckedChanged);
      // 
      // radioButton2
      // 
      this.radioButton2.AutoSize = true;
      this.radioButton2.Checked = true;
      this.radioButton2.Location = new System.Drawing.Point(238, 505);
      this.radioButton2.Name = "radioButton2";
      this.radioButton2.Size = new System.Drawing.Size(56, 17);
      this.radioButton2.TabIndex = 4;
      this.radioButton2.TabStop = true;
      this.radioButton2.Text = "Quads";
      this.radioButton2.UseVisualStyleBackColor = true;
      this.radioButton2.CheckedChanged += new System.EventHandler(this.radioButton2_CheckedChanged);
      // 
      // trackBar2
      // 
      this.trackBar2.Location = new System.Drawing.Point(301, 506);
      this.trackBar2.Name = "trackBar2";
      this.trackBar2.Size = new System.Drawing.Size(161, 45);
      this.trackBar2.TabIndex = 5;
      this.trackBar2.Scroll += new System.EventHandler(this.trackBar2_Scroll);
      // 
      // trackBar3
      // 
      this.trackBar3.Location = new System.Drawing.Point(468, 506);
      this.trackBar3.Name = "trackBar3";
      this.trackBar3.Size = new System.Drawing.Size(163, 45);
      this.trackBar3.TabIndex = 6;
      this.trackBar3.Scroll += new System.EventHandler(this.trackBar3_Scroll);
      // 
      // Form1
      // 
      this.ClientSize = new System.Drawing.Size(644, 562);
      this.Controls.Add(this.trackBar3);
      this.Controls.Add(this.trackBar2);
      this.Controls.Add(this.radioButton2);
      this.Controls.Add(this.radioButton1);
      this.Controls.Add(this.trackBar1);
      this.Controls.Add(this.glControl1);
      this.Controls.Add(this.menuStrip1);
      this.MainMenuStrip = this.menuStrip1;
      this.Name = "Form1";
      this.Load += new System.EventHandler(this.Form1_Load);
      this.menuStrip1.ResumeLayout(false);
      this.menuStrip1.PerformLayout();
      ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.trackBar2)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.trackBar3)).EndInit();
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    void Application_Idle(object sender, EventArgs e)
    {
      while (glControl1.IsIdle)
      {
        displayFPS();
        glControl1.Invalidate();
      }
    }

    private void открытьToolStripMenuItem_Click(object sender, EventArgs e)
    {
      OpenFileDialog d = new OpenFileDialog();
      if (d.ShowDialog() == DialogResult.OK)
      {
        string str = d.FileName;
        bin.read(str);
        view.setupWiew(glControl1.Width, glControl1.Height);
        loaded = true;
        glControl1.Invalidate();
      }
    }

    private void glControl1_Paint(object sender, PaintEventArgs e)
    {
      if (loaded)
      {
        if (!mode)
        {
          view.DrawQuads(currentLayer);
        }
        else
        {
          if (needReload)
          {
            view.generatetextureImage(currentLayer);
            view.Load2DTexture();
            needReload = false;
          }
          view.DrawTexture();
        }
        glControl1.SwapBuffers();
      }
    }

    private TrackBar trackBar1;

    private void trackBar1_Scroll(object sender, EventArgs e)
    {
      currentLayer = trackBar1.Value;
      needReload = true;
      glControl1.Refresh();
    }

    private RadioButton radioButton1;
    private RadioButton radioButton2;

    private void radioButton2_CheckedChanged(object sender, EventArgs e)
    {
      mode = false;
    }

    private void radioButton1_CheckedChanged(object sender, EventArgs e)
    {
      mode = true;
    }

    private TrackBar trackBar2;
    private TrackBar trackBar3;

    private void trackBar3_Scroll(object sender, EventArgs e)
    {
      view.tf_width = (Bin.min + Bin.width - view.tf_min) / 11 * (trackBar3.Value + 1);
      needReload = true;
      glControl1.Refresh();
    }

    private void trackBar2_Scroll(object sender, EventArgs e)
    {
      view.tf_min = (Bin.min + Bin.width) / 10 * trackBar2.Value;
      needReload = true;
      glControl1.Refresh();
    }
  }

  class Bin
  {
    public static int X, Y, Z;
    public static short[] array;
    public static int min, width;
    public Bin() { }

    public void read(string path)
    {
      if (File.Exists(path))
      {
        BinaryReader reader = new BinaryReader(File.Open(path, FileMode.Open));
        X = reader.ReadInt32();
        Y = reader.ReadInt32();
        Z = reader.ReadInt32();

        int arraySize = X * Y * Z;
        array = new short[arraySize];
        for (int i = 0; i < arraySize; i++)
        {
          array[i] = reader.ReadInt16();
        }
        int max = 0;
        min = array[0];
        for (int i = 0; i < arraySize; i++)
        {
          if (min > array[i]) min = array[i];
          if (max < array[i]) max = array[i];
        }
        width = max - min;
      }
    }
  }

  public class View
  {
    public View(int m, int w) 
    {
      tf_min = m;
      tf_width = w;
    }

    Bitmap textureImage;
    int VBOtexture;
    public int tf_min;
    public int tf_width;

    public void generatetextureImage(int layer)
    {
      int l = Bin.Z / 10 * layer;
      textureImage = new Bitmap(Bin.X, Bin.Y);
      for (int i = 0; i < Bin.X; i++)
      {
        for (int j = 0; j < Bin.Y; j++)
        {
          int n = i + j * Bin.X + l * Bin.X * Bin.Y;
          textureImage.SetPixel(i, j, TransferFunction(Bin.array[n]));
        }
      }
    }

    public void DrawTexture()
    {
      GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
      GL.Enable(EnableCap.Texture2D);
      GL.BindTexture(TextureTarget.Texture2D, VBOtexture);

      GL.Begin(BeginMode.Quads);
      GL.Color3(Color.White);
      GL.TexCoord2(0f, 0f);
      GL.Vertex2(0, 0);
      GL.TexCoord2(0f, 1f);
      GL.Vertex2(0, Bin.Y);
      GL.TexCoord2(1f, 1f);
      GL.Vertex2(Bin.X, Bin.Y);
      GL.TexCoord2(1f, 0f);
      GL.Vertex2(Bin.X, 0);
      GL.End();

      GL.Disable(EnableCap.Texture2D);
    }

    public void Load2DTexture()
    {
      GL.BindTexture(TextureTarget.Texture2D, VBOtexture);
      BitmapData data = textureImage.LockBits(new Rectangle(0, 0, textureImage.Width, textureImage.Height),
        ImageLockMode.ReadOnly, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
      GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, data.Width, data.Height, 0, OpenTK.Graphics.OpenGL.PixelFormat.Bgra, PixelType.UnsignedByte, data.Scan0);

      textureImage.UnlockBits(data);

      GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Linear);
      GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Linear);

      ErrorCode e = GL.GetError();
      string str = e.ToString();
    }

    public void setupWiew(int width, int height)
    {
      GL.ShadeModel(ShadingModel.Smooth);
      GL.MatrixMode(MatrixMode.Projection);
      GL.LoadIdentity();
      GL.Ortho(0, Bin.X, 0, Bin.Y, -1, 1);
      GL.Viewport(0, 0, width, height);
    }

    public int Clamp(int val, int min, int max)
    {
      if (val < min)
        return min;
      if (val > max)
        return max;
      return val;
    }

    public Color TransferFunction(short value)
    {
      int newVal = Clamp((value - tf_min) * 255 / tf_width, 0, 255);
      return Color.FromArgb(255, newVal, newVal, newVal);
    }

    public void DrawQuads(int layer)
    {
      GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
      int l = Bin.Z / 10 * layer;
      short value;
      
      for (int y = 0; y < Bin.Y - 1; y++)
      {
        GL.Begin(PrimitiveType.QuadStrip);
        value = Bin.array[y * Bin.X + l * Bin.X * Bin.Y];
        GL.Color3(TransferFunction(value));
        GL.Vertex2(0, y);
        value = Bin.array[(y + 1) * Bin.X + l * Bin.X * Bin.Y];
        GL.Color3(TransferFunction(value));
        GL.Vertex2(0, y + 1);
        for (int x = 0; x < Bin.X - 1; x++)
        {
          value = Bin.array[x + 1 + y * Bin.X + l * Bin.X * Bin.Y];
          GL.Color3(TransferFunction(value));
          GL.Vertex2(x + 1, y);

          value = Bin.array[x + 1 + (y + 1) * Bin.X + l * Bin.X * Bin.Y];
          GL.Color3(TransferFunction(value));
          GL.Vertex2(x + 1, y + 1);
        }
        GL.End();
      }
      
    }
  }

  

}