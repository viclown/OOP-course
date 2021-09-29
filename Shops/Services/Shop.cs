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

        public static void ChangePrice(Product product, float newPrice)
        {
            product.ShopPrice = newPrice;
        }

        public void PayForProduct(int quantity, float shopPrice)
        {
            if (Money < quantity * shopPrice) throw new ShopHasNotEnoughMoneyException();
            Money -= quantity * shopPrice;
        }

        public void GetMoneyForProduct(float price, int quantity)
        {
            Money += price * quantity;
        }

        public Product FindProduct(string name)
        {
            foreach (Product product in Products)
            {
                if (product.Name == name && product.Quantity != 0)
                    return product;
            }

            return null;
        }

        public void RemoveProduct(Product product)
        {
            Products.Remove(product);
        }
    }
}