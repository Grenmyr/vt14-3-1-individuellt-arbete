<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/Shared/Site.Master" AutoEventWireup="true" CodeBehind="ImageList.aspx.cs" Inherits="MemberGallery.Pages.MemberGalleryPages.ImageList" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder" runat="server">

    <%-- Listview generating images --%>
    <asp:ListView ID="ImageListView" runat="server" ItemType="MemberGallery.Model.Image"
        SelectMethod="ImageListView_GetData"
        DataKeyNames="ImageID">
        <LayoutTemplate>

            <h4>Kategorier</h4>

            <div class="ThumbList">
                <asp:PlaceHolder ID="itemPlaceholder" runat="server"></asp:PlaceHolder>
            </div>
        </LayoutTemplate>
        <ItemTemplate>
            <%--    THUMBNAIL with hyperlink to present picture in image.aspx />--%>
            <asp:HyperLink runat="server" Text='<%# string.Format("{0}", Item.ImgName ) %>' ImageUrl='<%# string.Format("~/Content/Thumbnails/{0}", Item.SaveName ) %>' NavigateUrl='<%# GetRouteUrl("Image", new { CategoryID=Page.RouteData.Values["CategoryID"], ImageID = Item.ImageID })%>'></asp:HyperLink>
        </ItemTemplate>
    </asp:ListView>
            
    <div class="EditCategories">          
    <%-- Hyperlink to Edit/Delete Categories. --%>
    <asp:HyperLink ID="HyperLink" runat="server" Text="Gå till Kategorier"
        NavigateUrl='<%$ RouteUrl:routename=Default %>'></asp:HyperLink>
        </div> 

        <%-- ValidationSummary catching all Errors  --%>
        <asp:ValidationSummary ID="ValidationSummary" runat="server" />

        <%-- FileupLoad button, with 2 Controls validating extension and that a file is selected. TODO: change property of Select button to swedish. --%>
    
        <asp:FileUpload ID="Select" runat="server" ToolTip="Välj bild" />
        <asp:RequiredFieldValidator runat="server" ErrorMessage="Ni måste först välja bild." ControlToValidate="Select" Display="none"></asp:RequiredFieldValidator>
        <asp:RegularExpressionValidator runat="server" Display="None" ErrorMessage="Bild måste vara av filformat .JPG eller .PNG" ValidationExpression="^.*\.(jpg|png|PNG|JPG)$" ControlToValidate="Select"></asp:RegularExpressionValidator>

        <%-- Textbox to Choose Name to present picture with. --%>
        <asp:RequiredFieldValidator runat="server" ErrorMessage="Bildnamn fältet får ej lämnas tomt." ControlToValidate="PictureName" Display="none"></asp:RequiredFieldValidator>
        <asp:TextBox ID="PictureName" runat="server" MaxLength="20"  ToolTip="Fyll i Bildnamn"></asp:TextBox>
        <asp:Button ID="UploadButton" runat="server" Text="Ladda upp bild" OnClick="UploadButton_Click" />
    <div class="checkboxArea">
        <%-- Checkboxlist using Selectmethod to load available Categories.Presenting users option to save Image into serveral Categories. --%>
        <asp:CustomValidator runat="server" ErrorMessage="En checkbox måste fyllas i." Display="None" OnServerValidate="CustomValidator_ServerValidate"></asp:CustomValidator>
        <asp:CheckBoxList ID="CheckBoxLisT" runat="server" ItemType="MemberGallery.Model.Category"
            DataTextField="CategoryProp" DataValueField="CategoryID" SelectMethod="CategoryListView_GetData" CssClass="CheckBoxList" RepeatDirection="Horizontal" RepeatColumns="5" TextAlign="Right" CellSpacing="10" CellPadding="1">
        </asp:CheckBoxList>
         <div class="clear"></div>
    </div>
    
     
</asp:Content>
