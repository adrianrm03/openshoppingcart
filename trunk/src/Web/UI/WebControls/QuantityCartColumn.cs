using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ShoppingCart.Web.UI.WebControls
{
	/// <summary>
	/// Summary description for QuantityTextBoxCartColumn.
	/// </summary>
	public abstract class QuantityCartColumn : CartColumn
	{
		public QuantityCartColumn()
		{
		}

		public override void Initialize()
		{
			base.Initialize ();
		}
	}
}
