<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/Shared/Site.Master" AutoEventWireup="true" CodeBehind="ImageList.aspx.cs" Inherits="MemberGallery.Pages.MemberGalleryPages.ImageList" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder" runat="server">

        <div class="ImageList">
         <h2>ImageDesc -- Här börjar min ContentPlaceholder</h2>
           <div id="image">
                <asp:Image ID="CurrentImage" runat="server" Width="800" />
            </div>

            <%-- Repeater generating thumbnails with properties. --%>
            <div class="thumbnails">
                <asp:Repeater ID="Repeater" runat="server" ItemType="System.String" SelectMethod="Repeater_GetData" >
                    <ItemTemplate>
                        <%-- Hyperlink which present images throug imageurl, the "Item" is the picture. And Navigateurl "# "?" + Item" expression set filename in browser field. --%>
                        <asp:HyperLink ID="HyperLink1" runat="server" Text='<%#: Item%>' ImageUrl='<%#"~/Files/Thumbnails/"+Item %>' NavigateUrl='<%# "?name=" + Item%>'></asp:HyperLink>
                    </ItemTemplate>
                </asp:Repeater>
            </div>
    </div>



</asp:Content>
