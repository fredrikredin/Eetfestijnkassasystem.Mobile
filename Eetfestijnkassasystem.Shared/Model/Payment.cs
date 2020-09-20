using Eetfestijnkassasystem.Shared.Abstract;
using Eetfestijnkassasystem.Shared.DTO;
using Eetfestijnkassasystem.Shared.Exceptions;

namespace Eetfestijnkassasystem.Shared.Model
{
    public class Payment : EntityBase
    {
        private double totalCost_;
        private double _amountCashPaid;
        private double _amountCashReturn;
        private int _numberOfPaymentCards;

        public Payment() : base() { }

        public double TotalCost
        {
            get { return totalCost_; }
            set
            {
                if (value < 0)
                    throw new NegativeValueException(nameof(PaymentDto), nameof(TotalCost), value);

                totalCost_ = value;
            }
        }

        public double AmountCashPaid
        {
            get { return _amountCashPaid; }
            set
            {
                if (value < 0)
                    throw new NegativeValueException(nameof(PaymentDto), nameof(AmountCashPaid), value);

                _amountCashPaid = value;
            }
        }

        public double AmountCashReturn
        {
            get { return _amountCashReturn; }
            set
            {
                if (value < 0)
                    throw new NegativeValueException(nameof(PaymentDto), nameof(AmountCashReturn), value);

                _amountCashReturn = value;
            }
        }

        public int NumberOfPaymentCards
        {
            get { return _numberOfPaymentCards; }
            set
            {
                if (value < 0)
                    throw new NegativeValueException(nameof(PaymentDto), nameof(NumberOfPaymentCards), value);

                _numberOfPaymentCards = value;
            }
        }
    }
}