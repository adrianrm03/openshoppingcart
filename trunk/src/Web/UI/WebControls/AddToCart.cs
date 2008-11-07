using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.Design;
using System.Drawing; 
using System.Drawing.Design;
using System.ComponentModel;
using System.Xml;
using System.Xml.Serialization;
using System.Globalization;
using System.Collections;

namespace Serialcoder.ShoppingCart.Web.UI.WebControls
{
	[ParseChildren(false)]
	[PersistChildrenAttribute(false)]
	public abstract class AddToCart : WebControl, IPostBackEventHandler
	{
		public AddToCart() 
		{
			this.EnableViewState = true;
		}

		#region Overriden Methods

		protected override void OnPreRender(EventArgs e)
		{
			Context.Trace.Write("AddToCart","OnPreRender");
			// Rechercher la boite de texte
			if (QuantityTextBox != null && QuantityTextBox != string.Empty)
			{
				Control c = Parent.FindControl(QuantityTextBox);
				if (c != null && c is TextBox)
				{
					TextBox box = c as TextBox;
					if (box.Text == null || box.Text == string.Empty)
					{
						box.Text = DefaultQuantity.ToString();
					}
				}
			}
			base.OnPreRender (e);
		}

		#endregion

		#region Insert

		protected void Insert()
		{
			Context.Trace.Write("AddToCartControl","Insert");
			// base.DataBind();
			CartItem item = new CartItem();
			if (QuantityTextBox == null)
			{
				item.Quantity = Quantity;
			}
			else
			{
				Control c = Parent.FindControl(QuantityTextBox);
				if (c != null && c is TextBox)
				{
					try
					{
						TextBox box = c as TextBox;
						int q = int.Parse(box.Text);
						item.Quantity = q;
					}
					catch
					{
						Page.Validators.Add(new BadEntryValidator(BadQuantityErrorText));
						return;
					}
				}
			}
			// Product
			item.ProductId = ProductId;
			item.Code = Code;
			item.Availability = Availability;
			item.Description = Description;
			item.ImageUrl = ImageUrl;
			item.ProductLink = ProductLink;
			// Price
			item.UnitSale = UnitSale;
			item.Reduce = Reduce;
			item.PublicPrice = PublicPrice;
			item.TaxRate = TaxRate;
			Cart.AddItem(item);
		}

		#endregion

		#region Properties Binding

		public string QuantityTextBox
		{
			get 
			{ 
				if (ViewState["QuantityTextBox"] == null)
				{
					return null;
				}
				return (string) ViewState["QuantityTextBox"]; 
			}
			set 
			{ 
				ViewState["QuantityTextBox"] = value; 
			}
		}

		[Bindable(true)]
		[Category("CartItem")]
		[Description("ProductId")]
		public string ProductId
		{
			get 
			{ 
				if (ViewState["ProductId"] == null)
				{
					return string.Empty;
				}
				return (string) ViewState["ProductId"];
			}
			set 
			{	
				ViewState["ProductId"] = value; 
			}
		}

		[Bindable(true)]
		[Category("CartItem")]
		[Description("Availability")]
		public string Availability
		{
			get 
			{ 
				if (ViewState["Availability"] == null)
				{
					return string.Empty;
				}
				return (string) ViewState["Availability"];
			}
			set 
			{	
				ViewState["Availability"] = value; 
			}
		}

		// string code;
		[Bindable(true)]
		[Category("CartItem")]
		[Description("Product Code")]
		[NotifyParentProperty(true)]
		public string Code
		{
			get 
			{ 
				if (ViewState["code"] == null)
				{
					return string.Empty;
				}
				return (string) ViewState["code"];
			}
			set 
			{ 
				ViewState["code"] = value; 
			}
		}

