using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace coursedb
{
    public partial class Departments : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void DeptRowCommand(object sender, GridViewCommandEventArgs e)
        {
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
            Edit.tableId = 3;
            Response.Redirect("~/Edit.aspx");
        }
    }
}