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
	[System.ComponentModel.ToolboxItem(false)]
	public class CartGridItem : TableRow , INamingContainer 
	{
		#region ctor

		public CartGridItem()
		{
		}

		public CartGridItem(ListItemType itemType, CartItem item, List<CartColumn> cols)
		{
			this.Item = item;
			this.ItemType = itemType;
		}

		#endregion

		#region Properties

		private int position;
		public int Position
		{
			get { return position; }
			set { position = value; }
		}

		ListItemType itemType;
		public ListItemType ItemType
		{
			get { return itemType; }
			set { itemType = value; }
		}

		private CartItem item;
        public CartItem Item
		{
			get { return item; }
			set { item = value; }
		}

		#endregion

		#region Rendering

		protected override void OnPreRender(EventArgs e)
		{
			base.DataBind();
		}

		#endregion

		#region Events

		protected override bool OnBubbleEvent(object source, EventArgs args)
		{
			if (args is CommandEventArgs)
			{
				RaiseBubbleEvent(this,args);
				return true;
			}
			return false;
		}

		#endregion
	}
}
