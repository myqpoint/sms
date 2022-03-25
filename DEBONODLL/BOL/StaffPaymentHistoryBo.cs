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
    public class StaffPaymentHistoryBo
    {
        #region Field Properties


        ///<summary>
        ///SPId
        ///<summary>
        ///<remarks>
        ///Property variable for SPId
        ///<remarks>
        private Int64 SPId;
        public Int64 _SPId
        {
            get
            {
                return SPId;
            }
            set
            {
                SPId = value;
            }
        }

        ///<summary>
        ///StaffId
        ///<summary>
        ///<remarks>
        ///Property variable for StaffId
        ///<remarks>
        private Int64 StaffId;
        public Int64 _StaffId
        {
            get
            {
                return StaffId;
            }
            set
            {
                StaffId = value;
            }
        }

        ///<summary>
        ///PaidAmount
        ///<summary>
        ///<remarks>
        ///Property variable for PaidAmount
        ///<remarks>
        private Decimal PaidAmount;
        public Decimal _PaidAmount
        {
            get
            {
                return PaidAmount;
            }
            set
            {
                PaidAmount = value;
            }
        }

        ///<summary>
        ///PaymentDate
        ///<summary>
        ///<remarks>
        ///Property variable for PaymentDate
        ///<remarks>
        private DateTime PaymentDate;
        public DateTime _PaymentDate
        {
            get
            {
                return PaymentDate;
            }
            set
            {
                PaymentDate = value;
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
            SPId = objCon.ConToInt64(drMainData["SPId"]);
            StaffId = objCon.ConToInt64(drMainData["StaffId"]);
            PaidAmount = objCon.ConToDec(drMainData["PaidAmount"]);
            PaymentDate = objCon.ConToDT(drMainData["PaymentDate"]);
            CreatedBy = objCon.ConToStr(drMainData["CreatedBy"]);
            CreatedOn = objCon.ConToDT(drMainData["CreatedOn"]);
            ModifiedBy = objCon.ConToStr(drMainData["ModifiedBy"]);
            ModifiedOn = objCon.ConToDT(drMainData["ModifiedOn"]);
        }
        #endregion

        #region Save  functions

        //***********************************
        //This Function will perform the action for Saving the Data into Table  StaffPaymentHistory
        //***********************************
        public int SaveStaffPaymentHistory()
        {
            String strInsertQuery = "";
            SqlParameter[] param = new SqlParameter[7];
                strInsertQuery = "insert into StaffPaymentHistory( StaffId,PaidAmount,PaymentDate,CreatedOn,CreatedBy  ) " +
                " values( @StaffId,@PaidAmount,@PaymentDate,@CreatedOn,@CreatedBy  ) ";

                param[0] = new SqlParameter("@StaffId", StaffId);
                param[1] = new SqlParameter("@PaidAmount", PaidAmount);
                param[2] = new SqlParameter("@PaymentDate", PaymentDate);
                param[3] = new SqlParameter("@CreatedOn", DateTime.Now.Date);
                param[4] = new SqlParameter("@CreatedBy", CreatedBy);
            
            Dal objDal = new Dal();
            int check = 0;
            check = objDal.ExecuteDataIdentity(strInsertQuery, param);
            if (check == 1)
            {
                SPId = objDal.ExecuteForID("select Max(SPId) from StaffPaymentHistory");
            }
            return check;
        }
        #endregion

        #region Update functions

        //***********************************
        //This Function will perform the action for Updating the Data into Table  StaffPaymentHistory for Primary Column SId
        //***********************************
        public int UpdateStaffPaymentHistory()
        {
            String strUpdateQuery ="";
            SqlParameter[] param = new SqlParameter[7];

            strUpdateQuery = "update StaffPaymentHistory Set PaidAmount=@PaidAmount,PaymentDate=@PaymentDate,ModifiedOn=@ModifiedOn,ModifiedBy=@ModifiedBy where SPId= @SPId";

                param[0] = new SqlParameter("@SPId", SPId);
                param[2] = new SqlParameter("@PaidAmount", PaidAmount);
                param[3] = new SqlParameter("@PaymentDate", PaymentDate);
                param[4] = new SqlParameter("@ModifiedOn", DateTime.Now.Date);
                param[5] = new SqlParameter("@ModifiedBy", CreatedBy);
            Dal objDal = new Dal();
            int check = 0;
            check = objDal.ExecuteDataIdentity(strUpdateQuery, param);
            return check;
        }
        #endregion

        #region Delete functions

        //***********************************
        //This Function will perform the action for Deleteing the Data into Table  StaffPaymentHistory for Primary Column SId
        //***********************************
        public int DeleteStaffPaymentHistory()
        {
            String strDeleteQuery = "Delete from StaffPaymentHistory where  SPId = @SPId ";
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@SPId", SPId);
            Dal objDal = new Dal();
            int check = 0;
            check = objDal.ExecuteDataIdentity(strDeleteQuery, param);
            return check;
        }

        #endregion

        #region Load functions

        //***********************************
        //This Function will perform the action for Loading the Data from Table  StaffPaymentHistory for Primary Column SId
        //***********************************
        public void LoadStaffPaymentHistory()
        {
            String strLoadQuery = "Select * From StaffPaymentHistory where  SPId = @SPId ";


            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@SPId", SPId);

            Conversion objCon = new Conversion();
            Dal objDal = new Dal();
            DataTable dtStaffPaymentHistory = new DataTable();
            dtStaffPaymentHistory = objDal.ExecuteTable(strLoadQuery, param);
            if (dtStaffPaymentHistory.Rows.Count > 0)
            {
                StaffId = objCon.ConToInt64(dtStaffPaymentHistory.Rows[0]["StaffId"]);
                PaidAmount = objCon.ConToDec(dtStaffPaymentHistory.Rows[0]["PaidAmount"]);
                PaymentDate = objCon.ConToDT(dtStaffPaymentHistory.Rows[0]["PaymentDate"]);
                CreatedOn = objCon.ConToDT(dtStaffPaymentHistory.Rows[0]["CreatedOn"]);
                CreatedBy = objCon.ConToStr(dtStaffPaymentHistory.Rows[0]["CreatedBy"]);
            }
        }

        #endregion

        #region ShowAllData funtion

        //***********************************
        //This Function will perform the action for Loading the Data from Table  Tarif
        //***********************************
        public DataTable ShowStaffPaymentHistory()
        {
            String strLoadQuery = "Select *,DATENAME(MONTH, PaymentDate) as MonthName From StaffPaymentHistory where  StaffId = @StaffId  order by PaymentDate desc ";
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@StaffId", StaffId);
            Conversion objCon = new Conversion();
            Dal objDal = new Dal();
            DataTable dtStaffPaymentHistory = new DataTable();
            dtStaffPaymentHistory = objDal.ExecuteTable(strLoadQuery, param);
            return dtStaffPaymentHistory;
        }

        #endregion
        #endregion
    }

}