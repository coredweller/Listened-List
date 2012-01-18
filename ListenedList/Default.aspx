<%--<%@ Register TagPrefix="uc" Namespace="ListenedList.Controls" Assembly="ListenedList.Controls" %>--%>
<%@ Register TagPrefix="uc" TagName="YearBox" Src="~/Controls/YearBoxes.ascx" %>
<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeBehind="Default.aspx.cs" Inherits="ListenedList._Default" %>

    <asp:Content ContentPlaceHolderID="HeadContent" runat="server">
    
    <%--LEFT OFF HERE adding jquery to make the boxes work--%>
    
    
    </asp:Content>


<asp:Content ID="cntMain" runat="server" ContentPlaceHolderID="MainContent">
<%--<asp:PlaceHolder ID="phMain" runat="server"></asp:PlaceHolder>--%>
<%--<uc:YearBox ID="yearBox97" runat="server" Year="1992" />--%>

						<%--<uc:ShowTextBox runat="server" id="ltxtLabelTextBox" />--%>


                        <uc:YearBox ID="yearBox83" runat="server" Year="1983" /><br />
                        <uc:YearBox ID="yearBox1" runat="server" Year="1984" /><br />
                        <uc:YearBox ID="yearBox2" runat="server" Year="1985" /><br />
                        <uc:YearBox ID="yearBox3" runat="server" Year="1986" /><br />
                        <uc:YearBox ID="yearBox4" runat="server" Year="1987" /><br />
                        <uc:YearBox ID="yearBox5" runat="server" Year="1988" /><br />
                        <uc:YearBox ID="yearBox6" runat="server" Year="1989" /><br />
                        <uc:YearBox ID="yearBox7" runat="server" Year="1990" /><br />
                        <uc:YearBox ID="yearBox8" runat="server" Year="1991" /><br />
                        <uc:YearBox ID="yearBox9" runat="server" Year="1992" /><br />
                        <uc:YearBox ID="yearBox10" runat="server" Year="1993" /><br />
                        <uc:YearBox ID="yearBox11" runat="server" Year="1994" /><br />
                        <uc:YearBox ID="yearBox12" runat="server" Year="1995" /><br />
                        <uc:YearBox ID="yearBox13" runat="server" Year="1996" /><br />
                        <uc:YearBox ID="yearBox14" runat="server" Year="1997" /><br />
                        <uc:YearBox ID="yearBox15" runat="server" Year="1998" /><br />
                        <uc:YearBox ID="yearBox16" runat="server" Year="1999" /><br />
                        <uc:YearBox ID="yearBox17" runat="server" Year="2000" /><br />
                        <uc:YearBox ID="yearBox18" runat="server" Year="2002" /><br />
                        <uc:YearBox ID="yearBox19" runat="server" Year="2003" /><br />
                        <uc:YearBox ID="yearBox20" runat="server" Year="2004" /><br />
                        <uc:YearBox ID="yearBox21" runat="server" Year="2009" /><br />
                        <uc:YearBox ID="yearBox22" runat="server" Year="2010" /><br />
                        <uc:YearBox ID="yearBox23" runat="server" Year="2011" /><br />
</asp:Content>