using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using DevExpress.XtraBars;
using DevExpress.Skins;
using System.Drawing;
using System.Reflection;
using System.ComponentModel;
using System.IO;
using System.Windows.Forms;
using DebonoDLL;
using DebonoDLL.App_Code.BOL;
namespace Debono
{
    public class App
    {
        Conversion objCon = new Conversion();
        // Application Settings
        // Company: General
        public static bool gnMultiUser = true;                       // Multi user installation ?
        public static bool gnUseDepartments = false;                 // Enable / disable department fields
        public static DateTime gnEarliestDate = DateTime.Parse("01/01/2000");
        public static int gnErrorLevel = 0;                          // What level of errors shoule be reported. Lower the no the more critical the error
        public static int gnQtyDP = 1;                               // Decimal palces for quantity fields
        public static int gnPriceDP = 2;                             // Decimal places for price related fields
       // public static string gnSrvLocalDir ="C:";
        //code commented by ajay
        public static string gnSrvLocalDir = Directory.GetDirectoryRoot(Environment.CurrentDirectory);                 // Local folder on DB server where temp backup files may be created
        //end commented code
        public static bool gnRoundAwayFromZero = true;               // Round decimal values to 2 place decimal away from zero; else do midpointrounding to even

        // Company: Auto
        public static bool auSeparateCreditNoteNos = false;          // Use separate numbers for sales credit notes ?
        public static string auFileNameTemplate = "[dno]-[dref]";    // Automatic file naming template
        public static int auFileNameZeros = 4;                       // Number of digits (for leading zeros) for a number used in file naming
        public static string auBackupTemplate = "[conm]-[timestamp]";  // Automatic file naming template for backup file

        // Company: Ageing
        public static int agSLAgeing = 1;                           // Automatic file naming template
        public static int agPLAgeing = 1;                           // Automatic file naming template
        public static int agPrd1 = 30;                              // Ageing period 1 days
        public static int agPrd2 = 60;                              // Ageing period 2 days
        public static int agPrd3 = 90;                              // Ageing period 3 days
        public static int agPrd4 = 120;                             // Ageing period 4 days
        public static int agAgeingDay = 28;                         // Account ageing day

        // Company: Currency & Tax
        public static int cuBaseCurrency = 1;
        public static string cuNonTaxCode = "T9";
        public static bool cuMultiCurrency = false;
        public static string cuTaxlabel = "VAT";
        public static bool cuEditFrgnCurrRate = true;
        public static bool cuVATRegistered = true;
        public static bool cuEditVAT = true;
        public static DateTime cuVATRegDate = gnEarliestDate;

        // Company: Printing
        public static bool prCoAddress = false;                      // Print company address on stationary
        public static bool prCoLogo = false;                         // Print company logo on stationary
        public static bool prEDIDocsAuto = false;                    // Print EDI documents automatically
        
        // Sales

        public static int slQuoteValidDays = 30;                     // Quote Valid For (Days)
        public static string slSONote1Label = "Wareouse";            // Sales Order Notes Label 1
        public static string slSONote2Label = "Delivery Note";       // Sales Order Notes Label 2
        public static string slSONote3Label =  "Office";             // Sales Order Notes Label 3
        public static string slInvNote1Label = "Warehouse";          // Sales Invoice Notes Label 1
        public static string slInvNote2Label = "Delivery Note";      // sBits[5] = Sales Invoice Notes Label 2
        public static string slInvNote3Label = "Office";             // sBits[6] = Sales Invoice Notes Label 3

        // Sales Calculate Button
        public static bool slCalcButtonUnitPr = false;               // Calculate unit price ?
        public static bool slCalcButtonNetPr = true;                 // Calculate net price ?
        public static string slCalcButtonCaption = "&Net Price";     // Button caption

        public static bool slPreviewGDNonInv = false;                // Preview GDN when Invoicing
        public static bool slGDN_AutoInv = true;                     // Automatically Create Sales Invoices when items are shipped (GDN created)
        public static bool slGroupInvItemsOnPosting = false;         // Group items when posing sales invoices - reduce no of transactions
        public static string slTaxCd = "T1";                         // Default tax code for sales
        public static string slCarriageTaxCd = "T1";                 // Default tax code for sales carriage
        
        // Purchase
        public static string plEnqXlsTemplate = Application.StartupPath.ToString() + "\\poe_template.xlsx";
        public static string plEnqDir = Application.StartupPath.ToString();                                  // Purch Enquiries folder
        public static string plPEITaxCd = "T1";                                                              // Purchase Enquiry Items Tax Code
        public static bool plEnableDisc = false;                                                             // PL Enable discounts
        public static string plPONote1Label = "1.";                  // Purchase Order Notes Label 1
        public static string plPONote2Label = "2.";                  // Purchase Order Notes Label 2
        public static string plPONote3Label = "3.";                  // Purchase Order Notes Label 3
        public static string plPINote1Label = "1.";                  // Purchase Invoice Notes Label 1
        public static string plPINote2Label = "2.";                  // Purchase Invoice Notes Label 2
        public static string plPINote3Label = "3.";                  // Purchase Invoice Notes Label 3
        public static string plPOPartRecvd = "Part Received";        // PO part received status text
        public static string plPOFullyRecvd = "Received";            // PO fully received status text
        public static string plPODelBtnEnable = "On-Order, Part Received";                                   // PO Statue values delimited list to enable Delivery Button
        public static string plGRNEnablePostingStatus = "";                                                  // GRN status when GRN can be posted
        public static string plGRNPostStatus = "";                                                           // GRN status to be set when GRN is posted
        public static string plDefPODelStkCd = "PURCHDEL";                                                   // Default PO Delivery Stock Code for when creating P Inv from GRN
        public static int plPymtDueDays = 30;                                                                // Default purchase payment due days
        public static string plTaxCd = "T1";                                                                 // Default puchase tax code
        public static string plCarriageTaxCd = "T1";                                                         // Default purchase carriage tax code
        public static bool plGRNSingleInv = true;                                                            // Generate a single purchase invoice per GRN irrespective of number of PO

        // Global Settings: Default Nominal Codes                              //////////////////////////////

        public static bool nlUsePeriods = false;
      //  public static DateTime nlPeriodStart = DateTime.Parse("01/01/" + System.DateTime.Now.Year.ToString());
       //// public static DateTime nlPeriodEnd = DateTime.Parse("31/12/" + System.DateTime.Now.Year.ToString());

        public static string nlDebtors = "1100";
        public static string nlCreditors = "2100";

        public static string nlSalesTax = "2200";
        public static string nlPurchaseTax = "2201";

        public static string nlSalesDiscount = "4009";
        public static string nlPurchaseDiscount = "5009";

        public static string nlBank = "1200";
        public static string ncdPettyCash = "1230";

        public static string nlSales = "4000";
        public static string ncdPurchase = "5000";

        public static string nlSalesCarriage = "4905";
        public static string nlPurchaseCarriage = "5100";
        public static string nlPEItemsNomCd = "5000";                // Purchase Enquiry Items Nominal Code

        public static string nlPrePayments = "1103";
        public static string nlAccruals = "2109";

        public static string nlRetaindEarnings = "3200";
        public static string nlCreditCharges = "4400";
        public static string nlBadDebt = "8100";
        public static string nlExchRateDifferences = "7906";

        public static string nlSuspense = "9998";
        public static string nlMispostings = "9999";

        public static string nlTillTaxCd = "T1";                     // Tax code for cash till sales
        public static string nlTillBank = "1200";
        public static string nlTillSales = "4000";
        public static string nlTillShortages = "9998";
        public static bool nlTillIncludesTax = true;

        // Stock
        public static bool stShowParam1 = false;                    // User definable stock parameters 1 - 6
        public static bool stShowParam2 = false;
        public static bool stShowParam3 = false;
        public static bool stShowParam4 = false;
        public static bool stShowParam5 = false;
        public static bool stShowParam6 = false;
        public static Int16 stParam1DP = 2;                         // User definable stock parameters 1 - 3, number of decimal places
        public static Int16 stParam2DP = 2;
        public static Int16 stParam3DP = 2;
        public static Int16 stNoOfPrices = 1;                       // No of stock prices, 1 - 4
        public static Int16 stNo_Depots = 1;                        // Number of depots
        public static bool stAllowNegativeStk = false;              // Allow negative stock figures
        public static bool stShowManuf = false;                     // Show Stock Manufacturer
        public static bool stShowSubCd = false;                     // Show Stock Subcode
        public static bool stTransfer121 = true;                    // Stock transfer between items has a 1 - 2 - 1 qty relationship
        public static bool stShowSaleHist = false;                  // Show customer item sale history in Stock List view ?
        public static string stCatLabel = "Category";               // Stock category label

        // Email                                                    ///////////////////////////////////////////////////////////////////////////////////////
        public static string emSMTPServer = "smtp.server.com";
        public static string emSMTPPort = "25";
        public static string emSecurity = "";
        public static bool emReqAuth = false;
        public static string emLoginName = "user_login";
        public static string emPassword = "";
        public static string emCC = "";
        public static string emBCC = "";
        public static string emFrom = "";                            // Who are the email messages from

