using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace coursedb
{
    public partial class Events : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void InsertEvent(object sender, EventArgs e)
        {
            InsertOrEditForm.tableId = 1;
            Response.Redirect("~/InsertOrEditForm.aspx");
        }

        protected void EventRowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "DeleteRow")
            {
                Util.DeleteFromTable(Util.GetIdFromGridView(e, EventGridView), "Event");
                EventGridView.DataBind();
            }
            else if (e.CommandName == "EditRow")
            {
                InsertOrEditForm.tableId = 1;
                object[] values = Util.GetValuesFromRow($"Событие WHERE Идентификатор_События = {Util.GetIdFromGridView(e, EventGridView)}", "*");
                for (int i = 0; i < 3; i++)
                {
                    InsertOrEditForm.values[i] = values[i];
                }
                InsertOrEditForm.values[3] = Util.GetValuesFromTable($"[Связь Член_Партии Событие] WHERE Идентификатор_События = {Util.GetIdFromGridView(e, EventGridView)}", "Идентификатор_Члена_Партии");
                Response.Redirect("~/InsertOrEditForm.aspx");
            }
            else if (e.CommandName == "ShowMembers")
            {
                MemberList.Items.Clear();
                string[] memberIdArr = Util.GetValuesFromTable($"[Связь Член_Партии Событие] WHERE Идентификатор_События = {Util.GetIdFromGridView(e, EventGridView)}", "Идентификатор_Члена_Партии");
                foreach (string memberId in memberIdArr)
                {
                    MemberList.Items.Add(Util.GetValuesFromTable($"Член_Партии WHERE Идентификатор_Члена_Партии = {memberId}", "Идентификатор_Члена_Партии, Фамилия")[0]);
                }
            }
        }
    }
}