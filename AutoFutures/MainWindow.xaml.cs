using SKCOMLib;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using System.Xml;

namespace AutoFutures
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
       
        public MainWindow()
        {
            InitializeComponent();
          
        }
       
        #region UsefulFunc
        public UIElement CloneElement(UIElement orig)
        {
            if (orig == null)
                return (null);
            string s = XamlWriter.Save(orig);
            StringReader stringReader = new StringReader(s);
            XmlReader xmlReader = XmlTextReader.Create(stringReader, new XmlReaderSettings());
            return (UIElement)XamlReader.Load(xmlReader);
        }
        #endregion
        #region ComponentFunc
        private void AccountBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Login_btn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void CloseApp_Click(object sender, RoutedEventArgs e)
        {

        }

        private void 版本更新_Click(object sender, RoutedEventArgs e)
        {

        }

        private void LoadCert_Click(object sender, RoutedEventArgs e)
        {

        }

        private void KLineSave_Click(object sender, RoutedEventArgs e)
        {

        }

        private void CantTrade_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Setting_btn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void IsAutoLogin_Click(object sender, RoutedEventArgs e)
        {

        }

        private void IsKeepPW_Click(object sender, RoutedEventArgs e)
        {

        }

        private void IsPostMsg_Click(object sender, RoutedEventArgs e)
        {

        }

        private void IsCustomer_Click(object sender, RoutedEventArgs e)
        {

        }

        private void 停利測試_Click(object sender, RoutedEventArgs e)
        {

        }

        private void 執行交易_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Change_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BuyTX_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BuyMTX_Click(object sender, RoutedEventArgs e)
        {

        }

        private void SellTX_Click(object sender, RoutedEventArgs e)
        {

        }

        private void SellMTX_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BuyTX_Next_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BuyMTX_Next_Click(object sender, RoutedEventArgs e)
        {

        }

        private void SellTX_Next_Click(object sender, RoutedEventArgs e)
        {

        }

        private void SellMTX_Next_Click(object sender, RoutedEventArgs e)
        {

        }

        private void IsPostCP_Click(object sender, RoutedEventArgs e)
        {

        }

        private void IsPostSettle_Click(object sender, RoutedEventArgs e)
        {

        }

        private void IsPostAlert_Click(object sender, RoutedEventArgs e)
        {

        }

        private void IsPostDiff_Click(object sender, RoutedEventArgs e)
        {

        }

        private void IsPostMove_Click(object sender, RoutedEventArgs e)
        {

        }
      
        private void listInformation_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void ChangePlan_Click(object sender, RoutedEventArgs e)
        {

        }
      
        private void push_Click(object sender, RoutedEventArgs e)
        {

        }

        private void MsgSize_LayoutUpdated(object sender, EventArgs e)
        {

            listInformation.Height = MsgSize.Value;
        }

        private void 萬用測試_Click(object sender, RoutedEventArgs e)
        {

            MaStack.Children.Add(new MASet(25, 3.8));
        }
        #endregion

    }
}
