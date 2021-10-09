using System;
using System.Collections.Generic;
using NUnit.Framework;
using Shops.Classes;
using Shops.Services;
using Shops.Tools;

namespace Shops.Tests
{
    public class Tests
    {
        private ShopManager _shopManager;

        [SetUp]
        public void Setup()
        {
            var shopManager = new ShopManager();
            Shop magnit = shopManager.AddShop(new Shop("Magnit", "zarechnaya 17", 500000));
            Shop fasol = shopManager.AddShop(new Shop("Fasol", "zarechnaya 18", 200000));
            _shopManager = shopManager;
        }

        [Test]
        public void ProvideProductsToShop_ShopHasProducts()
        {
            Shop magnit = _shopManager.FindShop("Magnit");
            Product milkProvider = _shopManager.AddProduct(new Product("Milk", 100));
            ShopProduct milkMagnit = _shopManager.ProvideProductToShop(milkProvider, 20, 50, magnit);
            Assert.AreEqual(milkMagnit, magnit.FindProduct("Milk"));
        }

        [Test]
        public void SetAndChangePriceToProduct()
        {
            Shop magnit = _shopManager.FindShop("Magnit");
            Product breadProvider = _shopManager.AddProduct(new Product("Bread", 100));
            ShopProduct breadMagnit = _shopManager.ProvideProductToShop(breadProvider, 50, 50, magnit);
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
            Product juiceProvider = _shopManager.AddProduct(new Product("Juice", 100));
            ShopProduct juiceMagnit = _shopManager.ProvideProductToShop(juiceProvider, 50, 50, magnit);
            ShopProduct juiceFasol = _shopManager.ProvideProductToShop(juiceProvider, 30, 20, fasol);
            Product meatProvider = _shopManager.AddProduct(new Product("Meat", 20));
            ShopProduct meatMagnit = _shopManager.ProvideProductToShop(meatProvider, 10, 250, magnit);
            ShopProduct meatFasol = _shopManager.ProvideProductToShop(meatProvider, 10, 270, fasol);
            Product beerProvider = _shopManager.AddProduct(new Product("Beer", 20));
            ShopProduct beerMagnit = _shopManager.ProvideProductToShop(beerProvider, 10, 80, magnit);
            Product eggsProvider = _shopManager.AddProduct(new Product("Eggs", 30));
            ShopProduct eggsFasol = _shopManager.ProvideProductToShop(eggsProvider, 8, 80, fasol);
            var consignment = new List<ProductToBuy>
            {
                new ProductToBuy("Juice", 2),
                new ProductToBuy("Eggs", 1),
                new ProductToBuy("Meat", 1),
                new ProductToBuy("Beer", 10),
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
            Product juiceProvider = _shopManager.AddProduct(new Product("Juice", 100));
            ShopProduct juiceMagnit = _shopManager.ProvideProductToShop(juiceProvider, 50, 50, magnit);
            ShopProduct juiceFasol = _shopManager.ProvideProductToShop(juiceProvider, 30, 20, fasol);
            Product beerProvider = _shopManager.AddProduct(new Product("Beer", 30));
            ShopProduct beerMagnit = _shopManager.ProvideProductToShop(beerProvider, 10, 80, magnit);
            ShopProduct beerFasol = _shopManager.ProvideProductToShop(beerProvider, 20, 75, fasol);
            Product eggsProvider = _shopManager.AddProduct(new Product("Eggs", 30));
            ShopProduct eggsMagnit = _shopManager.ProvideProductToShop(eggsProvider, 10, 80, magnit);
            ShopProduct eggsFasol = _shopManager.ProvideProductToShop(eggsProvider, 8, 79, fasol);
            var consignment = new List<ProductToBuy>
            {
                new ProductToBuy("Juice", 2),
                new ProductToBuy("Eggs", 1),
                new ProductToBuy("Beer", 10),
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