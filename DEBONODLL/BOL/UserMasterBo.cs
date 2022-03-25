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
    public class UserMasterBo
    {
        #region Field Properties

        ///<summary>
        ///UserId
        ///<summary>
        ///<remarks>
        ///Property variable for UserId
        ///<remarks>
        private Int64 UserId;
        public Int64 _UserId
        {
            get
            {
                return UserId;
            }
            set
            {
                UserId = value;
            }
        }

        ///<summary>
        ///RoleId
        ///<summary>
        ///<remarks>
        ///Property variable for RoleId
        ///<remarks>
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
        ///UserName
        ///<summary>
        ///<remarks>
        ///Property variable for UserName
        ///<remarks>
        private String UserName;
        public String _UserName
        {
            get
            {
                return UserName;
            }
            set
            {
                UserName = value;
            }
        }

        ///<summary>
        ///UserPassword
        ///<summary>
        ///<remarks>
        ///Property variable for UserPassword
        ///<remarks>
        private String UserPassword;
        public String _UserPassword
        {
            get
            {
                return UserPassword;
            }
            set
            {
                UserPassword = value;
            }
        }

        ///<summary>
        ///CreatedOn
        ///<summary>
        ///<remarks>
        ///Property variable for CreatedOn
        ///<remarks>
        private DateTime CreatedOn;
        public DateTime _CreatedOn
        {
            get
            {
                return CreatedOn;
            }
            set
            {
                CreatedOn = value;
            }
        }

        ///<summary>
        ///IsActive
        ///<summary>
        ///<remarks>
        ///Property variable for IsActive
        ///<remarks>
        private Boolean IsActive;
        public Boolean _IsActive
        {
            get
            {
                return IsActive;
            }
            set
            {
                IsActive = value;
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

            UserId = objCon.ConToInt64(drMainData["UserId"]);
            RoleId = objCon.ConToInt64(drMainData["RoleId"]);
            UserName = objCon.ConToStr(drMainData["UserName"]);
            UserPassword = objCon.ConToStr(drMainData["UserPassword"]);
            CreatedOn = objCon.ConToDT(drMainData["CreatedOn"]);
            IsActive = objCon.ConTobool(drMainData["IsActive"]);

        }
        #endregion
        #region Save  functions

        //***********************************
        //This Function will perform the action for Saving the Data into Table  UserMaster
        //***********************************
        public int SaveUserMaster()
        {
            String strInsertQuery = "insert into UserMaster( RoleId , UserName , UserPassword , CreatedOn , IsActive  ) " +
            " values( @RoleId , @UserName , @UserPassword , @CreatedOn , @IsActive  ) ";


            SqlParameter[] param = new SqlParameter[5];
            param[0] = new SqlParameter("@RoleId", RoleId);
            param[1] = new SqlParameter("@UserName", UserName);
            param[2] = new SqlParameter("@UserPassword", UserPassword);
            param[3] = new SqlParameter("@CreatedOn", CreatedOn);
            param[4] = new SqlParameter("@IsActive", IsActive);

            Dal objDal = new Dal();
            int check = 0;
            check = objDal.ExecuteDataIdentity(strInsertQuery, param);
            if (check == 1)
            {
                UserId = objDal.ExecuteForID("select Max(UserId) from UserMaster");
            }
            return check;
        }
        #endregion
        #region Update functions

        //***********************************
        //This Function will perform the action for Updating the Data into Table  UserMaster for Primary Column UserId
        //***********************************
        public int UpdateUserMaster()
        {
            String strUpdateQuery = "update UserMaster Set RoleId = @RoleId , UserName = @UserName , UserPassword = @UserPassword , CreatedOn = @CreatedOn , IsActive = @IsActive  where UserId= @UserId";


            SqlParameter[] param = new SqlParameter[6];
            param[0] = new SqlParameter("@UserId", UserId);
            param[1] = new SqlParameter("@RoleId", RoleId);
            param[2] = new SqlParameter("@UserName", UserName);
            param[3] = new SqlParameter("@UserPassword", UserPassword);
            param[4] = new SqlParameter("@CreatedOn", CreatedOn);
            param[5] = new SqlParameter("@IsActive", IsActive);

            Dal objDal = new Dal();
            int check = 0;
            check = objDal.ExecuteDataIdentity(strUpdateQuery, param);
            return check;
        }
        #endregion
        #region Delete functions

        //***********************************
        //This Function will perform the action for Deleteing the Data into Table  UserMaster for Primary Column UserId
        //***********************************
        public int DeleteUserMaster()
        {
            String strDeleteQuery = "Delete From UserMaster where  UserId = @UserId ";


            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@UserId", UserId);

            Dal objDal = new Dal();
            int check = 0;
            check = objDal.ExecuteDataIdentity(strDeleteQuery, param);
            return check;
        }
        #endregion
        #region Load functions

        //***********************************
        //This Function will perform the action for Loading the Data from Table  UserMaster for Primary Column UserId
        //***********************************
        public void LoadUserMaster()
        {
            String strLoadQuery = "Select * From UserMaster where  UserId = @UserId ";


            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@UserId", UserId);

            Conversion objCon = new Conversion();
            Dal objDal = new Dal();
            DataTable dtUserMaster = new DataTable();
            dtUserMaster = objDal.ExecuteTable(strLoadQuery, param);
            if (dtUserMaster.Rows.Count > 0)
            {
                UserId = objCon.ConToInt64(dtUserMaster.Rows[0]["UserId"]);
                RoleId = objCon.ConToInt64(dtUserMaster.Rows[0]["RoleId"]);
                UserName = objCon.ConToStr(dtUserMaster.Rows[0]["UserName"]);
                UserPassword = objCon.ConToStr(dtUserMaster.Rows[0]["UserPassword"]);
                CreatedOn = objCon.ConToDT(dtUserMaster.Rows[0]["CreatedOn"]);
                IsActive = objCon.ConTobool(dtUserMaster.Rows[0]["IsActive"]);
            }
        }
        #endregion
        #endregion

        public Boolean ValidateUser()
        {
            String strLoadQuery = "Select * From UserMaster where  UserName = @UserName and  UserPassword=@UserPassword";
            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@UserName", UserName);
            param[1] = new SqlParameter("@UserPassword", UserPassword);

            Conversion objCon = new Conversion();
            Dal objDal = new Dal();
            DataTable dtUserMaster = new DataTable();
            dtUserMaster = objDal.ExecuteTable(strLoadQuery, param);
            if (dtUserMaster.Rows.Count > 0)
            {
                UserId = objCon.ConToInt64(dtUserMaster.Rows[0]["UserId"]);
                RoleId = objCon.ConToInt64(dtUserMaster.Rows[0]["RoleId"]);
                UserName = objCon.ConToStr(dtUserMaster.Rows[0]["UserName"]);
                UserPassword = objCon.ConToStr(dtUserMaster.Rows[0]["UserPassword"]);
                CreatedOn = objCon.ConToDT(dtUserMaster.Rows[0]["CreatedOn"]);
                IsActive = objCon.ConTobool(dtUserMaster.Rows[0]["IsActive"]);
                return true;
            }
            return false;
        }
    }
}