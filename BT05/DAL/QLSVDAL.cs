using BT05.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BT05.DAL
{
    internal class QLSVDAL
    {
        public SV GetSVByDaTaRow(DataRow i)
        {
            return new SV
            {
                MSSV = i["MSSV"].ToString(),
                Name = i["Name"].ToString(),
                DTB = Convert.ToDouble(i["DTB"].ToString()),
                Gender = Convert.ToBoolean(i["Gender"].ToString()),
                ID_Lop = Convert.ToInt32(i["ID_Lop"].ToString()),
                NS= Convert.ToDateTime(i["NgaySinh"].ToString()),
                Anh = Convert.ToBoolean(i["Anh"].ToString()),
                HocBa = Convert.ToBoolean(i["HocBa"].ToString()),
                CCCD = Convert.ToBoolean(i["CCCD"].ToString()),
                

            };
        }
        public List<SV> GetAllSV()
        {
            List<SV> list = new List<SV>();
            string query = "select * from SV";
            foreach(DataRow i in DBHelper.Instance.GetReords(query).Rows)
            {
                list.Add(GetSVByDaTaRow(i));
            }
            return list;
        }
        public LSH GetLSHByDataRow(DataRow i)
        {
            return new LSH
            {
                ID_Lop = Convert.ToInt32(i["ID_Lop"].ToString()),
                NameLop = i["NameLop"].ToString()
            };
        }
        public List<LSH> GetAllLSH()
        {
            List<LSH> list = new List<LSH>();
            string query = "select * from LopSH";
            foreach(DataRow i in DBHelper.Instance.GetReords(query).Rows)
            {
                list.Add(GetLSHByDataRow(i));
            }
            return list;

        }
        public void AddSVDAL(SV s)
        {
            string query = "Insert into SV (MSSV, Name, DTB, NgaySinh, ID_Lop, Gender, Anh, CCCD, HocBa)"
                          + $" Values('{s.MSSV}', '{s.Name}', {s.DTB}, '{s.NS}', {s.ID_Lop}, '{s.Gender}', '{s.Anh}', '{s.CCCD}', '{s.HocBa}')";
            DBHelper.Instance.ExecuteDB(query);
        }
        public void UpdateSVDAL(SV s)
        {
            string query = $"Update SV Set  Name ='{s.Name}', DTB = {s.DTB}, NgaySinh ='{s.NS}', ID_Lop = {s.ID_Lop}, Gender = '{s.Gender}', Anh= '{s.Anh}', CCCD = '{s.CCCD}', HocBa= '{s.HocBa}'"
                            + $" where MSSV='{s.MSSV}'";
            DBHelper.Instance.ExecuteDB(query); 

        }
        public void DeleteSVDAL(string mssv)
        {
            string query = $"Delete from SV where MSSV = '{mssv}'";
            DBHelper.Instance.ExecuteDB(query);
        }
        public string GetNameLop(int id)
        {
            string  s = "";
            string query = $"select NameLop from LopSH where ID_Lop= {id}";
            DataTable dt = DBHelper.Instance.GetReords(query);
            DataRow r = dt.Rows[0];
            s = r["NameLop"].ToString();
            return s;


        }
        public int GetID_LopDAL(string NameLop)
        {
            int id;
            string query = $"select ID_Lop from LopSH where NameLop ='{NameLop}'";

            DataTable dt = DBHelper.Instance.GetReords(query);
            DataRow r = dt.Rows[0];
            id =Convert.ToInt32(r["ID_Lop"].ToString());
            return id;
        }
        
    }
}
