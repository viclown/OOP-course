using System;
using System.Collections.Generic;
using NUnit.Framework;
using Shops.Services;
using Shops.Tools;

namespace Shops.Tests
{
    public class Tests
    {
        private ShopManager _shopManager;
        private Provider _provider;

        [SetUp]
        public void Setup()
        {
            var shopManager = new ShopManager();
            var provider = new Provider();
            Shop magnit = shopManager.AddShop(new Shop("Magnit", "zarechnaya 17", 500000));
            Shop fasol = shopManager.AddShop(new Shop("Fasol", "zarechnaya 18", 200000));
            _shopManager = shopManager;
            _provider = provider;
        }

        [Test]
        public void ProvideProductsToShop_ShopHasProducts()
        {
            Shop magnit = _shopManager.FindShop("Magnit");
            Product milkProvider = _provider.AddProduct(new Product("Milk", 100, 45));
            Product milkMagnit = _provider.ProvideProductToShop(milkProvider, 20, 50, magnit);
            Assert.AreEqual(milkMagnit, magnit.FindProduct("Milk"));
        }

        [Test]
        public void SetAndChangePriceToProduct()
        {
            Shop magnit = _shopManager.FindShop("Magnit");
            Product breadProvider = _provider.AddProduct(new Product("Bread", 100, 25));
            Product breadMagnit = _provider.ProvideProductToShop(breadProvider, 50, 50, magnit);
            Assert.AreEqual(breadMagnit, magnit.FindProduct("Bread"));
            Assert.IsTrue(breadMagnit.ShopPrice == 50);
            magnit.ChangePrice(magnit.FindProduct("Bread"), 52);
            Assert.AreEqual(breadMagnit, magnit.FindProduct("Bread"));
            Assert.IsTrue(breadMagnit.ShopPrice == 52);
        }

        [Test]
        public void NoPossibilityToFindFullProductsConsignmentInAnyShops_ThrowException()
        {
            Shop magnit = _shopManager.FindShop("Magnit");
            Shop fasol = _shopManager.FindShop("Fasol");
            Product juiceProvider = _provider.AddProduct(new Product("Juice", 100, 25));
            Product juiceMagnit = _provider.ProvideProductToShop(juiceProvider, 50, 50, magnit);
            Product juiceFasol = _provider.ProvideProductToShop(juiceProvider, 30, 20, fasol);
            Product meatProvider = _provider.AddProduct(new Product("Meat", 20, 150));
            Product meatMagnit = _provider.ProvideProductToShop(meatProvider, 10, 250, magnit);
            Product meatFasol = _provider.ProvideProductToShop(meatProvider, 10, 270, fasol);
            Product beerProvider = _provider.AddProduct(new Product("Beer", 20, 50));
            Product beerMagnit = _provider.ProvideProductToShop(beerProvider, 10, 80, magnit);
            Product eggsProvider = _provider.AddProduct(new Product("Eggs", 30, 50));
            Product eggsFasol = _provider.ProvideProductToShop(eggsProvider, 8, 80, fasol);
            var consignment = new List<(string, int)>
            {
                new ("Juice", 2),
                new ("Eggs", 1),
                new ("Meat", 1),
                new ("Beer", 10),
            };
            Assert.Catch<Exception>(() =>
            {
                Shop shop = _shopManager.FindShopWithCheapProducts(consignment);
            });
        }

        [Test]
        public void BuyProductsConsignmentInShop()
        {
            Shop magnit = _shopManager.FindShop("Magnit");
            Shop fasol = _shopManager.FindShop("Fasol");
            Product juiceProvider = _provider.AddProduct(new Product("Juice", 100, 25));
            Product juiceMagnit = _provider.ProvideProductToShop(juiceProvider, 50, 50, magnit);
            Product juiceFasol = _provider.ProvideProductToShop(juiceProvider, 30, 20, fasol);
            Product beerProvider = _provider.AddProduct(new Product("Beer", 30, 50));
            Product beerMagnit = _provider.ProvideProductToShop(beerProvider, 10, 80, magnit);
            Product beerFasol = _provider.ProvideProductToShop(beerProvider, 20, 75, fasol);
            Product eggsProvider = _provider.AddProduct(new Product("Eggs", 30, 50));
            Product eggsMagnit = _provider.ProvideProductToShop(eggsProvider, 10, 80, magnit);
            Product eggsFasol = _provider.ProvideProductToShop(eggsProvider, 8, 79, fasol);
            var consignment = new List<(string, int)>
            {
                new ("Juice", 2),
                new ("Eggs", 1),
                new ("Beer", 10),
            };
            Shop theCheapestShop = _shopManager.FindShopWithCheapProducts(consignment);
            var masha = new Person("Masha", 20000);
            float moneyBefore = masha.Money;
            Assert.IsTrue(masha.Products.Count == 0);
            masha.PersonBuyProducts(consignment, theCheapestShop);
            Assert.IsTrue(masha.Products.Count == 3);
            Assert.IsTrue(theCheapestShop == fasol);
            Assert.IsTrue(masha.Money == (moneyBefore - 2 * juiceFasol.ShopPrice - eggsFasol.ShopPrice - 10 * beerFasol.ShopPrice));
        }
    }
}