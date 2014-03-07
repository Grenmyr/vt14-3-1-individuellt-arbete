﻿<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Categories.ascx.cs" Inherits="MemberGallery.Pages.Shared.Categories" %>

<div class="ContentList">
    <asp:ListView ID="CategoryListView" runat="server" ItemType="MemberGallery.Model.Category"
        SelectMethod="CategoryListView_GetData"
        InsertMethod="CategoryListView_InsertItem"
        UpdateMethod="CategoryListView_UpdateItem"
        DataKeyNames="CategoryID">
        <LayoutTemplate>
            <div>
                <h4>Kategorier-- Detta är min Categories.ascx</h4>
            </div>
            <asp:PlaceHolder ID="itemPlaceholder" runat="server"></asp:PlaceHolder>
        </LayoutTemplate>
        <ItemTemplate>
            <div>

                <asp:HyperLink ID="HyperLink" runat="server" Text='<%#Item.CategoryProp %>' ImageUrl='<%#"~/Files/Thumbnails/"+Item %>' NavigateUrl='<%# GetRouteUrl("ImageList", new { CategoryID = Item.CategoryID })  %>'></asp:HyperLink>
            </div>
        </ItemTemplate>

    </asp:ListView>


</div>
