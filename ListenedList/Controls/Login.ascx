<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Login.ascx.cs" Inherits="ListenedList.Controls.Login" %>
<%@ Import Namespace="Microsoft.AspNet.FriendlyUrls" %>
<br />
<a href="<%: FriendlyUrl.Href("~/CreateUser") %>" id="lnkCreateUser">New User?</a>&nbsp;&nbsp;OR&nbsp;&nbsp;<a
    href="<%: FriendlyUrl.Href("~/Forgot") %>" id="lnkForgot">Forgot Password?</a>
<br />
<br />
<br />
<div>
    <asp:LoginView ID="LoginView1" runat="server">
        <AnonymousTemplate>
            <asp:Login runat="server" ID="loginControl" OnLoggedIn="loginControl_LoggedIn">
                <LoginButtonStyle CssClass="normalButton" />
            </asp:Login>
        </AnonymousTemplate>
        <RoleGroups>
            <asp:RoleGroup Roles="Registered">
                <ContentTemplate>
                    Welcome back!
                </ContentTemplate>
            </asp:RoleGroup>
            <asp:RoleGroup Roles="Administrators">
                <ContentTemplate>
                    Welcome Administrator
                </ContentTemplate>
            </asp:RoleGroup>
        </RoleGroups>
        <LoggedInTemplate>
            You are logged in!! But, wait, you are not a member of any roles.
        </LoggedInTemplate>
    </asp:LoginView>
</div>
