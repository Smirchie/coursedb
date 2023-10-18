using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace coursedb
{
    public partial class Projects : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                foreach (GridViewRow row in ProjectGridView.Rows)
                {
                    row.Cells[4].Text = Util.GetValuesFromTable($"Член_Партии WHERE Идентификатор_Члена_Партии = {row.Cells[4].Text}", "Идентификатор_Члена_Партии, Фамилия")[0];
                }
            }
            catch
            {
            }
        }

        protected void ProjectRowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                foreach (GridViewRow row in ProjectGridView.Rows)
                {
                    row.Cells[4].Text = Util.GetValuesFromTable($"Член_Партии WHERE Идентификатор_Члена_Партии = {row.Cells[4].Text}", "Идентификатор_Члена_Партии, Фамилия")[0];
                }
            }
            catch
            {
            }
            if (e.CommandName == "DeleteRow")
            {
                Util.DeleteFromTable(Util.GetIdFromGridView(e, ProjectGridView), "Project");
                ProjectGridView.DataBind();
            }
            else if (e.CommandName == "EditRow")
            {
                Edit.tableId = 2;
                object[] values = Util.GetValuesFromRow($"Проект WHERE Идентификатор_Проекта = {Util.GetIdFromGridView(e, ProjectGridView)}", "*");
                for (int i = 0; i < 5; i++)
                {
                    Edit.values[i] = values[i];
                }
                Response.Redirect("~/Edit.aspx");
            }
        }

        protected void InsertProject(object sender, EventArgs e)
        {
            Edit.values = new object[10];
            Edit.tableId = 2;
            Response.Redirect("~/Edit.aspx");
        }

        protected void ProjectGridView_DataBound(object sender, EventArgs e)
        {
            try
            {
                foreach (GridViewRow row in ProjectGridView.Rows)
                {
                    row.Cells[4].Text = Util.GetValuesFromTable($"Член_Партии WHERE Идентификатор_Члена_Партии = {row.Cells[4].Text}", "Идентификатор_Члена_Партии, Фамилия")[0];
                }
            }
            catch
            {
            }
        }
    }
}