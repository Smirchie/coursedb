<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Appeals.aspx.cs" Inherits="coursedb.Appeals" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <main>
        
    <asp:Button ID="InsertButton" runat="server" OnClick="InsertDept" Text="Добавить" />

    <asp:GridView ID="AppealGridView" runat="server" Width="800px" HorizontalAlign ="Left" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" CellPadding="4" DataKeyNames="Идентификатор_Обращения" DataSourceID="AppealDataSource" ForeColor="#333333" GridLines="None" OnRowCommand="AppealRowCommand" ShowHeaderWhenEmpty="True" OnDataBound="AppealGridView_DataBound">
        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
        <Columns>
            <asp:BoundField DataField="Идентификатор_Обращения" HeaderText="Идентификатор" InsertVisible="False" ReadOnly="True" SortExpression="Идентификатор_Обращения" />
            <asp:BoundField DataField="Идентификатор_Отделения" HeaderText="Идентификатор Отделения" SortExpression="Идентификатор_Отделения" />
            <asp:BoundField DataField="Фамилия" HeaderText="Фамилия" SortExpression="Фамилия" />
            <asp:BoundField DataField="Имя" HeaderText="Имя" SortExpression="Имя" />
            <asp:BoundField DataField="Отчество" HeaderText="Отчество" SortExpression="Отчество" />
            <asp:CheckBoxField DataField="Пол" HeaderText="Пол" SortExpression="Пол" />
            <asp:BoundField DataField="Телефон" HeaderText="Телефон" SortExpression="Телефон" />
            <asp:BoundField DataField="Электронная_Почта" HeaderText="E-mail" SortExpression="Электронная_Почта" />
            <asp:ButtonField CommandName="ViewText" Text="Текст" />
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
    <asp:TextBox TextMode="MultiLine" runat ="server" id="AppealTextBox" Width="200px" ReadOnly="true" Rows ="15"></asp:TextBox>
        </main>
    <asp:SqlDataSource ID="AppealDataSource" runat="server" ConnectionString="<%$ ConnectionStrings:Pol_Party_courseConnectionString %>" SelectCommand="SELECT * FROM [Обращение] ORDER BY [Идентификатор_Обращения]"></asp:SqlDataSource>

        
</asp:Content>