using System;

namespace MonopolyKata
{
    public class TurnEventArgs : EventArgs
    {
        public Boolean Doubles { get; set; }
        public Int32 GoAgainCount { get; set; }
        public Int32 OrderOfPosition { get; set; }
        public IPlayer Player { get; set; }
        public Int32 Roll { get; set; }
        public Int32 Round { get; set; }
    }
}
