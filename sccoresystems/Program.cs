using System;
using System.Threading;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Reflection;
using System.IO.Pipes;
using System.IO;
using System.Text;

//using sccoresystems.sc_core;
//using sccoresystems.sc_console;
//using _messager = sccoresystems.sc_console._messager;
using sccsmessageobject = sccoresystems.sccsmessageobject;
//using sccsmessageobjectjitter = sccoresystems.sccsmessageobjectjitter;

using sccoresystems.sccscore;

using Win32Exception = System.ComponentModel.Win32Exception;

using WindowsInput;

using sccoresystems.sccsconsole;


using Jitter;
using Jitter.LinearMath;
using Jitter.Dynamics;
using Jitter.Collision;
using Jitter.Collision.Shapes;
using Jitter.Forces;


using sccoresystems.sccsgraphics;

namespace sccoresystems
{

    internal static class Program
    {
        static SharpDX.Direct3D11.Device screencapturedevice;
        public const int screencaptureenabled = 1; //0 for disabled and 1 for enabled
        static int initscreencaptureclass = screencaptureenabled;
        public static sccsscreencapture somesccsscreencapturemain;
        public static sccssharpdxscreenframe somesccsscreencapturemaindata;

        public static int physicsenabled = 0; //0 for disabled and 1 for enabled
        static int pythontoprogramswtc = 0; //0 for enabled and -1 for disabled



        //static sccsgraphicsupdate graphicsupdate;

        public static scsystemconfiguration config;

        public static int useArduinoOVRTouchKeymapper = 0;
        public static string programname = "sccoresystems";
        //public static sccssharpdxdirectx D3D;



        static sccsmessageobject[] mainreceivedmessages;//
        static int MaxSizeMainObject = 16;
        public static int MaxSizeMessageObject = 32;
        static sccsconsole.sccsconsolereader.consolereaderdata consolereaderstring;
        static object consolereaderobject;
        static messager[] sccsconsolewritermsg = new messager[MaxSizeMessageObject];

        public static int initdirectXmainswtch = -1;
        public static int initvrmainswtch = 2;
        static int hasinitdirectx = 0; //set to 0 so that the program has a chance to get into directx after entering your username and password.

        public static InputSimulator inputsim;
        public static KeyboardSimulator keyboardsim;
        public static MouseSimulator mousesim;


        public static sccsconsolekeyboardinput keyboardinput;

        static int totalThreads = 1;
        static int processorCount = 1;
        static int componentsexists = 0;
        //public static sccoresystems.sccscore.sccsglobalsaccessor SCGLOBALSACCESSORS;

        static int initconsolecursorx = 0;
        static int initconsolecursory = 0;

        static sccsmessageobject data00IN;
        static sccsmessageobject data00OUT;

        static int consolereadercanWork = 1;
        static int startThread = 0;
        public static sccsglobalsaccessor sccsglobalsaccessor;

        static int lastMenu = -2;
        static string lastMenuOption = "";
        static string lastUsername = "";
        static int someotherswtch = 0;
        public static JVector worldgravity = new JVector(0, -9.81f, 0); //-9.81f base
        public static int worlditerations = 3; // as high as possible normally for higher precision
        public static int worldsmalliterations = 3; // as high as possible normally for higher precision
        public static float worldallowedpenetration = 0.01f; //0.00123f  worldgravity = new JVector(0, -9.81f, 0);
        public static bool allowdeactivation = true;

        public static int physicsengineinstancex = 1; //4
        public static int physicsengineinstancey = 1; //1
        public static int physicsengineinstancez = 1; //4

        public static int worldwidth = 1;
        public static int worldheight = 1;
        public static int worlddepth = 1;


        const int SwHide = 0;
        const int SwShow = 5;

        static int restartserverswtc = 0;


        static int worker000hasinit = 0;
        static int startthreadconsolewriter = 1;
        static int failedscreencapture = 0;


        static bool somereleaseframeswtc = false;

        static int framecounterforpctoarduinoscreenswtc = 0;
        static Stopwatch arduinotickswatch = new Stopwatch();
        static int framecounterforpctoarduinoscreen = 0;
        static int framecounterforpctoarduinoscreenMax = 20;
        static int framecounterforpctoarduinoscreenFinal = 0;
        static int writetobuffer = 0;
        //public static sccsscreencapture somesccsscreencapturemain;

        //for thread tests
        //static workerThreads workThread;
        //for thread tests


