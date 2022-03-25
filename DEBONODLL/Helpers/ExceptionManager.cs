using System;
using System.IO;

namespace DebonoDLL
{
    /// <summary>
    /// Summary description for Class1
    /// </summary>
    public class ExceptionManager
    {
        public ExceptionManager()
        {
        }
        public static void LogException(string strMsg)
        {
            // Must remove before we give to customer
            if (System.Configuration.ConfigurationSettings.AppSettings["ShowErr"] != null)
            {
                //if (System.Configuration.ConfigurationSettings.AppSettings["ShowErr"].ToString().ToLower() == "true")
                //    System.Windows.Forms.MessageBox.Show(strMsg);
            }

            try
            {
                using (StreamWriter sw = new StreamWriter(AppDomain.CurrentDomain.BaseDirectory + @"\Exceptions.log", true))
                {
                    sw.WriteLine("---Exception occurred at :- " + DateTime.Now.ToString());
                    sw.WriteLine();
                    sw.WriteLine(strMsg);
                    sw.WriteLine();
                    sw.WriteLine("---End of Exception---");
                    sw.WriteLine();
                }
            }
            catch
            { }
        }







        public static void LogException(Exception ex)
        {
            // Must remove before we give to customer
            //System.Windows.Forms.MessageBox.Show(ex.Message);
            if (System.Configuration.ConfigurationSettings.AppSettings["ShowErr"] != null)
            {
                //if (System.Configuration.ConfigurationSettings.AppSettings["ShowErr"].ToString().ToLower() == "true")
                //    System.Windows.Forms.MessageBox.Show(ex.Message);
            }
            try
            {
                using (StreamWriter sw = new StreamWriter(AppDomain.CurrentDomain.BaseDirectory + @"\Exceptions.log", true))
                {
                    sw.WriteLine("---Exception occurred at :- " + DateTime.Now.ToString());
                    sw.WriteLine();
                    sw.WriteLine(ex.ToString());
                    sw.WriteLine();
                    sw.WriteLine("---End of Exception---");
                    sw.WriteLine();
                }
            }
            catch { }
        }


        public static void LogLoadingTime(String strMsg)
        {

            try
            {
                using (StreamWriter sw = new StreamWriter(AppDomain.CurrentDomain.BaseDirectory + @"\Load.log", true))
                {
                    sw.WriteLine("---Loading started at :- " + DateTime.Now.ToString("HH:mm:ss") + ":" + DateTime.Now.Millisecond.ToString() + "-------");
                    sw.WriteLine();
                    sw.WriteLine(strMsg);
                    sw.WriteLine();
                    sw.WriteLine("---End of Exception---");
                    sw.WriteLine();
                }
            }
            catch { }
        }

    }
}
