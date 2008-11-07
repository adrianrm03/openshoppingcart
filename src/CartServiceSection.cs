using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;

namespace Serialcoder.ShoppingCart
{
    internal class CartServiceSection : System.Configuration.ConfigurationSection
    {
        [ConfigurationProperty("providers")]
        public ProviderSettingsCollection Providers
        {
            get { return (ProviderSettingsCollection)base["providers"]; }
        }

        [ConfigurationProperty("defaultProvider", DefaultValue = "SessionCartProvider")]
        public string DefaultProvider
        {
            get { return (string)base["defaultProvider"]; }
            set { base["defaultProvider"] = value; }
        }
    }
}
