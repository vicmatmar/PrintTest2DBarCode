﻿using System;
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
            //side = 100; //=> 1"
            if (e.PageSettings.PrintableArea.Height < e.PageSettings.PrintableArea.Width)
                side = e.PageSettings.PrintableArea.Height;
            float modesize = side / 100 * e.Graphics.DpiX;

            QrCode qrCode = qrEncoder.Encode("0123456789");


            //ISizeCalculation iSizeCal = new FixedModuleSize(10, QuietZoneModules.Zero);
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
                //bitmap.SetResolution(10, 10);
                
                Graphics graphics = Graphics.FromImage(bitmap);
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

        private void buttonEncode_Click(object sender, EventArgs e)
        {
            QrEncoder qrEncoder = new QrEncoder(ErrorCorrectionLevel.H);
            QrCode qrCode = qrEncoder.Encode("0123456789");

            ISizeCalculation iSizeCal = new FixedModuleSize(10, QuietZoneModules.Zero);
            DrawingBrushRenderer dRenderer = new DrawingBrushRenderer(iSizeCal, System.Windows.Media.Brushes.Black, System.Windows.Media.Brushes.White);

            //MemoryStream mem_stream = new MemoryStream();
            //dRenderer.WriteToStream(qrCode.Matrix, ImageFormatEnum.BMP, mem_stream);
            //Bitmap bitmap = new Bitmap(mem_stream);

            BitmapSource bitmapsource = dRenderer.WriteToBitmapSource(qrCode.Matrix, new System.Windows.Point(300, 300));
            System.Drawing.Bitmap bitmap2;
            MemoryStream outStream = new MemoryStream();
            BitmapEncoder bitmapencoder = new BmpBitmapEncoder();
            BitmapFrame bitmapframe = BitmapFrame.Create(bitmapsource);
            bitmapencoder.Frames.Add(bitmapframe);
            bitmapencoder.Save(outStream);
            bitmap2 = new System.Drawing.Bitmap(outStream);
            this.pictureBox1.Image = bitmap2;

        }
    }
}
