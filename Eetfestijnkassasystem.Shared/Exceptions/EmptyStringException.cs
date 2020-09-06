using System;
using Eetfestijnkassasystem.Shared.Interface;

namespace Eetfestijnkassasystem.Shared.Exceptions
{
    public class EmptyStringException<T> : Exception, IEntityException where T : IEntity
    {
        public EmptyStringException(T entity, string property)
        {
            Entity = entity;
            Property = property;
        }

        public T Entity { get; set; }
        public string Property { get; set; }

        public string Type => 
            $"{GetType().Name.Replace("`1", "")}<{typeof(T).Name}>({ nameof(Property)}={Property})";
    }
}