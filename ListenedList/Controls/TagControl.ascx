<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="TagControl.ascx.cs"
    Inherits="ListenedList.Controls.TagControl" %>
<%@ Import Namespace="Microsoft.AspNet.FriendlyUrls" %>
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
<div class="mainDiv">
    <div style="font-size: 2.3em; font-weight: 800;">
        Tags
    </div>
    <br />
    <br />
    <br />
    <asp:PlaceHolder ID="phPart1" runat="server" Visible='false'><span class="tutorialTagHeader">
        Part 1: <span style="font-size: 1.4em;">Create new Tags simply by giving them a name.<br />
            --Create a tag and then Click Edit for Part 2.</span></span> </asp:PlaceHolder>
    <p style="font-size:1.5em;">
        Create New Tag (30 letters max):
        <asp:TextBox ID="txtNewTagName" runat="server" Width="150px"></asp:TextBox>
        <asp:Button ID="btnCreateTag" runat="server" Text="Create Tag" CssClass="normalButton"
            OnClick="btnCreateTag_Click" />
    </p>
    <br />
    <br />
    <asp:PlaceHolder ID="phEditTag" runat="server" Visible="false">
        <asp:PlaceHolder ID="phPart2" runat="server" Visible="false"><span class="tutorialTagHeader">
            Part 2: <span style="font-size: 1.4em;">Edit a tag by changing its name and/or color.
                <br />
                --Change tha name and/or color and click Save for Part 3.</span></span>
        </asp:PlaceHolder>
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
    <span class="tagLinks">
        <asp:LinkButton ID="lnkSeeAll" runat="server" Text="See All Tags" OnClick="lnkSeeAll_Click"></asp:LinkButton>
    </span>
    <br />
    <br />
    <asp:PlaceHolder ID="phPart3" runat="server" Visible="false">
        <p>
            <span class="tutorialTagHeader">Part 3: <span style="font-size: 1.4em;">Viewing Tagged
                shows by clicking on a Tag.
                <br />
                --Click the Favorite Show Tag to see the shows Tagged for this tutorial.
                <br />
                --Click the links to the shows to go to the desired Notes page. The tutorial is
                over. Welcome to Phisherman's Guide!</span></span>
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
                        <a id="HyperLink2" href='<%= LinkBuilder.ParentNotesLink( "12-31-1995" ) %>'>12/31/1995
                            - Madison Square Garden - New York, NY</a>
                    </td>
                </tr>
                <tr>
                    <td>
                        <a id="A1" href='<%= LinkBuilder.ParentNotesLink( "05-07-1994" ) %>'>05/07/1994 - The
                            Bomb Factory - Dallas, TX</a>
                    </td>
                </tr>
                <tr>
                    <td>
                        <a id="A2" href='<%= LinkBuilder.ParentNotesLink( "12-30-1997" ) %>'>12/30/1997 - Madison
                            Square Garden - New York, NY</a>
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
                    <span class="tagControlButtons">
                        <asp:LinkButton CssClass='<%# ((Core.DomainObjects.ITag)Container.DataItem).Color %>'
                            runat="server" CommandName="CLICK" CommandArgument='<%# ((Core.DomainObjects.ITag)Container.DataItem).Id %>'
                            ID="lnkTag" Text='<%# ((Core.DomainObjects.ITag)Container.DataItem).Name %>'>                       
                        </asp:LinkButton>&nbsp;&nbsp; </span><span class="tagLinks">
                            <asp:LinkButton ID="lnkEdit" runat="server" Text="Edit" CommandName="EDIT" CommandArgument='<%# (((Core.DomainObjects.ITag)Container.DataItem)).Id %>'>
                            </asp:LinkButton>&nbsp;&nbsp;
                            <asp:LinkButton ID="lnkDelete" runat="server" Text="Delete" CommandName="DELETE"
                                CommandArgument='<%# (((Core.DomainObjects.ITag)Container.DataItem)).Id %>'>
                            </asp:LinkButton>
                        </span>
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
                    <tr style="font-size:1.5em;">
                        <td>
                            <a id="lnkShow" href='<%# LinkBuilder.ParentNotesLink(((Core.DomainObjects.IShow)Container.DataItem).Id.ToString() ) %>'>
                                <%# ( (Core.DomainObjects.IShow)Container.DataItem ).GetShowName() %></a>
                        </td>
                    </tr>
                </ItemTemplate>
                <FooterTemplate>
                    </table></FooterTemplate>
            </asp:Repeater>
        </div>
    </asp:PlaceHolder>
</div>
