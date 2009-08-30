<%@ Page %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head>
	<title>default</title>
	<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
	<meta name="CODE_LANGUAGE" content="C#">
	<meta name="vs_defaultClientScript" content="JavaScript">
	<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
</head>
<body>
	<form id="Form1" method="post" runat="server">
		<strong>&nbsp; </strong>
		<asp:ValidationSummary ID="ValidationSummary1" runat="server" EnableViewState="False"
			HeaderText="Erreur" />
		<cart:Information ID="Information1" runat="Server">
			<emptycartitemtemplate><B>Your cart is empty</B></emptycartitemtemplate>
			<oneitemcartitemtemplate><B><A href="/">You have 1 product in your cart</A></B> </oneitemcartitemtemplate>
			<manyitemscartitemtemplate><B><A href="/">You have <%#Container.Count%> products in your cart</A></B> </manyitemscartitemtemplate>
		</cart:Information>
		<br />
		<asp:TextBox ID="TextBox1" runat="server" Width="50"></asp:TextBox>
		<cart:AddToCartHyperLink ID="Addtocartimagebutton1" runat="Server" Availability="100"
			Code="P1" DefaultQuantity="1" Description="Product1" ImageUrl="" ProductId="1"
			ProductLink="" PublicPrice="10.0" Quantity="1" QuantityTextBox="TextBox1" Reduce="0"
			TaxRate="0.196" Text="Add P1" UnitSale="1"></CART:AddToCartHyperLink>
		<br />
		<br />
		<asp:TextBox ID="Textbox2" runat="server" Width="50"></asp:TextBox>
		<cart:AddToCartButton ID="Addtocarthyperlink2" runat="Server" Availability="50" Code="P2"
			DefaultQuantity="5" Description="Product2" ImageUrl="" ProductId="2" ProductLink=""
			PublicPrice="15.0" Quantity="1" QuantityTextBox="TextBox2" Reduce="0" TaxRate="0.196"
			Text="Add P2" UnitSale="1"></CART:AddToCartButton>
		<br />
		<br />
		<cart:AddToCartHyperLink ID="Addtocarthyperlink1" runat="Server" Availability="50"
			Code="P3" DefaultQuantity="5" Description="Product3" ImageUrl="" ProductId="3"
			ProductLink="" PublicPrice="15.0" Quantity="1" Reduce="0" TaxRate="0.196" Text="Add P3"
			UnitSale="1"></cart:AddToCartHyperLink>
		<br />
		<cart:AddToCartImageButton src="~/images/btn-atc.gif" runat="Server" Availability="500"
			Code="P4" DefaultQuantity="5" Description="Product4" ImageUrl="" ProductId="4"
			ProductLink="" PublicPrice="19.9" Quantity="1" Reduce="0" TaxRate="0.196" Text="Add P4"
			UnitSale="1" id="uxAddToCartLinkButton"></cart:AddToCartImageButton>
		<br />
		<cart:cartgrid id="Cart1" runat="server" border="1" cellpadding="0" cellspacing="0"
			width="100%">
			<columns>
				<CART:DeleteCartColumnButton runat="server" Text="Delete" type="Button"></CART:DeleteCartColumnButton>
				<CART:DeleteCartColumnImageButton runat="server" Src="Toto.gif" ></CART:DeleteCartColumnImageButton>
				<CART:QuantityCartColumnTextBox runat="server" HeaderText="Qty." maxlength="7" Size="4">
				</CART:QuantityCartColumnTextBox>
				<CART:TemplateCartColumn runat="server" HeaderText="Code">
					<ITEMTEMPLATE ><A href="<%#Container.Item.ProductLink%>"><%#Container.Item.Code%></A></ITEMTEMPLATE>
				</CART:TemplateCartColumn>
				<CART:TemplateCartColumn runat="server" HeaderText="Description">
					<HEADERSTYLE Font-Bold="True" />
					<ITEMTEMPLATE ><%#Container.Item.Description%></ITEMTEMPLATE>
				</CART:TemplateCartColumn>
				<CART:TemplateCartColumn runat="Server" HeaderText="Dispo.">
					<ITEMTEMPLATE ><%#Container.Item.Availability%></ITEMTEMPLATE>
				</CART:TemplateCartColumn>
				<CART:TemplateCartColumn runat="Server" HeaderText="Amount (HT)">
					<ITEMSTYLE BackColor="#4A3C8C" Font-Bold="True" HorizontalAlign="Right" ForeColor="#F7F7F7" />
						<ITEMTEMPLATE ><%#Container.Item.RealPrice%></ITEMTEMPLATE>
				</CART:TemplateCartColumn><CART:TemplateCartColumn runat="Server" HeaderText="Total (HT)">
					<ITEMTEMPLATE ><%#Container.Item.FreeTaxAmount%></ITEMTEMPLATE>
				</CART:TemplateCartColumn>
			</columns>
			<footertemplate >
				<TABLE cellSpacing=0 cellPadding=0 width="100%" border=1><TBODY><TR><TD width="100%" rowSpan=3><asp:Button ID="Button1" Text="Recalc" Runat="server"></asp:Button> </TD><TD>Total HT :</TD><TD><%#Container.FreeTaxAmount%></TD></TR><TR><TD>TVA :</TD><TD><%#Container.TaxAmount%></TD></TR><TR><TD>Total TTC :</TD><TD><%#Container.Amount%></TD></TR></TBODY></TABLE>
				<CART:EmptyCartButton ID="Emptycartbutton1" runat="Server" name="Emptycartbutton1"
					Text="Empty cart"></CART:EmptyCartButton>
			</footertemplate>
			<EmptyCartTemplate>
				your cart is empty ...
			</EmptyCartTemplate>
		</cart:cartgrid>
	</form>
</body>
</html>
