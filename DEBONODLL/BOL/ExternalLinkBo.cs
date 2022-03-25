
#region Refrence Declration
using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using DebonoDLL.App_Code.BOL;
using DebonoDLL.App_Code.DAL;

#endregion


namespace DebonoDLL.BOL
{
    public class ExternalLinkBo
    {
        #region Field Properties

        public static string serverImagePath = "";
        public static string serverUsername = "";
        public static string serverPwd = "";

        ///<summary>
        ///ExernalId
        ///<summary>
        ///<remarks>
        ///Property variable for ExernalId
        ///<remarks>
        private Int64 ExernalId;
        public Int64 _ExernalId
        {
            get
            {
                return ExernalId;
            }
            set
            {
                ExernalId = value;
            }
        }

        ///<summary>
        ///FormType
        ///<summary>
        ///<remarks>
        ///Property variable for FormType
        ///<remarks>
        private String FormType;
        public String _FormType
        {
            get
            {
                return FormType;
            }
            set
            {
                FormType = value;
            }
        }

        ///<summary>
        ///FormId
        ///<summary>
        ///<remarks>
        ///Property variable for FormId
        ///<remarks>
        private Int64 FormId;
        public Int64 _FormId
        {
            get
            {
                return FormId;
            }
            set
            {
                FormId = value;
            }
        }

        ///<summary>
        ///LastUpdate
        ///<summary>
        ///<remarks>
        ///Property variable for LastUpdate
        ///<remarks>
        private DateTime LastUpdate;
        public DateTime _LastUpdate
        {
            get
            {
                return LastUpdate;
            }
            set
            {
                LastUpdate = value;
            }
        }

        ///<summary>
        ///LinkDescription
        ///<summary>
        ///<remarks>
        ///Property variable for LinkDescription
        ///<remarks>
        private String LinkDescription;
        public String _LinkDescription
        {
            get
            {
                return LinkDescription;
            }
            set
            {
                LinkDescription = value;
            }
        }

        ///<summary>
        ///LinkFile
        ///<summary>
        ///<remarks>
        ///Property variable for LinkFile
        ///<remarks>
        private String LinkFile;
        public String _LinkFile
        {
            get
            {
                return LinkFile;
            }
            set
            {
                LinkFile = value;
            }
        }

        ///<summary>
        ///LinkFileType
        ///<summary>
        ///<remarks>
        ///Property variable for LinkFileType
        ///<remarks>
        private String LinkFileType;
        public String _LinkFileType
        {
            get
            {
                return LinkFileType;
            }
            set
            {
                LinkFileType = value;
            }
        }


        ///<summary>
        ///Path
        ///<summary>
        ///<remarks>
        ///Property variable for Path
        ///<remarks>
        private String Path;
        public String _Path
        {
            get
            {
                return Path;
            }
            set
            {
                Path = value;
            }
        }


        ///<summary>
        ///Image
        ///<summary>
        ///<remarks>
        ///Property variable for Image
        ///<remarks>
        private Byte[] Image;
        public Byte[] _Image
        {
            get
            {
                return Image;
            }
            set
            {
                Image = value;
            }
        }

