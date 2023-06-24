using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace e_commerce.Models.Items
{
    public class Parameters
    {
        public int Id { get; set; }
        public ItemType Type { get; set; }
        public double Price { get; set; }
        public string Unit { get; set; }

        public virtual void Print()
        {
            Console.WriteLine($"Type: {Type}");
            Console.WriteLine($"Price: ${Price}/{Unit}");
        }

    }
}
