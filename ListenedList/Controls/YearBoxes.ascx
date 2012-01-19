<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="YearBoxes.ascx.cs" Inherits="ListenedList.Controls.YearBoxes" %>
<div>
    <asp:Repeater ID="rptShows" runat="server">
        <HeaderTemplate>
            <table><tr>
        </HeaderTemplate>
        <ItemTemplate>
            
                <td>
                    <asp:Button runat="server" Width="65px" BackColor="White" Text='<%# (((Data.DomainObjects.Show)Container.DataItem).ShowDate).Value.ToShortDateString() %>'></asp:Button>
                </td>
        
            
        </ItemTemplate>
        <FooterTemplate>
           </tr> </table></FooterTemplate>
    </asp:Repeater>

    <%--<table>
        <tr>
            <td>
               <asp:PlaceHolder ID="ph1" runat="server"></asp:PlaceHolder> 
            </td>
        </tr>
    </table>--%>
</div>
