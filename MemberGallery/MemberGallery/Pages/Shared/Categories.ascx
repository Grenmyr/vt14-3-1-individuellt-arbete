<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Categories.ascx.cs" Inherits="MemberGallery.Pages.Shared.Categories" %>

<div class="Categories">
   <%-- Listview used as navigation to present hyperlink to all categories. Probably stud, perhaps repeater woulda been better. --%>
    <asp:ListView ID="CategoryListView" runat="server" ItemType="MemberGallery.Model.Category"
        SelectMethod="CategoryListView_GetData" 
        DataKeyNames="CategoryID"    >
        <LayoutTemplate>
            <div>
                <h5>Visa kategori</h5>
            </div>
            <asp:PlaceHolder ID="itemPlaceholder" runat="server"></asp:PlaceHolder>
        </LayoutTemplate>
        <ItemTemplate>
                <asp:HyperLink  runat="server" Text='<%#Item.CategoryProp %>'  NavigateUrl='<%# GetRouteUrl("ImageList", new { CategoryID = Item.CategoryID })  %>'></asp:HyperLink>
        </ItemTemplate>
    </asp:ListView>
</div>

