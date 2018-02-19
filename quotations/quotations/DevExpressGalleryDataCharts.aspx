<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="DevExpressGalleryDataCharts.aspx.cs" Inherits="DevExpressGalleryDataCharts" %>

<%@ Register assembly="DevExpress.Web.Bootstrap.v17.2, Version=17.2.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.Bootstrap" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.v17.2, Version=17.2.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web" tagprefix="dx" %>
<asp:Content ID="Content1" runat="server" contentplaceholderid="MainContent">
    <p>
        BootstrapGridView</p>
    <dx:BootstrapGridView ID="BootstrapGridView1" runat="server" AutoGenerateColumns="False" DataSourceID="pbn_sql_server">
        <Settings ShowFilterRow="True" />
        <Columns>
            <dx:BootstrapGridViewCommandColumn ShowClearFilterButton="True" VisibleIndex="0">
            </dx:BootstrapGridViewCommandColumn>
            <dx:BootstrapGridViewTextColumn FieldName="NameOfBound" VisibleIndex="1">
            </dx:BootstrapGridViewTextColumn>
            <dx:BootstrapGridViewTextColumn FieldName="Price" VisibleIndex="2">
            </dx:BootstrapGridViewTextColumn>
            <dx:BootstrapGridViewTextColumn FieldName="DurationYears" VisibleIndex="3">
            </dx:BootstrapGridViewTextColumn>
        </Columns>
    </dx:BootstrapGridView>
    <p>
        &nbsp;</p>
    <p>
        BootstrapChart</p>
    <p>
        &nbsp;</p>
    <p>
        default datagrid</p>
    <p>
        &nbsp;</p>
    <p>
        <asp:SqlDataSource ID="pbn_sql_server" runat="server" ConnectionString="<%$ ConnectionStrings:DBConnectionString %>" SelectCommand="SELECT TOP 100 [NameOfBound], [Price], [DurationYears] FROM [Q_Bounds]"></asp:SqlDataSource>
    </p>
</asp:Content>


