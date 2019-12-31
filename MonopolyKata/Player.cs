using System;
using System.Collections.Generic;
using System.Linq;

namespace MonopolyKata
{
    public class Player : IPlayer
    {
        private String playerName = String.Empty;
        private Boolean inJail = false;
        private IList<IOwnableProperty> properties = new List<IOwnableProperty>();

        public Int32 Location { get; set; }
        public Int32 SentenceLeft { get; set; }
        public Boolean InJail 
        {
            get { return inJail; }
            set 
            {
                inJail = value;
                SentenceLeft = inJail ? 2 : 0;
            } 
        }

        public Player(String name)
        {
            playerName = name;
        }

        public void AddProperty(IOwnableProperty property)
        {
            properties.Add(property);
        }

        public Boolean OwnsProperty(IOwnableProperty property)
        {
            return properties.Any(p => p.Equals(property));
        }

        public override string ToString()
        {
            return playerName;
        }
    }
}
