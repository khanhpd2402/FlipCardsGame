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

namespace FlipCardsGame.CustomWindow
{
    /// <summary>
    /// Interaction logic for CustomMessageBox.xaml
    /// </summary>
    public partial class CustomMessageBox : Window
    {
        public bool Result { get; private set; }

        public CustomMessageBox(string message)
        {
            InitializeComponent();
            txtMessage.Text = message;

        }
        private void OKButton_Click(object sender, RoutedEventArgs e)
        {
            Result = true;
            this.Close();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            Result = false;
            this.Close();
        }

        public static bool Show(string message)
        {
            CustomMessageBox messageBox = new CustomMessageBox(message);
            messageBox.ShowDialog();
            return messageBox.Result;
        }
    }
}