        static int someupdatethreadframecounter = 0;
        static int someupdatethreadframecountermax = 100;
        static void Main(string[] args)
        {

            //INITIALIZING COMPONENTS // for wpf or windowsforms
            //for (int j = 0; j < 1; j++)
            //{
            //  try
            //  {
            //      InitializeComponent();
            //     componentsexists = 1;
            //  }
            //  catch (Exception ex)
            //  {
            //    Console.WriteLine(ex.ToString());
            //    componentsexists = 0;
            //    break;
            //  }
            //}
            //INITIALIZING COMPONENTS // for wpf or windowsforms


            //MessageBox((IntPtr)0,"test","sccoresystems",0);



            //CREATING SC_Console//
            for (int j = 0; j < 1; j++)
            {
                try
                {
                    sccsglobalsaccessor = new sccsglobalsaccessor(mainreceivedmessages);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                    break;
                }
            }
            //CREATING SC_Console//

            //CREATING SC_Console//
            for (int j = 0; j < 1; j++)
            {
                try
                {
                    sccsglobalsaccessor.SCCSCONSOLECORE.createConsole(mainreceivedmessages);
                    //SC_GCGollect collector = new SC_GCGollect();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                    break;
                }
            }
            //CREATING SC_Console//


            //getting processor count
            for (int j = 0; j < 1; j++)
            {
                try
                {
                    processorCount = Environment.ProcessorCount;// SC_SystemInfoSeeker.getSystemProcessorCount();
                }
                catch //(Exception ex)
                {
                    break;
                }
            }
            //getting processor count

            //OVERIDE PROCESSOR COUNT
            processorCount = 1;





            ////////////////////
            ///KEYBOARD INPUT///
            ////////////////////
            for (int j = 0; j < 1; j++)
            {
                try
                {
                    keyboardinput = new sccsconsolekeyboardinput();
                    //keyboardstate = _keyboard_input._KeyboardState;

                    /*inputsim = new InputSimulator();
                    keyboardsim = new KeyboardSimulator(inputsim);
                    mousesim = new MouseSimulator(inputsim);*/
                }
                catch (Exception ex)
                {
                    MessageBox((IntPtr)0, "cannot get keyboard info main 00: " + ex.ToString() + "", "_sc_core_systems error", 0);
                    //something is wrong, todo something else later. but not implemented yet. maybe get raw input instead from SharpDX i dont know
                    break;
                }
            }
            for (int j = 0; j < 1; j++)
            {
                try
                {
                    if (keyboardinput != null)
                    {
                        keyboardinput.InitializeKeyboard();
                        keyboardinput.KeyboardState = new SharpDX.DirectInput.KeyboardState();
                    }
                    else
                    {
                        MessageBox((IntPtr)0, "cannot get keyboard info main 01: ", "_sc_core_systems error", 0);
                        //something is wrong, todo something else later. but not implemented yet. maybe get raw input instead from SharpDX i dont know
                    }
                }
                catch (Exception ex)
                {
                    MessageBox((IntPtr)0, "cannot get keyboard info main 02: " + ex.ToString() + "", "_sc_core_systems error", 0);
                    //something is wrong, todo something else later. but not implemented yet. maybe get raw input instead from SharpDX i dont know
                    break;
                }
            }

            //Console.WriteLine("test");


            /*int workerThreadsTotalGetMaxThreads;
            int portThreadsTotalGetMaxThreads;
            int workerThreadsTotalGetAvailableThreads;
            int portThreadsTotalGetAvailableThreads;

            for (int j = 0; j < 1; j++)
            {
                try
                {
                    ThreadPool.GetMaxThreads(out workerThreadsTotalGetMaxThreads, out portThreadsTotalGetMaxThreads);
                    ThreadPool.GetAvailableThreads(out workerThreadsTotalGetAvailableThreads, out portThreadsTotalGetAvailableThreads);
                }
                catch
                {
                    break;
                }
            }*/
            //totalThreads = (int)(portThreadsTotalGetAvailableThreads * 0.01f);


            //SETTING UP THIS MAINS PROGRAM MESSAGE STRUCT THAT FLOWS INSIDE OF CHOSEN THREADS OR TASKS STARTING FROM THE TOP LEVEL THREAD OF THIS PROGRAM DOWN TO WHEREVER THE PROGRAMMER WANTS IT TO FLOW TO.
            mainreceivedmessages = new sccsmessageobject[MaxSizeMainObject];
            for (int i = 0; i < mainreceivedmessages.Length; i++)
            {
                mainreceivedmessages[i] = new sccsmessageobject();
                mainreceivedmessages[i].receivedswitchin = -1;
                mainreceivedmessages[i].receivedswitchout = -1;
                mainreceivedmessages[i].sendingswitchin = -1;
                mainreceivedmessages[i].sendingswitchout = -1;
                mainreceivedmessages[i].timeOut0 = -1;
                mainreceivedmessages[i].ParentTaskThreadID0 = -1;
                mainreceivedmessages[i].maincpucount = processorCount;
                mainreceivedmessages[i].passTest = "";
                mainreceivedmessages[i].welcomePackage = -1;
                mainreceivedmessages[i].workdone = -1;
                mainreceivedmessages[i].currentmenu = -1;
                mainreceivedmessages[i].lastcurrentmenu = -1;
                mainreceivedmessages[i].mainmenu = -1;
                mainreceivedmessages[i].menuOption = "";
                mainreceivedmessages[i].voRecSwtc = -1;
                mainreceivedmessages[i].voRecMsg = "";
                mainreceivedmessages[i].someData = new object[MaxSizeMainObject];

                for (int j = 0; j < mainreceivedmessages[i].someData.Length; j++)
                {
                    mainreceivedmessages[i].someData[j] = new object();
                }

                //mainreceivedmessages[0].someData[0] = jitter physics worlds data in sccoresystems







                //VOICE RECOGNITION. NOT IMPLEMENTED YET.
                /*if (i == MaxSizeMainObject - 1)
                {
                    mainreceivedmessages[i]._someData[0] = _keyboard_input._KeyboardState;
                    mainreceivedmessages[i]._voRecSwtc = 1;
                }*/
            }
            //SETTING UP THIS MAINS PROGRAM MESSAGE STRUCT THAT FLOWS INSIDE OF CHOSEN THREADS OR TASKS STARTING FROM THE TOP LEVEL THREAD OF THIS PROGRAM DOWN TO WHEREVER THE PROGRAMMER WANTS IT TO FLOW TO.


            //SETTING UP THE CONSOLE WRITER MESSAGES USED PER FRAME OR NOT FOR UPDATES ON THE CONSOLE RENDERER AS TEXT OR NOT.
            for (int i = 0; i < sccsconsolewritermsg.Length; i++)
            {
                sccsconsolewritermsg[i].swtch0 = -1;
                sccsconsolewritermsg[i].messagerlist = new messager[MaxSizeMainObject];
                sccsconsolewritermsg[i].message = "";
                sccsconsolewritermsg[i].originalMsg = "";
                sccsconsolewritermsg[i].messageCut = "";
                sccsconsolewritermsg[i].specialMessage = -1;
                sccsconsolewritermsg[i].specialMessageLineX = 0;
                sccsconsolewritermsg[i].specialMessageLineY = 0;
                sccsconsolewritermsg[i].orilineX = 0;
                sccsconsolewritermsg[i].orilineY = 0;
                sccsconsolewritermsg[i].lineX = 0;
                sccsconsolewritermsg[i].lineY = 0;
                sccsconsolewritermsg[i].lastOrilineX = 0;
                sccsconsolewritermsg[i].lastOrilineY = 0;
                sccsconsolewritermsg[i].count = 0;
                sccsconsolewritermsg[i].swtch0 = 1;
                sccsconsolewritermsg[i].swtch1 = 1;
                sccsconsolewritermsg[i].delay = 50;
                sccsconsolewritermsg[i].looping = 1;
            }
            consolereaderstring.hasmessagetodisplay = 0;
            consolereaderstring.consolereadermessage = "";
            consolereaderstring.hasinit = 0;
            consolereaderobject = consolereaderstring;
            //SETTING UP THE CONSOLE WRITER MESSAGES USED PER FRAME OR NOT FOR UPDATES ON THE CONSOLE RENDERER AS TEXT OR NOT.


            //SOME TESTS
            //Console.WriteLine("hello world!");
            //
            //for (int i = 0; i < processorCount; i++)
            //{
            //    workThread = new workerThreads(i);
            //}
            //SOME TESTS
            SharpDX.DXGI.Adapter1 adapter;
            using (var factory = new SharpDX.DXGI.Factory1())
            {
                adapter = factory.GetAdapter1(0);
            }
            screencapturedevice = new SharpDX.Direct3D11.Device(adapter);


            int someinitswtc = 0;

            if (screencaptureenabled == 1)
            {
                somesccsscreencapturemain = new sccsscreencapture(0, 0, screencapturedevice);
            }


        mainthreadloop:

            try
            {
                if (someinitswtc == 0)
                {
                    /*Task consolewritertask = Task<object[]>.Factory.StartNew((tester0001) =>
                    {

                    //////CONSOLE WRITER=>
                    threadloopconsole:

                        /*if (someupdatethreadframecounter >= someupdatethreadframecountermax)
                        {
                            //Console.WriteLine("main thread started on program root is alive.");
                            someupdatethreadframecounter = 0;
                        }
                        someupdatethreadframecounter += 1;

                        //main program root thread update. contains: console options - and enables the directx VR or not rendering based on user input - 
                        mainthreadupdate(mainreceivedmessages);
                        //main program root thread update. contains: console options - and enables the directx VR or not rendering based on user input - 
                        mainthreadupdate(mainreceivedmessages);
                        Thread.Sleep(1);

                        goto threadloopconsole;
                        //////CONSOLE WRITER <=
                    }, mainreceivedmessages);*/




                    //MAIN UPDATE THREAD AND IT IS INSIDE OF A BACKGROUND THREAD SO USING THE DISPATCHER TO ACCESS UI FUNCTIONS MIGHT BE REQUIRED. NOT FULLY TESTED BUT THIS TYPE OF UPDATE THREAD WORKS WITH RENDERING AND PHYSICS ENGINE STILL, AND IT'S BLAZING FAST.  CAN BE SWAPPED WITH A BACKGROUNDWORKER OR TASK BUT I ENDED UP USING A THREAD.
                    Thread mainthread = new Thread((tester0000) =>
                    {
                    //var somesccsscreencapturesec = new sccsscreencapture();
                    threadmainloop:

                        /*if (someupdatethreadframecounter >= someupdatethreadframecountermax)
                        {
                            //Console.WriteLine("main thread started on program root is alive.");
                            someupdatethreadframecounter = 0;
                        }
                        someupdatethreadframecounter += 1;*/

                        //main program root thread update. contains: console options - and enables the directx VR or not rendering based on user input - 
                        mainthreadupdate(mainreceivedmessages);
                        //main program root thread update. contains: console options - and enables the directx VR or not rendering based on user input - 


                        Thread.Sleep(1);
                        goto threadmainloop;

                    }, 10); //100000 //999999999
                    mainthread.IsBackground = true; // false
                    mainthread.SetApartmentState(ApartmentState.STA);
                    mainthread.Start();
                    //MAIN UPDATE THREAD AND IT IS INSIDE OF A BACKGROUND THREAD SO USING THE DISPATCHER TO ACCESS UI FUNCTIONS MIGHT BE REQUIRED. NOT FULLY TESTED BUT THIS TYPE OF UPDATE THREAD WORKS WITH RENDERING AND PHYSICS ENGINE STILL, AND IT'S BLAZING FAST. CAN BE SWAPPED WITH A BACKGROUNDWORKER OR TASK BUT I ENDED UP USING A THREAD.




                    /*
                    BackgroundWorker BackgroundWorker_00 = new BackgroundWorker();
                    BackgroundWorker_00.DoWork += (object sender, DoWorkEventArgs argers) =>
                    {
                        //Program.MessageBox((IntPtr)0, "threadstart succes", "sc core systems message", 0);
                       // Stopwatch _this_thread_ticks_01 = new Stopwatch();
                        //_this_thread_ticks_01.Start();

                    _thread_looper:

                        mainthreadupdate(mainreceivedmessages);
                        //_ticks_watch.Restart();

                        //Console.WriteLine(_ticks_watch.Elapsed.Ticks);
                        Thread.Sleep(1);
                        goto _thread_looper;
                    };

                    BackgroundWorker_00.RunWorkerCompleted += delegate (object sender, RunWorkerCompletedEventArgs argers)
                    {

                    };

                    BackgroundWorker_00.RunWorkerAsync();*/













                    /*if (restartserverswtc == 0)
                    {
                        runserver();
                        restartserverswtc = 1;
                    }*/

                    someinitswtc = 1;
                }

                //main program root update - for ui operations without using the dispatcher. but if being inside of the thread above or any other functions within it and in other scripts, using the dispatcher is the way that i go if i would make ui calls.
                //someupdate();
                //main program root update - for ui operations without using the dispatcher. but if being inside of the thread above or any other functions within it and in other scripts, using the dispatcher is the way that i go if i would make ui calls.
                Thread.Sleep(1);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                goto mainthreadloop;
            }

            //sending back the program root frame back upwards to the start of the goto statement so that we capture the frame and start the program.
            if (someinitswtc == 1)
            {
                if (System.Diagnostics.Debugger.IsAttached)
                {
                    goto mainthreadloop;
                }
                else
                {
                    goto mainthreadloop;
                }
            }
            else
            {
                goto mainthreadloop;
            }
            //sending back the program root frame back upwards to the start of the goto statement so that we capture the frame and start the program.


            //Console.WriteLine("end of program");
            //Console.ReadLine();
            //Console.WriteLine("end of program");
            /*if (someupdateframecounter >= someupdateframecountermax && someupdateframecounterswtc == 0)
            {
                //if (restartserverswtc == 0)
                //{
                //    runserver();
                //    restartserverswtc = 1;
                //}


                someupdateframecounterswtc = 1;

                //Console.WriteLine("main root alive.");
                someupdateframecounter = 0;
            }
            someupdateframecounter += 1;
            Thread.Sleep(0);
            goto mainthreadloop;*/
        }




