<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Tags.aspx.cs" Inherits="ListenedList.Tags"
    MasterPageFile="~/Masters/Genius.Master" %>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">
    <div>
        Tags
    </div>
    <div>
        Tag Name:
        <asp:TextBox ID="txtTagName" runat="server"></asp:TextBox>
        <asp:Button ID="btnSaveTagName" runat="server" OnClick="btnSaveTagName_Click" Text="Save" />
    </div>
    <div>
        <asp:Repeater ID="rptTags" runat="server" OnItemCommand="rptTags_ItemCommand">
            <ItemTemplate>
                <p>
                    <asp:LinkButton CssClass="tag" runat="server" ID="lnkTag" Text='<%# (((Core.DomainObjects.ITag)Container.DataItem)).Name %>'>
                    </asp:LinkButton>
                    <asp:LinkButton ID="lnkEdit" runat="server" Text="Edit" CommandName="EDIT" CommandArgument='<%# (((Core.DomainObjects.ITag)Container.DataItem)).Id %>'>
                    </asp:LinkButton>
                    <asp:LinkButton ID="lnkDelete" runat="server" Text="Delete" CommandName="DELETE"
                        CommandArgument='<%# (((Core.DomainObjects.ITag)Container.DataItem)).Id %>'>
                    </asp:LinkButton>
                </p>
            </ItemTemplate>
        </asp:Repeater>
    </div>
</asp:Content>
