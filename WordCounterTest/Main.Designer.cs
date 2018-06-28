namespace WordCounterTest
{
    partial class Main
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
            this.lblKeywords = new System.Windows.Forms.Label();
            this.txtKeywords = new System.Windows.Forms.TextBox();
            this.lblURL = new System.Windows.Forms.Label();
            this.txtURL = new System.Windows.Forms.TextBox();
            this.cmdTest = new System.Windows.Forms.Button();
            this.lblTest = new System.Windows.Forms.Label();
            this.cmdClear = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblKeywords
            // 
            this.lblKeywords.AutoSize = true;
            this.lblKeywords.Location = new System.Drawing.Point(20, 20);
            this.lblKeywords.Name = "lblKeywords";
            this.lblKeywords.Size = new System.Drawing.Size(112, 25);
            this.lblKeywords.TabIndex = 0;
            this.lblKeywords.Text = "Keywords:";
            // 
            // txtKeywords
            // 
            this.txtKeywords.Location = new System.Drawing.Point(150, 20);
            this.txtKeywords.Name = "txtKeywords";
            this.txtKeywords.Size = new System.Drawing.Size(600, 31);
            this.txtKeywords.TabIndex = 1;
            // 
            // lblURL
            // 
            this.lblURL.AutoSize = true;
            this.lblURL.Location = new System.Drawing.Point(20, 70);
            this.lblURL.Name = "lblURL";
            this.lblURL.Size = new System.Drawing.Size(60, 25);
            this.lblURL.TabIndex = 2;
            this.lblURL.Text = "URL:";
            // 
            // txtURL
            // 
            this.txtURL.Location = new System.Drawing.Point(150, 70);
            this.txtURL.Name = "txtURL";
            this.txtURL.Size = new System.Drawing.Size(600, 31);
            this.txtURL.TabIndex = 3;
            // 
            // cmdTest
            // 
            this.cmdTest.Location = new System.Drawing.Point(20, 150);
            this.cmdTest.Name = "cmdTest";
            this.cmdTest.Size = new System.Drawing.Size(80, 45);
            this.cmdTest.TabIndex = 4;
            this.cmdTest.Text = "Test";
            this.cmdTest.UseVisualStyleBackColor = true;
            this.cmdTest.Click += new System.EventHandler(this.cmdTest_Click);
            // 
            // lblTest
            // 
            this.lblTest.Location = new System.Drawing.Point(150, 150);
            this.lblTest.Name = "lblTest";
            this.lblTest.Size = new System.Drawing.Size(601, 53);
            this.lblTest.TabIndex = 5;
            this.lblTest.Text = "0 occurrences of the keywords were found in this URL.";
            // 
            // cmdClear
            // 
            this.cmdClear.Location = new System.Drawing.Point(20, 210);
            this.cmdClear.Name = "cmdClear";
            this.cmdClear.Size = new System.Drawing.Size(80, 45);
            this.cmdClear.TabIndex = 6;
            this.cmdClear.Text = "Clear";
            this.cmdClear.UseVisualStyleBackColor = true;
            this.cmdClear.Click += new System.EventHandler(this.cmdClear_Click);
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.cmdClear);
            this.Controls.Add(this.lblTest);
            this.Controls.Add(this.cmdTest);
            this.Controls.Add(this.txtURL);
            this.Controls.Add(this.lblURL);
            this.Controls.Add(this.txtKeywords);
            this.Controls.Add(this.lblKeywords);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Main";
            this.Text = "WordCounter Test";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblKeywords;
        private System.Windows.Forms.TextBox txtKeywords;
        private System.Windows.Forms.Label lblURL;
        private System.Windows.Forms.TextBox txtURL;
        private System.Windows.Forms.Button cmdTest;
        private System.Windows.Forms.Label lblTest;
        private System.Windows.Forms.Button cmdClear;
    }
}

