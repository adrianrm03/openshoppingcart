using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ShoppingCart.Web.Mvc.Tests.Services
{
	public class MockCartRepository : ShoppingCart.Web.Mvc.Services.ICartRepository
	{
		private Dictionary<string, Model.Cart> m_Cache;

		public MockCartRepository()
		{
			m_Cache = new Dictionary<string, ShoppingCart.Web.Mvc.Model.Cart>();
		}

		#region ICartRepository Members

		public string GetCartId()
		{
			return "1";
		}

		public ShoppingCart.Web.Mvc.Model.Cart this[string cartId]
		{
			get 
			{
				if (m_Cache.ContainsKey(cartId))
				{
					return m_Cache[cartId];
				}
				return null;
			}
		}

		public void Save(ShoppingCart.Web.Mvc.Model.Cart cart)
		{
			m_Cache.Add(cart.Code, cart);
		}

		public void Remove(ShoppingCart.Web.Mvc.Model.Cart cart)
		{
			m_Cache.Remove(cart.Code);
		}

		public void Remove(string cartId)
		{
			m_Cache.Remove(cartId);
		}

		public IQueryable<ShoppingCart.Web.Mvc.Model.Cart> GetList()
		{
			return m_Cache.Select(i => i.Value).AsQueryable();
		}

		public void ChangeCurrent(string cartId)
		{
			// Do nothing
		}

		#endregion
	}
}
