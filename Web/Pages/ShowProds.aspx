<%@ Page Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="ShowProds.aspx.cs" Inherits="PracticaMad.Web.Pages.ShowProds" meta:resourcekey="Page"%>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderMain" runat="server">

    <p>
        <asp:Label ID="lblInvalidProdName" meta:resourcekey="lblInvalidProdName" runat="server" Visible="false"></asp:Label>
    </p>

    <form runat="server">
        <asp:GridView ID="gvProducts" runat="server" CssClass="products"
            AutoGenerateColumns="False"
            ShowHeaderWhenEmpty="True" OnRowDataBound="gvProducts_RowDataBound">
            <Columns>
                <asp:BoundField DataField="prodId" HeaderText="Id"/>
                <asp:BoundField DataField="nombre" HeaderText="<%$ Resources:, name %>" />
                <asp:BoundField DataField="categoria" HeaderText="<%$ Resources:, category %>" />
                <asp:BoundField DataField="date" HeaderText="<%$ Resources:, date %>" />
                <asp:HyperLinkField DataTextField="<%$ Resources:, vendedor %>" DataNavigateUrlFields="vendedor" DataNavigateUrlFormatString="~/Pages/ShowRatings.aspx?username={0}"/>
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:HyperLink runat="server" Text="<%$ Resources:, comment %>" NavigateUrl='<%# string.Format("~/Pages/CommentProduct.aspx?id={0}&prodName={1}&username={2}",
                            HttpUtility.UrlEncode(Eval("prodId").ToString()), HttpUtility.UrlEncode(Eval("nombre").ToString()), HttpUtility.UrlEncode(Eval("vendedor").ToString())) %>'/>
                        </ItemTemplate>
                    </asp:TemplateField>
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:HyperLink runat="server" Text="<%$ Resources:, rate %>" NavigateUrl='<%# string.Format("~/Pages/RateProduct.aspx?id={0}",
                            HttpUtility.UrlEncode(Eval("prodId").ToString())) %>'/>
                        </ItemTemplate>
                    </asp:TemplateField>
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:HyperLink runat="server" Text="<%$ Resources:, show %>" NavigateUrl='<%# string.Format("~/Pages/ShowComments.aspx?id={0}&prodName={1}",
                            HttpUtility.UrlEncode(Eval("prodId").ToString()), HttpUtility.UrlEncode(Eval("nombre").ToString())) %>'/>
                        </ItemTemplate>
                    </asp:TemplateField>
            </Columns>
        </asp:GridView>
        <br />
    </form>

    <!-- "Previous" and "Next" links. -->
    <div class="previousNextLinks">
        <span class="previousLink">
            <asp:HyperLink ID="lnkPrevious" Text="<%$ Resources:Common, Previous %>" runat="server"
                Visible="False"></asp:HyperLink>
        </span><span class="nextLink">
            <asp:HyperLink ID="lnkNext" Text="<%$ Resources:Common, Next %>" runat="server" Visible="False"></asp:HyperLink>
        </span>
    </div>

    <br />
    <br />

</asp:Content>
