<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/Shared/Site.Master" AutoEventWireup="true" CodeBehind="Error.aspx.cs" Inherits="MemberGallery.Pages.Shared.Error" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <div class="errorMSG">
        <p>
            Ett oväntat fel inträffade, Försök igen.
        </p>
        <p>
            <a id="A1" href='<%$ RouteUrl:routename=Default %>' runat="server">Tillbaka till Kategorier</a>
        </p>
    </div>
</asp:Content>
