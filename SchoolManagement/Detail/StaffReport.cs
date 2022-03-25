using DebonoDLL.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DEBONO.Helper;
using DebonoDLL;
using DebonoDLL.App_Code.BOL;
using System.Data.SqlClient;
using DevExpress.XtraGrid.Views.Base;
using DebonoDLL.BOL;
using System.Xml;
using System.IO;
using System.Xml.Xsl;
using DEBONO;
using DevExpress.XtraGrid.Views.Grid;
using System.Configuration;
using DebonoDLL.App_Code.DAL;

namespace Debono.Detail
{
    public partial class StaffReport : XtraForm
    {
        Dal objdal = new Dal();
        private string _UserName;
        public string UserName
        {
            get { return _UserName; }
            set { _UserName = value; }
        }

        public StaffReport()
        {
            InitializeComponent();
            this._UserName = UserName;
        }
        
        private void OrdrDetail_Load(object sender, EventArgs e)
        {
            this._UserName = UserName;
        }

       
        private void btnexportdata_Click(object sender, EventArgs e)
        {
            try
            {
                if (gridView20.SelectedRowsCount > 0)
                {
                    string filepath = "";
                    saveFileDialog1.Filter = "Excel files (*.xls)|*.xls|All files (*.*)|*.*";
                    DialogResult result = saveFileDialog1.ShowDialog();
                    if (result == DialogResult.OK)
                    {
                        filepath = saveFileDialog1.FileName;
                        gridstockin.ExportToXls(filepath);
                        MessageBox.Show("Success ! Export To Excel ");
                    }
                }
                else
                {
                    MessageBox.Show("Sorry ! No Record Found In Grid ");
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.LogException(ex);
            }
        }

        public static int GetMonthDifference(DateTime startDate, DateTime endDate)
        {
            int monthsApart = 12 * (startDate.Year - endDate.Year) + startDate.Month - endDate.Month;
            return Math.Abs(monthsApart);
        }
    
        private void btnsearchstudentlist_Click(object sender, EventArgs e)
        {
            try
            {
                FormHelper.ShowWaitDialog();
                Conversion objcon = new Conversion();
                groupControl2.Text = "Staff Report";
                string cond = "";
                if (txtstartdate.Text != string.Empty && txtenddate.Text != string.Empty)
                {
                    cond = cond + " and Cast(CreatedOn as date) between cast('" + txtstartdate.Text + "' as date) and cast('" + txtstartdate.Text + "' as date)";
                }
                DataTable dt = new DataTable();
                dt = objdal.ExecuteTable("select Name,FatherName,Gender,MobileNumber,JoinningDate,SalaryAmount,Qualification,Religion,CasteCategory,Address from Staff where isnull(Deleted,0)=0 " + cond + " order by Name asc");
                if (dt != null && dt.Rows.Count > 0)
                {
                    gridView20.Columns.Clear();
                    gridstockin.DataSource = dt;
                }
                else
                {
                    MessageBox.Show("Sorry ! No Record Found");
                    gridView20.Columns.Clear();
                }
                FormHelper.CloseWaitDialog();

            }
            catch (Exception ex)
            {
                ExceptionManager.LogException(ex);
                FormHelper.CloseWaitDialog();
            }
        }

        private void btnstudentpaymenthistory_Click(object sender, EventArgs e)
        {
            try
            {
                FormHelper.ShowWaitDialog();
                Conversion objcon = new Conversion();
                groupControl2.Text = "Staff Payment History";
                string cond = "";
                if (txtstartdate.Text != string.Empty && txtenddate.Text != string.Empty)
                {
                    cond = cond + " and Cast(sph.CreatedOn as date) between cast('" + txtstartdate.Text + "' as date) and cast('" + txtstartdate.Text + "' as date)";
                }
                DataTable dt = new DataTable();
                dt = objdal.ExecuteTable("select Name,FatherName,Gender,MobileNumber,SalaryAmount,PaidAmount,PaymentDate,DATENAME(MONTH, PaymentDate) as PaidMonthName from staffPaymentHistory sph join staff s on s.StaffId=sph.StaffId where isnull(s.Deleted,0)=0 " + cond + " order by s.Name asc");
                if (dt != null && dt.Rows.Count > 0)
                {
                    gridView20.Columns.Clear();
                    gridstockin.DataSource = dt;
                }
                else
                {
                    MessageBox.Show("Sorry ! No Record Found");
                    gridView20.Columns.Clear();
                }
                FormHelper.CloseWaitDialog();

            }
            catch (Exception ex)
            {
                ExceptionManager.LogException(ex);
                FormHelper.CloseWaitDialog();
            }
        }
    }
}