        static sccsmessageobject[] mainthreadupdate(sccsmessageobject[] mainreceivedmessages)
        {
            //////READ KEYBOARD=>
            keyboardinput.ReadKeyboard();
            //////READ KEYBOARD=>

            //CREATION OF THE CONSOLE WRITER SO THAT A THREAD READS THE CONSOLE AT ALL TIMES FOR USER INPUT.
            if (startthreadconsolewriter == 1) //1
            {
                Task consolewritertask = Task<object[]>.Factory.StartNew((tester0001) =>
                {
                    var initX = (Console.WindowWidth / 2) - (programname.Length / 2);
                    var initY = (Console.WindowHeight / 2);

                    sccsconsolewritermsg[0].message = programname;
                    sccsconsolewritermsg[0].originalMsg = programname;
                    sccsconsolewritermsg[0].messageCut = programname;
                    sccsconsolewritermsg[0].specialMessage = 0;
                    sccsconsolewritermsg[0].specialMessageLineX = 0;
                    sccsconsolewritermsg[0].specialMessageLineY = 0;
                    sccsconsolewritermsg[0].orilineX = initX;
                    sccsconsolewritermsg[0].orilineY = initY;
                    sccsconsolewritermsg[0].lineX = initX;
                    sccsconsolewritermsg[0].lineY = initY;
                    sccsconsolewritermsg[0].lastOrilineX = initX;
                    sccsconsolewritermsg[0].lastOrilineY = initY;
                    sccsconsolewritermsg[0].count = 0;
                    sccsconsolewritermsg[0].swtch0 = 1;
                    sccsconsolewritermsg[0].swtch1 = 1;
                    sccsconsolewritermsg[0].delay = 5;
                    sccsconsolewritermsg[0].looping = 1;



                //////CONSOLE WRITER=>
                threadloopconsole:

                    sccsconsolewritermsg = sccsglobalsaccessor.SCCSCONSOLEWRITER.consolewriter(sccsconsolewritermsg);

                    Thread.Sleep(1);

                    goto threadloopconsole;
                    //////CONSOLE WRITER <=
                }, mainreceivedmessages);
                startthreadconsolewriter = 2;
                worker000hasinit = 1;
            }
            //CREATION OF THE CONSOLE WRITER SO THAT A THREAD READS THE CONSOLE AT ALL TIMES FOR USER INPUT.


            //CONFIRM CONSOLE WRITER IS WORKING=>
            if (worker000hasinit == 1)
            {
                if (sccsglobalsaccessor != null)
                {
                    if (sccsglobalsaccessor.SCCSCONSOLEWRITER != null)
                    {
                        sccsconsolewritermsg[1].message = "Console-WR" + " ENABLED";
                        sccsconsolewritermsg[1].originalMsg = "Console-WR" + " ENABLED";
                        sccsconsolewritermsg[1].messageCut = "Console-WR" + " ENABLED";
                        sccsconsolewritermsg[1].specialMessage = 0;
                        sccsconsolewritermsg[1].specialMessageLineX = 0;
                        sccsconsolewritermsg[1].specialMessageLineY = 0;
                        sccsconsolewritermsg[1].orilineX = 1;
                        sccsconsolewritermsg[1].orilineY = Console.WindowHeight - 2;
                        sccsconsolewritermsg[1].lineX = 1;
                        sccsconsolewritermsg[1].lineY = Console.WindowHeight - 2;
                        sccsconsolewritermsg[1].lastOrilineX = sccsconsolewritermsg[1].lineX;
                        sccsconsolewritermsg[1].lastOrilineY = sccsconsolewritermsg[1].lineY;
                        sccsconsolewritermsg[1].count = 0;
                        sccsconsolewritermsg[1].swtch0 = 1;
                        sccsconsolewritermsg[1].swtch1 = 1;
                        sccsconsolewritermsg[1].delay = 10;
                        sccsconsolewritermsg[1].looping = 1;

                        worker000hasinit = 2;
                    }
                }
            }
            //CONFIRM CONSOLE WRITER IS WORKING<=




            //CONFIRM CONSOLE READER IS WORKING=>
            if (worker000hasinit == 2) //2
            {
                if (sccsglobalsaccessor != null)
                {
                    if (sccsglobalsaccessor.SCCSCONSOLEREADER != null)
                    {
                        sccsconsolewritermsg[2].message = "Console-R" + " ENABLED";
                        sccsconsolewritermsg[2].originalMsg = "Console-R" + " ENABLED";
                        sccsconsolewritermsg[2].messageCut = "Console-R" + " ENABLED";
                        sccsconsolewritermsg[2].specialMessage = 0;
                        sccsconsolewritermsg[2].specialMessageLineX = 0;
                        sccsconsolewritermsg[2].specialMessageLineY = 0;
                        sccsconsolewritermsg[2].orilineX = 1;
                        sccsconsolewritermsg[2].orilineY = Console.WindowHeight - 3;
                        sccsconsolewritermsg[2].lineX = 1;
                        sccsconsolewritermsg[2].lineY = Console.WindowHeight - 3;
                        sccsconsolewritermsg[2].lastOrilineX = sccsconsolewritermsg[1].lineX;
                        sccsconsolewritermsg[2].lastOrilineY = sccsconsolewritermsg[1].lineY;
                        sccsconsolewritermsg[2].count = 0;
                        sccsconsolewritermsg[2].swtch0 = 1;
                        sccsconsolewritermsg[2].swtch1 = 1;
                        sccsconsolewritermsg[2].delay = 10;
                        sccsconsolewritermsg[2].looping = 1;

                        worker000hasinit = 3;
                    }
                }
            }
            //CONFIRM CONSOLE READER IS WORKING<=


            //ADVISING THE USER THE PROGRAM IS READY FOR CONSOLE KEYBOARD INPUT.
            if (worker000hasinit == 3)
            {
                if (sccsglobalsaccessor != null)
                {
                    if (sccsglobalsaccessor.SCCSCONSOLEREADER != null)
                    {
                        var programname0 = "Press Enter";
                        initconsolecursorx = (Console.WindowWidth / 2) - (programname0.Length / 2);
                        initconsolecursory = (Console.WindowHeight / 2) + 1;

                        sccsconsolewritermsg[3].message = programname0;
                        sccsconsolewritermsg[3].originalMsg = programname0;
                        sccsconsolewritermsg[3].messageCut = programname0;
                        sccsconsolewritermsg[3].specialMessage = 2;
                        sccsconsolewritermsg[3].specialMessageLineX = 0;
                        sccsconsolewritermsg[3].specialMessageLineY = 0;
                        sccsconsolewritermsg[3].orilineX = initconsolecursorx;
                        sccsconsolewritermsg[3].orilineY = initconsolecursory;
                        sccsconsolewritermsg[3].lineX = initconsolecursorx;
                        sccsconsolewritermsg[3].lineY = initconsolecursory;
                        sccsconsolewritermsg[3].count = 0;
                        sccsconsolewritermsg[3].swtch0 = 1;
                        sccsconsolewritermsg[3].swtch1 = 1;
                        sccsconsolewritermsg[3].delay = 100;
                        sccsconsolewritermsg[3].looping = 1;
                        worker000hasinit = 4;
                    }
                }
            }
            //ADVISING THE USER THE PROGRAM IS READY FOR CONSOLE KEYBOARD INPUT.

            //CREATING A TASK THAT RUNS IN TANDEM WITH THE CONSOLE READER AND THIS TASK READS THE USER INPUT AND SENDS IT TO THE TASK INSIDE OF THE CONSOLE READER.
            if (worker000hasinit == 4) //4
            {
                Task consolereadertask = Task<object[]>.Factory.StartNew((tester0001) =>
                {
                    while (true)
                    {
                        if (consolereadercanWork == 1)
                        {
                            consolereaderstring = sccsglobalsaccessor.SCCSCONSOLEREADER.consolereader(consolereaderobject);
                        }

                        if (sccsglobalsaccessor.SCCSCONSOLEREADER.mainhasinit == 1)
                        {
                            consolereaderstring.consolereadermessage = "";
                            consolereaderstring.hasmessagetodisplay = 0;

                            var programname0 = "WELCOME";
                            initconsolecursorx = (Console.WindowWidth / 2) - (programname0.Length / 2);
                            initconsolecursory = (Console.WindowHeight / 2) - 1;

                            sccsconsolewritermsg[4].message = programname0;
                            sccsconsolewritermsg[4].originalMsg = programname0;
                            sccsconsolewritermsg[4].messageCut = programname0;
                            sccsconsolewritermsg[4].specialMessage = 2;
                            sccsconsolewritermsg[4].specialMessageLineX = 0;
                            sccsconsolewritermsg[4].specialMessageLineY = 0;
                            sccsconsolewritermsg[4].orilineX = initconsolecursorx;
                            sccsconsolewritermsg[4].orilineY = initconsolecursory;
                            sccsconsolewritermsg[4].lineX = initconsolecursorx;
                            sccsconsolewritermsg[4].lineY = initconsolecursory;
                            sccsconsolewritermsg[4].count = 0;
                            sccsconsolewritermsg[4].swtch0 = 1;
                            sccsconsolewritermsg[4].swtch1 = 0;
                            sccsconsolewritermsg[4].delay = 200;
                            sccsconsolewritermsg[4].looping = 0;

                            sccsconsolewritermsg[0].swtch0 = 0;
                            sccsconsolewritermsg[0].swtch1 = 0;
                            sccsconsolewritermsg[3].swtch0 = 0;
                            sccsconsolewritermsg[3].swtch1 = 0;

                            programname0 = "Please Enter your Username: ";
                            initconsolecursorx = (Console.WindowWidth / 2) - (programname0.Length / 2);
                            initconsolecursory = (Console.WindowHeight / 2) + 2;

                            sccsconsolewritermsg[6].message = programname0;
                            sccsconsolewritermsg[6].originalMsg = programname0;
                            sccsconsolewritermsg[6].messageCut = programname0;
                            sccsconsolewritermsg[6].specialMessage = 2;
                            sccsconsolewritermsg[6].specialMessageLineX = 0;
                            sccsconsolewritermsg[6].specialMessageLineY = 0;
                            sccsconsolewritermsg[6].orilineX = initconsolecursorx;
                            sccsconsolewritermsg[6].orilineY = initconsolecursory;
                            sccsconsolewritermsg[6].lineX = initconsolecursorx;
                            sccsconsolewritermsg[6].lineY = initconsolecursory;
                            sccsconsolewritermsg[6].count = 0;
                            sccsconsolewritermsg[6].swtch0 = 1;
                            sccsconsolewritermsg[6].swtch1 = 1;
                            sccsconsolewritermsg[6].delay = 50;
                            sccsconsolewritermsg[6].looping = 1;

                            Console.SetCursorPosition(initconsolecursorx + programname0.Length, initconsolecursory);

                            startThread = 3;
                            sccsglobalsaccessor.SCCSCONSOLEREADER.mainhasinit = 2;
                        }

                        if (startThread == 3 && consolereaderstring.hasmessagetodisplay == 1)
                        {

                            if (consolereaderstring.consolereadermessage.ToLower() == "nine" || consolereaderstring.consolereadermessage.ToLower() == "ninekorn" || consolereaderstring.consolereadermessage.ToLower() == "9")
                            {

                                var programname0 = "Access Authorized";
                                initconsolecursorx = (Console.WindowWidth / 2) - (programname0.Length / 2);
                                initconsolecursory = (Console.WindowHeight / 2) + 2;

                                sccsconsolewritermsg[6].message = programname0;
                                sccsconsolewritermsg[6].originalMsg = programname0;
                                sccsconsolewritermsg[6].messageCut = programname0;
                                sccsconsolewritermsg[6].specialMessage = 2;
                                sccsconsolewritermsg[6].specialMessageLineX = 0;
                                sccsconsolewritermsg[6].specialMessageLineY = 0;
                                sccsconsolewritermsg[6].lineX = initconsolecursorx;
                                sccsconsolewritermsg[6].lineY = initconsolecursory;
                                sccsconsolewritermsg[6].count = 0;
                                sccsconsolewritermsg[6].swtch0 = 1;
                                sccsconsolewritermsg[6].swtch1 = 0;
                                sccsconsolewritermsg[6].delay = 50;
                                sccsconsolewritermsg[6].looping = 0;

                                for (int L0IN = 0; L0IN < mainreceivedmessages.Length; L0IN++)
                                {
                                    mainreceivedmessages[L0IN].passTest = consolereaderstring.consolereadermessage.ToLower();
                                }
                                Console.SetCursorPosition(initconsolecursorx, initconsolecursory + 1);
                                lastUsername = consolereaderstring.consolereadermessage;
                                consolereaderstring.consolereadermessage = "";
                                startThread = 4;
                            }
                            else if (consolereaderstring.consolereadermessage.ToLower() != " " || consolereaderstring.consolereadermessage.ToLower() != "")
                            {

                                var programname0 = "Access Denied";
                                initconsolecursorx = (Console.WindowWidth / 2) - (programname0.Length / 2);
                                initconsolecursory = (Console.WindowHeight / 2) + 2;

                                sccsconsolewritermsg[6].message = programname0;
                                sccsconsolewritermsg[6].originalMsg = programname0;
                                sccsconsolewritermsg[6].messageCut = programname0;
                                sccsconsolewritermsg[6].specialMessage = 2;
                                sccsconsolewritermsg[6].specialMessageLineX = 0;
                                sccsconsolewritermsg[6].specialMessageLineY = 0;
                                sccsconsolewritermsg[6].lineX = initconsolecursorx;
                                sccsconsolewritermsg[6].lineY = initconsolecursory;
                                sccsconsolewritermsg[6].count = 0;
                                sccsconsolewritermsg[6].swtch0 = 1;
                                sccsconsolewritermsg[6].swtch1 = 0;
                                sccsconsolewritermsg[6].delay = 50;
                                sccsconsolewritermsg[6].looping = 0;

                                lastUsername = "";
                                consolereaderstring.consolereadermessage = "";
                                Console.SetCursorPosition(initconsolecursorx, initconsolecursory);
                                startThread = 3;
                            }
                        }
                        else if (startThread == 4)
                        {
                            if (consolereaderstring.consolereadermessage.ToLower() == "vr" ||
                                 consolereaderstring.consolereadermessage.ToLower() == "standard" ||
                                  consolereaderstring.consolereadermessage.ToLower() == "std")
                            {
                                if (consolereaderstring.consolereadermessage.ToLower() == "vr")
                                {

                                    var programname0 = "creating VR mecanics";
                                    initconsolecursorx = (Console.WindowWidth / 2) - (programname0.Length / 2);
                                    initconsolecursory = (Console.WindowHeight / 2) + 2;

                                    sccsconsolewritermsg[6].message = programname0;
                                    sccsconsolewritermsg[6].originalMsg = programname0;
                                    sccsconsolewritermsg[6].messageCut = programname0;
                                    sccsconsolewritermsg[6].specialMessage = 2;
                                    sccsconsolewritermsg[6].specialMessageLineX = 0;
                                    sccsconsolewritermsg[6].specialMessageLineY = 0;
                                    sccsconsolewritermsg[6].lineX = initconsolecursorx;
                                    sccsconsolewritermsg[6].lineY = initconsolecursory;
                                    sccsconsolewritermsg[6].count = 0;
                                    sccsconsolewritermsg[6].swtch0 = 1;
                                    sccsconsolewritermsg[6].swtch1 = 0;
                                    sccsconsolewritermsg[6].delay = 50;
                                    sccsconsolewritermsg[6].looping = 0;

                                    lastMenuOption = consolereaderstring.consolereadermessage.ToLower();
                                    consolereaderstring.consolereadermessage = "";

                                    mainreceivedmessages[0].receivedswitchin = 1;
                                    mainreceivedmessages[0].receivedswitchout = 1;
                                    mainreceivedmessages[0].sendingswitchin = 1;
                                    mainreceivedmessages[0].sendingswitchout = 1;
                                    mainreceivedmessages[0].welcomePackage = 999;

                                    mainreceivedmessages = scconsolemenu.scconsolemenu00.consolemenu(mainreceivedmessages);
                                    Console.SetCursorPosition(initconsolecursorx, initconsolecursory + 1);
                                    someotherswtch = 1;
                                }
                                else if (consolereaderstring.consolereadermessage.ToLower() == "standard" ||
                                        consolereaderstring.consolereadermessage.ToLower() == "std")
                                {

                                    lastMenuOption = consolereaderstring.consolereadermessage.ToLower();
                                    consolereaderstring.consolereadermessage = "";

                                    mainreceivedmessages[0].receivedswitchin = 1;
                                    mainreceivedmessages[0].receivedswitchout = 1;
                                    mainreceivedmessages[0].sendingswitchin = 1;
                                    mainreceivedmessages[0].sendingswitchout = 1;
                                    mainreceivedmessages[0].welcomePackage = 998;
                                    mainreceivedmessages = scconsolemenu.scconsolemenu00.consolemenu(mainreceivedmessages);
                                    Console.SetCursorPosition(initconsolecursorx, initconsolecursory + 1);
                                    someotherswtch = 1;
                                }
                            }
                            else
                            {

                                var programname0 = "Option Not Implemented";
                                initconsolecursorx = (Console.WindowWidth / 2) - (programname0.Length / 2);
                                initconsolecursory = (Console.WindowHeight / 2) + 2;

                                sccsconsolewritermsg[6].message = programname0;
                                sccsconsolewritermsg[6].originalMsg = programname0;
                                sccsconsolewritermsg[6].messageCut = programname0;
                                sccsconsolewritermsg[6].specialMessage = 2;
                                sccsconsolewritermsg[6].specialMessageLineX = 0;
                                sccsconsolewritermsg[6].specialMessageLineY = 0;
                                sccsconsolewritermsg[6].lineX = initconsolecursorx;
                                sccsconsolewritermsg[6].lineY = initconsolecursory;
                                sccsconsolewritermsg[6].count = 0;
                                sccsconsolewritermsg[6].swtch0 = 1;
                                sccsconsolewritermsg[6].swtch1 = 0;
                                sccsconsolewritermsg[6].delay = 50;
                                sccsconsolewritermsg[6].looping = 0;

                                lastMenuOption = "";
                                consolereaderstring.consolereadermessage = "";

                                Console.SetCursorPosition(initconsolecursorx, initconsolecursory);
                            }
                        }

                        Thread.Sleep(1);
                    }
                }, mainreceivedmessages);
                worker000hasinit = 5;
            }
            //CREATING A TASK THAT RUNS IN TANDEM WITH THE CONSOLE READER AND THIS TASK READS THE USER INPUT AND SENDS IT TO THE TASK INSIDE OF THE CONSOLE READER.



            //SECTION OF THE MENU THAT DECIDES OF THE WELCOME PACKAGE TO GIVE THE USER BASED ON THE KEYBOARD PASSWORD INPUT INSIDE OF THE CONSOLE.
            if (worker000hasinit == 5) //5
            {
                Task consoleworkertask = Task<object[]>.Factory.StartNew((tester0001) =>
                {
                    while (true)
                    {
                        if (worker000hasinit == 2)
                        {
                            int welcomePackage00 = mainreceivedmessages[0].welcomePackage;

                            if (welcomePackage00 == 0)
                            {
                                mainreceivedmessages = scconsolemenu.scconsolemenu00.consolemenu(mainreceivedmessages);
                            }
                            else if (welcomePackage00 == 1)
                            {
                                int currentmenu00 = data00OUT.currentmenu;

                                if (lastMenu != currentmenu00)
                                {
                                    var programname0 = currentmenu00 + "";
                                    initconsolecursorx = (Console.WindowWidth / 2) - (programname0.Length / 2);
                                    initconsolecursory = (Console.WindowHeight / 2) + 9;
                                    sccsconsolewritermsg[5].message = programname0;
                                    sccsconsolewritermsg[5].originalMsg = programname0;
                                    sccsconsolewritermsg[5].messageCut = programname0;
                                    sccsconsolewritermsg[5].specialMessage = 2;
                                    sccsconsolewritermsg[5].specialMessageLineX = 0;
                                    sccsconsolewritermsg[5].specialMessageLineY = 0;
                                    sccsconsolewritermsg[5].lineX = initconsolecursorx;
                                    sccsconsolewritermsg[5].lineY = initconsolecursory;
                                    sccsconsolewritermsg[5].count = 0;
                                    sccsconsolewritermsg[5].swtch0 = 1;
                                    sccsconsolewritermsg[5].swtch1 = 0;
                                    sccsconsolewritermsg[5].delay = 50;
                                    sccsconsolewritermsg[5].looping = 0;
                                }

                                if (currentmenu00 == -1)
                                {
                                    data00IN.receivedswitchin = 0;
                                    data00IN.receivedswitchout = 0;
                                    data00IN.sendingswitchin = 0;
                                    data00IN.sendingswitchout = 0;

                                    data00IN.currentmenu = data00OUT.currentmenu;
                                    data00IN.menuOption = lastMenuOption;

                                    var objecterer = data00IN;
                                    data00OUT = scconsolemenu.scconsolemenu01.consolemenu(objecterer);

                                    lastMenu = data00OUT.currentmenu;
                                    lastMenuOption = "";
                                }
                                else if (currentmenu00 == 0)
                                {
                                    var programname0 = currentmenu00 + "";
                                    initconsolecursorx = (Console.WindowWidth / 2) - (programname0.Length / 2);
                                    initconsolecursory = (Console.WindowHeight / 2) + 9;
                                    sccsconsolewritermsg[5].message = programname0;
                                    sccsconsolewritermsg[5].originalMsg = programname0;
                                    sccsconsolewritermsg[5].messageCut = programname0;
                                    sccsconsolewritermsg[5].specialMessage = 2;
                                    sccsconsolewritermsg[5].specialMessageLineX = 0;
                                    sccsconsolewritermsg[5].specialMessageLineY = 0;
                                    sccsconsolewritermsg[5].lineX = initconsolecursorx;
                                    sccsconsolewritermsg[5].lineY = initconsolecursory;
                                    sccsconsolewritermsg[5].count = 0;
                                    sccsconsolewritermsg[5].swtch0 = 1;
                                    sccsconsolewritermsg[5].swtch1 = 0;
                                    sccsconsolewritermsg[5].delay = 50;
                                    sccsconsolewritermsg[5].looping = 0;

                                    data00IN.receivedswitchin = 0;
                                    data00IN.receivedswitchout = 0;
                                    data00IN.sendingswitchin = 0;
                                    data00IN.sendingswitchout = 0;

                                    data00IN.currentmenu = data00OUT.currentmenu;
                                    data00IN.menuOption = lastMenuOption;

                                    var objecterer = data00IN;
                                    data00OUT = scconsolemenu.scconsolemenu01.consolemenu(objecterer);
                                    lastMenu = data00OUT.currentmenu;
                                    lastMenuOption = "";
                                }
                                else if (currentmenu00 == 1)
                                {
                                    var programname0 = currentmenu00 + "";
                                    initconsolecursorx = (Console.WindowWidth / 2) - (programname0.Length / 2);
                                    initconsolecursory = (Console.WindowHeight / 2) + 9;
                                    sccsconsolewritermsg[5].message = programname0;
                                    sccsconsolewritermsg[5].originalMsg = programname0;
                                    sccsconsolewritermsg[5].messageCut = programname0;
                                    sccsconsolewritermsg[5].specialMessage = 2;
                                    sccsconsolewritermsg[5].specialMessageLineX = 0;
                                    sccsconsolewritermsg[5].specialMessageLineY = 0;
                                    sccsconsolewritermsg[5].lineX = initconsolecursorx;
                                    sccsconsolewritermsg[5].lineY = initconsolecursory;
                                    sccsconsolewritermsg[5].count = 0;
                                    sccsconsolewritermsg[5].swtch0 = 1;
                                    sccsconsolewritermsg[5].swtch1 = 0;
                                    sccsconsolewritermsg[5].delay = 50;
                                    sccsconsolewritermsg[5].looping = 0;

                                    data00IN.receivedswitchin = 0;
                                    data00IN.receivedswitchout = 0;
                                    data00IN.sendingswitchin = 0;
                                    data00IN.sendingswitchout = 0;

                                    data00IN.currentmenu = data00OUT.currentmenu;
                                    data00IN.menuOption = lastMenuOption;

                                    var objecterer = data00IN;
                                    data00OUT = scconsolemenu.scconsolemenu01.consolemenu(objecterer);
                                    lastMenu = data00OUT.currentmenu;
                                    lastMenuOption = "";
                                }
                            }
                        }
                        Thread.Sleep(1);
                    }
                }, mainreceivedmessages);
                worker000hasinit = 6;
            }
            //SECTION OF THE MENU THAT DECIDES OF THE WELCOME PACKAGE TO GIVE THE USER BASED ON THE KEYBOARD PASSWORD INPUT INSIDE OF THE CONSOLE.



            //SECTION TO CREATE THE GRAPHICS UPDATE
            if (someotherswtch == 1)  //_some_other_swtch == 1
            {
                if (hasinitdirectx == 0)
                {
                    if (initdirectXmainswtch == 2 || initvrmainswtch == 2)
                    {
                        if (initdirectXmainswtch == 2)
                        {
                            config = new scsystemconfiguration("sc core systems", 1920, 1080, false, false);

                            //handler = scconsolecore.handle;// SCGLOBALSACCESSORS.SCCONSOLECORE.handle;

                            //if (handler == IntPtr.Zero)
                            //{
                            //MessageBox((IntPtr)0, "null console ", "sccoresystems error", 0);
                            //}
                            //else
                            //{
                            //    //MessageBox((IntPtr)0, "!null console ", "sccoresystems error", 0);
                            //}
                            //scupdate = new SCUpdate();

                            initdirectXmainswtch = 3;
                        }

                        if (initvrmainswtch == 2)
                        {
                            config = new scsystemconfiguration("sc core systems", 1920, 1080, false, false);
                            //graphicsupdate = new sccsgraphicsupdate();
                            //sccsglobalsaccessor.D3D = graphicsupdate.D3D;

                            initvrmainswtch = 3;
                        }

                        hasinitdirectx = 1;
                    }
                }
            }
            //SECTION TO CREATE THE GRAPHICS UPDATE




            /*if (screencaptureenabled == 1)
            {
                if (initscreencaptureclass == 1)
                {
                    if (pythontoprogramswtc == 0)
                    {
                        mainreceivedmessages[0].someData[1] = somesccsscreencapturemaindata;
                    }
                }
            }*/


            if (screencaptureenabled == 1)
            {
                //Console.WriteLine("initializing screen capture");
                /*if (somesccsscreencapturemain != null && initscreencaptureclass == 0) //sccssharpdxdirectx.D3D != null && initscreencaptureclass == 0 && 
                {

                    //somesccsscreencapturemain = new sccsscreencapture(0, 0, screencapturedevice);
                    Console.SetCursorPosition(0, 5);
                    Console.WriteLine("initialized screen capture");
                    initscreencaptureclass = 1;
                }*/

                if (initscreencaptureclass == 1) //1
                {
                    for (int i = 0; i < 3; i++)
                    {
                        try
                        {
                            somesccsscreencapturemaindata = somesccsscreencapturemain.ScreenCapture(10);
                            /*somereleaseframeswtc = somesccsscreencapturemain.releaseFrame();

                            if (!somereleaseframeswtc)
                            {
                                somesccsscreencapturemain = null;
                                somesccsscreencapturemain = new sccsscreencapture();
                            }*/
                            break;
                        }
                        catch (Exception ex)
                        {
                            /*if (somesccsscreencapturemain!= null)
                            {
                                somesccsscreencapturemain = null;

                                //somesccsscreencapturemain.Dispose();

                                //somereleaseframeswtc = somesccsscreencapturemain.releaseFrame();
                            }
                            else
                            {
                                //somesccsscreencapturemain.Dispose();
                            }*/
                            somesccsscreencapturemain.Dispose();
                            somesccsscreencapturemain = new sccsscreencapture(0, 0, screencapturedevice);
                            /*if (!somereleaseframeswtc)
                            {
                                somesccsscreencapturemain = null;
                            }
                            somesccsscreencapturemain = new sccsscreencapture();*/


                            //somesccsscreencapturemain.Dispose();
                            //somesccsscreencapturemain = new sccsscreencapture();

                            //Console.WriteLine(ex.ToString());

                            MessageBox((IntPtr)0, ex.ToString(), "sccoresystems", 0);
                        }
                    }






                    mainreceivedmessages[0].someData[1] = somesccsscreencapturemaindata;







                    if (pythontoprogramswtc == -9) //0
                    {
                        Task pythontoprogramtask = Task<object[]>.Factory.StartNew((tester0001) =>
                        {
                            // Open the named pipe.
                            var server = new NamedPipeServerStream("sccsmscpcTopi");
                            //Console.WriteLine("Waiting for connection... task c# to python.");

                            string somemessage = "python pipe Pc to Pi. Waiting for connection...";
                            initconsolecursorx = 0; //(Console.WindowWidth / 2) - (somemessage.Length / 2)
                            initconsolecursory = 0; //(Console.WindowHeight / 2) + 

                            sccsconsolewritermsg[7].message = somemessage;
                            sccsconsolewritermsg[7].originalMsg = somemessage;
                            sccsconsolewritermsg[7].messageCut = somemessage;
                            sccsconsolewritermsg[7].specialMessage = 2;
                            sccsconsolewritermsg[7].specialMessageLineX = 0;
                            sccsconsolewritermsg[7].specialMessageLineY = 0;
                            sccsconsolewritermsg[7].orilineX = initconsolecursorx;
                            sccsconsolewritermsg[7].orilineY = initconsolecursory;
                            sccsconsolewritermsg[7].lineX = initconsolecursorx;
                            sccsconsolewritermsg[7].lineY = initconsolecursory;
                            sccsconsolewritermsg[7].count = 0;
                            sccsconsolewritermsg[7].swtch0 = 1;
                            sccsconsolewritermsg[7].swtch1 = 1;
                            sccsconsolewritermsg[7].delay = 5;
                            sccsconsolewritermsg[7].looping = 1;



                            server.WaitForConnection();

                            var br = new BinaryReader(server);
                            var bw = new BinaryWriter(server);

                            int frameoffsetcounterswtc = 0;
                            int frameoffsetcounter = 0;
                            int frameoffsetcountermax = 1;

                            int screencaptureimagecounter = 0;
                            int screencaptureimagecountermax = 100;


                        //////frame to python in =>
                        threadloopconsole:

                            /*try
                            {
                                var len = (int)br.ReadUInt32();            // Read string length
                                var str = new string(br.ReadChars(len));    // Read string

                                initconsolecursorx = 0;
                                initconsolecursory = 1;

                                //Console.SetCursorPosition(0, 1);
                                //Console.WriteLine("Read: \"{0}\"", str);
                                //Console.WriteLine("Read: \"{0}\"", str);

                                //string somemessagestringfinal = new string(str.Reverse().ToArray()); //new string(str.Reverse().ToArray()); //
                                                                                                       //send to python somesccsscreencapturemaindata.screencapturearrayofbytes

                                /*

                                for (int b = 0; b < somesccsscreencapturemaindata.screencapturearrayofbytes.Length;b++)
                                {
                                    //https://stackoverflow.com/questions/11654562/how-to-convert-byte-array-to-string
                                    var somemessagestring = System.Text.Encoding.Default.GetString(somesccsscreencapturemaindata.screencapturearrayofbytes[b]);
                                    somemessagestringfinal += somemessagestring;
                                }



                                str = new string(str.Reverse().ToArray());  // Just for fun
                                var buf = Encoding.ASCII.GetBytes(str);     // Get ASCII byte array     
                                bw.Write((uint)buf.Length);                // Write string length
                                bw.Write(buf);


                                //Console.SetCursorPosition(0, 2);
                                //Console.WriteLine("Wrote: \"{0}\"", str);
                                //Console.WriteLine("Wrote: \"{0}\"", str);
                            }
                            catch (EndOfStreamException)
                            {
                                //break;// When client disconnects
                            }*/

                            try
                            {
                                var len = (int)br.ReadUInt32();            // Read string length
                                var str = new string(br.ReadChars(len));    // Read string

                                initconsolecursorx = 0;
                                initconsolecursory = 1;

                                //Console.SetCursorPosition(0, 1);
                                //Console.WriteLine("Read: \"{0}\"", str);
                                //Console.WriteLine("Read: \"{0}\"", str);

                                //string somemessagestringfinal = new string(str.Reverse().ToArray()); //new string(str.Reverse().ToArray()); //
                                //send to python somesccsscreencapturemaindata.screencapturearrayofbytes


                                string somemessagestringfinal = "";
                                /*for (int b = 0; b < somesccsscreencapturemaindata.screencapturearrayofbytes.Length;b++)
                                {
                                    //https://stackoverflow.com/questions/11654562/how-to-convert-byte-array-to-string
                                    var somemessagestring = System.Text.Encoding.Default.GetString(somesccsscreencapturemaindata.screencapturearrayofbytes[b]);
                                    somemessagestringfinal = somemessagestring;
                                }*/

                                /*for (int b = 0; b < somesccsscreencapturemaindata.bitmapByteArray.Length; b++)
                                {
                                    //https://stackoverflow.com/questions/11654562/how-to-convert-byte-array-to-string
                                    var somemessagestring = System.Text.Encoding.ASCII.GetString(somesccsscreencapturemaindata.bitmapByteArray);
                                    somemessagestringfinal += somemessagestring;
                                }

                                str = new string(somemessagestringfinal);// str.Reverse().ToArray());  // Just for fun
                                var buf = Encoding.ASCII.GetBytes(str);     // Get ASCII byte array     
                                bw.Write((uint)buf.Length);                // Write string length
                                bw.Write(buf);
                                */

                                //TEST# SENDING A STANDARD BYTE ARRAY AND ITS WORKING PERIODICALLY.
                                byte[] somebytearray = new byte[8294400]; //82944 //8294400

                                for (int i = 0; i < somebytearray.Length; i++)
                                {
                                    somebytearray[i] = 0;
                                }

                                var somebuff = System.Text.Encoding.ASCII.GetString(somebytearray);


                                for (int b = 0; b < somesccsscreencapturemaindata.bitmapByteArray.Length; b++)
                                {
                                    //https://stackoverflow.com/questions/11654562/how-to-convert-byte-array-to-string
                                    //var somemessagestring = System.Text.Encoding.ASCII.GetString(somesccsscreencapturemaindata.bitmapByteArray);
                                    //somemessagestringfinal += somemessagestring;

                                }

                                Console.SetCursorPosition(0, 7);
                                Console.WriteLine(somesccsscreencapturemaindata.bitmapByteArray.Length);

                                var buf = Encoding.ASCII.GetBytes(somebuff);// Get ASCII byte array     
                                bw.Write((uint)buf.Length);// Write string length
                                bw.Write(buf);
                                //TEST# SENDING A STANDARD BYTE ARRAY AND ITS WORKING PERIODICALLY.

                                /*for (int b = 0; b < somesccsscreencapturemaindata.screencapturearrayofbytes.Length; b++)
                                {
                                    for (int y = 0; y < somesccsscreencapturemaindata.screencapturearrayofbytes[b].Length; y++)
                                    {
                                        //https://stackoverflow.com/questions/11654562/how-to-convert-byte-array-to-string
                                        var somemessagestring = System.Text.Encoding.ASCII.GetString(somesccsscreencapturemaindata.screencapturearrayofbytes[b]);
                                        somemessagestringfinal += somemessagestring;
                                    }
                                }

                                Console.SetCursorPosition(0, 7);
                                Console.WriteLine(somemessagestringfinal);


                                var buf = Encoding.ASCII.GetBytes(somemessagestringfinal);// Get ASCII byte array     
                                bw.Write((uint)buf.Length);// Write string length
                                bw.Write(buf);*/




                                //Console.SetCursorPosition(0, 2);
                                //Console.WriteLine("Wrote: \"{0}\"", str);
                                //Console.WriteLine("Wrote: \"{0}\"", str);
                            }
                            catch (EndOfStreamException)
                            {
                                //break;// When client disconnects
                            }



                            /*try
                            {
                                if (screencaptureimagecounter >= screencaptureimagecountermax)
                                {
                                    screencaptureimagecounter = 0;
                                }
                                sccssharpdxscreenframe someframecapture = (sccssharpdxscreenframe)mainreceivedmessages[0].someData[1];


                                //Console.SetCursorPosition(0, 6);
                                //Console.WriteLine("framecapturecounter:" + someframecapture.framecapturecounter);

                                //https://github.com/pythonnet/pythonnet/issues/1150
                                //PyObject builtins = Py.Import("builtins");
                                //PyObject bytes = builtins.GetAttr("bytes");
                                //bytes.Invoke(yourByteArray.ToPython());
                                //var bytes = new byte[] { 0x80, 0x03, (byte)'}', (byte)'q' };
                                //var bytes_base64 = System.Convert.ToBase64String(bytes);


                                //dynamic base64 = Py.Import("base64");
                                //var bytes_object = base64.b64decode(bytes_base64);
                                //Console.WriteLine(bytes_object.ToString());




















                                var len = (int)br.ReadUInt32();            // Read string length
                                var str = new string(br.ReadChars(len));    // Read string

                                initconsolecursorx = 0;
                                initconsolecursory = 1;















                                /*var somebuff = System.Text.Encoding.ASCII.GetString(somesccsscreencapturemaindata.bitmapByteArray);
                                var buf = Encoding.ASCII.GetBytes(somebuff);// Get ASCII byte array     
                                bw.Write((uint)somesccsscreencapturemaindata.bitmapByteArray.Length);// Write string length
                                bw.Write(buf);*/



                            /*bw.Write((uint)somesccsscreencapturemaindata.bitmapByteArray.Length);// Write string length
                            bw.Write(somesccsscreencapturemaindata.bitmapByteArray);*/








                            //Console.SetCursorPosition(0, 1);
                            //Console.WriteLine("Read: \"{0}\"", str);
                            //Console.WriteLine("Read: \"{0}\"", str);

                            //TEST# CREATING A STRING OUT OF THE SCREEN CAPTURE BYTE ARRAY AND ENCODING IT TO BYTES FOR SENDING TO THE BUFFER
                            /*string somemessagestringfinal = "";
                            for (int b = 0; b < somesccsscreencapturemaindata.bitmapByteArray.Length; b++)
                            {
                                somemessagestringfinal += somesccsscreencapturemaindata.bitmapByteArray[b];
                            }
                            var buf = Encoding.ASCII.GetBytes(somemessagestringfinal);// Get ASCII byte array     
                            bw.Write((uint)buf.Length);// Write string length
                            bw.Write(buf);*/
                            //TEST# CREATING A STRING OUT OF THE SCREEN CAPTURE BYTE ARRAY AND ENCODING IT TO BYTES FOR SENDING TO THE BUFFER











                            //Console.WriteLine(somemessagestringfinal);
                            //somebytearray[0] = 0;
                            //var somebuff = System.Text.Encoding.ASCII.GetBytes(somemessagestringfinal);// System.Text.Encoding.ASCII.GetString(somesccsscreencapturemaindata.bitmapByteArray);









                            /*string[] somemessagestringfinalarray = new string[somesccsscreencapturemaindata.screencapturearrayofbytes.Length];


                            string somemessagestringfinal = new string(str.Reverse().ToArray()); //

                            //string somemessagestringfinal = ""; //new string(str.Reverse().ToArray()); //
                            //send to python somesccsscreencapturemaindata.screencapturearrayofbytes

                            for (int b = 0; b < somesccsscreencapturemaindata.screencapturearrayofbytes.Length; b++)
                            {
                                for (int y = 0; y < somesccsscreencapturemaindata.screencapturearrayofbytes[b].Length; y++)
                                {
                                    var somemessagestring = System.Text.Encoding.Default.GetString(somesccsscreencapturemaindata.screencapturearrayofbytes[b]);
                                    somemessagestringfinalarray[b] = somemessagestring;
                                }
                                //var buf = Encoding.ASCII.GetBytes(somemessagestringfinalarray[b]);     // Get ASCII byte array     
                                //bw.Write((uint)buf.Length);                // Write string length
                                //bw.Write(buf);

                                /*var somebuff = System.Text.Encoding.ASCII.GetString(somesccsscreencapturemaindata.screencapturearrayofbytes[b]);

                                var buf = Encoding.ASCII.GetBytes(somebuff);// Get ASCII byte array     
                                bw.Write((uint)buf.Length);// Write string length
                                bw.Write(buf);


                            }

                            //Console.SetCursorPosition(0, 2);
                            //Console.WriteLine("Wrote: \"{0}\"", somemessagestringfinalarray);
                            //Console.SetCursorPosition(0, 2);
                            //Console.WriteLine(somesccsscreencapturemaindata.screencapturearrayofbytes[0].Length);





                            //TEST# SENDING A STANDARD BYTE ARRAY AND ITS WORKING PERIODICALLY.
                            byte[] somebytearray = new byte[1]; //82944

                            for (int i = 0;i < somebytearray.Length;i++)
                            {
                                somebytearray[i] = 0;
                            }

                            var somebuff = System.Text.Encoding.ASCII.GetString(somebytearray);

                            var buf = Encoding.ASCII.GetBytes(somebuff);// Get ASCII byte array     
                            bw.Write((uint)buf.Length);// Write string length
                            bw.Write(buf);
                            //TEST# SENDING A STANDARD BYTE ARRAY AND ITS WORKING PERIODICALLY.




                            /*

                            for (int b = 0; b < somesccsscreencapturemaindata.screencapturearrayofbytes.Length;b++)
                            {
                                //https://stackoverflow.com/questions/11654562/how-to-convert-byte-array-to-string
                                var somemessagestring = System.Text.Encoding.Default.GetString(somesccsscreencapturemaindata.screencapturearrayofbytes[b]);
                                somemessagestringfinal += somemessagestring;
                            }


                            //someframecapture.screencapturearrayofbytes;//
                            //str = new string(str.Reverse().ToArray());  // Just for fun

                            //Console.SetCursorPosition(0, 2);
                            //Console.WriteLine("Wrote: \"{0}\"", somemessagestringfinal);




                            screencaptureimagecounter++;
                            //Console.WriteLine("Wrote: \"{0}\"", str);
                        }
                        catch (EndOfStreamException)
                        {
                            //break;// When client disconnects
                        }*/











                            Thread.Sleep(1);
                            goto threadloopconsole;

                            //////frame from python out <=
                        }, mainreceivedmessages);

                        pythontoprogramswtc = 1;
                    }
                }
            }



            /*if (framecounterforpctoarduinoscreenswtc == 0)
            {
                arduinotickswatch.Restart();
                framecounterforpctoarduinoscreenswtc = 1;
            }*/




            /*
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

                        /*if (somesccsscreencapturemaindata.somebitmapforarduino != null)
                        {
                            Console.WriteLine("the somesccsscreencapturemaindata.somebitmapforarduino is != null");
                        }

                        if (serialPort != null && somesccsscreencapturemaindata.somebitmapforarduino != null)
                        {
                            //Console.WriteLine("test");
                            //int memoryBitmapStride = 4;// somesccsscreencapturemaindata.somebitmapforarduino.Width * 4;

                            /*int columns = somesccsscreencapturemaindata.somebitmapforarduino.Width;
                            int rows = somesccsscreencapturemaindata.somebitmapforarduino.Height;

                            System.Drawing.Rectangle rect = new System.Drawing.Rectangle(0, 0, somesccsscreencapturemaindata.somebitmapforarduino.Width, somesccsscreencapturemaindata.somebitmapforarduino.Height);
                            var somebitmapdata = somesccsscreencapturemaindata.somebitmapforarduino.LockBits(rect, System.Drawing.Imaging.ImageLockMode.ReadWrite, somesccsscreencapturemaindata.somebitmapforarduino.PixelFormat);
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
                            }
                            /*
                            for (int y = 0; y < somesccsscreencapturemaindata.somebitmapforarduino.Height; y++)
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

                            somesccsscreencapturemaindata.somebitmapforarduino.UnlockBits(somebitmapdata);
                        }
                        framecounterforpctoarduinoscreen = 0;
                    }
                }
                framecounterforpctoarduinoscreen++;
            }*/









            return mainreceivedmessages;
        }





