using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcShoppingCart.Models
{
	public class Product : ShoppingCart.Web.Mvc.Model.IProduct
	{
		#region IProduct Members

		public string Code { get; set; }

		public int SaleUnitValue { get; set; }

		public int Packaging { get; set; }

		public double DefaultTaxRate { get; set; }

		public decimal SalePrice { get; set; }

		public string Title	{ get ; set; }

		#endregion
	}
}
