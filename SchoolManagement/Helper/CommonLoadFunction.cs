using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using DebonoDLL;
using DevExpress.XtraEditors.Repository;
using DebonoDLL.BOL;
using DebonoDLL.App_Code.BOL;
using DevExpress.XtraGrid.Views.Grid;

namespace DEBONO.Helper
{
    public class CommonLoadFunction
    {

        public static void PopulateRepositoryGridLookupEdit(RepositoryItemGridLookUpEdit repgrdRawMaterial, DataTable dtData, string PrimaryColumn, string DisplayColumn, Boolean isDisplayPrimaryColumn)
        {
            repgrdRawMaterial.DataSource = dtData;
            repgrdRawMaterial.PopulateViewColumns();
            repgrdRawMaterial.DisplayMember = DisplayColumn;
            repgrdRawMaterial.ValueMember = PrimaryColumn;
            if (repgrdRawMaterial.View.Columns[PrimaryColumn] != null)
                repgrdRawMaterial.View.Columns[PrimaryColumn].Visible = isDisplayPrimaryColumn;
        }

        public static void FillUserRole(RepositoryItemGridLookUpEdit repGridLookUpEdit)
        {
            try
            {
                UserRoleBo objUserRole = new UserRoleBo();
                DataTable dtUserRole = objUserRole.GetAllUserRole();
                PopulateRepositoryGridLookupEdit(repGridLookUpEdit, dtUserRole, "UserRoleId", "RoleName", false);
            }
            catch (System.Exception ex)
            {
                ExceptionManager.LogException(ex);
            }
        }


        public static void FillFromConfig(RepositoryItemGridLookUpEdit repGridLookUpEdit, string type)
        {
            try
            {
                AppSettingBo objAppSetting = new AppSettingBo();
                objAppSetting._Type = type;
                DataTable dtMat = objAppSetting.ShowValueByType();
                PopulateRepositoryGridLookupEdit(repGridLookUpEdit, dtMat, "Id", "Value", false);
            }
            catch (System.Exception ex)
            {
                ExceptionManager.LogException(ex);
            }
        }

        public static void FillCategory(RepositoryItemGridLookUpEdit repGridLookUpEdit)
        {
            try
            {
                AppSettingBo objAppSetting = new AppSettingBo();
                DataTable dtMat = objAppSetting.LoadProductCategory();
                PopulateRepositoryGridLookupEdit(repGridLookUpEdit, dtMat, "Id", "Category", false);
            }
            catch (System.Exception ex)
            {
                ExceptionManager.LogException(ex);
            }
        }


        public static void FillConfig(RepositoryItemGridLookUpEdit repGridLookUpEdit, string type)
        {
            try
            {
                AppSettingBo objAppSetting = new AppSettingBo();
                objAppSetting._Type = type;
                DataTable dtMat = objAppSetting.ShowConfig();
                PopulateRepositoryGridLookupEdit(repGridLookUpEdit, dtMat, "Config", "Config", false);
            }
            catch (System.Exception ex)
            {
                ExceptionManager.LogException(ex);
            }
        }

        public static void FillColor(RepositoryItemGridLookUpEdit repGridLookUpEdit)
        {
            try
            {
                AppSettingBo objAppSetting = new AppSettingBo();
                objAppSetting._Type = "Color";
                DataTable dtColor = objAppSetting.LoadColor();
                PopulateRepositoryGridLookupEdit(repGridLookUpEdit, dtColor, "Id", "Value", false);
            }
            catch (System.Exception ex)
            {
                ExceptionManager.LogException(ex);
            }
        }






        public static void FillAllMaterialForP(RepositoryItemGridLookUpEdit repGridLookUpEdit)
        {
            try
            {
                //ProductBo objProduct = new ProductBo();
                //DataTable dtMat = objProduct.GetAllMaterialProduct();
                //PopulateRepositoryGridLookupEdit(repGridLookUpEdit, dtMat, "MaterialId", "Category", false);
                //repGridLookUpEdit.View.Columns["MatType"].Visible = false;

            }
            catch (System.Exception ex)
            {
                ExceptionManager.LogException(ex);
            }
        }


        public static void FillAllProduct(RepositoryItemGridLookUpEdit repGridLookUpEdit)
        {
            try
            {
                //ProductBo objMainProduct = new ProductBo();
                //DataTable dtProduct = objMainProduct.GetAllProduct();
                //PopulateRepositoryGridLookupEdit(repGridLookUpEdit, dtProduct, "ProductId", "Product", false);
                //repGridLookUpEdit.View.Columns["Category"].Visible = false;
            }
            catch (System.Exception ex)
            {
                ExceptionManager.LogException(ex);
            }
        }


