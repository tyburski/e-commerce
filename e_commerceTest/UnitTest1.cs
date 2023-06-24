using e_commerce.Models.Items;
using e_commerce.Models;
using e_commerce;

namespace e_commerceTest
{
    public class UnitTest1
    {
        [Fact]
        public async Task FreeHandlesDiscount_Test()
        {
            var windowItem = new Item()
            {
                parameters = new WindowParameters()
                {
                    Type = ItemType.Window,
                    Price = 100.25,
                    Unit = Units.item,
                    Height = 120,
                    Width = 60
                }
            };

            var handleItem = new Item()
            {
                parameters = new HandleParameters()
                {
                    Type = ItemType.Handle,
                    Price = 22.60,
                    Unit = Units.item,
                    Color = Colors.gold
                }
            };

            var cart = new Cart();
            for (int i = 0; i < 3; i++)
            {
                cart.Items.Add(windowItem);
                cart.Items.Add(handleItem);
            }
            var config = new Configuration();
            await Task.Run(() => cart.Build(config.activeDiscounts, config.maxPriority));

            Assert.Equal(300.75, cart.newPrice);
        }
        [Fact]

        public async Task KitDiscount_Test()
        {
            var windowItem = new Item()
            {
                parameters = new WindowParameters()
                {
                    Type = ItemType.Window,
                    Price = 100.25,
                    Unit = Units.item,
                    Height = 120,
                    Width = 60
                }
            };

            var handleItem = new Item()
            {
                parameters = new HandleParameters()
                {
                    Type = ItemType.Handle,
                    Price = 22.60,
                    Unit = Units.item,
                    Color = Colors.gold
                }
            };

            var cart = new Cart();
            cart.Items.Add(windowItem);
            cart.Items.Add(handleItem);

            var config = new Configuration();
            await Task.Run(() => cart.Build(config.activeDiscounts, config.maxPriority));

            Assert.Equal(110.565, cart.newPrice);
        }
        [Theory]
        [InlineData(1, 18.08)]
        [InlineData(3, 63.28)]
        public async Task CurrentWeekHandleDiscount_Test(int count, double expected)
        {
            var handleItem = new Item()
            {
                parameters = new HandleParameters()
                {
                    Type = ItemType.Handle,
                    Price = 22.60,
                    Unit = Units.item,
                    Color = Colors.gold
                }
            };

            var cart = new Cart();
            for (int i = 0; i < count; i++)
            {
                cart.Items.Add(handleItem);
            }
            var config = new Configuration();
            await Task.Run(() => cart.Build(config.activeDiscounts, config.maxPriority));

            Assert.Equal(expected, cart.newPrice);
        }
    }

   
}