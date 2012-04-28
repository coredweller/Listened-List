<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="YearBoxes.ascx.cs" Inherits="ListenedList.Controls.YearBoxes" %>
<div>
    <asp:Repeater ID="rptShows" runat="server">
        <HeaderTemplate>
            <table>
                <tr>
                    <td>
                        <b style="font-size:large;">
                            <%= Year %></b>
                    </td>
        </HeaderTemplate>
        <ItemTemplate>
            <td>
             <%--Width="77px"  BackColor='<%# GetStatus((int)Eval("Status")) %>' Style="padding-left:0px;"  --%>
                <asp:Button CssClass='<%# GetCssClass((int)Eval("Status")) %>' runat="server" Width="100px"
                    Text='<%# ((DateTime)Eval("ShowDate")).ToShortDateString() %>' ToolTip='<%# Eval("ShowName") %>'></asp:Button>
            </td>
        </ItemTemplate>
        <FooterTemplate>
            </tr> </table></FooterTemplate>
    </asp:Repeater>
</div>
