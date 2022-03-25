using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

using DebonoDLL.Helpers;
using DevExpress.XtraGrid.Views.Base;
using DebonoDLL;
using System.Data.SqlClient;
using System.Diagnostics;
using DebonoDLL.App_Code.BOL;
using DebonoDLL.BOL;
using System.IO;
using DEBONO;

namespace Debono
{
    public partial class UserRoleDetail : DevExpress.XtraEditors.XtraForm
    {
        public Int64 RoleId { get; set; }
        Conversion objCon = new Conversion();
        public UserRoleDetail()
        {
            InitializeComponent();
        }
        bool chekDeletePermission()
        {
            if (GlobleData.ManageRoleAccessDelete(this.Name))
                return true;
            else
                return false;
        }
        private void UserRole_Load(object sender, EventArgs e)
        {
            if (!chekDeletePermission() == true)
                toolStripButtonDelete.Enabled = false;
            BindScreenForRole();
            LoadUserRole();
            if (RoleId > 0)
            {
                EnableDisableControls(false);
            }
            else
            {
                EnableDisableControls(true);
            }
        }

        private void toolStripButtonEdit_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            EnableDisableControls(true);
        }

        private void toolStripButtonSave_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Int64 result = SaveRole();
            if (result > 0)
            {
                SaveRoleAccess();

                UserRole_Load(null, null);
                EnableDisableControls(false);
            }
        }

        private void toolStripButtonNew_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            RoleId = 0;
            UserRole_Load(null, null);
        }

        private void toolStripButtonCopy_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

        }

        private void toolStripButtonUndo_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

        }


        private void toolStripButtonDelete_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

        }

        private void RawMaterialDetail_FormClosing(object sender, FormClosingEventArgs e)
        {

            try
            {
                if (toolStripButtonSave.Enabled)
                {
                    int nCheck = Debono.DebonoMsg.MsgCloseSaveConfirmation();
                    if (nCheck == 1)
                    {
                        toolStripButtonNew_ItemClick(null, null);
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

        private void LoadOrSaveLayout(CommonFunction.custLayoutOptions enLayoutOptions)
        {
            CommonFunction objcmnFun = new CommonFunction();
            objcmnFun.LoadORSaveLayout(this.Name + "&", ref gvScreen, enLayoutOptions);
        }


        private void BindScreenForRole()
        {
            try
            {
                ScreenMasterBo objScreenMaster = new ScreenMasterBo();
                DataTable dtScreenMaster = objScreenMaster.GetAllScreenForRole(RoleId);
                grdScreen.DataSource = dtScreenMaster;
            }
            catch (Exception ex)
            {
                ExceptionManager.LogException(ex);
            }
        }

        private Int64 SaveRole()
        {
            try
            {
                UserRoleBo objUserRole = new UserRoleBo();
                objUserRole._RoleName = txtRoleName.Text;
                objUserRole._UserRoleId = RoleId;
                if (objUserRole._UserRoleId > 0)
                {
                    objUserRole.UpdateUserRole();
                }
                else
                {
                    objUserRole.SaveUserRole();
                    RoleId = objUserRole._UserRoleId;
                }
                return objUserRole._UserRoleId;
            }
            catch (Exception ex)
            {
                ExceptionManager.LogException(ex);
                return 0;
            }
        }

        private Int64 SaveRoleAccess()
        {
            try
            {
                ColumnView cView = grdScreen.FocusedView as ColumnView;
                cView.CloseEditor();
                cView.UpdateCurrentRow();
                DataTable dtScreenData = grdScreen.DataSource as DataTable;
                RoleAccessBo objRoleAccess = new RoleAccessBo();
                if (dtScreenData != null)
                {
                    foreach (DataRow dr in dtScreenData.Rows)
                    {
                        objRoleAccess._ScreenId = objCon.ConToInt64(dr["ScreenMasterId"]);
                        objRoleAccess._RoleId = RoleId;
                        if (objCon.ConTobool(dr["DeleteAccess"]) == true)
                        {
                            objRoleAccess._ViewAccess = true;
                            objRoleAccess._DeleteAccess = objCon.ConTobool(dr["DeleteAccess"]);
                        }
                        else
                        {
                            objRoleAccess._DeleteAccess = objCon.ConTobool(dr["DeleteAccess"]);
                            objRoleAccess._ViewAccess = objCon.ConTobool(dr["ViewAccess"]);
                        }
                        objRoleAccess._EditAccess = objCon.ConTobool(dr["EditAccess"]);
                        objRoleAccess._RoleAccessId = objCon.ConToInt64(dr["RoleAccessId"]);
                        objRoleAccess._EditLockAccess = objCon.ConTobool(dr["LockEditAccess"]);
                        if (objRoleAccess._RoleAccessId > 0)
                        {
                            objRoleAccess.UpdateRoleAccess();
                        }
                        else
                        {
                            objRoleAccess.SaveRoleAccess();
                        }

                    }
                }
                return 1;
            }
            catch (Exception ex)
            {
                ExceptionManager.LogException(ex);
                return 0;
            }
        }

        private void EnableDisableControls(Boolean isEnable)
        {
            toolStripButtonDocument.Enabled = isEnable;
            toolStripButtonSave.Enabled = isEnable;
            toolStripButtonStockBook.Enabled = isEnable;
            toolStripButtonUndo.Enabled = isEnable;

            if (!chekDeletePermission() == true)
                toolStripButtonDelete.Enabled = false;
            else
            toolStripButtonDelete.Enabled = !isEnable;
            toolStripButtonEdit.Enabled = !isEnable;
            toolStripButtonNext.Enabled = !isEnable;
            toolStripButtonNew.Enabled = !isEnable;
            toolStripButtonPrevious.Enabled = !isEnable;
            contextMenuStrip1.Enabled = isEnable;
            contextMenuStrip2.Enabled = isEnable;
            contextMenuStrip3.Enabled = isEnable;
            gvScreen.OptionsBehavior.Editable = isEnable;
            txtRoleName.Enabled = isEnable;
        }

        private void LoadUserRole()
        {
            UserRoleBo objUserRole = new UserRoleBo();
            objUserRole._UserRoleId = RoleId;
            objUserRole.LoadUserRole();
            txtRoleName.Text = objUserRole._RoleName;
        }





    }
}