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

using PowerCalibration;

namespace PrintTest
{
    public partial class Form1 : Form
    {
        string _printer_name = "Send To OneNote 2013";
        //string _printer_name = "Microsoft XPS Document Writer";
        //string _printer_name = "\\\\APOLLO\\Finance Phil mc5450";
        //string _printer_name = "KONICA MINOLTA mc5450 PS";
        //string _printer_name = "Brady IP300 Printer";

        Bitmap[] _bitmaps_for_print;

        Dictionary<char, Gma.QrCodeNet.Encoding.ErrorCorrectionLevel> _dic_error_correction = new Dictionary<char, ErrorCorrectionLevel>();
        Dictionary<char, QuietZoneModules> _dic_quite_zone = new Dictionary<char, QuietZoneModules>();

        // private class used to hold product selection combobox
        class product_desc
        {
            public int Id = 0;
            public string ModelString = null;
            public string Name = null;
        }

        public Form1()
        {
            InitializeComponent();

            string serial_number = SerialNumber.BuildSerial(5, 1);

            _dic_error_correction.Add('L', ErrorCorrectionLevel.L); // 7%
            _dic_error_correction.Add('M', ErrorCorrectionLevel.M); // 15%
            _dic_error_correction.Add('Q', ErrorCorrectionLevel.Q); // 25%
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
            float dim_inches = (float)numericUpDownSize.Value;
            if (dim_inches == 0)
            {
                // Full screen
                int side = pictureBox1.Height;
                //side = 100; //=> 1"
                if (pictureBox1.Height > pictureBox1.Width)
                    side = pictureBox1.Width;
                dim_inches = side / dpi;
            }
            else
            {
                ratio = pdpi / dpi;
                if (comboBoxSizeUnit.Text == "mm")
                {
                    dim_inches = (float)Length.FromMillimeters(dim_inches).Inches;
                }
            }

            //_bitmap_for_print = encodeToBitMap(data, dim, dpi, dpi, correction_level, quite_zone);

            product_desc product_desc = (product_desc)this.comboBoxProducts.SelectedItem;
            int start_serial = 0;
            int number_of_labels = 6;
            _bitmaps_for_print = encodeProductToBitMapArray(
                product_desc.Id, start_serial, dim_inches, number_of_labels, dpi, dpi, correction_level, quite_zone);

            Bitmap _bitmap_for_print = _bitmaps_for_print[0];

            /*
            DrawingBrushRenderer dRenderer = new DrawingBrushRenderer(iSizeCal,
                System.Windows.Media.Brushes.Black, System.Windows.Media.Brushes.White);
            MemoryStream mem_stream = new MemoryStream();
            dRenderer.WriteToStream(qrCode.Matrix, ImageFormatEnum.BMP, mem_stream);
            _bitmap_for_print = new Bitmap(mem_stream);
            _bitmap_for_print.SetResolution(dpi, dpi);
             * */

            float zoom_factor = (float)numericUpDownZoomFactor.Value;
            Bitmap pbitmap = new Bitmap(_bitmap_for_print,
                (int)(_bitmap_for_print.Width * ratio * zoom_factor), (int)(_bitmap_for_print.Height * ratio * zoom_factor));
            //this.pictureBox1.SizeMode = PictureBoxSizeMode.CenterImage;
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

            pictureBox2.Refresh();
            float space_between_labels = 0.0F;
            Graphics picture2_graphics = pictureBox2.CreateGraphics();
            picture2_graphics.PageUnit = GraphicsUnit.Pixel;
            float ratio_w = picture2_graphics.DpiX / dpi;
            float ratio_h = picture2_graphics.DpiY / dpi;
            float x_offset = 0.0F;
            for (int l = 0; l < number_of_labels; l++)
            {

                Bitmap bitmap = _bitmaps_for_print[l];
                Bitmap p2bitmap = new Bitmap(bitmap, (int)(bitmap.Width * ratio_w), (int)(bitmap.Height * ratio_h));
                picture2_graphics.DrawImage(p2bitmap, x_offset, 0.0F);

                x_offset += (dim_inches + space_between_labels) * picture2_graphics.DpiX; // Use when GraphicsUnit = Pixel
            }

        }

