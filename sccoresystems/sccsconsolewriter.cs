using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Win32.SafeHandles;
using System.IO;
using System.Runtime.InteropServices;

namespace sccoresystems.sccsconsole
{
    public struct messager
    {
        public messager[] messagerlist;
        public int specialMessage;
        public int specialMessageLineX;
        public int specialMessageLineY;
        public string message;
        public string messageCut;
        public string originalMsg;
        public int lineX;
        public int lineY;
        public int orilineX;
        public int orilineY;
        public int lastOrilineX;
        public int lastOrilineY;
        public int delay;
        public int swtch0;
        public int swtch1;
        public int count;
        public int looping;
    }



    public class sccsconsolewriter
    {
        [DllImport("User32.dll", CharSet = CharSet.Unicode)]
        public static extern int MessageBox(IntPtr h, string m, string c, int type);

        static messager[] messagetopass = new messager[Program.MaxSizeMessageObject];
        public static List<messager> messagetopasslist = new List<messager>();

        messager dummyMessage = new messager();

        int consolewidth = 0;
        int consoleheight = 0;

        static int[] maparraylast;
        static int[] maparray;
        static int[] maparraydirty;

        int originalwidth = 0;
        int originalheight = 0;

        string programname = "skYaRk";
        public static sccsconsolewriter CONSOLEWRITER;
        public List<object[]> TASK00WRQUEUE = new List<object[]>();
        public object ResultsOfTasks0;
        public int Task00initconsole = 1;
        public Task TASK00WR;
        public int consoleisalive00WR = 0;
        public int consoleERROR = -1;
        public int consolehasINIT = 0;
        public int xCurrentCursorPos;
        public int yCurrentCursorPos;

        string lastConsoleMessage = "";

        int mainMessageCursorPosSwitchCounter = 0;



        int currentWidthLast = 0;
        int currentHeightLast = 0;


        public sccsconsolewriter(sccoresystems.sccsmessageobject[] tester)// : base(tester)
        {
            consolewidth = Console.WindowWidth;
            consoleheight = Console.WindowHeight;
            originalwidth = consolewidth;
            originalheight = consoleheight;

            initX = (consolewidth / 2) - (programname.Length / 2);
            initY = (consoleheight / 2);

            maparray = new int[consolewidth * consoleheight];
            maparraylast = new int[consolewidth * consoleheight];
            maparraydirty = new int[consolewidth * consoleheight];

            //fastNoise = new FastNoise();

            for (int x = 0; x < consolewidth; x++)
            {
                for (int y = 0; y < consoleheight; y++)
                {
                    if (x == 0 && y > 0 && y < originalheight - 1)
                    {
                        try
                        {
                            //Draw(x, y, "│");
                            maparray[y * consolewidth + x] = 2;
                        }
                        catch (Exception ex)
                        {
                            Console.Write(ex.ToString());
                        }
                    }
                    else if (y == 0 && x > 0 && x < originalwidth - 1)
                    {
                        try
                        {
                            //Draw(x, y, "─");
                            maparray[y * consolewidth + x] = 2;
                        }
                        catch (Exception ex)
                        {
                            Console.Write(ex.ToString());
                        }
                    }
                    else if (x == originalwidth - 1 && y > 0 && y < originalheight - 1)
                    {
                        try
                        {
                            //Draw(x, y, "│");
                            maparray[y * consolewidth + x] = 2;
                        }
                        catch (Exception ex)
                        {
                            Console.Write(ex.ToString());
                        }
                    }
                    else if (y == originalheight - 1 && x > 0 && x < originalwidth - 1)
                    {
                        try
                        {
                            //Draw(x, y, "─");
                            maparray[y * consolewidth + x] = 2;
                        }
                        catch (Exception ex)
                        {
                            Console.Write(ex.ToString());
                        }
                    }
                    else if (y == 0 && x == 0)
                    {
                        try
                        {
                            //Draw(x, y, "┌");
                            maparray[y * consolewidth + x] = 2;
                        }
                        catch (Exception ex)
                        {
                            Console.Write(ex.ToString());
                        }
                    }
                    else if (y == originalheight - 1 && x == 0)
                    {
                        try
                        {
                            //Draw(x, y, "└");
                            maparray[y * consolewidth + x] = 2;
                        }
                        catch (Exception ex)
                        {
                            Console.Write(ex.ToString());
                        }
                    }
                    else if (y == 0 && x == originalwidth - 1)
                    {
                        try
                        {
                            //Draw(x, y, "┐");
                            maparray[y * consolewidth + x] = 2;
                        }
                        catch (Exception ex)
                        {
                            Console.Write(ex.ToString());
                        }
                    }
                    else if (y == originalheight - 1 && x == originalwidth - 1)
                    {
                        try
                        {
                            //Draw(x, y, "┘");
                            maparray[y * consolewidth + x] = 2;
                        }
                        catch (Exception ex)
                        {
                            Console.Write(ex.ToString());
                        }
                    }
                    else
                    {
                        try
                        {
                            //Draw(x, y, " ");
                            maparray[y * consolewidth + x] = 0;
                        }
                        catch (Exception ex)
                        {
                            Console.Write(ex.ToString());
                        }
                    }
                }
            }

            for (int i = 0; i < maparray.Length; i++)
            {
                maparraylast[i] = maparray[i];
                maparraydirty[i] = maparray[i];
            }

            currentWidthLast = consolewidth;
            currentHeightLast = consoleheight;

        }



