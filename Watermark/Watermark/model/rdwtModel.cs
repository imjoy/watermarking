using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Watermark.model
{
    public class rdwtModel
    {
        private Image src;
        public rdwtModel(Image x)
        {
            src = x;
        }
        private Bitmap _highHigh;

        public Bitmap HighHigh
        {
            get {
                if (_highHigh == null)
                {
                    _highHigh = new Bitmap(src, new Size(src.Width, src.Height));
                }
                return _highHigh;
            }
            set { _highHigh = value; }
        }

        private Bitmap _highLow;

        public Bitmap HighLow
        {
            get {
                if (_highLow == null)
                {
                    _highLow = new Bitmap(src, new Size(src.Width, src.Height));
                }
                return _highLow; }
            set { _highLow = value; }
        }

        private Bitmap _lowHigh;

        public Bitmap LowHigh
        {
            get {
                if (_lowHigh == null)
                {
                    _lowHigh = new Bitmap(src, new Size(src.Width, src.Height));
                }
                return _lowHigh; }
            set { _lowHigh = value; }
        }

        private Bitmap _lowLow;

        public Bitmap LowLow
        {
            get {
                if (_lowLow == null)
                {
                    _lowLow = new Bitmap(src, new Size(src.Width, src.Height));
                }
                return _lowLow; }
            set { _lowLow = value; }
        }

        private Bitmap _srcDwt;

        public Bitmap SrcDwt
        {
            get {
                if (_srcDwt == null)
                {
                    _srcDwt = new Bitmap(src, new Size(src.Width,src.Height));
                }
                return _srcDwt; }
            set { _srcDwt = value; }
        }

    }
}
