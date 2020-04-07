using System.Collections.Generic;

public class MachInfo
{
    public List<ItemCandy> match;
    public int matchInicioEixoX;
    public int matchFimEixoX;
    public int matchIncioEixoY;
    public int matchFimEixoY;

    public bool validMatch
    {
        get { return match != null; }
    }
}
