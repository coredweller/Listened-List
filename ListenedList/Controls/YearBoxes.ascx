﻿<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="YearBoxes.ascx.cs" Inherits="ListenedList.Controls.YearBoxes" %>
<%@ Register TagPrefix="uc" TagName="MonthBox" Src="~/Controls/MonthBoxes.ascx" %>
<%@ Import Namespace="Microsoft.AspNet.FriendlyUrls" %>
<asp:PlaceHolder ID="phYearMode" runat="server">
    <uc:MonthBox ID="yearBox" runat="server" />
</asp:PlaceHolder>
<asp:PlaceHolder ID="phMonthMode" runat="server">
    <div>
        <br />
        <br />
        <b style="font-size: large; padding-left: 4px;"><a href="<%: FriendlyUrl.Href("~/Main") %>"
            id="A1" class="lnkMonth">
            <img src="/images/minus_icon.jpg" /></a>
            <%= Year %>
        </b>
        <asp:PlaceHolder ID="phJan" runat="server" Visible="false">
            <div>
                <uc:MonthBox ID="janBox" runat="server" />
            </div>
        </asp:PlaceHolder>
        <asp:PlaceHolder ID="phFeb" runat="server" Visible="false">
            <div>
                <uc:MonthBox ID="febBox" runat="server" />
            </div>
        </asp:PlaceHolder>
        <asp:PlaceHolder ID="phMarch" runat="server" Visible="false">
            <div>
                <uc:MonthBox ID="marchBox" runat="server" />
            </div>
        </asp:PlaceHolder>
        <asp:PlaceHolder ID="phApril" runat="server" Visible="false">
            <div>
                <uc:MonthBox ID="aprilBox" runat="server" />
            </div>
        </asp:PlaceHolder>
        <asp:PlaceHolder ID="phMay" runat="server" Visible="false">
            <div>
                <uc:MonthBox ID="mayBox" runat="server" />
            </div>
        </asp:PlaceHolder>
        <asp:PlaceHolder ID="phJune" runat="server" Visible="false">
            <div>
                <uc:MonthBox ID="juneBox" runat="server" />
            </div>
        </asp:PlaceHolder>
        <asp:PlaceHolder ID="phJuly" runat="server" Visible="false">
            <div>
                <uc:MonthBox ID="julyBox" runat="server" />
            </div>
        </asp:PlaceHolder>
        <asp:PlaceHolder ID="phAug" runat="server" Visible="false">
            <div>
                <uc:MonthBox ID="augBox" runat="server" />
            </div>
        </asp:PlaceHolder>
        <asp:PlaceHolder ID="phSept" runat="server" Visible="false">
            <div>
                <uc:MonthBox ID="septBox" runat="server" />
            </div>
        </asp:PlaceHolder>
        <asp:PlaceHolder ID="phOct" runat="server" Visible="false">
            <div>
                <uc:MonthBox ID="octBox" runat="server" />
            </div>
        </asp:PlaceHolder>
        <asp:PlaceHolder ID="phNov" runat="server" Visible="false">
            <div>
                <uc:MonthBox ID="novBox" runat="server" />
            </div>
        </asp:PlaceHolder>
        <asp:PlaceHolder ID="phDec" runat="server" Visible="false">
            <div>
                <uc:MonthBox ID="decBox" runat="server" />
            </div>
        </asp:PlaceHolder>
        <br />
        <br />
    </div>
</asp:PlaceHolder>
