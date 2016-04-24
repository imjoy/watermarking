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
    public class ImageCtrl
    {
        private picture _pic;

        public picture Pic
        {
            get
            {
                if (_pic == null)
                {
                    _pic = new picture();
                }
                return _pic;
            }
            set { _pic = value; }
        }
        public Bitmap src;

        public void initSrc(String path, PictureBox x)
        {
            x.Load(path);
            src = new Bitmap(x.Image, new Size(x.Width,x.Height));
        }

    }
}
