using System;
using System.Web.UI.WebControls;

namespace coursedb
{
    public partial class Departments : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                foreach (GridViewRow row in DeptGridView.Rows)
                {
                    row.Cells[5].Text = Util.GetValuesFromTable($"Член_Партии WHERE Идентификатор_Члена_Партии = {row.Cells[5].Text}", "Идентификатор_Члена_Партии, Фамилия")[0];
                }
            }
            catch
            {
            }
        }

        protected void DeptRowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                foreach (GridViewRow row in DeptGridView.Rows)
                {
                    row.Cells[5].Text = Util.GetValuesFromTable($"Член_Партии WHERE Идентификатор_Члена_Партии = {row.Cells[5].Text}", "Идентификатор_Члена_Партии, Фамилия")[0];
                }
            }
            catch
            {
            }
            if (e.CommandName == "DeleteRow")
            {
                Util.DeleteFromTable(Util.GetIdFromGridView(e, DeptGridView), "Dept");
                DeptGridView.DataBind();
            }
            else if (e.CommandName == "EditRow")
            {
                Edit.tableId = 3;
                object[] values = Util.GetValuesFromRow($"Отделение WHERE Идентификатор_Отделения = {Util.GetIdFromGridView(e, DeptGridView)}", "*");
                for (int i = 0; i < 6; i++)
                {
                    Edit.values[i] = values[i];
                }
                Response.Redirect("~/Edit.aspx");
            }
        }

        protected void InsertDept(object sender, EventArgs e)
        {
            Edit.values = new object[10];
            Edit.tableId = 3;
            Response.Redirect("~/Edit.aspx");
        }

        protected void DeptGridView_DataBound(object sender, EventArgs e)
        {
            try
            {
                foreach (GridViewRow row in DeptGridView.Rows)
                {
                    row.Cells[5].Text = Util.GetValuesFromTable($"Член_Партии WHERE Идентификатор_Члена_Партии = {row.Cells[5].Text}", "Идентификатор_Члена_Партии, Фамилия")[0];
                }
            }
            catch
            {
            }
        }
    }
}