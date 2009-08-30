using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ShoppingCart.Web.Mvc.Services
{
	public interface ICatalogService
	{
		Model.IProduct GetProductByCode(string productCode);
		Model.Price GetPriceByProduct(Model.IProduct product);
	}
}
