using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ShoppingCart.Web.Mvc.Services
{
	/// <summary>
	/// Service de gestion des paniers de commande
	/// </summary>
	public interface ICartService
	{
		/// <summary>
		/// Retourne le panier en cours
		/// </summary>
		/// <returns></returns>
		Model.Cart GetCurrent();
		/// <summary>
		/// Retourne le panier courant ou creation d'un nouveau
		/// si celui-ci n'existe pas
		/// </summary>
		/// <param name="visitorId">The visitor id.</param>
		/// <param name="user">The user.</param>
		/// <returns></returns>
		Model.Cart GetOrCreateCart(string visitorId);
		/// <summary>
		/// Creation d'un panier de commande
		/// </summary>
		/// <param name="visitorId">The visitor id.</param>
		/// <param name="user">The user.</param>
		/// <returns></returns>
		Model.Cart CreateCart(string visitorId);
		/// <summary>
		/// Creation et sauvegarde d'un panier de commande
		/// </summary>
		/// <param name="visitorId">The visitor id.</param>
		/// <param name="user">The user.</param>
		/// <returns></returns>
		Model.Cart CreateAndSaveCart(string visitorId);
		/// <summary>
		/// Sauvegarde du panier
		/// </summary>
		/// <param name="cart">The cart.</param>
		void Save(Model.Cart cart);
		/// <summary>
		/// Gets the by id.
		/// </summary>
		/// <param name="cartId">The cart id.</param>
		/// <returns></returns>
		Model.Cart GetCartById(string cartId);
		/// <summary>
		/// Retourne la liste de tous les paniers non completés pour un 
		/// utilisateur donné
		/// </summary>
		/// <param name="visitorId">The visitor id.</param>
		/// <param name="user">The user.</param>
		/// <returns></returns>
		IList<Model.Cart> GetCurrentList(string visitorId);
		/// <summary>
		/// Permet d'ajouter un produit dans le panier
		/// </summary>
		/// <param name="cart">The cart.</param>
		/// <param name="productCode">The product code.</param>
		/// <param name="saleUnitValue">The sale unit value.</param>
		/// <param name="packagingValue">The packaging value.</param>
		/// <param name="quantity">The quantity.</param>
		/// <param name="salePrice">The sale price.</param>
		void AddItem(Model.Cart cart, string productCode, int saleUnitValue, int packagingValue, int quantity, Model.Price salePrice);
		/// <summary>
		/// Permet d'ajouter un produit dans le panier
		/// </summary>
		/// <param name="cart">The cart.</param>
		/// <param name="productCode">The product code.</param>
		/// <param name="saleUnitValue">The sale unit value.</param>
		/// <param name="packagingValue">The packaging value.</param>
		/// <param name="quantity">The quantity.</param>
		/// <param name="salePrice">The sale price.</param>
		/// <param name="recyclePrice">The recycle price.</param>
		void AddItem(Model.Cart cart, string productCode, int saleUnitValue, int packagingValue, int quantity, Model.Price salePrice, Model.Price recyclePrice);
		/// <summary>
		/// Retire un item du panier en fonction de sa position dans la liste
		/// </summary>
		/// <param name="cart">The cart.</param>
		/// <param name="index">The index.</param>
		void RemoveItem(Model.Cart cart, int index);
		/// <summary>
		/// Vide le panier
		/// </summary>
		/// <param name="cart">The cart.</param>
		void Clear(Model.Cart cart);
		
		/// <summary>
		/// Retire un panier de la liste des paniers en cours (tout utilisateurs confondus)
		/// </summary>
		/// <param name="cart">The cart.</param>
		void RemoveCart(Model.Cart cart);

		/// <summary>
		/// Supprime un panier de la liste d'un utilisateur
		/// </summary>
		/// <param name="cartId">The cart id.</param>
		void DeleteCart(string cartId);

		/// <summary>
		/// Change le panier courant par celui indiqué
		/// </summary>
		/// <param name="cartId">The cart id.</param>
		void ChangeCurrent(string cartId);

		/// <summary>
		/// Adds the cart.
		/// </summary>
		/// <param name="cart">The cart.</param>
		void AddCart(Model.Cart cart);

		/// <summary>
		/// Registers the routes.
		/// </summary>
		/// <param name="routes">The routes.</param>
		void RegisterRoutes(System.Web.Routing.RouteCollection routes);
	}
}
