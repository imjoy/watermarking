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

        public double[,] vtWmRed;
        public double[,] vtWmGreen;
        public double[,] vtWmBlue;

        public double[,] sWmRed;
        public double[,] swGreen;
        public double[,] swBlue;
        public Bitmap wm, host, vtwm;
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
                String filename = Environment.CurrentDirectory.ToString()+"\\Matriks\\HostRed.txt";
                writeFile.sendToFile(Hostred, filename);
                filename = Environment.CurrentDirectory.ToString() + "\\Matriks\\HostGreen.txt";
                writeFile.sendToFile(Hostgreen, filename);
                filename = Environment.CurrentDirectory.ToString() + "\\Matriks\\HostBlue.txt";
                writeFile.sendToFile(Hostblue, filename);
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
            
            Bitmap tmp = new Bitmap(watermarkedImg.Image);
            DCTTrans = new DCT(Temp);
            DCTTrans.InverseDCT(markedred);
            DCTTrans.InverseDCT(markedgreen);
            DCTTrans.InverseDCT(markedblue);
            generateImage(markedred, markedgreen, markedblue, tmp);
            watermarkedImg.Image = tmp;
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

            Transform = new rdwt();
            Bitmap wmked = new Bitmap(watermarkedImg.Image);
            Transform.InverseRDWT(markedred);
            Transform.InverseRDWT(markedgreen);
            Transform.InverseRDWT(markedblue);
            String FileName = Environment.CurrentDirectory.ToString() + "//Matriks//MarkedRed.txt";
            writeFile.sendToFile(markedred, FileName);
            FileName = Environment.CurrentDirectory.ToString() + "//Matriks//MarkedGreen.txt";
            writeFile.sendToFile(markedgreen, FileName);
            FileName = Environment.CurrentDirectory.ToString() + "//Matriks//MarkedBlue.txt";
            writeFile.sendToFile(markedblue, FileName);
            generateImage(markedred, markedgreen, markedblue, wmked);
            watermarkedImg.Image = wmked;

            double mseRed = mse(Hostred, markedred);
            double mseBlue = mse(Hostblue, markedblue);
            double mseGreen = mse(Hostgreen, markedgreen);
            double avgMSE = (mseRed + mseBlue + mseGreen) / 3.0;
            mseLbl.Text = avgMSE.ToString();

            double psnrRed = psnr(Hostred, markedred);
            double psnrGreen = psnr(Hostgreen, markedgreen);
            double pnsrBlue = psnr(Hostblue, markedblue);
            double avgPsnr = (psnrRed + psnrGreen + pnsrBlue) / 3.0;
            psnrLbl.Text = avgPsnr.ToString();
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
            Bitmap res = new Bitmap(OriginalPic.Image, new Size(watermarkedImg.Width, watermarkedImg.Height));
            Bitmap temp = new Bitmap(RDWTImage.Image);
            Bitmap tempwm = new Bitmap(RDWTWatermark.Image);
            markedred = new double[Hostred.GetLength(0), Hostred.GetLength(1)];
            markedblue = new double[Hostblue.GetLength(0), Hostblue.GetLength(1)];
            markedgreen = new double[Hostgreen.GetLength(0), Hostgreen.GetLength(1)];
            for (int i = 0; i < markedred.GetLength(0); i++)
            {
                for (int j = 0; j < markedred.GetLength(1); j++)
                {
                    markedred[i, j] = Hostred[i, j];
                    markedblue[i, j] = Hostblue[i, j];
                    markedgreen[i, j] = Hostgreen[i, j];
                }
            }
            markedred = svd_pso_operation(markedred, Wmred, temp, tempwm, vtWmRed,"red");
            markedgreen = svd_pso_operation(markedgreen, Wmgreen, temp, tempwm, vtWmGreen,"green");
            markedblue = svd_pso_operation(markedblue, Wmblue, temp, tempwm,vtWmBlue,"blue");
            generateImage(markedred, markedgreen, markedblue,res);
            watermarkedImg.Image = res;
        }

        private double[,] svd_pso_operation(double[,] hostcolor, double[,] wmcolor, Bitmap pic, Bitmap WmPic, double[,] vt , string color)
        {
            svd_host = new svd_operation(pic,hostcolor);
            svd_watermark = new svd_operation(WmPic,wmcolor);
            vt = svd_watermark.getP();
            for(int i=0;i < vt.GetLength(0);i++){
                for(int j=0;j<vt.GetLength(1);j++){
                    vt[i,j]*=0.0025;
                }
            }

            double[,] snew = new double[vt.GetLength(0), vt.GetLength(1)];
            snew = svd_host.getnew_W(vt);
            if (sWmRed == null)
            {
                sWmRed = new double[snew.GetLength(0), snew.GetLength(1)];
            }

            if (swGreen == null)
            {
                swGreen = new double[snew.GetLength(0), snew.GetLength(1)];
            }

            if (swBlue == null)
            {
                swBlue = new double[snew.GetLength(0), snew.GetLength(1)];
            }

            for (int i = 0; i < snew.GetLength(0); i++)
            {
                for (int j = 0; j < snew.GetLength(1); j++)
                {
                    if (color.Equals("red"))
                    {
                        sWmRed[i, j] = snew[i, j];
                    }else if (color.Equals("green"))
                    {
                        swGreen[i, j] = snew[i, j];
                    }else if (color.Equals("blue"))
                    {
                        swBlue[i, j] = snew[i, j];
                    }
                }
            }
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

        private double[,] Extracted_pso_operation(double[,] hostcolor, double[,] wmcolor, double[,] vtWcolor, Bitmap pic, Bitmap WmPic ,Bitmap vwPic,double[,] sWm)
        {
            svd_host = new svd_operation(pic, hostcolor);
            svd_watermark = new svd_operation(WmPic, wmcolor);
            svd_operation svd_ww = new svd_operation(vwPic,vtWcolor);
            double[,] wmP = svd_ww.get_w();
            double[,] hostP = svd_host.get_w();
            double[,] newS = new double[WmPic.Width, WmPic.Height];
            for (int i = 0; i < wmP.GetLength(0); i++)
            {
                for (int j = 0; j < wmP.GetLength(1); j++)
                {
                    newS[i,j] = (wmP[i,j]-hostP[i,j])/0.0025;
                }
            }
            double[,] wmU = svd_watermark.svd.U.ToArray();
            double[,] wmVt = svd_watermark.svd.VT.ToArray();

            double[,] recompo = new double[wmU.GetLength(0), wmU.GetLength(1)];
            recompo = svd_watermark.perkalian_matrix(wmU, newS);
            recompo = svd_watermark.perkalian_matrix(recompo, wmVt);

            for (int i = 0; i < recompo.GetLength(0); i++)
            {
                for (int j = 0; j < recompo.GetLength(1); j++)
                {
                    hostcolor[i, j] = recompo[i, j];
                }
            }
                return hostcolor;
        }

        private double mse(double[,] ori, double[,] wm)
        {
            double res = 0.0;
            for (int i = 0; i < ori.GetLength(0); i++)
            {
                for (int j = 0; j < ori.GetLength(1); j++)
                {
                    double temp = ori[i,j]-wm[i,j];
                    res += Math.Pow(temp, 2);
                }
            }

            double dimensi = ori.GetLength(0) * ori.GetLength(1);
            res /= dimensi;
             return res;
        }

        private double psnr(double[,] ori, double[,] wm)
        {
            double res = 0.0;

            double MSE = mse(ori, wm);
            double temp = (ori.GetLength(0)*ori.GetLength(1))/MSE;
            res = Math.Log10(temp);
            return res;
        }

        public void extractedImage()
        {
            wm = new Bitmap(RDWTWatermark.Image, new Size(RDWTWatermark.Width, RDWTWatermark.Height));
            host = new Bitmap(RDWTImage.Image, new Size(RDWTImage.Width, RDWTImage.Height));
            vtwm = new Bitmap(watermarkedImg.Image, new Size(watermarkedImg.Width, watermarkedImg.Height));

            extractedImg.Invoke((MethodInvoker)(() => extractedImg.Image = vtwm));
            Wmred= new double[wm.Width, wm.Height];
            Wmgreen = new double[wm.Width, wm.Height];
            Wmblue = new double[wm.Width, wm.Height];
            getValueColor(Wmred, Wmgreen, Wmblue, wm);

            Hostred = new double[host.Width, host.Height];
            Hostgreen = new double[host.Width, host.Height];
            Hostblue = new double[host.Width, host.Height];
            getValueColor(Hostred, Hostgreen, Hostblue, host);

            vtWmRed = new double[vtwm.Width, vtwm.Height];
            vtWmGreen = new double[vtwm.Width, vtwm.Height];
            vtWmBlue = new double[vtwm.Width, vtwm.Height];
            getValueColor(vtWmRed, vtWmGreen, vtWmBlue, vtwm);

            rdwt WmRdwt = new rdwt();
            WmRdwt.rdwtTransform(Wmred);
            WmRdwt.rdwtTransform(Wmgreen);
            WmRdwt.rdwtTransform(Wmblue);

            rdwt HostRdwt = new rdwt();
            HostRdwt.rdwtTransform(Hostred);
            HostRdwt.rdwtTransform(Hostgreen);
            HostRdwt.rdwtTransform(Hostblue);

            rdwt WRdwt = new rdwt();
            WRdwt.rdwtTransform(vtWmRed);
            WRdwt.rdwtTransform(vtWmGreen);
            WRdwt.rdwtTransform(vtWmBlue);

            generateImage(Wmred, Wmgreen, Wmblue, wm);
            generateImage(Hostred, Hostgreen, Hostblue, host);
            generateImage(vtWmRed, vtWmGreen, vtWmBlue, vtwm);

            extractedImg.Invoke((MethodInvoker)(() => extractedImg.Image = vtwm));
            //RDWTImage.Image = host;
            //RDWTWatermark.Image = wm;

            DCT WmDct = new DCT(wm);
            DCT HostDct = new DCT(host);
            DCT VtWmDct = new DCT(vtwm);

            WmDct.DCT_Transform(Wmred);
            DCTTrans.DCT_Transform(Wmgreen);
            DCTTrans.DCT_Transform(Wmblue);
            generateImage(Wmred, Wmgreen, Wmblue, wm);

            HostDct.DCT_Transform(Hostred);
            HostDct.DCT_Transform(Hostgreen);
            HostDct.DCT_Transform(Hostblue);
            generateImage(Hostred, Hostgreen, Hostblue, host);

            VtWmDct.DCT_Transform(vtWmRed);
            VtWmDct.DCT_Transform(vtWmGreen);
            VtWmDct.DCT_Transform(vtWmBlue);
            generateImage(vtWmRed, vtWmGreen, vtWmBlue, vtwm);

            extractedImg.Invoke((MethodInvoker)(() => extractedImg.Image = vtwm));

            vtWmRed = Extracted_pso_operation(Hostred, Wmred, vtWmRed, host, wm, vtwm,sWmRed);
            vtWmGreen = Extracted_pso_operation(Hostgreen, Wmgreen, vtWmGreen, host, wm, vtwm,swGreen);
            vtWmBlue = Extracted_pso_operation(Hostblue, Wmblue, vtWmBlue, host, wm, vtwm,swBlue);
            generateImage(vtWmRed, vtWmGreen, vtWmBlue, vtwm);

            DCT vtIDCT = new DCT(vtwm);
            vtIDCT.InverseDCT(vtWmRed);
            vtIDCT.InverseDCT(vtWmGreen);
            vtIDCT.InverseDCT(vtWmBlue);
            generateImage(vtWmRed, vtWmGreen, vtWmBlue, vtwm);
            extractedImg.Invoke((MethodInvoker)(() => extractedImg.Image = vtwm));

            int row = vtWmRed.GetLength(0) >> 1;
            int col = vtWmRed.GetLength(1) >> 1;


            for (int i = 0; i < row; i++)
            {
                for (int j = 0; j < col; j++)
                {
                    Wmred[i, j] = vtWmRed[i, j];
                    Wmgreen[i, j] = vtWmGreen[i, j];
                    Wmblue[i, j] = vtWmGreen[i, j];
                }
            }
            rdwt irdwt = new rdwt();
            irdwt.InverseRDWT(Wmred);
            irdwt.InverseRDWT(Wmgreen);
            irdwt.InverseRDWT(Wmblue);
            generateImage(Wmred, Wmgreen, Wmblue, vtwm);
            extractedImg.Invoke((MethodInvoker)(() => extractedImg.Image = vtwm));
        }

        private void button3_Click(object sender, EventArgs e)
        {
            extractedImage();
        }
    }
}
