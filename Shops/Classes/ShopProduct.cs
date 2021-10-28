using Shops.Tools;

namespace Shops.Classes
{
    public class ShopProduct
    {
        private int _quantity;
        private float _shopPrice;
        public ShopProduct(Product product, int quantity, float shopPrice)
        {
            Product = product;
            Quantity = quantity;
            ShopPrice = shopPrice;
        }

        public Product Product { get; }
        public int Quantity
        {
            get => _quantity;
            set
            {
                _quantity = value;
                if (_quantity < 0)
                    throw new InvalidQuantityException($"Trying to set invalid quantity ({_quantity}) for {Product.Name}");
            }
        }

        public float ShopPrice
        {
            get => _shopPrice;
            set
            {
                _shopPrice = value;
                if (_shopPrice < 0)
                    throw new InvalidPriceException($"Trying to set invalid price ({_shopPrice}) for {Product.Name}");
            }
        }
    }
}