using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace ShoppingCart.Web.Mvc.Html
{
	public static class CartExtensions
	{
		public static string GetStatusText(this Model.Cart cart)
		{
			if (cart == null)
			{
				return "Empty";
			}
			if (cart.ItemCount == 0)
			{
				return "Empty";
			}
			if (cart.ItemCount == 1)
			{
				return "1 produt";
			}
			return string.Format("{0} products", cart.ItemCount);
		}

		public static string AddToCart(this HtmlHelper helper, string title, string productCode)
		{
			return helper.RouteLink(title, "CartItemAdd", new { productCode = productCode });
		}

		/// <summary>
		/// Retourne un element html form, permettant d'ajouter un produit dans le panier
		/// </summary>
		/// <param name="helper">The helper.</param>
		/// <returns>un element html form</returns>
		/// <example>
		/// 	<code>
		/// <![CDATA[<%=Html.BeginCartForm()%>]]>
		/// ...
		/// Ici les données du formulaire
		/// ...
		/// <![CDATA[<%Html.EndForm();%>]]>
		/// 	</code>
		/// </example>
		public static MvcForm BeginCartForm(this HtmlHelper helper)
		{
			return helper.BeginRouteForm("CartRecalc", FormMethod.Post);
		}

		/// <summary>
		/// Retourne un element html anchor contenant le lien vers la page du panier
		/// </summary>
		/// <example>
		/// <code>
		/// <![CDATA[<%=Html.CartLink("titre du lien");%>]]>
		/// </code>
		/// </example>
		/// <param name="helper">The helper.</param>
		/// <param name="title">The title.</param>
		/// <returns>un elment anchor</returns>
		public static string CartLink(this HtmlHelper helper, string title)
		{
			return helper.RouteLink(title, "Cart", new { id = string.Empty });
		}

		/// <summary>
		/// Retourne le lien vers la page du panier
		/// </summary>
		/// <example>Exemple d'appel
		/// <code>
		/// <![CDATA[<%=Url.CartHref()%>]]>
		/// </code>
		/// </example>
		/// <param name="helper">The helper.</param>
		/// <returns></returns>
		public static string CartHref(this UrlHelper helper)
		{
			return helper.RouteUrl("Cart", new { id = string.Empty });
		}

		/// <summary>
		/// Retourne un element html form, permettant d'ajouter un produit dans le panier
		/// </summary>
		/// <example>
		/// <code>
		/// <![CDATA[<%Html.BeginAddToCartRouteForm();%>]]>
		/// ...
		/// Ici les données du formulaire
		/// ...
		/// <![CDATA[<%Html.EndForm();%>]]>
		/// </code>
		/// </example>
		/// <param name="helper">The helper.</param>
		public static void BeginAddToCartRouteForm(this HtmlHelper helper)
		{
			helper.BeginRouteForm("CartAdd", FormMethod.Post);
		}

		/// <summary>
		/// Retourne un element html anchor permetant de vider tous les elements du panier
		/// </summary>
		/// <example>Exemple d'appel :
		/// <code>
		/// <![CDATA[<%=Html.ClearCartLink("titre du lien")%>]]>
		/// </code>
		/// </example>
		/// <param name="helper">The helper.</param>
		/// <param name="title">The title.</param>
		/// <returns></returns>
		public static string ClearCartLink(this HtmlHelper helper, string title)
		{
			return helper.RouteLink(title, "CartClear", new { action = "Clear" });
		}

		/// <summary>
		/// Retourne l'url pour vider le panier 
		/// </summary>
		/// <example>Exemple d'appel :
		/// <code>
		/// <![CDATA[<%=Url.ClearCartHref()%>]]>
		/// </code>
		/// </example>
		/// <param name="helper">The helper.</param>
		/// <returns></returns>
		public static string ClearCartHref(this UrlHelper helper)
		{
			return helper.RouteUrl("CartClear", new { action = "Clear" });
		}

		/// <summary>
		/// Retourne un element html anchor permettant la suppression d'un item du panier
		/// </summary>
		/// <example>Exemple d'appel
		/// <code>
		///		<![CDATA[
		///		<%=Html.DeleteCartItemLink("titre du lien", 0)%> (permet la suppression de la premiere ligne)
		///		]]>
		/// </code>
		/// </example>
		/// <param name="helper">The helper.</param>
		/// <param name="title">The title.</param>
		/// <param name="itemIndex">Index of the item.</param>
		/// <returns></returns>
		public static string DeleteCartItemLink(this HtmlHelper helper, string title, int itemIndex)
		{
			return helper.RouteLink(title, "CartItemDelete", new { index = itemIndex });
		}

		/// <summary>
		/// Retourne l'url avec laquelle il est possible de supprimer un item du panier en fonction de sa position dans la list
		/// </summary>
		/// <example>Exemple d'appel
		/// <code>
		/// <![CDATA[
		/// <%=Url.DeleteCartItemHref(0)%> (Supprimer la première ligne du panier)
		/// ]]>
		/// </code>
		/// </example>
		/// <param name="helper">The helper.</param>
		/// <param name="itemIndex">Index of the item.</param>
		/// <returns></returns>
		public static string DeleteCartItemHref(this UrlHelper helper, int itemIndex)
		{
			return helper.RouteUrl("CartItemDelete", new { index = itemIndex });
		}


		/// <summary>
		/// Retourne un element anchor permettant de supprimer un panier
		/// </summary>
		/// <example>
		/// Exemple d'appel :
		/// <code>
		/// <![CDATA[
		/// <%=Html.DeleteCartLink("supprimer", "xxxx")%>
		/// ]]>
		/// </code>
		/// </example>
		/// <param name="helper">The helper.</param>
		/// <param name="title">The title.</param>
		/// <param name="cartId">The cart id.</param>
		/// <returns></returns>
		public static string DeleteCartLink(this HtmlHelper helper, string title, string cartId)
		{
			return helper.RouteLink(title, "CartDelete", new { cartId = cartId });
		}

		/// <summary>
		/// Retourne l'url pour supprimer un panier donné
		/// </summary>
		/// <example>
		/// Exemple d'appel :
		/// <code>
		/// <![CDATA[
		/// <%=Url.DeleteCartHref("cartId")%>
		/// ]]>
		/// </code>
		/// </example>
		/// <param name="helper">The helper.</param>
		/// <param name="cartId">The cart id.</param>
		/// <returns></returns>
		public static string DeleteCartHref(this UrlHelper helper, string cartId)
		{
			return helper.RouteUrl("CartDelete", new { cartId = cartId });
		}

		/// <summary>
		/// Retourne un element anchor permettant de changer de panier
		/// </summary>
		/// <example>
		/// Exemple d'appel :
		/// <code>
		/// <![CDATA[
		/// <%=Html.ChangeCartLink("supprimer", "xxxx")%>
		/// ]]>
		/// </code>
		/// </example>
		/// <param name="helper">The helper.</param>
		/// <param name="title">The title.</param>
		/// <param name="cartId">The cart id.</param>
		/// <returns></returns>
		public static string ChangeCartLink(this HtmlHelper helper, string title, string cartId)
		{
			return helper.RouteLink(title, "CartChange", new { cartId = cartId });
		}

		/// <summary>
		/// Retourne l'url pour changer de panier 
		/// </summary>
		/// <example>
		/// Exemple d'appel :
		/// <code>
		/// <![CDATA[
		/// <%=Url.ChangeCartHref("cartId")%>
		/// ]]>
		/// </code>
		/// </example>
		/// <param name="helper">The helper.</param>
		/// <param name="cartId">The cart id.</param>
		/// <returns></returns>
		public static string ChangeCartHref(this UrlHelper helper, string cartId)
		{
			return helper.RouteUrl("CartChange", new { cartId = cartId });
		}

		/// <summary>
		/// Retourne un element anchor permettant de voir un panier enregistré
		/// </summary>
		/// <example>
		/// Exemple d'appel :
		/// <code>
		/// <![CDATA[
		/// <%=Html.ShowCartLink("supprimer", "xxxx")%>
		/// ]]>
		/// </code>
		/// </example>
		/// <param name="helper">The helper.</param>
		/// <param name="title">The title.</param>
		/// <param name="cartId">The cart id.</param>
		/// <returns></returns>
		public static string ShowCartLink(this HtmlHelper helper, string title, string cartId)
		{
			return helper.RouteLink(title, "CartShow", new { cartId = cartId });
		}

		/// <summary>
		/// Retourne l'url pour changer de panier 
		/// </summary>
		/// <example>
		/// Exemple d'appel :
		/// <code>
		/// <![CDATA[
		/// <%=Url.ShowCartHref("cartId")%>
		/// ]]>
		/// </code>
		/// </example>
		/// <param name="helper">The helper.</param>
		/// <param name="cartId">The cart id.</param>
		/// <returns></returns>
		public static string ShowCartHref(this UrlHelper helper, string cartId)
		{
			return helper.RouteUrl("CartShow", new { cartId = cartId });
		}

		/// <summary>
		/// Retourne un element anchor permettant de commencer un nouveau panier
		/// </summary>
		/// <example>
		/// Exemple d'utilisation
		/// <code>
		/// <![CDATA[
		/// <%=Html.CreateNewCartLink("titre")%>
		/// ]]>
		/// </code>
		/// </example>
		/// <param name="helper">The helper.</param>
		/// <param name="title">The title.</param>
		/// <returns></returns>
		public static string CreateNewCartLink(this HtmlHelper helper, string title)
		{
			return helper.RouteLink(title, "CartCreate", new { id = string.Empty });
		}

		/// <summary>
		/// Retourne l'url pour creer un nouveau panier
		/// </summary>
		/// <example>
		/// Exemple d'utilisation :
		/// <code>
		/// <![CDATA[
		/// <%=Url.CreateNewCartHref()%>
		/// ]]>
		/// </code>
		/// </example>
		/// <param name="helper">The helper.</param>
		/// <returns></returns>
		public static string CreateNewCartHref(this UrlHelper helper)
		{
			return helper.RouteUrl("CartCreate", new { id = string.Empty });
		}

	}
}
