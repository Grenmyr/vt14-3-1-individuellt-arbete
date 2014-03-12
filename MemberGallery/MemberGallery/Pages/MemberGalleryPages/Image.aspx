<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/Shared/Site.Master" AutoEventWireup="true" CodeBehind="Image.aspx.cs" Inherits="MemberGallery.Pages.MemberGalleryPages.Image" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder" runat="server">

      <asp:FormView ID="FormView" runat="server" DataKeyNames="ImageID" ItemType="MemberGallery.Model.Image" DefaultMode="ReadOnly" SelectMethod="FormView_GetItem">
                    <ItemTemplate>
                        <asp:Literal ID="Literal1" Text='<%# Item.ImgName %>' runat="server"></asp:Literal>
                    </ItemTemplate>

                </asp:FormView>
</asp:Content>
