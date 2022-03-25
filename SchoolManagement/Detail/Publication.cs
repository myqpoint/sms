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
    public partial class Publication : XtraForm
    {
        private string _UserName;
        public string UserName
        {
            get { return _UserName; }
            set { _UserName = value; }
        }

        private bool _isEditNew = true;
        
        private Int64 _PID;
        public Int64 PID
        {
            get { return _PID; }
            set { _PID = value; }
        }

        public Publication()
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
                        //if (this.PID <= 0)
                        //    e.Cancel = true;
                    }
                    else if (nCheck == 2)
                    {

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
                    LoadPublications();
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
            txtpublicationname.Enabled = isEnable;
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
            //gvOrderDtl.OptionsBehavior.Editable = isEnable;

        }
        private void ClearValue()
        {
            PublicationBo objOrder = new PublicationBo();
            objOrder._PID = 0;
            txtpublicationname.Text = string.Empty;
        }

        #region Save
        private void toolStripButtonSave_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                if(string.IsNullOrEmpty(txtpublicationname.Text))
                { DebonoMsg.MsgInformation("Please fill the Publication"); return; }
                txtpublicationname.Text = System.Globalization.CultureInfo.CurrentCulture.TextInfo.ToTitleCase(txtpublicationname.Text.ToLower());
                Conversion objCon = new Conversion();
                int nCheck = 0;
                FormHelper.ShowWaitDialog();
                nCheck = SavePublication();
                if (nCheck > 0)
                {
                    EnableDisableControls(false);
                }
              FormHelper.CloseWaitDialog();
                if(nCheck>0)
              DebonoMsg.MsgInformation("Data Saved Successfully");
                LoadPublications();
            }
            catch (Exception ex)
            {

                ExceptionManager.LogException(ex);
                FormHelper.CloseWaitDialog();
            }
        }

        private int SavePublication()
        {
            Conversion objCon = new Conversion();
            try
            {
                PublicationBo objOrder = new PublicationBo();
                objOrder._PID = objCon.ConToInt64(_PID);
                objOrder._Publication = objCon.ConToStr(txtpublicationname.EditValue);
                objOrder._CreatedBy = this._UserName;
                objOrder._ModifiedBy = this._UserName;
                    int nCheck = 0;
                    if (objOrder._PID > 0)
                    {
                        nCheck = objOrder.UpdatePublicationMst();
                    }
                    else
                    {
                        nCheck = objOrder.SavePublicationMst();
                        this._PID = objOrder._PID;
                        FormHelper.SetScreenNumbering("PID");
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
                    PublicationBo objOrderDtl = new PublicationBo();
                    objOrderDtl._PID = this._PID;
                    int nCheck = objOrderDtl.DeletePublicationMst();
                    if (nCheck > 0)
                    {
                        this._PID = 0;
                    }
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
            this._PID = 0;
            EnableDisableControls(true);
        }

        private void toolStripButtonNew_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

            try
            {
                this._PID = 0;
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


        private void LoadPublications()
        {
            try
            {
                PublicationBo objOrderDetail = new PublicationBo();
                dtOrderDtl = objOrderDetail.ShowPublicationMst();
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

        private void gvOrderDtl_DoubleClick(object sender, EventArgs e)
        {
            Conversion objCon = new Conversion();
            PID = objCon.ConToInt16(gvOrderDtl.GetFocusedRowCellValue("PID"));
            txtpublicationname.Text = objCon.ConToStr(gvOrderDtl.GetFocusedRowCellValue("Publication"));
        }
    }
}
