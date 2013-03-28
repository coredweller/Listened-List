<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Step2.aspx.cs" Inherits="ListenedList.Step2"
    MasterPageFile="~/Masters/Wooden.Master" %>
<%@ Import Namespace="Microsoft.AspNet.FriendlyUrls" %>

<%@ Register TagPrefix="uc" TagName="TagControl" Src="~/Controls/TagControl.ascx" %>
<asp:Content ContentPlaceHolderID="MainContent" runat="server">
    <br />
    <br />
    <div class="mainDiv" style="padding-right: 200px;">
        <h3>
            Attach custom Tags to shows, such as "Best Jams", "Favorite Wolfman's Brothers",
            or anything else you can come up with to keep track of it all.
            <br /><br /> Create as many as you want and use different colors as well.
            <br /><br />
            This is the tutorial Tags page.  Create and do whatever you like. To use this page without the tutorial text go <a href="<%: FriendlyUrl.Href("/Tags") %>">HERE</a>
        </h3>
        <br />
        <br />



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