        Bitmap encodeToBitMap(string data,
            double dimension_inches,
            float dpi_x = 600,
            float dpi_y = 600,
            ErrorCorrectionLevel correction_level = ErrorCorrectionLevel.L,
            QuietZoneModules quite_zone = QuietZoneModules.Zero
            )
        {

            QrEncoder qrEncoder = new QrEncoder(correction_level);
            QrCode qrCode = qrEncoder.Encode(data);

            // Calculate number of pixels.  Note we use dpi in x direction
            // but we should probably use whichever is lowest
            int pixels = (int)(dimension_inches * dpi_x);

            // Check whether we have enough space
            //if (pixels < qrCode.Matrix.Width)
            //throw new Exception("Too small");

            ISizeCalculation iSizeCal = new FixedCodeSize(pixels, quite_zone);

            DrawingBrushRenderer dRenderer = new DrawingBrushRenderer(iSizeCal, System.Windows.Media.Brushes.Black, System.Windows.Media.Brushes.White);
            //DrawingBrushRenderer dRenderer = new DrawingBrushRenderer(iSizeCal, System.Windows.Media.Brushes.Black, System.Windows.Media.Brushes.LightGray);


            MemoryStream mem_stream = new MemoryStream();
            dRenderer.WriteToStream(qrCode.Matrix, ImageFormatEnum.BMP, mem_stream);
            Bitmap bitmap = new Bitmap(mem_stream);
            bitmap.SetResolution(dpi_x, dpi_y);
            // A different way to do the same.  Just incase the bitmap.SetResolution function does not work
            //System.Windows.Point dpipoint = new System.Windows.Point(dpi, dpi);
            //BitmapSource bitmapsource = dRenderer.WriteToBitmapSource(qrCode.Matrix, dpipoint);
            //MemoryStream outStream = new MemoryStream();
            //BitmapEncoder bitmapencoder = new BmpBitmapEncoder();
            //BitmapFrame bitmapframe = BitmapFrame.Create(bitmapsource);
            //bitmapencoder.Frames.Add(bitmapframe);
            //bitmapencoder.Save(outStream);
            //Bitmap bitmap = new System.Drawing.Bitmap(outStream);

            return bitmap;
        }

        /// <summary>
        /// Encodes a product to an array of bitmaps
        /// 
        /// The data is formed by using {product_id}{week_year}{start_serial_number++}
        /// </summary>
        /// <param name="product_id"></param>
        /// <param name="start_serial_number"></param>
        /// <param name="label_width"></param>
        /// <param name="number_of_labels"></param>
        /// <param name="spcae_between_labels"></param>
        /// <param name="dpi_x"></param>
        /// <param name="dpi_y"></param>
        /// <param name="correction_level"></param>
        /// <param name="quite_zone"></param>
        /// <returns></returns>
        Bitmap[] encodeProductToBitMapArray(
            int product_id = 1,
            int start_serial_number = 0,
            float label_width = 1.0F,
            int number_of_labels = 1,
            float dpi_x = 600,
            float dpi_y = 600,
            ErrorCorrectionLevel correction_level = ErrorCorrectionLevel.L,
            QuietZoneModules quite_zone = QuietZoneModules.Zero
            )
        {
            Bitmap[] bitmap_array = new Bitmap[number_of_labels];

            for (int l = 0; l < number_of_labels; l++)
            {
                string serial = SerialNumber.BuildSerial(product_id, start_serial_number++);

                Bitmap bitmap = encodeToBitMap(
                    serial, label_width, dpi_x, dpi_y, correction_level, quite_zone);

                bitmap_array[l] = bitmap;
            }

            return bitmap_array;
        }

        private void printDocument1_PrintPage(object sender, PrintPageEventArgs e)
        {
            if (_bitmaps_for_print == null)
                return;

            if (_bitmaps_for_print.Length < 1)
                return;

            e.Graphics.DrawImage(_bitmaps_for_print[0], 0, 0);

        }

        private void button1_Click(object sender, EventArgs e)
        {

            printDialog.Document = new PrintDocument();
            printDialog.Document.PrintPage += printPageSize115_PrintPage;
            DialogResult r = printDialog.ShowDialog();
            if (r == DialogResult.OK)
            {
                printDialog.Document.Print();
            }

        }

