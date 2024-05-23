using System.Collections.Generic;

public class HexMap : IMap
{
    private Dictionary<Hex, ICell> cells;

    public HexMap(Dictionary<Hex, ICell> cells)
    {
        this.cells = cells;
    }

    public Dictionary<Hex, ICell> Cells => cells;
}