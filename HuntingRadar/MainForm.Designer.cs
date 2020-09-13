namespace HuntingRadar
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
            this.components = new System.ComponentModel.Container();
            this.lGamestatus = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.CombatantListBox = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // lGamestatus
            // 
            this.lGamestatus.AutoSize = true;
            this.lGamestatus.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lGamestatus.ForeColor = System.Drawing.Color.Red;
            this.lGamestatus.Location = new System.Drawing.Point(650, 12);
            this.lGamestatus.Name = "lGamestatus";
            this.lGamestatus.Size = new System.Drawing.Size(218, 29);
            this.lGamestatus.TabIndex = 1;
            this.lGamestatus.Text = "Game Is Not Running";
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.Timer1_Tick);
            // 
            // CombatantListBox
            // 
            this.CombatantListBox.FormattingEnabled = true;
            this.CombatantListBox.ItemHeight = 27;
            this.CombatantListBox.Location = new System.Drawing.Point(12, 12);
            this.CombatantListBox.Name = "CombatantListBox";
            this.CombatantListBox.Size = new System.Drawing.Size(205, 544);
            this.CombatantListBox.TabIndex = 2;
            this.CombatantListBox.SelectedIndexChanged += new System.EventHandler(this.CombatantListBox_SelectedIndexChanged);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 27F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(880, 575);
            this.Controls.Add(this.CombatantListBox);
            this.Controls.Add(this.lGamestatus);
            this.Font = new System.Drawing.Font("Microsoft YaHei UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "MainForm";
            this.Text = "Hunting Radar";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainForm_FormClosed);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label lGamestatus;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.ListBox CombatantListBox;
    }
}

