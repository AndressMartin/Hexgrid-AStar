using System.Collections.Generic;

public interface IMap
{
    Dictionary<Hex, ICell> Cells { get; }
}