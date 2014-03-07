<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/Shared/Site.Master" AutoEventWireup="true" CodeBehind="ImageList.aspx.cs" Inherits="MemberGallery.Pages.MemberGalleryPages.ImageList" %>

<%@ Register Src="~/Pages/Shared/Categories.ascx" TagPrefix="uc1" TagName="Categories" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder" runat="server">

    <h2>ImageList -- Här börjar min ContentPlaceholder</h2>
    <div class="ContentList">
        <div id="image">
            <asp:Image ID="CurrentImage" runat="server" Width="800" />
        </div>


        <%-- Listview generating images --%>
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
                    <%-- Hyperlink which present images throug imageurl, the "Item" is the picture. And Navigateurl "# "?"  Item" expression set filename in browser field. --%>
                    <asp:HyperLink runat="server" Text='<%# Item.ImgName%>' ImageUrl='<%#"~/Content/Pictures/"+Item.ImgName %>' NavigateUrl='<%# "?name=" + Item%>'></asp:HyperLink>
                </div>
            </ItemTemplate>
        </asp:ListView>
        
        <uc1:Categories runat="server" id="Categories" />
      
        <%-- Here starts part contecrning upload new images. --%>
        <div id="upload">
            <asp:ValidationSummary ID="ValidationSummary" runat="server" />
            <asp:PlaceHolder ID="ButtonPlaceHolder" runat="server" Visible="true">
                <asp:FileUpload ID="Select" runat="server" />
                <asp:Button ID="UploadButton" runat="server" Text="Ladda upp" OnClick="UploadButton_Click" />
                <asp:Button ID="DeleteButton" runat="server" Text="Radera" OnClick="DeleteButton_Click" />
            </asp:PlaceHolder>
        </div>
    </div>
</asp:Content>
