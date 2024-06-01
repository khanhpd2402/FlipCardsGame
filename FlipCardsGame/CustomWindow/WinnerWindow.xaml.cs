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
using FlipCardsGame.Models;
namespace FlipCardsGame.CustomWindow
{
    /// <summary>
    /// Interaction logic for WinnerWindow.xaml
    /// </summary>
    public partial class WinnerWindow : Window
    {
        public WinnerWindow(GroupPlay groupPlay)
        {
            InitializeComponent();
            lbGrName.Content = "Chúc mừng " + groupPlay.GroupName;
        }
        public static void ShowWinner(GroupPlay groupPlay)
        {
            WinnerWindow winner = new WinnerWindow(groupPlay);
            winner.Show();
        }
    }
}
