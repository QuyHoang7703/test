using BT05.DAL;
using BT05.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BT05.BLL
{
    internal class QLSVBLL
    {
        public List<CBBItem> GetCBBItems()
        {
            List<CBBItem> list = new List<CBBItem>();
            QLSVDAL dal = new QLSVDAL();
            foreach(LSH i in dal.GetAllLSH())
            {
                list.Add(new CBBItem()
                {
                    Value = i.ID_Lop,
                    Text=i.NameLop
                });
                
            }
            return list;
        }
      
        public List<SVView> GetSVViewsByLSH(int id)
        {
            List<SVView> list = new List<SVView>();
            QLSVBLL bll = new QLSVBLL();

            if (id == 0)
            {
                list=bll.GetSVViewsALL();
            }
            else
            {
                foreach (SVView i in bll.GetSVViewsALL())
                {
                    QLSVDAL dal = new QLSVDAL();
                    if (dal.GetNameLop(id) == i.NameLop)
                    {
                        list.Add(i);
                    }

                }
            }
            return list;
        }
        public List<SVView> GetSVViewsByMSSV(string mssv)
        {
            List<SVView> list = new List<SVView>();
            QLSVBLL bll = new QLSVBLL();

            foreach(SVView i in bll.GetSVViewsALL())
            {
                if (mssv==i.MSSV)
                {

                    list.Add(i);
                }
            }
            return list;
        }
        public List<SVView> GetSVViewByID_MSSV(int id, string mssv)
        {
            List<SVView> list = new List<SVView>();
            QLSVBLL bll = new QLSVBLL();
            QLSVDAL dal = new QLSVDAL();
            foreach (SVView i in bll.GetSVViewsALL())
            {
                if (mssv == i.Name && dal.GetNameLop(id)==i.NameLop)
                {

                    list.Add(i);
                }
            }
            return list;
        }

      
       
        
        
        public List<SVView> GetSVViewsALL()
        {
            List<SVView> list = new List<SVView>();
            QLSVDAL dal = new QLSVDAL();
            foreach(SV i in dal.GetAllSV())
            {
                string nameLop = "";
                foreach(LSH j in dal.GetAllLSH())
                {
                    if(i.ID_Lop==j.ID_Lop)
                    {
                        nameLop=j.NameLop; break;
                    }

                }
                list.Add(new SVView()
                {
                    MSSV =i.MSSV,
                    Name=i.Name,
                    DTB=i.DTB,
                    NameLop=nameLop,
                    NS = Convert.ToDateTime(i.NS.ToString()),
                    Gender =Convert.ToBoolean(i.Gender.ToString()),
                    Anh= Convert.ToBoolean(i.Anh.ToString()),
                    HocBa=Convert.ToBoolean(i.HocBa.ToString()),
                    CCCD=Convert.ToBoolean(i.CCCD.ToString())

                });
            }
            return list;

            
        }
        public void AddSVBLL(SV s)
        {
            QLSVDAL dal = new QLSVDAL();
            dal.AddSVDAL(s);
        }
        public void DeleteSVBLL(string mssv)
        {
            QLSVDAL dal = new QLSVDAL();
            dal.DeleteSVDAL(mssv);
        }
        public void UpdateSVBLL(SV s)
        {
            QLSVDAL dal =new QLSVDAL();
            dal.UpdateSVDAL(s);
        }
        public int GetID_LopBLL(string NameLop)
        {
            QLSVDAL dal = new QLSVDAL();
            int id =dal.GetID_LopDAL(NameLop);
            return id;
        }
        public List<SVView> GetSVView(List<string> list)
        {
            List<SVView> l = new List<SVView>();
            foreach(string i in list)
            {
                foreach(SVView j in GetSVViewsALL())
                {
                    if (j.MSSV == i)
                    {
                        l.Add(j);
                    }
                }
            }
            return l;
        }
        public List<SVView> SortBy(List<string> list, string text)
        {
            List<SVView> l = new List<SVView>();
            l = GetSVView(list);
           
            if(text == "ascending")
            {
                l.Sort();
            }
            else
            {
                l.Reverse();
            }
            return l;



        }
       
    }
}
