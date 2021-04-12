using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Imaging; // for ImageLockMode
using System.Runtime.InteropServices; // for Marshal
using ZedGraph;
using GeoradarGui.Properties;
using System.Drawing.Drawing2D;

namespace GeoradarGui
{
    public partial class SpectrumForm : BaseDockContent
    {

        public delegate void AddDataToGraphFormControl(PointPairList _pointPairList, double parameter);
        public event AddDataToGraphFormControl addDataToGraphFormControl;

        private static List<List<double>> spec_data; // a list of column pixel values
        private readonly Random _random = new Random();
        private ControlForm controlForm;


        public string VisualisationMode;
        public int GeneratedDataHeightDepth;
        public int GeneratedDataWidth;
        public int InputDataScaleFactor;

        public int ScanStepValue;
        public int DeptPointsValue;

        public int  DielectricPermittivity;
        public bool DistanceTimeState;



        Settings settings = Settings.Default;

        int current_line_counter = 0;
        double period_counter = 0;



        public SpectrumForm()
        {
            InitializeComponent();
            VisualisationMode = "green";
            InputDataScaleFactor = 1;

            controlForm = new ControlForm();
            GeneratedDataWidth = 600;
            GeneratedDataHeightDepth = 768;


            Data_init();
            tmr_DataGenerating.Stop();
            //timer1.Start();
        }

        private void Data_init()
        {
            double tmp;

            // Create List of data-list:
            spec_data = new List<List<double>>();
            for (int x = 0; x < GeneratedDataWidth; x++)
            {
                List<double> column = new List<double>();
                for (int y = 0; y < GeneratedDataHeightDepth; y++)
                {
                    column.Add(0);
                }
                spec_data.Add(column);
            }

            // Add aditional column for storage average data
            List<double> average_column = new List<double>();
            for (int y = 0; y < GeneratedDataHeightDepth; y++)
            {
                average_column.Add(0);
            }
            spec_data.Add(average_column);
        }

        private void DataLineGenerate()
        {
            PointPairList _pointPairList = new PointPairList();
            //PointPair ResultPair = new PointPair(0,0);


            double tmp = 0;
            // Generate new line of data:
            period_counter++;
            for (int y = 0; y < GeneratedDataHeightDepth; y++)
            {
                double num = _random.NextDouble() - 0.5;
                tmp = (Math.Sin(Math.PI * ((double)y) / 90) + 1 + (0.5 * num)) * 2 * Math.Sin(Math.PI * (period_counter) / 180) ;
                PointPair _pointPair = new PointPair(tmp * 500,y);
                _pointPairList.Add(_pointPair);
            }

            addDataToGraphFormControl(_pointPairList, (double)ScanStepValue);
            
        }


        public void addDataLine(PointPairList _pointPairList)
        {
            // Set how many line will take one line:
            int line_multiplier = settings.AverageWindow;



            ///////////////////////////////////////////////////////////////////////////////

            MyDataFile current_data = new MyDataFile();
            for (int i = 0; i < _pointPairList.Count; i++)
            {
                current_data.data_set[i] = _pointPairList[i].X;
            }
            current_data.myDataSize = _pointPairList.Count;

// Here it save data to file:
//            current_data.SaveAdd("test.test");

            //////////////////////////////////////////////////////////////////////////////




            // Clear extra-line:
            if (current_line_counter == 0)
            {
                for (int y = 0; y < _pointPairList.Count; y++)
                {
                    spec_data[GeneratedDataWidth][y] = 0;
                }
            }

            // Add data-line to extra-line:
            if (current_line_counter < settings.AveragingLineWindow)
            {
                for (int y = 0; y < _pointPairList.Count; y++)
                {
                     spec_data[GeneratedDataWidth][y] = spec_data[GeneratedDataWidth][y] + _pointPairList[y].X;
                }
                current_line_counter++;
            }

            if (current_line_counter >= settings.AveragingLineWindow)
            {
                current_line_counter = 0;
                
                for (int y = 0; y < _pointPairList.Count; y++)
                {
                    spec_data[GeneratedDataWidth - 1][y] = (spec_data[GeneratedDataWidth][y] / (double)settings.AveragingLineWindow);
                }

                for (int x = 0; x < GeneratedDataWidth - line_multiplier; x++)
                {

                    for (int y = 0; y < GeneratedDataHeightDepth; y++)
                    {
                        spec_data[x][y] = spec_data[x + line_multiplier][y];
                    }
                }

                for (int i = 0; i < (line_multiplier - 1); i++)
                {
                    for (int y = 0; y < _pointPairList.Count; y++)
                    {
                        spec_data[GeneratedDataWidth - 2 - i][y] = spec_data[GeneratedDataWidth - 1][y];
                    }
                }

                // Update bitmap picture:                
                Update_bitmap_with_data();
            }
        }