        // User Settings                                             /////////////////////////////////////////////////////////////////////////////////////
        // Show SO Shipping preview screen, allows user to amend ship qty
        public static bool usShowSOShipPreview = false;
        public static bool usCloseSOShip = true;                     // Close SO form on Shipping ?
        public static bool usOpenFiles = false;                      // Automatically open print files ?
        public static int usStockListAutoRefresh = 0;                // How often is the stock list refreshesd automatically in milli-seconds. 0 = No autorefresh
        public static bool usRoaming = false;                        // Enable roaming ?
        public static bool usTFTD = true;                            // Display Thought for the Day ?
        public static bool usEscCloseTabs = true;                    // Close current tab on pressing Esc key ?
        public static bool usLogToFile = false;                      // Save logs to a text file ?
        public static bool usAskBeforeClosing = true;                // Ask user before closing app


        // Foreground & background colour schemes
        public static int usHighlightBG = Color.LightBlue.ToArgb();

        public static int usWarnBG = Color.Cyan.ToArgb();
        public static int usWarnFG = Color.Blue.ToArgb();
        public static int usMsgBG = Color.Transparent.ToArgb();
        public static int usMsgFG = Color.Black.ToArgb();
        public static int usUpdateBG = Color.Transparent.ToArgb();
        public static int usLogBG = Color.Transparent.ToArgb();
        public static int usLogFG = Color.Black.ToArgb();
        public static int usBeep = gv.ibeepNo;

        // Sales Ordere list view display colours
        public static int usSONoFG = Color.Black.ToArgb();
        public static int usSONoSelectedFG = Color.Black.ToArgb();
        public static int usSOPartDispatchFG = Color.Black.ToArgb();
        public static int usSOPartDispSelFG = Color.Black.ToArgb();
        public static int usSOFullDispatchFG = Color.Black.ToArgb();
        public static int usSOFullDispSelFG = Color.Black.ToArgb();

        // Sales Invoice List view background colours, normal & selected rows
        public static int usSInvFG = Color.Black.ToArgb();
        public static int usSInvSelFG = Color.Blue.ToArgb();
        public static int usSCrdFG = Color.Pink.ToArgb();
        public static int usSCrdSelFG = Color.Red.ToArgb();
        public static int usFrgnCurrBG = Color.Blue.ToArgb();

        public static string usAppSkin = "Caramel";                            // Application skin / colour scheme
        public static string usEmailFormat = "Text";
        public static string usListViewFont = "Calibri";                       // List view font
        public static int usListViewFontSize = 10;                             // List view font size

        // Workstation
        public static string wsSharedDir = "";
        public static bool wsTemplatesOnSrv = true;
        public static string wsTemplatesDir = "";
        public static string wsDBSrvSharedDir = "D:\\";
        public static bool wsGenerateGridLayouts = false;                        // Create default grid layout files automatically ?
    }

    public class Co
    {
        // Company Settings
        public static DateTime FinStartDate = DateTime.Parse("01/01/2014");    // Financial year start date
        public static int FinYear = 2014;                                      // Financial year start date - year
        public static int FinStartMonth = 1;                                   // Financial year start date - month

        public static string Nm = "CoName";                                    // Abbreviated company name, selected at startup
        public static string Name = "Company Name";
        public static string Adr1 = "Address Line 1";
        public static string Adr2 = "Address Line 2";
        public static string Adr3 = "Address Line 3";
        public static string Adr4 = "Address Line 4";
        public static string Adr5 = "Address Line 5";

        // Base Currency details - required globally. So, load them when the app starts
        public static int bcID = 1;
        public static string bcCode = "GBP";
        public static string bcSymbol = "£";
        public static string bcName = "Pound Sterling";
        public static string bcMajUnit = "pounds";
        public static string bcMinUnit = "pence";
        public static int bcInEMU = 0;

        public static decimal ValueDiscAmt_1 = 0;
        public static decimal ValueDiscAmt_2 = 0;
        public static decimal ValueDiscAmt_3 = 0;
        public static decimal ValueDiscAmt_4 = 0;
        public static decimal ValueDiscAmt_5 = 0;
        public static decimal ValueDiscAmt_6 = 0;
        public static decimal ValueDiscAmt_7 = 0;
        public static decimal ValueDiscAmt_8 = 0;
        public static decimal ValueDiscAmt_9 = 0;
        public static decimal ValueDiscAmt_10 = 0;
        public static decimal ValueDiscPerc_1 = 0;
        public static decimal ValueDiscPerc_2 = 0;
        public static decimal ValueDiscPerc_3 = 0;
        public static decimal ValueDiscPerc_4 = 0;
        public static decimal ValueDiscPerc_5 = 0;
        public static decimal ValueDiscPerc_6 = 0;
        public static decimal ValueDiscPerc_7 = 0;
        public static decimal ValueDiscPerc_8 = 0;
        public static decimal ValueDiscPerc_9 = 0;
        public static decimal ValueDiscPerc_10 = 0;
    }

    public class gv
    {
        // Constants for print report types
        public static Int16 prPrintID = 0;

        public const Int16 iPrID_CUL = 1;                                      // Customer List View print option
        public const Int16 iPrID_CU = 2;                                       // Customer Record print option
        public const Int16 iPrID_SOL = 3;                                      // SO List View
        public const Int16 iPrID_SO =  4;                                      // SO records
        public const Int16 iPrID_DNL = 5;                                      // Delivery Note List
        public const Int16 iPrID_DN = 6;                                       // Delivery Note
        public const Int16 iPrID_SHL = 7;                                      // Shipments List
        public const Int16 iPrID_SH =  8;                                      // Shipment
        public const Int16 iPrID_SIL = 9;                                      // Sales Invoice List
        public const Int16 iPrID_SI = 10;                                      // Sales Invoice
        public const Int16 iPrID_SUL = 11;                                     // Supplier List
        public const Int16 iPrID_SU = 12;                                      // Suppliers
        public const Int16 iPrID_PEL = 13;                                     // PO Enq List
        public const Int16 iPrID_PE = 14;                                      // PO Enq
        public const Int16 iPrID_POL = 15;                                     // PO List
        public const Int16 iPrID_PO = 16;                                      // PO
        public const Int16 iPrID_GRL = 17;                                     // GRN Lists
        public const Int16 iPrID_GRN = 18;                                     // GRN Lists
        public const Int16 iPrID_PIL = 19;                                     // Purchase Invoice List
        public const Int16 iPrID_PI = 20;                                      // PI
        public const Int16 iPrID_STL = 21;                                     // Stock Lists
        public const Int16 iPrID_ST = 22;                                      // Stock Record
        public const Int16 iPrID_CN = 23;                                      // Customer Notes
        public const Int16 iPrID_SN = 24;                                      // Suppliers Notes
        public const Int16 iPrID_PN = 25;                                      // Product (Stock) Notes

        // Beep Codes
        public const Int16 ibeepErr = 0;                                       // Sound beep on errors only
        public const Int16 ibeepErrWarn = 1;                                   // Sound beep on errors & warnings
        public const Int16 ibeepErrWarnMsg = 2;                                // Sound beep on errors, warnings and messages
        public const Int16 ibeepNo = 3;                                        // No beeping

        // Global Variables / Settings
        public static string AppName = "Accounts Master";                      // Variable used to define application name. To be set from some ini file etc..
        public static string AppDir = Application.StartupPath.ToString();      // Application Installation Dir
        public static string ClosePrintDialogue = "".PadLeft(60);              // Each character possition is used to indicate if a particular print option dialogue should be closed / not
        public static bool DevelopmentMode = false;
        public static bool bQuitProgram = false;                               // Are we quitting application prematurely ?
        public const string ErrorMailTo = "error.report@1t-s.com";
        public static string HelpID = "";
        public static string HelpURL = "";
        public static string PCName = "";
        public static bool ShowCloumnChooser = true;                           // Show column chooser option for list views
        public const string SuperUser = "super";
        public const string SUPassword = "access";
        public static string UserAccess;                                       // String with each characher representing user access levels to designated module / option,
        public static int UserID = 1;
        public static string UserLogin = "admin";
        public static bool AdminUser;                                          // true: if user is designated as the administrator or security is disabled
        public static string LastErrorNo = "";                                 // Last encountered number
        //public static bool CloseOnEsc = true;                                  // Enable close forms when the Esc key is pressed ? Also depends on user setting, App.usEscCloseTabs
        
        public static string[,] WinPos = new string[25, 9];                    // Array for storing windows size and possitions: No, name, max, x, y, height, width, default_height, default_width

        // Qty and Price display format strings
        public static string QtyDisplayFmtStr;
        public static string QtyMaskFmtStr;
        public static string PriceDisplayFmtStr;
        public static string PriceMaskFmtStr;
        public static DevExpress.Utils.FormatType QtyDisplayFmtTy;
        public static DevExpress.XtraEditors.Mask.MaskType QtyMaskFmtTy;
        public static DevExpress.Utils.FormatType PriceDisplayFmtTy;
        public static DevExpress.XtraEditors.Mask.MaskType PriceMaskFmtTy;
        public static MidpointRounding RoundingMode = MidpointRounding.AwayFromZero;

        // Email form related variables
        public static string emCustNotes = "";
        public static string emSuppNotes = "";
        public static string emStkNotes = "";

