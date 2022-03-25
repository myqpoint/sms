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
    public partial class StudentReport : XtraForm
    {
        Dal objdal = new Dal();
        private string _UserName;
        public string UserName
        {
            get { return _UserName; }
            set { _UserName = value; }
        }

        public StudentReport()
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
        private void btnexportDISReport_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtstartdate.Text == string.Empty || txtenddate.Text == string.Empty)
                {
                    MessageBox.Show("Sorry ! Please select session or date");
                    return;
                }
                FormHelper.ShowWaitDialog();
                Conversion objcon = new Conversion();
                groupControl2.Text = "Student Fees Report";
                string cond = "";
                if (ddlsession.Text != "")
                {
                    cond = cond + " and CI.SessionYear='" + ddlsession.Text + "'";
                }
                if (ddlclassname.Text != "")
                {
                    cond = cond + " and CI.ClassName='" + ddlclassname.Text + "'";
                }
                if (ddlsection.Text != "")
                {
                    cond = cond + " and CI.ClassSection='" + ddlsection.Text + "'";
                }
                if (ddlpaymenttype.Text != "")
                {
                    cond = cond + " and CI.PaymentType='" + ddlpaymenttype.Text + "'";
                }
                int month = 12;
                if (txtstartdate.Text != string.Empty && txtenddate.Text != string.Empty)
                {
                    cond = cond + " and Cast(CI.CreatedOn as date) between cast('" + txtstartdate.Text + "' as date) and cast('" + txtstartdate.Text + "' as date)";
                    month = GetMonthDifference(objcon.ConToDT(txtenddate.Text), objcon.ConToDT(txtstartdate.Text));
                }
                

                DataTable dt = new DataTable();
                dt = objdal.ExecuteTable("select FName+' '+isnull(LName,'') as Name,DOB,MobileNumber as 'MobileNo.',Gender,AadharNumber as 'AadharNo.',FatherName,CI.PaymentType,CI.ClassName as Class,CI.ClassSection as Section,CI.AdmissionDate,isnull((select ComputerFee from FeesType where ClassName=CI.ClassName),0)*" + month + " as ComputerFee,isnull((select GeneratorFee from FeesType where ClassName=CI.ClassName),0)*" + month + " as GeneratorFee,isnull((select PTAFee from FeesType where ClassName=CI.ClassName),0)*" + month + " as PTAFee,isnull((select isnull(PTAFee,0)+isnull(GeneratorFee,0)+isnull(ComputerFee,0) from FeesType where ClassName=CI.ClassName),0)*" + month + " as TotalFees from StudentInfo SI join ClassInfo CI on CI.SId=SI.SId where isnull(SI.Deleted,0)=0  order by FName asc");
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

        private void btnexportfees_Click(object sender, EventArgs e)
        {
            try
            {
                FormHelper.ShowWaitDialog();
                Conversion objcon = new Conversion();
                groupControl2.Text = "Student Fees Report";
                string cond = "";
                if (ddlsession.Text != "")
                {
                    cond = cond + " and CI.SessionYear='" + ddlsession.Text + "'";
                }
                if (ddlclassname.Text != "")
                {
                    cond = cond + " and CI.ClassName='" + ddlclassname.Text + "'";
                }
                if (ddlsection.Text != "")
                {
                    cond = cond + " and CI.ClassSection='" + ddlsection.Text + "'";
                }
                if (ddlpaymenttype.Text != "")
                {
                    cond = cond + " and CI.PaymentType='" + ddlpaymenttype.Text + "'";
                }
                if (txtstartdate.Text != string.Empty && txtenddate.Text != string.Empty)
                {
                    cond = cond + " and Cast(CI.CreatedOn as date) between cast('" + txtstartdate.Text + "' as date) and cast('" + txtstartdate.Text + "' as date)";
                }
                DataTable dt = new DataTable();
                dt = objdal.ExecuteTable("select FName+' '+isnull(LName,'') as Name,DOB,MobileNumber,Gender,AadharNumber,FatherName,CI.PaymentType,CI.ClassName,CI.ClassSection,CI.AdmissionDate,isnull((select Sum(Amount) from PaymentHistory where CId=CI.CId),0) as PaidFees,isnull((select Sum(Amount) from PaymentHistory where PaymentType='Admission Fee' and CId=CI.CId),0) as AdmissionFee,isnull((select Sum(Amount) from PaymentHistory where PaymentType='Library Fee' and CId=CI.CId),0) as LibraryFee,isnull((select Sum(Amount) from PaymentHistory where PaymentType='F.D.B.R.I. Fee' and CId=CI.CId),0) as FDBRIFee,isnull((select Sum(Amount) from PaymentHistory where PaymentType='Maintenance Fee' and CId=CI.CId),0) as MaintenceFee,isnull((select Sum(Amount) from PaymentHistory where PaymentType='Half Yearly Exam Fee' and CId=CI.CId),0) as HalfyFee,isnull((select Sum(Amount) from PaymentHistory where PaymentType='Annual Exam Fee' and CId=CI.CId),0) as AnnualExamFee,isnull((select Sum(GeneratorFee) from PaymentHistory where PaymentType='Monthly Fee' and CId=CI.CId),0) as GeneratorFee,isnull((select Sum(ComputerFee) from PaymentHistory where PaymentType='Monthly Fee' and CId=CI.CId),0) as ComputerFee,isnull((select Sum(PTAFee) from PaymentHistory where PaymentType='Monthly Fee' and CId=CI.CId),0) as PTAFee from StudentInfo SI join ClassInfo CI on CI.SId=SI.SId where isnull(SI.Deleted,0)=0 "+cond+" order by FName asc");
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

        private void btnsearchstudentlist_Click(object sender, EventArgs e)
        {
            try
            {
                FormHelper.ShowWaitDialog();
                Conversion objcon = new Conversion();
                groupControl2.Text = "Student Report";
                string cond = "";
                string cond1 = "";
                if (ddlsession.Text != "")
                {
                    cond1 = cond1 + " and SessionYear='" + ddlsession.Text + "'";
                    cond = " and SId in (select SId from classInfo where 1=1 " + cond1 + ")";
                }
                if (ddlclassname.Text != "")
                {
                    cond1 = cond1 + " and ClassName='" + ddlclassname.Text + "'";
                    cond = " and SId in (select SId from classInfo where 1=1 " + cond1 + ")";
                }
                if (ddlsection.Text != "")
                {
                    cond1 = cond1 + " and ClassSection='" + ddlsection.Text + "'";
                    cond = " and SId in (select SId from classInfo where 1=1 " + cond1 + ")";
                }
                if (ddlpaymenttype.Text != "")
                {
                    cond1 = cond1 + " and PaymentType='" + ddlpaymenttype.Text + "'";
                    cond = " and SId in (select SId from classInfo where 1=1 " + cond1 + ")";
                }
                if (txtstartdate.Text != string.Empty && txtenddate.Text != string.Empty)
                {
                    cond = cond + " and Cast(CreatedOn as date) between cast('" + txtstartdate.Text + "' as date) and cast('" + txtstartdate.Text + "' as date)";
                }
                DataTable dt = new DataTable();
                dt = objdal.ExecuteTable("select FName+' '+isnull(LName,'') as StudentName,Gender,DOB,AadharNumber,FatherName,MotherName,MobileNumber,EmailId,PaymentType,Religion,CasteCategory,AdmissionDate,isnull(Address,'')+','+isnull(City,'')+','+isnull(State,'')+','+isnull(Pincode,'') as Address,CreatedOn,CreatedBy from StudentInfo where isnull(Deleted,0)=0 "+cond+" order by FName asc");
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

        private void btnclear_Click(object sender, EventArgs e)
        {
            ddlsession.Text = string.Empty;
            ddlclassname.Text = string.Empty;
            ddlsection.Text = string.Empty;
            ddlpaymenttype.Text = string.Empty;
            txtstartdate.Text = string.Empty;
            txtenddate.Text = string.Empty;
            gridView20.Columns.Clear();
        }

        private void btnstudentpaymenthistory_Click(object sender, EventArgs e)
        {
            try
            {
                FormHelper.ShowWaitDialog();
                Conversion objcon = new Conversion();
                groupControl2.Text = "Student Fees History";
                string cond = "";
                if (ddlsession.Text != "")
                {
                    cond = cond + " and CI.SessionYear='" + ddlsession.Text + "'";
                }
                if (ddlclassname.Text != "")
                {
                    cond = cond + " and CI.ClassName='" + ddlclassname.Text + "'";
                }
                if (ddlsection.Text != "")
                {
                    cond = cond + " and CI.ClassSection='" + ddlsection.Text + "'";
                }
                if (ddlpaymenttype.Text != "")
                {
                    cond = cond + " and CI.PaymentType='" + ddlpaymenttype.Text + "'";
                }
                if (txtstartdate.Text != string.Empty && txtenddate.Text != string.Empty)
                {
                    cond = cond + " and Cast(PH.CreatedOn as date) between cast('" + txtstartdate.Text + "' as date) and cast('" + txtstartdate.Text + "' as date)";
                }
                DataTable dt = new DataTable();
                dt = objdal.ExecuteTable("select FName+' '+isnull(LName,'') as Name,DOB,MobileNumber,Gender,AadharNumber,FatherName,CI.PaymentType,CI.ClassName,CI.ClassSection,CI.AdmissionDate,Amount as PaidAmount,PH.PaymentType as FeesType,ComputerFee,GeneratorFee,PTAFee,PaymentDate,DATENAME(MONTH, PaymentDate) as PaidMonthName,PH.CreatedOn,PH.CreatedBy from StudentInfo SI join ClassInfo CI on CI.SId=SI.SId Join PaymentHistory PH on PH.CId=CI.CID and PH.SId=CI.SId where isnull(SI.Deleted,0)=0 "+cond+" order by FName asc");
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
