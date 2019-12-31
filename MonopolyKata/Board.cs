using System;
using System.Collections.Generic;
using System.Linq;

namespace MonopolyKata
{
    public class Board : IBoard
    {
        private Int32 jailLocation = 10;
        private List<ISpace> spaces = new List<ISpace>();

        public Board(IBanker banker, IDice dice)
        {
            var deck = new Deck(banker, this);
            InitializeBoard(banker, dice, deck);
        }

        public void SendPlayerToJail(IPlayer player)
        {
            player.Location = jailLocation;
            player.InJail = true;
        }

        public ISpace GetNextSpace(Int32 currentLocation)
        {
            var nextLocation = (currentLocation + 1) % 40;
            return spaces.First(s=>s.BoardLocation == nextLocation);
        }

        public void AdvancePlayerOnBoard(IPlayer player, Int32 spaces)
        {
            for (var move = 0; move < spaces - 1; move++)
                GetNextSpace(player.Location).Visit(player);

            var nextSpace = GetNextSpace(player.Location);
            nextSpace.Land(player);
        }

        private void InitializeBoard(IBanker banker, IDice dice, IDeck deck)
        {
            spaces.Add(new Go(0, banker, "Go"));
            spaces.Add(new CommunityChest(this, 2, banker, deck, "Community Chest"));
            spaces.Add(new IncomeTax(4, banker, "Income Tax"));
            spaces.Add(new Chance(this, 7, banker, deck, "Chance"));
            spaces.Add(new Space(jailLocation, "In Jail/Just Visiting"));
            spaces.Add(new CommunityChest(this, 17, banker, deck, "Community Chest"));
            spaces.Add(new Space(20, "Free Parking"));
            spaces.Add(new Chance(this, 22, banker, deck, "Chance"));
            spaces.Add(new GoToJail(30, "Go To Jail", jailLocation));
            spaces.Add(new CommunityChest(this, 33, banker, deck, "Community Chest"));
            spaces.Add(new Chance(this, 36, banker, deck, "Chance"));
            spaces.Add(new LuxuryTax(38, banker, "Luxury Tax"));

            spaces.AddRange(CreateGroupedSpaces(
                    new OwnableProperty(1, banker, "Mediterranean Avenue", 60, 2),
                    new OwnableProperty(3, banker, "Baltic Avenue", 60, 4)
                ));

            spaces.AddRange(CreateGroupedSpaces(
                    new OwnableProperty(6, banker, "Oriental Avenue", 100, 6),
                    new OwnableProperty(8, banker, "Vermont Avenue", 100, 6),
                    new OwnableProperty(9, banker, "Connecticut Avenue", 120, 8)
                ));

            spaces.AddRange(CreateGroupedSpaces(
                    new OwnableProperty(11, banker, "St. Charles Place", 140, 10),
                    new OwnableProperty(13, banker, "States Avenue", 140, 10),
                    new OwnableProperty(14, banker, "Virginia Avenue", 160, 12)
                ));

            spaces.AddRange(CreateGroupedSpaces(
                    new OwnableProperty(16, banker, "St. James Place", 180, 14),
                    new OwnableProperty(18, banker, "Tennessee Avenue", 180, 14),
                    new OwnableProperty(19, banker, "New York Avenue", 200, 16)
                ));

            spaces.AddRange(CreateGroupedSpaces(
                    new OwnableProperty(21, banker, "Kentucky Avenue", 220, 18),
                    new OwnableProperty(23, banker, "Indiana Avenue", 220, 18),
                    new OwnableProperty(24, banker, "Illinois Avenue", 240, 20)
                ));

            spaces.AddRange(CreateGroupedSpaces(
                    new OwnableProperty(26, banker, "Atlantic Avenue", 260, 22),
                    new OwnableProperty(27, banker, "Ventnor Avenue", 260, 22),
                    new OwnableProperty(29, banker, "Marvin Gardens", 280, 24)
                ));

            spaces.AddRange(CreateGroupedSpaces(
                    new OwnableProperty(31, banker, "Pacific Avenue", 300, 26),
                    new OwnableProperty(32, banker, "North Carolina Avenue", 300, 26),
                    new OwnableProperty(34, banker, "Pennsylvania Avenue", 320, 28)
                ));

            spaces.AddRange(CreateGroupedSpaces(
                    new OwnableProperty(37, banker, "Park Place", 350, 35),
                    new OwnableProperty(39, banker, "Boardwalk", 400, 50)
                ));

            spaces.AddRange(CreateGroupedSpaces(
                    new UtilityProperty(12, banker, "Electric Company", 150, dice),
                    new UtilityProperty(28, banker, "Water Works", 150, dice)
                ));

            spaces.AddRange(CreateGroupedSpaces(
                    new RailRoadProperty(5, banker, "Reading Railroad", 200),
                    new RailRoadProperty(15, banker, "Pennsylvania Railroad", 200),
                    new RailRoadProperty(25, banker, "B&O Railroad", 200),
                    new RailRoadProperty(35, banker, "Short Line", 200)
                ));
        }

        private IEnumerable<ISpace> CreateGroupedSpaces(params IOwnableProperty[] spaces)
        {
            var groupedSpaces = new List<ISpace>();
            foreach (IOwnableProperty space in spaces)
            {
                space.GroupedSpaces = spaces;
                groupedSpaces.Add(space as ISpace);
            }

            return groupedSpaces;
        }
    }
}
