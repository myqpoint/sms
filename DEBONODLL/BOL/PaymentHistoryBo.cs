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
    public class PaymentHistoryBo
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
        ///CId
        ///<summary>
        ///<remarks>
        ///Property variable for CId
        ///<remarks>
        private Int64 CId;
        public Int64 _CId
        {
            get
            {
                return CId;
            }
            set
            {
                CId = value;
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
        ///PaymentType
        ///<summary>
        ///<remarks>
        ///Property variable for PaymentType
        ///<remarks>
        private String PaymentType;
        public String _PaymentType
        {
            get
            {
                return PaymentType;
            }
            set
            {
                PaymentType = value;
            }
        }

        ///<summary>
        ///SessionYear
        ///<summary>
        ///<remarks>
        ///Property variable for SessionYear
        ///<remarks>
        private Decimal Amount;
        public Decimal _Amount
        {
            get
            {
                return Amount;
            }
            set
            {
                Amount = value;
            }
        }

        ///<summary>
        ///ComputerFee
        ///<summary>
        ///<remarks>
        ///Property variable for ComputerFee
        ///<remarks>
        private Decimal ComputerFee;
        public Decimal _ComputerFee
        {
            get
            {
                return ComputerFee;
            }
            set
            {
                ComputerFee = value;
            }
        }

        ///<summary>
        ///GeneratorFee
        ///<summary>
        ///<remarks>
        ///Property variable for GeneratorFee
        ///<remarks>
        private Decimal GeneratorFee;
        public Decimal _GeneratorFee
        {
            get
            {
                return GeneratorFee;
            }
            set
            {
                GeneratorFee = value;
            }
        }

        ///<summary>
        ///PTAFee
        ///<summary>
        ///<remarks>
        ///Property variable for PTAFee
        ///<remarks>
        private Decimal PTAFee;
        public Decimal _PTAFee
        {
            get
            {
                return PTAFee;
            }
            set
            {
                PTAFee = value;
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
            PId = objCon.ConToInt64(drMainData["PId"]);
            CId = objCon.ConToInt64(drMainData["CId"]);
            SId = objCon.ConToInt64(drMainData["SId"]);
            PaymentType = objCon.ConToStr(drMainData["PaymentType"]);
            Amount = objCon.ConToDec(drMainData["Amount"]);
            ComputerFee = objCon.ConToDec(drMainData["ComputerFee"]);
            GeneratorFee = objCon.ConToDec(drMainData["GeneratorFee"]);
            PTAFee = objCon.ConToDec(drMainData["PTAFee"]);
            PaymentDate = objCon.ConToDT(drMainData["PaymentDate"]);
            CreatedBy = objCon.ConToStr(drMainData["CreatedBy"]);
            CreatedOn = objCon.ConToDT(drMainData["CreatedOn"]);
            ModifiedBy = objCon.ConToStr(drMainData["ModifiedBy"]);
            ModifiedOn = objCon.ConToDT(drMainData["ModifiedOn"]);
        }
        #endregion

        #region Save  functions

        //***********************************
        //This Function will perform the action for Saving the Data into Table  PaymentHistory
        //***********************************
        public int SavePaymentHistory()
        {
            String strInsertQuery = "";
            SqlParameter[] param = new SqlParameter[10];
            strInsertQuery = "insert into PaymentHistory( CId,SId,PaymentType,Amount,ComputerFee,GeneratorFee,PTAFee,PaymentDate,CreatedOn,CreatedBy  ) " +
                " values( @CId,@SId,@PaymentType,@Amount,@ComputerFee,@GeneratorFee,@PTAFee,@PaymentDate,@CreatedOn,@CreatedBy  ) ";

                param[0] = new SqlParameter("@CId", CId);
                param[1] = new SqlParameter("@SId", SId);
                param[2] = new SqlParameter("@PaymentType", PaymentType);
                param[3] = new SqlParameter("@Amount", Amount);
                param[4] = new SqlParameter("@PaymentDate", PaymentDate);
                param[5] = new SqlParameter("@CreatedOn", DateTime.Now.Date);
                param[6] = new SqlParameter("@CreatedBy", CreatedBy);
                param[7] = new SqlParameter("@ComputerFee", ComputerFee);
                param[8] = new SqlParameter("@GeneratorFee", GeneratorFee);
                param[9] = new SqlParameter("@PTAFee", PTAFee);
            
            Dal objDal = new Dal();
            int check = 0;
            check = objDal.ExecuteDataIdentity(strInsertQuery, param);
            if (check == 1)
            {
                PId = objDal.ExecuteForID("select Max(PId) from PaymentHistory");
            }
            return check;
        }
        #endregion

        #region Update functions

        //***********************************
        //This Function will perform the action for Updating the Data into Table  PaymentHistory for Primary Column SId
        //***********************************
        public int UpdatePaymentHistory()
        {
            String strUpdateQuery ="";
            SqlParameter[] param = new SqlParameter[7];

            strUpdateQuery = "update PaymentHistory Set PaymentType=@PaymentType,Amount=@Amount,PaymentDate=@PaymentDate,ModifiedOn=@ModifiedOn,ModifiedBy=@ModifiedBy where PId= @PId";

                param[0] = new SqlParameter("@PId", PId);
                param[1] = new SqlParameter("@PaymentType", PaymentType);
                param[2] = new SqlParameter("@Amount", Amount);
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
        //This Function will perform the action for Deleteing the Data into Table  PaymentHistory for Primary Column SId
        //***********************************
        public int DeletePaymentHistory()
        {
            String strDeleteQuery = "Delete from PaymentHistory where  PId = @PId ";
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@PId", PId);
            Dal objDal = new Dal();
            int check = 0;
            check = objDal.ExecuteDataIdentity(strDeleteQuery, param);
            return check;
        }

        public int DeletePaymentHistoryByStudent()
        {
            String strDeleteQuery = "Delete from PaymentHistory where  SId = @SId ";
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@SId", SId);
            Dal objDal = new Dal();
            int check = 0;
            check = objDal.ExecuteDataIdentity(strDeleteQuery, param);
            return check;
        }

        #endregion

        #region Load functions

        //***********************************
        //This Function will perform the action for Loading the Data from Table  PaymentHistory for Primary Column SId
        //***********************************
        public void LoadPaymentHistory()
        {
            String strLoadQuery = "Select * From PaymentHistory where  PId = @PId ";


            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@PId", PId);

            Conversion objCon = new Conversion();
            Dal objDal = new Dal();
            DataTable dtPaymentHistory = new DataTable();
            dtPaymentHistory = objDal.ExecuteTable(strLoadQuery, param);
            if (dtPaymentHistory.Rows.Count > 0)
            {
                CId = objCon.ConToInt64(dtPaymentHistory.Rows[0]["CId"]);
                SId = objCon.ConToInt64(dtPaymentHistory.Rows[0]["SId"]);
                Amount = objCon.ConToDec(dtPaymentHistory.Rows[0]["Amount"]);
                ComputerFee = objCon.ConToDec(dtPaymentHistory.Rows[0]["ComputerFee"]);
                GeneratorFee = objCon.ConToDec(dtPaymentHistory.Rows[0]["GeneratorFee"]);
                PTAFee = objCon.ConToDec(dtPaymentHistory.Rows[0]["PTAFee"]);
                PaymentDate = objCon.ConToDT(dtPaymentHistory.Rows[0]["PaymentDate"]);
                PaymentType = objCon.ConToStr(dtPaymentHistory.Rows[0]["PaymentType"]);
                CreatedOn = objCon.ConToDT(dtPaymentHistory.Rows[0]["CreatedOn"]);
                CreatedBy = objCon.ConToStr(dtPaymentHistory.Rows[0]["CreatedBy"]);
            }
        }

        #endregion

        #region ShowAllData funtion

        //***********************************
        //This Function will perform the action for Loading the Data from Table  Tarif
        //***********************************
        public DataTable ShowStudentPaymentHistory()
        {
            String strLoadQuery = "Select *,DATENAME(MONTH, PaymentDate) as MonthName From PaymentHistory where  CId = @CId  order by PaymentDate asc ";
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@CId", CId);
            Conversion objCon = new Conversion();
            Dal objDal = new Dal();
            DataTable dtPaymentHistory = new DataTable();
            dtPaymentHistory = objDal.ExecuteTable(strLoadQuery, param);
            return dtPaymentHistory;
        }

        #endregion
        #endregion
    }

}