using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraBars;

using DebonoDLL.Helpers;
using Debono.Detail;
using DebonoDLL;
using DebonoDLL.App_Code.BOL;
using DebonoDLL.BOL;
using DEBONO;
using DevExpress.XtraGrid.Views.Grid;

namespace Debono.Info
{
    public partial class StudentFees : DevExpress.XtraEditors.XtraForm
    {
        private string _UserName;
        public string UserName
        {
            get { return _UserName; }
            set { _UserName = value; }
        }
        public StudentFees()
        {
            InitializeComponent();
        }

        void chekadminoruser()
        {
            try
            {
                if (!GlobleData.ManageRoleAccessEdit(this.Name))
                {
                    toolStripButtonDetail.Enabled = false;
                    toolStripButtonPrint.Enabled = false;
                }
            }
            catch (Exception ex)
            {
               ExceptionManager.LogException(ex);
            }
        }

        private void StudentFees_Load(object sender, EventArgs e)
        {
            this._UserName = UserName;
            int month = DateTime.Now.Month;
            int year = DateTime.Now.Year;
            if (month > 6)
                ddlsession.SelectedItem = year.ToString() + "-" + (year + 1).ToString();
            else
                ddlsession.SelectedItem = (year - 1).ToString() + "-" + year.ToString();
            chekadminoruser();
            GetAllStudentInfoData();
        }

        private void StudentFees_Activated(object sender, EventArgs e)
        {
            try
            {
                LoadOrSaveLayout(CommonFunction.custLayoutOptions.Load);
            }
            catch (Exception ex)
            {
                ExceptionManager.LogException(ex);
            }
            GetAllStudentInfoData();
        }
        Conversion objcon = new Conversion();
        private void GetAllStudentInfoData()
        {
            int nFocusRow = gvMatCategory.FocusedRowHandle;
            StudentInfoBo objStudentInfo = new StudentInfoBo();
            DataTable dtStudentInfo = new DataTable();
            try
            {
                if (ddlsession.Text == "Select")
                dtStudentInfo = objStudentInfo.ShowAllStudentFeesInfo();
                else
                    dtStudentInfo = objStudentInfo.ShowAllStudentFeesInfo(ddlsession.Text, ddlclassname.Text,ddlpaymenttype.Text,ddlsection.Text);
                GrdC_CustomerInfo.DataSource = dtStudentInfo;

                DataTable dtcountstudent = objStudentInfo.countstudent(ddlsession.Text, ddlclassname.Text,ddlsection.Text);
                if (dtcountstudent.Rows.Count > 0)
                {
                    lblRecievedfees.Text=objcon.ConToDec(dtStudentInfo.Compute("Sum(PaidFees)", string.Empty)).ToString();
                    lblgeneratorfees.Text = objcon.ConToDec(dtStudentInfo.Compute("Sum(GeneratorFee)", string.Empty)).ToString();
                    lblcomputerfees.Text = objcon.ConToDec(dtStudentInfo.Compute("Sum(ComputerFee)", string.Empty)).ToString();
                    lblptafees.Text = objcon.ConToDec(dtStudentInfo.Compute("Sum(PTAFee)", string.Empty)).ToString();
                    lbllibraryfees.Text = objcon.ConToDec(dtStudentInfo.Compute("Sum(LibraryFee)", string.Empty)).ToString();
                    lblfdbrifees.Text = objcon.ConToDec(dtStudentInfo.Compute("Sum(FDBRIFee)", string.Empty)).ToString();
                    lblmaintinencefees.Text = objcon.ConToDec(dtStudentInfo.Compute("Sum(MaintenceFee)", string.Empty)).ToString();
                    lblyearlyexamfees.Text = objcon.ConToDec(dtStudentInfo.Compute("Sum(AnnualExamFee)", string.Empty)).ToString();
                    lblhalfexamfees.Text = objcon.ConToDec(dtStudentInfo.Compute("Sum(HalfyFee)", string.Empty)).ToString();
                    lbltotalstudent.Text = dtStudentInfo.Rows.Count.ToString();
                    for (int i = 0; i < dtcountstudent.Rows.Count; i++)
                    {
                        if (dtcountstudent.Rows[i]["PaymentType"].ToString() == "Free")
                            lblfreestudent.Text = dtcountstudent.Rows[i]["TotalStudent"].ToString();
                        if (dtcountstudent.Rows[i]["PaymentType"].ToString() == "Half")
                            lblhalfpaidstudent.Text = dtcountstudent.Rows[i]["TotalStudent"].ToString();
                        if (dtcountstudent.Rows[i]["PaymentType"].ToString() == "Full")
                            lblfullpaidstudent.Text = dtcountstudent.Rows[i]["TotalStudent"].ToString();
                    }
                    lblfreestudent.Text = objcon.ConToInt(lblfreestudent.Text).ToString();
                    lblhalfpaidstudent.Text = objcon.ConToInt(lblhalfpaidstudent.Text).ToString();
                    lblfullpaidstudent.Text = objcon.ConToInt(lblfullpaidstudent.Text).ToString();
                }
                else
                {
                    lbltotalstudent.Text = "0";
                    lblfreestudent.Text = "0";
                    lblhalfpaidstudent.Text = "0";
                    lblfullpaidstudent.Text = "0";
                    lblRecievedfees.Text = "0";
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.LogException(ex);
            }
            gvMatCategory.FocusedRowHandle = nFocusRow;
        }

        private void toolStripButtonDetail_ItemClick(object sender, ItemClickEventArgs e)
        {
            Conversion objCon = new Conversion();
            ShowStudentInfoDetailForm(objCon.ConToInt64(gvMatCategory.GetFocusedRowCellValue("SId")),ddlsession.Text);
        }

        private void ShowStudentInfoDetailForm(long SId,string session)
        {
            StudentFeesPayment objStudentInfoDetail = new StudentFeesPayment(SId,session);
            objStudentInfoDetail.UserName = this._UserName;
            FormHelper.OpenForm(objStudentInfoDetail, this.MdiParent);
        }

        private void toolStripButtonNew_ItemClick(object sender, ItemClickEventArgs e)
        {
            StudentDetail objCustomerDetail = new StudentDetail();
            FormHelper.OpenForm(objCustomerDetail, this.MdiParent);
        }

        private void GrdV_CustomerInfo_DoubleClick(object sender, EventArgs e)
        {
            if (!GlobleData.ManageRoleAccessEdit(this.Name))
            {
                return;
            }
            Conversion objCon = new Conversion();
            ShowStudentInfoDetailForm(objCon.ConToInt64(gvMatCategory.GetFocusedRowCellValue("SId")), ddlsession.Text);
            // gvMatCategory.GetFocusedRowCellValue("StudentInfoId");
        }

        private void StudentFees_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                LoadOrSaveLayout(CommonFunction.custLayoutOptions.Save);
            }
            catch (Exception ex)
            {
                ExceptionManager.LogException(ex);
            }

        }

