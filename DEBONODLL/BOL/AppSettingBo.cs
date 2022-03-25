/*===========================================================================\
|| ######################################################################### ||
|| # Genrated By BO Generator						  					   # ||
|| # --------------------------------------------------------------------- # ||
|| # Copyright ©2009–2010 Prasad Solutions Pvt. Ltd. All Rights Reserved.  # ||
|| # This file may not be redistributed in whole or significant part. 	   # ||
|| # This application is only for internal use of the orgnasitation. 	   # ||
|| # ----------------BO Generator IS NOT FREE SOFTWARE ------------------  # ||
|| # --------------------------------------------------------------------  # ||
|| # Created On :PRASAD-10-PC         ;Created By :Prasad-10               # ||
|| # Date:Friday, March 28, 2014                                           # ||
|| # Time:8:25:02 PM	                                                   # ||
|| # --------------------------------------------------------------------- # ||
|| # http://www.prasad-solutions.com | 	| info@prasad-solutions.com        # ||
|| # --------------------------------------------------------------------- # ||
|| ######################################################################### ||
\===========================================================================*/

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


public class AppSettingBo
{
    #region Field Properties

    ///<summary>
    ///Id
    ///<summary>
    ///<remarks>
    ///Property variable for Id
    ///<remarks>
    private Int64 Id;
    public Int64 _Id
    {
        get
        {
            return Id;
        }
        set
        {
            Id = value;
        }
    }

    ///<summary>
    ///Value
    ///<summary>
    ///<remarks>
    ///Property variable for Value
    ///<remarks>
    private String Value;
    public String _Value
    {
        get
        {
            return Value;
        }
        set
        {
            Value = value;
        }
    }

    private String ServerPath;
    public String _ServerPath
    {
        get
        {
            return ServerPath;
        }
        set
        {
            ServerPath = value;
        }
    }
    private String ServerUserId;
    public String _ServerUserId
    {
        get
        {
            return ServerUserId;
        }
        set
        {
            ServerUserId = value;
        }
    }
    private String ServerPwd;
    public String _ServerPwd
    {
        get
        {
            return ServerPwd;
        }
        set
        {
            ServerPwd = value;
        }
    }



