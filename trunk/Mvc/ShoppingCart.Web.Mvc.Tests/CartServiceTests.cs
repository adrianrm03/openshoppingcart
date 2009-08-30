using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;


namespace ShoppingCart.Web.Mvc.Tests.Services
{
	[TestFixture]
	public class CartServiceTests 
	{
		private ShoppingCart.Web.Mvc.Services.ICartService m_CartService;

		public CartServiceTests()
		{

		}

		[SetUp]
		public void Initialize()
		{
			var cartRepository = new MockCartRepository();
			m_CartService = new ShoppingCart.Web.Mvc.Services.CartService(cartRepository);
		}

		#region Cart

		[Test]
		public void Get_Current_Cart()
		{
			var cart = m_CartService.GetCurrent();
			Assert.IsNull(cart);
		}

		[Test]
		public void Get_Or_Create_Cart()
		{
			var cart = m_CartService.GetOrCreateCart("vis1");
			Assert.IsNotNull(cart, "cart must be instancied");
			Assert.AreEqual(cart.VisitorId, "vis1");
		}

		[Test]
		public void Create_Cart()
		{
			var cart = m_CartService.CreateCart("vis1");
			Assert.IsNotNull(cart, "cart must be instancied");
			Assert.AreEqual(cart.VisitorId, "vis1");
		}

		[Test]
		public void Get_Cart_By_Id()
		{
			var cart = m_CartService.CreateCart("vis1");
			var id = cart.Code;
			var result = m_CartService.GetCartById(id);
			Assert.IsNotNull(result);
		}

		[Test]
		public void Add_Item()
		{
			var cart = m_CartService.CreateAndSaveCart("vis1");
			var price = new Model.Price(10.0, 0.196);
			m_CartService.AddItem(cart, "test1", 1, 1, 2, price);
			Assert.AreEqual(cart.ItemCount, 1);
			Assert.AreEqual(cart.Total, 10 * 2);
		}

		[Test]
		public void Remove_Item()
		{
			var cart = m_CartService.CreateAndSaveCart("vis1");

			var price1 = new Model.Price(10.0, 0.196);
			m_CartService.AddItem(cart, "test1", 1, 1, 2, price1);

			var price2 = new Model.Price(20.0, 0.196);
			m_CartService.AddItem(cart, "test2", 1, 1, 3, price2);

			Assert.AreEqual(cart.ItemCount, 2);

			m_CartService.RemoveItem(cart, 0);

			Assert.AreEqual(cart.ItemCount, 1);
		}

		[Test]
		public void Clear_Cart()
		{
			var cart = m_CartService.CreateAndSaveCart("vis1");

			var price1 = new Model.Price(10.0, 0.196);
			m_CartService.AddItem(cart, "test1", 1, 1, 2, price1);

			var price2 = new Model.Price(20.0, 0.196);
			m_CartService.AddItem(cart, "test2", 1, 1, 3, price2);

			Assert.AreEqual(cart.ItemCount, 2);

			m_CartService.Clear(cart);

			Assert.AreEqual(cart.ItemCount, 0);
		}

		#endregion

		#region Cart List

		[Test]
		public void Get_Current_List_Empty()
		{
			var list = m_CartService.GetCurrentList("vid");
			Assert.AreEqual(list.Count, 0);
		}

		[Test]
		public void Get_Current_List()
		{
			var cart = m_CartService.CreateCart("vid");
			m_CartService.AddItem(cart, "p1", 1, 1, 1, new ShoppingCart.Web.Mvc.Model.Price(10.0, 0.196));
			m_CartService.Save(cart);

			var list = m_CartService.GetCurrentList("vid");
			Assert.AreEqual(list.Count, 1);
		}

		[Test]
		public void Remove_Cart()
		{
			var cart = m_CartService.CreateCart("vid");
			m_CartService.AddItem(cart, "p1", 1, 1, 1, new ShoppingCart.Web.Mvc.Model.Price(10.0, 0.196));
			m_CartService.Save(cart);

			var list = m_CartService.GetCurrentList("vid");
			Assert.AreEqual(list.Count, 1);

			m_CartService.RemoveCart(cart);

			list = m_CartService.GetCurrentList("vid");

			Assert.AreEqual(list.Count, 0);
		}

		[Test]
		public void Change_Current_Cart()
		{
			var cart1 = m_CartService.CreateCart("vid");
			m_CartService.AddItem(cart1, "p1", 1, 1, 1, new ShoppingCart.Web.Mvc.Model.Price(10.0, 0.196));
			m_CartService.Save(cart1);

			var cart2 = m_CartService.CreateCart("vid");
			m_CartService.AddItem(cart2, "p2", 1, 1, 1, new ShoppingCart.Web.Mvc.Model.Price(10.0, 0.196));
			m_CartService.Save(cart2);

			m_CartService.ChangeCurrent(cart1.Code);

			var cart = m_CartService.GetCurrent();

			Assert.AreEqual(cart.Code, cart1.Code);

			m_CartService.ChangeCurrent(cart2.Code);

			cart = m_CartService.GetCurrent();

			Assert.AreEqual(cart.Code, cart2.Code);
		}

		[Test]
		public void Add_Cart()
		{
			var cart1 = m_CartService.CreateCart("vid");
			m_CartService.AddItem(cart1, "p1", 1, 1, 1, new ShoppingCart.Web.Mvc.Model.Price(10.0, 0.196));
			m_CartService.Save(cart1);

			var list = m_CartService.GetCurrentList("vid");

			Assert.AreEqual(list.Count, 1);
		}

		#endregion
	}
}
