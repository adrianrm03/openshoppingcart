using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using ShoppingCart.Web.Mvc.Html;

namespace ShoppingCart.Web.Mvc.Controllers
{
	[HandleError]
    public class CartController : Controller
    {
		public CartController()
			: this(System.Web.HttpContext.Current.Application["cartService"] as Services.ICartService
			,System.Web.HttpContext.Current.Application["catalogService"] as Services.ICatalogService
			,System.Web.HttpContext.Current.Application["userService"] as Services.IUserService)
		{
		}

		public CartController(Services.ICartService cartService
			,Services.ICatalogService catalogService
			,Services.IUserService userService)
		{
			this.CartService = cartService;
			this.CatalogService = catalogService;
			this.UserService = userService;
		}

		protected Services.ICartService CartService { get; set; }

		protected Services.ICatalogService CatalogService { get; set; }

		protected Services.IUserService UserService { get; set; }

		#region Cart

		public ActionResult Index()
        {
			var cart = CartService.GetCurrent();
			if (cart == null)
			{
				return View("EmptyCart");
			}
			ViewData.Model = CartService.GetCurrent();
            return View();
        }

		public ActionResult AddItem(string productCode)
		{
			var product = CatalogService.GetProductByCode(productCode);
			if (product == null)
			{
				return RedirectToRoute("Default");
			}

			var cart = CartService.GetOrCreateCart(UserService.GetVisitorId());
			var price = CatalogService.GetPriceByProduct(product);
			CartService.AddItem(cart, product.Code, product.SaleUnitValue, product.Packaging, product.Packaging, price);
			ViewData.Model = cart;
			return RedirectToAction("Index");
		}

		#region Ajax

		public ActionResult JsAddItem(string productCode)
		{
			var product = CatalogService.GetProductByCode(productCode);
			return JsAddItemToCart(product, product.Packaging);
		}

		public ActionResult JsAddItemWithQuantity(string productCode, int quantity)
		{
			var product = CatalogService.GetProductByCode(productCode);
			return JsAddItemToCart(product, quantity);
		}

		private ActionResult JsAddItemToCart(ShoppingCart.Web.Mvc.Model.IProduct product, int quantity)
		{
			if (product == null)
			{
				return new JsonResult();
			}
			var price = CatalogService.GetPriceByProduct(product);
			var cart = CartService.GetOrCreateCart(UserService.GetVisitorId());
			CartService.AddItem(cart, product.Code, product.SaleUnitValue, product.Packaging, quantity, price);

			var urlHelper = new UrlHelper(this.ControllerContext.RequestContext);

			return Json(new
			{
				status = cart.GetStatusText(),
				cartTotal = cart.TotalWithTax.ToString("#,#0.00"),
				title = product.Title,
				quantity = quantity,
				cartUrl = urlHelper.CartHref(),
			});
		}

		#endregion

		public ActionResult Clear()
		{
			var cart = CartService.GetCurrent();
			CartService.Clear(cart);
			ViewData.Model = cart;
			return View("EmptyCart");
		}

		[AcceptVerbs(HttpVerbs.Post)]
		public ActionResult Recalc(FormCollection form)
		{
			var cart = CartService.GetCurrent();
			if (form["quantity"] == null)
			{
				ViewData.Model = cart;
				return View("Index");
			}
			var values = form["quantity"].Split(',');

			for (int i = 0; i < cart.ItemCount; i++)
			{
				try
				{
					var cartItem = cart.Items[i];
					int quantity = cartItem.Packaging;
					int.TryParse(values[i], out quantity);
					cartItem.Quantity = quantity % cartItem.Packaging;
					cartItem.Quantity = Math.Max(cartItem.Packaging, quantity - cartItem.Quantity);
				}
				catch
				{
					continue;
				}
			}
			ViewData.Model = cart;
			return View("Index");
		}

		[AcceptVerbs(HttpVerbs.Get)]
		public ActionResult RemoveItem(int index)
		{
			var cart = CartService.GetCurrent();
			CartService.RemoveItem(cart, index);
			if (cart == null
				|| cart.ItemCount == 0)
			{
				return View("EmptyCart");
			}
			ViewData.Model = cart;
			return View("Index");
		}

		#region Partial Rendering

		public ActionResult ShowStatus(string viewName)
		{
			var cart = CartService.GetCurrent();
			ViewData.Model = cart;
			return View(string.Format("~/views/cart/{0}", viewName));
		}

		#endregion

		#endregion

		#region Cart List

		public ActionResult Delete(string cartId)
		{
			CartService.DeleteCart(cartId);
			return RedirectToAction("Index");
		}

		public ActionResult Change(string cartId)
		{
			CartService.ChangeCurrent(cartId);
			return RedirectToAction("Index");
		}

		public ActionResult Show(string cartId)
		{
			var cart = CartService.GetCartById(cartId);
			if (cart.ItemCount == 0)
			{
				return View("EmptyCart");
			}
			ViewData.Model = cart;
			return View("Index");
		}

		public ActionResult Create()
		{
			var cart = CartService.CreateCart(UserService.GetVisitorId());
			CartService.AddCart(cart);
			CartService.ChangeCurrent(cart.Code);
			return Redirect("/");
		}

		#region Partial Rendering

		public ActionResult ShowCurrentCartList(string viewName)
		{
			var list = CartService.GetCurrentList(UserService.GetVisitorId());
			if (list == null)
			{
				list = new List<Model.Cart>();
			}
			ViewData.Model = list;
			return View(string.Format("~/views/cart/{0}", viewName));
		}

		#endregion

		#endregion

	}
}