        //https://gist.github.com/JonathonReinhart/bbfa618f9ad19e2ca48d5fd10914b069
        static void runserver()
        {




            string somemessage = "python pipe Pc to Pi";
            initconsolecursorx = 0; //(Console.WindowWidth / 2) - (somemessage.Length / 2)
            initconsolecursory = 0; //(Console.WindowHeight / 2) + 

            sccsconsolewritermsg[7].message = somemessage;
            sccsconsolewritermsg[7].originalMsg = somemessage;
            sccsconsolewritermsg[7].messageCut = somemessage;
            sccsconsolewritermsg[7].specialMessage = 2;
            sccsconsolewritermsg[7].specialMessageLineX = 0;
            sccsconsolewritermsg[7].specialMessageLineY = 0;
            sccsconsolewritermsg[7].orilineX = initconsolecursorx;
            sccsconsolewritermsg[7].orilineY = initconsolecursory;
            sccsconsolewritermsg[7].lineX = initconsolecursorx;
            sccsconsolewritermsg[7].lineY = initconsolecursory;
            sccsconsolewritermsg[7].count = 0;
            sccsconsolewritermsg[7].swtch0 = 1;
            sccsconsolewritermsg[7].swtch1 = 1;
            sccsconsolewritermsg[7].delay = 150;
            sccsconsolewritermsg[7].looping = 1;






            // Open the named pipe.
            var server = new NamedPipeServerStream("NPtest");
            //Console.WriteLine("Waiting for connection...");
            server.WaitForConnection();

            var br = new BinaryReader(server);
            var bw = new BinaryWriter(server);

            while (true)
            {
                try
                {
                    var len = (int)br.ReadUInt32();            // Read string length
                    var str = new string(br.ReadChars(len));    // Read string


                    initconsolecursorx = 0;
                    initconsolecursory = 1;

                    Console.SetCursorPosition(0, 1);
                    Console.WriteLine("Read: \"{0}\"", str);
                    //Console.WriteLine("Read: \"{0}\"", str);






                    str = new string(str.Reverse().ToArray());  // Just for fun

                    var buf = Encoding.ASCII.GetBytes(str);     // Get ASCII byte array     
                    bw.Write((uint)buf.Length);                // Write string length
                    bw.Write(buf);


                    Console.SetCursorPosition(0, 2);
                    Console.WriteLine("Wrote: \"{0}\"", str);
                    //Console.WriteLine("Wrote: \"{0}\"", str);



                    Thread.Sleep(0);
                }
                catch (EndOfStreamException)
                {
                    break;// When client disconnects
                }
            }

            Console.WriteLine("Client disconnected.");
            server.Close();
            server.Dispose();
        }