        // Document print related global variables
        // prPrintID - Integer ID associated with each print button. Used to set the Close Print Dialogue & Document options
        // Set gv.prPrintID to the unique integer value at the top of the print button click routine (see frmPrintDialogue.cs, frm_PrintDialog_Load):
        //  1: Customer List
        //  2: Customer Record
        //  3: Sales Orders List
        //  4: Sales Order
        //  5: Shipments / Delivery Notes List
        //  6:*Shipment  / Delivery Note
        //  9: Invoice List
        // 10: Invoice
        // 11: Supplier List
        // 12: Suppliers
        // 13: PE List
        // 14: Purchase Enq
        // 15: PO List
        // 16: Purchase Order
        // 17: Goods Received Lists
        // 18: Goods Received Note
        // 19: Purchase Inv List
        // 20: Purchase Invoice
        // 21: Stock List
        // 22: Stock Record
        // 23: Bill of Materials
        public static string DBver = "1.09.00";                                // Database version. 1st part: table structure changes, mid part: field structure changes; 3rd part config data changes
        public static string prAccountRef = "blank";
        public static string[] prRepList = new string[4];                      // Cater for up to 4 types (0 - 3) of reports
        public static string prRepType = "blank";
        public static string prDocType = "blank";                              // Selected Report type
        public static int prDocNo = 0;
        public static DateTime prDocDate = System.DateTime.Now;
        public static string prName = "blank";                                 // Customer / Supplier name / Stock item description

        // browse forms refresh after save / close flags
        public static bool slCustBrwRfsh = false;
        public static bool slInvBrwRfsh = false;
        public static bool slOrdrBrwRfsh = false;
        public static bool slShpMBrwRfsh = false;

        public static bool plSuppBrwRfsh = false;
        public static bool plInvBrwRfsh = false;
        public static bool plPEnqBrwRfsh = false;
        public static bool plOrdrBrwRfsh = false;
        public static bool plDlvyBrwRfsh = false;

        public static bool stStkRBrwRfsh = false;
        public static string StockCode = "";               // General purpose field to save stock code when a stock list row is clicked - alows other routines to access the stock code
    }

    public class Rtime
    {
        // Run Time Settings
    }
    public class Settings
    {
        public Settings()
        {
        }
        //private static int MultiUser;
        //private static bool UseDept = false;
        //private static string TemplateServerPath;
        //private static bool bShowCloumnChooser = false;
        //private static int u_id;
        //private static string u_nm;
        //private static string u_mod;
        //private static string s_co_style;
        //private static string s_co_nm;
        //private static string s_co_adr1;
        //private static string s_co_adr2;
        //private static string s_co_adr3;
        //private static string s_co_adr4;
        //private static string s_co_adr5;

        //private static decimal value_disc_amt_1;
        //private static decimal value_disc_amt_2;
        //private static decimal value_disc_amt_3;
        //private static decimal value_disc_amt_4;
        //private static decimal value_disc_amt_5;
        //private static decimal value_disc_amt_6;
        //private static decimal value_disc_amt_7;
        //private static decimal value_disc_amt_8;
        //private static decimal value_disc_amt_9;
        //private static decimal value_disc_amt_10;

        //private static decimal value_disc_perc_1;
        //private static decimal value_disc_perc_2;
        //private static decimal value_disc_perc_3;
        //private static decimal value_disc_perc_4;
        //private static decimal value_disc_perc_5;
        //private static decimal value_disc_perc_6;
        //private static decimal value_disc_perc_7;
        //private static decimal value_disc_perc_8;
        //private static decimal value_disc_perc_9;
        //private static decimal value_disc_perc_10;

        //private static int FinYear;
        //private static int FinStartMonth;
        //private static DateTime FinStartDate;

        //private static int till_bnk;
        //private static int till_sa_ncd;
        //private static int till_taxcd;
        //private static int till_short;
        //private static int f_till_inctax;

        private static int ncd_endpmt;
        //private static int ncdbad_debts;
        private static int ncdcrdchrg;
        //private static int ncdcrdors;
        //private static int ncddebtors;
        //private static int ncddef_bnk;
        //private static int ncddef_sales;
        //private static int ncddiscnt_buy;
        //private static int ncddiscnt_sales;
        //private static int ncdmisposts;
        private static int ncdnlpp;
        private static int ncdkept_wages;
        private static int ncd_temp;
        //private static int ncdtax_buy;
        //private static int ncdtax_sales;
        //private static int ncdexchrate;

        private static int vat_scheme;

        private static int iddeliveryaddr;

        //private static Int16 ShowStkParam1;                  // User definable stock parameters 1 - 6
        //private static Int16 ShowStkParam2;
        //private static Int16 ShowStkParam3;
        //private static Int16 ShowStkParam4;
        //private static Int16 ShowStkParam5;
        //private static Int16 ShowStkParam6;
        //private static Int16 StkParam1DP;                     // User definable stock parameters 1 - 3, number of decimal places
        //private static Int16 StkParam2DP;
        //private static Int16 StkParam3DP;
        //private static Int16 NoOfStkPrices;                 // No of stock prices, 1 - 4
        //private static Int16 AllowNegativeStk;
        //private static Int16 ShowStkManuf;
        //private static Int16 ShowStkSubCd;
        //private static Int16 No_Depots;                    // Number of depots
        //private static bool bStkTransfer121 = true;        // Stock transfer between items has a 1 - 2 - 1 qty relationship

        //private static string warningBG;
        //private static string warningFG;

        //private static string messageBG;
        //private static string messageFG;

        //private static string updateBG;
        //private static string LogBG;

        // {John Porter}
        //private static string SharedFldr;
        //private static bool roam;

        //private static int decplaceqty;
        //private static int decplacemoney;

        //private static DevExpress.Utils.FormatType qtydisplayfmtty;
        //private static string qtydisplayfmtstr;

        //private static DevExpress.Utils.FormatType pricedisplayfmtty;
        //private static string pricedisplayfmtstr;

        //private static DevExpress.XtraEditors.Mask.MaskType qtymaskfmtty;
        //private static string qtymaskfmtstr;

        //private static DevExpress.XtraEditors.Mask.MaskType pricemaskfmtty;
        //private static string pricemaskfmtstr;

        //private static string listviewfont;
        //private static int listviewfontsize;

        //private static string sonodisp;                    // SOP list view - no items dispatched BG
        //private static string sondsel;                     // SOP list view - no items dispatched BG - selected items
        //private static string sopartdisp;                  // SOP list view - part dispatched BG
        //private static string sopdsel;                     // SOP list view - part dispatched BG - selected
        //private static string sofulldisp;                  // SOP list view - fully dispatched BG
        //private static string _sofdsel;                    // SOP list view - fully dispatched BG - selected
        //private static string sinv;                        // Sales Invoice list view BG 
        //private static string sinvsel;                     // Sales Invoice BG - selected
        //private static string scrd;                        // Sales Credit BG
        //private static string scrdsel;                     // Sales Credit BG - selected
        //private static string fcurr;                         // Foreign currency selected BG
        //private static bool mcurr;                           // Multi-currency enabled

        //private static bool bDevMode;                      // Is the system bein run on a development platform ?

        // System Configurations

        // Purchase
        //private static string cfg_poetmpl;
        //public static string POETemplate
        //{
        //    get { return cfg_poetmpl; }
        //    set { cfg_poetmpl = value; }
        //}
        //private static string cfg_poefldr;
        //public static string POEFolder
        //{
        //    get { return cfg_poefldr; }
        //    set { cfg_poefldr = value; }
        //}

        //private static int cfg_grn_enablepost;
        //public static int GRNEnablePost
        //{
        //    get { return cfg_grn_enablepost; }
        //    set { cfg_grn_enablepost = value; }
        //}
        //private static int cfg_grn_poststatus;
        //public static int GRNPostStatus
        //{
        //    get { return cfg_grn_poststatus; }
        //    set { cfg_grn_poststatus = value; }
        //}

        //private static bool bShowSOShPvue;                 // Show ship preview screen on shipping
        //private static bool bClSOShip;                     // Close SO screen after shipping


        //private static string sClPrnDlg;                   // Close Print Dialogue After Printing ? see frm_SettingsDlg_Load() in frmSettingsDlg.cs
        private static string sfTTip;                      // Global variable for ToolTip for setting Midi Form tabs tooltip
        //private static bool bAskBeforeClosing;             // Get user confirmation before quitting program ?

        //private static bool gdnautoinvoice;                // GDN Auto Invoice
        //private static bool GrpInvItms;                    // Post invoice items in groups, based on Nominal Codes

        // {john porter}

        //private static int beepSound;

        //private static string setup_style;

        //private static Color setup_style_highlighted;

        private static DataTable master_taxcd;
        private static DataTable master_nl_actype;
        private static DataTable dtdepots;

        //private static int nontaxcd;
        //private static int basecurr;

        private static int autostkallocation;

        //private static string helpid;
        //public static string HelpID
        //{
        //    get { return helpid; }
        //    set { helpid = value; }
        //}
        //private static string helpurl;
        //public static string HelpURL
        //{
        //    get { return helpurl; }
        //    set { helpurl = value; }
        //}

        // {John Porter}
        //public static string sharedfolder
        //{
        //    get { return SharedFldr; }
        //    set { SharedFldr = value; }
        //}
        //public static bool roaming
        //{
        //    get { return roam; }
        //    set { roam = value; }
        //}

