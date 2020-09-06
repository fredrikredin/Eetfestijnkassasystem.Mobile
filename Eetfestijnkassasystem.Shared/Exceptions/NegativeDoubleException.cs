using System;
using Eetfestijnkassasystem.Shared.Interface;

namespace Eetfestijnkassasystem.Shared.Exceptions
{
    public class NegativeDoubleException<T> : Exception, IEntityException where T : IEntity
    {
        public NegativeDoubleException(T entity, string property, double value)
        {
            Entitiy = entity;
            Property = property;
            Value = value;
        }

        public T Entitiy { get; set; }
        public string Property { get; set; }
        public double Value { get; set; }

        public string Type => 
            $"{GetType().Name.Replace("`1", "")}<{typeof(T).Name}>({nameof(Property)}={Property}, {nameof(Value)}={Value})";
    }
}