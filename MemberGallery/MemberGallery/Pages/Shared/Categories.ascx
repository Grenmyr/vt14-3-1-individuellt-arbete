﻿<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Categories.ascx.cs" Inherits="MemberGallery.Pages.Shared.Categories" %>

<div class="Categories">
   
    <asp:ListView ID="CategoryListView" runat="server" ItemType="MemberGallery.Model.Category"
        SelectMethod="CategoryListView_GetData" 
        
        DataKeyNames="CategoryID"    >
        <LayoutTemplate>
            <div>
                <h4>Kategorier</h4>
            </div>
            <asp:PlaceHolder ID="itemPlaceholder" runat="server"></asp:PlaceHolder>
        </LayoutTemplate>
        <ItemTemplate>
                <asp:HyperLink  runat="server" Text='<%#Item.CategoryProp %>' ImageUrl='<%#"~/Files/Thumbnails/"+Item %>' NavigateUrl='<%# GetRouteUrl("ImageList", new { CategoryID = Item.CategoryID })  %>'></asp:HyperLink>
        </ItemTemplate>
    </asp:ListView>
</div>

