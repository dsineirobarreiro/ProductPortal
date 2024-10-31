<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="RateProduct.aspx.cs" Inherits="PracticaMad.Web.Pages.RateProduct" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderMain" runat="server">

    <div id="form">

        <span class="label">
            <asp:Localize ID="UserName" runat="server" />
        </span>

        <form id="CommentProduct" method="post" runat="server">

            <div class="field">
                <asp:RadioButtonList ID="RatingList" runat="server" RepeatDirection="Horizontal">
                    <asp:ListItem>1</asp:ListItem>
                    <asp:ListItem>2</asp:ListItem>
                    <asp:ListItem>3</asp:ListItem>
                    <asp:ListItem>4</asp:ListItem>
                    <asp:ListItem>5</asp:ListItem>
                </asp:RadioButtonList>
                <asp:RequiredFieldValidator ID="rfvRadioButton" runat="server" ControlToValidate="RatingList"
                        Display="Dynamic" Text="<%$ Resources:Common, mandatoryField %>"
                        meta:resourcekey="rfvUserNameResource1">
                </asp:RequiredFieldValidator>
            </div>

            <div class="field">
                <span class="label">
                    <asp:Localize ID="lclComment" runat="server" meta:resourcekey="lclComment" />
                </span>
                <span class="entry">
                    <asp:TextBox ID="txtComment" runat="server" Width="100px" Columns="16"></asp:TextBox>
                </span>
            </div>
            <div class="button">
                <asp:Button ID="btnRating" runat="server" OnClick="btnRate_Click" meta:resourcekey="btnRating" />
            </div>
        </form>
    </div>

</asp:Content>
