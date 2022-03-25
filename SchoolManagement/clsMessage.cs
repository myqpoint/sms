using System;
using System.Windows.Forms;
using DevExpress.XtraEditors;
namespace Debono
{
    /// <summary>
    /// Summary description for clsDocMessage.
    /// </summary>
    public class DebonoMsg
    {
        private static String strDelete = " Are you sure you want to delete this record?";
        private static String strSaveChanges = " Do you want to save changes you have made?";
        private static String strCustomDelete;
        private static String strCustomInValid;
        private static String strCustomEnter;

        private static String strSaveBeforeAccess = " You need to save operation  first before assigning value to it.";
        private static String strMissingRequiredValue = " Current Record can not saved as you are missing required value. PLease Provide them and save again. ";
        private static String strInformation = "Information";
        private static String strFileNotExist = "PLease check as the file does not exist.";
        private static String strAlreadyUsed = "This workflow has been used at other places in system.";
        private static String strDuplicateInvNo = "Invoice no already exist.Please change invoice no.";
        private static String strCounterValueMgs = "New Counter Value Must Be Greater Than Old Counter Value";
        private static String strMaterialExist = "Material Already Exist";
        private static String strProductExist = "Product Already Exist";
        private static String strMsgFromToDate = "From Date must be Less Than To Date";
        private static String strMsgCantNull = "Name Can't Be Null or 0";
        private static String strMsgFactoraboveZero = "factor value can not be less than 1";
        private static String strMsgUniqueCounterNumber = "Counter no. should be unique";


        private static String strMsgmatCatCantNull = "MaterialCategory cannot be blank!!!";
        private static String strMsgProductCantNull = "Product code cannot be blank!!!";



        public DebonoMsg()
        {
            //
            // TODO: Add constructor logic here
            //
        }


        public static void MsgError(String strMsg)
        {
            XtraMessageBox.Show(strMsg, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        public static void MsgDuplicateInvNo()
        {
            XtraMessageBox.Show(strDuplicateInvNo, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        public static void MsgRequiredField()
        {
            XtraMessageBox.Show(strMissingRequiredValue, strInformation, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public static bool MsgExit()
        {
            if (XtraMessageBox.Show("Are you syour to exit", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public static bool ExportIntoExcelFile()
        {
            if (XtraMessageBox.Show("Are you syour to export this report into excel", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public static void MsgFileNotExist()
        {
            XtraMessageBox.Show(strFileNotExist, strInformation, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public static void MsgInformation(String strMsg)
        {
            XtraMessageBox.Show(strMsg, strInformation, MessageBoxButtons.OK, MessageBoxIcon.Information);

        }

        public static void MsgMaterialExist()
        {
            XtraMessageBox.Show(strMaterialExist, strInformation, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        public static void MsgProductExist()
        {
            XtraMessageBox.Show(strProductExist, strInformation, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public static void MsgInformationFromTo()
        {
            XtraMessageBox.Show(strMsgFromToDate, strInformation, MessageBoxButtons.OK, MessageBoxIcon.Information);

        }

        public static void MsgCantNull()
        {
            XtraMessageBox.Show(strMsgCantNull, strInformation, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public static void ValidFactor()
        {
            XtraMessageBox.Show(strMsgFactoraboveZero, strInformation, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }


        public static void ValidCounterNo()
        {
            XtraMessageBox.Show(strMsgUniqueCounterNumber, strInformation, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public static void MsgCounterValueInfo()
        {
            XtraMessageBox.Show(strCounterValueMgs, strInformation, MessageBoxButtons.OK, MessageBoxIcon.Information);

        }

        public static void MsgSaveBeforeCreate()
        {
            XtraMessageBox.Show(strSaveBeforeAccess, strInformation, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public static DialogResult MsgConfirmation(String strMsg)
        {
            return XtraMessageBox.Show(strMsg, "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
        }


        public static int MsgCloseSaveConfirmation()
        {
            DialogResult dRes = XtraMessageBox.Show(strSaveChanges, "Confirm", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
            if (dRes == DialogResult.Yes)
            {
                return 1;
            }
            else if (dRes == DialogResult.No)
            {
                return 2;
            }
            else
            {
                return 3;
            }
        }




        public static bool MsgDelete()
        {
            if (XtraMessageBox.Show(strDelete, "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        public static bool MsgAlreadyExist()
        {
            if (XtraMessageBox.Show(strAlreadyUsed, "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool MsgDelete(String strMsg)
        {
            strCustomDelete = "";
            strCustomDelete = "Are you sure you want to delete this ";
            strCustomDelete += strMsg + "?";
            if (XtraMessageBox.Show(strCustomDelete, "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
            {
                return true;
            }
            else
            {
                return false;
            }
        }



        public static void ShowException(string strOperation, string strErrorMessage)
        {
            /*	frm_ErrorDlg ErrDlg = new frm_ErrorDlg(strOperation, strErrorMessage);
                ErrDlg.ShowDialog();
                ErrDlg.Dispose(); 
             */
        }



    }
}
