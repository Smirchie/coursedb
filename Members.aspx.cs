using System;
using System.Web.UI.WebControls;

namespace coursedb
{
    public partial class Members : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ProperDisplay();
        }

        protected void MemberRowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "DeleteRow")
            {
                Util.DeleteFromTable(Util.GetIdFromGridView(e, MemberGridView), "Member");
                MemberGridView.DataBind();
                EventList.Visible = false;
            }
            else if (e.CommandName == "EditRow")
            {
                Edit.tableId = 0;
                object[] values = Util.GetValuesFromRow($"Член_Партии WHERE Идентификатор_Члена_Партии = {Util.GetIdFromGridView(e, MemberGridView)}", "*");
                for (int i = 0; i < 8; i++)
                {
                    Edit.values[i] = values[i];
                }
                Response.Redirect("~/Edit.aspx");
            }
            else if (e.CommandName == "ShowEvents")
            {
                EventList.Items.Clear();
                string[] eventIdArr = Util.GetValuesFromTable($"[Связь Член_Партии Событие] WHERE Идентификатор_Члена_Партии = {Util.GetIdFromGridView(e, MemberGridView)}", "Идентификатор_События");
                foreach (string eventId in eventIdArr)
                {
                    EventList.Items.Add(Util.GetValuesFromTable($"Событие WHERE Идентификатор_События = {eventId}", "Идентификатор_События, Дата_Проведения")[0]);
                }
                if (EventList.Items.Count < 1)
                {
                    EventList.Visible = false;
                }
                else
                {
                    EventList.Visible = true;
                }
            }
        }

        protected void InsertMember(object sender, EventArgs e)
        {
            Edit.values = new object[10];
            Edit.tableId = 0;
            Response.Redirect("~/Edit.aspx");
        }

        protected void ProperDisplay()
        {
            try
            {
                foreach (GridViewRow row in MemberGridView.Rows)
                {
                    CheckBox cb = row.Cells[4].Controls[0] as CheckBox;
                    if (cb.Checked)
                    {
                        row.Cells[4].Controls.Clear();
                        row.Cells[4].Text = "Ж";
                    }
                    else
                    {
                        row.Cells[4].Controls.Clear();
                        row.Cells[4].Text = "М";
                    }
                }
            }
            catch
            {
            }
        }

        protected void MemberGridView_DataBound(object sender, EventArgs e)
        {
            ProperDisplay();
        }
    }
}