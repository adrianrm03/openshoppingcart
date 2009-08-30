using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ShoppingCart.Web.UI.WebControls
{
	/// <summary>
	/// Summary description for DeleteCartColumn.
	/// </summary>
	public class DeleteCartColumnImageButton : CartColumn
	{
        public DeleteCartColumnImageButton()
		{
		}

		#region Properties

		public string Src
		{
			get
			{
                if (ViewState["Src"] == null)
				{
					return string.Empty;
				}
                return (string)ViewState["Src"];
			}
			set
			{
                ViewState["Src"] = value;
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
                ImageButton button = new ImageButton();
				button.CommandName = "Delete";
				button.CommandArgument = item.Item.Id.ToString();
				button.Command += new CommandEventHandler(button_Command);
                button.ImageUrl = this.Src;
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
