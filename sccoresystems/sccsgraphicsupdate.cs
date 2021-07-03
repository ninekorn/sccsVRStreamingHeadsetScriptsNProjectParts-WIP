using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Linq;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

//using Ab3d.DXEngine;
using Ab3d.OculusWrap;
//using Ab3d.DXEngine.OculusWrap;
using Ab3d.OculusWrap.DemoDX11;

using SharpDX;
using SharpDX.DXGI;
using SharpDX.Direct3D11;
using SharpDX.DirectInput;

using sccoresystems.sccsgraphics;

using Jitter;
using Jitter.Dynamics;
using Jitter.Collision;
using Jitter.LinearMath;
using Jitter.Collision.Shapes;
using Jitter.Forces;

using System.Collections.Generic;
using System.Collections;
using System.Runtime;
using System.Runtime.CompilerServices;

using System.ComponentModel;
using SharpDX.D3DCompiler;

using SC_message_object = sccoresystems.sccsmessageobject;
using SC_message_object_jitter = sccoresystems.sc_message_object_jitter;

using ISCCS_Jitter_Interface = Jitter.ISCCS_Jitter_Interface;
using Jitter;

namespace sccoresystems.sccsgraphics
{
    public class sccsgraphicsupdate : sccssharpdxdirectx
    {
        //System.Windows.Threading.Dispatcher somedispatcher;
        Matrix tempmatter;
        Quaternion quatt;

        public static float RotationOriginY { get; set; }
        public static float RotationOriginX { get; set; }
        public static float RotationOriginZ { get; set; }
        float rotMax = 25;
        float newRotX = 0;
        float someRotForPelvis = 0;
        float newRotY = 0;
        float pitch = 0;
        float yaw = 0;
        float roll = 0;
        public static int[] arduinoDIYOculusTouchArrayOfData = new int[12];


        public sccsgraphicsupdate()
        {

        }

        public static Matrix hmdmatrixRot = Matrix.Identity;

        BackgroundWorker BackgroundWorker_00;

        protected override void ShutDownGraphics()
        {
            //somedispatcher = System.Windows.Threading.Dispatcher;




            if (BackgroundWorker_00 != null)
            {
                BackgroundWorker_00.Dispose();
            }
            /*if (_Keyboard != null)
            {
                _Keyboard.Dispose();
            }
            if (_KeyboardState != null)
            {
                _KeyboardState = null;
            }*/

            if (somesccsscreencapturedata.ShaderResource != null)
            {
                somesccsscreencapturedata.ShaderResource = null;
            }

            /*if (somesccsscreencapture.lastShaderResourceView != null)
            {
                somesccsscreencapture.lastShaderResourceView = null;
            }*/
            if (somesccsscreencapture != null)
            {
                somesccsscreencapture = null;
            }
        }


        /*protected override void SC_Init_DirectX()
        {

            base.SC_Init_DirectX();
        }*/
        /*public override SC_message_object_jitter[][] sc_write_to_buffer(SC_message_object_jitter[][] _sc_jitter_tasks)
        {
            return _sc_jitter_tasks;
            //base.sc_write_to_buffer(_sc_jitter_tasks);
        }

        public override SC_message_object_jitter[][] loop_worlds(SC_message_object_jitter[][] _sc_jitter_tasks)
        {
            return _sc_jitter_tasks;
            //base.sc_write_to_buffer(_sc_jitter_tasks);
        }
        public override SC_message_object_jitter[][] workOnSomething(SC_message_object_jitter[][] _sc_jitter_tasks, Matrix viewMatrix, Matrix projectionMatrix, Vector3 OFFSETPOS, Matrix originRot, Matrix rotatingMatrix, Matrix rotatingMatrixForPelvis, Matrix _rightTouchMatrix, Matrix _leftTouchMatrix, Posef handPoseRight, Posef handPoseLeft)
        {
            return _sc_jitter_tasks;
            //base.sc_write_to_buffer(_sc_jitter_tasks);
        }
        public override SC_message_object_jitter[][] _sc_create_world_objects(SC_message_object_jitter[][] _sc_jitter_tasks)
        {
            return _sc_jitter_tasks;
            //base.sc_write_to_buffer(_sc_jitter_tasks);
        }*/






