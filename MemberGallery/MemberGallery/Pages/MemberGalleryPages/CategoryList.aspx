<%@ Page Title="Galleri översikt" Language="C#" MasterPageFile="~/Pages/Shared/Site.Master" AutoEventWireup="true" CodeBehind="CategoryList.aspx.cs" Inherits="MemberGallery.Pages.MemberGalleryPages.CategoryList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%-- Kod här --%>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <%-- Kod här --%>
    <h2>Bildkategorier -- Här börjar min ContentPlaceholder</h2>
    <div class="GalleryList">
        <asp:ListView ID="CategoryListView" runat="server" ItemType="MemberGallery.Model.Category"
            SelectMethod="CategoryListView_GetData" DataKeyNames="CategoryID" >
            <LayoutTemplate>
                <div>
                    <h4>Kategorier-- Detta är min databas kategorier</h4>
                </div>
                 <asp:PlaceHolder ID="itemPlaceholder" runat="server"></asp:PlaceHolder>
            </LayoutTemplate>
            <ItemTemplate>
                <div>
                <%--    <%#: Item.CategoryProp %>--%>
                      <asp:HyperLink ID="HyperLink" runat="server" Text='<%#Item.CategoryProp%>' ImageUrl='<%#"~/Files/Thumbnails/"+Item %>'  NavigateUrl='<%# GetRouteUrl("ImageList", new { CategoryID = Item.CategoryID })  %>'></asp:HyperLink>
                </div>
            </ItemTemplate>
          <%--  <InsertItemTemplate>
                <div>
                   <asp:HyperLink ID="CategoryHyperlink" runat="server" Text='<%# BindItem.CategoryProp %>'></asp:HyperLink>
                </div>
            </InsertItemTemplate>--%>
        </asp:ListView>
    </div>
</asp:Content>
