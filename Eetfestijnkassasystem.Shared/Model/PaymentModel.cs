using Eetfestijnkassasystem.Shared.Abstract;
using Eetfestijnkassasystem.Shared.DTO;
using Eetfestijnkassasystem.Shared.Exceptions;
using Eetfestijnkassasystem.Shared.Interface;

namespace Eetfestijnkassasystem.Shared.Model
{
    public class PaymentModel : EntityBase, IModelFor<Payment>
    {
        private double _amountCashPaid;
        private double _amountCashReturn;
        private int _numberOfPaymentCards;

        public PaymentModel() : base() { }

        public double AmountCashPaid
        {
            get { return _amountCashPaid; }
            set
            {
                if (value < 0)
                    throw new NegativeValueException(nameof(Payment), nameof(AmountCashPaid), value);

                _amountCashPaid = value;
            }
        }

        public double AmountCashReturn
        {
            get { return _amountCashReturn; }
            set
            {
                if (value < 0)
                    throw new NegativeValueException(nameof(Payment), nameof(AmountCashReturn), value);

                _amountCashReturn = value;
            }
        }

        public int NumberOfPaymentCards
        {
            get { return _numberOfPaymentCards; }
            set
            {
                if (value < 0)
                    throw new NegativeValueException(nameof(Payment), nameof(NumberOfPaymentCards), value);

                _numberOfPaymentCards = value;
            }
        }

        public Payment ToTransferObject()
        {
            return new Payment()
            {
                Id = this.Id,
                DateTimeCreated = this.DateTimeCreated,
                AmountCashPaid = this.AmountCashPaid,
                AmountCashReturn = this.AmountCashReturn,
                NumberOfPaymentCards = this.NumberOfPaymentCards,
            };
        }
    }
}