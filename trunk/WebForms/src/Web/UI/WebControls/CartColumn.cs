using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ShoppingCart.Web.UI.WebControls
{
	public abstract class CartColumn : WebControl, IValidator
	{
		public CartColumn()
		{
			EnableViewState = true;
			IsValid = true;
		}

		#region Properties

		public virtual string HeaderText
		{
			get
			{
				if (ViewState["HeaderText"] == null)
				{
					return string.Empty;
				}
				return (string) ViewState["HeaderText"];
			}
			set
			{
				ViewState["HeaderText"] = value;
			}
		}

		TableItemStyle headerStyle;
		[PersistenceMode (PersistenceMode.InnerProperty)]
		public virtual TableItemStyle HeaderStyle
		{
			get
			{
				if (headerStyle == null)
				{
					headerStyle = new TableItemStyle();
				}
				return headerStyle;
			}
		}

		TableItemStyle itemStyle;
		[PersistenceMode (PersistenceMode.InnerProperty)]
		public virtual TableItemStyle ItemStyle
		{
			get
			{
				if (itemStyle == null)
				{
					itemStyle = new TableItemStyle();
				}
				return itemStyle;
			}
		}

		#endregion

		#region Initialize

		public virtual void Initialize()
		{

		}

		public virtual void InitializeCell(TableCell cell, int index, CartGridItem item)
		{
			if (item.ItemType == ListItemType.Header)
			{
				InitializeCellHeader(cell , index);
			}
			else if(item.ItemType == ListItemType.Item)
			{
				cell.ApplyStyle(ItemStyle);
			}
		}

		private void InitializeCellHeader(TableCell cell, int index)
		{
			if (HeaderText.Length > 0)
			{
				cell.Text = HeaderText;
			}
			else
			{
				cell.Text = "&nbsp;";
			}
			cell.ApplyStyle(HeaderStyle);
		}

		#endregion

		#region IValidator Members

		public virtual void Validate()
		{
		}

		private bool isValid;
		public bool IsValid
		{
			get
			{
				return isValid;
			}
			set
			{
				isValid = value;
			}
		}

		private string errorMessage;
		public string ErrorMessage
		{
			get
			{
				return errorMessage;
			}
			set
			{
				errorMessage = value;
			}
		}

		#endregion
	}
}
