using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace e_commerce.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; } = String.Empty;
        public string Password { get; private set; } = String.Empty;
        public List<Order> Orders { get; set; }

        public void SetPassword(string newPassword)
        {
            if(Password.Equals(String.Empty))
            {
                Password = newPassword;
            }
            else throw new Exception("Password has already been set");
        }
    }
}
