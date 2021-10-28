using Shops.Tools;

namespace Shops.Classes
{
    public class Product
    {
        private float _providerPrice;
        public Product(string name, float providerPrice)
        {
            Name = name;
            ProviderPrice = providerPrice;
        }

        public string Name { get; }
        public float ProviderPrice
        {
            get => _providerPrice;
            set
            {
                _providerPrice = value;
                if (_providerPrice < 0)
                    throw new InvalidPriceException($"Trying to set invalid price ({_providerPrice})");
            }
        }
    }
}