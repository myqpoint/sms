using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.XtraGrid.Views.Grid;
using System.Data;
using System.Windows.Forms;
using DebonoDLL;

namespace Debono
{
    public class CommonFunction
    {
      
        public enum custLayoutOptions
        {
            Save,
            Load,
            Delete,
            None
        }

        public static void WaitCursor()
        {
            //Cursor myCursor = NativeMethods.LoadCustomCursor(@"AcctCursor.ani");
            //Cursor.Current = myCursor;
            Cursor.Current = Cursors.WaitCursor;
        }
        public static void DefaultCursor()
        {
            Cursor.Current = Cursors.Default;
        }
        public void LoadORSaveLayout(string strFormName, ref  GridView gridView, custLayoutOptions enLayoutOptions)
        {
            try
            {
                string strXMLFilePath = System.IO.Directory.GetCurrentDirectory().ToString() + "\\GridLayouts\\";

                if (!System.IO.Directory.Exists(strXMLFilePath))
                    System.IO.Directory.CreateDirectory(strXMLFilePath);

                string strXMLFileName = strXMLFilePath + strFormName + gridView.Name + ".xml";

                if (!System.IO.File.Exists(strXMLFileName))
                    System.IO.File.Create(strXMLFileName);

                switch (enLayoutOptions)
                {
                    case custLayoutOptions.Load:
                        {
                            //if (System.IO.File.Exists(strXMLFilePath))
                            gridView.RestoreLayoutFromXml(strXMLFileName);
                            //  gridView.RestoreLayoutFromRegistry(strRegKey);
                            break;
                        }
                    case custLayoutOptions.Save:
                        {
                            // gridView.SaveLayoutToRegistry(strRegKey);

                            gridView.SaveLayoutToXml(strXMLFileName);
                            break;
                        }
                    case custLayoutOptions.Delete:
                        gridView.DestroyCustomization();
                        break;
                }
            }
            catch (Exception ex)
            {

            }
        }

   
    }
}
