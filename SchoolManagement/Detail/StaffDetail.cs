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
    public partial class StaffDetail : XtraForm
    {
        private string _UserName;
        public string UserName
        {
            get { return _UserName; }
            set { _UserName = value; }
        }

        private Int64 _StaffId;
        private bool _isEditNew = true;
        public Int64 StaffId
        {
            get { return _StaffId; }
            set { _StaffId = value; }
        }
        private Int64 _SPId;
        public Int64 SPId
        {
            get { return _SPId; }
            set { _SPId = value; }
        }

        public StaffDetail()
        {

            InitializeComponent();
            this._StaffId = 0;
            this._UserName = UserName;
            _isEditNew = true;
        }

        public StaffDetail(long StaffId)
        {

            InitializeComponent();
            this._StaffId = StaffId;
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
            txtdob.DateTime = DateTime.UtcNow.Date;
            if (!chekDeletePermission() == true)
                toolStripButtonDelete.Enabled = false;
            if (!chekEditPermission() == true)
                toolStripButtonEdit.Enabled = false;
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
                        if (this.StaffId <= 0)
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
                //LoadOrSaveLayout(CommonFunction.custLayoutOptions.Save);
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
               // LoadOrSaveLayout(CommonFunction.custLayoutOptions.Load);
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
                    LoadStaffDetails();
                    EnableDisableControls(false);
                }
                else
                {
                   // Gc_orderdtl.DataSource = null;
                    EnableDisableControls(true);
                }
                LoadStaffExternalLink();
                //LoadPaymentHistory();
                FormHelper.CloseWaitDialog();

            }
            catch (Exception ex)
            {
                ExceptionManager.LogException(ex);
                FormHelper.CloseWaitDialog();
            }
        }

        private void LoadStaffDetails()
        {
            try
            {
                txtdob.EditValue = "";
                if (this._StaffId > 0)
                {
                    Conversion objCon = new Conversion();
                    StaffBo objorder = new StaffBo();
                    objorder._StaffId = this._StaffId;
                    objorder.LoadStaff();
                    txtstaffname.Text = objorder._Name;
                    
                    txtdob.EditValue = objorder._DOB;
                    ddlgender.SelectedItem = objorder._Gender;
                    txtmobilenumber.Text = objorder._MobileNumber;
                    txtaadharno.Text = objorder._AadharNumber;
                    txtemailid.Text = objorder._EmailId;
                    txtfathername.Text = objorder._FatherName;
                    txtaddress.Text = objorder._Address;
                    ddlreligion.SelectedItem = objorder._Religion;
                    ddlcategory.SelectedItem = objorder._CasteCategory;
                    txtjoinningdate.Text = objorder._JoinningDate.ToString();
                    txtqualification.Text = objorder._Qualification;
                    txtsalaryamount.Text = objorder._SalaryAmount.ToString();
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
        #endregion

        private void EnableDisableControls(Boolean isEnable)
        {
            txtstaffname.Enabled = isEnable;
            txtdob.Enabled = isEnable;
            txtjoinningdate.Enabled = isEnable;
            ddlgender.Enabled = isEnable;
            txtmobilenumber.Enabled = isEnable;
            txtemailid.Enabled = isEnable;
            txtaadharno.Enabled = isEnable;
            ddlreligion.Enabled = isEnable;
            ddlcategory.Enabled = isEnable;
            txtfathername.Enabled = isEnable;
            txtaddress.Enabled = isEnable;
            txtsalaryamount.Enabled = isEnable;
            txtqualification.Enabled = isEnable;
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
           // gvOrderDtl.OptionsBehavior.Editable = isEnable;

        }
        private void ClearValue()
        {
            StaffBo objOrder = new StaffBo();
            objOrder._StaffId = 0;


            txtstaffname.Text = string.Empty;
            txtmobilenumber.Text = string.Empty;
            txtemailid.Text = string.Empty;
            ddlgender.Text = string.Empty;
            ddlreligion.Text = string.Empty;
            ddlcategory.Text = string.Empty;
            txtfathername.Text = string.Empty;
            txtaadharno.Text = string.Empty;
            txtaddress.Text = string.Empty;
            txtsalaryamount.Text = string.Empty;
            txtqualification.Text = string.Empty;
            txtdob.Text = string.Empty;
            txtjoinningdate.Text = string.Empty;
            TE_image.Text = string.Empty;
            pictureEdit1.Image = null;
        }

        #region Save
        private void toolStripButtonSave_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                if(string.IsNullOrEmpty(txtstaffname.Text))
                { DebonoMsg.MsgInformation("Please fill the Staff Name"); return; }
                if (string.IsNullOrEmpty(txtfathername.Text))
                { DebonoMsg.MsgInformation("Please fill the Staff FatherName"); return; }
                txtstaffname.Text = System.Globalization.CultureInfo.CurrentCulture.TextInfo.ToTitleCase(txtstaffname.Text.ToLower());
                txtfathername.Text = System.Globalization.CultureInfo.CurrentCulture.TextInfo.ToTitleCase(txtfathername.Text.ToLower());
                Conversion objCon = new Conversion();
                int nCheck = 0;
                System.Text.RegularExpressions.Regex rEMail = new System.Text.RegularExpressions.Regex(@"^[a-zA-Z][\w\.-]{2,28}[a-zA-Z0-9]@[a-zA-Z0-9][\w\.-]*[a-zA-Z0-9]\.[a-zA-Z][a-zA-Z\.]*[a-zA-Z]$");
                //txtmail is name/object of textbox
                if (txtemailid.Text.Length > 0)
                {
                    //rEMail is object of Regex class located in System.Text.RegularExpressions
                    if (!rEMail.IsMatch(txtemailid.Text))
                    {
                        MessageBox.Show("E-Mail Id is Invalid", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        txtemailid.SelectAll();
                        return;
                    }
                }
                if (txtmobilenumber.Text.Length > 0 && txtmobilenumber.Text.Length<10)
                {
                    MessageBox.Show("Invalid Mobile Number", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtemailid.SelectAll();
                    return;
                }
                if (txtaadharno.Text.Length > 0 && txtaadharno.Text.Length < 12)
                {
                    MessageBox.Show("Invalid Aadhar Number", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtemailid.SelectAll();
                    return;
                }
                FormHelper.ShowWaitDialog();
                nCheck = SaveStaffInfo();
                if (nCheck > 0)
                {
                    SaveStaffExternalLink();
                    //SaveClassDetail();
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

        private int SaveStaffInfo()
        {
            Conversion objCon = new Conversion();
            try
            {
                StaffBo objOrder = new StaffBo();
                objOrder._StaffId = objCon.ConToInt64(_StaffId);
                objOrder._Name = objCon.ConToStr(txtstaffname.EditValue);
                objOrder._DOB = objCon.ConToDT(txtdob.EditValue);
                objOrder._JoinningDate = objCon.ConToDT(txtjoinningdate.EditValue);
                objOrder._Gender = objCon.ConToStr(ddlgender.SelectedItem);
                objOrder._MobileNumber = objCon.ConToStr(txtmobilenumber.EditValue);
                objOrder._EmailId = objCon.ConToStr(txtemailid.EditValue);
                objOrder._AadharNumber = objCon.ConToStr(txtaadharno.EditValue);
                objOrder._FatherName = objCon.ConToStr(txtfathername.Text.Trim());
                objOrder._Address = objCon.ConToStr(txtaddress.Text.Trim());
                objOrder._SalaryAmount = objCon.ConToDec(txtsalaryamount.Text.Trim());
                objOrder._Qualification = objCon.ConToStr(txtqualification.Text);
                objOrder._Religion = objCon.ConToStr(ddlreligion.Text);
                objOrder._CasteCategory = objCon.ConToStr(ddlcategory.Text);
                objOrder._CreatedBy = this._UserName;
                objOrder._ModifiedBy = this._UserName;
                if (pictureEdit1.Image != null)
                {
                    objOrder._StaffImage = imageToByteArray(pictureEdit1.Image);
                }
                    int nCheck = 0;
                    if (objOrder._StaffId > 0)
                    {
                        nCheck = objOrder.UpdateStaff();
                    }
                    else
                    {
                        nCheck = objOrder.SaveStaff();
                        this._StaffId = objOrder._StaffId;
                        FormHelper.SetScreenNumbering("StaffId");
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
        private void SaveClassDetail()
        {
            //ColumnView view1 = Gc_orderdtl.FocusedView as ColumnView;
            //view1.CloseEditor();
            //view1.UpdateCurrentRow();
            //Conversion objCon = new Conversion();
            //ClassInfoBo objOrderDtl = new ClassInfoBo();
            //objOrderDtl._StaffId = this._StaffId;
            //objOrderDtl.DeleteClassInfoByStaff();
            //for (int i = 0; i < dtOrderDtl.Rows.Count; i++)
            //{
            //    objOrderDtl._PaymentType = objCon.ConToStr(dtOrderDtl.Rows[i]["PaymentType"]);
            //    objOrderDtl._SessionYear = objCon.ConToStr(dtOrderDtl.Rows[i]["SessionYear"]);
            //    objOrderDtl._ClassName = objCon.ConToStr(dtOrderDtl.Rows[i]["ClassName"]);
            //    objOrderDtl._StaffId = this._StaffId;
            //    objOrderDtl._CreatedBy = this.UserName;
            //    objOrderDtl.SaveClassInfo();
            //}
            //if (objCon.ConToStr(ddlclassname.SelectedItem) != "" && ddlclassname.SelectedItem != "Select")
            //{
            //    if (objCon.ConToStr(ddlpaymenttype.SelectedItem) != "" && ddlpaymenttype.SelectedItem != "Select" && ddlsession.SelectedItem != "" && ddlsession.SelectedItem != "Select")
            //    {
            //        objOrderDtl._PaymentType = ddlpaymenttype.SelectedItem.ToString();
            //        objOrderDtl._SessionYear = ddlsession.SelectedItem.ToString();
            //        objOrderDtl._ClassName = ddlclassname.SelectedItem.ToString();
            //        objOrderDtl._StaffId = this._StaffId;
            //        objOrderDtl.LoadClassInfobyStaff();
            //        objOrderDtl._CreatedBy = this.UserName;
            //        if (objOrderDtl._CId > 0)
            //        {
            //            objOrderDtl.UpdateClassInfo();
            //        }
            //        else
            //        {
            //            objOrderDtl.SaveClassInfo();
            //        }
            //    }
            //}
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
                    StaffBo objOrderDtl = new StaffBo();
                    objOrderDtl._StaffId = this._StaffId;
                    int nCheck = objOrderDtl.DeleteStaff();
                    if (nCheck > 0)
                    {
                        this._StaffId = 0;
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
            this._StaffId = 0;
            EnableDisableControls(true);
        }

        private void toolStripButtonNew_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

            try
            {
                this._StaffId = 0;
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

        private void LoadOrSaveLayout(CommonFunction.custLayoutOptions enLayoutOptions)
        {
            CommonFunction objcmnFun = new CommonFunction();
            //objcmnFun.LoadORSaveLayout(this.Name + "&", ref gvOrderDtl, enLayoutOptions);
        }

        DataTable dtRawMaterialExtLink = new DataTable();
        DataTable dtRawMaterialExtLinkDrawing = new DataTable();
        String[] strDelMaterialExtLink = new string[250];
        private void btnExternalLink_Click(object sender, EventArgs e)
        {
            try
            {
                ExternalLink objExternalLink = new ExternalLink();
                objExternalLink._dtExternalLink = dtRawMaterialExtLink;
                objExternalLink.strToDeleteExternalLink = strDelMaterialExtLink;
                objExternalLink.ShowDialog();
                strDelMaterialExtLink = objExternalLink.strToDeleteExternalLink;
                dtRawMaterialExtLink = objExternalLink._dtExternalLink;
            }
            catch (Exception ex)
            {
                ExceptionManager.LogException(ex);
            }
        }
        private void LoadStaffExternalLink()
        {
            try
            {
                Conversion objCon = new Conversion();
                ExternalLinkBo objExternalLink = new ExternalLinkBo();
                objExternalLink._FormType = "Staff";
                objExternalLink._FormId = objCon.ConToInt64(_StaffId);
                dtRawMaterialExtLink = objExternalLink.LoadExternalLinkByFormIdAndType();
                dtRawMaterialExtLinkDrawing = dtRawMaterialExtLink.Clone();
                DataRow[] drArr = dtRawMaterialExtLink.Select("isDrawing=" + true);
                if (drArr != null && drArr.Length > 0)
                {
                    drArr.CopyToDataTable(dtRawMaterialExtLinkDrawing, LoadOption.Upsert);

                    foreach (DataRow dr in drArr)
                    {
                        dr.Delete();
                    }

                }
                else
                {
                    dtRawMaterialExtLinkDrawing = dtRawMaterialExtLink.Clone();
                }
                dtRawMaterialExtLink.AcceptChanges();

            }
            catch (Exception ex)
            {
                ExceptionManager.LogException(ex);
            }
        }

        private void SaveStaffExternalLink()
        {
            Conversion objCon = new Conversion();
            try
            {
                if (dtRawMaterialExtLink != null)
                {
                    //AppSettingBo objAppsettingbo = new AppSettingBo();
                    //serverpath = objAppsettingbo.getServerData("localpath");
                    //serverusername = objAppsettingbo.getServerData("SERVERUSERNAME");
                    //serverpwd = objAppsettingbo.getServerData("SERVERPWD");

                    SaveFileDialog objSave = new SaveFileDialog();
                    ExternalLinkBo objExternalLink = new ExternalLinkBo();
                    objExternalLink._FormId = this.StaffId;
                    objExternalLink._FormType = "Staff";
                    objExternalLink.DeleteExternalLinkByFormIdAndType();
                    string Id = objCon.ConToStr(Guid.NewGuid());

                    foreach (DataRow dr in dtRawMaterialExtLink.Rows)
                    {
                        string Paths = Application.StartupPath + "\\Image\\";
                        Paths = Paths + objCon.ConToStr(Guid.NewGuid()) + "_" + objCon.ConToStr(dr["LinkFile"]) + objCon.ConToStr(dr["LinkFileType"]);
                      string serverpath = ConfigurationManager.AppSettings["ServerImageFolder"];
                        string strserverpath = serverpath;
                        strserverpath = strserverpath + objCon.ConToStr(Guid.NewGuid()) + "_" + objCon.ConToStr(dr["LinkFile"]) + objCon.ConToStr(dr["LinkFileType"]);
                        if (!string.IsNullOrEmpty(serverpath))
                        { objExternalLink._Path = strserverpath; }
                        else
                        { objExternalLink._Path = Paths; }

                        objExternalLink._LastUpdate = DateTime.Today;
                        objExternalLink._LinkDescription = objCon.ConToStr(dr["LinkDescription"]);
                        objExternalLink._LinkFile = objCon.ConToStr(dr["LinkFile"]);
                        objExternalLink._LinkFileType = objCon.ConToStr(dr["LinkFileType"]);
                        objExternalLink.SaveExternalLink();
                        try
                        {
                            string base_Path1 = Application.StartupPath;
                            if (!string.IsNullOrEmpty(serverpath))
                                File.Copy(objCon.ConToStr(dr["Path"].ToString()), base_Path1+strserverpath, true);
                            else
                                File.Copy(objCon.ConToStr(dr["Path"].ToString()), Paths, true);
                        }
                        catch (Exception ex)
                        {
                            ExceptionManager.LogException(ex);
                        }
                    }

                    //foreach (DataRow dr in dtRawMaterialExtLinkDrawing.Rows)
                    //{

                    //    string Paths = Application.StartupPath + "\\Image\\";



                    //    string strserverpath = serverpath;
                    //    strserverpath = strserverpath + "\\" + objCon.ConToStr(Guid.NewGuid()) + "_" + objCon.ConToStr(dr["LinkFile"]) + objCon.ConToStr(dr["LinkFileType"]);

                    //    Paths = Paths + objCon.ConToStr(Guid.NewGuid()) + "_" + objCon.ConToStr(dr["LinkFile"]) + objCon.ConToStr(dr["LinkFileType"]);

                    //    if (!string.IsNullOrEmpty(serverpath))
                    //    { objExternalLink._Path = strserverpath; }
                    //    else
                    //    { objExternalLink._Path = Paths; }

                    //    objExternalLink._LastUpdate = DateTime.Today;
                    //    objExternalLink._LinkDescription = objCon.ConToStr(dr["LinkDescription"]);
                    //    objExternalLink._LinkFile = objCon.ConToStr(dr["LinkFile"]);
                    //    objExternalLink._LinkFileType = objCon.ConToStr(dr["LinkFileType"]);
                    //    objExternalLink._isDrawing = true;
                    //    objExternalLink.SaveExternalLink();
                    //    try
                    //    {
                    //        if (!string.IsNullOrEmpty(serverpath))
                    //            File.Copy(objCon.ConToStr(dr["Path"].ToString()), strserverpath, true);
                    //        else
                    //            File.Copy(objCon.ConToStr(dr["Path"].ToString()), Paths, true);
                    //    }
                    //    catch (Exception ex)
                    //    {
                    //        ExceptionManager.LogException(ex);
                    //    }
                    //}
                }
            }
            catch (SqlException ex)
            {
                ExceptionManager.LogException(ex);
            }
        }

        private void groupControl1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void groupControl3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void txtmobilenumber_Properties_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void txtsalaryamount_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
        }

        private void txtaadharno_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void txtemailid_KeyPress(object sender, KeyPressEventArgs e)
        {
            
        }
    }
}
