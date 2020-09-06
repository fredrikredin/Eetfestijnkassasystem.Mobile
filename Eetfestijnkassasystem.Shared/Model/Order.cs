using Eetfestijnkassasystem.Shared.Exceptions;
using Eetfestijnkassasystem.Shared.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Eetfestijnkassasystem.Shared.Model
{
    public class Order : IEntity
    {
        public Order()
        {

        }

        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime DateTimeCreated { get; set; }
        public List<MenuItem> Items { get; set; }
        public string Seating { get; set; }
        public string Comment { get; set; }

        //private double _totalCost = 0;
        //private int _numberOfPaymentCards;
        //private double _amountPaidInCash = 0.0f;
        //private float _cashReturn = 0.0f;

        //public int NumberOfPaymentCards
        //{
        //    get { return _numberOfPaymentCards; }
        //    set
        //    {
        //        if (value < 0)
        //            throw new NegativeValueException<Order>(this, nameof(NumberOfPaymentCards), value);

        //        _numberOfPaymentCards = value;
        //    }
        //}

        //public double AmountPaidInCash
        //{
        //    get { return _amountPaidInCash; }
        //    set
        //    {
        //        if (value < 0)
        //            throw new NegativeValueException<Order>(this, nameof(AmountPaidInCash), value);

        //        _amountPaidInCash = value;
        //    }
        //}

        //public float CashReturn
        //{
        //    get { return _cashReturn; }
        //    set
        //    {
        //        value = (value < 0.0f) ? 0.0f : value;
        //        _cashReturn = value;
        //    }
        //}



        //public void RefreshTotalCost()
        //{
        //    if (Items != null && Items.Any())
        //        TotalCost = Items.Sum(i => i.Price);
        //    else
        //        TotalCost = 0;



        //    if (Items != null && Items.Where(o => o != null).Count() > 0)
        //    {
        //        TotalCost = Items.Where(o => o != null).Sum(o => o.Price);
        //    }
        //    else
        //    {
        //        TotalCost = 0.0f;
        //    }
        //}


    }
}
