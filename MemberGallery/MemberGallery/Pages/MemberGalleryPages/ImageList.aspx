<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/Shared/Site.Master" AutoEventWireup="true" CodeBehind="ImageList.aspx.cs" Inherits="MemberGallery.Pages.MemberGalleryPages.ImageList" %>

<%@ Register Src="~/Pages/Shared/Categories.ascx" TagPrefix="uc1" TagName="Categories" %>
<%@ Register Src="~/Pages/Shared/ViewImage.ascx" TagPrefix="uc1" TagName="ViewImage" %>



<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder" runat="server">

    <h2>ImageList -- Här börjar min ContentPlaceholder</h2>
    <div class="ContentList">
        <div id="image">
            <asp:Image ID="CurrentImage" runat="server" Width="800" />
        </div>
       <%-- <uc1:ViewImage runat="server" ID="ViewImage" />--%>

        <%-- Listview generating images --%>
        <asp:ListView ID="ImageListView" runat="server" ItemType="MemberGallery.Model.Image"
            SelectMethod="ImageListView_GetData" DataKeyNames="ImageID" InsertItemPosition="LastItem">

            <LayoutTemplate>

                <div>
                    <h4>Kategorier-- Detta är min databas ImageList.aspx</h4>
                </div>
                <asp:PlaceHolder ID="itemPlaceholder" runat="server"></asp:PlaceHolder>
            </LayoutTemplate>
            <ItemTemplate>
                <div>

                    <%-- Hyperlink which present images throug imageurl, the "Item" is the picture. And Navigateurl "# "?"  Item" expression set filename in browser field. --%>
                    <asp:HyperLink runat="server" Text='<%# Item.ImgName%>'
                        ImageUrl='<%#"~/Content/Thumbnails/"+Item.ImgName %>' NavigateUrl='<%# GetRouteUrl("ViewImage", new { ImageID = Item.ImageID })  %>' ></asp:HyperLink>
                </div>
            </ItemTemplate>
            <InsertItemTemplate>
            </InsertItemTemplate>


        </asp:ListView>
        <div id="upload">
            <asp:ValidationSummary ID="ValidationSummary" runat="server" />
            <asp:PlaceHolder ID="ButtonPlaceHolder" runat="server" Visible="true">
                <asp:FileUpload ID="Select" runat="server" />
                <asp:Button ID="UploadButton" runat="server" Text="Ladda upp" OnClick="UploadButton_Click" />
                <asp:Button ID="DeleteButton" runat="server" Text="Radera" OnClick="DeleteButton_Click" />
            </asp:PlaceHolder>
            <asp:CheckBoxList ID="CheckBoxList" runat="server" ItemType="MemberGallery.Model.Category" DataTextField="CategoryProp" DataValueField="CategoryID" SelectMethod="CategoryListView_GetData">
            </asp:CheckBoxList>
            <%-- <asp:DropDownList ID="ContactTypeDropDownList" runat="server"
                    ItemType="MemberGallery.Model.Category"
                    SelectMethod="CategoryListView_GetData"
                    DataTextField="CategoryProp"
                    DataValueField="CategoryID"
                  />--%>
        </div>

        <%-- Här laddar jag categories.ascx --%>
        <uc1:Categories runat="server" ID="Categories" />

        <%-- Here starts part contecrning upload new images. --%>
    </div>
</asp:Content>
