using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace coursedb
{
    public partial class Appeals : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                foreach (GridViewRow row in AppealGridView.Rows)
                {
                    CheckBox cb = row.Cells[5].Controls[0] as CheckBox;
                    if (cb.Checked)
                    {
                        row.Cells[5].Controls.Clear();
                        row.Cells[5].Text = "Ж";
                    }
                    else
                    {
                        row.Cells[5].Controls.Clear();
                        row.Cells[5].Text = "М";
                    }
                }
            }
            catch
            {
            }
        }

        protected void AppealRowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "DeleteRow")
            {
                Util.DeleteFromTable(Util.GetIdFromGridView(e, AppealGridView), "Appeal");
                AppealGridView.DataBind();
                foreach (GridViewRow row in AppealGridView.Rows)
                {
                    CheckBox cb = row.Cells[5].Controls[0] as CheckBox;
                    if (cb.Checked)
                    {
                        row.Cells[5].Controls.Clear();
                        row.Cells[5].Text = "Ж";
                    }
                    else
                    {
                        row.Cells[5].Controls.Clear();
                        row.Cells[5].Text = "М";
                    }
                }
            }
            else if (e.CommandName == "EditRow")
            {
                Edit.tableId = 4;
                object[] values = Util.GetValuesFromRow($"Обращение WHERE Идентификатор_Обращения = {Util.GetIdFromGridView(e, AppealGridView)}", "*");
                for (int i = 0; i < 9; i++)
                {
                    Edit.values[i] = values[i];
                }
                Response.Redirect("~/Edit.aspx");
            }
            else if (e.CommandName == "ViewText")
            {
                string appealText = Util.GetValuesFromTable($"Обращение WHERE Идентификатор_Обращения = {Util.GetIdFromGridView(e, AppealGridView)}", "Текст")[0];
                AppealTextBox.Text = appealText;
            }
        }

        protected void InsertDept(object sender, EventArgs e)
        {
            Edit.tableId = 4;
            Response.Redirect("~/Edit.aspx");
        }
    }
}