<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/Shared/Site.Master" AutoEventWireup="true" CodeBehind="Image.aspx.cs" Inherits="MemberGallery.Pages.MemberGalleryPages.Image" %>



<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder" runat="server">


    <%-- Formview with edititemtemplate that let  me change Name on Image, Automaticly load new date into database on changed. --%>
    <asp:FormView ID="FormView" runat="server" DataKeyNames="ImageID" ItemType="MemberGallery.Model.Image"  SelectMethod="FormView_GetItem" UpdateMethod="FormView_UpdateItem" DeleteMethod="FormView_DeleteItem">
        <ItemTemplate>
            <asp:Image ID="Image" ImageUrl='<%# String.Format("~/Content/Pictures/{0}", Item.SaveName )%>' runat="server" Width="800" />
            <div>
                <asp:Literal ID="Literal1" Text='<%#  String.Format("Bildnamn: {0}",Item.ImgName) %>' runat="server"></asp:Literal>
                <asp:LinkButton ID="LinkButton3" runat="server" CausesValidation="false" CommandName="Delete" Text="Radera"
                    OnClientClick='<%# String.Format("return confirm(\"Ta bort bilden {0}?\")", Item.ImgName  ) %>'></asp:LinkButton>
                <asp:LinkButton ID="LinkButton4" runat="server" CausesValidation="false" CommandName="Edit" Text="Redigera"></asp:LinkButton>
            </div>
            <div>
                <asp:Literal ID="Literal3" Text='<%#  String.Format("Senast Ändrad: {0}",Item.UpLoaded)  %>' runat="server"></asp:Literal>
            </div>
        </ItemTemplate>
        <EditItemTemplate>
            <asp:Image ID="Image" ImageUrl='<%# String.Format("~/Content/Pictures/{0}", Item.SaveName )%>' runat="server" Width="800" />
            <div>
                <asp:TextBox ID="ImgName" runat="server" Text='<%# BindItem.ImgName %>' MaxLength="20" ></asp:TextBox>
                <asp:LinkButton ID="LinkButton1" runat="server" CommandName="Update" Text="Spara" ValidationGroup="Update"></asp:LinkButton>
                <asp:LinkButton ID="LinkButton2" runat="server" CommandName="Cancel" Text="Ångra" CausesValidation="false"></asp:LinkButton>
            </div>
                 <div>
                     <asp:Literal ID="Literal3" Text='<%#  String.Format("Senast Ändrad: {0}",Item.UpLoaded)  %>' runat="server"></asp:Literal>
                 </div>
        </EditItemTemplate>
    </asp:FormView>

</asp:Content>
