using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace e_commerce.Models.Discounts
{
    public class KitDiscount : Discount
    {
        public KitDiscount(int priority)
        {
            SetPriority(priority);
        }
        public override void Print()
        {
            Notice = "Set discount: -10% on every window + handle set";
            Console.WriteLine(Notice);
        }
        public override double Logic(Cart cart)
        {
            double discount = default;

            foreach (var item in cart.Items)
            {
                if (item.parameters.Type.Equals(ItemType.Window))
                {
                    foreach (var handle in cart.Items)
                    {
                        if (handle.parameters.Type.Equals(ItemType.Handle))
                        {
                            discount += handle.parameters.Price * 0.10;
                            return cart.Price - discount;
                        }
                    }
                }
                return cart.Price;
            }
            return cart.Price;
        }
        public override void SetPriority(int priority)
        {
            base.SetPriority(priority);
        }
    }
}
