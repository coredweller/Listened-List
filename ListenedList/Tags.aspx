﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Tags.aspx.cs" Inherits="ListenedList.Tags"
    MasterPageFile="~/Masters/Wooden.Master" %>

<%@ Register TagPrefix="uc" TagName="TagControl" Src="~/Controls/TagControl.ascx" %>
<asp:Content ContentPlaceHolderID="MainContent" runat="server">
   <uc:TagControl ID="tagControl" runat="server" Tutorial="false" />
</asp:Content>
