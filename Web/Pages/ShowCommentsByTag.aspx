<%@ Page Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="ShowCommentsByTag.aspx.cs" Inherits="PracticaMad.Web.Pages.ShowCommentsByTag" meta:resourcekey="Page"%>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderMain" runat="server">

    <form runat="server">
        <asp:GridView ID="gvProducts" runat="server" CssClass="products"
            AutoGenerateColumns="False"
            ShowHeaderWhenEmpty="True">
            <Columns>
                <asp:BoundField DataField="comID" Visible="false"/>
                <asp:BoundField DataField="usuario" HeaderText="<%$ Resources:, comprador %>" />
                <asp:BoundField DataField="date" HeaderText="<%$ Resources:, date %>" />
                <asp:BoundField DataField="text" HeaderText="<%$ Resources:, text %>" />
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
