using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Threading;
using System.Threading.Tasks;

using Jitter.LinearMath;
using System.Diagnostics;

using System.Runtime.InteropServices;
using System.IO;

using Jitter;
using Jitter.Dynamics;
using Jitter.Collision;

using Jitter.Collision.Shapes;

using System.Collections;

using System.Runtime;
using System.Runtime.CompilerServices;

using System.ComponentModel;

//using SC_message_object_jitter = _sc_message_object._sc_message_object_jitter;
//using SC_message_object = _sc_message_object._sc_message_object;
//https://jeremylindsayni.wordpress.com/2019/02/11/instantiating-a-c-object-from-a-string-using-activator-createinstance-in-net/



namespace Jitter
{
    public interface ISCCS_Jitter_Interface
    {
        jitter_sc sc_create_jitter_instances(sc_jitter_data _sc_jitter_data);
    }

    public class jitter_sc : ISCCS_Jitter_Interface
    {

        public int Id { get; set; }
        public string Name { get; set; }


        public static sc_jitter_data sc_jitter_data;

        public jitter_sc sc_create_jitter_instances(sc_jitter_data _sc_jitter_data)
        {
            sc_jitter_data = _sc_jitter_data;
            return Instance;
        }


        jitter_sc instance = null;

        public jitter_sc Instance
        {
            get
            {
                if (instance == null)
                {
                    //Console.WriteLine("instance == null");
                    instance = new jitter_sc();
                }
                else
                {
                    Initialize();
                }
                return instance;
            }
        }

        public jitter_sc()
        {
            instance = this;
            Initialize();
        }














        public int _startTasker { get; set; }

        /*public interface ITestClass
        {
            //_sc_jitter_physics _jitter_hook();
        }*/

        /*public _sc_jitter_physics _jitter_hook()
        {
            return Instance;
        }*/

       

        object[] mainobject = new object[1];


        public void Initialize()
        {
            _sc_create_jitter_world(sc_jitter_data);

            /*mainobject[0] = new object();

            var _mainTasker00 = Task<object[]>.Factory.StartNew((tester0000) =>
            {
                


            _thread_main_loop:


                Thread.Sleep(1);
                goto _thread_main_loop;
            }, mainobject);

            _mainTasker00.Start();*/


            /*Task _mainTasker00 = new TaskFactory<object>((tester0000) =>
            {
            _thread_main_loop:

       
                Thread.Sleep(0);
                goto _thread_main_loop;
            }, 9999999); //100000 //999999999*/

            //_mainTasker00.IsBackground = true;
            //_mainTasker00.SetApartmentState(ApartmentState.STA);
            //_mainTasker00.Start();

        }



        //BackgroundWorker[] _backg_worker;
        //Task[] _array_tasks;
        [DllImport("User32.dll", CharSet = CharSet.Unicode)]
        public static extern int MessageBox(IntPtr h, string m, string c, int type);

        static int _start_physics_worker_000 = 0;
        Task _console_worker_task_000;
        static int _start_physics_worker_00 = 0;
        Task _console_worker_task;
        static int _set_world = 0;
        static float[] _accumulated_times;

        static float deltaTime;

        static int _width = 4;
        static int _height = 1;
        static int _depth = 4;

        public static World[] _world_array;
        static CollisionSystemPersistentSAP[] _collisionSAP;
        //public World _some_world;
        static Stopwatch _frame_lag_checker = new Stopwatch();
        static float[] _array_stop_watch_tick;
        static float[] _delta_time_array;
        //ThreadManager[] _threadManagers;

        public void _sc_create_jitter_world(sc_jitter_data _sc_jitter_data) //World[]
        {
            _width = _sc_jitter_data.width;
            _height = _sc_jitter_data.height;
            _depth = _sc_jitter_data.depth;

            //Console.WriteLine(_width * _height * _depth);

            _world_array = new World[_width * _height * _depth];
            _collisionSAP = new CollisionSystemPersistentSAP[_width * _height * _depth];
            _delta_time_array = new float[_width * _height * _depth];
            _accumulated_times = new float[_width * _height * _depth];
            _array_stop_watch_tick = new float[_width * _height * _depth];
            //_array_tasks = new Task[width * height * depth];
            // _backg_worker = new BackgroundWorker[_width * _height * _depth];
            //_threadManagers = new ThreadManager[_width * _height * _depth];
            //_workers = new _task_worker[_width * _height * _depth];

            for (int x = 0; x < _width; x++)
            {
                for (int y = 0; y < _height; y++)
                {
                    for (int z = 0; z < _depth; z++)
                    {
                        int _index = x + _width * (y + _height * z);

                        //_threadManagers[_index] = new ThreadManager();

                        _array_stop_watch_tick[_index] = 0;
                        _collisionSAP[_index] = new CollisionSystemPersistentSAP(); //_threadManagers[_index]
                        World _the_world = new World(_collisionSAP[_index]);
                        //_the_world.threadManager = _threadManagers[_index];

                        _the_world.AllowDeactivation = _sc_jitter_data.alloweddeactivation;
                        _the_world.Gravity = _sc_jitter_data.gravity; //9.81f //19.62f
                        _the_world.SetIterations(_sc_jitter_data.smalliterations, _sc_jitter_data.iterations);
                        _the_world.ContactSettings.AllowedPenetration = _sc_jitter_data.allowedpenetration;

                        //_the_world.threadManager.Initialize();
                        _world_array[_index] = _the_world;

                        //_array_of_tasks[_index] = 
                        int _taskID = _index;

                        /*var tester = Task<>.Factory.StartNew((tester00) => //_tasks_array[_work_index]
                        {
                        _thread_looper_sec:
                            Thread.Sleep(0);
                            goto _thread_looper_sec;
                        }, _world_array);*/
                    }
                }
            }
            DoWork(0, 0);
            _startTasker = 1;
            //Console.WriteLine("created worlds CURRENT TEST");

            //return _world_array;
        }

        public World return_world(int index)
        {
            return _world_array[index];
        }

        public static async Task DoWork(int timeOut, int _taskID)
        {
            DateTime time1 = DateTime.Now;
            DateTime time2 = DateTime.Now;
            Stopwatch _stop_watch = new Stopwatch();
            Stopwatch timeStopWatch01 = new Stopwatch();
            int _StopWatch2_counter = 1;
            int _time2_counter = 1;


            int _swtch_counter_000 = 0;
            int _swtch_counter_001 = 0;
            int _swtch_counter_002 = 0;
            int _swtch_counter_003 = 0;

            int DoWork_stopwatch_counter = 1;
            int DoWork_time_counter = 1;

            float _frame_delta_time_divider_dowork = 100000000;
            float _time_delta_time_divider_dowork = 10000000;
            float delta_time_before = 0;

            DateTime time_start;
            float _last_frame_stop_watch = 0;
            float startTime = (float)(_stop_watch.ElapsedTicks);


            float _last_ticks_per_something = 0;
            //time_start = DateTime.Now;
            int _swtch = 0;

            float accumulatedTimes = 0;
            float totalTime = 0;
            int maxSteps = 250;
            float _slowingtimenot = 0.001f;
        _threadLoop:

            if (_swtch == 0)
            {

                if (_swtch_counter_000 >= 0)
                {
                    _stop_watch.Start();
                    _swtch_counter_000 = 0;
                }
                if (_swtch_counter_001 >= DoWork_stopwatch_counter)
                {
                    timeStopWatch01.Start();
                    _swtch_counter_001 = 0;
                    _swtch = 1;
                }
            }
            else
            {
                /*if (_swtch_counter_000 >= 0)
                {
                    _stop_watch.Stop();
                    _swtch_counter_000 = 0;
                }
                if (_swtch_counter_001 >= DoWork_stopwatch_counter)
                {
                    timeStopWatch01.Stop();
                    _swtch_counter_001 = 0;
                    _swtch = 2;
                }*/
            }
            if (_swtch_counter_002 >= 0)
            {
                time1 = DateTime.Now;
                _swtch_counter_002 = 0;
            }
            if (_swtch_counter_003 >= DoWork_time_counter)
            {
                time2 = DateTime.Now;
                _swtch_counter_003 = 0;
            }





            //FRAME DELTATIME // TICKS PER FRAME
            var _current_ticks_per_something = ((float)Math.Abs((timeStopWatch01.Elapsed.Ticks - _stop_watch.Elapsed.Ticks)));
            var _ticks_per_frame = (((time2.Ticks - time1.Ticks)));

            var current_delta_time = _stop_watch.ElapsedTicks;

            var difference = (_last_frame_stop_watch - current_delta_time);


            //_array_stop_watch_tick[_taskID] = (float)Math.Abs(_ticks_per_something - _ticks_per_frame - difference - _delta_timer_time - _delta_timer_frame) * 0.0000001f;

            /*var diff = (float)Math.Abs(_last_ticks_per_something - _current_ticks_per_something);

            if (_last_ticks_per_something > _current_ticks_per_something)
            {
                _array_stop_watch_tick[_taskID] = (float)Math.Abs(_current_ticks_per_something + diff);
            }
            else
            {
                _array_stop_watch_tick[_taskID] = (float)Math.Abs(_current_ticks_per_something + diff);
            }*/



            _array_stop_watch_tick[_taskID] = (float)Math.Abs(difference);







            _last_ticks_per_something = _current_ticks_per_something;
            _last_frame_stop_watch = current_delta_time;// _stop_watch.ElapsedTicks;

            //time1 = time2;
            await Task.Delay(1);
            Thread.Sleep(timeOut);

            _swtch_counter_000++;
            _swtch_counter_001++;
            _swtch_counter_002++;
            _swtch_counter_003++;

            goto _threadLoop;
        }


        int _swtch_counter_00 = 0;
        int _swtch_counter_01 = 0;
        int _swtch_counter_02 = 0;
        int _swtch_counter_03 = 0;




        float _delta_timer_frame = 0;
        float _delta_timer_time = 0;





        int _thread_result = -1;
        int _sc_main_swtch_test = 0;
        //SC_message_object_jitter[] _thread_safor, int _world_index


        public struct _task_worker
        {
            public Task _worker_task;
            public int _worker_task_id;
            //public SC_object_messenger_dispatcher_jitter_col_sys _world_obj;
            public World _world;
            public float _time_stepper;
        }






        int _someInt = 0;
        int indexer = 0;

        int _counter_task_creation = 0;
        _task_worker[] _workers;

        int _swtch = 0;

