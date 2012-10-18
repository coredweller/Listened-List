<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddShows.aspx.cs" Inherits="ListenedList.AddShows"
    MasterPageFile="~/Masters/Genius.Master" %>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">
    <br />
    <%--<asp:HyperLink ID="lnkBack" runat="server" Text="Back to Show Page" NavigateUrl="/Default.aspx"></asp:HyperLink>--%>
    <div style="padding-left: 100px;">
        <br />
        <br />
        <p style="font-size: 1.2em; font-weight: 500;">
            Choose a year to display shows from that year.
        </p>
        <br />
        <div>
            Year:<asp:DropDownList ID="ddlYears" runat="server">
            </asp:DropDownList>
            <asp:Button ID="btnSubmit" runat="server" Text="Choose Year" CssClass="normalButton" OnClick="btnSubmit_Click" />
        </div>
        <br />
        <br />
        <asp:PlaceHolder ID="phSaveButton1" runat="server" Visible="false">
            <asp:Button ID="btnSave" runat="server" CssClass="buttonSave" Text="SAVE" OnClick="SaveAll" />
        </asp:PlaceHolder>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <div>
                    <asp:Repeater ID="rptAdder" runat="server">
                        <%--OnItemCreated="rptAdder_ItemCreated">--%>
                        <HeaderTemplate>
                            <table>
                                <tr>
                                    <td>
                                        Never Heard --
                                    </td>
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
                                    <asp:Button ID="btnNeverHeard" runat="server" CommandArgument='<%# (int)Core.Services.ListenedStatus.None %>' Text="Never Heard" 
                                        CssClass='<%# GetClass("defaultButtonWhite", Core.Services.ListenedStatus.None, (((KeyValuePair<Core.DomainObjects.IShow,Core.DomainObjects.IListenedShow>)Container.DataItem)).Value != null ? (((KeyValuePair<Core.DomainObjects.IShow,Core.DomainObjects.IListenedShow>)Container.DataItem)).Value.Status : -1) %>'
                                        ToolTip='<%# (((KeyValuePair<Core.DomainObjects.IShow,Core.DomainObjects.IListenedShow>)Container.DataItem)).Key.ShowDate.Value.ToShortDateString() %>'
                                    />
                                    
                                    <%--<asp:RadioButton ID="rdoNeverHeard" runat="server" AutoPostBack="true" GroupName="Status"
                                        Checked='<%# (((KeyValuePair<Core.DomainObjects.IShow,Core.DomainObjects.IListenedShow>)Container.DataItem)).Value != null ? (((KeyValuePair<Core.DomainObjects.IShow,Core.DomainObjects.IListenedShow>)Container.DataItem)).Value.Status == (int)Core.Services.ListenedStatus.None ? true : false : true %>'
                                        ToolTip='<%# (((KeyValuePair<Core.DomainObjects.IShow,Core.DomainObjects.IListenedShow>)Container.DataItem)).Key.ShowDate.Value.ToShortDateString() %>' />--%>
                                </td>
                                <td style="padding-left: 20px;">
                                    <asp:Button ID="btnFinished" runat="server" CommandArgument='<%# (int)Core.Services.ListenedStatus.Finished %>' Text="Finished"
                                        CssClass='<%# GetClass("defaultButtonOrange", Core.Services.ListenedStatus.Finished, (((KeyValuePair<Core.DomainObjects.IShow,Core.DomainObjects.IListenedShow>)Container.DataItem)).Value != null ? (((KeyValuePair<Core.DomainObjects.IShow,Core.DomainObjects.IListenedShow>)Container.DataItem)).Value.Status : -1) %>'
                                        ToolTip='<%# (((KeyValuePair<Core.DomainObjects.IShow,Core.DomainObjects.IListenedShow>)Container.DataItem)).Key.ShowDate.Value.ToShortDateString() %>'
                                    />
                                    
                                    <%--<asp:RadioButton ID="rdoFinished" runat="server" AutoPostBack="true" GroupName="Status"
                                        Checked='<%# (((KeyValuePair<Core.DomainObjects.IShow,Core.DomainObjects.IListenedShow>)Container.DataItem)).Value != null ? (((KeyValuePair<Core.DomainObjects.IShow,Core.DomainObjects.IListenedShow>)Container.DataItem)).Value.Status == (int)Core.Services.ListenedStatus.Finished ? true : false : false %>'
                                        ToolTip='<%# (((KeyValuePair<Core.DomainObjects.IShow,Core.DomainObjects.IListenedShow>)Container.DataItem)).Key.ShowDate.Value.ToShortDateString() %>' />--%>
                                </td>
                                <td style="padding-left: 25px;">
                                    <asp:Button ID="btnInProgress" runat="server" CommandArgument='<%# (int)Core.Services.ListenedStatus.InProgress %>' Text="In Progress" 
                                        CssClass='<%# GetClass("defaultButtonYellow", Core.Services.ListenedStatus.InProgress, (((KeyValuePair<Core.DomainObjects.IShow,Core.DomainObjects.IListenedShow>)Container.DataItem)).Value != null ? (((KeyValuePair<Core.DomainObjects.IShow,Core.DomainObjects.IListenedShow>)Container.DataItem)).Value.Status : -1) %>'
                                        ToolTip='<%# (((KeyValuePair<Core.DomainObjects.IShow,Core.DomainObjects.IListenedShow>)Container.DataItem)).Key.ShowDate.Value.ToShortDateString() %>'
                                    />

                                    <%--<asp:RadioButton ID="rdoInProgress" runat="server" AutoPostBack="true" GroupName="Status"
                                        Checked='<%# (((KeyValuePair<Core.DomainObjects.IShow,Core.DomainObjects.IListenedShow>)Container.DataItem)).Value != null ? (((KeyValuePair<Core.DomainObjects.IShow,Core.DomainObjects.IListenedShow>)Container.DataItem)).Value.Status == (int)Core.Services.ListenedStatus.InProgress ? true : false : false %>'
                                        ToolTip='<%# (((KeyValuePair<Core.DomainObjects.IShow,Core.DomainObjects.IListenedShow>)Container.DataItem)).Key.ShowDate.Value.ToShortDateString() %>' />--%>
                                </td>
                                <td style="padding-left: 28px;">
                                    <asp:Button ID="btnNeedToListen" runat="server" CommandArgument='<%# (int)Core.Services.ListenedStatus.NeedToListen %>' Text="Need To Listen"
                                        CssClass='<%# GetClass("defaultButtonGreen", Core.Services.ListenedStatus.NeedToListen, (((KeyValuePair<Core.DomainObjects.IShow,Core.DomainObjects.IListenedShow>)Container.DataItem)).Value != null ? (((KeyValuePair<Core.DomainObjects.IShow,Core.DomainObjects.IListenedShow>)Container.DataItem)).Value.Status : -1) %>'
                                        ToolTip='<%# (((KeyValuePair<Core.DomainObjects.IShow,Core.DomainObjects.IListenedShow>)Container.DataItem)).Key.ShowDate.Value.ToShortDateString() %>'
                                     />
                                    
                                    <%--<asp:RadioButton ID="rdoNeedToListen" runat="server" AutoPostBack="true" GroupName="Status"
                                        Checked='<%# (((KeyValuePair<Core.DomainObjects.IShow,Core.DomainObjects.IListenedShow>)Container.DataItem)).Value != null ? (((KeyValuePair<Core.DomainObjects.IShow,Core.DomainObjects.IListenedShow>)Container.DataItem)).Value.Status == (int)Core.Services.ListenedStatus.NeedToListen ? true : false : false %>'
                                        ToolTip='<%# (((KeyValuePair<Core.DomainObjects.IShow,Core.DomainObjects.IListenedShow>)Container.DataItem)).Key.ShowDate.Value.ToShortDateString() %>' />--%>
                                </td>
                                <td>
                                    <b>
                                        <%# (((KeyValuePair<Core.DomainObjects.IShow,Core.DomainObjects.IListenedShow>)Container.DataItem)).Key.GetShowName() %></b>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="5">
                                    <hr class="horizontalRule1" />
                                </td>
                            </tr>
                        </ItemTemplate>
                        <FooterTemplate>
                            <tr>
                                <td>
                                    Never Heard --
                                </td>
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
                            </table></FooterTemplate>
                    </asp:Repeater>
                </div>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="rptAdder" EventName="ItemCreated" />
            </Triggers>
        </asp:UpdatePanel>
        <asp:PlaceHolder ID="phSaveButton2" runat="server" Visible="false">
            <asp:Button ID="btnSave2" runat="server" CssClass="buttonSave" Text="SAVE" OnClick="SaveAll" />
        </asp:PlaceHolder>
        <%--<br />
        <br />
        <p style="font-size: 1.2em; font-weight: 500;">
            <b>When you change a status it automatically saves!</b></p>--%>
    </div>
    <br />
    <br />
    <br />
</asp:Content>
