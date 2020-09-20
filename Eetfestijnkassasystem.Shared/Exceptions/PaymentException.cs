using Eetfestijnkassasystem.Shared.Abstract;

namespace Eetfestijnkassasystem.Shared.Exceptions
{
    public class PaymentException : EntityExceptionBase
    {
        public PaymentException(string model, string property, string message) : base(model, property, message)
        {
            Type = $"{GetType().Name}: {nameof(Model)}={Model}, {nameof(Property)}={Property}, {nameof(Message)}={Message}";
        }
    }
}
