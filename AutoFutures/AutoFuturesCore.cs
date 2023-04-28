using SKCOMLib;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Threading;

namespace AutoFutures
{
    class AutoFuturesCore
    {
        #region Environment Variable
        //----------------------------------------------------------------------
        //環境變數
        //----------------------------------------------------------------------
        private static string m_pPath;                                              // 存執行檔案路徑變數
        private static string m_pDropbox = "";                                      // 存執行檔網路上路徑
        private static string m_pVersion = "\\Version.ini";                         // 存執行檔網路上路徑
        private static string m_pSetting = "\\Setting.ini";                         // 設定檔檔名
        private static string m_pTest = "\\Test.ini";                               // 測試檔檔名
        private static string m_pSave = "\\Save.ini";                               // 密碼存檔
        private static string m_pMsgLog = "\\CapitalLog\\MsgLog.txt";               // 訊息log檔案 
        private static string m_pState = "\\State.ini";                             // 狀態紀錄 
        private static string m_pCPReport = "\\交易程式_CP報表.xlsx";               // CP報表
        private static string m_pTradeLog = "\\TradeLog.txt";                       // 交易log
        private static string m_pMaLog = "\\MaLog.txt";                             // Ma log
        private static string m_pMinKLine = "\\MinKLine.txt";                       // K線存檔
        static Application _Excel = null;
        private MainWindow m_pMainWindow;
        //----------------------------------------------------------------------
        //Lib相關宣告
        //----------------------------------------------------------------------
        int m_nCode;                                                                // 群益com Lib回報訊息
        SKCenterLib m_pSKCenter;                                                    // 群益Lib核心功能都在此物件中
        SKQuoteLib m_pSKQuote;
        SKOrderLib m_pSKOrder;
        SKReplyLib m_pSKReply;
        //----------------------------------------------------------------------
        //使用者資料相關宣告
        //----------------------------------------------------------------------
        string m_pUserID;
        string m_pUserAccount;
        string m_pCheckAccount;
        string m_pUserName;
        int m_nSeparateNum;
        int m_nSeparateTime;
        bool m_bPostCP;
        bool m_bPostDiff;
        bool m_bPostMove;
        bool m_bPostSettle;
        bool m_bPostChange;
        bool m_bTradeMTX;
        bool m_bCustomer;
        public Dictionary<int, float> m_kKLineCloses;   //記錄每分鐘 收盤價格
        public Dictionary<int, float> m_kma1;

