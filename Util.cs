using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.SessionState;
using System.Web.Configuration;
using System.Web.UI.WebControls;
using System.Security.Cryptography;
using System.Data;
using Microsoft.Ajax.Utilities;
using System.Data.Common;

namespace coursedb
{
    public static class Util
    {
        private static readonly SqlConnection con = new SqlConnection(WebConfigurationManager.ConnectionStrings["Pol_Party_courseConnectionString"].ConnectionString);

        public static string[] GetRowAsArray(GridViewCommandEventArgs e, GridView gridView)
        {
            List<string> lst = new List<string>();
            foreach (TableCell cell in gridView.Rows[Int32.Parse(e.CommandArgument.ToString())].Cells)
            {
                lst.Add(cell.Text);
            }
            return lst.ToArray();
        }

        public static int GetIdFromGridView(GridViewCommandEventArgs e, GridView gridView)
        {
            return Int32.Parse(gridView.Rows[Int32.Parse(e.CommandArgument.ToString())].Cells[0].Text);
        }

        public static string[] GetValuesFromTable(string tableName, string columnNames)
        {
            List<string> values = new List<string>();
            SqlCommand cmd = new SqlCommand($"SELECT {columnNames} FROM {tableName}", con);
            if (con.State == System.Data.ConnectionState.Closed)
            {
                con.Open();
            }
            SqlDataReader rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                values.Add(ReadSingleRow((IDataRecord)rdr));
            }
            con.Close();
            return values.ToArray();
        }

        public static object[] GetValuesFromRow(string tableName, string columnNames)
        {
            List<object> values = new List<object>();
            SqlCommand cmd = new SqlCommand($"SELECT {columnNames} FROM {tableName}", con);
            if (con.State == System.Data.ConnectionState.Closed)
            {
                con.Open();
            }
            SqlDataReader rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                for (int i = 0; i < (rdr as IDataRecord).FieldCount; i++)
                {
                    values.Add((rdr as IDataRecord).GetValue(i));
                }
            }
            con.Close();
            return values.ToArray();
        }

        public static int GetLastID(string tableName, string columnNames)
        {
            string[] strArr = GetValuesFromTable(tableName, columnNames);
            return GetIDFromString(strArr[strArr.Length - 1]);
        }

        private static string ReadSingleRow(IDataRecord dataRcd)
        {
            try
            {
                return String.Format("{0} - {1}", dataRcd[0], dataRcd[1]);
            }
            catch
            {
                return String.Format("{0}", dataRcd[0]);
            }
        }

        public static int GetIDFromString(string str)
        {
            str = str.SubstringUpToFirst(' ');
            return (Int32.Parse(str));
        }

        public static void InsertOrUpdateMember(object[] values)
        {
            SqlCommand cmd = new SqlCommand("InsertOrUpdateMember", con);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Id", values[0]);
            cmd.Parameters.AddWithValue("@LastName", values[1]);
            cmd.Parameters.AddWithValue("@FirstName", values[2]);
            cmd.Parameters.AddWithValue("@MiddleName", values[3]);
            cmd.Parameters.AddWithValue("@Sex", values[4]);
            cmd.Parameters.AddWithValue("@Birthday", values[5]);
            cmd.Parameters.AddWithValue("@Phone", values[6]);
            cmd.Parameters.AddWithValue("@Email", values[7]);
            Exec(cmd);
        }

        public static void InsertOrUpdateDept(object[] values)
        {
            SqlCommand cmd = new SqlCommand("InsertOrUpdateDept", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Id", values[0]);
            cmd.Parameters.AddWithValue("@RegionId", values[1]);
            cmd.Parameters.AddWithValue("@Address", values[2]);
            cmd.Parameters.AddWithValue("@Site", values[3]);
            cmd.Parameters.AddWithValue("@Phone", values[4]);
            cmd.Parameters.AddWithValue("@MemberId", values[5]);
            Exec(cmd);
        }

        public static void DeleteLink(int eventId)
        {
            SqlCommand cmd = new SqlCommand("DeleteLink", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@EventId", eventId);
            Exec(cmd);
        }

        public static void InsertOrUpdateAppeal(object[] values)
        {
            SqlCommand cmd = new SqlCommand("InsertOrUpdateAppeal", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Id", values[0]);
            cmd.Parameters.AddWithValue("@DeptId", values[1]);
            cmd.Parameters.AddWithValue("@LastName", values[2]);
            cmd.Parameters.AddWithValue("@FirstName", values[3]);
            cmd.Parameters.AddWithValue("@MiddleName", values[4]);
            cmd.Parameters.AddWithValue("@Sex", values[5]);
            cmd.Parameters.AddWithValue("@Phone", values[6]);
            cmd.Parameters.AddWithValue("@Email", values[7]);
            cmd.Parameters.AddWithValue("@Text", values[8]);
            Exec(cmd);
        }

        public static void InsertOrUpdateProject(object[] values)
        {
            SqlCommand cmd = new SqlCommand("InsertOrUpdateProject", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Id", values[0]);
            cmd.Parameters.AddWithValue("@Name", values[1]);
            cmd.Parameters.AddWithValue("@Desc", values[2]);
            cmd.Parameters.AddWithValue("@Finance", values[3]);
            cmd.Parameters.AddWithValue("@MemberId", values[4]);
            Exec(cmd);
        }

        public static void DeleteFromTable(int id, string table)
        {
            SqlCommand cmd = new SqlCommand("DeleteFrom" + table, con);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Id", id);
            Exec(cmd);
        }

        public static void LinkEventToMember(int memberId, int eventId)
        {
            SqlCommand cmd = new SqlCommand("LinkEventToMember", con);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@MemberId", memberId);
            cmd.Parameters.AddWithValue("@EventId", eventId);
            Exec(cmd);
            DeleteDuplicatesFromRelation();
        }

        private static void DeleteDuplicatesFromRelation()
        {
            SqlCommand cmd = new SqlCommand("DeleteDuplicatesFromRelation", con);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            Exec(cmd);
        }

        public static void InsertOrUpdateEvent(object[] values)
        {
            SqlCommand cmd = new SqlCommand("InsertOrUpdateEvent", con);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Id", values[0]);
            cmd.Parameters.AddWithValue("@Date", values[1]);
            cmd.Parameters.AddWithValue("@Desc", values[2]);
            Exec(cmd);
        }

        private static void Exec(SqlCommand cmd)
        {
            if (con.State == System.Data.ConnectionState.Closed)
            {
                con.Open();
            }
            cmd.ExecuteNonQuery();
            con.Close();
        }
    }
}