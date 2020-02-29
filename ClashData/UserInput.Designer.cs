namespace UserInput_Form
{
    partial class UserInput
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
            System.Windows.Forms.ListViewItem listViewItem1 = new System.Windows.Forms.ListViewItem("");
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UserInput));
            this.fzInput = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.fzAddBtn = new System.Windows.Forms.Button();
            this.fzCancelBtn = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.dcInputTextBox = new System.Windows.Forms.TextBox();
            this.dnInputTextBox = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.fzListBox = new System.Windows.Forms.ListBox();
            this.disAddBtn = new System.Windows.Forms.Button();
            this.disCancelBtn = new System.Windows.Forms.Button();
            this.label9 = new System.Windows.Forms.Label();
            this.fzRemoveBtn = new System.Windows.Forms.Button();
            this.disRemoveBtn = new System.Windows.Forms.Button();
            this.inEnterBtn = new System.Windows.Forms.Button();
            this.inCancelBtn = new System.Windows.Forms.Button();
            this.label10 = new System.Windows.Forms.Label();
            this.pdListView = new System.Windows.Forms.ListView();
            this.pdLoadBtn = new System.Windows.Forms.Button();
            this.pdSaveBtn = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // fzInput
            // 
            this.fzInput.Location = new System.Drawing.Point(556, 85);
            this.fzInput.Margin = new System.Windows.Forms.Padding(1);
            this.fzInput.Name = "fzInput";
            this.fzInput.Size = new System.Drawing.Size(195, 22);
            this.fzInput.TabIndex = 0;
            this.fzInput.KeyDown += new System.Windows.Forms.KeyEventHandler(this.fzInput_KeyDown);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(555, 111);
            this.label3.Margin = new System.Windows.Forms.Padding(1, 0, 1, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(110, 17);
            this.label3.TabIndex = 1;
            this.label3.Text = "(Ex: L01, B01...)";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(10, 32);
            this.label4.Margin = new System.Windows.Forms.Padding(1, 0, 1, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(144, 17);
            this.label4.TabIndex = 2;
            this.label4.Text = "1. Define Focus Zone";
            // 
            // fzAddBtn
            // 
            this.fzAddBtn.Location = new System.Drawing.Point(586, 142);
            this.fzAddBtn.Margin = new System.Windows.Forms.Padding(1);
            this.fzAddBtn.Name = "fzAddBtn";
            this.fzAddBtn.Size = new System.Drawing.Size(71, 29);
            this.fzAddBtn.TabIndex = 3;
            this.fzAddBtn.Text = "Add";
            this.fzAddBtn.UseVisualStyleBackColor = true;
            this.fzAddBtn.MouseClick += new System.Windows.Forms.MouseEventHandler(this.fzAddBtn_MouseClick);
            // 
            // fzCancelBtn
            // 
            this.fzCancelBtn.Location = new System.Drawing.Point(660, 142);
            this.fzCancelBtn.Margin = new System.Windows.Forms.Padding(1);
            this.fzCancelBtn.Name = "fzCancelBtn";
            this.fzCancelBtn.Size = new System.Drawing.Size(71, 29);
            this.fzCancelBtn.TabIndex = 4;
            this.fzCancelBtn.Text = "Cancel";
            this.fzCancelBtn.UseVisualStyleBackColor = true;
            this.fzCancelBtn.Click += new System.EventHandler(this.fzCancelBtn_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(10, 202);
            this.label5.Margin = new System.Windows.Forms.Padding(1, 0, 1, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(333, 17);
            this.label5.TabIndex = 5;
            this.label5.Text = "2. Define Discipline Code(s) and Discipline Name(s)";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(156, 240);
            this.label6.Margin = new System.Windows.Forms.Padding(1, 0, 1, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(123, 17);
            this.label6.TabIndex = 7;
            this.label6.Text = "Project Disciplines";
            // 
            // dcInputTextBox
            // 
            this.dcInputTextBox.Location = new System.Drawing.Point(556, 298);
            this.dcInputTextBox.Margin = new System.Windows.Forms.Padding(1);
            this.dcInputTextBox.Name = "dcInputTextBox";
            this.dcInputTextBox.Size = new System.Drawing.Size(195, 22);
            this.dcInputTextBox.TabIndex = 8;
            // 
            // dnInputTextBox
            // 
            this.dnInputTextBox.Location = new System.Drawing.Point(556, 378);
            this.dnInputTextBox.Margin = new System.Windows.Forms.Padding(1);
            this.dnInputTextBox.Name = "dnInputTextBox";
            this.dnInputTextBox.Size = new System.Drawing.Size(195, 22);
            this.dnInputTextBox.TabIndex = 9;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(591, 279);
            this.label7.Margin = new System.Windows.Forms.Padding(1, 0, 1, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(140, 17);
            this.label7.TabIndex = 10;
            this.label7.Text = "Input Discipline Code";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(594, 360);
            this.label8.Margin = new System.Windows.Forms.Padding(1, 0, 1, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(144, 17);
            this.label8.TabIndex = 11;
            this.label8.Text = "Input Discipline Name";
            // 
            // fzListBox
            // 
            this.fzListBox.FormattingEnabled = true;
            this.fzListBox.ItemHeight = 16;
            this.fzListBox.Location = new System.Drawing.Point(119, 82);
            this.fzListBox.Margin = new System.Windows.Forms.Padding(1);
            this.fzListBox.Name = "fzListBox";
            this.fzListBox.Size = new System.Drawing.Size(188, 20);
            this.fzListBox.TabIndex = 12;
            // 
            // disAddBtn
            // 
            this.disAddBtn.Location = new System.Drawing.Point(584, 436);
            this.disAddBtn.Margin = new System.Windows.Forms.Padding(1);
            this.disAddBtn.Name = "disAddBtn";
            this.disAddBtn.Size = new System.Drawing.Size(74, 29);
            this.disAddBtn.TabIndex = 13;
            this.disAddBtn.Text = "Add";
            this.disAddBtn.UseVisualStyleBackColor = true;
            this.disAddBtn.MouseClick += new System.Windows.Forms.MouseEventHandler(this.disAddBtn_MouseClick);
            // 
            // disCancelBtn
            // 
            this.disCancelBtn.Location = new System.Drawing.Point(660, 436);
            this.disCancelBtn.Margin = new System.Windows.Forms.Padding(1);
            this.disCancelBtn.Name = "disCancelBtn";
            this.disCancelBtn.Size = new System.Drawing.Size(74, 29);
            this.disCancelBtn.TabIndex = 14;
            this.disCancelBtn.Text = "Cancel";
            this.disCancelBtn.UseVisualStyleBackColor = true;
            this.disCancelBtn.MouseClick += new System.Windows.Forms.MouseEventHandler(this.disCancelBtn_MouseClick);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(172, 65);
            this.label9.Margin = new System.Windows.Forms.Padding(1, 0, 1, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(83, 17);
            this.label9.TabIndex = 15;
            this.label9.Text = "Focus Zone";
            // 
            // fzRemoveBtn
            // 
            this.fzRemoveBtn.Location = new System.Drawing.Point(176, 106);
            this.fzRemoveBtn.Margin = new System.Windows.Forms.Padding(1);
            this.fzRemoveBtn.Name = "fzRemoveBtn";
            this.fzRemoveBtn.Size = new System.Drawing.Size(71, 29);
            this.fzRemoveBtn.TabIndex = 16;
            this.fzRemoveBtn.Text = "Remove";
            this.fzRemoveBtn.UseVisualStyleBackColor = true;
            this.fzRemoveBtn.MouseClick += new System.Windows.Forms.MouseEventHandler(this.fzRemoveBtn_MouseClick);
            // 
            // disRemoveBtn
            // 
            this.disRemoveBtn.Location = new System.Drawing.Point(176, 486);
            this.disRemoveBtn.Margin = new System.Windows.Forms.Padding(1);
            this.disRemoveBtn.Name = "disRemoveBtn";
            this.disRemoveBtn.Size = new System.Drawing.Size(71, 29);
            this.disRemoveBtn.TabIndex = 17;
            this.disRemoveBtn.Text = "Remove";
            this.disRemoveBtn.UseVisualStyleBackColor = true;
            this.disRemoveBtn.MouseClick += new System.Windows.Forms.MouseEventHandler(this.disRemoveBtn_MouseClick);
            // 
            // inEnterBtn
            // 
            this.inEnterBtn.AutoSize = true;
            this.inEnterBtn.Location = new System.Drawing.Point(681, 514);
            this.inEnterBtn.Margin = new System.Windows.Forms.Padding(1);
            this.inEnterBtn.Name = "inEnterBtn";
            this.inEnterBtn.Size = new System.Drawing.Size(71, 29);
            this.inEnterBtn.TabIndex = 18;
            this.inEnterBtn.Text = "Enter";
            this.inEnterBtn.UseVisualStyleBackColor = true;
            this.inEnterBtn.MouseClick += new System.Windows.Forms.MouseEventHandler(this.inEnterBtn_MouseClick);
            // 
            // inCancelBtn
            // 
            this.inCancelBtn.Location = new System.Drawing.Point(755, 514);
            this.inCancelBtn.Margin = new System.Windows.Forms.Padding(1);
            this.inCancelBtn.Name = "inCancelBtn";
            this.inCancelBtn.Size = new System.Drawing.Size(71, 29);
            this.inCancelBtn.TabIndex = 19;
            this.inCancelBtn.Text = "Cancel";
            this.inCancelBtn.UseVisualStyleBackColor = true;
            this.inCancelBtn.MouseClick += new System.Windows.Forms.MouseEventHandler(this.inCancelBtn_MouseClick);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(602, 65);
            this.label10.Margin = new System.Windows.Forms.Padding(1, 0, 1, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(118, 17);
            this.label10.TabIndex = 20;
            this.label10.Text = "Input Focus Zone";
            // 
            // pdListView
            // 
            this.pdListView.FullRowSelect = true;
            this.pdListView.HideSelection = false;
            this.pdListView.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
            listViewItem1});
            this.pdListView.Location = new System.Drawing.Point(46, 258);
            this.pdListView.Margin = new System.Windows.Forms.Padding(1);
            this.pdListView.Name = "pdListView";
            this.pdListView.Size = new System.Drawing.Size(329, 225);
            this.pdListView.TabIndex = 21;
            this.pdListView.UseCompatibleStateImageBehavior = false;
            this.pdListView.View = System.Windows.Forms.View.Details;
            // 
            // pdLoadBtn
            // 
            this.pdLoadBtn.Location = new System.Drawing.Point(379, 329);
            this.pdLoadBtn.Margin = new System.Windows.Forms.Padding(1);
            this.pdLoadBtn.Name = "pdLoadBtn";
            this.pdLoadBtn.Size = new System.Drawing.Size(71, 29);
            this.pdLoadBtn.TabIndex = 22;
            this.pdLoadBtn.Text = "Load...";
            this.pdLoadBtn.UseVisualStyleBackColor = true;
            this.pdLoadBtn.MouseClick += new System.Windows.Forms.MouseEventHandler(this.pdLoad_MouseClick);
            // 
            // pdSaveBtn
            // 
            this.pdSaveBtn.Location = new System.Drawing.Point(379, 360);
            this.pdSaveBtn.Margin = new System.Windows.Forms.Padding(1);
            this.pdSaveBtn.Name = "pdSaveBtn";
            this.pdSaveBtn.Size = new System.Drawing.Size(71, 29);
            this.pdSaveBtn.TabIndex = 23;
            this.pdSaveBtn.Text = "Save";
            this.pdSaveBtn.UseVisualStyleBackColor = true;
            this.pdSaveBtn.MouseClick += new System.Windows.Forms.MouseEventHandler(this.pdSaveBtn_MouseClick);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(554, 324);
            this.label1.Margin = new System.Windows.Forms.Padding(1, 0, 1, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(130, 17);
            this.label1.TabIndex = 24;
            this.label1.Text = "(Ex: PL, MD, ELEC)";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(552, 404);
            this.label2.Margin = new System.Windows.Forms.Padding(1, 0, 1, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(164, 17);
            this.label2.TabIndex = 25;
            this.label2.Text = "(Ex: Plumbing, Electrical)";
            // 
            // UserInput
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(120F, 120F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(858, 558);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pdSaveBtn);
            this.Controls.Add(this.pdLoadBtn);
            this.Controls.Add(this.pdListView);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.inCancelBtn);
            this.Controls.Add(this.inEnterBtn);
            this.Controls.Add(this.disRemoveBtn);
            this.Controls.Add(this.fzRemoveBtn);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.disCancelBtn);
            this.Controls.Add(this.disAddBtn);
            this.Controls.Add(this.fzListBox);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.dnInputTextBox);
            this.Controls.Add(this.dcInputTextBox);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.fzCancelBtn);
            this.Controls.Add(this.fzAddBtn);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.fzInput);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(1);
            this.Name = "UserInput";
            this.Text = "Clash Data - Input";
            this.Load += new System.EventHandler(this.FZ_Input_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        //private System.Windows.Forms.TextBox textBox1;
        //private System.Windows.Forms.Label label1;
        //private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox fzInput;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button fzAddBtn;
        private System.Windows.Forms.Button fzCancelBtn;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox dcInputTextBox;
        private System.Windows.Forms.TextBox dnInputTextBox;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ListBox fzListBox;
        private System.Windows.Forms.Button disAddBtn;
        private System.Windows.Forms.Button disCancelBtn;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Button fzRemoveBtn;
        private System.Windows.Forms.Button disRemoveBtn;
        private System.Windows.Forms.Button inEnterBtn;
        private System.Windows.Forms.Button inCancelBtn;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.ListView pdListView;
        private System.Windows.Forms.Button pdLoadBtn;
        private System.Windows.Forms.Button pdSaveBtn;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
    }
}