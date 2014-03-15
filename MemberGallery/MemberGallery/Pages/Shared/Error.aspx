<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/Shared/Site.Master" AutoEventWireup="true" CodeBehind="Error.aspx.cs" Inherits="MemberGallery.Pages.Shared.Error" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <div class="errorMSG">
        <p>
            Vi är beklagar att ett fel inträffade och vi inte kunde hantera din förfrågan.
        </p>
        <p>
            <a id="A1" href='<%$ RouteUrl:routename=Default %>' runat="server">Tillbaka till Kategorier</a>
        </p>
    </div>
</asp:Content>
