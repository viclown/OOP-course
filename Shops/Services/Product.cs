namespace Shops.Services
{
    public class Product
    {
        public Product(string name, int quantity, float providerPrice, float shopPrice)
        {
            Name = name;
            Quantity = quantity;
            ProviderPrice = providerPrice;
            ShopPrice = shopPrice;
        }

        public Product(string name, int quantity, int providerPrice)
            : this(name, quantity, providerPrice, providerPrice) { }

        public string Name { get; }
        public int Quantity { get; set; }
        public float ProviderPrice { get; }
        public float ShopPrice { get; set; }
    }
}