using System.Collections.Generic;
using Shops.Tools;

namespace Shops.Services
{
    public class Provider
    {
        public Provider(List<Product> products)
        {
            ProviderProductsList = products;
        }

        public Provider()
            : this(new List<Product>()) { }

        private List<Product> ProviderProductsList { get; }

        public Product AddProduct(Product product)
        {
            if (product.ProviderPrice <= 0)
                throw new InvalidPriceException();
            if (product.Quantity < 0)
                throw new InvalidQuantityException();
            ProviderProductsList.Add(product);
            return product;
        }

        public Product ProvideProductToShop(Product product, int quantityToProvide, float shopPriceToSet, Shop shop)
        {
            if (shopPriceToSet <= 0)
                throw new InvalidPriceException();
            if (quantityToProvide < 0)
                throw new InvalidQuantityException();
            if (product.Quantity < quantityToProvide)
                throw new ProviderHasNotEnoughProductException();
            var newProduct = new Product(product.Name, quantityToProvide, product.ProviderPrice, shopPriceToSet);
            shop.PayForProduct(quantityToProvide, shopPriceToSet);
            shop.Products.Add(newProduct);
            return newProduct;
        }

        private Product FindProduct(string name)
        {
            Product product = ProviderProductsList.Find(product => product.Name.Equals(name));
            return product;
        }
    }
}