        //originRot* rotatingMatrix *rotatingMatrixForPelvis * hmdmatrixRot; //hmd_matrix

        public static SharpDX.Vector3 originPos = new SharpDX.Vector3(0, 1, 0);
        public static SharpDX.Vector3 originPosScreen = new SharpDX.Vector3(0, 1, -0.25f);

        float disco_sphere_rot_speed = 0.5f;

        float speedRot = 0.05f;
        float speedRotArduino = 0.000001f;

        float speedPos = 0.05f;
        float speedPosArduino = 0.001f;


        int _start_background_worker_01 = 0;
        //sc_graphics_main _the_graphics;

        float _delta_timer_frame = 0;
        float _delta_timer_time = 0;
        DateTime time1;
        DateTime time2;
        float deltaTime;
        Stopwatch timeStopWatch00 = new Stopwatch();
        Stopwatch timeStopWatch01 = new Stopwatch();
        int _swtch = 0;
        int _swtch_counter_00 = 0;
        int _swtch_counter_01 = 0;
        int _swtch_counter_02 = 0;

        public void DoWork(int timeOut) //async Task
        {
        //float startTime = (float)(timeStopWatch00.ElapsedMilliseconds);
        _threadLoop:

            if (_swtch == 0 || _swtch == 1)
            {
                if (_swtch == 0)
                {
                    if (_swtch_counter_00 >= 0)
                    {
                        ////////////////////
                        //UPGRADED DELTATIME
                        ////////////////////
                        //IMPORTANT PHYSICS TIME 
                        timeStopWatch00.Start();
                        time1 = DateTime.Now;
                        ////////////////////
                        //UPGRADED DELTATIME
                        ////////////////////
                        _swtch = 1;
                        _swtch_counter_00 = 0;
                    }
                }
                else if (_swtch == 1)
                {
                    if (_swtch_counter_01 >= 0)
                    {
                        ////////////////////
                        //UPGRADED DELTATIME
                        ////////////////////
                        timeStopWatch01.Start();
                        time2 = DateTime.Now;
                        ////////////////////
                        //UPGRADED DELTATIME
                        ////////////////////
                        _swtch = 2;
                        _swtch_counter_01 = 0;
                    }
                }
                else if (_swtch == 2)
                {

                }
            }

            /*//FRAME DELTATIME
            _delta_timer_frame = (float)Math.Abs((timeStopWatch01.Elapsed.Ticks - timeStopWatch00.Elapsed.Ticks)) * 100000000f;

            time2 = DateTime.Now;
            _delta_timer_time = (time2.Ticks - time1.Ticks) * 100000000f; //100000000f
            //time1 = time2;

            deltaTime = (float)Math.Abs(_delta_timer_time - _delta_timer_frame);
            */

            //FRAME DELTATIME
            _delta_timer_frame = (float)Math.Abs((timeStopWatch01.Elapsed.Ticks - timeStopWatch00.Elapsed.Ticks)); //10000000000f

            time2 = DateTime.Now;
            _delta_timer_time = (time2.Ticks - time1.Ticks); //100000000f //10000000000f
            time1 = time2;

            deltaTime = (float)Math.Abs(_delta_timer_time - _delta_timer_frame);

            //time1 = time2;
            //await Task.Delay(1);
            //Thread.Sleep(timeOut);
            _swtch_counter_00++;
            _swtch_counter_01++;
            _swtch_counter_02++;

            goto _threadLoop;
        }

        /*public SC_Console_GRAPHICS(sc_graphics_main _graphics)
        {
            _the_graphics = _graphics;
        }*/

        public void ShutDown()
        {

        }

        Matrix rotatingMatrixForGrabber = Matrix.Identity;
        int _sec_logic_swtch_grab = 0;

        int _swtch_hasRotated = 0;
        int _has_grabbed_right_swtch = 0;
        public static double RotationY { get; set; }
        public static double RotationX { get; set; }
        public static double RotationZ { get; set; }
        float thumbstickIsRight;
        float thumbstickIsUp;

        int RotationGrabbedSwtch = 0;

        public static double RotationY4Pelvis;
        public static double RotationX4Pelvis;
        public static double RotationZ4Pelvis;

        public static double RotationY4PelvisTwo;
        public static double RotationX4PelvisTwo;
        public static double RotationZ4PelvisTwo;


