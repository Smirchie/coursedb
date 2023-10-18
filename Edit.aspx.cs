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
    public partial class Edit : System.Web.UI.Page
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
            NewApplyButton();
            NewCancelButton();
            NewDropDownList("Идентификатор Отделеня", Util.GetValuesFromTable("Отделение", "Идентификатор_Отделения"), values[1]);
            NewLabel("Данные отправителя");
            NewTextBox("Фамилия", values[2]);
            NewTextBox("Имя", values[3]);
            NewTextBox("Отчество", values[4]);
            NewDropDownList("Пол", new string[] { "Мужской", "Женский" }, values[5]);
            NewTextBox("Телефон", values[6]);
            NewTextBox("E-mail", values[7]);
            NewTextBox("Текст обращения", values[8]);
        }

        private void AddDeptElems()
        {
            NewApplyButton();
            NewCancelButton();
            if (values[1] != null)
            {
                NewTextBox("Номер региона", values[1].ToString());
            }
            else
            {
                NewTextBox("Номер региона", null);
            }
            NewTextBox("Адрес", values[2]);
            NewTextBox("Сайт", values[3]);
            NewTextBox("Телефон", values[4]);
            NewDropDownList("Руководитель", Util.GetValuesFromTable("Член_Партии", "Идентификатор_Члена_Партии, Фамилия"), values[5]);
        }

        private void AddProjectElems()
        {
            NewApplyButton();
            NewCancelButton();
            NewTextBox("Название", values[1]);
            NewTextBox("Описание", values[2]);
            try
            {
                NewTextBox("Финансирование", values[3].ToString());
            }
            catch
            {
                NewTextBox("Финансирование", null);
            }
            NewDropDownList("Руководитель", Util.GetValuesFromTable("Член_Партии", "Идентификатор_Члена_Партии, Фамилия"), values[4]);
        }

        private void AddMemberElems()
        {
            NewApplyButton();
            NewCancelButton();
            NewTextBox("Фамилия", values[1]);
            NewTextBox("Имя", values[2]);
            NewTextBox("Отчество", values[3]);
            NewDropDownList("Пол", new string[] { "Мужской", "Женский" }, values[4]);
            try
            {
                DateTime bday = (DateTime)values[5];
                NewTextBox("Дата рождения", bday.ToShortDateString());
            }
            catch
            {
                NewTextBox("Дата рождения", null);
            }
            NewTextBox("Телефон", values[6]);
            NewTextBox("E-mail", values[7]);
        }

        private void AddEventElems()
        {
            NewApplyButton();
            NewCancelButton();
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
            try
            {
                DateTime date = (DateTime)values[1];
                NewCalendar("Дата проведения", date.Date);
                NewDropDownList("Время проведения", hArr, date.Hour);
                NewDropDownList(":", mArr, date.Minute);
            }
            catch
            {
                NewCalendar("Дата проведения", null);
                NewDropDownList("Время проведения", hArr, null);
                NewDropDownList(":", mArr, null);
            }
            NewTextBox("Описание", values[2]);
            NewTextBox("Адрес", values[3]);
            NewCheckBoxList("Участники", Util.GetValuesFromTable("Член_Партии", "Идентификатор_Члена_Партии, Фамилия"), values[4]);
        }

        private void RedirectBack(object sender, EventArgs e)
        {
            ErrorLabel.Text = null;
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
            try
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
                        if ((ctrl as TextBox).Text.Trim() == "")
                        {
                            lst.Add(null);
                        }
                        else
                        {
                            lst.Add((ctrl as TextBox).Text);
                        }
                    }
                    else if (ctrl is Calendar)
                    {
                        lst.Add((ctrl as Calendar).SelectedDate);
                    }
                    else if (ctrl is CheckBoxList)
                    {
                        foreach (ListItem item in (ctrl as CheckBoxList).Items)
                        {
                            if (item.Selected)
                            {
                                memberIdLst.Add(Util.GetIDFromString(item.Text));
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
                                            isHours = true;
                                        }
                                    }
                                    break;
                                }
                        }
                    }
                }

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
                            if (values[0] != null)
                            {
                                Util.DeleteLink((int)values[0]);
                            }
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
                ErrorLabel.Text = "Проверьте корректность введенных данных";
            }
        }

        private void NewCheckBoxList(string id, string[] items, object value)
        {
            NewLabel(id);
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
            BrLabel();
        }

        private void NewTextBox(string id, object value)
        {
            NewLabel(id);
            string textStr = value as string;
            TextBox tb = new TextBox() { ID = id };
            if (textStr != null)
            {
                tb.Text = textStr;
            }
            if (id == "Текст обращения" || id == "Описание" || id == "Адрес")
            {
                tb.TextMode = TextBoxMode.MultiLine;
                if (id == "Текст обращения")
                {
                    tb.MaxLength = 2048;
                    tb.Width = 600;
                    tb.Rows = 10;
                }
            }
            EditPanel.Controls.Add(tb);
            BrLabel();
        }

        private void NewDropDownList(string id, string[] items, object value)
        {
            NewLabel(id);
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
            if (id != "Время проведения")
            {
                BrLabel();
            }
        }

        private void NewLabel(string text)
        {
            Label lbl = new Label() { Text = text };
            EditPanel.Controls.Add(lbl);
            if (text != ":")
            {
                BrLabel();
            }
        }

        private void NewCalendar(string id, object value)
        {
            NewLabel(id);
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
            BrLabel();
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
            BrLabel();
        }

        private void BrLabel()
        {
            Label lbl = new Label() { Text = "</br>" };
            EditPanel.Controls.Add(lbl);
        }
    }
}