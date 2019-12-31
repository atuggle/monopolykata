using System;
using System.Collections.Generic;

namespace MonopolyKata
{
    public interface IOwnableProperty
    {
        IEnumerable<IOwnableProperty> GroupedSpaces { get; set; }
        IPlayer Owner { get; }
        Int32 Price { get; }
        Int32? Rent { get; }
    }
}