        #region atribute
        public static void FillAllProductAttributeLengthWidthHeight(RepositoryItemGridLookUpEdit repGridLookUpEdit)
        {
            try
            {
                //ProductAttributeBo objProductAttribute = new ProductAttributeBo();
                //DataTable dtLength = objProductAttribute.LoadAllProductAttributeLengthWidthHeightForOrder("LENGTH");
                //PopulateRepositoryGridLookupEdit(repGridLookUpEdit, dtLength, "Length", "Length", true);
                //DataTable dtWidth = objProductAttribute.LoadAllProductAttributeLengthWidthHeightForOrder("WIDTH");
                //PopulateRepositoryGridLookupEdit(repGridLookUpEdit, dtWidth, "Width", "Width", true);
                //DataTable dtHeight = objProductAttribute.LoadAllProductAttributeLengthWidthHeightForOrder("HEIGHT");
                //PopulateRepositoryGridLookupEdit(repGridLookUpEdit, dtHeight, "Height", "Height", true);
            }
            catch (System.Exception ex)
            {
                ExceptionManager.LogException(ex);
            }
        }


        public static void FillAllProductAttributeLength(RepositoryItemGridLookUpEdit repGridLookUpEdit)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                //ProductAttributeBo objProductAttribute = new ProductAttributeBo();
                //Conversion objCon = new Conversion();
                //DataTable dtLength = objProductAttribute.LoadAllProductAttributeLengthForOrder();


                //if (dtLength != null)
                //{
                   
                //    dtTemp.Columns.Add("Length", typeof(Decimal));
                //    dtTemp.Columns.Add("ProductId", typeof(Int64)); 

                //    foreach (DataRow dr in dtLength.Rows)
                //    {
                //        DataRow drTemp = dtTemp.NewRow();
                //        drTemp["Length"] = objCon.ConToDec(dr["Length"]);
                //        drTemp["ProductId"] = objCon.ConToDec(dr["ProductId"]);
                //        dtTemp.Rows.Add(drTemp);
                //    }
                //}