        void Update_bitmap_with_data()
        {
            int localGeneratedDataHeightDepth = GeneratedDataHeightDepth;

            Bitmap bitmap = new Bitmap(spec_data.Count-1, localGeneratedDataHeightDepth, PixelFormat.Format8bppIndexed);

            switch (VisualisationMode)
            {
                case "gray":
                    bitmap.Palette = pallette_gray(bitmap.Palette);
                    break;
                case "blue":
                    bitmap.Palette = pallette_blue(bitmap.Palette);
                    break;
                case "green":
                    bitmap.Palette = pallette_green(bitmap.Palette);
                    break;
                case "spectrum":
                    bitmap.Palette = pallette_spectrum(bitmap.Palette);
                    break;
                default:
                    bitmap.Palette = pallette_gray(bitmap.Palette);
                    break;
            }


            // prepare to access data via the bitmapdata object
            BitmapData bitmapData = bitmap.LockBits(new Rectangle(0, 0, bitmap.Width, bitmap.Height),
                                        ImageLockMode.ReadOnly, bitmap.PixelFormat);

            // create a byte array to reflect each pixel in the image
            byte[] pixels = new byte[bitmapData.Stride * bitmap.Height];

            // fill pixel array with data
            for (int col = 0; col < spec_data.Count-1; col++)
            {
                double scaleFactor = (double)InputDataScaleFactor/100.0;

                for (int row = 0; row < localGeneratedDataHeightDepth; row++)
                {
                    int bytePosition = row * bitmapData.Stride + col;
                    double pixelVal = spec_data[col][row] * scaleFactor;
                    if(pixelVal < 0)
                    { 
                        pixelVal = Math.Abs(pixelVal); 
                    }
                    
                    pixelVal = Math.Max(0, pixelVal);
                    pixelVal = Math.Min(255, pixelVal);
                    pixels[bytePosition] = (byte)(pixelVal);
                }
            }

            // turn the byte array back into a bitmap
            Marshal.Copy(pixels, 0, bitmapData.Scan0, pixels.Length);
            bitmap.UnlockBits(bitmapData);


            double Hscale = (double)pictureBox1.Size.Height / (double)bitmap.Height;
            double Wscale = (double)pictureBox1.Size.Width / (double)bitmap.Width;

            // Add axis legends:
            Bitmap tempBitmap = new Bitmap(pictureBox2.Size.Width, pictureBox2.Size.Height);
            Graphics flagGraphics = Graphics.FromImage(tempBitmap);
            System.Drawing.Pen myPen;
            myPen = new System.Drawing.Pen(System.Drawing.Color.White);
            // disabled data-picture: flagGraphics.DrawImage(bitmap,0,0);
            //flagGraphics.SmoothingMode = SmoothingMode.AntiAlias;
            //flagGraphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
            //flagGraphics.PixelOffsetMode = PixelOffsetMode.HighQuality;

            string depth_value;
            RectangleF rectf = new RectangleF(0, 0, 90, 50); // print place
            double depth_time;

            Brush LocalBrush = Brushes.White;


            if(DistanceTimeState == true)
            {
                flagGraphics.DrawString("metrs", new Font("Tahoma", 12), LocalBrush, rectf);
            }
            else
            {
                flagGraphics.DrawString("nS", new Font("Tahoma", 12), LocalBrush, rectf);
            }

            

            for (int i = 0; i < 10; i++)
            {
                flagGraphics.DrawLine(myPen, 0, (int)(tempBitmap.Height * (i + 1) / 10), tempBitmap.Width/2, (int)(tempBitmap.Height * (i + 1) / 10));
                rectf = new RectangleF(0, (tempBitmap.Height * (i + 1) / 10), 90, 50);

                depth_time = ((double)((ScanStepValue * DeptPointsValue * (i + 1) / 10.0) / 1000.0) / 2.0);

                if (DistanceTimeState == true)
                {
                    depth_value = (0.3 * depth_time / Math.Sqrt((double) DielectricPermittivity)).ToString("0.00");
                }
                else 
                {
                    depth_value = depth_time.ToString("0.00");
                }
                
                flagGraphics.DrawString(depth_value, new Font("Tahoma", 12), LocalBrush, rectf);
            }
            flagGraphics.Flush();


            try
            {
                Bitmap resized_data = new Bitmap(bitmap, new Size((int)((double)bitmap.Width * Wscale), (int)((double)bitmap.Height * Hscale)));
                // apply the bitmap to the picturebox
                pictureBox1.Image = resized_data;
                pictureBox2.Image = tempBitmap;
            }
            catch 
            { 
            
            }
        }


