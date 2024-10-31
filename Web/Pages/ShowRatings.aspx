<%@ Page Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="ShowRatings.aspx.cs" Inherits="PracticaMad.Web.Pages.ShowRatings" meta:resourcekey="Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderMain" runat="server">
    
   <form runat="server">
       <span class="label">
            <asp:Localize ID="UserName" runat="server" />
        </span>
       <br />
       <span class="label">
            <asp:Localize ID="lblTotal" runat="server" meta:resourcekey="lblTotal"/>
        </span>

       <br />

        <span class="label">
            <asp:Localize ID="lblAverage" runat="server" meta:resourcekey="lblAverage"/>
        </span>


        <asp:GridView ID="gvProducts" runat="server" CssClass="products"
            AutoGenerateColumns="False"
            ShowHeaderWhenEmpty="True">
            <Columns>
                <asp:BoundField DataField="valID" Visible="false"/>
                <asp:BoundField DataField="comprador" HeaderText="<%$ Resources:, comprador %>" />
                <asp:BoundField DataField="date" HeaderText="<%$ Resources:, date %>" />
                <asp:BoundField DataField="puntuacion" HeaderText="<%$ Resources:, puntuacion %>" />
                <asp:BoundField DataField="comentario" HeaderText="<%$ Resources:, text %>" />
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
