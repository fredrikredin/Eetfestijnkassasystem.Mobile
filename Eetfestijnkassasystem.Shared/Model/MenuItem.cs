using System;
using Eetfestijnkassasystem.Shared.Exceptions;
using Eetfestijnkassasystem.Shared.Interface;

namespace Eetfestijnkassasystem.Shared.Model
{
    public class MenuItem : IEntity
    {
        private double _cost = 0.0;
        private string _name = null;

        public MenuItem() { }

        public int Id { get; set; }
        public DateTime DateTimeCreated { get; set; }

        public string Name
        {
            get { return _name; }
            set 
            {
                if (String.IsNullOrWhiteSpace(value))
                    throw new EmptyStringException<MenuItem>(this, nameof(Name));

                _name = value;
            }
        }

        public double Cost 
        {
            get { return _cost; }
            set 
            {
                if (value < 0)
                    throw new NegativeDoubleException<MenuItem>(this, nameof(Cost), value);

                _cost = value;
            }
        }
    }
}
