using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ShoppingCart.Web.Mvc.Services
{
	public interface ICartRepository
	{
		string GetCartId();
		Model.Cart this[string cartId] { get; }
		void Save(Model.Cart cart);
		void Remove(Model.Cart cart);
		void Remove(string cartId);
		IQueryable<Model.Cart> GetList();
		void ChangeCurrent(string cartId);
	}
}
