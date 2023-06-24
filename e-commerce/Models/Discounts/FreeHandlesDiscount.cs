using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace e_commerce.Models.Discounts
{
    public class FreeHandlesDiscount : Discount
    {
        public FreeHandlesDiscount(int priority)
        {
            SetPriority(priority);
        }
        public override void Print()
        {
            Notice = "Buy at least 3 windows - get free handle for each!";
            Console.WriteLine(Notice);
        }
        public override double Logic(Cart cart)
        {
            int counter = 0;
            var handles = cart.Items.Where(x => x.parameters.Type.Equals(ItemType.Handle)).ToList();
            for (int i = 0; i < cart.Items.Count; i++)
            {
                if (cart.Items[i].parameters.Type.Equals(ItemType.Window))
                {
                    counter++;
                }
            }
            if (counter < 3) return cart.Price;
            int k = (counter / 3 - counter % 3);

            if(counter > 0 && handles.Count >= counter)
            {
                double discount = 0;
                for (int j = 0; j < k * 3; j++)
                {
                    discount += handles[j].parameters.Price;
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
