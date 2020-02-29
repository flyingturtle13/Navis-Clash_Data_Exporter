namespace ClashData
{
    partial class Form1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.label1 = new System.Windows.Forms.Label();
            this.reportTest = new System.Windows.Forms.Button();
            this.totObj = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(16, 11);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(199, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "Select type of report to export.";
            // 
            // reportTest
            // 
            this.reportTest.Location = new System.Drawing.Point(63, 55);
            this.reportTest.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.reportTest.Name = "reportTest";
            this.reportTest.Size = new System.Drawing.Size(313, 30);
            this.reportTest.TabIndex = 1;
            this.reportTest.Text = "Clash Test";
            this.reportTest.UseVisualStyleBackColor = true;
            this.reportTest.Click += new System.EventHandler(this.reportTest_Click);
            // 
            // totObj
            // 
            this.totObj.Location = new System.Drawing.Point(63, 111);
            this.totObj.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.totObj.Name = "totObj";
            this.totObj.Size = new System.Drawing.Size(313, 28);
            this.totObj.TabIndex = 3;
            this.totObj.Text = "Total Objects by Discipline";
            this.totObj.UseVisualStyleBackColor = true;
            this.totObj.Click += new System.EventHandler(this.totObj_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(340, 170);
            this.button1.Margin = new System.Windows.Forms.Padding(1);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(72, 30);
            this.button1.TabIndex = 4;
            this.button1.Text = "Cancel";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(451, 225);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.totObj);
            this.Controls.Add(this.reportTest);
            this.Controls.Add(this.label1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "Form1";
            this.Text = "Clash Data Exporter";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button reportTest;
        private System.Windows.Forms.Button totObj;
        private System.Windows.Forms.Button button1;
    }
}