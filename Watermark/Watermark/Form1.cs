using OpenCvSharp.UserInterface;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Watermark.control;
using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Double;
using MathNet.Numerics.LinearAlgebra.Factorization;

namespace Watermark
{
    public partial class Form1 : Form
    {
        public ImageCtrl original = new ImageCtrl();
        public ImageCtrl Watermark = new ImageCtrl();
        public ImageCtrl Watermarked = new ImageCtrl();
        public rdwt Transform;
        public DCT DCTTrans;
        public double[,] Hostred;
        public double[,] Hostgreen;
        public double[,] Hostblue;
        public double[,] Wmred;
        public double[,] Wmgreen;
        public double[,] Wmblue;
        public svd_operation svd_host;
        public svd_operation svd_watermark;
        public double[,] markedred;
        public double[,] markedgreen;
        public double[,] markedblue;
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            original.Pic.FilePicture = openFile();
            if (original.Pic.FilePicture != "none")
            {
                original.initSrc(original.Pic.FilePicture, OriginalPic);
                Hostred = new double[original.src.Width, original.src.Height];
                Hostgreen = new double[original.src.Width, original.src.Height];
                Hostblue = new double[original.src.Width, original.src.Height];
                getValueColor(Hostred, Hostgreen, Hostblue, original.src);

            }
            
            //Transform = new rdwt((Image)original.src);
            //Transform.rdwtTransform(original.src);
            //RDWTImage.Image = Transform.bmp.SrcDwt;
            
        }

        private String openFile()
        {
            OpenFileDialog win_up = new OpenFileDialog();
            win_up.Filter = "All Images File |*.jpg;*.JPG;*.JPEG;*.jpeg;*.png;*.gif";
            win_up.Multiselect = false;
            if (win_up.ShowDialog() == DialogResult.OK)
            {
                return win_up.FileName;
            }
            return "none";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Watermark.Pic.FilePicture = openFile();
            if (Watermark.Pic.FilePicture != "none")
            {
                Watermark.initSrc(Watermark.Pic.FilePicture,watermacPic);
                Wmred = new double[Watermark.src.Width, Watermark.src.Height];
                Wmgreen = new double[Watermark.src.Width, Watermark.src.Height];
                Wmblue = new double[Watermark.src.Width, Watermark.src.Height];
                getValueColor(Wmred, Wmgreen, Wmblue, Watermark.src);
            }
        }

        private void DCTBTN_Click(object sender, EventArgs e)
        {
            Bitmap Temp = new Bitmap(RDWTImage.Image);
            DCTTrans = new DCT(Temp);
            DCTTrans.DCT_Transform(Hostred);
            DCTTrans.DCT_Transform(Hostgreen);
            DCTTrans.DCT_Transform(Hostblue);
            generateImage(Hostred, Hostgreen, Hostblue, original.src);
            RDWTImage.Image = original.src;

            Temp = new Bitmap(RDWTWatermark.Image);
            DCTTrans = new DCT(Temp);
            DCTTrans.DCT_Transform(Wmred);
            DCTTrans.DCT_Transform(Wmgreen);
            DCTTrans.DCT_Transform(Wmblue);
            generateImage(Wmred, Wmgreen, Wmblue, Watermark.src);
            RDWTWatermark.Image = Watermark.src;
        }

        private void InverseDCTBtn_Click(object sender, EventArgs e)
        {
            Bitmap Temp = new Bitmap(RDWTImage.Image);
            DCTTrans = new DCT(Temp);
            DCTTrans.InverseDCT(Hostred);
            DCTTrans.InverseDCT(Hostgreen);
            DCTTrans.InverseDCT(Hostblue);
            generateImage(Hostred, Hostgreen, Hostblue, original.src);
            RDWTImage.Image = original.src;

            Temp = new Bitmap(RDWTWatermark.Image);
            DCTTrans = new DCT(Temp);
            DCTTrans.InverseDCT(Wmred);
            DCTTrans.InverseDCT(Wmgreen);
            DCTTrans.InverseDCT(Wmblue);
            generateImage(Wmred, Wmgreen, Wmblue, Watermark.src);
            RDWTWatermark.Image = Watermark.src;
        }

        private void InverseRDWTBTN_Click(object sender, EventArgs e)
        {
            Transform = new rdwt();
            Transform.InverseRDWT(Hostred);
            Transform.InverseRDWT(Hostgreen);
            Transform.InverseRDWT(Hostblue);
            generateImage(Hostred, Hostgreen, Hostblue, original.src);
            RDWTImage.Image = original.src;

            Transform = new rdwt();
            Transform.InverseRDWT(Wmred);
            Transform.InverseRDWT(Wmgreen);
            Transform.InverseRDWT(Wmblue);
            generateImage(Wmred, Wmgreen, Wmblue, Watermark.src);
            RDWTWatermark.Image = Watermark.src;
        }