        public static double RotationGrabbedYOff;
        public static double RotationGrabbedXOff;
        public static double RotationGrabbedZOff;


        public static double RotationGrabbedY;
        public static double RotationGrabbedX;
        public static double RotationGrabbedZ;


        //OCULUS TOUCH SETTINGS 
        Ab3d.OculusWrap.Result resultsRight;
        uint buttonPressedOculusTouchRight;
        Vector2f[] thumbStickRight;
        public static float[] handTriggerRight;
        float[] indexTriggerRight;
        Ab3d.OculusWrap.Result resultsLeft;
        uint buttonPressedOculusTouchLeft;
        Vector2f[] thumbStickLeft;
        public static float[] handTriggerLeft;
        public static float[] indexTriggerLeft;
        Posef handPoseLeft;
        SharpDX.Quaternion _leftTouchQuat;
        Posef handPoseRight;
        SharpDX.Quaternion _rightTouchQuat;
        Matrix _leftTouchMatrix = Matrix.Identity;
        Matrix _rightTouchMatrix = Matrix.Identity;
        //OCULUS TOUCH SETTINGS 

        float offsetPosX = 0.0f;
        float offsetPosY = 0.0f;
        float offsetPosZ = 0.0f;

        double displayMidpoint;
        TrackingState trackingState;
        Posef[] eyePoses;
        EyeType eye;
        EyeTexture eyeTexture;
        bool latencyMark = false;
        TrackingState trackState;
        PoseStatef poseStatefer;
        Posef hmdPose;
        Quaternionf hmdRot;
        Vector3 _hmdPoser;
        Quaternion _hmdRoter;

        Vector3 intersectPointRight;
        Vector3 intersectPointLeft;
        Matrix final_hand_pos_right_locked;
        Matrix final_hand_pos_left_locked;

        sccscamera Camera;

        int _failed_screen_capture = 0;
        public static sccsshadermanager _shaderManager;
        public int _can_work_physics_objects = 0;

        public static IntPtr HWND;
        sccoresystems.sccscore.scsystemconfiguration _configuration;
        sccsconsole.sccsconsolewriter _currentWriter;
        public static sccssharpdxscreenframe somesccsscreencapturedata;

        float xq;//= otherQuat.X;
        float yq;//= otherQuat.Y;
        float zq;//= otherQuat.Z;
        float wq;//= otherQuat.W;
        float pitcha;//= (float) Math.Atan2(2 * yq* wq - 2 * xq* zq, 1 - 2 * yq* yq - 2 * zq* zq); //(float)(180 / Math.PI)
        float yawa;//= (float) Math.Atan2(2 * yq* wq - 2 * xq* zq, 1 - 2 * yq* yq - 2 * zq* zq); //(float)(180 / Math.PI) *
        float rolla;// = (float) Math.Atan2(2 * yq* wq - 2 * xq* zq, 1 - 2 * yq* yq - 2 * zq* zq); // (float)(180 / Math.PI) *
        float hyp;// = diffNormPosY / Math.Cos(pitcha);

        int textureIndex;
        SharpDX.Vector3 eyePos;
        SharpDX.Matrix eyeQuaternionMatrix;
        SharpDX.Matrix finalRotationMatrix;
        Vector3 lookUp;
        Vector3 lookAt;
        Vector3 viewPosition;
        Matrix viewMatrix;
        Matrix _projectionMatrix;
        public static Vector3 OFFSETPOS;

        public static SharpDX.Vector3 movePos = new SharpDX.Vector3(0, 0, 0);
        public static SharpDX.Matrix originRot = SharpDX.Matrix.Identity;

        public static SharpDX.Matrix rotatingMatrixForPelvis = SharpDX.Matrix.Identity;
        public static SharpDX.Matrix rotatingMatrix = SharpDX.Matrix.Identity;
        float r = 0;
        float g = 0;
        float b = 0;
        float a = 1;

        Matrix WorldMatrix = Matrix.Identity;
        public static sccssharpdxscreencapture somesccsscreencapture;


        public static DateTime startTime;

