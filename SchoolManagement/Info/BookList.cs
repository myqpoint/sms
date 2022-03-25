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
    public partial class BookList : DevExpress.XtraEditors.XtraForm
    {
        private string _UserName;
        public string UserName
        {
            get { return _UserName; }
            set { _UserName = value; }
        }
        public BookList()
        {
            this._UserName = UserName;
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
        private void BookList_Load(object sender, EventArgs e)
        {
            this._UserName = UserName;
            chekadminoruser();

            GetAllStaffData();
        }

        private void BookList_Activated(object sender, EventArgs e)
        {
            try
            {
                LoadOrSaveLayout(CommonFunction.custLayoutOptions.Load);
            }
            catch (Exception ex)
            {
                ExceptionManager.LogException(ex);
            }
            GetAllStaffData();
        }

        private void GetAllStaffData()
        {
            int nFocusRow = gvMatCategory.FocusedRowHandle;
            BookBo objStudentInfo = new BookBo();
            DataTable dtStudentInfo = new DataTable();
            try
            {
                    dtStudentInfo = objStudentInfo.ShowBookMst();
                    GrdC_CustomerInfo.DataSource = dtStudentInfo;
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
            ShowStudentInfoDetailForm(objCon.ConToInt64(gvMatCategory.GetFocusedRowCellValue("BookID")));
        }

        private void ShowStudentInfoDetailForm(long BookID)
        {
            BookDetail objStudentInfoDetail = new BookDetail(BookID);
            objStudentInfoDetail.UserName = this.UserName;
            FormHelper.OpenForm(objStudentInfoDetail, this.MdiParent);
        }

        private void toolStripButtonNew_ItemClick(object sender, ItemClickEventArgs e)
        {
            BookDetail objCustomerDetail = new BookDetail();
            FormHelper.OpenForm(objCustomerDetail, this.MdiParent);
        }

        String AutoDeleteMessage = "are you sure to delete";
        String AutoDeleteTitle = "Confirm";
        private void toolStripButtonDelete_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                DeleteStaffInfo();
            }
            catch (Exception ex)
            {
                ExceptionManager.LogException(ex);
            }

        }

        private void DeleteStaffInfo()
        {
            try
            {
                Conversion objCon = new Conversion();
                //Message obj3 = new Message();
                //obj3.ShowMessage(AutoDeleteMessage, AutoDeleteTitle, MessageBoxButtons.YesNo, Message.MessageIcon.Question);
                if (Debono.DebonoMsg.MsgDelete())
                {
                    Int64 nMatID = 0;
                    if (gvMatCategory.GetSelectedRows() != null)
                    {
                        for (int i = 0; i < gvMatCategory.GetSelectedRows().Length; i++)
                        {
                            nMatID = objCon.ConToInt64(gvMatCategory.GetRowCellValue(gvMatCategory.GetSelectedRows()[i], "BookID"));
                            //ProductDetailBo objProduct = new ProductDetailBo();
                            //objProduct._MaterialId = "0-" + objCon.ConToStr(nMatID);
                            //DataTable DtExist = objProduct.GetStudentInfo();
                            //if (DtExist.Rows.Count > 0)
                            //{
                            //    Debono.DebonoMsg.MsgInformation("Cannot Delete Used as a refernce!!!!");
                            //    return;
                            //}
                            //else
                            //{

                            //    StudentInfoBo objStudentInfo = new StudentInfoBo();
                            //    objStudentInfo._StudentInfoId = nMatID;
                            //    int nCheck = objStudentInfo.DeleteStudentInfo();
                            //    if (nCheck > 0)
                            //    {
                            //        //ExternalLinkBo objExternalLink = new ExternalLinkBo();
                            //        //objExternalLink._FormType = "StudentInfo";
                            //        //objExternalLink._FormId = nMatID;
                            //        //objExternalLink.DeleteExternalLinkByFormIdAndType();

                            //    }
                            //}
                        }
                    }
                    GetAllStaffData();
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
            ShowStudentInfoDetailForm(objCon.ConToInt64(gvMatCategory.GetFocusedRowCellValue("BookID")));
            // gvMatCategory.GetFocusedRowCellValue("StudentInfoId");
        }

        private void BookList_FormClosing(object sender, FormClosingEventArgs e)
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
            
            DeleteStaffInfo();
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
                ShowStudentInfoDetailForm(objCon.ConToInt64(gvMatCategory.GetFocusedRowCellValue("BookID")));
            }
        }
    }
}