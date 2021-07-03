// Copyright (c) 2010-2013 SharpDX - Alexandre Mutel
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.

using System;
using System.Drawing.Imaging;
using System.Drawing;
using System.IO;
using SharpDX;
using SharpDX.Direct3D11;
using SharpDX.DXGI;
using SharpDX.WIC;
using Device = SharpDX.Direct3D11.Device;
using MapFlags = SharpDX.Direct3D11.MapFlags;
using System.Runtime.InteropServices;
using System.Windows.Input;
using SharpDX.Mathematics.Interop;
using Resource = SharpDX.DXGI.Resource;
using ResultCode = SharpDX.DXGI.ResultCode;
using System.Diagnostics;
using System.Threading;
using System.ComponentModel;
using System.Linq;
using System.Collections.Generic;
using System.Windows;
//using Ab3d;
using SharpDX.Direct3D;
using System.Reflection;
using SharpDX.WIC;
using System;
using System.Diagnostics;
using System.IO;
using SharpDX;
using SharpDX.DXGI;
using PixelFormat = System.Drawing.Imaging.PixelFormat;
using SharpDX.Direct2D1;
using SharpDX.DirectWrite;
using SharpDX.IO;
using AlphaMode = SharpDX.Direct2D1.AlphaMode;
using D2DPixelFormat = SharpDX.Direct2D1.PixelFormat;
using WicPixelFormat = SharpDX.WIC.PixelFormat;
using SharpDX.Win32;
using Bitmap = System.Drawing.Bitmap;
using sccoresystems.sccsconsole;

//http://csharphelper.com/blog/2016/06/split-image-files-in-c/
//https://stackoverflow.com/questions/15975972/copy-data-from-from-intptr-to-intptr

//using System.Windows.Media.Imaging;
//using System.Windows.Forms;
//using Device = SharpDX.Direct3D11.Device;
//using MapFlags = SharpDX.Direct3D11.MapFlags;
//using System.Windows.Interop;
//using Ab3d.DirectX.Models;
//using System.Windows.Controls;
//using System.Windows.Media;
//using System.Windows.Media.Media3D;
//using Ab3d.Cameras;
//using Ab3d.Common;
//using Ab3d.Common.Cameras;
//using Ab3d.Controls;
//using Ab3d.DirectX;
//using Ab3d.DirectX.Controls;
//using Ab3d.OculusWrap;
//using Ab3d.Visuals;
//using Ab3d.DXEngine.OculusWrap;
//using Ab3d.DirectX.Materials;
//using Windows.Storage.Streams;
//using Bitmap = SharpDX.WIC.Bitmap;


namespace sccoresystems
{
    /// <summary>
    ///   Screen capture of the desktop using DXGI OutputDuplication.
    /// </summary>
    public unsafe class sccssharpdxscreencapture
    {

        public Bitmap lastbitmap;
        public Texture2D lasttexture2d;
        Output output;
        Output1 output1;
        Adapter1 adapter;
        //public DTerrainHeightMap.DHeightMapType[] arrayOfPixData;
        Texture2DDescription textureDesc;
        public ShaderResourceView lastShaderResourceView;
        public ShaderResourceView[] lastShaderResourceViewArray;
        public ShaderResourceView[] ShaderResourceViewArray;

        // # of graphics card adapter
        static int numAdapter = 0;
        // # of output device (i.e. monitor)
        static int numOutput = 0;

        //static Factory1 factory;

         SharpDX.Direct3D11.Device device; //readonly
        //static Output output;



        static Texture2D texture2D;
        static Texture2D texture2DFinal;
         OutputDuplication outputDuplication; //readonly
        readonly Texture2DDescription textureDescription;
        public static Texture2DDescription textureDescriptionFinal;
        readonly Texture2DDescription textureDescriptionFinalFrac;
        System.Drawing.Bitmap bitmap;

        System.Drawing.Bitmap bitmapPlayerRect;
        Texture2D screenTexture;



        static int width = 0;
        static int height = 0;
        int bytesTotal;
        int bytesTotalPlayerRect;



        SharpDX.DXGI.Resource screenResource;
        OutputDuplicateFrameInformation duplicateFrameInformation;
        OutputDuplicateFrameInformation previousDuplicateFrameInformation;

        sccssharpdxscreenframe frameCaptureData;
        //Bitmap desktopBMP;

        byte[] emptyArrayPaste;
        byte[] arrayPlayerPos;
        byte[] arrayPreviousPlayerPos;
        byte[] currentArrayPlayerPos;


        [System.Runtime.InteropServices.DllImport("gdi32.dll")]
        public static extern bool DeleteObject(IntPtr hObject);

        static Thread thread;
        Action<string> fuckOff;

        public byte[] textureByteArray;//= new byte[1];

        UnmanagedMemoryStream unmanagedMemoryStreamPlayerRect;// = new UnmanagedMemoryStream
                                                               //int size = Marshal.SizeOf(textureByteArray[0] * textureByteArray.Length);
                                                               //var memIntPtr = Marshal.AllocHGlobal(size);

        int[] pastearray;// = new int[1];
        int[] pastearrayTwo;

        IntPtr[] imageptrList;
        System.Drawing.Bitmap piece;
        System.Drawing.Rectangle destrect;
        System.Drawing.Rectangle sourcerect;

        Graphics gr;// = Graphics.FromImage(piece)

        Texture2D[] arrayOfTexture2DFrac;


        const string outputFileName = "ScreenCapture.bmp";
        string path = "";

   

        public sccssharpdxscreencapture(int adapter_, int numOutput_, SharpDX.Direct3D11.Device device)
        {    
            //https://stackoverflow.com/questions/634142/how-to-get-a-path-to-the-desktop-for-current-user-in-c
            path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + @"\#screencapture\";
            Console.WriteLine(path);


            somebitmappath = path + "" + outputFileName;
            fileInfo = new FileInfo(somebitmappath);

            //textureByteArray[0] = 0;
            imageptrList = new IntPtr[numcols * numrows];
            frameCaptureData = new sccssharpdxscreenframe();

            arrayOfTexture2DFrac = new Texture2D[numcols * numrows];

            pastearray = new int[numcols * numrows];
            pastearrayTwo = new int[numcols * numrows];

            arrayOfBytesTwo = new byte[textureDescriptionFinal.Width * textureDescriptionFinal.Height];

            lastShaderResourceViewArray = new ShaderResourceView[numcols * numrows];
            ShaderResourceViewArray = new ShaderResourceView[numcols * numrows];


            numAdapter = adapter_;
            numOutput = numOutput_;


            try
            {
                using (var factory = new SharpDX.DXGI.Factory1())
                {
                    this.adapter = factory.GetAdapter1(numAdapter);
                }
            }
            catch (SharpDXException ex)
            {
                Console.WriteLine(ex.ToString());
                return;
            }

            try
            {
                //this.device = new Device(adapter);
                //this.device = SCCoreSystems.SCConsoleDIRECTX.dxDevice.Device;

                this.device = device;
            }
            catch (SharpDXException ex)
            {
                Console.WriteLine(ex.ToString());
                return;
            }

            try
            {
                //initializeOutput();
                using (var output = adapter.GetOutput(numOutput))
                {
                    // Width/Height of desktop to capture
                    //getDesktopBoundaries();
                    width = ((SharpDX.Rectangle)output.Description.DesktopBounds).Width;
                    height = ((SharpDX.Rectangle)output.Description.DesktopBounds).Height;
                    frameCaptureData.width = width;
                    frameCaptureData.height = height;
                    this.output1 = output.QueryInterface<Output1>();
                }
            }
            catch (SharpDXException ex)
            {
                Console.WriteLine(ex.ToString());
                return;
            }

            try
            {
                //duplicateOutput();
                this.outputDuplication = output1.DuplicateOutput(device);
            }
            catch (SharpDXException ex)
            {
                Console.WriteLine(ex.ToString());
                return;
            }

            try
            {
                //getTextureDescription();
                /*this.textureDescription = new Texture2DDescription
                {
                    CpuAccessFlags = CpuAccessFlags.Read,
                    BindFlags = BindFlags.None,//BindFlags.None, //| BindFlags.RenderTarget
                    Format = Format.B8G8R8X8_UNorm,
                    Width = width,
                    Height = height,
                    OptionFlags = ResourceOptionFlags.None,
                    MipLevels = 1,
                    ArraySize = 1,
                    SampleDescription = { Count = 1, Quality = 0 },
                    Usage = ResourceUsage.Staging
                };*/
                textureDesc = new Texture2DDescription
                {
                    CpuAccessFlags = CpuAccessFlags.None,
                    BindFlags = BindFlags.ShaderResource | BindFlags.RenderTarget,
                    Format = Format.B8G8R8X8_UNorm,
                    Width = width,
                    Height = height,
                    OptionFlags = ResourceOptionFlags.GenerateMipMaps,
                    MipLevels = 1,
                    ArraySize = 1,
                    SampleDescription = { Count = 1, Quality = 0 },
                    Usage = ResourceUsage.Default
                };


                // Create Staging texture CPU-accessible
                /*textureDesc = new Texture2DDescription
                {
                    CpuAccessFlags = CpuAccessFlags.Read,
                    BindFlags = BindFlags.None,
                    Format = Format.B8G8R8A8_UNorm,
                    Width = width,
                    Height = height,
                    OptionFlags = ResourceOptionFlags.None,
                    MipLevels = 1,
                    ArraySize = 1,
                    SampleDescription = { Count = 1, Quality = 0 },
                    Usage = ResourceUsage.Staging
                };*/
                screenTexture = new Texture2D(device, textureDesc);



                textureDescriptionFinal = new Texture2DDescription
                {
                    CpuAccessFlags = CpuAccessFlags.None,
                    BindFlags = BindFlags.ShaderResource | BindFlags.RenderTarget,
                    Format = Format.B8G8R8X8_UNorm,
                    Width = width,
                    Height = height,
                    OptionFlags = ResourceOptionFlags.GenerateMipMaps,
                    MipLevels = 1,
                    ArraySize = 1,
                    SampleDescription = { Count = 1, Quality = 0 },
                    Usage = ResourceUsage.Default
                };




                wid = textureDescriptionFinal.Width / numcols;
                hgt = textureDescriptionFinal.Height / numrows;

                /*this.textureDescriptionFinalFrac = new Texture2DDescription
                {
                    CpuAccessFlags = CpuAccessFlags.None,
                    BindFlags = BindFlags.ShaderResource | BindFlags.RenderTarget,
                    Format = Format.B8G8R8A8UNorm,
                    Width = wid,
                    Height = hgt,
                    OptionFlags = ResourceOptionFlags.GenerateMipMaps,
                    MipLevels = 1,
                    ArraySize = 1,
                    SampleDescription = { Count = 1, Quality = 0 },
                    Usage = ResourceUsage.Default
                };*/

                this.textureDescriptionFinalFrac = new Texture2DDescription
                {
                    CpuAccessFlags = CpuAccessFlags.Read,
                    BindFlags = BindFlags.None,//BindFlags.None, //| BindFlags.RenderTarget
                    Format = SharpDX.DXGI.Format.B8G8R8X8_UNorm,
                    Width = wid,
                    Height = hgt,
                    OptionFlags = ResourceOptionFlags.None,
                    MipLevels = 1,
                    ArraySize = 1,
                    SampleDescription = { Count = 1, Quality = 0 },
                    Usage = ResourceUsage.Staging
                };










                piece = new Bitmap(wid, hgt);
                gr = Graphics.FromImage(piece);
                destrect = new System.Drawing.Rectangle(0, 0, wid, hgt);

                strider = wid * 4;

                for (int i = 0; i < arrayOfImage.Length; i++)
                {
                    arrayOfImage[i] = new int[wid * hgt * 4];
                }

                for (int i = 0; i < arrayOfBytes.Length; i++)
                {
                    arrayOfBytes[i] = new byte[wid * hgt * 4];
                }


                piece = new System.Drawing.Bitmap(wid, hgt);
                destrect = new System.Drawing.Rectangle(0, 0, wid, hgt);

                //int numrows = textureDescriptionFinal.Height / hgt;
                //int numcols = textureDescriptionFinal.Width / wid;
                sourcerect = new System.Drawing.Rectangle(0, 0, wid, hgt);


                for (int tex2D = 0; tex2D < 10 * 10; tex2D++)
                {
                    arrayOfTexture2DFrac[tex2D] = new Texture2D(device, textureDescriptionFinalFrac);
                }


                setinitvarsunsafetest();

            }
            catch (SharpDXException ex)
            {
                Console.WriteLine(ex.ToString());
                return;
            }

            //texture2D = new Texture2D(device, textureDescription);
            /*texture2DFinal = new Texture2D(device, textureDescriptionFinal);

            resourceViewDescription = new ShaderResourceViewDescription
            {
                Format = texture2DFinal.Description.Format,
                Dimension = SharpDX.Direct3D.ShaderResourceViewDimension.Texture2D,
                Texture2D = new ShaderResourceViewDescription.Texture2DResource
                {
                    MipLevels = -1,
                    MostDetailedMip = 0
                }
            };

            bitmap = new System.Drawing.Bitmap(width, height, PixelFormat.Format32bppArgb);
            System.Drawing.Rectangle boundsRect = new System.Drawing.Rectangle(0, 0, width, height);
            var bmpData = bitmap.LockBits(boundsRect, ImageLockMode.ReadOnly, bitmap.PixelFormat);
            bytesTotal = Math.Abs(bmpData.Stride) * bitmap.Height;
            bitmap.UnlockBits(bmpData);
            textureByteArray = new byte[bytesTotal];*/


            /*arrayOfPixData = new DTerrainHeightMap.DHeightMapType[bytesTotal];// width * height];
            for (int i = 0; i < arrayOfPixData.Length; i++)
            {
                arrayOfPixData[i] = new DTerrainHeightMap.DHeightMapType();

            }*/


            bitmap = new System.Drawing.Bitmap(width, height, PixelFormat.Format32bppArgb);
            var boundsRect = new System.Drawing.Rectangle(0, 0, width, height);
            var bmpData = bitmap.LockBits(boundsRect, System.Drawing.Imaging.ImageLockMode.ReadOnly, bitmap.PixelFormat);
            bytesTotal = Math.Abs(bmpData.Stride) * bitmap.Height;
            bitmap.UnlockBits(bmpData);
            textureByteArray = new byte[bytesTotal];

            boundsRect = new System.Drawing.Rectangle(0, 0, width, height);



            /*try
            {

            }
            catch (SharpDXException ex)
            {
                Console.WriteLine(ex.ToString());
                return;
            }*/
        }
        void writeTo(string test)
        {
            Console.WriteLine(test);
        }

