using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Watermark.control
{
    public class writeFile
    {

        public static void sendToFile(double[,] color, String File)
        {
            String location = File;

            bool folderExist = Directory.Exists(Environment.CurrentDirectory.ToString() + "\\Matriks");
            if (!folderExist)
            {
                Directory.CreateDirectory(Environment.CurrentDirectory.ToString() + "\\Matriks");
            }

            using (StreamWriter writer = new StreamWriter(location, false))
            {
                for (int i = 0; i < color.GetLength(0); i++)
                {
                    for (int j = 0; j < color.GetLength(1); j++)
                    {
                        writer.Write( Math.Floor(Form1.Scale(-1,1,0,255,color[i, j])));

                        if (j < color.GetLength(1) - 1)
                        {
                            writer.Write(" ");
                        }
                    }

                    writer.Write("\r\n");
                }
            }
        }
    }
}
