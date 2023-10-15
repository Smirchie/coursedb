<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Events.aspx.cs" Inherits="coursedb.Events" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <main>
        <asp:Panel ID ="EventPanel" runat ="server">
            <asp:GridView ID ="EventGridView" OnRowCommand ="EventRowCommand" runat ="server" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" CellPadding="4" DataKeyNames="Идентификатор_События" DataSourceID="EventDataSource" ForeColor="#333333" GridLines="None" ShowHeaderWhenEmpty ="True">

                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                <Columns>
                    <asp:BoundField DataField="Идентификатор_События" HeaderText="ID" InsertVisible="False" ReadOnly="True" SortExpression="Идентификатор_События" />
                    <asp:BoundField DataField="Дата_Проведения" HeaderText="Время" SortExpression="Дата_Проведения" />
                    <asp:BoundField DataField="Описание" HeaderText="Описание" SortExpression="Описание" />
                    <asp:ButtonField CommandName="ShowMembers" Text="Участники" />
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
            <asp:Button ID="InsertButton" Text ="Добавить" runat="server" OnClick ="InsertEvent"/>
            <asp:ListBox id="MemberList" runat="server"></asp:ListBox>
        </asp:Panel>
        <asp:SqlDataSource ID="EventDataSource" runat="server" ConnectionString="<%$ ConnectionStrings:Pol_Party_courseConnectionString %>" SelectCommand="SELECT [Идентификатор_События], [Дата_Проведения], [Описание] FROM [Событие] ORDER BY [Идентификатор_События]"></asp:SqlDataSource>
    </main>

</asp:Content>
