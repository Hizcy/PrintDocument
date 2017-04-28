namespace WindowsFormsApplication2
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
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.button1 = new System.Windows.Forms.Button();
            this.textBox = new System.Windows.Forms.TextBox();
            this.FileMenuItem_Print = new System.Windows.Forms.Button();
            this.FileMenuItem_PrintView = new System.Windows.Forms.Button();
            this.FileMenuItem_PageSet = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(444, 12);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // textBox
            // 
            this.textBox.Location = new System.Drawing.Point(444, 56);
            this.textBox.Multiline = true;
            this.textBox.Name = "textBox";
            this.textBox.Size = new System.Drawing.Size(235, 347);
            this.textBox.TabIndex = 1;
            // 
            // FileMenuItem_Print
            // 
            this.FileMenuItem_Print.Location = new System.Drawing.Point(12, 23);
            this.FileMenuItem_Print.Name = "FileMenuItem_Print";
            this.FileMenuItem_Print.Size = new System.Drawing.Size(165, 41);
            this.FileMenuItem_Print.TabIndex = 2;
            this.FileMenuItem_Print.Text = "FileMenuItem_Print";
            this.FileMenuItem_Print.UseVisualStyleBackColor = true;
            this.FileMenuItem_Print.Click += new System.EventHandler(this.FileMenuItem_Print_Click);
            // 
            // FileMenuItem_PrintView
            // 
            this.FileMenuItem_PrintView.Location = new System.Drawing.Point(12, 82);
            this.FileMenuItem_PrintView.Name = "FileMenuItem_PrintView";
            this.FileMenuItem_PrintView.Size = new System.Drawing.Size(185, 33);
            this.FileMenuItem_PrintView.TabIndex = 3;
            this.FileMenuItem_PrintView.Text = "FileMenuItem_PrintView";
            this.FileMenuItem_PrintView.UseVisualStyleBackColor = true;
            this.FileMenuItem_PrintView.Click += new System.EventHandler(this.FileMenuItem_PrintView_Click);
            // 
            // FileMenuItem_PageSet
            // 
            this.FileMenuItem_PageSet.Location = new System.Drawing.Point(12, 152);
            this.FileMenuItem_PageSet.Name = "FileMenuItem_PageSet";
            this.FileMenuItem_PageSet.Size = new System.Drawing.Size(185, 33);
            this.FileMenuItem_PageSet.TabIndex = 3;
            this.FileMenuItem_PageSet.Text = "FileMenuItem_PageSet";
            this.FileMenuItem_PageSet.UseVisualStyleBackColor = true;
            this.FileMenuItem_PageSet.Click += new System.EventHandler(this.FileMenuItem_PageSet_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(13, 219);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(184, 54);
            this.button2.TabIndex = 4;
            this.button2.Text = "button2";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(691, 446);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.FileMenuItem_PageSet);
            this.Controls.Add(this.FileMenuItem_PrintView);
            this.Controls.Add(this.FileMenuItem_Print);
            this.Controls.Add(this.textBox);
            this.Controls.Add(this.button1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox textBox;
        private System.Windows.Forms.Button FileMenuItem_Print;
        private System.Windows.Forms.Button FileMenuItem_PrintView;
        private System.Windows.Forms.Button FileMenuItem_PageSet;
        private System.Windows.Forms.Button button2;
    }
}

