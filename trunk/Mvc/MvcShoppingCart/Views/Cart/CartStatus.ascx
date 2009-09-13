<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<ShoppingCart.Web.Mvc.Model.Cart>" %>

<%if (Model == null || Model.Items.Count == 0)  { %>
Your cart is empty
<% } else { %>

You have <%=Model.Items.Count%> product(s) in your cart<br />
for <strong><%=Model.GrandTotalWithTax.ToCurrency()%> €</strong><br />
[&nbsp;<%=Html.CartLink("Go to cart")%>&nbsp;]

<% }  %>
