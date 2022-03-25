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
    public partial class PanaltyList : DevExpress.XtraEditors.XtraForm
    {
        private string _UserName;
        public string UserName
        {
            get { return _UserName; }
            set { _UserName = value; }
        }
        public PanaltyList()
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
                    toolStripButtonPrint.Enabled = false;
                }
            }
            catch (Exception ex)
            {
               ExceptionManager.LogException(ex);
            }
        }
        Conversion objcon = new Conversion();
        private void PanaltyList_Load(object sender, EventArgs e)
        {
            this._UserName = UserName;
            chekadminoruser();
            GetAllStaffData();
        }

        private void PanaltyList_Activated(object sender, EventArgs e)
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
            PanaltyMstBo objStudentInfo = new PanaltyMstBo();
            DataTable dtStudentInfo = new DataTable();
            try
            {
                    dtStudentInfo = objStudentInfo.ShowPanaltyMst();
                    GrdC_CustomerInfo.DataSource = dtStudentInfo;
            }
            catch (Exception ex)
            {
                ExceptionManager.LogException(ex);
            }
            gvMatCategory.FocusedRowHandle = nFocusRow;
        }

        private void ShowStudentInfoDetailForm(long RId)
        {
            BookReturnForm objStudentInfoDetail = new BookReturnForm(RId);
            objStudentInfoDetail.UserName = this.UserName;
            FormHelper.OpenForm(objStudentInfoDetail, this.MdiParent);
        }

        private void GrdV_CustomerInfo_DoubleClick(object sender, EventArgs e)
        {
            //if (!GlobleData.ManageRoleAccessEdit(this.Name))
            //{
            //    return;
            //}
            //Conversion objCon = new Conversion();
            //ShowStudentInfoDetailForm(objCon.ConToInt64(gvMatCategory.GetFocusedRowCellValue("RId")));
            // gvMatCategory.GetFocusedRowCellValue("StudentInfoId");
        }

        private void PanaltyList_FormClosing(object sender, FormClosingEventArgs e)
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
            //if (e.KeyCode == Keys.Enter)
            //{
            //    if (!GlobleData.ManageRoleAccessEdit(this.Name))
            //    {
            //        return;
            //    }
            //    Conversion objCon = new Conversion();
            //    ShowStudentInfoDetailForm(objCon.ConToInt64(gvMatCategory.GetFocusedRowCellValue("RId")));
            //}
        }
    }
}