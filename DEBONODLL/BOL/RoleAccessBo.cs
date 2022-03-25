/*===========================================================================\
|| ######################################################################### ||
|| # Genrated By BO Generator						  					   # ||
|| # --------------------------------------------------------------------- # ||
|| # Copyright ©2009–2010 Prasad Solutions Pvt. Ltd. All Rights Reserved.  # ||
|| # This file may not be redistributed in whole or significant part. 	   # ||
|| # This application is only for internal use of the orgnasitation. 	   # ||
|| # ----------------BO Generator IS NOT FREE SOFTWARE ------------------  # ||
|| # --------------------------------------------------------------------  # ||
|| # Created On :PRASAD-22-PC         ;Created By :Prasad-22               # ||
|| # Date:07 June 2014                                                     # ||
|| # Time:11:08:12	                                                     # ||
|| # --------------------------------------------------------------------- # ||
|| # http://www.prasad-solutions.com | 	| info@prasad-solutions.com        # ||
|| # --------------------------------------------------------------------- # ||
|| ######################################################################### ||
\===========================================================================*/

#region Refrence Declration
using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using DebonoDLL.App_Code.DAL;
using DebonoDLL.App_Code.BOL;

#endregion

namespace DebonoDLL.BOL
{
    public class RoleAccessBo
    {
        #region Field Properties

        ///<summary>
        ///RoleAccessId
        ///<summary>
        ///<remarks>
        ///Property variable for RoleAccessId
        ///<remarks>
        private Int64 RoleAccessId;
        public Int64 _RoleAccessId
        {
            get
            {
                return RoleAccessId;
            }
            set
            {
                RoleAccessId = value;
            }
        }

        ///<summary>
        ///ScreenId
        ///<summary>
        ///<remarks>
        ///Property variable for ScreenId
        ///<remarks>
        private Int64 ScreenId;
        public Int64 _ScreenId
        {
            get
            {
                return ScreenId;
            }
            set
            {
                ScreenId = value;
            }
        }
        private Int64 RoleId;
        public Int64 _RoleId
        {
            get
            {
                return RoleId;
            }
            set
            {
                RoleId = value;
            }
        }

        ///<summary>
        ///ViewAccess
        ///<summary>
        ///<remarks>
        ///Property variable for ViewAccess
        ///<remarks>
        private Boolean DeleteAccess;
        public Boolean _DeleteAccess
        {
            get
            {
                return DeleteAccess;
            }
            set
            {
                DeleteAccess = value;
            }
        }

        ///<summary>
        ///ViewAccess
        ///<summary>
        ///<remarks>
        ///Property variable for ViewAccess
        ///<remarks>
        private Boolean ViewAccess;
        public Boolean _ViewAccess
        {
            get
            {
                return ViewAccess;
            }
            set
            {
                ViewAccess = value;
            }
        }

        ///<summary>
        ///EditLockAccess
        ///<summary>
        ///<remarks>
        ///Property variable for EditLockAccess
        ///<remarks>
        private Boolean EditLockAccess;
        public Boolean _EditLockAccess
        {
            get
            {
                return EditLockAccess;
            }
            set
            {
                EditLockAccess = value;
            }
        }
        ///<summary>
        ///EditAccess
        ///<summary>
        ///<remarks>
        ///Property variable for EditAccess
        ///<remarks>
        private Boolean EditAccess;
        public Boolean _EditAccess
        {
            get
            {
                return EditAccess;
            }
            set
            {
                EditAccess = value;
            }
        }
        #endregion
        #region Saving , Updating , Deleting and Loading functions
        #region AssignParameter  functions

        //***********************************
        //This Function will Assign the values to all properties from the DataRow. you Only need to pass DataRow for it. 
        //***********************************
        public void AssignVariableFromDataTable(DataRow drMainData)
        {
            Conversion objCon = new Conversion();

            RoleAccessId = objCon.ConToInt64(drMainData["RoleAccessId"]);
            ScreenId = objCon.ConToInt64(drMainData["ScreenId"]);
            ViewAccess = objCon.ConTobool(drMainData["ViewAccess"]);
            EditAccess = objCon.ConTobool(drMainData["EditAccess"]);
            RoleId = objCon.ConToInt64(drMainData["RoleId"]);
            DeleteAccess = objCon.ConTobool(drMainData["DeleteAccess"]);
            EditLockAccess = objCon.ConTobool(drMainData["LockEditAccess"]);
        }
        #endregion
        #region Save  functions