        public List<string> listKLine;
        public Queue<double> KLineMA;
        public Dictionary<double, double> KLineSort;
        // 期貨團隊策略相關變數宣告(O)---------------------------------------------------------------------->
        static public int m_nTXMonPerOne = 316000;
        static public int m_nMTXMonPerOne = 79000;
        public int m_nTXMonPerOneOriginal = 316000;
        public int m_nMTXMonPerOneOriginal = 79000;
        public int m_nTXShouldLot = 0;
        public int m_nMTXShouldLot = 0;
        public double m_nMA;
        public List<string> Transition;
        public double m_fBIAS;
        public int m_nDefault;                      // 當前策略使用n日MA的值
        public double m_nNowMa;                     // 當前策略使用n日MA的值
        public double m_nNowClose;                  // 當前收盤價
        public double m_nNextMonClose;              // 次月收盤價
        public double m_nNowCloseMTX;               // 當前收盤價
        public double m_nNextMonCloseMTX;           //  次月小台收盤價
        public short m_nBuySell;                    // 當前方向 買0 賣1 (無倉 -1)
        public short m_nTradeBuySell;
        public int m_nTradeTXNum;                   //此次交易需要交易大台口數
        public int m_nTradeMTXNum;                  //此次交易需要交易小台口數
        public int m_nNextTradeTXNum;               //下次交易需要交易大台口數
        public int m_nNextTradeMTXNum;              //下次交易需要交易小台口數
        public int m_nNextTradeHaveTXNum;           //下次交易完需要擁有大台口數
        public int m_nNextTradeHaveMTXNum;          //下次交易完需要擁有小台口數
        public int m_nTradeNextMonTXNum;            //此次交易需要交易次月大台口數
        public int m_nTradeNextMonMTXNum;           //此次交易需要交易次月小台口數
        public bool m_bIsMoreThanMa;                // 在此之前現價是否在MA之上
        public bool m_bIsStartTrade;                // 是否開始交易階段
        public bool m_bIsTradeUpdateLock;           // 是否開始交易階段
        public bool m_bIsClosePosition;             // 是否平倉完成
        public bool m_bIsDeal;
        public bool m_bIsCancel;
        public bool m_bIsTradingDay;                  // 是否為交易日
        public bool m_bIsStartChange;                  // 是否開始交易階段    
        public bool m_bIsGetPositionData;
        public bool m_bIsOverSettlementDate;
        public bool m_bDoCantNotTrade;
        public bool m_bTodayCloseDate;
        public bool m_bHasMTX;
        public bool m_bHasTX;
        public bool m_bHasNextMonMTX;
        public bool m_bHasNextMonTX;
        public bool m_bIsEarlyProfit;
        public bool m_bIsTestTrade;
        public bool m_bIsTestEarlyProfit;
        public bool m_bIsInitQuoteOK;
        public int m_nNowChangeTX;
        public int m_nNowChangeMTX;
        public int m_nTestChangeLots;
        public int m_nTestNextChangeLots;
        public int m_nChangeDays;                   // 換倉分段天數
        public int m_nNowChangeDays;                // 換倉剩餘天數
        public int m_nChangeTimes;                  // 換倉分段次數
        public bool m_bIsSeparateTrade;              // 是否CP分散
        public int m_nSeprateTradeTimes;             // 分散次數
        public int m_nSeparateTradeMin;              // 分散間隔(分鐘)
        string m_pSettlePlan;
        public Dictionary<string, int> SettlePlanDict;
        //public DateTime m_pCloseDate;                 // 結算日期
        public DateTime m_pCloseDateThisMon;                 // 結算日期
        public double m_nNomMaSum;
        public double m_nNextMaSum;
        public int m_nNowTXNum;
        public double m_fNowCumMon;
        public int m_nNowMTXNum;
        public int m_nNextMonTXNum;
        public int m_nNextMonMTXNum;
        public int m_nNowTotalMoney;
        public double m_fNowPositionCum;
        public bool m_bPastOverMa;
        public double m_fEarlyProfitNum;
        public double m_fEarlyProfitRate;
        System.Diagnostics.Stopwatch sw;
        enum TradeType
        {
            Normal = 0x0,
            Change = 0x1,
            TestNormal = 0x3,
            TestChange = 0x4,
        }
        enum RePortType  //回報類型
        {
            None = 0x0,
            CP = 0x1,  //CP報告
            Transaction = 0x2,
            System = 0x3,
            TradeLog = 0x4,
            MaLog = 0x5,

        };
        enum PostType  //推播類型
        {
            None = 0x0,
            CP = 0x1,
            Diff = 0x2,
            Move = 0x3,
            Settle = 0x4,
            Trade = 0x5,
            Change = 0x6,
            StopProfit = 0x7

        };
        // 期貨團隊策略相關變數宣告(E)----------------------------------------------------------------------->

        public float m_nMyPrice;
        public int m_nDayCounter;

        public string m_pNowMonTXPro;
        public string m_pNowMonMTXPro;
        public string m_pNextMonTXPro;
        public string m_pNextMonMTXPro;
        public string m_pStrategy;
        public string[] m_pWhen;
        public string[] m_pHappen;
        public List<int> m_pTXMargin;
        public List<int> m_pMTXMargin;
        public Queue<bool> m_pWhenResult;
        public bool rule1;
        public bool rule2;

        //用來計算換分鐘相關參數
        public int m_nNum;
        public int m_nTimes;
        public int m_nPrice;

        public int m_nMoveNum;
        public string m_pServerTime;
        public int m_nServerTime;
        public bool m_bIsTest;
        public bool m_bFirstHistory;
        public int m_nFirstTime;
        public Dictionary<int, string> m_kTickDict;
        public Dictionary<int, bool> m_krule1;
        public Dictionary<int, bool> m_krule2;
        public bool PreCPCheck;

        SolidColorBrush m_pBrush;                                                   // 訊號狀態球刷色用
        SolidColorBrush m_pBrush_Reply;                                                   // 訊號狀態球刷色用
        private System.Data.DataTable m_dtStocks;                                               // 顯示商品資訊的Table資料
        DispatcherTimer timerReconected;
        int m_nReconectedTime;
        DispatcherTimer timerCheckReconected;
        int m_nCheckReconectedTime;
        DispatcherTimer RPtimerReconected;
        int m_nRPReconectedTime;
        DispatcherTimer LItimerReconected;
        int m_nLIReconectedTime;
        DispatcherTimer DBtimerReconected;
        int m_nDBReconectedTime;
        Timer m_kTimer;
        // 連結Manager變數
        StreamWriter m_pServerSW;
        StreamReader m_pServerSR;
        String m_pServerReseq;
        public Thread m_pClientTread;
        public string m_pSendData;
        public string m_pSettingTip;
        public short m_bManagerAuto;
        Queue<string> m_pPipeLogStr;
        // 訊息用結構
        private struct MsgStructure
        {
            public string Type { get; set; }
            public string Message { get; set; }
            public string Time { get; set; }
        }