        //public static string bg_sonodisp
        //// SOP list view - no items dispatched BG
        //{
        //    get { return sonodisp; }
        //    set { sonodisp = value; }
        //}

        //private static string bg_sonodispsel
        //// SOP list view - no items dispatched BG - selected items
        //{
        //    get { return sondsel; }
        //    set { sondsel = value; }
        //}

        //public static string bg_sopartdisp
        //// SOP list view - part dispatched BG
        //{
        //    get { return sopartdisp; }
        //    set { sopartdisp = value; }
        //}

        //public static string bg_sopdsel
        //// SOP list view - part dispatched BG - selected
        //{
        //    get { return sopdsel; }
        //    set { sopdsel = value; }
        //}

        //public static string bg_sofulldisp
        //// SOP list view - fully dispatched BG
        //{
        //    get { return sofulldisp; }
        //    set { sofulldisp = value; }
        //}

        //public static string sofdsel
        //// SOP list view - fully dispatched BG - selected
        //{
        //    get { return sofulldisp; }
        //    set { sofulldisp = value; }
        //}

        //public static string bg_sinv
        //// Sales Invoice list view BG
        //{
        //    get { return sinv; }
        //    set { sinv = value; }
        //}

        //public static string bg_sinvsel
        //// Sales Invoice BG - selected
        //{
        //    get { return sinvsel; }
        //    set { sinvsel = value; }
        //}

        //public static string bg_scrd
        //// Sales Credit BG
        //{
        //    get { return scrd; }
        //    set { scrd = value; }
        //}

        //public static string bg_scrdsel
        //// Sales Credit BG - selected
        //{
        //    get { return scrdsel; }
        //    set { scrdsel = value; }
        //}

        //public static string bg_fcurr
        //// Foreign currency selected BG
        //{
        //    get { return fcurr; }
        //    set { fcurr = value; }
        //}

        public static string sfToolTip
        // Midi Forms tabs tool tip text
        {
            get { return sfTTip; }
            set { sfTTip = value; }
        }

        //public static string sClosePrintDialogue
        //// Close Print Dialogue After Printing ?
        //{
        //    get { return sClPrnDlg; }
        //    set { sClPrnDlg = value; }
        //}

        //// Bob Kapur Integer ID associated with each print button. Used to set the Close Print Dialogue & Document options
        //// e.g. 1 - Customer list view, 2 - Customer record, 3 - SO List View, 4 - SO Screen, see frm_PrintDialog_Load()
        //private static Int16 iPrnID;

        //public static Int16 iPrintID
        //// Integer ID associated with each print Button
        //{
        //    get { return iPrnID; }
        //    set { iPrnID = value; }
        //}


        //// Printing to file. Parameters that may be used in creating filenames automatically

        //// Print file name template
        //private static string PrintFlNmTmpl;

        //public static string PrintFileNameTmpl
        //// Template for creating file names automatically
        //{
        //    get { return PrintFlNmTmpl; }
        //    set { PrintFlNmTmpl = value; }
        //}

        //private static int PrintFlNmDigits;
        //public static int PrintFileNameDigits
        //// number of digits with leading zeros for the document number
        //{
        //    get { return PrintFlNmDigits; }
        //    set { PrintFlNmDigits = value; }
        //}

        // Account reference e.g. sl.accref, pl.accref, stk.stkcd
        //private static string PrintAcRef;
        //public static string PrintAccRef
        //// Integer ID associated with each print Button
        //{
        //    get { return PrintAcRef; }
        //    set { PrintAcRef = value; }
        //}

        //// Document type e.g. so, sq, si, po, gr, pi, st
        //private static string PrintDocTy;
        //public static string PrintDocType
        //// Integer ID associated with each print Button
        //{
        //    get { return PrintDocTy; }
        //    set { PrintDocTy = value; }
        //}

        //// Document number e.g. Invoice number, PO number etc..
        //private static int PrintDNo;
        //public static int PrintDocNo
        //{
        //    get { return PrintDNo; }
        //    set { PrintDNo = value; }
        //}

        //// Document date e.g. PO or Invoice Date
        //private static DateTime PrintDDt;
        //public static DateTime PrintDocDate
        //{
        //    get { return PrintDDt; }
        //    set { PrintDDt = value; }
        //}



        //public static bool ShowSOShipPreview
        //// Show SO Ship Preview screen on shipping ?
        //{
        //    get { return bShowSOShPvue; }
        //    set { bShowSOShPvue = value; }
        //}

        //public static bool CloseSOShip
        //// Show SO Ship Preview screen on shipping ?
        //{
        //    get { return bClSOShip; }
        //    set { bClSOShip = value; }
        //}

        // {john porter}

        //public static bool bDevelopmentMode
        //// Is the system being run on a development platform ? i.e. NOT on end user / client system
        //{
        //    get { return bDevMode; }
        //    set { bDevMode = value; }
        //}

        //public static int setup_auto_allocation
        //{
        //    get { return autostkallocation; }
        //    set { autostkallocation = value; }
        //}

        public static int setup_delivery_addr
        {
            get { return iddeliveryaddr; }
            set { iddeliveryaddr = value; }
        }

        // System configurations, Nominal tab
        //private static bool ncd_UsePeriods;
        //public static bool setup_ncd_UsePeriods
        //{
        //    get { return ncd_UsePeriods; }
        //    set { ncd_UsePeriods = value; }
        //}

        //private static DateTime ncd_PeriodStart;
        //public static DateTime setup_ncd_PeriodStart
        //{
        //    get { return ncd_PeriodStart; }
        //    set { ncd_PeriodStart = value; }
        //}

        //private static DateTime ncd_PeriodEnd;
        //public static DateTime setup_ncd_PeriodEnd
        //{
        //    get { return ncd_PeriodEnd; }
        //    set { ncd_PeriodEnd = value; }
        //}

        public static int setup_ncd_endpmt
        {
            get { return ncd_endpmt; }
            set { ncd_endpmt = value; }
        }
        //public static int setup_ncdbad_debts
        //{
        //    get { return ncdbad_debts; }
        //    set { ncdbad_debts = value; }
        //}
        //public static int setup_ncdcrdchrg
        //{
        //    get { return ncdcrdchrg; }
        //    set { ncdcrdchrg = value; }
        //}
        //public static int setup_ncdcrdors
        //{
        //    get { return ncdcrdors; }
        //    set { ncdcrdors = value; }
        //}
        //public static int setup_ncddebtors
        //{
        //    get { return ncddebtors; }
        //    set { ncddebtors = value; }
        //}
        //public static int setup_ncddef_bnk
        //{
        //    get { return ncddef_bnk; }
        //    set { ncddef_bnk = value; }
        //}
        //public static int setup_ncddef_sales
        //{
        //    get { return ncddef_sales; }
        //    set { ncddef_sales = value; }
        //}
        //public static int setup_ncddiscnt_buy
        //{
        //    get { return ncddiscnt_buy; }
        //    set { ncddiscnt_buy = value; }
        //}
        //public static int setup_ncddiscnt_sales
        //{
        //    get { return ncddiscnt_sales; }
        //    set { ncddiscnt_sales = value; }
        //}
        //public static int setup_ncdmisposts
        //{
        //    get { return ncdmisposts; }
        //    set { ncdmisposts = value; }
        //}
        public static int setup_ncdnlpp
        {
            get { return ncdnlpp; }
            set { ncdnlpp = value; }
        }
        public static int setup_ncdkept_wages
        {
            get { return ncdkept_wages; }
            set { ncdkept_wages = value; }
        }
        public static int setup_ncd_temp
        {
            get { return ncd_temp; }
            set { ncd_temp = value; }
        }
        //public static int setup_ncdtax_buy
        //{
        //    get { return ncdtax_buy; }
        //    set { ncdtax_buy = value; }
        //}
        //public static int setup_ncdtax_sales
        //{
        //    get { return ncdtax_sales; }
        //    set { ncdtax_sales = value; }
        //}
        //public static int setup_ncdexchrate
        //{
        //    get { return ncdexchrate; }
        //    set { ncdexchrate = value; }
        //}

        //public static int setup_till_bnk
        //{
        //    get { return till_bnk; }
        //    set { till_bnk = value; }
        //}

        //public static int setup_till_sa_ncd
        //{
        //    get { return till_sa_ncd; }
        //    set { till_sa_ncd = value; }
        //}

        //public static int setup_till_taxcd
        //{
        //    get { return till_taxcd; }
        //    set { till_taxcd = value; }
        //}

        //public static int setup_till_short
        //{
        //    get { return till_short; }
        //    set { till_short = value; }
        //}

        //public static int setup_f_till_inctax
        //{
        //    get { return f_till_inctax; }
        //    set { f_till_inctax = value; }
        //}

        public static DataTable setup_master_taxcd
        {
            // Data table with a list of all tax codes
            get { return master_taxcd; }
            set { master_taxcd = value; }
        }

        public static DataTable setup_master_nl_actype
        {
            get { return master_nl_actype; }
            set { master_nl_actype = value; }
        }

        //public static int setup_multiuser
        //{
        //    get { return MultiUser; }
        //    set { MultiUser = value; }
        //}

        //public static bool setup_MultiCurr
        //// Multi Currency Enabled
        //{
        //    get { return mcurr; }
        //    set { mcurr = value; }
        //}