                PopulateRepositoryGridLookupEdit(repGridLookUpEdit, dtTemp, "Length", "Length", true);
            }
            catch (System.Exception ex)
            {
                ExceptionManager.LogException(ex);
            }
        }

        public static void FillAllProductAttributeWidth(RepositoryItemGridLookUpEdit repGridLookUpEdit)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                Conversion objCon = new Conversion();
                //ProductAttributeBo objProductAttribute = new ProductAttributeBo();
                //DataTable dtWidth = objProductAttribute.LoadAllProductAttributeWidthForOrder();
                //if (dtWidth != null)
                //{
                
                //    dtTemp.Columns.Add("Width", typeof(Decimal));
                //    dtTemp.Columns.Add("ProductId", typeof(Int64));

                //    dtWidth.Columns.Add("Width1", typeof(decimal));
                //    foreach (DataRow dr in dtWidth.Rows)
                //    {
                //        DataRow drTemp = dtTemp.NewRow();
                //        drTemp["Width"] = objCon.ConToDec(dr["Width"]);
                //        drTemp["ProductId"] = objCon.ConToDec(dr["ProductId"]);
                //        dtTemp.Rows.Add(drTemp);
                //    }
                //}
                //dtWidth.Columns.Remove("Width");
                //dtWidth.Columns["Width1"].ColumnName = "Width";
                //PopulateRepositoryGridLookupEdit(repGridLookUpEdit, dtTemp, "Width", "Width", true);
                //repGridLookUpEdit.View.Columns["ProductId"].Visible = false;
            }
            catch (System.Exception ex)
            {
                ExceptionManager.LogException(ex);
            }
        }

        public static void FillAllProductAttributeHeight(RepositoryItemGridLookUpEdit repGridLookUpEdit)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                Conversion objCon = new Conversion();
                //ProductAttributeBo objProductAttribute = new ProductAttributeBo();
                //DataTable dtHeight = objProductAttribute.LoadAllProductAttributeHeightForOrder();

                //if (dtHeight != null)
                //{
                 
                //    dtTemp.Columns.Add("Height", typeof(Decimal));
                //    dtTemp.Columns.Add("ProductId", typeof(Int64));

                //    dtHeight.Columns.Add("Height1", typeof(decimal));
                //    foreach (DataRow dr in dtHeight.Rows)
                //    {
                //        DataRow drTemp = dtTemp.NewRow();
                //        drTemp["Height"] = objCon.ConToDec(dr["Height"]);
                //        drTemp["ProductId"] = objCon.ConToDec(dr["ProductId"]);
                //        dtTemp.Rows.Add(drTemp);
                //    }
                //}
                //dtHeight.Columns.Remove("Height");
                //dtHeight.Columns["Height1"].ColumnName = "Height";
                //PopulateRepositoryGridLookupEdit(repGridLookUpEdit, dtTemp, "Height", "Height", true);
                // repGridLookUpEdit.View.Columns["ProductId"].Visible = false;
            }
            catch (System.Exception ex)
            {
                ExceptionManager.LogException(ex);
            }
        }


        public static void FillAllProductAttributePlacement(RepositoryItemGridLookUpEdit repGridLookUpEdit)
        {
            try
            {
                //ProductAttributeBo objProductAttribute = new ProductAttributeBo();
                //DataTable dtPlacement = objProductAttribute.LoadAllProductAttributePlacementForOrder();
                //PopulateRepositoryGridLookupEdit(repGridLookUpEdit, dtPlacement, "Placement", "Placement", true);
                //repGridLookUpEdit.View.Columns["ProductId"].Visible = false;
            }
            catch (System.Exception ex)
            {
                ExceptionManager.LogException(ex);
            }
        }

        public static void FillAllProductAttributeColor1(RepositoryItemGridLookUpEdit repGridLookUpEdit)
        {
            try
            {
                //ProductAttributeBo objProductAttribute = new ProductAttributeBo();
                //DataTable dtColor1 = objProductAttribute.LoadAllProductAttributeColor1ForOrder();
                //PopulateRepositoryGridLookupEdit(repGridLookUpEdit, dtColor1, "Color1", "Color1", true);
                // repGridLookUpEdit.View.Columns["ProductId"].Visible = false;
            }
            catch (System.Exception ex)
            {
                ExceptionManager.LogException(ex);
            }
        }


        public static void FillAllProductAttributeColor2(RepositoryItemGridLookUpEdit repGridLookUpEdit)
        {
            try
            {
                //ProductAttributeBo objProductAttribute = new ProductAttributeBo();
                //DataTable dtColor2 = objProductAttribute.LoadAllProductAttributeColor2ForOrder();
                //PopulateRepositoryGridLookupEdit(repGridLookUpEdit, dtColor2, "Color2", "Color2", true);
                //repGridLookUpEdit.View.Columns["ProductId"].Visible = false;
            }
            catch (System.Exception ex)
            {
                ExceptionManager.LogException(ex);
            }
        }

        public static void FillAllProductAttributeColor3(RepositoryItemGridLookUpEdit repGridLookUpEdit)
        {
            try
            {
                //ProductAttributeBo objProductAttribute = new ProductAttributeBo();
                //DataTable dtColor3 = objProductAttribute.LoadAllProductAttributeColor3ForOrder();
                //PopulateRepositoryGridLookupEdit(repGridLookUpEdit, dtColor3, "Color3", "Color3", true);
                //repGridLookUpEdit.View.Columns["ProductId"].Visible = false;
            }
            catch (System.Exception ex)
            {
                ExceptionManager.LogException(ex);
            }
        }
        public static void FillAllProductAttributeColor4(RepositoryItemGridLookUpEdit repGridLookUpEdit)
        {
            try
            {
                //ProductAttributeBo objProductAttribute = new ProductAttributeBo();
                //DataTable dtColor4 = objProductAttribute.LoadAllProductAttributeColor4ForOrder();
                //PopulateRepositoryGridLookupEdit(repGridLookUpEdit, dtColor4, "Color4", "Color4", true);
                //repGridLookUpEdit.View.Columns["ProductId"].Visible = false;
            }
            catch (System.Exception ex)
            {
                ExceptionManager.LogException(ex);
            }
        }


        #endregion


        public static void DeleteItemFromGrid(GridView gvData, String PrimaryColumn, ref string[] strIdToDelete)
        {
            try
            {
                Conversion objCon = new Conversion();

                Int64 DelId = objCon.ConToInt64(gvData.GetFocusedRowCellValue(PrimaryColumn));
                if (Debono.DebonoMsg.MsgDelete())
                {
                    for (int k = 0; k < gvData.GetSelectedRows().Length; k++)
                    {
                        if (gvData.GetRowCellValue(gvData.GetSelectedRows()[k], PrimaryColumn) != null)
                        {
                            Int64 IDDEL = objCon.ConToInt64(gvData.GetRowCellValue(gvData.GetSelectedRows()[k], PrimaryColumn));
                            if (IDDEL != 0)
                            {
                                for (int i = 0; i <= strIdToDelete.Length; i++)
                                {
                                    if (strIdToDelete[i] == null)
                                    {
                                        strIdToDelete[i] = IDDEL.ToString();
                                        break;
                                    }
                                }
                            }
                        }
                    }
                    gvData.DeleteSelectedRows();                   
                }
            }
            catch (System.Exception ex)
            {
                ExceptionManager.LogException(ex);
            }
        }
    }
}
