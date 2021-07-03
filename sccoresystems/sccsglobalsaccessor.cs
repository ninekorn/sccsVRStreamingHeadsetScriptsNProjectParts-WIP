using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;

using sccoresystems.sccsgraphics;
using sccoresystems.sccsconsole;

namespace sccoresystems.sccscore
{
    public class sccsglobalsaccessor : sccsicomponent, sccsglobals
    {
        public static sccsicomponent SCCSICOMPONENT;
        sccsglobals sccsicomponent.SCCSGlobals
        {
            get => SCCSGLOB;
        }
        public static sccsglobals SCCSGLOB;



        public virtual sccoresystems.sccsconsole.sccsconsolecore SCCSCONSOLECORE { get; set; }
        public virtual sccoresystems.sccsconsole.sccsconsolewriter SCCSCONSOLEWRITER { get; set; }
        public virtual sccoresystems.sccsconsole.sccsconsolereader SCCSCONSOLEREADER { get; set; }
        public virtual sccoresystems.sccscore.sccsglobalsaccessor SCCSGLOBALSACCESSORS { get; set; }
        //public virtual sccssharpdxdirectx D3D { get; set; }
        //public virtual sccsgraphicsupdate sccsgraphicsupdate { get; set; }


        private sccoresystems.sccsmessageobject testingInit(sccoresystems.sccsmessageobject mainobject)
        {   
            return mainobject;
        }

        public sccsglobalsaccessor(sccoresystems.sccsmessageobject[] mainobject)
        {
            SCCSGLOB = this;
            SCCSICOMPONENT = this;

            SCCSCONSOLECORE = new sccsconsole.sccsconsolecore(mainobject);
            SCCSCONSOLEWRITER = new sccsconsole.sccsconsolewriter(mainobject);
            SCCSCONSOLEREADER = new sccsconsole.sccsconsolereader(mainobject);

            SCCSGLOB.SCCSCONSOLECORE = SCCSCONSOLECORE;
            SCCSGLOB.SCCSCONSOLEWRITER = SCCSCONSOLEWRITER;
            SCCSGLOB.SCCSCONSOLEREADER = SCCSCONSOLEREADER;

            //SCCSGLOB.D3D = D3D;
            //SCCSGLOB.sccsgraphicsupdate = sccsgraphicsupdate;
            //Console.WriteLine("some globals to change the whole integral script later but not right now as i need to keep things organized as they were... disorganized it's easier. im searching to migrate my whole sccoresystems solution in u");  
        }
    }
}
