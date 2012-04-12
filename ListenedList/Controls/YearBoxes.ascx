﻿<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="YearBoxes.ascx.cs" Inherits="ListenedList.Controls.YearBoxes" %>
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
                <asp:Button Style="padding-left:0px;" runat="server" Width="77px" BackColor='<%# GetStatus((int)Eval("Status")) %>'
                    Text='<%# ((DateTime)Eval("ShowDate")).ToShortDateString() %>' ToolTip='<%# Eval("ShowName") %>'></asp:Button>
            </td>
        </ItemTemplate>
        <FooterTemplate>
            </tr> </table></FooterTemplate>
    </asp:Repeater>
</div>
