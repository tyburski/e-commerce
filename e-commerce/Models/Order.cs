using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace e_commerce.Models
{
    internal class Order
    {
        public int Id { get; set; }
        public User User { get; set; }
        public List<Item> Items { get; set; }
    }
}
