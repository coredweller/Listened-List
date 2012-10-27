<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Settings.aspx.cs" Inherits="ListenedList.Settings"
    MasterPageFile="~/Masters/Genius.Master" %>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">
    <div style="padding-left: 100px;">
        <br />
        <br />
        <p style="font-size: 2em; font-weight: 700;">
            Settings</p>
        <br />
        <br />
        <p style="padding-left:2px;">
            User Name:
            <asp:Label ID="lblUserName" runat="server"></asp:Label>
        </p>
        <p style="padding-left:2px;">
            Public:
            <asp:CheckBox ID="chkPublic" runat="server" />
            (If you check this than others can see a READONLY version of your Main view, otherwise
            only you can see it.)
        </p>
        <table>
            <tr>
                <td>
                    Name:
                </td>
                <td>
                    <asp:TextBox ID="txtName" runat="server" Columns="50"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    Email:
                </td>
                <td>
                    <asp:TextBox ID="txtEmail" runat="server" Columns="50"></asp:TextBox>
                </td>
            </tr>
        </table>
        <br />
        <p>
            <asp:Button ID="btnSaveProfile" runat="server" OnClick="btnSaveProfile_Click" CssClass="normalButton"
                Text="Save Profile" />
        </p>
    </div>
</asp:Content>
