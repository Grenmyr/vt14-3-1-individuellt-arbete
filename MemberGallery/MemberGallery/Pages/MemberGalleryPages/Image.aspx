<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/Shared/Site.Master" AutoEventWireup="true" CodeBehind="Image.aspx.cs" Inherits="MemberGallery.Pages.MemberGalleryPages.Image" %>

<asp:Content ID="ImageContent" ContentPlaceHolderID="ContentPlaceHolder" runat="server">

    <%-- Formview with edititemtemplate that let  me change ImgName, Automaticly load new date into database on changed. I am using Extensionclass to load SaveName from Image table. --%>
    <asp:FormView CssClass="ImageCat" runat="server" DataKeyNames="ImgDescID" ItemType="MemberGallery.Model.ImageDescExtension" SelectMethod="FormView_GetItem" UpdateMethod="FormView_UpdateItem" DeleteMethod="FormView_DeleteItem">
        <ItemTemplate>
            <asp:Image ID="Image" ImageUrl='<%# String.Format("~/Content/Pictures/{0}", Item.SaveName )%>' runat="server" Width="800" />
            <div>
                <asp:Literal Text='<%#  String.Format("Bildnamn: {0}",Item.ImgName) %>' runat="server"></asp:Literal>
                <asp:LinkButton ID="LinkButton3" runat="server" CausesValidation="false" CommandName="Delete" Text="Radera"
                    OnClientClick='<%# String.Format("return confirm(\"Ta bort bilden {0}?\")", Item.ImgName  ) %>'></asp:LinkButton>
                <asp:LinkButton runat="server" CausesValidation="false" CommandName="Edit" Text="Redigera"></asp:LinkButton>
            </div>
            <div>
                <asp:Literal Text='<%#  String.Format("Senast Ändrad: {0}",Item.Edited)  %>' runat="server"></asp:Literal>
            </div>
        </ItemTemplate>
        <EditItemTemplate>
            <asp:Image ID="Image" ImageUrl='<%# String.Format("~/Content/Pictures/{0}", Item.SaveName )%>' runat="server" Width="800" />
            <div>
                <asp:RequiredFieldValidator runat="server" ErrorMessage="Bildnamn fältet får ej lämnas tomt." ControlToValidate="ImgName" Display="none"></asp:RequiredFieldValidator>
                <asp:TextBox ID="ImgName" runat="server" Text='<%# BindItem.ImgName %>' MaxLength="20"></asp:TextBox>
                <asp:LinkButton runat="server" CommandName="Update" Text="Spara"></asp:LinkButton>
                <asp:LinkButton runat="server" CommandName="Cancel" Text="Ångra" CausesValidation="false"></asp:LinkButton>
            </div>
            <div id="Edited">
                <asp:Literal Text='<%#  String.Format("Senast Ändrad: {0}",Item.Edited)  %>' runat="server"></asp:Literal>
            </div>
        </EditItemTemplate>
    </asp:FormView>

    <%-- Validationsummary --%>
    <asp:ValidationSummary ID="ValidationSummary" runat="server" />

    <%-- Reading what category user are on, and presenting hyperlink back to images in that category. --%>
    <asp:FormView ID="CategoryFormView" runat="server" DataKeyNames="CategoryID" SelectMethod="CategoryFormView_GetCategoryByID" ItemType="MemberGallery.Model.Category">
        <ItemTemplate>
            <asp:HyperLink ID="ReturnThumbnails" runat="server" Text='<%# String.Format("Tillbaka till {0}", Item.CategoryProp) %>' NavigateUrl='<%# GetRouteUrl("ImageList", new { CategoryID = Item.CategoryID })  %>'></asp:HyperLink>
        </ItemTemplate>
    </asp:FormView>
</asp:Content>