        public string sCheckPoint;
        List<int> kCheckPointComing;
        string sEndCheckPoint;
        string sEarlyCheckPoint;
        bool[] bEarlyPoint;
        bool[] bCheckPoint;
        string sChangePostion;
        bool[] bChangePostion;
        string sResetMa;
        string sClose;
        bool[] bClose;
        string sSeparateTrade = "";
        Queue<int> kTXSeparateLot;
        //----------------------------------------------------------------------
        //delegate 下單相關Handler
        //----------------------------------------------------------------------
        public delegate void MyMessageHandler(string strType, int nCode, string strMessage); // Lib的訊息回報Handler
        public event MyMessageHandler GetMessage;


        public delegate void OrderHandler(string strLogInID, bool bAsyncOrder, FUTUREORDER pStock);
        public event OrderHandler OnFutureOrderSignal;

        public delegate void OrderCLRHandler(string strLogInID, bool bAsyncOrder, FUTUREORDER pStock);
        public event OrderCLRHandler OnFutureOrderCLRSignal;

        public delegate void DecreaseOrderHandler(string strLogInID, bool bAsyncOrder, string strAccount, string strSeqNo, int nDecreaseQty);
        public event DecreaseOrderHandler OnDecreaseOrderSignal;

        public delegate void CancelOrderHandler(string strLogInID, bool bAsyncOrder, string strAccount, string strSeqNo);
        public event CancelOrderHandler OnCancelOrderSignal;

        public delegate void CancelOrderByStockHandler(string strLogInID, bool bAsyncOrder, string strAccount, string strStockNo);
        public event CancelOrderByStockHandler OnCancelOrderByStockSignal;


        public delegate void CorrectPriceBySeqNoHandler(string strLogInID, bool bAsyncOrder, string strAccount, string strSeqNo, string strPrice, int nTradeType);
        public event CorrectPriceBySeqNoHandler OnCorrectPriceBySeqNo;

        public delegate void CorrectPriceByBookNoHandler(string strLogInID, bool bAsyncOrder, string strAccount, string strSymbol, string strSeqNo, string strPrice, int nTradeType);
        public event CorrectPriceByBookNoHandler OnCorrectPriceByBookNo;


        public delegate void OpenInterestHandler(string strLogInID, string strAccount);
        public event OpenInterestHandler OnOpenInterestSignal;

        public delegate void FutureRightsHandler(string strLogInID, string strAccount, int nCoinType);
        public event FutureRightsHandler OnFutureRightsSignal;

        public delegate void CancelOrderByBookHandler(string strLogInID, bool bAsyncOrder, string strAccount, string strBookNo);
        public event CancelOrderByBookHandler OnCancelOrderByBookSignal;

        public delegate void SendTXOffsetSignalHandler(string strLogInID, bool bAsyncOrder, string strAccount, string strYearMonth, int nBuySell, int nQty);
        public event SendTXOffsetSignalHandler OnSendTXOffsetSignal;

        public delegate void OpenInterestWithFormatHandler(string strLogInID, string strAccount, int nFormat);
        public event OpenInterestWithFormatHandler OnOpenInterestWithFormatSignal;


        enum Setting  //紀錄個人設定的列舉變數
        {
            None = 0x0,
            AutoLogin = 0x1  //自動登入(記住密碼)

        };
        private Setting m_nSettingFlag;  //記錄勾選後狀態變數
        private string m_PCName;
        #endregion
        #region CustomizedFunc
        private void InitAutoFuture()
        {
            m_pMainWindow = (MainWindow)Application.Current.MainWindow;
            m_pSKCenter = new SKCenterLib();
            m_pSKQuote = new SKQuoteLib();
            m_pSKOrder = new SKOrderLib();
            m_pSKReply = new SKReplyLib();
            m_pSKReply.OnReplyMessage += new _ISKReplyLibEvents_OnReplyMessageEventHandler(this.OnAnnouncement);
            m_pSKCenter.SKCenterLib_SetAuthority(1);
            m_pBrush = new SolidColorBrush();  // 訊號球顏色
            m_pBrush.Color = Colors.Red;       // 起始紅色
            m_pMainWindow.lblSignal.Fill = m_pBrush;         // 給求上色

            m_pBrush_Reply = new SolidColorBrush();  // 訊號球顏色
            m_pBrush_Reply.Color = Colors.Red;       // 起始紅色
            m_pMainWindow.lblSignal_Reply.Fill = m_pBrush_Reply;   // 給求上色
            m_pPath = System.IO.Directory.GetCurrentDirectory();
            m_dtStocks = CreateStocksDataTable();
        }

