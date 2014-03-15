<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Upload.ascx.cs" Inherits="MemberGallery.Pages.Shared.Upload" %>

 <asp:PlaceHolder ID="UploadPlaceholder" runat="server" Visible="true">
        <%-- ValidationSummary catching all Errors  --%>
        <asp:ValidationSummary ID="ValidationSummary" runat="server" />

        <%-- FileupLoad button, with 2 Controls validating extension and that a file is selected. TODO: change property of Select button to swedish. --%>

        <asp:FileUpload ID="Select" runat="server" ToolTip="Välj bild" />
        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Ni måste först välja bild." ControlToValidate="Select" Display="none"></asp:RequiredFieldValidator>
        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" Display="None" ErrorMessage="Bild måste vara av filformat .JPG eller .PNG" ValidationExpression="^.*\.(jpg|png|PNG|JPG)$" ControlToValidate="Select"></asp:RegularExpressionValidator>

        <%-- Textbox to Choose Name to present picture with. --%>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Bildnamn fältet får ej lämnas tomt." ControlToValidate="PictureName" Display="none"></asp:RequiredFieldValidator>
        <asp:TextBox ID="PictureName" runat="server" MaxLength="20" text="Bildnamn" ></asp:TextBox>
        <asp:Button ID="UploadButton" runat="server" Text="Ladda upp bild" OnClick="UploadButton_Click" />

        <%-- Checkboxlist using Selectmethod to load available Categories.Presenting users option to save Image into serveral Categories. --%>
        <asp:CustomValidator ID="CustomValidator1" runat="server" ErrorMessage="En checkbox måste fyllas i." Display="None" OnServerValidate="CustomValidator_ServerValidate"></asp:CustomValidator>
        <asp:CheckBoxList ID="CheckBoxLisT" runat="server" ItemType="MemberGallery.Model.Category"
            DataTextField="CategoryProp" DataValueField="CategoryID" SelectMethod="CategoryListView_GetData" CssClass="CheckBoxList">
        </asp:CheckBoxList>
            </asp:PlaceHolder>
