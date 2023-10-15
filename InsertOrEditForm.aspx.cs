using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace coursedb
{
    public partial class InsertOrEditForm : System.Web.UI.Page
    {
        public static int tableId = -1;
        public static object[] values;

        protected void Page_Load(object sender, EventArgs e)
        {
            EditPanel.Controls.Clear();
            switch (tableId)
            {
                case 0:
                    {
                        AddMemberElems();
                        break;
                    }
                case 1:
                    {
                        AddEventElems();
                        break;
                    }
                case 2:
                    {
                        AddProjectElems();
                        break;
                    }
                case 3:
                    {
                        AddDeptElems();
                        break;
                    }
                case 4:
                    {
                        AddAppealElems();
                        break;
                    }
            }
        }

        private void AddAppealElems()
        {
            NewDropDownList("DeptId", Util.GetValuesFromTable("Отделение", "Идентификатор_Отделения"));
            NewTextBox("LName");
            NewTextBox("FName");
            NewTextBox("MName");
            NewDropDownList("Sex", new string[] { "Мужской", "Женский" });
            NewTextBox("Phone");
            NewTextBox("E-mail");
            NewTextBox("AppealText");
            NewApplyButton();
            NewCancelButton();
        }

        private void AddDeptElems()
        {
            NewTextBox("RegionId");
            NewTextBox("Address");
            NewTextBox("Site");
            NewTextBox("Phone");
            NewDropDownList("Head", Util.GetValuesFromTable("Член_Партии", "Идентификатор_Члена_Партии, Фамилия"));
            NewApplyButton();
            NewCancelButton();
        }

        private void AddProjectElems()
        {
            NewTextBox("Name");
            NewTextBox("Desc");
            NewTextBox("Finance");
            NewDropDownList("Members", Util.GetValuesFromTable("Член_Партии", "Идентификатор_Члена_Партии, Фамилия"));
            NewApplyButton();
            NewCancelButton();
        }

        private void AddMemberElems()
        {
            NewTextBox("LName");
            NewTextBox("FName");
            NewTextBox("MName");
            NewDropDownList("Sex", new string[] { "Мужской", "Женский" });
            NewCalendar("Birthday");
            NewTextBox("Phone");
            NewTextBox("Email");
            NewApplyButton();
            NewCancelButton();
        }

        private void AddEventElems()
        {
            NewCalendar("Date");
            NewTextBox("Desc");
            NewCheckBoxList("Members", Util.GetValuesFromTable("Член_Партии", "Идентификатор_Члена_Партии, Фамилия"));
            NewApplyButton();
            NewCancelButton();
        }

        private void RedirectBack(object sender, EventArgs e)
        {
            values = null;
            switch (tableId)
            {
                case 0:
                    {
                        Response.Redirect("~/Members.aspx");
                        break;
                    }
                case 1:
                    {
                        Response.Redirect("~/Events.aspx");
                        break;
                    }
                case 2:
                    {
                        Response.Redirect("~/Projects.aspx");
                        break;
                    }
                case 3:
                    {
                        Response.Redirect("~/Departments.aspx");
                        break;
                    }
                case 4:
                    {
                        Response.Redirect("~/Appeals.aspx");
                        break;
                    }
            }
        }

        protected void Apply(object sender, EventArgs e)
        {
            List<int> memberIdLst = new List<int>();
            List<object> lst = new List<object>();
            if (values != null)
            {
                lst.Add(values[0]);
            }
            else
            {
                lst.Add(-1);
            }
            foreach (Control ctrl in EditPanel.Controls)
            {
                if (ctrl is TextBox)
                {
                    lst.Add((ctrl as TextBox).Text);
                    DebugLabel.Text += (ctrl as TextBox).Text;
                }
                else if (ctrl is Calendar)
                {
                    lst.Add((ctrl as Calendar).SelectedDate);
                    DebugLabel.Text += (ctrl as Calendar).SelectedDate.ToShortDateString();
                }
                else if (ctrl is CheckBoxList)
                {
                    foreach (ListItem item in (ctrl as CheckBoxList).Items)
                    {
                        if (item.Selected)
                        {
                            memberIdLst.Add(Util.GetIDFromString(item.Text));
                            DebugLabel.Text += (Util.GetIDFromString(item.Text));
                        }
                    }
                }
                else if (ctrl is DropDownList)
                {
                    DebugLabel.Text += (ctrl as DropDownList).SelectedValue;
                    switch ((ctrl as DropDownList).SelectedValue)
                    {
                        case "Мужской":
                            {
                                lst.Add(false);
                                break;
                            }
                        case "Женский":
                            {
                                lst.Add(true);
                                break;
                            }
                        default:
                            {
                                if (tableId == 4)
                                {
                                    lst.Add((ctrl as DropDownList).SelectedValue);
                                }
                                else
                                {
                                    lst.Add(Util.GetIDFromString((ctrl as DropDownList).SelectedValue));
                                }
                                break;
                            }
                    }
                }
            }
            try
            {
                switch (tableId)
                {
                    case 0:
                        {
                            Util.InsertOrUpdateMember(lst.ToArray());
                            break;
                        }
                    case 1:
                        {
                            Util.InsertOrUpdateEvent(lst.ToArray());
                            foreach (int memberId in memberIdLst)
                            {
                                if (values is null)
                                {
                                    Util.LinkEventToMember(memberId, Util.GetLastID("Событие", "Идентификатор_События"));
                                }
                                else
                                {
                                    Util.LinkEventToMember(memberId, Int32.Parse((string)values[0]));
                                }
                            }
                            break;
                        }
                    case 2:
                        {
                            Util.InsertOrUpdateProject(lst.ToArray());
                            break;
                        }
                    case 3:
                        {
                            Util.InsertOrUpdateDept(lst.ToArray());
                            break;
                        }
                    case 4:
                        {
                            Util.InsertOrUpdateAppeal(lst.ToArray());
                            break;
                        }
                }
                RedirectBack(sender, e);
            }
            catch
            {
            }
        }

        private void NewCheckBoxList(string id, string[] items)
        {
            CheckBoxList cbl = new CheckBoxList() { ID = id };
            foreach (string item in items)
            {
                cbl.Items.Add(item);
            }
            EditPanel.Controls.Add(cbl);
        }

        private void NewTextBox(string id)
        {
            TextBox tb = new TextBox() { ID = id };
            EditPanel.Controls.Add(tb);
        }

        private void NewDropDownList(string id, string[] items)
        {
            DropDownList lb = new DropDownList() { ID = id };
            foreach (string item in items)
            {
                lb.Items.Add(item);
            }
            EditPanel.Controls.Add(lb);
        }

        private void NewCalendar(string id)
        {
            Calendar cld = new Calendar() { ID = id };
            cld.SelectionMode = CalendarSelectionMode.Day;
            cld.SelectedDate = DateTime.Today;
            EditPanel.Controls.Add(cld);
        }

        private void NewApplyButton()
        {
            Button btn = new Button() { ID = "ApplyBtn", Text = "Сохранить" };
            btn.Command += Apply;
            EditPanel.Controls.Add(btn);
        }

        private void NewCancelButton()
        {
            Button btn = new Button() { ID = "CancelBtn", Text = "Отменить" };
            btn.Command += RedirectBack;
            EditPanel.Controls.Add(btn);
        }
    }
}