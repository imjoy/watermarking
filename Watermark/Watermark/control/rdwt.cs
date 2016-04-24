using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Watermark.model;

namespace Watermark.control
{
   public class rdwt
    {
        public rdwtModel bmp;
        public static double Konstanta = 0.2;
        public Image src;
        private const double w0 = 0.5071067812;
        private const double w1 = -0.5071067812;
        private const double s0 = 0.5071067812;
        private const double s1 = 0.5071067812;
        public rdwt()
        {
            //src = x;
            //bmp =  new rdwtModel(src);
        }
        public void rdwtTransform(double[,] color)
        {


            color = filterPass(color); 

            //int HalfWidth = Red.GetLength(0) >> 1;
            //int halfheigh = Red.GetLength(1) >> 1;
            //bmp.LowLow = new Bitmap(null,new Size(HalfWidth,halfheigh));
            //for (int i = 0; i < halfheigh; i++)
            //{
            //    for (int j = 0; j < HalfWidth; j++)
            //    {
            //        bmp.LowLow.SetPixel(j, i, Color.FromArgb((int)Scale(-1, 1, 0, 255, Red[j, i]), (int)Scale(-1, 1, 0, 255, Green[j, i]), (int)Scale(-1, 1, 0, 255, Blue[j, i])));
            //    }
            //}
            
        }

      

        public double[,] filterPass(double[,] warna)
        {
            int row = warna.GetLength(0);
            int col = warna.GetLength(1);
            double[] rows = new double[col];
            double[] cols = new double[row];
            for (int i = 0; i < row; i++)
            {
                for (int j = 0; j < rows.Length;j++){
                   
                    rows[j] = warna[i, j];
                }
                rows = FilterPas1d(rows);
                for (int j = 0; j < rows.Length; j++)
                {
                    warna[i, j] = rows[j];
                }
            }

            for (int i = 0; i < col; i++)
            {
                for (int j = 0; j < cols.Length; j++)
                {
                    cols[j] = warna[j, i];  
                }
                cols = FilterPas1d(cols);
                for (int j = 0; j < cols.Length; j++)
                {
                    warna[j, i] = cols[j];
                }
            }
            return warna;
        }

        public double[] FilterPas1d(double[] data)
        {

            double[] temp = new double[data.Length];

            int h = data.Length >> 1;
            for (int i = 0; i < h; i++)
            {
                int k = (i << 1);
                temp[i] = data[k] * s0 + data[k + 1] * s1;
                temp[i + h] = data[k] * w0 + data[k + 1] * w1;
            }

            return temp;
        }

        public double HighPassFilter(double x,int n)
        {
            x = 1 / (1 + Math.Pow((Konstanta / x), (2 *n)));
            return x;
        }

        public double LowPassFilter(double x, int n)
        {
            x = 1 / (1 + Math.Pow((x / Konstanta), (2 * n)));
            return x;
        }

       

        public double[,] IWT(double[,] data)
        {
            int rows = data.GetLength(0);
            int cols = data.GetLength(1);

            double[] col = new double[rows];
            double[] row = new double[cols];

            
                for (int j = 0; j < cols; j++)
                {
                    for (int i = 0; i < row.Length; i++)
                        col[i] = data[i, j];

                   col =  IWT1D(col);

                    for (int i = 0; i < col.Length; i++)
                        data[i, j] = col[i];
                }

                for (int i = 0; i < rows; i++)
                {
                    for (int j = 0; j < row.Length; j++)
                        row[j] = data[i, j];

                    row = IWT1D(row);

                    for (int j = 0; j < row.Length; j++)
                        data[i, j] = row[j];
                }
                return data;
        }

        public double[] IWT1D(double[] data)
        {
            double[] temp = new double[data.Length];

            int h = data.Length >> 1;
            for (int i = 0; i < h; i++)
            {
                int k = (i << 1);
                temp[k] = (data[i] * s0 + data[i + h] * w0) / w0;
                temp[k + 1] = (data[i] * s1 + data[i + h] * w1) / s0;
            }

            return temp;
        }

        public void InverseRDWT(double[,] color)
        {
            color = IWT(color);
            

            //int HalfWidth = Red.GetLength(0) >> 1;
            //int halfheigh = Red.GetLength(1) >> 1;
            //bmp.LowLow = new Bitmap(null,new Size(HalfWidth,halfheigh));
            //for (int i = 0; i < halfheigh; i++)
            //{
            //    for (int j = 0; j < HalfWidth; j++)
            //    {
            //        bmp.LowLow.SetPixel(j, i, Color.FromArgb((int)Scale(-1, 1, 0, 255, Red[j, i]), (int)Scale(-1, 1, 0, 255, Green[j, i]), (int)Scale(-1, 1, 0, 255, Blue[j, i])));
            //    }
           // }
        }
    }
}
