using DebonoDLL.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DEBONO.Helper;
using DebonoDLL;
using DebonoDLL.App_Code.BOL;
using System.Data.SqlClient;
using DevExpress.XtraGrid.Views.Base;
using DebonoDLL.BOL;
using System.Xml;
using System.IO;
using System.Xml.Xsl;
using DEBONO;
using DevExpress.XtraGrid.Views.Grid;

namespace Debono.Detail
{
    public partial class StaffSalaryPayment : XtraForm
    {
        private string _UserName;
        public string UserName
        {
            get { return _UserName; }
            set { _UserName = value; }
        }

        private Int64 _StaffId;
        public Int64 StaffId
        {
            get { return _StaffId; }
            set { _StaffId = value; }
        }
        
        public StaffSalaryPayment()
        {

            InitializeComponent();
            this._UserName = UserName;
        }

        public StaffSalaryPayment(long StaffId)
        {

            InitializeComponent();
            this._UserName = UserName;
            this._StaffId = StaffId;
        }
        bool chekEditPermission()
        {
            if (GlobleData.ManageRoleAccessEditDetail(this.Name))
                return true;
            else
                return false;
        }
        bool chekDeletePermission()
        {
            if (GlobleData.ManageRoleAccessDeleteDetail(this.Name))
                return true;
            else
                return false;
        }
        private void OrdrDetail_Load(object sender, EventArgs e)
        {
            this._UserName = UserName;
            GetOrderDetail();
        }

        private void OrdrDetail_FormClosing(object sender, FormClosingEventArgs e)
        {
        }

        private void OrdrDetail_Activated(object sender, EventArgs e)
        {
            try
            {
                LoadOrSaveLayout(CommonFunction.custLayoutOptions.Load);
            }
            catch (Exception ex)
            {
                ExceptionManager.LogException(ex);
            }
        }

        #region Load

        private void GetOrderDetail()
        {
            try
            {
                FormHelper.ShowWaitDialog();
                if (this._StaffId > 0)
                {
                    Conversion objcon = new Conversion();
                    LoadStudentDetails();
                    LoadPaymentHistory();
                    //Permission
                    if (!chekEditPermission() == true)
                        btnpaysalary.Enabled = false;
                    else
                        btnpaysalary.Enabled = true;
                    if (!chekDeletePermission() == true)
                        CM_Delete.Enabled = false;
                    else
                        CM_Delete.Enabled = true;
                    gvOrderDtl.OptionsBehavior.Editable = false;
                }
                FormHelper.CloseWaitDialog();

            }
            catch (Exception ex)
            {
                ExceptionManager.LogException(ex);
                FormHelper.CloseWaitDialog();
            }
        }

        private void LoadStudentDetails()
        {
            try
            {
                if (this._StaffId > 0)
                {
                    Conversion objCon = new Conversion();
                    StaffBo objorder = new StaffBo();
                    objorder._StaffId = this._StaffId;
                    objorder.LoadStaff();
                    lblstudentname.Text = objorder._Name;
                    lblgender.Text = objorder._Gender;
                    lblmobilenumber.Text = objorder._MobileNumber;
                    lblaadharno.Text = objorder._AadharNumber;
                    lblemailid.Text = objorder._EmailId;
                    lblfathername.Text = objorder._FatherName;
                    lbladdress.Text = objorder._Address;
                    lbljoinningdate.Text = objorder._JoinningDate.ToString();
                    lblqualification.Text = objorder._Qualification;
                    lblsalaryamount.Text = objorder._SalaryAmount.ToString();
                    DataTable dtImage = objorder.LoadStaffImage();
                    if (dtImage != null && dtImage.Rows.Count > 0)
                    {
                        byte[] img = (byte[])(dtImage.Rows[0]["StaffImage"]);
                        MemoryStream mstream = new MemoryStream(img);
                        pictureEdit1.Image = Image.FromStream(mstream);
                    }
                    else
                        pictureEdit1.Image = null;
                }

            }
            catch (Exception ex)
            {
                ExceptionManager.LogException(ex);
            }
        }

