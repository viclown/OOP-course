using System.Collections.Generic;
using Shops.Tools;

namespace Shops.Services
{
    public class ShopManager
    {
        private List<Shop> Shops { get; } = new List<Shop>();

        public Shop AddShop(Shop shop)
        {
            Shops.Add(shop);
            return shop;
        }

        public Shop FindShop(string shopToFind)
        {
            foreach (Shop shop in Shops)
            {
                if (shop.Name == shopToFind)
                    return shop;
            }

            throw new ShopDoesNotExistException();
        }

        public Shop GetShop(string shopToFind)
        {
            Shop shop = FindShop(shopToFind);
            if (shop != null)
                return shop;
            throw new ShopDoesNotExistException();
        }

        public Shop FindShopWithCheapProducts(List<(string, int)> products)
        {
            float minimumPrice = 90000000;
            Shop theCheapestShop = null;
            var productsToBuy = new List<(Product, int)>();

            foreach (Shop shop in Shops)
            {
                float priceForProducts = 0;
                var theCheapestProducts = new List<(Product, int)>();

                foreach ((string name, int quantity) in products)
                {
                    Product product = shop.FindProduct(name);
                    if (product != null && product.Quantity >= quantity)
                    {
                        theCheapestProducts.Add((product, quantity));
                        priceForProducts += product.ShopPrice;
                    }
                }

                if (priceForProducts < minimumPrice && products.Count == theCheapestProducts.Count)
                {
                    productsToBuy = theCheapestProducts;
                    theCheapestShop = shop;
                    minimumPrice = priceForProducts;
                }
            }

            if (theCheapestShop == null)
                throw new NoAffordableProductsNowException();

            return theCheapestShop;
        }

        public Shop FindShopWithCheapProduct(string name, int quantity)
        {
            var productToList = new List<(string, int)> { (name, quantity) };
            return FindShopWithCheapProducts(productToList);
        }
    }
}