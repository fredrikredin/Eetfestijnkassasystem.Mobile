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
            Type = $"{GetType().Name.Replace("`1", "")}: {nameof(Model)}={Model}, {nameof(Property)}={Property}";
        }
    }
}