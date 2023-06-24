// See https://aka.ms/new-console-template for more information
using e_commerce;
using e_commerce.Migrations;
using e_commerce.Models;
using e_commerce.Models.Discounts;
using e_commerce.Models.Items;
using e_commerce.Services;
using Microsoft.EntityFrameworkCore;

var context = new AppDbContext();
var seeder = new DataSeeder(context);

seeder.Init();
var accountService = new AccountService(context);

var user = accountService.Login();
if(user!=null)
{
    new ShopService(context, accountService, user).Menu();
}