        [DllImport("Kernel32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        static extern SafeFileHandle CreateFile(
        string fileName,
        [MarshalAs(UnmanagedType.U4)] uint fileAccess,
        [MarshalAs(UnmanagedType.U4)] uint fileShare,
        IntPtr securityAttributes,
        [MarshalAs(UnmanagedType.U4)] FileMode creationDisposition,
        [MarshalAs(UnmanagedType.U4)] int flags,
        IntPtr template);

        [StructLayout(LayoutKind.Sequential)]
        public struct Coord
        {
            public short X;
            public short Y;

            public Coord(short X, short Y)
            {
                this.X = X;
                this.Y = Y;
            }
        };

        [DllImport("kernel32.dll", SetLastError = true)]
        static extern bool WriteConsoleOutputCharacter(
            SafeFileHandle hConsoleOutput,
            string lpCharacter,
            int nLength,
            Coord dwWriteCoord,
            ref int lpumberOfCharsWritten);

        public void Draw(int x, int y, string renderingChar)
        {
            // The handle to the output buffer of the console
            SafeFileHandle consoleHandle = CreateFile("CONOUT$", 0x40000000, 2, IntPtr.Zero, FileMode.Open, 0, IntPtr.Zero);

            // Draw with this native method because this method does NOT move the cursor.
            int n = 0;
            WriteConsoleOutputCharacter(consoleHandle, renderingChar, 1, new Coord((short)x, (short)y), ref n);
        }


        int initX = 0;
        int initY = 0;
        string somechar;
        string cutmsg;
        int targetLineX;
        int targetLineY;

        int lastconsolewidth = 0;
        int lastconsoleheight = 0;
        int counterresetconsoleborders = 0;

        //int currentmessageposX = 0;

        public messager[] consolewriter(messager[] mainobject)  //object[]
        {
            //int currentWidth = Console.WindowWidth;
            //int currentHeight = Console.WindowHeight;

            xCurrentCursorPos = Console.CursorLeft;
            yCurrentCursorPos = Console.CursorTop;

            mainMessageCursorPosSwitchCounter = 0;





            if (Console.WindowWidth != lastconsolewidth || Console.WindowHeight != lastconsoleheight)
            {
                if (counterresetconsoleborders > 50)
                {
                    lastconsolewidth = Console.WindowWidth;
                    lastconsoleheight = Console.WindowHeight;
                    counterresetconsoleborders = 0;
                }
                counterresetconsoleborders++;
            }


            for (int i = 0; i < mainobject.Length; i++)
            {
                messagetopass[i] = (messager)mainobject[i];

                if (messagetopass[i].specialMessage == 0)
                {
                    if (messagetopass[i].swtch0 == 1)
                    {
                        if (messagetopass[i].count >= messagetopass[i].delay)
                        {
                            if (messagetopass[i].messageCut != "")
                            {
                                somechar = messagetopass[i].messageCut.Substring(0, 1);
                                cutmsg = messagetopass[i].messageCut.Substring(1, messagetopass[i].messageCut.Length - 1);
                                messagetopass[i].messageCut = cutmsg;
                                messagetopass[i].message = somechar;

                                targetLineX = (int)messagetopass[i].lineX;
                                targetLineY = (int)messagetopass[i].lineY;

                                Draw(targetLineX, targetLineY, somechar);
                                maparray[targetLineY * consolewidth + targetLineX] = 2;
                                maparraydirty[targetLineY * consolewidth + targetLineX] = messagetopass[i].delay * 10;

                                lastConsoleMessage = somechar.ToString();

                                messagetopass[i].count = 0;

                                targetLineX = (int)messagetopass[i].lineX + 1;
                                targetLineY = (int)messagetopass[i].lineY;
                                messagetopass[i].lineX = targetLineX;
                                messagetopass[i].lineY = targetLineY;
                            }
                            else
                            {
                                messagetopass[i].message = "";
                                messagetopass[i].originalMsg = "";
                                messagetopass[i].messageCut = "";
                                messagetopass[i].specialMessage = -1;
                                messagetopass[i].specialMessageLineX = 0;
                                messagetopass[i].specialMessageLineY = 0;
                                messagetopass[i].lineX = 0;
                                messagetopass[i].lineY = 0;
                                messagetopass[i].count = 0;
                                messagetopass[i].swtch0 = 0;
                            }
                        }
                        messagetopass[i].count = messagetopass[i].count + 1;
                        mainMessageCursorPosSwitchCounter++;
                    }
                    else
                    {
                        mainMessageCursorPosSwitchCounter--;
                    }
                }
                else if (messagetopass[i].specialMessage == 2)
                {
                    if (messagetopass[i].swtch0 == 1)
                    {
                        messagetopass[i].count = messagetopass[i].delay;
                        messagetopass[i].swtch0 = 2;
                    }
                    else if (messagetopass[i].swtch0 == 2)
                    {
                        if (messagetopass[i].messageCut != "")
                        {
                            if (messagetopass[i].count <= 0)
                            {
                                somechar = messagetopass[i].messageCut.Substring(0, 1);

                                cutmsg = messagetopass[i].messageCut.Substring(1, messagetopass[i].messageCut.Length - 1);
                                messagetopass[i].messageCut = cutmsg;
                                messagetopass[i].message = somechar;

                                targetLineX = (int)messagetopass[i].lineX;
                                targetLineY = (int)messagetopass[i].lineY;

                                Draw(targetLineX, targetLineY, somechar);
                                maparray[targetLineY * consolewidth + targetLineX] = 1;
                                maparraydirty[targetLineY * consolewidth + targetLineX] = messagetopass[i].delay * 10;

                                lastConsoleMessage = somechar.ToString();

                                messagetopass[i].count = 0;

                                targetLineX = (int)messagetopass[i].lineX + 1;
                                targetLineY = (int)messagetopass[i].lineY;
                                messagetopass[i].lineX = targetLineX;
                                messagetopass[i].lineY = targetLineY;
                                messagetopass[i].count = messagetopass[i].delay;
                            }
                            else
                            {
                                messagetopass[i].count--;
                            }
                        }
                        else
                        {
                            if (messagetopass[i].swtch1 == 1)
                            {
                                messagetopass[i].count = messagetopass[i].delay * 15;

                                messagetopass[i].swtch1 = 2;
                            }
                            else if (messagetopass[i].swtch1 == 2)
                            {
                                if (messagetopass[i].count <= 0)
                                {
                                    if (messagetopass[i].looping == 1)
                                    {
                                        messagetopass[i].message = messagetopass[i].originalMsg;
                                        messagetopass[i].originalMsg = messagetopass[i].originalMsg;
                                        messagetopass[i].messageCut = messagetopass[i].originalMsg;
                                        messagetopass[i].specialMessage = 2;
                                        messagetopass[i].specialMessageLineX = 0;
                                        messagetopass[i].specialMessageLineY = 0;
                                        messagetopass[i].lineX = messagetopass[i].orilineX;
                                        messagetopass[i].lineY = messagetopass[i].orilineY;
                                        messagetopass[i].count = 0;
                                        messagetopass[i].swtch0 = 1;
                                        messagetopass[i].swtch1 = 1;
                                    }
                                    else
                                    {
                                        messagetopass[i].message = messagetopass[i].originalMsg;
                                        messagetopass[i].originalMsg = messagetopass[i].originalMsg;
                                        messagetopass[i].messageCut = messagetopass[i].originalMsg;
                                        messagetopass[i].specialMessage = -1;
                                        messagetopass[i].specialMessageLineX = 0;
                                        messagetopass[i].specialMessageLineY = 0;
                                        messagetopass[i].lineX = 0;
                                        messagetopass[i].lineY = 0;
                                        messagetopass[i].count = 0;
                                        messagetopass[i].swtch0 = 0;
                                        messagetopass[i].swtch1 = 0;
                                    }
                                }
                                else
                                {
                                    messagetopass[i].count--;
                                }
                            }
                        }
                    }
                }
                else if (messagetopass[i].specialMessage == 3)
                {
                    if (messagetopass[i].swtch0 == 1)
                    {
                        if (messagetopass[i].count >= messagetopass[i].delay)
                        {
                            if (messagetopass[i].messageCut != "")
                            {
                                somechar = messagetopass[i].messageCut.Substring(0, 1);
                                cutmsg = messagetopass[i].messageCut.Substring(1, messagetopass[i].messageCut.Length - 1);
                                messagetopass[i].messageCut = cutmsg;
                                messagetopass[i].message = somechar;

                                targetLineX = (int)messagetopass[i].lineX;
                                targetLineY = (int)messagetopass[i].lineY;

                                Draw(targetLineX, targetLineY, somechar);
                                //maparray[targetLineY * consolewidth + targetLineX] = 2;
                                //maparraydirty[targetLineY * consolewidth + targetLineX] = messagetopass[i].delay * 10;

                                lastConsoleMessage = somechar.ToString();

                                messagetopass[i].count = 0;

                                targetLineX = (int)messagetopass[i].lineX + 1;
                                targetLineY = (int)messagetopass[i].lineY;
                                messagetopass[i].lineX = targetLineX;
                                messagetopass[i].lineY = targetLineY;
                            }
                            else
                            {
                                messagetopass[i].message = "";
                                messagetopass[i].originalMsg = "";
                                messagetopass[i].messageCut = "";
                                messagetopass[i].specialMessage = -1;
                                messagetopass[i].specialMessageLineX = 0;
                                messagetopass[i].specialMessageLineY = 0;
                                messagetopass[i].lineX = 0;
                                messagetopass[i].lineY = 0;
                                messagetopass[i].count = 0;
                                messagetopass[i].swtch0 = 0;
                            }
                        }
                        messagetopass[i].count = messagetopass[i].count + 1;
                        mainMessageCursorPosSwitchCounter++;
                    }
                    else
                    {
                        mainMessageCursorPosSwitchCounter--;
                    }
                }
                else if (messagetopass[i].specialMessage == 4)
                {
                    if (messagetopass[i].swtch0 == 1)
                    {
                        if (messagetopass[i].count >= messagetopass[i].delay)
                        {
                            if (messagetopass[i].messageCut != "")
                            {
                                somechar = messagetopass[i].messageCut.Substring(0, 1);
                                cutmsg = messagetopass[i].messageCut.Substring(1, messagetopass[i].messageCut.Length - 1);
                                messagetopass[i].messageCut = cutmsg;
                                messagetopass[i].message = somechar;

                                targetLineX = (int)messagetopass[i].lineX;
                                targetLineY = (int)messagetopass[i].lineY;

                                Draw(targetLineX, targetLineY, somechar);
                                maparray[targetLineY * consolewidth + targetLineX] = 2;
                                maparraydirty[targetLineY * consolewidth + targetLineX] = messagetopass[i].delay * 10;

                                lastConsoleMessage = somechar.ToString();

                                messagetopass[i].count = 0;

                                targetLineX = (int)messagetopass[i].lineX + 1;
                                targetLineY = (int)messagetopass[i].lineY;
                                messagetopass[i].lineX = targetLineX;
                                messagetopass[i].lineY = targetLineY;
                            }
                            else
                            {
                                messagetopass[i].message = "";
                                messagetopass[i].originalMsg = "";
                                messagetopass[i].messageCut = "";
                                messagetopass[i].specialMessage = -1;
                                messagetopass[i].specialMessageLineX = 0;
                                messagetopass[i].specialMessageLineY = 0;
                                messagetopass[i].lineX = 0;
                                messagetopass[i].lineY = 0;
                                messagetopass[i].count = 0;
                                messagetopass[i].swtch0 = 0;
                            }
                        }
                        messagetopass[i].count = messagetopass[i].count + 1;
                        mainMessageCursorPosSwitchCounter++;
                    }
                    else
                    {
                        mainMessageCursorPosSwitchCounter--;
                    }
                }
                /*if (messagetopass[i].messagerlist != null)
                {

                    for (int c = 0; c < messagetopass[i].messagerlist.Length; c++)
                    {
                        if (messagetopass[i].messagerlist[c].specialMessage == 0)
                        {
                            if (messagetopass[i].messagerlist[c].swtch0 == 1)
                            {
                                if (messagetopass[i].messagerlist[c].count >= messagetopass[i].messagerlist[c].delay)
                                {
                                    if (messagetopass[i].messagerlist[c].messageCut != "")
                                    {
                                        char = messagetopass[i].messagerlist[c].messageCut.Substring(0, 1);

                                        cutmsg = messagetopass[i].messagerlist[c].messageCut.Substring(1, messagetopass[i].messagerlist[c].messageCut.Length - 1);
                                        messagetopass[i].messagerlist[c].messageCut = cutmsg;
                                        messagetopass[i].messagerlist[c].message = char;

                                        targetLineX = (int)messagetopass[i].messagerlist[c].lineX;
                                        targetLineY = (int)messagetopass[i].messagerlist[c].lineY;

                                        Draw(targetLineX, targetLineY, char);
                                        maparray[targetLineY * consolewidth + targetLineX] = 2;
                                        maparraydirty[targetLineY * consolewidth + targetLineX] = messagetopass[i].messagerlist[c].delay * 10;

                                        lastConsoleMessage = char.ToString();

                                        messagetopass[i].messagerlist[c].count = 0;

                                        targetLineX = (int)messagetopass[i].messagerlist[c].lineX + 1;
                                        targetLineY = (int)messagetopass[i].messagerlist[c].lineY;
                                        messagetopass[i].messagerlist[c].lineX = targetLineX;
                                        messagetopass[i].messagerlist[c].lineY = targetLineY;
                                    }
                                    else
                                    {
                                        messagetopass[i].messagerlist[c].message = "";
                                        messagetopass[i].messagerlist[c].originalMsg = "";
                                        messagetopass[i].messagerlist[c].messageCut = "";
                                        messagetopass[i].messagerlist[c].specialMessage = -1;
                                        messagetopass[i].messagerlist[c].specialMessageLineX = 0;
                                        messagetopass[i].messagerlist[c].specialMessageLineY = 0;
                                        messagetopass[i].messagerlist[c].lineX = 0;
                                        messagetopass[i].messagerlist[c].lineY = 0;
                                        messagetopass[i].messagerlist[c].count = 0;
                                        messagetopass[i].messagerlist[c].swtch0 = 0;
                                    }
                                }
                                messagetopass[i].messagerlist[c].count = messagetopass[i].messagerlist[c].count + 1;
                                mainMessageCursorPosSwitchCounter++;
                            }
                            else
                            {
                                mainMessageCursorPosSwitchCounter--;
                            }
                        }
                        else if (messagetopass[i].messagerlist[c].specialMessage == 2)
                        {
                            if (messagetopass[i].messagerlist[c].swtch0 == 1)
                            {
                                messagetopass[i].messagerlist[c].count = messagetopass[i].messagerlist[c].delay;
                                messagetopass[i].messagerlist[c].swtch0 = 2;
                            }
                            else if (messagetopass[i].messagerlist[c].swtch0 == 2)
                            {
                                if (messagetopass[i].messagerlist[c].messageCut != "")
                                {
                                    if (messagetopass[i].messagerlist[c].count <= 0)
                                    {
                                        char = messagetopass[i].messagerlist[c].messageCut.Substring(0, 1);

                                        cutmsg = messagetopass[i].messagerlist[c].messageCut.Substring(1, messagetopass[i].messagerlist[c].messageCut.Length - 1);
                                        messagetopass[i].messagerlist[c].messageCut = cutmsg;
                                        messagetopass[i].messagerlist[c].message = char;

                                        targetLineX = (int)messagetopass[i].messagerlist[c].lineX;
                                        targetLineY = (int)messagetopass[i].messagerlist[c].lineY;

                                        Draw(targetLineX, targetLineY, char);
                                        maparray[targetLineY * consolewidth + targetLineX] = 1;
                                        maparraydirty[targetLineY * consolewidth + targetLineX] = messagetopass[i].messagerlist[c].delay * 10;

                                        lastConsoleMessage = char.ToString();

                                        messagetopass[i].messagerlist[c].count = 0;

                                        targetLineX = (int)messagetopass[i].messagerlist[c].lineX + 1;
                                        targetLineY = (int)messagetopass[i].messagerlist[c].lineY;
                                        messagetopass[i].messagerlist[c].lineX = targetLineX;
                                        messagetopass[i].messagerlist[c].lineY = targetLineY;
                                        messagetopass[i].messagerlist[c].count = messagetopass[i].messagerlist[c].delay;
                                    }
                                    else
                                    {
                                        messagetopass[i].messagerlist[c].count--;
                                    }
                                }
                                else
                                {
                                    if (messagetopass[i].messagerlist[c].swtch1 == 1)
                                    {
                                        messagetopass[i].messagerlist[c].count = messagetopass[i].messagerlist[c].delay * 15;

                                        messagetopass[i].messagerlist[c].swtch1 = 2;
                                    }
                                    else if (messagetopass[i].messagerlist[c].swtch1 == 2)
                                    {
                                        if (messagetopass[i].messagerlist[c].count <= 0)
                                        {
                                            if (messagetopass[i].messagerlist[c].looping == 1)
                                            {
                                                messagetopass[i].messagerlist[c].message = messagetopass[i].messagerlist[c].originalMsg;
                                                messagetopass[i].messagerlist[c].originalMsg = messagetopass[i].messagerlist[c].originalMsg;
                                                messagetopass[i].messagerlist[c].messageCut = messagetopass[i].messagerlist[c].originalMsg;
                                                messagetopass[i].messagerlist[c].specialMessage = 2;
                                                messagetopass[i].messagerlist[c].specialMessageLineX = 0;
                                                messagetopass[i].messagerlist[c].specialMessageLineY = 0;
                                                messagetopass[i].messagerlist[c].lineX = messagetopass[i].messagerlist[c].orilineX;
                                                messagetopass[i].messagerlist[c].lineY = messagetopass[i].messagerlist[c].orilineY;
                                                messagetopass[i].messagerlist[c].count = 0;
                                                messagetopass[i].messagerlist[c].swtch0 = 1;
                                                messagetopass[i].messagerlist[c].swtch1 = 1;
                                            }
                                            else
                                            {
                                                messagetopass[i].messagerlist[c].message = messagetopass[i].messagerlist[c].originalMsg;
                                                messagetopass[i].messagerlist[c].originalMsg = messagetopass[i].messagerlist[c].originalMsg;
                                                messagetopass[i].messagerlist[c].messageCut = messagetopass[i].messagerlist[c].originalMsg;
                                                messagetopass[i].messagerlist[c].specialMessage = -1;
                                                messagetopass[i].messagerlist[c].specialMessageLineX = 0;
                                                messagetopass[i].messagerlist[c].specialMessageLineY = 0;
                                                messagetopass[i].messagerlist[c].lineX = 0;
                                                messagetopass[i].messagerlist[c].lineY = 0;
                                                messagetopass[i].messagerlist[c].count = 0;
                                                messagetopass[i].messagerlist[c].swtch0 = 0;
                                                messagetopass[i].messagerlist[c].swtch1 = 0;
                                            }
                                        }
                                        else
                                        {
                                            messagetopass[i].messagerlist[c].count--;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }*/
            }
            lock (messagetopasslist)
            {
                if (messagetopasslist.Count > 0)
                {
                    dummyMessage = messagetopasslist[0];
                    if (dummyMessage.count >= dummyMessage.delay)
                    {
                        if (dummyMessage.messageCut != "")
                        {
                            string somechar = dummyMessage.messageCut.Substring(0, 1);
                            string cutmsg = dummyMessage.messageCut.Substring(1, dummyMessage.messageCut.Length - 1);
                            dummyMessage.messageCut = cutmsg;

                            int targetLineX = (int)dummyMessage.lineX;
                            int targetLineY = (int)dummyMessage.lineY;
                            Draw(targetLineX, targetLineY, somechar);

                            //MessageBox((IntPtr)0, char + "", "Console", 0);

                            dummyMessage.count = 0;
                            targetLineX = (int)dummyMessage.lineX + 1;
                            targetLineY = (int)dummyMessage.lineY;
                            dummyMessage.lineX = targetLineX;
                            dummyMessage.lineY = targetLineY;

                            messagetopasslist[0] = dummyMessage;
                        }
                        else
                        {
                            messagetopasslist[0] = dummyMessage;
                            messagetopasslist.Remove(messagetopasslist[0]);
                        }
                    }
                    else
                    {
                        dummyMessage.count = dummyMessage.count + 1;
                        messagetopasslist[0] = dummyMessage;
                    }
                }
                else
                {
                    //Draw(1, 6, messagetopasslist.Count + "");
                }
            }

            for (int x = 0; x < consolewidth; x++)
            {
                for (int y = 0; y < consoleheight; y++)
                {
                    if (maparray[y * consolewidth + x] == 1)
                    {
                        if (maparraydirty[y * consolewidth + x] != 0)
                        {
                            maparraydirty[y * consolewidth + x]--;

                        }
                        else
                        {
                            Draw(x, y, " ");
                            maparray[(y * consolewidth) + x] = 0;
                            maparraydirty[(y * consolewidth) + x] = 0;
                        }
                    }
                    else
                    {

                    }
                }
            }
            return messagetopass;
        }
    }
}
