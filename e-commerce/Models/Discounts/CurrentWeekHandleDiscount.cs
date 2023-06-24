using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace e_commerce.Models.Discounts
{
    internal class CurrentWeekHandleDiscount : Discount
    {
        public CurrentWeekHandleDiscount(int priority)
        {
            SetPriority(priority);
        }
        private DateTime _thisWeekEnd { get; set; }
        public override void Print()
        {
            DateTime baseDate = DateTime.Now;
            var thisWeekStart = baseDate.AddDays(-(int)baseDate.DayOfWeek);
            var thisWeekEnd = thisWeekStart.Date.AddDays(7).AddHours(23).AddMinutes(59).AddSeconds(59);
            _thisWeekEnd = thisWeekEnd;
            Notice = $"Until {_thisWeekEnd}: Get -20% on every handle";

            base.Print();
        }
        public override double Logic(Cart cart)
        {
            double discount = default;
            if(DateTime.Now <= _thisWeekEnd)
            {
                foreach(var item in cart.Items)
                {
                    if(item.parameters.Type.Equals(ItemType.Handle))
                    {
                        discount = item.parameters.Price * 0.20;
                    }
                }
                return cart.Price - discount;
            }
            return cart.Price;
        }
        public override void SetPriority(int priority)
        {
            base.SetPriority(priority);
        }
    }
}
