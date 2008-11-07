using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Serialcoder.ShoppingCart.Web.UI.WebControls
{
	/// <summary>
	/// Summary description for QuantityTextBoxCartColumn.
	/// </summary>
	public class QuantityCartColumnTextBox : QuantityCartColumn
	{
		protected TextBox box;

		public QuantityCartColumnTextBox()
		{
		}

		protected override void OnInit(EventArgs e)
		{
			Page.Validators.Add(this);
			base.OnInit (e);
		}

		protected override void OnUnload(EventArgs e)
		{
			Page.Validators.Remove(this);
			base.OnUnload (e);
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
				box = new TextBox();
				box.Text = item.Item.Quantity.ToString();
				box.ID = "QuantityTextBox";
				box.CopyBaseAttributes(this);
				box.TextChanged += new EventHandler(box_TextChanged);
				cell.Controls.Add(box);
			}
		}

		public override void Validate()
		{
			if (box.Text == null)
			{
				IsValid = false;
				ErrorMessage = "Vous devez indiquer une quantité";
				return;
			}
			try
			{
				int.Parse(box.Text);
			}
			catch
			{
				IsValid = false;
				ErrorMessage = "Vous devez indiquer une quantité valide";
				return;
			}
			IsValid = true;
		}

		private void box_TextChanged(object sender, EventArgs e)
		{
			TextBox boxChange = (TextBox) sender;
			// TR/TD
			CartGridItem gi = (CartGridItem) boxChange.Parent.Parent;
			try
			{
				gi.Item.Quantity = int.Parse(boxChange.Text);
			}
			catch
			{
				IsValid = false;
				ErrorMessage = "Vous devez indiquer une quantité valide";
			}
		}
	}
}
