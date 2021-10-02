using System;
using System.Collections.Generic;
using Shops.Services;

namespace Shops
{
    internal class Program
    {
        private static void Main()
        {
            var shopManager = new ShopManager();
            var provider = new Provider();
            Shop magnit = shopManager.AddShop(new Shop("Magnit", "zarechnaya 17", 500000));
            Shop fasol = shopManager.AddShop(new Shop("Fasol", "zarechnaya 18", 200000));
            var masha = new Person("Masha", 20000);
            Product milkProvider = provider.AddProduct(new Product("Milk", 100, 45));
            Product milkMagnit1 = provider.ProvideProductToShop("Milk", 20, 50, magnit);
            Product milkFasol = provider.ProvideProductToShop("Milk", 20, 40, fasol);
            masha.PersonBuyProduct("Milk", 20, magnit);
            Product milkMagnit2 = provider.ProvideProductToShop("Milk", 20, 50, magnit);
            Product breadProvider = provider.AddProduct(new Product("Bread", 100, 25));
            Product breadMagnit = provider.ProvideProductToShop("Bread", 40, 50, magnit);
            Product breadFasol = provider.ProvideProductToShop("Bread", 20, 20, fasol);
            Product meatProvider = provider.AddProduct(new Product("Meat", 20, 150));
            Product meatMagnit = provider.ProvideProductToShop("Meat", 10, 250, magnit);
            Product meatFasol = provider.ProvideProductToShop("Meat", 10, 270, fasol);
            var consignment = new List<(string, int)>
            {
                new ("Milk", 2),
                new ("Bread", 1),
                new ("Meat", 1),
            };

            // masha.PersonBuyProducts(consignment, magnit);
            // Shop cheapShop = shopManager.FindShopWithCheapProducts(consignment);
            // if (cheapShop.Name == "Fasol")
                // Console.Write("fasooool");
        }
    }
}