        /*
        public class workerThreads
        {
            public int mainFrameCounterThreadOne = 0;
            public int availableThreads = 0;
            public ThreadStart threadStart;
            public Thread[] listOfThreads;
            public int threadID;
            public Thread thread;


            public workerThreads(int threadID_)
            {

                this.threadID = threadID_;

                threadStart = new ThreadStart(() =>
                {
                    threadOneMainDispatcherUpdate(threadID);
                });

                thread = new Thread(threadStart);
                thread.IsBackground = true;
                thread.SetApartmentState(ApartmentState.STA);
                thread.Start();
            }

            static int someupdateframecounter = 0;
            static int someupdateframecountermax = 100;
            public static int whosFirst = 0;
            public static int threadCreationCounter = 0;
            public static bool canLoadStarterItems = true;
            //[STAThread]
            void threadOneMainDispatcherUpdate(int threadIndex_)
            {
                int threadIndex = threadIndex_;
                int workerFrame = 0;

                try
                {
                //SC_Console.consoleMessageQueue messageQueue00 = new SC_Console.consoleMessageQueue("SC_AvailableThreads#" + _totalThreads, 0, 0);
                //SC_Console.consoleMessageQueue messageQueue0 = new SC_Console.consoleMessageQueue("#" + _threadIndex + "", 0, 3 + _threadIndex);

                threadloop:
                    //List of functions that the multithreaded app will start every frames.
                    ////////////////////
                    //_threadUpdateTest();
                    ////////////////////
                    if (canLoadStarterItems)
                    {
                        //Console.WriteLine("grammar load");
                        //threadOneGrammarLoad();
                        canLoadStarterItems = false;
                    }

                    if (someupdateframecounter >= someupdateframecountermax)
                    {
                        //Console.WriteLine("main thread root alive.");
                        someupdateframecounter = 0;
                    }
                    someupdateframecounter += 1;
                    //if (workerFrame > -1)
                    //{
                    //    threadCreationCounter++;
                    //    workerFrame = 0;
                    //}

                    workerFrame++;
                    Thread.Sleep(1);

                    goto threadloop;
                }
                //#pragma warning disable CS0168 // The variable 'ex' is declared but never used
                catch (Exception ex)
                //#pragma warning restore CS0168 // The variable 'ex' is declared but never used
                {
                    //SC_Console.consoleMessageQueue messageQueue0 = new SC_Console.consoleMessageQueue(ex.ToString(), 0, 20);
                }
            }
        }*/



