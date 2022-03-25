using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using DebonoDLL.BOL;
using DebonoDLL.App_Code.BOL;
using System.Data;
using System.IO;

namespace DEBONO.Detail
{
    public partial class ICard1 : DevExpress.XtraReports.UI.XtraReport
    {
        public ICard1()
        {
            InitializeComponent();
        }
        private void xrPictureBox1_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            StudentInfoBo objorder = new StudentInfoBo();
            Conversion objcon = new Conversion();
            objorder._SId = objcon.ConToInt64(xrLabel8.Text);
            DataTable dtImage = objorder.LoadStudentInfoImage();
            if (dtImage != null && dtImage.Rows.Count > 0)
            {
                byte[] img = (byte[])(dtImage.Rows[0]["StudentImage"]);
                MemoryStream mstream = new MemoryStream(img);
                xrPictureBox1.Image = Image.FromStream(mstream);
            }
            else
                xrPictureBox1.Image = null;
            xrLabel8.Visible = false;
        }
    }
}
