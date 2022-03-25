using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace DebonoControl
{
     
    public partial class WaitingDialog : Form
    {
       

        public WaitingDialog()
        {
            InitializeComponent();


            this.Show();

            
        }

        public void ShowWaitingDialog(string title, string caption)
        {
            lblCaption.Text = caption;
            lblTitle.Text = title;
          
          
        }


        public void ShowWaitingDialog()
        {
            lblCaption.Text = "Please Wait...";
            lblTitle.Text ="Data Is Loading";
            
          
           
        }


     

       
    }
}
