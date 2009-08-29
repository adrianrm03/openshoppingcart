<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<MvcShoppingCart.Models.Product>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Catalog
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Catalog</h2>
    
    <% foreach (var product in Model) { %>
    
    <% =product.Title %>
    <% = Html.AddToCart("Add to cart", product.Code) %>
		   
	<% } %>

</asp:Content>