        private void InitPaser()
        {

        }
        //private void SaveSave() // 存入Save
        //{
        //    try
        //    {

        //        using (StreamWriter file = new StreamWriter(m_pPath + m_pSave))
        //        {
        //            string save_str = "";

        //            if (IsKeepPW.IsChecked == true)
        //            {
        //                save_str = "IsKeepPW=1";
        //                save_str = Encrypt(save_str);
        //                file.WriteLine(save_str);
        //                save_str = "PassWord=" + Password_txt.Password.Trim();
        //                save_str = Encrypt(save_str);
        //                file.WriteLine(save_str);
        //                save_str = "Account=" + Account_txt.Text.Trim();
        //                save_str = Encrypt(save_str);
        //                file.WriteLine(save_str);

        //            }
        //            else
        //            {
        //                save_str = "IsKeepPW=0";
        //                save_str = Encrypt(save_str);
        //                file.WriteLine(save_str);
        //            }
        //            if (開啟CP.IsChecked == false)
        //            {
        //                save_str = "OpenCP=0";
        //                save_str = Encrypt(save_str);
        //                file.WriteLine(save_str);
        //            }
        //            else
        //            {
        //                save_str = "OpenCP=1";
        //                save_str = Encrypt(save_str);
        //                file.WriteLine(save_str);
        //            }
        //            if (IsAutoLogin.IsChecked == true)
        //            {
        //                save_str = "IsAutoLogin=1";
        //                save_str = Encrypt(save_str);
        //                file.WriteLine(save_str);
        //            }
        //            else
        //            {
        //                save_str = "IsAutoLogin=0";
        //                save_str = Encrypt(save_str);
        //                file.WriteLine(save_str);
        //            }
        //            if (IsPostCP.IsChecked == true)
        //            {
        //                save_str = "IsPostCP=1";
        //                save_str = Encrypt(save_str);
        //                file.WriteLine(save_str);
        //            }
        //            else
        //            {
        //                save_str = "IsPostCP=0";
        //                save_str = Encrypt(save_str);
        //                file.WriteLine(save_str);
        //            }
        //            if (IsPostDiff.IsChecked == true)
        //            {
        //                save_str = "IsPostDiff=1";
        //                save_str = Encrypt(save_str);
        //                file.WriteLine(save_str);
        //            }
        //            else
        //            {
        //                save_str = "IsPostDiff=0";
        //                save_str = Encrypt(save_str);
        //                file.WriteLine(save_str);
        //            }
        //            if (IsPostMove.IsChecked == true)
        //            {
        //                save_str = "IsPostMove=1";
        //                save_str = Encrypt(save_str);
        //                file.WriteLine(save_str);
        //            }
        //            else
        //            {
        //                save_str = "IsPostMove=0";
        //                save_str = Encrypt(save_str);
        //                file.WriteLine(save_str);
        //            }
        //            if (IsPostSettle.IsChecked == true)
        //            {
        //                save_str = "IsPostSettle=1";
        //                save_str = Encrypt(save_str);
        //                file.WriteLine(save_str);
        //            }
        //            else
        //            {
        //                save_str = "IsPostSettle=0";
        //                save_str = Encrypt(save_str);
        //                file.WriteLine(save_str);
        //            }
        //            if (IsPostChange.IsChecked == true)
        //            {
        //                save_str = "IsPostChange=1";
        //                save_str = Encrypt(save_str);
        //                file.WriteLine(save_str);
        //            }
        //            else
        //            {
        //                save_str = "IsPostChange=0";
        //                save_str = Encrypt(save_str);
        //                file.WriteLine(save_str);
        //            }
        //            if (IsTradeMTX.IsChecked == true)
        //            {
        //                save_str = "IsTradeMTX=1";
        //                save_str = Encrypt(save_str);
        //                file.WriteLine(save_str);
        //            }
        //            else
        //            {
        //                save_str = "IsTradeMTX=0";
        //                save_str = Encrypt(save_str);
        //                file.WriteLine(save_str);
        //            }
        //            if (IsCustomer.IsChecked == true)
        //            {
        //                save_str = "IsCustomer=1";
        //                save_str = Encrypt(save_str);
        //                file.WriteLine(save_str);
        //            }
        //            else
        //            {
        //                save_str = "IsCustomer=0";
        //                save_str = Encrypt(save_str);
        //                file.WriteLine(save_str);
        //            }

        //        }
        //    }
        //    catch (Exception e)
        //    {

        //    }
        //}

        private string Encrypt(string save_str)
        {
            throw new NotImplementedException();
        }

