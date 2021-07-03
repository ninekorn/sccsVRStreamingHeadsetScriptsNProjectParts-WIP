using System;
using System.Collections.Generic;
using System.Text;
using SharpDX;
using SharpDX.Direct3D11;
using SharpDX.DXGI;

using Device = SharpDX.Direct3D11.Device;
using PixelFormat = System.Drawing.Imaging.PixelFormat;
using System.Reflection;

//using System.IO;
using System.Runtime.InteropServices;

using System.IO;

namespace sccoresystems
{
    public interface ISCCSScreenCaptureInterface
    {
        sccssharpdxscreencapture sccscreatescreencapture(sccssharpdxscreencapturedata sccsscreencapturedata);
    }

    public class sccssharpdxscreencapture : IDisposable, ISCCSScreenCaptureInterface
    {
        

        public int Id { get; set; }
        public string Name { get; set; }


        public static sccssharpdxscreencapturedata sccsscreencapturedata;

        public sccssharpdxscreencapture sccscreatescreencapture(sccssharpdxscreencapturedata sccsscreencapturedata_)
        {
            sccsscreencapturedata = sccsscreencapturedata_;
            return Instance;
        }


        sccssharpdxscreencapture instance = null;

        sccssharpdxscreencapture someinstance => Instance;

        public sccssharpdxscreencapture Instance
        {
            get
            {
                if (instance == null)
                {
                    //Console.WriteLine("instance == null");
                    instance = new sccssharpdxscreencapture();
                }
                else
                {
                    //startsccssharpdxscreencapture();
                }
                return instance;
            }
            set
            {
                instance = null;
            }
        }

        [System.Runtime.InteropServices.DllImport("gdi32.dll")]
        public static extern bool DeleteObject(IntPtr hObject);

        public sccssharpdxscreencapture()
        {
            instance = this;
            startsccssharpdxscreencapture();
        }

        OutputDuplication duplicatedOutput;
        Texture2D screenTexture;
        Device device;
        sccssharpdxscreenframe frameCaptureData;
        ShaderResourceView shaderresourceview;
        Texture2D texture2DFinal;
        Texture2D lasttexture2DFinal;
        
        System.Drawing.Bitmap lastbitmap;
        System.Drawing.Bitmap bitmap;

        SharpDX.DXGI.Resource lastscreenResource;
        Texture2D lastscreenTexture;


        ShaderResourceViewDescription resourceViewDescription;
        ShaderResourceView lastShaderResourceView;
        Output output;
        Output1 output1;
        Adapter1 adapter;
        int width;
        int height;
        const string outputFileName = "ScreenCapture.bmp";
        string path = "";
        Texture2DDescription textureDesc;
      