        bool hasAcquiredFrame = false;

        //[STAThread]
        public sccssharpdxscreenframe ScreenCapture(int timeOut)
        {
            hasAcquiredFrame = false;
            try
            {
                if (!acquireFrame(timeOut))
                {
                    hasAcquiredFrame = false;
                    //return frameCaptureData;
                }
                else
                {
                    //releaseFrame();
                    hasAcquiredFrame = true;
                }

                if (!copyResource())
                {
                    //Console.WriteLine("has NOT copyResource");
                    hasAcquiredFrame = false;
                }
            }
            catch //(SharpDXException ex)
            {
                //Console.WriteLine(ex.ToString());
            }
            finally
            {
                if (hasAcquiredFrame)
                {
                    releaseFrame();
                }
               
                incrementgccollecttest();
            }


          




            return frameCaptureData;
        }

        bool acquireFrame(int timeOut)
        {
            screenResource = null;
            try
            {
                SharpDX.Result result = outputDuplication.TryAcquireNextFrame(timeOut, out duplicateFrameInformation, out screenResource);
            }
            catch (SharpDXException ex)
            {

            }

            if (screenResource != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        Bitmap lastBitmap;

        public void Disposer()
        {

            device.Dispose();
            output1.Dispose();
            texture2D.Dispose();
            outputDuplication.Dispose();
            //textureDescription.Dispose();
            bitmap.Dispose();
            bitmapPlayerRect.Dispose();
            screenResource.Dispose();
            //frameCaptureData.Dispose();
            GC.Collect();
        }




        IntPtr ptrIntInit;
        //UnmanagedMemoryStream unmanagedMemoryStreamPlayerRect = new UnmanagedMemoryStream(0, 0,0);


        /*void SetColor(System.Drawing.Color color)
        {
            current[0] = color.R;
            current[1] = color.G;
            current[2] = color.B;
        }*/

        Texture2D lastTexture2DFinal;
        Texture2D smallerTexture;
        ShaderResourceView smallerTextureView;
        Stopwatch SystemTickPerformance = new Stopwatch();



        public static int numcols = 10;// image.Width / wid;
        public static int numrows = 10;// image.Height / hgt;
        public static int totalDimension = numcols * numrows;

        int[][] arrayOfImage = new int[totalDimension][];
        byte[][] arrayOfBytes = new byte[totalDimension][];


        byte[] arrayOfBytesTwo;// = new byte[];

        public static int wid = 0;
        public static int hgt = 0;

        int strider = 0;
        int imageCounter = 0;

        System.Drawing.Bitmap image;

        //https://stackoverflow.com/questions/15975972/copy-data-from-from-intptr-to-intptr
        [DllImport("kernel32.dll", EntryPoint = "CopyMemory", SetLastError = false)]
        public static extern void CopyMemory(IntPtr dest, IntPtr src, uint count);

        int looponce = 0;
        int index = 0;
        IntPtr interptr0;

        FileInfo fileInfo;
        string somebitmappath;
        DataBox dataBox1;
        int memoryBitmapStride;
        int columns;
        int rows;
        IntPtr interptr1;
        IntPtr destPtr;



        System.Drawing.Rectangle boundsRect;
        BitmapData mapDest;
        IntPtr sourcePtr;

        bool copyResource()
        {
            try
            {

                using (var screenTexture2DD = screenResource.QueryInterface<Texture2D>())
                {
                    /*var textureDescription = new Texture2DDescription
                    {
                        CpuAccessFlags = CpuAccessFlags.None,
                        BindFlags = BindFlags.ShaderResource | BindFlags.RenderTarget,
                        Format = Format.B8G8R8X8_UNorm,
                        Width = width,
                        Height = height,
                        OptionFlags = ResourceOptionFlags.GenerateMipMaps,
                        MipLevels = 1,
                        ArraySize = 1,
                        SampleDescription = { Count = 1, Quality = 0 },
                        Usage = ResourceUsage.Default
                    };*/
                    var textureDescription = new Texture2DDescription
                    {
                        CpuAccessFlags = CpuAccessFlags.Read,
                        BindFlags = BindFlags.None,
                        Format = Format.B8G8R8A8_UNorm,
                        Width = width,
                        Height = height,
                        OptionFlags = ResourceOptionFlags.None,
                        MipLevels = 1,
                        ArraySize = 1,
                        SampleDescription = { Count = 1, Quality = 0 },
                        Usage = ResourceUsage.Staging
                    };

                    texture2D = new Texture2D(device, textureDescription);


                    device.ImmediateContext.CopyResource(screenTexture2DD, texture2D);
                }



                dataBox1 = device.ImmediateContext.MapSubresource(texture2D, 0, MapMode.Read, SharpDX.Direct3D11.MapFlags.None);

                boundsRect = new System.Drawing.Rectangle(0, 0, width, height);
                mapDest = bitmap.LockBits(boundsRect, System.Drawing.Imaging.ImageLockMode.WriteOnly, bitmap.PixelFormat);

                if (boundsRect.IsEmpty)
                {
                    Console.WriteLine("test");
                }
                sourcePtr = dataBox1.DataPointer;

                destPtr = mapDest.Scan0;

                for (int y = 0; y < height; y++)
                {
                    // Copy a single line 
                    Utilities.CopyMemory(destPtr, sourcePtr, width * 4);

                    // Advance pointers
                    sourcePtr = IntPtr.Add(sourcePtr, dataBox1.RowPitch);
                    destPtr = IntPtr.Add(destPtr, mapDest.Stride);
                }

                // Release source and dest locks
                bitmap.UnlockBits(mapDest);
                device.ImmediateContext.UnmapSubresource(texture2D, 0);

                if (somebitmapcounter >= somebitmapcountermax)
                {
                    somebitmapcounter = 0;
                }

                // Save the output
                bitmap.Save(path + "" + somebitmapcounter + "" + outputFileName); //
                somebitmapcounter++;

                //or device.ImmediateContext.UnmapSubresource(texture2DFinal, 0);
                //bitmap.Save(path + outputFileName);
                DeleteObject(sourcePtr);
                DeleteObject(destPtr);

                fileInfo.Refresh();

                if (lasttexture2d != null)
                {
                    lasttexture2d.Dispose();
                    lasttexture2d = null;
                }
                lasttexture2d = texture2D;

                if (mapDest != null)
                {
                    mapDest = null;
                }

                if (screenResource != null)
                {
                    screenResource.Dispose();
                    screenResource = null;
                }






                //IntPtr interptr0 = dataBox0.DataPointer;

                // It can happen that the stride on the GPU is bigger then the stride on the bitmap in main memory (width * 4)
                /*if (dataBox0.RowPitch == memoryBitmapStride)
                {
                    // Stride is the same
                    Marshal.Copy(interptr, textureByteArray, 0, bytesTotal);
                }
                else
                {
                    // Stride not the same - copy line by line
                    for (int y = 0; y < height; y++)
                    {
                        Marshal.Copy(interptr + y * dataBox.RowPitch, textureByteArray, y * memoryBitmapStride, memoryBitmapStride);
                    }
                }





                //TO READD when in need of copying a single full size screen capture into a single array of bytes and shrinked to the size of the ST7735
                //TO READD when in need of copying a single full size screen capture into a single array of bytes and shrinked to the size of the ST7735
                //TO READD when in need of copying a single full size screen capture into a single array of bytes and shrinked to the size of the ST7735
                var dataBox0 = device.ImmediateContext.MapSubresource(texture2DFinal, 0, SharpDX.Direct3D11.MapMode.Read, SharpDX.Direct3D11.MapFlags.None);

                //int memoryBitmapStride = textureDescription.Width * 4;

                //int columns = textureDescription.Width;
                //int rows = textureDescription.Height;
                IntPtr interptr0 = dataBox0.DataPointer;

                // It can happen that the stride on the GPU is bigger then the stride on the bitmap in main memory (width * 4)
                if (dataBox0.RowPitch == memoryBitmapStride)
                {
                    // Stride is the same
                    Marshal.Copy(interptr, textureByteArray, 0, bytesTotal);
                }
                else
                {
                    // Stride not the same - copy line by line
                    for (int y = 0; y < height; y++)
                    {
                        Marshal.Copy(interptr + y * dataBox.RowPitch, textureByteArray, y * memoryBitmapStride, memoryBitmapStride);
                    }
                }
                var somebitmap = new System.Drawing.Bitmap(160, 128, 160 * 4, PixelFormat.Format32bppArgb, interptr0);
                DeleteObject(interptr0);
                device.ImmediateContext.UnmapSubresource(texture2DFinal, 0);
                //TO READD when in need of copying a single full size screen capture into a single array of bytes and shrinked to the size of the ST7735
                //TO READD when in need of copying a single full size screen capture into a single array of bytes and shrinked to the size of the ST7735
                //TO READD when in need of copying a single full size screen capture into a single array of bytes and shrinked to the size of the ST7735









                //outputDuplication.TryAcquireNextFrame(10000, out duplicateFrameInformation, out screenResource);
                //device.ImmediateContext.CopyResource(texture2D, screenTexture);

                // copy resource into memory that can be accessed by the CPU
                /*using (var screenTexture2D = screenResource.QueryInterface<Texture2D>())
                {
                   
                }*/

                //screenTexture = new Texture2D(device, textureDesc);
                //



                //screenTexture = new Texture2D(device, textureDesc);
                //var screenTexture2D = screenResource.QueryInterface<Texture2D>();
                //device.ImmediateContext.CopyResource(screenTexture2D, screenTexture);








                /*
                if (lastscreenTexture != null)
                {
                    lastscreenTexture.Dispose();
                    screenTexture.Dispose();
                }
                lastscreenTexture = screenTexture;*/


























                /*if (lastbitmap != null)
                {
                    lastbitmap.Dispose();
                    lastbitmap = null;
                }
                lastbitmap = bitmap;*/


                /*if (screenTexture2D != null)
                {
                    screenTexture2D.Dispose();
                    screenTexture2D = null;                   
                }*/



                /*
                //MessageBox((IntPtr)0, screenTexture2D.Description.BindFlags.ToString() + "", "Oculus Error", 0);
                using (var screenTexture2D = screenResource.QueryInterface<Texture2D>())
                {
                    var textureDescription = new Texture2DDescription
                    {
                        CpuAccessFlags = CpuAccessFlags.None,
                        BindFlags = BindFlags.ShaderResource | BindFlags.RenderTarget,
                        Format = Format.B8G8R8X8_UNorm,
                        Width = width,
                        Height = height,
                        OptionFlags = ResourceOptionFlags.GenerateMipMaps,
                        MipLevels = 1,
                        ArraySize = 1,
                        SampleDescription = { Count = 1, Quality = 0 },
                        Usage = ResourceUsage.Default
                    };
                    texture2D = new Texture2D(device, textureDescription);

                    

                    device.ImmediateContext.CopyResource(screenTexture2D, texture2D);
                }*/
                //device.ImmediateContext.CopyResource(texture2D, texture2DFinal);
                //device.ImmediateContext.UnmapSubresource(screenTexture2D, 0);


                //TO READD WHEN IN NEED OF 1 bitmap and 1 array of bytes.
                /*var dataBox1 = device.ImmediateContext.MapSubresource(texture2D, 0, SharpDX.Direct3D11.MapMode.Read, SharpDX.Direct3D11.MapFlags.None);

                int memoryBitmapStride = textureDescription.Width * 4;

                int columns = textureDescription.Width;
                int rows = textureDescription.Height;
                IntPtr interptr1 = dataBox1.DataPointer;

                //DISCARDED
                // It can happen that the stride on the GPU is bigger then the stride on the bitmap in main memory (width * 4)
                if (dataBox1.RowPitch == memoryBitmapStride)
                {
                    // Stride is the same
                    Marshal.Copy(interptr1, textureByteArray, 0, bytesTotal);
                }
                else
                {
                    // Stride not the same - copy line by line
                    for (int y = 0; y < height; y++)
                    {
                        Marshal.Copy(interptr1 + y * dataBox1.RowPitch, textureByteArray, y * memoryBitmapStride, memoryBitmapStride);
                    }
                }
                //DISCARDED

                var somebitmap = new System.Drawing.Bitmap(160, 128, 160 * 4, PixelFormat.Format32bppArgb, interptr1);
                device.ImmediateContext.UnmapSubresource(texture2D, 0);
                DeleteObject(interptr1);*/
                //TO READD WHEN IN NEED OF 1 bitmap and 1 array of bytes.






                //temp discarded. tests for vertex linked to pixels for heightmaps 3d maps inside of vr. not working yet
                //temp discarded. tests for vertex linked to pixels for heightmaps 3d maps inside of vr. not working yet
                //temp discarded. tests for vertex linked to pixels for heightmaps 3d maps inside of vr. not working yet 
                /*
                index = 0;
                for (var j = 0; j < rows - 1; j++)
                {
                    for (var i = 0; i < columns - 1; i++)
                    {
                        var bytePoser = ((j * columns) + i);

                        //HeightMapSobel.Add(new DHeightMapType()
                        //{
                        //    x = i,
                        //    y = SCUpdate.desktopDupe.textureByteArray[bytePoser],// image.GetPixel(i, j).R,
                        //    z = j
                        //});

                        arrayOfPixData[bytePoser].x = i;
                        arrayOfPixData[bytePoser].y = textureByteArray[bytePoser];
                        arrayOfPixData[bytePoser].z = j;

                        int indexBottomLeft1 = ((rows * j) + i);          // Bottom left.
                        int indexBottomRight2 = ((rows * j) + (i + 1));      // Bottom right.
                        int indexUpperLeft3 = ((rows * (j + 1)) + i);      // Upper left.
                        int indexUpperRight4 = ((rows * (j + 1)) + (i + 1));  // Upper right.

                        if (scgraphicssec.Terrain != null)
                        {
                            if (scgraphicssec.Terrain.vertices != null)
                            {
                                if (scgraphicssec.Terrain.vertices.Length > 0)
                                {

                                    if (index < scgraphicssec.Terrain.vertices.Length)
                                    {
                                        scgraphicssec.Terrain.vertices[index].position = new Vector3(arrayOfPixData[indexUpperLeft3].x, arrayOfPixData[indexUpperLeft3].y, arrayOfPixData[indexUpperLeft3].z);
                                        scgraphicssec.Terrain.vertices[index].position = new Vector3(arrayOfPixData[indexUpperRight4].x, arrayOfPixData[indexUpperRight4].y, arrayOfPixData[indexUpperRight4].z);
                                        scgraphicssec.Terrain.vertices[index].position = new Vector3(arrayOfPixData[indexUpperRight4].x, arrayOfPixData[indexUpperRight4].y, arrayOfPixData[indexUpperRight4].z);
                                        scgraphicssec.Terrain.vertices[index].position = new Vector3(arrayOfPixData[indexBottomLeft1].x, arrayOfPixData[indexBottomLeft1].y, arrayOfPixData[indexBottomLeft1].z);
                                        scgraphicssec.Terrain.vertices[index].position = new Vector3(arrayOfPixData[indexBottomLeft1].x, arrayOfPixData[indexBottomLeft1].y, arrayOfPixData[indexBottomLeft1].z);
                                        scgraphicssec.Terrain.vertices[index].position = new Vector3(arrayOfPixData[indexUpperLeft3].x, arrayOfPixData[indexUpperLeft3].y, arrayOfPixData[indexUpperLeft3].z);
                                        scgraphicssec.Terrain.vertices[index].position = new Vector3(arrayOfPixData[indexBottomLeft1].x, arrayOfPixData[indexBottomLeft1].y, arrayOfPixData[indexBottomLeft1].z);
                                        scgraphicssec.Terrain.vertices[index].position = new Vector3(arrayOfPixData[indexUpperRight4].x, arrayOfPixData[indexUpperRight4].y, arrayOfPixData[indexUpperRight4].z);
                                        scgraphicssec.Terrain.vertices[index].position = new Vector3(arrayOfPixData[indexUpperRight4].x, arrayOfPixData[indexUpperRight4].y, arrayOfPixData[indexUpperRight4].z);
                                        scgraphicssec.Terrain.vertices[index].position = new Vector3(arrayOfPixData[indexBottomRight2].x, arrayOfPixData[indexBottomRight2].y, arrayOfPixData[indexBottomRight2].z);
                                        scgraphicssec.Terrain.vertices[index].position = new Vector3(arrayOfPixData[indexBottomRight2].x, arrayOfPixData[indexBottomRight2].y, arrayOfPixData[indexBottomRight2].z);
                                        scgraphicssec.Terrain.vertices[index].position = new Vector3(arrayOfPixData[indexBottomLeft1].x, arrayOfPixData[indexBottomLeft1].y, arrayOfPixData[indexBottomLeft1].z);

                                    }

                                    index++;
                                    
                                }
                            }
                        }
                    }
                }*/
                //temp discarded. tests for vertex linked to pixels for heightmaps 3d maps inside of vr. not working yet
                //temp discarded. tests for vertex linked to pixels for heightmaps 3d maps inside of vr. not working yet
                //temp discarded. tests for vertex linked to pixels for heightmaps 3d maps inside of vr. not working yet 








                //TESTS FAILURE WITH UNSAFE CODE. I THINK IT FAILS BUT I ALREADY SUCCEEDED IN THE PAST. JUST GOTTA FIND MY BACKUP. STARTING SOMETHING FROM SCRATCH THATS ALREADY HARD
                //TO DO AND PUTTING THE PROJECT ON THE SHELF SOMEWHERE AND LOSING TRACK OF IT AND REDOING THE WHOLE EXERCISE IS A PAIN WHEN YOU'RE NOT DEALING WITH UNSAFE CODE ALMOST EVER,
                //IT'S EASY TO FORGET WHEN YOU'VE DONE A PILE OF OTHER PROGRAMS AS PRACTICE AFTER THAT. BUT WHILE YOU ALREADY DONE IT IN THE PAST ALTHOUGH REFRESHING OUR MEMORY ONCE IN A WHILE
                //ON CODE WE ALREADY DEVELOPPED IN THE PAST AND UNDERSTOOD IS ALWAYS A GOOD THING TO DO, BUT WHEN IN A RUSH, DEALING WITH THINGS WE'VE ALREADY DONE IS A PAIN. LEAVING THIS HERE TO WORK ON IT LATER.
                /*if (scgraphicssec.Terrain != null)
                {
                    if (scgraphicssec.Terrain.vertices.Length > 0)
                    {
                        Console.WriteLine(scgraphicssec.Terrain.vertices.Length);
                    }
                }*/

                /*byte* ptr = (byte*)interptr.ToPointer();

                int pixelSize = 3;
                int nWidth = textureDescriptionFinal.Width * pixelSize;
                int nHeight = textureDescriptionFinal.Height;
                int counterY = 0;
                int counterX = 0;

                int nWidthDIV = (textureDescriptionFinal.Width * pixelSize) / numcols;
                int nWidthDIVTWO = (textureDescriptionFinal.Width * 4) / numcols;
                int nHeightDIV = textureDescriptionFinal.Height / numrows;
                int mainArrayIndex = 0;

                int ycount = 0;
                int xcount = 0;*/

                /* byte* ptr = (byte*)interptr.ToPointer();

                 int pixelSize = 3;
                 int nWidth = textureDescriptionFinal.Width * pixelSize;
                 int nHeight = textureDescriptionFinal.Height;
                 int counterY = 0;
                 int counterX = 0;
                 int mainArrayIndex = 0;

                 int someIndexX = 0;
                 int someIndexY = 0;

                 int nWidthDIV = (textureDescriptionFinal.Width * pixelSize) / numcols;
                 int nWidthDIVTWO = (textureDescriptionFinal.Width * 4) / numcols;
                 int nHeightDIV = textureDescriptionFinal.Height / numrows;

                 for (int y = 0; y < nHeight; y++)
                 {
                     for (int x = 0; x < nWidth; x++)
                     {
                         if (x % pixelSize == 0 || x == 0)
                         {                       
                             var bytePoser = ((y * nWidth) + x);
                             mainArrayIndex = (counterY * numcols) + counterX;

                             var test0 = ptr[bytePoser + 0];
                             var test1 = ptr[bytePoser + 1];
                             var test2 = ptr[bytePoser + 2];

                             var indexOfFracturedImageBytes = ((someIndexY) * nWidthDIV) + someIndexX;

                             try
                             {
                                 arrayOfBytes[mainArrayIndex][indexOfFracturedImageBytes + 0] = test0; //b
                                 arrayOfBytes[mainArrayIndex][indexOfFracturedImageBytes + 1] = test1; //g
                                 arrayOfBytes[mainArrayIndex][indexOfFracturedImageBytes + 2] = test2; //r
                                 arrayOfBytes[mainArrayIndex][indexOfFracturedImageBytes + 3] = 1;     //a
                             }
                             catch (Exception ex)
                             {
                                 MainWindow.MessageBox((IntPtr)0, "index: " + mainArrayIndex + "  " + indexOfFracturedImageBytes + "  " + ex.ToString(), "sccs message", 0);
                             }

                             ptr++;
                         }



                         /*if (someIndexY % nHeightDIV == 0 && someIndexY != 0)
                         {
                             someIndexY = 0;
                         }

                         if (x % nWidthDIV == 0 && x != 0 && counterX < 9)
                         {
                             counterX++;
                         }

                         someIndexX++;
                         if (someIndexX % nWidthDIV == 0 && someIndexX != 0)
                         {
                             someIndexX = 0;
                         }
                     }

                     if (y % nHeightDIV == 0 && y != 0 && counterY < 9)
                     {
                         someIndexY = 0;
                         counterY++;
                         counterX = 0;
                     }
                     someIndexY++;
                 }*/

                //TESTS FAILURE WITH UNSAFE CODE. I THINK IT FAILS BUT I ALREADY SUCCEEDED IN THE PAST. JUST GOTTA FIND MY BACKUP. STARTING SOMETHING FROM SCRATCH THATS ALREADY HARD
                //TO DO AND PUTTING THE PROJECT ON THE SHELF SOMEWHERE AND LOSING TRACK OF IT AND REDOING THE WHOLE EXERCISE IS A PAIN WHEN YOU'RE NOT DEALING WITH UNSAFE CODE ALMOST EVER,
                //IT'S EASY TO FORGET WHEN YOU'VE DONE A PILE OF OTHER PROGRAMS AS PRACTICE AFTER THAT. BUT WHILE YOU ALREADY DONE IT IN THE PAST ALTHOUGH REFRESHING OUR MEMORY ONCE IN A WHILE
                //ON CODE WE ALREADY DEVELOPPED IN THE PAST AND UNDERSTOOD IS ALWAYS A GOOD THING TO DO, BUT WHEN IN A RUSH, DEALING WITH THINGS WE'VE ALREADY DONE IS A PAIN. LEAVING THIS HERE TO WORK ON IT LATER.











                //TO READD WHEN IN NEED OF DIVIDING A FULL SIZE DESKTOP SCREEN INTO MULTIPLE ARRAYS OF BYTES 
                //TO READD WHEN IN NEED OF DIVIDING A FULL SIZE DESKTOP SCREEN INTO MULTIPLE ARRAYS OF BYTES 
                //TO READD WHEN IN NEED OF DIVIDING A FULL SIZE DESKTOP SCREEN INTO MULTIPLE ARRAYS OF BYTES 
                /*sourcerect = new System.Drawing.Rectangle(0, 0, textureDescriptionFinal.Width, textureDescriptionFinal.Height);

                var region = new ResourceRegion(0, 0, 0, wid, hgt, 1);

                region = new ResourceRegion(sourcerect.X, sourcerect.Y, 0, textureDescriptionFinal.Width, textureDescriptionFinal.Height, 1);

                for (int row = 0; row < numrows; row++)
                {
                    sourcerect.X = 0;

                    for (int col = 0; col < numcols; col++)
                    {
                        var mainArrayIndex = (row * numcols) + col;

                        region.Left = sourcerect.X;

                        region.Top = sourcerect.Y;

                        device.ImmediateContext.CopySubresourceRegion(texture2DFinal, 0, region, arrayOfTexture2DFrac[mainArrayIndex], 0);
                        //ShaderResourceViewArray[mainArrayIndex] = new ShaderResourceView(device, texture2DFinal, resourceViewDescription);

                        //device.ImmediateContext.GenerateMips(ShaderResourceViewArray[mainArrayIndex]);

                        sourcerect.X += wid;
                    }
                    sourcerect.Y += hgt;
                }

                sourcerect.X = 0;// = new System.Drawing.Rectangle(0, 0, textureDescriptionFinal.Width, textureDescriptionFinal.Height);
                sourcerect.Y = 0;//
                region.Left = sourcerect.X;
                region.Top = sourcerect.Y;
                //region = new ResourceRegion(0, 0, 0, wid, hgt, 1);
                //region = new ResourceRegion(sourcerect.X, sourcerect.Y, 0, textureDescriptionFinal.Width, textureDescriptionFinal.Height, 1);


                for (int row = 0; row < numrows; row++)
                {
                    sourcerect.X = 0;

                    for (int col = 0; col < numcols; col++)
                    {
                        var mainArrayIndex = (row * numcols) + col;

                        region.Left = sourcerect.X;

                        region.Top = sourcerect.Y;

                        //device.ImmediateContext.CopySubresourceRegion(texture2DFinal, 0, region, arrayOfTexture2DFrac[mainArrayIndex], 0);
                        //ShaderResourceViewArray[mainArrayIndex] = new ShaderResourceView(device, texture2DFinal, resourceViewDescription);
                        //device.ImmediateContext.GenerateMips(ShaderResourceViewArray[mainArrayIndex]);

                        //for (int tex2D = 0; tex2D < 10 * 10; tex2D++)
                        //{
                        //    arrayOfTexture2DFrac[tex2D] = new Texture2D(device, textureDescriptionFinalFrac);
                        //}

                        var dataBox = device.ImmediateContext.MapSubresource(arrayOfTexture2DFrac[mainArrayIndex], 0, SharpDX.Direct3D11.MapMode.Read, SharpDX.Direct3D11.MapFlags.None);

                        int memoryBitmapStride0 = wid * 4;// textureDescription.Width * 4;

                        int columns0 = wid;// textureDescription.Width;
                        int rows0 = hgt;// textureDescription.Height;
                        IntPtr interptr = dataBox.DataPointer;

                        // It can happen that the stride on the GPU is bigger then the stride on the bitmap in main memory (width * 4)
                        if (dataBox.RowPitch == memoryBitmapStride0)
                        {
                            // Stride is the same
                            Marshal.Copy(interptr, arrayOfBytes[mainArrayIndex], 0, wid * hgt * 4);
                        }
                        else
                        {
                            // Stride not the same - copy line by line
                            for (int y = 0; y < height; y++)
                            {
                                Marshal.Copy(interptr + y * dataBox.RowPitch, arrayOfBytes[mainArrayIndex], y * memoryBitmapStride0, memoryBitmapStride0);
                            }
                        }

                        device.ImmediateContext.UnmapSubresource(arrayOfTexture2DFrac[mainArrayIndex], 0);

                        DeleteObject(interptr);

                        sourcerect.X += wid;
                    }
                    sourcerect.Y += hgt;
                }*/
                //TO READD WHEN IN NEED OF DIVIDING A FULL SIZE DESKTOP SCREEN INTO MULTIPLE ARRAYS OF BYTES 
                //TO READD WHEN IN NEED OF DIVIDING A FULL SIZE DESKTOP SCREEN INTO MULTIPLE ARRAYS OF BYTES 
                //TO READD WHEN IN NEED OF DIVIDING A FULL SIZE DESKTOP SCREEN INTO MULTIPLE ARRAYS OF BYTES 












                //TO READD when in need of copying a single full size screen capture into a single array of bytes and shrinked to the size of the ST7735
                //TO READD when in need of copying a single full size screen capture into a single array of bytes and shrinked to the size of the ST7735
                //TO READD when in need of copying a single full size screen capture into a single array of bytes and shrinked to the size of the ST7735
                /*var dataBox0 = device.ImmediateContext.MapSubresource(texture2DFinal, 0, SharpDX.Direct3D11.MapMode.Read, SharpDX.Direct3D11.MapFlags.None);

                //int memoryBitmapStride = textureDescription.Width * 4;

                //int columns = textureDescription.Width;
                //int rows = textureDescription.Height;
                IntPtr interptr0 = dataBox0.DataPointer;

                // It can happen that the stride on the GPU is bigger then the stride on the bitmap in main memory (width * 4)
                /*if (dataBox0.RowPitch == memoryBitmapStride)
                {
                    // Stride is the same
                    Marshal.Copy(interptr, textureByteArray, 0, bytesTotal);
                }
                else
                {
                    // Stride not the same - copy line by line
                    for (int y = 0; y < height; y++)
                    {
                        Marshal.Copy(interptr + y * dataBox.RowPitch, textureByteArray, y * memoryBitmapStride, memoryBitmapStride);
                    }
                }
                //var somebitmap = new System.Drawing.Bitmap(160, 128, 160 * 4, PixelFormat.Format32bppArgb, interptr0);
                DeleteObject(interptr0);
                device.ImmediateContext.UnmapSubresource(texture2DFinal, 0);*/
                //TO READD when in need of copying a single full size screen capture into a single array of bytes and shrinked to the size of the ST7735
                //TO READD when in need of copying a single full size screen capture into a single array of bytes and shrinked to the size of the ST7735
                //TO READD when in need of copying a single full size screen capture into a single array of bytes and shrinked to the size of the ST7735








                /*
                shaderRes = new ShaderResourceView(device, texture2DFinal, resourceViewDescription);
                device.ImmediateContext.GenerateMips(shaderRes);

                if (shaderRes != null)
                {
                    frameCaptureData.ShaderResource = shaderRes;
                    frameCaptureData.texture2DFinal = texture2D;


                    //TO READD
                    //TO READD
                    //TO READD
                    //frameCaptureData.screencapturearrayofbytes = arrayOfBytes;
                    //TO READD
                    //TO READD
                    //TO READD
                    //TO READD WHEN IN NEED OF SOME BITMAPS FOR ST7735
                    //frameCaptureData.somebitmapforarduino = somebitmap;
                    //TO READD WHEN IN NEED OF SOME BITMAPS FOR ST7735


                    //frameCaptureData.frameByteArray = textureByteArray;


                }

                if (lastShaderResourceView != null)
                {
                    lastShaderResourceView.Dispose();
                }

                lastShaderResourceView = shaderRes;*/














                //TO READD WHEN IN NEED OF SOME BITMAPS FOR ST7735
                /*if (lastBitmap != null)
                {
                    lastBitmap.Dispose();
                }
                lastBitmap = somebitmap;*/
                //TO READD WHEN IN NEED OF SOME BITMAPS FOR ST7735
















                //arrayOfTexture2DFrac
                //Copy(device, texture2DFinal, arrayOfTexture2DFrac);
                //device.ImmediateContext.CopySubresourceRegion(source, 0, region, target, 0);

                //SystemTickPerformance.Stop();
                //SystemTickPerformance.Reset();
                //SystemTickPerformance.Start();

                //image = new System.Drawing.Bitmap(columns, rows, memoryBitmapStride, PixelFormat.Format32bppArgb, interptr);

                //sourcerect = new System.Drawing.Rectangle(0, 0, wid, hgt);








                /*if (ShaderResourceViewArray != null)
                {
                    frameCaptureData.ShaderResourceArray = ShaderResourceViewArray;
                }

                if (lastShaderResourceViewArray != null)
                {
                    for (int i = 0; i < lastShaderResourceViewArray.Length; i++)
                    {
                        if (lastShaderResourceViewArray[i] != null)
                        {
                            lastShaderResourceViewArray[i].Dispose();
                        }
                    }
                }

                lastShaderResourceViewArray = ShaderResourceViewArray;*/















                //sourcerect.X = 0;// = new System.Drawing.Rectangle(0, 0, textureDescriptionFinal.Width, textureDescriptionFinal.Height);
                //sourcerect.Y = 0;//
                //region.Left = sourcerect.X;
                //region.Top = sourcerect.Y;
                //sourcerect = new System.Drawing.Rectangle(0, 0, textureDescriptionFinal.Width, textureDescriptionFinal.Height);
                //region = new ResourceRegion(0, 0, 0, wid, hgt, 1);
                //region = new ResourceRegion(sourcerect.X, sourcerect.Y, 0, textureDescriptionFinal.Width, textureDescriptionFinal.Height, 1);






                /*
                
                sourcerect.X = 0;// = new System.Drawing.Rectangle(0, 0, textureDescriptionFinal.Width, textureDescriptionFinal.Height);
                sourcerect.Y = 0;//
                region.Left = sourcerect.X;
                region.Top = sourcerect.Y;
                int nWidth = wid;
                int nHeight = hgt;

                for (int row = 0; row < numrows; row++)
                {
                    sourcerect.X = 0;

                    for (int col = 0; col < numcols; col++)
                    {
                        var mainArrayIndex = (row * numcols) + col;

                        region.Left = sourcerect.X;

                        region.Top = sourcerect.Y;

                        for (int y = 0; y < nHeight; y++)
                        {
                            for (int x = 0; x < nWidth; x++)
                            {
                                var bytePoser = (((y) * nWidth) + (x)) * 4;

                                SharpDX.Color color = new SharpDX.Color(arrayOfBytes[mainArrayIndex][bytePoser + 0], arrayOfBytes[mainArrayIndex][bytePoser + 1], arrayOfBytes[mainArrayIndex][bytePoser + 2], arrayOfBytes[mainArrayIndex][bytePoser + 3]);

                                //System.Drawing.Color color = System.Drawing.Color.Black;

                                //if (color.B <= 10 && color.G <= 10 && color.R <= 10 && color.A == 255 || color.B <= 10 && color.G <= 10 && color.R <= 10 && color.A >= 235
                                //    || color.B <= 10 && color.G <= 10 && color.R <= 10 && color.A >= 10)
                                if (color.B == 255 && color.G == 255 && color.R == 255 && color.A == 255)
                                {
                                    //float test = 0.5f;
                                    //MainWindow.MessageBox((IntPtr)0, "b: " + arrayOfBytes[mainArrayIndex][bytePoser + 0] + " g: " + arrayOfBytes[mainArrayIndex][bytePoser + 1] + " r: " + arrayOfBytes[mainArrayIndex]
                                    //[bytePoser + 2] + " a: " + arrayOfBytes[mainArrayIndex][bytePoser + 3], "sc core systems", 0);
                                    arrayOfBytes[mainArrayIndex][bytePoser + 0] = 0;
                                    arrayOfBytes[mainArrayIndex][bytePoser + 1] = 0;
                                    arrayOfBytes[mainArrayIndex][bytePoser + 2] = 0;
                                    arrayOfBytes[mainArrayIndex][bytePoser + 3] = 0;
                                    //a = 0;
                                }
                                sourcerect

                                /*if (color.B == 66 && color.G == 66 && color.R == 66 && color.A == 255)
                                {
                                    //MainWindow.MessageBox((IntPtr)0, "b: " + arrayOfBytes[mainArrayIndex][bytePoser + 0] + " g: " + arrayOfBytes[mainArrayIndex][bytePoser + 1] + " r: " + arrayOfBytes[mainArrayIndex]
                                    arrayOfBytes[mainArrayIndex][bytePoser + 0] = 255;
                                    arrayOfBytes[mainArrayIndex][bytePoser + 1] = 255;
                                    arrayOfBytes[mainArrayIndex][bytePoser + 2] = 255;
                                    arrayOfBytes[mainArrayIndex][bytePoser + 3] = 0;
                                    //a = 0;
                                }
                            }
                        }
                        
                        sourcerect.X += wid;
                    }
                    sourcerect.Y += hgt;
                }
                



                sourcerect = new System.Drawing.Rectangle(0, 0, textureDescriptionFinal.Width, textureDescriptionFinal.Height);

                region = new ResourceRegion(0, 0, 0, wid, hgt, 1);

                region = new ResourceRegion(sourcerect.X, sourcerect.Y, 0, textureDescriptionFinal.Width, textureDescriptionFinal.Height, 1);



                nWidth = wid;
                nHeight = hgt;*/


                /*for (int i = 0; i < textureByteArray.Length; i+= arrayOfBytes[0].Length)
                {
                    int length = arrayOfBytes[0].Length;
                    if (i + arrayOfBytes[0].Length >= textureByteArray.Length)
                    {
                        length = arrayOfBytes[0].Length - i;
                    }
                    Array.Copy(arrayOfBytes[0], 0, textureByteArray, i, length);
                }*/




                /*
                var dataBox0 = device.ImmediateContext.MapSubresource(texture2D, 0, SharpDX.Direct3D11.MapMode.Read, SharpDX.Direct3D11.MapFlags.None);

                int memoryBitmapStride1 = textureDescription.Width * 4;

                int columns1 = textureDescription.Width;
                int rows1 = textureDescription.Height;
                interptr0 = dataBox0.DataPointer;

                // It can happen that the stride on the GPU is bigger then the stride on the bitmap in main memory (width * 4)
                if (dataBox0.RowPitch == memoryBitmapStride1)
                {
                    // Stride is the same
                    Marshal.Copy(interptr0, textureByteArray, 0, bytesTotal);
                }
                else
                {
                    // Stride not the same - copy line by line
                    for (int y = 0; y < height; y++)
                    {
                        Marshal.Copy(interptr0 + y * dataBox0.RowPitch, textureByteArray, y * memoryBitmapStride1, memoryBitmapStride1);
                    }
                }

                device.ImmediateContext.UnmapSubresource(texture2D, 0);


                DeleteObject(interptr0);*/




                //var bytePoser = (((y) * nWidth) + (x)) * 4;


                /*for (int y = 0; y < height; y++)
                {
                    for (int x = 0; x < width; x++)
                    {
                        //arrayOfBytes[mainArrayIndex][bytePoser + 3] = 0;
                        var bytePoser = ((((y) * width) + (x)) * 4);

                        SharpDX.Color color = new SharpDX.Color(textureByteArray[bytePoser + 0], textureByteArray[bytePoser + 1], textureByteArray[bytePoser + 2], textureByteArray[bytePoser + 3]);

                        if (color.B == 255 && color.G == 255 && color.R == 255 && color.A == 255)
                        {
                            //float test = 0.5f;
                            //MainWindow.MessageBox((IntPtr)0, "b: " + arrayOfBytes[mainArrayIndex][bytePoser + 0] + " g: " + arrayOfBytes[mainArrayIndex][bytePoser + 1] + " r: " + arrayOfBytes[mainArrayIndex]
                            //[bytePoser + 2] + " a: " + arrayOfBytes[mainArrayIndex][bytePoser + 3], "sc core systems", 0);
                            textureByteArray[bytePoser + 0] = 0;
                            textureByteArray[bytePoser + 1] = 0;
                            textureByteArray[bytePoser + 2] = 0;
                            textureByteArray[bytePoser + 3] = 0;
                            //a = 0;
                        }
                    }
                }*/





                /*
                for (int row = 0; row < numrows; row++)
                {
                    //sourcerect.X = 0;

                    for (int col = 0; col < numcols; col++)
                    {
                        var mainArrayIndex = (row * numcols) + col;

                        var someTest = (((sourcerect.Y) * 1920) + (sourcerect.X)) * 4;

                        //region.Left = sourcerect.X;
                        //region.Top = sourcerect.Y;

                        //var bytePoser = ((((0) * nWidth) + (0)) * 4);

                        //for (int y = 0; y < nHeight; y++)
                        //{
                        //    for (int x = 0; x < nWidth; x++)
                        //    {
                        //        //arrayOfBytes[mainArrayIndex][bytePoser + 3] = 0;
                                var bytePoser = ((((y) * nWidth) + (x)) * 4);
                        //
                        //        //textureByteArray[((mainArrayIndex) * (bytePoser + 0))] = arrayOfBytes[mainArrayIndex][bytePoser + 0];
                        //        //textureByteArray[((mainArrayIndex) * (bytePoser + 1))] = arrayOfBytes[mainArrayIndex][bytePoser + 1];
                        //        //textureByteArray[((mainArrayIndex) * (bytePoser + 2))] = arrayOfBytes[mainArrayIndex][bytePoser + 2];
                        //        //textureByteArray[((mainArrayIndex) * (bytePoser + 3))] = arrayOfBytes[mainArrayIndex][bytePoser + 3];                    
                        //    }
                        //}




                        /*var dataBox = device.ImmediateContext.MapSubresource(arrayOfTexture2DFrac[mainArrayIndex], 0, SharpDX.Direct3D11.MapMode.Read, SharpDX.Direct3D11.MapFlags.None);

                        int memoryBitmapStride0 = wid * 4;// textureDescription.Width * 4;

                        int columns0 = wid;// textureDescription.Width;
                        int rows0 = hgt;// textureDescription.Height;
                        IntPtr interptr = dataBox.DataPointer;

                        // It can happen that the stride on the GPU is bigger then the stride on the bitmap in main memory (width * 4)
                        if (dataBox.RowPitch == memoryBitmapStride0)
                        {
                            // Stride is the same
                            //Marshal.Copy(interptr, arrayOfBytes[mainArrayIndex], 0, wid * hgt * 4);
                            Array.Copy(arrayOfBytes[mainArrayIndex], 0, textureByteArray, someTest, wid * hgt * 4);
                        }
                        else
                        {
                            // Stride not the same - copy line by line
                            for (int y = 0; y < height; y++)
                            {
                                //Marshal.Copy(interptr + y * dataBox.RowPitch, arrayOfBytes[mainArrayIndex], y * memoryBitmapStride0, memoryBitmapStride0);
                            }
                        }

                        device.ImmediateContext.UnmapSubresource(arrayOfTexture2DFrac[mainArrayIndex], 0);


                        DeleteObject(interptr);*/

                //System.Buffer.BlockCopy(bufferPlayerShipRect, 0, currentByteArray, bytePoser, bufferPlayerShipRect.Length);
                //System.Buffer.BlockCopy(arrayOfBytes[mainArrayIndex], 0, textureByteArray, someTest, arrayOfBytes[mainArrayIndex].Length);

                //Array.Copy(arrayOfBytes[mainArrayIndex], 0, textureByteArray, someTest + 0, wid * hgt * 4);

                /*for (int y = 0; y < height; y++)
                {
                    Marshal.Copy(interptr + y * (), arrayOfBytes[mainArrayIndex], y * memoryBitmapStride0, memoryBitmapStride0);
                }*/

                /*for (int x = 0; x < nWidth; x++)
                {
                    //arrayOfBytes[mainArrayIndex][bytePoser + 3] = 0;
                    var bytePoser = ((nWidth) + x) * 4;
                    Array.Copy(arrayOfBytes[mainArrayIndex], 0, textureByteArray, someTest + bytePoser, wid * 4);
                }


                //Marshal.Copy(Marshal.UnsafeAddrOfPinnedArrayElement(arrayOfBytes[mainArrayIndex], 0), textureByteArray, 0, wid * hgt * 4);

                sourcerect.X += wid;
            }
            sourcerect.Y += hgt;
        }*/





                /*
                for (int row = 0; row < numrows; row++)
                {
                    sourcerect.X = 0;

                    for (int col = 0; col < numcols; col++)
                    {
                        var mainArrayIndex = (row * numcols) + col;

                        region.Left = sourcerect.X;

                        region.Top = sourcerect.Y;

                        var someTest = ((sourcerect.Y*4) * numcols) + (sourcerect.X*4);

                        /*for (int y = 0; y < nHeight; y++)
                        {
                            for (int x = 0; x < nWidth; x++)
                            {
                                //arrayOfBytes[mainArrayIndex][bytePoser + 3] = 0;
                                var bytePoser = (((y) * nWidth) + (x)) * 4;

                                //textureByteArray[((mainArrayIndex) * bytePoser) + 0] = arrayOfBytes[mainArrayIndex][bytePoser + 0];
                                //textureByteArray[((mainArrayIndex) * bytePoser) + 1] = arrayOfBytes[mainArrayIndex][bytePoser + 1];
                                //textureByteArray[((mainArrayIndex) * bytePoser) + 2] = arrayOfBytes[mainArrayIndex][bytePoser + 2];
                                //textureByteArray[((mainArrayIndex) * bytePoser) + 3] = arrayOfBytes[mainArrayIndex][bytePoser + 3];
                            }
                        }


                        for (int i = 0; i < textureByteArray.Length; i++)
                        {
                            textureByteArray[i] = arrayOfBytes[mainArrayIndex][i % arrayOfBytes.Length];
                        }



                        //Array.Copy(arrayOfBytes[mainArrayIndex], 0, textureByteArray, someTest, wid * hgt * 4);

                        //Marshal.Copy(Marshal.UnsafeAddrOfPinnedArrayElement(arrayOfBytes[mainArrayIndex], 0), textureByteArray, 0, wid * hgt * 4);

                        sourcerect.X += wid;
                    }
                    sourcerect.Y += hgt;
                }*/



                /*
                var ycount = 0;
                var xcount = 0;
                var xreset = 0;
                var yreset = 0;

                for (int y = 0; y < height; y++)
                {
                    if (yreset == hgt)
                    {
                        ycount++;
                        yreset = 0;
                    }

                    for (int x = 0; x < width; x++)
                    {
                        if (xreset == wid)
                        {
                            xcount++;
                            xreset = 0;
                        }

                        var mainArrayIndex = ((y * width) + x) * 4;

                        var secArrayIndex = (ycount * numcols) + xcount;

                        if (secArrayIndex < numcols * numrows)
                        {
                            textureByteArray[mainArrayIndex] = arrayOfBytes[secArrayIndex][mainArrayIndex % arrayOfBytes.Length];
                        }

                        /*for (int i = 0; i < textureByteArray.Length; i++)
                        {
                            textureByteArray[i] = arrayOfBytes[mainArrayIndex][i % arrayOfBytes.Length];
                        }

                        xreset++;
                    }
                    yreset++;
                }*/




















                /*var dataBox0 = device.ImmediateContext.MapSubresource(texture2D, 0, SharpDX.Direct3D11.MapMode.Read, SharpDX.Direct3D11.MapFlags.None);

                int memoryBitmapStride1 = textureDescription.Width * 4;

                int columns1 = textureDescription.Width;
                int rows1 = textureDescription.Height;
                IntPtr interptr0 = dataBox0.DataPointer;

                // It can happen that the stride on the GPU is bigger then the stride on the bitmap in main memory (width * 4)
                if (dataBox0.RowPitch == memoryBitmapStride1)
                {
                    // Stride is the same
                    Marshal.Copy(interptr0, textureByteArray, 0, bytesTotal);
                }
                else
                {
                    // Stride not the same - copy line by line
                    for (int y = 0; y < height; y++)
                    {
                        Marshal.Copy(interptr0 + y * dataBox0.RowPitch, textureByteArray, y * memoryBitmapStride1, memoryBitmapStride1);
                    }
                }

                device.ImmediateContext.UnmapSubresource(texture2D, 0);


                DeleteObject(interptr0);*/




                //int memoryBitmapStride = textureDescription.Width * 4;

                //int columns = textureDescription.Width;
                //int rows = textureDescription.Height;
                //IntPtr interptr = dataBox.DataPointer;

                //shaderRes = Ab3d.DirectX.TextureLoader.CreateShaderResourceView(device, textureByteArray, columns, rows, memoryBitmapStride, Format.B8G8R8A8UNorm, true);
















                /*
                 image = new System.Drawing.Bitmap(columns, rows, memoryBitmapStride, PixelFormat.Format32bppArgb, interptr);

                 using (Graphics gr = Graphics.FromImage(piece))
                 {               
                     for (int row = 0; row < numrows; row++)
                     {
                         sourcerect.X = 0;
                         for (int col = 0; col < numcols; col++)
                         {
                             //gr.DrawImage(image, destrect, sourcerect, GraphicsUnit.Pixel);

                             //piece.Save(@"C:\Users\steve\OneDrive\Desktop\screenRecord\" + row.ToString("00") + col.ToString("00") + ".png");
                             sourcerect.X += wid;
                         }
                         sourcerect.Y += hgt;
                     }
                 }
                 MessageBox((IntPtr)0, SystemTickPerformance.Elapsed.Milliseconds + "", "Oculus Error", 0);*/

                //SystemTickPerformance.Stop();
                //SystemTickPerformance.Reset();
                //SystemTickPerformance.Start();

                /*for (int i = 0; i < arrayOfBytes.Length; i++)
                {
                    if (arrayOfBytes[i] != null)
                    {
                        //var dataBoxer = device.ImmediateContext.MapSubresource(arrayOfTexture2DFrac[i], 0, SharpDX.Direct3D11.MapMode.Read, SharpDX.Direct3D11.MapFlags.None);
                        //memoryBitmapStride = textureDescription.Width * 4;
                        //columns = textureDescription.Width;
                        //rows = textureDescription.Height;
                        //var interptr2 = dataBoxer.DataPointer;

                        //device.ImmediateContext.UnmapSubresource(arrayOfTexture2DFrac[i], 0);

                        System.Drawing.Bitmap image = new System.Drawing.Bitmap(wid, hgt, strider, PixelFormat.Format32bppArgb, Marshal.UnsafeAddrOfPinnedArrayElement(arrayOfBytes[i], 0)); //arrayOfBytes[i]);// Marshal.UnsafeAddrOfPinnedArrayElement(arrayOfTexture2DFrac[i], 0));

                        string filePathVE = "desktop capture";
                        var exportedToFolderPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

                        filePathVE = exportedToFolderPath + "\\" + filePathVE;// @"LAYERS\PNG\";

                        if (!Directory.Exists(filePathVE))
                        {
                            Directory.CreateDirectory(filePathVE);
                        }

                        var fi = new FileInfo(filePathVE);
                        fi.Refresh();

                        image.Save(filePathVE + "\\" + imageCounter + ".jpg");
                        imageCounter++;
                    }
                }*/
                //MessageBox((IntPtr)0, SystemTickPerformance.Elapsed.Milliseconds + "", "Oculus Error", 0);

                /*if (arrayOfIntPTR.Count > 0)
                {
                    if (counterForPTR > 10)
                    {
                        for (int i = 0; i < (int)Math.Ceiling((double)arrayOfIntPTR.Count * howFast); i++)
                        {
                            DeleteObject(arrayOfIntPTR[i]);
                        }
                        counterForPTR = 0;
                    }
                }
                counterForPTR++;*/

                return true;
            }
            catch (Exception ex)
            {
                //Program.MessageBox((IntPtr)0, index + " " + ex.ToString(), "sccs error message", 0);
            }
            return false;
        }

        [DllImport("User32.dll", CharSet = CharSet.Unicode)]
        public static extern int MessageBox(IntPtr h, string m, string c, int type);

        ShaderResourceView shaderRes;
        ShaderResourceViewDescription resourceViewDescription;






        int somefinalGCCollectcounter = 0;
        int somegccollectframecounter = 0;
        int somegccollectframecountermax = 0;

        int somebitmapcounter = 0;
        int somebitmapcountermax = 10;

        bool releasedFrame = true;
        public bool releaseFrame()
        {
            //Console.WriteLine("releaseFrame is called.");
            //texture2D.Dispose(); // lags like fucking hell
            for (int i = 0; i < 2; i++)
            {

                try
                {
                    if (outputDuplication != null)
                    {
                        outputDuplication.ReleaseFrame();
                    }
                    else
                    {

                    }

                    releasedFrame = true;
                }
                catch //(SharpDXException ex)
                {
                    releasedFrame = false;
                    //Console.WriteLine(ex.ToString());

                }

                if (releasedFrame)
                {
                    break;
                }
            }
            if (releasedFrame)
            {
                return releasedFrame;
            }
            else
            {
                //sccsgraphics.sccsgraphicsupdate.somesccsscreencapture = new sccssharpdxscreencapture(0, 0, this.device); // not that good but let's leave it at that.
                //var somescreencap = new sccssharpdxscreencapture();

                //Program.somesccsscreencapture = somescreencap;

                //instance = null;
                //Instance = null;
                //Instance = new sccssharpdxscreencapture();
                //Dispose(true);
                Console.SetCursorPosition(0, 6);
                Console.WriteLine("capture screen can't release frame: " + somecounterstuck);

                Dispose();
                somecounterstuck++;
                return releasedFrame;
            }
        }
        int somecounterstuck = 0;



        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            Console.WriteLine("Dispose is called.");
            //Program.MessageBox((IntPtr)0, "Dispose is called.", "sccs error", 0);
            if (!disposed)
            {
                if (disposing)
                {

                    //GC.SuppressFinalize(duplicateFrameInformation);

                    //GC.SuppressFinalize(resourceViewDescription);

                    /*if (screenTexture!= null)
                    {
                        GC.SuppressFinalize(screenTexture);
                    }
                    if (screenResource != null)
                    {
                        GC.SuppressFinalize(screenResource);
                    }*/





                    //GC.SuppressFinalize(texture2DFinal);

                    /*GC.SuppressFinalize(textureDesc);
                    GC.SuppressFinalize(width);
                    GC.SuppressFinalize(height);
                    GC.SuppressFinalize(output);
                    GC.SuppressFinalize(output1);*/

                    //GC.SuppressFinalize(factory);



                    if (output != null)
                    {
                        GC.SuppressFinalize(output);
                        output = null;
                    }
                    if (output1 != null)
                    {
                        GC.SuppressFinalize(output1);
                        output1 = null;
                    }


                    if (device != null)
                    {
                        GC.SuppressFinalize(device);
                        device = null;
                    }



                    if (adapter != null)
                    {
                        GC.SuppressFinalize(adapter);
                        adapter = null;
                    }


                    if (frameCaptureData.ShaderResource != null)
                    {
                        GC.SuppressFinalize(frameCaptureData.ShaderResource);
                        frameCaptureData.ShaderResource = null;
                    }
                    if (frameCaptureData.ShaderResourceArray != null)
                    {
                        GC.SuppressFinalize(frameCaptureData.ShaderResourceArray);
                        frameCaptureData.ShaderResourceArray = null;
                    }

                    if (frameCaptureData.screencapturearrayofbytes != null)
                    {
                        GC.SuppressFinalize(frameCaptureData.screencapturearrayofbytes);
                        frameCaptureData.screencapturearrayofbytes = null;
                    }



                    if (lastShaderResourceView != null)
                    {
                        GC.SuppressFinalize(lastShaderResourceView);
                        lastShaderResourceView = null;
                    }

                    if (shaderRes != null)
                    {
                        GC.SuppressFinalize(shaderRes);
                        shaderRes = null;
                    }

                    if (outputDuplication != null)
                    {
                        GC.SuppressFinalize(outputDuplication);
                        outputDuplication.Dispose();

                        //outputDuplication = null;
                    }




                    /*
                    if (somegccollectframecounter >= somegccollectframecountermax)
                    {
                        //Console.SetCursorPosition(0, 5);
                        //Console.WriteLine("GCCollect:" + somefinalGCCollectcounter);
                        GC.Collect();
                        somefinalGCCollectcounter++;
                        somegccollectframecounter = 0;
                    }*/
                    // called via myClass.Dispose(). 
                    // OK to use any private object references


                }
                // Release unmanaged resources.
                // Set large fields to null.                
                disposed = true;
            }
        }
        int somereset = 0;

        unsafe void setinitvarsunsafetest()
        {
            //incrementercountermax = &collectgcmax;
            fixed (int* test = &collectgcmax)
            {
                incrementercountermax = test;
                //int* tester = &someres;
            }

            fixed (int* test = &collectgcreset)
            {
                incrementercounterreset = test;
                //int* tester = &someres;
            }
        }


        static int collectgcmax = 3;
        unsafe int collectgcreset = 0;


        static int collectgccounter = 0;

        unsafe int* incrementercounter;
        unsafe int* incrementercountermax;
        unsafe int* incrementercounterreset;
        //int* collectgcpointer;

        unsafe void incrementgccollecttest()
        {

            //is this working like a stack alloc of c# 7.3++ ? as in, is it better than not using pointers? i am still new to using the visual studio debugger so i have yet to know. but i had a 0.1 memory leak in the diagnostics tools window. testing gccollect.
            fixed (int* incrementercountermax = &collectgcmax)
            {
                fixed (int* incrementercounter = &collectgccounter)
                {
                    fixed (int* incrementercounterreset = &collectgcreset)
                    {
                        if (collectgccounter >= collectgcmax)
                        {
                            GC.Collect();
                            //Program.MessageBox((IntPtr)0, "GC.Collect is called.", "sccs error", 0);
                            collectgccounter = collectgcreset;
                        }
                        collectgccounter++;
                    }
                }
            }





            /*fixed (int* incrementercounterreset = &collectgcreset)
            {
                fixed (int* incrementercounter = &collectgccounter)
                {
                    fixed (int* incrementercountermax = &collectgcmax)
                    {
                        /*if (collectgccounter >= incrementercounterreset)
                        {
                            Program.MessageBox((IntPtr)0, "GC.Collect is called.", "sccs error", 0);
                            GC.Collect();
                            collectgccounter = incrementercounterreset;
                        }
                        collectgccounter++;
                        incrementercounter++
                        //GC.Collect();
                        //incrementercounter++;
                    }
                }
            }*/

            /*unsafe
            {
                int* numbers = stackalloc int[] { 0, 1, 2 };
                int* p1 = &numbers[0];
                int* p2 = p1;
                Console.WriteLine($"Before operation: p1 - {(long)p1}, p2 - {(long)p2}");
                Console.WriteLine($"Postfix increment of p1: {(long)(p1++)}");
                Console.WriteLine($"Prefix increment of p2: {(long)(++p2)}");
                Console.WriteLine($"After operation: p1 - {(long)p1}, p2 - {(long)p2}");
            }*/

            /*
            fixed (int* collectgcpointer = &collectgc)
            {
                int x = 0;
                int* reseter = &x;
                if (collectgc >= collectgcmax)
                {
                    GC.Collect();
                    collectgc = reset;
                }
                incrementervar++;
            }*/
        }




        public void Dispose() // Implement IDisposable
        {
            Program.MessageBox((IntPtr)0, "Dispose is called.", "sccs error", 0);

            /*GC.SuppressFinalize(duplicateFrameInformation);
            GC.SuppressFinalize(frameCaptureData);
            GC.SuppressFinalize(resourceViewDescription);


            GC.SuppressFinalize(screenTexture);
            GC.SuppressFinalize(screenResource);
            GC.SuppressFinalize(texture2DFinal);

            GC.SuppressFinalize(textureDesc);
            GC.SuppressFinalize(width);
            GC.SuppressFinalize(height);
            GC.SuppressFinalize(output);
            GC.SuppressFinalize(output1);

            GC.SuppressFinalize(device);
            //GC.SuppressFinalize(factory);
            GC.SuppressFinalize(adapter);

            GC.SuppressFinalize(lastShaderResourceView);
            GC.SuppressFinalize(shaderresourceview);



            if (somegccollectframecounter >= somegccollectframecountermax)
            {
                //Console.SetCursorPosition(0, 5);
                //Console.WriteLine("GCCollect:" + somefinalGCCollectcounter);
                GC.Collect();
                somefinalGCCollectcounter++;
                somegccollectframecounter = 0;
            }*/

            //duplicateFrameInformation = null;
            //frameCaptureData = null;



            /*if (duplicatedOutput != null)
            {
                GC.SuppressFinalize(duplicatedOutput);
                duplicatedOutput = null;
            }

            if (frameCaptureData.ShaderResource != null)
            {
                GC.SuppressFinalize(frameCaptureData);
                frameCaptureData.ShaderResource = null;
            }
            if (frameCaptureData.ShaderResourceArray != null)
            {
                GC.SuppressFinalize(frameCaptureData.ShaderResourceArray);
                frameCaptureData.ShaderResourceArray = null;
            }

            if (frameCaptureData.screencapturearrayofbytes != null)
            {
                GC.SuppressFinalize(frameCaptureData.screencapturearrayofbytes);
                frameCaptureData.screencapturearrayofbytes = null;
            }




            if (output != null)
            {
                GC.SuppressFinalize(output);
                output = null;
            }
            if (output1 != null)
            {
                GC.SuppressFinalize(output1);
                output1 = null;
            }


            if (device != null)
            {
                GC.SuppressFinalize(device);
                device = null;
            }



            if (adapter != null)
            {
                GC.SuppressFinalize(adapter);
                adapter = null;
            }




            if (lastShaderResourceView != null)
            {
                GC.SuppressFinalize(lastShaderResourceView);
                lastShaderResourceView = null;
            }

            if (shaderresourceview != null)
            {
                GC.SuppressFinalize(shaderresourceview);
                shaderresourceview = null;
            }*/



            Dispose(true);
            GC.SuppressFinalize(this);


        }



        //https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/classes-and-structs/destructors
        ~sccssharpdxscreencapture()
        {
            System.Diagnostics.Trace.WriteLine("First's finalizer is called.");
            Program.MessageBox((IntPtr)0, "finalizer is called.", "sccs error", 0);
            Dispose(false);
        }

        public static void Copy(Device device, Texture2D source, Texture2D target)
        {
            if (device == null)
                throw new ArgumentNullException("device");
            if (source == null)
                throw new ArgumentNullException("source");
            if (target == null)
                throw new ArgumentNullException("target");

            int sourceWidth = source.Description.Width;
            int sourceHeight = source.Description.Height;
            int targetWidth = target.Description.Width;
            int targetHeight = target.Description.Height;

            if (sourceWidth == targetWidth && sourceHeight == targetHeight)
            {
                device.ImmediateContext.CopyResource(source, target);
            }
            else
            {
                int width = Math.Min(sourceWidth, targetWidth);
                int height = Math.Min(sourceHeight, targetHeight);
                var region = new ResourceRegion(0, 0, 0, width, height, 1);
                device.ImmediateContext.CopySubresourceRegion(source, 0, region, target, 0);
            }
        }
    }
}


/*
byte* ptr = (byte*)interptr.ToPointer();

int pixelSize = 3;
int nWidth = textureDescriptionFinal.Width * pixelSize;
int nHeight = textureDescriptionFinal.Height;

                for (int y = 0; y<nHeight; y++)
                {
                    for (int x = 0; x<nWidth; x++)
                    {
                        if (x % pixelSize == 0 || x == 0)
                        {
                            var bytePoser = (((y) * nWidth) + (x));

var test0 = ptr[bytePoser + 0];
var test1 = ptr[bytePoser + 1];
var test2 = ptr[bytePoser + 2];

ptr++;
                        }
                    }
                }*/










/*int wid = textureDescriptionFinal.Width / 10;
int hgt = textureDescriptionFinal.Height / 10;
System.Drawing.Bitmap piece = new System.Drawing.Bitmap(wid, hgt);
System.Drawing.Rectangle destrect = new System.Drawing.Rectangle(0, 0, wid, hgt);
using (Graphics gr = Graphics.FromImage(piece))
{
int numrows = image.Height / hgt;
int numcols = image.Width / wid;
System.Drawing.Rectangle sourcerect = new System.Drawing.Rectangle(0, 0, wid, hgt);
for (int row = 0; row < numrows; row++)
{
    sourcerect.X = 0;
    for (int col = 0; col < numcols; col++)
    {
        gr.DrawImage(image, destrect, sourcerect,GraphicsUnit.Pixel);
        piece.Save(@"C:\Users\steve\OneDrive\Desktop\screenRecord\" + row.ToString("00") + col.ToString("00") + ".png");
        sourcerect.X += wid;
    }
    sourcerect.Y += hgt;
}
}*/


//FastMemCopy.FastMemoryCopy(interptr + totalBytesOffsetSrc, arrayOfImagePTR[(*iptr)] + totalBytesOffsetDest, bytesToTransferWidth);












//sure lets use this and lag the whole program // 1000% WORKING // 1000% FAILING PERFORMANCE
/*
SystemTickPerformance.Stop();
SystemTickPerformance.Reset();
SystemTickPerformance.Start();

int numrows = hgt;
int numcols = wid;
System.Drawing.Rectangle sourcerect = new System.Drawing.Rectangle(0, 0, wid, hgt);
for (int row = 0; row < 10; row++)
{
    sourcerect.X = 0;
    for (int col = 0; col < 10; col++)
    {
        // Copy the piece of the image.
        gr.DrawImage(image, destrect, sourcerect, GraphicsUnit.Pixel);

        string filePathVE = "desktop capture";
        var exportedToFolderPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

        filePathVE = exportedToFolderPath + "\\" + filePathVE;// @"LAYERS\PNG\";

        if (!Directory.Exists(filePathVE))
        {
            Directory.CreateDirectory(filePathVE);
        }

        var fi = new FileInfo(filePathVE);
        fi.Refresh();

        piece.Save(filePathVE + "\\" + imageCounter + ".jpg");
        imageCounter++;


        sourcerect.X += wid;
    }
    sourcerect.Y += hgt;
}



//Console.WriteLine(SystemTickPerformance.Elapsed.Milliseconds);
MainWindow.MessageBox((IntPtr)0, SystemTickPerformance.Elapsed.Milliseconds + "", "sccs error message", 0);*/














/* // not working yet - to test later for performance after it works if it ever will.
for (iptr = &ii; *iptr < 3; (*iptr)++) //, (*arrayOfImageIterator)++
{
    iii = 0;

    if (*xx >= *w)
    {
        *yy += hgt; //hh
        *xx = *z;
    }

    for (loopH = &iii; *loopH < hgt - 1; (*loopH)++)
    {
        if (*arrayOfImageIterator < hgt)
        {

            //Console.WriteLine(*loopH + "");
            x = *xx;
            y = *yy;
            lh = *loopH;

            totalBytesOffsetDest = bytesToTransferWidth * (lh);
            totalBytesOffsetSrc = ((x) + (((y) + (lh)) * textureDescriptionFinal.Width)) * 4;

            Marshal.Copy(interptr + totalBytesOffsetSrc, (int[])arrayOfImage[(*arrayOfImageIterator)], totalBytesOffsetDest, bytesToTransferWidth);



            //MainWindow.MessageBox((IntPtr)0, *arrayOfImageIterator + "", "sccs error message", 0);
            //Console.WriteLine(*arrayOfImageIterator);
            arrayOfImageIteratorTwo
        }
        (*arrayOfImageIterator)++;
    }
    if (*xx < *w)
    {
        *xx += wid;
    }
}*/
/*if (*iptr < 10)
      {
          //Console.WriteLine(*loopH + "");
          x = *xx;
          y = *yy;
          lh = *loopH;

          totalBytesOffsetDest = bytesToTransferWidth * (lh);

          //(Y coordinate * width) + X coordinate
          totalBytesOffsetSrc = ((y) * bytesToTransferWidth) + x;

          Marshal.Copy(interptr + totalBytesOffsetSrc, (int[])arrayOfImage[(*iptr)++], totalBytesOffsetDest, bytesToTransferWidth);

          //totalBytesOffsetSrc = ((x) + (((y) + (lh)) * textureDescriptionFinal.Width)) * 4;

          //var indexer01 = x + MainWindow.worldwidth * (y + MainWindow.worldheight * z);

          //MainWindow.MessageBox((IntPtr)0, *arrayOfImageIterator + "", "sccs error message", 0);
          //Console.WriteLine(*iptr);
          //arrayOfImageIteratorTwo
      }*/
