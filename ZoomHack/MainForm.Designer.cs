﻿namespace Zoom_Assistant
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.label1 = new System.Windows.Forms.Label();
            this.ZoomUpDown = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.SetDefaultbtn = new System.Windows.Forms.Button();
            this.FOVUpDown = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.ZoomUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.FOVUpDown)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(31, 69);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(92, 27);
            this.label1.TabIndex = 0;
            this.label1.Text = "视距解锁";
            // 
            // ZoomUpDown
            // 
            this.ZoomUpDown.Location = new System.Drawing.Point(244, 69);
            this.ZoomUpDown.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.ZoomUpDown.Minimum = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.ZoomUpDown.Name = "ZoomUpDown";
            this.ZoomUpDown.Size = new System.Drawing.Size(201, 33);
            this.ZoomUpDown.TabIndex = 1;
            this.ZoomUpDown.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.ZoomUpDown.Value = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.ZoomUpDown.ValueChanged += new System.EventHandler(this.ZoomUpDown_ValueChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(29, 124);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(94, 27);
            this.label2.TabIndex = 2;
            this.label2.Text = "POV解锁";
            // 
            // SetDefaultbtn
            // 
            this.SetDefaultbtn.Location = new System.Drawing.Point(315, 12);
            this.SetDefaultbtn.Name = "SetDefaultbtn";
            this.SetDefaultbtn.Size = new System.Drawing.Size(130, 35);
            this.SetDefaultbtn.TabIndex = 5;
            this.SetDefaultbtn.Text = "恢复默认";
            this.SetDefaultbtn.UseVisualStyleBackColor = true;
            this.SetDefaultbtn.Click += new System.EventHandler(this.SetDefaultbtn_Click);
            // 
            // POVUpDown
            // 
            this.FOVUpDown.BackColor = System.Drawing.Color.White;
            this.FOVUpDown.Location = new System.Drawing.Point(244, 122);
            this.FOVUpDown.Maximum = new decimal(new int[] {
            150,
            0,
            0,
            0});
            this.FOVUpDown.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.FOVUpDown.Name = "POVUpDown";
            this.FOVUpDown.Size = new System.Drawing.Size(201, 33);
            this.FOVUpDown.TabIndex = 1;
            this.FOVUpDown.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.FOVUpDown.Value = new decimal(new int[] {
            45,
            0,
            0,
            0});
            this.FOVUpDown.ValueChanged += new System.EventHandler(this.FOVUpDown_ValueChanged);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 27F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(457, 181);
            this.Controls.Add(this.SetDefaultbtn);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.FOVUpDown);
            this.Controls.Add(this.ZoomUpDown);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Microsoft YaHei UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MainForm";
            this.Text = "Zoom Assistant 【ZNH Industry™】";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainForm_FormClosed);
            ((System.ComponentModel.ISupportInitialize)(this.ZoomUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.FOVUpDown)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown ZoomUpDown;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button SetDefaultbtn;
        private System.Windows.Forms.NumericUpDown FOVUpDown;
    }
}

