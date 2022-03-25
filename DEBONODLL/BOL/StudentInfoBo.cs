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
    public class StudentInfoBo
    {
        #region Field Properties

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
        ///FName
        ///<summary>
        ///<remarks>
        ///Property variable for FName
        ///<remarks>
        private String FName;
        public String _FName
        {
            get
            {
                return FName;
            }
            set
            {
                FName = value;
            }
        }

        ///<summary>
        ///LName
        ///<summary>
        ///<remarks>
        ///Property variable for LName
        ///<remarks>
        private String LName;
        public String _LName
        {
            get
            {
                return LName;
            }
            set
            {
                LName = value;
            }
        }

        ///<summary>
        ///DOB
        ///<summary>
        ///<remarks>
        ///Property variable for DOB
        ///<remarks>
        private DateTime DOB;
        public DateTime _DOB
        {
            get
            {
                return DOB;
            }
            set
            {
                DOB = value;
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
        ///MobileNumber
        ///<summary>
        ///<remarks>
        ///Property variable for MobileNumber
        ///<remarks>
        private String MobileNumber;
        public String _MobileNumber
        {
            get
            {
                return MobileNumber;
            }
            set
            {
                MobileNumber = value;
            }
        }

        ///<summary>
        ///Gender
        ///<summary>
        ///<remarks>
        ///Property variable for Gender
        ///<remarks>
        private String Gender;
        public String _Gender
        {
            get
            {
                return Gender;
            }
            set
            {
                Gender = value;
            }
        }

        ///<summary>
        ///EmailId
        ///<summary>
        ///<remarks>
        ///Property variable for EmailId
        ///<remarks>
        private String EmailId;
        public String _EmailId
        {
            get
            {
                return EmailId;
            }
            set
            {
                EmailId = value;
            }
        }

        ///<summary>
        ///AadharNumber
        ///<summary>
        ///<remarks>
        ///Property variable for AadharNumber
        ///<remarks>
        private String AadharNumber;
        public String _AadharNumber
        {
            get
            {
                return AadharNumber;
            }
            set
            {
                AadharNumber = value;
            }
        }

        ///<summary>
        ///FatherName
        ///<summary>
        ///<remarks>
        ///Property variable for FatherName
        ///<remarks>
        private String FatherName;
        public String _FatherName
        {
            get
            {
                return FatherName;
            }
            set
            {
                FatherName = value;
            }
        }

        ///<summary>
        ///MotherName
        ///<summary>
        ///<remarks>
        ///Property variable for MotherName
        ///<remarks>
        private String MotherName;
        public String _MotherName
        {
            get
            {
                return MotherName;
            }
            set
            {
                MotherName = value;
            }
        }

        ///<summary>
        ///FatherMobileNumber
        ///<summary>
        ///<remarks>
        ///Property variable for FatherMobileNumber
        ///<remarks>
        private String FatherMobileNumber;
        public String _FatherMobileNumber
        {
            get
            {
                return FatherMobileNumber;
            }
            set
            {
                FatherMobileNumber = value;
            }
        }

        ///<summary>
        ///Address
        ///<summary>
        ///<remarks>
        ///Property variable for Address
        ///<remarks>
        private String Address;
        public String _Address
        {
            get
            {
                return Address;
            }
            set
            {
                Address = value;
            }
        }

        ///<summary>
        ///City
        ///<summary>
        ///<remarks>
        ///Property variable for City
        ///<remarks>
        private String City;
        public String _City
        {
            get
            {
                return City;
            }
            set
            {
                City = value;
            }
        }

        ///<summary>
        ///State
        ///<summary>
        ///<remarks>
        ///Property variable for State
        ///<remarks>
        private String State;
        public String _State
        {
            get
            {
                return State;
            }
            set
            {
                State = value;
            }
        }


        ///<summary>
        ///Pincode
        ///<summary>
        ///<remarks>
        ///Property variable for Pincode
        ///<remarks>
        private String Pincode;
        public String _Pincode
        {
            get
            {
                return Pincode;
            }
            set
            {
                Pincode = value;
            }
        }


        ///<summary>
        ///Religion
        ///<summary>
        ///<remarks>
        ///Property variable for Religion
        ///<remarks>
        private String Religion;
        public String _Religion
        {
            get
            {
                return Religion;
            }
            set
            {
                Religion = value;
            }
        }

        ///<summary>
        ///CasteCategory
        ///<summary>
        ///<remarks>
        ///Property variable for CasteCategory
        ///<remarks>
        private String CasteCategory;
        public String _CasteCategory
        {
            get
            {
                return CasteCategory;
            }
            set
            {
                CasteCategory = value;
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
        ///Session
        ///<summary>
        ///<remarks>
        ///Property variable for Session
        ///<remarks>
        private String Session;
        public String _Session
        {
            get
            {
                return Session;
            }
            set
            {
                Session = value;
            }
        }

        ///<summary>
        ///StudentImage
        ///<summary>
        ///<remarks>
        ///Property variable for StudentImage
        ///<remarks>
        private Byte[] StudentImage;
        public Byte[] _StudentImage
        {
            get
            {
                return StudentImage;
            }
            set
            {
                StudentImage = value;
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

            SId = objCon.ConToInt64(drMainData["SId"]);
            FName = objCon.ConToStr(drMainData["FName"]);
            LName = objCon.ConToStr(drMainData["LName"]);
            DOB = objCon.ConToDT(drMainData["DOB"]);
            AdmissionDate = objCon.ConToDT(drMainData["AdmissionDate"]);
            MobileNumber = objCon.ConToStr(drMainData["MobileNumber"]);
            Gender = objCon.ConToStr(drMainData["Gender"]);
            EmailId = objCon.ConToStr(drMainData["EmailId"]);
            FatherName = objCon.ConToStr(drMainData["FatherName"]);
            FatherMobileNumber = objCon.ConToStr(drMainData["FatherMobileNumber"]);
            MotherName = objCon.ConToStr(drMainData["MotherName"]);
            Address = objCon.ConToStr(drMainData["Address"]);
            City = objCon.ConToStr(drMainData["City"]);
            State = objCon.ConToStr(drMainData["State"]);
            Pincode = objCon.ConToStr(drMainData["Pincode"]);
            Religion = objCon.ConToStr(drMainData["Religion"]);
            CasteCategory = objCon.ConToStr(drMainData["CasteCategory"]);
            PaymentType = objCon.ConToStr(drMainData["PaymentType"]);
            AadharNumber = objCon.ConToStr(drMainData["AadharNumber"]);
            Session = objCon.ConToStr(drMainData["Session"]);
            CreatedBy = objCon.ConToStr(drMainData["CreatedBy"]);
            CreatedOn = objCon.ConToDT(drMainData["CreatedOn"]);
        }
        #endregion

        #region Save  functions

        //***********************************
        //This Function will perform the action for Saving the Data into Table  StudentInfo
        //***********************************
        public int SaveStudentInfo()
        {
            String strInsertQuery = "";
            SqlParameter[] param = new SqlParameter[22];
            if (StudentImage != null)
            {
                strInsertQuery = "insert into StudentInfo( FName,LName,DOB,AdmissionDate,MobileNumber,Gender,AadharNumber,EmailId,FatherName,FatherMobileNumber,MotherName,Address,City,State,Pincode,StudentImage,Religion,CasteCategory,PaymentType,Session,CreatedOn,CreatedBy  ) " +
               " values( @FName,@LName,@DOB,@AdmissionDate,@MobileNumber,@Gender,@AadharNumber,@EmailId,@FatherName,@FatherMobileNumber,@MotherName,@Address,@City,@State,@Pincode,@StudentImage,@Religion,@CasteCategory,@PaymentType,@Session,@CreatedOn,@CreatedBy  ) ";
                param[4] = new SqlParameter("@StudentImage", StudentImage);
            }
            else
            {
                strInsertQuery = "insert into StudentInfo( FName,LName,DOB,AdmissionDate,MobileNumber,Gender,AadharNumber,EmailId,FatherName,FatherMobileNumber,MotherName,Address,City,State,Pincode,Religion,CasteCategory,PaymentType,Session,CreatedOn,CreatedBy  ) " +
                " values( @FName,@LName,@DOB,@AdmissionDate,@MobileNumber,@Gender,@AadharNumber,@EmailId,@FatherName,@FatherMobileNumber,@MotherName,@Address,@City,@State,@Pincode,@Religion,@CasteCategory,@PaymentType,@Session,@CreatedOn,@CreatedBy  ) ";
            }

            param[0] = new SqlParameter("@FName", FName);
            param[1] = new SqlParameter("@LName", LName);
            param[2] = new SqlParameter("@DOB", DOB);
            param[3] = new SqlParameter("@MobileNumber", MobileNumber);
            param[5] = new SqlParameter("@Gender", Gender);
            param[6] = new SqlParameter("@EmailId", EmailId);
            param[7] = new SqlParameter("@FatherName", FatherName);
            param[8] = new SqlParameter("@FatherMobileNumber", FatherMobileNumber);
            param[9] = new SqlParameter("@MotherName", MotherName);
            param[10] = new SqlParameter("@Address", Address);
            param[11] = new SqlParameter("@City", City);
            param[12] = new SqlParameter("@State", State);
            param[13] = new SqlParameter("@Pincode", Pincode);
            param[14] = new SqlParameter("@Religion", Religion);
            param[15] = new SqlParameter("@CasteCategory", CasteCategory);
            param[16] = new SqlParameter("@PaymentType", PaymentType);
            param[17] = new SqlParameter("@Session", Session);
            param[18] = new SqlParameter("@AadharNumber", AadharNumber);
            param[19] = new SqlParameter("@CreatedOn", DateTime.Now.Date);
            param[20] = new SqlParameter("@CreatedBy", CreatedBy);
            param[21] = new SqlParameter("@AdmissionDate", AdmissionDate);
            
            Dal objDal = new Dal();
            int check = 0;
            check = objDal.ExecuteDataIdentity(strInsertQuery, param);
            if (check == 1)
            {
                SId = objDal.ExecuteForID("select Max(SId) from StudentInfo");
            }
            return check;
        }
        #endregion

        #region Update functions

        //***********************************
        //This Function will perform the action for Updating the Data into Table  StudentInfo for Primary Column SId
        //***********************************
        public int UpdateStudentInfo()
        {
            String strUpdateQuery ="";
            SqlParameter[] param = new SqlParameter[23];
            if (StudentImage != null)
            {
                strUpdateQuery = "update StudentInfo Set FName=@FName,LName=@LName,DOB=@DOB,AdmissionDate=@AdmissionDate,MobileNumber=@MobileNumber,Gender=@Gender,AadharNumber=@AadharNumber,EmailId=@EmailId,FatherName=@FatherName,FatherMobileNumber=@FatherMobileNumber,MotherName=@MotherName,Address=@Address,City=@City,State=@State,Pincode=@Pincode,StudentImage=@StudentImage,Religion=@Religion,CasteCategory=@CasteCategory,PaymentType=@PaymentType,Session=@Session,ModifiedOn=@ModifiedOn,ModifiedBy=@ModifiedBy where SId= @SId";
                param[4] = new SqlParameter("@StudentImage", StudentImage);
            }
            else
            {
                strUpdateQuery = "update StudentInfo Set FName=@FName,LName=@LName,DOB=@DOB,AdmissionDate=@AdmissionDate,MobileNumber=@MobileNumber,Gender=@Gender,AadharNumber=@AadharNumber,EmailId=@EmailId,FatherName=@FatherName,FatherMobileNumber=@FatherMobileNumber,MotherName=@MotherName,Address=@Address,City=@City,State=@State,Pincode=@Pincode,Religion=@Religion,CasteCategory=@CasteCategory,PaymentType=@PaymentType,Session=@Session,ModifiedOn=@ModifiedOn,ModifiedBy=@ModifiedBy where SId= @SId";
            }


            param[0] = new SqlParameter("@FName", FName);
            param[1] = new SqlParameter("@LName", LName);
            param[2] = new SqlParameter("@DOB", DOB);
            param[3] = new SqlParameter("@MobileNumber", MobileNumber);
            param[5] = new SqlParameter("@Gender", Gender);
            param[6] = new SqlParameter("@EmailId", EmailId);
            param[7] = new SqlParameter("@FatherName", FatherName);
            param[8] = new SqlParameter("@FatherMobileNumber", FatherMobileNumber);
            param[9] = new SqlParameter("@MotherName", MotherName);
            param[10] = new SqlParameter("@Address", Address);
            param[11] = new SqlParameter("@City", City);
            param[12] = new SqlParameter("@State", State);
            param[13] = new SqlParameter("@Pincode", Pincode);
            param[14] = new SqlParameter("@Religion", Religion);
            param[15] = new SqlParameter("@CasteCategory", CasteCategory);
            param[16] = new SqlParameter("@PaymentType", PaymentType);
            param[17] = new SqlParameter("@Session", Session);
            param[18] = new SqlParameter("@AadharNumber", AadharNumber);
            param[19] = new SqlParameter("@ModifiedOn", DateTime.Now.Date);
            param[20] = new SqlParameter("@ModifiedBy", CreatedBy);
            param[21] = new SqlParameter("@SId", SId);
            param[22] = new SqlParameter("@AdmissionDate", AdmissionDate);
            Dal objDal = new Dal();
            int check = 0;
            check = objDal.ExecuteDataIdentity(strUpdateQuery, param);
            return check;
        }
        #endregion

        #region Delete functions

        //***********************************
        //This Function will perform the action for Deleteing the Data into Table  StudentInfo for Primary Column SId
        //***********************************
        public int DeleteStudentInfo()
        {
            String strDeleteQuery = "Update StudentInfo set Deleted=1 where  SId = @SId ";


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
        //This Function will perform the action for Loading the Data from Table  StudentInfo for Primary Column SId
        //***********************************
        public void LoadStudentInfo()
        {
            String strLoadQuery = "Select * From StudentInfo where  SId = @SId ";


            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@SId", SId);

            Conversion objCon = new Conversion();
            Dal objDal = new Dal();
            DataTable dtStudentInfo = new DataTable();
            dtStudentInfo = objDal.ExecuteTable(strLoadQuery, param);
            if (dtStudentInfo.Rows.Count > 0)
            {
                SId = objCon.ConToInt64(dtStudentInfo.Rows[0]["SId"]);
                FName = objCon.ConToStr(dtStudentInfo.Rows[0]["FName"]);
                LName = objCon.ConToStr(dtStudentInfo.Rows[0]["LName"]);
                DOB = objCon.ConToDT(dtStudentInfo.Rows[0]["DOB"]);
                AdmissionDate = objCon.ConToDT(dtStudentInfo.Rows[0]["AdmissionDate"]);
                Gender = objCon.ConToStr(dtStudentInfo.Rows[0]["Gender"]);
                MobileNumber = objCon.ConToStr(dtStudentInfo.Rows[0]["MobileNumber"]);
                EmailId = objCon.ConToStr(dtStudentInfo.Rows[0]["EmailId"]);
                FatherName = objCon.ConToStr(dtStudentInfo.Rows[0]["FatherName"]);
                FatherMobileNumber = objCon.ConToStr(dtStudentInfo.Rows[0]["FatherMobileNumber"]);
                MotherName = objCon.ConToStr(dtStudentInfo.Rows[0]["MotherName"]);
                Address = objCon.ConToStr(dtStudentInfo.Rows[0]["Address"]);
                City = objCon.ConToStr(dtStudentInfo.Rows[0]["City"]);
                State = objCon.ConToStr(dtStudentInfo.Rows[0]["State"]);
                Pincode = objCon.ConToStr(dtStudentInfo.Rows[0]["Pincode"]);
                Religion = objCon.ConToStr(dtStudentInfo.Rows[0]["Religion"]);
                CasteCategory = objCon.ConToStr(dtStudentInfo.Rows[0]["CasteCategory"]);
                Session = objCon.ConToStr(dtStudentInfo.Rows[0]["Session"]);
                PaymentType = objCon.ConToStr(dtStudentInfo.Rows[0]["PaymentType"]);
                AadharNumber = objCon.ConToStr(dtStudentInfo.Rows[0]["AadharNumber"]);
            }
        }

        public DataTable LoadStudentInfoImage()
        {
            String strLoadQuery = "Select StudentImage From StudentInfo where  SId = @SId ";


            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@SId", SId);

            Conversion objCon = new Conversion();
            Dal objDal = new Dal();
            DataTable dtStudentInfo = new DataTable();
            dtStudentInfo = objDal.ExecuteTable(strLoadQuery, param);
            return dtStudentInfo;
        }
        #endregion

        #region ShowAllData funtion

        //***********************************
        //This Function will perform the action for Loading the Data from Table  Tarif
        //***********************************
        public DataTable ShowAllStudentInfo()
        {
            String strLoadQuery = @"select FName+' '+isnull(LName,'') as Name,* from StudentInfo where isnull(Deleted,0)=0 order by SId asc     ";
            Conversion objCon = new Conversion();
            DebonoDLL.App_Code.DAL.Dal objDal = new DebonoDLL.App_Code.DAL.Dal();
            DataTable dtStudentInfoInfo = new DataTable();
            dtStudentInfoInfo = objDal.ExecuteTable(strLoadQuery);
            return dtStudentInfoInfo;
        }
        public DataTable ShowAllStudentInfo(string Session,string ClassName,string ClassSection)
        {
            string cond = "";
            string cond1 = "";
            if (Session != "Select" && Session != "")
            {
                cond1 = cond1 + " and SessionYear='" + Session + "'";
                cond = " and SId in (select SId from classInfo where 1=1 " + cond1 + ")";
            }
            if (ClassName != "Select" && ClassName != "")
            {
                cond1 = cond1 + " and ClassName='" + ClassName + "'";
                cond = " and SId in (select SId from classInfo where 1=1 " + cond1 + ")";
            }
            if (ClassSection != "Select" && ClassSection != "")
            {
                cond1 = cond1 + " and ClassSection='" + ClassSection + "'";
                cond = " and SId in (select SId from classInfo where 1=1 " + cond1 + ")";
            }
            String strLoadQuery = @"select FName+' '+isnull(LName,'') as Name,* from StudentInfo where isnull(Deleted,0)=0 "+cond+" order by SId asc     ";
            Conversion objCon = new Conversion();
            DebonoDLL.App_Code.DAL.Dal objDal = new DebonoDLL.App_Code.DAL.Dal();
            DataTable dtStudentInfoInfo = new DataTable();
            dtStudentInfoInfo = objDal.ExecuteTable(strLoadQuery);
            return dtStudentInfoInfo;
        }
        public DataTable countstudent(string Session, string ClassName, string ClassSection)
        {
            string cond = "";
            if (Session != "Select" && Session != "")
                cond = cond + " and SessionYear='" + Session + "'";
            if (ClassName != "Select" && ClassName != "")
                cond = cond + " and ClassName='" + ClassName + "'";
            if (ClassSection != "Select" && ClassSection != "")
                cond = cond + " and ClassSection='" + ClassSection + "'";
            String strLoadQuery = @"select count(SId)TotalStudent,PaymentType from classInfo where 1=1 "+cond+"  group by PaymentType";
            Conversion objCon = new Conversion();
            DebonoDLL.App_Code.DAL.Dal objDal = new DebonoDLL.App_Code.DAL.Dal();
            DataTable dtStudentInfoInfo = new DataTable();
            dtStudentInfoInfo = objDal.ExecuteTable(strLoadQuery);
            return dtStudentInfoInfo;
        }
        public DataTable ShowAllStudentFeesInfo()
        {
            String strLoadQuery = @"select CI.CId,SI.SId,FName+' '+isnull(LName,'') as Name,DOB,MobileNumber,Gender,AadharNumber,FatherName,CI.PaymentType,CI.ClassName,CI.ClassSection,CI.AdmissionDate,isnull((select Sum(Amount) from PaymentHistory where CId=CI.CId),0) as PaidFees,isnull((select Sum(Amount) from PaymentHistory where PaymentType='Admission Fee' and CId=CI.CId),0) as AdmissionFee,isnull((select Sum(Amount) from PaymentHistory where PaymentType='Library Fee' and CId=CI.CId),0) as LibraryFee,isnull((select Sum(Amount) from PaymentHistory where PaymentType='F.D.B.R.I. Fee' and CId=CI.CId),0) as FDBRIFee,isnull((select Sum(Amount) from PaymentHistory where PaymentType='Maintenance Fee' and CId=CI.CId),0) as MaintenceFee,isnull((select Sum(Amount) from PaymentHistory where PaymentType='Half Yearly Exam Fee' and CId=CI.CId),0) as HalfyFee,isnull((select Sum(Amount) from PaymentHistory where PaymentType='Annual Exam Fee' and CId=CI.CId),0) as AnnualExamFee,isnull((select Sum(GeneratorFee) from PaymentHistory where PaymentType='Monthly Fee' and CId=CI.CId),0) as GeneratorFee,isnull((select Sum(ComputerFee) from PaymentHistory where PaymentType='Monthly Fee' and CId=CI.CId),0) as ComputerFee,isnull((select Sum(PTAFee) from PaymentHistory where PaymentType='Monthly Fee' and CId=CI.CId),0) as PTAFee from StudentInfo SI join ClassInfo CI on CI.SId=SI.SId where isnull(SI.Deleted,0)=0 and CI.SessionYear=@Session order by FName asc";
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@Session", Session);
            Conversion objCon = new Conversion();
            Dal objDal = new Dal();
            DataTable dtStudentInfo = new DataTable();
            dtStudentInfo = objDal.ExecuteTable(strLoadQuery, param);
            return dtStudentInfo;
        }

        public DataTable ShowAllStudentFeesInfo(string Session, string ClassName, string PaymentType, string ClassSection)
        {
            string cond = "";
            if (Session != "Select" && Session != "")
                cond = cond + " and CI.SessionYear='" + Session + "'";
            if (ClassName != "Select" && ClassName != "")
                cond = cond + " and CI.ClassName='" + ClassName + "'";
            if (PaymentType != "Select" && PaymentType != "")
                cond = cond + " and CI.PaymentType='" + PaymentType + "'";
            if (ClassSection != "Select" && ClassSection != "")
                cond = cond + " and CI.ClassSection='" + ClassSection + "'";
            String strLoadQuery = @"select CI.CId,SI.SId,FName+' '+isnull(LName,'') as Name,DOB,MobileNumber,Gender,AadharNumber,FatherName,CI.PaymentType,CI.ClassName,CI.ClassSection,CI.AdmissionDate,isnull((select Sum(Amount) from PaymentHistory where CId=CI.CId),0) as PaidFees,isnull((select Sum(Amount) from PaymentHistory where PaymentType='Admission Fee' and CId=CI.CId),0) as AdmissionFee,isnull((select Sum(Amount) from PaymentHistory where PaymentType='Library Fee' and CId=CI.CId),0) as LibraryFee,isnull((select Sum(Amount) from PaymentHistory where PaymentType='F.D.B.R.I. Fee' and CId=CI.CId),0) as FDBRIFee,isnull((select Sum(Amount) from PaymentHistory where PaymentType='Maintenance Fee' and CId=CI.CId),0) as MaintenceFee,isnull((select Sum(Amount) from PaymentHistory where PaymentType='Half Yearly Exam Fee' and CId=CI.CId),0) as HalfyFee,isnull((select Sum(Amount) from PaymentHistory where PaymentType='Annual Exam Fee' and CId=CI.CId),0) as AnnualExamFee,isnull((select Sum(GeneratorFee) from PaymentHistory where PaymentType='Monthly Fee' and CId=CI.CId),0) as GeneratorFee,isnull((select Sum(ComputerFee) from PaymentHistory where PaymentType='Monthly Fee' and CId=CI.CId),0) as ComputerFee,isnull((select Sum(PTAFee) from PaymentHistory where PaymentType='Monthly Fee' and CId=CI.CId),0) as PTAFee from StudentInfo SI join ClassInfo CI on CI.SId=SI.SId where isnull(SI.Deleted,0)=0 " + cond + " order by FName asc";
            Conversion objCon = new Conversion();
            Dal objDal = new Dal();
            DataTable dtStudentInfo = new DataTable();
            dtStudentInfo = objDal.ExecuteTable(strLoadQuery);
            return dtStudentInfo;
        }

        public DataTable ShowAllStudentFeesInfobystudent()
        {
            String strLoadQuery = @"select CI.CId,FName+' '+isnull(LName,'') as Name,DOB,MobileNumber,Gender,AadharNumber,FatherName,CI.PaymentType,CI.ClassName,CI.ClassSection,CI.AdmissionDate,(select Sum(Amount) from PaymentHistory where CId=CI.CId) as PaidFees from StudentInfo SI join ClassInfo CI on CI.SId=SI.SId where isnull(SI.Deleted,0)=0 and CI.SessionYear=@Session and SI.SId=@SId order by FName asc";
            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@Session", Session);
            param[1] = new SqlParameter("@SId", SId);
            Conversion objCon = new Conversion();
            Dal objDal = new Dal();
            DataTable dtStudentInfo = new DataTable();
            dtStudentInfo = objDal.ExecuteTable(strLoadQuery, param);
            return dtStudentInfo;
        }

        public DataTable ShowAllIssueBookstudent()
        {
            String strLoadQuery = @"select SId,FName+' '+isnull(LName,'') as StudentName from StudentInfo where isnull(Deleted,0)=0 and SId in (select SId from rentmst where ReturnDate is null) order by FName asc";
            Conversion objCon = new Conversion();
            Dal objDal = new Dal();
            DataTable dtStudentInfo = new DataTable();
            dtStudentInfo = objDal.ExecuteTable(strLoadQuery);
            return dtStudentInfo;
        }
        public DataTable ShowAllIssueBookbystudent()
        {
            String strLoadQuery = @"select BookId,BookName from BookMst where BookId in (select BookId from rentmst where ReturnDate is null and SId=@SId) order by BookName asc";
            SqlParameter[] param = new SqlParameter[2];
            param[1] = new SqlParameter("@SId", SId);
            Conversion objCon = new Conversion();
            Dal objDal = new Dal();
            DataTable dtStudentInfo = new DataTable();
            dtStudentInfo = objDal.ExecuteTable(strLoadQuery, param);
            return dtStudentInfo;
        }
        #endregion

        #region check exist  funtion

        //***********************************
        //This Function will perform the action for Loading the Data from Table  Tarif
        //***********************************
        public bool CheckCounterMatCategoryUniq()
        {
            Conversion objCon = new Conversion();
            string strCheckQuery = "Select count(*)as count From StudentInfo where Upper(FName)='" + this.FName + "' and SId != @SId";
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@SId", SId);
            DebonoDLL.App_Code.DAL.Dal objDal = new DebonoDLL.App_Code.DAL.Dal();
            DataTable dtRecords = new DataTable();
            dtRecords = objDal.ExecuteTable(strCheckQuery, param);
            if (dtRecords != null && dtRecords.Rows.Count > 0 && objCon.ConToInt(dtRecords.Rows[0][0]) == 0)
                return true;
            else
                return false;
        }

        #endregion
        #endregion
    }

}