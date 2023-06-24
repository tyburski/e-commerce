using e_commerce.Models;
using e_commerce.Models.Discounts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace e_commerce.Services
{
    internal class ShopService
    {
        private readonly AppDbContext _context;
        private readonly AccountService _accountService;
        private readonly User _user;
        Configuration config = new();

        private Cart cart { get; set; }  = new();
        public ShopService(AppDbContext context, AccountService accountService, User user)
        {
            _context = context;
            _accountService = accountService;
            _user = user;
        }

        public void Menu()
        {
            Console.Clear();
            foreach (var discount in config.activeDiscounts)
            {
                discount.Print();
            }
            Console.WriteLine("\nMenu:\n");
            bool selected = false;
            string[] options = { "Option : 1 [ Shop ]", "Option : 2 [ Cart ]", "Option : 3 [ Logout ]" };
            foreach (var option in options)
            {
                Console.WriteLine(option);
            }
            while (selected == false)
            {
                Console.Write("\nChoose an option: ");
                var selectedOptionString = Console.ReadLine();
                var parse = Int32.TryParse(selectedOptionString, out int selectedOption);

                if (selectedOption <= options.Length)
                {
                    switch (selectedOption)
                    {
                        case 1:
                            selected = true;
                            GetItems();
                            break;
                        case 2:
                            selected = true;
                            GetCart();
                            break;
                        case 3:
                            selected = true;
                            _accountService.Login();
                            break;
                    }
                }
                else Console.WriteLine("\n***We do not support this option");
            }
        }
        public void GetItems()
        {
            Console.Clear();

            Console.WriteLine("Items available:\n");

            var items = _context.Items.Include(x => x.parameters).ToList();
            int i = 1;
            foreach (var item in items)
            {
                Console.WriteLine($"Option : {i}");
                item.parameters.Print();
                Console.WriteLine("\n");
                i++;
            }
            Console.WriteLine($"***Type {i} to return");
            bool selected = false;
            while(selected == false)
            {
                Console.Write("\nChoose an option: ");
                var selectedOptionString = Console.ReadLine();
                var parse = Int32.TryParse(selectedOptionString, out int selectedOption);
                if(parse)
                {
                    if (selectedOption - 1 < items.Count)
                    {
                        selected = true;
                        SelectedItem(items[selectedOption - 1]);
                    }
                    else if ((selectedOption - 1).Equals(items.Count))
                    {
                        selected = true;
                        Menu();
                    }
                    else
                    {
                        Console.WriteLine("\n***We do not support this option");
                    }
                }
                
            }
        }
        public void SelectedItem(Item item)
        {
            Console.Clear();
            item.parameters.Print();

            string[] options = { "\nOption : 1 [ Add to cart ]", "Option : 2 [ Back to available items ]" };
            foreach (string option in options)
            {
                Console.WriteLine(option);
            }

            bool selected = false;

            while(selected == false)
            {
                Console.Write("\nChoose an option: ");
                var selectedOptionString = Console.ReadLine();
                var parse = Int32.TryParse(selectedOptionString, out int selectedOption);

                if (selectedOption <= options.Length)
                {
                    switch (selectedOption)
                    {
                        case 1:
                            selected = true;
                            SetQuantity(item);
                            break;
                        case 2:
                            selected = true;
                            GetItems();
                            break;
                    }
                }
                else Console.WriteLine("***We do not support this option");
            }          
        }
        public void SetQuantity(Item item)
        {
            Console.Clear();
            Console.Write("Enter quantity:");
            var parse = Int32.TryParse(Console.ReadLine(), out int quantity);
            if(parse)
            {
                if (quantity.Equals(0))
                {
                    SelectedItem(item);
                }
                if (quantity > 0)
                {
                    for (int i = 0; i < quantity; i++)
                    {
                        cart.Items.Add(item);
                    }
                    GetCart();
                }
            }          
        }
        public void GetCart()
        {
            Console.Clear();
            
            Console.WriteLine("Cart:\n");
            int i = 1;
            if (cart.Items.Count.Equals(0))
            {
                Console.WriteLine("***There is nothing here");
            }
            else
            {
                foreach(var item in cart.Items)
                {
                    Console.WriteLine($"Option : {i}");
                    item.parameters.Print();
                    Console.WriteLine("\n");
                    i++;
                }
            }
            cart.Build(config.activeDiscounts,config.maxPriority);
            Console.WriteLine($"Regular price: ${String.Format("{0:0.00}",cart.Price)}");
            Console.WriteLine($"Price after discounts: ${String.Format("{0:0.00}", cart.newPrice)}");

            string[] options = { "\nOption : 1 [ Go to available items ]", "Option : 2 [ Return to menu ]", "Option : 3 [ Remove item ]", "Option : 4 [ Confirm order ]"};
            foreach (string option in options)
            {
                Console.WriteLine(option);
            }
            bool selected = false;

            while (selected == false)
            {
                Console.Write("\nChoose an option: ");
                var selectedOptionString = Console.ReadLine();
                var parse = Int32.TryParse(selectedOptionString, out int selectedOption);

                if (selectedOption <= options.Length)
                {
                    switch (selectedOption)
                    {
                        case 1:
                            selected = true;
                            GetItems();
                            break;
                        case 2:
                            selected = true;
                            Menu();
                            break;
                        case 3:
                            selected = true;
                            RemoveItem();
                            break;
                        case 4:
                            selected = true;
                            ConfirmOrder();
                            break;
                    }
                }
                else Console.WriteLine("\n***We do not support this option");
            }
        }
        public void RemoveItem()
        {
            if (cart.Items.Count.Equals(0))
            {
                GetCart();
            }
            else
            {
                bool selected = false;
                while (selected == false)
                {
                    Console.WriteLine($"\n***Type {cart.Items.Count + 1} to return");
                    Console.Write("\nChoose item to remove: ");

                    var parse = Int32.TryParse(Console.ReadLine(), out int selectedOption);
                    if(parse)
                    {
                        if (selectedOption - 1 >= 0 && selectedOption - 1 < cart.Items.Count)
                        {
                            selected = true;
                            cart.Items.RemoveAt(selectedOption - 1);
                            GetCart();
                        }
                        else if (selectedOption - 1 >= 0 && (selectedOption - 1).Equals(cart.Items.Count))
                        {
                            selected = true;
                            GetCart();
                        }
                        else
                        {
                            Console.WriteLine("\n***We do not support this option");
                        }
                    }                   
                }
            }         
        }
        public void ConfirmOrder()
        {
            Console.Clear();
            Console.Write("Processing");
            for (int i = 5; i > 0; i--)
            {
                Console.Write($".");

                Thread.Sleep(500);
            }
            if(cart.Items.Count>0)
            {
                var order = new Order()
                {
                    Items = cart.Items,
                    User = _user
                };
                _context.Orders.Add(order);
                _context.SaveChanges();
                cart.Items.Clear();

                Console.WriteLine("\nYour order has been confirmed\n");
            }
            else Console.WriteLine("\nYour order is empty\n");

            Console.Write("Redirection in: ");

            for (int i = 5; i > 0 ; i--)
            {
                Console.Write($"{i} ");

                Thread.Sleep(1000);
            }
            Menu();
        }
    }
}
