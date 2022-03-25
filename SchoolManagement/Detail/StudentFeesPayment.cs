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
    public partial class StudentFeesPayment : XtraForm
    {
        private string _UserName;
        public string UserName
        {
            get { return _UserName; }
            set { _UserName = value; }
        }

        private Int64 _PId;
        public Int64 PId
        {
            get { return _PId; }
            set { _PId = value; }
        }
        private Int64 _CId;
        public Int64 CId
        {
            get { return _CId; }
            set { _CId = value; }
        }
        private Int64 _SId;
        private bool _isEditNew = true;
        public Int64 SId
        {
            get { return _SId; }
            set { _SId = value; }
        }
        private string _Session;
        public string Session
        {
            get { return _Session; }
            set { _Session = value; }
        }
        public StudentFeesPayment()
        {

            InitializeComponent();
            this._UserName = UserName;
            this._SId = 0;
            _isEditNew = true;
        }

        public StudentFeesPayment(long SId,string session)
        {

            InitializeComponent();
            this._UserName = UserName;
            this._SId = SId;
            this._Session = session;
            _isEditNew = true;
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
                if (this._SId > 0)
                {
                    Conversion objcon = new Conversion();
                    LoadStudentDetails();
                    LoadPaymentHistory();
                    //Permission
                    if (!chekEditPermission() == true)
                        btnsave.Enabled = false;
                    else
                        btnsave.Enabled = true;
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
                if (this._SId > 0)
                {
                    Conversion objCon = new Conversion();
                    StudentInfoBo objorder = new StudentInfoBo();
                    objorder._SId = this._SId;
                    objorder._Session = this._Session;
                    DataTable dtstudent = objorder.ShowAllStudentFeesInfobystudent();
                    lblstudentname.Text = objCon.ConToStr(dtstudent.Rows[0]["Name"]);
                    lblfathername.Text = objCon.ConToStr(dtstudent.Rows[0]["FatherName"]);
                    lblmobilenumber.Text = objCon.ConToStr(dtstudent.Rows[0]["MobileNumber"]);
                    lblclass.Text = objCon.ConToStr(dtstudent.Rows[0]["ClassName"]);
                    lblpaymenttype.Text = objCon.ConToStr(dtstudent.Rows[0]["PaymentType"]);
                    lblpaidfees.Text = objCon.ConToStr(dtstudent.Rows[0]["PaidFees"]);
                    lblaadharno.Text = objCon.ConToStr(dtstudent.Rows[0]["AadharNumber"]);
                    CId = objCon.ConToInt(dtstudent.Rows[0]["CId"]);
                    DataTable dtImage = objorder.LoadStudentInfoImage();
                    if (dtImage != null && dtImage.Rows.Count > 0)
                    {
                        byte[] img = (byte[])(dtImage.Rows[0]["StudentImage"]);
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
                PaymentHistoryBo objOrderDetail = new PaymentHistoryBo();
                objOrderDetail._CId = this._CId;
                dtOrderDtl = objOrderDetail.ShowStudentPaymentHistory();
                Gc_orderdtl.DataSource = dtOrderDtl;

            }
            catch (Exception ex)
            {
                ExceptionManager.LogException(ex);
            }
        }

        #endregion

        private void ClearValue()
        {
            txtpaidamount.Text = string.Empty;
            txtpaymentdate.Text = string.Empty;
        }

        #region Save
        private void btnsave_Click(object sender, EventArgs e)
        {
            try
            {
                if(string.IsNullOrEmpty(txtpaymentdate.Text))
                { DebonoMsg.MsgInformation("Please fill the Payment Date"); return; }
                if (string.IsNullOrEmpty(txtpaidamount.Text))
                { DebonoMsg.MsgInformation("Please fill the Amount"); return; }
                if (string.IsNullOrEmpty(ddlpaymenttype.Text) || ddlpaymenttype.Text=="Select")
                { DebonoMsg.MsgInformation("Please select payment type"); return; }
                Conversion objCon = new Conversion();
                int nCheck = 0;
                FormHelper.ShowWaitDialog();
                nCheck = SaveStudentPayment();
                if (nCheck > 0)
                {
                    txtpaymentdate.Text = string.Empty;
                    ddlpaymenttype.Text = string.Empty;
                    txtpaidamount.Text = string.Empty;
                    txtgeneratorfee.Text = string.Empty;
                    txtptafee.Text = string.Empty;
                }
              FormHelper.CloseWaitDialog();
                if(nCheck>0)
              DebonoMsg.MsgInformation("Data Saved Successfully");
                LoadStudentDetails();
                LoadPaymentHistory();
                
            }
            catch (Exception ex)
            {

                ExceptionManager.LogException(ex);
                FormHelper.CloseWaitDialog();
            }
        }

        private int SaveStudentPayment()
        {
            Conversion objCon = new Conversion();
            try
            {
                PaymentHistoryBo objOrder = new PaymentHistoryBo();
                objOrder._SId = objCon.ConToInt64(_SId);
                objOrder._CId = objCon.ConToInt64(_CId);
                objOrder._Amount = objCon.ConToDec(txtpaidamount.EditValue) + objCon.ConToDec(txtgeneratorfee.EditValue) + objCon.ConToDec(txtptafee.EditValue);
                objOrder._ComputerFee = objCon.ConToDec(txtpaidamount.EditValue);
                objOrder._GeneratorFee = objCon.ConToDec(txtgeneratorfee.EditValue);
                objOrder._PTAFee = objCon.ConToDec(txtptafee.EditValue);
                objOrder._PaymentType = objCon.ConToStr(ddlpaymenttype.Text);
                objOrder._PaymentDate = objCon.ConToDT(txtpaymentdate.EditValue);
                objOrder._CreatedBy = this._UserName;
                    int nCheck = 0;
                    if (objOrder._PId > 0)
                    {
                        nCheck = objOrder.UpdatePaymentHistory();
                    }
                    else
                    {
                        nCheck = objOrder.SavePaymentHistory();
                        this._PId = objOrder._PId;
                        FormHelper.SetScreenNumbering("PId");
                    }
                    return nCheck;
            }
            catch (SqlException ex)
            {
                return 0;
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
                    
                    int PId = objCon.ConToInt(gvOrderDtl.GetFocusedRowCellValue("PId"));
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

        private void ddlpaymenttype_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlpaymenttype.Text == "Monthly Fee")
            {
                labelControl8.Text = "Computer Fees : "; labelControl9.Text = "Generator Fees : "; labelControl10.Text = "PTA Fees : ";
                txtpaidamount.Text = ""; txtgeneratorfee.Text = ""; txtptafee.Text = "";
                labelControl8.Visible = true; labelControl9.Visible = true; labelControl10.Visible = true; txtpaidamount.Visible = true; txtgeneratorfee.Visible = true; txtptafee.Visible = true;
            }
            if (ddlpaymenttype.Text == "Admission Fee")
            {
                labelControl8.Text = "Admission Fees : ";
                labelControl8.Visible = true; txtpaidamount.Visible = true; txtpaidamount.Text = ""; txtgeneratorfee.Text = ""; txtptafee.Text = "";
                labelControl9.Visible = false; labelControl10.Visible = false; txtgeneratorfee.Visible = false; txtptafee.Visible = false;
            }
            if (ddlpaymenttype.Text == "Half Yearly Exam Fee" || ddlpaymenttype.Text == "Annual Exam Fee")
            {
                labelControl8.Text = "Exam Fees : "; labelControl8.Visible = true; txtpaidamount.Visible = true; txtpaidamount.Text = ""; txtgeneratorfee.Text = ""; txtptafee.Text = "";
                labelControl9.Visible = false; labelControl10.Visible = false; txtgeneratorfee.Visible = false; txtptafee.Visible = false;
            }
            if (ddlpaymenttype.Text == "Maintenance Fee")
            {
                labelControl8.Text = "Maintenance Fees : "; labelControl8.Visible = true; txtpaidamount.Visible = true; txtpaidamount.Text = ""; txtgeneratorfee.Text = ""; txtptafee.Text = "";
                labelControl9.Visible = false; labelControl10.Visible = false; txtgeneratorfee.Visible = false; txtptafee.Visible = false;
            }
            if (ddlpaymenttype.Text == "F.D.B.R.I. Fee")
            {
                labelControl8.Text = "F.D.B.R.I. Fees : "; labelControl8.Visible = true; txtpaidamount.Visible = true; txtpaidamount.Text = ""; txtgeneratorfee.Text = ""; txtptafee.Text = "";
                labelControl9.Visible = false; labelControl10.Visible = false; txtgeneratorfee.Visible = false; txtptafee.Visible = false;
            }
            if (ddlpaymenttype.Text == "Library Fee")
            {
                labelControl8.Text = "Library Fees : "; labelControl8.Visible = true; txtpaidamount.Visible = true; txtpaidamount.Text = ""; txtgeneratorfee.Text = ""; txtptafee.Text = "";
                labelControl9.Visible = false; labelControl10.Visible = false; txtgeneratorfee.Visible = false; txtptafee.Visible = false;
            }

        }

        private void groupControl1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void txtptafee_Properties_KeyUp(object sender, KeyEventArgs e)
        {

        }

        private void txtptafee_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void txtgeneratorfee_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
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
