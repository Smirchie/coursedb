<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Members.aspx.cs" Inherits="coursedb.Members" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    
    <main>
        <asp:Panel id ="MemberPanel"
                   runat ="server" HorizontalAlign="Left"/>
        <asp:Button ID="InsertButton" Text ="Добавить" runat="server" OnClick ="InsertMember" />
        <asp:GridView id="MemberGridView"
                      runat ="server" OnRowCommand="MemberRowCommand" AutoGenerateColumns="False" DataKeyNames="Идентификатор_Члена_Партии" DataSourceID="MemberDataSource" CellPadding="4" ForeColor="#333333" GridLines="None" AllowPaging="True" AllowSorting="True" CellSpacing="2" Font-Names="Arial" HorizontalAlign="Left" ShowHeaderWhenEmpty="True" Width="1000px">
            <AlternatingRowStyle BackColor="White" ForeColor="#284775" Wrap="True" />
            <Columns>
                <asp:BoundField DataField="Идентификатор_Члена_Партии" HeaderText="ID" ReadOnly="True" SortExpression="Идентификатор_Члена_Партии" />
                <asp:BoundField DataField="Фамилия" HeaderText="Фамилия" SortExpression="Фамилия" />
                <asp:BoundField DataField="Имя" HeaderText="Имя" SortExpression="Имя" />
                <asp:BoundField DataField="Отчество" HeaderText="Отчество" SortExpression="Отчество" />
                <asp:CheckBoxField DataField="Пол" HeaderText="Пол" SortExpression="Пол" />
                <asp:BoundField DataField="Дата_Рождения" HeaderText="Дата Рождения" SortExpression="Дата_Рождения" DataFormatString="{0:d}" />
                <asp:BoundField DataField="Телефон" HeaderText="Телефон" SortExpression="Телефон" />
                <asp:BoundField DataField="Электронная_Почта" HeaderText="E-mail" SortExpression="Электронная_Почта" />
                <asp:ButtonField CommandName="ShowEvents" Text="События" />
                <asp:ButtonField ButtonType="Button" CommandName ="EditRow" Text="Изменить" />
                <asp:ButtonField ButtonType="Button" CommandName ="DeleteRow" Text="Удалить" />
            </Columns>
            <EditRowStyle BackColor="#999999" BorderColor="White" BorderWidth="0px" />
            <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
            <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
            <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
            <RowStyle BackColor="#F7F6F3" ForeColor="#333333" BorderColor="White" BorderWidth="0px" />
            <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
            <SortedAscendingCellStyle BackColor="#E9E7E2" />
            <SortedAscendingHeaderStyle BackColor="#506C8C" />
            <SortedDescendingCellStyle BackColor="#FFFDF8" />
            <SortedDescendingHeaderStyle BackColor="#6F8DAE" />

</asp:GridView>
        
        <asp:ListBox ID="EventList" runat="server"></asp:ListBox>
<asp:SqlDataSource ID="MemberDataSource" runat="server" ConnectionString="<%$ ConnectionStrings:Pol_Party_courseConnectionString %>" SelectCommand="SELECT [Идентификатор_Члена_Партии], [Фамилия], [Имя], [Отчество], [Пол], [Дата_Рождения], [Телефон], [Электронная_Почта] FROM [Член_Партии] ORDER BY [Идентификатор_Члена_Партии]"></asp:SqlDataSource>
    </main>
</asp:Content>
