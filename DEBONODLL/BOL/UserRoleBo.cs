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
|| # Time:11:08:11	                                                     # ||
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
using DebonoDLL.App_Code.BOL;
using DebonoDLL.App_Code.DAL;

#endregion

namespace DebonoDLL.BOL
{
    public class UserRoleBo
    {
        #region Field Properties

        ///<summary>
        ///UserRoleId
        ///<summary>
        ///<remarks>
        ///Property variable for UserRoleId
        ///<remarks>
        private Int64 UserRoleId;
        public Int64 _UserRoleId
        {
            get
            {
                return UserRoleId;
            }
            set
            {
                UserRoleId = value;
            }
        }

        ///<summary>
        ///RoleName
        ///<summary>
        ///<remarks>
        ///Property variable for RoleName
        ///<remarks>
        private String RoleName;
        public String _RoleName
        {
            get
            {
                return RoleName;
            }
            set
            {
                RoleName = value;
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

            UserRoleId = objCon.ConToInt64(drMainData["UserRoleId"]);
            RoleName = objCon.ConToStr(drMainData["RoleName"]);

        }
        #endregion
        #region Save  functions

        //***********************************
        //This Function will perform the action for Saving the Data into Table  UserRole
        //***********************************
        public int SaveUserRole()
        {
            String strInsertQuery = "insert into UserRole( RoleName  ) " +
            " values( @RoleName  ) ";


            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@RoleName", RoleName);

            Dal objDal = new Dal();
            int check = 0;
            check = objDal.ExecuteDataIdentity(strInsertQuery, param);
            if (check == 1)
            {
                UserRoleId = objDal.ExecuteForID("select Max(UserRoleId) from UserRole");
            }
            return check;
        }
        #endregion
        #region Update functions

        //***********************************
        //This Function will perform the action for Updating the Data into Table  UserRole for Primary Column UserRoleId
        //***********************************
        public int UpdateUserRole()
        {
            String strUpdateQuery = "update UserRole Set RoleName = @RoleName  where UserRoleId= @UserRoleId";


            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@UserRoleId", UserRoleId);
            param[1] = new SqlParameter("@RoleName", RoleName);

            Dal objDal = new Dal();
            int check = 0;
            check = objDal.ExecuteDataIdentity(strUpdateQuery, param);
            return check;
        }
        #endregion
        #region Delete functions

        //***********************************
        //This Function will perform the action for Deleteing the Data into Table  UserRole for Primary Column UserRoleId
        //***********************************
        public int DeleteUserRole()
        {
            String strDeleteQuery = "Delete From UserRole where  UserRoleId = @UserRoleId ";


            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@UserRoleId", UserRoleId);

            Dal objDal = new Dal();
            int check = 0;
            check = objDal.ExecuteDataIdentity(strDeleteQuery, param);
            return check;
        }
        #endregion
        #region Load functions

        //***********************************
        //This Function will perform the action for Loading the Data from Table  UserRole for Primary Column UserRoleId
        //***********************************
        public void LoadUserRole()
        {
            String strLoadQuery = "Select * From UserRole where  UserRoleId = @UserRoleId ";


            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@UserRoleId", UserRoleId);

            Conversion objCon = new Conversion();
            Dal objDal = new Dal();
            DataTable dtUserRole = new DataTable();
            dtUserRole = objDal.ExecuteTable(strLoadQuery, param);
            if (dtUserRole.Rows.Count > 0)
            {
                UserRoleId = objCon.ConToInt64(dtUserRole.Rows[0]["UserRoleId"]);
                RoleName = objCon.ConToStr(dtUserRole.Rows[0]["RoleName"]);
            }
        }
        #endregion
        #endregion

        public DataTable GetAllUserRole()
        {
            Dal objDal = new Dal();
            DataTable dtUserRole = objDal.ExecuteTable("Select * from UserRole");
            return dtUserRole;
        }
    }

}