        private Boolean isDrawing;
        public Boolean _isDrawing
        {
            get
            {
                return isDrawing;
            }
            set
            {
                isDrawing = value;
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

            ExernalId = objCon.ConToInt64(drMainData["ExernalId"]);
            FormType = objCon.ConToStr(drMainData["FormType"]);
            FormId = objCon.ConToInt64(drMainData["FormId"]);
            LastUpdate = objCon.ConToDT(drMainData["LastUpdate"]);
            LinkDescription = objCon.ConToStr(drMainData["LinkDescription"]);
            LinkFile = objCon.ConToStr(drMainData["LinkFile"]);
            LinkFileType = objCon.ConToStr(drMainData["LinkFileType"]);
            Path = objCon.ConToStr(drMainData["Path"]);
            isDrawing = objCon.ConTobool(drMainData["isDrawing"]);

        }
        #endregion

        #region Save  functions

        //***********************************
        //This Function will perform the action for Saving the Data into Table  ExternalLink
        //***********************************
        public int SaveExternalLink()
        {
            String strInsertQuery = "insert into ExternalLink( FormType , FormId , LastUpdate , LinkDescription , LinkFile , LinkFileType ,Path ,isDrawing ) " +
            " values( @FormType , @FormId , @LastUpdate , @LinkDescription , @LinkFile , @LinkFileType , @Path,@isDrawing  ) ";


            SqlParameter[] param = new SqlParameter[8];
            param[0] = new SqlParameter("@FormType", FormType);
            param[1] = new SqlParameter("@FormId", FormId);
            param[2] = new SqlParameter("@LastUpdate", LastUpdate);
            param[3] = new SqlParameter("@LinkDescription", LinkDescription);
            param[4] = new SqlParameter("@LinkFile", LinkFile);
            param[5] = new SqlParameter("@LinkFileType", LinkFileType);
            param[6] = new SqlParameter("@Path", Path);
            param[7] = new SqlParameter("@isDrawing", isDrawing);

            Dal objDal = new Dal();
            int check = 0;
            check = objDal.ExecuteDataIdentity(strInsertQuery, param);
            if (check == 1)
            {
                ExernalId = objDal.ExecuteForID("select Max(ExernalId) from ExternalLink");
            }
            return check;
        }
        #endregion

        #region Update functions

        //***********************************
        //This Function will perform the action for Updating the Data into Table  ExternalLink for Primary Column ExernalId
        //***********************************
        public int UpdateExternalLink()
        {
            String strUpdateQuery = "update ExternalLink Set FormType = @FormType , FormId = @FormId , LastUpdate = @LastUpdate , LinkDescription = @LinkDescription , LinkFile = @LinkFile , LinkFileType = @LinkFileType , Path = @Path ,Image  = @Image ,isDrawing=@isDrawing  where ExernalId= @ExernalId";


            SqlParameter[] param = new SqlParameter[10];
            param[0] = new SqlParameter("@ExernalId", ExernalId);
            param[1] = new SqlParameter("@FormType", FormType);
            param[2] = new SqlParameter("@FormId", FormId);
            param[3] = new SqlParameter("@LastUpdate", LastUpdate);
            param[4] = new SqlParameter("@LinkDescription", LinkDescription);
            param[5] = new SqlParameter("@LinkFile", LinkFile);
            param[6] = new SqlParameter("@LinkFileType", LinkFileType);
            param[7] = new SqlParameter("@Path", Path);
            param[8] = new SqlParameter("@Image", Image);
            param[9] = new SqlParameter("@isDrawing", isDrawing);

            Dal objDal = new Dal();
            int check = 0;
            check = objDal.ExecuteDataIdentity(strUpdateQuery, param);
            return check;
        }
        #endregion

        #region Delete functions

        //***********************************
        //This Function will perform the action for Deleteing the Data into Table  ExternalLink for Primary Column ExernalId
        //***********************************
        public int DeleteExternalLink()
        {
            String strDeleteQuery = "Delete From ExternalLink where  ExernalId = @ExernalId ";


            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@ExernalId", ExernalId);

            Dal objDal = new Dal();
            int check = 0;
            check = objDal.ExecuteDataIdentity(strDeleteQuery, param);
            return check;
        }


        public int DeleteExternalLinkByFormIdAndType()
        {
            String strDeleteQuery = "Delete From ExternalLink  where  upper(FormType) = @FormType  and FormId =@FormId ";


            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@FormType", FormType.ToUpper());
            param[1] = new SqlParameter("@FormId", FormId);

            Dal objDal = new Dal();
            int check = 0;
            check = objDal.ExecuteDataIdentity(strDeleteQuery, param);
            return check;
        }



        #endregion

        #region Load functions

        //***********************************
        //This Function will perform the action for Loading the Data from Table  ExternalLink for Primary Column ExernalId
        //***********************************
        public void LoadExternalLink()
        {
            String strLoadQuery = "Select * From ExternalLink where  ExernalId = @ExernalId ";


            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@ExernalId", ExernalId);

            Conversion objCon = new Conversion();
            Dal objDal = new Dal();
            DataTable dtExternalLink = new DataTable();
            dtExternalLink = objDal.ExecuteTable(strLoadQuery, param);
            if (dtExternalLink.Rows.Count > 0)
            {
                ExernalId = objCon.ConToInt64(dtExternalLink.Rows[0]["ExernalId"]);
                FormType = objCon.ConToStr(dtExternalLink.Rows[0]["FormType"]);
                FormId = objCon.ConToInt64(dtExternalLink.Rows[0]["FormId"]);
                LastUpdate = objCon.ConToDT(dtExternalLink.Rows[0]["LastUpdate"]);
                LinkDescription = objCon.ConToStr(dtExternalLink.Rows[0]["LinkDescription"]);
                LinkFile = objCon.ConToStr(dtExternalLink.Rows[0]["LinkFile"]);
                LinkFileType = objCon.ConToStr(dtExternalLink.Rows[0]["LinkFileType"]);
                Path = objCon.ConToStr(dtExternalLink.Rows[0]["Path"]);
                isDrawing = objCon.ConTobool(dtExternalLink.Rows[0]["isDrawing"]);
            }
        }




        #endregion


        #region Load functions

        //***********************************
        //This Function will perform the action for Loading the Data from Table  ExternalLink for Primary Column ExernalId
        //***********************************

        public DataTable LoadExternalLinkByFormIdAndType()
        {
            String strLoadQuery = "Select * From ExternalLink where  upper(FormType) = @FormType  and FormId =@FormId ";


            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@FormType", FormType.ToUpper());
            param[1] = new SqlParameter("@FormId", FormId);

            Conversion objCon = new Conversion();
            Dal objDal = new Dal();
            DataTable dtExternalLink = new DataTable();
            dtExternalLink = objDal.ExecuteTable(strLoadQuery, param);
            return dtExternalLink;
        }


        public DataTable LoadExternalLinkEmpty()
        {
            String strLoadQuery = "Select * From ExternalLink where  1 =0 ";
            Conversion objCon = new Conversion();
            Dal objDal = new Dal();
            DataTable dtExternalLink = new DataTable();
            dtExternalLink = objDal.ExecuteTable(strLoadQuery);
            return dtExternalLink;
        }

        //***********************************
        public Int64 GetMaxId()
        {
            String strLoadQuery = "Select isnull(max(ExernalId),0) as ExernalId from ExternalLink  ";
            Conversion objCon = new Conversion();
            Dal objDal = new Dal();
            DataTable dtExternalLink = new DataTable();
            dtExternalLink = objDal.ExecuteTable(strLoadQuery);

            if (dtExternalLink.Rows.Count > 0)
            {
                ExernalId = objCon.ConToInt64(dtExternalLink.Rows[0]["ExernalId"]);
            }
            return ExernalId;
        }

        public DataTable LoadExternalLinkByFormIdAndType1()
        {
            String strLoadQuery = "Select * From ExternalLink where      FormId =@FormId and isnull(isDrawing,0)=@isDrawing";


            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@FormType", FormType.ToUpper());
            param[1] = new SqlParameter("@FormId", FormId);
            param[2] = new SqlParameter("@isDrawing", isDrawing);

            Conversion objCon = new Conversion();
            Dal objDal = new Dal();
            DataTable dtExternalLink = new DataTable();
            dtExternalLink = objDal.ExecuteTable(strLoadQuery, param);
            return dtExternalLink;
        }
        #endregion
        #endregion
    }
}
