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
using System.Windows.Media.Animation;
using FlipCardsGame.Models;
using FlipCardsGame.CustomWindow;
namespace FlipCardsGame
{
    /// <summary>
    /// Interaction logic for PlayWindow.xaml
    /// </summary>
    public partial class PlayWindow : Window
    {
        private readonly QuizGameDBContext? _context;
        public PlayWindow(QuizGameDBContext context)
        {
            InitializeComponent();
            _context = context;
            HandleScore();
        }
        public void HandleScore()
        {
            List<GroupPlay> groupPlays = InitialData.Groups.ToList();
            
            labelScore1.Content = groupPlays[0].Score;
            labelScore2.Content = groupPlays[1].Score;
            labelScore3.Content = groupPlays[2].Score;
            labelScore4.Content = groupPlays[3].Score;
            labelScore5.Content = groupPlays[4].Score;
        }
        private void Button_MouseEnter(object sender, MouseEventArgs e)
        {
            if (sender is Button button && button.Content is Label label)
            {
                label.FontSize += 7; // Increase font size by 5
                label.Foreground = new SolidColorBrush(Colors.Red); // Change color to Red
            }
        }

        private void Button_MouseLeave(object sender, MouseEventArgs e)
        {
            if (sender is Button button && button.Content is Label label)
            {
                if (label.Name.Equals("labelBack"))
                {
                    label.FontSize = 45;
                    label.Foreground = new SolidColorBrush(Colors.AliceBlue); // Change color back to original
                }
                else
                {
                    label.FontSize = 40;
                    label.Foreground = new SolidColorBrush(Colors.Wheat); // Change color back to original
                }
            }
        }

        private void Image_MouseEnter(object sender, MouseEventArgs e)
        {
            if (sender is Image image)
            {
                    // Tạo animation để phóng to hình ảnh
                    DoubleAnimation animation = new DoubleAnimation(150, TimeSpan.FromSeconds(0.5));
                    image.BeginAnimation(Image.WidthProperty, animation);

                    animation = new DoubleAnimation(150, TimeSpan.FromSeconds(0.5));
                    image.BeginAnimation(Image.HeightProperty, animation);
            }
        }

        private void Image_MouseLeave(object sender, MouseEventArgs e)
        {
            if (sender is Image image )
            {
                // Tạo animation để thu nhỏ hình ảnh về kích thước ban đầu
                DoubleAnimation animation = new DoubleAnimation(100, TimeSpan.FromSeconds(0.5));
                image.BeginAnimation(Image.WidthProperty, animation);

                animation = new DoubleAnimation(100, TimeSpan.FromSeconds(0.5));
                image.BeginAnimation(Image.HeightProperty, animation);
            }
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
                //set gr tra loi
                GroupPlayManager.Instance.ResetGroup();
                GroupPlayManager.Instance.SetCurrentGroup(groupPlay);
                //tao cau hoi moi
               var question = QuestionManager.Instance.GetRandomQuestion();
                if (question == null)
                {
                    MessageBox.Show("Xin Chúc Mừng Nhóm", "Thông báo");
                    //mo man hinh chuc mung                   
                    WinnerWindow.ShowWinner(GetHighestScoringGroup());
                    return;
                }
            }
            //open game window
            GameWindow window = new GameWindow(_context);
            window.Loaded += (s, evt) =>
            {
                DoubleAnimation fadeInAnimation = new DoubleAnimation
                {
                    From = 0,
                    To = 1,
                    Duration = TimeSpan.FromSeconds(2)
                };
                (s as Window).BeginAnimation(Window.OpacityProperty, fadeInAnimation);
            };

            window.Show();

            DoubleAnimation fadeOutAnimation = new DoubleAnimation
            {
                From = 1,
                To = 0,
                Duration = TimeSpan.FromSeconds(0.5)
            };
            fadeOutAnimation.Completed += (s, evt) => this.Close();
            this.BeginAnimation(Window.OpacityProperty, fadeOutAnimation);
        }
        public GroupPlay? GetHighestScoringGroup()
        {
            return InitialData.Groups.OrderByDescending(g => g.Score).FirstOrDefault();
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            MainWindow window = new MainWindow(_context);
            window.Loaded += (s, evt) =>
            {
                DoubleAnimation fadeInAnimation = new DoubleAnimation
                {
                    From = 0,
                    To = 1,
                    Duration = TimeSpan.FromSeconds(1.5)
                };
                (s as Window).BeginAnimation(Window.OpacityProperty, fadeInAnimation);
            };

            window.Show();

            DoubleAnimation fadeOutAnimation = new DoubleAnimation
            {
                From = 1,
                To = 0,
                Duration = TimeSpan.FromSeconds(1)
            };
            fadeOutAnimation.Completed += (s, evt) => this.Close();
            this.BeginAnimation(Window.OpacityProperty, fadeOutAnimation);
        }
    }

}
