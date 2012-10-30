<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AssignAdmin.aspx.cs" Inherits="ListenedList.Admin.AssignAdmin"
    MasterPageFile="~/Masters/Genius.Master" %>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">
    <div class="mainDiv" style="padding-top: 50px;">
        <div>
            User:
            <asp:TextBox ID="txtUserName" runat="server"></asp:TextBox>
            <asp:Button ID="btnMakeAdmin" runat="server" OnClick="btnMakeAdmin_Click" Text="Assign" />
        </div>
        <br />
        <br />
        <div>
            <h2>
                Admin List</h2>
            <br />
            <asp:Repeater ID="rptAdmins" runat="server" OnItemCommand="rptAdmins_ItemCommand">
                <HeaderTemplate>
                    <table>
                </HeaderTemplate>
                <ItemTemplate>
                    <tr>
                        <td>
                            <%# ((string)Container.DataItem) %>
                        </td>
                        <td>
                            <asp:LinkButton ID="lnkRemove" runat="server" CommandArgument='<%# ((string)Container.DataItem) %>'
                                CommandName="REMOVE" Text="Remove"></asp:LinkButton>
                        </td>
                    </tr>
                </ItemTemplate>
                <FooterTemplate>
                    </table></FooterTemplate>
            </asp:Repeater>
        </div>
    </div>
</asp:Content>
