<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Step2.aspx.cs" Inherits="ListenedList.Step2"
    MasterPageFile="~/Masters/Wooden.Master" %>

<%@ Import Namespace="Microsoft.AspNet.FriendlyUrls" %>
<%@ Register TagPrefix="uc" TagName="TagControl" Src="~/Controls/TagControl.ascx" %>
<asp:Content ContentPlaceHolderID="MainContent" runat="server">
    <div id="content" style="font-size: 14px;">
        <span class="tagControlButtons" style="font-size: 20px;">Attach custom Tags to shows,
            such as:
            <br />
            <br />
            <asp:LinkButton CssClass='orangeTag' runat="server" ID="lnkTag" Enabled="false" Text='Best Jams'></asp:LinkButton>
            <asp:LinkButton CssClass='greenTag' runat="server" ID="lnkTag2" Enabled="false" Text='Favorite YEM'></asp:LinkButton><br />
            <br />
            <br />
            <br />
            These are just examples.
            <br />
            <br /><br />
            Be Creative! Create whatever tags you want to add to your shows.
            <br />
            <br /><br />
            Plenty of colors to separate your tags into categories.
            <br />
            <br />
        </span>
        <br />
        <br />
        <br />
        <br />
        This is the tutorial Tags page. Create and do whatever you like. To use this page
        without the tutorial text go <a href='<%= LinkBuilder.DefaultTagsLink() %>'>HERE</a>
    </div>
    <br />
    <br />
    <br />
    <div class="mainDiv" style="padding-right: 200px;">
        <uc:TagControl ID="tagControl" runat="server" Tutorial="true" />
        <%--

        <h3>
            Part 1: Creating and Applying Tags
        </h3>
        <li>Create new Tags simply by giving them a name.</li>
        <li>Apply previously created Tags to a show.</li>
        <h3 style="padding-top: 200px;">
            Part 2: Altering Tags
        </h3>
        <h3 style="padding-top: 200px;">
            Part 3: View Tagged Shows
        </h3>--%>
    </div>
</asp:Content>
