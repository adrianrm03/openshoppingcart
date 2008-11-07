using System;
using System.Collections.Generic;
using System.Text;

namespace Serialcoder.ShoppingCart
{
    internal class CartProviderCollection : System.Configuration.Provider.ProviderCollection
    {
        public new CartProvider this[string name]
        {
            get { return (CartProvider)base[name]; }
        }

        public void Add(CartProvider provider)
        {
            if (provider == null)
                throw new ArgumentNullException("provider");

            if (!(provider is CartProvider))
                throw new ArgumentException("Invalid provider type", "provider");

            base.Add(provider);
        }

    }
}
