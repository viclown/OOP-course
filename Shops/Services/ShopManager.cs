using System.Collections.Generic;
using Shops.Classes;
using Shops.Tools;

namespace Shops.Services
{
    public class ShopManager
    {
        private List<Shop> _shops = new List<Shop>();
        private List<Product> _providerProductsList = new List<Product>();

        public Shop AddShop(Shop shop)
        {
            _shops.Add(shop);
            return shop;
        }

        public Shop FindShop(string shopToFind)
        {
            Shop shop = _shops.Find(shop => shop.Name.Equals(shopToFind));
            return shop;
        }

        public Shop GetShop(string shopToFind)
        {
            Shop shop = FindShop(shopToFind);
            if (shop == null)
                throw new ShopDoesNotExistException($"Shop with name {shopToFind} does not exist");
            return shop;
        }

        public Shop FindShopWithCheapProducts(List<ProductToBuy> products)
        {
            float minimumPrice = 90000000;
            Shop theCheapestShop = null;
            var productsToBuy = new List<ProductToBuy>();

            foreach (Shop shop in _shops)
            {
                float priceForProducts = 0;
                var theCheapestProducts = new List<ProductToBuy>();

                foreach (ProductToBuy productToBuy in products)
                {
                    ShopProduct product = shop.FindProduct(productToBuy.Name);
                    if (product != null && product.Quantity >= productToBuy.Quantity)
                    {
                        theCheapestProducts.Add(productToBuy);
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
                throw new NoAffordableProductsNowException($"No shop has all the products");

            return theCheapestShop;
        }

        public Shop FindShopWithCheapProduct(ProductToBuy productToBuy)
        {
            var productToList = new List<ProductToBuy> { productToBuy };
            return FindShopWithCheapProducts(productToList);
        }

        public Product AddProduct(Product product)
        {
            _providerProductsList.Add(product);
            return product;
        }

        public ShopProduct ProvideProductToShop(Product product, int quantityToProvide, float shopPriceToSet, Shop shop)
        {
            if (shopPriceToSet <= 0)
                throw new InvalidPriceException($"Trying to set invalid price ({shopPriceToSet})");
            if (quantityToProvide < 0)
                throw new InvalidQuantityException($"Trying to set invalid quantity ({quantityToProvide})");
            var newProduct = new ShopProduct(product, quantityToProvide, shopPriceToSet);
            shop.PayForProduct(newProduct);
            shop.AddShopProduct(newProduct);
            return newProduct;
        }

        private Product FindProduct(string name)
        {
            Product product = _providerProductsList.Find(product => product.Name.Equals(name));
            return product;
        }
    }
}