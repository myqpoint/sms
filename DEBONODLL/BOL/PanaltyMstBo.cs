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
    public class PanaltyMstBo
    {
        #region Field Properties


        ///<summary>
        ///PId
        ///<summary>
        ///<remarks>
        ///Property variable for PId
        ///<remarks>
        private Int64 PId;
        public Int64 _PId
        {
            get
            {
                return PId;
            }
            set
            {
                PId = value;
            }
        }

        ///<summary>
        ///BookId
        ///<summary>
        ///<remarks>
        ///Property variable for BookId
        ///<remarks>
        private Int64 BookId;
        public Int64 _BookId
        {
            get
            {
                return BookId;
            }
            set
            {
                BookId = value;
            }
        }

        ///<summary>
        ///SId
        ///<summary>
        ///<remarks>
        ///Property variable for SId
        ///<remarks>
        private Int64 SId;
        public Int64 _SId
        {
            get
            {
                return SId;
            }
            set
            {
                SId = value;
            }
        }

        ///<summary>
        ///PanaltyAmount
        ///<summary>
        ///<remarks>
        ///Property variable for PanaltyAmount
        ///<remarks>
        private decimal PanaltyAmount;
        public decimal _PanaltyAmount
        {
            get
            {
                return PanaltyAmount;
            }
            set
            {
                PanaltyAmount = value;
            }
        }


        ///<summary>
        ///Message
        ///<summary>
        ///<remarks>
        ///Property variable for Message
        ///<remarks>
        private String Message;
        public String _Message
        {
            get
            {
                return Message;
            }
            set
            {
                Message = value;
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

        #endregion

        #region Saving , Updating , Deleting and Loading functions
        #region AssignParameter  functions

        //***********************************
        //This Function will Assign the values to all properties from the DataRow. you Only need to pass DataRow for it. 
        //***********************************
        public void AssignVariableFromDataTable(DataRow drMainData)
        {
            Conversion objCon = new Conversion();
            PId = objCon.ConToInt64(drMainData["PId"]);
            BookId = objCon.ConToInt64(drMainData["BookId"]);
            SId = objCon.ConToInt64(drMainData["SId"]);
            PanaltyAmount = objCon.ConToDec(drMainData["PanaltyAmount"]);
            Message = objCon.ConToStr(drMainData["Message"]);
        }
        #endregion

        #region Save  functions

        //***********************************
        //This Function will perform the action for Saving the Data into Table  PanaltyMst
        //***********************************
        public int SavePanaltyMst()
        {
            String strInsertQuery = "";
            SqlParameter[] param = new SqlParameter[14];

            strInsertQuery = "insert into PanaltyMst(BookId,SId,PanaltyAmount,Message,CreatedBy,CreatedOn) " +
                   " values(@BookId,@SId,@PanaltyAmount,@Message,@CreatedBy,getdate()) ";

            param[0] = new SqlParameter("@BookId", BookId);
            param[1] = new SqlParameter("@SId", SId);
            param[2] = new SqlParameter("@PanaltyAmount", PanaltyAmount);
            param[3] = new SqlParameter("@Message", Message);
            param[4] = new SqlParameter("@CreatedBy", CreatedBy);
            Dal objDal = new Dal();
            int check = 0;
            check = objDal.ExecuteDataIdentity(strInsertQuery, param);
            if (check == 1)
            {
                PId = objDal.ExecuteForID("select Max(PId) from PanaltyMst");
            }
            return check;
        }
        #endregion

        #region Update functions

        //***********************************
        //This Function will perform the action for Updating the Data into Table  PanaltyMst for Primary Column BookId
        //***********************************
        

        public int UpdateBookRentQty()
        {
            String strUpdateQuery = "";
            SqlParameter[] param = new SqlParameter[15];

            strUpdateQuery = "update BookMst Set RentQty=isnull(RentQty,0)-1,AvailableQty=AvailableQty+1 where BookId= @BookId";
            param[0] = new SqlParameter("@BookId", BookId);
            Dal objDal = new Dal();
            int check = 0;
            check = objDal.ExecuteDataIdentity(strUpdateQuery, param);
            return check;
        }
        #endregion

        #region Delete functions

        //***********************************
        //This Function will perform the action for Deleteing the Data into Table  PanaltyMst for Primary Column BookId
        //***********************************
       

        #endregion

        #region Load functions

        //***********************************
        //This Function will perform the action for Loading the Data from Table  PanaltyMst for Primary Column BookId
        //***********************************
        public void LoadPanaltyMst()
        {
            String strLoadQuery = "Select * From PanaltyMst where  PId = @PId ";


            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@PId", PId);

            Conversion objCon = new Conversion();
            Dal objDal = new Dal();
            DataTable dtPanaltyMst = new DataTable();
            dtPanaltyMst = objDal.ExecuteTable(strLoadQuery, param);
            if (dtPanaltyMst.Rows.Count > 0)
            {

                PId = objCon.ConToInt64(dtPanaltyMst.Rows[0]["PId"]);
                BookId = objCon.ConToInt64(dtPanaltyMst.Rows[0]["BookId"]);
                SId = objCon.ConToInt64(dtPanaltyMst.Rows[0]["SId"]);
                PanaltyAmount = objCon.ConToDec(dtPanaltyMst.Rows[0]["PanaltyAmount"]);
                Message = objCon.ConToStr(dtPanaltyMst.Rows[0]["Message"]);
            }
        }

        #endregion

        #region ShowAllData funtion

        //***********************************
        //This Function will perform the action for Loading the Data from Table  Tarif
        //***********************************
        public DataTable ShowPanaltyMst()
        {
            String strLoadQuery = "select RM.PId,BM.BookName,SI.FName+' '+isnull(LName,'') as StudentName,RM.PanaltyAmount,RM.Message,RM.CreatedOn,RM.CreatedBy from panaltymst RM join BookMST BM on BM.BookId=RM.BookId join StudentInfo SI on SI.SId=RM.SId order by Rm.CreatedOn desc";
            Conversion objCon = new Conversion();
            Dal objDal = new Dal();
            DataTable dtPanaltyMst = new DataTable();
            dtPanaltyMst = objDal.ExecuteTable(strLoadQuery);
            return dtPanaltyMst;
        }
       

        #endregion
        #endregion
    }

}