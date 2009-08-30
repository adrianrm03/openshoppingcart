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

namespace ShoppingCart.Web.UI.WebControls
{
	[Designer(typeof(System.Web.UI.Design.WebControls.ButtonDesigner))]
	[ToolboxData("<{0}:AddToCartButton runat=\"server\" Text=\"Add\"></{0}:AddToCartButton>")]
	[DefaultProperty("Text")]
	/// <summary>
	/// 
	/// </summary>
	public class EmptyCartButton : EmptyCart
	{
		protected override HtmlTextWriterTag TagKey
		{
			get
			{
				return HtmlTextWriterTag.Input;
			}
		}

		protected override void AddAttributesToRender(HtmlTextWriter writer)
		{
			if (Page != null)
			{
				Page.VerifyRenderingInServerForm(this);
			}
			writer.AddAttribute(HtmlTextWriterAttribute.Type,"submit");
			writer.AddAttribute(HtmlTextWriterAttribute.Name,this.UniqueID);
			writer.AddAttribute(HtmlTextWriterAttribute.Value, Text);
			base.AddAttributesToRender (writer);
		}

		#region Properties

		[Bindable(true)]
		[Category("CartItem")]
		[Description("button text")]
		[DefaultValue("Clear Cart")]
		public string Text
		{
			get
			{
				if (ViewState["Text"] == null)
				{
					return string.Empty;
				}
				return (string) ViewState["Text"];
			}
			set
			{
				ViewState["Text"] = value;
			}
		}

		#endregion
	}
}
