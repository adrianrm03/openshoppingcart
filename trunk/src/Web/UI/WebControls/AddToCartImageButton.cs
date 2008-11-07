using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.ComponentModel;
using System.ComponentModel.Design;

namespace ShoppingCart.Web.UI.WebControls
{
	[Designer(typeof(Designers.AddToCartImageButtonDesigner))]
	[ToolboxData("<{0}:AddToCartImageButton runat=\"server\" Text=\"AddToCart\"></{0}:AddToCartImageButton>")]
	[DefaultProperty("Text")]
	[ToolboxItem(false)]
	public class AddToCartImageButton : AddToCart, IPostBackDataHandler
	{
		public AddToCartImageButton()
		{
		}

		protected override HtmlTextWriterTag TagKey
		{
			get
			{
				return HtmlTextWriterTag.Input;
			}
		}

		#region Rendering

		protected override void AddAttributesToRender(HtmlTextWriter writer)
		{
			if (Page != null)
			{
				Page.VerifyRenderingInServerForm(this);
			}
			writer.AddAttribute(HtmlTextWriterAttribute.Type,"image");
			writer.AddAttribute(HtmlTextWriterAttribute.Name,this.UniqueID);
			writer.AddAttribute(HtmlTextWriterAttribute.Src, ResolveUrl(this.Src));
			if (Page != null && Page.Validators.Count > 0)
			{
				writer.AddAttribute(HtmlTextWriterAttribute.Onclick, "if (typeof(Page_ClientValidate) == \'function\') Page_ClientValidate();") ;
				writer.AddAttribute("language", "javascript");
			}
		}

		protected override void OnPreRender(EventArgs e)
		{
			if (this.Page != null)
			{
				Page.RegisterRequiresPostBack(this);
			}
			base.OnPreRender(e);
		}

		#endregion

		#region Properties

		[Bindable(true)]
		[Category("CartItem")]
		[Description("button image source")]
		public string Src
		{
			get
			{
				if (ViewState["Src"] == null)
				{
					return string.Empty;
				}
				return (string) ViewState["Src"];
			}
			set
			{
				ViewState["Src"] = value;
			}
		}

		#endregion

		#region IPostBackDataHandler Members

		public void RaisePostDataChangedEvent()
		{
			// TODO:  Add AddToCartImageButton.RaisePostDataChangedEvent implementation
		}

		public bool LoadPostData(string postDataKey, System.Collections.Specialized.NameValueCollection postCollection)
		{
			string uniqueId = this.UniqueID;
			string x = postCollection[uniqueId + ".x"];
			string y = postCollection[uniqueId + ".y"];
			if (((x != null) && (y != null)) && ((x.Length > 0) && (y.Length > 0)))
			{
				Page.RegisterRequiresRaiseEvent(this);
			}
			return false;
		}

		#endregion
	}
}
