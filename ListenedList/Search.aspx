<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Search.aspx.cs" Inherits="ListenedList.Search"
    MasterPageFile="~/Masters/Wooden.Master" %>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">
    <div class="mainDiv">
        <br />
        <br />
        <p style="font-size: 2em; font-weight: 700;">
            Search for Public lists</p>
        <br />
        <br />
        <div>
            User Name:
            <asp:TextBox ID="txtUserName" runat="server"></asp:TextBox>&nbsp;&nbsp;&nbsp;
            <asp:Button ID="btnSearchUser" runat="server" Text="Search" CssClass="normalButton" OnClick="btnSearchUser_Click" />
        </div>
        <br />
        <br />
        <br />
        <p style="font-size: 1.7em; font-weight: 500;">
            <asp:Label ID="lblResultsType" runat="server" Text="15 Most Recently Changed Guides"></asp:Label>
            <asp:PlaceHolder ID="phReset" runat="server" Visible="false">&nbsp;---&nbsp;<asp:LinkButton
                ID="btnReset" runat="server" Text="Reset" OnClick="btnReset_Click" />
            </asp:PlaceHolder>
        </p>
        <br />
        <asp:Repeater ID="rptResults" runat="server" OnItemCommand="rptResults_ItemCommand">
            <HeaderTemplate>
                <table>
            </HeaderTemplate>
            <ItemTemplate>
                <tr style="height: 40px;font-size: 1.3em;">
                    <%--<td style="float: right; vertical-align: bottom;">
                        <asp:LinkButton ID="lnkSubscribe" runat="server" CssClass='<%# ((Core.Helpers.LatestProfile)Container.DataItem).Subscribed == true ? "subscribedButton" : "subscribeButton" %>'
                            CommandName="SUBSCRIBE" CommandArgument='<%# ((Core.Helpers.LatestProfile)Container.DataItem).Profile.UserName %>'
                            Enabled='<%# ((Core.Helpers.LatestProfile)Container.DataItem).Subscribed == true ? false : true %>'
                            Text='<%# ((Core.Helpers.LatestProfile)Container.DataItem).Subscribed == true ? "SUBSCRIBED" : "SUBSCRIBE" %>'></asp:LinkButton>
                    </td>--%>
                    <td>
                        <strong>
                            <asp:HyperLink NavigateUrl='<%# ((Core.Helpers.LatestProfile)Container.DataItem).Profile.Public == true ? "Main/" + ((Core.Helpers.LatestProfile)Container.DataItem).Profile.UserName : "" %>'
                                runat="server" Style="font-size: larger;" Text='<%# ((Core.Helpers.LatestProfile)Container.DataItem).Profile.UserName %>'></asp:HyperLink></strong>----
                    </td>
                    <td>
                        <%# ((Core.Helpers.LatestProfile)Container.DataItem).Profile.Public == true ? "Public" : "Private" %>----
                    </td>
                    <td>
                        Last Updated Show:
                        <%# ((Core.Helpers.LatestProfile)Container.DataItem).LatestShow.GetShowName() %>
                    </td>
                </tr>
            </ItemTemplate>
            <FooterTemplate>
                </table></FooterTemplate>
        </asp:Repeater>
    </div>
    <asp:HiddenField ID="hdnSearchMode" runat="server" Visible="false" />
</asp:Content>
