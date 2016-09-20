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
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.buttonEncode = new System.Windows.Forms.Button();
            this.numericUpDownSize = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownDPI = new System.Windows.Forms.NumericUpDown();
            this.buttonPrint = new System.Windows.Forms.Button();
            this.printDialog = new System.Windows.Forms.PrintDialog();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.comboBoxCorrectionLevel = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.comboBoxQuiteZone = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.comboBoxSizeUnit = new System.Windows.Forms.ComboBox();
            this.textBoxData = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.comboBoxProducts = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownSize)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownDPI)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox1.Location = new System.Drawing.Point(13, 13);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(250, 250);
            this.pictureBox1.TabIndex = 2;
            this.pictureBox1.TabStop = false;
            // 
            // buttonEncode
            // 
            this.buttonEncode.Location = new System.Drawing.Point(12, 319);
            this.buttonEncode.Name = "buttonEncode";
            this.buttonEncode.Size = new System.Drawing.Size(75, 23);
            this.buttonEncode.TabIndex = 1;
            this.buttonEncode.Text = "&Encode";
            this.buttonEncode.UseVisualStyleBackColor = true;
            this.buttonEncode.Click += new System.EventHandler(this.buttonEncode_Click);
            // 
            // numericUpDownSize
            // 
            this.numericUpDownSize.DecimalPlaces = 2;
            this.numericUpDownSize.Increment = new decimal(new int[] {
            5,
            0,
            0,
            65536});
            this.numericUpDownSize.Location = new System.Drawing.Point(67, 3);
            this.numericUpDownSize.Name = "numericUpDownSize";
            this.numericUpDownSize.Size = new System.Drawing.Size(48, 20);
            this.numericUpDownSize.TabIndex = 0;
            this.numericUpDownSize.Value = new decimal(new int[] {
            55,
            0,
            0,
            65536});
            this.numericUpDownSize.ValueChanged += new System.EventHandler(this.numericUpDownSize_ValueChanged);
            // 
            // numericUpDownDPI
            // 
            this.numericUpDownDPI.Increment = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.numericUpDownDPI.Location = new System.Drawing.Point(67, 30);
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
            this.numericUpDownDPI.TabIndex = 4;
            this.numericUpDownDPI.Value = new decimal(new int[] {
            300,
            0,
            0,
            0});
            this.numericUpDownDPI.ValueChanged += new System.EventHandler(this.numericUpDownDPI_ValueChanged);
            // 
            // buttonPrint
            // 
            this.buttonPrint.Location = new System.Drawing.Point(93, 319);
            this.buttonPrint.Name = "buttonPrint";
            this.buttonPrint.Size = new System.Drawing.Size(75, 23);
            this.buttonPrint.TabIndex = 2;
            this.buttonPrint.Text = "&Print";
            this.buttonPrint.UseVisualStyleBackColor = true;
            this.buttonPrint.Click += new System.EventHandler(this.buttonPrint_Click);
            // 
            // printDialog
            // 
            this.printDialog.UseEXDialog = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.tableLayoutPanel1);
            this.groupBox1.Location = new System.Drawing.Point(269, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(220, 251);
            this.groupBox1.TabIndex = 10;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Settings";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.Controls.Add(this.numericUpDownSize, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 4);
            this.tableLayoutPanel1.Controls.Add(this.numericUpDownDPI, 1, 4);
            this.tableLayoutPanel1.Controls.Add(this.label2, 0, 5);
            this.tableLayoutPanel1.Controls.Add(this.comboBoxCorrectionLevel, 1, 5);
            this.tableLayoutPanel1.Controls.Add(this.label3, 0, 6);
            this.tableLayoutPanel1.Controls.Add(this.comboBoxQuiteZone, 1, 6);
            this.tableLayoutPanel1.Controls.Add(this.label4, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.comboBoxSizeUnit, 2, 1);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(13, 19);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 7;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.Size = new System.Drawing.Size(185, 126);
            this.tableLayoutPanel1.TabIndex = 13;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 27);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(24, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "dpi:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 53);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(58, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Correction:";
            // 
            // comboBoxCorrectionLevel
            // 
            this.comboBoxCorrectionLevel.FormattingEnabled = true;
            this.comboBoxCorrectionLevel.Items.AddRange(new object[] {
            "L",
            "M",
            "Q",
            "H"});
            this.comboBoxCorrectionLevel.Location = new System.Drawing.Point(67, 56);
            this.comboBoxCorrectionLevel.Name = "comboBoxCorrectionLevel";
            this.comboBoxCorrectionLevel.Size = new System.Drawing.Size(32, 21);
            this.comboBoxCorrectionLevel.TabIndex = 9;
            this.comboBoxCorrectionLevel.Text = "L";
            this.comboBoxCorrectionLevel.SelectedIndexChanged += new System.EventHandler(this.comboBoxCorrectionLevel_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 80);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(35, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Quite:";
            // 
            // comboBoxQuiteZone
            // 
            this.comboBoxQuiteZone.AutoCompleteCustomSource.AddRange(new string[] {
            "0",
            "2",
            "4"});
            this.comboBoxQuiteZone.FormattingEnabled = true;
            this.comboBoxQuiteZone.Items.AddRange(new object[] {
            "0",
            "2",
            "4"});
            this.comboBoxQuiteZone.Location = new System.Drawing.Point(67, 83);
            this.comboBoxQuiteZone.Name = "comboBoxQuiteZone";
            this.comboBoxQuiteZone.Size = new System.Drawing.Size(32, 21);
            this.comboBoxQuiteZone.TabIndex = 12;
            this.comboBoxQuiteZone.Text = "0";
            this.comboBoxQuiteZone.SelectedIndexChanged += new System.EventHandler(this.comboBoxQuiteZone_SelectedIndexChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(3, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(30, 13);
            this.label4.TabIndex = 13;
            this.label4.Text = "Size:";
            // 
            // comboBoxSizeUnit
            // 
            this.comboBoxSizeUnit.FormattingEnabled = true;
            this.comboBoxSizeUnit.Items.AddRange(new object[] {
            "mm",
            "in"});
            this.comboBoxSizeUnit.Location = new System.Drawing.Point(121, 3);
            this.comboBoxSizeUnit.Name = "comboBoxSizeUnit";
            this.comboBoxSizeUnit.Size = new System.Drawing.Size(44, 21);
            this.comboBoxSizeUnit.TabIndex = 14;
            this.comboBoxSizeUnit.Text = "mm";
            this.comboBoxSizeUnit.SelectedIndexChanged += new System.EventHandler(this.comboBoxSizeUnit_SelectedIndexChanged);
            // 
            // textBoxData
            // 
            this.textBoxData.Location = new System.Drawing.Point(13, 287);
            this.textBoxData.Name = "textBoxData";
            this.textBoxData.Size = new System.Drawing.Size(165, 20);
            this.textBoxData.TabIndex = 0;
            this.textBoxData.Text = "12345678901234567890";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(199, 318);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 11;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // comboBoxProducts
            // 
            this.comboBoxProducts.FormattingEnabled = true;
            this.comboBoxProducts.Location = new System.Drawing.Point(199, 286);
            this.comboBoxProducts.Name = "comboBoxProducts";
            this.comboBoxProducts.Size = new System.Drawing.Size(290, 21);
            this.comboBoxProducts.TabIndex = 14;
            this.comboBoxProducts.SelectedIndexChanged += new System.EventHandler(this.comboBoxProducts_SelectedIndexChanged);
            this.comboBoxProducts.Format += new System.Windows.Forms.ListControlConvertEventHandler(this.comboBoxProducts_Format);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(509, 378);
            this.Controls.Add(this.comboBoxProducts);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.textBoxData);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.buttonPrint);
            this.Controls.Add(this.buttonEncode);
            this.Controls.Add(this.pictureBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownSize)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownDPI)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button buttonEncode;
        private System.Windows.Forms.NumericUpDown numericUpDownSize;
        private System.Windows.Forms.NumericUpDown numericUpDownDPI;
        private System.Windows.Forms.Button buttonPrint;
        private System.Windows.Forms.PrintDialog printDialog;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxData;
        private System.Windows.Forms.ComboBox comboBoxCorrectionLevel;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox comboBoxQuiteZone;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox comboBoxSizeUnit;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ComboBox comboBoxProducts;
    }
}

