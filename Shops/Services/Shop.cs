using System.Collections.Generic;
using Shops.Tools;

namespace Shops.Services
{
    public class Shop
    {
        private readonly int _lastId = 100000;
        public Shop(string name, string address, float money, List<Product> products)
        {
            Name = name;
            Address = address;
            Money = money;
            Products = products;
            Id = _lastId++;
        }

        public Shop(string name, string address, float money)
            : this(name, address, money, new List<Product>()) { }

        public string Name { get; }
        public List<Product> Products { get; }
        private float Money { get; set; }
        private string Address { get; }
        private int Id { get; }

        public void ChangePrice(Product product, float newPrice)
        {
            if (newPrice <= 0)
                throw new InvalidPriceException();
            product.ShopPrice = newPrice;
        }

        public void PayForProduct(int quantity, float shopPrice)
        {
            if (Money < quantity * shopPrice)
                throw new ShopHasNotEnoughMoneyException();
            Money -= quantity * shopPrice;
        }

        public void GetMoneyForProduct(Product product)
        {
            Money += product.ShopPrice * product.Quantity;
        }

        public Product FindProduct(string name)
        {
            Product product = Products.Find(product => product.Name.Equals(name));
            return product;
        }

        public void RemoveProduct(Product product)
        {
            Products.Remove(product);
        }
    }
}