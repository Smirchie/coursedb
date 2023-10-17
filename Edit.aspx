<%@ Page Language="C#" AutoEventWireup="true"  MasterPageFile="~/Site.Master" CodeBehind="Edit.aspx.cs" Inherits="coursedb.Edit" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    
    <main>
       <asp:Label ID="ErrorLabel" runat="server" ForeColor ="Red" />
        <asp:Panel ID ="EditPanel" runat="server"/>
    </main>
</asp:Content>
