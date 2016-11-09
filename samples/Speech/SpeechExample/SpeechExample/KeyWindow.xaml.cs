using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using SpeechExample.Utils;

namespace SpeechExample
{
    /// <summary>
    /// Interaction logic for KeyWindow.xaml
    /// </summary>
    public partial class KeyWindow : Window
    {
        KeyManagement _keyManagement = new KeyManagement();
        public KeyWindow()
        {
            InitializeComponent();
            _keyManagement.GetSubscriptionKeyFromIsolatedStorage();

            KeyText.Text = _keyManagement.SubscriptionKey;
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {

            _keyManagement.SubscriptionKey = KeyText.Text;

            _keyManagement.SaveSubscriptionKeyToIsolatedStorage();

            this.Close();
        }
    }
}
