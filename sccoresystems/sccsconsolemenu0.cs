using System;

namespace sccoresystems.scconsolemenu
{
    public static class scconsolemenu00
    {
        static sccoresystems.sccsmessageobject someReceivedObject0 = new sccoresystems.sccsmessageobject();
        static sccoresystems.sccsmessageobject data00IN;
        static sccoresystems.sccsmessageobject toReturnObject;

        public static sccsmessageobject[] consolemenu(sccsmessageobject[] mainobject)
        {
            try
            {
                for (int L0IN = 0; L0IN < mainobject.Length; L0IN++)
                {
                    data00IN = mainobject[L0IN];

                    int workdoner = data00IN.workdone;
                    int receivedswitchin00 = data00IN.receivedswitchin;
                    int receivedswitchout00 = data00IN.receivedswitchout;
                    int sendingswitchin00 = data00IN.sendingswitchin;
                    int sendingswitchout00 = data00IN.sendingswitchout;

                    int timeOut00 = data00IN.timeOut0;
                    int ParentTaskThreadID00 = data00IN.ParentTaskThreadID0;
                    int maincpucount00 = data00IN.maincpucount;
                    string passTest00 = data00IN.passTest;
                    int welcomePackage00 = data00IN.welcomePackage;
                    int currentmenu00 = data00IN.currentmenu;
                    int lastcurrentmenu00 = data00IN.lastcurrentmenu;

                    if (receivedswitchin00 == 0 &&
                        receivedswitchout00 == 0 &&
                        sendingswitchin00 == 0 &&
                        sendingswitchout00 == 0)
                    {
                        mainobject[L0IN].receivedswitchin = 0;
                        mainobject[L0IN].receivedswitchout = 0;
                        mainobject[L0IN].sendingswitchin = 0;
                        mainobject[L0IN].sendingswitchout = 0;
                    }
                    else if (receivedswitchin00 == 1 &&
                             receivedswitchout00 == 0 &&
                             sendingswitchin00 == 0 &&
                             sendingswitchout00 == 0)
                    {
                        if (welcomePackage00 == -1)
                        {
                            mainobject[L0IN].receivedswitchin = 0;
                            mainobject[L0IN].receivedswitchout = 0;
                            mainobject[L0IN].sendingswitchin = 0;
                            mainobject[L0IN].sendingswitchout = 0;
                        }
                        else if (welcomePackage00 == 0)
                        {
                            if (passTest00.ToLower() == "nine" || passTest00.ToLower() == "ninekorn" || passTest00.ToLower() == "9")
                            {
                                mainobject[L0IN].receivedswitchin = 1;
                                mainobject[L0IN].receivedswitchout = 1;
                                mainobject[L0IN].sendingswitchin = 0;
                                mainobject[L0IN].sendingswitchout = 0;
                                mainobject[L0IN].passTest = passTest00.ToLower();
                                mainobject[L0IN].welcomePackage = 1;
                                mainobject[L0IN].workdone = 1;
                                mainobject[L0IN].mainmenu = 0;
                            }
                            else
                            {
                                mainobject[L0IN].receivedswitchin = 1;
                                mainobject[L0IN].receivedswitchout = 0;
                                mainobject[L0IN].sendingswitchin = 0;
                                mainobject[L0IN].sendingswitchout = 0;
                                mainobject[L0IN].welcomePackage = 0;
                                mainobject[L0IN].workdone = 1;

                                mainobject[L0IN].passTest = "";
                            }
                        }
                    }
                    else if (receivedswitchin00 == 0 &&
                             receivedswitchout00 == 1 &&
                             sendingswitchin00 == 0 &&
                             sendingswitchout00 == 0)
                    {
                        mainobject[L0IN].receivedswitchin = 0;
                        mainobject[L0IN].receivedswitchout = 0;
                        mainobject[L0IN].sendingswitchin = 0;
                        mainobject[L0IN].sendingswitchout = 0;
                    }
                    else if (receivedswitchin00 == 0 &&
                             receivedswitchout00 == 0 &&
                             sendingswitchin00 == 1 &&
                             sendingswitchout00 == 0)
                    {
                        mainobject[L0IN].receivedswitchin = 0;
                        mainobject[L0IN].receivedswitchout = 0;
                        mainobject[L0IN].sendingswitchin = 0;
                        mainobject[L0IN].sendingswitchout = 0;
                    }
                    else if (receivedswitchin00 == 0 &&
                           receivedswitchout00 == 0 &&
                           sendingswitchin00 == 0 &&
                           sendingswitchout00 == 1)
                    {

                        string optionCommand = Console.ReadLine();

                        if (optionCommand.ToLower() == "option" ||
                            optionCommand.ToLower() == "command" ||
                            optionCommand.ToLower() == "options" ||
                            optionCommand.ToLower() == "commands")
                        {

                            mainobject[L0IN].receivedswitchin = 0;
                            mainobject[L0IN].receivedswitchout = 0;
                            mainobject[L0IN].sendingswitchin = 0;
                            mainobject[L0IN].sendingswitchout = 0;
                        }
                        else
                        {
                            mainobject[L0IN].receivedswitchin = 0;
                            mainobject[L0IN].receivedswitchout = 0;
                            mainobject[L0IN].sendingswitchin = 0;
                            mainobject[L0IN].sendingswitchout = 0;
                        }
                    }
                    else if (receivedswitchin00 == 1 &&
                            receivedswitchout00 == 1 &&
                            sendingswitchin00 == 0 &&
                            sendingswitchout00 == 0)
                    {
                        mainobject[L0IN].receivedswitchin = 0;
                        mainobject[L0IN].receivedswitchout = 0;
                        mainobject[L0IN].sendingswitchin = 0;
                        mainobject[L0IN].sendingswitchout = 0;
                    }
                    else if (receivedswitchin00 == 1 &&
                         receivedswitchout00 == 0 &&
                         sendingswitchin00 == 1 &&
                         sendingswitchout00 == 0)
                    {
                        mainobject[L0IN].receivedswitchin = 0;
                        mainobject[L0IN].receivedswitchout = 0;
                        mainobject[L0IN].sendingswitchin = 0;
                        mainobject[L0IN].sendingswitchout = 0;
                    }
                    else if (receivedswitchin00 == 1 &&
                           receivedswitchout00 == 0 &&
                           sendingswitchin00 == 0 &&
                           sendingswitchout00 == 1)
                    {
                        mainobject[L0IN].receivedswitchin = 0;
                        mainobject[L0IN].receivedswitchout = 0;
                        mainobject[L0IN].sendingswitchin = 0;
                        mainobject[L0IN].sendingswitchout = 0;
                    }
                    else if (receivedswitchin00 == 0 &&
                        receivedswitchout00 == 1 &&
                        sendingswitchin00 == 1 &&
                        sendingswitchout00 == 0)
                    {
                        mainobject[L0IN].receivedswitchin = 0;
                        mainobject[L0IN].receivedswitchout = 0;
                        mainobject[L0IN].sendingswitchin = 0;
                        mainobject[L0IN].sendingswitchout = 0;
                    }
                    else if (receivedswitchin00 == 0 &&
                          receivedswitchout00 == 1 &&
                          sendingswitchin00 == 0 &&
                          sendingswitchout00 == 1)
                    {
                        mainobject[L0IN].receivedswitchin = 0;
                        mainobject[L0IN].receivedswitchout = 0;
                        mainobject[L0IN].sendingswitchin = 0;
                        mainobject[L0IN].sendingswitchout = 0;
                    }
                    else if (receivedswitchin00 == 0 &&
                          receivedswitchout00 == 0 &&
                          sendingswitchin00 == 1 &&
                          sendingswitchout00 == 1)
                    {
                        mainobject[L0IN].receivedswitchin = 0;
                        mainobject[L0IN].receivedswitchout = 0;
                        mainobject[L0IN].sendingswitchin = 0;
                        mainobject[L0IN].sendingswitchout = 0;
                    }
                    else if (receivedswitchin00 == 1 &&
                               receivedswitchout00 == 0 &&
                               sendingswitchin00 == 1 &&
                               sendingswitchout00 == 1)
                    {
                        mainobject[L0IN].receivedswitchin = 0;
                        mainobject[L0IN].receivedswitchout = 0;
                        mainobject[L0IN].sendingswitchin = 0;
                        mainobject[L0IN].sendingswitchout = 0;
                    }
                    else if (receivedswitchin00 == 0 &&
                            receivedswitchout00 == 1 &&
                            sendingswitchin00 == 1 &&
                            sendingswitchout00 == 1)
                    {
                        mainobject[L0IN].receivedswitchin = 0;
                        mainobject[L0IN].receivedswitchout = 0;
                        mainobject[L0IN].sendingswitchin = 0;
                        mainobject[L0IN].sendingswitchout = 0;
                    }

                    else if (receivedswitchin00 == 1 &&
                            receivedswitchout00 == 1 &&
                            sendingswitchin00 == 0 &&
                            sendingswitchout00 == 1)
                    {
                        mainobject[L0IN].receivedswitchin = 0;
                        mainobject[L0IN].receivedswitchout = 0;
                        mainobject[L0IN].sendingswitchin = 0;
                        mainobject[L0IN].sendingswitchout = 0;
                    }
                    else if (receivedswitchin00 == 1 &&
                            receivedswitchout00 == 1 &&
                            sendingswitchin00 == 1 &&
                            sendingswitchout00 == 0)
                    {
                        mainobject[L0IN].receivedswitchin = 0;
                        mainobject[L0IN].receivedswitchout = 0;
                        mainobject[L0IN].sendingswitchin = 0;
                        mainobject[L0IN].sendingswitchout = 0;
                    }
                    else if (receivedswitchin00 == 1 &&
                          receivedswitchout00 == 1 &&
                          sendingswitchin00 == 1 &&
                          sendingswitchout00 == 1)
                    {
                        if (welcomePackage00 == 998)
                        {
                            Program.initdirectXmainswtch = 2;
                        }
                        else if (welcomePackage00 == 999)
                        {
                            Program.initvrmainswtch = 2;
                        }
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return mainobject;
        }
    }
}
