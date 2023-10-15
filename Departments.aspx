<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Departments.aspx.cs" Inherits="coursedb.Departments" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <main>
    </main>

    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataKeyNames="Идентификатор_Отделения" DataSourceID="DeptDataSource">
        <Columns>
            <asp:BoundField DataField="Идентификатор_Отделения" HeaderText="Идентификатор_Отделения" ReadOnly="True" SortExpression="Идентификатор_Отделения" />
            <asp:BoundField DataField="Номер_Региона" HeaderText="Номер_Региона" SortExpression="Номер_Региона" />
            <asp:BoundField DataField="Адрес" HeaderText="Адрес" SortExpression="Адрес" />
            <asp:BoundField DataField="Сайт" HeaderText="Сайт" SortExpression="Сайт" />
            <asp:BoundField DataField="Телефон" HeaderText="Телефон" SortExpression="Телефон" />
        </Columns>
    </asp:GridView>
    <asp:SqlDataSource ID="DeptDataSource" runat="server" ConnectionString="<%$ ConnectionStrings:Pol_Party_courseConnectionString %>" SelectCommand="SELECT * FROM [Отделение] ORDER BY [Идентификатор_Отделения]"></asp:SqlDataSource>

</asp:Content>