        static int someupdateframecounter = 0;
        static int someupdateframecountermax = 100;
        static int someupdateframecounterswtc = 0;


        public static void someupdate()
        {



            if (screencaptureenabled == 1)
            {
                //Console.WriteLine("initializing screen capture");
                /*if (somesccsscreencapturemain != null && initscreencaptureclass == 0) //sccssharpdxdirectx.D3D != null && initscreencaptureclass == 0 && 
                {

                    //somesccsscreencapturemain = new sccsscreencapture(0, 0, screencapturedevice);
                    Console.SetCursorPosition(0, 5);
                    Console.WriteLine("initialized screen capture");
                    initscreencaptureclass = 1;
                }*/

                if (initscreencaptureclass == 1) //1
                {
                    for (int i = 0; i < 3; i++)
                    {
                        try
                        {
                            somesccsscreencapturemaindata = somesccsscreencapturemain.ScreenCapture(-1);

                            //somesccsscreencapturemaindata = somesccsscreencapturemain.ScreenCapture(-1);
                            /*somereleaseframeswtc = somesccsscreencapturemain.releaseFrame();

                            if (!somereleaseframeswtc)
                            {
                                somesccsscreencapturemain = null;
                                somesccsscreencapturemain = new sccsscreencapture();
                            }*/
                            break;
                        }
                        catch (Exception ex)
                        {
                            /*if (somesccsscreencapturemain!= null)
                            {
                                somesccsscreencapturemain = null;

                                //somesccsscreencapturemain.Dispose();

                                //somereleaseframeswtc = somesccsscreencapturemain.releaseFrame();
                            }
                            else
                            {
                                //somesccsscreencapturemain.Dispose();
                            }*/
                            somesccsscreencapturemain.Dispose();
                            somesccsscreencapturemain = new sccsscreencapture(0, 0, screencapturedevice);
                            /*if (!somereleaseframeswtc)
                            {
                                somesccsscreencapturemain = null;
                            }
                            somesccsscreencapturemain = new sccsscreencapture();*/


                            //somesccsscreencapturemain.Dispose();
                            //somesccsscreencapturemain = new sccsscreencapture();

                            //Console.WriteLine(ex.ToString());

                            MessageBox((IntPtr)0, ex.ToString(), "sccoresystems", 0);
                        }
                    }

                    //somesccsscreencapturedata




                    if (pythontoprogramswtc == -9)
                    {
                        Task pythontoprogramtask = Task<object[]>.Factory.StartNew((tester0001) =>
                        {
                            // Open the named pipe.
                            var server = new NamedPipeServerStream("NPtest");
                            //Console.WriteLine("Waiting for connection... task c# to python.");

                            string somemessage = "python pipe Pc to Pi. Waiting for connection...";
                            initconsolecursorx = 0; //(Console.WindowWidth / 2) - (somemessage.Length / 2)
                            initconsolecursory = 0; //(Console.WindowHeight / 2) + 

                            sccsconsolewritermsg[7].message = somemessage;
                            sccsconsolewritermsg[7].originalMsg = somemessage;
                            sccsconsolewritermsg[7].messageCut = somemessage;
                            sccsconsolewritermsg[7].specialMessage = 2;
                            sccsconsolewritermsg[7].specialMessageLineX = 0;
                            sccsconsolewritermsg[7].specialMessageLineY = 0;
                            sccsconsolewritermsg[7].orilineX = initconsolecursorx;
                            sccsconsolewritermsg[7].orilineY = initconsolecursory;
                            sccsconsolewritermsg[7].lineX = initconsolecursorx;
                            sccsconsolewritermsg[7].lineY = initconsolecursory;
                            sccsconsolewritermsg[7].count = 0;
                            sccsconsolewritermsg[7].swtch0 = 1;
                            sccsconsolewritermsg[7].swtch1 = 1;
                            sccsconsolewritermsg[7].delay = 5;
                            sccsconsolewritermsg[7].looping = 1;



                            server.WaitForConnection();

                            var br = new BinaryReader(server);
                            var bw = new BinaryWriter(server);

                            int frameoffsetcounterswtc = 0;
                            int frameoffsetcounter = 0;
                            int frameoffsetcountermax = 1;

                        //somesccsscreencapturedata

                        //////frame to python in =>
                        threadloopconsole:

                            try
                            {


                                var len = (int)br.ReadUInt32();            // Read string length
                                var str = new string(br.ReadChars(len));    // Read string

                                initconsolecursorx = 0;
                                initconsolecursory = 1;

                                Console.SetCursorPosition(0, 1);
                                //Console.WriteLine("Read: \"{0}\"", str);
                                //Console.WriteLine("Read: \"{0}\"", str);



                                string somemessagestringfinal = ""; //new string(str.Reverse().ToArray()); //
                                                                    //send to python somesccsscreencapturemaindata.screencapturearrayofbytes

                                /*

                                for (int b = 0; b < somesccsscreencapturemaindata.screencapturearrayofbytes.Length;b++)
                                {
                                    //https://stackoverflow.com/questions/11654562/how-to-convert-byte-array-to-string
                                    var somemessagestring = System.Text.Encoding.Default.GetString(somesccsscreencapturemaindata.screencapturearrayofbytes[b]);
                                    somemessagestringfinal += somemessagestring;
                                }*/



                                //str = new string(str.Reverse().ToArray());  // Just for fun
                                var buf = Encoding.ASCII.GetBytes(somemessagestringfinal);// Get ASCII byte array     
                                bw.Write((uint)buf.Length);// Write string length
                                bw.Write(buf);

                                //Console.SetCursorPosition(0, 2);
                                //Console.WriteLine("Wrote: \"{0}\"", somemessagestringfinal);
                                //Console.WriteLine("Wrote: \"{0}\"", str);
                            }
                            catch (EndOfStreamException)
                            {
                                //break;// When client disconnects
                            }

                            Thread.Sleep(1);
                            goto threadloopconsole;

                            //////frame from python out <=
                        }, mainreceivedmessages);

                        pythontoprogramswtc = 1;
                    }
                }
            }











            //mainthreadloop:



            /*if (someupdateframecounter >= someupdateframecountermax && someupdateframecounterswtc == 0)
            {
                if (restartserverswtc == 0)
                {
                    //runserver();
                    restartserverswtc = 1;
                }


                someupdateframecounterswtc = 1;

                //Console.WriteLine("main root alive.");
                someupdateframecounter = 0;
            }
            someupdateframecounter+=1;*/


            //Thread.Sleep(0);
            //goto mainthreadloop;
        }






