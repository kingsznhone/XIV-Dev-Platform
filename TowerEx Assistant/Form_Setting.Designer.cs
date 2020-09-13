namespace TowerEx_Assistant
{
    partial class Form_Setting
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.TP_Ctrl_Check = new System.Windows.Forms.CheckBox();
            this.TP_Shift_Check = new System.Windows.Forms.CheckBox();
            this.TP_Alt_Check = new System.Windows.Forms.CheckBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // TP_Ctrl_Check
            // 
            this.TP_Ctrl_Check.AutoSize = true;
            this.TP_Ctrl_Check.Location = new System.Drawing.Point(65, 65);
            this.TP_Ctrl_Check.Name = "TP_Ctrl_Check";
            this.TP_Ctrl_Check.Size = new System.Drawing.Size(67, 31);
            this.TP_Ctrl_Check.TabIndex = 0;
            this.TP_Ctrl_Check.Text = "Ctrl";
            this.TP_Ctrl_Check.UseVisualStyleBackColor = true;
            // 
            // TP_Shift_Check
            // 
            this.TP_Shift_Check.AutoSize = true;
            this.TP_Shift_Check.Location = new System.Drawing.Point(158, 65);
            this.TP_Shift_Check.Name = "TP_Shift_Check";
            this.TP_Shift_Check.Size = new System.Drawing.Size(77, 31);
            this.TP_Shift_Check.TabIndex = 0;
            this.TP_Shift_Check.Text = "Shift";
            this.TP_Shift_Check.UseVisualStyleBackColor = true;
            // 
            // TP_Alt_Check
            // 
            this.TP_Alt_Check.AutoSize = true;
            this.TP_Alt_Check.Location = new System.Drawing.Point(261, 65);
            this.TP_Alt_Check.Name = "TP_Alt_Check";
            this.TP_Alt_Check.Size = new System.Drawing.Size(60, 31);
            this.TP_Alt_Check.TabIndex = 0;
            this.TP_Alt_Check.Text = "Alt";
            this.TP_Alt_Check.UseVisualStyleBackColor = true;
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(372, 63);
            this.textBox1.MaxLength = 1;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(145, 33);
            this.textBox1.TabIndex = 1;
            this.textBox1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBox1_KeyUp);
            // 
            // Form_Setting
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 27F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(853, 403);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.TP_Alt_Check);
            this.Controls.Add(this.TP_Shift_Check);
            this.Controls.Add(this.TP_Ctrl_Check);
            this.Font = new System.Drawing.Font("Microsoft YaHei UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "Form_Setting";
            this.Text = "Form_Setting";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox TP_Ctrl_Check;
        private System.Windows.Forms.CheckBox TP_Shift_Check;
        private System.Windows.Forms.CheckBox TP_Alt_Check;
        private System.Windows.Forms.TextBox textBox1;
    }
}