    ///<summary>
    ///Type
    ///<summary>
    ///<remarks>
    ///Property variable for Type
    ///<remarks>
    private String Type;
    public String _Type
    {
        get
        {
            return Type;
        }
        set
        {
            Type = value;
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

        Id = objCon.ConToInt64(drMainData["Id"]);
        Value = objCon.ConToStr(drMainData["Value"]);
        Type = objCon.ConToStr(drMainData["Type"]);

    }
    #endregion
    #region Save  functions

    //***********************************
    //This Function will perform the action for Saving the Data into Table  RawMaterial
    //***********************************
    public int SaveRawMaterial()
    {
        String strInsertQuery = "insert into AppSetting( Value , Type ) values( @Value , @Type ) ";


        SqlParameter[] param = new SqlParameter[2];
        param[0] = new SqlParameter("@Value", Value);
        param[1] = new SqlParameter("@Type", Type);
        Dal objDal = new Dal();
        int check = 0;
        check = objDal.ExecuteDataIdentity(strInsertQuery, param);
        if (check == 1)
        {
            Id = objDal.ExecuteForID("select Max(Id) from AppSetting");
        }
        return check;
    }
    #endregion
    #region Update functions

    //***********************************
    //This Function will perform the action for Updating the Data into Table  OrderDtl for Primary Column Id
    //***********************************
    public int UpdateAppSetting()
    {
        String strUpdateQuery = "changeappsettingorderdetail";


        SqlParameter[] param = new SqlParameter[3];
        param[0] = new SqlParameter("@Id", Id);
        param[1] = new SqlParameter("@Value", Value);
        param[2] = new SqlParameter("@Type", Type);

        Dal objDal = new Dal();
        int check = 0;
        check = objDal.ExecuteSPDataIdentity(strUpdateQuery, param);
        return check;
    }


    
    #endregion

    





    #region Delete functions

    //***********************************
    //This Function will perform the action for Deleteing the Data into Table  RawMaterial for Primary Column Type
    //***********************************
    public int DeleteRawMaterial()
    {
        String strDeleteQuery = "Delete From AppSetting where  Id = @Id ";


        SqlParameter[] param = new SqlParameter[1];
        param[0] = new SqlParameter("@Id", Id);

        Dal objDal = new Dal();
        int check = 0;
        check = objDal.ExecuteDataIdentity(strDeleteQuery, param);
        return check;
    }


    public int DeleteRawMaterialByidAndType()
    {
        String strDeleteQuery = "Delete From AppSetting where  Id = @Id and Upper(TYPE)='OPERATION'";


        SqlParameter[] param = new SqlParameter[1];
        param[0] = new SqlParameter("@Id", Id);

        Dal objDal = new Dal();
        int check = 0;
        check = objDal.ExecuteDataIdentity(strDeleteQuery, param);
        return check;
    }

    public int DeleteRawMaterialByType()
    {
        String strDeleteQuery = "Delete From AppSetting where  Type = @Type ";


        SqlParameter[] param = new SqlParameter[1];
        param[0] = new SqlParameter("@Type", Type);

        Dal objDal = new Dal();
        int check = 0;
        check = objDal.ExecuteDataIdentity(strDeleteQuery, param);
        return check;
    }
    #endregion
    #region Load functions

    //***********************************
    //This Function will perform the action for Loading the Data from Table  RawMaterial for Primary Column Id
    //***********************************
    public void LoadRawMaterial()
    {
        String strLoadQuery = "Select * From AppSetting where  Type = @Type ";
        SqlParameter[] param = new SqlParameter[1];
        param[0] = new SqlParameter("@Type", Type);

        Conversion objCon = new Conversion();
        Dal objDal = new Dal();
        DataTable dtAppSetting = new DataTable();
        dtAppSetting = objDal.ExecuteTable(strLoadQuery, param);
        if (dtAppSetting.Rows.Count > 0)
        {
            Id = objCon.ConToInt64(dtAppSetting.Rows[0]["Id"]);
            Value = objCon.ConToStr(dtAppSetting.Rows[0]["Value"]);
            Type = objCon.ConToStr(dtAppSetting.Rows[0]["Type"]);
        }
    }

    public void LoadValue()
    {
        String strLoadQuery = "Select * From AppSetting where  Id = @Id ";
        SqlParameter[] param = new SqlParameter[1];
        param[0] = new SqlParameter("@Id", Id);

        Conversion objCon = new Conversion();
        Dal objDal = new Dal();
        DataTable dtAppSetting = new DataTable();
        dtAppSetting = objDal.ExecuteTable(strLoadQuery, param);
        if (dtAppSetting.Rows.Count > 0)
        {
            Id = objCon.ConToInt64(dtAppSetting.Rows[0]["Id"]);
            Value = objCon.ConToStr(dtAppSetting.Rows[0]["Value"]);
            Type = objCon.ConToStr(dtAppSetting.Rows[0]["Type"]);
        }
    }



    public DataTable LoadRawMaterialByType()
    {
        String strLoadQuery = "Select * From AppSetting where  UPPER( Type ) = @Type order by Value ";
        SqlParameter[] param = new SqlParameter[1];
        param[0] = new SqlParameter("@Type", Type.ToString().ToUpper().Trim());

        Conversion objCon = new Conversion();
        Dal objDal = new Dal();
        DataTable dtAppSetting = new DataTable();
        dtAppSetting = objDal.ExecuteTable(strLoadQuery, param);
        return dtAppSetting;
    }



    public DataTable LoadRawMaterialByType(string id)
    {
        String strLoadQuery = "Select * From AppSetting where  UPPER( Type ) = 'OPERATION' and Id in ( " + id + " ) ";
        SqlParameter[] param = new SqlParameter[1];
        param[0] = new SqlParameter("@Type", Type.ToString().ToUpper().Trim());

        Conversion objCon = new Conversion();
        Dal objDal = new Dal();
        DataTable dtAppSetting = new DataTable();
        dtAppSetting = objDal.ExecuteTable(strLoadQuery, param);
        return dtAppSetting;
    }

    public DataTable LoadRawMaterialByTypeForgrd()
    {
        String strLoadQuery = "Select distinct(Value) From AppSetting where  UPPER( Type ) = @Type ";
        SqlParameter[] param = new SqlParameter[1];
        param[0] = new SqlParameter("@Type", Type.ToString().ToUpper().Trim());

        Conversion objCon = new Conversion();
        Dal objDal = new Dal();
        DataTable dtAppSetting = new DataTable();
        dtAppSetting = objDal.ExecuteTable(strLoadQuery, param);
        return dtAppSetting;
    }


    public DataTable LoadRawMaterialByTypeForgrdWithId()
    {
        String strLoadQuery = "Select distinct(Value) ,Id From AppSetting where  UPPER( Type ) = @Type Order by Value ";
        SqlParameter[] param = new SqlParameter[1];
        param[0] = new SqlParameter("@Type", Type.ToString().ToUpper().Trim());

        Conversion objCon = new Conversion();
        Dal objDal = new Dal();
        DataTable dtAppSetting = new DataTable();
        dtAppSetting = objDal.ExecuteTable(strLoadQuery, param);
        return dtAppSetting;
    }



    public DataTable LoadColor()
    {
        String strLoadQuery = "Select distinct Value,Id From AppSetting where  UPPER( Type ) = @Type ";
        SqlParameter[] param = new SqlParameter[1];
        param[0] = new SqlParameter("@Type", Type.ToString().ToUpper().Trim());

        Conversion objCon = new Conversion();
        Dal objDal = new Dal();
        DataTable dtAppSetting = new DataTable();
        dtAppSetting = objDal.ExecuteTable(strLoadQuery, param);
        return dtAppSetting;
    }


    public DataTable LoadProductCategory()
    {
        String strLoadQuery = "Select distinct Value as Category,Id From AppSetting where  UPPER( Type ) = 'CATEGORY' ";
        Conversion objCon = new Conversion();
        Dal objDal = new Dal();
        DataTable dtAppSetting = new DataTable();
        dtAppSetting = objDal.ExecuteTable(strLoadQuery);
        return dtAppSetting;
    }



    public DataTable ShowValueByType()
    {
        String strLoadQuery = "Select Value,Value as Id From AppSetting where  UPPER( Type )= @Type   group by value";
        SqlParameter[] param = new SqlParameter[1];
        param[0] = new SqlParameter("@Type", Type.ToString().ToUpper().Trim());

        Conversion objCon = new Conversion();
        Dal objDal = new Dal();
        DataTable dtAppSetting = new DataTable();
        dtAppSetting = objDal.ExecuteTable(strLoadQuery, param);
        return dtAppSetting;
    }

    public DataTable ShowConfig()
    {
        String strLoadQuery = "Select Value as Config,Value as Config From AppSetting where  UPPER( Type )= @Type ";
        SqlParameter[] param = new SqlParameter[1];
        param[0] = new SqlParameter("@Type", Type.ToString().ToUpper().Trim());

        Conversion objCon = new Conversion();
        Dal objDal = new Dal();
        DataTable dtAppSetting = new DataTable();
        dtAppSetting = objDal.ExecuteTable(strLoadQuery, param);
        return dtAppSetting;
    }

    public bool CheckServerPathExists(string type)
    {
        String strLoadQuery = "Select id,value From AppSetting where  UPPER( Type) like '"+type+"%' ";       
        Conversion objCon = new Conversion();
        Dal objDal = new Dal();
        DataTable dtAppSetting = new DataTable();
        dtAppSetting = objDal.ExecuteTable(strLoadQuery);
        if (dtAppSetting.Rows.Count > 0)
        {
            Id = objCon.ConToInt64(dtAppSetting.Rows[0]["id"]);
            return true; 
        }
        else
        { return false; }
    }

    public string getServerData(string type)
    {
        string val="";
        String strLoadQuery = "Select value From AppSetting where  UPPER( Type) like '" + type + "%' ";
        Conversion objCon = new Conversion();
        Dal objDal = new Dal();
        DataTable dtAppSetting = new DataTable();
        dtAppSetting = objDal.ExecuteTable(strLoadQuery);
        if (dtAppSetting.Rows.Count > 0)
        {
            val = objCon.ConToStr(dtAppSetting.Rows[0]["value"]);
            return val;  
        }
        else
        { return val; }
    }
   


    public DataTable LoadDistinctType()
    {
        String strLoadQuery = "Select Type From AppMain ";

        Conversion objCon = new Conversion();
        Dal objDal = new Dal();
        DataTable dtAppSetting = new DataTable();
        dtAppSetting = objDal.ExecuteTable(strLoadQuery);
        return dtAppSetting;
    }



    #endregion
    #endregion


    public DataTable CheckExists()
    {
        String strLoadQuery = "select * from AppSetting where Id= @Id and upper(Type) ='OPERATION'";
        SqlParameter[] param = new SqlParameter[1];
        param[0] = new SqlParameter("@Id", Id);
        Dal objDal = new Dal();
        DataTable dtAppSetting = new DataTable();
        
        dtAppSetting = objDal.ExecuteTable(strLoadQuery, param);
        return dtAppSetting;
    }
}
