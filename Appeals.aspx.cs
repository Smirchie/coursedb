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
        }

        protected void AppealRowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "DeleteRow")
            {
                Util.DeleteFromTable(Util.GetIdFromGridView(e, AppealGridView), "Appeal");
                AppealGridView.DataBind();
            }
            else if (e.CommandName == "EditRow")
            {
                InsertOrEditForm.tableId = 4;
                InsertOrEditForm.values = Util.GetRowAsArray(e, AppealGridView);
                Response.Redirect("~/InsertOrEditForm.aspx");
            }
            else if (e.CommandName == "ViewText")
            {
                string appealText = Util.GetValuesFromTable($"Обращение WHERE Идентификатор_Обращения = {Util.GetIdFromGridView(e, AppealGridView)}", "Текст")[0];
                AppealTextBox.Text = appealText;
            }
        }

        protected void InsertDept(object sender, EventArgs e)
        {
            InsertOrEditForm.tableId = 4;
            Response.Redirect("~/InsertOrEditForm.aspx");
        }
    }
}