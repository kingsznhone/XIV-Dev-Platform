namespace BloodLetter_Fantasy
{
    partial class Main
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main));
            this.GameStatusLB = new System.Windows.Forms.Label();
            this.BLParam = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.BLvK = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.AutoS = new System.Windows.Forms.CheckBox();
            this.AutoA = new System.Windows.Forms.CheckBox();
            this.AutoC = new System.Windows.Forms.CheckBox();
            this.AutovK = new System.Windows.Forms.ComboBox();
            this.StartBtn = new System.Windows.Forms.Button();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // GameStatusLB
            // 
            this.GameStatusLB.AutoSize = true;
            this.GameStatusLB.ForeColor = System.Drawing.Color.Red;
            this.GameStatusLB.Location = new System.Drawing.Point(25, 34);
            this.GameStatusLB.Name = "GameStatusLB";
            this.GameStatusLB.Size = new System.Drawing.Size(216, 27);
            this.GameStatusLB.TabIndex = 0;
            this.GameStatusLB.Text = "Game Is Not Running";
            // 
            // BLParam
            // 
            this.BLParam.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.BLParam.FormattingEnabled = true;
            this.BLParam.Items.AddRange(new object[] {
            "Ctrl",
            "Alt",
            "Shift",
            " "});
            this.BLParam.Location = new System.Drawing.Point(221, 94);
            this.BLParam.Name = "BLParam";
            this.BLParam.Size = new System.Drawing.Size(120, 35);
            this.BLParam.TabIndex = 4;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(347, 102);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(27, 27);
            this.label4.TabIndex = 6;
            this.label4.Text = "+";
            // 
            // BLvK
            // 
            this.BLvK.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.BLvK.FormattingEnabled = true;
            this.BLvK.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8",
            "9",
            "0",
            "-",
            "=",
            "Q",
            "W",
            "E",
            "R",
            "T",
            "Y",
            "U",
            "I",
            "O",
            "P",
            "A",
            "S",
            "D",
            "F",
            "G",
            "H",
            "J",
            "K",
            "L",
            "Z",
            "X",
            "C",
            "V",
            "B",
            "N",
            "M",
            "Tiled",
            "F1",
            "F2",
            "F3",
            "F4",
            "F5",
            "F6",
            "F7",
            "F8",
            "F9",
            "F10",
            "F11",
            "F12",
            "`"});
            this.BLvK.Location = new System.Drawing.Point(380, 94);
            this.BLvK.Name = "BLvK";
            this.BLvK.Size = new System.Drawing.Size(120, 35);
            this.BLvK.TabIndex = 5;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(435, 175);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(27, 27);
            this.label7.TabIndex = 12;
            this.label7.Text = "+";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(319, 175);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(27, 27);
            this.label8.TabIndex = 13;
            this.label8.Text = "+";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(220, 175);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(27, 27);
            this.label9.TabIndex = 14;
            this.label9.Text = "+";
            // 
            // AutoS
            // 
            this.AutoS.AutoSize = true;
            this.AutoS.Location = new System.Drawing.Point(352, 175);
            this.AutoS.Name = "AutoS";
            this.AutoS.Size = new System.Drawing.Size(77, 31);
            this.AutoS.TabIndex = 9;
            this.AutoS.Text = "Shift";
            this.AutoS.UseVisualStyleBackColor = true;
            // 
            // AutoA
            // 
            this.AutoA.AutoSize = true;
            this.AutoA.Location = new System.Drawing.Point(253, 175);
            this.AutoA.Name = "AutoA";
            this.AutoA.Size = new System.Drawing.Size(60, 31);
            this.AutoA.TabIndex = 10;
            this.AutoA.Text = "Alt";
            this.AutoA.UseVisualStyleBackColor = true;
            // 
            // AutoC
            // 
            this.AutoC.AutoSize = true;
            this.AutoC.Location = new System.Drawing.Point(147, 175);
            this.AutoC.Name = "AutoC";
            this.AutoC.Size = new System.Drawing.Size(67, 31);
            this.AutoC.TabIndex = 11;
            this.AutoC.Text = "Ctrl";
            this.AutoC.UseVisualStyleBackColor = true;
            // 
            // AutovK
            // 
            this.AutovK.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.AutovK.FormattingEnabled = true;
            this.AutovK.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8",
            "9",
            "0",
            "-",
            "=",
            "Q",
            "W",
            "E",
            "R",
            "T",
            "Y",
            "U",
            "I",
            "O",
            "P",
            "A",
            "S",
            "D",
            "F",
            "G",
            "H",
            "J",
            "K",
            "L",
            "Z",
            "X",
            "C",
            "V",
            "B",
            "N",
            "M",
            "Tiled",
            "F1",
            "F2",
            "F3",
            "F4",
            "F5",
            "F6",
            "F7",
            "F8",
            "F9",
            "F10",
            "F11",
            "F12",
            "Numpad0",
            "Numpad1",
            "Numpad2",
            "Numpad3",
            "Numpad4",
            "Numpad5",
            "Numpad6",
            "Numpad7",
            "Numpad8",
            "Numpad9",
            "`"});
            this.AutovK.Location = new System.Drawing.Point(468, 175);
            this.AutovK.Name = "AutovK";
            this.AutovK.Size = new System.Drawing.Size(121, 35);
            this.AutovK.TabIndex = 8;
            // 
            // StartBtn
            // 
            this.StartBtn.Enabled = false;
            this.StartBtn.ForeColor = System.Drawing.Color.Green;
            this.StartBtn.Location = new System.Drawing.Point(225, 235);
            this.StartBtn.Name = "StartBtn";
            this.StartBtn.Size = new System.Drawing.Size(121, 40);
            this.StartBtn.TabIndex = 16;
            this.StartBtn.Text = "Arm";
            this.StartBtn.UseVisualStyleBackColor = true;
            this.StartBtn.Click += new System.EventHandler(this.StartBtn_Click);
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(25, 175);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(88, 27);
            this.label10.TabIndex = 20;
            this.label10.Text = "Activate";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(505, 34);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(84, 27);
            this.label11.TabIndex = 21;
            this.label11.Text = "By:ZNH";
            // 
            // pictureBox1
            // 
            this.pictureBox1.InitialImage = null;
            this.pictureBox1.Location = new System.Drawing.Point(94, 77);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(52, 52);
            this.pictureBox1.TabIndex = 22;
            this.pictureBox1.TabStop = false;
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 27F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(613, 307);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.StartBtn);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.AutoS);
            this.Controls.Add(this.AutoA);
            this.Controls.Add(this.AutoC);
            this.Controls.Add(this.AutovK);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.BLvK);
            this.Controls.Add(this.BLParam);
            this.Controls.Add(this.GameStatusLB);
            this.Font = new System.Drawing.Font("Microsoft YaHei UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "Main";
            this.Text = "BMS Fantasy";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Main_FormClosed);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label GameStatusLB;
        private System.Windows.Forms.ComboBox BLParam;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox BLvK;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.CheckBox AutoS;
        private System.Windows.Forms.CheckBox AutoA;
        private System.Windows.Forms.CheckBox AutoC;
        private System.Windows.Forms.ComboBox AutovK;
        private System.Windows.Forms.Button StartBtn;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}

