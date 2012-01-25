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
    <uc:YearBox ID="yearBox84" runat="server" Year="1984" />
    <br />
    <uc:YearBox ID="yearBox85" runat="server" Year="1985" />
    <br />
    <uc:YearBox ID="yearBox86" runat="server" Year="1986" />
    <br />
    <uc:YearBox ID="yearBox87" runat="server" Year="1987" />
    <br />
    <uc:YearBox ID="yearBox88" runat="server" Year="1988" />
    <br />
    <uc:YearBox ID="yearBox89" runat="server" Year="1989" />
    <br />
    <uc:YearBox ID="yearBox90" runat="server" Year="1990" />
    <br />
    <uc:YearBox ID="yearBox91" runat="server" Year="1991" />
    <br />
    <uc:YearBox ID="yearBox92" runat="server" Year="1992" />
    <br />
    <uc:YearBox ID="yearBox93" runat="server" Year="1993" />
    <br />
    <uc:YearBox ID="yearBox94" runat="server" Year="1994" />
    <br />
    <uc:YearBox ID="yearBox95" runat="server" Year="1995" />
    <br />
    <uc:YearBox ID="yearBox96" runat="server" Year="1996" />
    <br />
    <uc:YearBox ID="yearBox97" runat="server" Year="1997" />
    <br />
    <uc:YearBox ID="yearBox98" runat="server" Year="1998" />
    <br />
    <uc:YearBox ID="yearBox99" runat="server" Year="1999" />
    <br />
    <uc:YearBox ID="yearBox00" runat="server" Year="2000" />
    <br />
    <uc:YearBox ID="yearBox02" runat="server" Year="2002" />
    <br />
    <uc:YearBox ID="yearBox03" runat="server" Year="2003" />
    <br />
    <uc:YearBox ID="yearBox04" runat="server" Year="2004" />
    <br />
    <uc:YearBox ID="yearBox09" runat="server" Year="2009" />
    <br />
    <uc:YearBox ID="yearBox10" runat="server" Year="2010" />
    <br />
    <uc:YearBox ID="yearBox11" runat="server" Year="2011" />
    <br />
    <asp:HiddenField ID="hdnUserId" runat="server" Visible="true" />
</asp:Content>
