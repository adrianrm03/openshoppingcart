using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.Design;
using System.Drawing;
using System.Drawing.Design;
using System.ComponentModel;
using System.Xml;
using System.Xml.Serialization;
using System.Globalization;
using System.Collections;

namespace Serialcoder.ShoppingCart.Web.UI.WebControls
{
	[ParseChildren(false)]
	[PersistChildrenAttribute(false)]
	/// <summary>
	/// 
	/// </summary>
	public abstract class EmptyCart : WebControl, IPostBackEventHandler
	{
		public EmptyCart()
		{
		}

		private void Empty()
		{
			Context.Trace.Write("EmptyCartControl","EmptyCart");
			Cart.Clear();
		}

		#region IPostBackEventHandler Members

		public void RaisePostBackEvent(string eventArgument)
		{
			Context.Trace.Write("EmptyCartControl","RaisePostBackEvent");
			Empty();
		}

		#endregion
	}
}
