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
    public class FeesTypeBo
    {
        #region Field Properties

        ///<summary>
        ///FId
        ///<summary>
        ///<remarks>
        ///Property variable for FId
        ///<remarks>
        private Int64 FId;
        public Int64 _FId
        {
            get
            {
                return FId;
            }
            set
            {
                FId = value;
            }
        }

        ///<summary>
        ///SessionYear
        ///<summary>
        ///<remarks>
        ///Property variable for SessionYear
        ///<remarks>
        private String SessionYear;
        public String _SessionYear
        {
            get
            {
                return SessionYear;
            }
            set
            {
                SessionYear = value;
            }
        }
        ///<summary>
        ///ClassName
        ///<summary>
        ///<remarks>
        ///Property variable for ClassName
        ///<remarks>
        private String ClassName;
        public String _ClassName
        {
            get
            {
                return ClassName;
            }
            set
            {
                ClassName = value;
            }
        }

        ///<summary>
        ///GeneratorFee
        ///<summary>
        ///<remarks>
        ///Property variable for GeneratorFee
        ///<remarks>
        private decimal GeneratorFee;
        public decimal _GeneratorFee
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
        ///ComputerFee
        ///<summary>
        ///<remarks>
        ///Property variable for ComputerFee
        ///<remarks>
        private decimal ComputerFee;
        public decimal _ComputerFee
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
        ///TutionFee
        ///<summary>
        ///<remarks>
        ///Property variable for TutionFee
        ///<remarks>
        private decimal TutionFee;
        public decimal _TutionFee
        {
            get
            {
                return TutionFee;
            }
            set
            {
                TutionFee = value;
            }
        }
        ///<summary>
        ///PTAFee
        ///<summary>
        ///<remarks>
        ///Property variable for PTAFee
        ///<remarks>
        private decimal PTAFee;
        public decimal _PTAFee
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
        ///ExamFees
        ///<summary>
        ///<remarks>
        ///Property variable for ExamFees
        ///<remarks>
        private decimal ExamFees;
        public decimal _ExamFees
        {
            get
            {
                return ExamFees;
            }
            set
            {
                ExamFees = value;
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

            FId = objCon.ConToInt64(drMainData["FId"]);
            ClassName = objCon.ConToStr(drMainData["ClassName"]);
            SessionYear = objCon.ConToStr(drMainData["SessionYear"]);
            GeneratorFee = objCon.ConToDec(drMainData["GeneratorFee"]);
            ComputerFee = objCon.ConToDec(drMainData["ComputerFee"]);
            TutionFee = objCon.ConToDec(drMainData["TutionFee"]);
            PTAFee = objCon.ConToDec(drMainData["PTAFee"]);
            ExamFees = objCon.ConToDec(drMainData["ExamFees"]);
        }
        #endregion

        #region Save  functions

        //***********************************
        //This Function will perform the action for Saving the Data into Table  FeesType
        //***********************************
        public int SaveFeesType()
        {
            String strInsertQuery = "";
            SqlParameter[] param = new SqlParameter[9];
            strInsertQuery = "insert into FeesType( ClassName,GeneratorFee,ComputerFee,TutionFee,PTAFee,ExamFees,CreatedOn,CreatedBy,SessionYear  ) " +
                " values( @ClassName,@GeneratorFee,@ComputerFee,@TutionFee,@PTAFee,@ExamFees,@CreatedOn,@CreatedBy,@SessionYear  ) ";

            param[0] = new SqlParameter("@ClassName", ClassName);
            param[1] = new SqlParameter("@GeneratorFee", GeneratorFee);
            param[2] = new SqlParameter("@ComputerFee", ComputerFee);
            param[3] = new SqlParameter("@TutionFee", TutionFee);
                param[4] = new SqlParameter("@CreatedOn", DateTime.Now.Date);
                param[5] = new SqlParameter("@CreatedBy", CreatedBy);
                param[6] = new SqlParameter("@PTAFee", PTAFee);
                param[7] = new SqlParameter("@ExamFees", ExamFees);
                param[8] = new SqlParameter("@SessionYear", SessionYear);
            
            Dal objDal = new Dal();
            int check = 0;
            check = objDal.ExecuteDataIdentity(strInsertQuery, param);
            if (check == 1)
            {
                FId = objDal.ExecuteForID("select Max(FId) from FeesType");
            }
            return check;
        }
        #endregion

        #region Update functions

        //***********************************
        //This Function will perform the action for Updating the Data into Table  FeesType for Primary Column SId
        //***********************************
        public int UpdateFeesType()
        {
            String strUpdateQuery ="";
            SqlParameter[] param = new SqlParameter[10];

            strUpdateQuery = "update FeesType Set ClassName=@ClassName,SessionYear=@SessionYear,GeneratorFee=@GeneratorFee,ComputerFee=@ComputerFee,ExamFees=@ExamFees,TutionFee=@TutionFee,PTAFee=@PTAFee,ModifiedOn=@ModifiedOn,ModifiedBy=@ModifiedBy where FId= @FId";

            param[0] = new SqlParameter("@ClassName", ClassName);
            param[1] = new SqlParameter("@GeneratorFee", GeneratorFee);
            param[2] = new SqlParameter("@ComputerFee", ComputerFee);
            param[3] = new SqlParameter("@TutionFee", TutionFee);
                param[4] = new SqlParameter("@ModifiedOn", DateTime.Now.Date);
                param[5] = new SqlParameter("@ModifiedBy", CreatedBy);
                param[6] = new SqlParameter("@PTAFee", PTAFee);
                param[7] = new SqlParameter("@FId", FId);
                param[8] = new SqlParameter("@ExamFees", ExamFees);
                param[9] = new SqlParameter("@SessionYear", SessionYear);
            Dal objDal = new Dal();
            int check = 0;
            check = objDal.ExecuteDataIdentity(strUpdateQuery, param);
            return check;
        }
        #endregion

        #region Delete functions

        //***********************************
        //This Function will perform the action for Deleteing the Data into Table  FeesType for Primary Column SId
        //***********************************
        public int DeleteFeesType()
        {
            String strDeleteQuery = "Delete from FeesType where  FId = @FId ";
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@FId", FId);
            Dal objDal = new Dal();
            int check = 0;
            check = objDal.ExecuteDataIdentity(strDeleteQuery, param);
            return check;
        }

        #endregion

        #region Load functions

        //***********************************
        //This Function will perform the action for Loading the Data from Table  FeesType for Primary Column SId
        //***********************************
        public void LoadFeesType()
        {
            String strLoadQuery = "Select * From FeesType where  FId = @FId ";


            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@FId", FId);

            Conversion objCon = new Conversion();
            Dal objDal = new Dal();
            DataTable dtFeesType = new DataTable();
            dtFeesType = objDal.ExecuteTable(strLoadQuery, param);
            if (dtFeesType.Rows.Count > 0)
            {
                ClassName = objCon.ConToStr(dtFeesType.Rows[0]["ClassName"]);
                SessionYear = objCon.ConToStr(dtFeesType.Rows[0]["SessionYear"]);
                GeneratorFee = objCon.ConToDec(dtFeesType.Rows[0]["GeneratorFee"]);
                ComputerFee = objCon.ConToDec(dtFeesType.Rows[0]["ComputerFee"]);
                TutionFee = objCon.ConToDec(dtFeesType.Rows[0]["TutionFee"]);
                PTAFee = objCon.ConToDec(dtFeesType.Rows[0]["PTAFee"]);
                ExamFees = objCon.ConToDec(dtFeesType.Rows[0]["ExamFees"]);
            }
        }
        public DataTable LoadEmptyFeesType()
        {
            String strLoadQuery = "Select *  From FeesType where  1 = 0 ";
            Conversion objCon = new Conversion();
            Dal objDal = new Dal();
            DataTable dtProductDetail = new DataTable();
            dtProductDetail = objDal.ExecuteTable(strLoadQuery);
            return dtProductDetail;
        }
        public DataTable LoadAllClass()
        {
            String strLoadQuery = "Select FId,ClassName From FeesType where SessionYear='2020-2021' ";
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@SessionYear", SessionYear);
            Conversion objCon = new Conversion();
            Dal objDal = new Dal();
            DataTable dtFeesType = new DataTable();
            dtFeesType = objDal.ExecuteTable(strLoadQuery, param);
            return dtFeesType;
        }
        public DataTable LoadallFeesTypeWithClass()
        {
            String strLoadQuery = "Select * From FeesType where  SessionYear = @SessionYear ";
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@SessionYear", SessionYear);
            Conversion objCon = new Conversion();
            Dal objDal = new Dal();
            DataTable dtFeesType = new DataTable();
            dtFeesType = objDal.ExecuteTable(strLoadQuery, param);
            return dtFeesType;
        }
        public DataTable LoadallFeesTypeById()
        {
            String strLoadQuery = "Select * From FeesType where  FId = @FId ";
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@FId", FId);
            Conversion objCon = new Conversion();
            Dal objDal = new Dal();
            DataTable dtFeesType = new DataTable();
            dtFeesType = objDal.ExecuteTable(strLoadQuery, param);
            return dtFeesType;
        }
        public DataTable LoadFeesTypeByClassName()
        {
            String strLoadQuery = "Select * From FeesType where  ClassName = @ClassName and SessionYear=@SessionYear ";
            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@ClassName", ClassName);
            param[1] = new SqlParameter("@SessionYear", SessionYear);
            Conversion objCon = new Conversion();
            Dal objDal = new Dal();
            DataTable dtFeesType = new DataTable();
            dtFeesType = objDal.ExecuteTable(strLoadQuery, param);
            return dtFeesType;
        }
        #endregion
        #endregion
    }

}