        /*
        [DllImport("kernel32", SetLastError = true, CharSet = CharSet.Ansi)]
        static extern IntPtr LoadLibrary([MarshalAs(UnmanagedType.LPStr)]string lpFileName);

        public static IntPtr LoadWin32Library(string libPath)
        {
            if (String.IsNullOrEmpty(libPath))
                throw new ArgumentNullException("libPath");
            if (Environment.Is64BitProcess)
                throw new Exception(String.Format("Can't load {0} because this is a 64 bit proccess", libPath));



            libPath = "/home/pi/Desktop/sccoresystems/PInvoke.Kernel32.dll";


            IntPtr moduleHandle = LoadLibrary(libPath);
            if (moduleHandle == IntPtr.Zero)
            {
                var lasterror = Marshal.GetLastWin32Error();
                var innerEx = new Win32Exception(lasterror);
                innerEx.Data.Add("LastWin32Error", lasterror);

                throw new Exception("can't load DLL " + libPath, innerEx);
            }
            return moduleHandle;
        }*/




        [DllImport("user32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        public static extern int MessageBox(IntPtr hWnd, string lpText, string lpCaption, uint uType);
        [System.Runtime.InteropServices.DllImport("gdi32.dll")]
        public static extern bool DeleteObject(IntPtr hObject);

        //[DllImport("kernel32.dll")]
        //private static extern uint GetCurrentThreadId();
        //[DllImport(@"PInvoke.Kernel32.dll", ExactSpelling = true)]
        //[DllImport("kernel32.dll", ExactSpelling = true)]
        //private static extern IntPtr GetConsoleWindow();       

        //[DllImport(@"PInvoke.Kernel32.dll", SetLastError = true)]   
        //[DllImport(@"kernel32.dll", SetLastError = true)]
        //static extern bool AllocConsole();
        //[DllImport(@"user32.dll")]
        //static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        /*[DllImport("user32.dll")]
        public static extern long GetWindowRect(IntPtr hWnd, ref System.Drawing.Rectangle lpRect);

        [StructLayout(LayoutKind.Sequential)]
        public struct RECT
        {
            public int Left;        // x position of upper-left corner
            public int Top;         // y position of upper-left corner
            public int Right;       // x position of lower-right corner
            public int Bottom;      // y position of lower-right corner
        }

        Rectangle myRect = new Rectangle();


        IntPtr MSEdgeHandle;

        [DllImport("user32.dll")]
        private static extern bool EnumWindows(EnumWindowsProc enumProc, IntPtr lParam);

        [DllImport("user32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        private static extern int GetWindowText(IntPtr hWnd, StringBuilder lpString, int nMaxCount);

        [DllImport("user32.dll", CharSet = CharSet.Unicode)]
        private static extern int GetWindowTextLength(IntPtr hWnd);

        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        private static extern int GetClassName(IntPtr hWnd, StringBuilder lpClassName, int nMaxCount);

        [DllImport("user32.dll", ExactSpelling = true, CharSet = CharSet.Auto)]
        private static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        public delegate bool EnumWindowsProc(IntPtr hWnd, IntPtr lParam);


        public const int SW_HIDE = 0;
        public const int SW_SHOWNORMAL = 1;
        public const int SW_SHOWMINIMIZED = 2;
        public const int SW_SHOWMAXIMIZED = 3;


        //https://stackoverflow.com/questions/61451756/how-to-check-if-a-user-has-a-browser-open-in-c-sharp user Metzgermeister
        internal class BrowserDetector
        {
            private readonly Dictionary<string, string> browsers = new Dictionary<string, string>
            {
                {
                    "firefox", "Mozilla Firefox"
                },
                {
                    "chrome", "Google Chrome"
                },
                {
                    "iexplore", "Internet Explorer"
                },
                {
                    "MicrosoftEdgeCP", "Microsoft Edge"
                }
                ,
                {
                    "msedge", "Microsoft Edge"
                }
                 ,
                {
                    "MicrosoftEdge", "Microsoft Edge"
                }
                



                // add other browsers
            };

            public bool BrowserIsOpen()
            {
                return Process.GetProcesses().Any(this.IsBrowserWithWindow);
            }

            private bool IsBrowserWithWindow(Process process)
            {
                return this.browsers.TryGetValue(process.ProcessName, out var browserTitle) && process.MainWindowTitle.Contains(browserTitle);
            }
        }


        //[DllImport("user32.dll")]
        //public static extern bool GetWindowRect(IntPtr hwnd, ref Rect rectangle);
        //Rect r = new Rect();
        //GetWindowRect(hwnd, ref r);

        [DllImport("user32.dll", EntryPoint = "FindWindow", SetLastError = true)]
        static extern IntPtr FindWindowByCaption(IntPtr ZeroOnly, string lpWindowName);

        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        public static extern IntPtr GetForegroundWindow();

        //[DllImport("user32.dll")]
        //private static extern bool GetWindowRect(IntPtr hWnd, Rectangle rect);

        public const int KEY_W = 0x57;
        public const int KEY_A = 0x41;
        public const int KEY_S = 0x53;
        public const int KEY_D = 0x44;
        public const int KEY_SPACE = 0x20; //0x39
        public const int KEY_E = 0x45;
        public const int KEY_Q = 0x51;

        public const int KEYEVENTF_KEYUP = 0x0002;
        public const int KEYEVENTF_EXTENDEDKEY = 0x0001;

        const uint MOUSEEVENTF_ABSOLUTE = 0x8000;
        const uint MOUSEEVENTF_LEFTDOWN = 0x0002;
        const uint MOUSEEVENTF_LEFTUP = 0x0004;
        const uint MOUSEEVENTF_MIDDLEDOWN = 0x0020;
        const uint MOUSEEVENTF_MIDDLEUP = 0x0040;
        const uint MOUSEEVENTF_MOVE = 0x0001;
        const uint MOUSEEVENTF_RIGHTDOWN = 0x0008;
        const uint MOUSEEVENTF_RIGHTUP = 0x0010;
        const uint MOUSEEVENTF_XDOWN = 0x0080;
        const uint MOUSEEVENTF_XUP = 0x0100;
        const uint MOUSEEVENTF_WHEEL = 0x0800;
        const uint MOUSEEVENTF_HWHEEL = 0x01000;
        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall, EntryPoint = "mouse_event")]
        public static extern void mouse_event(uint dwFlags, uint dx, uint dy, long dwData, uint dwExtraInfo);
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        static extern IntPtr SendMessage(IntPtr hWnd,
                                 UInt32 Msg,
                                 IntPtr wParam,
                                 IntPtr lParam);
        [DllImport("User32.dll")]
        private static extern bool SetCursorPos(int X, int Y);


        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern bool Wow64DisableWow64FsRedirection(ref IntPtr ptr);
        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern bool Wow64RevertWow64FsRedirection(IntPtr ptr);
        private const UInt32 WM_SYSCOMMAND = 0x112;
        private const UInt32 SC_RESTORE = 0xf120;

        private const string OnScreenKeyboardExe = "osk.exe";

        private void ShowKeyboard()
        {
            var path64 = @"c:\windows\sysnative\osk.exe"; //@"C:\Windows\winsxs\amd64_microsoft-windows-osk_31bf3856ad364e35_6.1.7600.16385_none_06b1c513739fb828\osk.exe";
            var path32 = @"c:\windows\sysnative\osk.exe";// @"C:\windows\system32\osk.exe"; 
            var path = (Environment.Is64BitOperatingSystem) ? path64 : path32;
            Process.Start(path);
        }
        void StartOsk()
        {
            IntPtr ptr = new IntPtr(); ;
            bool sucessfullyDisabledWow64Redirect = false;

            // Disable x64 directory virtualization if we're on x64,
            // otherwise keyboard launch will fail.
            if (System.Environment.Is64BitOperatingSystem)
            {
                sucessfullyDisabledWow64Redirect = Wow64DisableWow64FsRedirection(ref ptr);
            }

            ProcessStartInfo psi = new ProcessStartInfo();
            psi.FileName = OnScreenKeyboardExe;
            // We must use ShellExecute to start osk from the current thread
            // with psi.UseShellExecute = false the CreateProcessWithLogon API 
            // would be used which handles process creation on a separate thread 
            // where the above call to Wow64DisableWow64FsRedirection would not 
            // have any effect.

            psi.UseShellExecute = true;
            psi.Verb = "runas";

            Process.Start(psi);

            // Re-enable directory virtualisation if it was disabled.
            if (System.Environment.Is64BitOperatingSystem)
                if (sucessfullyDisabledWow64Redirect)
                    Wow64RevertWow64FsRedirection(ptr);
        }*/
    }
}



//static PInvoke.Kernel32 somepinvokekernel32;// = new PInvoke();
//private static IntPtr DllImportResolver(string libraryName, Assembly assembly, DllImportSearchPath? searchPath)
//{
//    if (libraryName == "nativedep")
//    {
//        // On systems with AVX2 support, load a different library.
//        if (System.Runtime.Intrinsics.X86.Avx2.IsSupported)
//        {
//            return NativeLibrary.Load("nativedep_avx2", assembly, searchPath); // nativedep_avx2
//        }
//    }
//
//   // Otherwise, fallback to default import resolver.
//    return IntPtr.Zero;
//}












/*NamedPipeServerStream server = new NamedPipeServerStream("Demo");
           server.WaitForConnection();

           MemoryStream stream = new MemoryStream();
           using (BinaryWriter writer = new BinaryWriter(stream))
           {
               writer.Write("print \"hello\"");
               server.Write(stream.ToArray(), 0, stream.ToArray().Length);
           }

           stream.Close();
           server.Disconnect();
           server.Close();*/