        //public static bool setup_UseDepartments
        //{
        //    get { return UseDept; }
        //    set { UseDept = value; }
        //}

        //public static string setup_templateserverpath
        //{
        //    get { return TemplateServerPath; }
        //    set { TemplateServerPath = value; }
        //}

        private static DataTable u_access_data;
        public static DataTable setup_u_access_data
        {
            get { return u_access_data; }
            set { u_access_data = value; }
        }

        //public static Color setup_hgcolor
        //{
        //    get { return setup_style_highlighted; }
        //    set { setup_style_highlighted = value; }
        //}

        //public static string setup_u_nm
        //{
        //    get { return u_nm; }
        //    set { u_nm = value; }
        //}

        //private static string wstnid;                                          // Work Station ID
        //public static string setup_WrkStnId
        //{
        //    get { return wstnid; }
        //    set { wstnid = value; }
        //}

        //public static string setup_u_mod
        //{
        //    get { return u_mod; }
        //    set { u_mod = value; }
        //}

        //public static int setup_u_id
        //{
        //    get { return u_id; }
        //    set { u_id = value; }
        //}

        //public static DateTime setup_FinStartDate
        //{
        //    get { return FinStartDate; }
        //    set { FinStartDate = value; }
        //}

        //public static int setup_FinYear
        //{
        //    get { return FinYear; }
        //    set { FinYear = value; }
        //}

        //public static int setup_FinStartMonth
        //{
        //    get { return FinStartMonth; }
        //    set { FinStartMonth = value; }
        //}

        //public static bool ShowCloumnChooser
        //{
        //    get { return bShowCloumnChooser; }
        //    set { bShowCloumnChooser = value; }
        //}

        private static BarStaticItem bsi_statusrcnt;

        public static BarStaticItem statusrcnt
        {
            get { return bsi_statusrcnt; }
            set { bsi_statusrcnt = value; }
        }

        //public static string setup_co_style
        //{
        //    get { return s_co_style; }
        //    set { s_co_style = value; }
        //}

        //public static int setup_Beep
        //{
        //    get { return beepSound; }
        //    set { beepSound = value; }
        //}

        //public static string setup_co_name
        //{
        //    get { return s_co_nm; }
        //    set { s_co_nm = value; }
        //}
        //public static string setup_co_adr1
        //{
        //    get { return s_co_adr1; }
        //    set { s_co_adr1 = value; }
        //}
        //public static string setup_co_adr2
        //{
        //    get { return s_co_adr2; }
        //    set { s_co_adr2 = value; }
        //}
        //public static string setup_co_adr3
        //{
        //    get { return s_co_adr3; }
        //    set { s_co_adr3 = value; }
        //}
        //public static string setup_co_adr4
        //{
        //    get { return s_co_adr4; }
        //    set { s_co_adr4 = value; }
        //}
        //public static string setup_co_adr5
        //{
        //    get { return s_co_adr5; }
        //    set { s_co_adr5 = value; }
        //}

        //public static decimal setup_value_disc_amt_1
        //{
        //    get { return value_disc_amt_1; }
        //    set { value_disc_amt_1 = value; }
        //}

        //public static decimal setup_value_disc_amt_2
        //{
        //    get { return value_disc_amt_2; }
        //    set { value_disc_amt_2 = value; }
        //}

        //public static decimal setup_value_disc_amt_3
        //{
        //    get { return value_disc_amt_3; }
        //    set { value_disc_amt_3 = value; }
        //}

        //public static decimal setup_value_disc_amt_4
        //{
        //    get { return value_disc_amt_4; }
        //    set { value_disc_amt_4 = value; }
        //}

        //public static decimal setup_value_disc_amt_5
        //{
        //    get { return value_disc_amt_5; }
        //    set { value_disc_amt_5 = value; }
        //}

        //public static decimal setup_value_disc_amt_6
        //{
        //    get { return value_disc_amt_6; }
        //    set { value_disc_amt_6 = value; }
        //}

        //public static decimal setup_value_disc_amt_7
        //{
        //    get { return value_disc_amt_7; }
        //    set { value_disc_amt_7 = value; }
        //}

        //public static decimal setup_value_disc_amt_8
        //{
        //    get { return value_disc_amt_8; }
        //    set { value_disc_amt_8 = value; }
        //}

        //public static decimal setup_value_disc_amt_9
        //{
        //    get { return value_disc_amt_9; }
        //    set { value_disc_amt_9 = value; }
        //}

        //public static decimal setup_value_disc_amt_10
        //{
        //    get { return value_disc_amt_10; }
        //    set { value_disc_amt_10 = value; }
        //}

        //public static decimal setup_value_disc_perc_1
        //{
        //    get { return value_disc_perc_1; }
        //    set { value_disc_perc_1 = value; }
        //}

        //public static decimal setup_value_disc_perc_2
        //{
        //    get { return value_disc_perc_2; }
        //    set { value_disc_perc_2 = value; }
        //}

        //public static decimal setup_value_disc_perc_3
        //{
        //    get { return value_disc_perc_3; }
        //    set { value_disc_perc_3 = value; }
        //}

        //public static decimal setup_value_disc_perc_4
        //{
        //    get { return value_disc_perc_4; }
        //    set { value_disc_perc_4 = value; }
        //}

        //public static decimal setup_value_disc_perc_5
        //{
        //    get { return value_disc_perc_5; }
        //    set { value_disc_perc_5 = value; }
        //}

        //public static decimal setup_value_disc_perc_6
        //{
        //    get { return value_disc_perc_6; }
        //    set { value_disc_perc_6 = value; }
        //}

        //public static decimal setup_value_disc_perc_7
        //{
        //    get { return value_disc_perc_7; }
        //    set { value_disc_perc_7 = value; }
        //}

        //public static decimal setup_value_disc_perc_8
        //{
        //    get { return value_disc_perc_8; }
        //    set { value_disc_perc_8 = value; }
        //}

        //public static decimal setup_value_disc_perc_9
        //{
        //    get { return value_disc_perc_9; }
        //    set { value_disc_perc_9 = value; }
        //}

        //public static decimal setup_value_disc_perc_10
        //{
        //    get { return value_disc_perc_10; }
        //    set { value_disc_perc_10 = value; }
        //}

        //public static Int16 setup_NoOfSalesPrices
        //{
        //    get { return NoOfStkPrices; }
        //    set { NoOfStkPrices = value; }
        //}

        //public static Int16 setup_showstkparam1
        //{
        //    get { return ShowStkParam1; }
        //    set { ShowStkParam1 = value; }
        //}

        //public static Int16 setup_showstkparam2
        //{
        //    get { return ShowStkParam2; }
        //    set { ShowStkParam2 = value; }
        //}

        //public static Int16 setup_showstkparam3
        //{
        //    get { return ShowStkParam3; }
        //    set { ShowStkParam3 = value; }
        //}

        //public static Int16 setup_showstkparam4
        //{
        //    get { return ShowStkParam4; }
        //    set { ShowStkParam4 = value; }
        //}

        //public static Int16 setup_showstkparam5
        //{
        //    get { return ShowStkParam5; }
        //    set { ShowStkParam5 = value; }
        //}

        //public static Int16 setup_showstkparam6
        //{
        //    get { return ShowStkParam6; }
        //    set { ShowStkParam6 = value; }
        //}

        //public static Int16 setup_no_depots
        //{
        //    get { return No_Depots; }
        //    set { No_Depots = value; }
        //}

        //public static bool setup_stktransfer121
        //{
        //    get { return bStkTransfer121; }
        //    set { bStkTransfer121 = value; }
        //}

        //public static string setup_warningBG
        //{
        //    get { return warningBG; }
        //    set { warningBG = value; }
        //}

        //public static string setup_warningFG
        //{
        //    get { return warningFG; }
        //    set { warningFG = value; }
        //}

        //public static string setup_messageBG
        //{
        //    get { return messageBG; }
        //    set { messageBG = value; }
        //}

        //public static string setup_messageFG
        //{
        //    get { return messageFG; }
        //    set { messageFG = value; }
        //}

        //public static string setup_updateBG
        //{
        //    get { return updateBG; }
        //    set { updateBG = value; }
        //}

        //public static string setup_LogBG
        //{
        //    get { return LogBG; }
        //    set { LogBG = value; }
        //}

        //public static Int16 setup_showstkmanuf
        //{
        //    get { return ShowStkManuf; }
        //    set { ShowStkManuf = value; }
        //}

        //public static Int16 setup_showstksubcd
        //{
        //    get { return ShowStkSubCd; }
        //    set { ShowStkSubCd = value; }
        //}

        //public static int setup_basecurr
        //{
        //    get { return basecurr; }
        //    set { basecurr = value; }
        //}

        //public static int setup_nontaxcd
        //{
        //    get { return nontaxcd; }
        //    set { nontaxcd = value; }
        //}

        public static int setup_vat_scheme
        {
            get { return vat_scheme; }
            set { vat_scheme = value; }
        }

       

