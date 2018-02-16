<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="DevExpressGallery.aspx.cs" Inherits="DevExpressGallery" %>

<%@ Register assembly="DevExpress.Web.v17.2, Version=17.2.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web" tagprefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" Runat="Server">
    <p>
    Here are some DevExpress controls to check out</p>
a button<dx:ASPxButton ID="ASPxButton1" runat="server" Text="ASPxButton" Theme="MaterialCompact">
</dx:ASPxButton>
<br />
a calendar<br />
<dx:ASPxCalendar ID="ASPxCalendar1" runat="server" Theme="MaterialCompact">
</dx:ASPxCalendar>
<br />
a captcha<br />
<br />
<dx:ASPxCaptcha ID="ASPxCaptcha1" runat="server">
</dx:ASPxCaptcha>
<br />
checkbox and radiobutton<br />
<dx:ASPxCheckBox ID="ASPxCheckBox1" runat="server" Theme="MaterialCompact">
</dx:ASPxCheckBox>
<dx:ASPxRadioButton ID="ASPxRadioButton1" runat="server" Theme="MaterialCompact">
</dx:ASPxRadioButton>
<br />
a combobox<br />
<br />
<dx:ASPxComboBox ID="ASPxComboBox1" runat="server" Theme="MaterialCompact">
</dx:ASPxComboBox>
</asp:Content>

