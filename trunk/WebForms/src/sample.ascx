<%@ Control %>
<%@ Register TagPrefix="CART" Namespace="Serialcoder.ShoppingCart.UI.WebControls" Assembly="Serialcoder.ShoppingCart"%>

<CART:Information runat="Server" ID="Information1" >
	<EmptyCartItemTemplate>
		<b>Votre panier est vide</b>
	</EmptyCartItemTemplate>
	<OneItemCartItemTemplate>
		<b><A href="#" >Vous avez un produit dans votre panier</a></b>
	</OneItemCartItemTemplate>
	<ManyItemsCartItemTemplate>
		<b><A href="#">Vous avez <%#Container.Count%> produits dans votre panier</a></b>
	</ManyItemsCartItemTemplate>
</CART:Information>
<br>
<CART:AddToCartHyperLink
	runat="Server"
	Text="Ajouter P1"
	productId="1"
	code="P1"
	Description="Produit1"
	Availability="100"
	DefaultQuantity="1"
	Quantity="1"
	ProductLink=''
	ImageUrl=''
	PublicPrice='10.0'
	UnitSale="1"
	Reduce="0"
	TaxRate="0.196" 
	ID="Addtocartimagebutton1" 	>
</CART:AddToCartHyperLink>
<br>

<CART:AddToCartHyperLink
	runat="Server"
	Text="Ajouter P2"
	productId="2"
	code="P2"
	Description="Produit2"
	Availability="50"
	DefaultQuantity="5"
	Quantity="1"
	ProductLink=''
	ImageUrl=''
	PublicPrice='15.0'
	UnitSale="1"
	Reduce="0"
	TaxRate="0.196" 
	ID="Addtocarthyperlink1" 	>
</CART:AddToCartHyperLink>
<br>
									
<CART:CartGrid runat="server" id="Cart1" width="100%" border="1" cellpadding="0" cellspacing="0">
	<Columns>
		<CART:DeleteCartColumnButton runat="server" Text="Supprimer" type="Button" />
		<CART:QuantityCartColumnTextBox runat="server" HeaderText="Qté" Size="4" maxlength="7" />
		<CART:TemplateCartColumn runat="server" HeaderText="Code">
			<ItemTemplate>
				<a href="<%#Container.Item.ProductLink%>"><%#Container.Item.Code%></a>
			</ItemTemplate>			
		</CART:TemplateCartColumn>
		<CART:TemplateCartColumn runat="server" HeaderText="Description">
			<HeaderStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#4A3C8C"></HeaderStyle>
			<ItemTemplate>
				<%#Container.Item.Description%>
			</ItemTemplate>
		</CART:TemplateCartColumn>
		<CART:TemplateCartColumn runat="Server" HeaderText="Dispo.">
			<ItemTemplate>
				<%#Container.Item.Availability%>
			</ItemTemplate>
		</CART:TemplateCartColumn>
		<CART:TemplateCartColumn runat="Server" HeaderText="Tarif (HT)">
			<ItemStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#4A3C8C" HorizontalAlign="Right"></ItemStyle>
			<ItemTemplate>
				<%#Container.Item.RealPrice%>
			</ItemTemplate>
		</CART:TemplateCartColumn>
		<CART:TemplateCartColumn runat="Server" HeaderText="Total (HT)">
			<ItemTemplate>
				<%#Container.Item.FreeTaxAmount%>
			</ItemTemplate>
		</CART:TemplateCartColumn>
	</Columns>
	<FooterTemplate>
		<table border="1" cellpadding="0" cellspacing="0" width="100%">
			<tr>
				<td rowspan="3" width="100%"> <asp:Button Runat="server" Text="Recalc"/> </td>
				<td>Total HT :</td>
				<td><%#Container.FreeTaxAmount%></td>
			</tr>
			<tr>
				<td>TVA :</td>
				<td><%#Container.TaxAmount%></td>
			</tr>
			<tr>
				<td>Total TTC :</td>
				<td><%#Container.Amount%></td>
			</tr>
		</table>
	</FooterTemplate>
</CART:CartGrid>
<CART:EmptyCartButton Text="Vider le panier" runat="Server" ID="Emptycartbutton1" NAME="Emptycartbutton1"/>