        //***********************************
        //This Function will perform the action for Saving the Data into Table  RoleAccess
        //***********************************
        public int SaveRoleAccess()
        {
            String strInsertQuery = "insert into RoleAccess( ScreenId , ViewAccess , EditAccess ,RoleId , DeleteAccess,LockEditAccess  ) " +
            " values( @ScreenId , @ViewAccess , @EditAccess ,@RoleId , @DeleteAccess,@EditLockAccess ) ";


            SqlParameter[] param = new SqlParameter[6];
            param[0] = new SqlParameter("@ScreenId", ScreenId);
            param[1] = new SqlParameter("@ViewAccess", ViewAccess);
            param[2] = new SqlParameter("@EditAccess", EditAccess);
            param[3] = new SqlParameter("@RoleId", RoleId);
            param[4] = new SqlParameter("@DeleteAccess", DeleteAccess);
            param[5] = new SqlParameter("@EditLockAccess", EditLockAccess);
            Dal objDal = new Dal();
            int check = 0;
            check = objDal.ExecuteDataIdentity(strInsertQuery, param);
            if (check == 1)
            {
                RoleAccessId = objDal.ExecuteForID("select Max(RoleAccessId) from RoleAccess");
            }
            return check;
        }
        #endregion
        #region Update functions

        //***********************************
        //This Function will perform the action for Updating the Data into Table  RoleAccess for Primary Column RoleAccessId
        //***********************************
        public int UpdateRoleAccess()
        {
            String strUpdateQuery = "update RoleAccess Set DeleteAccess=@DeleteAccess , ScreenId = @ScreenId , ViewAccess = @ViewAccess , EditAccess = @EditAccess ,RoleId=@RoleId,LockEditAccess=@EditLockAccess where RoleAccessId= @RoleAccessId";


            SqlParameter[] param = new SqlParameter[7];
            param[0] = new SqlParameter("@RoleAccessId", RoleAccessId);
            param[1] = new SqlParameter("@ScreenId", ScreenId);
            param[2] = new SqlParameter("@ViewAccess", ViewAccess);
            param[3] = new SqlParameter("@EditAccess", EditAccess);
            param[4] = new SqlParameter("@RoleId", RoleId);
            param[5] = new SqlParameter("@DeleteAccess", DeleteAccess);
            param[6] = new SqlParameter("@EditLockAccess", EditLockAccess);
            Dal objDal = new Dal();
            int check = 0;
            check = objDal.ExecuteDataIdentity(strUpdateQuery, param);
            return check;
        }
        #endregion
        #region Delete functions

        //***********************************
        //This Function will perform the action for Deleteing the Data into Table  RoleAccess for Primary Column RoleAccessId
        //***********************************
        public int DeleteRoleAccess()
        {
            String strDeleteQuery = "Delete From RoleAccess where  RoleAccessId = @RoleAccessId ";


            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@RoleAccessId", RoleAccessId);

            Dal objDal = new Dal();
            int check = 0;
            check = objDal.ExecuteDataIdentity(strDeleteQuery, param);
            return check;
        }
        #endregion
        #region Load functions

        //***********************************
        //This Function will perform the action for Loading the Data from Table  RoleAccess for Primary Column RoleAccessId
        //***********************************
        public void LoadRoleAccess()
        {
            String strLoadQuery = "Select * From RoleAccess where  RoleAccessId = @RoleAccessId ";


            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@RoleAccessId", RoleAccessId);

            Conversion objCon = new Conversion();
            Dal objDal = new Dal();
            DataTable dtRoleAccess = new DataTable();
            dtRoleAccess = objDal.ExecuteTable(strLoadQuery, param);
            if (dtRoleAccess.Rows.Count > 0)
            {
                RoleAccessId = objCon.ConToInt64(dtRoleAccess.Rows[0]["RoleAccessId"]);
                ScreenId = objCon.ConToInt64(dtRoleAccess.Rows[0]["ScreenId"]);
                ViewAccess = objCon.ConTobool(dtRoleAccess.Rows[0]["ViewAccess"]);
                EditAccess = objCon.ConTobool(dtRoleAccess.Rows[0]["EditAccess"]);
                EditLockAccess = objCon.ConTobool(dtRoleAccess.Rows[0]["LockEditAccess"]);
                RoleId = objCon.ConToInt64(dtRoleAccess.Rows[0]["RoleId"]);
            }
        }
        #endregion
        #endregion

        public DataTable GetAccessForRole()
        {
            String strLoadQuery = "Select * From RoleAccess ra left outer join ScreenMaster sm on sm.ScreenMasterId=ra.ScreenId where  RoleId = @RoleId "; 
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@RoleId", RoleId); 
            Conversion objCon = new Conversion();
            Dal objDal = new Dal();
            DataTable dtRoleAccess = new DataTable();
            dtRoleAccess = objDal.ExecuteTable(strLoadQuery, param);
            return dtRoleAccess; 
        }

    }

}