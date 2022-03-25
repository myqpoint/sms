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
    public partial class StudentList : DevExpress.XtraEditors.XtraForm
    {
        private string _UserName;
        public string UserName
        {
            get { return _UserName; }
            set { _UserName = value; }
        }
        public StudentList()
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
                    toolStripButtonHelp.Enabled = false;
                    toolStripButtonNew.Enabled = false;
                    toolStripButtonPrint.Enabled = false;
                }
                if (!GlobleData.ManageRoleAccessDelete(this.Name))
                {
                    toolStripButtonDelete.Enabled = false;
                    CM_Delete.Enabled = false;
                }
            }
            catch (Exception ex)
            {
               ExceptionManager.LogException(ex);
            }
        }
        Conversion objcon = new Conversion();
        private void StudentList_Load(object sender, EventArgs e)
        {
            this._UserName = UserName;
          int month = DateTime.Now.Month;
            int year = DateTime.Now.Year;
            if (month > 6)
                ddlsession.SelectedItem = year.ToString() + "-" + (year + 1).ToString();
            else
                ddlsession.SelectedItem = (year-1).ToString() + "-" + year.ToString();
            chekadminoruser();
            GetAllStudentInfoData();
        }

        private void StudentList_Activated(object sender, EventArgs e)
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

        private void GetAllStudentInfoData()
        {
            int nFocusRow = gvMatCategory.FocusedRowHandle;
            StudentInfoBo objStudentInfo = new StudentInfoBo();
            DataTable dtStudentInfo = new DataTable();
            try
            {
                if (ddlsession.Text == "Select")
                    dtStudentInfo = objStudentInfo.ShowAllStudentInfo();
                else
                {
                    dtStudentInfo = objStudentInfo.ShowAllStudentInfo(ddlsession.Text,ddlclassname.Text,ddlsection.Text);
                }
               
                GrdC_CustomerInfo.DataSource = dtStudentInfo;
                DataTable dtcountstudent = objStudentInfo.countstudent(ddlsession.Text, ddlclassname.Text,ddlsection.Text);
                if (dtcountstudent.Rows.Count > 0)
                {
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
            ShowStudentInfoDetailForm(objCon.ConToInt64(gvMatCategory.GetFocusedRowCellValue("SId")));
        }

        private void ShowStudentInfoDetailForm(long SId)
        {
            StudentDetail objStudentInfoDetail = new StudentDetail(SId);
            objStudentInfoDetail.UserName = this._UserName;
            FormHelper.OpenForm(objStudentInfoDetail, this.MdiParent);
        }

        private void toolStripButtonNew_ItemClick(object sender, ItemClickEventArgs e)
        {
            StudentDetail objCustomerDetail = new StudentDetail();
            FormHelper.OpenForm(objCustomerDetail, this.MdiParent);
        }

        String AutoDeleteMessage = "are you sure to delete";
        String AutoDeleteTitle = "Confirm";
        private void toolStripButtonDelete_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                DeleteStudentInfo();
            }
            catch (Exception ex)
            {
                ExceptionManager.LogException(ex);
            }

        }

        private void DeleteStudentInfo()
        {
            try
            {
                Conversion objCon = new Conversion();
                if (Debono.DebonoMsg.MsgDelete())
                {
                    //Int64 nMatID = 0;
                    //if (gvMatCategory.GetSelectedRows() != null)
                    //{
                    //    for (int i = 0; i < gvMatCategory.GetSelectedRows().Length; i++)
                    //    {
                    //        nMatID = objCon.ConToInt64(gvMatCategory.GetRowCellValue(gvMatCategory.GetSelectedRows()[i], "SId"));
                    //    }
                    //}
                    GetAllStudentInfoData();
                }
            }
            catch (Exception ex)
            {

                ExceptionManager.LogException(ex);
            }
        }
        private void GrdV_CustomerInfo_DoubleClick(object sender, EventArgs e)
        {
            if (!GlobleData.ManageRoleAccessEdit(this.Name))
            {
                return;
            }
            Conversion objCon = new Conversion();
            ShowStudentInfoDetailForm(objCon.ConToInt64(gvMatCategory.GetFocusedRowCellValue("SId")));
            // gvMatCategory.GetFocusedRowCellValue("StudentInfoId");
        }

        private void StudentList_FormClosing(object sender, FormClosingEventArgs e)
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

        private void CM_Delete_Click(object sender, EventArgs e)
        {
            DeleteStudentInfo();
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
                ShowStudentInfoDetailForm(objCon.ConToInt64(gvMatCategory.GetFocusedRowCellValue("SId")));
            }
        }

        private void ddlsession_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetAllStudentInfoData();
        }

        private void ddlclassname_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetAllStudentInfoData();
        }

        private void ddlsection_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetAllStudentInfoData();
        }
    }
}