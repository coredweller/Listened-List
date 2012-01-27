<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CreateUser.aspx.cs" Inherits="ListenedList.CreateUser" MasterPageFile="~/Masters/Site.Master" %>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">
    <asp:CreateUserWizard runat="server" OnContinueButtonClick="createControl_ContinueButtonClick" ID="createControl" OnCreatedUser="createControl_CreatedUser">
    </asp:CreateUserWizard>
    <br />
     
    
    *It may take a minute to process your request.  
    
    <br /><br />Please do not press the Create User button more than once.
</asp:Content>
