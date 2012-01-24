<%--<%@ Register TagPrefix="uc" Namespace="ListenedList.Controls" Assembly="ListenedList.Controls" %>--%>
<%@ Register TagPrefix="uc" TagName="YearBox" Src="~/Controls/YearBoxes.ascx" %>

<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeBehind="Default.aspx.cs" Inherits="ListenedList._Default" %>

<asp:Content ContentPlaceHolderID="HeadContent" runat="server">
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.5.2/jquery.min.js"></script>
    <script type="text/javascript" src="/Scripts/jquery-impromptu.3.2.min.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {



            //If any button on the page is clicked    
            var input = $(":input").click(function (event) {

                var showDate = $(this).val();

                var status = $.prompt('What is the listening status for this show?',
                                        { buttons: { Finished: 2, InProgress: 1, Cancel: 0 }, 
                                          focus: 1,
                                          submit: function(x, y, z)
                                          {


                                              //grab the user id from the hidden element
                                              var userId = $('#<%= hdnUserId.ClientID %>').val();
                                              //grab the showdate from the button that was clicked
                                              

                                              //Send user id and show date to the handler to process the update
                                              $.getJSON("Handlers/ShowHandler.ashx", { s: showDate, u: userId, st: x }, function (data) {

                                                  var items = data.records

                                              });

                                           
                                          }
                                        });
                

                

                event.preventDefault();
            });


        });
    
    </script>
</asp:Content>
<asp:Content ID="cntMain" runat="server" ContentPlaceHolderID="MainContent">
    <%--<asp:PlaceHolder ID="phMain" runat="server"></asp:PlaceHolder>--%>
    <%--<uc:YearBox ID="yearBox97" runat="server" Year="1992" />--%>
    <%--<uc:ShowTextBox runat="server" id="ltxtLabelTextBox" />--%>
    <uc:YearBox ID="yearBox83" runat="server" Year="1983" />
    <br />
    <uc:YearBox ID="yearBox1" runat="server" Year="1984" />
    <br />
    <uc:YearBox ID="yearBox2" runat="server" Year="1985" />
    <br />
    <uc:YearBox ID="yearBox3" runat="server" Year="1986" />
    <br />
    <uc:YearBox ID="yearBox4" runat="server" Year="1987" />
    <br />
    <uc:YearBox ID="yearBox5" runat="server" Year="1988" />
    <br />
    <uc:YearBox ID="yearBox6" runat="server" Year="1989" />
    <br />
    <uc:YearBox ID="yearBox7" runat="server" Year="1990" />
    <br />
    <uc:YearBox ID="yearBox8" runat="server" Year="1991" />
    <br />
    <uc:YearBox ID="yearBox9" runat="server" Year="1992" />
    <br />
    <uc:YearBox ID="yearBox10" runat="server" Year="1993" />
    <br />
    <uc:YearBox ID="yearBox11" runat="server" Year="1994" />
    <br />
    <uc:YearBox ID="yearBox12" runat="server" Year="1995" />
    <br />
    <uc:YearBox ID="yearBox13" runat="server" Year="1996" />
    <br />
    <uc:YearBox ID="yearBox14" runat="server" Year="1997" />
    <br />
    <uc:YearBox ID="yearBox15" runat="server" Year="1998" />
    <br />
    <uc:YearBox ID="yearBox16" runat="server" Year="1999" />
    <br />
    <uc:YearBox ID="yearBox17" runat="server" Year="2000" />
    <br />
    <uc:YearBox ID="yearBox18" runat="server" Year="2002" />
    <br />
    <uc:YearBox ID="yearBox19" runat="server" Year="2003" />
    <br />
    <uc:YearBox ID="yearBox20" runat="server" Year="2004" />
    <br />
    <uc:YearBox ID="yearBox21" runat="server" Year="2009" />
    <br />
    <uc:YearBox ID="yearBox22" runat="server" Year="2010" />
    <br />
    <uc:YearBox ID="yearBox23" runat="server" Year="2011" />
    <br />
    <asp:HiddenField ID="hdnUserId" runat="server" Visible="true" />
</asp:Content>
