// See https://aka.ms/new-console-template for more information
using e_commerce;
using e_commerce.Models;
using e_commerce.Models.Discounts;
using e_commerce.Models.Items;
using e_commerce.Services;
using Microsoft.EntityFrameworkCore;

var context = new AppDbContext();
var seeder = new DataSeeder(context);
var accountService = new AccountService(context);

seeder.Init();
accountService.Login();









