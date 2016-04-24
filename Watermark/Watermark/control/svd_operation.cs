using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Double;
using MathNet.Numerics.LinearAlgebra.Factorization;
using MathNet.Numerics.IntegralTransforms;
using System.Drawing;

namespace Watermark.control
{
    public class svd_operation
    {
        public Matrix<double> Input;
        public Matrix<double> Output;
        public Svd<double> svd;
        private double[,] src;
        private int row = 0;
        private int col = 0;
        public svd_operation(Bitmap x, double[,] color)
        {
            row = x.Width >> 1;
            col = x.Height >> 1;
            src = new double[row, col];
            src = init_src(color,row,col);
            Input = DenseMatrix.OfArray(src);
            svd = Input.Svd(true);
        }

        public double[,] init_src(double[,] color,int x, int y)
        {
            double[,] temp = new double[x, y];
            for (int i = 0; i < x; i++)
            {
                for (int j = 0; j < y; j++)
                {
                    temp[i, j] = color[i, j];
                }
            }
            return temp;
        }

        public double[,] getP()
        {
            double[] nilai_s = svd.S.ToArray();
            int count = 0;
            double[,] nilai_w = new double[row, col];
            for (int i = 0; i < row; i++)
            {
                for (int j = 0; j < col; j++)
                {
                    if (i == j)
                    {
                        nilai_w[i,j] = nilai_s[count];
                        count++;
                    }
                    else
                    {
                        nilai_w[i, j] = 0.0;
                    }
                }
            }
            double[,] nilai_u = svd.U.ToArray();
            double[,] hasil = perkalian_matrix(nilai_u, nilai_w);
            return hasil;
        }

        public double[,] recompotion(Matrix<double>u,Matrix<double>s,Matrix<double>vt)
        {
            double[,] temp = u.Multiply(s).Multiply(vt).ToArray();
            return temp;
        }

        public double[,] get_w()
        {
            double[] nilai_s = svd.S.ToArray();
            int count = 0;
            double[,] nilai_w = new double[row, col];
            for (int i = 0; i < row; i++)
            {
                for (int j = 0; j < col; j++)
                {
                    if (i == j)
                    {
                        nilai_w[i, j] = nilai_s[count];
                        count++;
                    }
                    else
                    {
                        nilai_w[i, j] = 0.0;
                    }
                }
            }
            return nilai_w;
        }

        public double[,] getnew_W(double[,] p)
        {

            double[,] nilai_w = get_w();
            for (int i = 0; i < row; i++)
            {
                for (int j = 0; j < col; j++)
                {
                    nilai_w[i, j] += p[i, j];
                }
            }
            return nilai_w;

        }

        public double[,] perkalian_matrix(double[,] x, double[,] y)
        {
            double[,] hasil = new double[row, col];

            for (int i = 0; i < row; i++)
            {
                for (int j = 0; j < row; j++)
                {
                    hasil[i, j] = 0;
                    for (int k = 0; k < col; k++)
                    {
                        hasil[i, j] += (x[i, k] * y[k, j]);
                    }
                }
            }
                return hasil;
        }
    }
}
