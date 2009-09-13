using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcShoppingCart.Services
{
	public class CatalogService : ShoppingCart.Web.Mvc.Services.ICatalogService
	{
		#region ICatalogService Members

		public ShoppingCart.Web.Mvc.Model.IProduct GetProductByCode(string productCode)
		{
			return GetProductList().SingleOrDefault(i => i.Code.Equals(productCode, StringComparison.InvariantCultureIgnoreCase));
		}

		#endregion

		internal IEnumerable<Models.Product> GetProductList()
		{
			for (int i = 0; i < 20; i++)
			{
				yield return new Models.Product()
				{
					Code = i.ToString(),
					DefaultTaxRate = 0.196,
					Packaging = Math.Max(1, new Random().Next(10)),
					SaleUnitValue = 1,
					Title = string.Format("Product {0}", i + 1),
					SalePrice = new Random().Next(1000),
				};
			}

			yield break;
		}

		public ShoppingCart.Web.Mvc.Model.Price GetPriceByProduct(ShoppingCart.Web.Mvc.Model.IProduct product)
		{
			var realProduct = product as Models.Product;
			return new ShoppingCart.Web.Mvc.Model.Price(realProduct.SalePrice, realProduct.DefaultTaxRate);
		}
	}
}