        public static string LoadParam(string keName, string Opts)
        {
            string usr = "";
            string wrkstn = "";
            bool findparam = false;
            string val = null;

            try
            {
                // 09/11/2013 Bob Kapur Now returns a blank string and NOT null, if value is not found
                if (Opts.Contains("U"))
                {
                    //usr = gv.UserID.ToString();
                    //AcctDO.SetupParam setp = new AcctDO.SetupParam();
                    //setp.SetConn();
                    //setp.i_ty = 2;
                    //setp.s_key = keName;
                    //setp.s_ref = usr;
                    //val = setp.IsExistParam();
                    //if (val != null)
                    //{
                    //    findparam = true;
                    //}
                }
                if (Opts.Contains("W"))
                {
                    wrkstn = gv.PCName;
                    if (!findparam)
                    {
                        //AcctDO.SetupParam setp1 = new AcctDO.SetupParam();
                        //setp1.SetConn();
                        //setp1.i_ty = 1;
                        //setp1.s_key = keName;
                        //setp1.s_ref = wrkstn;
                        //val = setp1.IsExistParam();
                        //if (val != null)
                        //{
                        //    findparam = true;
                        //}
                    }
                }

                if (Opts.Contains("G"))
                {
                    if (!findparam)
                    {
                        //AcctDO.SetupParam setp2 = new AcctDO.SetupParam();
                        //setp2.SetConn();
                        //setp2.i_ty = 0;
                        //setp2.s_key = keName;
                        //setp2.s_ref = "";
                        //val = setp2.IsExistParam();
                        //if (val != null)
                        //{
                        //    findparam = true;
                        //}
                    }
                }
            }
            catch (Exception ex)
            {
               // AcctMsg.MsgExceptionErr(ex, "00401", 1, "Load settings [" + keName + " / " + Opts + "]");
                ExceptionManager.LogException(ex);
            }

            return (val == null ? "" : val);
        }

       
        public static void LoadSetupParam()
        {
             Conversion objCon = new Conversion();
            Cursor oCursor = Cursor.Current;
            //CommonFunction.WaitCursor();

            string sParam = "";
            string[] sBits;
            string sSection = "Load system settings";
            string sAppPath = Application.StartupPath.ToString() + "\\";

            try
            {
                // Configurations: Company, General //
                sSection = "Load company general settings";
                sParam = LoadParam("CoGenConfig", "G");
                if (sParam != "")
                {
                    sBits = sParam.Split(';');
                    App.gnMultiUser = (sBits[0] == "Y");
                    App.gnUseDepartments = (sBits[1] == "Y");
                    //Functions.EarliestDate = sBits[2] == "" ? DateTime.Parse("01/01/2000") : DateTime.Parse(sBits[2]);
                    App.gnErrorLevel = int.Parse(sBits[3]);
                    App.gnSrvLocalDir = sBits[4];
                    App.gnQtyDP = objCon.ConToInt(sBits[5]);
                    App.gnPriceDP = objCon.ConToInt(sBits[6]);
                }

                // define the display and mask formats
                gv.QtyDisplayFmtTy = DevExpress.Utils.FormatType.Numeric;
                gv.QtyDisplayFmtStr = "n" + App.gnQtyDP;

                gv.QtyMaskFmtTy = DevExpress.XtraEditors.Mask.MaskType.Numeric;
                gv.QtyMaskFmtStr = "f" + App.gnQtyDP;

                gv.PriceDisplayFmtTy = DevExpress.Utils.FormatType.Numeric;
                gv.PriceDisplayFmtStr = "n" + App.gnPriceDP;

                gv.PriceMaskFmtTy = DevExpress.XtraEditors.Mask.MaskType.Numeric;
                gv.PriceMaskFmtStr = "f" + App.gnPriceDP;

                // Configurations: Company, Auto               ////////////////////////////////////////
                sSection = "Load company auto settings";
                sParam = LoadParam("CoAutoConfig", "G");         // Auto file naming convension template see configurations, auto & clsGeneralFinctions.GetFileName()
                if (sParam != "")
                {
                    sParam += ";;;;";
                    sBits = sParam.Split(';');
                    App.auSeparateCreditNoteNos = (sBits[0] == "Y");
                    App.auFileNameTemplate = sBits[1];
                    int i = "0123456789".IndexOf(sBits[2]);
                    App.auFileNameZeros = ((i > 0 && i < 10) ? i : 4);       // Default to 4 digits
                    App.auBackupTemplate = sBits[3];
                }

                // Configurations: Company, Ageing             ////////////////////////////////////////
                sSection = "Load company ageing settings";
                sParam = LoadParam("CoAgeConfig", "G");
                if (sParam != "")
                {
                    sBits = sParam.Split(';');
                    App.agSLAgeing = int.Parse(sBits[0]);
                    App.agPLAgeing = int.Parse(sBits[1]);
                    App.agPrd1 = int.Parse(sBits[2]);
                    App.agPrd2 = int.Parse(sBits[3]);
                    App.agPrd3 = int.Parse(sBits[4]);
                    App.agPrd4 = int.Parse(sBits[5]);
                    App.agAgeingDay = int.Parse(sBits[6]);
                }

                // Configurations: Company, Currency           ////////////////////////////////////////
                sSection = "Load company currency settings";
                sParam = LoadParam("CoCurrConfig", "G");
                if (sParam != "")
                {
                    sBits = sParam.Split(';');
                    App.cuBaseCurrency = int.Parse(sBits[0]);
                    App.cuTaxlabel = (sBits[1] == "" ? "VAT" : sBits[1]);
                    App.cuNonTaxCode = sBits[2];
                    App.cuVATRegistered = (sBits[3] == "Y");
                    App.cuVATRegDate = DateTime.Parse(sBits[4]);
                    App.cuEditVAT = (sBits[5] == "Y");
                    App.cuMultiCurrency = (sBits[6] == "Y");
                    App.cuEditFrgnCurrRate = (sBits[7] == "Y");
                }

                // Configurations: Company, Printing           ////////////////////////////////////////
                sSection = "Load company printing settings";
                sParam = LoadParam("CoPrConfig", "G");
                if (sParam != "")
                {
                    sBits = sParam.Split(';');
                    App.prCoAddress = (sBits[0] == "Y");
                    App.prCoLogo = (sBits[1] == "Y");
                    App.prEDIDocsAuto = (sBits[2] == "Y");
                }

                // Configurations: Company Intrastat           ////////////////////////////////////////

                // Configurations: Sales                       ////////////////////////////////////////
                sSection = "Load sales section settings";
                sParam = LoadParam("SLConfig", "G");        // +"30;Picking List;Delivery Note;Office;Warehouse;Delivery Note;Office;N;Uni&t Price;N;Y;Y;;;;";
                if (sParam != "")
                {
                    sBits = sParam.Split(';');
                    App.slQuoteValidDays = (sBits[0] == "" ? 30 : int.Parse(sBits[0]));          // Quote Valid For (Days)
                    App.slSONote1Label = (sBits[1] == "" ? "Note" : sBits[1]);                    // Sales Order Notes Label 1
                    App.slSONote2Label = sBits[2];                                               // Sales Order Notes Label 2
                    App.slSONote3Label = sBits[3];                                               // Sales Order Notes Label 3
                    App.slInvNote1Label = (sBits[4] == "" ? "Note" : sBits[4]);                  // Sales Invoice Notes Label 1
                    App.slInvNote1Label = sBits[5];                                              // Sales Invoice Notes Label 2
                    App.slInvNote1Label = sBits[6];                                              // Sales Invoice Notes Label 3

                    // Sales Calculator Button
                    App.slCalcButtonUnitPr = (sBits[7] == "Y");                                  // Calculate unit price
                    App.slCalcButtonNetPr = (sBits[8] == "Y");                                   // Calculate net price
                    App.slCalcButtonCaption = (sBits[9] == "" ? "&Net Price" : sBits[9]);        // caption

                    App.slPreviewGDNonInv = (sBits[10] == "Y");                                  // Preview GDN when generating sales invoices
                    App.slGDN_AutoInv = (sBits[11] == "Y");                                      // Automatically create sales invoices when goods are shipped (GDN created)
                    App.slGroupInvItemsOnPosting = (sBits[12] == "Y");                           // Post Invoice items in Groups, based on their Nominal Codes

                    App.slTaxCd = (sBits[13] == "" ? "T1" : sBits[13]);
                    App.slCarriageTaxCd = (sBits[14] == "" ? "T1" : sBits[14]);
                }

                // Configurations: Purchase                    ////////////////////////////////////////
                sSection = "Load purchase section settings";
                sParam = LoadParam("PLConfig", "G");                                         // User definable PL parameters
                if (sParam != "")
                {
                    sBits = (sParam + ";;;;;;;;;;;;;;;;;;").Split(';');
                    if (sBits[0] != "") App.plEnqXlsTemplate = sBits[0];
                    if (sBits[1] != "") App.plEnqDir = sBits[1];
                    if (sBits[2] != "") App.plPEITaxCd = sBits[2];
                    App.plEnableDisc = (sBits[3] == "Y");
                    App.plPONote1Label = sBits[4];
                    App.plPONote2Label = sBits[5];
                    App.plPONote3Label = sBits[6];
                    App.plPINote1Label = sBits[7];
                    App.plPINote2Label = sBits[8];
                    App.plPINote3Label = sBits[9];
                    //if (sBits[4] != "") App.plGRNEnablePostingStatus = int.Parse(sBits[10]);
                    //if (sBits[5] != "") App.plGRNPostStatus = int.Parse(sBits[11]);
                    if (sBits[10] != "") App.plPOPartRecvd = sBits[10];
                    if (sBits[11] != "") App.plPOFullyRecvd = sBits[11];
                    App.plPODelBtnEnable = sBits[12];
                    if (sBits[13] != "") App.plGRNEnablePostingStatus = sBits[13];
                    if (sBits[14] != "") App.plGRNPostStatus = sBits[14];
                    if (sBits[15] != "") App.plDefPODelStkCd = sBits[15];
                    if (sBits[16] != "") App.plTaxCd = sBits[16];
                    if (sBits[17] != "") App.plCarriageTaxCd = sBits[17];
                    App.plGRNSingleInv = (sBits[18] == "Y");
                }

                // Configurations: Nominal                     ////////////////////////////////////////
                //    sParam = "N;01/01/2014;01/01/2014;1100;2100;;2200;2201;4009;5009;1200;1230;4000;5000;4905;5100;1103;2109;3200;4400;8100;7906;1200;4000;9998;9998;9999";

                sSection = "Load nominal section settings";
                sParam = LoadParam("NLConfig", "G");
                //if (sParam == "") sParam = "N;01/01/2014;01/01/2014;1100;2100;2200;2201;4009;5009;1200;1230;4000;5000;4905;5000;5100;1103;2109;3200;4400;8100;7906;1200;4000;9998;9998;9999;";
                if (sParam != "")
                {
                    sBits = sParam.Split(';');
                    App.nlUsePeriods = (sBits[0] == "Y");
                    //App.nlPeriodStart = DateTime.Parse(sBits[1]);
                   // App.nlPeriodEnd = DateTime.Parse(sBits[2]);

                    App.nlDebtors = sBits[3];
                    App.nlCreditors = sBits[4];
                    App.nlSalesTax = sBits[5];
                    App.nlPurchaseTax = sBits[6];
                    App.nlSalesDiscount = sBits[7];
                    App.nlPurchaseDiscount = sBits[8];
                    App.nlBank = sBits[9];
                    App.ncdPettyCash = sBits[10];
                    App.nlSales = sBits[11];
                    App.ncdPurchase = sBits[12];
                    App.nlSalesCarriage = sBits[13];
                    App.nlPurchaseCarriage = sBits[14];
                    App.nlPEItemsNomCd = sBits[15];
                    App.nlPrePayments = sBits[16];
                    App.nlAccruals = sBits[17];
                    App.nlRetaindEarnings = sBits[18];
                    App.nlCreditCharges = sBits[19];
                    App.nlBadDebt = sBits[20];
                    App.nlExchRateDifferences = sBits[21];
                    App.nlTillBank = sBits[22];
                    App.nlTillSales = sBits[23];
                    App.nlTillShortages = sBits[24];
                    App.nlSuspense = sBits[25];
                    App.nlMispostings = sBits[26];
                }
               // Conversion objCon = new Conversion();
                // Configurations: Stock                       ////////////////////////////////////////
                sSection = "Load stock control settings";
                sParam = LoadParam("StkConfig", "G");               // User definable stock parameters
                if (sParam != "")
                {
                    sBits = (sParam + ";;;;;;;;;;;;;;;;;").Split(';');
                    App.stNoOfPrices = objCon.ConToInt16(sBits[0]);
                    App.stAllowNegativeStk = (sBits[1] == "Y");
                    App.stShowManuf = (sBits[2] == "Y");
                    App.stShowSubCd = (sBits[3] == "Y");
                    App.stTransfer121 = (sBits[4] == "" || sBits[13] == "Y");
                    App.stShowParam1 = (sBits[5] == "Y");
                    App.stShowParam2 = (sBits[6] == "Y");
                    App.stShowParam3 = (sBits[7] == "Y");
                    App.stShowParam4 = (sBits[8] == "Y");
                    App.stShowParam5 = (sBits[9] == "Y");
                    App.stShowParam6 = (sBits[10] == "Y");
                    App.stParam1DP = objCon.ConToInt16(sBits[11]);                                      // Decimal places
                    App.stParam2DP = objCon.ConToInt16(sBits[12]);
                    App.stParam3DP = objCon.ConToInt16(sBits[13]);
                    App.stShowSaleHist = (sBits[14] == "Y");
                    App.stCatLabel = sBits[15];
                    App.stNo_Depots = objCon.ConToInt16(sBits[16]);                                    // Number of depots
                }

                // Configurations: Email                       ////////////////////////////////////////
                sSection = "Load email settings";
                sParam = LoadParam("EmailConfig", "G");
                if (sParam != "")
                {
                    sBits = sParam.Split(';');
                    App.emSMTPServer = sBits[0];
                    App.emSMTPPort = sBits[1];
                    App.emSecurity = sBits[2];
                    App.emReqAuth = (sBits[3] == "Y");
                    App.emLoginName = sBits[4];
                    App.emPassword = sBits[5];
                    App.emCC = sBits[6];
                    App.emBCC = sBits[7];
                    App.emFrom = sBits[8];
                }

                // Configurations: User                        ////////////////////////////////////////

                sSection = "Load user settings";
                sParam = LoadParam("UserConfig", "UG");
                if (sParam != "")
                {
                    sBits = sParam.Split(';');

                    App.usAppSkin = sBits[0];
                    App.usHighlightBG = int.Parse(sBits[1]);

                    // Foreground & background colour schemes
                    App.usMsgBG = int.Parse(sBits[2]);
                    App.usMsgFG = int.Parse(sBits[3]);
                    App.usWarnBG = int.Parse(sBits[4]);
                    App.usWarnFG = int.Parse(sBits[5]);
                    App.usUpdateBG = int.Parse(sBits[6]);
                    App.usLogBG = int.Parse(sBits[7]);

                    // Sales Ordere list view display colours
                    App.usSONoFG = int.Parse(sBits[8]);
                    App.usSOPartDispatchFG = int.Parse(sBits[9]);
                    App.usSOFullDispatchFG = int.Parse(sBits[10]);

                    // Sales Invoice List view background colours, normal & selected rows
                    App.usSInvFG = int.Parse(sBits[11]);
                    App.usSCrdFG = int.Parse(sBits[12]);
                    App.usFrgnCurrBG = int.Parse(sBits[13]);
                    App.usBeep = int.Parse(sBits[14]);
                    App.usEmailFormat = sBits[15];
                    App.usShowSOShipPreview = (sBits[16] == "Y");
                    App.usCloseSOShip = (sBits[17] == "Y");                      // Close SO form on Shipping ?
                    App.usOpenFiles = (sBits[18] == "Y");

                    // refresh interval is saved in milli-seconds; 0 = disable timer. Minimum interval is 1 minute / 60,000 ms
                    App.usStockListAutoRefresh = int.Parse(sBits[19]);
                    if (App.usStockListAutoRefresh != 0 && App.usStockListAutoRefresh < 60000) App.usStockListAutoRefresh = 60000;
                    App.usRoaming = (sBits[20] == "Y");
                    App.usTFTD = (sBits[21] == "Y");
                    App.usEscCloseTabs = (sBits[22] == "Y");

                    App.usSONoSelectedFG = int.Parse(sBits[23]);
                    App.usSOPartDispSelFG = int.Parse(sBits[24]);
                    App.usSOFullDispSelFG = int.Parse(sBits[25]);
                    App.usSInvSelFG = int.Parse(sBits[26]);
                    App.usSCrdSelFG = int.Parse(sBits[27]);

                    App.usListViewFont = sBits[28];
                    App.usListViewFontSize = int.Parse(sBits[29]);
                    if (App.usListViewFontSize < 3 || App.usListViewFontSize > 40) App.usListViewFontSize = 10;

                }

                sParam = LoadParam("AskBeforeClosing", "UG");
                App.usAskBeforeClosing = (sParam == "Y");

                // Configurations: Workstation                 ////////////////////////////////////////
                sSection = "Load workstation settings";
                sParam = LoadParam("WrkStnConfig", "WG") + ";;;;;;";
                sBits = sParam.Split(';');
                App.wsSharedDir = (sBits[0] == "" ? sAppPath : sBits[0]);
                App.wsTemplatesOnSrv = (sBits[1] == "Y");
                App.wsTemplatesDir = (sBits[2] == "" ? sBits[3] : sBits[2]);
                if (App.wsTemplatesDir == "") App.wsTemplatesDir = App.wsSharedDir + @"\Templates";
                App.wsDBSrvSharedDir = sBits[4];
                App.wsGenerateGridLayouts = (sBits[5] == "Y");

                // ############################################################################

                // Bob Kapur User setting: close print dialogue after printing ? String with each character representing different print screen / option
                // make sure the string is long enough for all options, say 60 for now!
                sSection = "Load print dialogue settings";
                gv.ClosePrintDialogue = LoadParam("ClosePrintDialogue", "UG").PadRight(60, ' ');

                // autostkallocation: Not used at present
                //sParam = LoadParam("SOPAutoAlloc", "G");
                //autostkallocation = GeneralFunctions.fnInt32(sParam);
                //Cursor.Current = oCursor;

            }

            catch (Exception ex)
            {
               // AcctMsg.MsgExceptionErr(ex, "00402", 1, sSection);
                ExceptionManager.LogException(ex);
            }
        }

 

