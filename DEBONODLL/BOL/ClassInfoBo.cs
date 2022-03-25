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
    public class ClassInfoBo
    {
        #region Field Properties

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
        ///ClassSection
        ///<summary>
        ///<remarks>
        ///Property variable for ClassSection
        ///<remarks>
        private String ClassSection;
        public String _ClassSection
        {
            get
            {
                return ClassSection;
            }
            set
            {
                ClassSection = value;
            }
        }

        ///<summary>
        ///AdmissionDate
        ///<summary>
        ///<remarks>
        ///Property variable for AdmissionDate
        ///<remarks>
        private DateTime AdmissionDate;
        public DateTime _AdmissionDate
        {
            get
            {
                return AdmissionDate;
            }
            set
            {
                AdmissionDate = value;
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

            CId = objCon.ConToInt64(drMainData["CId"]);
            SId = objCon.ConToInt64(drMainData["SId"]);
            PaymentType = objCon.ConToStr(drMainData["PaymentType"]);
            SessionYear = objCon.ConToStr(drMainData["SessionYear"]);
            ClassName = objCon.ConToStr(drMainData["ClassName"]);
            ClassSection = objCon.ConToStr(drMainData["ClassSection"]);
            AdmissionDate = objCon.ConToDT(drMainData["AdmissionDate"]);
        }
        #endregion

        #region Save  functions

        //***********************************
        //This Function will perform the action for Saving the Data into Table  ClassInfo
        //***********************************
        public int SaveClassInfo()
        {
            String strInsertQuery = "";
            SqlParameter[] param = new SqlParameter[8];
            strInsertQuery = "insert into ClassInfo( SId,PaymentType,SessionYear,ClassName,ClassSection,AdmissionDate,CreatedOn,CreatedBy  ) " +
                " values( @SId,@PaymentType,@SessionYear,@ClassName,@ClassSection,@AdmissionDate,@CreatedOn,@CreatedBy  ) ";

                param[0] = new SqlParameter("@SId", SId);
                param[1] = new SqlParameter("@PaymentType", PaymentType);
                param[2] = new SqlParameter("@SessionYear", SessionYear);
                param[3] = new SqlParameter("@ClassName", ClassName);
                param[4] = new SqlParameter("@CreatedOn", DateTime.Now.Date);
                param[5] = new SqlParameter("@CreatedBy", CreatedBy);
                param[6] = new SqlParameter("@AdmissionDate", AdmissionDate);
                param[7] = new SqlParameter("@ClassSection", ClassSection);
            
            Dal objDal = new Dal();
            int check = 0;
            check = objDal.ExecuteDataIdentity(strInsertQuery, param);
            if (check == 1)
            {
                CId = objDal.ExecuteForID("select Max(CId) from ClassInfo");
            }
            return check;
        }
        #endregion

        #region Update functions

        //***********************************
        //This Function will perform the action for Updating the Data into Table  ClassInfo for Primary Column SId
        //***********************************
        public int UpdateClassInfo()
        {
            String strUpdateQuery ="";
            SqlParameter[] param = new SqlParameter[9];

            strUpdateQuery = "update ClassInfo Set SId=@SId,PaymentType=@PaymentType,SessionYear=@SessionYear,AdmissionDate=@AdmissionDate,ClassName=@ClassName,ClassSection=@ClassSection,ModifiedOn=@ModifiedOn,ModifiedBy=@ModifiedBy where CId= @CId";

                param[0] = new SqlParameter("@CId", CId);
                param[1] = new SqlParameter("@PaymentType", PaymentType);
                param[2] = new SqlParameter("@SessionYear", SessionYear);
                param[3] = new SqlParameter("@ClassName", ClassName);
                param[4] = new SqlParameter("@ModifiedOn", DateTime.Now.Date);
                param[5] = new SqlParameter("@ModifiedBy", CreatedBy);
                param[6] = new SqlParameter("@SId", SId);
                param[7] = new SqlParameter("@AdmissionDate", AdmissionDate);
                param[8] = new SqlParameter("@ClassSection", ClassSection);
            Dal objDal = new Dal();
            int check = 0;
            check = objDal.ExecuteDataIdentity(strUpdateQuery, param);
            return check;
        }
        #endregion

        #region Delete functions

        //***********************************
        //This Function will perform the action for Deleteing the Data into Table  ClassInfo for Primary Column SId
        //***********************************
        public int DeleteClassInfo()
        {
            String strDeleteQuery = "Delete from ClassInfo where  CId = @CId ";
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@CId", CId);
            Dal objDal = new Dal();
            int check = 0;
            check = objDal.ExecuteDataIdentity(strDeleteQuery, param);
            return check;
        }

        public int DeleteClassInfoByStudent()
        {
            String strDeleteQuery = "Delete from ClassInfo where  SId = @SId ";
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
        //This Function will perform the action for Loading the Data from Table  ClassInfo for Primary Column SId
        //***********************************
        public void LoadClassInfo()
        {
            String strLoadQuery = "Select * From ClassInfo where  CId = @CId ";


            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@CId", CId);

            Conversion objCon = new Conversion();
            Dal objDal = new Dal();
            DataTable dtClassInfo = new DataTable();
            dtClassInfo = objDal.ExecuteTable(strLoadQuery, param);
            if (dtClassInfo.Rows.Count > 0)
            {
                SId = objCon.ConToInt64(dtClassInfo.Rows[0]["SId"]);
                ClassName = objCon.ConToStr(dtClassInfo.Rows[0]["ClassName"]);
                ClassSection = objCon.ConToStr(dtClassInfo.Rows[0]["ClassSection"]);
                SessionYear = objCon.ConToStr(dtClassInfo.Rows[0]["SessionYear"]);
                PaymentType = objCon.ConToStr(dtClassInfo.Rows[0]["PaymentType"]);
                AdmissionDate = objCon.ConToDT(dtClassInfo.Rows[0]["AdmissionDate"]);
            }
        }

        public void LoadClassInfobystudent()
        {
            String strLoadQuery = "Select * From ClassInfo where  SId = @SId and ClassName=@ClassName ";


            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@SId", SId);
            param[1] = new SqlParameter("@ClassName", ClassName);

            Conversion objCon = new Conversion();
            Dal objDal = new Dal();
            DataTable dtClassInfo = new DataTable();
            dtClassInfo = objDal.ExecuteTable(strLoadQuery, param);
            if (dtClassInfo.Rows.Count > 0)
            {
                CId = objCon.ConToInt64(dtClassInfo.Rows[0]["CId"]);
            }
        }
        #endregion

        #region ShowAllData funtion

        //***********************************
        //This Function will perform the action for Loading the Data from Table  Tarif
        //***********************************
        public DataTable ShowStudentClassInfo()
        {
            String strLoadQuery = "Select * From ClassInfo where  SId = @SId ";
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@SId", SId);
            Conversion objCon = new Conversion();
            Dal objDal = new Dal();
            DataTable dtClassInfo = new DataTable();
            dtClassInfo = objDal.ExecuteTable(strLoadQuery, param);
            return dtClassInfo;
        }

        public DataTable LoadStudentByClass()
        {
            String strLoadQuery = "select SI.SId,FName+' '+isnull(LName,'') as StudentName from studentInfo SI join ClassInfo CI on CI.SId=SI.SId where CI.ClassName=@ClassName ";
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@ClassName", ClassName);
            Conversion objCon = new Conversion();
            Dal objDal = new Dal();
            DataTable dtClassInfo = new DataTable();
            dtClassInfo = objDal.ExecuteTable(strLoadQuery, param);
            return dtClassInfo;
        }

        public DataTable getStudentClassDetails()
        {
            String strLoadQuery = "select top 1 s.SId,'100012' as RollNumber,S.FName+' '+isnull(LName,'') as StudentName,s.FatherName,s.MobileNumber,CI.ClassName+' '+CI.ClassSection as ClassName,CI.SessionYear from StudentInfo s join classinfo CI on CI.SId=CI.SId where s.SId=@SId";
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@SId", SId);
            Conversion objCon = new Conversion();
            Dal objDal = new Dal();
            DataTable dtClassInfo = new DataTable();
            dtClassInfo = objDal.ExecuteTable(strLoadQuery, param);
            return dtClassInfo;
        }
        #endregion
        #endregion
    }

}