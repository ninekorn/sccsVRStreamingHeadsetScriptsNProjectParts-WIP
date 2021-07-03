using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SharpDX;
using SharpDX.Direct3D11;

using System.Drawing;


using SharpDX.D3DCompiler;

using System.Runtime.InteropServices;
//using System.Windows.Forms;



namespace sccoresystems
{
    public struct sccssharpdxscreenframe
    {
        public int width;
        public int height;
        //public byte[] bitmapByteArray;
        //public byte[] bitmapEmptyByteArray;
        //public byte[] frameByteArray;
        //public Bitmap mouseCursor;
        //public byte[] cursorByteArray;
        //public byte[] cursorByteArray;
        public byte[] bitmapByteArray;

        public byte[][] screencapturearrayofbytes;
        public int[][] screencapturearrayofints;

        //public System.Drawing.Point cursorPointPos;

        //public int desktopWidth;
        //public int desktopHeight;
        public ShaderResourceView ShaderResource;
        public ShaderResourceView[] ShaderResourceArray;
        //public Texture2D texture2DFinal;
        //public Bitmap somebitmapforarduino;
        //public int memorymapstride;
        public int framecapturecounter;
    }

    public struct sccssharpdxscreencapturedata
    {
        public sccsscreencapture currentscreencaptureclass;
    }
}
