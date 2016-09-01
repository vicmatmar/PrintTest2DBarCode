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

//using System.Windows;
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

            pd.PrinterSettings.PrinterName = "Send To OneNote 2013";
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
            float leftMargin = e.MarginBounds.Left;
            float topMargin = e.MarginBounds.Top;

            //e.Graphics.DrawString("This is a test", new Font(FontFamily.GenericSansSerif, 12), Brushes.Blue, new PointF(0, 0));

            QrEncoder qrEncoder = new QrEncoder(ErrorCorrectionLevel.H);
            QrCode qrCode = qrEncoder.Encode("Victor says hi");

            ISizeCalculation iSizeCal = new FixedModuleSize(1, QuietZoneModules.Two);
            GraphicsRenderer gRenderer = new GraphicsRenderer(iSizeCal, Brushes.Black, Brushes.White);
            
            //WriteableBitmapRenderer gRenderer = new WriteableBitmapRenderer(iSizeCal);
            //WriteableBitmap bitmap = new WriteableBitmap(300, 300, 300, 300, System.Windows.Media.PixelFormats.BlackWhite, BitmapPalettes.BlackAndWhite);


            gRenderer.Draw(e.Graphics, qrCode.Matrix);

            using (FileStream stream = new FileStream("test.png", FileMode.Create))
            {
                gRenderer.WriteToStream(qrCode.Matrix, ImageFormat.Png, stream);
            }

        }
    }
}
