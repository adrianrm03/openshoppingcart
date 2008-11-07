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
using System.Collections.Generic;

namespace Serialcoder.ShoppingCart.Web.UI.WebControls
{
	[ToolboxData("<{0}:Cart runat=\"server\"></{0}:Cart>")]
	[DefaultProperty("Columns")]
    [ToolboxItem(false)]
	[Designer(typeof(Designers.CartGridDesigner))]
	public class CartGrid : WebControl, INamingContainer
	{
		#region ctor

		public CartGrid()
		{
		}

		#endregion

		#region Properties

		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);
			if (Context.Request.HttpMethod == "GET")
			{
				if (Cart.Context["ContinueShoppingLink"] == null)
				{
					if (Context.Request.UrlReferrer != null)
					{
						Cart.Context.Add("ContinueShoppingLink", Context.Request.UrlReferrer.PathAndQuery);
					}
				}
				else
				{
					if (Context.Request.UrlReferrer != null)
					{
						Cart.Context["ContinueShoppingLink"] = Context.Request.UrlReferrer.PathAndQuery;
					}
				}
			}
		}

		[Bindable(true)]
		[Category("Behavior")]
		[Description("Show Amount without tax")]
		[DefaultValue(true)]
		public bool ShowFreeTaxValues
		{
			get 
			{ 
				if (ViewState["ShowFreeTaxValues"] == null)
				{
					return true;
				}
				return (bool) ViewState["ShowFreeTaxValues"]; 
			}
			set 
			{ 
				ViewState["ShowFreeTaxValues"] = value; 
			}
		}

		[Bindable(true)]
		[Category("Behavior")]
		[Description("Allow Zero Quantity")]
		[DefaultValue(false)]
		public bool AllowZeroQuantity
		{
			get
			{
				if (ViewState["AllowZeroQuantity"] == null)
				{
					return false;
				}
				return (bool) ViewState["AllowZeroQuantity"];
			}
			set
			{
				ViewState["AllowZeroQuantity"] = value;
			}
		}

		[Bindable(true)]
		[Category("Behavior")]
		[Description("Referer")]
		[DefaultValue(false)]
		public string ContinueShoppingLink
		{
			get 
			{
				if (DesignMode)
				{
					return null;
				}
				string s = (string) Cart.Context["ContinueShoppingLink"];
				if (s == null)
				{
					return "/";
				}
				return s;
			}
		}

		[Bindable(false)]
		public decimal FreeTaxAmount
		{
			get 
			{
				return Cart.FreeTaxAmount; 
			}
		}

		[Bindable(false)]
		public decimal TaxAmount
		{
			get { return Cart.TaxAmount; }
		}

		[Bindable(false)]
		public decimal Amount
		{
			get { return Cart.Amount; }
		}

		[Bindable(false)]
		public int Count
		{
			get { return Cart.Count; }
		}

		[Bindable(false)]
		public DateTime CreationDate
		{
			get { return Cart.CreationDate; } 
		}

		private List<CartColumn> columns;
		public virtual List<CartColumn> Columns
		{
			get
			{
				if (columns == null)
				{
					columns = new List<CartColumn>();
				}
				return columns;
			}
		}

		public override ControlCollection Controls
		{
			get
			{
				EnsureChildControls();
				return base.Controls;
			}
		}


		List<CartGridItem> m_items;
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
 		public List<CartGridItem> Items
		{
			get 
			{ 
				if (m_items == null)
				{
					m_items = new List<CartGridItem>();
				}
				return m_items; 
			}
			set { m_items = value; }
		}

		#endregion

		#region Events

		/*
		public event CartGridEventHandler DeleteCommand
		{
			add 
			{
				Events.AddHandler(
			}
			remove
			{
			}
		}
		*/

		#endregion

		#region Rendering

		protected override void AddAttributesToRender(HtmlTextWriter writer)
		{
			base.AddAttributesToRender (writer);
		}

		protected virtual void CreateControlHierarchy()
		{
			Context.Trace.Write("CartGrid","CreateChildControls");

			Controls.Clear();

			if (Cart.Count == 0)
			{
				if (EmptyCartTemplate != null)
				{
					Control c = new Control();
					EmptyCartTemplate.InstantiateIn(c);
					Controls.Add(c);
				}
				return;
			}

			if (HeaderTemplate != null)
			{
				Control c = new Control();
				HeaderTemplate.InstantiateIn(c);
				Controls.Add(c);
			}

			Table table = new Table(); 
			
			Controls.Add(table);

			// Create Header
			CartGridItem item = new CartGridItem(ListItemType.Header, null,Columns);
			InitializeItem(item);
			table.Rows.Add(item);

			// Bind CartItems
			for(int i=0; i < Cart.Count; i++)
			{
				CartGridItem cgItem = new CartGridItem(ListItemType.Item,Cart.SelectItemByIndex(i), Columns);
				cgItem.ID = "CartGridItem" + i;
				InitializeItem(cgItem);
				table.Rows.Add(cgItem);
				Items.Add(cgItem);
			}

			// Create Footer
			if (FooterTemplate != null)
			{
				TableCell cell = new TableCell();
				FooterTemplate.InstantiateIn(cell);
				cell.ColumnSpan = Columns.Count;
				TableRow row = new TableRow();
				row.Cells.Add(cell);
				table.Rows.Add(row);
			}
		}

		protected virtual void InitializeItem(CartGridItem item)
		{
			TableCellCollection cells = item.Cells;
			TableCell cell;
			for(int i =0; i < Columns.Count; i++)
			{
				cell = new TableCell();
				Columns[i].InitializeCell(cell,i,item);
				cells.Add(cell);
			}
		}

		protected override void CreateChildControls()
		{
			Context.Trace.Write("CartGrid","CreateChildControls");
			Controls.Clear();
			CreateControlHierarchy();
			ClearChildViewState();
		}

		protected override void OnPreRender(EventArgs e)
		{
			Context.Trace.Write("CartGrid","OnPreRender");
			if (Page.IsPostBack)
			{
				CreateControlHierarchy();
			}
			base.OnPreRender (e);
			base.DataBind();
		}

		protected override void Render(HtmlTextWriter writer)
		{
			if (Controls.Count > 0 && Cart.Count > 0)
			{
				foreach(Control c in Controls)
				{
					if (c is Table)
					{
						Table table = c as Table;
						if (table.ID == null)
						{
							table.Attributes.Add("id", this.ID);
						}
						table.CopyBaseAttributes(this);
						break;
					}
				}
			}
			RenderContents(writer);
		}


		#endregion

		#region Templates

		ITemplate headerTemplate;
		[TemplateContainer(typeof(CartGrid))]
		[PersistenceMode(PersistenceMode.InnerDefaultProperty)]
		[Bindable(false)]
		[Browsable(false)]
		public ITemplate HeaderTemplate
		{
			get { return headerTemplate; }
			set { headerTemplate = value; }
		}

		ITemplate footerTemplate;
		[TemplateContainer(typeof(CartGrid))]
		[PersistenceMode(PersistenceMode.InnerDefaultProperty)]
		[Bindable(false)]
		[Browsable(false)]
		public ITemplate FooterTemplate
		{
			get { return footerTemplate; }
			set { footerTemplate = value; }
		}

		ITemplate emptyCartTemplate;
		[TemplateContainer(typeof(CartGrid))]
		[PersistenceMode(PersistenceMode.InnerDefaultProperty)]
		[Bindable(false)]
		[Browsable(false)]
		public ITemplate EmptyCartTemplate
		{
			get { return emptyCartTemplate; }
			set { emptyCartTemplate = value; }
		}

		#endregion

	}
}