        private void printPageSize115_PrintPage(object sender, PrintPageEventArgs e)
        {
            //PageSize115 = THT-11-423,3000,12,250,250,0,125,1
            // page width = 3000"/1000  = 3"? =>page width (printable area) (in thousands of inch)
            // 12?  =>diecut count (0 = continuous tape, 1 = single diecut, # = multi-up diecut).
            // label width = 250"/1000 = 0.25 = 1/4" =>diecut width (in thousands of inch)
            // label height = 250"/1000 = 0.25 = 1/4" =>diecut height (in thousands of inch)
            // x spacing = 0 =>horizontal space between diecuts (in thousands of inch)
            // y spacing = 125 =>vertical space between diecuts (in thousands of inch)
            // Sensor type (0 = Reflective, 1 = See-Through).

            int labels_per_page = 6;
            float label_width = 0.25F;
            float spcae_between_labels = 0.0F;

            int product_id = 86;
            int start_serial_number = 0;

            ErrorCorrectionLevel correction_level = _dic_error_correction[comboBoxCorrectionLevel.Text[0]];
            QuietZoneModules quite_zone = _dic_quite_zone[comboBoxQuiteZone.Text[0]];

            /*
            float x_offset = 0.0F;
            e.Graphics.PageUnit = GraphicsUnit.Pixel;
            Bitmap[] _bitmap_array = new Bitmap[labels_per_page];
            for (int l = 0; l < labels_per_page; l++)
            {
                string serial = SerialNumber.BuildSerial(product_id, start_serial_number++);

                Bitmap bitmap = encodeToBitMap(
                    serial, label_width, e.Graphics.DpiX, e.Graphics.DpiY, correction_level, quite_zone);
                _bitmap_array[l] = bitmap;

                e.Graphics.DrawImage(bitmap, x_offset, 0.0F);

                //x_offset += (int)(label_width * 100);  // Use when GraphicsUnit = Display
                x_offset += (label_width + spcae_between_labels) * e.Graphics.DpiX; // Use when GraphicsUnit = Pixel
            }
             */

            Bitmap[] _bitmap_array = encodeProductToBitMapArray(product_id, start_serial_number, label_width, labels_per_page, e.Graphics.DpiX, e.Graphics.DpiY, correction_level, quite_zone);
            float x_offset = 0.0F;
            e.Graphics.PageUnit = GraphicsUnit.Pixel;
            for (int l = 0; l < labels_per_page; l++)
            {
                e.Graphics.DrawImage(_bitmap_array[l], x_offset, 0.0F);

                //x_offset += (int)(label_width * 100);  // Use when GraphicsUnit = Display
                x_offset += (label_width + spcae_between_labels) * e.Graphics.DpiX; // Use when GraphicsUnit = Pixel
            }


            Graphics picture_graphics = pictureBox2.CreateGraphics();
            picture_graphics.PageUnit = GraphicsUnit.Pixel;
            float ratio_w = picture_graphics.DpiX / e.Graphics.DpiX;
            float ratio_h = picture_graphics.DpiY / e.Graphics.DpiY;
            x_offset = 0.0F;
            for (int l = 0; l < labels_per_page; l++)
            {

                Bitmap bitmap = _bitmap_array[l];
                Bitmap pbitmap = new Bitmap(bitmap, (int)(bitmap.Width * ratio_w), (int)(bitmap.Height * ratio_h));
                picture_graphics.DrawImage(pbitmap, x_offset, 0.0F);

                x_offset += (label_width + spcae_between_labels) * picture_graphics.DpiX; // Use when GraphicsUnit = Pixel
            }

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

        private void Form1_Load(object sender, EventArgs e)
        {
            ManufacturingStore_DataContext dc = Utils.DC;

            product_desc[] products =
                dc.Products.Select(s =>
                    new product_desc { Id = s.Id, Name = s.Name, ModelString = s.ModelString }).OrderBy(s => s.ModelString).ToArray();
            comboBoxProducts.DataSource = products;

            // or to select everything
            //comboBoxProducts.DataSource = dc.Products.ToArray();

        }
        /// <summary>
        /// Format the product combobox selection
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void comboBoxProducts_Format(object sender, ListControlConvertEventArgs e)
        {
            //PowerCalibration.Product product = (PowerCalibration.Product)e.ListItem;
            product_desc product = (product_desc)e.ListItem;
            e.Value = string.Format("{0,-10} ({1})", product.ModelString, product.Name);
        }

        private void comboBoxProducts_SelectedIndexChanged(object sender, EventArgs e)
        {
            product_desc product = (product_desc)comboBoxProducts.SelectedItem;

            string serial = SerialNumber.BuildSerial(product.Id, 1);
            textBoxData.Text = serial;
        }

        private void numericUpDownZoomFactor_ValueChanged(object sender, EventArgs e)
        {
            float zoom_factor = (float)numericUpDownZoomFactor.Value;
            if (zoom_factor < 1)
                numericUpDownZoomFactor.Increment = 0.1M;
            else
                numericUpDownZoomFactor.Increment = 1.0M;

            float ratio = pictureBox1.Image.HorizontalResolution / (float)numericUpDownDPI.Value;
            Bitmap bitmap = _bitmaps_for_print[0];
            Bitmap pbitmap = new Bitmap(bitmap, (int)(bitmap.Width * ratio * zoom_factor), (int)(bitmap.Height * ratio * zoom_factor));
            this.pictureBox1.Image = pbitmap;

        }

        private void textBoxData_TextChanged(object sender, EventArgs e)
        {
            encodeToPictureBox();
        }

        private void printToolStripMenuItem_Click(object sender, EventArgs e)
        {
            printDialog.Document = new PrintDocument();
            printDialog.Document.PrintPage += printDocument1_PrintPage;
            DialogResult r = printDialog.ShowDialog();
            if (r == DialogResult.OK)
            {
                printDialog.Document.Print();
            }

        }


    }
}
