<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Departments.aspx.cs" Inherits="coursedb.Departments" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <main>
    </main>

    <asp:GridView ID="DeptGridView" runat="server" AutoGenerateColumns="False" DataKeyNames="Идентификатор_Отделения" DataSourceID="DeptDataSource" AllowPaging="True" AllowSorting="True" CellPadding="4" ForeColor="#333333" GridLines="None" OnRowCommand="DeptRowCommand" ShowHeaderWhenEmpty ="True">
        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
        <Columns>
            <asp:BoundField DataField="Идентификатор_Отделения" HeaderText="Идентификатор_Отделения" ReadOnly="True" SortExpression="Идентификатор_Отделения" />
            <asp:BoundField DataField="Номер_Региона" HeaderText="Номер_Региона" SortExpression="Номер_Региона" />
            <asp:BoundField DataField="Адрес" HeaderText="Адрес" SortExpression="Адрес" />
            <asp:BoundField DataField="Сайт" HeaderText="Сайт" SortExpression="Сайт" />
            <asp:BoundField DataField="Телефон" HeaderText="Телефон" SortExpression="Телефон" />
            <asp:BoundField DataField="Идентификатор_Члена_Партии" HeaderText="Идентификатор_Члена_Партии" SortExpression="Идентификатор_Члена_Партии" />
            <asp:ButtonField ButtonType="Button" CommandName="EditRow" Text="Изменить" />
            <asp:ButtonField ButtonType="Button" CommandName="DeleteRow" Text="Удалить" />
        </Columns>
        <EditRowStyle BackColor="#999999" />
        <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
        <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
        <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
        <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
        <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
        <SortedAscendingCellStyle BackColor="#E9E7E2" />
        <SortedAscendingHeaderStyle BackColor="#506C8C" />
        <SortedDescendingCellStyle BackColor="#FFFDF8" />
        <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
    </asp:GridView>
     <asp:Button ID="InsertButton" Text ="Добавить" runat="server" OnClick ="InsertDept"/>
    <asp:SqlDataSource ID="DeptDataSource" runat="server" ConnectionString="<%$ ConnectionStrings:Pol_Party_courseConnectionString %>" SelectCommand="SELECT * FROM [Отделение] ORDER BY [Идентификатор_Отделения]"></asp:SqlDataSource>

</asp:Content>
