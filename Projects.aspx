<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Projects.aspx.cs" Inherits="coursedb.Projects" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <main>
    <asp:Button ID="InsertButton" Text ="Добавить" runat="server" OnClick ="InsertProject"/>
        <asp:Panel ID ="EventPanel" runat ="server">
    <asp:GridView ID="ProjectGridView" OnRowCommand="ProjectRowCommand" Width ="1000px" runat="server" AutoGenerateColumns="False" CellPadding="4" DataKeyNames="Идентификатор_Проекта" DataSourceID="ProjectsDataSource" ForeColor="#333333" GridLines="None"  ShowHeaderWhenEmpty ="True" AllowPaging="True" AllowSorting="True">
        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
        <Columns>
            <asp:BoundField DataField="Идентификатор_Проекта" HeaderText="ID" ReadOnly="True" SortExpression="Идентификатор_Проекта" />
            <asp:BoundField DataField="Название" HeaderText="Название" SortExpression="Название" />
            <asp:BoundField DataField="Описание" HeaderText="Описание" SortExpression="Описание" />
            <asp:BoundField DataField="Финансирование" HeaderText="Финансирование" SortExpression="Финансирование" />
            <asp:BoundField DataField="Идентификатор_Члена_Партии" ReadOnly="True" SortExpression="Идентификатор_Члена_Партии" HeaderText="Руководитель" />
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
            </asp:Panel>
    <asp:SqlDataSource ID="ProjectsDataSource" runat="server" ConnectionString="<%$ ConnectionStrings:Pol_Party_courseConnectionString %>" SelectCommand="SELECT * FROM [Проект] ORDER BY [Идентификатор_Проекта]"></asp:SqlDataSource>
        </main>
</asp:Content>