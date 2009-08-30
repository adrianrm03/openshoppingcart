using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.Design;
using System.Web.UI.WebControls;
using System.Drawing;
using System.Drawing.Design;
using System.ComponentModel;
using System.Xml;
using System.Xml.Serialization;
using System.Globalization;
using System.Collections;

namespace ShoppingCart.Web.UI.WebControls
{
	[
	ToolboxData("<{0}:LinkInformation runat=\"server\"></{0}:LinkInformation>")
	, ParseChildren(true)
	]
	public class LinkInformation : HyperLink
	{
		private string emptyCartLabel;
		private string oneItemCartLabel;
		private string itemsCartLabel;

		public LinkInformation()
		{
			emptyCartLabel = "Your cart is empty";
			oneItemCartLabel = "You have 1 item in your cart";
			itemsCartLabel = "You have {0} items in your cart";
		}

		[Bindable(true)]
		public string EmptyCartLabel
		{
			set { emptyCartLabel = value; }
		}

		[Bindable(true)]
		public string OneItemCartLable
		{
			set { oneItemCartLabel = value; }
		}

		[Bindable(true)]
		public string ItemsCartLabel
		{
			set { itemsCartLabel = value; }
		}

		protected override void Render(HtmlTextWriter writer)
		{
            switch (Cart.Status)
            {
                case CartStatus.Empty :
                    this.Text = emptyCartLabel; 
                    break;
                case CartStatus.OneItem :
                    this.Text = oneItemCartLabel;
                    break;
                case CartStatus.ManyItems :
                    this.Text = string.Format(itemsCartLabel,Cart.Count);
                    break;
            }
			base.Render (writer);
		}
	}
}
