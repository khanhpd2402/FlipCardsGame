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
using System.Windows.Media.Animation;

namespace FlipCardsGame
{
    /// <summary>
    /// Interaction logic for ChooseGroupWindow.xaml
    /// </summary>
    public partial class ChooseGroupWindow : Window
    {
        private readonly GameWindow _gameWindow;

        public ChooseGroupWindow( GameWindow gameWindow)
        {
            InitializeComponent();
            _gameWindow = gameWindow;
            HandleGroup();
        }
        private void HandleGroup()
        {
            var groupPlay = GroupPlayManager.Instance.CurrentGroup;
            if (groupPlay != null)
            {
                for (int i = 1; i <= 5; i++)
                {
                    var label = (Label)FindName($"labelGr{i}");
                    var button = (Button)FindName($"btnGr{i}");

                    if (label != null && button != null)
                    {
                        switch (label.Content)
                        {
                            case var groupName when groupName.Equals(groupPlay.GroupName):
                                button.Visibility = Visibility.Hidden;
                                break;
                            default:
                                break;
                        }
                    }
                }
            }
        }
        public static void Show(GameWindow gameWindow)
        {
            ChooseGroupWindow chooseGroup = new ChooseGroupWindow(gameWindow);
            chooseGroup.ShowDialog();
        }
        //chuyen window
        private void btnGr_Click(object sender, RoutedEventArgs e)
        {
            //bat su kien xem button nao click
            Button clickedButton = sender as Button;
            if (clickedButton == null)
                return;

            Label associatedLabel = null;

            switch (clickedButton.Name)
            {
                case "btnGr1":
                    associatedLabel = labelGr1;
                    break;
                case "btnGr2":
                    associatedLabel = labelGr2;
                    break;
                case "btnGr3":
                    associatedLabel = labelGr3;
                    break;
                case "btnGr4":
                    associatedLabel = labelGr4;
                    break;
                case "btnGr5":
                    associatedLabel = labelGr5;
                    break;
                default:
                    return;
            }
            //handle set group play
            var grName = associatedLabel.Content.ToString();
            var groupPlay = InitialData.Groups.FirstOrDefault(x => x.GroupName.Equals(grName));
            if (groupPlay != null)
            {
                GroupPlayManager.Instance.SetNewGroup(groupPlay);
            }
            // Cập nhật TextBlock của GameWindow
            _gameWindow.HandGroupName();
            // Close choose window
            this.Close();
        }
    }
}
