using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace e_commerce.Models.Items
{
    public class FoilParameters : Parameters
    {
        public int Permeability { get; set; }

        public override void Print()
        {
            base.Print();
            Console.WriteLine($"Permeability: {Permeability}%");
        }
    }
}
