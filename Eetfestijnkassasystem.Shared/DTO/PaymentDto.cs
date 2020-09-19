﻿using Eetfestijnkassasystem.Shared.Abstract;

namespace Eetfestijnkassasystem.Shared.DTO
{
    public class PaymentDto : EntityBase 
    {
        public double AmountCashPaid { get; set; }
        public double AmountCashReturn { get; set; }
        public int NumberOfPaymentCards { get; set; }
    }
}