        public static void Read_Windows_Settings()
        {
            // Read a text file containing various windows size and possition, saved on exit at previous session
            string FileName = "";
            //FileName = Acct.App.wsSharedDir + "\\Layouts\\" + gv.UserLogin + "_WinPos.txt";
            FileName = WindowsSettingsFile();

            string[] sBits;
            Int16 i = 0;

            try
            {
                if (System.IO.File.Exists(FileName))
                {  // File exists, read the top 20 lines, allowing for blank lines
                    System.IO.StreamReader oFile = new System.IO.StreamReader(FileName);

                    while (!oFile.EndOfStream && i < gv.WinPos.GetLength(0))
                    {
                        sBits = (oFile.ReadLine() + ";;;;;;;").Split(';');
                        gv.WinPos[i, 1] = sBits[1];                        // Window name
                        gv.WinPos[i, 2] = sBits[2];                        // Min / Sized / Max; 0 = Min, 1 = Sized, 2 = Max
                        gv.WinPos[i, 3] = sBits[3];                        // X-Pos
                        gv.WinPos[i, 4] = sBits[4];                        // Y-Pos
                        gv.WinPos[i, 5] = sBits[5];                        // Height
                        gv.WinPos[i, 6] = sBits[6];                        // Width
                        i++;
                    }

                    while (i < gv.WinPos.GetLength(0))
                    {
                        // If teh file did not have enough data for all windows, initialise remaining elements
                        gv.WinPos[i, 1] = "";                           // Window name
                        gv.WinPos[i, 2] = "";                           // Min / Sized / Max; 0 = Min, 1 = Sized, 2 = Max
                        gv.WinPos[i, 3] = "200";                        // X-Pos
                        gv.WinPos[i, 4] = "200";                        // Y-Pos
                        gv.WinPos[i, 5] = "200";                        // Height
                        gv.WinPos[i, 6] = "200";                        // Width
                        i++;
                    }

                    oFile.Close();
                    oFile = null;
                }
                else
                {
                    // File does not exist, setup blank array
                    WinPosBlank();
                }
            }
            catch (Exception ex)
            {
               // AcctMsg.MsgExceptionErr(ex, "00404", 2, "Load forms settings file: " + FileName);
                ExceptionManager.LogException(ex);
                // Something went wrong when reading file, setup blank array
                WinPosBlank();
            }
        }

