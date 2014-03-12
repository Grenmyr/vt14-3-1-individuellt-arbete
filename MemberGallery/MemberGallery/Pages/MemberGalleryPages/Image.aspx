<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/Shared/Site.Master" AutoEventWireup="true" CodeBehind="Image.aspx.cs" Inherits="MemberGallery.Pages.MemberGalleryPages.Image" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder" runat="server">

    <asp:FormView ID="FormView" runat="server" DataKeyNames="ImageID" ItemType="MemberGallery.Model.Image" DefaultMode="ReadOnly" SelectMethod="FormView_GetItem">
        <ItemTemplate>
            <asp:Image ID="Image" ImageUrl='<%# String.Format("~/Content/Pictures/{0}{1}", Item.ImageID,Item.Extension )%>' runat="server" Width="800" />
            <div>
                <asp:Literal ID="Literal1" Text='<%#  String.Format("Bildnamn: {0}",Item.ImgName) %>' runat="server"></asp:Literal>
                <asp:Literal ID="Literal3" Text='<%#  String.Format("Senast Ändrad: {0}",Item.UpLoaded)  %>' runat="server"></asp:Literal>
            </div>

        </ItemTemplate>

    </asp:FormView>
</asp:Content>
