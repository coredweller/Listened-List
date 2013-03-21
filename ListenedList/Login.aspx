<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="ListenedList.Login"
    MasterPageFile="~/Masters/Wooden.Master" %>

<%@ Register TagPrefix="uc" TagName="Login" Src="~/Controls/Login.ascx" %>
<asp:Content ContentPlaceHolderID="MainContent" runat="server">
    <div class="mainDiv">
        <br />
        <br />
        <uc:Login ID="Login1" runat="Server"></uc:Login>
        <br />
        <br />
        <br />
    </div>
</asp:Content>
