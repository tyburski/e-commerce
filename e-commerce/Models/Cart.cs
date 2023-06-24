using e_commerce.Models.Discounts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace e_commerce.Models
{
    public class Cart
    {
        public int Id { get; set; }

        public List<Item> Items { get; set; } = new();

        public double Price { get; private set; }
        public double newPrice { get; private set; }

        public void Build(List<Discount> discounts, int maxPriority)
        {
            Price = default;
            newPrice = default;
            foreach (var item in Items)
            {              
                Price += item.parameters.Price;
                newPrice = Price;               
            }
            for (int i = maxPriority; i > 0 ; i--)
            {
                var result = discounts.FirstOrDefault(x => x.Priority.Equals(i));

                var price = result.Logic(this);
                if (!price.Equals(Price))
                {
                    newPrice = price;
                    break;
                }
            }
        }
    }
}
