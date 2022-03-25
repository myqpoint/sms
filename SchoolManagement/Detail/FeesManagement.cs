using DebonoDLL.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DebonoDLL.App_Code.BOL;
using DebonoDLL;
using System.Data.SqlClient;
using DevExpress.XtraGrid.Views.Base;
using DEBONO.Helper;
using DevExpress.XtraEditors;
using Debono.Detail;
using System.Collections;
using DebonoDLL.BOL;
using System.IO;
using System.Diagnostics;
using System.Net;
using DEBONO;
using DevExpress.XtraGrid.Views.Grid;
using System.Configuration;
using Debono.Info;

namespace Debono.Detail
{
    public partial class FeesManagement : XtraForm
    {
        public DataTable dtopval;
        Int64 proddtloperationid = 0;
        Int64 proddtlsuboperationid = 0;
        private string _UserName;
        public string UserName
        {
            get { return _UserName; }
            set { _UserName = value; }
        }

        private static bool isfromorderform;

        public static bool Isfromorderform
        {
            get { return isfromorderform; }
            set { isfromorderform = value; }
        }

        private bool _isEditNew = true;
        DataTable dtProductDtl = new DataTable();
        String[] strProductToDelete = new string[250];
        Conversion objCon = new Conversion();

        public FeesManagement()
        {
            InitializeComponent();
            this._UserName = UserName;
            _isEditNew = true;
        }

        public FeesManagement(long ProductId)
        {
            InitializeComponent();
            this._UserName = UserName;
            _isEditNew = false;
            if (Isfromorderform)
            {
                GetFeesManagement();
                EnableDisableControls(true);
            }
        }
        bool chekDeletePermission()
        {
            if (GlobleData.ManageRoleAccessDeleteDetail(this.Name))
                return true;
            else
                return false;
        }
        bool chekEditPermission()
        {
            if (GlobleData.ManageRoleAccessEditDetail(this.Name))
                return true;
            else
                return false;
        }
       
        private void Product_Load(object sender, EventArgs e)
        {
            this._UserName = UserName;
            groupControl3.Visible = true;
            if (!chekDeletePermission() == true)
            {
                CM_Delete.Enabled = false;
            }
            GetFeesManagement();
            EnableDisableControls(false);
        }

        private void Product_Activated(object sender, EventArgs e)
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

