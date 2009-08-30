using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using System.Web.Mvc;

namespace ShoppingCart.Web.Mvc.Tests.Controllers
{
	/// <summary>
	/// Summary description for CartControllerTests
	/// </summary>
	[TestFixture]
	public class CartControllerTests
	{
		private ShoppingCart.Web.Mvc.Controllers.CartController m_CartController;
		private ShoppingCart.Web.Mvc.Services.ICatalogService m_CatalogService;
		private ShoppingCart.Web.Mvc.Services.ICartService m_CartService;

		public CartControllerTests()
		{
		}

		[SetUp]
		public void Initialize()
		{
			// base.Initialize();
			m_CartController = CreateCartController();
		}

		ShoppingCart.Web.Mvc.Controllers.CartController CreateCartController()
		{
			var mock = new Moq.Mock<ControllerContext>();

			var cartRepository = new Services.MockCartRepository();
			var cartService = new ShoppingCart.Web.Mvc.Services.CartService(cartRepository);

			var product = new Moq.Mock<Model.IProduct>();
			product.Setup(i => i.Code).Returns("xxx");
			product.Setup(i => i.Packaging).Returns(1);
			product.Setup(i => i.SaleUnitValue).Returns(1);
			product.Setup(i => i.Title).Returns("product 1");

			var catalogService = new Moq.Mock<Mvc.Services.ICatalogService>();
			catalogService.Setup(i => i.GetProductByCode("xxx")).Returns(product.Object);
			catalogService.Setup(i => i.GetPriceByProduct(product.Object)).Returns(new ShoppingCart.Web.Mvc.Model.Price(10.0, 0.196));

			var userService = new Moq.Mock<Mvc.Services.IUserService>();
			userService.Setup(i => i.GetVisitorId()).Returns("vid");

			var controller = new ShoppingCart.Web.Mvc.Controllers.CartController(
				cartService, 
				catalogService.Object,
				userService.Object);

			controller.ControllerContext = mock.Object;

			return controller;
		}

		[Test]
		public void New_Cart_Index()
		{
			var result = m_CartController.AddItem("xxx") as RedirectToRouteResult;
			var resultIndex = m_CartController.Index() as ViewResult;

			var currentCart = resultIndex.ViewData.Model as Model.Cart;

			Assert.IsNotNull(currentCart);
		}

		[Test]
		public void Add_To_Cart()
		{
			var result = m_CartController.AddItem("xxx") as RedirectToRouteResult;
			Assert.IsInstanceOfType(typeof(RedirectToRouteResult), result);
			Assert.AreEqual("Index", result.RouteValues["action"]);
		}

		[Test]
		public void Clear_Cart()
		{
			var result = m_CartController.AddItem("XBOX360") as System.Web.Mvc.ViewResult;
			var cart = result.ViewData.Model as Model.Cart;

			Assert.IsNotNull(cart);

			var clearResult = m_CartController.Clear() as System.Web.Mvc.ViewResult;
			cart = clearResult.ViewData.Model as Model.Cart;

			Assert.AreEqual(cart.ItemCount, 0);
			Assert.AreEqual(clearResult.ViewName, "EmptyCart");
		}
	}
}
