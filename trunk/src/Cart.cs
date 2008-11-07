using System;
using System.Collections.Generic;
using System.Text;
using System.Web.Configuration;
using System.Configuration.Provider;

namespace Serialcoder.ShoppingCart
{
    public sealed class Cart
    {
        #region Properties

        private static CartProviderCollection _providers = null;

        private static CartProvider _provider = null;
        public static CartProvider Provider
        {
            get 
			{
				if (_provider == null)
				{
					LoadProviders();
				}
				return _provider; 
			}
			set
			{
				_provider = value;
			}
        }

        public static CartStatus Status
        {
            get
            {
                return Provider.Status;
            }
        }

        public static int Count
        {
            get
            {
				return Provider.Count;
            }
        }

        public static System.Collections.Specialized.HybridDictionary Context
        {
            get
            {
				return Provider.Context;
            }
            set
            {
				Provider.Context = value;
            }
        }

        public static decimal FreeTaxAmount
        {
            get
            {
				return Provider.FreeTaxAmount;
            }
        }

        public static decimal TaxAmount
        {
            get
            {
				return Provider.TaxAmount;
            }
        }

        public static decimal Amount
        {
            get
            {
				return Provider.Amount;
            }
        }

        public static DateTime CreationDate
        {
            get
            {
				return Provider.CreationDate;
            }
        }

        public static List<CartItem> List
        {
            get
            {
				return Provider.List;
            }
        }

        #endregion

        #region Services

        public static void AddItem(CartItem ci)
        {
			Provider.Add(ci);
        }

        public static void RemoveItem(CartItem ci)
        {
			Provider.Remove(ci);
        }

        public static CartItem SelectItemByGuid(System.Guid id)
        {
			return Provider[id];
        }

        public static CartItem SelectItemByIndex(int index)
        {
			return Provider[index];
        }

        public static void Clear()
        {
			Provider.Clear();
        }

        #endregion

        #region LoadProviders

        private static object _lock = new object();
        private static void LoadProviders()
        {
            // Avoid claiming lock if providers are already loaded
            if (_provider == null)
            {
                lock (_lock)
                {
                    // Do this again to make sure _provider is still null
                    if (_provider == null)
                    {
                        // Get a reference to the <imageService> section
                        CartServiceSection section = (CartServiceSection) WebConfigurationManager.GetSection("cartService");

                        // Load registered providers and point _provider
                        // to the default provider
                        _providers = new CartProviderCollection();

                        if (section == null)
                        {
                            _provider = new Providers.SessionCartProvider();
                            _provider.Initialize("default", new System.Collections.Specialized.NameValueCollection());
                            _providers.Add(_provider);
                        }
                        else
                        {
                            ProvidersHelper.InstantiateProviders(section.Providers, _providers, typeof(CartProviderCollection));
                            _provider = _providers[section.DefaultProvider];
                        }

                        if (_provider == null)
                            throw new ProviderException("Unable to load default CartService");
                    }
                }
            }
        }

        #endregion

    }
}
