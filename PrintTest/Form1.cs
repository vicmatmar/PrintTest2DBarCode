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

using PDF417;

namespace PrintTest
{
    public partial class Form1 : Form
    {
        string _printer_name = "Send To OneNote 2013";
        //string _printer_name = "Microsoft XPS Document Writer";
        //string _printer_name = "\\\\APOLLO\\Finance Phil mc5450";
        //string _printer_name = "KONICA MINOLTA mc5450 PS";
        //string _printer_name = "Brady IP300 Printer";

        Bitmap _bitmap_for_print;
        Dictionary<char, Gma.QrCodeNet.Encoding.ErrorCorrectionLevel> _dic_error_correction = new Dictionary<char, ErrorCorrectionLevel>();
        Dictionary<char, QuietZoneModules> _dic_quite_zone = new Dictionary<char, QuietZoneModules>();

        public Form1()
        {
            InitializeComponent();

            var sm = SerialNumber.Machine_ID;

            string serial_number = SerialNumber.BuildSerial(5);

            _dic_error_correction.Add('L', ErrorCorrectionLevel.L); // 7%
            _dic_error_correction.Add('Q', ErrorCorrectionLevel.Q); // 25%
            _dic_error_correction.Add('M', ErrorCorrectionLevel.M); // 15%
            _dic_error_correction.Add('H', ErrorCorrectionLevel.H); // 30%

            _dic_quite_zone.Add('0', QuietZoneModules.Zero);
            _dic_quite_zone.Add('2', QuietZoneModules.Two);
            _dic_quite_zone.Add('4', QuietZoneModules.Four);

        }

        void test1(PrintPageEventArgs e)
        {
            int x = Convert.ToInt32(e.PageSettings.PrintableArea.X);
            int y = Convert.ToInt32(e.PageSettings.PrintableArea.Y);

            int h = Convert.ToInt32(e.PageSettings.PrintableArea.Height);
            int w = Convert.ToInt32(e.PageSettings.PrintableArea.Width);
            Point dpi = new Point(e.PageSettings.PrinterResolution.X, e.PageSettings.PrinterResolution.Y);


            //e.Graphics.DrawString("This is a test", new Font(FontFamily.GenericSansSerif, 12), Brushes.Blue, new PointF(0, 0));

            QrEncoder qrEncoder = new QrEncoder(Gma.QrCodeNet.Encoding.ErrorCorrectionLevel.L);


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

        private void how_to_print_doc(object sender, EventArgs e)
        {
            PrintDocument pd = new PrintDocument();
            pd.PrinterSettings.PrinterName = _printer_name;


            pd.PrintPage += how_to_print_page_handle;
            if (pd.PrinterSettings.IsValid)
            {
                pd.Print();
            }
            else
            {
                MessageBox.Show("Printer is invalid.");
            }
        }

        void how_to_print_page_handle(object sender, PrintPageEventArgs e)
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
            ISizeCalculation iSizeCal = new FixedCodeSize((int)modesize - 16, QuietZoneModules.Zero);

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
            // The other guy's encoder
            QRCodeEncoder qrCodeEncoder = new QRCodeEncoder();
            qrCodeEncoder.QRCodeEncodeMode = QRCodeEncoder.ENCODE_MODE.NUMERIC;
            qrCodeEncoder.QRCodeScale = 1;
            qrCodeEncoder.QRCodeVersion = 1;
            qrCodeEncoder.QRCodeErrorCorrect = QRCodeEncoder.ERROR_CORRECTION.M;
            Bitmap image = qrCodeEncoder.Encode(textBoxData.Text);
            this.pictureBox1.Image = image;
        }

        private void buttonEncode_Click(object sender, EventArgs e)
        {
            encodeToPictureBox();
        }

        void encodeToPictureBox()
        {
            String data = textBoxData.Text;
            ErrorCorrectionLevel correction_level = _dic_error_correction[comboBoxCorrectionLevel.Text[0]];
            QuietZoneModules quite_zone = _dic_quite_zone[comboBoxQuiteZone.Text[0]];


            QrEncoder qrEncoder = new QrEncoder(correction_level);
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
                iSizeCal = new FixedCodeSize(side, quite_zone);
            }
            else
            {
                ratio = pdpi / dpi;
                double dim = (double)numericUpDownSize.Value;
                if (comboBoxSizeUnit.Text == "mm")
                {
                    dim = Length.FromMillimeters(dim).Inches;
                }

                int pixels = (int)(dim * dpi);  // number of pixels
                iSizeCal = new FixedCodeSize(pixels, quite_zone);
            }
            DrawingBrushRenderer dRenderer = new DrawingBrushRenderer(iSizeCal,
                System.Windows.Media.Brushes.Black, System.Windows.Media.Brushes.White);
            MemoryStream mem_stream = new MemoryStream();
            dRenderer.WriteToStream(qrCode.Matrix, ImageFormatEnum.BMP, mem_stream);
            _bitmap_for_print = new Bitmap(mem_stream);
            _bitmap_for_print.SetResolution(dpi, dpi);

            //pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            Bitmap pbitmap = new Bitmap(_bitmap_for_print, (int)(_bitmap_for_print.Width * ratio), (int)(_bitmap_for_print.Height * ratio));
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


            //PDF417Generator p = new PDF417Generator();
            //Bitmap bitmap2 = p.GeneratePDF417Code(pictureBox2.Height / 4, pictureBox2.Width / 2, textBoxData.Text);
            //bitmap2.SetResolution(300,300);
            //this.pictureBox2.Image = bitmap2;
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
            //e.Graphics.DrawImage(pictureBox1.Image, 0, 0);
            if (_bitmap_for_print == null)
                return;

            e.Graphics.DrawImage(_bitmap_for_print, 0, 0);
        }

        private void numericUpDownDPI_ValueChanged(object sender, EventArgs e)
        {
            encodeToPictureBox();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            encodeToPictureBox();
        }


        private void comboBoxQuiteZone_SelectedIndexChanged(object sender, EventArgs e)
        {
            encodeToPictureBox();
        }


        private void comboBoxSizeUnit_SelectedIndexChanged(object sender, EventArgs e)
        {
            double val = (double)numericUpDownSize.Value;
            switch (comboBoxSizeUnit.Text)
            {
                case "mm":
                    numericUpDownSize.Maximum = 100;
                    numericUpDownSize.Increment = 1;
                    numericUpDownSize.ValueChanged -= numericUpDownSize_ValueChanged;
                    numericUpDownSize.Value = (decimal)UnitsNet.Length.FromInches(val).Millimeters;
                    encodeToPictureBox();
                    numericUpDownSize.ValueChanged += numericUpDownSize_ValueChanged;

                    break;
                case "in":
                    numericUpDownSize.ValueChanged -= numericUpDownSize_ValueChanged;
                    numericUpDownSize.Value = (decimal)UnitsNet.Length.FromMillimeters(val).Inches; ;
                    numericUpDownSize.Maximum = 4;
                    numericUpDownSize.Increment = 0.5M;
                    encodeToPictureBox();
                    numericUpDownSize.ValueChanged += numericUpDownSize_ValueChanged;

                    break;
                default:
                    break;
            }
        }

        private void numericUpDownSize_ValueChanged(object sender, EventArgs e)
        {
            encodeToPictureBox();
        }

        private void comboBoxCorrectionLevel_SelectedIndexChanged(object sender, EventArgs e)
        {
            encodeToPictureBox();
        }


    }
}
