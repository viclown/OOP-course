using System.Collections.Generic;
using Shops.Tools;

namespace Shops.Classes
{
    public class Person
    {
        private float _money;
        public Person(string name, float money, List<ShopProduct> products)
        {
            Name = name;
            Money = money;
            Products = products;
        }

        public Person(string name, float money)
            : this(name, money, new List<ShopProduct>()) { }

        public string Name { get; }
        public List<ShopProduct> Products { get; }

        public float Money
        {
            get => _money;
            private set
            {
                _money = value;
                if (_money < 0)
                    throw new InvalidMoneyException($"Trying to set invalid amount of money ({_money})");
            }
        }

        public void PersonBuyProduct(ProductToBuy productToBuy, Shop shop)
        {
            ShopProduct shopProduct = shop.FindProduct(productToBuy.Name);
            if (shopProduct.Quantity < productToBuy.Quantity)
                throw new ShopDoesNotHaveEnoughProductException($"Shop has only {shopProduct.Quantity} {shopProduct.Product.Name}, but Person needs {productToBuy.Quantity}");
            if (Money < shopProduct.ShopPrice * productToBuy.Quantity)
                throw new PersonDoesNotHaveEnoughMoneyException($"Person has {Money} money but needs {shopProduct.ShopPrice * productToBuy.Quantity}");
            Money -= shopProduct.ShopPrice * productToBuy.Quantity;
            shop.UpdateShopMoneyAfterSelling(shopProduct);
            var personsProduct = new ShopProduct(shopProduct.Product, productToBuy.Quantity, shopProduct.ShopPrice);
            shopProduct.Quantity -= productToBuy.Quantity;
            if (shopProduct.Quantity == 0)
                shop.RemoveProduct(shopProduct);
            Products.Add(personsProduct);
        }

        public void PersonBuyProducts(List<ProductToBuy> productsToBuy, Shop shop)
        {
            foreach (ProductToBuy productToBuy in productsToBuy)
            {
                PersonBuyProduct(productToBuy, shop);
            }
        }
    }
}