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
using Debono;
using DebonoDLL.BOL;

namespace DEBONO.Info
{
    public partial class UserRoleInfo : XtraForm
    {

        public UserRoleInfo()
        {
            InitializeComponent();
        }
        void chekadminoruser()
        {
            try
            {
                if (!GlobleData.ManageRoleAccessEdit(this.Name))
                {
                    toolStripButtonEdit.Enabled = false;
                    toolStripButtonHelp.Enabled = false;
                    barButtonNew.Enabled = false;
                }              
                if (!GlobleData.ManageRoleAccessDelete(this.Name))
                {
                    toolStripButtonDelete.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.LogException(ex);
            }
        }
        private void Propconfig_Load(object sender, EventArgs e)
        {
            chekadminoruser();

            //if (!GlobleData.ManageRoleAccessEdit(this.Name))
            //{
            //    ribbonControl2.Enabled = false;
            //}
            /*
            //created by ajay
            if (GlobleData.ManageRoleAccessDelete(this.Name))
            {
                ribbonControl2.Enabled = true;
                toolStripButtonDelete.Enabled = true;
                toolStripButtonEdit.Enabled = false;
                toolStripButtonHelp.Enabled = false;
                toolStripButtonNew.Enabled = false;               
            }

            //end creation
            */

            GetAllUserRole();
        }

        private void bindType()
        {
        }


        private void grdlpeProperty_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void toolStripButtonDelete_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Int64 roleId = objCon.ConToInt64(gvRole.GetFocusedRowCellValue("RoleId"));
            if (roleId > 0)
            {
                if (DebonoMsg.MsgDelete())
                {
                    UserRoleBo UserRoleBo = new UserRoleBo();
                    UserRoleBo._UserRoleId = roleId;
                    UserRoleBo.DeleteUserRole();
                }
            }
        }


        Conversion objCon = new Conversion();
        private void toolStripButtonEdit_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Int64 roleId = objCon.ConToInt64(gvRole.GetFocusedRowCellValue("UserRoleId"));
            UserRoleDetail objUserRole = new UserRoleDetail();
            objUserRole.RoleId = roleId;
            FormHelper.OpenForm(objUserRole, this.MdiParent);
        }

        private void barButtonNew_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Int64 roleId = objCon.ConToInt64(gvRole.GetFocusedRowCellValue("UserRoleId"));
            UserRoleDetail objUserRole = new UserRoleDetail();
            objUserRole.RoleId = 0;
            FormHelper.OpenForm(objUserRole, this.MdiParent);
        }

        private void GetAllUserRole()
        {
            UserRoleBo objUser = new UserRoleBo();
            DataTable dtUserRoles = objUser.GetAllUserRole();
            grdRole.DataSource = dtUserRoles;
        }

        private void gvRole_DoubleClick(object sender, EventArgs e)
        {
            if (!GlobleData.ManageRoleAccessEdit(this.Name))
            {
                return;
            }
            Int64 roleId = objCon.ConToInt64(gvRole.GetFocusedRowCellValue("UserRoleId"));
            UserRoleDetail objUserRole = new UserRoleDetail();
            objUserRole.RoleId = roleId;
            FormHelper.OpenForm(objUserRole, this.MdiParent);
        }

        private void UserRoleInfo_Activated(object sender, EventArgs e)
        {
            GetAllUserRole();
        }

        private void grdRole_ProcessGridKey(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (!GlobleData.ManageRoleAccessEdit(this.Name))
                {
                    return;
                }
                Int64 roleId = objCon.ConToInt64(gvRole.GetFocusedRowCellValue("UserRoleId"));
                UserRoleDetail objUserRole = new UserRoleDetail();
                objUserRole.RoleId = roleId;
                FormHelper.OpenForm(objUserRole, this.MdiParent);
            }
        }

    }
}
