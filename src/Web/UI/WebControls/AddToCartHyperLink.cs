using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.ComponentModel;
using System.ComponentModel.Design;

namespace ShoppingCart.Web.UI.WebControls
{
	[Designer(typeof(Designers.AddToCartHyperLinkDesigner))]
	[ToolboxData("<{0}:AddToCartHyperLink runat=\"server\" Text=\"AddToCart\"></{0}:AddToCartHyperLink>")]
	[DefaultProperty("Text")]
	[ParseChildren(true)]
	/// <summary>
	/// 
	/// </summary>
	public class AddToCartHyperLink : AddToCart, IPostBackDataHandler
	{
		public AddToCartHyperLink()
		{
		}

		protected override HtmlTextWriterTag TagKey
		{
			get
			{
				return HtmlTextWriterTag.A;
			}
		}

		protected override void AddAttributesToRender(HtmlTextWriter writer)
		{
			writer.AddAttribute(HtmlTextWriterAttribute.Name,this.UniqueID);
			writer.AddAttribute(HtmlTextWriterAttribute.Href, Page.ClientScript.GetPostBackClientHyperlink(this, string.Empty));
		}

		protected override void Render(HtmlTextWriter writer)
		{
			if (Page != null)
			{
				Page.VerifyRenderingInServerForm(this);
			}
			base.Render (writer);
		}

		protected override void RenderContents(HtmlTextWriter writer)
		{
			if (this.ImageUrl.Length > 0)
			{
				Image img = new Image();
				img.ImageUrl = ResolveUrl(this.ImageUrl);
				img.AlternateText = this.Text;
				img.BorderWidth = new Unit(0, UnitType.Pixel);
				img.RenderControl(writer);
			}
			else
			{
				writer.Write(Text);
			}
		}

		protected override void AddParsedSubObject(object obj)
		{
			if (obj is LiteralControl)
			{
				Text = obj as string;
			}
		}

		[Bindable(true)]
		[Category("CartItem")]
		[Description("text")]
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

		#region IPostBackDataHandler Members

		public void RaisePostDataChangedEvent()
		{
			// TODO:  Add AddToCartHyperLink.RaisePostDataChangedEvent implementation
		}

		public bool LoadPostData(string postDataKey, System.Collections.Specialized.NameValueCollection postCollection)
		{
			string uniqueId = this.UniqueID;
			if (postCollection[uniqueId] != null)
			{
				Page.RegisterRequiresRaiseEvent(this);
			}
			return false;
		}

		#endregion
	}
}
