using System;

namespace sccoresystems.sccsconsole
{
    public class sccsconsolereader
    {
        public sccsconsolewriter SCCONSOLEWRITER;
        consolereaderdata currentconsolereaderdata;
        public int mainhasinit = 0;

        public sccsconsolereader(object tester)
        {
            SCCONSOLEWRITER = sccoresystems.sccscore.sccsglobalsaccessor.SCCSGLOB.SCCSCONSOLEWRITER;
        }

        public consolereaderdata consolereader(object mainobject)
        {
            if (mainhasinit == 0)
            {

                string tester = Console.ReadLine();
                currentconsolereaderdata.consolereadermessage = "nothing ";
                currentconsolereaderdata.hasmessagetodisplay = 0;
                mainhasinit = 1;
            }
            else if (mainhasinit == 1 || mainhasinit == 2)
            {
                string tester = Console.ReadLine();
                currentconsolereaderdata.consolereadermessage = tester;
                currentconsolereaderdata.hasmessagetodisplay = 1;
            }

            return currentconsolereaderdata;
        }

        public struct consolereaderdata
        {
            public int hasinit;
            public int hasmessagetodisplay;
            public string consolereadermessage;
        }
    }
}
