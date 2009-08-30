using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ShoppingCart.Web.Mvc.Model
{
	public interface IProduct
	{
		string Code { get; set; }
		int SaleUnitValue { get; set; }
		int Packaging { get; set; }
		string Title { get; set; }
	}
}
