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
|| # Time:11:09:22	                                                     # ||
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

    public class ScreenMasterBo
    {
        #region Field Properties

        ///<summary>
        ///ScreenMasterId
        ///<summary>
        ///<remarks>
        ///Property variable for ScreenMasterId
        ///<remarks>
        private Int64 ScreenMasterId;
        public Int64 _ScreenMasterId
        {
            get
            {
                return ScreenMasterId;
            }
            set
            {
                ScreenMasterId = value;
            }
        }

        ///<summary>
        ///ScreenName
        ///<summary>
        ///<remarks>
        ///Property variable for ScreenName
        ///<remarks>
        private String ScreenName;
        public String _ScreenName
        {
            get
            {
                return ScreenName;
            }
            set
            {
                ScreenName = value;
            }
        }

        ///<summary>
        ///DisplayName
        ///<summary>
        ///<remarks>
        ///Property variable for DisplayName
        ///<remarks>
        private String DisplayName;
        public String _DisplayName
        {
            get
            {
                return DisplayName;
            }
            set
            {
                DisplayName = value;
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

            ScreenMasterId = objCon.ConToInt64(drMainData["ScreenMasterId"]);
            ScreenName = objCon.ConToStr(drMainData["ScreenName"]);
            DisplayName = objCon.ConToStr(drMainData["DisplayName"]);

        }
        #endregion
        #region Save  functions

        //***********************************
        //This Function will perform the action for Saving the Data into Table  ScreenMaster
        //***********************************
        public int SaveScreenMaster()
        {
            String strInsertQuery = "insert into ScreenMaster( ScreenName , DisplayName  ) " +
            " values( @ScreenName , @DisplayName  ) ";


            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@ScreenName", ScreenName);
            param[1] = new SqlParameter("@DisplayName", DisplayName);

            Dal objDal = new Dal();
            int check = 0;
            check = objDal.ExecuteDataIdentity(strInsertQuery, param);
            if (check == 1)
            {
                ScreenMasterId = objDal.ExecuteForID("select Max(ScreenMasterId) from ScreenMaster");
            }
            return check;
        }
        #endregion
        #region Update functions

        //***********************************
        //This Function will perform the action for Updating the Data into Table  ScreenMaster for Primary Column ScreenMasterId
        //***********************************
        public int UpdateScreenMaster()
        {
            String strUpdateQuery = "update ScreenMaster Set ScreenName = @ScreenName , DisplayName = @DisplayName  where ScreenMasterId= @ScreenMasterId";


            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@ScreenMasterId", ScreenMasterId);
            param[1] = new SqlParameter("@ScreenName", ScreenName);
            param[2] = new SqlParameter("@DisplayName", DisplayName);

            Dal objDal = new Dal();
            int check = 0;
            check = objDal.ExecuteDataIdentity(strUpdateQuery, param);
            return check;
        }
        #endregion
        #region Delete functions

        //***********************************
        //This Function will perform the action for Deleteing the Data into Table  ScreenMaster for Primary Column ScreenMasterId
        //***********************************
        public int DeleteScreenMaster()
        {
            String strDeleteQuery = "Delete From ScreenMaster where  ScreenMasterId = @ScreenMasterId ";


            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@ScreenMasterId", ScreenMasterId);

            Dal objDal = new Dal();
            int check = 0;
            check = objDal.ExecuteDataIdentity(strDeleteQuery, param);
            return check;
        }
        #endregion
        #region Load functions

        //***********************************
        //This Function will perform the action for Loading the Data from Table  ScreenMaster for Primary Column ScreenMasterId
        //***********************************
        public void LoadScreenMaster()
        {
            String strLoadQuery = "Select * From ScreenMaster where  ScreenMasterId = @ScreenMasterId ";

            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@ScreenMasterId", ScreenMasterId);

            Conversion objCon = new Conversion();
            Dal objDal = new Dal();
            DataTable dtScreenMaster = new DataTable();
            dtScreenMaster = objDal.ExecuteTable(strLoadQuery, param);
            if (dtScreenMaster.Rows.Count > 0)
            {
                ScreenMasterId = objCon.ConToInt64(dtScreenMaster.Rows[0]["ScreenMasterId"]);
                ScreenName = objCon.ConToStr(dtScreenMaster.Rows[0]["ScreenName"]);
                DisplayName = objCon.ConToStr(dtScreenMaster.Rows[0]["DisplayName"]);
            }
        }
        #endregion
        #endregion

        public DataTable GetAllScreenForRole(Int64 RoleId)
        {
            String strLoadQuery = "Select sm.*,ra.RoleAccessId, isnull(ra.ViewAccess,0) as ViewAccess, isnull(ra.EditAccess,0) as EditAccess ,  isnull(ra.DeleteAccess,0) as DeleteAccess,  isnull(ra.LockEditAccess,0) as LockEditAccess From ScreenMaster sm left outer join RoleAccess ra on sm.ScreenMasterId=ra.ScreenId and ra.RoleId=@RoleId ";

            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@RoleId", RoleId);
            Conversion objCon = new Conversion();
            Dal objDal = new Dal();
            DataTable dtScreenMaster = new DataTable();
            dtScreenMaster = objDal.ExecuteTable(strLoadQuery,param);
            return dtScreenMaster;
        }
    }
}