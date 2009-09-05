using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ShoppingCart.Web.Mvc.Model
{
	/// <summary>
	/// Panier de commande
	/// </summary>
	[Serializable]
	public class Cart
	{
		private List<CartItem> m_CartItemList;

		public Cart()
		{
			CreationDate = DateTime.Now;
			m_CartItemList = new List<CartItem>();
			Code = Guid.NewGuid().ToString();
		}

		/// <summary>
		/// Code du panier généralement un GUID
		/// </summary>
		/// <value>The code.</value>
		public string Code { get; set; }
		/// <summary>
		/// Date de création du panier
		/// </summary>
		/// <value>The creation date.</value>
		public DateTime CreationDate { get; set; }
		/// <summary>
		/// Identifiant interne du client s'il est identifié
		/// </summary>
		/// <value>The customer id.</value>
		public int? CustomerId { get; set; }
		/// <summary>
		/// Identitifiant du visiteur
		/// </summary>
		/// <value>The visitor id.</value>
		public string VisitorId { get; set; }
		/// <summary>
		/// Retourne le nombre d'item dans le panier
		/// </summary>
		/// <value>The item count.</value>
		public int ItemCount
		{
			get
			{
				if (m_CartItemList == null)
				{
					return 0;
				}
				return m_CartItemList.Count();
			}
		}

		public void Add(CartItem item)
		{
			m_CartItemList.Add(item);
		}

		/// <summary>
		/// Liste des items du panier
		/// </summary>
		/// <value>The items.</value>
		public List<CartItem> Items
		{
			get
			{
				return m_CartItemList;
			}
		}

		#region Totals

		/// <summary>
		/// Montant total HT du panier sans l'eco participation
		/// </summary>
		/// <value>The total.</value>
		public decimal Total
		{
			get
			{
				return Items.Sum(i => i.Total);
			}
		}

		/// <summary>
		/// Montant de la tva du panier sans l'eco participation
		/// </summary>
		/// <value>The total tax.</value>
		public decimal TotalTax
		{
			get
			{
				return Items.Sum(i => i.TotalTax);
			}
		}

		/// <summary>
		/// Montant total TTC du panier sans l'eco participation
		/// </summary>
		/// <value>The total with tax.</value>
		public decimal TotalWithTax
		{
			get
			{
				return Items.Sum(i => i.TotalWithTax);
			}
		}

		/// <summary>
		/// Montant total ht de l'eco participation
		/// </summary>
		/// <value>The recycle total.</value>
		public decimal RecycleTotal
		{
			get
			{
				return Items.Sum(i => i.RecycleTotal);
			}
		}
		/// <summary>
		/// Montant total de la tva de l'eco participation
		/// </summary>
		/// <value>The recycle tax total.</value>
		public decimal RecycleTaxTotal
		{
			get
			{
				return Items.Sum(i => i.RecycleTaxTotal);
			}
		}
		/// <summary>
		/// Montant total ttc de l'eco participation
		/// </summary>
		/// <value>The recycle total with tax.</value>
		public decimal RecycleTotalWithTax
		{
			get
			{
				return Items.Sum(i => i.RecycleTotalWithTax);
			}
		}
		/// <summary>
		/// Montant total ht du panier y compris l'eco participation
		/// </summary>
		/// <value>The grand total.</value>
		public decimal GrandTotal
		{
			get
			{
				return Total + RecycleTotal;
			}
		}
		/// <summary>
		/// Montant total de la tva y compris avec l'eco participation
		/// </summary>
		/// <value>The grand tax total.</value>
		public decimal GrandTaxTotal
		{
			get
			{
				return TotalTax + RecycleTaxTotal ;
			}
		}
		/// <summary>
		/// Montant total ttc du panier y compris avec l'eco participation
		/// </summary>
		/// <value>The grand total with tax.</value>
		public decimal GrandTotalWithTax
		{
			get
			{
				return TotalWithTax + RecycleTotalWithTax;
			}
		}

		#endregion
	}
}