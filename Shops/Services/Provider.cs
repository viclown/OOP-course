using System.Collections.Generic;
using Shops.Tools;

namespace Shops.Services
{
    public class Provider
    {
        public Provider(List<Product> products)
        {
            ProviderProducts = products.AsReadOnly();
            ProviderProductsList = products;
        }

        public Provider()
            : this(new List<Product>()) { }

        public IList<Product> ProviderProducts { get; }
        private List<Product> ProviderProductsList { get; }

        public Product AddProduct(Product product)
        {
            ProviderProductsList.Add(product);
            return product;
        }

        public Product FindProduct(string name)
        {
            foreach (Product product in ProviderProducts)
            {
                if (product.Name == name)
                    return product;
            }

            throw new ProductIsNotRegisteredException();
        }

        public Product ProvideProductToShop(string name, int quantity, float shopPrice, Shop shop)
        {
            Product product = FindProduct(name);
            if (product.Quantity < quantity)
                throw new ProviderHasNotEnoughProductException();
            var newProduct = new Product(name, quantity, product.ProviderPrice, shopPrice);
            shop.PayForProduct(quantity, shopPrice);
            shop.Products.Add(newProduct);
            return newProduct;
        }
    }
}