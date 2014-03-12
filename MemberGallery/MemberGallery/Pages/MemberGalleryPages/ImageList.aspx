<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/Shared/Site.Master" AutoEventWireup="true" CodeBehind="ImageList.aspx.cs" Inherits="MemberGallery.Pages.MemberGalleryPages.ImageList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
 
        <h2>ImageList -- Här börjar min ContentPlaceholder</h2>
 
          
            <%-- Listview generating images --%>
            <asp:ListView ID="ImageListView" runat="server" ItemType="MemberGallery.Model.Image"
                SelectMethod="ImageListView_GetData" DeleteMethod="ImageListView_DeleteItem" UpdateMethod="ImageListView_UpdateItem"
                DataKeyNames="ImageID">
                <LayoutTemplate>

                    <h4>Kategorier-- Detta är min databas ImageList.aspx</h4>
                    <div class ="ThumbList">
                    <asp:PlaceHolder ID="itemPlaceholder" runat="server"></asp:PlaceHolder>
                        </div>
                </LayoutTemplate>
                <ItemTemplate>
                    <%--    THUMBNAIL with hyperlink to present picture  />--%>
                    <div>
                        <asp:HyperLink runat="server" Text='<%# string.Format("{0}{1}", Item.ImgName, Item.Extension ) %>' ImageUrl='<%# string.Format("~/Content/Thumbnails/{0}{1}", Item.SaveName, Item.Extension ) %>' NavigateUrl='<%# GetRouteUrl("Image", new { CategoryID=Page.RouteData.Values["CategoryID"], ImageID = Item.ImageID })%>'></asp:HyperLink>
                        <asp:Label ID="Label" runat="server" Text='<%# BindItem.ImgName %>'></asp:Label>
                    </div>
                    <div>
                        <asp:LinkButton runat="server" CausesValidation="false" CommandName="Delete" Text="Radera"
                            OnClientClick='<%# String.Format("return confirm(\"Ta bort bilden {0}?\")", Item.ImgName  ) %>'></asp:LinkButton>
                        <asp:LinkButton runat="server" CausesValidation="false" CommandName="Edit" Text="Redigera"></asp:LinkButton>
                    </div>
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:HyperLink runat="server" Text='<%# string.Format("{0}{1}", Item.ImageID, Item.Extension ) %>' ImageUrl='<%# string.Format("~/Content/Thumbnails/{0}{1}", Item.ImageID, Item.Extension ) %>' NavigateUrl='<%# string.Format("{0}&name={1}{2}", GetRouteUrl("ImageList", Page.RouteData.Values["CategoryID"]), Item.ImageID,Item.Extension)  %>'></asp:HyperLink>

                    <asp:RequiredFieldValidator runat="server" ErrorMessage="Bildnamn fältet får ej lämnas tomt." ControlToValidate="ImgName" Text="*"></asp:RequiredFieldValidator>
                    <asp:TextBox ID="ImgName" runat="server" Text='<%# BindItem.ImgName %>' MaxLength="20"></asp:TextBox>
                     
                    <asp:LinkButton runat="server" CommandName="Update" Text="Spara" ValidationGroup="Update"></asp:LinkButton>
                    <asp:LinkButton runat="server" CommandName="Cancel" Text="Ångra" CausesValidation="false"></asp:LinkButton>
                </EditItemTemplate>

            </asp:ListView>

            <div>
                <asp:ValidationSummary ID="ValidationSummary" runat="server" />

                <asp:FileUpload ID="Select" runat="server" />
                <asp:RequiredFieldValidator runat="server" ErrorMessage="Ni måste först välja bild." ControlToValidate="Select" Display="None"></asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator Visible="false" runat="server" ErrorMessage="Bilden måste vara av typen jpg|png" ControlToValidate="Select" ValidationExpression="^.*\.(jpg|png|JPG|PNG)$"></asp:RegularExpressionValidator>     
                <asp:RequiredFieldValidator runat="server" ErrorMessage="Bildnamn fältet får ej lämnas tomt." ControlToValidate="PictureName" Display="Static"></asp:RequiredFieldValidator>
               
                <asp:TextBox ID="PictureName" runat="server" MaxLength="20"></asp:TextBox>
                <asp:Button ID="UploadButton" runat="server" Text="Ladda upp bild" OnClick="UploadButton_Click" />
                <asp:Button ID="DeleteButton" runat="server" Text="Radera bild" OnClick="DeleteButton_Click" />


      

                <asp:CustomValidator runat="server" ErrorMessage="En checkbox måste fyllas i." Display="None" OnServerValidate="CustomValidator1_ServerValidate"></asp:CustomValidator>
                <asp:CheckBoxList ID="CheckBoxLisT" runat="server" ItemType="MemberGallery.Model.Category"
                    DataTextField="CategoryProp" DataValueField="CategoryID" SelectMethod="CategoryListView_GetData" EnableViewState="False">
                </asp:CheckBoxList>

            </div>
            <asp:HyperLink ID="HyperLink" runat="server" Text="Till ända/Lägg till kategorier"
                NavigateUrl='<%$ RouteUrl:routename=Default %>'></asp:HyperLink>
     



    <%-- Here starts part contecrning upload new images. --%>
</asp:Content>
