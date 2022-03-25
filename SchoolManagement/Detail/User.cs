using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DebonoDLL.BOL;
using DevExpress.XtraGrid.Views.Base;
using DebonoDLL.Helpers;

using DevExpress.XtraGrid.Views.Grid;
using Debono;
using DebonoDLL.App_Code.DAL;
using DebonoDLL.App_Code.BOL;
using DebonoDLL;
using DEBONO.Helper;
using DEBONO;

namespace Debono.Detail
{
    public partial class User : DevExpress.XtraEditors.XtraForm
    {
        public User()
        {
            InitializeComponent();
        }

        private void toolStripButtonSave_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (!ValidateData())
            {
                DebonoMsg.MsgError("UserName can not be blank or Dulicate User Exist.!");
                return;
            }
            SaveAllUser();
            EnableDisableControl(false);
        }

        private void toolStripButtonEdit_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            EnableDisableControl(true);
        }
        bool chekDeletePermission()
        {
            if (GlobleData.ManageRoleAccessDelete(this.Name))
                return true;
            else
                return false;
        }
        private void User_Load(object sender, EventArgs e)
        {
            if (!chekDeletePermission() == true)
                toolStripButtonDelete.Enabled = false;
            if (!GlobleData.ManageRoleAccessEdit(this.Name))
            {               
                toolStripButtonEdit.Enabled = false;
                toolStripButtonHelp.Enabled = false;
                toolStripButtonNew.Enabled = false;
                toolStripButtonSave.Enabled = false;
                toolStripButtonUndo.Enabled = false;
            }
            LoadAllUser();
            BindUserRole();
            EnableDisableControl(false);

        }

     

        private void EnableDisableControl(Boolean isEnable)
        {
            gridView2.OptionsBehavior.Editable = isEnable;           
            if (!chekDeletePermission() == true)
                toolStripButtonDelete.Enabled = false;
            else
            toolStripButtonDelete.Enabled = isEnable;
            if (!GlobleData.ManageRoleAccessEdit(this.Name))
            {
                toolStripButtonEdit.Enabled = false;
                toolStripButtonHelp.Enabled = false;
                toolStripButtonNew.Enabled = false;
                toolStripButtonSave.Enabled = false;
                toolStripButtonUndo.Enabled = false;
            }
            else
            {
                toolStripButtonEdit.Enabled = !isEnable;
                toolStripButtonSave.Enabled = isEnable;
                toolStripButtonUndo.Enabled = isEnable;
            }
        }
        DataTable dtUser = new DataTable();
        private void LoadAllUser()
        {
            Dal objdal = new Dal();
            dtUser = objdal.ExecuteTable("Select * from UserMaster where IsActive=1");
            grdUser.DataSource = dtUser; 
        }

        private void SaveAllUser()
        {
            try
            {
                ColumnView view1 = grdUser.FocusedView as ColumnView;
                view1.CloseEditor();
                view1.UpdateCurrentRow();

                Conversion objCon = new Conversion();
                for (int i = 0; i < dtUser.Rows.Count; i++)
                {
                    UserMasterBo objUserMaster = new UserMasterBo();
                    objUserMaster.AssignVariableFromDataTable(dtUser.Rows[i]);
                    objUserMaster._IsActive = true;
                    if (objCon.ConToDec(objUserMaster._UserId) > 0)
                    {
                        objUserMaster.UpdateUserMaster();
                    }
                    else
                    {
                        objUserMaster.SaveUserMaster();
                    }
                }
                LoadAllUser();

            }
            catch (Exception ex)
            {
                ExceptionManager.LogException(ex);
            }
        }

        private bool ValidateData()
        {
            try
            {
                ColumnView view1 = grdUser.FocusedView as ColumnView;
                view1.CloseEditor();
                view1.UpdateCurrentRow();
                DataTable dtUserData = grdUser.DataSource as DataTable;
                Conversion objCon = new Conversion();
                dtUserData.AcceptChanges();
                if (dtUserData != null)
                {
                    foreach (DataRow dr in dtUserData.Rows)
                    {
                        if (String.IsNullOrEmpty(objCon.ConToStr(dr["UserName"]))) return false;
                    }

                    int mainCount1 = dtUserData.Rows.Count;
                    DataView view = new DataView(dtUserData);
                    DataTable dtUserData1 = view.ToTable(true, "UserName", "RoleId");
                    int mainCount2 = dtUserData1.Rows.Count;
                    if (mainCount1 == mainCount2)
                    {
                        return true;
                    }
                }
                return false;
            }
            catch (Exception ex)
            {
                ExceptionManager.LogException(ex);
                return false;
            }
        }

        private void toolStripButtonDelete_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                if (DebonoMsg.MsgDelete())
                {
                    Conversion objCon = new Conversion();
                    Int64 id = objCon.ConToInt64(gridView2.GetFocusedRowCellValue("UserId"));
                    UserMasterBo objUserMaster = new UserMasterBo();
                    objUserMaster._UserId = id;
                    int result = objUserMaster.DeleteUserMaster();
                    gridView2.DeleteSelectedRows();
                    grdUser.RefreshDataSource();
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.LogException(ex);
            }
        }

        private void User_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (toolStripButtonSave.Enabled)
            {
                int result = DebonoMsg.MsgCloseSaveConfirmation();
                if (result == 1)
                {
                    toolStripButtonSave_ItemClick(null, null);
                }
                if (result == 3)
                {
                    e.Cancel = true;
                }
            }
            LoadOrSaveLayout(CommonFunction.custLayoutOptions.Save, gridView2);
        }


        private void LoadOrSaveLayout(CommonFunction.custLayoutOptions enLayoutOptions, GridView gv)
        {
            CommonFunction objcmnFun = new CommonFunction();
            objcmnFun.LoadORSaveLayout(this.Name + "&", ref gv, enLayoutOptions);
        }

        private void User_Activated(object sender, EventArgs e)
        {
            LoadOrSaveLayout(CommonFunction.custLayoutOptions.Load, gridView2);
        }

        private void gridView2_CustomColumnDisplayText(object sender, CustomColumnDisplayTextEventArgs e)
        {
            if (e.Column.FieldName == "UserPassword")
            {
                e.DisplayText = "******";
            }
        }

        private void BindUserRole()
        {
            CommonLoadFunction.FillUserRole(repUserRole);
        }
    }
}