        private void getValueColor( double[,] Red, double[,] green, double[,] blue, Bitmap src)
        {
            Color c;
            for (int i = 0; i < src.Width; i++)
            {
                for (int j = 0; j < src.Height; j++)
                {
                    c = src.GetPixel(i, j);
                    Red[i, j] = Scale(0,255,-1,1,c.R);
                    green[i, j] = Scale(0,255,-1,1,c.G);
                    blue[i, j] = Scale(0,255,-1,1,c.B);
                }
            }
        }

        private void rdwthostbtn_Click(object sender, EventArgs e)
        {
            Transform = new rdwt();
            Transform.rdwtTransform(Hostred);
            Transform.rdwtTransform(Hostgreen);
            Transform.rdwtTransform(Hostblue);
            generateImage(Hostred, Hostgreen, Hostblue, original.src);
            RDWTImage.Image = original.src;

            Transform = new rdwt();
            Transform.rdwtTransform(Wmred);
            Transform.rdwtTransform(Wmgreen);
            Transform.rdwtTransform(Wmblue);
            generateImage(Wmred, Wmgreen, Wmblue, Watermark.src);
            RDWTWatermark.Image = Watermark.src;
        }

        public static double Scale(double fromMin, double fromMax, double toMin, double toMax, double x)
        {
            if (fromMax - fromMin == 0) return 0;
            double value = (toMax - toMin) * (x - fromMin) / (fromMax - fromMin) + toMin;
            if (value > toMax)
            {
                value = toMax;
            }
            if (value < toMin)
            {
                value = toMin;
            }
            return value;
        }

        public void generateImage(double[,] red, double[,] green, double[,] blue, Bitmap x)
        {
            for (int i = 0; i < x.Width; i++)
            {
                for (int j = 0; j < x.Height; j++)
                {
                    x.SetPixel(i, j, Color.FromArgb((int)Scale(-1, 1, 0, 255, red[i, j]), (int)Scale(-1, 1, 0, 255, green[i, j]), (int)Scale(-1, 1, 0, 255, blue[i, j])));
                }
            }
        }

        private void svd_pso_Click(object sender, EventArgs e)
        {
            Watermarked = new ImageCtrl();
            Watermarked.src = new Bitmap(RDWTImage.Image);
            Bitmap temp = new Bitmap(RDWTImage.Image);
            Bitmap tempwm = new Bitmap(RDWTWatermark.Image);
            markedred = Hostred;
            markedgreen = Hostgreen;
            markedblue = Hostblue;
            markedred = svd_pso_operation(Hostred, Wmred, temp, tempwm);
            markedgreen = svd_pso_operation(Hostgreen, Wmgreen, temp, tempwm);
            markedblue = svd_pso_operation(Hostblue, Wmblue, temp, tempwm);
            generateImage(markedred, markedgreen, markedblue, Watermarked.src);
            RDWTImage.Image = Watermarked.src;
        }

        private double[,] svd_pso_operation(double[,] hostcolor, double[,] wmcolor, Bitmap pic, Bitmap WmPic)
        {
            svd_host = new svd_operation(pic,hostcolor);
            svd_watermark = new svd_operation(WmPic,wmcolor);
            double[,] pp = svd_watermark.getP();
            for(int i=0;i < pp.GetLength(0);i++){
                for(int j=0;j<pp.GetLength(1);j++){
                    pp[i,j]*=0.0025;
                }
            }

            double[,] snew = new double[pp.GetLength(0), pp.GetLength(1)];
            snew = svd_host.getnew_W(pp);
            double[,] host_u = svd_host.svd.U.ToArray();
            double[,] host_vt = svd_host.svd.VT.ToArray();
            double[,] host_w = svd_host.get_w();
            double[,] recompo = new double[host_u.GetLength(0), host_u.GetLength(1)];
            recompo = svd_host.perkalian_matrix(host_u, snew);
            recompo = svd_host.perkalian_matrix(recompo, host_vt);
            for (int i = 0; i < recompo.GetLength(0); i++)
            {
                for (int j = 0; j < recompo.GetLength(1); j++)
                {
                    hostcolor[i, j] = recompo[i, j];
                }
            }
                return hostcolor;
        }


    }
}