        public void startsccssharpdxscreencapture()
        {
           
            // # of graphics card adapter
            const int numAdapter = 0;

            // # of output device (i.e. monitor)
            const int numOutput = 0;

     

            somebitmappath = path + "" + outputFileName;
            fileInfo = new FileInfo(somebitmappath);
            // Create DXGI Factory1
            var factory = new Factory1();
            adapter = factory.GetAdapter1(numAdapter);

            // Create device from Adapter
            device = new Device(adapter);

            // Get DXGI.Output
            output = adapter.GetOutput(numOutput);
            output1 = output.QueryInterface<Output1>();

            // Width/Height of desktop to capture
            width = ((Rectangle)output.Description.DesktopBounds).Width;
            height = ((Rectangle)output.Description.DesktopBounds).Height;

            // Create Staging texture CPU-accessible
            textureDesc = new Texture2DDescription
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

            /*textureDesc = new Texture2DDescription
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

            screenTexture = new Texture2D(device, textureDesc);

            var textureDescriptionFinal = new Texture2DDescription
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

            texture2DFinal = new Texture2D(device, textureDescriptionFinal);

            // Duplicate the output
            duplicatedOutput = output1.DuplicateOutput(device);

            frameCaptureData = new sccssharpdxscreenframe();
            frameCaptureData.width = width;
            frameCaptureData.height = height;
 
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
            var boundsRect = new System.Drawing.Rectangle(0, 0, width, height);
            var bmpData = bitmap.LockBits(boundsRect, System.Drawing.Imaging.ImageLockMode.ReadOnly, bitmap.PixelFormat);
            bytesTotal = Math.Abs(bmpData.Stride) * bitmap.Height;
            bitmap.UnlockBits(bmpData);
            textureByteArray = new byte[bytesTotal];

            boundsRect = new System.Drawing.Rectangle(0, 0, width, height);


            // Display the texture using system associated viewer
            //System.Diagnostics.Process.Start(Path.GetFullPath(Path.Combine(Environment.CurrentDirectory, outputFileName)));
            // TODO: We should cleanp up all allocated COM objects here


            setinitvarsunsafetest();
        }

        



        int bytesTotal = 0;

        byte[] textureByteArray;

        SharpDX.DXGI.Resource screenResource;
        OutputDuplicateFrameInformation duplicateFrameInformation;
        FileInfo fileInfo;
        string somebitmappath;
        DataBox dataBox1;
        int memoryBitmapStride;
        int columns;
        int rows;
        IntPtr interptr1;
        IntPtr destPtr;

        System.Drawing.Rectangle boundsRect;
        System.Drawing.Imaging.BitmapData mapDest;
        IntPtr sourcePtr;
        public sccssharpdxscreenframe ScreenCapture(int screencapturedelay)
        {
            //for (int i = 0; !captureDone; i++)
            //{
            //}
            try
            {
                //output1 = output.QueryInterface<Output1>();
                //duplicatedOutput = output1.DuplicateOutput(device);
                // Try to get duplicated frame within given time

           

                duplicatedOutput.TryAcquireNextFrame(10000, out duplicateFrameInformation, out screenResource);


                // copy resource into memory that can be accessed by the CPU
                /*using (var screenTexture2D = screenResource.QueryInterface<Texture2D>())
                {
                   
                }*/

                //screenTexture = new Texture2D(device, textureDesc);
                screenTexture = new Texture2D(device, textureDesc);
                var screenTexture2D = screenResource.QueryInterface<Texture2D>();
                device.ImmediateContext.CopyResource(screenTexture2D, screenTexture);








                /*
                if (lastscreenTexture != null)
                {
                    lastscreenTexture.Dispose();
                    screenTexture.Dispose();
                }
                lastscreenTexture = screenTexture;*/







                dataBox1 = device.ImmediateContext.MapSubresource(screenTexture, 0, MapMode.Read, SharpDX.Direct3D11.MapFlags.None);
                //or
                //var mapSource = device.ImmediateContext.MapSubresource(texture2DFinal, 0, MapMode.Read, SharpDX.Direct3D11.MapFlags.None);

                // Create Drawing.Bitmap
                //var bitmap = new System.Drawing.Bitmap(width, height, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
                boundsRect = new System.Drawing.Rectangle(0, 0, width, height);

                // Copy pixels from screen capture Texture to GDI bitmap
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
                device.ImmediateContext.UnmapSubresource(screenTexture, 0);

                if (somebitmapcounter >= somebitmapcountermax)
                {
                    somebitmapcounter = 0;
                }

                // Save the output
                bitmap.Save(path + "" + somebitmapcounter + "" + outputFileName); //
                somebitmapcounter++;
                

                //or device.ImmediateContext.UnmapSubresource(texture2DFinal, 0);
                bitmap.Save(path + outputFileName);
                DeleteObject(sourcePtr);
                DeleteObject(destPtr);
                fileInfo.Refresh();














                //to readd
                //device.ImmediateContext.CopyResource(screenTexture, texture2DFinal);
                // Get the desktop capture texture
                /*var mapSource = device.ImmediateContext.MapSubresource(screenTexture, 0, MapMode.Read, SharpDX.Direct3D11.MapFlags.None);
                //or
                //var mapSource = device.ImmediateContext.MapSubresource(texture2DFinal, 0, MapMode.Read, SharpDX.Direct3D11.MapFlags.None);

                // Create Drawing.Bitmap
                var bitmap = new System.Drawing.Bitmap(width, height, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
                var boundsRect = new System.Drawing.Rectangle(0, 0, width, height);

                // Copy pixels from screen capture Texture to GDI bitmap
                var mapDest = bitmap.LockBits(boundsRect, System.Drawing.Imaging.ImageLockMode.WriteOnly, bitmap.PixelFormat);
                var sourcePtr = mapSource.DataPointer;
                var destPtr = mapDest.Scan0;
                for (int y = 0; y < height; y++)
                {
                    // Copy a single line 
                    Utilities.CopyMemory(destPtr, sourcePtr, width * 4);

                    // Advance pointers
                    sourcePtr = IntPtr.Add(sourcePtr, mapSource.RowPitch);
                    destPtr = IntPtr.Add(destPtr, mapDest.Stride);
                }

                // Release source and dest locks
                bitmap.UnlockBits(mapDest);
                device.ImmediateContext.UnmapSubresource(screenTexture, 0);
                //or device.ImmediateContext.UnmapSubresource(texture2DFinal, 0);
                DeleteObject(sourcePtr);
                DeleteObject(destPtr);


                if (somebitmapcounter >= somebitmapcountermax)
                {
                    somebitmapcounter = 0;
                }

                // Save the output
                bitmap.Save(path + "" + somebitmapcounter + "" + outputFileName);
                somebitmapcounter++;*/
                //to readd


                //TO READD WHEN IN NEED OF 1 bitmap and 1 array of bytes.
                /*dataBox1 = device.ImmediateContext.MapSubresource(screenTexture, 0, SharpDX.Direct3D11.MapMode.Read, SharpDX.Direct3D11.MapFlags.None);

                memoryBitmapStride = textureDesc.Width * 4;

                columns = textureDesc.Width;
                rows = textureDesc.Height;
                interptr1 = dataBox1.DataPointer;

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
                bitmap = new System.Drawing.Bitmap(width, height, memoryBitmapStride, PixelFormat.Format32bppArgb, Marshal.UnsafeAddrOfPinnedArrayElement(textureByteArray, 0));
                DeleteObject(interptr1);*/
                //DISCARDED

                /*if (somebitmapcounter >= somebitmapcountermax)
                {
                    somebitmapcounter = 0;
                }

                //bitmap = new System.Drawing.Bitmap(160, 128, 160 * 4, PixelFormat.Format32bppArgb, Marshal.UnsafeAddrOfPinnedArrayElement(textureByteArray, 0));
                bitmap = new System.Drawing.Bitmap(width, height, memoryBitmapStride, PixelFormat.Format32bppArgb, Marshal.UnsafeAddrOfPinnedArrayElement(textureByteArray, 0));
                device.ImmediateContext.UnmapSubresource(screenTexture, 0);
                //somebitmappath = path + "" + somebitmapcounter + "" + outputFileName;
                somebitmappath = path + "" + outputFileName;
                //bitmap.Save(somebitmappath);
                bitmap.Save(somebitmappath);*/



                //somebitmapcounter++;

                //TO READD WHEN IN NEED OF 1 bitmap and 1 array of bytes.









                /*shaderresourceview = new ShaderResourceView(device, screenTexture, resourceViewDescription);
                device.ImmediateContext.GenerateMips(shaderresourceview);

                if (shaderresourceview != null)
                {
                    frameCaptureData.ShaderResource = shaderresourceview;
                }

                if (lastShaderResourceView != null)
                {
                    lastShaderResourceView.Dispose();
                }

                lastShaderResourceView = shaderresourceview;

                if (lastscreenTexture != null)
                {
                    lastscreenTexture.Dispose();
                }
                lastscreenTexture = screenTexture;



         


                /*if (lasttexture2DFinal != null)
                {
                    lasttexture2DFinal.Dispose();
                }
                lasttexture2DFinal = texture2DFinal;*/




                /*if (lastbitmap!= null)
                {
                    lastbitmap.Dispose();
                }
                lastbitmap = bitmap;*/

                //screenResource.Dispose();
                //screenTexture.Dispose();

                /*if (i > 0)
                {
                    
                    // Capture done
                    captureDone = true;
                }*/


                /*if (lastscreenResource != null)
                {
                    lastscreenResource.Dispose();
                }
                lastscreenResource = screenResource;*/
                //screenResource.Dispose();
                //texture2DFinal.Dispose();
                //releaseFrame();

                //duplicatedOutput.ReleaseFrame();
                //duplicatedOutput.Dispose();


                if (screenTexture2D != null)
                {
                    GC.SuppressFinalize(screenTexture2D);
                    screenTexture2D.Dispose();
                }
                screenTexture2D = null;


                if (screenTexture != null)
                {
                    GC.SuppressFinalize(screenTexture);
                    screenTexture.Dispose();
                }
                screenTexture = null;
                releaseFrame();
            }
            catch (Exception ex) //SharpDXException
            {
                //Program.MessageBox((IntPtr)0, "" + e.ToString(), "sccs error", 0);
                /*if (e.ResultCode.Code != SharpDX.DXGI.ResultCode.WaitTimeout.Result.Code)
                {
                    throw e;
                }*/
                //Program.MessageBox((IntPtr)0, ex.ToString(), "sccoresystems", 0);
            }
            finally
            {
                incrementgccollecttest();
                //GC.Collect();


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



                /*if (output != null)
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
                */
                /*
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

                if (shaderresourceview != null)
                {
                    GC.SuppressFinalize(shaderresourceview);
                    shaderresourceview = null;
                }*/

                /*if (somegccollectframecounter >= somegccollectframecountermax)
                {
                    //Console.SetCursorPosition(0, 5);
                    //Console.WriteLine("GCCollect:" + somefinalGCCollectcounter);
                    GC.Collect();
                    somefinalGCCollectcounter++;
                    somegccollectframecounter = 0;
                }
                
                GC.SuppressFinalize(frameCaptureData);



               somegccollectframecounter += 1;*/



            }
            return frameCaptureData;
        }










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
                    if (duplicatedOutput != null)
                    {
                        duplicatedOutput.ReleaseFrame();
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

                    if (shaderresourceview != null)
                    {
                        GC.SuppressFinalize(shaderresourceview);
                        shaderresourceview = null;
                    }

                    if (duplicatedOutput != null)
                    {
                        GC.SuppressFinalize(duplicatedOutput);
                        duplicatedOutput.Dispose();

                        duplicatedOutput = null;
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
    }
}
