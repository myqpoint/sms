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
using System.Configuration;

namespace Debono.Detail
{
    public partial class BookReturnForm : XtraForm
    {
        private string _UserName;
        public string UserName
        {
            get { return _UserName; }
            set { _UserName = value; }
        }
        private Int64 _RId;
        public Int64 RId
        {
            get { return _RId; }
            set { _RId = value; }
        }
        private bool _isEditNew = true;
        public BookReturnForm()
        {

            InitializeComponent();
            this._UserName = UserName;
            _isEditNew = true;
        }

        public BookReturnForm(Int64 rid)
        {
            this._RId = rid;
            InitializeComponent();
            this._UserName = UserName;
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
            groupControl3.Visible = false;
            this._UserName = UserName;
            StudentInfoBo objpub = new StudentInfoBo();
            DataTable dtpub = objpub.ShowAllIssueBookstudent();
            DataRow dr = dtpub.NewRow();
            dr[0] = "0";
            dr[1] = "Select";
            dtpub.Rows.InsertAt(dr, 0);
            ddlstudent.DataSource = dtpub;
            if (this.RId > 0)
            {
                RentMstBo objrent = new RentMstBo();
                objrent._RId = this.RId;
                objrent.LoadRentMst();
                ddlstudent.SelectedValue = objrent._SId;
                ddlbook.SelectedValue = objrent._BookId;
                btnsearch_Click(null,null);
            }
        }

        private void ClearValue()
        {
            StaffBo objOrder = new StaffBo();
            objOrder._StaffId = 0;
            txtpanaltyamount.Text = string.Empty;
            pictureEdit1.Image = null;
        }

        private void btnsearch_Click(object sender, EventArgs e)
        {
            try
            {
                if (ddlbook.SelectedValue.ToString()=="0")
                { DebonoMsg.MsgInformation("Please fill the Book Name"); return; }
                Conversion objCon = new Conversion();
                FormHelper.ShowWaitDialog();

                BookBo objbook = new BookBo();
                objbook._BookID = objCon.ConToInt16(ddlbook.SelectedValue);
                objbook.LoadBookMst();
                lblauthor.Text = objbook._Author.ToString();
                lblbookname.Text = objbook._BookName;
                lblprice.Text = objbook._Price.ToString();
                FeesTypeBo objclass = new FeesTypeBo();
                objclass._FId = objbook._ClassId;
                objclass.LoadFeesType();
                lblclass.Text = objclass._ClassName;
                PublicationBo obpub = new PublicationBo();
                obpub._PID = objbook._PublicationId;
                obpub.LoadPublicationMst();
                lblpublication.Text = obpub._Publication;

                RentMstBo objrent = new RentMstBo();
                objrent._BookId = objCon.ConToInt16(ddlbook.SelectedValue);
                objrent._SId = objCon.ConToInt16(ddlstudent.SelectedValue);
                DataTable dtrent = objrent.ShowRentMstByStudentandbook();
                if (dtrent.Rows.Count > 0)
                {
                    lbldays.Text = dtrent.Rows[0]["Days"].ToString();
                    lblissuedate.Text = dtrent.Rows[0]["IssueDate"].ToString();
                    lblpaneltystatus.Text = dtrent.Rows[0]["PanaltyStatus"].ToString();
                    if (lblpaneltystatus.Text == "No")
                    {
                        txtmessage.Visible = false;
                        txtpanaltyamount.Visible = false;
                        labelControl11.Visible = false;
                        labelControl6.Visible = false;
                    }
                    else
                    {
                        txtmessage.Visible = true;
                        txtpanaltyamount.Visible = true;
                        labelControl11.Visible = true;
                        labelControl6.Visible = true;
                    }
                }
                DataTable dtImage = objbook.LoadBookImage();
                if (dtImage != null && dtImage.Rows.Count > 0)
                {
                    byte[] img = (byte[])(dtImage.Rows[0]["BookImage"]);
                    MemoryStream mstream = new MemoryStream(img);
                    pictureEdit1.Image = Image.FromStream(mstream);
                }
                else
                    pictureEdit1.Image = null;
                groupControl3.Visible = true;
                FormHelper.CloseWaitDialog();
               

            }
            catch (Exception ex)
            {

                ExceptionManager.LogException(ex);
                FormHelper.CloseWaitDialog();
            }
        }

        private void btnbookissue_Click(object sender, EventArgs e)
        {
             Conversion objcon = new Conversion();
            if (ddlstudent.SelectedValue.ToString() == "0")
            { DebonoMsg.MsgInformation("Please fill the Student Name"); return; }
            if (ddlbook.SelectedValue.ToString() == "0")
            { DebonoMsg.MsgInformation("Please fill the Book Name"); return; }
            if (lblpaneltystatus.Text == "Yes")
            {
                if (txtpanaltyamount.Text == "0" || txtpanaltyamount.Text == "")
                { DebonoMsg.MsgInformation("Please fill the panalty amount"); return; }
            }
            PanaltyMstBo objpanelty = new PanaltyMstBo();
            objpanelty._BookId = objcon.ConToInt64(ddlbook.SelectedValue);
            objpanelty._SId = objcon.ConToInt64(ddlstudent.SelectedValue);
            objpanelty._PanaltyAmount = objcon.ConToDec(txtpanaltyamount.Text);
            objpanelty._Message = objcon.ConToStr(txtmessage.Text);
            int chk = objpanelty.SavePanaltyMst();
            if (chk > 0)
            {
                chk=objpanelty.UpdateBookRentQty();
                RentMstBo objrent = new RentMstBo();
                objrent._BookId = objcon.ConToInt64(ddlbook.SelectedValue);
                objrent._SId = objcon.ConToInt64(ddlstudent.SelectedValue);
                objrent._Days = objcon.ConToInt64(lbldays.Text);
                objrent._ReturnBy = this._UserName;
                chk = objrent.UpdateRentMst();
                if (chk > 0)
                {
                    DebonoMsg.MsgInformation("Data Saved Successfully");
                    ddlstudent.SelectedValue = "0";
                    ddlbook.SelectedValue = "0";
                    txtpanaltyamount.Text = "";
                    txtmessage.Text="";
                }
            }
        }

        private void txtdays_KeyPress(object sender, KeyPressEventArgs e)
        {
           
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
        }

        private void ddlstudent_SelectedIndexChanged(object sender, EventArgs e)
        {
            Conversion objcon = new Conversion();
            StudentInfoBo objbook = new StudentInfoBo();
            objbook._SId = objcon.ConToInt16(ddlstudent.SelectedValue);
            DataTable dtbook = objbook.ShowAllIssueBookbystudent();
            DataRow dr = dtbook.NewRow();
            dr[0] = "0";
            dr[1] = "Select";
            dtbook.Rows.InsertAt(dr, 0);
            ddlbook.DataSource = dtbook;

            groupControl3.Visible = false;
        }

        private void txtdays_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void groupControl3_Paint(object sender, PaintEventArgs e)
        {

        }

    }
}
