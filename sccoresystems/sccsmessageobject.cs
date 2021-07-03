using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace sccoresystems
{
    public struct sccsmessageobject
    {
        public int receivedswitchin;
        public int receivedswitchout;
        public int sendingswitchin;
        public int sendingswitchout;
        //public List<int[]> chainOfTasks0;
        public int timeOut0;
        public int ParentTaskThreadID0;
        public int maincpucount;
        public string passTest;
        public int welcomePackage;
        //public ManualResetEvent resetevent;
        public int workdone;
        public int currentmenu;
        public int lastcurrentmenu;
        public int mainmenu;
        public string menuOption;
        public int voRecSwtc;
        public string voRecMsg;
        public object[] someData;
    }


    public struct sc_message_object_jitter
    {
        public int _sc_jitter_main;
        public int _sc_jitter_can_work;
        public float _world_step;
        public object[] _world_data;
        public int _work_index;
    }
}

