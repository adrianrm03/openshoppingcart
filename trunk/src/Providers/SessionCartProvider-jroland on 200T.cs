using System;
using System.Collections;
using System.Collections.Specialized;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;
using System.IO;
using System.Web;

namespace SerialCoder.ShoppingCart.Providers
{
    public sealed class SessionCartProvider : CartProvider
	{
        public override void Initialize(string name, NameValueCollection config)
        {
            base.Initialize(name, config);
        }

        private const string SESSION_KEY = "SerialCoder.ShoppingCart.SessionCartProvider";
        public override List<CartItem> List
        {
            get
            {
				if (System.Web.HttpContext.Current.Session[SESSION_KEY] == null)
				{
					System.Web.HttpContext.Current.Session[SESSION_KEY] = new List<CartItem>();
				}
                return (List<CartItem>)System.Web.HttpContext.Current.Session[SESSION_KEY];
            }
            set
            {
                System.Web.HttpContext.Current.Session[SESSION_KEY] = value;
            }
        }

        private const string SESSION_KEY_CONTEXT = "SerialCoder.ShoppingCart.SessionCartProvider.Context";
        public override System.Collections.Specialized.HybridDictionary Context
        {
            get
            {
				if (System.Web.HttpContext.Current.Session[SESSION_KEY_CONTEXT] == null)
				{
					System.Web.HttpContext.Current.Session[SESSION_KEY_CONTEXT] = new HybridDictionary();
				}
                return (HybridDictionary)System.Web.HttpContext.Current.Session[SESSION_KEY_CONTEXT];
            }
            set
            {
                System.Web.HttpContext.Current.Session[SESSION_KEY_CONTEXT] = value;
            }
        }

        public override object Id
        {
            get
            {
                return System.Web.HttpContext.Current.Session.SessionID;
            }
            set
            {
            }
        }

        private const string SESSION_KEY_CREATIONDATE = "SerialCoder.ShoppingCart.SessionCartProvider.CreationDate";
        public override DateTime CreationDate
        {
            get
            {
				if (System.Web.HttpContext.Current.Session[SESSION_KEY_CREATIONDATE] == null)
				{
					System.Web.HttpContext.Current.Session[SESSION_KEY_CREATIONDATE] = DateTime.Now;
				}
                return (DateTime) System.Web.HttpContext.Current.Session[SESSION_KEY_CREATIONDATE];
            }
            set
            {
                System.Web.HttpContext.Current.Session[SESSION_KEY_CREATIONDATE] = value;
            }
        }
	}
}