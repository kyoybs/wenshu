namespace WinformSpider
{
    partial class Form1
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
            this.btnGet = new System.Windows.Forms.Button();
            this.txtUrl = new System.Windows.Forms.TextBox();
            this.txtParms = new System.Windows.Forms.TextBox();
            this.btnTest = new System.Windows.Forms.Button();
            this.txtResult = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // btnGet
            // 
            this.btnGet.Location = new System.Drawing.Point(108, 116);
            this.btnGet.Name = "btnGet";
            this.btnGet.Size = new System.Drawing.Size(140, 47);
            this.btnGet.TabIndex = 0;
            this.btnGet.Text = "确定";
            this.btnGet.UseVisualStyleBackColor = true;
            this.btnGet.Click += new System.EventHandler(this.btnGet_Click);
            // 
            // txtUrl
            // 
            this.txtUrl.Location = new System.Drawing.Point(52, 29);
            this.txtUrl.Name = "txtUrl";
            this.txtUrl.Size = new System.Drawing.Size(412, 25);
            this.txtUrl.TabIndex = 1;
            this.txtUrl.Text = "http://wenshu.court.gov.cn/CreateContentJS/CreateListDocZip.aspx?action=1";
            // 
            // txtParms
            // 
            this.txtParms.Location = new System.Drawing.Point(53, 76);
            this.txtParms.Name = "txtParms";
            this.txtParms.Size = new System.Drawing.Size(412, 25);
            this.txtParms.TabIndex = 2;
            this.txtParms.Text = "docIds=5a005846-0c1d-422e-89b2-a8f10104a322|张文中诈骗、单位行贿、挪用资金再审刑事判决书|2018-05-30&key" +
    "Code=";
            // 
            // btnTest
            // 
            this.btnTest.Location = new System.Drawing.Point(108, 247);
            this.btnTest.Name = "btnTest";
            this.btnTest.Size = new System.Drawing.Size(148, 62);
            this.btnTest.TabIndex = 3;
            this.btnTest.Text = "Test";
            this.btnTest.UseVisualStyleBackColor = true;
            this.btnTest.Click += new System.EventHandler(this.btnTest_Click);
            // 
            // txtResult
            // 
            this.txtResult.Location = new System.Drawing.Point(375, 142);
            this.txtResult.Multiline = true;
            this.txtResult.Name = "txtResult";
            this.txtResult.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtResult.Size = new System.Drawing.Size(396, 296);
            this.txtResult.TabIndex = 4;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.txtResult);
            this.Controls.Add(this.btnTest);
            this.Controls.Add(this.txtParms);
            this.Controls.Add(this.txtUrl);
            this.Controls.Add(this.btnGet);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnGet;
        private System.Windows.Forms.TextBox txtUrl;
        private System.Windows.Forms.TextBox txtParms;
        private System.Windows.Forms.Button btnTest;
        private System.Windows.Forms.TextBox txtResult;
    }
}