        private void SaveState() // 存入State
        {
            using (StreamWriter file = new StreamWriter(m_pPath + m_pState))
            {
                string save_str = "";

                if (m_bIsMoreThanMa)
                {
                    save_str = "IsMoreThanMa=1";
                    file.WriteLine(save_str);
                }
                else
                {
                    save_str = "IsMoreThanMa=0";
                    file.WriteLine(save_str);
                }
                if (m_bIsEarlyProfit)
                {
                    save_str = "IsEarlyProfit=1";
                    file.WriteLine(save_str);
                }
                else
                {
                    save_str = "IsEarlyProfit=0";
                    file.WriteLine(save_str);
                }
                int maxShould = Math.Max(m_nNowMTXNum, m_nMTXShouldLot);
                m_nMTXShouldLot = maxShould;
                save_str = "ShouldLotMTX=" + maxShould;
                file.WriteLine(save_str);
                maxShould = Math.Max(m_nNowTXNum, m_nTXShouldLot);
                m_nTXShouldLot = maxShould;
                save_str = "ShouldLotTX=" + maxShould;
                file.WriteLine(save_str);
                if (m_pSettlePlan != "")
                {
                    file.WriteLine("SettlePlan=" + m_pSettlePlan);
                }
                file.Close();
            }

        }
        //private bool ParseState() // 
        //{
        //    try
        //    {
        //        using (StreamReader sr = new StreamReader(m_pPath + m_pState))
        //        {
        //            string line;
        //            while ((line = sr.ReadLine()) != null)
        //            {
        //                string[] dict = line.Split('=');
        //                //bool value = Boolean.Parse(dict[1]);
        //                switch (dict[0])
        //                {
        //                    case "IsMoreThanMa":
        //                        if (Int32.Parse(dict[1]) == 0)
        //                        {
        //                            m_bIsMoreThanMa = false;
        //                        }
        //                        else
        //                        {
        //                            m_bIsMoreThanMa = true;
        //                        }
        //                        break;
        //                    case "ShouldLotMTX":
        //                        int.TryParse(dict[1], out m_nMTXShouldLot);
        //                        break;
        //                    case "ShouldLotTX":
        //                        int.TryParse(dict[1], out m_nTXShouldLot);
        //                        break;
        //                    case "IsEarlyProfit":
        //                        if (Int32.Parse(dict[1]) == 0)
        //                        {
        //                            m_bIsEarlyProfit = false;
        //                        }
        //                        else
        //                        {
        //                            m_bIsEarlyProfit = true;
        //                        }
        //                        break;
        //                    case "SettlePlan":
        //                        m_pSettlePlan = dict[1];
        //                        string[] settledate = dict[1].Split(';');
        //                        DateTime nowTime = DateTime.Now;
        //                        string todaystring = nowTime.ToString("yyyyMMdd");
        //                        foreach (var list in settledate)
        //                        {
        //                            if (list != "")
        //                            {
        //                                string[] settlelist = list.Split('|');
        //                                if (todaystring == settlelist[0])
        //                                {
        //                                    m_bIsStartChange = true;
        //                                }
        //                                string[] point = settlelist[1].Split(',');
        //                                foreach (var str in point)
        //                                {
        //                                    string[] lots = str.Split('_');
        //                                    string time = lots[0].Replace(":", "");
        //                                    int lottx;
        //                                    int lotmtx;
        //                                    int.TryParse(lots[1], out lottx);
        //                                    int.TryParse(lots[2], out lotmtx);
        //                                    int mtxtx = (lottx * 1000) + lotmtx;
        //                                    SettlePlanDict.Add(settlelist[0] + time, mtxtx);
        //                                }
        //                            }
        //                        }

        //                        break;

        //                }
        //            }
        //            sr.Close();
        //        }

        //        using (StreamReader sr = new StreamReader(m_pPath + m_pMsgLog))
        //        {
        //            string line;
        //            string line_str = "";
        //            while ((line = sr.ReadLine()) != null)
        //            {
        //                if (line == "")
        //                {
        //                    continue;
        //                }
        //                line_str = Decrypt(line);
        //                string[] dict = line_str.Split('|');
        //                string[] datestr = dict[2].Split('】');
        //                DateTime Date = Convert.ToDateTime(datestr[1]);
        //                DateTime NowDate = DateTime.Now;
        //                double DifDays = (NowDate - Date).TotalDays;
        //                if (dict[1].Contains("秒後"))
        //                    continue;
        //                if (DifDays > 7)
        //                    continue;
        //                listInformation.Items.Add(new MsgStructure { Type = dict[0], Message = dict[1], Time = dict[2] });


