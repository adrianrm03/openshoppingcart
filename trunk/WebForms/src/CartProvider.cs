using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace ShoppingCart
{
    public abstract class CartProvider : System.Configuration.Provider.ProviderBase
    {
        public override void Initialize(string name, System.Collections.Specialized.NameValueCollection config)
        {
            this.name = name;
            base.Initialize(name, config);

            minimalOrder = (config["minimalOrder"] == null) ? 0 : decimal.Parse(config["minimalOrder"]);
            allowSameProduct = (config["allowSameProduct"] == null) ? false : bool.Parse(config["allowSameProduct"]);
        }

        private string name;
        public override string Name
        {
            get
            {
                return name;
            }
        }

        #region properties

        public abstract List<CartItem> List { get; set; }
        public abstract object Id { get ; set ; }
        public abstract DateTime CreationDate { get ; set ; }

        public decimal FreeTaxAmount
        {
            get
            {
                decimal amount = 0;
                foreach (CartItem item in List)
                {
                    amount += item.FreeTaxAmount;
                }
                return amount;
            }
        }

        public decimal TaxAmount
        {
            get
            {
                decimal amount = 0;
                foreach (CartItem item in List)
                {
                    amount += item.TaxAmount;
                }
                return amount;
            }
        }

        public decimal Amount
        {
            get
            {
                return FreeTaxAmount + TaxAmount;
            }
        }

        public CartStatus Status
        {
            get
            {
                if (Count == 0)
                {
                    return CartStatus.Empty;
                }
                if (Count == 1)
                {
                    return CartStatus.OneItem;
                }
                return CartStatus.ManyItems;
            }
        }

        public int Count
        {
            get
            {
                if (List != null)
                {
                    return List.Count;
                }
                return 0;
            }
        }

		public int DistinctProductCount
		{
			get
			{
				if (List != null)
				{
					return List.Select(i => i.ProductId).Distinct().Count();
				}
				return 0;
			}
		}

        private decimal minimalOrder;
        public decimal MinimalOrder
        {
            get
            {
                return minimalOrder;
            }
        }

        private bool allowSameProduct;
        public bool AllowSameProduct
        {
            get
            {
                return allowSameProduct;
            }
        }
 
        #endregion

        #region Methods

        public virtual CartItem this[int index]
        {
            get { return (CartItem)List[index]; }
            set { List[index] = value; }
        }

        public virtual CartItem this[object id]
        {
            get
            {
                return List.Find(delegate(CartItem item)
                {
                    return item.Id.Equals(id);
                });
            }
        }

        public virtual void Add(CartItem item)
        {
            if (!AllowSameProduct)
            {
                CartItem existingCartItem = List.Find(delegate(CartItem ci)
                {
                    return ci.ProductId.Equals(item.ProductId);
                });
                if (existingCartItem != null)
                {
                    existingCartItem.Quantity += item.Quantity;
                    return;
                }
            }
            List.Add(item);
            // Set de creationDate on the first addItem
            if (List.Count == 1)
            {
                CreationDate = DateTime.Now;
            }
        }

        public virtual void Remove(CartItem item)
        {
            List.Remove(item);
        }

        public virtual int IndexOf(CartItem item)
        {
            return List.IndexOf(item);
        }

        public virtual void Clear()
        {
            List.Clear();
        }

        public abstract System.Collections.Specialized.HybridDictionary Context { get; set; }

        #endregion
    }
}
