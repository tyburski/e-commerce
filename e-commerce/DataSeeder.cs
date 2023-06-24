using e_commerce.Models.Items;
using e_commerce.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace e_commerce
{
    public class DataSeeder
    {
        private readonly AppDbContext _context;

        public DataSeeder(AppDbContext context)
        {
            _context = context;
        }
        public void Init()
        {
            if(!_context.Items.Any())
            {
                var initItems = new List<Item>()
                {
                    new Item()
                    {
                        parameters = new WindowParameters()
                        {
                            Type = ItemType.Window,
                            Price = 100.25,
                            Unit = Units.item,
                            Height = 120,
                            Width = 60
                        }
                    },
                    new Item()
                    {
                        parameters = new DoorGasketParameters()
                        {
                            Type = ItemType.DoorGasket,
                            Price = 0.80,
                            Unit = Units.meter,
                            Width = 2
                        }
                    },
                    new Item()
                    {
                        parameters = new FoilParameters()
                        {
                            Type = ItemType.Foil,
                            Price = 3.30,
                            Unit = Units.item,
                            Permeability = 10
                        }
                    },
                    new Item()
                    {
                        parameters = new HandleParameters()
                        {
                            Type = ItemType.Handle,
                            Price = 22.60,
                            Unit = Units.item,
                            Color = Colors.gold
                        }
                    },
                    new Item()
                    {
                        parameters = new HandleParameters()
                        {
                            Type = ItemType.Handle,
                            Price = 24.50,
                            Unit = Units.item,
                            Color = Colors.silver
                        }
                    }
                };

                _context.Items.AddRange(initItems);               
            }
            if(!_context.Users.Any())
            {
                var testUser = new User()
                {
                    Username = "test"
                };
                testUser.SetPassword(BCrypt.Net.BCrypt.HashPassword("test"));
                _context.Users.Add(testUser);
            }
            if (_context.ChangeTracker.HasChanges())
            {
                _context.SaveChanges();
            }
        }
    }
}
