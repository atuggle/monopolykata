using System;

namespace MonopolyKata
{
    public class Space : ISpace
    {
        public Int32 BoardLocation { get; private set; }
        public String Name { get; private set; }

        public Space(Int32 boardLocation, String name)
        {
            BoardLocation = boardLocation;
            Name = name;
        }

        public virtual void Visit(IPlayer player)
        {
            player.Location = BoardLocation;
        }

        public virtual void Land(IPlayer player)
        {
            this.Visit(player);
        }
    }
}
