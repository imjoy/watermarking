using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Watermark.control
{
    public class DCT
    {
        public bool Intiated = false;
        public int row;
        public int col;
        public double[,] c_red;
        public double[,] ct_red;

        public DCT(Bitmap DWT)
        {
            row = DWT.Width >> 1;
            col = DWT.Height >> 1;
            if (!Intiated)
            {
                inisiasi(row, col);
                Intiated = true;
            }
            for (int i = 0; i < col; i++)
            {
                c_red[0, i] = 1.0 / Math.Sqrt((double)col) * Math.Cos(Math.PI * (2 * i + 1) * 0 / (2.0 * (double)col));
                ct_red[i, 0] = c_red[0, i]; 
            }
            for (int i = 1; i < row; i++)
            {
                for (int j = 0; j < col; j++)
                {
                    c_red[i, j] = Math.Sqrt(2.0 / (double)col) * Math.Cos(Math.PI * (2 * j + 1) * i / (2.0 * (double)col));
                    ct_red[j, i] = c_red[i, j];
                }
            }

        }

        private void inisiasi(int row, int col){
            c_red = new double[row, col];
            ct_red = new double[row, col];
        }

        public void DCT_Transform(double[,] color){

            double[,] temp = new double[row, col];
            double final_temp;

                for (int i = 0; i < row; i++)
                {
                    for (int j = 0; j < col; j++)
                    {
                        temp[i, j] = 0.0;
                        for (int k = 0; k < row; k++)
                        {
                            temp[i, j] += ((color[i, k] - Form1.Scale(0,255,-1,1,128)) * ct_red[k, j]);
                        }
                    }
                }

                for (int i = 0; i < row; i++)
                {
                    for (int j = 0; j < col; j++)
                    {
                        final_temp = 0.0;
                        for (int k = 0; k < row; k++)
                        {
                            final_temp += (c_red[i, k] * temp[k, j]);
                        }
                        color[i, j] = final_temp;
                    }
                }
        }


        public void InverseDCT(double[,] color){
            
            double[,] temp = new double[row, col];
            double final_temp;
            for (int i = 0; i < row; i++)
            {
                for (int j = 0; j < col; j++)
                {
                   
                    temp[i, j] = 0.0;

                    for (int k = 0; k < col; k++)
                    {
                       temp[i, j] += (color[i, k]+Form1.Scale(0,255,-1,1,128)) * c_red[k, j];
                    }
                }
            }
            for (int i = 0; i < row; i++)
            {
                for (int j = 0; j < col; j++)
                {
                    final_temp=0.0;

                    for (int k = 0; k < row; k++)
                    {
                        final_temp += (ct_red[i, k]) * temp[k, j];
                    }


                    color[i, j] = final_temp;
                }
            }
        }
    }
}
