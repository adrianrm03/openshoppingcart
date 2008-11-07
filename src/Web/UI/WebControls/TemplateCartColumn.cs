using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Serialcoder.ShoppingCart.Web.UI.WebControls
{
	/// <summary>
	/// Summary description for BoundCartColumn.
	/// </summary>
	public class TemplateCartColumn : CartColumn
	{
		public TemplateCartColumn()
		{
		}

		public override void Initialize()
		{
			base.Initialize ();
		}

		public override void InitializeCell(System.Web.UI.WebControls.TableCell cell, int index, CartGridItem item)
		{
			base.InitializeCell (cell, index, item);
			if (item.ItemType == ListItemType.Item)
			{
				ITemplate content = itemTemplate;
                if (content != null) // Test if Skin mode
                {
                    content.InstantiateIn(cell);
                }
			}
		}

		private ITemplate itemTemplate;
		[TemplateContainer(typeof(CartGridItem))]
		public virtual ITemplate ItemTemplate
		{
			get { return itemTemplate; }
			set { itemTemplate = value; }

		}
	}
}
