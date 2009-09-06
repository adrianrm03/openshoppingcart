using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcShoppingCart.Services
{
	public static class CartExtensions
	{
		public static Models.Product GetProduct(this ShoppingCart.Web.Mvc.Model.CartItem cartItem)
		{
			var catalogService = System.Web.HttpContext.Current.Application["catalogService"] as Services.CatalogService;
			return (Models.Product)catalogService.GetProductByCode(cartItem.ProductCode);
		}

		public static string ToCurrency(this decimal value)
		{
			return string.Format("{0:#,##0.00}", value);
		}
	}
}
