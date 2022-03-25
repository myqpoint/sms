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
    public class BookBo
    {
        #region Field Properties

        ///<summary>
        ///BookID
        ///<summary>
        ///<remarks>
        ///Property variable for BookID
        ///<remarks>
        private Int64 BookID;
        public Int64 _BookID
        {
            get
            {
                return BookID;
            }
            set
            {
                BookID = value;
            }
        }

        ///<summary>
        ///BookName
        ///<summary>
        ///<remarks>
        ///Property variable for BookName
        ///<remarks>
        private String BookName;
        public String _BookName
        {
            get
            {
                return BookName;
            }
            set
            {
                BookName = value;
            }
        }
        ///<summary>
        ///Author
        ///<summary>
        ///<remarks>
        ///Property variable for Author
        ///<remarks>
        private String Author;
        public String _Author
        {
            get
            {
                return Author;
            }
            set
            {
                Author = value;
            }
        }
        ///<summary>
        ///Detail
        ///<summary>
        ///<remarks>
        ///Property variable for Detail
        ///<remarks>
        private String Detail;
        public String _Detail
        {
            get
            {
                return Detail;
            }
            set
            {
                Detail = value;
            }
        }
        ///<summary>
        ///Price
        ///<summary>
        ///<remarks>
        ///Property variable for Price
        ///<remarks>
        private decimal Price;
        public decimal _Price
        {
            get
            {
                return Price;
            }
            set
            {
                Price = value;
            }
        }
        ///<summary>
        ///PublicationId
        ///<summary>
        ///<remarks>
        ///Property variable for PublicationId
        ///<remarks>
        private Int16 PublicationId;
        public Int16 _PublicationId
        {
            get
            {
                return PublicationId;
            }
            set
            {
                PublicationId = value;
            }
        }
        ///<summary>
        ///ClassId
        ///<summary>
        ///<remarks>
        ///Property variable for ClassId
        ///<remarks>
        private Int16 ClassId;
        public Int16 _ClassId
        {
            get
            {
                return ClassId;
            }
            set
            {
                ClassId = value;
            }
        }
        ///<summary>
        ///Quantities
        ///<summary>
        ///<remarks>
        ///Property variable for Quantities
        ///<remarks>
        private Int16 Quantities;
        public Int16 _Quantities
        {
            get
            {
                return Quantities;
            }
            set
            {
                Quantities = value;
            }
        }
        ///<summary>
        ///AvailableQty
        ///<summary>
        ///<remarks>
        ///Property variable for AvailableQty
        ///<remarks>
        private Int16 AvailableQty;
        public Int16 _AvailableQty
        {
            get
            {
                return AvailableQty;
            }
            set
            {
                AvailableQty = value;
            }
        }
        ///<summary>
        ///RentQty
        ///<summary>
        ///<remarks>
        ///Property variable for RentQty
        ///<remarks>
        private Int16 RentQty;
        public Int16 _RentQty
        {
            get
            {
                return RentQty;
            }
            set
            {
                RentQty = value;
            }
        }
        ///<summary>
        ///ISBN
        ///<summary>
        ///<remarks>
        ///Property variable for ISBN
        ///<remarks>
        private String ISBN;
        public String _ISBN
        {
            get
            {
                return ISBN;
            }
            set
            {
                ISBN = value;
            }
        }
        ///<summary>
        ///Barcode
        ///<summary>
        ///<remarks>
        ///Property variable for Barcode
        ///<remarks>
        private String Barcode;
        public String _Barcode
        {
            get
            {
                return Barcode;
            }
            set
            {
                Barcode = value;
            }
        }

        ///<summary>
        ///SubjectId
        ///<summary>
        ///<remarks>
        ///Property variable for SubjectId
        ///<remarks>
        private Int16 SubjectId;
        public Int16 _SubjectId
        {
            get
            {
                return SubjectId;
            }
            set
            {
                SubjectId = value;
            }
        }

        ///<summary>
        ///BookImage
        ///<summary>
        ///<remarks>
        ///Property variable for BookImage
        ///<remarks>
        private Byte[] BookImage;
        public Byte[] _BookImage
        {
            get
            {
                return BookImage;
            }
            set
            {
                BookImage = value;
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

            BookID = objCon.ConToInt64(drMainData["BookID"]);
            BookName = objCon.ConToStr(drMainData["BookName"]);
            Author = objCon.ConToStr(drMainData["Author"]);
            Price = objCon.ConToDec(drMainData["Price"]);
            Quantities = objCon.ConToInt16(drMainData["Quantities"]);
            AvailableQty = objCon.ConToInt16(drMainData["AvailableQty"]);
            RentQty = objCon.ConToInt16(drMainData["RentQty"]);
            ISBN = objCon.ConToStr(drMainData["ISBN"]);
            Barcode = objCon.ConToStr(drMainData["Barcode"]);
            
        }
        #endregion

        #region Save  functions

        //***********************************
        //This Function will perform the action for Saving the Data into Table  BookMst
        //***********************************
        public int SaveBookMst()
        {
            String strInsertQuery = "";
            SqlParameter[] param = new SqlParameter[14];
            if (BookImage != null)
            {
                strInsertQuery = "insert into BookMst(BookName,Author,Detail,Price,PublicationId,ClassId,Quantities,AvailableQty,RentQty,ISBN,Barcode,SubjectId,CreatedBy,CreatedOn,BookImage) " +
                    " values(@BookName,@Author,@Detail,@Price,@PublicationId,@ClassId,@Quantities,@AvailableQty,@RentQty,@ISBN,@Barcode,@SubjectId,@CreatedBy,getdate(),@BookImage) ";
                param[13] = new SqlParameter("@BookImage", BookImage);
            }
            else
            {
                strInsertQuery = "insert into BookMst(BookName,Author,Detail,Price,PublicationId,ClassId,Quantities,AvailableQty,RentQty,ISBN,Barcode,SubjectId,CreatedBy,CreatedOn) " +
                   " values(@BookName,@Author,@Detail,@Price,@PublicationId,@ClassId,@Quantities,@AvailableQty,@RentQty,@ISBN,@Barcode,@SubjectId,@CreatedBy,getdate()) ";
            }

            param[0] = new SqlParameter("@BookName", BookName);
            param[1] = new SqlParameter("@Author", Author);
            param[2] = new SqlParameter("@Detail", Detail);
            param[3] = new SqlParameter("@Price", Price);
            param[4] = new SqlParameter("@PublicationId", PublicationId);
            param[5] = new SqlParameter("@ClassId", ClassId);
            param[6] = new SqlParameter("@Quantities", Quantities);
            param[7] = new SqlParameter("@AvailableQty", AvailableQty);
            param[8] = new SqlParameter("@RentQty", RentQty);
            param[9] = new SqlParameter("@ISBN", ISBN);
            param[10] = new SqlParameter("@Barcode", Barcode);
            param[11] = new SqlParameter("@SubjectId", SubjectId);
            param[12] = new SqlParameter("@CreatedBy", CreatedBy);
            Dal objDal = new Dal();
            int check = 0;
            check = objDal.ExecuteDataIdentity(strInsertQuery, param);
            if (check == 1)
            {
                BookID = objDal.ExecuteForID("select Max(BookID) from BookMst");
            }
            return check;
        }
        #endregion

        #region Update functions

        //***********************************
        //This Function will perform the action for Updating the Data into Table  BookMst for Primary Column BookID
        //***********************************
        public int UpdateBookMst()
        {
            String strUpdateQuery ="";
            SqlParameter[] param = new SqlParameter[15];
            if (BookImage != null)
            {
                strUpdateQuery = "update BookMst Set BookName=@BookName,Author=@Author,Detail=@Detail,Price=@Price,PublicationId=@PublicationId,ClassId=@ClassId,Quantities=@Quantities,AvailableQty=@AvailableQty,ISBN=@ISBN,Barcode=@Barcode,SubjectId=@SubjectId,BookImage=@BookImage where BookID= @BookID";
                param[14] = new SqlParameter("@BookImage", BookImage);
            }
            else
            {
                strUpdateQuery = "update BookMst Set BookName=@BookName,Author=@Author,Detail=@Detail,Price=@Price,PublicationId=@PublicationId,ClassId=@ClassId,Quantities=@Quantities,AvailableQty=@AvailableQty,ISBN=@ISBN,Barcode=@Barcode,SubjectId=@SubjectId where BookID= @BookID";
            }
            param[0] = new SqlParameter("@BookName", BookName);
            param[1] = new SqlParameter("@Author", Author);
            param[2] = new SqlParameter("@Detail", Detail);
            param[3] = new SqlParameter("@Price", Price);
            param[4] = new SqlParameter("@PublicationId", PublicationId);
            param[5] = new SqlParameter("@ClassId", ClassId);
            param[6] = new SqlParameter("@Quantities", Quantities);
            param[7] = new SqlParameter("@AvailableQty", AvailableQty);
            //param[8] = new SqlParameter("@RentQty", RentQty);
            param[9] = new SqlParameter("@ISBN", ISBN);
            param[10] = new SqlParameter("@Barcode", Barcode);
            param[11] = new SqlParameter("@SubjectId", SubjectId);
            param[12] = new SqlParameter("@CreatedBy", CreatedBy);
            param[13] = new SqlParameter("@BookID", BookID);
            
               
            Dal objDal = new Dal();
            int check = 0;
            check = objDal.ExecuteDataIdentity(strUpdateQuery, param);
            return check;
        }
        #endregion

        #region Delete functions

        //***********************************
        //This Function will perform the action for Deleteing the Data into Table  BookMst for Primary Column BookID
        //***********************************
        public int DeleteBookMst()
        {
            String strDeleteQuery = "Delete from BookMst where  BookID = @BookID ";
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@BookID", BookID);
            Dal objDal = new Dal();
            int check = 0;
            check = objDal.ExecuteDataIdentity(strDeleteQuery, param);
            return check;
        }

        #endregion

        #region Load functions

        //***********************************
        //This Function will perform the action for Loading the Data from Table  BookMst for Primary Column BookID
        //***********************************
        public void LoadBookMst()
        {
            String strLoadQuery = "Select * From BookMst where  BookID = @BookID ";


            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@BookID", BookID);

            Conversion objCon = new Conversion();
            Dal objDal = new Dal();
            DataTable dtBookMst = new DataTable();
            dtBookMst = objDal.ExecuteTable(strLoadQuery, param);
            if (dtBookMst.Rows.Count > 0)
            {
                BookID = objCon.ConToInt64(dtBookMst.Rows[0]["BookID"]);
                BookName = objCon.ConToStr(dtBookMst.Rows[0]["BookName"]);
                Author = objCon.ConToStr(dtBookMst.Rows[0]["Author"]);
                Price = objCon.ConToDec(dtBookMst.Rows[0]["Price"]);
                Quantities = objCon.ConToInt16(dtBookMst.Rows[0]["Quantities"]);
                AvailableQty = objCon.ConToInt16(dtBookMst.Rows[0]["AvailableQty"]);
                RentQty = objCon.ConToInt16(dtBookMst.Rows[0]["RentQty"]);
                ISBN = objCon.ConToStr(dtBookMst.Rows[0]["ISBN"]);
                Detail = objCon.ConToStr(dtBookMst.Rows[0]["Detail"]);
                Barcode = objCon.ConToStr(dtBookMst.Rows[0]["Barcode"]);
                SubjectId = objCon.ConToInt16(dtBookMst.Rows[0]["SubjectId"]);
                PublicationId = objCon.ConToInt16(dtBookMst.Rows[0]["PublicationId"]);
                ClassId = objCon.ConToInt16(dtBookMst.Rows[0]["ClassId"]);
            }
        }
        public DataTable LoadBookImage()
        {
            String strLoadQuery = "Select BookImage From BookMst where  BookID = @BookID ";


            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@BookID", BookID);

            Conversion objCon = new Conversion();
            Dal objDal = new Dal();
            DataTable dtStaff = new DataTable();
            dtStaff = objDal.ExecuteTable(strLoadQuery, param);
            return dtStaff;
        }

        public DataTable LoadBookByPublication()
        {
            String strLoadQuery = "Select * From BookMst where  PublicationId = @PublicationId ";


            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@PublicationId", PublicationId);

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
        public DataTable ShowBookMst()
        {
            String strLoadQuery = "Select * From BookMst order by  BookName asc";
            Conversion objCon = new Conversion();
            Dal objDal = new Dal();
            DataTable dtBookMst = new DataTable();
            dtBookMst = objDal.ExecuteTable(strLoadQuery);
            return dtBookMst;
        }

       

        #endregion
        #endregion
    }

}