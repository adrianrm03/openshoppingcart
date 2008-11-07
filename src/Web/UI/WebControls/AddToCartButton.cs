using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.ComponentModel;
using System.ComponentModel.Design;

namespace ShoppingCart.Web.UI.WebControls
{
	[Designer(typeof(Designers.AddToCartButtonDesigner))]
	[ToolboxData("<{0}:AddToCartButton runat=\"server\" Text=\"Add\"></{0}:AddToCartButton>")]
	[DefaultProperty("Text")]
	public class AddToCartButton : AddToCart
	{
		public AddToCartButton()
		{
		}

		[Browsable(false)]
		protected override HtmlTextWriterTag TagKey
		{
			get
			{
				return HtmlTextWriterTag.Input;
			}
		}

		protected override void AddAttributesToRender(HtmlTextWriter writer)
		{
			writer.AddAttribute(HtmlTextWriterAttribute.Type,"submit");
			writer.AddAttribute(HtmlTextWriterAttribute.Name,this.UniqueID);
			writer.AddAttribute(HtmlTextWriterAttribute.Value, Text);
		}

		protected override void Render(HtmlTextWriter writer)
		{
			if (Page != null)
			{
				Page.VerifyRenderingInServerForm(this);
			}
			base.Render (writer);
		}

		[Bindable(true)]
		[Category("CartItem")]
		[Description("button text")]
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
	}
}
