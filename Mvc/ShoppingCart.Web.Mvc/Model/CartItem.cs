using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ShoppingCart.Web.Mvc.Model
{
	/// <summary>
	/// Détail du panier
	/// </summary>
	[Serializable]
	public class CartItem
	{
		public CartItem()
		{
			Id = Guid.NewGuid().ToString();
		}

		/// <summary>
		/// Identifiant interne de la ligne
		/// </summary>
		/// <value>The id.</value>
		public string Id { get; private set; }
		/// <summary>
		/// Le produit selectionné
		/// </summary>
		/// <value>The product.</value>
		public string ProductCode { get; set; }
		/// <summary>
		/// La quantité
		/// </summary>
		/// <value>The quantity.</value>
		public int Quantity { get; set; }
		/// <summary>
		/// Le prix de vente 
		/// </summary>
		/// <value>The sale price.</value>
		public Price SalePrice { get; set; }
		/// <summary>
		/// Unité de vente
		/// </summary>
		/// <value>The sale unit value.</value>
		public int SaleUnitValue { get; set; }
		/// <summary>
		/// Packaging
		/// </summary>
		/// <value>The packaging.</value>
		public int Packaging { get; set; }
		/// <summary>
		/// Montant de l'eco taxe
		/// </summary>
		/// <remarks>
		/// s'il n'y a pas d'eco taxe sur le produit cette valeur est nulle
		/// </remarks>
		/// <value>The recycle price.</value>
		public Price RecyclePrice { get; set; }

		#region Totals

		/// <summary>
		/// Montant total HT de l'eco taxe
		/// </summary>
		/// <value>The recycle total.</value>
		public virtual decimal RecycleTotal
		{
			get
			{
				if (RecyclePrice == null)
				{
					return 0;
				}
				return Quantity * RecyclePrice.Value;
			}
		}
		/// <summary>
		/// Montant de la tva de l'eco taxe
		/// </summary>
		/// <value>The recycle tax total.</value>
		public virtual decimal RecycleTaxTotal
		{
			get
			{
				if (RecyclePrice == null)
				{
					return 0;
				}
				return Quantity * RecyclePrice.TaxValue;
			}
		}
		/// <summary>
		/// Montant total TTC de l'eco taxe
		/// </summary>
		/// <value>The recycle total with tax.</value>
		public virtual decimal RecycleTotalWithTax
		{
			get
			{
				if (RecyclePrice == null)
				{
					return 0;
				}
				return Quantity * RecyclePrice.ValueWithTax;
			}
		}
		/// <summary>
		/// Montant total de la ligne HT hors eco taxe
		/// </summary>
		/// <value>The total.</value>
		public virtual decimal Total
		{
			get
			{
				return Convert.ToDecimal((Quantity / (SaleUnitValue * 1.0))) * SalePrice.Value;
			}
		}
		/// <summary>
		/// Montant total de la tva de la ligne hors eco taxe
		/// </summary>
		/// <value>The total tax.</value>
		public virtual decimal TotalTax
		{
			get
			{
				return new Price(Total, SalePrice.TaxRate).TaxValue;
			}
		}

		/// <summary>
		/// Montant total ttc de la ligne hors eco taxe
		/// </summary>
		/// <value>The total with tax.</value>
		public virtual decimal TotalWithTax
		{
			get
			{
				return new Price(Total, SalePrice.TaxRate).ValueWithTax;
			}
		}
		/// <summary>
		/// Montant total ht de la ligne, y compris l'eco taxe
		/// </summary>
		/// <value>The grand total.</value>
		public virtual decimal GrandTotal
		{
			get
			{
				return Total + RecycleTotal;
			}
		}
		/// <summary>
		/// Montant total de la tva , y compris l'eco taxe
		/// </summary>
		/// <value>The grand tax total.</value>
		public virtual decimal GrandTaxTotal
		{
			get
			{
				return TotalTax + RecycleTaxTotal;
			}
		}
		/// <summary>
		/// Montant total ttc de la ligne, y compris l'eco taxe
		/// </summary>
		/// <value>The grand total with tax.</value>
		public virtual decimal GrandTotalWithTax
		{
			get
			{
				return TotalWithTax + RecycleTotalWithTax;
			}
		}

		#endregion
	}
}
