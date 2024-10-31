<%@ Page Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="SearchPage.aspx.cs" Inherits="PracticaMad.Web.Pages.SearchPage" meta:resourcekey="Page"%>
<asp:Content ID="lclContent" ContentPlaceHolderID="ContentPlaceHolderMain" runat="server">
    <form id="form1" runat="server">
    <div class="searchContainer">

        <asp:TextBox ID="searchText" runat="server" CssClass="searchBox"></asp:TextBox>
        <asp:Button ID="searchButton" runat="server" CssClass="searchBtn" meta:resourcekey="searchButton" OnClick="searchButton_Click"/>

    </div>
    </form>
</asp:Content>
