using System;
namespace sccoresystems.scconsolemenu
{
    public static class scconsolemenu01
    {
        static sccoresystems.sccsmessageobject someReceivedObject0 = new sccoresystems.sccsmessageobject();
        static sccoresystems.sccsmessageobject data00IN;
        static sccoresystems.sccsmessageobject toReturnObject;

        public static sccoresystems.sccsmessageobject consolemenu(object mainobject)
        {
            try
            {
                data00IN = (sccoresystems.sccsmessageobject)mainobject;

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
                string menuOption = data00IN.menuOption;

                someReceivedObject0 = data00IN;

                if (receivedswitchin00 == 0 &&
                    receivedswitchout00 == 0 &&
                    sendingswitchin00 == 0 &&
                    sendingswitchout00 == 0)
                {
                    if (menuOption.ToLower() == "vr" ||
                        menuOption.ToLower() == "std" ||
                        menuOption.ToLower() == "standard" ||
                        menuOption.ToLower() == "command" ||
                        menuOption.ToLower() == "commands" ||
                        menuOption.ToLower() == "credit" ||
                        menuOption.ToLower() == "credits" ||
                        menuOption.ToLower() == "singleplayer" ||
                        menuOption.ToLower() == "multiplayer" ||
                        menuOption.ToLower() == "single" ||
                        menuOption.ToLower() == "multi")
                    {
                        if (menuOption.ToLower() == "vr")
                        {
                            someReceivedObject0.currentmenu = 0;

                            toReturnObject = someReceivedObject0;
                            return toReturnObject;
                        }
                        else if (menuOption.ToLower() == "standard" || menuOption.ToLower() == "std")
                        {
                            someReceivedObject0.currentmenu = 1;
                            toReturnObject = someReceivedObject0;
                            return toReturnObject;
                        }
                    }
                    else
                    {
                        someReceivedObject0.receivedswitchin = 0;
                        someReceivedObject0.receivedswitchout = 0;
                        someReceivedObject0.sendingswitchin = 0;
                        someReceivedObject0.sendingswitchout = 0;
                        someReceivedObject0.welcomePackage = 1;
                        someReceivedObject0.workdone = 0;
                        someReceivedObject0.currentmenu = -1;
                    }
                    toReturnObject = someReceivedObject0;
                    return toReturnObject;
                }
                else if (receivedswitchin00 == 1 &&
                         receivedswitchout00 == 0 &&
                         sendingswitchin00 == 0 &&
                         sendingswitchout00 == 0)
                {
                    someReceivedObject0.receivedswitchin = 0;
                    someReceivedObject0.receivedswitchout = 0;
                    someReceivedObject0.sendingswitchin = 0;
                    someReceivedObject0.sendingswitchout = 0;
                    toReturnObject = someReceivedObject0;
                    return toReturnObject;
                }
                else if (receivedswitchin00 == 0 &&
                         receivedswitchout00 == 1 &&
                         sendingswitchin00 == 0 &&
                         sendingswitchout00 == 0)
                {
                    someReceivedObject0.receivedswitchin = 0;
                    someReceivedObject0.receivedswitchout = 0;
                    someReceivedObject0.sendingswitchin = 0;
                    someReceivedObject0.sendingswitchout = 0;
                    toReturnObject = someReceivedObject0;
                    return toReturnObject;
                }
                else if (receivedswitchin00 == 0 &&
                         receivedswitchout00 == 0 &&
                         sendingswitchin00 == 1 &&
                         sendingswitchout00 == 0)
                {
                    someReceivedObject0.receivedswitchin = 0;
                    someReceivedObject0.receivedswitchout = 0;
                    someReceivedObject0.sendingswitchin = 0;
                    someReceivedObject0.sendingswitchout = 0;
                    toReturnObject = someReceivedObject0;
                    toReturnObject = someReceivedObject0;
                    return toReturnObject;
                }
                else if (receivedswitchin00 == 0 &&
                       receivedswitchout00 == 0 &&
                       sendingswitchin00 == 0 &&
                       sendingswitchout00 == 1)
                {
                    someReceivedObject0.receivedswitchin = 0;
                    someReceivedObject0.receivedswitchout = 0;
                    someReceivedObject0.sendingswitchin = 0;
                    someReceivedObject0.sendingswitchout = 0;
                    toReturnObject = someReceivedObject0;
                    return toReturnObject;

                }
                else if (receivedswitchin00 == 1 &&
                        receivedswitchout00 == 1 &&
                        sendingswitchin00 == 0 &&
                        sendingswitchout00 == 0)
                {

                    someReceivedObject0.receivedswitchin = 0;
                    someReceivedObject0.receivedswitchout = 0;
                    someReceivedObject0.sendingswitchin = 0;
                    someReceivedObject0.sendingswitchout = 0;
                    toReturnObject = someReceivedObject0;
                    return toReturnObject;
                }
                else if (receivedswitchin00 == 1 &&
                     receivedswitchout00 == 0 &&
                     sendingswitchin00 == 1 &&
                     sendingswitchout00 == 0)
                {
                    someReceivedObject0.receivedswitchin = 0;
                    someReceivedObject0.receivedswitchout = 0;
                    someReceivedObject0.sendingswitchin = 0;
                    someReceivedObject0.sendingswitchout = 0;
                    toReturnObject = someReceivedObject0;
                    return toReturnObject;
                }
                else if (receivedswitchin00 == 1 &&
                       receivedswitchout00 == 0 &&
                       sendingswitchin00 == 0 &&
                       sendingswitchout00 == 1)
                {
                    someReceivedObject0.receivedswitchin = 0;
                    someReceivedObject0.receivedswitchout = 0;
                    someReceivedObject0.sendingswitchin = 0;
                    someReceivedObject0.sendingswitchout = 0;
                    toReturnObject = someReceivedObject0;
                    return toReturnObject;
                }
                else if (receivedswitchin00 == 0 &&
                    receivedswitchout00 == 1 &&
                    sendingswitchin00 == 1 &&
                    sendingswitchout00 == 0)
                {
                    someReceivedObject0.receivedswitchin = 0;
                    someReceivedObject0.receivedswitchout = 0;
                    someReceivedObject0.sendingswitchin = 0;
                    someReceivedObject0.sendingswitchout = 0;
                    toReturnObject = someReceivedObject0;
                    return toReturnObject;
                }
                else if (receivedswitchin00 == 0 &&
                      receivedswitchout00 == 1 &&
                      sendingswitchin00 == 0 &&
                      sendingswitchout00 == 1)
                {
                    someReceivedObject0.receivedswitchin = 0;
                    someReceivedObject0.receivedswitchout = 0;
                    someReceivedObject0.sendingswitchin = 0;
                    someReceivedObject0.sendingswitchout = 0;
                    toReturnObject = someReceivedObject0;
                    return toReturnObject;
                }
                else if (receivedswitchin00 == 0 &&
                      receivedswitchout00 == 0 &&
                      sendingswitchin00 == 1 &&
                      sendingswitchout00 == 1)
                {
                    someReceivedObject0.receivedswitchin = 0;
                    someReceivedObject0.receivedswitchout = 0;
                    someReceivedObject0.sendingswitchin = 0;
                    someReceivedObject0.sendingswitchout = 0;
                    toReturnObject = someReceivedObject0;
                    return toReturnObject;
                }
                else if (receivedswitchin00 == 1 &&
                           receivedswitchout00 == 0 &&
                           sendingswitchin00 == 1 &&
                           sendingswitchout00 == 1)
                {
                    someReceivedObject0.receivedswitchin = 0;
                    someReceivedObject0.receivedswitchout = 0;
                    someReceivedObject0.sendingswitchin = 0;
                    someReceivedObject0.sendingswitchout = 0;
                    toReturnObject = someReceivedObject0;
                    return toReturnObject;
                }
                else if (receivedswitchin00 == 0 &&
                        receivedswitchout00 == 1 &&
                        sendingswitchin00 == 1 &&
                        sendingswitchout00 == 1)
                {
                    someReceivedObject0.receivedswitchin = 0;
                    someReceivedObject0.receivedswitchout = 0;
                    someReceivedObject0.sendingswitchin = 0;
                    someReceivedObject0.sendingswitchout = 0;
                    toReturnObject = someReceivedObject0;
                    return toReturnObject;
                }

                else if (receivedswitchin00 == 1 &&
                        receivedswitchout00 == 1 &&
                        sendingswitchin00 == 0 &&
                        sendingswitchout00 == 1)
                {
                    someReceivedObject0.receivedswitchin = 0;
                    someReceivedObject0.receivedswitchout = 0;
                    someReceivedObject0.sendingswitchin = 0;
                    someReceivedObject0.sendingswitchout = 0;
                    toReturnObject = someReceivedObject0;
                    return toReturnObject;
                }
                else if (receivedswitchin00 == 1 &&
                         receivedswitchout00 == 1 &&
                         sendingswitchin00 == 1 &&
                         sendingswitchout00 == 0)
                {
                    someReceivedObject0.receivedswitchin = 0;
                    someReceivedObject0.receivedswitchout = 0;
                    someReceivedObject0.sendingswitchin = 0;
                    someReceivedObject0.sendingswitchout = 0;
                    toReturnObject = someReceivedObject0;
                    return toReturnObject;
                }
                else if (receivedswitchin00 == 1 &&
                      receivedswitchout00 == 1 &&
                      sendingswitchin00 == 1 &&
                      sendingswitchout00 == 1)
                {
                    someReceivedObject0.receivedswitchin = 0;
                    someReceivedObject0.receivedswitchout = 0;
                    someReceivedObject0.sendingswitchin = 0;
                    someReceivedObject0.sendingswitchout = 0;
                    toReturnObject = someReceivedObject0;
                    return toReturnObject;
                }

            }
            catch (Exception ex)
            {
                Program.MessageBox((IntPtr)0, "Fail 00" + ex.Message, "Oculus error", 0);
            }
            return toReturnObject;
        }
    }
}
