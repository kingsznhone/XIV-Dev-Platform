using KeyboardApi;
using ScreenSearchApi;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Speech.Synthesis;
using System.Threading;
using System.Windows.Forms;
using 自动进本助手.Properties;

namespace 自动进本
{
	public class Form1 : Form
	{
		private IntPtr GamehWnd;

		private SpeechSynthesizer Speaker;

		private bool isrunning;

		private Scanner scanner;

		private Thread T;

		private IContainer components = null;

		private Button StartBtn;

		private Label label1;

		private PictureBox pictureBox1;

		[DllImport("user32.dll")]
		private static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

		public Form1()
		{
			GamehWnd = FindWindow(null, "最终幻想XIV");
			if (GamehWnd == IntPtr.Zero)
			{
				MessageBox.Show("游戏未运行", "严重错误：上溢", MessageBoxButtons.OK);
				Close();
			}
			InitializeComponent();
			isrunning = false;
			Speaker = new SpeechSynthesizer();
			Speaker.SelectVoiceByHints(VoiceGender.Female, VoiceAge.Teen);
			Speaker.Volume = 100;
			Speaker.Rate = 0;
			Speaker.SpeakAsync("Initialized");
			scanner = new Scanner();
			pictureBox1.Image = Resources.问号;
		}

		private void StartBtn_Click(object sender, EventArgs e)
		{
			if (isrunning)
			{
				isrunning = !isrunning;
				StartBtn.Text = "开始监控";
				T.Abort();
			}
			else
			{
				isrunning = !isrunning;
				StartBtn.Text = "停止监控";
				T = new Thread(findnclick);
				T.Start();
			}
		}

		private void findnclick()
		{
			while (Findpic(Resources.进本, click: true) != 1)
			{
				Thread.Sleep(3000);
			}
			StartBtn.Invoke((EventHandler)delegate
			{
				StartBtn.Text = "开始监控";
			});
		}

		private int Findpic(Bitmap subpic, bool click)
		{
			int TargetX = 0;
			int TargetY = 0;
			long Elapsed = 0L;
			scanner.SetSubPic(subpic);
			int num = scanner.Match(out TargetX, out TargetY, out Elapsed);
			if (num == 1 && click)
			{
				Mouse.LeftClick(GamehWnd, TargetX, TargetY);
				return num;
			}
			if (num == 1 && !click)
			{
				Mouse.MoveMouse(GamehWnd, TargetX, TargetY);
				return num;
			}
			Mouse.MoveMouse(GamehWnd, 0, 0);
			return num;
		}

		private void pictureBox1_Click(object sender, EventArgs e)
		{
			MessageBox.Show("设置UI缩放为100%，触发一次后自动解除" + Environment.NewLine + "这个东西就是帮你点一下进本的按钮" + Environment.NewLine + "防止去上厕所喝水拿外卖错过排本", "使用说明");
		}

		private void Form1_FormClosed(object sender, FormClosedEventArgs e)
		{
			Environment.Exit(0);
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing && components != null)
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		private void InitializeComponent()
		{
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(自动进本.Form1));
			StartBtn = new System.Windows.Forms.Button();
			label1 = new System.Windows.Forms.Label();
			pictureBox1 = new System.Windows.Forms.PictureBox();
			((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
			SuspendLayout();
			StartBtn.Location = new System.Drawing.Point(12, 64);
			StartBtn.Name = "StartBtn";
			StartBtn.Size = new System.Drawing.Size(253, 50);
			StartBtn.TabIndex = 0;
			StartBtn.Text = "开始监控";
			StartBtn.UseVisualStyleBackColor = true;
			StartBtn.Click += new System.EventHandler(StartBtn_Click);
			label1.AutoSize = true;
			label1.Location = new System.Drawing.Point(181, 21);
			label1.Name = "label1";
			label1.Size = new System.Drawing.Size(84, 27);
			label1.TabIndex = 1;
			label1.Text = "By:ZNH";
			pictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			pictureBox1.Location = new System.Drawing.Point(12, 16);
			pictureBox1.Name = "pictureBox1";
			pictureBox1.Size = new System.Drawing.Size(32, 32);
			pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
			pictureBox1.TabIndex = 2;
			pictureBox1.TabStop = false;
			pictureBox1.Click += new System.EventHandler(pictureBox1_Click);
			base.AutoScaleDimensions = new System.Drawing.SizeF(12f, 27f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.ClientSize = new System.Drawing.Size(281, 126);
			base.Controls.Add(pictureBox1);
			base.Controls.Add(label1);
			base.Controls.Add(StartBtn);
			Font = new System.Drawing.Font("Microsoft YaHei UI", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
			base.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "Form1";
			Text = "自动进本助手 v1.2";
			base.FormClosed += new System.Windows.Forms.FormClosedEventHandler(Form1_FormClosed);
			((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
			ResumeLayout(false);
			PerformLayout();
		}
	}
}
