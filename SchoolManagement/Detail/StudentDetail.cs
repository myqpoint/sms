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
using System.Text.RegularExpressions;

namespace Debono.Detail
{
    public partial class StudentDetail : XtraForm
    {
        private string _UserName;
        public string UserName
        {
            get { return _UserName; }
            set { _UserName = value; }
        }

        private Int64 _SId;
        private bool _isEditNew = true;
        public Int64 SId
        {
            get { return _SId; }
            set { _SId = value; }
        }

        public StudentDetail()
        {

            InitializeComponent();
            this._SId = 0;
            this._UserName = UserName;
            _isEditNew = true;
        }

        public StudentDetail(long SId)
        {

            InitializeComponent();
            this._SId = SId;
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
            //txtdob.DateTime = DateTime.UtcNow.Date;
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
                        if (this.SId <= 0)
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
                LoadOrSaveLayout(CommonFunction.custLayoutOptions.Save);
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
                    LoadClassDetails();
                    EnableDisableControls(false);
                }
                else
                {
                    Gc_orderdtl.DataSource = null;
                    EnableDisableControls(true);
                }
                LoadStudentExternalLink();
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
                txtdob.EditValue = "";
                if (this._SId > 0)
                {
                    Conversion objCon = new Conversion();
                    StudentInfoBo objorder = new StudentInfoBo();
                    objorder._SId = this._SId;
                    objorder.LoadStudentInfo();
                    txtfirstname.Text = objorder._FName;
                    txtlastname.Text = objorder._LName;
                    txtdob.EditValue = objorder._DOB;
                    ddlgender.SelectedItem = objorder._Gender;
                    txtmobilenumber.Text = objorder._MobileNumber;
                    txtaadharno.Text = objorder._AadharNumber;
                    txtemailid.Text = objorder._EmailId;
                    txtfathername.Text = objorder._FatherName;
                   // txtfathermobilenumber.Text = objorder._FatherMobileNumber;
                    txtmothername.Text = objorder._MotherName;
                    txtaddress.Text = objorder._Address;
                    txtcity.Text = objorder._City;
                    txtstate.Text = objorder._State;
                    txtpincode.Text = objorder._Pincode;
                    ddlreligion.SelectedItem = objorder._Religion;
                    ddlcategory.SelectedItem = objorder._CasteCategory;
                    ddlsession.SelectedItem = objorder._Session;
                    ddlpaymenttype.SelectedItem = objorder._PaymentType;

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

        private void LoadClassDetails()
        {
            try
            {
                ClassInfoBo objOrderDetail = new ClassInfoBo();
                objOrderDetail._SId = this._SId;
                dtOrderDtl = objOrderDetail.ShowStudentClassInfo();
                Gc_orderdtl.DataSource = dtOrderDtl;

            }
            catch (Exception ex)
            {
                ExceptionManager.LogException(ex);
            }
        }

        #endregion

        private void EnableDisableControls(Boolean isEnable)
        {
            txtfirstname.Enabled = isEnable;
            txtlastname.Enabled = isEnable;
            txtdob.Enabled = isEnable;
            txtadmissiondate.Enabled = isEnable;
            ddlgender.Enabled = isEnable;
            txtmobilenumber.Enabled = isEnable;
            txtemailid.Enabled = isEnable;
            txtaadharno.Enabled = isEnable;
            ddlreligion.Enabled = isEnable;
            ddlcategory.Enabled = isEnable;
            txtfathername.Enabled = isEnable;
            txtmothername.Enabled = isEnable;
            ddlsection.Enabled = isEnable;
            txtaddress.Enabled = isEnable;
            txtcity.Enabled = isEnable;
            txtstate.Enabled = isEnable;
            txtpincode.Enabled = isEnable;
            ddlsession.Enabled = isEnable;
            ddlpaymenttype.Enabled = isEnable;
            ddlclassname.Enabled = isEnable;
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
            gvOrderDtl.OptionsBehavior.Editable = isEnable;

        }
        private void ClearValue()
        {
            StudentInfoBo objOrder = new StudentInfoBo();
            objOrder._SId = 0;


            txtfirstname.Text = string.Empty;
            txtlastname.Text = string.Empty;
            txtmobilenumber.Text = string.Empty;
            txtemailid.Text = string.Empty;
            ddlgender.Text = string.Empty;
            ddlreligion.Text = string.Empty;
            ddlcategory.Text = string.Empty;
            txtfathername.Text = string.Empty;
            txtmothername.Text = string.Empty;
            txtaadharno.Text = string.Empty;
            ddlsection.Text = string.Empty;
            txtaddress.Text = string.Empty;
            txtcity.Text = string.Empty;
            txtstate.Text = string.Empty;
            txtpincode.Text = string.Empty;
            ddlsession.Text = string.Empty;
            ddlpaymenttype.Text = string.Empty;
            txtdob.Text = string.Empty;
            txtadmissiondate.Text = string.Empty;
            ddlclassname.Text = string.Empty;
            TE_image.Text = string.Empty;
            pictureEdit1.Image = null;
        }

        #region Save
        private void toolStripButtonSave_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                if(string.IsNullOrEmpty(txtfirstname.Text))
                { DebonoMsg.MsgInformation("Please fill the FirstName"); return; }
                if (txtemailid.Text.Trim() != "")
                {
                    if (!ValidateEmail())
                    {
                        DebonoMsg.MsgInformation("Email Id is InValid");
                        txtemailid.Focus();
                        return;
                    }
                }
                if (ddlclassname.Text != "" && ddlclassname.Text != "Select")
                {
                    if (ddlsection.Text == "")
                    {
                        DebonoMsg.MsgInformation("Please Select Class Section");
                        ddlsection.Focus();
                        return;
                    }
                }
                Conversion objCon = new Conversion();
                int nCheck = 0;
                FormHelper.ShowWaitDialog();
                nCheck = SaveStudentInfo();
                if (nCheck > 0)
                {
                    SaveStudentExternalLink();
                    SaveClassDetail();
                    EnableDisableControls(false);
                }
              FormHelper.CloseWaitDialog();
                if(nCheck>0)
              DebonoMsg.MsgInformation("Data Saved Successfully");
                LoadClassDetails();
            }
            catch (Exception ex)
            {

                ExceptionManager.LogException(ex);
                FormHelper.CloseWaitDialog();
            }
        }

