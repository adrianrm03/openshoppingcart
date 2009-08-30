<%@ Page Language="C#" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<script runat="server">

</script>

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    		<asp:ValidationSummary ID="ValidationSummary1" runat="server" EnableViewState="False"
			HeaderText="Erreur" />
		<cart:Information ID="Information1" runat="Server">
			<emptycartitemtemplate><b>Your cart is empty</b></emptycartitemtemplate>
			<oneitemcartitemtemplate><b><a href="/">You have 1 product in your cart</a></b> </oneitemcartitemtemplate>
			<manyitemscartitemtemplate><b><a href="/">You have <%#Container.Count%> products in your cart</a></b> </manyitemscartitemtemplate>
		</cart:Information>
		<br />
		<asp:TextBox ID="TextBox1" runat="server" Width="50"></asp:TextBox>
		<cart:AddToCartHyperLink ID="Addtocartimagebutton1" runat="Server" Availability="100"
			Code="P1" DefaultQuantity="1" Description="Product1" ImageUrl="" ProductId="1"
			ProductLink="" PublicPrice="10.0" Quantity="1" QuantityTextBox="TextBox1" Reduce="0"
			TaxRate="0.196" Text="Add P1" UnitSale="1"></cart:AddToCartHyperLink>
		<br />
		<br />
		<asp:TextBox ID="Textbox2" runat="server" Width="50"></asp:TextBox>
		<cart:AddToCartButton ID="Addtocarthyperlink2" runat="Server" Availability="50" Code="P2"
			DefaultQuantity="5" Description="Product2" ImageUrl="" ProductId="2" ProductLink=""
			PublicPrice="15.0" Quantity="1" QuantityTextBox="TextBox2" Reduce="0" TaxRate="0.196"
			Text="Add P2" UnitSale="1"></cart:AddToCartButton>
		<br />
		<br />
		<cart:AddToCartHyperLink ID="Addtocarthyperlink1" runat="Server" Availability="50"
			Code="P3" DefaultQuantity="5" Description="Product3" ImageUrl="" ProductId="3"
			ProductLink="" PublicPrice="15.0" Quantity="1" Reduce="0" TaxRate="0.196" Text="Add P3"
			UnitSale="1"></cart:AddToCartHyperLink>
		<br />
		<cart:AddToCartImageButton runat="server" id="uxAddToCartImageButton" Src="~/images/btn-atc.gif" Availability="50"
			Code="P4" DefaultQuantity="5" Description="Product4" ImageUrl="" ProductId="4"
			ProductLink="" PublicPrice="15.0" Quantity="1" Reduce="0" TaxRate="0.196" Text="Add P4"
			UnitSale="1" />
		<br />
		<cart:cartgrid id="Cart1" runat="server" border="1" cellpadding="0" cellspacing="0"
			width="100%">
			<columns>
				<cart:DeleteCartColumnButton runat="server" Text="Delete" type="Button"></cart:DeleteCartColumnButton>
				<cart:QuantityCartColumnTextBox runat="server" HeaderText="Qty." maxlength="7" Size="4">
				</cart:QuantityCartColumnTextBox>
				<cart:TemplateCartColumn runat="server" HeaderText="Code">
					<ITEMTEMPLATE ><a href="<%#Container.Item.ProductLink%>"><%#Container.Item.Code%></a></ITEMTEMPLATE>
				</cart:TemplateCartColumn>
				<cart:TemplateCartColumn runat="server" HeaderText="Description">
					<HEADERSTYLE Font-Bold="True" />
					<ITEMTEMPLATE ><%#Container.Item.Description%></ITEMTEMPLATE>
				</cart:TemplateCartColumn>
				<cart:TemplateCartColumn runat="Server" HeaderText="Dispo.">
					<ITEMTEMPLATE ><%#Container.Item.Availability%></ITEMTEMPLATE>
				</cart:TemplateCartColumn>
				<cart:TemplateCartColumn runat="Server" HeaderText="Amount (HT)">
					<ITEMSTYLE BackColor="#4A3C8C" Font-Bold="True" HorizontalAlign="Right" ForeColor="#F7F7F7" />
						<ITEMTEMPLATE ><%#Container.Item.RealPrice%></ITEMTEMPLATE>
				</cart:TemplateCartColumn><cart:TemplateCartColumn runat="Server" HeaderText="Total (HT)">
					<ITEMTEMPLATE ><%#Container.Item.FreeTaxAmount%></ITEMTEMPLATE>
				</cart:TemplateCartColumn>
			</columns>
			<footertemplate>
				<TABLE cellSpacing=0 cellPadding=0 width="100%" border=1><TBODY><TR><TD width="100%" rowSpan=3><asp:Button ID="Button1" Text="Recalc" Runat="server"></asp:Button> </TD><TD>Total HT :</TD><TD><%#Container.FreeTaxAmount%></TD></TR><TR><TD>TVA :</TD><TD><%#Container.TaxAmount%></TD></TR><TR><TD>Total TTC :</TD><TD><%#Container.Amount%></TD></TR></TBODY></TABLE>
				<cart:EmptyCartButton ID="Emptycartbutton1" runat="Server" name="Emptycartbutton1"
					Text="Empty cart"></cart:EmptyCartButton>
			</footertemplate>
			<emptycarttemplate>
				your cart is empty ...
			</emptycarttemplate>
		</cart:cartgrid>
    </div>
    </form>
</body>
</html>
