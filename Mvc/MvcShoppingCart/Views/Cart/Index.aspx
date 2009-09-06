<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<ShoppingCart.Web.Mvc.Model.Cart>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Cart
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Cart</h2>
    
    <% Html.BeginCartForm(); %>
	<table class="cart">
		<tr>
			<th>
				&nbsp;
			</th>
			<th>
				Qty
			</th>
			<th>
				Code / Description
			</th>
			<th width="80" nowrap="nowrap">
				Amount
			</th>
			<th title="Sale Unit">
				S.U.
			</th>
			<th width="80" nowrap="nowrap">
				Total
			</th>
		</tr>
		<% foreach (var item in Model.Items) { %>
		<tr>
			<td>
				<%=Html.DeleteCartItemLink("Del.", Model.Items.IndexOf(item)) %>
			</td>
			<td width="80">
				<% =Html.TextBox("quantity", item.Quantity, new { size = 4 , name="quantity" })%>
			</td>
			<td width="70%" align="left">
				#Ref:<%=item.GetProduct().Title%><br />
				<% =Html.Encode(item.GetProduct().Title) %>
			</td>
			<td align="right">
				<% =item.SalePrice.ToCurrency() %>
			</td>
			<td align="right">
				<%=item.SaleUnitValue%>
			</td>
			<td align="right">
				<% =item.Total.ToCurrency() %>
			</td>
		</tr>
		<% } %>
		<tr>
			<td colspan="3" rowspan="4" align="center" valign="middle">
				<input type="submit" value="Recalc" />
			</td>
			<td colspan="2" align="left">
				Total
			</td>
			<td align="right">
				<%=Model.Total.ToCurrency()%>
			</td>
		</tr>
		<tr>
			<td colspan="2" align="left">
				Tax Total
			</td>
			<td align="right">
				<%=Model.TotalTax.ToCurrency()%>
			</td>
		</tr>
		<tr>
			<td colspan="2" align="left">
				Total with Tax
			</td>
			<td align="right">
				<b>
					<%=Model.TotalWithTax.ToCurrency()%></b>
			</td>
		</tr>
	</table>
	<% Html.EndForm(); %>
	<p>Click <% =Html.ClearCartLink("here")%> for clear cart</p>


</asp:Content>
