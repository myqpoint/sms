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
    public partial class BookIssueForm : XtraForm
    {
        private string _UserName;
        public string UserName
        {
            get { return _UserName; }
            set { _UserName = value; }
        }

        private bool _isEditNew = true;
        public BookIssueForm()
        {

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
            PublicationBo objpub = new PublicationBo();
            DataTable dtpub = objpub.ShowPublicationMst();
            DataRow dr = dtpub.NewRow();
            dr[0] = "0";
            dr[1] = "Select";
            dtpub.Rows.InsertAt(dr, 0);
            ddlpublication.DataSource = dtpub;

            
        }

        private void ClearValue()
        {
            StaffBo objOrder = new StaffBo();
            objOrder._StaffId = 0;
            txtdays.Text = string.Empty;
            pictureEdit1.Image = null;
        }

        private void btnpaysalary_Click(object sender, EventArgs e)
        {
            try
            {
                if (ddlbook.SelectedValue.ToString()=="0")
                { DebonoMsg.MsgInformation("Please fill the Book Name"); return; }
                Conversion objCon = new Conversion();
                FormHelper.ShowWaitDialog();
                groupControl3.Visible = true;
                BookBo objbook = new BookBo();
                objbook._BookID = objCon.ConToInt16(ddlbook.SelectedValue);
                objbook.LoadBookMst();
                lblqty.Text = objbook._Quantities.ToString();
                lblprice.Text = objbook._Price.ToString();
                lbldetails.Text = objbook._Detail;
                lblavailableqty.Text = objbook._AvailableQty.ToString();
                lblrentqty.Text = objbook._RentQty.ToString();
                lblbookname.Text = objbook._BookName;
                lblauthor .Text = objbook._Author.ToString();
                PublicationBo objpub = new PublicationBo();
                objpub._PID = objCon.ConToInt(objbook._PublicationId);
                objpub.LoadPublicationMst();
                lblpublication.Text = objpub._Publication;

                FeesTypeBo objclass = new FeesTypeBo();
                objclass._FId = objCon.ConToInt(objbook._ClassId);
                objclass.LoadFeesType();
                lblclass.Text = objclass._ClassName;

                DataTable dtImage = objbook.LoadBookImage();
                if (dtImage != null && dtImage.Rows.Count > 0)
                {
                    byte[] img = (byte[])(dtImage.Rows[0]["BookImage"]);
                    MemoryStream mstream = new MemoryStream(img);
                    pictureEdit1.Image = Image.FromStream(mstream);
                }
                else
                    pictureEdit1.Image = null;

                FeesTypeBo objfees = new FeesTypeBo();
                DataTable dtfees = objfees.LoadAllClass();
                DataRow dr = dtfees.NewRow();
                dr[0] = "0";
                dr[1] = "Select";
                dtfees.Rows.InsertAt(dr, 0);
                ddlclass.DataSource = dtfees;


                FormHelper.CloseWaitDialog();
               

            }
            catch (Exception ex)
            {

                ExceptionManager.LogException(ex);
                FormHelper.CloseWaitDialog();
            }
        }
        private void ddlpublication_SelectedIndexChanged(object sender, EventArgs e)
        {
            Conversion objcon = new Conversion();
            BookBo objbook = new BookBo();
            objbook._PublicationId =objcon.ConToInt16(ddlpublication.SelectedValue);
            DataTable dtbook = objbook.LoadBookByPublication();
            DataRow dr = dtbook.NewRow();
            dr[0] = "0";
            dr[1] = "Select";
            dtbook.Rows.InsertAt(dr, 0);
            ddlbook.DataSource = dtbook;
            groupControl3.Visible = false;
        }

        private void ddlclass_SelectedIndexChanged(object sender, EventArgs e)
        {
            Conversion objcon = new Conversion();
            ClassInfoBo objbook = new ClassInfoBo();
            objbook._ClassName = objcon.ConToStr(ddlclass.Text);
            DataTable dtbook = objbook.LoadStudentByClass();
            DataRow dr = dtbook.NewRow();
            dr[0] = "0";
            dr[1] = "Select";
            dtbook.Rows.InsertAt(dr, 0);
            ddlstudent.DataSource = dtbook;
        }

        private void btnbookissue_Click(object sender, EventArgs e)
        {
             Conversion objcon = new Conversion();
            if (ddlclass.SelectedValue.ToString() == "0")
            { DebonoMsg.MsgInformation("Please fill the Class Name"); return; }
            if (ddlstudent.SelectedValue.ToString() == "0")
            { DebonoMsg.MsgInformation("Please fill the Student Name"); return; }
            if (txtdays.Text == "0" || txtdays.Text=="") 
            { DebonoMsg.MsgInformation("Please fill the Days"); return; }
            if (objcon.ConToInt(lblavailableqty.Text) == 0)
            {
                DebonoMsg.MsgInformation("Sorry ! Book not available"); return;
            }
            RentMstBo objrent = new RentMstBo();
            objrent._BookId = objcon.ConToInt64(ddlbook.SelectedValue);
            objrent._SId = objcon.ConToInt64(ddlstudent.SelectedValue);
            objrent._Days = objcon.ConToInt64(txtdays.Text);
            objrent._IssueBy = this._UserName;
            int chk = objrent.SaveRentMst();
            if (chk > 0)
            {
                lblavailableqty.Text = (objcon.ConToInt(lblavailableqty.Text) - 1).ToString();
                lblrentqty.Text = (objcon.ConToInt(lblrentqty.Text) + 1).ToString();
                objrent.UpdateBookRentQty();
                DebonoMsg.MsgInformation("Data Saved Successfully");
                ddlstudent.SelectedValue = "0";
                ddlclass.SelectedValue = "0";
                txtdays.Text = "";
            }
        }

        private void txtdays_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Verify that the pressed key isn't CTRL or any non-numeric digit
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }

            // If you want, you can allow decimal (float) numbers
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
        }

    }
}
