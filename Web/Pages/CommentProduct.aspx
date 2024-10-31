<%@ Page Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="CommentProduct.aspx.cs" Inherits="PracticaMad.Web.Pages.CommentProduct" 
        meta:resourcekey="Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderMain" runat="server">
    <div id="form">

        <span class="label">
            <asp:Localize ID="ProdName" runat="server" />
        </span>

        <span class="label">
            <asp:Localize ID="UserName" runat="server" />
        </span>

        <form id="CommentProduct" method="post" runat="server">

            <div class="field">
                <span class="label">
                    <asp:Localize ID="lclTag" runat="server" meta:resourcekey="lclTag" />
                </span>
                <span class="entry">
                    <asp:TextBox ID="TagText" runat="server" Width="100px" Columns="16"></asp:TextBox>
                </span>
            </div>

            <div class="field">
                <span class="label">
                    <asp:Localize ID="lclComment" runat="server" meta:resourcekey="lclComment" />
                </span>
                <span class="entry">
                    <asp:TextBox ID="txtComment" runat="server" Width="100px" Columns="16"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvComment" runat="server" ControlToValidate="txtComment"
                        Display="Dynamic" Text="<%$ Resources:Common, mandatoryField %>"
                        meta:resourcekey="rfvUserNameResource1"></asp:RequiredFieldValidator>
                    <asp:Label ID="lblNoComment" runat="server" ForeColor="Red" Style="position: relative"
                        Visible="False" meta:resourcekey="lblNoComment"></asp:Label>
                </span>
            </div>
            <div class="button">
                <asp:Button ID="btnComment" runat="server" OnClick="BtnCommentClick" meta:resourcekey="btnComment" />
            </div>
        </form>
    </div>
</asp:Content>
