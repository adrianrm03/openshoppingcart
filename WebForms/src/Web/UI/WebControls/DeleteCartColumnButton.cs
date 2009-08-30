using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ShoppingCart.Web.UI.WebControls
{
	/// <summary>
	/// Summary description for DeleteCartColumn.
	/// </summary>
	public class DeleteCartColumnButton : CartColumn
	{
		public DeleteCartColumnButton()
		{
		}

		#region Properties

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

		public string Type
		{
			get
			{
				if (ViewState["Type"] == null)
				{
					return string.Empty;
				}
				return (string) ViewState["Type"];
			}
			set
			{
				ViewState["Type"] = value;
			}
		}

		#endregion

		public override void Initialize()
		{
			base.Initialize ();
		}

		public override void InitializeCell(System.Web.UI.WebControls.TableCell cell, int index, CartGridItem item)
		{
			base.InitializeCell (cell, index, item);
			if (item.ItemType == ListItemType.Item)
			{
				Button button = new Button();
				button.CommandName = "Delete";
				button.CommandArgument = item.Item.Id.ToString();
				button.Command += new CommandEventHandler(button_Command);
				button.Text = this.Text;
				cell.Controls.Add(button);
			}
		}

		private void button_Command(object sender, CommandEventArgs e)
		{
			System.Guid id = new Guid((string) e.CommandArgument);
			CartItem item = Cart.SelectItemByGuid(id);
			if (item != null)
			{
				Cart.RemoveItem(item);
			}
		}
	}
}
