using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace ShoppingCart.Web.Mvc.Services
{
	public class CartService : ICartService 
	{
		public CartService(ICartRepository cartRepository)
		{
			this.CartRepository = cartRepository;
		}

		protected ICartRepository CartRepository { get; set; }

		public Model.Cart GetOrCreateCart(string visitorId)
		{
			var cartId = CartRepository.GetCartId();
			var cart = CartRepository[cartId] as Model.Cart;
			if (cart != null)
			{
				return cart;
			}
			// Recherche dans le cache sur le visitorId
			cart = CartRepository.GetList().FirstOrDefault(i => i.VisitorId.Equals(visitorId, StringComparison.InvariantCultureIgnoreCase));
			if (cart != null)
			{
				return cart;
			}
			cart = CreateCart(visitorId);
			cart.Code = cartId;
			CartRepository.Save(cart);
			return cart;
		}

		public Model.Cart GetCurrent()
		{
			var cartId = CartRepository.GetCartId();
			var cart = GetCartById(cartId);
			return cart;
		}

		public Model.Cart CreateCart(string visitorId)
		{
			var cart = new Model.Cart();
			cart.VisitorId = visitorId;
			return cart;
		}

		public Model.Cart CreateAndSaveCart(string visitorId)
		{
			var cart = new Model.Cart();
			cart.VisitorId = visitorId;
			Save(cart);
			return cart;
		}

		public void Save(Model.Cart cart)
		{
			CartRepository.Save(cart);
		}

		public Model.Cart GetCartById(string cartId)
		{
			return CartRepository[cartId] as Model.Cart;
		}

		public IList<Model.Cart> GetCurrentList(string visitorId)
		{
			var list = CartRepository.GetList().Where(i => i.VisitorId.Equals(visitorId, StringComparison.InvariantCultureIgnoreCase));
			return list.ToList();
		}

		public void RemoveCart(Model.Cart cart)
		{
			CartRepository.Remove(cart);
		}

		public void AddItem(Model.Cart cart, string productCode, int saleUnitValue, int packagingValue, int quantity, Model.Price salePrice)
		{
			var existing = cart.Items.SingleOrDefault(i => i.ProductCode.Equals(productCode, StringComparison.InvariantCultureIgnoreCase));
			if (existing == null)
			{
				cart.Add(new Model.CartItem()
				{
					ProductCode = productCode,
					Quantity = quantity,
					SalePrice = salePrice,
					SaleUnitValue = saleUnitValue,
					Packaging = packagingValue,
				});
			}
			else
			{
				existing.Quantity += quantity;
			}
		}

		public void RemoveItem(Model.Cart cart, int index)
		{
			if (index < cart.ItemCount)
			{
				var item = cart.Items[index];
				cart.Items.Remove(item);
			}
		}

		public void Clear(Model.Cart cart)
		{
			if (cart != null)
			{
				cart.Items.Clear();
			}
		}

		public void ChangeCurrent(string cartId)
		{
			CartRepository.ChangeCurrent(cartId);
		}

		public void DeleteCart(string cartId)
		{
			var current = GetCurrent();
			if (current != null && current.Code.Equals(cartId, StringComparison.InvariantCultureIgnoreCase))
			{
				CartRepository.Remove(cartId);
			}
			else
			{
				var list = GetCurrentList(current.VisitorId);
				if (list == null || list.Count == 0)
				{
					ChangeCurrent(list.First().Code);
				}
			}
			CartRepository.Remove(cartId);
		}

		public void AddCart(Model.Cart cart)
		{
			CartRepository.Save(cart);
		}

		public void RegisterRoutes(System.Web.Routing.RouteCollection routes)
		{
			var nameSpaces = new string[] { "ShoppingCart.Web.Mvc.Controllers" };

			routes.MapRoute(
				"CartItemAdd"
				, "cart/add/{*productCode}" 
				, new { controller = "Cart", action = "AddItem" , productCode = string.Empty }
				, null
				, nameSpaces
			);

			routes.MapRoute(
				"CartRecalc"
				, "cart/recalc" 
				, new { controller = "Cart", action = "Recalc" }
				, null
				, nameSpaces
			);

			routes.MapRoute(
				"CartItemDelete"
				, "cart/remove/{index}"
				, new { controller = "Cart", action = "RemoveItem", index = 0 }
				, new { index = @"\d+" }
				, nameSpaces
			);

			routes.MapRoute(
				"CartClear"
				, "cart/clear" 
				, new { controller = "Cart", action = "Clear" }
				, null
				, nameSpaces
			);

			routes.MapRoute(
				"CartDelete"
				, "cart/delete/{cartId}" 
				, new { controller = "Cart", action = "Delete", cartId = string.Empty }
				, null
				, nameSpaces
			);

			routes.MapRoute(
				"CartChange"
				,"cart/change/{cartId}"
				, new { controller = "Cart", action = "Change", cartId = string.Empty }
				, null
				, nameSpaces
			);

			routes.MapRoute(
				"CartShow"
				, "cart/show/{cartId}" 
				, new { controller = "Cart", action = "Show", cartId = string.Empty }
				, null
				, nameSpaces
			);

			routes.MapRoute(
				"CartCreate"
				, "cart/create" 
				, new { controller = "Cart", action = "Create" }
				, null
				, nameSpaces
			);

			routes.MapRoute(
				"Cart"
				, "cart" 
				, new { controller = "Cart", action = "Index", id = string.Empty }
				, null
				, nameSpaces
			);

		}
	}
}
