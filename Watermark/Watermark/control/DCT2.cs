using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using OpenCvSharp;

namespace Watermark.control
{
    public class DCT2
    {
        public Boolean intiated = false;
        public int row;
        public int col;
        public double[,] ct;

        public DCT2(Bitmap dwt)
        {
            double x = 0.0;
            double y = 0.0;
            row = dwt.Width >> 1;
            col = dwt.Height >> 1;
            if (!intiated)
            {
                inisiasii();
            }
            for (int i = 0; i < row; i++)
            {
                if(i == 0){
                    x = 1/Math.Sqrt(row);
                }else{
                    x = 2/Math.Sqrt(row);
                }
                for (int j = 0; j < col; j++)
                {
                    if(j == 0){
                        x = 1/Math.Sqrt(row);
                    }else{
                        x = 2/Math.Sqrt(row);
                    }
                    ct[i,j] = x*y;
                }
            }
        }


        public void inisiasii()
        {
            ct = new double[row, col];
        }

        public Bitmap DCT_Transform(Bitmap dwt)
        {
            Bitmap Temp = new Bitmap(dwt);
            Color c;
            double[,] temp_red = new double[row, col];
            double[,] temp_green = new double[row, col];
            double[,] temp_blue = new double[row, col];
            double final_temp_red;
            double final_temp_green;
            double final_temp_blue;
            double[,] red = new double[row, col];
            double[,] green = new double[row, col];
            double[,] blue = new double[row, col];
            for (int i = 0; i < row; i++)
            {
                for (int j = 0; j < col; j++)
                {
                    c = Temp.GetPixel(i, j);
                    red[i, j] = c.R - 128;
                    green[i, j] = c.G - 128;
                    blue[i, j] = c.B - 128;
                }
            }

            for (int i = 0; i < row; i++)
            {
                for (int j = 0; j < col; j++)
                {
                    temp_red[i,j] = 0.0;
                    temp_green[i,j] = 0.0;
                    temp_blue[i,j] = 0.0;
                    for (int k = 0; k < row; k++)
                    {
                        for (int l = 0; l < row; l++)
                        {
                            temp_red[i, j] += red[i, j] * (Math.Cos((Math.PI * ((2 * k) + 1) * i) / (2 * row))) * (Math.Cos((Math.PI * ((2 * k) + 1) * j) / (2 * col)));
                            temp_green[i, j] += green[i, j] * (Math.Cos((Math.PI * ((2 * k) + 1) * i) / (2 * row))) * (Math.Cos((Math.PI * ((2 * k) + 1) * j) / (2 * col)));
                            temp_blue[i, j] += blue[i, j] * (Math.Cos((Math.PI * ((2 * k) + 1) * i) / (2 * row))) * (Math.Cos((Math.PI * ((2 * k) + 1) * j) / (2 * col)));
                        }
                    }
                    temp_red[i,j] *= ct[i,j];
                    temp_green[i,j] *= ct[i,j];
                    temp_blue[i,j] *= ct[i,j];
                }
            }

            for (int i = 0; i < row; i++)
            {
                for (int j = 0; j < col; j++)
                {
                    red[i, j] = normalize(temp_red[i, j]);
                    green[i, j] = normalize(temp_green[i, j]);
                    blue[i, j] = normalize(temp_blue[i, j]);

                    Temp.SetPixel(i, j, Color.FromArgb((int)red[i,j],(int)green[i,j],(int)blue[i,j]));
                }
            }
                return Temp; 
        }


        public double normalize(double x)
        {
            if (x > 255.0)
            {
                x = 255;
            }
            else if (x < 0)
            {
                x = 0;
            }
            return x;
        }

    }
}
