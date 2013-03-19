<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddShows.aspx.cs" Inherits="ListenedList.AddShows"
    MasterPageFile="~/Masters/Wooden.Master" %>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">
    <br />
    <div class="mainDiv">
        <br />
        <br />
        <p style="font-size: 1.2em; font-weight: 500;">
            Choose a year to display shows from that year.
        </p>
        <br />
        <div>
            Year:<asp:DropDownList ID="ddlYears" runat="server">
            </asp:DropDownList>
            <asp:Button ID="btnSubmit" runat="server" Text="Choose Year" CssClass="normalButton"
                OnClick="btnSubmit_Click" />
        </div>
        <br />
        <h5 class="warningMessage">
            Please Note: There is no need to save! <br />When you click the buttons its saves automatically.
            Enjoy!</h5>
        <br />
        <br />
        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <div>
                    <asp:Repeater ID="rptAdder" runat="server" OnItemCommand="rptAdder_ItemCommand">
                        <HeaderTemplate>
                            <table>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <tr>
                                <td style="padding-left: 12px;">
                                    <asp:Button ID="btnNeverHeard" runat="server" Text="Never Heard" CommandArgument='<%# (int)Core.Services.ListenedStatus.None %>'
                                        CommandName='<%# (((KeyValuePair<Core.DomainObjects.IShow,Core.DomainObjects.IListenedShow>)Container.DataItem)).Key.Id %>'
                                        CssClass='<%# GetClass("defaultButtonWhite", Core.Services.ListenedStatus.None, (((KeyValuePair<Core.DomainObjects.IShow,Core.DomainObjects.IListenedShow>)Container.DataItem)).Value != null ? (((KeyValuePair<Core.DomainObjects.IShow,Core.DomainObjects.IListenedShow>)Container.DataItem)).Value.Status : -1) %>'
                                        ToolTip='<%# (((KeyValuePair<Core.DomainObjects.IShow,Core.DomainObjects.IListenedShow>)Container.DataItem)).Key.ShowDate.Value.ToShortDateString() %>' />
                                </td>
                                <td style="padding-left: 20px;">
                                    <asp:Button ID="btnFinished" runat="server" Text="Finished" CommandArgument='<%# (int)Core.Services.ListenedStatus.Finished %>'
                                        CommandName='<%# (((KeyValuePair<Core.DomainObjects.IShow,Core.DomainObjects.IListenedShow>)Container.DataItem)).Key.Id %>'
                                        CssClass='<%# GetClass("defaultButtonOrange", Core.Services.ListenedStatus.Finished, (((KeyValuePair<Core.DomainObjects.IShow,Core.DomainObjects.IListenedShow>)Container.DataItem)).Value != null ? (((KeyValuePair<Core.DomainObjects.IShow,Core.DomainObjects.IListenedShow>)Container.DataItem)).Value.Status : -1) %>'
                                        ToolTip='<%# (((KeyValuePair<Core.DomainObjects.IShow,Core.DomainObjects.IListenedShow>)Container.DataItem)).Key.ShowDate.Value.ToShortDateString() %>' />
                                </td>
                                <td style="padding-left: 25px;">
                                    <asp:Button ID="btnInProgress" runat="server" Text="In Progress" CommandArgument='<%# (int)Core.Services.ListenedStatus.InProgress %>'
                                        CommandName='<%# (((KeyValuePair<Core.DomainObjects.IShow,Core.DomainObjects.IListenedShow>)Container.DataItem)).Key.Id %>'
                                        CssClass='<%# GetClass("defaultButtonYellow", Core.Services.ListenedStatus.InProgress, (((KeyValuePair<Core.DomainObjects.IShow,Core.DomainObjects.IListenedShow>)Container.DataItem)).Value != null ? (((KeyValuePair<Core.DomainObjects.IShow,Core.DomainObjects.IListenedShow>)Container.DataItem)).Value.Status : -1) %>'
                                        ToolTip='<%# (((KeyValuePair<Core.DomainObjects.IShow,Core.DomainObjects.IListenedShow>)Container.DataItem)).Key.ShowDate.Value.ToShortDateString() %>' />
                                </td>
                                <td style="padding-left: 28px;">
                                    <asp:Button ID="btnNeedToListen" runat="server" Text="Need To Listen" CommandArgument='<%# (int)Core.Services.ListenedStatus.NeedToListen %>'
                                        CommandName='<%# (((KeyValuePair<Core.DomainObjects.IShow,Core.DomainObjects.IListenedShow>)Container.DataItem)).Key.Id %>'
                                        CssClass='<%# GetClass("defaultButtonGreen", Core.Services.ListenedStatus.NeedToListen, (((KeyValuePair<Core.DomainObjects.IShow,Core.DomainObjects.IListenedShow>)Container.DataItem)).Value != null ? (((KeyValuePair<Core.DomainObjects.IShow,Core.DomainObjects.IListenedShow>)Container.DataItem)).Value.Status : -1) %>'
                                        ToolTip='<%# (((KeyValuePair<Core.DomainObjects.IShow,Core.DomainObjects.IListenedShow>)Container.DataItem)).Key.ShowDate.Value.ToShortDateString() %>' />
                                </td>
                                <td>
                                    <b>
                                        <%# ( ( (KeyValuePair<Core.DomainObjects.IShow, Core.DomainObjects.IListenedShow>)Container.DataItem ) ).Key.GetShowName() %></b>
                                </td>
                                <td>
                                    <asp:Button ID="btnAttended" runat="server" Text='<%# (((KeyValuePair<Core.DomainObjects.IShow,Core.DomainObjects.IListenedShow>)Container.DataItem)).Value != null ? (((KeyValuePair<Core.DomainObjects.IShow,Core.DomainObjects.IListenedShow>)Container.DataItem)).Value.Attended ? "Attended" : "Did Not Attend" : "Did Not Attend" %>'
                                        CommandArgument='<%# (int)Core.Services.ListenedStatus.Attended %>' CommandName='<%# (((KeyValuePair<Core.DomainObjects.IShow,Core.DomainObjects.IListenedShow>)Container.DataItem)).Key.Id %>'
                                        CssClass='<%# (((KeyValuePair<Core.DomainObjects.IShow,Core.DomainObjects.IListenedShow>)Container.DataItem)).Value != null ? (((KeyValuePair<Core.DomainObjects.IShow,Core.DomainObjects.IListenedShow>)Container.DataItem)).Value.Attended ? "notesAttended" : "notesDidNotAttend" : "notesDidNotAttend" %>'
                                        ToolTip='<%# (((KeyValuePair<Core.DomainObjects.IShow,Core.DomainObjects.IListenedShow>)Container.DataItem)).Key.ShowDate.Value.ToShortDateString() %>' />
                                </td>
                            </tr>
                            <tr>
                                <td colspan="5">
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
        <asp:PlaceHolder ID="phNoSaveMessage" runat="server" Visible="false">
            <br />
            <h5 class="warningMessage">
                Please Note: There is no need to save. When you click the buttons its saves automatically.
                Enjoy!</h5>
        </asp:PlaceHolder>
    </div>
    <br />
    <br />
    <br />
</asp:Content>
