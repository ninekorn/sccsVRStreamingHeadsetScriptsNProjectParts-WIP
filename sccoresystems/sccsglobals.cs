
using sccoresystems.sccsconsole;
using sccoresystems.sccscore;
using sccoresystems.sccsgraphics;

namespace sccoresystems
{
    public interface sccsglobals
    {
        sccoresystems.sccsconsole.sccsconsolecore SCCSCONSOLECORE { get; set; }
        sccoresystems.sccsconsole.sccsconsolewriter SCCSCONSOLEWRITER { get; set; }
        sccoresystems.sccsconsole.sccsconsolereader SCCSCONSOLEREADER { get; set; }
        sccoresystems.sccscore.sccsglobalsaccessor SCCSGLOBALSACCESSORS { get; set; }
        //sccssharpdxdirectx D3D { get; set; }
        //sccsgraphicsupdate sccsgraphicsupdate { get; set; }
    }
}
