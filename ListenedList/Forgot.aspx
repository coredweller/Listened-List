﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Forgot.aspx.cs" Inherits="ListenedList.Forgot"
    MasterPageFile="~/Masters/Wooden.Master" %>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">
    <br />
    <br />
    <div class="mainDiv">
        <a href='<%= CreateUserLink %>' id="lnkCreateUser">Create User</a>
        <br />
        <div>
            <h2>
                Forgot User Name or Password?
            </h2>
            *Put your email address in and we will send it to you.
            <br />
            Email:
            <asp:TextBox ID="txtEmail" runat="server"></asp:TextBox>
            <asp:Button ID="btnEmail" runat="server" Text="Send Email" OnClick="btnEmail_Click" />
            <br />
            <br />
        </div>
    </div>
</asp:Content>
