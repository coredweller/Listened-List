<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="TagControl.ascx.cs"
    Inherits="ListenedList.Controls.TagControl" %>
<script type="text/javascript">
    $(document).ready(function () {

        $('#<%= lnkFavoriteShowTutorial.ClientID %>').click(function (event) {
            $('#divFavLinks').show();

            event.preventDefault();
        });



        


    });
</script>
<br />
<br />
<div style="padding-left: 100px;">
    <div style="font-size: 2em; font-weight: 700;">
        Tags
    </div>
    <br />
    <br />
    <br />
    <asp:PlaceHolder ID="phPart1" runat="server" Visible='false'><span style="font-size: larger;
        font-weight: bolder; font-family: Comic Sans MS;">(Part 1: Create new Tags simply
        by giving them a name.<br />
        Create a tag and then Click Edit for Part 2.)</span> </asp:PlaceHolder>
    <p>
        Create New Tag (30 letters max):
        <asp:TextBox ID="txtNewTagName" runat="server" Width="150px"></asp:TextBox>
        <asp:Button ID="btnCreateTag" runat="server" Text="Create Tag" CssClass="normalButton"
            OnClick="btnCreateTag_Click" />
    </p>
    <br />
    <br />
    <asp:PlaceHolder ID="phEditTag" runat="server" Visible="false">
        <asp:PlaceHolder ID="phPart2" runat="server" Visible="false"><span style="font-size: larger;
            font-weight: bolder; font-family: Comic Sans MS;">(Part 2: Edit a tag by changing
            its name and/or color.
            <br />
            Change tha name and/or color and click Save for Part 3.)</span> </asp:PlaceHolder>
        <div>
            Edit Tag Name:
            <asp:TextBox ID="txtTagName" runat="server"></asp:TextBox>
            <br />
            Color:
            <asp:DropDownList ID="ddlColor" runat="server" Width="200px">
            </asp:DropDownList>
            <asp:Button ID="btnSaveTagName" runat="server" OnClick="btnSaveTagName_Click" CssClass="normalButton"
                Text="Save" />
        </div>
    </asp:PlaceHolder>
    <br />
    <asp:LinkButton ID="lnkSeeAll" runat="server" Text="See All Tags" OnClick="lnkSeeAll_Click"></asp:LinkButton>
    <br />
    <br />
    <asp:PlaceHolder ID="phPart3" runat="server" Visible="false">
        <p>
            <span style="font-size: larger;
            font-weight: bolder; font-family: Comic Sans MS;">(Part 3: Viewing Tagged shows by clicking on a Tag. Click the Favorite Show Tag to see the shows Tagged with Favorite Show for this tutorial.
            <br />
            Click the links to the shows to go to the desired Notes page. The tutorial is over.  Welcome to Phisherman's Guide!)</span>
            <br />
            <asp:LinkButton CssClass='blueTag' runat="server" ID="lnkFavoriteShowTutorial" Text='Favorite Show'>
            </asp:LinkButton>&nbsp;&nbsp;(For Tutorial Part 3 only)
            <%--<asp:LinkButton ID="lnkEdit" runat="server" Text="Edit"></asp:LinkButton>&nbsp;&nbsp;
            <asp:LinkButton ID="lnkDelete" runat="server" Text="Delete"></asp:LinkButton>--%>
        </p>
        <div id="divFavLinks" style="display: none;">
            <table>
                <tr>
                    <td>
                        <asp:HyperLink ID="HyperLink2" runat="server" Text='12/31/1995 - Madison Square Garden - New York, NY'
                            NavigateUrl='/Notes.aspx?showDate=12/31/1995'></asp:HyperLink>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:HyperLink ID="HyperLink1" runat="server" Text='05/07/1994 - The Bomb Factory - Dallas, TX'
                            NavigateUrl='/Notes.aspx?showDate=05/07/1994'></asp:HyperLink>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:HyperLink ID="lnkShow" runat="server" Text='12/30/1997 - Madison Square Garden - New York, NY'
                            NavigateUrl='/Notes.aspx?showDate=12/30/1997'></asp:HyperLink>
                    </td>
                </tr>
            </table>
            <br />
        </div>
    </asp:PlaceHolder>
    <div>
        <asp:Repeater ID="rptTags" runat="server" OnItemCommand="rptTags_ItemCommand">
            <ItemTemplate>
                <p>
                    <asp:LinkButton CssClass='<%# ((Core.DomainObjects.ITag)Container.DataItem).Color %>'
                        runat="server" CommandName="CLICK" CommandArgument='<%# ((Core.DomainObjects.ITag)Container.DataItem).Id %>'
                        ID="lnkTag" Text='<%# ((Core.DomainObjects.ITag)Container.DataItem).Name %>'>
                    </asp:LinkButton>&nbsp;&nbsp;
                    <asp:LinkButton ID="lnkEdit" runat="server" Text="Edit" CommandName="EDIT" CommandArgument='<%# (((Core.DomainObjects.ITag)Container.DataItem)).Id %>'>
                    </asp:LinkButton>&nbsp;&nbsp;
                    <asp:LinkButton ID="lnkDelete" runat="server" Text="Delete" CommandName="DELETE"
                        CommandArgument='<%# (((Core.DomainObjects.ITag)Container.DataItem)).Id %>'>
                    </asp:LinkButton>
                </p>
            </ItemTemplate>
        </asp:Repeater>
    </div>
    <asp:PlaceHolder ID="phShowList" runat="server" Visible="false">
        <div>
            <asp:Repeater ID="rptShows" runat="server">
                <HeaderTemplate>
                    <table>
                </HeaderTemplate>
                <ItemTemplate>
                    <tr>
                        <td>
                            <asp:HyperLink ID="lnkShow" runat="server" Text='<%# ( (Core.DomainObjects.IShow)Container.DataItem ).GetShowName() %>'
                                NavigateUrl='<%# "/Notes.aspx?showId=" + ((Core.DomainObjects.IShow)Container.DataItem).Id %>'></asp:HyperLink>
                        </td>
                    </tr>
                </ItemTemplate>
                <FooterTemplate>
                    </table></FooterTemplate>
            </asp:Repeater>
        </div>
    </asp:PlaceHolder>
</div>
