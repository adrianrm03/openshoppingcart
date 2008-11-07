using System;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.Design;

namespace Serialcoder.ShoppingCart.Designers
{
	public class CartGridDesigner : ControlDesigner
	{
		private Web.UI.WebControls.CartGrid cart;

		public CartGridDesigner()
		{
		}

		public override void Initialize(System.ComponentModel.IComponent component)
		{
			cart = (Web.UI.WebControls.CartGrid) component;
			base.Initialize(cart);
		}

		public override string GetDesignTimeHtml()
		{
			return base.GetEmptyDesignTimeHtml();
		}

	}
}
