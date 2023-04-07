using BT05.BLL;
using BT05.DTO;
using BT05.View;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BT05
{
    public partial class Form1 : Form
    {
        public delegate void MyDel1(SVView s);
        public MyDel1 d { get;set;}
        public Form1()
        {
            InitializeComponent();
            SetCBBLSH();
            SetCBBSort();
        }
        public void SetCBBLSH()
        {
            QLSVBLL bll = new QLSVBLL();
            cbbLSH.Items.Add(new CBBItem()
            {
                Value = 0,
                Text = "All"
            });
            cbbLSH.Items.AddRange(bll.GetCBBItems().ToArray());
        }
        public void ShowDGV(List<SVView> list)
        {
            dataGridView1.DataSource = list;

        }

        private void butSearch_Click(object sender, EventArgs e)
        {


            QLSVBLL bll = new QLSVBLL();
            int id;
            string text;
            if (cbbLSH.SelectedItem!=null && txtSearch.Text == "")
            {
                id = ((CBBItem)(cbbLSH.SelectedItem)).Value;
                ShowDGV(bll.GetSVViewsByLSH(id));

            }
            else if (cbbLSH.SelectedIndex <= 0 && txtSearch.Text != "")
            {

                
                text = txtSearch.Text;
                ShowDGV(bll.GetSVViewsByMSSV(text));

            }
            else if(cbbLSH.SelectedIndex!=0 && txtSearch.Text != "")
            {
                id = ((CBBItem)(cbbLSH.SelectedItem)).Value;
                text = txtSearch.Text;
                ShowDGV(bll.GetSVViewByID_MSSV(id, text));
            }
           
           

        }

        private void butAdd_Click(object sender, EventArgs e)
        {
            Form2 f = new Form2();
            f.d += new Form2.MyDel(ShowDGV);
            f.mssv = null;

            f.Show();
        }

        private void butDelete_Click(object sender, EventArgs e)
        {
            DataGridViewSelectedRowCollection l = dataGridView1.SelectedRows;
            QLSVBLL bll = new QLSVBLL();
            foreach (DataGridViewRow i in l)
            {
                string mssv = i.Cells["MSSV"].Value.ToString();
                
                bll.DeleteSVBLL(mssv);

            }
            ShowDGV(bll.GetSVViewsALL());
            
        }

        private void butUpdate_Click(object sender, EventArgs e)
        {
           
                DataGridViewSelectedRowCollection l = dataGridView1.SelectedRows;
                if (l.Count == 1)
                {
                    Form2 f = new Form2();
                    this.d += new Form1.MyDel1(f.GetDL);
                    f.mssv = l[0].Cells["MSSV"].Value.ToString();
                    SVView s = new SVView()
                    {
                        MSSV = l[0].Cells["MSSV"].Value.ToString(),
                        Name = l[0].Cells["Name"].Value.ToString(),
                        DTB = Convert.ToDouble(l[0].Cells["DTB"].Value.ToString()),
                        Gender = Convert.ToBoolean(l[0].Cells["Gender"].Value.ToString()),
                        NameLop = l[0].Cells["NameLop"].Value.ToString(),
                        Anh = Convert.ToBoolean(l[0].Cells["Anh"].Value.ToString()),
                        CCCD = Convert.ToBoolean(l[0].Cells["CCCD"].Value.ToString()),
                        HocBa = Convert.ToBoolean(l[0].Cells["HocBa"].Value.ToString()),
                        NS = Convert.ToDateTime(l[0].Cells["NS"].Value.ToString()),
                    };
                    d(s);

                    f.Show();
                }
         
               

            
        }
        public void SetCBBSort()
        {
            cbbSort.Items.Add("ascending");
            cbbSort.Items.Add("descending");
        }


        private void butSort_Click(object sender, EventArgs e)
        {
            DataGridViewSelectedRowCollection l = dataGridView1.SelectedRows;
            List<string> s = new List<string>();
            foreach(DataGridViewRow i in l)
            {
                s.Add(i.Cells["MSSV"].Value.ToString());
            }
            QLSVBLL bll = new QLSVBLL();
            
            ShowDGV(bll.SortBy(s, cbbSort.SelectedText));
        }
    }
}
