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
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
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
            this.BLParam.Location = new System.Drawing.Point(155, 58);
            this.BLParam.Name = "BLParam";
            this.BLParam.Size = new System.Drawing.Size(120, 35);
            this.BLParam.TabIndex = 4;
            this.BLParam.SelectedValueChanged += new System.EventHandler(this.Modifier_CheckedChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(281, 61);
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
            this.BLvK.Location = new System.Drawing.Point(314, 58);
            this.BLvK.Name = "BLvK";
            this.BLvK.Size = new System.Drawing.Size(120, 35);
            this.BLvK.TabIndex = 5;
            this.BLvK.SelectedValueChanged += new System.EventHandler(this.Modifier_CheckedChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(434, 161);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(27, 27);
            this.label7.TabIndex = 12;
            this.label7.Text = "+";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(318, 161);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(27, 27);
            this.label8.TabIndex = 13;
            this.label8.Text = "+";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(219, 161);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(27, 27);
            this.label9.TabIndex = 14;
            this.label9.Text = "+";
            // 
            // AutoS
            // 
            this.AutoS.AutoSize = true;
            this.AutoS.Location = new System.Drawing.Point(351, 161);
            this.AutoS.Name = "AutoS";
            this.AutoS.Size = new System.Drawing.Size(77, 31);
            this.AutoS.TabIndex = 9;
            this.AutoS.Text = "Shift";
            this.AutoS.UseVisualStyleBackColor = true;
            // 
            // AutoA
            // 
            this.AutoA.AutoSize = true;
            this.AutoA.Location = new System.Drawing.Point(252, 161);
            this.AutoA.Name = "AutoA";
            this.AutoA.Size = new System.Drawing.Size(60, 31);
            this.AutoA.TabIndex = 10;
            this.AutoA.Text = "Alt";
            this.AutoA.UseVisualStyleBackColor = true;
            // 
            // AutoC
            // 
            this.AutoC.AutoSize = true;
            this.AutoC.Location = new System.Drawing.Point(146, 161);
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
            this.AutovK.Location = new System.Drawing.Point(467, 158);
            this.AutovK.Name = "AutovK";
            this.AutovK.Size = new System.Drawing.Size(121, 35);
            this.AutovK.TabIndex = 8;
            // 
            // StartBtn
            // 
            this.StartBtn.ForeColor = System.Drawing.Color.Black;
            this.StartBtn.Location = new System.Drawing.Point(467, 57);
            this.StartBtn.Name = "StartBtn";
            this.StartBtn.Size = new System.Drawing.Size(121, 35);
            this.StartBtn.TabIndex = 16;
            this.StartBtn.Text = "保存";
            this.StartBtn.UseVisualStyleBackColor = true;
            this.StartBtn.Click += new System.EventHandler(this.StartBtn_Click);
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 50;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.White;
            this.pictureBox1.Image = global::BloodLetter_Fantasy.Properties.Resources.BloodLetter;
            this.pictureBox1.InitialImage = null;
            this.pictureBox1.Location = new System.Drawing.Point(31, 26);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(100, 100);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 22;
            this.pictureBox1.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(48, 162);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(72, 27);
            this.label1.TabIndex = 23;
            this.label1.Text = "快捷键";
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 27F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(624, 235);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pictureBox1);
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
            this.Font = new System.Drawing.Font("Microsoft YaHei UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.MaximizeBox = false;
            this.Name = "Main";
            this.Text = "Bloodletter Assistant v1.0 【ZNH Industry™】";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Main_FormClosed);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
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
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label1;
    }
}

