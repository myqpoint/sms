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
    public partial class BookDetail : XtraForm
    {
        private string _UserName;
        public string UserName
        {
            get { return _UserName; }
            set { _UserName = value; }
        }

        private Int64 _BookId;
        private bool _isEditNew = true;
        public Int64 BookId
        {
            get { return _BookId; }
            set { _BookId = value; }
        }

        public BookDetail()
        {

            InitializeComponent();
            this._BookId = 0;
            this._UserName = UserName;
            _isEditNew = true;
        }

        public BookDetail(long BookId)
        {

            InitializeComponent();
            this._BookId = BookId;
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
            this._UserName = UserName;
            if (!chekDeletePermission() == true)
                toolStripButtonDelete.Enabled = false;
            if (!chekEditPermission() == true)
                toolStripButtonEdit.Enabled = false;


            SubjectBo objsub = new SubjectBo();
            DataTable dtsub = objsub.ShowSubjectMst();
            DataRow dr = dtsub.NewRow();
            dr[0] = "0";
            dr[1] = "Select";
            dtsub.Rows.InsertAt(dr, 0);
            ddlsubject.DataSource = dtsub;

            PublicationBo objpub = new PublicationBo();
            DataTable dtpub = objpub.ShowPublicationMst();
            dr = dtpub.NewRow();
            dr[0] = "0";
            dr[1] = "Select";
            dtpub.Rows.InsertAt(dr, 0);
            ddlpublication.DataSource = dtpub;

            FeesTypeBo objfees = new FeesTypeBo();
            DataTable dtfees = objfees.LoadAllClass();
            dr = dtfees.NewRow();
            dr[0] = "0";
            dr[1] = "Select";
            dtfees.Rows.InsertAt(dr, 0);
            ddlclass.DataSource = dtfees;

            GetOrderDetail();
        }

        private void OrdrDetail_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                if (toolStripButtonSave.Enabled)
                {
                    int nCheck = Debono.DebonoMsg.MsgCloseSaveConfirmation();
                    if (nCheck == 1)
                    {
                        toolStripButtonSave_ItemClick(null, null);
                        if (this.BookId <= 0)
                            e.Cancel = true;
                    }
                    else if (nCheck == 2)
                    {

                    }
                    else if (nCheck == 3)
                    {
                        e.Cancel = true;
                    }

                }
               // LoadOrSaveLayout(CommonFunction.custLayoutOptions.Save);
            }
            catch (Exception ex)
            {
                ExceptionManager.LogException(ex);
            }
        }

        private void OrdrDetail_Activated(object sender, EventArgs e)
        {
            try
            {
              //  LoadOrSaveLayout(CommonFunction.custLayoutOptions.Load);
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
                if (this._BookId > 0)
                {
                    Conversion objcon = new Conversion();
                    LoadBookDetails();
                    EnableDisableControls(false);
                }
                else
                {
                    EnableDisableControls(true);
                }
                FormHelper.CloseWaitDialog();
            }
            catch (Exception ex)
            {
                ExceptionManager.LogException(ex);
                FormHelper.CloseWaitDialog();
            }
        }

        private void LoadBookDetails()
        {
            try
            {
                if (this._BookId > 0)
                {
                    Conversion objCon = new Conversion();
                    BookBo objorder = new BookBo();
                    objorder._BookID = this._BookId;
                    objorder.LoadBookMst();
                    txtisbnnumber.Text = objorder._BookName;
                    
                    txtbarcode.Text = objorder._Barcode;

                    txtqty.Text = objorder._Quantities.ToString();
                    txtprice.Text = objorder._Price.ToString();
                    txtdetail.Text = objorder._Detail;
                    ddlsubject.SelectedValue = objorder._SubjectId;
                    ddlpublication.SelectedValue = objorder._PublicationId;
                    ddlclass.SelectedValue = objorder._ClassId;
                    txtbookname.Text = objorder._BookName;
                    txtauthor.Text = objorder._Author.ToString();
                    DataTable dtImage = objorder.LoadBookImage();
                    if (dtImage != null && dtImage.Rows.Count > 0)
                    {
                        byte[] img = (byte[])(dtImage.Rows[0]["BookImage"]);
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
        #endregion

        private void EnableDisableControls(Boolean isEnable)
        {
            txtisbnnumber.Enabled = isEnable;
            ddlsubject.Enabled = isEnable;
            txtbarcode.Enabled = isEnable;
            txtqty.Enabled = isEnable;
            ddlpublication.Enabled = isEnable;
            ddlclass.Enabled = isEnable;
            txtprice.Enabled = isEnable;
            txtdetail.Enabled = isEnable;
            txtauthor.Enabled = isEnable;
            txtbookname.Enabled = isEnable;
            CM_Delete.Enabled = isEnable;
            toolStripButtonSave.Enabled = isEnable;
            if (!chekDeletePermission() == true)
                toolStripButtonDelete.Enabled = false;
            else
            toolStripButtonDelete.Enabled = !isEnable;
            if (!chekEditPermission() == true)
                toolStripButtonEdit.Enabled = false;
            else
            toolStripButtonEdit.Enabled = !isEnable;
            toolStripButtonNew.Enabled = !isEnable;

        }
        private void ClearValue()
        {
            BookBo objOrder = new BookBo();
            objOrder._BookID = 0;


            txtisbnnumber.Text = string.Empty;
            txtbarcode.Text = string.Empty;
            txtqty.Text = string.Empty;
            ddlsubject.Text = string.Empty;
            ddlpublication.Text = string.Empty;
            ddlclass.Text = string.Empty;
            txtprice.Text = string.Empty;
            txtdetail.Text = string.Empty;
            txtauthor.Text = string.Empty;
            txtbookname.Text = string.Empty;
            TE_image.Text = string.Empty;
            pictureEdit1.Image = null;
        }

        #region Save
        private void toolStripButtonSave_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                if(string.IsNullOrEmpty(txtisbnnumber.Text))
                { DebonoMsg.MsgInformation("Please fill the ISBN Number"); return; }
                if (string.IsNullOrEmpty(txtbookname.Text))
                { DebonoMsg.MsgInformation("Please fill the Book Name"); return; }
                if (string.IsNullOrEmpty(txtauthor.Text))
                { DebonoMsg.MsgInformation("Please fill the Author "); return; }
                txtbookname.Text = System.Globalization.CultureInfo.CurrentCulture.TextInfo.ToTitleCase(txtbookname.Text.ToLower());
                txtauthor.Text = System.Globalization.CultureInfo.CurrentCulture.TextInfo.ToTitleCase(txtauthor.Text.ToLower());
                Conversion objCon = new Conversion();
                int nCheck = 0;
                FormHelper.ShowWaitDialog();
                nCheck = SaveBookInfo();
                if (nCheck > 0)
                {
                    EnableDisableControls(false);
                }
              FormHelper.CloseWaitDialog();
                if(nCheck>0)
              DebonoMsg.MsgInformation("Data Saved Successfully");
            }
            catch (Exception ex)
            {

                ExceptionManager.LogException(ex);
                FormHelper.CloseWaitDialog();
            }
        }

        private int SaveBookInfo()
        {
            Conversion objCon = new Conversion();
            try
            {
                BookBo objOrder = new BookBo();
                objOrder._BookID = objCon.ConToInt64(_BookId);
                objOrder.LoadBookMst();
                int avlqty = objOrder._AvailableQty;
                int rentqty = objOrder._RentQty;
                if (objCon.ConToInt16(txtqty.EditValue) < rentqty)
                {
                    DebonoMsg.MsgInformation("Sorry Qty can not less then rent qty "+rentqty.ToString()); return 0; 
                }
                objOrder._ISBN = objCon.ConToStr(txtisbnnumber.EditValue);
                objOrder._SubjectId = objCon.ConToInt16(ddlsubject.SelectedValue);
                objOrder._Barcode = objCon.ConToStr(txtbarcode.EditValue);
                objOrder._AvailableQty =objCon.ConToInt16(objCon.ConToInt(txtqty.Text)-objCon.ConToInt(rentqty));
                objOrder._Quantities = objCon.ConToInt16(txtqty.EditValue);
                objOrder._Price = objCon.ConToDec(txtprice.Text.Trim());
                objOrder._Detail = objCon.ConToStr(txtdetail.Text.Trim());
                objOrder._Author = objCon.ConToStr(txtauthor.Text.Trim());
                objOrder._BookName = objCon.ConToStr(txtbookname.Text);
                objOrder._PublicationId = objCon.ConToInt16(ddlpublication.SelectedValue);
                objOrder._ClassId = objCon.ConToInt16(ddlclass.SelectedValue);
                objOrder._CreatedBy = this._UserName;
                objOrder._ModifiedBy = this._UserName;
                if (pictureEdit1.Image != null)
                {
                    objOrder._BookImage = imageToByteArray(pictureEdit1.Image);
                }
                    int nCheck = 0;
                    if (objOrder._BookID > 0)
                    {
                        nCheck = objOrder.UpdateBookMst();
                    }
                    else
                    {
                        nCheck = objOrder.SaveBookMst();
                        this._BookId = objOrder._BookID;
                        FormHelper.SetScreenNumbering("BookId");
                    }
                    return nCheck;
            }
            catch (SqlException ex)
            {
                return 0;
            }


        }
        byte[] imageToByteArray(System.Drawing.Image imageIn)
        {
            MemoryStream ms = new MemoryStream();
            imageIn.Save(ms, System.Drawing.Imaging.ImageFormat.Gif);
            return ms.ToArray();
        }
        private void btnsingleUpload_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                TE_image.Text = openFileDialog1.FileName;
                pictureEdit1.Image = new Bitmap(openFileDialog1.FileName);
            }
        }
        
        #endregion

        #region Delete
        String AutoDeleteMessage = "are you sure to delete";
        String AutoDeleteTitle = "Confirm";
        private void toolStripButtonDelete_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Conversion objCon = new Conversion();
            try
            {
                if (Debono.DebonoMsg.MsgDelete())
                {
                    BookBo objOrderDtl = new BookBo();
                    objOrderDtl._BookID = this._BookId;
                    int nCheck = objOrderDtl.DeleteBookMst();
                    if (nCheck > 0)
                    {
                        this._BookId = 0;
                    }
                    GetOrderDetail();
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.LogException(ex);
            }
            this.Close();
        }
        DataTable dtOrderDtl = new DataTable();

        #endregion


        private void toolStripButtonCopy_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this._BookId = 0;
            EnableDisableControls(true);
        }

        private void toolStripButtonNew_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

            try
            {
                this._BookId = 0;
                ClearValue();
                GetOrderDetail();
            }
            catch (Exception)
            {

              //  throw;
            }
        }

        private void toolStripButtonEdit_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            GetOrderDetail();
            EnableDisableControls(true);
        }

        private void txtqty_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void txtprice_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
        }

        private void txtbookname_KeyPress(object sender, KeyPressEventArgs e)
        {
            
        }
    }
}