        protected override SC_message_object_jitter[][] init_update_variables(SC_message_object_jitter[][] _sc_jitter_tasks, sccoresystems.sccscore.scsystemconfiguration configuration, IntPtr hwnd, sccoresystems.sccsconsole.sccsconsolewriter _writer)
        {
            try
            {
                startTime = DateTime.Now;

                HWND = hwnd;

                _configuration = configuration;

                _currentWriter = _writer;

                /*
                if (Program.physicsenabled == 1)
                {
                    Thread main_thread_update = new Thread(() =>
                    {

                    _thread_looper:

                        try
                        {
                            DoWork(0);
                        }
                        catch (Exception ex)
                        {

                        }
                        Thread.Sleep(1);
                        goto _thread_looper;

                        //ShutDown();
                        //ShutDownGraphics();

                    }, 0);

                    main_thread_update.IsBackground = true;
                    main_thread_update.SetApartmentState(ApartmentState.STA);
                    main_thread_update.Start();
                }*/


                Camera = new sccscamera();

                _shaderManager = new sccsshadermanager();
                _shaderManager.Initialize(Device, HWND);

                //somesccsscreencapture = new sccssharpdxscreencapture(0, 0, device);


                _graphics_sec = new sccsgraphicsupdatessec();
                _sc_jitter_tasks = _graphics_sec._sc_create_world_objects(_sc_jitter_tasks);
            }
            catch
            {

            }
            return _sc_jitter_tasks;
        }

        sccsgraphicsupdatessec _graphics_sec;

        protected override SC_message_object_jitter[][] Update(jitter_sc[] jitter_sc, SC_message_object_jitter[][] _sc_jitter_tasks)
        {
            if (_shaderManager != null)
            {
                // Render the graphics scene.
                try
                {
                    _sc_jitter_tasks = _FrameVRTWO(jitter_sc, _sc_jitter_tasks);
                }
                catch (Exception ex)
                {
                    Program.MessageBox((IntPtr)0, "" + ex.ToString(), "sc core systems message", 0);
                }
            }
            else
            {
                //MessageBox((IntPtr)0, "" + ex.ToString(), "sc core systems message", 0);
            }
            return _sc_jitter_tasks;
        }

        int _last_frame_init = 0;
        Vector3f[] hmdToEyeViewOffsets = new Vector3f[2];