        private int SaveStudentInfo()
        {
            Conversion objCon = new Conversion();
            try
            {
                StudentInfoBo objOrder = new StudentInfoBo();
                objOrder._SId = objCon.ConToInt64(_SId);
                objOrder._FName = objCon.ConToStr(txtfirstname.EditValue);
                objOrder._LName = objCon.ConToStr(txtlastname.Text.Trim());
                objOrder._DOB = objCon.ConToDT(txtdob.EditValue);
                objOrder._AdmissionDate = objCon.ConToDT(txtadmissiondate.EditValue);
                objOrder._Gender = objCon.ConToStr(ddlgender.SelectedItem);
                objOrder._MobileNumber = objCon.ConToStr(txtmobilenumber.EditValue);
                objOrder._EmailId = objCon.ConToStr(txtemailid.EditValue);
                objOrder._AadharNumber = objCon.ConToStr(txtaadharno.EditValue);
                objOrder._FatherName = objCon.ConToStr(txtfathername.Text.Trim());
                //objOrder._FatherMobileNumber = objCon.ConToStr(ddlsection.Text.Trim());
                objOrder._MotherName = objCon.ConToStr(txtmothername.Text.Trim());
                objOrder._Address = objCon.ConToStr(txtaddress.Text.Trim());
                objOrder._City = objCon.ConToStr(txtcity.Text.Trim());
                objOrder._State = objCon.ConToStr(txtstate.Text.Trim());
                objOrder._Pincode = objCon.ConToStr(txtpincode.Text.Trim());
                objOrder._Religion = objCon.ConToStr(ddlreligion.SelectedItem);
                objOrder._CasteCategory = objCon.ConToStr(ddlcategory.SelectedItem);
                objOrder._PaymentType = objCon.ConToStr(ddlpaymenttype.SelectedItem);
                objOrder._Session = objCon.ConToStr(ddlsession.SelectedItem);
                objOrder._CreatedBy = this._UserName;
                objOrder._ModifiedBy = this._UserName;
                if (pictureEdit1.Image != null)
                {
                    objOrder._StudentImage = imageToByteArray(pictureEdit1.Image);
                }
                    int nCheck = 0;
                    if (objOrder._SId > 0)
                    {
                        nCheck = objOrder.UpdateStudentInfo();
                    }
                    else
                    {
                        nCheck = objOrder.SaveStudentInfo();
                        this._SId = objOrder._SId;
                        FormHelper.SetScreenNumbering("SId");
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
            ColumnView view1 = Gc_orderdtl.FocusedView as ColumnView;
            view1.CloseEditor();
            view1.UpdateCurrentRow();
            Conversion objCon = new Conversion();
            ClassInfoBo objOrderDtl = new ClassInfoBo();
            objOrderDtl._SId = this._SId;
            //objOrderDtl.DeleteClassInfoByStudent();
            //for (int i = 0; i < dtOrderDtl.Rows.Count; i++)
            //{
            //    objOrderDtl._PaymentType = objCon.ConToStr(dtOrderDtl.Rows[i]["PaymentType"]);
            //    objOrderDtl._SessionYear = objCon.ConToStr(dtOrderDtl.Rows[i]["SessionYear"]);
            //    objOrderDtl._ClassName = objCon.ConToStr(dtOrderDtl.Rows[i]["ClassName"]);
            //    objOrderDtl._AdmissionDate = objCon.ConToDT(dtOrderDtl.Rows[i]["AdmissionDate"]);
            //    objOrderDtl._SId = this._SId;
            //    objOrderDtl._CreatedBy = this.UserName;
            //    objOrderDtl.SaveClassInfo();
            //}
            if (objCon.ConToStr(ddlclassname.SelectedItem) != "" && ddlclassname.SelectedItem != "Select")
            {
                if (objCon.ConToStr(ddlpaymenttype.SelectedItem) != "" && ddlpaymenttype.SelectedItem != "Select" && ddlsession.SelectedItem != "" && ddlsession.SelectedItem != "Select")
                {
                    objOrderDtl._PaymentType = ddlpaymenttype.SelectedItem.ToString();
                    objOrderDtl._SessionYear = ddlsession.SelectedItem.ToString();
                    objOrderDtl._ClassName = ddlclassname.SelectedItem.ToString();
                    objOrderDtl._ClassSection = ddlsection.SelectedItem.ToString();
                    objOrderDtl._AdmissionDate = objCon.ConToDT(txtadmissiondate.EditValue);
                    objOrderDtl._SId = this._SId;
                    objOrderDtl.LoadClassInfobystudent();
                    objOrderDtl._CreatedBy = this.UserName;
                    if (objOrderDtl._CId > 0)
                    {
                        objOrderDtl.UpdateClassInfo();
                    }
                    else
                    {
                        objOrderDtl.SaveClassInfo();
                    }
                }
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
                    StudentInfoBo objOrderDtl = new StudentInfoBo();
                    objOrderDtl._SId = this._SId;
                    int nCheck = objOrderDtl.DeleteStudentInfo();
                    if (nCheck > 0)
                    {
                        ClassInfoBo objOrder = new ClassInfoBo();
                        objOrder._SId = this._SId;
                        objOrder.DeleteClassInfoByStudent();
                        this._SId = 0;
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
        private void CM_Delete_Click(object sender, EventArgs e)
        {
            try
            {
                if (Debono.DebonoMsg.MsgDelete())
                {
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


        private void toolStripButtonCopy_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this._SId = 0;
            EnableDisableControls(true);
        }

        private void toolStripButtonNew_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

            try
            {
                this._SId = 0;
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
            objcmnFun.LoadORSaveLayout(this.Name + "&", ref gvOrderDtl, enLayoutOptions);
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
        private void LoadStudentExternalLink()
        {
            try
            {
                Conversion objCon = new Conversion();
                ExternalLinkBo objExternalLink = new ExternalLinkBo();
                objExternalLink._FormType = "Student";
                objExternalLink._FormId = objCon.ConToInt64(_SId);
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

        private void SaveStudentExternalLink()
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
                    objExternalLink._FormId = this.SId;
                    objExternalLink._FormType = "Student";
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

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void labelControl7_Click(object sender, EventArgs e)
        {

        }

        private void txtmobilenumber_Properties_KeyPress(object sender, KeyPressEventArgs e)
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

        private void txtpincode_KeyPress(object sender, KeyPressEventArgs e)
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
        private bool ValidateEmail()
        {
            string email = txtemailid.Text;
            Regex regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
            Match match = regex.Match(email);
            return match.Success;
           
        }

        private void txtaadharno_KeyPress(object sender, KeyPressEventArgs e)
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
