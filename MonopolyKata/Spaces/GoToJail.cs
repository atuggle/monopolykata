using System;

namespace MonopolyKata
{
    public class GoToJail : Space
    {
        private Int32 jailLocation;

        public GoToJail(Int32 boardLocation, String name, Int32 jailLocation) : base(boardLocation, name) 
        {
            this.jailLocation = jailLocation;
        }

        public override void Land(IPlayer player)
        {
            base.Land(player);
            player.InJail = true;
            player.Location = jailLocation;
        }
    }
}
