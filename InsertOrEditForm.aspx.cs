using Microsoft.Ajax.Utilities;
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
        public static object[] values = new object[10];

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
            NewDropDownList("DeptId", Util.GetValuesFromTable("Отделение", "Идентификатор_Отделения"), values[1]);
            NewTextBox("LName", values[2]);
            NewTextBox("FName", values[3]);
            NewTextBox("MName", values[4]);
            NewDropDownList("Sex", new string[] { "Мужской", "Женский" }, values[5]);
            NewTextBox("Phone", values[6]);
            NewTextBox("E-mail", values[7]);
            NewTextBox("AppealText", values[8]);
            NewApplyButton();
            NewCancelButton();
        }

        private void AddDeptElems()
        {
            if (values[1] != null)
            {
                NewTextBox("RegionId", values[1].ToString());
            }
            else
            {
                NewTextBox("RegionId", null);
            }
            NewTextBox("Address", values[2]);
            NewTextBox("Site", values[3]);
            NewTextBox("Phone", values[4]);
            NewDropDownList("Head", Util.GetValuesFromTable("Член_Партии", "Идентификатор_Члена_Партии, Фамилия"), values[5]);
            NewApplyButton();
            NewCancelButton();
        }

        private void AddProjectElems()
        {
            NewTextBox("Name", values[1]);
            NewTextBox("Desc", values[2]);
            NewTextBox("Finance", values[3]);
            NewDropDownList("Members", Util.GetValuesFromTable("Член_Партии", "Идентификатор_Члена_Партии, Фамилия"), values[4]);
            NewApplyButton();
            NewCancelButton();
        }

        private void AddMemberElems()
        {
            NewTextBox("LName", values[1]);
            NewTextBox("FName", values[2]);
            NewTextBox("MName", values[3]);
            NewDropDownList("Sex", new string[] { "Мужской", "Женский" }, values[4]);
            NewCalendar("Birthday", values[5]);
            NewTextBox("Phone", values[6]);
            NewTextBox("Email", values[7]);
            NewApplyButton();
            NewCancelButton();
        }

        private void AddEventElems()
        {
            string[] hArr = new string[24];
            string[] mArr = new string[60];
            for (int h = 0; h < 24; h++)
            {
                if (h < 10)
                {
                    hArr[h] = "0" + h.ToString();
                }
                else
                {
                    hArr[h] = h.ToString();
                }
            }
            for (int m = 0; m < 60; m++)
            {
                if (m < 10)
                {
                    mArr[m] = "0" + m.ToString();
                }
                else
                {
                    mArr[m] = m.ToString();
                }
            }
            if (values[0] != null)
            {
                DateTime date = (DateTime)values[1];
                NewCalendar("Date", date.Date);
                NewDropDownList("Hours", hArr, date.Hour);
                NewDropDownList("Minutes", mArr, date.Minute);
            }
            else
            {
                NewCalendar("Date", null);
                NewDropDownList("Hours", hArr, null);
                NewDropDownList("Minutes", mArr, null);
            }
            NewTextBox("Desc", values[2]);
            NewCheckBoxList("Members", Util.GetValuesFromTable("Член_Партии", "Идентификатор_Члена_Партии, Фамилия"), values[3]);
            NewApplyButton();
            NewCancelButton();
        }

        private void RedirectBack(object sender, EventArgs e)
        {
            values = new object[10];
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
            bool isHours = true;
            List<int> memberIdLst = new List<int>();
            List<object> lst = new List<object>();
            if (values[0] != null)
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
                                if (tableId != 1)
                                {
                                    if (tableId == 4)
                                    {
                                        lst.Add((ctrl as DropDownList).SelectedValue);
                                    }
                                    else
                                    {
                                        lst.Add(Util.GetIDFromString((ctrl as DropDownList).SelectedValue));
                                    }
                                }
                                else
                                {
                                    if (isHours)
                                    {
                                        DateTime date = (DateTime)lst[1];
                                        date = date.AddHours(Double.Parse((ctrl as DropDownList).SelectedValue));
                                        lst[1] = date;
                                        isHours = false;
                                    }
                                    else
                                    {
                                        DateTime date = (DateTime)lst[1];
                                        date = date.AddMinutes(Double.Parse((ctrl as DropDownList).SelectedValue));
                                        lst[1] = date;
                                        DebugLabel.Text = lst[1].ToString();
                                        isHours = true;
                                    }
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
                                if (values[0] is null)
                                {
                                    Util.LinkEventToMember(memberId, Util.GetLastID("Событие", "Идентификатор_События"));
                                }
                                else
                                {
                                    Util.LinkEventToMember(memberId, (int)values[0]);
                                }
                            }
                            if (memberIdLst.Count == 0)
                            {
                                Util.DeleteLink((int)values[0]);
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

        private void NewCheckBoxList(string id, string[] items, object value)
        {
            string[] selectedStrArr = value as string[];
            CheckBoxList cbl = new CheckBoxList() { ID = id };
            foreach (string item in items)
            {
                cbl.Items.Add(item);
            }
            foreach (ListItem item in cbl.Items)
            {
                if (selectedStrArr != null)
                {
                    foreach (string str in selectedStrArr)
                    {
                        if (item.Text.SubstringUpToFirst(' ').Contains(str))
                        {
                            item.Selected = true;
                            break;
                        }
                    }
                }
            }
            EditPanel.Controls.Add(cbl);
        }

        private void NewTextBox(string id, object value)
        {
            string textStr = value as string;
            TextBox tb = new TextBox() { ID = id };
            if (textStr != null)
            {
                tb.Text = textStr;
            }
            EditPanel.Controls.Add(tb);
        }

        private void NewDropDownList(string id, string[] items, object value)
        {
            DropDownList ddl = new DropDownList() { ID = id };
            foreach (string item in items)
            {
                ddl.Items.Add(item);
            }
            if (value != null)
            {
                switch (value)
                {
                    case true:
                        {
                            ddl.SelectedValue = "Женский";
                            break;
                        }
                    case false:
                        {
                            ddl.SelectedValue = "Мужской";
                            break;
                        }
                    default:
                        {
                            foreach (ListItem item in ddl.Items)
                            {
                                if (item.Text.Contains(value.ToString()))
                                {
                                    item.Selected = true;
                                    break;
                                }
                            }
                            break;
                        }
                }
            }
            EditPanel.Controls.Add(ddl);
        }

        private void NewCalendar(string id, object value)
        {
            Calendar cld = new Calendar() { ID = id };
            cld.SelectionMode = CalendarSelectionMode.Day;
            if (value is null)
            {
                cld.SelectedDate = DateTime.Today;
            }
            else
            {
                cld.SelectedDate = (DateTime)value;
            }
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