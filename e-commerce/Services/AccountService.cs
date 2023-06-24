using Azure.Identity;
using e_commerce.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace e_commerce.Services
{
    public class AccountService 
    {
        private readonly AppDbContext _context;
        public AccountService(AppDbContext context)
        {
            _context = context;
        }
        public string HidePassword()
        {
            ConsoleKeyInfo key;
            string code = "";
            do
            {             
                key = Console.ReadKey(true);
                if(key.Key!=ConsoleKey.Enter)
                {
                    Console.Write("*");
                    code += key.KeyChar;
                }
                
            } while (key.Key != ConsoleKey.Enter);
            return code;

        }
        public User Login()
        {
            Console.Clear();
            int i = 0;

            while(i < 5)
            {
                if(!i.Equals(0))
                {
                    
                    Console.Clear();
                    Console.WriteLine("***Invalid Login Attempt\n");
                }
                
                Console.Write("Username: ");
                var username = Console.ReadLine();
                Console.Write("Password: ");
                var password = HidePassword();
                Console.WriteLine("\n");

                var user = _context.Users.FirstOrDefault(x => x.Username.Equals(username));
                if (user != null)
                {
                    if (BCrypt.Net.BCrypt.Verify(password, user.Password))
                    {
                        new ShopService(_context, this, user).Menu();
                        return user;
                    }
                    else { i++; return null; };
                }
                else { i++; return null; };
            }
            Console.WriteLine("***The allowed number of login attempts has been exceeded");
            return null;
            
        }
    }
}
