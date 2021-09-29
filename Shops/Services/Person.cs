using System.Collections.Generic;
using Shops.Tools;

namespace Shops.Services
{
    public class Person
    {
        public Person(string name, float money, List<Product> products)
        {
            Name = name;
            Money = money;
            Products = products;
        }

        public Person(string name, float money)
            : this(name, money, new List<Product>()) { }

        public string Name { get; }
        public List<Product> Products { get; }
        public float Money { get; private set; }

        public void PersonBuyProduct(string name, int quantity, Shop shop)
        {
            Product product = shop.FindProduct(name);
            if (product.Quantity < quantity)
                throw new ShopDoesNotHaveEnoughProductException();
            if (Money < product.ShopPrice * quantity)
                throw new PersonDoesNotHaveEnoughMoneyException();
            Money -= product.ShopPrice * quantity;
            shop.GetMoneyForProduct(product.ShopPrice, quantity);
            var personProduct = new Product(name, quantity, product.ProviderPrice, product.ShopPrice);
            product.Quantity -= quantity;
            if (product.Quantity == 0)
                shop.RemoveProduct(product);
            Products.Add(personProduct);
        }

        public void PersonBuyProducts(List<(string, int)> productsToBuy, Shop shop)
        {
            foreach ((string name, int quantity) in productsToBuy)
            {
                PersonBuyProduct(name, quantity, shop);
            }
        }
    }
}