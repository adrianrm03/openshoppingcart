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
	[System.ComponentModel.ToolboxItem(false)]
	public class CartInformations : Control, INamingContainer
	{
		public CartInformations()
		{
		}

		#region Properties

		[Bindable(true)]
		[Category("Information")]
		[Description("Amount without tax")]
		public decimal FreeTaxAmount
		{
			get { return Cart.FreeTaxAmount; }
		}

		[Bindable(true)]
		[Category("Information")]
		[Description("Tax Amount")]
		public decimal TaxAmount
		{
			get { return Cart.TaxAmount; }
		}

		[Bindable(true)]
		[Category("Information")]
		[Description("Amount with tax")]
		public decimal Amount
		{
			get { return Cart.Amount; }
		}

		[Bindable(true)]
		[Category("Information")]
		[Description("How many items in cart")]
		public int Count
		{
			get { return Cart.Count; }
		}

		[Bindable(true)]
		[Category("Information")]
		[Description("Creation date")]
		public DateTime CreationDate
		{
			get { return Cart.CreationDate; } 
		}

		#endregion

	}
}
