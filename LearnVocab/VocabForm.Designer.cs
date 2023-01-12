namespace LearnVocab
{
    partial class VocabForm
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
            this.lblEn = new System.Windows.Forms.Label();
            this.lblDefine = new System.Windows.Forms.Label();
            this.lblType = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lblEn
            // 
            this.lblEn.AutoSize = true;
            this.lblEn.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEn.Location = new System.Drawing.Point(32, 25);
            this.lblEn.Name = "lblEn";
            this.lblEn.Size = new System.Drawing.Size(51, 20);
            this.lblEn.TabIndex = 0;
            this.lblEn.Text = "label1";
            // 
            // lblDefine
            // 
            this.lblDefine.AutoSize = true;
            this.lblDefine.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDefine.Location = new System.Drawing.Point(32, 128);
            this.lblDefine.Name = "lblDefine";
            this.lblDefine.Size = new System.Drawing.Size(51, 20);
            this.lblDefine.TabIndex = 1;
            this.lblDefine.Text = "label1";
            // 
            // lblType
            // 
            this.lblType.AutoSize = true;
            this.lblType.Location = new System.Drawing.Point(36, 59);
            this.lblType.Name = "lblType";
            this.lblType.Size = new System.Drawing.Size(35, 13);
            this.lblType.TabIndex = 2;
            this.lblType.Text = "label1";
            // 
            // VocabForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(375, 209);
            this.Controls.Add(this.lblType);
            this.Controls.Add(this.lblDefine);
            this.Controls.Add(this.lblEn);
            this.Name = "VocabForm";
            this.Text = "VocabForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblEn;
        private System.Windows.Forms.Label lblDefine;
        private System.Windows.Forms.Label lblType;
    }
}