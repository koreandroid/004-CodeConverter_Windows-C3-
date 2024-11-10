namespace CodeConverter {
    partial class Form1 {
        /// <summary>
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 디자이너에서 생성한 코드

        /// <summary>
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
        /// </summary>
        private void InitializeComponent() {
            this.txtSourceCode = new System.Windows.Forms.TextBox();
            this.btnConvert = new System.Windows.Forms.Button();
            this.txtHistory = new System.Windows.Forms.TextBox();
            this.lblRuleInfo = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // txtSourceCode
            // 
            this.txtSourceCode.CausesValidation = false;
            this.txtSourceCode.Font = new System.Drawing.Font("GulimChe", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.txtSourceCode.Location = new System.Drawing.Point(64, 64);
            this.txtSourceCode.Multiline = true;
            this.txtSourceCode.Name = "txtSourceCode";
            this.txtSourceCode.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtSourceCode.Size = new System.Drawing.Size(570, 633);
            this.txtSourceCode.TabIndex = 0;
            this.txtSourceCode.WordWrap = false;
            this.txtSourceCode.TextChanged += new System.EventHandler(this.txtSourceCode_TextChanged);
            // 
            // btnConvert
            // 
            this.btnConvert.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.btnConvert.CausesValidation = false;
            this.btnConvert.FlatAppearance.BorderSize = 0;
            this.btnConvert.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnConvert.Font = new System.Drawing.Font("GulimChe", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btnConvert.ForeColor = System.Drawing.SystemColors.Window;
            this.btnConvert.Location = new System.Drawing.Point(650, 590);
            this.btnConvert.Name = "btnConvert";
            this.btnConvert.Size = new System.Drawing.Size(326, 75);
            this.btnConvert.TabIndex = 1;
            this.btnConvert.Text = "변환하기";
            this.btnConvert.UseVisualStyleBackColor = false;
            this.btnConvert.Click += new System.EventHandler(this.btnConvert_Click);
            // 
            // txtHistory
            // 
            this.txtHistory.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.txtHistory.CausesValidation = false;
            this.txtHistory.Cursor = System.Windows.Forms.Cursors.Default;
            this.txtHistory.Font = new System.Drawing.Font("Gulim", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.txtHistory.ForeColor = System.Drawing.SystemColors.InfoText;
            this.txtHistory.Location = new System.Drawing.Point(650, 366);
            this.txtHistory.Multiline = true;
            this.txtHistory.Name = "txtHistory";
            this.txtHistory.ReadOnly = true;
            this.txtHistory.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtHistory.Size = new System.Drawing.Size(326, 218);
            this.txtHistory.TabIndex = 2;
            this.txtHistory.TabStop = false;
            // 
            // lblRuleInfo
            // 
            this.lblRuleInfo.AutoSize = true;
            this.lblRuleInfo.CausesValidation = false;
            this.lblRuleInfo.Font = new System.Drawing.Font("Gulim", 11.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lblRuleInfo.Location = new System.Drawing.Point(61, 39);
            this.lblRuleInfo.Name = "lblRuleInfo";
            this.lblRuleInfo.Size = new System.Drawing.Size(955, 15);
            this.lblRuleInfo.TabIndex = 3;
            this.lblRuleInfo.Text = "들여쓰기 단위는 공백 4개씩 | 함수에 return문 없으면 기본값으로 0 반환 | 기본적인 유효성 검사 외에 Python 코드작동확인먼저";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CausesValidation = false;
            this.ClientSize = new System.Drawing.Size(1008, 729);
            this.Controls.Add(this.lblRuleInfo);
            this.Controls.Add(this.txtHistory);
            this.Controls.Add(this.btnConvert);
            this.Controls.Add(this.txtSourceCode);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.Text = "CodeConverter";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtSourceCode;
        private System.Windows.Forms.Button btnConvert;
        private System.Windows.Forms.TextBox txtHistory;
        private System.Windows.Forms.Label lblRuleInfo;
    }
}

