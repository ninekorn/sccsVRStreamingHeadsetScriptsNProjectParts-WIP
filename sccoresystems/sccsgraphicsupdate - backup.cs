using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Linq;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

using Ab3d.DXEngine;
using Ab3d.OculusWrap;
using Ab3d.DXEngine.OculusWrap;
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

            if (somesccsscreencapture.lastShaderResourceView != null)
            {
                somesccsscreencapture.lastShaderResourceView = null;
            }
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
                }


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

        Matrix hmd_matrix = Matrix.Identity;
        private unsafe SC_message_object_jitter[][] _FrameVRTWO(jitter_sc[] jitter_sc, SC_message_object_jitter[][] _sc_jitter_tasks)
        {
            //oculus rift vr controllers and vr headset inputs.
            /*
            //HEADSET POSITION
            displayMidpoint = OVR.GetPredictedDisplayTime(sessionPtr, 0);
            trackingState = OVR.GetTrackingState(sessionPtr, displayMidpoint, true);
            latencyMark = false;
            trackState = OVR.GetTrackingState(sessionPtr, 0.0f, latencyMark);
            poseStatefer = trackState.HeadPose;
            hmdPose = poseStatefer.ThePose;
            hmdRot = hmdPose.Orientation;
            _hmdPoser = new Vector3(hmdPose.Position.X, hmdPose.Position.Y, hmdPose.Position.Z);
            _hmdRoter = new Quaternion(hmdPose.Orientation.X, hmdPose.Orientation.Y, hmdPose.Orientation.Z, hmdPose.Orientation.W);
            //SET CAMERA POSITION
            Camera.SetPosition(hmdPose.Position.X, hmdPose.Position.Y, hmdPose.Position.Z);
            Quaternion quatter = new Quaternion(hmdRot.X, hmdRot.Y, hmdRot.Z, hmdRot.W);
            Vector3 oculusRiftDir = sccsmaths._getDirection(Vector3.ForwardRH, quatter);

  
            Matrix.RotationQuaternion(ref quatter, out hmd_matrix);

            Matrix hmd_matrix_test;
            Matrix.RotationQuaternion(ref quatter, out hmd_matrix_test);

            hmd_matrix_test = hmd_matrix_test * finalRotationMatrix;

            //TOUCH CONTROLLER RIGHT
            resultsRight = OVR.GetInputState(sessionPtr, controllerTypeRTouch, ref inputStateRTouch);
            buttonPressedOculusTouchRight = inputStateRTouch.Buttons;
            thumbStickRight = inputStateRTouch.Thumbstick;
            handTriggerRight = inputStateRTouch.HandTrigger;
            indexTriggerRight = inputStateRTouch.IndexTrigger;
            handPoseRight = trackingState.HandPoses[1].ThePose;

            _rightTouchQuat.X = handPoseRight.Orientation.X;
            _rightTouchQuat.Y = handPoseRight.Orientation.Y;
            _rightTouchQuat.Z = handPoseRight.Orientation.Z;
            _rightTouchQuat.W = handPoseRight.Orientation.W;

            _rightTouchQuat = new SharpDX.Quaternion(handPoseRight.Orientation.X, handPoseRight.Orientation.Y, handPoseRight.Orientation.Z, handPoseRight.Orientation.W);
            SharpDX.Matrix.RotationQuaternion(ref _rightTouchQuat, out _rightTouchMatrix);

            _rightTouchMatrix.M41 = handPoseRight.Position.X + originPos.X + movePos.X;
            _rightTouchMatrix.M42 = handPoseRight.Position.Y + originPos.Y + movePos.Y;
            _rightTouchMatrix.M43 = handPoseRight.Position.Z + originPos.Z + movePos.Z;

            //TOUCH CONTROLLER LEFT
            resultsLeft = OVR.GetInputState(sessionPtr, controllerTypeLTouch, ref inputStateLTouch);
            buttonPressedOculusTouchLeft = inputStateLTouch.Buttons;
            thumbStickLeft = inputStateLTouch.Thumbstick;
            handTriggerLeft = inputStateLTouch.HandTrigger;
            indexTriggerLeft = inputStateLTouch.IndexTrigger;
            handPoseLeft = trackingState.HandPoses[0].ThePose;

            _leftTouchQuat.X = handPoseLeft.Orientation.X;
            _leftTouchQuat.Y = handPoseLeft.Orientation.Y;
            _leftTouchQuat.Z = handPoseLeft.Orientation.Z;
            _leftTouchQuat.W = handPoseLeft.Orientation.W;

            _leftTouchQuat = new SharpDX.Quaternion(handPoseLeft.Orientation.X, handPoseLeft.Orientation.Y, handPoseLeft.Orientation.Z, handPoseLeft.Orientation.W);

            SharpDX.Matrix.RotationQuaternion(ref _leftTouchQuat, out _leftTouchMatrix);
            //_other_left_touch_matrix = _leftTouchMatrix;
            //_other_left_touch_matrix.M41 = handPoseLeft.Position.X;
            //_other_left_touch_matrix.M42 = handPoseLeft.Position.Y;
            //_other_left_touch_matrix.M43 = handPoseLeft.Position.Z;

            _leftTouchMatrix.M41 = handPoseLeft.Position.X + originPos.X + movePos.X;
            _leftTouchMatrix.M42 = handPoseLeft.Position.Y + originPos.Y + movePos.Y;
            _leftTouchMatrix.M43 = handPoseLeft.Position.Z + originPos.Z + movePos.Z;
            */
            //oculus rift vr controllers and vr headset inputs.









            //some arduino tests that ended up working but the thumbsticks will require an average origin position calculated over frames otherwise, every now and then,
            //after a reset of the program, and the thumbsticks don't even have the same origin points anymore.
            /*
            object somearduinodata = (object)_sc_jitter_tasks[0][0]._world_data[1];
            string arduinomsgcom3 = (string)somearduinodata;
            Console.WriteLine(arduinomsgcom3);


            //if (!SC_console_directx.serialPort.IsOpen)
            //{
            //    SC_console_directx.serialPort.Open();
            //}

            //SC_console_directx.serialPort.read
            if (arduinomsgcom3.Length > 0)
            {
                var someSplitString = arduinomsgcom3.Split('|');


                arduinoDIYOculusTouchArrayOfData = new int[someSplitString.Length];
                for (int si = 0; si < someSplitString.Length; si++) //someSplitString.Length
                {
                    if (someSplitString[si] != null)
                    {
                        int.TryParse(someSplitString[si], out arduinoDIYOculusTouchArrayOfData[si]);
                    }


                }
            }

            //if (_out_of_bounds_oculus_rift == 1)
            {

                //Console.WriteLine("x:" + arduinoDIYOculusTouchArrayOfData[7] + "/y:" + arduinoDIYOculusTouchArrayOfData[8]);


                //Console.WriteLine("x:" + arduinoDIYOculusTouchArrayOfData[7]);
                if (arduinoDIYOculusTouchArrayOfData[7] < 520 || arduinoDIYOculusTouchArrayOfData[7] >= 564 || arduinoDIYOculusTouchArrayOfData[8] < 480 || arduinoDIYOculusTouchArrayOfData[8] >= 565)
                {
                    if (arduinoDIYOculusTouchArrayOfData[7] < 507 || arduinoDIYOculusTouchArrayOfData[8] < 480 || arduinoDIYOculusTouchArrayOfData[7] >= 564 || arduinoDIYOculusTouchArrayOfData[8] >= 565)
                    {
                        var tempDataArduinoOVRTouchY = arduinoDIYOculusTouchArrayOfData[7];
                        if (arduinoDIYOculusTouchArrayOfData[7] < 520)
                        {
                            tempDataArduinoOVRTouchY = Math.Abs(520 - arduinoDIYOculusTouchArrayOfData[7]);
                            if (RotationOriginX < -1) //360?
                            {
                                RotationOriginX = -1;//360?
                            }

                            RotationOriginX -= 0.1f;
                        }
                        else if (arduinoDIYOculusTouchArrayOfData[7] >= 564)
                        {
                            tempDataArduinoOVRTouchY = -Math.Abs(1023 - arduinoDIYOculusTouchArrayOfData[7]);
                            if (RotationOriginX > 1)//360?
                            {
                                RotationOriginX = 1;//360?
                            }
                            RotationOriginX += 0.1f;
                        }


                        //Console.WriteLine("RotationOriginX:" + RotationOriginX);

                        //public static double RotationOriginY { get; set; }
                        //public static double RotationOriginX { get; set; }
                        //public static double RotationOriginZ { get; set; }

                        RotationGrabbedYOff = 0;
                        RotationGrabbedXOff = 0;
                        RotationGrabbedZOff = 0;

                        RotationGrabbedSwtch = 1;

                        thumbstickIsRight = RotationOriginX;// tempDataArduinoOVRTouchX;
                        thumbstickIsUp = RotationOriginY;// tempDataArduinoOVRTouchY;

                        //newRotationY;

                        float rotMax = 25;



                        //float rot0 = (float)((180 / Math.PI) * (Math.Atan(thumbstickIsUp / thumbstickIsRight))); // opp/adj
                        //float rot1 = (float)((180 / Math.PI) * (Math.Atan(thumbstickIsRight / thumbstickIsUp)));

                        float newRotY = thumbstickIsRight * (rotMax) * -1;

                        RotationY = newRotY;
                        float someRotForPelvis = (float)RotationY;

                        if (RotationY > rotMax * 0.99f)
                        {
                            RotationY = rotMax * 0.99f;
                            //RotationY4Pelvis = rotMax * 0.99f;
                            //RotationY4PelvisTwo = rotMax * 0.99f;
                            //RotationGrabbedY = rotMax * 0.99f;
                            RotationY4Pelvis += speedRot * 100;
                            RotationY4PelvisTwo += speedRot * 100;
                            RotationGrabbedY += speedRot * 100;
                        }

                        if (RotationY < -rotMax * 0.99f)
                        {
                            RotationY = -rotMax * 0.99f;

                            //RotationY4Pelvis = -rotMax * 0.99f;
                            //RotationY4PelvisTwo = -rotMax * 0.99f;
                            //RotationGrabbedY = -rotMax * 0.99f;
                            RotationY4Pelvis += -speedRot * 100;
                            RotationY4PelvisTwo += -speedRot * 100;
                            RotationGrabbedY += -speedRot * 100;
                        }

                        rotMax = 25;
                        float newRotX = thumbstickIsUp * (rotMax) * -1;
                        RotationX = newRotX;

                        if (RotationX > rotMax * 0.99f)
                        {
                            RotationX = rotMax * 0.99f;
                        }

                        if (RotationX < -rotMax * 0.99f)
                        {
                            RotationX = -rotMax * 0.99f;
                        }

                        //float pitch = (float)(RotationX * 0.0174532925f);
                        //float yaw = (float)(RotationY * 0.0174532925f);
                        //float roll = (float)(RotationZ * 0.0174532925f);


                        float pitch = (float)(Math.PI * (RotationX) / 180.0f);
                        float yaw = (float)(Math.PI * (RotationY) / 180.0f);
                        float roll = (float)(Math.PI * (RotationZ) / 180.0f);

                        rotatingMatrix = SharpDX.Matrix.RotationYawPitchRoll(yaw, pitch, roll);

                        //pitch = (float)(RotationX4Pelvis * 0.0174532925f);
                        //yaw = (float)(RotationY4Pelvis * 0.0174532925f);
                        //roll = (float)(RotationZ4Pelvis * 0.0174532925f);

                        pitch = (float)(Math.PI * (RotationX4Pelvis) / 180.0f);
                        yaw = (float)(Math.PI * (RotationY4Pelvis) / 180.0f);
                        roll = (float)(Math.PI * (RotationZ4Pelvis) / 180.0f);


                        rotatingMatrixForPelvis = SharpDX.Matrix.RotationYawPitchRoll(yaw, pitch, roll);

                        if (_has_grabbed_right_swtch == 2)
                        {
                            _swtch_hasRotated = 1;
                        }

                        pitch = (float)(Math.PI * (RotationGrabbedX) / 180.0f);
                        yaw = (float)(Math.PI * (RotationGrabbedY) / 180.0f);
                        roll = (float)(Math.PI * (RotationGrabbedZ) / 180.0f);


                        rotatingMatrixForGrabber = SharpDX.Matrix.RotationYawPitchRoll(yaw, pitch, roll);



                    }

                    if (arduinoDIYOculusTouchArrayOfData[8] < 480 || arduinoDIYOculusTouchArrayOfData[8] >= 565)
                    {




                        var tempDataArduinoOVRTouchX = arduinoDIYOculusTouchArrayOfData[8];
                        if (arduinoDIYOculusTouchArrayOfData[8] < 480)
                        {
                            tempDataArduinoOVRTouchX = -Math.Abs(480 - arduinoDIYOculusTouchArrayOfData[8]);
                            if (RotationOriginY < -1) //360?
                            {
                                RotationOriginY = -1;//360?
                            }

                            RotationOriginY += 0.1f;
                        }
                        else if (arduinoDIYOculusTouchArrayOfData[8] >= 565)
                        {
                            tempDataArduinoOVRTouchX = Math.Abs(1023 - arduinoDIYOculusTouchArrayOfData[8]);
                            if (RotationOriginY > 1)//360?
                            {
                                RotationOriginY = 1;//360?
                            }
                            RotationOriginY -= 0.1f;
                        }


                        //Console.WriteLine("RotationOriginY:" + RotationOriginY);

                        //public static double RotationOriginY { get; set; }
                        //public static double RotationOriginX { get; set; }
                        //public static double RotationOriginZ { get; set; }

                        RotationGrabbedYOff = 0;
                        RotationGrabbedXOff = 0;
                        RotationGrabbedZOff = 0;

                        RotationGrabbedSwtch = 1;

                        thumbstickIsRight = RotationOriginY;// tempDataArduinoOVRTouchX;
                        thumbstickIsUp = RotationOriginX;// tempDataArduinoOVRTouchX;

                        //newRotationY;

                        rotMax = 25;

                        //float rot0 = (float)((180 / Math.PI) * (Math.Atan(thumbstickIsUp / thumbstickIsRight))); // opp/adj
                        //float rot1 = (float)((180 / Math.PI) * (Math.Atan(thumbstickIsRight / thumbstickIsUp)));

                        newRotX = thumbstickIsRight * (rotMax) * -1;

                        RotationX = newRotX;
                        someRotForPelvis = (float)RotationX;

                        if (RotationX > rotMax * 0.99f)
                        {
                            RotationX = rotMax * 0.99f;
                            //RotationY4Pelvis = rotMax * 0.99f;
                            //RotationY4PelvisTwo = rotMax * 0.99f;
                            //RotationGrabbedY = rotMax * 0.99f;
                            //RotationX4Pelvis += speedRot * 100;
                            //RotationX4PelvisTwo += speedRot * 100;
                            //RotationGrabbedX += speedRot * 100;
                        }

                        if (RotationX < -rotMax * 0.99f)
                        {
                            RotationX = -rotMax * 0.99f;

                            //RotationY4Pelvis = -rotMax * 0.99f;
                            //RotationY4PelvisTwo = -rotMax * 0.99f;
                            //RotationGrabbedY = -rotMax * 0.99f;
                            //RotationX4Pelvis += -speedRot * 100;
                            //RotationX4PelvisTwo += -speedRot * 100;
                            //RotationGrabbedX += -speedRot * 100;
                        }

                        rotMax = 25;
                        newRotY = thumbstickIsUp * (rotMax) * -1;
                        RotationY = newRotY;

                        if (RotationY > rotMax * 0.99f)
                        {
                            RotationY = rotMax * 0.99f;
                        }

                        if (RotationY < -rotMax * 0.99f)
                        {
                            RotationY = -rotMax * 0.99f;
                        }

                        //float pitch = (float)(RotationX * 0.0174532925f);
                        //float yaw = (float)(RotationY * 0.0174532925f);
                        //float roll = (float)(RotationZ * 0.0174532925f);


                        pitch = (float)(Math.PI * (RotationX) / 180.0f);
                        yaw = (float)(Math.PI * (RotationY) / 180.0f);
                        roll = (float)(Math.PI * (RotationZ) / 180.0f);

                        rotatingMatrix = SharpDX.Matrix.RotationYawPitchRoll(yaw, pitch, roll);

                        //pitch = (float)(RotationX4Pelvis * 0.0174532925f);
                        //yaw = (float)(RotationY4Pelvis * 0.0174532925f);
                        //roll = (float)(RotationZ4Pelvis * 0.0174532925f);

                        pitch = (float)(Math.PI * (RotationX4Pelvis) / 180.0f);
                        yaw = (float)(Math.PI * (RotationY4Pelvis) / 180.0f);
                        roll = (float)(Math.PI * (RotationZ4Pelvis) / 180.0f);


                        rotatingMatrixForPelvis = SharpDX.Matrix.RotationYawPitchRoll(yaw, pitch, roll);

                        if (_has_grabbed_right_swtch == 2)
                        {
                            _swtch_hasRotated = 1;
                        }

                        pitch = (float)(Math.PI * (RotationGrabbedX) / 180.0f);
                        yaw = (float)(Math.PI * (RotationGrabbedY) / 180.0f);
                        roll = (float)(Math.PI * (RotationGrabbedZ) / 180.0f);


                        rotatingMatrixForGrabber = SharpDX.Matrix.RotationYawPitchRoll(yaw, pitch, roll);

                    }
                }
                else
                {
                    RotationOriginX = 0;
                    RotationOriginY = 0;
                    RotationOriginZ = 0;

                    RotationGrabbedYOff = RotationY4Pelvis;
                    RotationGrabbedXOff = RotationX4Pelvis;
                    RotationGrabbedZOff = RotationZ4Pelvis;

                    if (RotationGrabbedSwtch == 1)
                    {
                        RotationX4PelvisTwo = 0;
                        RotationY4PelvisTwo = 0;
                        RotationZ4PelvisTwo = 0;
                        RotationGrabbedSwtch = 0;
                    }

                    //RotationGrabbedY = 0;
                    //RotationGrabbedX = 0;
                    //RotationGrabbedZ = 0;

                    if (arduinoDIYOculusTouchArrayOfData[7] >= 520 && arduinoDIYOculusTouchArrayOfData[7] < 533) //&& arduinoDIYOculusTouchArrayOfData[8] >=480  && arduinoDIYOculusTouchArrayOfData[8] <= 560
                    {
                        if (_swtch_hasRotated == 1)
                        {

                            _swtch_hasRotated = 2;
                        }
                        //_swtch_hasRotated = 0;

                        RotationX = 0;
                        RotationY = 0;
                        RotationZ = 0;

                        float pitch = (float)(RotationX * 0.0174532925f);
                        float yaw = (float)(RotationY * 0.0174532925f);
                        float roll = (float)(RotationZ * 0.0174532925f);

                        rotatingMatrix = SharpDX.Matrix.RotationYawPitchRoll(yaw, pitch, roll);

                        pitch = (float)(RotationX4Pelvis * 0.0174532925f);
                        yaw = (float)(RotationY4Pelvis * 0.0174532925f);
                        roll = (float)(RotationZ4Pelvis * 0.0174532925f);

                        rotatingMatrixForPelvis = SharpDX.Matrix.RotationYawPitchRoll(yaw, pitch, roll);

                        pitch = (float)(RotationGrabbedX * 0.0174532925f);
                        yaw = (float)(RotationGrabbedY * 0.0174532925f);
                        roll = (float)(RotationGrabbedZ * 0.0174532925f);

                        rotatingMatrixForGrabber = SharpDX.Matrix.RotationYawPitchRoll(yaw, pitch, roll);
                        if (_swtch_hasRotated == 0)
                        {
                            _sec_logic_swtch_grab = 0;
                        }
                    }
                    else
                    {

                    }
                }
            }


            Matrix tempmat = hmd_matrix * rotatingMatrixForPelvis * hmdmatrixRot;
            Quaternion otherQuat;
            Quaternion.RotationMatrix(ref tempmat, out otherQuat);

            Vector3 direction_feet_forward;
            Vector3 direction_feet_right;
            Vector3 direction_feet_up;

            direction_feet_forward = sc_maths._getDirection(Vector3.ForwardRH, otherQuat);
            direction_feet_right = sc_maths._getDirection(Vector3.Right, otherQuat);
            direction_feet_up = sc_maths._getDirection(Vector3.Up, otherQuat);

            //Console.WriteLine("x:" + arduinoDIYOculusTouchArrayOfData[10] + "/y:" + arduinoDIYOculusTouchArrayOfData[11]);
            if (arduinoDIYOculusTouchArrayOfData != null)
            {
                if (arduinoDIYOculusTouchArrayOfData[10] != null)
                {
                    //Console.WriteLine("x:"+arduinoDIYOculusTouchArrayOfData[10]);
                    //Console.WriteLine("x:" + arduinoDIYOculusTouchArrayOfData[7] + "/y:" + arduinoDIYOculusTouchArrayOfData[8]);
                    //Console.WriteLine();
                    if (arduinoDIYOculusTouchArrayOfData[10] >= 550)
                    {
                        movePos += direction_feet_right * speedPos * arduinoDIYOculusTouchArrayOfData[10] * speedPosArduino;
                    }

                    if (arduinoDIYOculusTouchArrayOfData[10] < 480)
                    {
                        var tempData = -Math.Abs(1027 - arduinoDIYOculusTouchArrayOfData[10]);
                        movePos += direction_feet_right * speedPos * tempData * speedPosArduino;
                    }
                }

                if (arduinoDIYOculusTouchArrayOfData.Length >= 10)
                {

                }




                if (arduinoDIYOculusTouchArrayOfData[11] != null)
                {
                    //Console.WriteLine("y:" + arduinoDIYOculusTouchArrayOfData[11]);
                    //Console.WriteLine("TEST");
                    //Console.WriteLine("x:" + arduinoDIYOculusTouchArrayOfData[7] + "/y:" + arduinoDIYOculusTouchArrayOfData[8]);
                    //Console.WriteLine();
                    if (arduinoDIYOculusTouchArrayOfData[11] >= 565) // average 506 or 
                    {
                        movePos += direction_feet_forward * speedPos * -arduinoDIYOculusTouchArrayOfData[11] * speedPosArduino;
                    }

                    if (arduinoDIYOculusTouchArrayOfData[11] < 480)
                    {
                        var tempData = Math.Abs(1027 - arduinoDIYOculusTouchArrayOfData[11]);
                        movePos += direction_feet_forward * speedPos * tempData * speedPosArduino;
                    }
                }
                if (arduinoDIYOculusTouchArrayOfData.Length >= 11)
                {

                }
            }*/
            //some arduino tests that ended up working but the thumbsticks will require an average origin position calculated over frames otherwise, every now and then,
            //after a reset of the program, and the thumbsticks don't even have the same origin points anymore.


            OFFSETPOS = originPos + movePos;// + _hmdPoser; //_hmdPoser

            //calibrate device todo. 
            //1. ask the player to budge their thumbsticks in all directions so that it goes back to the middle idle position.
            //2. since the thumbstick in most cases will fall back to their middle idle position when they aren't broken, i can set an average, if the exercise is repeated 1 to 9 times and set the average as the origin IDLE position.
            //3. When the origin Idle position is set for the x and y axis, adding a +-5 difference so that minuscule changes over a certain period of frame is also set as the IDLE position average min/max values where it should be set as IDLE.
            //4. add if/else so that minuscule movement of the player isn't confused by the IDLE position, especially if everytime minuscule movement of the thumbstick is engaged and increases of +1 over 10 frames when the player uses the left thumbstick to strafe left, the arduino oculus touch emulator data would show the number from 513 to 515 and lowering to 0
            //   and if the player would turn right the data would show 513 to 515 right up to 1027-1023 max... So if minuscule changes are never counted, then the player would reach the far left or right of their thumbstick joystick and never even
            //   have strafe a single unit ingame, which in turns returns ranting and raging players which is never cool.




















            /*
            //START OF PHYSICS ENGINE STEPS
            if (_start_background_worker_01 == 0 && Program.physicsenabled == 1)
            {
                if (_can_work_physics == 1)
                {
                    var main_thread_update = new Thread(() =>
                    {
                        try
                        {
                            //Program.MessageBox((IntPtr)0, "threadstart succes", "sc core systems message", 0);
                            Stopwatch thisthreadticks01 = new Stopwatch();
                            thisthreadticks01.Start();

                        threadlooper:

                            for (int xx = 0; xx < Program.physicsengineinstancex; xx++)
                            {
                                for (int yy = 0; yy < Program.physicsengineinstancey; yy++)
                                {
                                    for (int zz = 0; zz < Program.physicsengineinstancez; zz++)
                                    {
                                        var indexer0 = xx + Program.physicsengineinstancex * (yy + Program.physicsengineinstancey * zz);

                                        try
                                        {
                                            for (int x = 0; x < Program.worldwidth; x++)
                                            {
                                                for (int y = 0; y < Program.worldheight; y++)
                                                {
                                                    for (int z = 0; z < Program.worlddepth; z++)
                                                    {
                                                        var indexer1 = x + Program.worldwidth * (y + Program.worldheight * z);
                                                        object somedata00 = (object)_sc_jitter_tasks[indexer0][indexer1]._world_data[0];
                                                        World jitterworld = (World)somedata00;

                                                        if (jitterworld != null)
                                                        {
                                                            deltaTime = timeStopWatch00.Elapsed.Seconds * 2;

                                                            if (deltaTime > 1.0f * 0.01f)
                                                            {
                                                                deltaTime = 1.0f * 0.01f;
                                                            }

                                                            jitterworld.Step(deltaTime, true);
                                                        }

                                                    }
                                                }
                                            }
                                        }
                                        catch
                                        {

                                        }
                                    }
                                }
                            }
                            //MessageBox((IntPtr)0, thisthreadticks.ElapsedTicks + "", "sc core systems message", 0);
                            //Console.WriteLine("ticks: " + thisthreadticks.ElapsedTicks);
                            Thread.Sleep(1);
                            goto threadlooper;
                        }
                        catch (Exception ex)
                        {

                        }

                    }, 0);

                    main_thread_update.IsBackground = true;
                    //main_thread_update.SetApartmentState(ApartmentState.STA);
                    main_thread_update.Start();








                    
                    //var tasker = new Task(() =>
                    //{
                    //
                    //});
                    //tasker.Start();

                    //BackgroundWorker_00 = new BackgroundWorker();
                    //BackgroundWorker_00.DoWork += (object sender, DoWorkEventArgs argers) =>
                    //{
                    ///    
                    //};

                    //BackgroundWorker_00.RunWorkerCompleted += delegate (object sender, RunWorkerCompletedEventArgs argers)
                    //{
                    //
                    //};

                    //BackgroundWorker_00.RunWorkerAsync();

                    BackgroundWorker_00 = new BackgroundWorker();
                    BackgroundWorker_00.DoWork += (object sender, DoWorkEventArgs argers) =>
                    {
                        //Program.MessageBox((IntPtr)0, "threadstart succes", "sc core systems message", 0);
                        Stopwatch _this_thread_ticks_01 = new Stopwatch();
                        _this_thread_ticks_01.Start();

                    _thread_looper:

                        _sc_jitter_tasks = _graphics_sec.loop_worlds(_sc_jitter_tasks, originRot, rotatingMatrix, hmdmatrixRot, hmd_matrix, rotatingMatrixForPelvis, _rightTouchMatrix, _leftTouchMatrix);

                        //_ticks_watch.Restart();

                        //Console.WriteLine(_ticks_watch.Elapsed.Ticks);
                        Thread.Sleep(1);
                        goto _thread_looper;
                    };

                    BackgroundWorker_00.RunWorkerCompleted += delegate (object sender, RunWorkerCompletedEventArgs argers)
                    {

                    };

                    BackgroundWorker_00.RunWorkerAsync();



                    _start_background_worker_01 = 1;
                }
            }*/
            //END OF PHYSICS ENGINE STEPS


            /*
            try
            {
                if (_can_work_physics == 1 && Program.physicsenabled == 1)
                {
                    //if (writetobuffer == 0)
                    {
                        _sc_jitter_tasks = _graphics_sec.sc_write_to_buffer(_sc_jitter_tasks);

                        writetobuffer = 1;
                    }

                }
            }
            catch (Exception ex)
            {
                Program.MessageBox((IntPtr)0, "" + ex.ToString(), "sc core systems message", 0);
            }
            _ticks_watch.Restart();*/





            try
            {

                Vector3f[] hmdToEyeViewOffsets = { eyeTextures[0].HmdToEyeViewOffset, eyeTextures[1].HmdToEyeViewOffset };
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

                    device.ImmediateContext.OutputMerger.SetRenderTargets(eyeTexture.DepthStencilView, eyeTexture.RenderTargetViews[textureIndex]);
                    device.ImmediateContext.ClearRenderTargetView(eyeTexture.RenderTargetViews[textureIndex], somevrbackgroundcolor); //DimGray //Black
                    device.ImmediateContext.ClearDepthStencilView(eyeTexture.DepthStencilView, DepthStencilClearFlags.Depth | DepthStencilClearFlags.Stencil, 1.0f, 0);
                    device.ImmediateContext.Rasterizer.SetViewport(eyeTexture.Viewport);






                    /*
                    eyeQuaternionMatrix = SharpDX.Matrix.RotationQuaternion(new SharpDX.Quaternion(eyePoses[eyeIndex].Orientation.X, eyePoses[eyeIndex].Orientation.Y, eyePoses[eyeIndex].Orientation.Z, eyePoses[eyeIndex].Orientation.W));

                    eyePos = SharpDX.Vector3.Transform(new SharpDX.Vector3(eyePoses[eyeIndex].Position.X, eyePoses[eyeIndex].Position.Y, eyePoses[eyeIndex].Position.Z), originRot * rotatingMatrix * rotatingMatrixForPelvis * hmdmatrixRot).ToVector3();

                    //finalRotationMatrix = eyeQuaternionMatrix * originRot * rotatingMatrix;
                    finalRotationMatrix = eyeQuaternionMatrix * originRot * rotatingMatrix * rotatingMatrixForPelvis * hmdmatrixRot;

                    lookUp = Vector3.Transform(new Vector3(0, 1, 0), finalRotationMatrix).ToVector3();
                    lookAt = Vector3.Transform(new Vector3(0, 0, -1), finalRotationMatrix).ToVector3();

                    viewPosition = eyePos + OFFSETPOS;*/
                    //viewPosition.Y += eyePos.Y;





                    //viewMatrix = Matrix.LookAtRH(viewPosition, viewPosition + lookAt, lookUp);
                    //Quaternion quatt;
                    //Quaternion.RotationMatrix(ref viewMatrix, out quatt);
                    //quatt.Invert();
                    //Vector3 forwardOVR = sc_maths._newgetdirforward(quatt);
                    //forwardOVR.Normalize();
                    //forwardOVR *= 0.15f;
                    //viewPosition = viewPosition + (forwardOVR);
                    //viewMatrix = Matrix.LookAtRH(viewPosition, viewPosition + lookAt, lookUp);





                   


                    //ADDING A THIRD PERSON VIEW TO THE CHARACTER IN VR
                    //ADDING A THIRD PERSON VIEW TO THE CHARACTER IN VR
                    //ADDING A THIRD PERSON VIEW TO THE CHARACTER IN VR WITH AN INVERSE KINEMATICS FROM WOLFRAM TUTORIAL AND FROM THE HELP OF A YOUTUBER DEVELOPPER'S PROGRAM IN UNITY3D WHO WAS SHARING HIS INVERSE KINEMATICS "WITHOUT VR"... But the mathf
                    //functions of unity3d were not compatible with sharpdx so i had to make my own project of understanding and figuring out how it worked and leaving myself the most easy to read variable names was going to do the trick for anyone
                    //reading a piece of code. As far as i know, if i am cockblocked from making my own best version of tutorials on youtube, i cannot leave behind the ones who i learned from, line by line and scripts by scripts, and learning from 
                    //modding inside of Javascript Ecma 5 for Void Expanse has given me something i never recognized enough, the ability to understand almost freely, when i don't leave my work behind for so long, a 2D x and y axis incorporating 
                    //2D pathfinding ultra performnant frame per frame linkable tiny chunks for an ever or limited spiral infinite search... It's all learned from the super well taught sebastian lague tutorial on youtube until tutorial series episode 9.
                    //It was working from there in the labyrinth i had made for my unity3d projects which is a somewhat infinite version of a labyrinth but still not working in void expanse as expected. But prior to the pathfinding of Sebastian Lague
                    //there was a first pathfinding tutorial which i could understanding things up to a point... After a point, i didn't understand how it was working. But then sebastian lagues tutorial on youtube demystified some things about nodes,
                    //and neighboor nodes. It's not working as expected yet though as the character 3d person camera is giving heartaches. Use at your caution for the moment.
                    //was explaining things differently.
                    //tempmatter = hmd_matrix * rotatingMatrixForPelvis * hmdmatrixRot;
                    //Quaternion.RotationMatrix(ref tempmatter, out quatt);
                    //Vector3 forwardOVR = sc_maths._getDirection(Vector3.ForwardRH, quatt);
                    //forwardOVR.Normalize();
                    //forwardOVR *= -0.5f; // -1.0f
                    //viewPosition = viewPosition + (forwardOVR);



                    /*
                    //somefunctioncaller to update
                    if (_can_work_physics == 1 && Program.physicsenabled == 1)
                    {
                        viewMatrix = Matrix.LookAtRH(viewPosition, viewPosition + lookAt, lookUp);

                        _projectionMatrix = OVR.Matrix4f_Projection(eyeTexture.FieldOfView, 0.1f, 1000.0f, ProjectionModifier.None).ToMatrix();

                        Matrix oriProjectionMatrix = _projectionMatrix;

                        _projectionMatrix.Transpose();
                        _sc_jitter_tasks = _graphics_sec.workOnSomething(_sc_jitter_tasks, viewMatrix, _projectionMatrix, OFFSETPOS, originRot, rotatingMatrix, hmdmatrixRot, hmd_matrix, rotatingMatrixForPelvis, _rightTouchMatrix, _leftTouchMatrix, handPoseRight, handPoseLeft, oriProjectionMatrix); // ?? i cant call the abstract function.                                                                                                                                                                                                                                               //_sc_jitter_tasks = Program._graphics_sec.workOnSomething(_sc_jitter_tasks, viewMatrix, _projectionMatrix, OFFSETPOS, originRot, rotatingMatrix, rotatingMatrixForPelvis, _rightTouchMatrix, _leftTouchMatrix, handPoseRight, handPoseLeft); // ?? i cant call the abstract function.
                    }*/
                    
                    result = eyeTexture.SwapTextureSet.Commit();
                    WriteErrorDetails(OVR, result, "Failed to commit the swap chain texture.");
                }
                
                //_ticks_watch.Restart();

                result = OVR.SubmitFrame(sessionPtr, 0L, IntPtr.Zero, ref layerEyeFov);

                //Console.WriteLine(_ticks_watch.Elapsed.Milliseconds);

                WriteErrorDetails(OVR, result, "Failed to submit the frame of the current layers.");
                DeviceContext.CopyResource(mirrorTextureD3D, BackBuffer);
                SwapChain.Present(0, PresentFlags.None);
            }
            catch (Exception ex)
            {
                Program.MessageBox((IntPtr)0, "" + ex.ToString(), "sc core systems message", 0);
            }




            //Console.WriteLine(_ticks_watch.Elapsed.Ticks);
















            /*
            for (int i = 0; i < 3;)
            {
                _failed_screen_capture = 0;
                try
                {
                    somesccsscreencapturedata = somesccsscreencapture.ScreenCapture(0);
                }
                catch (Exception ex)
                {
                    somesccsscreencapture = new sccssharpdxscreencapture(0, 0, device);
                    _failed_screen_capture = 1;
                }
                i++;
                if (_failed_screen_capture == 0)
                {
                    break;
                }
            }






            /*if (framecounterforpctoarduinoscreenswtc == 0)
            {
                arduinotickswatch.Restart();
                framecounterforpctoarduinoscreenswtc = 1;
            }
            
            if (framecounterforpctoarduinoscreenswtc == 0)
            {
                arduinotickswatch.Restart();
                framecounterforpctoarduinoscreenswtc = 1;
            }


            if (Program.useArduinoOVRTouchKeymapper == 1)
            {

                if (arduinotickswatch.Elapsed.Ticks >= 5)
                {
                    framecounterforpctoarduinoscreenMax = 20;


                    if (serialPort != null) //latency visible at 1 millisecond with a simple counter
                    {
                        string somemsgforarduino = framecounterforpctoarduinoscreenFinal + "";

                        //"#sccs#" + framecounterforpctoarduinoscreenFinal + "#sccs#";
                        //serialPort.Write(somemsgforarduino);
                        //serialPort.DataBits = 8;
                        //serialPort.StopBits = System.IO.Ports.StopBits.One;
                        //serialPort.Parity = System.IO.Ports.Parity.None;
                        //serialPort.Open();

                        /*var updateMainUITitle = new Action(() =>
                        {
                           
                        });

                        System.Windows.Threading.Dispatcher.Invoke(updateMainUITitle);
                        serialPort.Write(somemsgforarduino); // + "\n"
                        //serialPort.WriteLine(somemsgforarduino);
                        //serialPort.Close();
                        //serialPort.
                        framecounterforpctoarduinoscreenFinal++;
                        arduinotickswatch.Restart();
                    }



                    if (framecounterforpctoarduinoscreen >= framecounterforpctoarduinoscreenMax)
                    {
                        if (serialPort == null)
                        {
                            //Console.WriteLine("the serial port is null");

                        }

                        /*if (somesccsscreencapturedata.somebitmapforarduino != null)
                        {
                            Console.WriteLine("the somesccsscreencapturedata.somebitmapforarduino is != null");
                        }

                        if (serialPort != null && somesccsscreencapturedata.somebitmapforarduino != null)
                        {
                            //Console.WriteLine("test");
                            //int memoryBitmapStride = 4;// somesccsscreencapturedata.somebitmapforarduino.Width * 4;

                            /*int columns = somesccsscreencapturedata.somebitmapforarduino.Width;
                            int rows = somesccsscreencapturedata.somebitmapforarduino.Height;

                            System.Drawing.Rectangle rect = new System.Drawing.Rectangle(0, 0, somesccsscreencapturedata.somebitmapforarduino.Width, somesccsscreencapturedata.somebitmapforarduino.Height);
                            var somebitmapdata = somesccsscreencapturedata.somebitmapforarduino.LockBits(rect, System.Drawing.Imaging.ImageLockMode.ReadWrite, somesccsscreencapturedata.somebitmapforarduino.PixelFormat);
                            var bitmapscanptr = somebitmapdata.Scan0;
                            int memoryBitmapStride = somebitmapdata.Stride;
                            var _bytesTotal = Math.Abs(somebitmapdata.Stride) * somebitmapdata.Height;

                            byte[] somebytearray = new byte[_bytesTotal];
                            
                            byte[][] arrayOfSmallerArrays = new byte[10][];

                            int sometotal = 0;
                            for (int i = 0; i < arrayOfSmallerArrays.Length; i++)
                            {
                                arrayOfSmallerArrays[i] = new byte[_bytesTotal/10];

                                for (int j = 0; j < arrayOfSmallerArrays[i].Length; j++)
                                {
                                    arrayOfSmallerArrays[i][j] = somebytearray[sometotal];
                                    sometotal++;
                                }
                            }*/
                            /*
                            for (int y = 0; y < somesccsscreencapturedata.somebitmapforarduino.Height; y++)
                            {
                                somebytearray = new byte[_bytesTotal];
                                Marshal.Copy(bitmapscanptr + y * memoryBitmapStride, somebytearray, y * memoryBitmapStride, memoryBitmapStride);

                                //string somemsgforarduino = "";

                                for (int i = 0; i < arrayOfSmallerArrays.Length; i++)
                                {
                                    //Console.WriteLine(arrayOfSmallerArrays[i].Length);
                                    for (int j = 0; j < arrayOfSmallerArrays[i].Length; j++)
                                    {
                                        serialPort.Write(arrayOfSmallerArrays[i], 0, arrayOfSmallerArrays[i].Length);//
                                        //somemsgforarduino += arrayOfSmallerArrays[i][j];
                                    }
                                }

                                //int sometestint = 1111111111;
                                //byte[] b = G



                                /*for (int i = 0; i < somebytearray.Length; i++)
                                {
                                    somemsgforarduino += somebytearray[i];
                                }



                                // Console.WriteLine("" + somebytearray.Length);
                                //serialPort.Write(somebytearray,0, somebytearray.Length);// somemsgforarduino); //"" + somebytearray.Length
                                //serialPort.Write(somebytearray, 0, somebytearray.Length);//


                            }






                            somesccsscreencapturedata.somebitmapforarduino.UnlockBits(somebitmapdata);
                        }
                        framecounterforpctoarduinoscreen = 0;
                    }
                }
                framecounterforpctoarduinoscreen++;
            }*/


            //###SC start physics on frame 1 instead of 0
            _can_work_physics = 1;
            _can_work_physics_objects = 1;
            //###SC start physics on frame 1 instead of 0
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

