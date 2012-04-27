<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Search.aspx.cs" Inherits="ListenedList.Search"
    MasterPageFile="~/Masters/Genius.Master" %>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">
    <div style="padding-left: 100px;">
        <br />
        <br />
        <p style="font-size: 2em; font-weight: 700;">
            Search for Public lists</p>
        <br />
        <br />
        <div>
            User Name:
            <asp:TextBox ID="txtUserName" runat="server"></asp:TextBox>&nbsp;&nbsp;&nbsp;
            <asp:Button ID="btnSearchUser" runat="server" Text="Search" OnClick="btnSearchUser_Click" />
        </div>
        <br />
        <br />
        <br />
        <p style="font-size: 1.3em; font-weight: 500;">
            <asp:Label ID="lblResultsType" runat="server" Text="15 Most Recently Changed Guides"></asp:Label>
            <asp:PlaceHolder ID="phReset" runat="server" Visible="false">
                &nbsp;---&nbsp;<asp:LinkButton ID="btnReset" runat="server" Text="Reset" OnClick="btnReset_Click" />
            </asp:PlaceHolder>
        </p>
        <br />
        <asp:Repeater ID="rptResults" runat="server">
            <HeaderTemplate>
                <table>
            </HeaderTemplate>
            <ItemTemplate>
                <tr>
                    <td>
                        <asp:LinkButton ID="lnkSubscribe" runat="server" CommandName="SUBSCRIBE" CommandArgument='<%# ((Core.Helpers.LatestProfile)Container.DataItem).Profile.UserName %>'
                            Text="Subscribe"></asp:LinkButton>
                    </td>
                    <td>
                        <asp:HyperLink NavigateUrl='<%# ((Core.Helpers.LatestProfile)Container.DataItem).Profile.Public == true ? "Default.aspx?userName=" + ((Core.Helpers.LatestProfile)Container.DataItem).Profile.UserName : "" %>'
                            runat="server" Text='<%# ((Core.Helpers.LatestProfile)Container.DataItem).Profile.UserName %>'></asp:HyperLink>
                    </td>
                    <td>
                        <%# ((Core.Helpers.LatestProfile)Container.DataItem).Profile.Public == true ? "Public" : "Private" %>
                    </td>
                    <td>
                        Latest Updated Show: <%# ((Core.Helpers.LatestProfile)Container.DataItem).LatestShow.GetShowName() %>
                    </td>
                </tr>
            </ItemTemplate>
            <FooterTemplate>
                </table></FooterTemplate>
        </asp:Repeater>
    </div>
</asp:Content>
