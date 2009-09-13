using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using ShoppingCart.Web.Mvc.Services;

namespace MvcShoppingCart.Controllers
{
	[HandleError]
	public class HomeController : Controller
	{
		public HomeController()
		{
			CatalogService = (Services.CatalogService)System.Web.HttpContext.Current.Application["catalogService"];
		}

		protected Services.CatalogService CatalogService { get; set; }

		public ActionResult Index()
		{
			ViewData["Message"] = "Welcome to Open Shopping Cart for ASP.NET MVC!";
			return View();
		}

		public ActionResult About()
		{
			return View();
		}

		public ActionResult Catalog()
		{
			var list = CatalogService.GetProductList();
			ViewData.Model = list;
			return View();
		}
	}
}
