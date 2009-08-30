using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.Design;

namespace ShoppingCart.Designers
{
	public class AddToCartImageButtonDesigner : ControlDesigner
	{
		private Web.UI.WebControls.AddToCartImageButton addToCart;

		public AddToCartImageButtonDesigner()
		{
		}

		public override void Initialize(System.ComponentModel.IComponent component)
		{
			addToCart = (Web.UI.WebControls.AddToCartImageButton)component;
			base.Initialize(component);
		}

		protected override string GetEmptyDesignTimeHtml()
		{
			StringWriter sw = new StringWriter();
			HtmlTextWriter tw = new HtmlTextWriter(sw);
			return base.GetEmptyDesignTimeHtml();
		}
	}
}
