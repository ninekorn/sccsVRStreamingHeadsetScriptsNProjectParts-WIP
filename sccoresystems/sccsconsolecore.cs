using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SharpDX.Direct3D11;
using System.Runtime.InteropServices;
using System.Threading;

using System.ComponentModel;

namespace sccoresystems.sccsconsole
{
    
    public class sccsconsolecore : sccsicomponent
    {

        

        public static sccsicomponent SCCSICOMPONENT;
        sccsglobals sccsicomponent.SCCSGlobals
        {
            get => SCCSGLOB;
        }
        public static sccsglobals SCCSGLOB;
        public int consolehasinit = 1;
        public static IntPtr handle = IntPtr.Zero;
        const int SwHide = 0;
        const int SwShow = 5;

        public uint _originalConsoleModeWithMouseInput;
        public uint _originalConsoleModeWithoutMouseInput;
        public uint _modifiedConsoleMode;

        const uint ENABLE_QUICK_EDIT = 0x0040;
        const int STD_INPUT_HANDLE = -10;
   

        public sccsconsolecore(sccoresystems.sccsmessageobject[] tester)
        {
            //IS CALLED FROM PROGRAM.CS ALREADY.
            //createConsole(tester);
            //IS CALLED FROM PROGRAM.CS ALREADY.

            //Console.WriteLine("sccsconsolecore class created!");
        }
        public void createConsole(sccoresystems.sccsmessageobject[] tester)
        {
            handle = GetConsoleWindow();

            if (handle == IntPtr.Zero)
            {
                AllocConsole();
                //consoleMessageQueue _consoleMessageQueue = new consoleMessageQueue("IntPtr.Zero", 0, 0);
            }
            else
            {
                ShowWindow(handle, SwShow);
                //consoleMessageQueue _consoleMessageQueue = new consoleMessageQueue("!IntPtr.Zero", 0, 0);
            }



            //Console.WriteLine("handle:" + handle );




            IntPtr consoleHandle = GetStdHandle(STD_INPUT_HANDLE);

            if (!GetConsoleMode(consoleHandle, out _originalConsoleModeWithMouseInput))
            {
                // ERROR: Unable to get console mode.
                //return false;
                //consoleMessageQueue _consoleMessageQueue = new consoleMessageQueue("null GetConsoleMode", 0, 0);
            }
            else
            {
                //consoleMessageQueue _consoleMessageQueue = new consoleMessageQueue("not null GetConsoleMode", 0, 0);
            }      

            _modifiedConsoleMode = _originalConsoleModeWithMouseInput;

            _modifiedConsoleMode &= ~ENABLE_QUICK_EDIT;

            _originalConsoleModeWithoutMouseInput = _modifiedConsoleMode;

            // set the new mode
            if (!SetConsoleMode(consoleHandle, _modifiedConsoleMode))
            {
                // ERROR: Unable to set console mode
                //consoleMessageQueue _consoleMessageQueue = new consoleMessageQueue("null SetConsoleMode", 0, 0);
            }
            else
            {
                //consoleMessageQueue _consoleMessageQueue = new consoleMessageQueue("not null SetConsoleMode", 0, 0);
            }
            //Console.SetBufferSize(Console.BufferWidth, Console.BufferHeight);

            IntPtr sysMenu = GetSystemMenu(handle, false);

            if (handle != IntPtr.Zero)
            {
                //DeleteMenu(sysMenu, SC_CLOSE, MF_BYCOMMAND);
                //DeleteMenu(sysMenu, SC_MINIMIZE, MF_BYCOMMAND);
                //DeleteMenu(sysMenu, SC_MAXIMIZE, MF_BYCOMMAND);
                //DeleteMenu(sysMenu, SC_SIZE, MF_BYCOMMAND);
            }

            /*Console.OutputEncoding = System.Text.Encoding.GetEncoding(28591); //System.Text.Encoding.UTF8;//  65001
            Console.SetCursorPosition(0, 0);

            int origWidth = Console.WindowWidth;
            int origHeight = Console.WindowHeight;
            int width = origWidth;
            int height = origHeight;
            Console.SetWindowSize(width, height);
            Console.SetBufferSize(width, height);

            ShowScrollBar(handle, (int)ScrollBarDirection.SB_VERT, false);
            ShowScrollBar(handle, (int)ScrollBarDirection.SB_HORZ, false);
            ShowScrollBar(handle, (int)ScrollBarDirection.SB_BOTH, false);
  
            _console_hasINIT = 1;*/

        }


        [DllImport("kernel32.dll", ExactSpelling = true)]
        private static extern IntPtr GetConsoleWindow();          
        [DllImport(@"kernel32.dll", SetLastError = true)]
        static extern bool AllocConsole();
        [DllImport(@"user32.dll")]
        static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);
        [DllImport("kernel32.dll")]
        static extern bool GetConsoleMode(IntPtr hConsoleHandle, out uint lpMode);
        [DllImport("kernel32.dll", SetLastError = true)]
        static extern IntPtr GetStdHandle(int nStdHandle);
        [DllImport("kernel32.dll")]
        static extern bool SetConsoleMode(IntPtr hConsoleHandle, uint dwMode);
        [DllImport("user32.dll")]
        private static extern IntPtr GetSystemMenu(IntPtr hWnd, bool bRevert);
    }
}