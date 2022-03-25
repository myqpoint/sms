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
    public class RentMstBo
    {
        #region Field Properties


        ///<summary>
        ///RId
        ///<summary>
        ///<remarks>
        ///Property variable for RId
        ///<remarks>
        private Int64 RId;
        public Int64 _RId
        {
            get
            {
                return RId;
            }
            set
            {
                RId = value;
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
        ///Days
        ///<summary>
        ///<remarks>
        ///Property variable for Days
        ///<remarks>
        private Int64 Days;
        public Int64 _Days
        {
            get
            {
                return Days;
            }
            set
            {
                Days = value;
            }
        }


        ///<summary>
        ///Status
        ///<summary>
        ///<remarks>
        ///Property variable for Status
        ///<remarks>
        private Int16 Status;
        public Int16 _Status
        {
            get
            {
                return Status;
            }
            set
            {
                Status = value;
            }
        }

        ///<summary>
        ///IssueBy
        ///<summary>
        ///<remarks>
        ///Property variable for IssueBy
        ///<remarks>
        private String IssueBy;
        public String _IssueBy
        {
            get
            {
                return IssueBy;
            }
            set
            {
                IssueBy = value;
            }
        }
        ///<summary>
        ///IssueDate
        ///<summary>
        ///<remarks>
        ///Property variable for IssueDate
        ///<remarks>
        private DateTime IssueDate;
        public DateTime _IssueDate
        {
            get
            {
                return IssueDate;
            }
            set
            {
                IssueDate = value;
            }
        }
        ///<summary>
        ///ReturnBy
        ///<summary>
        ///<remarks>
        ///Property variable for ReturnBy
        ///<remarks>
        private String ReturnBy;
        public String _ReturnBy
        {
            get
            {
                return ReturnBy;
            }
            set
            {
                ReturnBy = value;
            }
        }
        ///<summary>
        ///ReturnDate
        ///<summary>
        ///<remarks>
        ///Property variable for ReturnDate
        ///<remarks>
        private DateTime ReturnDate;
        public DateTime _ReturnDate
        {
            get
            {
                return ReturnDate;
            }
            set
            {
                ReturnDate = value;
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
            RId = objCon.ConToInt64(drMainData["RId"]);
            BookId = objCon.ConToInt64(drMainData["BookId"]);
            SId = objCon.ConToInt64(drMainData["SId"]);
            Days = objCon.ConToInt64(drMainData["Days"]);
            Status = objCon.ConToInt16(drMainData["Status"]);
            IssueDate = objCon.ConToDT(drMainData["IssueDate"]);
            IssueBy = objCon.ConToStr(drMainData["IssueBy"]);
            ReturnDate = objCon.ConToDT(drMainData["ReturnDate"]);
            ReturnBy = objCon.ConToStr(drMainData["ReturnBy"]);
        }
        #endregion

        #region Save  functions

        //***********************************
        //This Function will perform the action for Saving the Data into Table  RentMst
        //***********************************
        public int SaveRentMst()
        {
            String strInsertQuery = "";
            SqlParameter[] param = new SqlParameter[14];
           
                strInsertQuery = "insert into RentMst(BookId,SId,Days,Status,IssueBy,IssueDate) " +
                   " values(@BookId,@SId,@Days,@Status,@IssueBy,getdate()) ";

            param[0] = new SqlParameter("@BookId", BookId);
            param[1] = new SqlParameter("@SId", SId);
            param[2] = new SqlParameter("@Days", Days);
            param[3] = new SqlParameter("@Status", 0);
            param[4] = new SqlParameter("@IssueBy", IssueBy);
            Dal objDal = new Dal();
            int check = 0;
            check = objDal.ExecuteDataIdentity(strInsertQuery, param);
            if (check == 1)
            {
                RId = objDal.ExecuteForID("select Max(RId) from RentMst");
            }
            return check;
        }
        #endregion

        #region Update functions

        //***********************************
        //This Function will perform the action for Updating the Data into Table  RentMst for Primary Column BookId
        //***********************************
        public int UpdateRentMst()
        {
            String strUpdateQuery ="";
            SqlParameter[] param = new SqlParameter[15];

            strUpdateQuery = "update RentMst Set Status=@Status,ReturnDate=getdate(),ReturnBy=@ReturnBy where BookId= @BookId and SId=@SId and Days=@Days";
            param[0] = new SqlParameter("@BookId", BookId);
            param[1] = new SqlParameter("@SId", SId);
            param[2] = new SqlParameter("@Days", Days);
            param[3] = new SqlParameter("@Status", 1);
            param[4] = new SqlParameter("@ReturnBy", ReturnBy);
               
            Dal objDal = new Dal();
            int check = 0;
            check = objDal.ExecuteDataIdentity(strUpdateQuery, param);
            return check;
        }

        public int UpdateBookRentQty()
        {
            String strUpdateQuery = "";
            SqlParameter[] param = new SqlParameter[15];

            strUpdateQuery = "update BookMst Set RentQty=isnull(RentQty,0)+1,AvailableQty=AvailableQty-1 where BookId= @BookId";
            param[0] = new SqlParameter("@BookId", BookId);
            Dal objDal = new Dal();
            int check = 0;
            check = objDal.ExecuteDataIdentity(strUpdateQuery, param);
            return check;
        }
        #endregion

        #region Delete functions

        //***********************************
        //This Function will perform the action for Deleteing the Data into Table  RentMst for Primary Column BookId
        //***********************************
        public int DeleteRentMst()
        {
            String strDeleteQuery = "Delete from RentMst where  RId = @RId ";
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@RId", RId);
            Dal objDal = new Dal();
            int check = 0;
            check = objDal.ExecuteDataIdentity(strDeleteQuery, param);
            return check;
        }

        #endregion

        #region Load functions

        //***********************************
        //This Function will perform the action for Loading the Data from Table  RentMst for Primary Column BookId
        //***********************************
        public void LoadRentMst()
        {
            String strLoadQuery = "Select * From RentMst where  RId = @RId ";


            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@RId", RId);

            Conversion objCon = new Conversion();
            Dal objDal = new Dal();
            DataTable dtRentMst = new DataTable();
            dtRentMst = objDal.ExecuteTable(strLoadQuery, param);
            if (dtRentMst.Rows.Count > 0)
            {

                RId = objCon.ConToInt64(dtRentMst.Rows[0]["RId"]);
                BookId = objCon.ConToInt64(dtRentMst.Rows[0]["BookId"]);
                SId = objCon.ConToInt64(dtRentMst.Rows[0]["SId"]);
                Days = objCon.ConToInt64(dtRentMst.Rows[0]["Days"]);
                Status = objCon.ConToInt16(dtRentMst.Rows[0]["Status"]);
                IssueDate = objCon.ConToDT(dtRentMst.Rows[0]["IssueDate"]);
                IssueBy = objCon.ConToStr(dtRentMst.Rows[0]["IssueBy"]);
                ReturnDate = objCon.ConToDT(dtRentMst.Rows[0]["ReturnDate"]);
                ReturnBy = objCon.ConToStr(dtRentMst.Rows[0]["ReturnBy"]);
            }
        }

        #endregion

        #region ShowAllData funtion

        //***********************************
        //This Function will perform the action for Loading the Data from Table  Tarif
        //***********************************
        public DataTable ShowRentMst()
        {
            String strLoadQuery = "select RM.RId,BM.BookId,SI.SId,BM.BookName,SI.FName+' '+isnull(LName,'') as StudentName,RM.Days,RM.IssueDate,RM.IssueBy from RentMst RM join BookMST BM on BM.BookId=RM.BookId join StudentInfo SI on SI.SId=RM.SId where ReturnDate is null order by  RM.RId asc";
            Conversion objCon = new Conversion();
            Dal objDal = new Dal();
            DataTable dtRentMst = new DataTable();
            dtRentMst = objDal.ExecuteTable(strLoadQuery);
            return dtRentMst;
        }
        public DataTable ShowRentMstByStudent()
        {
            String strLoadQuery = "select RM.RId,BM.BookId,SI.SId,BM.BookName,SI.FName+' '+isnull(LName,'') as StudentName,RM.Days,RM.IssueDate,RM.IssueBy from RentMst RM join BookMST BM on BM.BookId=RM.BookId join StudentInfo SI on SI.SId=RM.SId where ReturnDate is null and RM.SId=@SId order by  RM.RId asc";
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@SId", SId);

            Conversion objCon = new Conversion();
            Dal objDal = new Dal();
            DataTable dtRentMst = new DataTable();
            dtRentMst = objDal.ExecuteTable(strLoadQuery, param);
            return dtRentMst;
        }

        public DataTable ShowRentMstByStudentandbook()
        {
            String strLoadQuery = "select RM.RId,BM.BookId,SI.SId,BM.BookName,SI.FName+' '+isnull(LName,'') as StudentName,RM.Days,RM.IssueDate,RM.IssueBy,case when cast(dateadd(day,RM.Days,RM.issuedate) as Date)>=cast(getdate() as date) then 'No' else 'Yes' end as PanaltyStatus from RentMst RM join BookMST BM on BM.BookId=RM.BookId join StudentInfo SI on SI.SId=RM.SId where ReturnDate is null and RM.SId=@SId and RM.BookId=@BookId order by  RM.RId asc";
            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@SId", SId);
            param[1] = new SqlParameter("@BookId", BookId);
            Conversion objCon = new Conversion();
            Dal objDal = new Dal();
            DataTable dtRentMst = new DataTable();
            dtRentMst = objDal.ExecuteTable(strLoadQuery, param);
            return dtRentMst;
        }

        #endregion
        #endregion
    }

}