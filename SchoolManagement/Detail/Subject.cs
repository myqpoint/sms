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

namespace Debono.Detail
{
    public partial class Subject : XtraForm
    {
        private string _UserName;
        public string UserName
        {
            get { return _UserName; }
            set { _UserName = value; }
        }
        private Int16 _SubjectID;
        public Int16 SubjectID
        {
            get { return _SubjectID; }
            set { _SubjectID = value; }
        }

        private bool _isEditNew = true;

        public Subject()
        {
            InitializeComponent();
            this._UserName = UserName;
            _isEditNew = true;
        }
       
        bool chekEditPermission()
        {
            if (GlobleData.ManageRoleAccessEditDetail(this.Name))
                return true;
            else
                return false;
        }
        bool chekDeletePermission()
        {
            if (GlobleData.ManageRoleAccessDeleteDetail(this.Name))
                return true;
            else
                return false;
        }
        private void OrdrDetail_Load(object sender, EventArgs e)
        {
            this._UserName = UserName;
            if (!chekDeletePermission() == true)
                toolStripButtonDelete.Enabled = false;
            if (!chekEditPermission() == true)
                toolStripButtonEdit.Enabled = false;
            GetOrderDetail();
        }

        private void OrdrDetail_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                if (toolStripButtonSave.Enabled)
                {
                    int nCheck = Debono.DebonoMsg.MsgCloseSaveConfirmation();
                    if (nCheck == 1)
                    {
                        toolStripButtonSave_ItemClick(null, null);
                    }
                    else if (nCheck == 3)
                    {
                        e.Cancel = true;
                    }
                }
                LoadOrSaveLayout(CommonFunction.custLayoutOptions.Save);
            }
            catch (Exception ex)
            {
                ExceptionManager.LogException(ex);
            }
        }

        private void OrdrDetail_Activated(object sender, EventArgs e)
        {
            try
            {
                LoadOrSaveLayout(CommonFunction.custLayoutOptions.Load);
            }
            catch (Exception ex)
            {
                ExceptionManager.LogException(ex);
            }
        }

        #region Load

        private void GetOrderDetail()
        {
            try
            {
                    FormHelper.ShowWaitDialog();
                    Conversion objcon = new Conversion();
                    LoadSubject();
                    EnableDisableControls(false);
                   FormHelper.CloseWaitDialog();

            }
            catch (Exception ex)
            {
                ExceptionManager.LogException(ex);
                FormHelper.CloseWaitDialog();
            }
        }

        #endregion

        private void EnableDisableControls(Boolean isEnable)
        {
            txtsubjectname.Enabled = isEnable;
            CM_Delete.Enabled = isEnable;
            toolStripButtonSave.Enabled = isEnable;
            if (!chekDeletePermission() == true)
                toolStripButtonDelete.Enabled = false;
            else
            toolStripButtonDelete.Enabled = !isEnable;
            if (!chekEditPermission() == true)
                toolStripButtonEdit.Enabled = false;
            else
            toolStripButtonEdit.Enabled = !isEnable;
            toolStripButtonNew.Enabled = !isEnable;
           // gvOrderDtl.OptionsBehavior.Editable = isEnable;

        }
        private void ClearValue()
        {
            StaffBo objOrder = new StaffBo();
            objOrder._StaffId = 0;
            txtsubjectname.Text = string.Empty;
        }

        #region Save
        private void toolStripButtonSave_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                if(string.IsNullOrEmpty(txtsubjectname.Text))
                { DebonoMsg.MsgInformation("Please fill the Subject"); return; }
                txtsubjectname.Text = System.Globalization.CultureInfo.CurrentCulture.TextInfo.ToTitleCase(txtsubjectname.Text.ToLower());
                Conversion objCon = new Conversion();
                int nCheck = 0;
                FormHelper.ShowWaitDialog();
                nCheck = SaveSubject();
                if (nCheck > 0)
                {
                    EnableDisableControls(false);
                }
              FormHelper.CloseWaitDialog();
                if(nCheck>0)
              DebonoMsg.MsgInformation("Data Saved Successfully");
                LoadSubject();
            }
            catch (Exception ex)
            {

                ExceptionManager.LogException(ex);
                FormHelper.CloseWaitDialog();
            }
        }

        private int SaveSubject()
        {
            Conversion objCon = new Conversion();
            try
            {
                SubjectBo objOrder = new SubjectBo();
                objOrder._SubjectID = objCon.ConToInt64(_SubjectID);
                objOrder._SubjectName = objCon.ConToStr(txtsubjectname.EditValue);
                    int nCheck = 0;
                    if (objOrder._SubjectID > 0)
                    {
                        nCheck = objOrder.UpdateSubjectMst();
                    }
                    else
                    {
                        nCheck = objOrder.SaveSubjectMst();
                        FormHelper.SetScreenNumbering("SubjectID");
                    }
                    return nCheck;
            }
            catch (SqlException ex)
            {
                return 0;
            }


        }

        #endregion

        #region Delete
        String AutoDeleteMessage = "are you sure to delete";
        String AutoDeleteTitle = "Confirm";
        private void toolStripButtonDelete_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Conversion objCon = new Conversion();
            try
            {
                if (Debono.DebonoMsg.MsgDelete())
                {
                    SubjectBo objOrderDtl = new SubjectBo();
                    //objOrderDtl._StaffId = this._StaffId;
                   // int nCheck = objOrderDtl.DeleteStaff();
                    //if (nCheck > 0)
                   // {
                     //   this._StaffId = 0;
                   // }
                    GetOrderDetail();
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.LogException(ex);
            }
            this.Close();
        }
        DataTable dtOrderDtl = new DataTable();
        private void CM_Delete_Click(object sender, EventArgs e)
        {
            try
            {
                if (Debono.DebonoMsg.MsgDelete())
                {
                    gvOrderDtl.DeleteSelectedRows();
                    gvOrderDtl.RefreshData();
                    dtOrderDtl.AcceptChanges();
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.LogException(ex);
            }

        }

        #endregion


        private void toolStripButtonCopy_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
           // this._StaffId = 0;
            EnableDisableControls(true);
        }

        private void toolStripButtonNew_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

            try
            {
                this._SubjectID = 0;
                ClearValue();
                GetOrderDetail();
                EnableDisableControls(true);
            }
            catch (Exception)
            {

              //  throw;
            }
        }

        private void toolStripButtonEdit_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            GetOrderDetail();
            EnableDisableControls(true);
        }

        private void LoadOrSaveLayout(CommonFunction.custLayoutOptions enLayoutOptions)
        {
            CommonFunction objcmnFun = new CommonFunction();
            objcmnFun.LoadORSaveLayout(this.Name + "&", ref gvOrderDtl, enLayoutOptions);
        }
        
        private void LoadSubject()
        {
            try
            {
                SubjectBo objOrderDetail = new SubjectBo();
                dtOrderDtl = objOrderDetail.ShowSubjectMst();
                Gc_orderdtl.DataSource = dtOrderDtl;

            }
            catch (Exception ex)
            {
                ExceptionManager.LogException(ex);
            }
        }
        private void groupControl3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Gc_orderdtl_DoubleClick(object sender, EventArgs e)
        {
            Conversion objCon = new Conversion();
            SubjectID=objCon.ConToInt16(gvOrderDtl.GetFocusedRowCellValue("SubjectID"));
            txtsubjectname.Text = objCon.ConToStr(gvOrderDtl.GetFocusedRowCellValue("SubjectName"));
        }
    }
}
