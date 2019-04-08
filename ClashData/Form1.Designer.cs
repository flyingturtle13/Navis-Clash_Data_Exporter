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
            this.label1 = new System.Windows.Forms.Label();
            this.reportTest = new System.Windows.Forms.Button();
            this.totObj = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(149, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Select type of report to export.";
            // 
            // reportTest
            // 
            this.reportTest.Location = new System.Drawing.Point(47, 45);
            this.reportTest.Name = "reportTest";
            this.reportTest.Size = new System.Drawing.Size(235, 24);
            this.reportTest.TabIndex = 1;
            this.reportTest.Text = "Clash Test";
            this.reportTest.UseVisualStyleBackColor = true;
            this.reportTest.Click += new System.EventHandler(this.reportTest_Click);
            // 
            // totObj
            // 
            this.totObj.Location = new System.Drawing.Point(47, 90);
            this.totObj.Name = "totObj";
            this.totObj.Size = new System.Drawing.Size(235, 23);
            this.totObj.TabIndex = 3;
            this.totObj.Text = "Total Objects by Discipline";
            this.totObj.UseVisualStyleBackColor = true;
            this.totObj.Click += new System.EventHandler(this.totObj_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(255, 138);
            this.button1.Margin = new System.Windows.Forms.Padding(1, 1, 1, 1);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(54, 24);
            this.button1.TabIndex = 4;
            this.button1.Text = "Cancel";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(338, 183);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.totObj);
            this.Controls.Add(this.reportTest);
            this.Controls.Add(this.label1);
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