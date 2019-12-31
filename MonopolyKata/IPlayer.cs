using System;

namespace MonopolyKata
{
    public interface IPlayer
    {
        Boolean InJail { get; set; }
        Int32 Location { get; set; }
        Int32 SentenceLeft { get; set; }

        void AddProperty(IOwnableProperty property);
        Boolean OwnsProperty(IOwnableProperty property);
    }
}
