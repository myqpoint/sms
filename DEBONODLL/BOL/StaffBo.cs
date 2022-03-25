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
    public class StaffBo
    {
        #region Field Properties

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
        ///Name
        ///<summary>
        ///<remarks>
        ///Property variable for Name
        ///<remarks>
        private String Name;
        public String _Name
        {
            get
            {
                return Name;
            }
            set
            {
                Name = value;
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
        ///JoinningDate
        ///<summary>
        ///<remarks>
        ///Property variable for JoinningDate
        ///<remarks>
        private DateTime JoinningDate;
        public DateTime _JoinningDate
        {
            get
            {
                return JoinningDate;
            }
            set
            {
                JoinningDate = value;
            }
        }


        ///<summary>
        ///SalaryAmount
        ///<summary>
        ///<remarks>
        ///Property variable for SalaryAmount
        ///<remarks>
        private Decimal SalaryAmount;
        public Decimal _SalaryAmount
        {
            get
            {
                return SalaryAmount;
            }
            set
            {
                SalaryAmount = value;
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
        ///Qualification
        ///<summary>
        ///<remarks>
        ///Property variable for Qualification
        ///<remarks>
        private String Qualification;
        public String _Qualification
        {
            get
            {
                return Qualification;
            }
            set
            {
                Qualification = value;
            }
        }

        ///<summary>
        ///StaffImage
        ///<summary>
        ///<remarks>
        ///Property variable for StaffImage
        ///<remarks>
        private Byte[] StaffImage;
        public Byte[] _StaffImage
        {
            get
            {
                return StaffImage;
            }
            set
            {
                StaffImage = value;
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

            StaffId = objCon.ConToInt64(drMainData["StaffId"]);
            Name = objCon.ConToStr(drMainData["Name"]);
            FatherName = objCon.ConToStr(drMainData["FatherName"]);
            Gender = objCon.ConToStr(drMainData["Gender"]);
            DOB = objCon.ConToDT(drMainData["DOB"]);
            MobileNumber = objCon.ConToStr(drMainData["MobileNumber"]);
            EmailId = objCon.ConToStr(drMainData["EmailId"]);
            AadharNumber = objCon.ConToStr(drMainData["AadharNumber"]);
            JoinningDate = objCon.ConToDT(drMainData["JoinningDate"]);
            SalaryAmount = objCon.ConToDec(drMainData["SalaryAmount"]);
            Address = objCon.ConToStr(drMainData["Address"]);
            Qualification = objCon.ConToStr(drMainData["Qualification"]);
            Religion = objCon.ConToStr(drMainData["Religion"]);
            CasteCategory = objCon.ConToStr(drMainData["CasteCategory"]);
            CreatedBy = objCon.ConToStr(drMainData["CreatedBy"]);
            CreatedOn = objCon.ConToDT(drMainData["CreatedOn"]);
        }
        #endregion

        #region Save  functions

        //***********************************
        //This Function will perform the action for Saving the Data into Table  Staff
        //***********************************
        public int SaveStaff()
        {
            String strInsertQuery = "";
            SqlParameter[] param = new SqlParameter[22];
            if (StaffImage != null)
            {
                strInsertQuery = "insert into Staff( Name,DOB,JoinningDate,MobileNumber,Gender,AadharNumber,EmailId,FatherName,Address,Qualification,SalaryAmount,StaffImage,Religion,CasteCategory,CreatedOn,CreatedBy  ) " +
               " values( @Name,@DOB,@JoinningDate,@MobileNumber,@Gender,@AadharNumber,@EmailId,@FatherName,@Address,@Qualification,@SalaryAmount,@StaffImage,@Religion,@CasteCategory,@CreatedOn,@CreatedBy  ) ";
                param[4] = new SqlParameter("@StaffImage", StaffImage);
            }
            else
            {
                strInsertQuery = "insert into Staff( Name,DOB,JoinningDate,MobileNumber,Gender,AadharNumber,EmailId,FatherName,Address,Qualification,SalaryAmount,Religion,CasteCategory,CreatedOn,CreatedBy  ) " +
              " values( @Name,@DOB,@JoinningDate,@MobileNumber,@Gender,@AadharNumber,@EmailId,@FatherName,@Address,@Qualification,@SalaryAmount,@Religion,@CasteCategory,@CreatedOn,@CreatedBy  ) ";
            }

            param[0] = new SqlParameter("@Name", Name);
            param[1] = new SqlParameter("@FatherName", FatherName);
            param[2] = new SqlParameter("@DOB", DOB);
            param[3] = new SqlParameter("@MobileNumber", MobileNumber);
            param[5] = new SqlParameter("@Gender", Gender);
            param[6] = new SqlParameter("@EmailId", EmailId);
            param[7] = new SqlParameter("@JoinningDate", JoinningDate);
            param[8] = new SqlParameter("@AadharNumber", AadharNumber);
            param[9] = new SqlParameter("@Address", Address);
            param[10] = new SqlParameter("@Qualification", Qualification);
            param[11] = new SqlParameter("@SalaryAmount", SalaryAmount);
            param[12] = new SqlParameter("@CreatedOn", DateTime.Now.Date);
            param[13] = new SqlParameter("@CreatedBy", CreatedBy);
            param[14] = new SqlParameter("@Religion", Religion);
            param[15] = new SqlParameter("@CasteCategory", CasteCategory);
            
            Dal objDal = new Dal();
            int check = 0;
            check = objDal.ExecuteDataIdentity(strInsertQuery, param);
            if (check == 1)
            {
                StaffId = objDal.ExecuteForID("select Max(StaffId) from Staff");
            }
            return check;
        }
        #endregion

        #region Update functions

        //***********************************
        //This Function will perform the action for Updating the Data into Table  Staff for Primary Column StaffId
        //***********************************
        public int UpdateStaff()
        {
            String strUpdateQuery ="";
            SqlParameter[] param = new SqlParameter[23];
            if (StaffImage != null)
            {
                strUpdateQuery = "update Staff Set Name=@Name,DOB=@DOB,JoinningDate=@JoinningDate,MobileNumber=@MobileNumber,Gender=@Gender,AadharNumber=@AadharNumber,Religion=@Religion,CasteCategory=@CasteCategory,EmailId=@EmailId,FatherName=@FatherName,Address=@Address,Qualification=@Qualification,SalaryAmount=@SalaryAmount,StaffImage=@StaffImage,ModifiedOn=@ModifiedOn,ModifiedBy=@ModifiedBy where StaffId= @StaffId";
                param[4] = new SqlParameter("@StaffImage", StaffImage);
            }
            else
            {
                strUpdateQuery = "update Staff Set Name=@Name,DOB=@DOB,JoinningDate=@JoinningDate,MobileNumber=@MobileNumber,Gender=@Gender,AadharNumber=@AadharNumber,Religion=@Religion,CasteCategory=@CasteCategory,EmailId=@EmailId,FatherName=@FatherName,Address=@Address,Qualification=@Qualification,SalaryAmount=@SalaryAmount,ModifiedOn=@ModifiedOn,ModifiedBy=@ModifiedBy where StaffId= @StaffId";
            }


            param[0] = new SqlParameter("@Name", Name);
            param[1] = new SqlParameter("@FatherName", FatherName);
            param[2] = new SqlParameter("@DOB", DOB);
            param[3] = new SqlParameter("@MobileNumber", MobileNumber);
            param[5] = new SqlParameter("@Gender", Gender);
            param[6] = new SqlParameter("@EmailId", EmailId);
            param[7] = new SqlParameter("@JoinningDate", JoinningDate);
            param[8] = new SqlParameter("@AadharNumber", AadharNumber);
            param[9] = new SqlParameter("@Address", Address);
            param[10] = new SqlParameter("@Qualification", Qualification);
            param[11] = new SqlParameter("@SalaryAmount", SalaryAmount);
            param[12] = new SqlParameter("@ModifiedOn", DateTime.Now.Date);
            param[13] = new SqlParameter("@ModifiedBy", CreatedBy);
            param[14] = new SqlParameter("@StaffId", StaffId);
            param[15] = new SqlParameter("@Religion", Religion);
            param[16] = new SqlParameter("@CasteCategory", CasteCategory);
            Dal objDal = new Dal();
            int check = 0;
            check = objDal.ExecuteDataIdentity(strUpdateQuery, param);
            return check;
        }
        #endregion

        #region Delete functions

        //***********************************
        //This Function will perform the action for Deleteing the Data into Table  Staff for Primary Column StaffId
        //***********************************
        public int DeleteStaff()
        {
            String strDeleteQuery = "Update Staff set Deleted=1 where  StaffId = @StaffId ";


            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@StaffId", StaffId);

            Dal objDal = new Dal();
            int check = 0;
            check = objDal.ExecuteDataIdentity(strDeleteQuery, param);
            return check;
        }
        #endregion

        #region Load functions

        //***********************************
        //This Function will perform the action for Loading the Data from Table  Staff for Primary Column StaffId
        //***********************************
        public void LoadStaff()
        {
            String strLoadQuery = "Select * From Staff where  StaffId = @StaffId ";


            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@StaffId", StaffId);

            Conversion objCon = new Conversion();
            Dal objDal = new Dal();
            DataTable dtStaff = new DataTable();
            dtStaff = objDal.ExecuteTable(strLoadQuery, param);
            if (dtStaff.Rows.Count > 0)
            {
                StaffId = objCon.ConToInt64(dtStaff.Rows[0]["StaffId"]);
                Name = objCon.ConToStr(dtStaff.Rows[0]["Name"]);
                DOB = objCon.ConToDT(dtStaff.Rows[0]["DOB"]);
                JoinningDate = objCon.ConToDT(dtStaff.Rows[0]["JoinningDate"]);
                Gender = objCon.ConToStr(dtStaff.Rows[0]["Gender"]);
                MobileNumber = objCon.ConToStr(dtStaff.Rows[0]["MobileNumber"]);
                EmailId = objCon.ConToStr(dtStaff.Rows[0]["EmailId"]);
                FatherName = objCon.ConToStr(dtStaff.Rows[0]["FatherName"]);
                Qualification = objCon.ConToStr(dtStaff.Rows[0]["Qualification"]);
                SalaryAmount = objCon.ConToDec(dtStaff.Rows[0]["SalaryAmount"]);
                Religion = objCon.ConToStr(dtStaff.Rows[0]["Religion"]);
                CasteCategory = objCon.ConToStr(dtStaff.Rows[0]["CasteCategory"]);
                Address = objCon.ConToStr(dtStaff.Rows[0]["Address"]);
                AadharNumber = objCon.ConToStr(dtStaff.Rows[0]["AadharNumber"]);
            }
        }

        public DataTable LoadStaffImage()
        {
            String strLoadQuery = "Select StaffImage From Staff where  StaffId = @StaffId ";


            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@StaffId", StaffId);

            Conversion objCon = new Conversion();
            Dal objDal = new Dal();
            DataTable dtStaff = new DataTable();
            dtStaff = objDal.ExecuteTable(strLoadQuery, param);
            return dtStaff;
        }
        #endregion

        #region ShowAllData funtion

        //***********************************
        //This Function will perform the action for Loading the Data from Table  Tarif
        //***********************************
        public DataTable ShowAllStaff()
        {
            String strLoadQuery = @"select * from Staff where isnull(Deleted,0)=0 order by StaffId asc     ";
            Conversion objCon = new Conversion();
            DebonoDLL.App_Code.DAL.Dal objDal = new DebonoDLL.App_Code.DAL.Dal();
            DataTable dtStaffInfo = new DataTable();
            dtStaffInfo = objDal.ExecuteTable(strLoadQuery);
            return dtStaffInfo;
        }
      
        #endregion

        #region check exist  funtion

        //***********************************
        //This Function will perform the action for Loading the Data from Table  Tarif
        //***********************************
        public bool CheckCounterMatCategoryUniq()
        {
            Conversion objCon = new Conversion();
            string strCheckQuery = "Select count(*)as count From Staff where Upper(FName)='" + this.Name + "' and StaffId != @StaffId";
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@StaffId", StaffId);
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