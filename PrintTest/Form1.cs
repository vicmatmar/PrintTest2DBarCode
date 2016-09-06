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

using Gma.QrCodeNet.Encoding;
using Gma.QrCodeNet.Encoding.Windows.Render;

using System.IO;
using System.Drawing.Imaging;

using System.Windows.Media;
//using System.Windows.Media.Imaging;


namespace PrintTest
{
    public partial class Form1 : Form
    {
        PrinterSettings _printersettings;

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            PrintDialog pd = new PrintDialog();
            pd.PrinterSettings = _printersettings;
            if (pd.ShowDialog() == DialogResult.OK)
            {
                _printersettings = pd.PrinterSettings;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Font printFont = new Font("Arial", 10);
            PrintDocument pd = new PrintDocument();

            //pd.PrinterSettings.PrinterName = "Send To OneNote 2013";
            pd.PrinterSettings.PrinterName = "Microsoft XPS Document Writer";
            //pd.PrinterSettings.PrinterName = "\\\\APOLLO\\Finance Phil mc5450";

            pd.PrintPage += pd_PrintPage;
            if (pd.PrinterSettings.IsValid)
            {
                pd.Print();
            }
            else
            {
                MessageBox.Show("Printer is invalid.");
            }
        }

        void pd_PrintPage(object sender, PrintPageEventArgs e)
        {
            int h = (int)(e.PageSettings.PrintableArea.Height);
            int w = (int)(e.PageSettings.PrintableArea.Width);

            QrEncoder qrEncoder = new QrEncoder(ErrorCorrectionLevel.L);

            float side = e.PageSettings.PrintableArea.Width;
            //side = 100; //=> 1"
            if (e.PageSettings.PrintableArea.Height < e.PageSettings.PrintableArea.Width)
                side = e.PageSettings.PrintableArea.Height;
            float modesize = side / 100 * e.Graphics.DpiX;

            QrCode qrCode = qrEncoder.Encode("0123456789");
            //ISizeCalculation iSizeCal = new FixedModuleSize(3, QuietZoneModules.Zero);
            ISizeCalculation iSizeCal = new FixedCodeSize( (int)modesize, QuietZoneModules.Zero);
            DrawingBrushRenderer dRenderer = new DrawingBrushRenderer(iSizeCal, System.Windows.Media.Brushes.Black, System.Windows.Media.Brushes.White);
            DrawingBrush dBrush = dRenderer.DrawBrush(qrCode.Matrix);
            MemoryStream mem_stream = new MemoryStream();
            dRenderer.WriteToStream(qrCode.Matrix, ImageFormatEnum.PNG, mem_stream);


            using (Bitmap bitmap = new Bitmap(mem_stream))
            {

                bitmap.SetResolution(e.Graphics.DpiX, e.Graphics.DpiX);

                using (Graphics graphics = Graphics.FromImage(bitmap))
                {

                    e.Graphics.DrawImage(bitmap, e.Graphics.RenderingOrigin.X, e.Graphics.RenderingOrigin.Y);
                    //bitmap.Save(mem_stream, ImageFormat.Bmp);
                }

            }

        }

        void test1(PrintPageEventArgs e)
        {
            int x =  Convert.ToInt32(e.PageSettings.PrintableArea.X);
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
    }
}
