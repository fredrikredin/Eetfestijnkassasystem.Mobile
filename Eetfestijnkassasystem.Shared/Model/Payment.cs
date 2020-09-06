using Eetfestijnkassasystem.Shared.Exceptions;
using Eetfestijnkassasystem.Shared.Interface;

namespace Eetfestijnkassasystem.Shared.Model
{
    public class Payment : EntityBase, IEntity
    {
        private double _amountCashPaid;
        private double _amountCashReturn;
        private int _numberOfPaymentCards;

        public Payment() : base() { }

        public double AmountCashPaid
        {
            get { return _amountCashPaid; }
            set
            {
                if (value < 0)
                    throw new NegativeValueException<Payment>(this, nameof(AmountCashPaid), value);

                _amountCashPaid = value;
            }
        }

        public double AmountCashReturn
        {
            get { return _amountCashReturn; }
            set
            {
                if (value < 0)
                    throw new NegativeValueException<Payment>(this, nameof(AmountCashReturn), value);

                _amountCashReturn = value;
            }
        }

        public int NumberOfPaymentCards
        {
            get { return _numberOfPaymentCards; }
            set
            {
                if (value < 0)
                    throw new NegativeValueException<Payment>(this, nameof(NumberOfPaymentCards), value);

                _numberOfPaymentCards = value;
            }
        }
    }
}