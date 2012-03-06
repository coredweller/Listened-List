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
            <asp:Button ID="btnCreateTag" runat="server" Text="Create Tag" OnClick="btnCreateTag_Click" />
        </p>
        <br />
        <br />
        <asp:PlaceHolder ID="phEditTag" runat="server" Visible="false">
            <div>
                Edit Tag Name:
                <asp:TextBox ID="txtTagName" runat="server"></asp:TextBox>
                <br />
                Color: <asp:DropDownList ID="ddlColor" runat="server" Width="200px" ></asp:DropDownList>
                <asp:Button ID="btnSaveTagName" runat="server" OnClick="btnSaveTagName_Click" Text="Save" />

            </div>
        </asp:PlaceHolder>
        <br />
        <br />
        <div>
            <asp:Repeater ID="rptTags" runat="server" OnItemCommand="rptTags_ItemCommand">
                <ItemTemplate>
                    <p>
                        <asp:LinkButton CssClass="tag" runat="server" ID="lnkTag" Text='<%# (((Core.DomainObjects.ITag)Container.DataItem)).Name %>'>
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
    </div>
</asp:Content>
