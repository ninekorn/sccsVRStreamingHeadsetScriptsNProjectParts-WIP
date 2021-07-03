using SharpDX;
using SharpDX.Direct3D11;
using SharpDX.DXGI;
using System;
using System.Runtime.InteropServices;
using DSystemConfiguration = sccoresystems.sccscore.scsystemconfiguration;
using Ab3d.OculusWrap;
//using Ab3d.DXEngine.OculusWrap;
using Ab3d.OculusWrap.DemoDX11;
//using Result = Result;


using System.Threading;
using System.Threading.Tasks;
using SC_message_object = sccoresystems.sccsmessageobject;
using SC_message_object_jitter = sccoresystems.sc_message_object_jitter;

using ISCCS_Jitter_Interface = Jitter.ISCCS_Jitter_Interface;
using Jitter;
using Jitter.LinearMath;
using System.Diagnostics;
using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Diagnostics;
//using System.Windows.Forms;

using SharpDX;
using SharpDX.D3DCompiler;
using SharpDX.Direct3D;
using SharpDX.Direct3D11;
using SharpDX.DXGI;
//using SharpDX.Windows;
using Buffer = SharpDX.Direct3D11.Buffer;
using Device = SharpDX.Direct3D11.Device;


using SharpDX.WIC;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Runtime.InteropServices;

//using System.Windows.Media;
using System.Collections;

using System.Threading;
//using System.Windows.Media.Imaging;

using Matrix = SharpDX.Matrix;

using SharpDX.DirectInput;

using System.IO.Ports;

using Result = Ab3d.OculusWrap.Result;

namespace sccoresystems.sccsgraphics
{
    public abstract class sccssharpdxdirectx : ISCCS_Jitter_Interface
    {

        RenderTargetView BackBufferRenderTargetView = null;
        public static SerialPort serialPort;


        public jitter_sc sc_create_jitter_instances(sc_jitter_data _sc_jitter_data)
        {
            return null;
        }

        static jitter_sc instance = null;

        public static jitter_sc Instance
        {
            get
            {
                /*if (instance == null)
                {
                    instance = new jitter_sc();
                }*/
                //instance = new jitter_sc();

                return instance;
            }
        }

        public jitter_sc[] createjitterinstances(jitter_sc[] scjitterphysics, sc_jitter_data scjitterdata)
        {
            for (int xx = 0; xx < Program.physicsengineinstancex; xx++)
            {
                for (int yy = 0; yy < Program.physicsengineinstancey; yy++)
                {
                    for (int zz = 0; zz < Program.physicsengineinstancez; zz++)
                    {
                        var indexer00 = xx + Program.physicsengineinstancex * (yy + Program.physicsengineinstancey * zz);

                        //instance = new jittersc();

                        instance = new jitter_sc();
                        scjitterphysics[indexer00] = instance.sc_create_jitter_instances(scjitterdata);
                    }
                }
            }

            return scjitterphysics;
        }

        public enum BodyTag
        {
            DrawMe,
            DontDrawMe,
            Terrain,
            pseudoCloth,

            PlayerHandLeft,
            PlayerHandRight,
            PlayerShoulderLeft,
            PlayerShoulderRight,
            PlayerTorso,
            PlayerPelvis,
            PlayerUpperArmLeft,
            PlayerLowerArmLeft,
            PlayerUpperArmRight,
            PlayerLowerArmRight,
            PlayerUpperLegLeft,
            PlayerLowerLegLeft,
            PlayerUpperLegRight,
            PlayerLowerLegRight,
            PlayerFootRight,
            PlayerFootLeft,
            PlayerHead,
            PlayerLeftElbowTarget,
            PlayerRightHandGrabTarget,
            PlayerLeftHandGrabTarget,

            PlayerRightElbowTarget,
            PlayerLeftElbowTargettwo,
            PlayerRightElbowTargettwo,
            PlayerLeftTargetKnee,
            PlayerRightTargetKnee,
            PlayerLeftTargettwoKnee,
            PlayerRightTargettwoKnee,

            sc_containment_grid,
            sc_grid,

            Screen,
            sc_jitter_cloth,
            //someothertest,
            //testChunkCloth,
            //cloth_cube,
            //screen_corners,
            //screen_pointer_touch,
            //screen_pointer_HMD,
            _terrain_tiles,
            _terrain,
            _floor,
            //_icosphere,
            //_sphere,
            _spectrum,
            _pixelspectrumscreen,
            //_physics_cube_group_b,
            _screen_assets,

            physicsInstancedCube,
            physicsInstancedCone,
            physicsInstancedCylinder,
            physicsInstancedCapsule,
            physicsInstancedSphere,

            sc_perko_voxel,
            physicsInstancedScreen,
            physicsInstancedScreenHeightmaps,
            sc_perko_voxel_planet_chunk
        }


        Thread main_thread_update;

        public Matrix OrthoMatrix { get; private set; }
        public SharpDX.Direct3D11.Device device { get; set; }

        [DllImport("User32.dll", CharSet = CharSet.Unicode)]
        public static extern int MessageBox(IntPtr h, string m, string c, int type);

        //OCULUS RIFT
        public bool _useOculusRift = true;
        public int SurfaceWidth;
        public int SurfaceHeight;
        public DateTime startTime;
        //public OculusWrapVirtualRealityProvider _oculusRiftVirtualRealityProvider;
        //public static Ab3d.DirectX.DXDevice _dxDevice;
        private RenderTargetView _renderTargetView;
        SharpDX.Direct3D11.Texture2D depthBuffer;
        private DepthStencilView depthStencilView;
        protected DepthStencilView DepthStencilView => depthStencilView;
        SharpDX.Direct3D11.DepthStencilState depthStencilState;
        MirrorTexture mirrorTexture = null;
        Guid textureInterfaceId = new Guid("6f15aaf2-d208-4e89-9ab4-489535d34f9c"); // Interface ID of the Direct3D Texture2D interface.
        


        // Properties.
        public bool VerticalSyncEnabled { get; set; }
        public int VideoCardMemory { get; private set; }
        public string VideoCardDescription { get; private set; }
        public SwapChain SwapChain { get; set; }
        public SharpDX.Direct3D11.Device Device { get; private set; }
        public DeviceContext DeviceContext { get; private set; }
        public Texture2D DepthStencilBuffer { get; set; }
        public DepthStencilState _depthStencilState { get; set; }
        public RasterizerState RasterState { get; set; }
        public Matrix ProjectionMatrix { get; private set; }

        public IntPtr sessionPtr;
        public Result result;
      
        public EyeTexture[] eyeTextures;
        public Texture2D BackBuffer;
        public SharpDX.Direct3D11.Texture2D mirrorTextureD3D;

        
        public ControllerType controllerTypeRTouch;
        public ControllerType controllerTypeLTouch;
        public Ab3d.OculusWrap.InputState inputStateLTouch;
        public Ab3d.OculusWrap.InputState inputStateRTouch;
        public LayerEyeFov layerEyeFov;
        public OvrWrap OVR;
        public HmdDesc hmdDesc;





        public sccssharpdxdirectx D3D;

        public DepthStencilState DepthDisabledStencilState { get; private set; }
        public BlendState AlphaEnableBlendingState { get; private set; }
        public BlendState AlphaDisableBlendingState { get; private set; }
        public DepthStencilState DepthStencilState { get; private set; }


        // Constructor
        /*public SC_console_directx()
        {

        }*/

        protected sccssharpdxdirectx() //DSystemConfiguration configuration, IntPtr Hwnd, sc_console.sc_console_writer _writer
        {
            D3D = this;
            //Update();
            scinitdirectx(); //configuration, Hwnd, _writer 
        }




