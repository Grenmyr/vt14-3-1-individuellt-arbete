<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/Shared/Site.Master" AutoEventWireup="true" CodeBehind="ImageList.aspx.cs" Inherits="MemberGallery.Pages.MemberGalleryPages.ImageList" %>

<%@ Register Src="~/Pages/Shared/Categories.ascx" TagPrefix="uc1" TagName="Categories" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
     <asp:Panel ID="Panel1" runat="server">
    <h2>ImageList -- Här börjar min ContentPlaceholder</h2>
    <div class="ContentList">
        <div id="image">
            <asp:Image ID="CurrentImage" runat="server" Width="800"  />
        </div>
       
        <%-- Listview generating images --%>
        <asp:ListView ID="ImageListView" runat="server" ItemType="MemberGallery.Model.Image"
            SelectMethod="ImageListView_GetData"  DeleteMethod="ImageListView_DeleteItem"
           
            DataKeyNames="ImageID" InsertItemPosition="LastItem">
            <LayoutTemplate>

                    <h4>Kategorier-- Detta är min databas ImageList.aspx</h4>

                <asp:PlaceHolder ID="itemPlaceholder" runat="server"></asp:PlaceHolder>
            </LayoutTemplate>
            <ItemTemplate>
               <%--    THUMBNAIL with hyperlink to present picture  />--%>
                <div >
                    
                    <asp:HyperLink ID="HyperLink1" runat="server" Text='<%# string.Format("{0}{1}", Item.ImageID, Item.Extension ) %>'  ImageUrl='<%# string.Format("~/Content/Thumbnails/{0}{1}", Item.ImageID, Item.Extension ) %>' NavigateUrl='<%# string.Format("{0}&name={1}", GetRouteUrl("ImageList", Page.RouteData.Values["CategoryID"]), Item.ImgName)  %>' ></asp:HyperLink>

                     <asp:LinkButton ID="LinkButton3" runat="server" CausesValidation="false" CommandName="Delete" Text="Radera"
                            OnClientClick='<%# String.Format("return confirm(\"Ta Bilden {0}?\")", Item.ImgName) %>'></asp:LinkButton>
                      </div>
            </ItemTemplate>
            <InsertItemTemplate>
            </InsertItemTemplate>
         
        </asp:ListView>
        
        <div id="upload">
            <asp:ValidationSummary ID="ValidationSummary" runat="server" />
            <asp:PlaceHolder ID="ButtonPlaceHolder" runat="server" Visible="true">
                <asp:FileUpload ID="Select" runat="server" />
                <asp:TextBox ID="PictureName" runat="server"  MaxLength="20"></asp:TextBox>
                <asp:Button ID="UploadButton" runat="server" Text="Ladda upp bild" OnClick="UploadButton_Click" />
                <asp:Button ID="DeleteButton" runat="server" Text="Radera bild" OnClick="DeleteButton_Click" />
            </asp:PlaceHolder>
            <asp:CheckBoxList ID="CheckBoxList" runat="server" ItemType="MemberGallery.Model.Category" DataTextField="CategoryProp" DataValueField="CategoryID" SelectMethod="CategoryListView_GetData">
            </asp:CheckBoxList>
         
        </div>  
           <asp:HyperLink ID="HyperLink" runat="server" Text="Till ända/Lägg till kategorier"  
               NavigateUrl='<%$ RouteUrl:routename=Default %>'></asp:HyperLink>
    </div>
            </asp:Panel>

        <%-- Här laddar jag categories.ascx --%>
        <uc1:Categories runat="server" ID="Categories" Visible="true" />
            
      
        <%-- Here starts part contecrning upload new images. --%>
  
</asp:Content>