        private void LoadOrSaveLayout(CommonFunction.custLayoutOptions enLayoutOptions)
        {
            CommonFunction objcmnFun = new CommonFunction();
            objcmnFun.LoadORSaveLayout(this.Name + "&", ref gvMatCategory, enLayoutOptions);

        }

        private void gvMatCategory_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            try
            {
                Conversion conver = new Conversion();
                //ProductDetailBo objProductDtl1 = new ProductDetailBo();
                //GridView View = sender as GridView;
                //var mm = View.GetRow(e.RowHandle);
                //Int64 rowmati=conver.ConToInt64(View.GetRowCellValue(e.RowHandle, View.Columns["StudentInfoId"]));
                //string rowmatid = conver.ConToStr(View.GetRowCellValue(e.RowHandle, View.Columns["StudentInfoId"]).ToString());
                //DataTable dt2 = new DataTable();
                //DataRowView dvr = (DataRowView)mm;
                //if (e.RowHandle >= 0)
                //{
                //    rowmatid = rowmatid.Replace("0-", "");
                //    DataTable dt = objProductDtl1.getcategorylock(rowmatid);
                //    if (dt != null && dt.Rows.Count > 0)
                //    {
                //        if (dt.Rows[0]["Lockedit"].ToString() == "True")
                //        {
                //            e.Appearance.BackColor = Color.LightGreen;
                //            e.Appearance.BackColor2 = Color.LightGreen;
                //        }
                //    }

                //}  
            }
            catch (Exception ex)
            { }
        }

        private void toolStripButtonPrint_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                string filepath = "";
                saveFileDialog1.Filter = "Excel files (*.xls)|*.xls|All files (*.*)|*.*";
              DialogResult result = saveFileDialog1.ShowDialog();
              if (result == DialogResult.OK)
              {
                  filepath = saveFileDialog1.FileName;              
                  GrdC_CustomerInfo.ExportToXls(filepath);
              }
            }
            catch (Exception ex)
            {
                ExceptionManager.LogException(ex);
            }
        }

        private void GrdC_CustomerInfo_ProcessGridKey(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (!GlobleData.ManageRoleAccessEdit(this.Name))
                {
                    return;
                }
                Conversion objCon = new Conversion();
                ShowStudentInfoDetailForm(objCon.ConToInt64(gvMatCategory.GetFocusedRowCellValue("SId")), ddlsession.Text);
            }
        }

        private void GrdC_CustomerInfo_Click(object sender, EventArgs e)
        {

        }

        private void ddlsession_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetAllStudentInfoData();
        }

        private void ddlclassname_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetAllStudentInfoData();
        }

        private void ddlpaymenttype_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetAllStudentInfoData();
        }

        private void ddlsection_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetAllStudentInfoData();
        }

        private void ribbonControl2_Click(object sender, EventArgs e)
        {

        }

        private void label19_Click(object sender, EventArgs e)
        {

        }
    }
}