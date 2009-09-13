<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<IList<ShoppingCart.Web.Mvc.Model.Cart>>" %>

<% if (Model != null && Model.Count > 0) { %>

<% foreach(var cart in Model) { %>

Cart : <%=cart.Code %>

<% } %> 

<% } %> 
