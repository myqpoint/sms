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
    public class SubjectBo
    {
        #region Field Properties

        ///<summary>
        ///SubjectID
        ///<summary>
        ///<remarks>
        ///Property variable for SubjectID
        ///<remarks>
        private Int64 SubjectID;
        public Int64 _SubjectID
        {
            get
            {
                return SubjectID;
            }
            set
            {
                SubjectID = value;
            }
        }

        ///<summary>
        ///SubjectName
        ///<summary>
        ///<remarks>
        ///Property variable for SubjectName
        ///<remarks>
        private String SubjectName;
        public String _SubjectName
        {
            get
            {
                return SubjectName;
            }
            set
            {
                SubjectName = value;
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
        ///ModifiedBy
        ///<summary>
        ///<remarks>
        ///Property variable for ModifiedBy
        ///<remarks>
        private String ModifiedBy;
        public String _ModifiedBy
        {
            get
            {
                return ModifiedBy;
            }
            set
            {
                ModifiedBy = value;
            }
        }
        ///<summary>
        ///ModifiedOn
        ///<summary>
        ///<remarks>
        ///Property variable for ModifiedOn
        ///<remarks>
        private DateTime ModifiedOn;
        public DateTime _ModifiedOn
        {
            get
            {
                return ModifiedOn;
            }
            set
            {
                ModifiedOn = value;
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

            SubjectID = objCon.ConToInt64(drMainData["SubjectID"]);
            SubjectName = objCon.ConToStr(drMainData["SubjectName"]);
        }
        #endregion

        #region Save  functions

        //***********************************
        //This Function will perform the action for Saving the Data into Table  SubjectMst
        //***********************************
        public int SaveSubjectMst()
        {
            String strInsertQuery = "";
            SqlParameter[] param = new SqlParameter[8];
            strInsertQuery = "insert into SubjectMst(SubjectName) " +
                " values(@SubjectName) ";

            param[0] = new SqlParameter("@SubjectName", SubjectName);
            Dal objDal = new Dal();
            int check = 0;
            check = objDal.ExecuteDataIdentity(strInsertQuery, param);
            if (check == 1)
            {
                SubjectID = objDal.ExecuteForID("select Max(SubjectID) from SubjectMst");
            }
            return check;
        }
        #endregion

        #region Update functions

        //***********************************
        //This Function will perform the action for Updating the Data into Table  SubjectMst for Primary Column SubjectID
        //***********************************
        public int UpdateSubjectMst()
        {
            String strUpdateQuery ="";
            SqlParameter[] param = new SqlParameter[9];

            strUpdateQuery = "update SubjectMst Set SubjectName=@SubjectName where SubjectID= @SubjectID";

            param[0] = new SqlParameter("@SubjectID", SubjectID);
            param[1] = new SqlParameter("@SubjectName", SubjectName);
               
            Dal objDal = new Dal();
            int check = 0;
            check = objDal.ExecuteDataIdentity(strUpdateQuery, param);
            return check;
        }
        #endregion

        #region Delete functions

        //***********************************
        //This Function will perform the action for Deleteing the Data into Table  SubjectMst for Primary Column SubjectID
        //***********************************
        public int DeleteSubjectMst()
        {
            String strDeleteQuery = "Delete from SubjectMst where  SubjectID = @SubjectID ";
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@SubjectID", SubjectID);
            Dal objDal = new Dal();
            int check = 0;
            check = objDal.ExecuteDataIdentity(strDeleteQuery, param);
            return check;
        }

        #endregion

        #region Load functions

        //***********************************
        //This Function will perform the action for Loading the Data from Table  SubjectMst for Primary Column SubjectID
        //***********************************
        public void LoadSubjectMst()
        {
            String strLoadQuery = "Select * From SubjectMst where  SubjectID = @SubjectID ";


            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@SubjectID", SubjectID);

            Conversion objCon = new Conversion();
            Dal objDal = new Dal();
            DataTable dtSubjectMst = new DataTable();
            dtSubjectMst = objDal.ExecuteTable(strLoadQuery, param);
            if (dtSubjectMst.Rows.Count > 0)
            {
                SubjectID = objCon.ConToInt64(dtSubjectMst.Rows[0]["SubjectID"]);
                SubjectName = objCon.ConToStr(dtSubjectMst.Rows[0]["SubjectName"]);
            }
        }
        #endregion

        #region ShowAllData funtion

        //***********************************
        //This Function will perform the action for Loading the Data from Table  Tarif
        //***********************************
        public DataTable ShowSubjectMst()
        {
            String strLoadQuery = "Select * From SubjectMst order by  SubjectName asc";
            Conversion objCon = new Conversion();
            Dal objDal = new Dal();
            DataTable dtSubjectMst = new DataTable();
            dtSubjectMst = objDal.ExecuteTable(strLoadQuery);
            return dtSubjectMst;
        }

        #endregion
        #endregion
    }

}