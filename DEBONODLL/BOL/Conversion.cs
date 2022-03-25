using System;
using System.Collections.Generic;
using System.Text;
using DebonoDLL.App_Code;
using DebonoDLL.App_Code.DAL;

namespace DebonoDLL.App_Code.BOL
{
    public class Conversion
    {

        public System.Int32 ConToInt(string strvalue)
        {
            try
            {
                return Convert.ToInt32(strvalue);
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public System.Int32 ConToInt(object strvalue)
        {
            try
            {
                if (strvalue != null & strvalue.ToString().Trim() != "")
                    return Convert.ToInt32(strvalue);
                else
                    return 0;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }


        public System.Int64 ConToInt64(string strvalue)
        {
            try
            {
                return Convert.ToInt64(strvalue.Trim());
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public System.Int64 ConToInt64(object strvalue)
        {
            try
            {
                if (strvalue != null & strvalue.ToString() != "")
                    return Convert.ToInt64(strvalue);
                else
                    return 0;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public System.Int16 ConToInt16(string strvalue)
        {
            try
            {
                return Convert.ToInt16(strvalue.Trim());
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public System.Int16 ConToInt16(object strvalue)
        {
            try
            {
                if (strvalue != null & strvalue.ToString().Trim() != "")
                    return Convert.ToInt16(strvalue);
                else
                    return 0;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public System.Decimal ConToDec(string strvalue)
        {
            try
            {
                System.Decimal decVal = 0;
                decVal = Convert.ToDecimal(strvalue.Trim());
                return Math.Round(decVal, 2);
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public Boolean ChkToDec(string strvalue)
        {
            try
            {
                decimal val = Math.Round(Convert.ToDecimal(strvalue.Trim()), 2);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public decimal ConToDec(object strvalue)
        {
            try
            {
                System.Decimal decVal = 0;
                if (strvalue != null & strvalue.ToString().Trim() != "")
                {
                    decVal = Math.Round(Convert.ToDecimal(strvalue.ToString().Trim()), 4);
                }
                else
                    return decVal = 0;

                return Math.Round(decVal, 4);

            }
            catch (Exception ex)
            {
                return 0;
            }
        }



        public decimal ConToDec(object strvalue, int Len)
        {
            try
            {
                System.Decimal decVal = 0;
                if (strvalue != null & strvalue.ToString().Trim() != "")
                {
                    decVal = Convert.ToDecimal(strvalue.ToString().Trim());
                }
                else
                    return decVal = 0;

                return Math.Round(decVal, Len);
            }
            catch (Exception ex)
            {
                return 0;
            }
        }



        public Boolean ChkToDec(object strvalue)
        {
            try
            {
                if (strvalue != null & strvalue.ToString() != "")
                {
                    decimal VAL = Math.Round(Convert.ToDecimal(strvalue), 2);
                    return true;
                }
                else
                    return false;
            }
            catch (Exception ex)
            {
                return false;
            }
        }


        public double ConTodob(string strvalue)
        {
            try
            {
                double decVal = 0;
                decVal = Convert.ToDouble(strvalue.Trim());
                return Math.Round(decVal, 2);
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public Boolean ChkTodob(string strvalue)
        {
            try
            {
                Double dbVal = Convert.ToDouble(strvalue.Trim());
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }


        public double ConTodob(object strvalue)
        {
            try
            {
                double decVal = 0;
                if (strvalue != null & strvalue.ToString().Trim() != "")
                {
                    decVal = Convert.ToDouble(strvalue.ToString().Trim());
                }
                else
                    return decVal = 0;

                return Math.Round(decVal, 2);
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public double ConTodob(string strvalue, int NDecimal)
        {
            try
            {
                return Math.Round(Convert.ToDouble(strvalue), NDecimal);

            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public Boolean chkTodob(object strvalue)
        {
            try
            {
                double dbval = (System.Double)(strvalue);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }


        public Boolean ConTobool(string strvalue)
        {
            try
            {
                return Convert.ToBoolean(strvalue.Trim());
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public Boolean ConTobool(object strvalue)
        {
            try
            {
                if (strvalue != null & strvalue.ToString().Trim() != "")
                    return Convert.ToBoolean(strvalue.ToString().Trim());
                else
                    return false;
            }
            catch (Exception ex)
            {
                return false;
            }
        }


        public DateTime ConToDT(string strvalue)
        {
            try
            {
                return Convert.ToDateTime(strvalue.Trim());
            }
            catch (Exception ex)
            {
                return DateTime.Today.Date;
            }
        }

        public DateTime ConToDT(object strvalue)
        {
            try
            {
                if (strvalue == null || strvalue.ToString() == "")
                    return DateTime.Today;
                else
                    return Convert.ToDateTime(strvalue.ToString().Trim());
            }
            catch (Exception ex)
            {
                return DateTime.Today.Date;
            }
        }

        public string ConToStr(object strvalue)
        {
            try
            {
                if (strvalue != null)
                    return strvalue.ToString();
                else
                    return string.Empty;
            }
            catch (Exception ex)
            {
                return string.Empty;
            }
        }



        public Boolean ChkForDulicate(string strTable, String strNo, String strValue)
        {
            Boolean Valid = false;
            if (strTable != "" & strNo != "")
            {
                String StrQuery = "Select * From " + strTable + " where " + strNo + "= '" + strValue + "'";
                Dal objDal = new Dal();
                System.Data.DataTable dtChk = objDal.ExecuteTable(StrQuery);
                if (dtChk != null)
                {
                    if (dtChk.Rows.Count < 1)
                        Valid = true;
                }
                else
                    Valid = true;
            }


            return Valid;
        }
    }
}
