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
                InsertOrEditForm.tableId = 3;
                InsertOrEditForm.values = Util.GetRowAsArray(e, DeptGridView);
                Response.Redirect("~/InsertOrEditForm.aspx");
            }
        }

        protected void InsertDept(object sender, EventArgs e)
        {
            InsertOrEditForm.tableId = 3;
            Response.Redirect("~/InsertOrEditForm.aspx");
        }
    }
}