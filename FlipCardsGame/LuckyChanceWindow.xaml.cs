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
namespace FlipCardsGame
{
    /// <summary>
    /// Interaction logic for LuckyChanceWindow.xaml
    /// </summary>
    public partial class LuckyChanceWindow : Window
    {
        private readonly QuizGameDBContext _context;

        public LuckyChanceWindow(QuizGameDBContext context)
        {
            InitializeComponent();
            _context = context;
        }
        public static void ShowChest(QuizGameDBContext context)
        {
            LuckyChanceWindow window = new LuckyChanceWindow(context);
            window.Show();
        }
        private void btnOpenChest_Click(object sender, RoutedEventArgs e)
        {
            // Start the shake animation
            var shakeAnimation = (Storyboard)FindResource("ShakeAnimation");
            shakeAnimation.Completed += ShakeAnimation_Completed;
            shakeAnimation.Begin(imgChestClosed);
            btnOpenChest.Visibility = Visibility.Hidden;
        }

        private void ShakeAnimation_Completed(object sender, EventArgs e)
        {
            // Detach event handler to prevent multiple calls
            var shakeAnimation = (Storyboard)FindResource("ShakeAnimation");
            shakeAnimation.Completed -= ShakeAnimation_Completed;

            // After shaking, open the chest
            imgChestClosed.Visibility = Visibility.Hidden;
            imgChestOpen.Visibility = Visibility.Visible;

            // Start the fade-in animation
            var fadeInAnimation = (Storyboard)FindResource("FadeInAnimation");
            fadeInAnimation.Completed += FadeInAnimation_Completed;
            fadeInAnimation.Begin(imgChestOpen);
            btnContinue.Visibility = Visibility.Visible;
        }

        private void FadeInAnimation_Completed(object sender, EventArgs e)
        {
            // Detach event handler to prevent multiple calls
            var fadeInAnimation = (Storyboard)FindResource("FadeInAnimation");
            fadeInAnimation.Completed -= FadeInAnimation_Completed;

            // Get a random challenge
            var challenge = GetRandomChallenge();
            if (challenge != null)
            {
                HandleReward(challenge.ChallengeType);
                txtChallenge.Text = challenge.Content;
                grChallenge.Visibility = Visibility.Visible;

                // Start fade-in animation for the challenge text
                var challengeFadeIn = new DoubleAnimation(0.0, 1.0, TimeSpan.FromSeconds(1));
                txtChallenge.BeginAnimation(OpacityProperty, challengeFadeIn);
            }
        }
        private void HandleReward(string challengeType)
        {
            // Hiển thị hình ảnh phần thưởng tương ứng với loại phần thưởng
            switch (challengeType.ToLower())
            {
                case "shield":
                    imgitem.Visibility = Visibility.Visible;
                    break;
                case "box":
                    imgitem2.Visibility = Visibility.Visible;
                    break;
                case "crown":
                    imgitem3.Visibility = Visibility.Visible;
                    break;
                case "star":
                    imgitem4.Visibility = Visibility.Visible;
                    break;
                case "magnet":
                    imgitem5.Visibility = Visibility.Visible;
                    break;
                case "money":
                    imgitem6.Visibility = Visibility.Visible;
                    break;
                case "spear":
                    imgitem7.Visibility = Visibility.Visible;
                    break;
                case "sword":
                    imgitem8.Visibility = Visibility.Visible;
                    break;
                case "coins":
                    imgitem9.Visibility = Visibility.Visible;
                    break;
                case "bomb":
                    imgitem10.Visibility = Visibility.Visible;
                    break;
                case "potion":
                    imgitem11.Visibility = Visibility.Visible;
                    break;
                case "card":
                    imgitem12.Visibility = Visibility.Visible;
                    break;
                case "clock":
                    imgitem13.Visibility = Visibility.Visible;
                    break;
                case "diamond":
                    imgitem14.Visibility = Visibility.Visible;
                    break;
                case "coin":
                    imgitem15.Visibility = Visibility.Visible;
                    break;
                default:
                    break;
            }
        }
        private void btnContinue_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        public Challenge GetRandomChallenge()
        {
            var random = new Random();
            int count = InitialData.Challenges.Count();
            if (count == 0)
            {
                return null; // Trường hợp không có Challenge nào trong cơ sở dữ liệu
            }

            while (true)
            {
                int index = random.Next(count);
                var randomChallenge = InitialData.Challenges.Skip(index).FirstOrDefault();
                if (randomChallenge != null)
                {
                    return randomChallenge;
                }
            }
        }
    }
}