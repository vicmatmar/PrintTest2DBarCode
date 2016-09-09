namespace PrintTest
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
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.buttonEncode = new System.Windows.Forms.Button();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.numericUpDownSize = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownDPI = new System.Windows.Forms.NumericUpDown();
            this.buttonPrint = new System.Windows.Forms.Button();
            this.printDialog1 = new System.Windows.Forms.PrintDialog();
            this.printDocument1 = new System.Drawing.Printing.PrintDocument();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.radioButton_in = new System.Windows.Forms.RadioButton();
            this.radioButton_mm = new System.Windows.Forms.RadioButton();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownSize)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownDPI)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(491, 269);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(491, 300);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 1;
            this.button2.Text = "button2";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox1.Location = new System.Drawing.Point(13, 13);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(255, 253);
            this.pictureBox1.TabIndex = 2;
            this.pictureBox1.TabStop = false;
            // 
            // buttonEncode
            // 
            this.buttonEncode.Location = new System.Drawing.Point(14, 310);
            this.buttonEncode.Name = "buttonEncode";
            this.buttonEncode.Size = new System.Drawing.Size(75, 23);
            this.buttonEncode.TabIndex = 3;
            this.buttonEncode.Text = "&Encode";
            this.buttonEncode.UseVisualStyleBackColor = true;
            this.buttonEncode.Click += new System.EventHandler(this.buttonEncode_Click);
            // 
            // pictureBox2
            // 
            this.pictureBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox2.Location = new System.Drawing.Point(279, 13);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(255, 253);
            this.pictureBox2.TabIndex = 4;
            this.pictureBox2.TabStop = false;
            // 
            // numericUpDownSize
            // 
            this.numericUpDownSize.DecimalPlaces = 2;
            this.numericUpDownSize.Increment = new decimal(new int[] {
            5,
            0,
            0,
            65536});
            this.numericUpDownSize.Location = new System.Drawing.Point(7, 15);
            this.numericUpDownSize.Name = "numericUpDownSize";
            this.numericUpDownSize.Size = new System.Drawing.Size(48, 20);
            this.numericUpDownSize.TabIndex = 5;
            this.numericUpDownSize.Value = new decimal(new int[] {
            55,
            0,
            0,
            65536});
            this.numericUpDownSize.ValueChanged += new System.EventHandler(this.numericUpDown1_ValueChanged);
            // 
            // numericUpDownDPI
            // 
            this.numericUpDownDPI.Increment = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.numericUpDownDPI.Location = new System.Drawing.Point(306, 313);
            this.numericUpDownDPI.Maximum = new decimal(new int[] {
            2400,
            0,
            0,
            0});
            this.numericUpDownDPI.Minimum = new decimal(new int[] {
            50,
            0,
            0,
            0});
            this.numericUpDownDPI.Name = "numericUpDownDPI";
            this.numericUpDownDPI.Size = new System.Drawing.Size(48, 20);
            this.numericUpDownDPI.TabIndex = 6;
            this.numericUpDownDPI.Value = new decimal(new int[] {
            300,
            0,
            0,
            0});
            this.numericUpDownDPI.ValueChanged += new System.EventHandler(this.numericUpDownDPI_ValueChanged);
            // 
            // buttonPrint
            // 
            this.buttonPrint.Location = new System.Drawing.Point(381, 305);
            this.buttonPrint.Name = "buttonPrint";
            this.buttonPrint.Size = new System.Drawing.Size(75, 23);
            this.buttonPrint.TabIndex = 7;
            this.buttonPrint.Text = "&Print";
            this.buttonPrint.UseVisualStyleBackColor = true;
            this.buttonPrint.Click += new System.EventHandler(this.buttonPrint_Click);
            // 
            // printDialog1
            // 
            this.printDialog1.UseEXDialog = true;
            // 
            // printDocument1
            // 
            this.printDocument1.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(this.printDocument1_PrintPage);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(294, 279);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(46, 13);
            this.label1.TabIndex = 8;
            this.label1.Text = "Size mm";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(267, 279);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(21, 13);
            this.label2.TabIndex = 9;
            this.label2.Text = "dpi";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.radioButton_in);
            this.groupBox1.Controls.Add(this.radioButton_mm);
            this.groupBox1.Controls.Add(this.numericUpDownSize);
            this.groupBox1.Location = new System.Drawing.Point(98, 288);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(119, 45);
            this.groupBox1.TabIndex = 10;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Size";
            // 
            // radioButton_in
            // 
            this.radioButton_in.AutoSize = true;
            this.radioButton_in.Location = new System.Drawing.Point(61, 25);
            this.radioButton_in.Name = "radioButton_in";
            this.radioButton_in.Size = new System.Drawing.Size(33, 17);
            this.radioButton_in.TabIndex = 7;
            this.radioButton_in.Text = "in";
            this.radioButton_in.UseVisualStyleBackColor = true;
            this.radioButton_in.CheckedChanged += new System.EventHandler(this.radioButton_size_CheckedChanged);
            // 
            // radioButton_mm
            // 
            this.radioButton_mm.AutoSize = true;
            this.radioButton_mm.Checked = true;
            this.radioButton_mm.Location = new System.Drawing.Point(61, 4);
            this.radioButton_mm.Name = "radioButton_mm";
            this.radioButton_mm.Size = new System.Drawing.Size(41, 17);
            this.radioButton_mm.TabIndex = 6;
            this.radioButton_mm.TabStop = true;
            this.radioButton_mm.Text = "mm";
            this.radioButton_mm.UseVisualStyleBackColor = true;
            this.radioButton_mm.CheckedChanged += new System.EventHandler(this.radioButton_size_CheckedChanged);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(583, 345);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.buttonPrint);
            this.Controls.Add(this.numericUpDownDPI);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.buttonEncode);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownSize)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownDPI)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button buttonEncode;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.NumericUpDown numericUpDownSize;
        private System.Windows.Forms.NumericUpDown numericUpDownDPI;
        private System.Windows.Forms.Button buttonPrint;
        private System.Windows.Forms.PrintDialog printDialog1;
        private System.Drawing.Printing.PrintDocument printDocument1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton radioButton_in;
        private System.Windows.Forms.RadioButton radioButton_mm;
    }
}