        Matrix hmd_matrix = Matrix.Identity;
        private unsafe SC_message_object_jitter[][] _FrameVRTWO(jitter_sc[] jitter_sc, SC_message_object_jitter[][] _sc_jitter_tasks)
        {


            try
            {

                //hmdToEyeViewOffsets = { eyeTextures[0].HmdToEyeViewOffset, eyeTextures[1].HmdToEyeViewOffset };
                hmdToEyeViewOffsets[0] = eyeTextures[0].HmdToEyeViewOffset;
                hmdToEyeViewOffsets[1] = eyeTextures[1].HmdToEyeViewOffset;



                displayMidpoint = OVR.GetPredictedDisplayTime(sessionPtr, 0);
                trackingState = OVR.GetTrackingState(sessionPtr, displayMidpoint, true);
                eyePoses = new Posef[2];
                OVR.CalcEyePoses(trackingState.HeadPose.ThePose, hmdToEyeViewOffsets, ref eyePoses);

                for (int eyeIndex = 0; eyeIndex < 2; eyeIndex++)
                {
                    eye = (EyeType)eyeIndex;
                    eyeTexture = eyeTextures[eyeIndex];

                    if (eyeIndex == 0)
                    {
                        layerEyeFov.RenderPoseLeft = eyePoses[0];
                    }
                    else
                    {
                        layerEyeFov.RenderPoseRight = eyePoses[1];
                    }

                    eyeTexture.RenderDescription = OVR.GetRenderDesc(sessionPtr, eye, hmdDesc.DefaultEyeFov[eyeIndex]);

                    result = eyeTexture.SwapTextureSet.GetCurrentIndex(out textureIndex);
                    //WriteErrorDetails(OVR, result, "Failed to retrieve texture swap chain current index.");
                    /*
                    device.ImmediateContext.OutputMerger.SetRenderTargets(eyeTexture.DepthStencilView, eyeTexture.RenderTargetViews[textureIndex]);
                    device.ImmediateContext.ClearRenderTargetView(eyeTexture.RenderTargetViews[textureIndex], somevrbackgroundcolor); //DimGray //Black
                    device.ImmediateContext.ClearDepthStencilView(eyeTexture.DepthStencilView, DepthStencilClearFlags.Depth | DepthStencilClearFlags.Stencil, 1.0f, 0);
                    device.ImmediateContext.Rasterizer.SetViewport(eyeTexture.Viewport);*/






                    /*
                    eyeQuaternionMatrix = SharpDX.Matrix.RotationQuaternion(new SharpDX.Quaternion(eyePoses[eyeIndex].Orientation.X, eyePoses[eyeIndex].Orientation.Y, eyePoses[eyeIndex].Orientation.Z, eyePoses[eyeIndex].Orientation.W));

                    eyePos = SharpDX.Vector3.Transform(new SharpDX.Vector3(eyePoses[eyeIndex].Position.X, eyePoses[eyeIndex].Position.Y, eyePoses[eyeIndex].Position.Z), originRot * rotatingMatrix * rotatingMatrixForPelvis * hmdmatrixRot).ToVector3();

                    //finalRotationMatrix = eyeQuaternionMatrix * originRot * rotatingMatrix;
                    finalRotationMatrix = eyeQuaternionMatrix * originRot * rotatingMatrix * rotatingMatrixForPelvis * hmdmatrixRot;

                    lookUp = Vector3.Transform(new Vector3(0, 1, 0), finalRotationMatrix).ToVector3();
                    lookAt = Vector3.Transform(new Vector3(0, 0, -1), finalRotationMatrix).ToVector3();

                    viewPosition = eyePos + OFFSETPOS;*/
                    //viewPosition.Y += eyePos.Y;
                    
                    result = eyeTexture.SwapTextureSet.Commit();
                    WriteErrorDetails(OVR, result, "Failed to commit the swap chain texture.");
                }
                
                result = OVR.SubmitFrame(sessionPtr, 0L, IntPtr.Zero, ref layerEyeFov);

                WriteErrorDetails(OVR, result, "Failed to submit the frame of the current layers.");
                DeviceContext.CopyResource(mirrorTextureD3D, BackBuffer);
                SwapChain.Present(0, PresentFlags.None);
            }
            catch (Exception ex)
            {
                //Program.MessageBox((IntPtr)0, "" + ex.ToString(), "sc core systems message", 0);
            }


            if (_can_work_physics == 0)
            {
                //###SC start physics on frame 1 instead of 0
                _can_work_physics = 1;
                _can_work_physics_objects = 1;
                //###SC start physics on frame 1 instead of 0
            }

            return _sc_jitter_tasks;
        }


        int framecounterforpctoarduinoscreenswtc = 0;
        Stopwatch arduinotickswatch = new Stopwatch();
        int framecounterforpctoarduinoscreen = 0;
        int framecounterforpctoarduinoscreenMax = 20;
        int framecounterforpctoarduinoscreenFinal = 0;
        SharpDX.Color somevrbackgroundcolor = SharpDX.Color.DimGray;

        int writetobuffer = 0;

        Stopwatch _ticks_watch = new Stopwatch();
        public int _can_work_physics = 0;

        /*public KeyboardState _KeyboardState;
        public SharpDX.DirectInput.Keyboard _Keyboard;
        DirectInput directInput;*/

        /*private bool ReadKeyboard()
        {
            directInput = new DirectInput();

            _Keyboard = new Keyboard(directInput);

            //Acquire the joystick
            _Keyboard.Properties.BufferSize = 128;

            var resultCode = SharpDX.DirectInput.ResultCode.Ok;
            _KeyboardState = new KeyboardState();

            try
            {
                // Read the keyboard device.
                _Keyboard.GetCurrentState(ref _KeyboardState);
            }
            catch (SharpDX.SharpDXException ex)
            {
                resultCode = ex.Descriptor; // ex.ResultCode;
            }
            catch (Exception)
            {
                return false;
            }

            // If the mouse lost focus or was not acquired then try to get control back.
            if (resultCode == SharpDX.DirectInput.ResultCode.InputLost || resultCode == SharpDX.DirectInput.ResultCode.NotAcquired)
            {
                try
                {
                    _Keyboard.Acquire();

                }
                catch
                { 
                
                }
                return true;
            }

            if (resultCode == SharpDX.DirectInput.ResultCode.Ok)
            {
                return true;
            }

            return false;
        }*/
    }
}