        protected virtual void scinitdirectx() //DSystemConfiguration configuration, IntPtr Hwnd, sc_console.sc_console_writer _writer
        {
            //MessageBox((IntPtr)0,"" + Program.handler, "sccoresystems Error", 0);

            //sc_graphics_sec _graphics_sec
            // Methods
            //public bool Initialize(DSystemConfiguration configuration, IntPtr Hwnd,sc_console.sc_console_writer _writer)
            //{
            try
            {
                startTime = DateTime.Now;
                //var dpiScale = GetDpiScale();

                using (var _factory = new Factory1())
                {
                    var _adapter = _factory.GetAdapter1(0);

                    using (var _output = _adapter.GetOutput(0))
                    {
                        SurfaceWidth = ((SharpDX.Rectangle)_output.Description.DesktopBounds).Width;
                        SurfaceHeight = ((SharpDX.Rectangle)_output.Description.DesktopBounds).Height;
                    }
                }

                if (_useOculusRift)
                {
                    Result result;

                    controllerTypeRTouch = ControllerType.RTouch;
                    controllerTypeLTouch = ControllerType.LTouch;

                    OVR = OvrWrap.Create();

                    // Define initialization parameters with debug flag.
                    InitParams initializationParameters = new InitParams();
                    initializationParameters.Flags = InitFlags.Debug | InitFlags.RequestVersion;
                    initializationParameters.RequestedMinorVersion = 17;

                    // Initialize the Oculus runtime.
                    string errorReason = null;
                    try
                    {
                        result = OVR.Initialize(initializationParameters);

                        if (result < Result.Success)
                            errorReason = result.ToString();
                    }
                    catch (Exception ex)
                    {
                        errorReason = ex.Message;
                    }

                    if (errorReason != null)
                    {
                        Console.WriteLine("Failed to initialize the Oculus runtime library:\r\n" + errorReason);
                        //MessageBox.Show("Failed to initialize the Oculus runtime library:\r\n" + errorReason, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    // Use the head mounted display.
                    sessionPtr = IntPtr.Zero;
                    var graphicsLuid = new GraphicsLuid();
                    result = OVR.Create(ref sessionPtr, ref graphicsLuid);
                    if (result < Result.Success)
                    {
                        Console.WriteLine("The HMD is not enabled: " + result.ToString());
                        //MessageBox.Show("The HMD is not enabled: " + result.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    hmdDesc = OVR.GetHmdDesc(sessionPtr);


                    try
                    {
                        // Create a set of layers to submit.
                        eyeTextures = new EyeTexture[2];

                        // Create DirectX drawing device.
                        //Device = new Device(SharpDX.Direct3D.DriverType.Hardware, DeviceCreationFlags.Debug);
                        var swapChainDesc = new SwapChainDescription()
                        {
                            OutputHandle = sccsconsole.sccsconsolecore.handle,
                            BufferCount = 1,
                            Flags = SwapChainFlags.AllowModeSwitch,
                            IsWindowed = true,
                            ModeDescription = new ModeDescription(SurfaceWidth, SurfaceHeight, new Rational(60, 1), Format.B8G8R8A8_UNorm),
                            SampleDescription = new SampleDescription(1, 0),
                            SwapEffect = SwapEffect.Discard,
                            Usage = Usage.RenderTargetOutput | Usage.Shared

                        };

                        Device device_;
                        SwapChain someswapChain;
                        Device.CreateWithSwapChain(DriverType.Hardware, DeviceCreationFlags.None, swapChainDesc, out device_, out someswapChain);


                        SwapChain = someswapChain;
                        Device = device_;
                        device = Device;
                        DeviceContext = device.ImmediateContext;

                        // Create DirectX Graphics Interface factory, used to create the swap chain.
                        //var factory = new SharpDX.DXGI.Factory4();
                        // Define the properties of the swap chain.
                        //SwapChainDescription swapChainDescription = new SwapChainDescription();
                        //swapChainDescription.BufferCount = 1;
                        //swapChainDescription.IsWindowed = true;
                        //swapChainDescription.OutputHandle = sccsconsole.sccsconsolecore.handle;
                        //swapChainDescription.SampleDescription = new SampleDescription(1, 0);
                        //swapChainDescription.Usage = Usage.RenderTargetOutput | Usage.ShaderInput;
                        //swapChainDescription.SwapEffect = SwapEffect.Sequential;
                        //swapChainDescription.Flags = SwapChainFlags.AllowModeSwitch;
                        //swapChainDescription.ModeDescription.Width = SurfaceWidth;
                        //swapChainDescription.ModeDescription.Height = SurfaceHeight;
                        //swapChainDescription.ModeDescription.Format = Format.R8G8B8A8_UNorm;
                        //swapChainDescription.ModeDescription.RefreshRate.Numerator = 0;
                        //swapChainDescription.ModeDescription.RefreshRate.Denominator = 1;

                        // Create the swap chain.
                        //SwapChain = new SwapChain(factory, device, swapChainDescription);

                        // Retrieve the back buffer of the swap chain.
                        BackBuffer = SwapChain.GetBackBuffer<Texture2D>(0);
                        BackBufferRenderTargetView = new RenderTargetView(device, BackBuffer);

                        // Create a depth buffer, using the same width and height as the back buffer.
                        Texture2DDescription depthBufferDescription = new Texture2DDescription();
                        depthBufferDescription.Format = Format.D32_Float;
                        depthBufferDescription.ArraySize = 1;
                        depthBufferDescription.MipLevels = 1;
                        depthBufferDescription.Width = SurfaceWidth;
                        depthBufferDescription.Height = SurfaceHeight;
                        depthBufferDescription.SampleDescription = new SampleDescription(1, 0);
                        depthBufferDescription.Usage = ResourceUsage.Default;
                        depthBufferDescription.BindFlags = BindFlags.DepthStencil;
                        depthBufferDescription.CpuAccessFlags = CpuAccessFlags.None;
                        depthBufferDescription.OptionFlags = ResourceOptionFlags.None;

                        // Define how the depth buffer will be used to filter out objects, based on their distance from the viewer.
                        DepthStencilStateDescription depthStencilStateDescription = new DepthStencilStateDescription();
                        depthStencilStateDescription.IsDepthEnabled = true;
                        depthStencilStateDescription.DepthComparison = Comparison.Less;
                        depthStencilStateDescription.DepthWriteMask = DepthWriteMask.Zero;

                        // Create the depth buffer.
                        depthBuffer = new Texture2D(device, depthBufferDescription);
                        depthStencilView = new DepthStencilView(device, depthBuffer);
                        depthStencilState = new DepthStencilState(device, depthStencilStateDescription);

                        var viewport = new Viewport(0, 0, hmdDesc.Resolution.Width, hmdDesc.Resolution.Height, 0.0f, 1.0f);

                        DeviceContext.OutputMerger.SetDepthStencilState(depthStencilState);
                        DeviceContext.OutputMerger.SetRenderTargets(depthStencilView, BackBufferRenderTargetView);
                        DeviceContext.Rasterizer.SetViewport(viewport);

                        // Retrieve the DXGI device, in order to set the maximum frame latency.
                        using (SharpDX.DXGI.Device1 dxgiDevice = device.QueryInterface<SharpDX.DXGI.Device1>())
                        {
                            dxgiDevice.MaximumFrameLatency = 1;
                        }

                        var layerEyeFov = new LayerEyeFov();
                        layerEyeFov.Header.Type = LayerType.EyeFov;
                        layerEyeFov.Header.Flags = LayerFlags.None;

                        for (int eyeIndex = 0; eyeIndex < 2; eyeIndex++)
                        {
                            EyeType eye = (EyeType)eyeIndex;
                            var eyeTexture = new EyeTexture();
                            eyeTextures[eyeIndex] = eyeTexture;

                            // Retrieve size and position of the texture for the current eye.
                            eyeTexture.FieldOfView = hmdDesc.DefaultEyeFov[eyeIndex];
                            eyeTexture.TextureSize = OVR.GetFovTextureSize(sessionPtr, eye, hmdDesc.DefaultEyeFov[eyeIndex], 1.0f);
                            eyeTexture.RenderDescription = OVR.GetRenderDesc(sessionPtr, eye, hmdDesc.DefaultEyeFov[eyeIndex]);
                            eyeTexture.HmdToEyeViewOffset = eyeTexture.RenderDescription.HmdToEyePose.Position;
                            eyeTexture.ViewportSize.Position = new Vector2i(0, 0);
                            eyeTexture.ViewportSize.Size = eyeTexture.TextureSize;
                            eyeTexture.Viewport = new Viewport(0, 0, eyeTexture.TextureSize.Width, eyeTexture.TextureSize.Height, 0.0f, 1.0f);

                            // Define a texture at the size recommended for the eye texture.
                            eyeTexture.Texture2DDescription = new Texture2DDescription();
                            eyeTexture.Texture2DDescription.Width = eyeTexture.TextureSize.Width;
                            eyeTexture.Texture2DDescription.Height = eyeTexture.TextureSize.Height;
                            eyeTexture.Texture2DDescription.ArraySize = 1;
                            eyeTexture.Texture2DDescription.MipLevels = 1;
                            eyeTexture.Texture2DDescription.Format = Format.R8G8B8A8_UNorm;
                            eyeTexture.Texture2DDescription.SampleDescription = new SampleDescription(1, 0);
                            eyeTexture.Texture2DDescription.Usage = ResourceUsage.Default;
                            eyeTexture.Texture2DDescription.CpuAccessFlags = CpuAccessFlags.None;
                            eyeTexture.Texture2DDescription.BindFlags = BindFlags.ShaderResource | BindFlags.RenderTarget;

                            // Convert the SharpDX texture description to the Oculus texture swap chain description.
                            TextureSwapChainDesc textureSwapChainDesc = SharpDXHelpers.CreateTextureSwapChainDescription(eyeTexture.Texture2DDescription);

                            // Create a texture swap chain, which will contain the textures to render to, for the current eye.
                            IntPtr textureSwapChainPtr;

                            result = OVR.CreateTextureSwapChainDX(sessionPtr, device.NativePointer, ref textureSwapChainDesc, out textureSwapChainPtr);
                            WriteErrorDetails(OVR, result, "Failed to create swap chain.");

                            eyeTexture.SwapTextureSet = new TextureSwapChain(OVR, sessionPtr, textureSwapChainPtr);


                            // Retrieve the number of buffers of the created swap chain.
                            int textureSwapChainBufferCount;
                            result = eyeTexture.SwapTextureSet.GetLength(out textureSwapChainBufferCount);
                            WriteErrorDetails(OVR, result, "Failed to retrieve the number of buffers of the created swap chain.");

                            // Create room for each DirectX texture in the SwapTextureSet.
                            eyeTexture.Textures = new Texture2D[textureSwapChainBufferCount];
                            eyeTexture.RenderTargetViews = new RenderTargetView[textureSwapChainBufferCount];

                            // Create a texture 2D and a render target view, for each unmanaged texture contained in the SwapTextureSet.
                            for (int textureIndex = 0; textureIndex < textureSwapChainBufferCount; textureIndex++)
                            {
                                // Retrieve the Direct3D texture contained in the Oculus TextureSwapChainBuffer.
                                IntPtr swapChainTextureComPtr = IntPtr.Zero;
                                result = eyeTexture.SwapTextureSet.GetBufferDX(textureIndex, textureInterfaceId, out swapChainTextureComPtr);
                                WriteErrorDetails(OVR, result, "Failed to retrieve a texture from the created swap chain.");

                                // Create a managed Texture2D, based on the unmanaged texture pointer.
                                eyeTexture.Textures[textureIndex] = new Texture2D(swapChainTextureComPtr);

                                // Create a render target view for the current Texture2D.
                                eyeTexture.RenderTargetViews[textureIndex] = new RenderTargetView(device, eyeTexture.Textures[textureIndex]);
                            }

                            // Define the depth buffer, at the size recommended for the eye texture.
                            eyeTexture.DepthBufferDescription = new Texture2DDescription();
                            eyeTexture.DepthBufferDescription.Format = Format.D32_Float;
                            eyeTexture.DepthBufferDescription.Width = eyeTexture.TextureSize.Width;
                            eyeTexture.DepthBufferDescription.Height = eyeTexture.TextureSize.Height;
                            eyeTexture.DepthBufferDescription.ArraySize = 1;
                            eyeTexture.DepthBufferDescription.MipLevels = 1;
                            eyeTexture.DepthBufferDescription.SampleDescription = new SampleDescription(1, 0);
                            eyeTexture.DepthBufferDescription.Usage = ResourceUsage.Default;
                            eyeTexture.DepthBufferDescription.BindFlags = BindFlags.DepthStencil;
                            eyeTexture.DepthBufferDescription.CpuAccessFlags = CpuAccessFlags.None;
                            eyeTexture.DepthBufferDescription.OptionFlags = ResourceOptionFlags.None;

                            // Create the depth buffer.
                            eyeTexture.DepthBuffer = new Texture2D(device, eyeTexture.DepthBufferDescription);
                            eyeTexture.DepthStencilView = new DepthStencilView(device, eyeTexture.DepthBuffer);

                            // Specify the texture to show on the HMD.
                            if (eyeIndex == 0)
                            {
                                layerEyeFov.ColorTextureLeft = eyeTexture.SwapTextureSet.TextureSwapChainPtr;
                                layerEyeFov.ViewportLeft.Position = new Vector2i(0, 0);
                                layerEyeFov.ViewportLeft.Size = eyeTexture.TextureSize;
                                layerEyeFov.FovLeft = eyeTexture.FieldOfView;
                            }
                            else
                            {
                                layerEyeFov.ColorTextureRight = eyeTexture.SwapTextureSet.TextureSwapChainPtr;
                                layerEyeFov.ViewportRight.Position = new Vector2i(0, 0);
                                layerEyeFov.ViewportRight.Size = eyeTexture.TextureSize;
                                layerEyeFov.FovRight = eyeTexture.FieldOfView;
                            }
                        }

                        MirrorTextureDesc mirrorTextureDescription = new MirrorTextureDesc();
                        mirrorTextureDescription.Format = TextureFormat.R8G8B8A8_UNorm_SRgb;
                        mirrorTextureDescription.Width = SurfaceWidth;
                        mirrorTextureDescription.Height = SurfaceHeight;
                        mirrorTextureDescription.MiscFlags = TextureMiscFlags.None;

                        // Create the texture used to display the rendered result on the computer monitor.
                        IntPtr mirrorTexturePtr;
                        result = OVR.CreateMirrorTextureDX(sessionPtr, device.NativePointer, ref mirrorTextureDescription, out mirrorTexturePtr);
                        WriteErrorDetails(OVR, result, "Failed to create mirror texture.");

                        mirrorTexture = new MirrorTexture(OVR, sessionPtr, mirrorTexturePtr);


                        // Retrieve the Direct3D texture contained in the Oculus MirrorTexture.
                        IntPtr mirrorTextureComPtr = IntPtr.Zero;
                        result = mirrorTexture.GetBufferDX(textureInterfaceId, out mirrorTextureComPtr);
                        WriteErrorDetails(OVR, result, "Failed to retrieve the texture from the created mirror texture buffer.");

                        // Create a managed Texture2D, based on the unmanaged texture pointer.
                        mirrorTextureD3D = new Texture2D(mirrorTextureComPtr);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.ToString());
                    }
































                        /*
                        //---------------------------------------------------------\\ OCULUS WRAP

                        OVR = OvrWrap.Create(); //Ab3d.DXEngine.OculusWrap    

                        // Check if OVR service is running
                        var detectResult = OVR.Detect(0);

                        if (!detectResult.IsOculusServiceRunning)
                        {
                            MessageBox((IntPtr)0, "Oculus service is not running", "Oculus Error", 0);
                        }
                        else
                        {
                            //MessageBox((IntPtr)0, "Oculus service is running", "Oculus Message", 0);
                        }
                        // Check if Head Mounter Display is connected
                        if (!detectResult.IsOculusHMDConnected)
                        {
                            MessageBox((IntPtr)0, "Oculus HMD (Head Mounter Display) is not connected", "Oculus Error", 0);
                        }
                        else
                        {
                            //MessageBox((IntPtr)0, "Oculus HMD (Head Mounter Display) is connected", "Oculus Message", 0);
                        }


                        try
                        {
                            //gotta find a way to load the oculus rift service manually maybe.

                            //------------------------------FOR AB3D DX ENGINE Device.
                            _oculusRiftVirtualRealityProvider = new OculusWrapVirtualRealityProvider(OVR, multisamplingCount: 4);
                            //hmdDesc = _oculusRiftVirtualRealityProvider.HmdDescription;

                            try
                            {
                                // Then we initialize Oculus OVR and create a new DXDevice that uses the same adapter (graphic card) as Oculus Rift
                                _dxDevice = _oculusRiftVirtualRealityProvider.InitializeOvrAndDXDevice(requestedOculusSdkMinorVersion: 17);
                            }
                            catch (Exception ex)
                            {
                                //System.Windows.MessageBox.Show("Failed to initialize the Oculus runtime library.\r\nError: " + ex.Message, "Oculus error", MessageBoxButton.OK, MessageBoxImage.Error);
                                //return;
                                //MessageBox((IntPtr)0, "Failed to initialize the Oculus runtime library.\r\nError: " + ex.Message, "Oculus error", 0);
                            }


                            OVR.RecenterTrackingOrigin(_oculusRiftVirtualRealityProvider.SessionPtr);


                            //OVR.GetTextureSwapChainBufferDX(_oculusRiftVirtualRealityProvider.SessionPtr,);

                            sessionPtr = _oculusRiftVirtualRealityProvider.SessionPtr;
                            hmdDesc = OVR.GetHmdDesc(sessionPtr);*/
                        //----------------------FOR AB3D DX ENGINE Device.

                        //SessionStatus sessionStat = _oculusRiftVirtualRealityProvider.LastSessionStatus;
                        //var res = OVR.GetSessionStatus(sessionPtr, ref sessionStat);

                        /*if (sessionStat.foc)
                        {

                        }


                        //WriteErrorDetails(OVR, res, "Session Status");

                        Device = _dxDevice.Device;


                        device = Device;
                        //hasinit = 1;
                        DeviceContext = Device.ImmediateContext;

                        if (Device == null)
                        {
                            MessageBox((IntPtr)0, "null", "sccoresystems Error", 0);
                        }




                        // Create DirectX drawing device.
                        // Create DirectX Graphics Interface factory, used to create the swap chain.
                        using (var factory = new SharpDX.DXGI.Factory1())
                        {
                            //factory.MakeWindowAssociation(Program.GameHandle, WindowAssociationFlags.IgnoreAll);

                            // Define the properties of the swap chain.
                            SwapChainDescription swapChainDescription = new SwapChainDescription();
                            swapChainDescription.BufferCount = 1;
                            swapChainDescription.IsWindowed = true;
                            swapChainDescription.OutputHandle = sccsconsole.sccsconsolecore.handle;
                            swapChainDescription.SampleDescription = new SampleDescription(1, 0);
                            swapChainDescription.Usage = Usage.RenderTargetOutput | Usage.ShaderInput;
                            swapChainDescription.SwapEffect = SwapEffect.Sequential;
                            swapChainDescription.Flags = SwapChainFlags.AllowModeSwitch;
                            swapChainDescription.ModeDescription.Width = SurfaceWidth;
                            swapChainDescription.ModeDescription.Height = SurfaceHeight;
                            swapChainDescription.ModeDescription.Format = Format.R8G8B8A8_UNorm;
                            swapChainDescription.ModeDescription.RefreshRate.Numerator = 0;
                            swapChainDescription.ModeDescription.RefreshRate.Denominator = 1;
                            // Create the swap chain.
                            SwapChain = new SwapChain(factory, Device, swapChainDescription);
                            factory.Dispose();


                            // Retrieve the back buffer of the swap chain.
                            BackBuffer = SwapChain.GetBackBuffer<SharpDX.Direct3D11.Texture2D>(0);
                            _renderTargetView = new RenderTargetView(Device, BackBuffer);

                            // Create a depth buffer, using the same width and height as the back buffer.
                            Texture2DDescription depthBufferDescription = new Texture2DDescription();
                            depthBufferDescription.Format = Format.D32_Float;
                            depthBufferDescription.ArraySize = 1;
                            depthBufferDescription.MipLevels = 1;
                            depthBufferDescription.Width = SurfaceWidth;
                            depthBufferDescription.Height = SurfaceHeight;
                            depthBufferDescription.SampleDescription = new SampleDescription(1, 0);
                            depthBufferDescription.Usage = ResourceUsage.Default;
                            depthBufferDescription.BindFlags = BindFlags.DepthStencil;
                            depthBufferDescription.CpuAccessFlags = CpuAccessFlags.None;
                            depthBufferDescription.OptionFlags = ResourceOptionFlags.None;

                            // Define how the depth buffer will be used to filter out objects, based on their distance from the viewer.
                            DepthStencilStateDescription depthStencilStateDescription = new DepthStencilStateDescription();
                            depthStencilStateDescription.IsDepthEnabled = true;
                            depthStencilStateDescription.DepthComparison = Comparison.Less;
                            depthStencilStateDescription.DepthWriteMask = DepthWriteMask.Zero;

                            depthBuffer = new SharpDX.Direct3D11.Texture2D(Device, depthBufferDescription);
                            _depthStencilView = new DepthStencilView(Device, depthBuffer);
                            depthStencilState = new SharpDX.Direct3D11.DepthStencilState(Device, depthStencilStateDescription);
                            var viewport = new SharpDX.Viewport(0, 0, hmdDesc.Resolution.Width, hmdDesc.Resolution.Height, 0.0f, 1.0f);

                            DeviceContext.OutputMerger.SetDepthStencilState(depthStencilState);
                            DeviceContext.OutputMerger.SetRenderTargets(_depthStencilView, _renderTargetView);

                            // Setup the raster description which will determine how and what polygon will be drawn.
                            RasterizerStateDescription rasterDesc = new RasterizerStateDescription()
                            {
                                IsAntialiasedLineEnabled = false,
                                CullMode = CullMode.Front,
                                DepthBias = 0,
                                DepthBiasClamp = .0f,
                                IsDepthClipEnabled = true,
                                FillMode = FillMode.Solid,
                                IsFrontCounterClockwise = false,
                                IsMultisampleEnabled = true,
                                IsScissorEnabled = false,
                                SlopeScaledDepthBias = .0f
                            };

                            // Create the rasterizer state from the description we just filled out.
                            RasterState = new RasterizerState(Device, rasterDesc);

                            // Now set the rasterizer state.
                            DeviceContext.Rasterizer.State = RasterState;

                            /*var rasterDesc = new RasterizerStateDescription()
                            {
                                IsAntialiasedLineEnabled = false,
                                CullMode = CullMode.Back,
                                DepthBias = 0,
                                DepthBiasClamp = 0.0f,
                                IsDepthClipEnabled = true,
                                FillMode = SharpDX.Direct3D11.FillMode.Solid,
                                IsFrontCounterClockwise = false,
                                IsMultisampleEnabled = false,
                                IsScissorEnabled = false,
                                SlopeScaledDepthBias = 0.0f
                            };

                            // Create the rasterizer state from the description we just filled out.
                            RasterState = new RasterizerState(_device, rasterDesc);

                            // Now set the rasterizer state.
                            _device.ImmediateContext.Rasterizer.State = RasterState;


                            DeviceContext.Rasterizer.SetViewport(viewport);

                            // Retrieve the DXGI device, in order to set the maximum frame latency.
                            using (SharpDX.DXGI.Device1 dxgiDevice = Device.QueryInterface<SharpDX.DXGI.Device1>())
                            {
                                dxgiDevice.MaximumFrameLatency = 1;
                            }

                            layerEyeFov = new LayerEyeFov();
                            layerEyeFov.Header.Type = LayerType.EyeFov;
                            layerEyeFov.Header.Flags = LayerFlags.None;

                            // Create a set of layers to submit.
                            eyeTextures = new EyeTexture[2];

                            for (int eyeIndex = 0; eyeIndex < 2; eyeIndex++)
                            {
                                EyeType eye = (EyeType)eyeIndex;
                                var eyeTexture = new EyeTexture();
                                eyeTextures[eyeIndex] = eyeTexture;

                                // Retrieve size and position of the texture for the current eye.
                                eyeTexture.FieldOfView = hmdDesc.DefaultEyeFov[eyeIndex];
                                eyeTexture.TextureSize = OVR.GetFovTextureSize(sessionPtr, eye, hmdDesc.DefaultEyeFov[eyeIndex], 1.0f);
                                eyeTexture.RenderDescription = OVR.GetRenderDesc(sessionPtr, eye, hmdDesc.DefaultEyeFov[eyeIndex]);
                                eyeTexture.HmdToEyeViewOffset = eyeTexture.RenderDescription.HmdToEyePose.Position;
                                eyeTexture.ViewportSize.Position = new Vector2i(0, 0);
                                eyeTexture.ViewportSize.Size = eyeTexture.TextureSize;
                                eyeTexture.Viewport = new SharpDX.Viewport(0, 0, eyeTexture.TextureSize.Width, eyeTexture.TextureSize.Height, 0.0f, 1.0f);

                                // Define a texture at the size recommended for the eye texture.
                                eyeTexture.Texture2DDescription = new Texture2DDescription();
                                eyeTexture.Texture2DDescription.Width = eyeTexture.TextureSize.Width;
                                eyeTexture.Texture2DDescription.Height = eyeTexture.TextureSize.Height;
                                eyeTexture.Texture2DDescription.ArraySize = 1;
                                eyeTexture.Texture2DDescription.MipLevels = 1;
                                eyeTexture.Texture2DDescription.Format = Format.R8G8B8A8_UNorm;
                                eyeTexture.Texture2DDescription.SampleDescription = new SampleDescription(1, 0);
                                eyeTexture.Texture2DDescription.Usage = ResourceUsage.Default;
                                eyeTexture.Texture2DDescription.CpuAccessFlags = CpuAccessFlags.None;
                                eyeTexture.Texture2DDescription.BindFlags = BindFlags.ShaderResource | BindFlags.RenderTarget;

                                // Convert the SharpDX texture description to the Oculus texture swap chain description.
                                TextureSwapChainDesc textureSwapChainDesc = SharpDXHelpers.CreateTextureSwapChainDescription(eyeTexture.Texture2DDescription);

                                // Create a texture swap chain, which will contain the textures to render to, for the current eye.
                                IntPtr textureSwapChainPtr;

                                result = OVR.CreateTextureSwapChainDX(sessionPtr, Device.NativePointer, ref textureSwapChainDesc, out textureSwapChainPtr);
                                WriteErrorDetails(OVR, result, "Failed to create swap chain.");

                                eyeTexture.SwapTextureSet = new TextureSwapChain(OVR, sessionPtr, textureSwapChainPtr);


                                // Retrieve the number of buffers of the created swap chain.
                                int textureSwapChainBufferCount;
                                result = eyeTexture.SwapTextureSet.GetLength(out textureSwapChainBufferCount);
                                WriteErrorDetails(OVR, result, "Failed to retrieve the number of buffers of the created swap chain.");

                                // Create room for each DirectX texture in the SwapTextureSet.
                                eyeTexture.Textures = new SharpDX.Direct3D11.Texture2D[textureSwapChainBufferCount];
                                eyeTexture.RenderTargetViews = new RenderTargetView[textureSwapChainBufferCount];

                                // Create a texture 2D and a render target view, for each unmanaged texture contained in the SwapTextureSet.
                                for (int textureIndex = 0; textureIndex < textureSwapChainBufferCount; textureIndex++)
                                {
                                    // Retrieve the Direct3D texture contained in the Oculus TextureSwapChainBuffer.
                                    IntPtr swapChainTextureComPtr = IntPtr.Zero;
                                    result = eyeTexture.SwapTextureSet.GetBufferDX(textureIndex, textureInterfaceId, out swapChainTextureComPtr);
                                    WriteErrorDetails(OVR, result, "Failed to retrieve a texture from the created swap chain.");

                                    // Create a managed Texture2D, based on the unmanaged texture pointer.
                                    eyeTexture.Textures[textureIndex] = new SharpDX.Direct3D11.Texture2D(swapChainTextureComPtr);

                                    // Create a render target view for the current Texture2D.
                                    eyeTexture.RenderTargetViews[textureIndex] = new RenderTargetView(Device, eyeTexture.Textures[textureIndex]);
                                }

                                // Define the depth buffer, at the size recommended for the eye texture.
                                eyeTexture.DepthBufferDescription = new Texture2DDescription();
                                eyeTexture.DepthBufferDescription.Format = Format.D32_Float;
                                eyeTexture.DepthBufferDescription.Width = eyeTexture.TextureSize.Width;
                                eyeTexture.DepthBufferDescription.Height = eyeTexture.TextureSize.Height;
                                eyeTexture.DepthBufferDescription.ArraySize = 1;
                                eyeTexture.DepthBufferDescription.MipLevels = 1;
                                eyeTexture.DepthBufferDescription.SampleDescription = new SampleDescription(1, 0);
                                eyeTexture.DepthBufferDescription.Usage = ResourceUsage.Default;
                                eyeTexture.DepthBufferDescription.BindFlags = BindFlags.DepthStencil;
                                eyeTexture.DepthBufferDescription.CpuAccessFlags = CpuAccessFlags.None;
                                eyeTexture.DepthBufferDescription.OptionFlags = ResourceOptionFlags.None;

                                // Create the depth buffer.
                                eyeTexture.DepthBuffer = new SharpDX.Direct3D11.Texture2D(Device, eyeTexture.DepthBufferDescription);
                                eyeTexture.DepthStencilView = new DepthStencilView(Device, eyeTexture.DepthBuffer);

                                // Specify the texture to show on the HMD.
                                if (eyeIndex == 0)
                                {
                                    layerEyeFov.ColorTextureLeft = eyeTexture.SwapTextureSet.TextureSwapChainPtr;
                                    layerEyeFov.ViewportLeft.Position = new Vector2i(0, 0);
                                    layerEyeFov.ViewportLeft.Size = eyeTexture.TextureSize;
                                    layerEyeFov.FovLeft = eyeTexture.FieldOfView;
                                }
                                else
                                {
                                    layerEyeFov.ColorTextureRight = eyeTexture.SwapTextureSet.TextureSwapChainPtr;
                                    layerEyeFov.ViewportRight.Position = new Vector2i(0, 0);
                                    layerEyeFov.ViewportRight.Size = eyeTexture.TextureSize;
                                    layerEyeFov.FovRight = eyeTexture.FieldOfView;
                                }
                            }

                            MirrorTextureDesc mirrorTextureDescription = new MirrorTextureDesc();
                            mirrorTextureDescription.Format = TextureFormat.R8G8B8A8_UNorm_SRgb;
                            mirrorTextureDescription.Width = SurfaceWidth;
                            mirrorTextureDescription.Height = SurfaceHeight;
                            mirrorTextureDescription.MiscFlags = TextureMiscFlags.None;

                            // Create the texture used to display the rendered result on the computer monitor.
                            IntPtr mirrorTexturePtr;
                            result = OVR.CreateMirrorTextureDX(sessionPtr, Device.NativePointer, ref mirrorTextureDescription, out mirrorTexturePtr);
                            WriteErrorDetails(OVR, result, "Failed to create mirror texture.");

                            mirrorTexture = new MirrorTexture(OVR, sessionPtr, mirrorTexturePtr);

                            // Retrieve the Direct3D texture contained in the Oculus MirrorTexture.
                            IntPtr mirrorTextureComPtr = IntPtr.Zero;
                            result = mirrorTexture.GetBufferDX(textureInterfaceId, out mirrorTextureComPtr);
                            WriteErrorDetails(OVR, result, "Failed to retrieve the texture from the created mirror texture buffer.");

                            // Create a managed Texture2D, based on the unmanaged texture pointer.
                            mirrorTextureD3D = new SharpDX.Direct3D11.Texture2D(mirrorTextureComPtr);
























                            /*
                            var swapChainDesc = new SwapChainDescription()
                            {
                                OutputHandle = Program.consoleHandle,
                                BufferCount = 1,
                                Flags = SwapChainFlags.AllowModeSwitch,
                                IsWindowed = true,
                                ModeDescription = new ModeDescription(SurfaceWidth, SurfaceHeight, new Rational(60, 1), Format.B8G8R8A8_UNorm),
                                SampleDescription = new SampleDescription(1, 0),
                                SwapEffect = SwapEffect.Discard,
                                Usage = Usage.RenderTargetOutput | Usage.Shared

                            };

                            Device someDevice;
                            SwapChain swapChain;
                            Device.CreateWithSwapChain(DriverType.Hardware, DeviceCreationFlags.None, swapChainDesc, out someDevice, out swapChain);


                            Device = someDevice;
                            device = Device;
                            DeviceContext = device.ImmediateContext;
                            SwapChain = swapChain;

                            //bool userResized = true;
                            //Texture2D backBuffer = null;
                            //RenderTargetView renderView = null;
                            //Texture2D depthBuffer = null;
                            //DepthStencilView depthView = null;
                            // Dispose all previous allocated resources
                            //Utilities.Dispose(ref backBuffer);
                            //Utilities.Dispose(ref renderView);
                            //Utilities.Dispose(ref depthBuffer);
                            //Utilities.Dispose(ref depthView);

                            // Get the pointer to the back buffer.
                            BackBuffer = Texture2D.FromSwapChain<Texture2D>(SwapChain, 0);

                            // Create the render target view with the back buffer pointer.
                            _renderTargetView = new RenderTargetView(device, BackBuffer);


                            // Initialize and set up the description of the depth buffer.
                            var _depthBufferDesc = new Texture2DDescription()
                            {
                                Width = SurfaceWidth,
                                Height = SurfaceHeight,
                                MipLevels = 1,
                                ArraySize = 1,
                                Format = Format.D24_UNorm_S8_UInt,
                                SampleDescription = new SampleDescription(1, 0),
                                Usage = ResourceUsage.Default,
                                BindFlags = BindFlags.DepthStencil,
                                CpuAccessFlags = CpuAccessFlags.None,
                                OptionFlags = ResourceOptionFlags.None

                            };

                            // Create the texture for the depth buffer using the filled out description.
                            var _depthStencilBuffer = new Texture2D(device, _depthBufferDesc);


                            // Initialize and set up the depth stencil view.
                            var depthStencilViewDesc = new DepthStencilViewDescription()
                            {
                                Format = Format.D24_UNorm_S8_UInt,
                                Dimension = DepthStencilViewDimension.Texture2D,
                                Texture2D = new DepthStencilViewDescription.Texture2DResource()
                                {
                                    MipSlice = 0
                                }
                            };

                            // Create the depth stencil view.
                            _depthStencilView = new DepthStencilView(device, DepthStencilBuffer, depthStencilViewDesc);

                            // Bind the render target view and depth stencil buffer to the output render pipeline.
                            DeviceContext.OutputMerger.SetTargets(_depthStencilView, _renderTargetView);

                            // Setup the raster description which will determine how and what polygon will be drawn.
                            var rasterDesctwo = new RasterizerStateDescription()
                            {
                                IsAntialiasedLineEnabled = false,
                                CullMode = CullMode.Back,
                                DepthBias = 0,
                                DepthBiasClamp = 0.0f,
                                IsDepthClipEnabled = true,
                                FillMode = SharpDX.Direct3D11.FillMode.Solid,
                                IsFrontCounterClockwise = false,
                                IsMultisampleEnabled = false,
                                IsScissorEnabled = false,
                                SlopeScaledDepthBias = 0.0f
                            };

                            // Create the rasterizer state from the description we just filled out.
                            RasterState = new RasterizerState(device, rasterDesctwo);

                            // Now set the rasterizer state.
                            DeviceContext.Rasterizer.State = RasterState;

                            // Setup and create the viewport for rendering.
                            var ViewPort = new ViewportF()
                            {
                                Width = SurfaceWidth,
                                Height = SurfaceHeight,
                                MinDepth = 0.0f,
                                MaxDepth = 1.0f,
                                X = 0.0f,
                                Y = 0.0f
                            };
                            DeviceContext.Rasterizer.SetViewport(ViewPort);
                            //DeviceContext.Rasterizer.SetViewport(new Viewport(0, 0, SurfaceWidth, SurfaceHeight, 0.0f, 1.0f));


                            // Setup and create the projection matrix.
                            ProjectionMatrix = SharpDX.Matrix.PerspectiveFovLH((float)(Math.PI / 4), (((float)SurfaceWidth / (float)SurfaceHeight)), DSystemConfiguration.ScreenNear, DSystemConfiguration.ScreenDepth);





                            controllerTypeRTouch = ControllerType.RTouch;
                            controllerTypeLTouch = ControllerType.LTouch;

                            //---------------------------------------------------------\\ OCULUS WRAP

                            OVR = OvrWrap.Create(); //Ab3d.DXEngine.OculusWrap    

                            // Check if OVR service is running
                            var detectResulttwo = OVR.Detect(0);

                            if (!detectResult.IsOculusServiceRunning)
                            {
                                MessageBox((IntPtr)0, "Oculus service is not running", "Oculus Error", 0);
                            }
                            else
                            {
                                //MessageBox((IntPtr)0, "Oculus service is running", "Oculus Message", 0);
                            }
                            // Check if Head Mounter Display is connected
                            if (!detectResult.IsOculusHMDConnected)
                            {
                                MessageBox((IntPtr)0, "Oculus HMD (Head Mounter Display) is not connected", "Oculus Error", 0);
                            }
                            else
                            {
                                //MessageBox((IntPtr)0, "Oculus HMD (Head Mounter Display) is connected", "Oculus Message", 0);
                            }


                            try
                            {
                                //gotta find a way to load the oculus rift service manually maybe.

                                //------------------------------FOR AB3D DX ENGINE Device.
                                _oculusRiftVirtualRealityProvider = new OculusWrapVirtualRealityProvider(OVR, multisamplingCount: 4);
                                //hmdDesc = _oculusRiftVirtualRealityProvider.HmdDescription;

                                try
                                {
                                    // Then we initialize Oculus OVR and create a new DXDevice that uses the same adapter (graphic card) as Oculus Rift
                                    _dxDevice = _oculusRiftVirtualRealityProvider.InitializeOvrAndDXDevice(requestedOculusSdkMinorVersion: 17);
                                }
                                catch (Exception ex)
                                {
                                    //System.Windows.MessageBox.Show("Failed to initialize the Oculus runtime library.\r\nError: " + ex.Message, "Oculus error", MessageBoxButton.OK, MessageBoxImage.Error);
                                    //return;
                                    //MessageBox((IntPtr)0, "Failed to initialize the Oculus runtime library.\r\nError: " + ex.Message, "Oculus error", 0);
                                }


                                OVR.RecenterTrackingOrigin(_oculusRiftVirtualRealityProvider.SessionPtr);


                                //OVR.GetTextureSwapChainBufferDX(_oculusRiftVirtualRealityProvider.SessionPtr,);

                                sessionPtr = _oculusRiftVirtualRealityProvider.SessionPtr;
                                hmdDesc = OVR.GetHmdDesc(sessionPtr);
                                //----------------------FOR AB3D DX ENGINE Device.

                                SessionStatus sessionStattwo = _oculusRiftVirtualRealityProvider.LastSessionStatus;
                                var restwo = OVR.GetSessionStatus(sessionPtr, ref sessionStattwo);
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine(ex.ToString());
                            }




                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.ToString());
                    }*/
                        //DeviceContext.ClearState();
                        //DeviceContext.Flush();
                    }
                else if (!_useOculusRift)
                {

                    var swapChainDesc = new SwapChainDescription()
                    {
                        OutputHandle = sccsconsole.sccsconsolecore.handle,
                        BufferCount = 1,
                        Flags = SwapChainFlags.AllowModeSwitch,
                        IsWindowed = true,
                        ModeDescription = new ModeDescription(SurfaceWidth, SurfaceHeight, new Rational(60, 1), Format.B8G8R8A8_UNorm),
                        SampleDescription = new SampleDescription(1, 0),
                        SwapEffect = SwapEffect.Discard,
                        Usage = Usage.RenderTargetOutput | Usage.Shared

                    };

                    Device someDevice;
                    SwapChain swapChain;
                    Device.CreateWithSwapChain(DriverType.Hardware, DeviceCreationFlags.None, swapChainDesc, out someDevice, out swapChain);


                    Device = someDevice;
                    device = Device;
                    //hasinit = 1;
                    DeviceContext = device.ImmediateContext;
                    SwapChain = swapChain;

                    //bool userResized = true;
                    //Texture2D backBuffer = null;
                    //RenderTargetView renderView = null;
                    //Texture2D depthBuffer = null;
                    //DepthStencilView depthView = null;
                    // Dispose all previous allocated resources
                    //Utilities.Dispose(ref backBuffer);
                    //Utilities.Dispose(ref renderView);
                    //Utilities.Dispose(ref depthBuffer);
                    //Utilities.Dispose(ref depthView);

                    // Get the pointer to the back buffer.
                    BackBuffer = Texture2D.FromSwapChain<Texture2D>(SwapChain, 0);

                    // Create the render target view with the back buffer pointer.
                    _renderTargetView = new RenderTargetView(device, BackBuffer);


                    // Initialize and set up the description of the depth buffer.
                    var _depthBufferDesc = new Texture2DDescription()
                    {
                        Width = SurfaceWidth,
                        Height = SurfaceHeight,
                        MipLevels = 1,
                        ArraySize = 1,
                        Format = Format.D24_UNorm_S8_UInt,
                        SampleDescription = new SampleDescription(1, 0),
                        Usage = ResourceUsage.Default,
                        BindFlags = BindFlags.DepthStencil,
                        CpuAccessFlags = CpuAccessFlags.None,
                        OptionFlags = ResourceOptionFlags.None

                    };

                    // Create the texture for the depth buffer using the filled out description.
                    var _depthStencilBuffer = new Texture2D(device, _depthBufferDesc);


                    // Initialize and set up the depth stencil view.
                    var depthStencilViewDesc = new DepthStencilViewDescription()
                    {
                        Format = Format.D24_UNorm_S8_UInt,
                        Dimension = DepthStencilViewDimension.Texture2D,
                        Texture2D = new DepthStencilViewDescription.Texture2DResource()
                        {
                            MipSlice = 0
                        }
                    };

                    // Create the depth stencil view.
                    depthStencilView = new DepthStencilView(device, DepthStencilBuffer, depthStencilViewDesc);

                    // Bind the render target view and depth stencil buffer to the output render pipeline.
                    DeviceContext.OutputMerger.SetTargets(depthStencilView, _renderTargetView);

                    // Setup the raster description which will determine how and what polygon will be drawn.
                    var rasterDesc = new RasterizerStateDescription()
                    {
                        IsAntialiasedLineEnabled = false,
                        CullMode = CullMode.Back,
                        DepthBias = 0,
                        DepthBiasClamp = 0.0f,
                        IsDepthClipEnabled = true,
                        FillMode = SharpDX.Direct3D11.FillMode.Solid,
                        IsFrontCounterClockwise = false,
                        IsMultisampleEnabled = false,
                        IsScissorEnabled = false,
                        SlopeScaledDepthBias = 0.0f
                    };

                    // Create the rasterizer state from the description we just filled out.
                    RasterState = new RasterizerState(device, rasterDesc);

                    // Now set the rasterizer state.
                    DeviceContext.Rasterizer.State = RasterState;

                    // Setup and create the viewport for rendering.
                    var ViewPort = new ViewportF()
                    {
                        Width = SurfaceWidth,
                        Height = SurfaceHeight,
                        MinDepth = 0.0f,
                        MaxDepth = 1.0f,
                        X = 0.0f,
                        Y = 0.0f
                    };
                    DeviceContext.Rasterizer.SetViewport(ViewPort);
                    //DeviceContext.Rasterizer.SetViewport(new Viewport(0, 0, SurfaceWidth, SurfaceHeight, 0.0f, 1.0f));


                    // Setup and create the projection matrix.
                    ProjectionMatrix = SharpDX.Matrix.PerspectiveFovLH((float)(Math.PI / 4), (((float)SurfaceWidth / (float)SurfaceHeight)), DSystemConfiguration.ScreenNear, DSystemConfiguration.ScreenDepth);



                    



                    // Initialize the world matrix to the identity matrix.
                    //Worldm = SharpDX.Matrix.Identity;
                    /*
                    // SwapChain description
                    var desc = new SwapChainDescription()
                    {
                        BufferCount = 1,
                        ModeDescription =
                            new ModeDescription(SurfaceWidth, SurfaceHeight, new Rational(60, 1), Format.R8G8B8A8_UNorm),
                        IsWindowed = true,
                        OutputHandle = Program.consoleHandle,
                        SampleDescription = new SampleDescription(1, 0),
                        SwapEffect = SwapEffect.Sequential,// SwapEffect.Discard,
                        Usage = Usage.RenderTargetOutput | Usage.ShaderInput //Usage.RenderTargetOutput
                    };


                    /*SwapChainDescription swapChainDescription = new SwapChainDescription();
                    swapChainDescription.BufferCount = 1;
                    swapChainDescription.IsWindowed = true;
                    swapChainDescription.OutputHandle = Program.consoleHandle;
                    swapChainDescription.SampleDescription = new SampleDescription(1, 0);
                    swapChainDescription.Usage = Usage.RenderTargetOutput | Usage.ShaderInput;
                    swapChainDescription.SwapEffect = SwapEffect.Sequential;
                    swapChainDescription.Flags = SwapChainFlags.AllowModeSwitch;
                    swapChainDescription.ModeDescription.Width = SurfaceWidth;
                    swapChainDescription.ModeDescription.Height = SurfaceHeight;
                    swapChainDescription.ModeDescription.Format = Format.R8G8B8A8_UNorm;
                    swapChainDescription.ModeDescription.RefreshRate.Numerator = 0;
                    swapChainDescription.ModeDescription.RefreshRate.Denominator = 1;



                    // Used for debugging dispose object references
                    // Configuration.EnableObjectTracking = true;

                    // Disable throws on shader compilation errors
                    //Configuration.ThrowOnShaderCompileError = false;

                    // Create Device and SwapChain

                    Device someDevice;
                    SwapChain swapChain;
                    Device.CreateWithSwapChain(DriverType.Hardware, DeviceCreationFlags.None, desc, out someDevice, out swapChain);
                    DeviceContext = device.ImmediateContext;

                    Device = someDevice;
                    device = Device;



                    /*var contextPerThread = new DeviceContext[7];

                    var deferredContexts = new DeviceContext[7];
                    for (int i = 0; i < deferredContexts.Length; i++)
                    {
                        deferredContexts[i] = new DeviceContext(device);
                        contextPerThread[i] = context;
                    }



                    //var commandLists = new CommandList[7];
                    //CommandList[] frozenCommandLists = null;

                    // Check if driver is supporting natively CommandList
                    bool supportConcurentResources;
                    bool supportCommandList;
                    device.CheckThreadingSupport(out supportConcurentResources, out supportCommandList);




                    // Ignore all windows events
                    var factory = swapChain.GetParent<Factory>();
                    factory.MakeWindowAssociation(Program.consoleHandle, WindowAssociationFlags.IgnoreAll);



                    Matrix _WorldMatrix = Matrix.Identity;
                    var view = Matrix.LookAtLH(new Vector3(0, 0, -5), new Vector3(0, 0, 0), Vector3.UnitY);
                    Matrix proj = Matrix.Identity;

                    // Use clock
                    var clock = new Stopwatch();
                    clock.Start();

                    // Declare texture for rendering
                    bool userResized = true;
                    Texture2D backBuffer = null;
                    RenderTargetView renderView = null;
                    Texture2D depthBuffer = null;
                    DepthStencilView depthView = null;
                    // Dispose all previous allocated resources
                    Utilities.Dispose(ref backBuffer);
                    Utilities.Dispose(ref renderView);
                    Utilities.Dispose(ref depthBuffer);
                    Utilities.Dispose(ref depthView);

                    // Resize the backbuffer
                    swapChain.ResizeBuffers(desc.BufferCount, SurfaceWidth, SurfaceHeight, Format.Unknown, SwapChainFlags.None);

                    // Get the backbuffer from the swapchain
                    backBuffer = Texture2D.FromSwapChain<Texture2D>(swapChain, 0);

                    // Renderview on the backbuffer
                    renderView = new RenderTargetView(device, backBuffer);

                    // Create the depth buffer
                    depthBuffer = new Texture2D(device, new Texture2DDescription()
                    {
                        Format = Format.D32_Float_S8X24_UInt,
                        ArraySize = 1,
                        MipLevels = 1,
                        Width = SurfaceWidth,
                        Height = SurfaceHeight,
                        SampleDescription = new SampleDescription(1, 0),
                        Usage = ResourceUsage.Default,
                        BindFlags = BindFlags.DepthStencil,
                        CpuAccessFlags = CpuAccessFlags.None,
                        OptionFlags = ResourceOptionFlags.None
                    });

                    // Create the depth buffer view
                    depthView = new DepthStencilView(device, depthBuffer);

                    // Setup targets and viewport for rendering
                    DeviceContext.Rasterizer.SetViewport(new Viewport(0, 0, SurfaceWidth, SurfaceHeight, 0.0f, 1.0f));
                    DeviceContext.OutputMerger.SetTargets(depthView, renderView);

                    // Setup new projection matrix with correct aspect ratio
                    ProjectionMatrix = Matrix.PerspectiveFovLH((float)Math.PI / 4.0f, SurfaceWidth / (float)SurfaceHeight, 0.1f, 100.0f);*/
                    // We are done resizing
                }
                //return true;
            }
            catch
            {
                //return false;
            }








            /*
            // Initialize and set up the description of the depth buffer.
            var depthBufferDesc = new Texture2DDescription()
            {
                Width = Program.config.Width,
                Height = Program.config.Height,
                MipLevels = 1,
                ArraySize = 1,
                Format = Format.D24_UNorm_S8_UInt,
                SampleDescription = new SampleDescription(1, 0),
                Usage = ResourceUsage.Default,
                BindFlags = BindFlags.DepthStencil,
                CpuAccessFlags = CpuAccessFlags.None,
                OptionFlags = ResourceOptionFlags.None
            };

            // Create the texture for the depth buffer using the filled out description.
            DepthStencilBuffer = new Texture2D(device, depthBufferDesc);


            
            
            
            
            // Initialize and set up the description of the stencil state.
            var depthStencilDesc = new DepthStencilStateDescription()
            {
                IsDepthEnabled = true,
                DepthWriteMask = DepthWriteMask.All,
                DepthComparison = Comparison.Less,
                IsStencilEnabled = true,
                StencilReadMask = 0xFF,
                StencilWriteMask = 0xFF,
                // Stencil operation if pixel front-facing.
                FrontFace = new DepthStencilOperationDescription()
                {
                    FailOperation = StencilOperation.Keep,
                    DepthFailOperation = StencilOperation.Increment,
                    PassOperation = StencilOperation.Keep,
                    Comparison = Comparison.Always
                },
                // Stencil operation if pixel is back-facing.
                BackFace = new DepthStencilOperationDescription()
                {
                    FailOperation = StencilOperation.Keep,
                    DepthFailOperation = StencilOperation.Decrement,
                    PassOperation = StencilOperation.Keep,
                    Comparison = Comparison.Always
                }
            };

            // Create the depth stencil state.
            DepthStencilState = new DepthStencilState(Device, depthStencilDesc);








            //STRAIGHT COPY PASTE FROM C# RASTERTEK DAN6040. ALL CREDITS TO HIM. WOW HE IS SUCH A GOOD SCRIPTER. I AM MISSING TIME.

            // Create an orthographic projection matrix for 2D rendering.
            OrthoMatrix = Matrix.OrthoLH(Program.config.Width, Program.config.Height, DSystemConfiguration.ScreenNear, DSystemConfiguration.ScreenDepth);



            // Now create a second depth stencil state which turns off the Z buffer for 2D rendering. Added in Tutorial 11
            // The difference is that DepthEnable is set to false.
            // All other parameters are the same as the other depth stencil state.
            var depthDisabledStencilDesc = new DepthStencilStateDescription()
            {
                IsDepthEnabled = false,
                DepthWriteMask = DepthWriteMask.All,
                DepthComparison = Comparison.Less,
                IsStencilEnabled = true,
                StencilReadMask = 0xFF,
                StencilWriteMask = 0xFF,
                // Stencil operation if pixel front-facing.
                FrontFace = new DepthStencilOperationDescription()
                {
                    FailOperation = StencilOperation.Keep,
                    DepthFailOperation = StencilOperation.Increment,
                    PassOperation = StencilOperation.Keep,
                    Comparison = Comparison.Always
                },
                // Stencil operation if pixel is back-facing.
                BackFace = new DepthStencilOperationDescription()
                {
                    FailOperation = StencilOperation.Keep,
                    DepthFailOperation = StencilOperation.Decrement,
                    PassOperation = StencilOperation.Keep,
                    Comparison = Comparison.Always
                }
            };

            // Create the depth stencil state.
            DepthDisabledStencilState = new DepthStencilState(Device, depthDisabledStencilDesc);














            // Create an alpha enabled blend state description.
            var blendStateDesc = new BlendStateDescription();
            blendStateDesc.RenderTarget[0].IsBlendEnabled = true;
            blendStateDesc.RenderTarget[0].SourceBlend = BlendOption.SourceAlpha;
            blendStateDesc.RenderTarget[0].DestinationBlend = BlendOption.InverseSourceAlpha;
            blendStateDesc.RenderTarget[0].BlendOperation = BlendOperation.Add;
            blendStateDesc.RenderTarget[0].SourceAlphaBlend = BlendOption.One;
            blendStateDesc.RenderTarget[0].DestinationAlphaBlend = BlendOption.Zero;
            blendStateDesc.RenderTarget[0].AlphaBlendOperation = BlendOperation.Add;
            blendStateDesc.RenderTarget[0].RenderTargetWriteMask = ColorWriteMaskFlags.All;

            // Create the blend state using the description.
            AlphaEnableBlendingState = new BlendState(device, blendStateDesc);

            // Modify the description to create an disabled blend state description.
            blendStateDesc.RenderTarget[0].IsBlendEnabled = false;

            // Create the blend state using the description.
            AlphaDisableBlendingState = new BlendState(device, blendStateDesc);
            */
























            try
            {
                Thread mainthreadupdate = new Thread(() =>
                {
                    jitter_sc[] jittersc = new jitter_sc[Program.physicsengineinstancex * Program.physicsengineinstancey * Program.physicsengineinstancez];
                    sc_message_object_jitter[][] scjittertasks = new sc_message_object_jitter[Program.physicsengineinstancex * Program.physicsengineinstancey * Program.physicsengineinstancez][];

                    sc_jitter_data scjitterdata = new sc_jitter_data();
                    scjitterdata.alloweddeactivation = Program.allowdeactivation;
                    scjitterdata.allowedpenetration = Program.worldallowedpenetration;
                    scjitterdata.width = Program.worldwidth;
                    scjitterdata.height = Program.worldheight;
                    scjitterdata.depth = Program.worlddepth;
                    scjitterdata.gravity = Program.worldgravity;
                    scjitterdata.smalliterations = Program.worldsmalliterations;
                    scjitterdata.iterations = Program.worlditerations;


                    for (int xx = 0; xx < Program.physicsengineinstancex; xx++)
                    {
                        for (int yy = 0; yy < Program.physicsengineinstancey; yy++)
                        {
                            for (int zz = 0; zz < Program.physicsengineinstancez; zz++)
                            {
                                var indexer00 = xx + Program.physicsengineinstancex * (yy + Program.physicsengineinstancey * zz);
                                //jitterphysics[indexer00] = DoSpecialThing();
                                scjittertasks[indexer00] = new sc_message_object_jitter[Program.worldwidth * Program.worldheight * Program.worlddepth];

                                for (int x = 0; x < Program.worldwidth; x++)
                                {
                                    for (int y = 0; y < Program.worldheight; y++)
                                    {
                                        for (int z = 0; z < Program.worlddepth; z++)
                                        {
                                            var indexer01 = x + Program.worldwidth * (y + Program.worldheight * z);
                                            scjittertasks[indexer00][indexer01] = new sc_message_object_jitter();
                                        }
                                    }
                                }
                            }
                        }
                    }


                    //Console.WriteLine("built0");
                    jittersc = createjitterinstances(jittersc, scjitterdata);

                    for (int xx = 0; xx < Program.physicsengineinstancex; xx++)
                    {
                        for (int yy = 0; yy < Program.physicsengineinstancey; yy++)
                        {
                            for (int zz = 0; zz < Program.physicsengineinstancez; zz++)
                            {
                                var indexer00 = xx + Program.physicsengineinstancex * (yy + Program.physicsengineinstancey * zz);


                                //if (jittersc.Length > 0)
                                //{
                                //    Console.WriteLine("built00");
                                //}
                                //Console.WriteLine("index: " + indexer00);
                                jittersc[indexer00]._sc_create_jitter_world(scjitterdata);


                                for (int x = 0; x < Program.worldwidth; x++)
                                {
                                    for (int y = 0; y < Program.worldheight; y++)
                                    {
                                        for (int z = 0; z < Program.worlddepth; z++)
                                        {
                                            var indexer1 = x + Program.worldwidth * (y + Program.worldheight * z);

                                            var world = jittersc[indexer00].return_world(indexer1);

                                            if (world == null)
                                            {
                                                Console.WriteLine("null");
                                            }
                                            else
                                            {
                                                //Console.WriteLine("!null");

                                                scjittertasks[indexer00][indexer1]._world_data = new object[2];
                                                scjittertasks[indexer00][indexer1]._work_index = -1;
                                                scjittertasks[indexer00][indexer1]._world_data[0] = world;
                                                //Console.WriteLine("index: " + indexer1);
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }

                    scjittertasks = init_update_variables(scjittertasks, Program.config, sccoresystems.sccsconsole.sccsconsolecore.handle, Program.sccsglobalsaccessor.SCCSCONSOLEWRITER);
                    


                    //Console.WriteLine("built1");

                    //nine's alternative oculus touch arduino key mapping for broken oculus touch devices that are too pricey even when used and purchased online.
                    //https://instructables.com/C-Serial-Communication-With-Arduino/
                    //string currentSerialPortInput = serialPort.ReadExisting();
                    //Console.WriteLine(currentSerialPortInput);


                    
                    if (Program.useArduinoOVRTouchKeymapper == 1)
                    {
                        serialPort = new System.IO.Ports.SerialPort();
                        serialPort.PortName = "COM3";
                        serialPort.BaudRate = 9600; //9600 //115200 //14400

                        serialPort.Open();
                        if (serialPort­.IsOpen)
                        {
                            Console.WriteLine("0status: " + serialPort­.IsOpen);
                        }
                    }
                    








                //serialPort.Open();

                //Console.WriteLine("1status: " + serialPort­.IsOpen);

                threadloop:

                    try
                    {
                        //for the sccsHandControllers
                        /*if (serialPort != null)
                        {
                            string msgg = serialPort.ReadTo("#");
                            //Console.Write(msgg);
                            scjittertasks[0][0].worlddata[1] = msgg;
                        }
                        else
                        {
                            scjittertasks[0][0].worlddata[1] = "";
                            MessageBox((IntPtr)0, "arduino device disconnected on COM3", "msg", 0);
                        }*/
                        //for the sccsHandControllers




                        scjittertasks = Update(jittersc, scjittertasks);
                    }
                    catch (Exception ex)
                    {
                        MessageBox((IntPtr)0, "" + ex.ToString(), "msg", 0);
                    }
                    Thread.Sleep(1);
                    goto threadloop;

                    //ShutDown();
                    //ShutDownGraphics();

                }, 0);

                mainthreadupdate.IsBackground = true;
                mainthreadupdate.SetApartmentState(ApartmentState.STA);
                mainthreadupdate.Start();

            }
            catch
            {

            }
            /*finally
            {

            }*/
            //somesccsscreencapture = new sccssharpdxscreencapture(0, 0, device);
        }
        //public int hasinit = 0;

        //public sccssharpdxscreencapture somesccsscreencapture;


        public void TurnOnAlphaBlending()
        {
            // Setup the blend factor.
            var blendFactor = new Color4(0, 0, 0, 0);

            // Turn on the alpha blending.
            DeviceContext.OutputMerger.SetBlendState(AlphaEnableBlendingState, blendFactor, -1);
        }

        public void TurnOffAlphaBlending()
        {
            // Setup the blend factor.
            var blendFactor = new Color4(0, 0, 0, 0);

            // Turn on the alpha blending.
            DeviceContext.OutputMerger.SetBlendState(AlphaDisableBlendingState, blendFactor, -1);
        }

        public void TurnZBufferOn()
        {
            DeviceContext.OutputMerger.SetDepthStencilState(DepthStencilState, 1);
        }

        public void TurnZBufferOff()
        {
            DeviceContext.OutputMerger.SetDepthStencilState(DepthDisabledStencilState, 1);
        }









        protected abstract SC_message_object_jitter[][] init_update_variables(SC_message_object_jitter[][] _sc_jitter_tasks, sccoresystems.sccscore.scsystemconfiguration configuration, IntPtr hwnd, sccoresystems.sccsconsole.sccsconsolewriter _writer); //void Update();
        protected abstract SC_message_object_jitter[][] Update(jitter_sc[] jitter_sc, SC_message_object_jitter[][] _sc_jitter_tasks); //void Update();
        protected abstract void ShutDownGraphics();


        public void ShutDown()
        {
            // Before shutting down set to windowed mode or when you release the swap chain it will throw an exception.   
            SwapChain?.SetFullscreenState(false, null);
            RasterState?.Dispose();
            RasterState = null;
            depthStencilState?.Dispose();
            depthStencilState = null;
            DepthStencilBuffer?.Dispose();
            DepthStencilBuffer = null;
            depthStencilView?.Dispose();
            depthStencilView = null;
            _renderTargetView?.Dispose();
            _renderTargetView = null;
            DeviceContext?.Dispose();
            Device?.Dispose();
            SwapChain?.Dispose();



            AlphaEnableBlendingState?.Dispose();
            AlphaEnableBlendingState = null;
            AlphaDisableBlendingState?.Dispose();
            AlphaDisableBlendingState = null;
            DepthDisabledStencilState?.Dispose();
            DepthDisabledStencilState = null;
            //DepthStencilView?.Dispose();
            //DepthStencilView = null;
            DepthStencilState?.Dispose();
            DepthStencilState = null;
            DepthStencilBuffer?.Dispose();
            DepthStencilBuffer = null;


            if (main_thread_update != null)
            {
                //main_thread_update.Suspend();
                main_thread_update = null;
            }
            ShutDownGraphics();
        }






        public void WriteErrorDetails(OvrWrap OVR, Result result, string message)
        {


            if (result >= Result.Success)
                return;

            ErrorInfo errorInformation = OVR.GetLastErrorInfo();

            string formattedMessage = string.Format("{0}. \nMessage: {1} (Error code={2})", message, errorInformation.ErrorString, errorInformation.Result);

            //Program.MessageBox((IntPtr)0, formattedMessage, "message", 0);


            //Trace.WriteLine(formattedMessage);
            //System.Windows.Forms.MessageBox.Show(formattedMessage, message);

            throw new Exception(formattedMessage);
        }
    }
}


//serialPort.Handshake = System.IO.Ports.Handshake.None;
///serialPort.Parity = Parity.None;
//serialPort.DataBits = 8;
//serialPort.StopBits = StopBits.None;
//serialPort.ReadTimeout = 200;
//serialPort.WriteTimeout = 500;

//serialPort.DataReceived += new System.IO.Ports.SerialDataReceivedEventHandler(functionname)








/*
_sec_received_messages[18]._message = msgg;
                            _sec_received_messages[18]._originalMsg = msgg;
                            _sec_received_messages[18]._messageCut = msgg;
                            _sec_received_messages[18]._specialMessage = 2;
                            _sec_received_messages[18]._specialMessageLineX = 0;
                            _sec_received_messages[18]._specialMessageLineY = 5;
                            _sec_received_messages[18]._orilineX = 0;
                            _sec_received_messages[18]._orilineY = 5;
                            _sec_received_messages[18]._lineX = 0;
                            _sec_received_messages[18]._lineY = 5;
                            _sec_received_messages[18]._count = 0;
                            _sec_received_messages[18]._swtch0 = 1;b
                            _sec_received_messages[18]._swtch1 = 1;
                            _sec_received_messages[18]._delay = 11;
                            _sec_received_messages[18]._looping = 0;

                            _sec_received_messages[5]._message = msgg;
                            _sec_received_messages[5]._originalMsg = msgg;
                            _sec_received_messages[5]._messageCut = msgg;
                            _sec_received_messages[5]._specialMessage = 2;
                            _sec_received_messages[5]._specialMessageLineX = 0;
                            _sec_received_messages[5]._specialMessageLineY = 0;
                            _sec_received_messages[5]._lineX = _initX0;
                            _sec_received_messages[5]._lineY = _initY0;
                            _sec_received_messages[5]._count = 0;
                            _sec_received_messages[5]._swtch0 = 1;
                            _sec_received_messages[5]._swtch1 = 0;
                            _sec_received_messages[5]._delay = 0;
                            _sec_received_messages[5]._looping = 0;
                            _sc_jitter_tasks[0][0]._world_data[1] = msgg;*/
