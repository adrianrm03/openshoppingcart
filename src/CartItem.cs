using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using System.IO;

namespace Serialcoder.ShoppingCart
{
	[Serializable]
    public class CartItem
    {
   		#region ctor

        public CartItem()
		{
			reduce = 0;
			creationDate = DateTime.Now;
			id = Guid.NewGuid();
		}

		#endregion

        #region Properties

        private System.Guid id;
        public System.Guid Id
        {
            get { return id; }
        }

        private DateTime creationDate;
        public DateTime CreationDate
        {
            get { return creationDate; }
        }

        private object productId;
        public object ProductId
        {
            get { return productId; }
            set { productId = value; }
        }

        private string code;
        public string Code
        {
            get { return code; }
            set { code = value; }
        }

        private string description;
        public string Description
        {
            get { return description; }
            set { description = value; }
        }

        private int quantity;
        public int Quantity
        {
            get { return quantity; }
            set
            {
                if (value > 0 && value < 9999999)
                {
                    quantity = value;
                }
                else
                {
                    throw new Exception("Quantity out of interval");
                }
            }
        }

        private decimal publicPrice;
        public decimal PublicPrice
        {
            get { return publicPrice; }
            set { publicPrice = value; }
        }

        public decimal RealPrice
        {
            get { return PublicPrice * (decimal)(1 - reduce); }
        }

        private int unitSale;
        public int UnitSale
        {
            get { return unitSale; }
            set
            {
                if (value <= 0)
                    throw new Exception("This value must be greater than zero");
                unitSale = value;
            }
        }

        private double reduce;
        public double Reduce
        {
            get { return reduce; }
            set
            {
                if (value > 1 || value < 0)
                    throw new Exception("reduce must be between 0 to 1");
                reduce = value;
            }
        }

        private string availability;
        public string Availability
        {
            get { return availability; }
            set { availability = value; }
        }

        private string customerCode;
        public string CustomerCode
        {
            get { return customerCode; }
            set { customerCode = value; }
        }

        private double taxRate;
        public double TaxRate
        {
            get { return taxRate; }
            set { taxRate = value; }
        }

        DateTime shippingDate;
        public DateTime ShippingDate
        {
            get { return shippingDate; }
            set { shippingDate = value; }
        }

        public decimal FreeTaxAmount
        {
            get
            {
                return (Convert.ToDecimal(this.quantity) / Convert.ToDecimal(this.unitSale)) * RealPrice;
            }
        }

        public decimal TaxAmount
        {
            get
            {
                return Amount - FreeTaxAmount;
            }
        }

        public decimal Amount
        {
            get
            {
                return FreeTaxAmount * (1 + (decimal)taxRate);
            }
        }

        private string productLink;
        public string ProductLink
        {
            get { return productLink; }
            set { productLink = value; }
        }

        private string imageUrl;
        public string ImageUrl
        {
            get { return imageUrl; }
            set { imageUrl = value; }
        }

        private int packaging = 1;
        public int Packaging
        {
            get { return packaging; }
            set { packaging = value; }
        }

        #endregion

        public string ToXmlString()
        {
            XmlSerializer serializer = new XmlSerializer(this.GetType());
            MemoryStream ms = new MemoryStream();
            serializer.Serialize(ms, this);
            string result = System.Text.Encoding.ASCII.GetString(ms.GetBuffer());
            ms.Close();
            return result;
        }
    }
}
