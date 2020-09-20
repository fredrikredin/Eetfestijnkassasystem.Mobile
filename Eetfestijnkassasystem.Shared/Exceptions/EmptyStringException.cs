using System;
using System.Linq;
using Eetfestijnkassasystem.Shared.Abstract;
using Eetfestijnkassasystem.Shared.Interface;

namespace Eetfestijnkassasystem.Shared.Exceptions
{
    public class EmptyStringException : EntityExceptionBase
    {
        public EmptyStringException(string model, string property) : base(model, property)
        {
            Type = $"{GetType().Name}: {nameof(Model)}={Model}, {nameof(Property)}={Property}";
        }
    }
}