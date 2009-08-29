using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcShoppingCart.Services
{
	public class UserService : ShoppingCart.Web.Mvc.Services.IUserService
	{
		System.Web.HttpContext m_Context;

		public UserService(System.Web.HttpContext context)
		{
			m_Context = context;
		}

		#region IUserService Members

		public string GetVisitorId()
		{
			var vidCookie = m_Context.Request.Cookies["vid"];
			return vidCookie.Value;
		}

		#endregion

	}
}
