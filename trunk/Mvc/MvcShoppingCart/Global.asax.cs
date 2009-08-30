using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace MvcShoppingCart
{
	// Note: For instructions on enabling IIS6 or IIS7 classic mode, 
	// visit http://go.microsoft.com/?LinkId=9394801

	public class MvcApplication : System.Web.HttpApplication
	{
		public static void RegisterRoutes(RouteCollection routes)
		{
			routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

			routes.MapRoute(
				"Default",                                              // Route name
				"{controller}/{action}/{id}",                           // URL with parameters
				new { controller = "Home", action = "Index", id = "" }  // Parameter defaults
			);
		}

		protected void Application_Start()
		{
			RegisterSerivces();

			var cartService = (ShoppingCart.Web.Mvc.Services.ICartService) Application["cartService"];

			cartService.RegisterRoutes(RouteTable.Routes);
			RegisterRoutes(RouteTable.Routes);
		}

		public void Application_BeginRequest(object sender, EventArgs args)
		{
			var vidCookie = Request.Cookies["vid"];
			if (vidCookie == null)
			{
				CreateVisitorCookie(Context, "vid");
			}
		}

		HttpCookie CreateVisitorCookie(HttpContext ctx, string name)
		{
			var cookie = new System.Web.HttpCookie(name);
			cookie.Expires = DateTime.Now.AddDays(60);
			cookie.Path = "/";
			cookie.Value = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 20);
			ctx.Response.Cookies.Add(cookie);
			return cookie;
		}

		private void RegisterSerivces()
		{
			var cartRepository = new ShoppingCart.Web.Mvc.Services.HttpCartRepository(Context);
			var cartService = new ShoppingCart.Web.Mvc.Services.CartService(cartRepository);
			Application.Add("cartService", cartService);
			var catalogService = new Services.CatalogService();
			Application.Add("catalogService", catalogService);
			var userService = new Services.UserService(Context);
			Application.Add("userService", userService);
		}
	}
}