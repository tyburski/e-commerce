using e_commerce.Models.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace e_commerce.Models
{
    internal class Item
    {
        public int Id { get; set; }
        public Parameters parameters { get; set; }
    }
}
