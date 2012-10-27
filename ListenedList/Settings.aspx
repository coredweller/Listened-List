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
        <div>
            <p>
                User Name:
                <asp:Label ID="lblUserName" runat="server"></asp:Label>
            </p>
            <p>
                Name:
                <asp:TextBox ID="txtName" runat="server"></asp:TextBox>
            </p>
            <p>
                Email:
                <asp:TextBox ID="txtEmail" runat="server"></asp:TextBox>
            </p>
            <p>
                Public:
                <asp:CheckBox ID="chkPublic" runat="server" />
                (If you check this than others can see a READONLY version of your Main view, otherwise
                only you can see it.)
            </p>
            <p>
                <asp:Button ID="btnSaveProfile" runat="server" OnClick="btnSaveProfile_Click" CssClass="normalButton" Text="Save Profile" />
            </p>
        </div>
    </div>
</asp:Content>
