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
    public class PublicationBo
    {
        #region Field Properties

        ///<summary>
        ///PID
        ///<summary>
        ///<remarks>
        ///Property variable for PID
        ///<remarks>
        private Int64 PID;
        public Int64 _PID
        {
            get
            {
                return PID;
            }
            set
            {
                PID = value;
            }
        }

        ///<summary>
        ///Publication
        ///<summary>
        ///<remarks>
        ///Property variable for Publication
        ///<remarks>
        private String Publication;
        public String _Publication
        {
            get
            {
                return Publication;
            }
            set
            {
                Publication = value;
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

            PID = objCon.ConToInt64(drMainData["PID"]);
            Publication = objCon.ConToStr(drMainData["Publication"]);
        }
        #endregion

        #region Save  functions

        //***********************************
        //This Function will perform the action for Saving the Data into Table  PublicationMst
        //***********************************
        public int SavePublicationMst()
        {
            String strInsertQuery = "";
            SqlParameter[] param = new SqlParameter[8];
            strInsertQuery = "insert into PublicationMst(Publication) " +
                " values(@Publication) ";

            param[0] = new SqlParameter("@Publication", Publication);
            Dal objDal = new Dal();
            int check = 0;
            check = objDal.ExecuteDataIdentity(strInsertQuery, param);
            if (check == 1)
            {
                PID = objDal.ExecuteForID("select Max(PID) from PublicationMst");
            }
            return check;
        }
        #endregion

        #region Update functions

        //***********************************
        //This Function will perform the action for Updating the Data into Table  PublicationMst for Primary Column PID
        //***********************************
        public int UpdatePublicationMst()
        {
            String strUpdateQuery ="";
            SqlParameter[] param = new SqlParameter[9];

            strUpdateQuery = "update PublicationMst Set Publication=@Publication where PID= @PID";

            param[0] = new SqlParameter("@PID", PID);
            param[1] = new SqlParameter("@Publication", Publication);
               
            Dal objDal = new Dal();
            int check = 0;
            check = objDal.ExecuteDataIdentity(strUpdateQuery, param);
            return check;
        }
        #endregion

        #region Delete functions

        //***********************************
        //This Function will perform the action for Deleteing the Data into Table  PublicationMst for Primary Column PID
        //***********************************
        public int DeletePublicationMst()
        {
            String strDeleteQuery = "Delete from PublicationMst where  PID = @PID ";
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@PID", PID);
            Dal objDal = new Dal();
            int check = 0;
            check = objDal.ExecuteDataIdentity(strDeleteQuery, param);
            return check;
        }

        #endregion

        #region Load functions

        //***********************************
        //This Function will perform the action for Loading the Data from Table  PublicationMst for Primary Column PID
        //***********************************
        public void LoadPublicationMst()
        {
            String strLoadQuery = "Select * From PublicationMst where  PID = @PID ";


            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@PID", PID);

            Conversion objCon = new Conversion();
            Dal objDal = new Dal();
            DataTable dtPublicationMst = new DataTable();
            dtPublicationMst = objDal.ExecuteTable(strLoadQuery, param);
            if (dtPublicationMst.Rows.Count > 0)
            {
                PID = objCon.ConToInt64(dtPublicationMst.Rows[0]["PID"]);
                Publication = objCon.ConToStr(dtPublicationMst.Rows[0]["Publication"]);
            }
        }
        #endregion

        #region ShowAllData funtion

        //***********************************
        //This Function will perform the action for Loading the Data from Table  Tarif
        //***********************************
        public DataTable ShowPublicationMst()
        {
            String strLoadQuery = "Select * From PublicationMst order by  Publication asc";
            Conversion objCon = new Conversion();
            Dal objDal = new Dal();
            DataTable dtPublicationMst = new DataTable();
            dtPublicationMst = objDal.ExecuteTable(strLoadQuery);
            return dtPublicationMst;
        }

        #endregion
        #endregion
    }

}