<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="DevExpressGalleryDataCharts.aspx.cs" Inherits="DevExpressGalleryDataCharts" %>

<%@ Register assembly="DevExpress.Web.Bootstrap.v17.2, Version=17.2.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.Bootstrap" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.v17.2, Version=17.2.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web" tagprefix="dx" %>
<asp:Content ID="Content1" runat="server" contentplaceholderid="MainContent">
    <p>
        BootstrapGridView</p>
    <dx:BootstrapGridView ID="BootstrapGridView1" runat="server">
    </dx:BootstrapGridView>
    <p>
        &nbsp;</p>
    <p>
        BootstrapChart</p>
    <p>
        &nbsp;</p>
</asp:Content>


