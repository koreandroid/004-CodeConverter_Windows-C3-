namespace CodeConverter {
    partial class Form2 {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.lblSourceCode = new System.Windows.Forms.Label();
            this.txtSourceCode = new System.Windows.Forms.TextBox();
            this.lblTargetCode = new System.Windows.Forms.Label();
            this.txtTargetCode = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // lblSourceCode
            // 
            this.lblSourceCode.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.lblSourceCode.AutoSize = true;
            this.lblSourceCode.CausesValidation = false;
            this.lblSourceCode.Font = new System.Drawing.Font("Gulim", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lblSourceCode.Location = new System.Drawing.Point(29, 14);
            this.lblSourceCode.Name = "lblSourceCode";
            this.lblSourceCode.Size = new System.Drawing.Size(150, 15);
            this.lblSourceCode.TabIndex = 0;
            this.lblSourceCode.Text = "원본 코드 (Python)";
            // 
            // txtSourceCode
            // 
            this.txtSourceCode.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.txtSourceCode.BackColor = System.Drawing.SystemColors.Window;
            this.txtSourceCode.CausesValidation = false;
            this.txtSourceCode.Font = new System.Drawing.Font("GulimChe", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.txtSourceCode.Location = new System.Drawing.Point(32, 32);
            this.txtSourceCode.Multiline = true;
            this.txtSourceCode.Name = "txtSourceCode";
            this.txtSourceCode.ReadOnly = true;
            this.txtSourceCode.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtSourceCode.Size = new System.Drawing.Size(440, 665);
            this.txtSourceCode.TabIndex = 1;
            this.txtSourceCode.Visible = false;
            this.txtSourceCode.WordWrap = false;
            // 
            // lblTargetCode
            // 
            this.lblTargetCode.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.lblTargetCode.AutoSize = true;
            this.lblTargetCode.CausesValidation = false;
            this.lblTargetCode.Font = new System.Drawing.Font("Gulim", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lblTargetCode.Location = new System.Drawing.Point(533, 14);
            this.lblTargetCode.Name = "lblTargetCode";
            this.lblTargetCode.Size = new System.Drawing.Size(33, 15);
            this.lblTargetCode.TabIndex = 2;
            this.lblTargetCode.Text = "null";
            // 
            // txtTargetCode
            // 
            this.txtTargetCode.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtTargetCode.BackColor = System.Drawing.SystemColors.Window;
            this.txtTargetCode.CausesValidation = false;
            this.txtTargetCode.Font = new System.Drawing.Font("GulimChe", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.txtTargetCode.Location = new System.Drawing.Point(536, 32);
            this.txtTargetCode.Multiline = true;
            this.txtTargetCode.Name = "txtTargetCode";
            this.txtTargetCode.ReadOnly = true;
            this.txtTargetCode.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtTargetCode.Size = new System.Drawing.Size(440, 665);
            this.txtTargetCode.TabIndex = 3;
            this.txtTargetCode.Visible = false;
            this.txtTargetCode.WordWrap = false;
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CausesValidation = false;
            this.ClientSize = new System.Drawing.Size(1008, 729);
            this.Controls.Add(this.txtTargetCode);
            this.Controls.Add(this.txtSourceCode);
            this.Controls.Add(this.lblTargetCode);
            this.Controls.Add(this.lblSourceCode);
            this.Name = "Form2";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "null";
            this.Load += new System.EventHandler(this.Form2_Load);
            this.Resize += new System.EventHandler(this.Form2_Resize);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblSourceCode;
        private System.Windows.Forms.TextBox txtSourceCode;
        private System.Windows.Forms.Label lblTargetCode;
        private System.Windows.Forms.TextBox txtTargetCode;
    }
}