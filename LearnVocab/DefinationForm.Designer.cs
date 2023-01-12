using System.Windows.Forms;

namespace LearnVocab
{
    partial class DefinationForm
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.labelVSub = new System.Windows.Forms.TextBox();
            this.saveBtn = new System.Windows.Forms.Button();
            this.labelEn = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.AutoScroll = true;
            this.panel1.BackColor = System.Drawing.SystemColors.Window;
            this.panel1.Controls.Add(this.labelVSub);
            this.panel1.Location = new System.Drawing.Point(18, 95);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(626, 379);
            this.panel1.TabIndex = 4;
            // 
            // labelVSub
            // 
            this.labelVSub.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.labelVSub.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelVSub.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.labelVSub.Location = new System.Drawing.Point(0, 7);
            this.labelVSub.MaximumSize = new System.Drawing.Size(327, 40);
            this.labelVSub.Multiline = true;
            this.labelVSub.Name = "labelVSub";
            this.labelVSub.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.labelVSub.Size = new System.Drawing.Size(327, 32);
            this.labelVSub.TabIndex = 0;
            this.labelVSub.Text = "chạy";
            // 
            // saveBtn
            // 
            this.saveBtn.Location = new System.Drawing.Point(558, 65);
            this.saveBtn.Name = "saveBtn";
            this.saveBtn.Size = new System.Drawing.Size(86, 23);
            this.saveBtn.TabIndex = 5;
            this.saveBtn.Text = "Lưu";
            this.saveBtn.UseVisualStyleBackColor = true;
            this.saveBtn.Click += new System.EventHandler(this.saveBtn_Click);
            // 
            // labelEn
            // 
            this.labelEn.AutoSize = true;
            this.labelEn.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelEn.Location = new System.Drawing.Point(15, 68);
            this.labelEn.Name = "labelEn";
            this.labelEn.Size = new System.Drawing.Size(56, 18);
            this.labelEn.TabIndex = 6;
            this.labelEn.Text = "English";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(18, 26);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(516, 20);
            this.textBox1.TabIndex = 7;
            this.textBox1.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox1_KeyPress);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(558, 26);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(86, 23);
            this.button1.TabIndex = 8;
            this.button1.Text = "Dịch";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // DefinationForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(656, 502);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.labelEn);
            this.Controls.Add(this.saveBtn);
            this.Controls.Add(this.panel1);
            this.Name = "DefinationForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Form3";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox labelVSub;
        private System.Windows.Forms.Button saveBtn;
        private Label labelEn;
        private TextBox textBox1;
        private Button button1;
    }
}