        public static void Write_Windows_Settings()
        {
            // Save various window settings, in an array to a single text file
            // See also SetWinPosSize() & SaveWinPosSize(), below.
            string sLine = "";

            //string FileName = FileWindowsSettings();
            //string FileName = Path.Combine(Acct.App.wsSharedDir, "Layouts", gv.UserLogin + "_WinPos.txt");

            string FileName = WindowsSettingsFile();

            try
            {
                System.IO.StreamWriter ofile = new System.IO.StreamWriter(FileName, false);

                for (int j = 0; j < gv.WinPos.GetLength(0); j++)
                {
                    sLine = (j).ToString() + ";" + gv.WinPos[j, 1] + ";" + gv.WinPos[j, 2] + ";" + gv.WinPos[j, 3] + ";" + gv.WinPos[j, 4] + ";" + gv.WinPos[j, 5] + ";" + gv.WinPos[j, 6] + ";";
                    ofile.WriteLine(sLine);
                }

                ofile.Close();
                ofile = null;
            }

            catch (Exception ex)
            {
                //AcctMsg.MsgExceptionErr(ex, "00405", 2, "Saving forms settings to file: " + FileName);
            }
        }

        public static void ResetWinSetting()
        {
            // We don't need to delete the settings files which is overwritten each time the program exits.
            // We simply need to reset our settings array
            for (int i = 0; i < gv.WinPos.GetLength(0); i++)
            {
                // Set each windows heitht and width to 0 to force default settings
                gv.WinPos[i, 5] = "0";
                gv.WinPos[i, 6] = "0";
            }
        }

        public static void SetWinPosSize(int i, int h_default, int w_default, Form f)
        {
            // Set the windows (i) size and possition
            // i = Window number, dh = default height, dw = default width, f = actual form / window
            // Also see SaveWinPosSize(), below

            int h, w, l, t;
            l = 1;                                                             // Left
            t = 1;                                                             // Top
            h = h_default;                                                     // Height
            w = w_default;                                                     // Width
            // gv.WinPos[] Array for storing windows size and possitions: No, name, max, x, y, height, width, default_height, default_width
            // 0: Window Number
            // 1: Window Name
            // 2: Is the window maximised ?
            // 3: Left   - previous session
            // 4: Top    - Previous session
            // 5: Height - Previous session
            // 6: Width  - Previous session
            // 7: Default Height
            // 8: Default Width

            gv.WinPos[i, 7] = h_default.ToString();                            // Save default values, incase we want to reset window to default
            gv.WinPos[i, 8] = w_default.ToString();
            Conversion objCon = new Conversion();
            if (i < gv.WinPos.GetLength(0))
            {
                // Yes, windows number, i less than the no of Windows catered for ?

                if (gv.WinPos[i, 2] == "")                                     // We have no data from previous session
                {
                    // There is no data from previous session, use defaults
                    l = (Screen.PrimaryScreen.Bounds.Width - w) / 2;           // Center possition on screen
                    t = (Screen.PrimaryScreen.Bounds.Height - h) / 2;
                }
                else
                {
                    if (gv.WinPos[i, 2] != "1")
                    {  // Windows state is NOT maximised, set size and possition
                        l = objCon.ConToInt(gv.WinPos[i, 3]);                  // Left
                        t = objCon.ConToInt(gv.WinPos[i, 4]);                  // Top
                        h = objCon.ConToInt(gv.WinPos[i, 5]);
                        w = objCon.ConToInt(gv.WinPos[i, 6]);
                    }
                }

                if (gv.WinPos[i, 2] == "1")
                {  // Window state is maximised, no need to set size and possition
                    f.WindowState = FormWindowState.Maximized;
                }
                else
                {
                    // Make sure top left corner of screen is withing viewing area
                    if (l < 0 || l > Screen.PrimaryScreen.Bounds.Width) l = 1;
                    if (t < 0 || t > Screen.PrimaryScreen.Bounds.Height) t = 1;

                    f.Top = t;
                    f.Left = l;

                    if (w > 0)
                    {
                        // Set form width only if we have width value from previous session
                        if (w < 100) w = w_default;                  // Make sure the window size is big enough to be visible on screen
                        f.Width = w;
                    }

                    if (h > 0)
                    {
                        // Set form height only if we have height value from previous session
                        if (h < 100) h = h_default;
                        f.Height = h;
                    }

                    gv.WinPos[i, 5] = h.ToString();               // Save values for next session
                    gv.WinPos[i, 6] = w.ToString();
                }
            }
        }

        public static void SaveWinPosSize(int i, string name, Form f)
        {
            // Save the windows (i) size and possition, to an array, gv.WinPos[]
            // Also see SetWinPosSize(), above and Write_Windows_Settings()

            // Currently defined windows are:
            //  0: UPrCalc
            //  1: Program
            //  2: Print
            //  3: SOIDetails
            //  4: SOStkAlloc
            //  5: SOShipPreview
            //  6: SInvDetails
            //  7: OpeningStk
            //  8: StkSplitMerge
            //  9: StkTransfer
            // 10: StkConsolidate
            // 11: StkMove
            // 12: POESelectSupp
            // 13. Logs Browse
            // 14. NL Group
            // 15. NL Category
            // 16. Tax Codes
            // 17. Countries
            // 18. Currency
            // 19. Report Wizard
            // 20. Company List
            // 21. Backup/Restore
            // 22. UserList
            // 23. Email Form, frmEmail

            if (i < gv.WinPos.GetLength(0))
            {
                gv.WinPos[i, 1] = name;                                                  // Window name

                if (f.WindowState == FormWindowState.Maximized)                          // Window state
                {
                    gv.WinPos[i, 2] = "1";
                }
                else
                {
                    gv.WinPos[i, 2] = "0";
                }

                gv.WinPos[i, 3] = f.Left.ToString();                                     // Left
                gv.WinPos[i, 4] = f.Top.ToString();                                      // Top
                gv.WinPos[i, 5] = f.Height.ToString();                                   // Height
                gv.WinPos[i, 6] = f.Width.ToString();                                    // Width
            }
        }

        private static void WinPosBlank()
        {
            // Initialise array, WinPos[] to blank / default values
            int j = 0;

            while (j < gv.WinPos.GetLength(0))
            {
                gv.WinPos[j, 1] = "";                              // Window name
                gv.WinPos[j, 2] = "";                              // '' = No Data,  1= Windows state = Maximised
                gv.WinPos[j, 3] = "200";                           // X-Pos
                gv.WinPos[j, 4] = "200";                           // Y-Pos
                gv.WinPos[j, 5] = "0";                             // Height=0 - we have no data
                gv.WinPos[j, 6] = "0";                             // Width=0 - we have no data
                j++;
            }
        }

        private static string WindowsSettingsFile()
        {
            // return file name used to save / read windows (forms) settings
            return "";
           // return Path.Combine(DEBONO.App.wsSharedDir, "Layouts", gv.UserLogin + "_WinPos.txt");
        }
    }
}
