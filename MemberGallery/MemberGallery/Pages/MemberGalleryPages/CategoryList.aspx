<%@ Page Title="Galleri översikt" Language="C#" MasterPageFile="~/Pages/Shared/Site.Master" AutoEventWireup="true" CodeBehind="CategoryList.aspx.cs" Inherits="MemberGallery.Pages.MemberGalleryPages.CategoryList" %>

<%@ Register Src="~/Pages/Shared/Categories.ascx" TagPrefix="uc1" TagName="Categories" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%-- Kod här --%>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <%-- Kod här --%>
    <h2>Bildkategorier -- Här börjar min ContentPlaceholder</h2>

    <%-- Gamla över här --%>
    <div class="ContentList">
        <%-- ValidationSummary för de 2 olika validationgrupperna. Samt Rättmeddelande under. --%>
        <asp:ValidationSummary ID="ValidationSummary" ValidationGroup="Insert" runat="server" />
        <asp:ValidationSummary ID="ValidationSummary1" ValidationGroup="Update" runat="server" />

        <%-- Placeholder to handle confirmationmessages. --%>
        <asp:PlaceHolder ID="PlaceHolder" runat="server" Visible="false">
            <asp:Literal ID="ConfirmationMessage" runat="server"></asp:Literal></asp:PlaceHolder>

        <%-- Listview With Methods to initialize Code --%>
        <asp:ListView ItemType="MemberGallery.Model.Category" runat="server" ID="CategoryListView"
            SelectMethod="CategoryListView_GetData"
            InsertMethod="CategoryListView_InsertItem"
            UpdateMethod="CategoryListView_UpdateItem"
            DeleteMethod="CategoryListView_DeleteItem"
            DataKeyNames="CategoryID"
            InsertItemPosition="FirstItem" ViewStateMode="Enabled">

            <%-- Emty table which is filled. --%>
            <LayoutTemplate>
                <table>
                    <tr>
                        <th>Kategori
                        </th>
                    </tr>
                    <asp:PlaceHolder ID="itemPlaceholder" runat="server"></asp:PlaceHolder>
                </table>

            </LayoutTemplate>
            <ItemTemplate>
                <tr>
                    <td>
                        <%# Item.CategoryProp %>
                    </td>
                    <td class="SidoCommand">
                        <%-- Buttons to remove and edit contacts in the list, they are rendered to the right in table. --%>
                            <asp:LinkButton  runat="server" CausesValidation="false" CommandName="Delete" Text="Radera"
                                OnClientClick='<%# String.Format("return confirm(\"Ta Kontakten {0}?\")", Item.CategoryProp) %>'></asp:LinkButton>
                            <asp:LinkButton  runat="server" CausesValidation="false" CommandName="Edit" Text="Redigera"></asp:LinkButton>
                    </td>
                </tr>
            </ItemTemplate>
            <EmptyDataTemplate>
                <%-- If null replace with this (Not implemented)--%>
                <table>
                    <tr>
                        <td>Kontaktuppgifter saknas.
                        </td>
                    </tr>
                </table>
            </EmptyDataTemplate>
            <InsertItemTemplate>
                <%-- This is how it is rendered into table, with using BindItem links to the different propertie name from columns. --%>
                 <asp:RequiredFieldValidator runat="server" ErrorMessage="Kategori fältet får ej lämnas tomt." ControlToValidate="CategoryName" ValidationGroup="Insert" Display="None"></asp:RequiredFieldValidator>
                <tr>
                    <td>
                        <asp:TextBox ID="CategoryName" runat="server" Text='<%# BindItem.CategoryProp %>' MaxLength="20" ValidationGroup="Insert"></asp:TextBox>
                    </td>
                    <td class="TopCommand">
                        <%-- Buttons to control insertion of rows into table, they are rendered in top of table because of the FirstItem postion --%>
                        <asp:LinkButton  runat="server" CommandName="Insert" Text="Lägg till" ValidationGroup="Insert"></asp:LinkButton>
                        <asp:LinkButton  runat="server" CausesValidation="false" CommandName="Cancel" Text="Rensa"></asp:LinkButton>
                    </td>
                </tr>
            </InsertItemTemplate>
            <EditItemTemplate>
                <%-- Here i edit my columns it uses same ID on controls as insert, but works as they are't used at same time. --%>
                             <asp:RequiredFieldValidator runat="server" ErrorMessage="Kategori fältet får ej lämnas tomt." ControlToValidate="CategoryName" ValidationGroup="Update"  Display="None"></asp:RequiredFieldValidator>
                <tr>
                    <td>
                        <asp:TextBox ID="CategoryName" runat="server" Text='<%# BindItem.CategoryProp %>' ValidationGroup="Update"></asp:TextBox>
                    </td>

                    <td>
                        <%-- BUttons to Edit a contact and abort to cancel the editing. --%>
                        <asp:LinkButton  runat="server" CommandName="Update" Text="Spara" ValidationGroup="Update"></asp:LinkButton>
                        <asp:LinkButton  runat="server" CommandName="Cancel" Text="Ångra" CausesValidation="false"></asp:LinkButton>
                    </td>
                </tr>
            </EditItemTemplate>
        </asp:ListView>
    </div>
    <uc1:Categories runat="server" ID="Categories" />
</asp:Content>