        ColorPalette pallette_gray(ColorPalette pal)
        {
            for (int i = 0; i < 256; i++)
                pal.Entries[i] = Color.FromArgb(255, i, i, i);
            return pal;
        }

        ColorPalette pallette_blue(ColorPalette pal)
        {
            for (int i = 0; i < 256; i++)
            {
                int rampFirst = Math.Min(255, (i * 2));
                int rampLast = Math.Max(0, (i - 128) * 2);
                pal.Entries[i] = Color.FromArgb(255, rampLast, i, rampFirst);
            }
            return pal;
        }

        ColorPalette pallette_green(ColorPalette pal)
        {
            for (int i = 0; i < 256; i++)
            {
                int rampFirst = Math.Min(255, (i * 2));
                int rampLast = Math.Max(0, (i - 128) * 2);
                pal.Entries[i] = Color.FromArgb(255, rampLast, rampFirst, i);
            }
            return pal;
        }

        ColorPalette pallette_spectrum(ColorPalette pal)
        {
            int r, g, b;
            for (int i = 0; i < 256; i++)
            {
                r = i * 4 - 128 * 4;
                b = 128 * 2 - i * 4;
                g = i * 4;
                if (i > 128) g = 256 * 4 - g;

                r = Math.Max(0, Math.Min(255, r));
                g = Math.Max(0, Math.Min(255, g));
                b = Math.Max(0, Math.Min(255, b));
                pal.Entries[i] = Color.FromArgb(255, r, g, b);
            }
            return pal;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            DataLineGenerate();
        }

        public void SetTimerState(bool state_value)
        {
            if (state_value)
            {
                tmr_DataGenerating.Start();
            }
            else 
            {
                tmr_DataGenerating.Stop();
            }
        }

        public void SavePictureImage()
        {
            SaveFileDialog ofd = new SaveFileDialog();
            ofd.InitialDirectory = "";
            ofd.Filter = "Image Files | *.bmp";
            ofd.DefaultExt = "bmp";
            ofd.RestoreDirectory = true;
            if (ofd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                pictureBox1.Image.Save(ofd.FileName, ImageFormat.Bmp);
            }

        }


    }

    static class SpectrumExtension
    {
        delegate string SetSpectrumAddSpectrumCallback(ComboBox cmb_colormap);
        /// <summary>
        /// This extension method appends collored text.
        /// </summary>
        public static string getCmbColor(this ComboBox cmb_colormap)
        {
            // If this method was called from the another thread.
            if (cmb_colormap.InvokeRequired)
            {
                var deleg = new SetSpectrumAddSpectrumCallback(getCmbColor);
                
                return (string) cmb_colormap.Invoke(deleg, new object[] { cmb_colormap });
                
            }
            else
            {
                return cmb_colormap.Text;
            }
        }

    }

    class MyDataFile : AppSettings<MyDataFile>
    {
        public int myScanCounter = 1;
        public int myDataSize = 1;
        public string VisualisationMode = "green";
        public bool chkFilter = true;
        public double[] data_set = new double[1000];
    }

}
