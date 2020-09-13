using Eetfestijnkassasystem.Shared.Abstract;
using Eetfestijnkassasystem.Shared.Interface;
using Eetfestijnkassasystem.Shared.Model;

namespace Eetfestijnkassasystem.Shared.DTO
{
    public class Payment : EntityBase, ITransferObjectFor<PaymentModel>
    {
        public double AmountCashPaid { get; set; }
        public double AmountCashReturn { get; set; }
        public int NumberOfPaymentCards { get; set; }

        public PaymentModel ToModelEntity()
        {
            return new PaymentModel()
            {
                AmountCashPaid = this.AmountCashPaid,
                AmountCashReturn = this.AmountCashReturn,
                NumberOfPaymentCards = this.NumberOfPaymentCards,
            };
        }
    }
}
