<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/Shared/Site.Master" AutoEventWireup="true" CodeBehind="ImageList.aspx.cs" Inherits="MemberGallery.Pages.MemberGalleryPages.ImageList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder" runat="server">

        <h2>ImageList -- Här börjar min ContentPlaceholder</h2>
        <div id="image">
            <asp:Image ID="CurrentImage" runat="server" Width="800" />
        </div>

        <%-- Repeater generating thumbnails with properties. --%>

            <asp:ListView ID="ImageListView" runat="server" ItemType="MemberGallery.Model.Image"
                SelectMethod="ImageListView_GetData" DataKeyNames="ImageID">
                <LayoutTemplate>
                    <div>
                        <h4>Kategorier-- Detta är min databas Images</h4>
                    </div>
                    <asp:PlaceHolder ID="itemPlaceholder" runat="server"></asp:PlaceHolder>
                </LayoutTemplate>
                <ItemTemplate>
                    <div>
                        <%-- Hyperlink which present images throug imageurl, the "Item" is the picture. And Navigateurl "# "?" + Item" expression set filename in browser field. --%>
                        <asp:HyperLink ID="HyperLink1" runat="server" Text='<%#: Item%>' ImageUrl='<%#"~/Files/Thumbnails/"+Item %>' NavigateUrl='<%# "?name=" + Item%>'></asp:HyperLink>
                    </div>
                </ItemTemplate>
            </asp:ListView>
</asp:Content>
