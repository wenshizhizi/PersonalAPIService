namespace PersonalAPIService
{
    partial class ServiceContainer
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
            this.btnStartup = new System.Windows.Forms.Button();
            this.gbHostBox = new System.Windows.Forms.GroupBox();
            this.textBoxPort = new System.Windows.Forms.TextBox();
            this.labelPort = new System.Windows.Forms.Label();
            this.textBoxIP = new System.Windows.Forms.TextBox();
            this.labelIP = new System.Windows.Forms.Label();
            this.gbHostBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnStartup
            // 
            this.btnStartup.BackColor = System.Drawing.Color.Sienna;
            this.btnStartup.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnStartup.Location = new System.Drawing.Point(546, 252);
            this.btnStartup.Name = "btnStartup";
            this.btnStartup.Size = new System.Drawing.Size(75, 23);
            this.btnStartup.TabIndex = 0;
            this.btnStartup.Text = "启动服务";
            this.btnStartup.UseVisualStyleBackColor = false;
            this.btnStartup.Click += new System.EventHandler(this.btnStartup_Click);
            // 
            // gbHostBox
            // 
            this.gbHostBox.Controls.Add(this.textBoxPort);
            this.gbHostBox.Controls.Add(this.labelPort);
            this.gbHostBox.Controls.Add(this.textBoxIP);
            this.gbHostBox.Controls.Add(this.labelIP);
            this.gbHostBox.Location = new System.Drawing.Point(440, 12);
            this.gbHostBox.Name = "gbHostBox";
            this.gbHostBox.Size = new System.Drawing.Size(181, 234);
            this.gbHostBox.TabIndex = 1;
            this.gbHostBox.TabStop = false;
            this.gbHostBox.Text = "服务端配置";
            // 
            // textBoxPort
            // 
            this.textBoxPort.Location = new System.Drawing.Point(46, 47);
            this.textBoxPort.Name = "textBoxPort";
            this.textBoxPort.Size = new System.Drawing.Size(129, 21);
            this.textBoxPort.TabIndex = 3;
            this.textBoxPort.Text = "9529";
            // 
            // labelPort
            // 
            this.labelPort.AutoSize = true;
            this.labelPort.ForeColor = System.Drawing.Color.Maroon;
            this.labelPort.Location = new System.Drawing.Point(11, 50);
            this.labelPort.Name = "labelPort";
            this.labelPort.Size = new System.Drawing.Size(29, 12);
            this.labelPort.TabIndex = 2;
            this.labelPort.Text = "Port";
            // 
            // textBoxIP
            // 
            this.textBoxIP.Location = new System.Drawing.Point(46, 20);
            this.textBoxIP.Name = "textBoxIP";
            this.textBoxIP.Size = new System.Drawing.Size(129, 21);
            this.textBoxIP.TabIndex = 1;
            this.textBoxIP.Text = "10.2.1.101";
            // 
            // labelIP
            // 
            this.labelIP.AutoSize = true;
            this.labelIP.ForeColor = System.Drawing.Color.Maroon;
            this.labelIP.Location = new System.Drawing.Point(23, 23);
            this.labelIP.Name = "labelIP";
            this.labelIP.Size = new System.Drawing.Size(17, 12);
            this.labelIP.TabIndex = 0;
            this.labelIP.Text = "IP";
            // 
            // ServiceContainer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.BurlyWood;
            this.ClientSize = new System.Drawing.Size(633, 287);
            this.Controls.Add(this.gbHostBox);
            this.Controls.Add(this.btnStartup);
            this.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "ServiceContainer";
            this.Text = "个人服务程序";
            this.gbHostBox.ResumeLayout(false);
            this.gbHostBox.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnStartup;
        private System.Windows.Forms.GroupBox gbHostBox;
        private System.Windows.Forms.Label labelIP;
        private System.Windows.Forms.TextBox textBoxIP;
        private System.Windows.Forms.TextBox textBoxPort;
        private System.Windows.Forms.Label labelPort;
    }
}

