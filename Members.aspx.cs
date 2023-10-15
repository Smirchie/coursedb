using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Common.CommandTrees;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace coursedb
{
    public partial class Members : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void MemberRowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "DeleteRow")
            {
                Util.DeleteFromTable(Util.GetIdFromGridView(e, MemberGridView), "Member");
                MemberGridView.DataBind();
            }
            else if (e.CommandName == "EditRow")
            {
                InsertOrEditForm.tableId = 0;
                InsertOrEditForm.values = Util.GetRowAsArray(e, MemberGridView);
                Response.Redirect("~/InsertOrEditForm.aspx");
            }
            else if (e.CommandName == "ShowEvents")
            {
                EventList.Items.Clear();
                string[] eventIdArr = Util.GetValuesFromTable($"[Связь Член_Партии Событие] WHERE Идентификатор_Члена_Партии = {Util.GetIdFromGridView(e, MemberGridView)}", "Идентификатор_События");
                foreach (string eventId in eventIdArr)
                {
                    EventList.Items.Add(Util.GetValuesFromTable($"Событие WHERE Идентификатор_События = {eventId}", "Идентификатор_События, Дата_Проведения")[0]);
                }
            }
        }

        protected void InsertMember(object sender, EventArgs e)
        {
            InsertOrEditForm.tableId = 0;
            Response.Redirect("~/InsertOrEditForm.aspx");
        }
    }
}