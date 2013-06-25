<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Login.ascx.cs" Inherits="ListenedList.Controls.Login" %>
<br />
<span style="font-size:1.5em;">
<a href='<%= CreateUserLink %>' id="lnkCreateUser">New User?</a>&nbsp;&nbsp;OR&nbsp;&nbsp;<a
    href='<%= ForgotPasswordLink %>' id="lnkForgot">Forgot Password?</a>
    </span>
<br />
<br />
<br />
<div>
    <%--<span style="margin-left: 50px; width:auto; line-height:100px;">Welcome to Phisherman's Guide where Phish
        phans come to track what shows they have listened to. </span>--%>
    <span style="float: left; font-size: 1.3em;">
        <asp:LoginView ID="LoginView1" runat="server">
            <AnonymousTemplate>
                <asp:Login runat="server" ID="loginControl" OnLoggedIn="loginControl_LoggedIn" TextBoxStyle-CssClass="sharpTextBox">
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
    </span>
</div>
