<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="YearBoxes.ascx.cs" Inherits="ListenedList.Controls.YearBoxes" %>
<%@ Register TagPrefix="uc" TagName="MonthBox" Src="~/Controls/MonthBoxes.ascx" %>
<asp:PlaceHolder ID="phYearMode" runat="server">
    <uc:MonthBox ID="yearBox" runat="server" />
</asp:PlaceHolder>
<asp:PlaceHolder ID="phMonthMode" runat="server">
    <div>
        <br />
        <b style="font-size: large; padding-left: 4px;"><a href='<%= LinkBuilder.BaseMainLink() %>'
            id="A1" class="lnkMonth">
            <img src="/images/minus_icon.jpg" /></a>
            <%= Year %>
        </b>
        <div style="padding-left: 30px;">
            <asp:PlaceHolder ID="phJan" runat="server" Visible="false">
                <uc:MonthBox ID="janBox" runat="server" />
            </asp:PlaceHolder>
            <asp:PlaceHolder ID="phFeb" runat="server" Visible="false">
                <uc:MonthBox ID="febBox" runat="server" />
            </asp:PlaceHolder>
            <asp:PlaceHolder ID="phMarch" runat="server" Visible="false">
                <uc:MonthBox ID="marchBox" runat="server" />
            </asp:PlaceHolder>
            <asp:PlaceHolder ID="phApril" runat="server" Visible="false">
                <uc:MonthBox ID="aprilBox" runat="server" />
            </asp:PlaceHolder>
            <asp:PlaceHolder ID="phMay" runat="server" Visible="false">
                <uc:MonthBox ID="mayBox" runat="server" />
            </asp:PlaceHolder>
            <asp:PlaceHolder ID="phJune" runat="server" Visible="false">
                <uc:MonthBox ID="juneBox" runat="server" />
            </asp:PlaceHolder>
            <asp:PlaceHolder ID="phJuly" runat="server" Visible="false">
                <uc:MonthBox ID="julyBox" runat="server" />
            </asp:PlaceHolder>
            <asp:PlaceHolder ID="phAug" runat="server" Visible="false">
                <uc:MonthBox ID="augBox" runat="server" />
            </asp:PlaceHolder>
            <asp:PlaceHolder ID="phSept" runat="server" Visible="false">
                <uc:MonthBox ID="septBox" runat="server" />
            </asp:PlaceHolder>
            <asp:PlaceHolder ID="phOct" runat="server" Visible="false">
                <uc:MonthBox ID="octBox" runat="server" />
            </asp:PlaceHolder>
            <asp:PlaceHolder ID="phNov" runat="server" Visible="false">
                <uc:MonthBox ID="novBox" runat="server" />
            </asp:PlaceHolder>
            <asp:PlaceHolder ID="phDec" runat="server" Visible="false">
                <uc:MonthBox ID="decBox" runat="server" />
            </asp:PlaceHolder>
        </div>
        <br />
    </div>
</asp:PlaceHolder>