        //            }
        //            listInformation.SelectedIndex = listInformation.Items.Count - 1;
        //            listInformation.ScrollIntoView(listInformation.SelectedItem);
        //            sr.Close();
        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        return false;
        //    }
        //    return true;
        //}
        //private bool ParseSave() // 讀取Save
        //{
        //    try
        //    {
        //        using (StreamReader sr = new StreamReader(m_pPath + m_pSave))
        //        {
        //            string line;
        //            while ((line = sr.ReadLine()) != null)
        //            {
        //                line = Decrypt(line);
        //                string[] dict = line.Split('=');
        //                //bool value = Boolean.Parse(dict[1]);
        //                switch (dict[0])
        //                {
        //                    case "PassWord":
        //                        Password_txt.Password = dict[1];
        //                        break;
        //                    case "Account":
        //                        Account_txt.Text = dict[1];
        //                        break;
        //                    case "IsAutoLogin":
        //                        if (Int32.Parse(dict[1]) == 1)
        //                        {
        //                            IsAutoLogin.IsChecked = true;
        //                        }
        //                        else
        //                        {
        //                            IsAutoLogin.IsChecked = false;
        //                        }
        //                        break;
        //                    case "IsKeepPW":
        //                        if (Int32.Parse(dict[1]) == 1)
        //                        {
        //                            IsKeepPW.IsChecked = true;
        //                        }
        //                        else
        //                        {
        //                            IsKeepPW.IsChecked = false;
        //                        }
        //                        break;
        //                    case "IsPostCP":
        //                        if (Int32.Parse(dict[1]) == 1)
        //                        {
        //                            IsPostCP.IsChecked = true;
        //                        }
        //                        else
        //                        {
        //                            IsPostCP.IsChecked = false;
        //                        }
        //                        break;
        //                    case "IsPostDiff":
        //                        if (Int32.Parse(dict[1]) == 1)
        //                        {
        //                            IsPostDiff.IsChecked = true;
        //                        }
        //                        else
        //                        {
        //                            IsPostDiff.IsChecked = false;
        //                        }
        //                        break;
        //                    case "IsPostMove":
        //                        if (Int32.Parse(dict[1]) == 1)
        //                        {
        //                            IsPostMove.IsChecked = true;
        //                        }
        //                        else
        //                        {
        //                            IsPostMove.IsChecked = false;
        //                        }
        //                        break;
        //                    case "IsPostSettle":
        //                        if (Int32.Parse(dict[1]) == 1)
        //                        {
        //                            IsPostSettle.IsChecked = true;
        //                        }
        //                        else
        //                        {
        //                            IsPostSettle.IsChecked = false;
        //                        }
        //                        break;
        //                    case "IsTradeMTX":
        //                        if (Int32.Parse(dict[1]) == 1)
        //                        {
        //                            IsTradeMTX.IsChecked = true;
        //                        }
        //                        else
        //                        {
        //                            IsTradeMTX.IsChecked = false;
        //                        }
        //                        break;
        //                    case "IsCustomer":
        //                        if (Int32.Parse(dict[1]) == 1)
        //                        {
        //                            IsCustomer.IsChecked = true;
        //                        }
        //                        else
        //                        {
        //                            IsCustomer.IsChecked = false;
        //                        }
        //                        break;
        //                    case "IsPostChange":
        //                        if (Int32.Parse(dict[1]) == 1)
        //                        {
        //                            IsPostChange.IsChecked = true;
        //                        }
        //                        else
        //                        {
        //                            IsPostChange.IsChecked = false;
        //                        }
        //                        break;
        //                    case "OpenCP":
        //                        if (Int32.Parse(dict[1]) == 1)
        //                        {
        //                            開啟CP.IsChecked = true;
        //                        }
        //                        else
        //                        {
        //                            開啟CP.IsChecked = false;
        //                        }
        //                        break;
        //                }
        //            }
        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        return false;
        //    }
        //    return true;
        //}
        private void ChangeSettlement(string nowSettlement) // 讀取設定檔
        {
            using (StreamReader sr = new StreamReader(m_pPath + m_pSetting))
            {
                string str = sr.ReadToEnd();
                sr.Close();
                str = str.Replace(m_pCloseDateThisMon.ToString("yyyyMMdd"), nowSettlement);

                //更改保存文本
                StreamWriter sw = new StreamWriter(m_pPath + m_pSetting, false);
                sw.WriteLine(str);
                sw.Close();
                //m_pMainWindow.Settlement值.Content = "結算日 " + nowSettlement;
            }
        }
        public void ChangeMA(string ma) // 讀取設定檔
        {
            using (StreamReader sr = new StreamReader(m_pPath + m_pSetting))
            {
                string str = sr.ReadToEnd();
                sr.Close();
                str = str.Replace("MA=" + m_nMA, "MA=" + ma);

                //更改保存文本
                StreamWriter sw = new StreamWriter(m_pPath + m_pSetting, false);
                sw.WriteLine(str);
                sw.Close();
                //m_pMainWindow.MA值.Content = "MA  " + ma;
            }
        }
        private bool ParseTest() // 讀取設定檔
        {
            try
            {
                using (StreamReader sr = new StreamReader(m_pPath + m_pTest))
                {
                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        if (line == "")
                        {
                            continue;
                        }

                        string[] dict = line.Split('=');
                        //bool value = Boolean.Parse(dict[1]);
                        switch (dict[0])
                        {
                            case "TestChangeLots":
                                int.TryParse(dict[1], out m_nTestChangeLots);
                                break;
                        }
                    }
                }
            }
            catch (Exception e)
            {
                return false;
            }
            return true;
        }
        private void ParserSetup()
        {
            ParserManager manager = ParserManager.getInstance();
            FileParser setting = new FileParser(m_pPath + m_pSetting);
            manager.m_pParsers.Add("Setting", setting);
            setting.m_pKeys.Add("MA");
            setting.m_pKeys.Add("Settlement");
            setting.m_pKeys.Add("Default");
            setting.m_pKeys.Add("CP");
            setting.m_pKeys.Add("CPList");
            setting.m_pKeys.Add("SettleCPList");
            setting.m_pKeys.Add("CPLEarlyProfitCPListist");
            setting.m_pKeys.Add("PCName");
            setting.m_pKeys.Add("EarlyProfit");
            setting.m_pKeys.Add("EarlyRate");
            setting.m_pKeys.Add("SettleCPTime");
            setting.m_pKeys.Add("ResetMA");
            setting.m_pKeys.Add("Close");
            setting.m_pKeys.Add("TXMargin");
            setting.m_pKeys.Add("MTXMargin");

        }
        private bool ParseSetting() // 讀取設定檔
        {

            try
            {
                using (StreamReader sr = new StreamReader(m_pPath + m_pSetting))
                {
                    string line;
                    m_pSettingTip = "";
                    while ((line = sr.ReadLine()) != null)
                    {
                        if (line == "")
                        {
                            continue;
                        }

                        m_pSettingTip += (line + "><");
                        string[] dict = line.Split('=');

                        switch (dict[0])
                        {
                            case "AutoLogin":
                                m_nSettingFlag |= Setting.AutoLogin;
                                break;
                            case "Settlement":
                                //m_pMainWindow.Settlement值.Content = "結算日 " + dict[1];
                                dict[1] = dict[1] + "1325";
                                m_pCloseDateThisMon = DateTime.ParseExact(dict[1], "yyyyMMddHHmm", System.Globalization.CultureInfo.CurrentCulture);
                                break;
                            case "MA":
                                //m_pMainWindow.MA值.Content = "MA  " + dict[1];
                                //m_wSetting.MA數值.Text = dict[1];
                                double.TryParse(dict[1], out m_nMA);
                                break;
                            case "Default":
                                int.TryParse(dict[1], out m_nDefault);
                                //m_wSetting.Default值.Text = dict[1];
                                //原始投資金額值1.Text = "$ " + m_nDefault.ToString("#,0"); ;
                                break;
                            case "Dropbox":
                                //m_wSetting.Dropbox值.Text = dict[1];
                                m_pDropbox = dict[1];
                                break;
                            case "CP":
                                //m_pMainWindow.CP值.Content = "CP  " + dict[1];
                                break;
                            case "CPList":
                                sCheckPoint = dict[1];
                                break;
                            case "SettleCPList":
                                sEndCheckPoint = dict[1];
                                break;
                            case "EarlyProfitCPList":
                                sEarlyCheckPoint = dict[1];
                                break;
                            case "PCName":
                                m_PCName = dict[1];
                                break;
                            case "EarlyProfit":
                                double.TryParse(dict[1], out m_fEarlyProfitNum);
                                break;
                            case "EarlyRate":
                                double.TryParse(dict[1], out m_fEarlyProfitRate);
                                break;
                            case "SettleCPTime":
                                //m_wSetting.SettleCPTime值.Text = dict[1];
                                sChangePostion = dict[1];
                                string[] arrstr = sChangePostion.Split('|');
                                sChangePostion = arrstr[1];
                                int.TryParse(arrstr[0], out m_nChangeDays);
                                string[] arrstr2 = arrstr[1].Split(',');
                                m_nChangeTimes = arrstr2.Length;
                                //m_pMainWindow.SettleTime值.Content = "換倉時間 " + arrstr[0] + "日," + arrstr[1];
                                break;
                            case "ResetMA":
                                sResetMa = dict[1];
                                break;
                            case "Close":
                                sClose = dict[1];
                                break;
                            case "TXMargin":
                                int.TryParse(dict[1], out m_nTXMonPerOne);
                                int.TryParse(dict[1], out m_nTXMonPerOneOriginal);
                                break;
                            case "MTXMargin":
                                int.TryParse(dict[1], out m_nMTXMonPerOne);
                                int.TryParse(dict[1], out m_nMTXMonPerOneOriginal);
                                break;
                            case "Account":
                                m_pUserAccount = dict[1];
                                break;
                            case "Name":
                                m_pUserName = dict[1];
                                m_pMainWindow.Title = m_pUserName;
                                break;
                            case "Separate":
                                string[] strarray = dict[1].Split(',');

                                int.TryParse(strarray[0], out m_nSeparateNum);
                                int.TryParse(strarray[1], out m_nSeparateTime);
                                break;
                            case "TradeMTX":
                                int trade;
                                int.TryParse(dict[1], out trade);
                                if (trade == 1)
                                {
                                    m_pMainWindow.IsTradeMTX.IsChecked = true;
                                }
                                else if (trade == 0)
                                {
                                    m_pMainWindow.IsTradeMTX.IsChecked = false;
                                }
                                break;
                            case "ManagerAuto":
                                short.TryParse(dict[1], out m_bManagerAuto);
                                break;
                            case "TradeType":
                                string[] separate = dict[1].Split(',');
                                int.TryParse(separate[0], out m_nSeparateTradeMin);
                                int.TryParse(separate[1], out m_nSeprateTradeTimes);
                                break;
                        }
                    }
                }
            }
            catch (Exception e)
            {
                if (System.Windows.MessageBox.Show("Setting有問題，請洽工程師", "提示：",
                            MessageBoxButton.OK) == MessageBoxResult.OK)
                {
                    return false;
                }
                return false;
            }
            return true;
        }
        private System.Data.DataTable CreateStocksDataTable()
        {
            System.Data.DataTable myDataTable = new System.Data.DataTable();

            DataColumn myDataColumn;

            myDataColumn = new DataColumn();
            myDataColumn.DataType = Type.GetType("System.Int16");
            myDataColumn.ColumnName = "m_sStockidx";
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = Type.GetType("System.Int16");
            myDataColumn.ColumnName = "m_sDecimal";
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = Type.GetType("System.Int16");
            myDataColumn.ColumnName = "m_sTypeNo";
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = Type.GetType("System.String");
            myDataColumn.ColumnName = "m_cMarketNo";
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = Type.GetType("System.String");
            myDataColumn.ColumnName = "m_caStockNo";
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = Type.GetType("System.String");
            myDataColumn.ColumnName = "m_caName";
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = Type.GetType("System.Double");
            myDataColumn.ColumnName = "m_nOpen";
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = Type.GetType("System.Double");
            myDataColumn.ColumnName = "m_nHigh";
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = Type.GetType("System.Double");
            myDataColumn.ColumnName = "m_nLow";
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = Type.GetType("System.Double");
            myDataColumn.ColumnName = "m_nClose";
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = Type.GetType("System.Int32");
            myDataColumn.ColumnName = "m_nTickQty";
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = Type.GetType("System.Double");
            myDataColumn.ColumnName = "m_nRef";
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = Type.GetType("System.Double");
            myDataColumn.ColumnName = "m_nBid";
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = Type.GetType("System.Int32");
            myDataColumn.ColumnName = "m_nBc";
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = Type.GetType("System.Double");
            myDataColumn.ColumnName = "m_nAsk";
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = Type.GetType("System.Int32");
            myDataColumn.ColumnName = "m_nAc";
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = Type.GetType("System.Int32");
            myDataColumn.ColumnName = "m_nTBc";
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = Type.GetType("System.Int32");
            myDataColumn.ColumnName = "m_nTAc";
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = Type.GetType("System.Int32");
            myDataColumn.ColumnName = "m_nFutureOI";
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = Type.GetType("System.Int32");
            myDataColumn.ColumnName = "m_nTQty";
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = Type.GetType("System.Int32");
            myDataColumn.ColumnName = "m_nYQty";
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = Type.GetType("System.Double");
            myDataColumn.ColumnName = "m_nUp";
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = Type.GetType("System.Double");
            myDataColumn.ColumnName = "m_nDown";
            myDataTable.Columns.Add(myDataColumn);

            myDataTable.PrimaryKey = new DataColumn[] { myDataTable.Columns["m_caStockNo"] };

            return myDataTable;
        }
        #endregion
        #region callback
        void OnAnnouncement(string strUserID, string bstrMessage, out short nConfirmCode)
        {
            //WriteMessage(strUserID + "_" + bstrMessage);
            nConfirmCode = -1;

        }
        #endregion
    }
}
