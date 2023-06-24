using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace e_commerce.Models.Items
{
    internal class HandleParameters : Parameters
    {
        public string Color { get; set; }

        public override void Print()
        {
            base.Print();
            Console.WriteLine($"Color: {Color}");
        }
    }
}