        private void Product_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                if (toolStripButtonSave.Enabled)
                {
                    int nCheck = Debono.DebonoMsg.MsgCloseSaveConfirmation();
                    if (nCheck == 1)
                    {
                        toolStripButtonSave_ItemClick(null, null);
                        //if (this.ProductId <= 0)
                            e.Cancel = true;
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

        private void GetFeesManagement()
        {
            try
            {
                List<string> opvallist = new List<string>();
                DataTable dtopval = new DataTable();
                FormHelper.ShowWaitDialog();
                    LoadFeesManagements();
            }
            catch (Exception ex)
            {
                ExceptionManager.LogException(ex);
                 FormHelper.CloseWaitDialog();
            }
            FormHelper.CloseWaitDialog();
        }

        #region Load
       
       private void LoadFeesManagements()
        {
            try
            {
                FeesTypeBo objProductDtl = new FeesTypeBo();
                objProductDtl._SessionYear = ddlsession.Text;
                dtProductDtl = objProductDtl.LoadallFeesTypeWithClass();
                if (dtProductDtl.Rows.Count > 0)
                {
                    //Int64 AutoIncrementSeed = objCon.ConToInt64(dtProductDtl.Rows[dtProductDtl.Rows.Count - 1]["FId"]);
                    //dtProductDtl.Columns["FId"].AutoIncrement = true;
                    //dtProductDtl.Columns["FId"].AutoIncrementSeed = AutoIncrementSeed;
                    //dtProductDtl.Columns["FId"].AutoIncrementStep = 1;
                    //foreach (DataRow dr in dtProductDtl.Rows)
                    //{
                    //    dr["FId"] = AutoIncrementSeed++;
                    //}
                    GC_FeesManagement.DataSource = dtProductDtl;
                    EnableDisableControls(false);
                }
                else
                {
                    dtProductDtl = objProductDtl.LoadEmptyFeesType();
                    AddRowNo();
                    GC_FeesManagement.DataSource = dtProductDtl;
                    EnableDisableControls(true);
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.LogException(ex);
            }
        }

        private void AddRowNo()
        {
            try
            {
                dtProductDtl.Columns["FId"].AutoIncrement = true;
                dtProductDtl.Columns["FId"].AutoIncrementSeed = 1;
                dtProductDtl.Columns["FId"].AutoIncrementStep = 1;
                int i = 0;
                foreach (DataRow dr in dtProductDtl.Rows)
                {
                    ++i;
                    dr["FId"] = i;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.LogException(ex);
            }
        }

        #endregion

        private void EnableDisableControls(Boolean isEnable)
        {
            CM_Delete.Enabled = isEnable;
            gvFeesManagement.OptionsBehavior.Editable = isEnable;
            toolStripButtonSave.Enabled = isEnable;
        }

        #region save
        //save
        private void toolStripButtonSave_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                Conversion objCon = new Conversion();
                ColumnView view1 = GC_FeesManagement.FocusedView as ColumnView;
                view1.CloseEditor();
                view1.UpdateCurrentRow();
                #region validation
                if (dtProductDtl.Rows.Count == 0)
                {
                    Debono.DebonoMsg.MsgInformation("No record exist!!");
                    return;
                }
                for (int i = 0; i < dtProductDtl.Rows.Count; i++)
                {
                    if (string.IsNullOrEmpty(dtProductDtl.Rows[i]["ClassName"].ToString()))
                    {
                        Debono.DebonoMsg.MsgInformation("ClassName must be selected in grid!!");
                        return;
                    }
                    else
                    {
                        FeesTypeBo objraw = new FeesTypeBo();
                        objraw._ClassName = objCon.ConToStr(dtProductDtl.Rows[i]["ClassName"].ToString());
                        objraw._SessionYear = ddlsession.Text;
                         DataTable dtclass=objraw.LoadFeesTypeByClassName();
                         if (dtclass.Rows.Count > 0 && objCon.ConToStr(dtProductDtl.Rows[i]["FId"].ToString())=="")
                         {
                             Debono.DebonoMsg.MsgInformation("ClassName already exist!!");
                             return;
                         }
                    }
                }
                SaveFeesManagement();
                #endregion
            }
            catch (Exception ex)
            {
                ExceptionManager.LogException(ex);
            }
           FormHelper.CloseWaitDialog();
        }

        Boolean initl = true;

        private void SaveFeesManagement()
        {
            try
            {
                ColumnView view1 = GC_FeesManagement.FocusedView as ColumnView;
                view1.CloseEditor();
                view1.UpdateCurrentRow();
                Conversion objCon = new Conversion();

                //delete  ProductDt
                FeesTypeBo objProductDtl = new FeesTypeBo();
                for (int i = 0; i < dtProductDtl.Rows.Count; i++)
                {
                    objProductDtl._FId = objCon.ConToInt64(dtProductDtl.Rows[i]["FId"]);
                    objProductDtl._ClassName = objCon.ConToStr(dtProductDtl.Rows[i]["ClassName"]);
                    objProductDtl._GeneratorFee = objCon.ConToDec(dtProductDtl.Rows[i]["GeneratorFee"]);
                    objProductDtl._ComputerFee = objCon.ConToDec(dtProductDtl.Rows[i]["ComputerFee"]);
                    objProductDtl._TutionFee = objCon.ConToDec(dtProductDtl.Rows[i]["TutionFee"]);
                    objProductDtl._PTAFee = objCon.ConToDec(dtProductDtl.Rows[i]["PTAFee"]);
                    objProductDtl._ExamFees = objCon.ConToDec(dtProductDtl.Rows[i]["ExamFees"]);
                    objProductDtl._SessionYear = objCon.ConToStr(ddlsession.Text);
                    objProductDtl._CreatedBy = this._UserName;
                    if (checkFeesManagement(objProductDtl._FId))
                    {
                        objProductDtl._FId = 0;
                    }
                    if (objProductDtl._FId > 0)
                    {
                        objProductDtl.UpdateFeesType();
                    }
                    else
                    {
                        objProductDtl.SaveFeesType();
                    }
                }
                Debono.DebonoMsg.MsgInformation("Data Saved Successfully");
                EnableDisableControls(false);

            }
            catch (Exception ex)
            {
                ExceptionManager.LogException(ex);
            }
        }

        public bool checkFeesManagement(Int64 FId)
        {
            FeesTypeBo pro = new FeesTypeBo();
            Boolean str = true;
            pro._FId = FId;
            DataTable dt = pro.LoadallFeesTypeById();
            if(dt.Rows.Count>0)
            {
                str = false;
            }
            return str;
        }
       

        #endregion

        #region other button event
        //edit
        private void toolStripButtonEdit_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            groupControl3.Visible = true;
            GetFeesManagement();
            EnableDisableControls(true);
            FormHelper.CloseWaitDialog();
        }

        #endregion

        #region delete
        //delete
        String AutoDeleteMessage = "are you sure to delete";
        String AutoDeleteTitle = "Confirm";
        private void CM_Delete_Click(object sender, EventArgs e)
        {
            
            try
            {
                if (Debono.DebonoMsg.MsgDelete())
                {
                    Int64 nPid = objCon.ConToInt64(gvFeesManagement.GetFocusedRowCellValue("FId"));

                    FeesTypeBo objFeesManagementbo = new FeesTypeBo();
                    objFeesManagementbo._FId = nPid;
                    int msg = objFeesManagementbo.DeleteFeesType();
                    gvFeesManagement.DeleteSelectedRows();
                    gvFeesManagement.RefreshData();
                    dtProductDtl.AcceptChanges();
                }

            }
            catch (Exception ex)
            {
                ExceptionManager.LogException(ex);
            }
        }


        #endregion

        private void LoadOrSaveLayout(CommonFunction.custLayoutOptions enLayoutOptions)
        {
            CommonFunction objcmnFun = new CommonFunction();
            objcmnFun.LoadORSaveLayout(this.Name + "&", ref gvFeesManagement, enLayoutOptions);
        }

        private void GC_FeesManagement_ProcessGridKey(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (!GlobleData.ManageRoleAccessEdit(this.Name))
                {
                    return;
                }
                Conversion objCon = new Conversion();
                //ShowFeesManagementForm(objCon.ConToInt64(gvOrderDtl.GetFocusedRowCellValue("ProductId")));
            }
        }
        private void OpenForm(Form objFrom)
        {
           // objFrom.Show();
          FormHelper.OpenForm(objFrom, this.MdiParent);
        }

        private void ddlsession_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetFeesManagement();
        }
    }
}
