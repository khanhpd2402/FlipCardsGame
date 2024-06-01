using System.Text;
using System.Windows;
using System;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Media.Animation;
using FlipCardsGame.Models;
namespace FlipCardsGame
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly QuizGameDBContext? _context;
        private QuestionManager _questionManager;
        public MainWindow( QuizGameDBContext context)
        {
            InitializeComponent();
            _context = context;
            //load question
            _questionManager = QuestionManager.Instance;
            _questionManager.LoadQuestions(_context.QuizItems.ToList());
        }
        private void Button_MouseEnter(object sender, MouseEventArgs e)
        {
            if (sender is Button button && button.Content is Label label)
            {
                label.FontSize += 5; // Increase font size by 5
                label.Foreground = new SolidColorBrush(Colors.Red); // Change color to Red
            }
        }

        private void Button_MouseLeave(object sender, MouseEventArgs e)
        {
            if (sender is Button button && button.Content is Label label)
            {
                switch (label.Name)
                {
                    case "labelPlayMain":
                        label.FontSize = 45;
                        break;
                    case "labelGuide":
                        label.FontSize = 46;
                        break;
                    case "labelAddQuestion":
                        label.FontSize = 32;
                        break;
                    case "labelQuit":
                        label.FontSize = 45;
                        break;
                }
                label.Foreground = new SolidColorBrush(Colors.AliceBlue); // Change color back to original
            }
        }


        private void btnPlayMain_Click(object sender, RoutedEventArgs e)
        {
            PlayWindow playWindow = new PlayWindow(_context);
            playWindow.Loaded += (s, evt) =>
            {
                DoubleAnimation fadeInAnimation = new DoubleAnimation
                {
                    From = 0,
                    To = 1,
                    Duration = TimeSpan.FromSeconds(1.5)
                };
                (s as Window).BeginAnimation(Window.OpacityProperty, fadeInAnimation);
            };

            playWindow.Show();

            DoubleAnimation fadeOutAnimation = new DoubleAnimation
            {
                From = 1,
                To = 0,
                Duration = TimeSpan.FromSeconds(0.5)
            };
            fadeOutAnimation.Completed += (s, evt) => this.Close();
            this.BeginAnimation(Window.OpacityProperty, fadeOutAnimation);
        }

        private void btnQuit_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Bạn có chắc chắn muốn thoát khỏi ứng dụng không?", "Xác nhận thoát", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                Application.Current.Shutdown();
            }
        }

        private void btnGuide_Click(object sender, RoutedEventArgs e)
        {
            GuideWindow window = new GuideWindow();
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
                Duration = TimeSpan.FromSeconds(0.5)
            };
            fadeOutAnimation.Completed += (s, evt) => this.Close();
            this.BeginAnimation(Window.OpacityProperty, fadeOutAnimation);
        }

        private void btnAddQuestion_Click(object sender, RoutedEventArgs e)
        {
            AddQuestionWindow window = new AddQuestionWindow(_context);
            window.Loaded += (s, evt) =>
            {
                DoubleAnimation fadeInAnimation = new DoubleAnimation
                {
                    From = 0,
                    To = 1,
                    Duration = TimeSpan.FromSeconds(0.5)
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
    }
}