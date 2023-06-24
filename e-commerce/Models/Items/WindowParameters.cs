using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace e_commerce.Models.Items
{
    public class WindowParameters : Parameters
    {
        public int Height { get; set; }
        public int Width { get; set; }

        public override void Print()
        {
            base.Print();
            Console.WriteLine($"Dimensions: {Height}cm/{Width}cm");
        }
    }
}
