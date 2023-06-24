using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace e_commerce.Models.Discounts
{
    public class Discount
    {
        public int Id { get; set; }
        public string Notice { get; set; }

        public int Priority { get; private set; }
        public virtual void Print()
        {
            Console.WriteLine(Notice);
        }
        public virtual double Logic(Cart cart)
        {
            return default;
        }
        public virtual void SetPriority(int priority)
        {
            if (priority > 0)
            {
                Priority = priority;
            }
            else Priority = 0;
        }
    }
}
