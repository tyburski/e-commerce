using e_commerce.Models.Discounts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace e_commerce
{
    internal class Configuration
    {
        private static string Server = "(localdb)\\MSSQLLocalDB";
        private static string Database = "E-CommerceDB";

        public string ConnectionString = $"Server={Server};Database={Database};Trusted_Connection=True;";

        public List<Discount> activeDiscounts = new List<Discount>()
        {
            new CurrentWeekHandleDiscount(1),
            new FreeHandlesDiscount(3),
            new KitDiscount(2)
        };
        public int maxPriority { get; } = 3;
    }
}
