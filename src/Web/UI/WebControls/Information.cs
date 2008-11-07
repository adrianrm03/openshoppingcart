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

namespace Serialcoder.ShoppingCart.Web.UI.WebControls
{
	[ParseChildren(true)]
	[ToolboxData("<{0}:Information runat=\"server\"></{0}:Information>")]
	public class Information : Control, INamingContainer
	{
		public Information()
		{
		}

		#region Templates

		private ITemplate headerTemplate;
		[TemplateContainer(typeof(CartInformations))]
		[PersistenceMode(PersistenceMode.InnerDefaultProperty)]
		[Browsable(false)]
		public ITemplate HeaderTemplate
		{
			get { return headerTemplate; }
			set { headerTemplate = value; }
		}

		private ITemplate emptyCartItemTemplate;
		[TemplateContainer(typeof(CartInformations))]
		[PersistenceMode(PersistenceMode.InnerDefaultProperty)]
		[Browsable(false)]
		public ITemplate EmptyCartItemTemplate
		{
			get { return emptyCartItemTemplate; }
			set { emptyCartItemTemplate = value; }
		}

		private ITemplate oneItemCartItemTemplate;
		[TemplateContainer(typeof(CartInformations))]
		[PersistenceMode(PersistenceMode.InnerDefaultProperty)]
		[Browsable(false)]
		public ITemplate OneItemCartItemTemplate
		{
			get { return oneItemCartItemTemplate; }
			set { oneItemCartItemTemplate = value; }
		}

		private ITemplate manyItemsCartItemTemplate;
		[TemplateContainer(typeof(CartInformations))]
		[PersistenceMode(PersistenceMode.InnerDefaultProperty)]
		[Browsable(false)]
		public ITemplate ManyItemsCartItemTemplate
		{
			get { return manyItemsCartItemTemplate; }
			set { manyItemsCartItemTemplate = value; }
		}

		private ITemplate footerTemplate;
		[TemplateContainer(typeof(CartInformations))]
		[PersistenceMode(PersistenceMode.InnerDefaultProperty)]
		[Browsable(false)]
		public ITemplate FooterTemplate
		{
			get { return footerTemplate; }
			set { footerTemplate = value; }
		}

		#endregion

		#region Rendering

		protected override void CreateChildControls()
		{
			Controls.Clear();
			CreateControlHierarchy();
		}

		protected virtual void CreateControlHierarchy()
		{
			if (headerTemplate != null)
			{
				Control c = new Control();
				headerTemplate.InstantiateIn(c);
				Controls.Add(c);
			}

			CartInformations ci = new CartInformations();
			switch (Cart.Status)
			{
				case CartStatus.Empty :
					if (emptyCartItemTemplate != null)
					{
						emptyCartItemTemplate.InstantiateIn(ci);
					}
					break;
				case CartStatus.OneItem :
					if (oneItemCartItemTemplate != null)
					{
						oneItemCartItemTemplate.InstantiateIn(ci);
					}
					break;
				case CartStatus.ManyItems :
					if (manyItemsCartItemTemplate != null)
					{
						manyItemsCartItemTemplate.InstantiateIn(ci);
					}
					break;
			}
			Controls.Add(ci);

			if (footerTemplate != null)
			{
				Control c = new Control();
				footerTemplate.InstantiateIn(c);
				Controls.Add(c);
			}
		}

		protected override void OnPreRender(EventArgs e)
		{
			base.DataBind();
		}

		#endregion

		internal string RenderAtDesignTime() 
		{ 
			return "TODO create a designer"; 
		}
	}
}
