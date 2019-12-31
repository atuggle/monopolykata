using System;
using System.Collections.Generic;
using System.Linq;

namespace MonopolyKata
{
    public class OwnableProperty : TransactionalProperty, IOwnableProperty
    {
        private List<IOwnableProperty> groupedSpaces = new List<IOwnableProperty>();
        public IPlayer Owner { get; protected set; }

        public IEnumerable<IOwnableProperty> GroupedSpaces
        {
            get { return groupedSpaces; }
            set { groupedSpaces.AddRange(value); }
        }

        public OwnableProperty(Int32 boardLocation, IBanker bank, String name, Int32 price, Int32? rent)
            : base(boardLocation, bank, name, price, rent) { }

        public override void Visit(IPlayer player)
        {
            base.Visit(player);
        }

        protected Int32 NumberOfPropertiesOwned()
        {
            return GroupedSpaces.Count(s => this.Owner != null && this.Owner.Equals(s.Owner));
        }

        protected Boolean OwnerOwnsAllProperties()
        {
            return !GroupedSpaces.Any(p => !this.Owner.Equals(p.Owner)); ;
        }

        public override void Land(IPlayer player)
        {
            base.Land(player);

            var playerBalance = banker.GetPlayerBalance(player);

            if (Owner == null && playerBalance >= Price)
                PurchaseProperty(player);
            else if (Owner != null && !player.Equals(Owner))
                PayRent(player);
        }

        private void PurchaseProperty(IPlayer player)
        {
            Owner = player;
            banker.Debit(player, Price);
            player.AddProperty(this);
        }

        private void PayRent(IPlayer player)
        {
            var payment = CalculatePayment(player);
            banker.SubmitPayment(player, Owner, payment);
        }

        public virtual Int32 CalculatePayment(IPlayer player) 
        {
            var rentOwed = Rent.GetValueOrDefault(0);
            if (OwnerOwnsAllProperties())
                rentOwed *= 2;

            return rentOwed;
        }
    }
}
