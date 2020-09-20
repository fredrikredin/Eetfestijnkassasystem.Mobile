using System;
using Eetfestijnkassasystem.Shared.Interface;

namespace Eetfestijnkassasystem.Shared.Abstract
{
    public abstract class EntityExceptionBase : Exception, IEntityException
    {
        public EntityExceptionBase(string model, string property, string message = null) : base(message)
        {
            Model = model;
            Property = property;
        }

        public string Model { get; set; }
        public string Property { get; set; }
        public string Type { get; set; }
    }
}
