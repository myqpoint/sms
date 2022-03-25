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
using System ;
using System.Collections.Generic ;
using System.Text ;
using System.Data.SqlClient ;
using System.Configuration ;
using System.Data ;
using DebonoDLL.App_Code.BOL;
using DebonoDLL.App_Code.DAL;

#endregion

namespace DebonoDLL.BOL
{
    public class RoleBo
    {
        #region Field Properties

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

        ///<summary>
        ///CreatedBy
        ///<summary>
        ///<remarks>
        ///Property variable for CreatedBy
        ///<remarks>
        private String CreatedBy;
        public String _CreatedBy
        {
            get
            {
                return CreatedBy;
            }
            set
            {
                CreatedBy = value;
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

            RoleId = objCon.ConToInt64(drMainData["RoleId"]);
            RoleName = objCon.ConToStr(drMainData["RoleName"]);
            CreatedBy = objCon.ConToStr(drMainData["CreatedBy"]);

        }
        #endregion
        #region Save  functions

        //***********************************
        //This Function will perform the action for Saving the Data into Table  Role
        //***********************************
        public int SaveRole()
        {
            String strInsertQuery = "insert into Role( RoleName , CreatedBy  ) " +
            " values( @RoleName , @CreatedBy  ) ";


            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@RoleName", RoleName);
            param[1] = new SqlParameter("@CreatedBy", CreatedBy);

            Dal objDal = new Dal();
            int check = 0;
            check = objDal.ExecuteDataIdentity(strInsertQuery, param);
            if (check == 1)
            {
                RoleId = objDal.ExecuteForID("select Max(RoleId) from Role");
            }
            return check;
        }
        #endregion
        #region Update functions

        //***********************************
        //This Function will perform the action for Updating the Data into Table  Role for Primary Column RoleId
        //***********************************
        public int UpdateRole()
        {
            String strUpdateQuery = "update Role Set RoleName = @RoleName , CreatedBy = @CreatedBy  where RoleId= @RoleId";


            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@RoleId", RoleId);
            param[1] = new SqlParameter("@RoleName", RoleName);
            param[2] = new SqlParameter("@CreatedBy", CreatedBy);

            Dal objDal = new Dal();
            int check = 0;
            check = objDal.ExecuteDataIdentity(strUpdateQuery, param);
            return check;
        }
        #endregion
        #region Delete functions

        //***********************************
        //This Function will perform the action for Deleteing the Data into Table  Role for Primary Column RoleId
        //***********************************
        public int DeleteRole()
        {
            String strDeleteQuery = "Delete From Role where  RoleId = @RoleId ";


            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@RoleId", RoleId);

            Dal objDal = new Dal();
            int check = 0;
            check = objDal.ExecuteDataIdentity(strDeleteQuery, param);
            return check;
        }
        #endregion
        #region Load functions

        //***********************************
        //This Function will perform the action for Loading the Data from Table  Role for Primary Column RoleId
        //***********************************
        public void LoadRole()
        {
            String strLoadQuery = "Select * From Role where  RoleId = @RoleId ";


            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@RoleId", RoleId);

            Conversion objCon = new Conversion();
            Dal objDal = new Dal();
            DataTable dtRole = new DataTable();
            dtRole = objDal.ExecuteTable(strLoadQuery, param);
            if (dtRole.Rows.Count > 0)
            {
                RoleId = objCon.ConToInt64(dtRole.Rows[0]["RoleId"]);
                RoleName = objCon.ConToStr(dtRole.Rows[0]["RoleName"]);
                CreatedBy = objCon.ConToStr(dtRole.Rows[0]["CreatedBy"]);
            }
        }
        #endregion
        #endregion
    }
}