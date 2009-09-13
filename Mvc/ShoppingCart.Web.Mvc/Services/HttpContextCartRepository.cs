using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ShoppingCart.Web.Mvc.Services
{
	public class HttpContextCartRepository : ICartRepository
	{
		public const string CART_COOKIE_NAME = "cartId";

		public HttpContextCartRepository()
		{
		}

		#region ICartRepository Members

		public string GetCartId()
		{
			var cookie = System.Web.HttpContext.Current.Request.Cookies[CART_COOKIE_NAME];
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
			System.Web.HttpContext.Current.Response.Cookies.Add(cookie);
		}

		public void Remove(string cartId)
		{
			var key = GetKey(cartId);
			System.Web.HttpContext.Current.Response.Cookies.Remove(CART_COOKIE_NAME);
		}

		public void Remove(Model.Cart cart)
		{
			var key = GetKey(cart.Code);
			System.Web.HttpContext.Current.Cache.Remove(key);
		}

		public Model.Cart this[string cartId]
		{
			get
			{
				var key = GetKey(cartId);
				return System.Web.HttpContext.Current.Cache[key] as Model.Cart;
			}
		}

		public void Save(Model.Cart cart)
		{
			var key = GetKey(cart.Code);
			System.Web.HttpContext.Current.Cache.Insert(key, cart, null, DateTime.Now.AddDays(1), TimeSpan.Zero);
		}

		public IQueryable<Model.Cart> GetList()
		{
			return System.Web.HttpContext.Current.Cache.GetListOf<Model.Cart>();
		}

		#endregion

		private string GetKey(string cartId)
		{
			return string.Format("cart:{0}", cartId);
		}

	}
}
