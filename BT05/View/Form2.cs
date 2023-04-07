using BT05.BLL;
using BT05.DAL;
using BT05.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BT05.View
{
    public partial class Form2 : Form
    {
        public delegate void MyDel(List<SVView> list);
        public MyDel d { get; set; }
        public string mssv { get; set; }
        public Form2()
        {
            InitializeComponent();
            SetCBBLSH();
        }
        public void SetCBBLSH()
        {
            QLSVBLL bll  = new QLSVBLL();   
            cbbLSH.Items.AddRange(bll.GetCBBItems().ToArray());
        }
        public void Add()
        {
            SV s = new SV()
            {
                MSSV = txtMSSV.Text,
                Name = txtName.Text,
                ID_Lop = ((CBBItem)cbbLSH.SelectedItem).Value,
                NS = Convert.ToDateTime(dtp.Value.ToString()),
                DTB = Convert.ToDouble(txtDTB.Text),
                Gender = Convert.ToBoolean(SetGender()),
                Anh = cbAnh.Checked,
                HocBa = cbHocBa.Checked,
                CCCD = cbCCCD.Checked,
            };
            QLSVBLL bll = new QLSVBLL();
            bll.AddSVBLL(s);
            d(bll.GetSVViewsALL());

        }
        public string SetGender()
        {
            string gt = "";
            if (radioButton1.Checked)
            {
                gt = "true";
            }
            else if (radioButton2.Checked)
            {
                gt = "false";
            }
            return gt;

        }
        public void GetDL(SVView s)
        {
            string NameLop = s.NameLop;
           
            QLSVBLL bll = new QLSVBLL();
            int id = bll.GetID_LopBLL(NameLop)-1;
       
            txtMSSV.Text = s.MSSV;
            txtMSSV.Enabled = false;
            txtName.Text = s.Name;
             cbbLSH.SelectedIndex = id;
          
            dtp.Value = s.NS;
            txtDTB.Text = Convert.ToString(s.DTB);
            if (s.Gender==true)
            {
                radioButton1.Checked = true;
            }
            else
            {
                radioButton2.Checked = true;
            }
            cbAnh.Checked = s.Anh;
            cbCCCD.Checked = s.CCCD;
            cbHocBa.Checked= s.HocBa;

        }
        public void Edit()
        {
            SV s = new SV()
            {
                MSSV = mssv,
                Name=txtName.Text,
                ID_Lop=((CBBItem)cbbLSH.SelectedItem).Value,
                NS=Convert.ToDateTime(dtp.Value.ToString()),
                DTB=Convert.ToDouble(txtDTB.Text),
                Gender=Convert.ToBoolean(SetGender()),
                Anh=cbAnh.Checked,
                HocBa=cbHocBa.Checked,
                CCCD=cbCCCD.Checked
                
            };
            QLSVBLL bll = new QLSVBLL();
            bll.UpdateSVBLL(s);
            
        }

        private void butOK_Click(object sender, EventArgs e)
        {
            if(mssv==null)
            {
                Add();
            }else if (mssv != null)
            {
                Edit();
            }
        }

        private void butCancel_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
    }
}
