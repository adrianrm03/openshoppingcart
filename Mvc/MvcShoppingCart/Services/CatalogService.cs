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
			yield return new Models.Product()
			{
				Code = "1",
				DefaultTaxRate = 0.196,
				Packaging = 5,
				SaleUnitValue = 1,
				Title = "Product 1",
				SalePrice = Convert.ToDecimal(10.0),
			};

			yield return new Models.Product()
			{
				Code = "2",
				DefaultTaxRate = 0.196,
				Packaging = 1,
				SaleUnitValue = 1,
				Title = "Product 2",
				SalePrice = Convert.ToDecimal(15.0),
			};

			yield break;
		}


		public ShoppingCart.Web.Mvc.Model.Price GetPriceByProduct(ShoppingCart.Web.Mvc.Model.IProduct product)
		{
			var realProduct = product as Models.Product;
			return new ShoppingCart.Web.Mvc.Model.Price(realProduct.SalePrice, realProduct.DefaultTaxRate);
		}

	}
}
