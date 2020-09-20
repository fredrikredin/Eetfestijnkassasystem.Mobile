using Eetfestijnkassasystem.Shared.Abstract;

namespace Eetfestijnkassasystem.Shared.Exceptions
{
    public class NegativeValueException : EntityExceptionBase
    {
        public NegativeValueException(string model, string property, double value) : base(model, property)
        {
            Value = value;
            Type = $"{GetType().Name}: {nameof(Model)}={Model}, {nameof(Property)}={Property}, {nameof(Value)}={Value}";
        }

        public double Value { get; set; }
    }
}