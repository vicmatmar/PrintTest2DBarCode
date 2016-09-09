using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Drawing.Printing;


using System.IO;
using System.Drawing.Imaging;

using System.Windows.Media;
using System.Windows.Media.Imaging;

using Gma.QrCodeNet.Encoding;
using Gma.QrCodeNet.Encoding.Windows.Render;

using ThoughtWorks.QRCode.Codec;

using UnitsNet;

namespace PrintTest
{
    public partial class Form1 : Form
    {
        string _printer_name = "Send To OneNote 2013";
        //string _printer_name = "Microsoft XPS Document Writer";
        //string _printer_name = "\\\\APOLLO\\Finance Phil mc5450";
        //string _printer_name = "KONICA MINOLTA mc5450 PS";
        //string _printer_name = "Brady IP300 Printer";

        public Form1()
        {
            InitializeComponent();
        }

        void test1(PrintPageEventArgs e)
        {
            int x = Convert.ToInt32(e.PageSettings.PrintableArea.X);
            int y = Convert.ToInt32(e.PageSettings.PrintableArea.Y);

            int h = Convert.ToInt32(e.PageSettings.PrintableArea.Height);
            int w = Convert.ToInt32(e.PageSettings.PrintableArea.Width);
            Point dpi = new Point(e.PageSettings.PrinterResolution.X, e.PageSettings.PrinterResolution.Y);


            //e.Graphics.DrawString("This is a test", new Font(FontFamily.GenericSansSerif, 12), Brushes.Blue, new PointF(0, 0));

            QrEncoder qrEncoder = new QrEncoder(ErrorCorrectionLevel.L);


            ISizeCalculation iSizeCal = new FixedModuleSize(1, QuietZoneModules.Two);
            //ISizeCalculation iSizeCal = new FixedCodeSize(25, QuietZoneModules.Two);


            GraphicsRenderer gRenderer = new GraphicsRenderer(iSizeCal,
                System.Drawing.Brushes.Black, System.Drawing.Brushes.White);


            QrCode qrCode1 = qrEncoder.Encode("Victor says hi");
            QrCode qrCode2 = qrEncoder.Encode("Victor says goodbye");
            QrCode qrCode3 = qrEncoder.Encode("0123456789");
            QrCode qrCode4 = qrEncoder.Encode("ABCDEF1234");


            DrawingBrushRenderer dRenderer = new DrawingBrushRenderer(iSizeCal,
                System.Windows.Media.Brushes.Black, System.Windows.Media.Brushes.White);
            DrawingBrush dBrush = dRenderer.DrawBrush(qrCode1.Matrix);

            int side = w;
            if (w > h)
                side = h;



            gRenderer.Draw(e.Graphics, qrCode1.Matrix);

            var size = iSizeCal.GetSize(qrCode1.Matrix.Width);
            Point offset = new Point(size.CodeWidth + 10, 0);
            gRenderer.Draw(e.Graphics, qrCode2.Matrix, offset);

            size = iSizeCal.GetSize(qrCode2.Matrix.Width);
            offset.X += size.CodeWidth + 10;
            gRenderer.Draw(e.Graphics, qrCode3.Matrix, offset);

            size = iSizeCal.GetSize(qrCode3.Matrix.Width);
            offset.X += size.CodeWidth + 10;
            gRenderer.Draw(e.Graphics, qrCode4.Matrix, offset);

            using (FileStream stream = new FileStream("test.png", FileMode.Create))
            {
                gRenderer.WriteToStream(qrCode1.Matrix, ImageFormat.Png, stream);
                gRenderer.WriteToStream(qrCode2.Matrix, ImageFormat.Png, stream);
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            test2();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            PrintDocument pd = new PrintDocument();
            pd.PrinterSettings.PrinterName = _printer_name;


            pd.PrintPage += pd_PrintPage1;
            if (pd.PrinterSettings.IsValid)
            {
                pd.Print();
            }
            else
            {
                MessageBox.Show("Printer is invalid.");
            }
        }

        void pd_PrintPage1(object sender, PrintPageEventArgs e)
        {
            int h = (int)(e.PageSettings.PrintableArea.Height);
            int w = (int)(e.PageSettings.PrintableArea.Width);

            QrEncoder qrEncoder = new QrEncoder(ErrorCorrectionLevel.M);

            float side = e.PageSettings.PrintableArea.Width;
            side = 100; //=> 1"
            if (e.PageSettings.PrintableArea.Height < e.PageSettings.PrintableArea.Width)
                side = e.PageSettings.PrintableArea.Height;
            float modesize = side / 100 * e.Graphics.DpiX;

            QrCode qrCode = qrEncoder.Encode("0123456789");


            //ISizeCalculation iSizeCal = new FixedModuleSize(2, QuietZoneModules.Zero);
            // This works with the Brady on a very small label
            ISizeCalculation iSizeCal = new FixedCodeSize( (int)modesize-16, QuietZoneModules.Zero);

            DrawingBrushRenderer dRenderer = new DrawingBrushRenderer(iSizeCal, 
                System.Windows.Media.Brushes.Black, System.Windows.Media.Brushes.White);

            string test = "test1";
            if (test == "test1")
            {
                MemoryStream mem_stream = new MemoryStream();
                dRenderer.WriteToStream(qrCode.Matrix, ImageFormatEnum.BMP, mem_stream);

                Bitmap bitmap = new Bitmap(mem_stream);
                bitmap.SetResolution(e.Graphics.DpiX, e.Graphics.DpiY);
                //Graphics graphics = Graphics.FromImage(bitmap);
                e.Graphics.DrawImage(bitmap, 0, 0);
            }
            else
            {
                System.Windows.Point dpipoint = new System.Windows.Point(e.Graphics.DpiX, e.Graphics.DpiY);
                BitmapSource bitmapsource = dRenderer.WriteToBitmapSource(qrCode.Matrix, dpipoint);
                BitmapFrame bitmapframe = BitmapFrame.Create(bitmapsource);

                BitmapEncoder bitmapencoder = new BmpBitmapEncoder();
                bitmapencoder.Frames.Add(bitmapframe);

                MemoryStream mem_stream = new MemoryStream();
                bitmapencoder.Save(mem_stream);

                System.Drawing.Bitmap bitmap = new System.Drawing.Bitmap(mem_stream);
                e.Graphics.DrawImage(bitmap, 0, 0);
            }

        }

        void test2()
        {
            QRCodeEncoder encoder = new QRCodeEncoder();
            encoder.QRCodeEncodeMode = QRCodeEncoder.ENCODE_MODE.NUMERIC;
            encoder.QRCodeErrorCorrect = QRCodeEncoder.ERROR_CORRECTION.M;
            encoder.QRCodeBackgroundColor = System.Drawing.Color.Black;
            encoder.QRCodeForegroundColor = System.Drawing.Color.White;
            encoder.QRCodeScale = 1;

            Bitmap bitmap = encoder.Encode("1234567890");
        }

        private void buttonEncode_Click(object sender, EventArgs e)
        {
            encodeToPictureBox();
        }

        void encodeToPictureBox()
        {
            String data = "012345678901234567890";

            QrEncoder qrEncoder = new QrEncoder(ErrorCorrectionLevel.M);
            QrCode qrCode = qrEncoder.Encode(data);

            float dpi = (float)numericUpDownDPI.Value;
            float pdpi = pictureBox1.CreateGraphics().DpiX;
            float ratio = 1;
            ISizeCalculation iSizeCal;
            if (numericUpDownSize.Value == 0)
            {
                // Full screen
                int side = pictureBox1.Height;
                //side = 100; //=> 1"
                if (pictureBox1.Height > pictureBox1.Width)
                    side = pictureBox1.Width;
                iSizeCal = new FixedCodeSize(side, QuietZoneModules.Zero);
            }
            else
            {
                ratio = pdpi / dpi;
                double dim = (double)numericUpDownSize.Value;
                if (radioButton_mm.Checked)
                {
                    dim = Length.FromMillimeters(dim).Inches;
                }
                
                int pixels = (int)(dim * dpi);  // number of pixels
                iSizeCal = new FixedCodeSize(pixels, QuietZoneModules.Zero);

            }
            DrawingBrushRenderer dRenderer = new DrawingBrushRenderer(iSizeCal, 
                System.Windows.Media.Brushes.Black, System.Windows.Media.Brushes.White);
            MemoryStream mem_stream = new MemoryStream();
            dRenderer.WriteToStream(qrCode.Matrix, ImageFormatEnum.BMP, mem_stream);
            Bitmap bitmap = new Bitmap(mem_stream);
            bitmap.SetResolution(dpi,dpi);
            
            //pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            Bitmap pbitmap = new Bitmap(bitmap, (int)(bitmap.Width * ratio), (int)(bitmap.Height * ratio));
            this.pictureBox1.Image = pbitmap;

            
            // A different way to do the same.  Just incase the bitmap.SetResolution function does not work
            //System.Windows.Point dpipoint = new System.Windows.Point(300, 300);
            //BitmapSource bitmapsource = dRenderer.WriteToBitmapSource(qrCode.Matrix, dpipoint);
            //MemoryStream outStream = new MemoryStream();
            //BitmapEncoder bitmapencoder = new BmpBitmapEncoder();
            //BitmapFrame bitmapframe = BitmapFrame.Create(bitmapsource);
            //bitmapencoder.Frames.Add(bitmapframe);
            //bitmapencoder.Save(outStream);
            //Bitmap bitmap2 = new System.Drawing.Bitmap(outStream);


            // The other guy encoder
            QRCodeEncoder qrCodeEncoder = new QRCodeEncoder();
            qrCodeEncoder.QRCodeEncodeMode = QRCodeEncoder.ENCODE_MODE.NUMERIC;
            qrCodeEncoder.QRCodeScale = 5;
            qrCodeEncoder.QRCodeVersion = 1;
            qrCodeEncoder.QRCodeErrorCorrect = QRCodeEncoder.ERROR_CORRECTION.M;
            Image image = qrCodeEncoder.Encode(data);


            this.pictureBox2.Image = image;

        }

        private void buttonPrint_Click(object sender, EventArgs e)
        {

            printDialog1.Document = printDocument1;
            DialogResult r = printDialog1.ShowDialog();
            if (r == DialogResult.OK)
            {
                printDocument1.Print();
            }            

        }

        private void printDocument1_PrintPage(object sender, PrintPageEventArgs e)
        {
            e.Graphics.DrawImage(pictureBox1.Image, 0, 0);  
        }

        private void numericUpDownDPI_ValueChanged(object sender, EventArgs e)
        {
            encodeToPictureBox();
        }
        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            encodeToPictureBox();
        }

        private void radioButton_size_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton_mm.Checked)
            {
                numericUpDownSize.Maximum = 100;
                numericUpDownSize.Increment = 1;
            }
            else
            {
                numericUpDownSize.Maximum = 4;
                numericUpDownSize.Increment = new Decimal(0.5);
            }

        }



    }
}