        public jitter_sc _sc_jitter_world_input() //SC_object_messenger_dispatcher_jitter_col_sys _thread_safor
        {
            try
            {
                if (_startTasker == 1)
                {
                    for (int x = 0; x < _width; x++)
                    {
                        for (int y = 0; y < _height; y++)
                        {
                            for (int z = 0; z < _depth; z++)
                            {
                                int _indexer = x + _width * (y + _height * z);

                                _world_array[_indexer].Step(_array_stop_watch_tick[0], true);

                            }
                        }
                    }
                    /*var _mainTasker000 = new Thread((tester0000) =>
                    {
                    _thread_main_loop:

                     

                        Thread.Sleep(0);
                        goto _thread_main_loop;

                    }, _someInt);
                    _mainTasker000.IsBackground = true;
                    //_mainTasker000.SetApartmentState = ApartmentState.STA;
                    _mainTasker000.Start();

                    _startTasker = 2;*/
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            return this;
        }











        /*if (_current_ticks_per_something > 1.0f * _some_max)
        {
            _current_ticks_per_something = 1.0f * _some_max;
        }*/


        /*var diff = (float)Math.Abs(_last_ticks_per_something - _current_ticks_per_something);

        if (_last_ticks_per_something > _current_ticks_per_something) // catching up
        {

            _script_physics._world_array[_taskID].StepTwo(_array_stop_watch_tick[_taskID], true);
            //_array_stop_watch_tick[_taskID] = (float)Math.Abs(_current_ticks_per_something + diff);
        }
        else // catching up
        {
            int counter = 0;


            /*var someTotal = timestep / accumulatedTime;

            if (accumulatedTime > timestep)
            {
                for (int i = 0; i < someTotal; i++)
                {
                    Step(timestep, multithread);

                    accumulatedTime -= timestep;
                    counter++;

                    if (counter > maxSteps)
                    {
                        // okay, okay... we can't keep up
                        accumulatedTime = 0.0f;
                        break;
                    }
                }
            }



            counter = 0;
            accumulatedTimes += totalTime;



            var timestep = current_delta_time * _slowingtimenot;
            var total = Math.Abs(timestep - accumulatedTimes);

            for (int i = 0; i < total; i++)
            {
                _script_physics._world_array[_taskID].StepTwo(_array_stop_watch_tick[_taskID], true);

                accumulatedTimes -= timestep;
                counter++;

                if (counter > maxSteps)
                {
                    // okay, okay... we can't keep up
                    accumulatedTimes = 0.0f;
                    break;
                }
            }
            //_array_stop_watch_tick[_taskID] = (float)Math.Abs(_current_ticks_per_something + diff);
        }*/









        /*lock (_array_stop_watch_tick)
        {
            _array_stop_watch_tick[_taskID] = _ticks_per_frame;
        }*/

        //_delta_timer_frame *= 10;
        //time2 = DateTime.Now;
        //_delta_timer_time = (((time2.Ticks - time1.Ticks) / _time_delta_time_divider_dowork) / _time2_counter); //100000000f

        //_delta_timer_time = time_start.Ticks - time1.


        //_delta_timer_time += time_start.Ticks;










        //time1 = time2;
        /*if (_delta_timer_frame > 1.0f * _some_max)
        {
            _delta_timer_frame = 1.0f * _some_max;
        }

        if (_delta_timer_time > 1.0f * _some_max)
        {
            _delta_timer_time = 1.0f * _some_max;
        }*/

        //delta_time_before = (float)Math.Abs(difference);// _delta_timer_frame); //_delta_timer_time //_delta_timer_time

















        //object[]  _object_array = (object[])_thread_safor[_world_index]._world_data;
        //World _some_world = (World)_object_array[1];

        //World _some_world = (World)_thread_safor[world_index]._world_data[1];

        //_frame_lag_checker.Stop();
        //_frame_lag_checker.Reset();
        //_frame_lag_checker.Start();

        /*if (_startTasker == 1)
        {

            //Task[] taskArray = new Task[_script_physics._width * _script_physics._height * _script_physics._depth];


            float[] _accumulated_time;
            _accumulated_time = new float[this._width * this._height * this._depth];
            //Task[] _array_of_tasks;
            //_array_of_tasks = new Task[_script_physics._width * _script_physics._height * _script_physics._depth];

            /*_to_send_to_threadmanager_StepThree _step_three = new _to_send_to_threadmanager_StepThree();
            SC_object_messenger_dispatcher_jitter_col_sys[] _world_objects = new SC_object_messenger_dispatcher_jitter_col_sys[this._width * this._height * this._depth];
            _to_send_to_threadmanager_StepFive _step_five = new _to_send_to_threadmanager_StepFive();
            _to_send_to_threadmanager_StepSix _step_six = new _to_send_to_threadmanager_StepSix();
            _to_send_to_threadmanager_StepSeven _step_seven = new _to_send_to_threadmanager_StepSeven();

            if (_swtch == 0)
            {
                //_task_worker _worker = new _task_worker();
                //int _index = 0;
                int x = 0;
                int y = 0;
                int z = 0;
                //int _index = 0;

                for (x = 0; x < this._width; x++)
                {
                    for (y = 0; y < this._height; y++)
                    {
                        for (z = 0; z < this._depth; z++)
                        {
                            int _index = x + this._width * (y + this._height * z);

                            _world_objects[_index] = new SC_object_messenger_dispatcher_jitter_col_sys();

                            _workers[_index] = new _task_worker();
                            _workers[_index]._worker_task_id = _index;
                            _workers[_index]._world_obj = _world_objects[_index];
                            _workers[_index]._world = _world_array[_index];
                            _workers[_index]._world_obj._step_three_data = _step_three;
                            _workers[_index]._world_obj._step_five_data = _step_five;
                            _workers[_index]._world_obj._step_Six_data = _step_six;
                            _workers[_index]._world_obj._step_Seven_data = _step_seven;
                            _workers[_index]._world_obj._step_Seven_data._rigidbody = new List<RigidBody>();
                            //_workers[_index]._world_obj._collision_sap = _collisionSAP[_index];

                            _workers[_index]._world_obj._time_step = _array_stop_watch_tick[_index];
                            _workers[_index]._world_obj._detect_two_data = new _to_send_to_threadmanager_DetectTwo();
                        }
                    }
                }
                _swtch = 1;
            }

            var _mainTasker000 = new Thread((tester0000) =>
            {
            _thread_main_loop:

                Thread.Sleep(0);
                goto _thread_main_loop;

            }, _someInt);
            _mainTasker000.IsBackground = true;
            _mainTasker000.Start();

            _startTasker = 1;
        }*/










        /*_workers[_indexer]._world_obj._time_step = _array_stop_watch_tick[_indexer];

        if (_workers[_indexer]._world_obj._time_step > 1.0f * 0.01f)
        {
            _workers[_indexer]._world_obj._time_step = 1.0f * 0.01f;
        }*/

        /*if (_indexer < (this._width * this._height * this._depth))
        {
            _workers[_indexer]._world_obj._AddedObjectsBruteForceIsUsed = _collisionSAP[_indexer].AddedObjectsBruteForceIsUsed;
            _workers[_indexer]._world_obj._fullOverlaps = _collisionSAP[_indexer].fullOverlaps;
            _workers[_indexer]._world_obj._activeList = _collisionSAP[_indexer].activeList;
            _workers[_indexer]._world_obj._axis1 = _collisionSAP[_indexer].axis1;
            _workers[_indexer]._world_obj._axis2 = _collisionSAP[_indexer].axis2;
            _workers[_indexer]._world_obj._axis3 = _collisionSAP[_indexer].axis3;
            _workers[_indexer]._world_obj._addCounter = _collisionSAP[_indexer].addCounter;
            _workers[_indexer]._world_obj._sortCallback = _collisionSAP[_indexer].sortCallback;

            _workers[_indexer]._world_obj = _workers[_indexer]._world._step_one_00(_workers[_indexer]._world_obj);
            //_workers[_indexer]._world_obj = _script_physics._world_array[_indexer].Stepper(10, true, _workers[_indexer]._world_obj._time_step, 500, _accumulated_time[_indexer], _workers[_indexer]._world_obj);

            for (int i = 0; i < _workers[_indexer]._world_obj._detect_one_data._someAction_axis_1.Count; i++)
            {
                _workers[_indexer]._world.threadManager.AddTask(_workers[_indexer]._world_obj._detect_one_data._someAction_axis_1[i], _workers[_indexer]._world_obj._detect_one_data._param_axis_1[i]);
                _workers[_indexer]._world.threadManager.AddTask(_workers[_indexer]._world_obj._detect_one_data._someAction_axis_2[i], _workers[_indexer]._world_obj._detect_one_data._param_axis_2[i]);
                _workers[_indexer]._world.threadManager.AddTask(_workers[_indexer]._world_obj._detect_one_data._someAction_axis_3[i], _workers[_indexer]._world_obj._detect_one_data._param_axis_3[i]);

                _workers[_indexer]._world.threadManager.Execute();
            }

            _collisionSAP[_indexer].AddedObjectsBruteForceIsUsed = _workers[_indexer]._world_obj._AddedObjectsBruteForceIsUsed;
            _collisionSAP[_indexer].fullOverlaps = _workers[_indexer]._world_obj._fullOverlaps;
            _collisionSAP[_indexer].activeList = _workers[_indexer]._world_obj._activeList;
            _collisionSAP[_indexer].axis1 = _workers[_indexer]._world_obj._axis1;
            _collisionSAP[_indexer].axis2 = _workers[_indexer]._world_obj._axis2;
            _collisionSAP[_indexer].axis3 = _workers[_indexer]._world_obj._axis3;
            _collisionSAP[_indexer].addCounter = _workers[_indexer]._world_obj._addCounter;
            _collisionSAP[_indexer].sortCallback = _workers[_indexer]._world_obj._sortCallback;




            try
            {
                _workers[_indexer]._world_obj._detect_two_data._fullOverlaps = _collisionSAP[_indexer].fullOverlaps;

                _workers[_indexer]._world_obj = _workers[_indexer]._world._step_one_01(_workers[_indexer]._world_obj);

                for (int i = 0; i < _workers[_indexer]._world_obj._detect_two_data._someAction_det_cb_1.Count; i++)
                {
                    _workers[_indexer]._world.threadManager.AddTask(_workers[_indexer]._world_obj._detect_two_data._someAction_det_cb_1[i], _workers[_indexer]._world_obj._detect_two_data._param_det_cb_1[i]);
                }

                _workers[_indexer]._world.threadManager.Execute();
                _collisionSAP[_indexer].addCounter = _workers[_indexer]._world_obj._detect_two_data._addCounter;
                _collisionSAP[_indexer].fullOverlaps = _workers[_indexer]._world_obj._detect_two_data._fullOverlaps;
            }
            catch (Exception ex)
            {
                //MessageBox((IntPtr)0, "Task: " + _indexer + " __ " + ex.ToString(), "Oculus error", 0);
            }



            //nothing to do here
            _workers[_indexer]._world_obj = _workers[_indexer]._world._step_two_00(_workers[_indexer]._world_obj);
            //nothing to do here

            //_workers[_indexer]._world_obj._step_Seven_data._world_events = _world_array[_indexer].Events;
            _workers[_indexer]._world_obj = _workers[_indexer]._world._step_two_01(_workers[_indexer]._world_obj);
            //_world_array[_indexer].Events = _workers[_indexer]._world_obj._step_Seven_data._world_events;

            //nothing to do here
            _workers[_indexer]._world_obj = _workers[_indexer]._world._step_two_02(_workers[_indexer]._world_obj);
            //nothing to do here




            try
            {
                _workers[_indexer]._world_obj._step_five_data.contactIterations = _world_array[_indexer].contactIterations;
                _workers[_indexer]._world_obj._arbiterCallback = _world_array[_indexer].arbiterCallback;

                _workers[_indexer]._world_obj = _workers[_indexer]._world._step_three(_workers[_indexer]._world_obj);

                for (int i = 0; i < _workers[_indexer]._world_obj._step_five_data._someAction_det_cb_1.Count; i++)
                {
                    _workers[_indexer]._world.threadManager.AddTask(_workers[_indexer]._world_obj._step_five_data._someAction_det_cb_1[i], _workers[_indexer]._world_obj._step_five_data._param_det_cb_1[i]);
                }

                _workers[_indexer]._world.threadManager.Execute();


                _world_array[_indexer].contactIterations = _workers[_indexer]._world_obj._step_five_data.contactIterations;
                _world_array[_indexer].arbiterCallback = _workers[_indexer]._world_obj._arbiterCallback;
            }
            catch (Exception ex)
            {
                MessageBox((IntPtr)0, "Step5: " + _indexer + " __ " + ex.ToString(), "Oculus error", 0);
            }









            try
            {
                _workers[_indexer]._world_obj._integrateCallback = _world_array[_indexer].integrateCallback;

                _workers[_indexer]._world_obj = _workers[_indexer]._world._step_four(_workers[_indexer]._world_obj);

                for (int i = 0; i < _workers[_indexer]._world_obj._step_Six_data._someAction_det_cb_1.Count; i++)
                {
                    _workers[_indexer]._world.threadManager.AddTask(_workers[_indexer]._world_obj._step_Six_data._someAction_det_cb_1[i], _workers[_indexer]._world_obj._step_Six_data._param_det_cb_1[i]);
                }

                _workers[_indexer]._world.threadManager.Execute();

                _world_array[_indexer].integrateCallback = _workers[_indexer]._world_obj._integrateCallback;

            }
            catch (Exception ex)
            {
                MessageBox((IntPtr)0, "Step6: " + _indexer + " __ " + ex.ToString(), "Oculus error", 0);
            }




            //nothing to do here
            _workers[_indexer]._world_obj = _workers[_indexer]._world._step_five(_workers[_indexer]._world_obj);
            //nothing to do here
        }*/


        /*
        public class _some_task
        {
            _sc_jitter_physics _script_physics;
            int _width;
            int _height;
            int _depth;
            World _world;
            int _taskID;
            float _some_max;

            public _some_task(_sc_jitter_physics script_physics, int width, int height, int depth, World world, int taskID, float some_max)
            {
                this._some_max = some_max;
                this._world = world;
                this._taskID = taskID;
                this._width = width;
                this._height = height;
                this._depth = depth;
                this._script_physics = script_physics;
            }

            public void _just_work(int timeOut) //, out _sc_jitter_physics _script_physiks //async Task 
            {

                World _some_world = _script_physics._world_array[_taskID];
                float[] _accumulated_time;
                _accumulated_time = new float[this._width * this._height * this._depth];

            _threadLoop:

                var delta_time_beforer = _script_physics._array_stop_watch_tick[_taskID];
                if (delta_time_beforer > 1.0f * _some_max)
                {
                    delta_time_beforer = 1.0f * _some_max;
                }

                //delta_time_before *= 100;


                //_accumulated_time[_taskID] = _some_world.Stepper(10, true, delta_time_beforer, 500, _accumulated_time[_taskID]);


                //this._world.Step(delta_time_beforer, true); //_world_array[_taskID].
                _script_physics._accumulated_times = _accumulated_time;
                //await Task.Delay(1);
                Thread.Sleep(timeOut);
                goto _threadLoop;
            }
        }*/
    }
}


/*if (startOnce == 0)
{
    for (int x = 0; x < _script_physics._width; x++)
    {
        for (int y = 0; y < _script_physics._height; y++)
        {
            for (int z = 0; z < _script_physics._depth; z++)
            {
                int _index = x + _script_physics._width * (y + _script_physics._height * z);

            //_thread_looper:
                //MessageBox((IntPtr)0, "col sys 02", "Oculus error", 0);

                //var resulter = (_sc_jitter_physics)args.Result;

                //var tester0 = _script_physics._world_array[_index];
                //var tester1 = _script_physics._array_stop_watch_tick[_index];
                //_script_physics._world_array[_index].Step(_script_physics._array_stop_watch_tick[_index], true);


                /*if(resulter != null)
                {
                    MessageBox((IntPtr)0, "col sys 02", "Oculus error", 0);
                }*/

//Thread.Sleep(1);
//goto _thread_looper;



//World _some_world = _world_array[_index];

//var delta_time_beforer = _script_physics._array_stop_watch_tick[_index];
/*if (delta_time_beforer > 1.0f * _script_physics._some_max)
{
    delta_time_beforer = 1.0f * _script_physics._some_max;
}

//delta_time_before *= 100;


//_accumulated_time[_index] = _script_physics._world_array[_index].Stepper(10, true, delta_time_beforer, 1000, _accumulated_time[_index]);


//_script_physics._world_array[_index].Step(delta_time_beforer, true);

//World _some_world = _world_array[_index];
//_some_task _someTask = new _some_task(_script_physics, _width, _height, _depth, _some_world, _index, _some_max);
//_someTask._just_work(0);

}
}
}

startOnce = 1;
}*/















/*var _task = Task<_sc_jitter_physics>.Factory.StartNew((tester000) => //_tasks_array[_work_index]
{
    int startOnce = 0;

    float[] _accumulated_time;
    _accumulated_time = new float[_script_physics._width * _script_physics._height * _script_physics._depth];
_thread_looper_sec:
    for (int x = 0; x < _script_physics._width; x++)
    {
        for (int y = 0; y < _script_physics._height; y++)
        {
            for (int z = 0; z < _script_physics._depth; z++)
            {
                int _index = x + _script_physics._width * (y + _script_physics._height * z);


                //World _some_world = _world_array[_index];

                var delta_time_beforer = _script_physics._array_stop_watch_tick[_index];
                if (delta_time_beforer > 1.0f * _script_physics._some_max)
                {
                    delta_time_beforer = 1.0f * _script_physics._some_max;
                }

                //delta_time_before *= 100;


                _accumulated_time[_index] = _script_physics._world_array[_index].Stepper(1, true, delta_time_beforer, 100, _accumulated_time[_index]);


                //_world_array[i].Step(delta_time_beforer, true);

                //World _some_world = _world_array[_index];
                //_some_task _someTask = new _some_task(_script_physics, _width, _height, _depth, _some_world, _index, _some_max);
                //_someTask._just_work(0);
            }
        }
    }


    _script_physics._accumulated_times = _accumulated_time;



    if (startOnce == 0)
    {

        /*for (int x = 0; x < this._width; x++)
        {
            for (int y = 0; y < this._height; y++)
            {
                for (int z = 0; z < this._depth; z++)
                {
                    int _index = x + this._width * (y + this._height * z);

                    //var tester = _just_work(0, _index); //_array_tasks[_index] 
                    World _some_world = _world_array[_index];
                    _some_task _someTask = new _some_task(_script_physics,_width,_height,_depth, _some_world,_index,_some_max);
                    _someTask._just_work(0);

                }
            }
        }
        startOnce = 1;
    }


    Thread.Sleep(0);
    goto _thread_looper_sec;

}, _script_physics);*/
/*for (int i = 0;i < _array_tasks.Length;i++)
{



}*/



//_counter_task_creation++;

/*_array_tasks[0] = Task<_sc_jitter_physics>.Factory.StartNew((tester000) => //_tasks_array[_work_index]
{
    World _some_world = _world_array[0];
    float[] _accumulated_time;
    _accumulated_time = new float[this._width * this._height * this._depth];

_thread_looper_sec:


    var delta_time_beforer = _script_physics._array_stop_watch_tick[0];
    if (delta_time_beforer > 1.0f * _some_max)
    {
        delta_time_beforer = 1.0f * _some_max;
    }

    //delta_time_before *= 100;


    _accumulated_time[0] = _some_world.Stepper(10, true, delta_time_beforer, 500, _accumulated_time[0]);


    //_world_array[i].Step(delta_time_beforer, true);
    _script_physics._accumulated_times = _accumulated_time;
    Thread.Sleep(0);
    goto _thread_looper_sec;



}, _script_physics);*/



/*for (int i = 0; i < _array_tasks.Length; i++)
{




}*/













/*
_accumulated_time = _script_physics._accumulated_times;
// _frame_lag_checker.Stop();
//_frame_lag_checker.Reset();
//_frame_lag_checker.Start();

//_frame_lag_checker.Stop();

//onsole.SetCursorPosition(1, 16);
//Console.WriteLine("lag_jitter: " + _frame_lag_checker.Elapsed.Ticks);

var delta_time_beforer = _script_physics._array_stop_watch_tick[_index];
if (delta_time_beforer > 1.0f * _some_max)
{
    delta_time_beforer = 1.0f * _some_max;
}

//delta_time_before *= 100;


_accumulated_time[_index] = _some_world.Stepper(10, true, delta_time_beforer, 500, _accumulated_time[_index]);


//_some_world.Step(delta_time_beforer, true);
_script_physics._accumulated_times = _accumulated_time;*/



/*
for (int x = 0; x < this._width; x++)
{
    for (int y = 0; y < this._height; y++)
    {
        for (int z = 0; z < this._depth; z++)
        {
            int _index = x + this._width * (y + this._height * z);

        }
    }
}*/



//_start_physics_worker_000 = 1;


//_some_world.Step(delta_time_before, true); //_thread_safor, 10, true, 500 //_thread_safor = 




//_the_world.Step(delta_time_before, true); //_thread_safor, 10, true, 500 //_thread_safor = 



//_thread_safor._can_start = 1;
/*_console_worker_task = Task<SC_message_object[]>.Factory.StartNew((test000) => //SC_object_messenger_dispatcher_jitter_col_sys
{
_thread_looper_01:

    if (delta_time_before > 1.0f * _some_max)
    {
        delta_time_before = 1.0f * _some_max;
    }

    _the_world.Step(delta_time_before, true); //_thread_safor, 10, true, 500 //_thread_safor = 

    Thread.Sleep(0);
    goto _thread_looper_01;
}, _thread_safor);*/
//_start_physics_worker_00 = 1;
//_start_physics_worker_000 = 1;

//_start_physics_worker_000 = 1;


/*if (_thread_safor._sc_jitter_can_work == 4)
{
    MessageBox((IntPtr)0, "col sys 02", "Oculus error", 0);
}*/
/*
_console_worker_task_000 = Task<SC_object_messenger_dispatcher_jitter_col_sys>.Factory.StartNew((test00) => //SC_object_messenger_dispatcher_jitter_col_sys
{
_thread_looper:





    /*if (_thread_safor._can_start == 2)
    {
        MessageBox((IntPtr)0, "col sys 02", "Oculus error", 0);
    }*/



//_thread_safe = this.CollisionSystem.DetectOne(_thread_safe);
/*if (_thread_safe._can_start == 1)
{
    MessageBox((IntPtr)0, "col sys 01", "Oculus error", 0);
}
//_thread_safor._can_start = 2;
//_thread_safe = Runner(() => CollisionSystem.DetectOne(_thread_safe, out _thread_safe));

Thread.Sleep(1);
goto _thread_looper;
}, _thread_safor);
_start_physics_worker_000 = 1;*/



//var last = (_sc_jitter_physics)_thread_safor[_thread_safor.Length - 1]._world_data[world_index];


//_thread_safor._can_start = 2;



/*BackgroundWorker threads = new BackgroundWorker();

                                        threads.DoWork += (object sender, DoWorkEventArgs args) =>
                                        {

                                            args.Result = _script_physics;
                                        _thread_looper:
                                            //MessageBox((IntPtr)0, "col sys 02", "Oculus error", 0);

                                            var resulter = (_sc_jitter_physics)args.Result;

                                            //var tester0 = _script_physics._world_array[_index];
                                            //var tester1 = _script_physics._array_stop_watch_tick[_index];
                                            _script_physics._world_array[_index].Step(_script_physics._array_stop_watch_tick[_index], true);


                                            //if(resulter != null)
                                            //{
                                            //    MessageBox((IntPtr)0, "col sys 02", "Oculus error", 0);
                                            //}

                                            Thread.Sleep(1);
                                            //args.Result = _script_physics;



                                            args.Result = _script_physics;
                                            goto _thread_looper;
                                        };

                                        threads.RunWorkerCompleted += delegate (object sender, RunWorkerCompletedEventArgs args)
                                        {

                                        //MessageBox((IntPtr)0, "col sys 02", "Oculus error", 0);
                                        //var resulter = (_sc_jitter_physics)args.Result;


                                        //args.Result = _script_physics;
                                        /*_thread_looper:
                                            //MessageBox((IntPtr)0, "col sys 02", "Oculus error", 0);

                                            var resulter = (_sc_jitter_physics)args.Result;

                                            //var tester0 = _script_physics._world_array[_index];
                                            //var tester1 = _script_physics._array_stop_watch_tick[_index];
                                            _script_physics._world_array[_index].Step(_script_physics._array_stop_watch_tick[_index], true);


                                            //if(resulter != null)
                                            //{
                                            //    MessageBox((IntPtr)0, "col sys 02", "Oculus error", 0);
                                            //}

                                            Thread.Sleep(1);
                                            //args.Result = _script_physics;




                                            goto _thread_looper;

                                            //MessageBox((IntPtr)0, "col sys 02", "Oculus error", 0);
                                            //if (args.Result != null)
                                            //{

                                            //    if (typeof(_sc_jitter_physics) == args.Result.GetType())
                                            //    {
                                            //        MessageBox((IntPtr)0, "col sys 02", "Oculus error", 0);
                                            //    }
                                            //}
                                        };

                                        threads.RunWorkerAsync();*/














































/*int _indexor = 0;
for (x = 0; x < _script_physics._width; x++)
{
    for (y = 0; y < _script_physics._height; y++)
    {
        for (z = 0; z < _script_physics._depth; z++)
        {
            int _index = x + _script_physics._width * (y + _script_physics._height * z);


            if (_index < (_script_physics._width * _script_physics._height * _script_physics._depth))
            {
                //_workers[_index]._world.Step(_workers[_index]._world_obj._time_step, true);// _workers[_index]._world_obj);


                _workers[_index]._worker_task = Task.Factory.StartNew((obj) =>
                {
            _thread_looper_sec:

                    _workers[_index]._world.Step(_workers[_index]._world_obj._time_step, true);// _workers[_index]._world_obj);

                    Thread.Sleep(0);
                    goto _thread_looper_sec;

                }, _index++);*/





/*var _mainTasker_00 = new Thread((obj_00) =>
{
    //var _indexer = _index;

_thread_looper_sec:
    _workers[_index]._world_obj = _workers[_index]._world.Step(_workers[_index]._world_obj);
    Thread.Sleep(0);

    goto _thread_looper_sec;

}, _index);
_mainTasker_00.IsBackground = true;
_mainTasker_00.Start();*/













/*_workers[_indexer]._world_obj._step_Seven_data._rigidbody = new List<RigidBody>();
int ii = 0;
foreach (RigidBody rigid in _world_array[_indexer].RigidBodies)
{
    _workers[_indexer]._world_obj._step_Seven_data._rigidbody.Add(rigid);
    ii++;
}


_workers[_indexer]._world_obj._AddedObjectsBruteForceIsUsed = _collisionSAP[_indexer].AddedObjectsBruteForceIsUsed;
_workers[_indexer]._world_obj._fullOverlaps = _collisionSAP[_indexer].fullOverlaps;
_workers[_indexer]._world_obj._activeList = _collisionSAP[_indexer].activeList;
_workers[_indexer]._world_obj._axis1 = _collisionSAP[_indexer].axis1;
_workers[_indexer]._world_obj._axis2 = _collisionSAP[_indexer].axis2;
_workers[_indexer]._world_obj._axis3 = _collisionSAP[_indexer].axis3;
_workers[_indexer]._world_obj._addCounter = _collisionSAP[_indexer].addCounter;
_workers[_indexer]._world_obj._sortCallback = _collisionSAP[_indexer].sortCallback;

//_world_objects[_indexer] = _workers[_indexer]._world_obj;
//_workers[_indexer]._world_obj._time_step = _array_stop_watch_tick[_indexer];

_workers[_indexer]._world_obj = _workers[_indexer]._world._Step_One(_workers[_indexer]._world_obj);
//_workers[_indexer]._world_obj = _script_physics._world_array[_indexer].Stepper(10, true, _workers[_indexer]._world_obj._time_step, 500, _accumulated_time[_indexer], _workers[_indexer]._world_obj);

for (int i = 0; i < _workers[_indexer]._world_obj._detect_one_data._someAction_axis_1.Count; i++)
{
    _workers[_indexer]._world.threadManager.AddTask(_workers[_indexer]._world_obj._detect_one_data._someAction_axis_1[i], _workers[_indexer]._world_obj._detect_one_data._param_axis_1[i]);
    _workers[_indexer]._world.threadManager.AddTask(_workers[_indexer]._world_obj._detect_one_data._someAction_axis_2[i], _workers[_indexer]._world_obj._detect_one_data._param_axis_2[i]);
    _workers[_indexer]._world.threadManager.AddTask(_workers[_indexer]._world_obj._detect_one_data._someAction_axis_3[i], _workers[_indexer]._world_obj._detect_one_data._param_axis_3[i]);

    _workers[_indexer]._world.threadManager.Execute();
}

_collisionSAP[_indexer].AddedObjectsBruteForceIsUsed = _workers[_indexer]._world_obj._AddedObjectsBruteForceIsUsed;
_collisionSAP[_indexer].fullOverlaps = _workers[_indexer]._world_obj._fullOverlaps;
_collisionSAP[_indexer].activeList = _workers[_indexer]._world_obj._activeList;
_collisionSAP[_indexer].axis1 = _workers[_indexer]._world_obj._axis1;
_collisionSAP[_indexer].axis2 = _workers[_indexer]._world_obj._axis2;
_collisionSAP[_indexer].axis3 = _workers[_indexer]._world_obj._axis3;
_collisionSAP[_indexer].addCounter = _workers[_indexer]._world_obj._addCounter;
_collisionSAP[_indexer].sortCallback = _workers[_indexer]._world_obj._sortCallback;

try
{
    //_workers[_indexer]._world_obj._detect_two_data._fullOverlaps = _collisionSAP[_indexer].fullOverlaps;
    _workers[_indexer]._world_obj._detect_two_data = _workers[_indexer]._world._Step_Two(_workers[_indexer]._world_obj._detect_two_data);

    for (int i = 0; i < _workers[_indexer]._world_obj._detect_two_data._someAction_det_cb_1.Count; i++)
    {
        _workers[_indexer]._world.threadManager.AddTask(_workers[_indexer]._world_obj._detect_two_data._someAction_det_cb_1[i], _workers[_indexer]._world_obj._detect_two_data._param_det_cb_1[i]);
    }

    _workers[_indexer]._world.threadManager.Execute();

    _collisionSAP[_indexer].addCounter = _workers[_indexer]._world_obj._detect_two_data._addCounter;
    _collisionSAP[_indexer].fullOverlaps = _workers[_indexer]._world_obj._detect_two_data._fullOverlaps;                                                
}
catch (Exception ex)
{
    //MessageBox((IntPtr)0, "Task: " + _indexer + " __ " + ex.ToString(), "Oculus error", 0);
}

try
{
    _workers[_indexer]._world_obj._step_three_data.islands = _world_array[_indexer].Islands;
    _workers[_indexer]._world_obj._step_three_data.island_manager = _world_array[_indexer].island_manager;
    _workers[_indexer]._world_obj._step_three_data.addedArbiterQueue = _world_array[_indexer].addedArbiterQueue;
}
catch (Exception ex)
{
    MessageBox((IntPtr)0, "Step3-1: " + _indexer + " __ " + ex.ToString(), "Oculus error", 0);
    throw new NotImplementedException();
}

try
{
    _workers[_indexer]._world_obj._step_three_data = _workers[_indexer]._world._Step_Three(_workers[_indexer]._world_obj._step_three_data);
}
catch (Exception ex)
{
    MessageBox((IntPtr)0, "Step3-2: " + _indexer + " __ " + ex.ToString(), "Oculus error", 0);
    throw new NotImplementedException();
}

try
{
    _world_array[_indexer].island_manager = _workers[_indexer]._world_obj._step_three_data.island_manager;
    _world_array[_indexer].addedArbiterQueue = _workers[_indexer]._world_obj._step_three_data.addedArbiterQueue;
    _world_array[_indexer].Islands = _workers[_indexer]._world_obj._step_three_data.islands;
}
catch (Exception ex)
{
    MessageBox((IntPtr)0, "Step3-3: " + _indexer + " __ " + ex.ToString(), "Oculus error", 0);
    throw new NotImplementedException();
}

try
{
    _workers[_indexer]._world_obj = _workers[_indexer]._world._Step_Four(_workers[_indexer]._world_obj);
}
catch (Exception ex)
{
    MessageBox((IntPtr)0, "Step4: " + _indexer + " __ " + ex.ToString(), "Oculus error", 0);
    throw new NotImplementedException();
}

try
{
    //if (_world_array[_indexer].arbiterCallback == null)
    //{
    //    MessageBox((IntPtr)0, "Step5-0: " + "null", "Oculus error", 0);
    //}
    //else
    //{
    //    MessageBox((IntPtr)0, "Step5-1: " + "!null", "Oculus error", 0);
    //}

    _workers[_indexer]._world_obj._step_five_data.contactIterations = _world_array[_indexer].contactIterations;
    _workers[_indexer]._world_obj._step_five_data.island_manager = _world_array[_indexer].island_manager;
    _workers[_indexer]._world_obj._step_five_data.islands = _world_array[_indexer].Islands;
    _workers[_indexer]._world_obj._arbiterCallback = _world_array[_indexer].arbiterCallback;

    _workers[_indexer]._world_obj = _workers[_indexer]._world._Step_Five(_workers[_indexer]._world_obj);        

    for (int i = 0; i < _workers[_indexer]._world_obj._step_five_data._someAction_det_cb_1.Count; i++)
    {
        _workers[_indexer]._world.threadManager.AddTask(_workers[_indexer]._world_obj._step_five_data._someAction_det_cb_1[i], _workers[_indexer]._world_obj._step_five_data._param_det_cb_1[i]);
    }

    _workers[_indexer]._world.threadManager.Execute();

    _world_array[_indexer].contactIterations = _workers[_indexer]._world_obj._step_five_data.contactIterations;
    _world_array[_indexer].island_manager = _workers[_indexer]._world_obj._step_five_data.island_manager;
    _world_array[_indexer].Islands = _workers[_indexer]._world_obj._step_five_data.islands;
    _world_array[_indexer].arbiterCallback = _workers[_indexer]._world_obj._arbiterCallback;
}
catch (Exception ex)
{
    MessageBox((IntPtr)0, "Step5: " + _indexer + " __ " + ex.ToString(), "Oculus error", 0);
    throw new NotImplementedException();
}






try
{
    _workers[_indexer]._world_obj._integrateCallback = _world_array[_indexer].integrateCallback;

    _workers[_indexer]._world_obj = _workers[_indexer]._world._Step_Six(_workers[_indexer]._world_obj);

    for (int i = 0; i < _workers[_indexer]._world_obj._step_Six_data._someAction_det_cb_1.Count; i++)
    {
        _workers[_indexer]._world.threadManager.AddTask(_workers[_indexer]._world_obj._step_Six_data._someAction_det_cb_1[i], _workers[_indexer]._world_obj._step_Six_data._param_det_cb_1[i]);
    }

    _workers[_indexer]._world.threadManager.Execute();

    _world_array[_indexer].integrateCallback = _workers[_indexer]._world_obj._integrateCallback;

}
catch (Exception ex)
{
    MessageBox((IntPtr)0, "Step6: " + _indexer + " __ " + ex.ToString(), "Oculus error", 0);
    throw new NotImplementedException();
}





//try
//{
//    _workers[_indexer]._world_obj._step_Seven_data._world_events = _world_array[_indexer].Events;
//}
//catch (Exception ex)
//{
//    MessageBox((IntPtr)0, "Step7: " + _indexer + " __ " + ex.ToString(), "Oculus error", 0);
//}
//
//_workers[_indexer]._world_obj = _workers[_indexer]._world._Step_Seven(_workers[_indexer]._world_obj);
//
//_world_array[_indexer].Events = _workers[_indexer]._world_obj._step_Seven_data._world_events;



try
{


    //int ii = 0;
    //foreach (RigidBody rigid in _world_array[_indexer].RigidBodies)
    //{
    //    _workers[_indexer]._world_obj._step_Seven_data._rigidbody.Add(rigid);
    //    ii++;
    //}
    _workers[_indexer]._world_obj._step_Seven_data._world_events = _world_array[_indexer].Events;
    _workers[_indexer]._world_obj = _workers[_indexer]._world._Step_Seven(_workers[_indexer]._world_obj);

    ii = 0;
    foreach (RigidBody rigid in _world_array[_indexer].RigidBodies)
    {

        if (!rigid.IsStatic)
        {
            //rigid.affectedByGravity = _workers[_indexer]._world_obj._step_Seven_data._rigidbody[ii].affectedByGravity;
            rigid.AffectedByGravity = _workers[_indexer]._world_obj._step_Seven_data._rigidbody[ii].AffectedByGravity;

            rigid.AllowDeactivation = _workers[_indexer]._world_obj._step_Seven_data._rigidbody[ii].AllowDeactivation;

            //rigid._angularVelocity = _workers[_indexer]._world_obj._step_Seven_data._rigidbody[ii]._angularVelocity;
            rigid.AngularVelocity = _workers[_indexer]._world_obj._step_Seven_data._rigidbody[ii].AngularVelocity;

            //rigid.arbiters = _workers[_indexer]._world_obj._step_Seven_data._rigidbody[ii].arbiters;

            rigid.BroadphaseTag = _workers[_indexer]._world_obj._step_Seven_data._rigidbody[ii].BroadphaseTag;

            //rigid._boundingBox = _workers[_indexer]._world_obj._step_Seven_data._rigidbody[ii]._boundingBox;
            rigid.BoundingBox = _workers[_indexer]._world_obj._step_Seven_data._rigidbody[ii].BoundingBox;

            rigid.CollisionIsland = _workers[_indexer]._world_obj._step_Seven_data._rigidbody[ii].CollisionIsland;

            //rigid.connections = _workers[_indexer]._world_obj._step_Seven_data._rigidbody[ii].connections;

            //rigid.constraints = _workers[_indexer]._world_obj._step_Seven_data._rigidbody[ii].constraints;
            rigid.Constraints = _workers[_indexer]._world_obj._step_Seven_data._rigidbody[ii].Constraints;

            rigid.Damping = _workers[_indexer]._world_obj._step_Seven_data._rigidbody[ii].Damping;

            rigid.EnableDebugDraw = _workers[_indexer]._world_obj._step_Seven_data._rigidbody[ii].EnableDebugDraw;

            rigid.EnableSpeculativeContacts = _workers[_indexer]._world_obj._step_Seven_data._rigidbody[ii].EnableSpeculativeContacts;

            //rigid._force = _workers[_indexer]._world_obj._step_Seven_data._rigidbody[ii]._force;
            rigid.Force = _workers[_indexer]._world_obj._step_Seven_data._rigidbody[ii].Force;



            //rigid.inactiveTime = _workers[_indexer]._world_obj._step_Seven_data._rigidbody[ii].inactiveTime;

            //rigid._inertia = _workers[_indexer]._world_obj._step_Seven_data._rigidbody[ii]._inertia;
            rigid.Inertia = _workers[_indexer]._world_obj._step_Seven_data._rigidbody[ii].Inertia;


            //rigid.internalIndex = _workers[_indexer]._world_obj._step_Seven_data._rigidbody[ii].internalIndex;

            rigid.InverseInertia = _workers[_indexer]._world_obj._step_Seven_data._rigidbody[ii].InverseInertia;

            rigid.InverseInertiaWorld = _workers[_indexer]._world_obj._step_Seven_data._rigidbody[ii].InverseInertiaWorld;

            //rigid._inverseMass = _workers[_indexer]._world_obj._step_Seven_data._rigidbody[ii]._inverseMass;


            //rigid._invInertia = _workers[_indexer]._world_obj._step_Seven_data._rigidbody[ii]._invInertia;

            //rigid._invInertiaWorld = _workers[_indexer]._world_obj._step_Seven_data._rigidbody[ii]._invInertiaWorld;

            //rigid._invOrientation = _workers[_indexer]._world_obj._step_Seven_data._rigidbody[ii]._invOrientation;

            //rigid.isActive = _workers[_indexer]._world_obj._step_Seven_data._rigidbody[ii].isActive;
            rigid.IsActive = _workers[_indexer]._world_obj._step_Seven_data._rigidbody[ii].IsActive;

            //rigid._island = _workers[_indexer]._world_obj._step_Seven_data._rigidbody[ii]._island;

            //rigid.isParticle = _workers[_indexer]._world_obj._step_Seven_data._rigidbody[ii].isParticle;
            rigid.IsParticle = _workers[_indexer]._world_obj._step_Seven_data._rigidbody[ii].IsParticle;


            //rigid.isStatic = _workers[_indexer]._world_obj._step_Seven_data._rigidbody[ii].isStatic;
            rigid.IsStatic = _workers[_indexer]._world_obj._step_Seven_data._rigidbody[ii].IsStatic;


            rigid.IsStaticOrInactive = _workers[_indexer]._world_obj._step_Seven_data._rigidbody[ii].IsStaticOrInactive;

            rigid.LinearVelocity = _workers[_indexer]._world_obj._step_Seven_data._rigidbody[ii].LinearVelocity;

            //rigid.marker = _workers[_indexer]._world_obj._step_Seven_data._rigidbody[ii].marker;

            rigid.Mass = _workers[_indexer]._world_obj._step_Seven_data._rigidbody[ii].Mass;

            //rigid._material = _workers[_indexer]._world_obj._step_Seven_data._rigidbody[ii]._material;
            rigid.Material = _workers[_indexer]._world_obj._step_Seven_data._rigidbody[ii].Material;

            //rigid._orientation = _workers[_indexer]._world_obj._step_Seven_data._rigidbody[ii]._orientation;
            rigid.Orientation = _workers[_indexer]._world_obj._step_Seven_data._rigidbody[ii].Orientation;

            //rigid._position = _workers[_indexer]._world_obj._step_Seven_data._rigidbody[ii]._position;
            rigid.Position = _workers[_indexer]._world_obj._step_Seven_data._rigidbody[ii].Position;

            rigid.Shape = _workers[_indexer]._world_obj._step_Seven_data._rigidbody[ii].Shape;

            //rigid._sweptDirection = _workers[_indexer]._world_obj._step_Seven_data._rigidbody[ii]._sweptDirection;

            rigid.Tag = _workers[_indexer]._world_obj._step_Seven_data._rigidbody[ii].Tag;


            //rigid._torque = _workers[_indexer]._world_obj._step_Seven_data._rigidbody[ii]._torque;
            rigid.Torque = _workers[_indexer]._world_obj._step_Seven_data._rigidbody[ii].Torque;
        }
        ii++;
    }

    //_world_array[_indexer].RigidBodies = _workers[_indexer]._world_obj._step_Seven_data._rigidbody;
    _world_array[_indexer].Events = _workers[_indexer]._world_obj._step_Seven_data._world_events;
}
catch (Exception ex)
{
    MessageBox((IntPtr)0, "Step7: " + _indexer + " __ " + ex.ToString(), "Oculus error", 0);
    throw new NotImplementedException();
}*/










/*_workers[_index]._worker_task = Task.Factory.StartNew((obj) =>
{
    //_workers[_index]._world_obj = _world_objects[_index];
    //MessageBox((IntPtr)0, "Task: " + _index, "Oculus error", 0);
    var _indexer = _index;

    DoWork(0, _indexer);

_thread_looper_sec:

    _workers[_indexer]._world_obj._AddedObjectsBruteForceIsUsed = _collisionSAP[_indexer].AddedObjectsBruteForceIsUsed;
    _workers[_indexer]._world_obj._fullOverlaps = _collisionSAP[_indexer].fullOverlaps;
    _workers[_indexer]._world_obj._activeList = _collisionSAP[_indexer].activeList;
    _workers[_indexer]._world_obj._axis1 = _collisionSAP[_indexer].axis1;
    _workers[_indexer]._world_obj._axis2 = _collisionSAP[_indexer].axis2;
    _workers[_indexer]._world_obj._axis3 = _collisionSAP[_indexer].axis3;
    _workers[_indexer]._world_obj._addCounter = _collisionSAP[_indexer].addCounter;
    _workers[_indexer]._world_obj._sortCallback = _collisionSAP[_indexer].sortCallback;

    //_world_objects[_indexer] = _workers[_indexer]._world_obj;
    //_workers[_indexer]._world_obj._time_step = _array_stop_watch_tick[_indexer];

    _workers[_indexer]._world_obj = _workers[_indexer]._world.Step(_workers[_indexer]._world_obj);
    //_workers[_indexer]._world_obj = _script_physics._world_array[_indexer].Stepper(10, true, _workers[_indexer]._world_obj._time_step, 500, _accumulated_time[_indexer], _workers[_indexer]._world_obj);

    for (int i = 0; i < _workers[_indexer]._world_obj._detect_one_data._someAction_axis_1.Count; i++)
    {
        _workers[_indexer]._world.threadManager.AddTask(_workers[_indexer]._world_obj._detect_one_data._someAction_axis_1[i], _workers[_indexer]._world_obj._detect_one_data._param_axis_1[i]);
        _workers[_indexer]._world.threadManager.AddTask(_workers[_indexer]._world_obj._detect_one_data._someAction_axis_2[i], _workers[_indexer]._world_obj._detect_one_data._param_axis_2[i]);
        _workers[_indexer]._world.threadManager.AddTask(_workers[_indexer]._world_obj._detect_one_data._someAction_axis_3[i], _workers[_indexer]._world_obj._detect_one_data._param_axis_3[i]);

        _workers[_indexer]._world.threadManager.Execute();
    }

    _collisionSAP[_indexer].AddedObjectsBruteForceIsUsed = _workers[_indexer]._world_obj._AddedObjectsBruteForceIsUsed;
    _collisionSAP[_indexer].fullOverlaps = _workers[_indexer]._world_obj._fullOverlaps;
    _collisionSAP[_indexer].activeList = _workers[_indexer]._world_obj._activeList;
    _collisionSAP[_indexer].axis1 = _workers[_indexer]._world_obj._axis1;
    _collisionSAP[_indexer].axis2 = _workers[_indexer]._world_obj._axis2;
    _collisionSAP[_indexer].axis3 = _workers[_indexer]._world_obj._axis3;
    _collisionSAP[_indexer].addCounter = _workers[_indexer]._world_obj._addCounter;
    _collisionSAP[_indexer].sortCallback = _workers[_indexer]._world_obj._sortCallback;
    try
    {
        //_workers[_indexer]._world_obj._detect_two_data._fullOverlaps = _collisionSAP[_indexer].fullOverlaps;
        _workers[_indexer]._world_obj._detect_two_data = _workers[_indexer]._world._Step_Two(_workers[_indexer]._world_obj._detect_two_data);

        for (int i = 0; i < _workers[_indexer]._world_obj._detect_two_data._someAction_det_cb_1.Count; i++)
        {
            _workers[_indexer]._world.threadManager.AddTask(_workers[_indexer]._world_obj._detect_two_data._someAction_det_cb_1[i], _workers[_indexer]._world_obj._detect_two_data._param_det_cb_1[i]);
        }

        _workers[_indexer]._world.threadManager.Execute();
        _collisionSAP[_indexer].addCounter = _workers[_indexer]._world_obj._detect_two_data._addCounter;
        //_collisionSAP[_indexer].fullOverlaps = _workers[_indexer]._world_obj._detect_two_data._fullOverlaps;                                                
    }
    catch (Exception ex)
    {
        //MessageBox((IntPtr)0, "Task: " + _indexer + " __ " + ex.ToString(), "Oculus error", 0);
    }


    try
    {
        _workers[_indexer]._world_obj._step_three_data.islands = _world_array[_indexer].Islands;
        _workers[_indexer]._world_obj._step_three_data.island_manager = _world_array[_indexer].island_manager;
        _workers[_indexer]._world_obj._step_three_data.addedArbiterQueue = _world_array[_indexer].addedArbiterQueue;
    }
    catch (Exception ex)
    {
        MessageBox((IntPtr)0, "Step3-1: " + _indexer + " __ " + ex.ToString(), "Oculus error", 0);
        throw new NotImplementedException();
    }



    try
    {
        _workers[_indexer]._world_obj._step_three_data = _workers[_indexer]._world._Step_Three(_workers[_indexer]._world_obj._step_three_data);
    }
    catch (Exception ex)
    {
        MessageBox((IntPtr)0, "Step3-2: " + _indexer + " __ " + ex.ToString(), "Oculus error", 0);
        throw new NotImplementedException();
    }

    try
    {
        _world_array[_indexer].island_manager = _workers[_indexer]._world_obj._step_three_data.island_manager;
        _world_array[_indexer].addedArbiterQueue = _workers[_indexer]._world_obj._step_three_data.addedArbiterQueue;
        _world_array[_indexer].Islands = _workers[_indexer]._world_obj._step_three_data.islands;
    }
    catch (Exception ex)
    {
        MessageBox((IntPtr)0, "Step3-3: " + _indexer + " __ " + ex.ToString(), "Oculus error", 0);
        throw new NotImplementedException();
    }


    try
    {
        _workers[_indexer]._world_obj = _workers[_indexer]._world._Step_Four(_workers[_indexer]._world_obj);
    }
    catch (Exception ex)
    {
        MessageBox((IntPtr)0, "Step4: " + _indexer + " __ " + ex.ToString(), "Oculus error", 0);
        throw new NotImplementedException();
    }

    try
    {
        //if (_world_array[_indexer].arbiterCallback == null)
        //{
        //    MessageBox((IntPtr)0, "Step5-0: " + "null", "Oculus error", 0);
        //}
        //else
        //{
        //    MessageBox((IntPtr)0, "Step5-1: " + "!null", "Oculus error", 0);
        //}

        _workers[_indexer]._world_obj._step_five_data.contactIterations = _world_array[_indexer].contactIterations;
        _workers[_indexer]._world_obj._step_five_data.island_manager = _world_array[_indexer].island_manager;
        _workers[_indexer]._world_obj._step_five_data.islands = _world_array[_indexer].Islands;
        _workers[_indexer]._world_obj.arbiterCallback = _world_array[_indexer].arbiterCallback;

        _workers[_indexer]._world_obj = _workers[_indexer]._world._Step_Five(_workers[_indexer]._world_obj);

        for (int i = 0; i < _workers[_indexer]._world_obj._step_five_data._someAction_det_cb_1.Count; i++)
        {
            _workers[_indexer]._world.threadManager.AddTask(_workers[_indexer]._world_obj._step_five_data._someAction_det_cb_1[i], _workers[_indexer]._world_obj._step_five_data._param_det_cb_1[i]);
        }

        _workers[_indexer]._world.threadManager.Execute();


        _world_array[_indexer].contactIterations = _workers[_indexer]._world_obj._step_five_data.contactIterations;
        _world_array[_indexer].island_manager = _workers[_indexer]._world_obj._step_five_data.island_manager;
        _world_array[_indexer].Islands = _workers[_indexer]._world_obj._step_five_data.islands;
        _world_array[_indexer].arbiterCallback = _workers[_indexer]._world_obj.arbiterCallback;
    }
    catch (Exception ex)
    {
        MessageBox((IntPtr)0, "Step5: " + _indexer + " __ " + ex.ToString(), "Oculus error", 0);
        throw new NotImplementedException();
    }

    try
    {
        _workers[_indexer]._world_obj = _workers[_indexer]._world._Step_Six(_workers[_indexer]._world_obj);

    }
    catch (Exception ex)
    {
        MessageBox((IntPtr)0, "Step6: " + _indexer + " __ " + ex.ToString(), "Oculus error", 0);
        throw new NotImplementedException();
    }

    try
    {
        _workers[_indexer]._world_obj = _workers[_indexer]._world._Step_Seven(_workers[_indexer]._world_obj);

    }
    catch (Exception ex)
    {
        MessageBox((IntPtr)0, "Step6: " + _indexer + " __ " + ex.ToString(), "Oculus error", 0);
        throw new NotImplementedException();
    }

    Thread.Sleep(0);
    goto _thread_looper_sec;

}, _index++);*/

































/*_backg_worker[_index] = new BackgroundWorker();

_backg_worker[_index].DoWork += (object sender, DoWorkEventArgs args) =>
{

    object[] parametors = args.Argument as object[];
    var workor = (_task_worker)parametors[0];
    //var parameters[] = _array_stop_watch_tick[_index];
    int _indexer = workor._worker_task_id;

    //DoWork(0, _indexer);

    //while (true)
    {

    _thread_looper_sec:

        try
        {
            /*lock (_array_stop_watch_tick)
            {

            }
            _workers[_indexer]._world_obj._time_step = _array_stop_watch_tick[_indexer];
            //_workers[_indexer]._world_obj._time_step = _array_stop_watch_tick[_indexer];

            _workers[_indexer]._world_obj._AddedObjectsBruteForceIsUsed = _collisionSAP[_indexer].AddedObjectsBruteForceIsUsed;
            _workers[_indexer]._world_obj._fullOverlaps = _collisionSAP[_indexer].fullOverlaps;
            _workers[_indexer]._world_obj._activeList = _collisionSAP[_indexer].activeList;
            _workers[_indexer]._world_obj._axis1 = _collisionSAP[_indexer].axis1;
            _workers[_indexer]._world_obj._axis2 = _collisionSAP[_indexer].axis2;
            _workers[_indexer]._world_obj._axis3 = _collisionSAP[_indexer].axis3;
            _workers[_indexer]._world_obj._addCounter = _collisionSAP[_indexer].addCounter;
            _workers[_indexer]._world_obj._sortCallback = _collisionSAP[_indexer].sortCallback;

            //_world_objects[_indexer] = _workers[_indexer]._world_obj;
            //_workers[_indexer]._world_obj._time_step = _array_stop_watch_tick[_indexer];

            _workers[_indexer]._world_obj = _workers[_indexer]._world.Step(_workers[_indexer]._world_obj);
            //_workers[_indexer]._world_obj = _script_physics._world_array[_indexer].Stepper(10, true, _workers[_indexer]._world_obj._time_step, 500, _accumulated_time[_indexer], _workers[_indexer]._world_obj);

            for (int i = 0; i < _workers[_indexer]._world_obj._detect_one_data._someAction_axis_1.Count; i++)
            {
                _workers[_indexer]._world.threadManager.AddTask(_workers[_indexer]._world_obj._detect_one_data._someAction_axis_1[i], _workers[_indexer]._world_obj._detect_one_data._param_axis_1[i]);
                _workers[_indexer]._world.threadManager.AddTask(_workers[_indexer]._world_obj._detect_one_data._someAction_axis_2[i], _workers[_indexer]._world_obj._detect_one_data._param_axis_2[i]);
                _workers[_indexer]._world.threadManager.AddTask(_workers[_indexer]._world_obj._detect_one_data._someAction_axis_3[i], _workers[_indexer]._world_obj._detect_one_data._param_axis_3[i]);

                _workers[_indexer]._world.threadManager.Execute();
            }

            _collisionSAP[_indexer].AddedObjectsBruteForceIsUsed = _workers[_indexer]._world_obj._AddedObjectsBruteForceIsUsed;
            _collisionSAP[_indexer].fullOverlaps = _workers[_indexer]._world_obj._fullOverlaps;
            _collisionSAP[_indexer].activeList = _workers[_indexer]._world_obj._activeList;
            _collisionSAP[_indexer].axis1 = _workers[_indexer]._world_obj._axis1;
            _collisionSAP[_indexer].axis2 = _workers[_indexer]._world_obj._axis2;
            _collisionSAP[_indexer].axis3 = _workers[_indexer]._world_obj._axis3;
            _collisionSAP[_indexer].addCounter = _workers[_indexer]._world_obj._addCounter;
            _collisionSAP[_indexer].sortCallback = _workers[_indexer]._world_obj._sortCallback;



            //_workers[_indexer]._world_obj._detect_two_data._fullOverlaps = _collisionSAP[_indexer].fullOverlaps;
            _workers[_indexer]._world_obj._detect_two_data = _workers[_indexer]._world._Step_Two(_workers[_indexer]._world_obj._detect_two_data);

            for (int i = 0; i < _workers[_indexer]._world_obj._detect_two_data._someAction_det_cb_1.Count; i++)
            {
                _workers[_indexer]._world.threadManager.AddTask(_workers[_indexer]._world_obj._detect_two_data._someAction_det_cb_1[i], _workers[_indexer]._world_obj._detect_two_data._param_det_cb_1[i]);
            }

            _workers[_indexer]._world.threadManager.Execute();


            _collisionSAP[_indexer].addCounter = _workers[_indexer]._world_obj._detect_two_data._addCounter;
            //_collisionSAP[_indexer].fullOverlaps = _workers[_indexer]._world_obj._detect_two_data._fullOverlaps;                                                






            _workers[_indexer]._world_obj = _workers[_indexer]._world._Step_Three(_workers[_indexer]._world_obj);

        }
        catch (Exception ex)
        {
            MessageBox((IntPtr)0, "Task: " + _indexer + " __ " + ex.ToString(), "Oculus error", 0);
        }

        Thread.Sleep(1);
        goto _thread_looper_sec;
    }
    Thread.Sleep(0);
};

_backg_worker[_index].RunWorkerCompleted += delegate (object sender, RunWorkerCompletedEventArgs args)
{
    //_script_physics = (_sc_jitter_physics)args.Result;
    MessageBox((IntPtr)0, "Task: " + _index, "Oculus error", 0);
};

object[] parameters = new object[2];
parameters[0] = _workers[_index];
//parameters[1] = _array_stop_watch_tick[_index];
_backg_worker[_index].RunWorkerAsync(parameters);*/











































/*
int _indexer = _workers[_index]._worker_task_id;
_workers[_indexer]._worker_task = Task<Task>.Factory.StartNew((obj) =>
{
//_workers[_index]._world_obj = _world_objects[_index];
//MessageBox((IntPtr)0, "Task: " + _index, "Oculus error", 0);

_thread_looper_sec:



    _workers[_indexer]._world_obj._AddedObjectsBruteForceIsUsed = _collisionSAP[_indexer].AddedObjectsBruteForceIsUsed;
    _workers[_indexer]._world_obj._fullOverlaps = _collisionSAP[_indexer].fullOverlaps;
    _workers[_indexer]._world_obj._activeList = _collisionSAP[_indexer].activeList;
    _workers[_indexer]._world_obj._axis1 = _collisionSAP[_indexer].axis1;
    _workers[_indexer]._world_obj._axis2 = _collisionSAP[_indexer].axis2;
    _workers[_indexer]._world_obj._axis3 = _collisionSAP[_indexer].axis3;
    _workers[_indexer]._world_obj._addCounter = _collisionSAP[_indexer].addCounter;
    _workers[_indexer]._world_obj._sortCallback = _collisionSAP[_indexer].sortCallback;


    //_world_objects[_indexer] = _workers[_indexer]._world_obj;
    _workers[_indexer]._world_obj._time_step = _array_stop_watch_tick[_indexer];

    _workers[_indexer]._world_obj = _workers[_indexer]._world.Step(_workers[_indexer]._world_obj);


    for (int i = 0; i < _workers[_indexer]._world_obj._detect_one_data._someAction_axis_1.Count; i++)
    {
        _workers[_indexer]._world.threadManager.AddTask(_workers[_indexer]._world_obj._detect_one_data._someAction_axis_1[i], _workers[_indexer]._world_obj._detect_one_data._param_axis_1[i]);
        _workers[_indexer]._world.threadManager.AddTask(_workers[_indexer]._world_obj._detect_one_data._someAction_axis_2[i], _workers[_indexer]._world_obj._detect_one_data._param_axis_2[i]);
        _workers[_indexer]._world.threadManager.AddTask(_workers[_indexer]._world_obj._detect_one_data._someAction_axis_3[i], _workers[_indexer]._world_obj._detect_one_data._param_axis_3[i]);

        _workers[_indexer]._world.threadManager.Execute();
    }

    _collisionSAP[_indexer].AddedObjectsBruteForceIsUsed = _workers[_indexer]._world_obj._AddedObjectsBruteForceIsUsed;
    _collisionSAP[_indexer].fullOverlaps = _workers[_indexer]._world_obj._fullOverlaps;
    _collisionSAP[_indexer].activeList = _workers[_indexer]._world_obj._activeList;
    _collisionSAP[_indexer].axis1 = _workers[_indexer]._world_obj._axis1;
    _collisionSAP[_indexer].axis2 = _workers[_indexer]._world_obj._axis2;
    _collisionSAP[_indexer].axis3 = _workers[_indexer]._world_obj._axis3;
    _collisionSAP[_indexer].addCounter = _workers[_indexer]._world_obj._addCounter;
    _collisionSAP[_indexer].sortCallback = _workers[_indexer]._world_obj._sortCallback;

    _workers[_indexer]._world_obj = _workers[_indexer]._world._Step_Two(_workers[_indexer]._world_obj);
    _workers[_indexer]._world_obj = _workers[_indexer]._world_obj;

    _collisionSAP[_indexer].AddedObjectsBruteForceIsUsed = _workers[_indexer]._world_obj._AddedObjectsBruteForceIsUsed;
    _collisionSAP[_indexer].fullOverlaps = _workers[_indexer]._world_obj._fullOverlaps;
    _collisionSAP[_indexer].activeList = _workers[_indexer]._world_obj._activeList;
    _collisionSAP[_indexer].axis1 = _workers[_indexer]._world_obj._axis1;
    _collisionSAP[_indexer].axis2 = _workers[_indexer]._world_obj._axis2;
    _collisionSAP[_indexer].axis3 = _workers[_indexer]._world_obj._axis3;
    _collisionSAP[_indexer].addCounter = _workers[_indexer]._world_obj._addCounter;
    _collisionSAP[_indexer].sortCallback = _workers[_indexer]._world_obj._sortCallback;

    for (int i = 0; i < _workers[_indexer]._world_obj._detect_two_data._someAction_det_cb_1.Count; i++)
    {
        _workers[_indexer]._world.threadManager.AddTask(_workers[_indexer]._world_obj._detect_two_data._someAction_det_cb_1[i], _workers[_indexer]._world_obj._detect_two_data._param_det_cb_1[i]);
    }

    _workers[_indexer]._world.threadManager.Execute();


    _workers[_indexer]._world_obj = _workers[_indexer]._world._Step_Three(_workers[_indexer]._world_obj);
    //_workers[_indexer]._world_obj = _workers[_indexer]._world_obj;

    _collisionSAP[_indexer].AddedObjectsBruteForceIsUsed = _workers[_indexer]._world_obj._AddedObjectsBruteForceIsUsed;
    _collisionSAP[_indexer].fullOverlaps = _workers[_indexer]._world_obj._fullOverlaps;
    _collisionSAP[_indexer].activeList = _workers[_indexer]._world_obj._activeList;
    _collisionSAP[_indexer].axis1 = _workers[_indexer]._world_obj._axis1;
    _collisionSAP[_indexer].axis2 = _workers[_indexer]._world_obj._axis2;
    _collisionSAP[_indexer].axis3 = _workers[_indexer]._world_obj._axis3;
    _collisionSAP[_indexer].addCounter = _workers[_indexer]._world_obj._addCounter;
    _collisionSAP[_indexer].sortCallback = _workers[_indexer]._world_obj._sortCallback;


    Thread.Sleep(1);
    goto _thread_looper_sec;

}, _workers[_indexer]);*/


/*//_index = 0;
int _indexer = _workers[_index]._worker_task_id;
_workers[_indexer]._worker_task = Task.Factory.StartNew((Object obj) =>
{
//_workers[_index]._world_obj = _world_objects[_index];
//MessageBox((IntPtr)0, "Task: " + _index, "Oculus error", 0);

_thread_looper_sec:

    //_world_objects[_indexer] = _workers[_indexer]._world_obj;
    _workers[_indexer]._world_obj._time_step = _array_stop_watch_tick[_indexer];

    _workers[_indexer]._world_obj = _workers[_indexer]._world.Step(_workers[_indexer]._world_obj);


    for (int i = 0; i < _workers[_indexer]._world_obj._detect_one_data._someAction_axis_1.Count; i++)
    {
        _workers[_indexer]._world.threadManager.AddTask(_workers[_indexer]._world_obj._detect_one_data._someAction_axis_1[i], _workers[_indexer]._world_obj._detect_one_data._param_axis_1[i]);
        _workers[_indexer]._world.threadManager.AddTask(_workers[_indexer]._world_obj._detect_one_data._someAction_axis_2[i], _workers[_indexer]._world_obj._detect_one_data._param_axis_2[i]);
        _workers[_indexer]._world.threadManager.AddTask(_workers[_indexer]._world_obj._detect_one_data._someAction_axis_3[i], _workers[_indexer]._world_obj._detect_one_data._param_axis_3[i]);

        _workers[_indexer]._world.threadManager.Execute();
    }

    CollisionSystemPersistentSAP.AddedObjectsBruteForceIsUsed = _workers[_indexer]._world_obj._AddedObjectsBruteForceIsUsed;
    CollisionSystemPersistentSAP.fullOverlaps = _workers[_indexer]._world_obj._fullOverlaps;
    CollisionSystemPersistentSAP.activeList = _workers[_indexer]._world_obj._activeList;
    CollisionSystemPersistentSAP.axis1 = _workers[_indexer]._world_obj._axis1;
    CollisionSystemPersistentSAP.axis2 = _workers[_indexer]._world_obj._axis2;
    CollisionSystemPersistentSAP.axis3 = _workers[_indexer]._world_obj._axis3;
    CollisionSystemPersistentSAP.addCounter = _workers[_indexer]._world_obj._addCounter;
    CollisionSystemPersistentSAP.sortCallback = _workers[_indexer]._world_obj._sortCallback;

    _workers[_indexer]._world_obj = _workers[_indexer]._world._Step_Two(_workers[_indexer]._world_obj);
    _workers[_indexer]._world_obj = _workers[_indexer]._world_obj;

    CollisionSystemPersistentSAP.AddedObjectsBruteForceIsUsed = _workers[_indexer]._world_obj._AddedObjectsBruteForceIsUsed;
    CollisionSystemPersistentSAP.fullOverlaps = _workers[_indexer]._world_obj._fullOverlaps;
    CollisionSystemPersistentSAP.activeList = _workers[_indexer]._world_obj._activeList;
    CollisionSystemPersistentSAP.axis1 = _workers[_indexer]._world_obj._axis1;
    CollisionSystemPersistentSAP.axis2 = _workers[_indexer]._world_obj._axis2;
    CollisionSystemPersistentSAP.axis3 = _workers[_indexer]._world_obj._axis3;
    CollisionSystemPersistentSAP.addCounter = _workers[_indexer]._world_obj._addCounter;
    CollisionSystemPersistentSAP.sortCallback = _workers[_indexer]._world_obj._sortCallback;

    for (int i = 0; i < _workers[_indexer]._world_obj._detect_two_data._someAction_det_cb_1.Count; i++)
    {
        _workers[_indexer]._world.threadManager.AddTask(_workers[_indexer]._world_obj._detect_two_data._someAction_det_cb_1[i], _workers[_indexer]._world_obj._detect_two_data._param_det_cb_1[i]);
    }

    _workers[_indexer]._world.threadManager.Execute();


    _workers[_indexer]._world_obj = _workers[_indexer]._world._Step_Three(_workers[_indexer]._world_obj);
    //_workers[_indexer]._world_obj = _workers[_indexer]._world_obj;

    CollisionSystemPersistentSAP.AddedObjectsBruteForceIsUsed = _workers[_indexer]._world_obj._AddedObjectsBruteForceIsUsed;
    CollisionSystemPersistentSAP.fullOverlaps = _workers[_indexer]._world_obj._fullOverlaps;
    CollisionSystemPersistentSAP.activeList = _workers[_indexer]._world_obj._activeList;
    CollisionSystemPersistentSAP.axis1 = _workers[_indexer]._world_obj._axis1;
    CollisionSystemPersistentSAP.axis2 = _workers[_indexer]._world_obj._axis2;
    CollisionSystemPersistentSAP.axis3 = _workers[_indexer]._world_obj._axis3;
    CollisionSystemPersistentSAP.addCounter = _workers[_indexer]._world_obj._addCounter;
    CollisionSystemPersistentSAP.sortCallback = _workers[_indexer]._world_obj._sortCallback;


    Thread.Sleep(1);
    goto _thread_looper_sec;

}, _indexer++);*/

/*_backg_worker[_index] = new BackgroundWorker();

_backg_worker[_index].DoWork += (object sender, DoWorkEventArgs args) =>
{
    object[] parametors = args.Argument as object[];
    var workor = (_task_worker)parametors[0];
    int _indexer = workor._worker_task_id;

_thread_looper_sec:


    _workers[_indexer]._world_obj._time_step = _array_stop_watch_tick[_indexer];

    _workers[_indexer]._world_obj = _workers[_indexer]._world.Step(_workers[_indexer]._world_obj);


    for (int i = 0; i < _workers[_indexer]._world_obj._detect_one_data._someAction_axis_1.Count; i++)
    {
        _workers[_indexer]._world.threadManager.AddTask(_workers[_indexer]._world_obj._detect_one_data._someAction_axis_1[i], _workers[_indexer]._world_obj._detect_one_data._param_axis_1[i]);
        _workers[_indexer]._world.threadManager.AddTask(_workers[_indexer]._world_obj._detect_one_data._someAction_axis_2[i], _workers[_indexer]._world_obj._detect_one_data._param_axis_2[i]);
        _workers[_indexer]._world.threadManager.AddTask(_workers[_indexer]._world_obj._detect_one_data._someAction_axis_3[i], _workers[_indexer]._world_obj._detect_one_data._param_axis_3[i]);

        _workers[_indexer]._world.threadManager.Execute();
    }

    CollisionSystemPersistentSAP.AddedObjectsBruteForceIsUsed = _workers[_indexer]._world_obj._AddedObjectsBruteForceIsUsed;
    CollisionSystemPersistentSAP.fullOverlaps = _workers[_indexer]._world_obj._fullOverlaps;
    CollisionSystemPersistentSAP.activeList = _workers[_indexer]._world_obj._activeList;
    CollisionSystemPersistentSAP.axis1 = _workers[_indexer]._world_obj._axis1;
    CollisionSystemPersistentSAP.axis2 = _workers[_indexer]._world_obj._axis2;
    CollisionSystemPersistentSAP.axis3 = _workers[_indexer]._world_obj._axis3;
    CollisionSystemPersistentSAP.addCounter = _workers[_indexer]._world_obj._addCounter;
    CollisionSystemPersistentSAP.sortCallback = _workers[_indexer]._world_obj._sortCallback;

    _workers[_indexer]._world_obj = _workers[_indexer]._world._Step_Two(_workers[_indexer]._world_obj);
    _workers[_indexer]._world_obj = _workers[_indexer]._world_obj;

    CollisionSystemPersistentSAP.AddedObjectsBruteForceIsUsed = _workers[_indexer]._world_obj._AddedObjectsBruteForceIsUsed;
    CollisionSystemPersistentSAP.fullOverlaps = _workers[_indexer]._world_obj._fullOverlaps;
    CollisionSystemPersistentSAP.activeList = _workers[_indexer]._world_obj._activeList;
    CollisionSystemPersistentSAP.axis1 = _workers[_indexer]._world_obj._axis1;
    CollisionSystemPersistentSAP.axis2 = _workers[_indexer]._world_obj._axis2;
    CollisionSystemPersistentSAP.axis3 = _workers[_indexer]._world_obj._axis3;
    CollisionSystemPersistentSAP.addCounter = _workers[_indexer]._world_obj._addCounter;
    CollisionSystemPersistentSAP.sortCallback = _workers[_indexer]._world_obj._sortCallback;

    for (int i = 0; i < _workers[_indexer]._world_obj._detect_two_data._someAction_det_cb_1.Count; i++)
    {
        _workers[_indexer]._world.threadManager.AddTask(_workers[_indexer]._world_obj._detect_two_data._someAction_det_cb_1[i], _workers[_indexer]._world_obj._detect_two_data._param_det_cb_1[i]);
    }

    _workers[_indexer]._world.threadManager.Execute();


    _workers[_indexer]._world_obj = _workers[_indexer]._world._Step_Three(_workers[_indexer]._world_obj);
    //_workers[_indexer]._world_obj = _workers[_indexer]._world_obj;

    CollisionSystemPersistentSAP.AddedObjectsBruteForceIsUsed = _workers[_indexer]._world_obj._AddedObjectsBruteForceIsUsed;
    CollisionSystemPersistentSAP.fullOverlaps = _workers[_indexer]._world_obj._fullOverlaps;
    CollisionSystemPersistentSAP.activeList = _workers[_indexer]._world_obj._activeList;
    CollisionSystemPersistentSAP.axis1 = _workers[_indexer]._world_obj._axis1;
    CollisionSystemPersistentSAP.axis2 = _workers[_indexer]._world_obj._axis2;
    CollisionSystemPersistentSAP.axis3 = _workers[_indexer]._world_obj._axis3;
    CollisionSystemPersistentSAP.addCounter = _workers[_indexer]._world_obj._addCounter;
    CollisionSystemPersistentSAP.sortCallback = _workers[_indexer]._world_obj._sortCallback;



    Thread.Sleep(1);
    goto _thread_looper_sec;
};

_backg_worker[_index].RunWorkerCompleted += delegate (object sender, RunWorkerCompletedEventArgs args)
{
    //_script_physics = (_sc_jitter_physics)args.Result;
};


object[] parameters = new object[1];
parameters[0] = _workers[_index];
_backg_worker[_index].RunWorkerAsync(parameters);
}
}
}
_swtch = 2;
}*/



/*if (_swtch == 1)
{
    int _indexor = 0;
    for (int x = 0; x < _script_physics._width; x++)
    {
        for (int y = 0; y < _script_physics._height; y++)
        {
            for (int z = 0; z < _script_physics._depth; z++)
            {
                int _index = x + _script_physics._width * (y + _script_physics._height * z);


                if (_index < (_script_physics._width * _script_physics._height * _script_physics._depth))
                {
                    _workers[_index]._world.Step(_workers[_index]._world_obj._time_step, true);// _workers[_index]._world_obj);
                }
            }
        }
    }
}*/






















/*for (int x = 0; x < _script_physics._width; x++)
{
    for (int y = 0; y < _script_physics._height; y++)
    {
        for (int z = 0; z < _script_physics._depth; z++)
        {
            int _indexer = x + _script_physics._width * (y + _script_physics._height * z);
            _workers[_indexer]._world_obj = _workers[_indexer]._world._Step_Three(_workers[_indexer]._world_obj);
        }
    }
}*/


/*taskArray[_index] = Task<_sc_jitter_physics>.Factory.StartNew((Object obj) =>
{

//MessageBox((IntPtr)0, "Task: " + _index, "Oculus error", 0);

_thread_looper_sec:
    _script_physics._world_array[_index].Step(_script_physics._array_stop_watch_tick[_index], true);
    Thread.Sleep(1);
    goto _thread_looper_sec;


}, _index);*/


/*taskArray[_index] = Task.Factory.StartNew((Object obj) =>
{
//MessageBox((IntPtr)0, "Task: " + _index, "Oculus error", 0);

_thread_looper_sec:
    _script_physics._world_array[_index].Step(_script_physics._array_stop_watch_tick[_index], true);
    Thread.Sleep(1);
    goto _thread_looper_sec;


}, _index++);*/

/*taskArray[_index] = Task<_sc_jitter_physics>.Factory.StartNew((tester000) => //_tasks_array[_work_index]
{
_thread_looper_sec:

    //MessageBox((IntPtr)0, "Task: " + _index, "Oculus error", 0);
    _script_physics._world_array[_index].Step(_script_physics._array_stop_watch_tick[_index], true);

    Thread.Sleep(1);
    goto _thread_looper_sec;

}, _script_physics);*/




















/*int c = 0;

_index_looper = 0;
_backg_worker[_index_looper] = new BackgroundWorker();

_backg_worker[_index_looper].DoWork += (object sender, DoWorkEventArgs args) =>
{
_thread_looper_sec:
    _script_physics._world_array[_index_looper].Step(_script_physics._array_stop_watch_tick[_index_looper], true);
    args.Result = _script_physics;
    Thread.Sleep(1);
    goto _thread_looper_sec;
};

_backg_worker[_index_looper].RunWorkerCompleted += delegate (object sender, RunWorkerCompletedEventArgs args)
{
    _script_physics = (_sc_jitter_physics)args.Result;
};




_index_looper = 1;
_backg_worker[_index_looper] = new BackgroundWorker();

_backg_worker[_index_looper].DoWork += (object sender, DoWorkEventArgs args) =>
{
_thread_looper_sec:
    _script_physics._world_array[_index_looper].Step(_script_physics._array_stop_watch_tick[_index_looper], true);
    args.Result = _script_physics;
    Thread.Sleep(1);
    goto _thread_looper_sec;
};

_backg_worker[_index_looper].RunWorkerCompleted += delegate (object sender, RunWorkerCompletedEventArgs args)
{
    _script_physics = (_sc_jitter_physics)args.Result;
};*/





/*for (int i = 0; i < _backg_worker.Length; i++)
{
    _index_looper = 0;
    _backg_worker[_index_looper] = new BackgroundWorker();

    _backg_worker[_index_looper].DoWork += (object sender, DoWorkEventArgs args) =>
    {
    _thread_looper_sec:
        _script_physics._world_array[_index_looper].Step(_script_physics._array_stop_watch_tick[_index_looper], true);
        args.Result = _script_physics;
        Thread.Sleep(1);
        goto _thread_looper_sec;
    };

    _backg_worker[_index_looper].RunWorkerCompleted += delegate (object sender, RunWorkerCompletedEventArgs args)
    {
        _script_physics = (_sc_jitter_physics)args.Result;
    };
    c++;
}*/

/*for (int i = 0; i < _backg_worker.Length; i++)
{
    _backg_worker[i].RunWorkerAsync();
}*/


/*Task<int>[] taskArray = 
{
    Task<int>.Factory.StartNew(() => _script_physics._world_array[0].Step(_script_physics._array_stop_watch_tick[0], true)),

    Task<int>.Factory.StartNew(() =>_script_physics._world_array[1].Step(_script_physics._array_stop_watch_tick[1], true)),

    Task<int>.Factory.StartNew(() => _script_physics._world_array[2].Step(_script_physics._array_stop_watch_tick[2], true))
};


Task.WaitAll(taskArray);*/


/*Task[] taskArray = new Task[_script_physics._width * _script_physics._height * _script_physics._depth];

for (int i = 0; i < taskArray.Length;)
{
    taskArray[i] = Task.Factory.StartNew((Object obj) =>
    {
        MessageBox((IntPtr)0, "col sys 02" + i, "Oculus error", 0);
    }, i++);
}


Task.WaitAll(taskArray);*/







//_swtch = 2;
/*int c = 0;
for (int i = 0; i < _array_of_tasks.Length; i++)
{
    int temp = c;
    _array_of_tasks[i] = new Task(() => 

    _script_physics._world_array[temp].Step(_script_physics._array_stop_watch_tick[temp], true)


    );
    c = c+1;
}*/

//for (int i = 0; i < _array_of_tasks.Length; i++)
//{
//    _array_of_tasks[i].Start();
//}


/*foreach (Task tasker in _array_of_tasks)
{
    tasker.Start();
}
*/

















/*if (_counter_task_creation >= 20)
{
    if (_swtch == 0)
    {
        if (indexer < (_script_physics._width * _script_physics._height * _script_physics._depth))
        {
            /*_array_of_tasks[indexer] = Task<_sc_jitter_physics>.Factory.StartNew((tester000) => //_tasks_array[_work_index]
            {
            _thread_looper_sec:

                _script_physics._world_array[indexer].Step(_script_physics._array_stop_watch_tick[indexer], true);

                Thread.Sleep(0);
                goto _thread_looper_sec;

            }, _script_physics);*/






/*_backg_worker[indexer] = new BackgroundWorker();

_backg_worker[indexer].DoWork += (object sender, DoWorkEventArgs args) =>
{
    args.Result = _script_physics;
};
_backg_worker[indexer].RunWorkerCompleted += delegate (object sender, RunWorkerCompletedEventArgs args)
{

_thread_looper_sec:

    _script_physics._world_array[indexer].Step(_script_physics._array_stop_watch_tick[indexer], true);
    //args.Result = _script_physics;
    Thread.Sleep(1);
    goto _thread_looper_sec;

};

_backg_worker[indexer].RunWorkerAsync();




indexer++;
//_swtch = 1;
_counter_task_creation = 0;
}
else
{
_swtch = 2;
}
}

}
else
{
_counter_task_creation++;
}*/


/*for (int x = 0; x < _script_physics._width; x++)
{
    for (int y = 0; y < _script_physics._height; y++)
    {
        for (int z = 0; z < _script_physics._depth; z++)
        {
            int _indexer = x + _script_physics._width * (y + _script_physics._height * z);

            _world_array[_indexer] = _workers[_indexer]._world;


            /*var _world_objects = _workers[_index]._world_obj;

            for (int i = 0; i < _world_objects._detect_one_data._someAction_axis_1.Count; i++)
            {
                _world_array[_index].threadManager.AddTask(_world_objects._detect_one_data._someAction_axis_1[i], _world_objects._detect_one_data._param_axis_1[i]);
                _world_array[_index].threadManager.AddTask(_world_objects._detect_one_data._someAction_axis_2[i], _world_objects._detect_one_data._param_axis_2[i]);
                _world_array[_index].threadManager.AddTask(_world_objects._detect_one_data._someAction_axis_3[i], _world_objects._detect_one_data._param_axis_3[i]);

                _world_array[_index].threadManager.Execute();
            }

            CollisionSystemPersistentSAP.AddedObjectsBruteForceIsUsed = _world_objects._AddedObjectsBruteForceIsUsed;
            CollisionSystemPersistentSAP.fullOverlaps = _world_objects._fullOverlaps;
            CollisionSystemPersistentSAP.activeList = _world_objects._activeList;
            CollisionSystemPersistentSAP.axis1 = _world_objects._axis1;
            CollisionSystemPersistentSAP.axis2 = _world_objects._axis2;
            CollisionSystemPersistentSAP.axis3 = _world_objects._axis3;
            CollisionSystemPersistentSAP.addCounter = _world_objects._addCounter;
            CollisionSystemPersistentSAP.sortCallback = _world_objects._sortCallback;


            _world_objects = _world_array[_index]._Step_Two(_world_objects);*/
/*for (int i = 0; i < _workers[_index]._world_obj._detect_one_data._someAction_axis_1.Count; i++)
{
    _world_array[_index].threadManager.AddTask(_workers[_index]._world_obj._detect_one_data._someAction_axis_1[i], _workers[_index]._world_obj._detect_one_data._param_axis_1[i]);
    _world_array[_index].threadManager.AddTask(_workers[_index]._world_obj._detect_one_data._someAction_axis_2[i], _workers[_index]._world_obj._detect_one_data._param_axis_2[i]);
    _world_array[_index].threadManager.AddTask(_workers[_index]._world_obj._detect_one_data._someAction_axis_3[i], _workers[_index]._world_obj._detect_one_data._param_axis_3[i]);

    _world_array[_index].threadManager.Execute();
}


CollisionSystemPersistentSAP.AddedObjectsBruteForceIsUsed = _workers[_index]._world_obj._AddedObjectsBruteForceIsUsed;
CollisionSystemPersistentSAP.fullOverlaps = _workers[_index]._world_obj._fullOverlaps;
CollisionSystemPersistentSAP.activeList = _workers[_index]._world_obj._activeList;
CollisionSystemPersistentSAP.axis1 = _workers[_index]._world_obj._axis1;
CollisionSystemPersistentSAP.axis2 = _workers[_index]._world_obj._axis2;
CollisionSystemPersistentSAP.axis3 = _workers[_index]._world_obj._axis3;
CollisionSystemPersistentSAP.addCounter = _workers[_index]._world_obj._addCounter;
CollisionSystemPersistentSAP.sortCallback = _workers[_index]._world_obj._sortCallback;


_workers[_index]._world_obj = _world_array[_index]._Step_Two(_workers[_index]._world_obj);
}
}
}*/
/*for (int x = 0; x < _script_physics._width; x++)
 {
     for (int y = 0; y < _script_physics._height; y++)
     {
         for (int z = 0; z < _script_physics._depth; z++)
         {
             int _indexer = x + _script_physics._width * (y + _script_physics._height * z);
             _workers[_indexer]._world_obj._time_step = _array_stop_watch_tick[_indexer];

         }
     }
 }*/




//_script_physics._accumulated_times = _accumulated_time;






/*for (int x = 0; x < this._width; x++)
{
    for (int y = 0; y < this._height; y++)
    {
        for (int z = 0; z < this._depth; z++)
        {
            int _index = x + this._width * (y + this._height * z);

            World _some_world = _world_array[_index];
            if (_start_physics_worker_00 == 0)
            {
                /*if (delta_time_before > (1 / 60))//1.0f * _some_max)
                {
                    delta_time_before = (1 / 60);// 1.0f * _some_max;
                }
                delta_time_before =( 1 / 60)*100; //*= 10;//



                var delta_time_beforer = _array_stop_watch_tick[_index];


                if (delta_time_beforer > 1.0f * _some_max)
                {
                    delta_time_beforer = 1.0f * _some_max;
                }

                //delta_time_before *= 100;


                _accumulated_time[_index] = _some_world.Stepper(10, true, delta_time_beforer, 500, _accumulated_time[_index]);


                //_some_world.Step(delta_time_beforer, true);
            }
        }
    }
}*/


//_frame_lag_checker.Stop();

//Console.SetCursorPosition(1, 16);
//Console.WriteLine("lag_jitter: " + _frame_lag_checker.Elapsed.Ticks);







/*BackgroundWorker threads = new BackgroundWorker();
threads.DoWork += (object sender, DoWorkEventArgs args) =>
{
_thread_looper:


    _frame_lag_checker.Stop();
    _frame_lag_checker.Reset();
    _frame_lag_checker.Start();
    for (int x = 0; x < this._width; x++)
    {
        for (int y = 0; y < this._height; y++)
        {
            for (int z = 0; z < this._depth; z++)
            {
                int _index = x + this._width * (y + this._height * z);

                World _some_world = _world_array[_index];
                if (_start_physics_worker_00 == 0)
                {
                    /*if (delta_time_before > 1.0f * _some_max)
                    {
                        delta_time_before = 1.0f * _some_max;
                    }

                    //_some_world.Stepper(1, true, delta_time_before, 1);
                    _some_world.Step(delta_time_before, true);

                    var delta_time_beforer = _array_stop_watch_tick[_index];


                    if (delta_time_beforer > 1.0f * _some_max)
                    {
                        delta_time_beforer = 1.0f * _some_max;
                    }

                    //delta_time_before *= 100;


                    _accumulated_time[_index] = _some_world.Stepper(10, true, delta_time_beforer, 500, _accumulated_time[_index]);


                    //_some_world.Step(delta_time_beforer, true);
                }
            }
        }
    }


    _frame_lag_checker.Stop();

    Console.SetCursorPosition(1, 16);
    Console.WriteLine("lag_jitter: " + _frame_lag_checker.Elapsed.Ticks);

    Thread.Sleep(0);
    goto _thread_looper;
};

threads.RunWorkerCompleted += delegate (object sender, RunWorkerCompletedEventArgs args)
{

};

threads.RunWorkerAsync();*/





/*if (delta_time_beforer > 1.0f * _some_max)
{
    delta_time_beforer = 1.0f * _some_max;
}

//delta_time_before *= 100;


_accumulated_time[_index] = _some_world.Stepper(10, true, delta_time_beforer, 500, _accumulated_time[_index]);


//_some_world.Step(delta_time_beforer, true);*/



/*var _task_prime = Task<_sc_jitter_physics>.Factory.StartNew((tester0) => //_tasks_array[_work_index]
{


//_script_physics
_thread_looper_prim:

    //_script_physics._world_array = 




    Thread.Sleep(0);
    goto _thread_looper_prim;
}, _script_physics);*/


/*if (_startTasker == 0)
{


    //int total = this._width * this._height * this._depth;

    var _task_sec = Task<_sc_jitter_physics>.Factory.StartNew((tester00) => //_tasks_array[_work_index]
    {

        //MessageBox((IntPtr)0, _index.ToString(), "Oculus error", 0);


        //World _some_world = _script_physics._world_array[_index];
        int _counter_task_creation = 0;
    _thread_looper_prim:




        _counter_task_creation++;
        Thread.Sleep(0);
        goto _thread_looper_prim;
    }, _script_physics);

    _startTasker = 1;
}*/


