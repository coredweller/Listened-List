<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddShows.aspx.cs" Inherits="ListenedList.AddShows"
    MasterPageFile="~/Masters/Genius.Master" %>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">
    <br />
    <%--<asp:HyperLink ID="lnkBack" runat="server" Text="Back to Show Page" NavigateUrl="/Default.aspx"></asp:HyperLink>--%>
    <div style="padding-left: 100px;">
        <br />
        <br />
        <p style="font-size: 1.2em; font-weight: 500;">
            Choose a year to display shows. Then choose the listened status you want for each
            show.</p>
        <br />
        <div>
            Year:<asp:DropDownList ID="ddlYears" runat="server">
            </asp:DropDownList>
            <asp:Button ID="btnSubmit" runat="server" Text="Submit" OnClick="btnSubmit_Click" />
        </div>
        <br />
        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <div>
                    <asp:Repeater ID="rptAdder" runat="server" OnItemCreated="rptAdder_ItemCreated">
                        <HeaderTemplate>
                            <table>
                                <tr>
                                    <td>
                                        Finished --
                                    </td>
                                    <td>
                                        In Progress --
                                    </td>
                                    <td>
                                        Need To Listen
                                    </td>
                                </tr>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <tr>
                                <td style="padding-left: 12px;">
                                    <asp:CheckBox ID="chkFinished" runat="server" AutoPostBack="true" Checked='<%# (((KeyValuePair<Core.DomainObjects.IShow,Core.DomainObjects.IListenedShow>)Container.DataItem)).Value != null ? (((KeyValuePair<Core.DomainObjects.IShow,Core.DomainObjects.IListenedShow>)Container.DataItem)).Value.Status == (int)Core.Services.ListenedStatus.Finished ? true : false : false %>'
                                        ToolTip='<%# (((KeyValuePair<Core.DomainObjects.IShow,Core.DomainObjects.IListenedShow>)Container.DataItem)).Key.ShowDate.Value.ToShortDateString() %>' />
                                    <%--<%# (((KeyValuePair<Core.DomainObjects.IShow,Core.DomainObjects.IListenedShow>)Container.DataItem)).Key.ShowDate.Value.ToShortDateString() + (int)Core.Services.ListenedStatus.Finished %>--%>
                                </td>
                                <td style="padding-left: 20px;">
                                    <asp:CheckBox ID="chkInProgress" runat="server" AutoPostBack="true" Checked='<%# (((KeyValuePair<Core.DomainObjects.IShow,Core.DomainObjects.IListenedShow>)Container.DataItem)).Value != null ? (((KeyValuePair<Core.DomainObjects.IShow,Core.DomainObjects.IListenedShow>)Container.DataItem)).Value.Status == (int)Core.Services.ListenedStatus.InProgress ? true : false : false %>'
                                        ToolTip='<%# (((KeyValuePair<Core.DomainObjects.IShow,Core.DomainObjects.IListenedShow>)Container.DataItem)).Key.ShowDate.Value.ToShortDateString() %>' />
                                </td>
                                <td style="padding-left: 25px;">
                                    <asp:CheckBox ID="chkNeedToListen" runat="server" AutoPostBack="true" Checked='<%# (((KeyValuePair<Core.DomainObjects.IShow,Core.DomainObjects.IListenedShow>)Container.DataItem)).Value != null ? (((KeyValuePair<Core.DomainObjects.IShow,Core.DomainObjects.IListenedShow>)Container.DataItem)).Value.Status == (int)Core.Services.ListenedStatus.NeedToListen ? true : false : false %>'
                                        ToolTip='<%# (((KeyValuePair<Core.DomainObjects.IShow,Core.DomainObjects.IListenedShow>)Container.DataItem)).Key.ShowDate.Value.ToShortDateString() %>' />
                                </td>
                                <td>
                                    <b>
                                        <%# (((KeyValuePair<Core.DomainObjects.IShow,Core.DomainObjects.IListenedShow>)Container.DataItem)).Key.GetShowName() %></b>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="4">
                                    <hr class="horizontalRule1" />
                                </td>
                            </tr>
                        </ItemTemplate>
                        <FooterTemplate>
                            </table></FooterTemplate>
                    </asp:Repeater>
                </div>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="rptAdder" EventName="ItemCreated" />
            </Triggers>
        </asp:UpdatePanel>
    </div>
</asp:Content>
