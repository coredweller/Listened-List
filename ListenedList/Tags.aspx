<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Tags.aspx.cs" Inherits="ListenedList.Tags"
    MasterPageFile="~/Masters/Genius.Master" %>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">
    <br />
    <br />
    <div style="padding-left: 100px;">
        <div style="font-size: 2em; font-weight: 700;">
            Tags
        </div>
        <br />
        <br />
        <br />
        <p>
            Create New Tag (30 letters max):
            <asp:TextBox ID="txtNewTagName" runat="server" Width="150px"></asp:TextBox>
            <asp:Button ID="btnCreateTag" runat="server" Text="Create Tag" CssClass="normalButton" OnClick="btnCreateTag_Click" />
        </p>
        <br />
        <br />
        <asp:PlaceHolder ID="phEditTag" runat="server" Visible="false">
            <div>
                Edit Tag Name:
                <asp:TextBox ID="txtTagName" runat="server"></asp:TextBox>
                <br />
                Color:
                <asp:DropDownList ID="ddlColor" runat="server" Width="200px">
                </asp:DropDownList>
                <asp:Button ID="btnSaveTagName" runat="server" OnClick="btnSaveTagName_Click" Text="Save" />
            </div>
        </asp:PlaceHolder>
        <br />
        <asp:LinkButton ID="lnkSeeAll" runat="server" Text="See All Tags" OnClick="lnkSeeAll_Click"></asp:LinkButton>
        <br />
        <br />
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
</asp:Content>
