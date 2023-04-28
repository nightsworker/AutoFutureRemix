using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AutoFutures
{
    /// <summary>
    /// MASet.xaml 的互動邏輯
    /// </summary>
    public partial class MASet : UserControl
    {
        int m_nID;
        public MASet(int MA, double bias)
        {
            m_nID = (MA * 1000) + (int)(bias*10);
            InitializeComponent();
        }
    }
}