		// string description;
		[Bindable(true)]
		[Category("CartItem")]
		[Description("Description of product")]
		public string Description
		{
			get 
			{ 
				if (ViewState["Description"] == null)
				{
					return string.Empty;
				}
				return (string) ViewState["Description"];
			}
			set 
			{ 
				ViewState["Description"] = value; 
			}
		}

		[Bindable(true)]
		[Category("CartItem")]
		[Description("Quantity by default")]
		[DefaultValue(1)]
		public int DefaultQuantity
		{
			get 
			{ 
				if (ViewState["DefaultQuantity"]==null)
				{
					return 1;
				}
				return (int) ViewState["DefaultQuantity"];
			}
			set 
			{ 
				ViewState["DefaultQuantity"] = value;
			}
		}

		[Bindable(true)]
		[Category("CartItem")]
		[Description("Url for show detailled product")]
		public string ProductLink
		{
			get 
			{ 
				if (ViewState["ProductLink"] == null)
				{
					return string.Empty;
				}
				return (string) ViewState["ProductLink"]; 
			}
			set 
			{ 
				ViewState["ProductLink"] = value; 
			}
		}

		[Bindable(true)]
		[Category("CartItem")]
		[Description("Url for show image of product")]
		public string ImageUrl
		{
			get 
			{ 
				if (ViewState["ImageUrl"]==null)
				{
					return string.Empty; 
				}
				return (string) ViewState["ImageUrl"]; 
			}
			set 
			{ 
				ViewState["ImageUrl"] = value; 
			}
		}

		[Bindable(true)]
		[Category("CartItem")]
		[Description("The quantity of produt")]
		public int Quantity
		{
			get 
			{ 
				if (ViewState["Quantity"] == null)
				{
					return 1;
				}
				return (int) ViewState["Quantity"]; 
			}
			set 
			{ 
				ViewState["Quantity"] = value; 
			}
		}

		#region Price

		[Bindable(true)]
		[Category("CartItem")]
		[DefaultValue(0)]
		[Description("public price for product")]
		public decimal PublicPrice
		{
			get 
			{ 
				if (ViewState["PublicPrice"] == null)
				{
					return 0;
				}
				return (decimal) ViewState["PublicPrice"]; 
			}
			set 
			{ 
				ViewState["PublicPrice"] = value; 
			}
		}

		[Bindable(true)]
		[Category("CartItem")]
		[Description("unit of sale for product")]
		[DefaultValue(1)]
		public int UnitSale
		{
			get 
			{ 
				if (ViewState["UnitSale"] == null)
				{
					return 1;
				}
				return (int) ViewState["UnitSale"]; 
			}
			set 
			{ 
				ViewState["UnitSale"] = value; 
			}
		}

		[Bindable(true)]
		[Category("CartItem")]
		[Description("reduce")]
		[DefaultValue(0)]
		public double Reduce
		{
			get 
			{ 
				if (ViewState["Reduce"] == null)
				{
					return 0;
				}
				return (double) ViewState["Reduce"]; 
			}
			set 
			{ 
				ViewState["Reduce"] = value; 
			}
		}

		[Bindable(true)]
		[Category("CartItem")]
		[Description("tax rate")]
		[DefaultValue(0)]
		public double TaxRate
		{
			get 
			{ 
				if (ViewState["TaxRate"] == null)
				{
					return 0;
				}
				return (double) ViewState["TaxRate"]; 
			}
			set 
			{ 
				ViewState["TaxRate"] = value; 
			}
		}

		#endregion

		public string BadQuantityErrorText
		{
			get
			{
				if (ViewState["BadQuantityErrorText"] == null)
				{
					return "bad quantity";
				}
				return (string) ViewState["BadQuantityErrorText"];
			}
			set
			{
				ViewState["BadQuantityErrorText"] = value;
			}
		}

		#endregion

		#region IPostBackEventHandler Members

		public void RaisePostBackEvent(string eventArgument)
		{
			Context.Trace.Write("AddToCartControl","RaisePostBackEvent");
			Insert();
		}

		#endregion
	}
}
