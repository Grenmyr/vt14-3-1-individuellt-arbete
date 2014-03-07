<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Categories.ascx.cs" Inherits="MemberGallery.Pages.Shared.Categories" %>




<h2>Bildkategorier -- Här börjar min CategoriesCA</h2>
<div class="ContentList">
    <asp:ListView ID="CategoryListView" runat="server" ItemType="MemberGallery.Model.Category"
        SelectMethod="CategoryListView_GetData" DataKeyNames="CategoryID">
        <LayoutTemplate>
            <div>
                <h4>Kategorier-- Detta är min databas kategorier</h4>
            </div>
            <asp:PlaceHolder ID="itemPlaceholder" runat="server"></asp:PlaceHolder>
        </LayoutTemplate>
        <ItemTemplate>
            <div>        <asp:CheckBox ID='<%#Item.CategoryProp %>' runat="server" />
                        <asp:HyperLink ID="HyperLink" runat="server" Text='<%#Item.CategoryProp %>' ImageUrl='<%#"~/Files/Thumbnails/"+Item %>' NavigateUrl='<%# GetRouteUrl("ImageList", new { CategoryID = Item.CategoryID })  %>'></asp:HyperLink> 
            </div>
        </ItemTemplate>
        <InsertItemTemplate>
         

        </InsertItemTemplate>
    </asp:ListView>
                        
         
</div>

