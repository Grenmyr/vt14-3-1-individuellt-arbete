﻿<%@ Page Title="Galleri översikt" Language="C#" MasterPageFile="~/Pages/Shared/Site.Master" AutoEventWireup="true" CodeBehind="CategoryList.aspx.cs" Inherits="MemberGallery.Pages.MemberGalleryPages.CategoryList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%-- Kod här --%>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <%-- Kod här --%>
    <h2>Bildkategorier</h2>
    <div class="GalleryList">
        <asp:Repeater ID="Repeater" runat="server">
           <%-- Kod för generera alla Kategorier. --%>
            <ItemTemplate>
                <div>
                    CategoriNamn
                </div>
            </ItemTemplate>
        </asp:Repeater>
    </div>
</asp:Content>
