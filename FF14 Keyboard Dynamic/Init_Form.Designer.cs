namespace XIV_Keyboard_Dynamic
{
    partial class Init_Form
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Init_Form));
            this.ProgressDir = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.SelectProgress = new System.Windows.Forms.Button();
            this.LoadProgress = new System.Windows.Forms.OpenFileDialog();
            this.Isopen = new System.Windows.Forms.Label();
            this.debug = new System.Windows.Forms.RichTextBox();
            this.StartBtn = new System.Windows.Forms.Button();
            this.Repeat = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.Abort = new System.Windows.Forms.Button();
            this.Valueble = new System.Windows.Forms.CheckBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.SettingDir = new System.Windows.Forms.TextBox();
            this.SelectSetting = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.Factory = new System.Windows.Forms.RadioButton();
            this.Paddling = new System.Windows.Forms.RadioButton();
            ((System.ComponentModel.ISupportInitialize)(this.Repeat)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // ProgressDir
            // 
            this.ProgressDir.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.ProgressDir.Location = new System.Drawing.Point(38, 242);
            this.ProgressDir.Name = "ProgressDir";
            this.ProgressDir.ReadOnly = true;
            this.ProgressDir.Size = new System.Drawing.Size(342, 30);
            this.ProgressDir.TabIndex = 0;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(33, 192);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(50, 25);
            this.label2.TabIndex = 2;
            this.label2.Text = "配方";
            // 
            // SelectProgress
            // 
            this.SelectProgress.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.SelectProgress.Location = new System.Drawing.Point(386, 242);
            this.SelectProgress.Name = "SelectProgress";
            this.SelectProgress.Size = new System.Drawing.Size(78, 30);
            this.SelectProgress.TabIndex = 3;
            this.SelectProgress.Text = "加载";
            this.SelectProgress.UseVisualStyleBackColor = true;
            this.SelectProgress.Click += new System.EventHandler(this.SelectProgress_Click);
            // 
            // LoadProgress
            // 
            this.LoadProgress.FileName = "加载生产流程";
            // 
            // Isopen
            // 
            this.Isopen.AutoSize = true;
            this.Isopen.ForeColor = System.Drawing.Color.Red;
            this.Isopen.Location = new System.Drawing.Point(357, 356);
            this.Isopen.Name = "Isopen";
            this.Isopen.Size = new System.Drawing.Size(107, 25);
            this.Isopen.TabIndex = 5;
            this.Isopen.Text = "游戏未运行";
            // 
            // debug
            // 
            this.debug.BackColor = System.Drawing.Color.White;
            this.debug.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.debug.Location = new System.Drawing.Point(38, 384);
            this.debug.Name = "debug";
            this.debug.ReadOnly = true;
            this.debug.Size = new System.Drawing.Size(426, 147);
            this.debug.TabIndex = 6;
            this.debug.Text = "";
            this.debug.TextChanged += new System.EventHandler(this.debug_TextChanged);
            // 
            // StartBtn
            // 
            this.StartBtn.Enabled = false;
            this.StartBtn.Location = new System.Drawing.Point(304, 537);
            this.StartBtn.Name = "StartBtn";
            this.StartBtn.Size = new System.Drawing.Size(160, 38);
            this.StartBtn.TabIndex = 3;
            this.StartBtn.Text = "模式选择";
            this.StartBtn.UseVisualStyleBackColor = true;
            this.StartBtn.Click += new System.EventHandler(this.StartIndustrialization);
            // 
            // Repeat
            // 
            this.Repeat.Location = new System.Drawing.Point(127, 301);
            this.Repeat.Name = "Repeat";
            this.Repeat.Size = new System.Drawing.Size(83, 30);
            this.Repeat.TabIndex = 7;
            this.Repeat.ValueChanged += new System.EventHandler(this.Repeat_ValueChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(33, 303);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(88, 25);
            this.label1.TabIndex = 8;
            this.label1.Text = "重复次数";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(33, 356);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(88, 25);
            this.label3.TabIndex = 9;
            this.label3.Text = "运行状态";
            // 
            // Abort
            // 
            this.Abort.ForeColor = System.Drawing.Color.Red;
            this.Abort.Location = new System.Drawing.Point(38, 537);
            this.Abort.Name = "Abort";
            this.Abort.Size = new System.Drawing.Size(108, 38);
            this.Abort.TabIndex = 3;
            this.Abort.Text = "紧急中断";
            this.Abort.UseVisualStyleBackColor = true;
            this.Abort.Click += new System.EventHandler(this.Abort_Click);
            // 
            // Valueble
            // 
            this.Valueble.AutoSize = true;
            this.Valueble.Location = new System.Drawing.Point(225, 302);
            this.Valueble.Name = "Valueble";
            this.Valueble.Size = new System.Drawing.Size(91, 29);
            this.Valueble.TabIndex = 10;
            this.Valueble.Text = "收藏品";
            this.Valueble.UseVisualStyleBackColor = true;
            this.Valueble.CheckedChanged += new System.EventHandler(this.Valueble_CheckedChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(383, 303);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(81, 25);
            this.label4.TabIndex = 11;
            this.label4.Text = "By:ZNH";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(33, 112);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(88, 25);
            this.label5.TabIndex = 2;
            this.label5.Text = "配置文件";
            // 
            // SettingDir
            // 
            this.SettingDir.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.SettingDir.Location = new System.Drawing.Point(38, 140);
            this.SettingDir.Name = "SettingDir";
            this.SettingDir.ReadOnly = true;
            this.SettingDir.Size = new System.Drawing.Size(342, 30);
            this.SettingDir.TabIndex = 0;
            // 
            // SelectSetting
            // 
            this.SelectSetting.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.SelectSetting.Location = new System.Drawing.Point(386, 140);
            this.SelectSetting.Name = "SelectSetting";
            this.SelectSetting.Size = new System.Drawing.Size(78, 30);
            this.SelectSetting.TabIndex = 3;
            this.SelectSetting.Text = "加载";
            this.SelectSetting.UseVisualStyleBackColor = true;
            this.SelectSetting.Click += new System.EventHandler(this.SelectSetting_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.Paddling);
            this.groupBox1.Controls.Add(this.Factory);
            this.groupBox1.Location = new System.Drawing.Point(38, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(426, 78);
            this.groupBox1.TabIndex = 12;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "模式";
            // 
            // Factory
            // 
            this.Factory.AutoSize = true;
            this.Factory.Location = new System.Drawing.Point(313, 29);
            this.Factory.Name = "Factory";
            this.Factory.Size = new System.Drawing.Size(90, 29);
            this.Factory.TabIndex = 0;
            this.Factory.TabStop = true;
            this.Factory.Text = "工业化";
            this.Factory.UseVisualStyleBackColor = true;
            this.Factory.CheckedChanged += new System.EventHandler(this.radioButton1_CheckedChanged);
            // 
            // Paddling
            // 
            this.Paddling.AutoSize = true;
            this.Paddling.Location = new System.Drawing.Point(6, 29);
            this.Paddling.Name = "Paddling";
            this.Paddling.Size = new System.Drawing.Size(109, 29);
            this.Paddling.TabIndex = 0;
            this.Paddling.TabStop = true;
            this.Paddling.Text = "战斗划水";
            this.Paddling.UseVisualStyleBackColor = true;
            this.Paddling.CheckedChanged += new System.EventHandler(this.radioButton2_CheckedChanged);
            // 
            // Init_Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(488, 595);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.Valueble);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.Repeat);
            this.Controls.Add(this.debug);
            this.Controls.Add(this.Isopen);
            this.Controls.Add(this.Abort);
            this.Controls.Add(this.StartBtn);
            this.Controls.Add(this.SelectSetting);
            this.Controls.Add(this.SelectProgress);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.SettingDir);
            this.Controls.Add(this.ProgressDir);
            this.Font = new System.Drawing.Font("Microsoft YaHei UI", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.Name = "Init_Form";
            this.Text = "FFXIV Keyboard Dynamic";
            this.TopMost = true;
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Init_Form_FormClosed);
            this.Load += new System.EventHandler(this.Init_Form_Load);
            ((System.ComponentModel.ISupportInitialize)(this.Repeat)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox ProgressDir;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button SelectProgress;
        private System.Windows.Forms.OpenFileDialog LoadProgress;
        private System.Windows.Forms.Label Isopen;
        private System.Windows.Forms.Button StartBtn;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown Repeat;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button Abort;
        public System.Windows.Forms.RichTextBox debug;
        private System.Windows.Forms.CheckBox Valueble;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox SettingDir;
        private System.Windows.Forms.Button SelectSetting;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton Paddling;
        private System.Windows.Forms.RadioButton Factory;
    }
}

