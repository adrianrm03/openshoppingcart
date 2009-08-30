using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ShoppingCart.Web.Mvc.Services
{
	public class HttpCartRepository : ICartRepository
	{
		public const string CART_COOKIE_NAME = "cartId";

		private System.Web.HttpContext m_HttpContext;

		public HttpCartRepository(System.Web.HttpContext ctx)
		{
			m_HttpContext = ctx;
		}

		#region ICartRepository Members

		public string GetCartId()
		{
			var cookie = m_HttpContext.Request.Cookies[CART_COOKIE_NAME];
			if (cookie == null)
			{
				cookie = new System.Web.HttpCookie(CART_COOKIE_NAME);
				cookie.Expires = DateTime.Now.AddDays(30);
				cookie.Path = "/";
				cookie.Value = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 20);
				System.Web.HttpContext.Current.Response.Cookies.Add(cookie);
			}
			return cookie.Value;
		}

		public void ChangeCurrent(string cartId)
		{
			var cookie = new System.Web.HttpCookie(CART_COOKIE_NAME);
			cookie.Expires = DateTime.Now.AddDays(30);
			cookie.Path = "/";
			cookie.Value = cartId;
			m_HttpContext.Response.Cookies.Add(cookie);
		}

		public void Remove(string cartId)
		{
			var key = GetKey(cartId);
			m_HttpContext.Response.Cookies.Remove(CART_COOKIE_NAME);
		}

		public void Remove(Model.Cart cart)
		{
			var key = GetKey(cart.Code);
			m_HttpContext.Cache.Remove(key);
		}

		public Model.Cart this[string cartId]
		{
			get
			{
				var key = GetKey(cartId);
				return m_HttpContext.Cache[key] as Model.Cart;
			}
		}

		public void Save(Model.Cart cart)
		{
			var key = GetKey(cart.Code);
			m_HttpContext.Cache.Insert(key, cart, null, DateTime.Now.AddDays(1), TimeSpan.Zero);
		}

		public IQueryable<Model.Cart> GetList()
		{
			return m_HttpContext.Cache.GetListOf<Model.Cart>();
		}

		#endregion

		private string GetKey(string cartId)
		{
			return string.Format("cart:{0}", cartId);
		}

	}
}