        private void LoadPaymentHistory()
        {
            try
            {
                StaffPaymentHistoryBo objOrderDetail = new StaffPaymentHistoryBo();
                objOrderDetail._StaffId = this._StaffId;
                dtOrderDtl = objOrderDetail.ShowStaffPaymentHistory();
                Gc_orderdtl.DataSource = dtOrderDtl;

            }
            catch (Exception ex)
            {
                ExceptionManager.LogException(ex);
            }
        }

        #endregion

        #region Save
        private void btnpaysalary_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtpaymentdate.Text))
                { DebonoMsg.MsgInformation("Please fill the Payment Date"); return; }
                if (string.IsNullOrEmpty(txtpaidamount.Text))
                { DebonoMsg.MsgInformation("Please fill the Amount"); return; }

                Conversion objCon = new Conversion();
                int nCheck = 0;
                FormHelper.ShowWaitDialog();
                nCheck = SaveStaffPayment();
                if (nCheck > 0)
                {
                    txtpaymentdate.Text = string.Empty;
                    txtpaidamount.Text = string.Empty;
                }
                FormHelper.CloseWaitDialog();
                if (nCheck > 0)
                    DebonoMsg.MsgInformation("Data Saved Successfully");
                LoadPaymentHistory();

            }
            catch (Exception ex)
            {

                ExceptionManager.LogException(ex);
                FormHelper.CloseWaitDialog();
            }
        }
        #endregion

        #region Delete
        String AutoDeleteMessage = "are you sure to delete";
        String AutoDeleteTitle = "Confirm";
        DataTable dtOrderDtl = new DataTable();
        private void CM_Delete_Click(object sender, EventArgs e)
        {
            Conversion objCon=new Conversion();
            try
            {
                if (Debono.DebonoMsg.MsgDelete())
                {
                    
                    int PId = objCon.ConToInt(gvOrderDtl.GetFocusedRowCellValue("SPId"));
                    if (PId > 0)
                    {
                        PaymentHistoryBo objOrder = new PaymentHistoryBo();
                        objOrder._PId = PId;
                       int chk= objOrder.DeletePaymentHistory();
                       if (chk > 0)
                       {
                           DebonoMsg.MsgInformation("Selected Row Deleted Successfully");
                       }
                    }
                    gvOrderDtl.DeleteSelectedRows();
                    gvOrderDtl.RefreshData();
                    dtOrderDtl.AcceptChanges();
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.LogException(ex);
            }

        }

        #endregion


        private void LoadOrSaveLayout(CommonFunction.custLayoutOptions enLayoutOptions)
        {
            CommonFunction objcmnFun = new CommonFunction();
            objcmnFun.LoadORSaveLayout(this.Name + "&", ref gvOrderDtl, enLayoutOptions);
        }

        private void groupControl1_Paint(object sender, PaintEventArgs e)
        {

        }

       

        private int SaveStaffPayment()
        {
            Conversion objCon = new Conversion();
            try
            {
                StaffPaymentHistoryBo objOrder = new StaffPaymentHistoryBo();
                objOrder._StaffId = objCon.ConToInt64(_StaffId);
                objOrder._PaidAmount = objCon.ConToDec(txtpaidamount.EditValue);
                objOrder._PaymentDate = objCon.ConToDT(txtpaymentdate.EditValue);
                objOrder._CreatedBy = this._UserName;
                int nCheck = 0;
                if (objOrder._SPId > 0)
                {
                    nCheck = objOrder.UpdateStaffPaymentHistory();
                }
                else
                {
                    nCheck = objOrder.SaveStaffPaymentHistory();
                    FormHelper.SetScreenNumbering("SPId");
                }
                return nCheck;
            }
            catch (SqlException ex)
            {
                return 0;
            }


        }

        private void txtpaidamount_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        
    }
}
