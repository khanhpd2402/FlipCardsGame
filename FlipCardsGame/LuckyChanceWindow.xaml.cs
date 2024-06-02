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
using System.Numerics;
using System.Text.RegularExpressions;

namespace FlipCardsGame
{
    /// <summary>
    /// Interaction logic for LuckyChanceWindow.xaml
    /// </summary>
    public partial class LuckyChanceWindow : Window
    {
        private readonly QuizGameDBContext _context;
        GroupPlay? _group;
        Challenge? _challenge;
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
            btnItemShow.Visibility = Visibility.Visible;
            btnGrShow.Visibility = Visibility.Visible;
        }

        private void FadeInAnimation_Completed(object sender, EventArgs e)
        {
            // Detach event handler to prevent multiple calls
            var fadeInAnimation = (Storyboard)FindResource("FadeInAnimation");
            fadeInAnimation.Completed -= FadeInAnimation_Completed;

            // Get a random challenge
            _challenge = GetRandomChallenge();
            HandleReward(_challenge.ChallengeType);
            txtChallenge.Text = _challenge.Content;
            grChallenge.Visibility = Visibility.Visible;

            // Start fade-in animation for the challenge text
            var challengeFadeIn = new DoubleAnimation(0.0, 1.0, TimeSpan.FromSeconds(1));
            txtChallenge.BeginAnimation(OpacityProperty, challengeFadeIn);
        }

        private void HandleReward(string challengeType)
        {
            // Hiển thị hình ảnh phần thưởng tương ứng với loại phần thưởng
            switch (challengeType.ToLower())
            {
                case "shield":
                    shield.Visibility = Visibility.Visible;
                    break;
                case "box":
                    box.Visibility = Visibility.Visible;
                    break;
                case "crown":
                    crown.Visibility = Visibility.Visible;
                    break;
                case "star":
                    star.Visibility = Visibility.Visible;
                    break;
                case "magnet":
                    magnet.Visibility = Visibility.Visible;
                    break;
                case "money":
                    money.Visibility = Visibility.Visible;
                    break;
                case "spear":
                    spear.Visibility = Visibility.Visible;
                    break;
                case "sword":
                    sword.Visibility = Visibility.Visible;
                    break;
                case "coins":
                    coins.Visibility = Visibility.Visible;
                    break;
                case "bomb":
                    bomb.Visibility = Visibility.Visible;
                    break;
                case "potion":
                    potion.Visibility = Visibility.Visible;
                    break;
                case "card":
                    card.Visibility = Visibility.Visible;
                    break;
                case "clock":
                    clock.Visibility = Visibility.Visible;
                    break;
                case "diamond":
                    diamond.Visibility = Visibility.Visible;
                    break;
                case "coin":
                    coin.Visibility = Visibility.Visible;
                    break;
                default:
                    break;
            }
        }
        private void btnContinue_Click(object sender, RoutedEventArgs e)
        {
            // Lấy ra nhóm chơi
            var groupLucky = GroupPlayManager.Instance.NewGroup;
            if (groupLucky == null || string.IsNullOrWhiteSpace(groupLucky.GroupName))
            {
                groupLucky = GroupPlayManager.Instance.CurrentGroup;
            }

            // Xử lý điểm
            if (_challenge != null)
            {
                // Hiển thị điểm trước khi thay đổi
                MessageBox.Show($"Before: Current Group Score: {GroupPlayManager.Instance.CurrentGroup.Score}, New Group Score: {GroupPlayManager.Instance.NewGroup?.Score}");

                string challengeType = _challenge.ChallengeType.ToLower(); // Đảm bảo không phân biệt hoa thường

                if (challengeType == "coin" || challengeType == "coins" || challengeType == "diamond")
                {
                    HandScore.AddPoints(groupLucky.GroupName, _challenge.ChallengeValue);
                }
                else if (challengeType == "spear")
                {
                    HandScore.SubtractPoints(groupLucky.GroupName, _challenge.ChallengeValue);
                }
                else if (challengeType == "box")
                {
                    if (_group != null)
                        HandScore.TransferPointsBetweenTeams(_group.GroupName, groupLucky.GroupName, _challenge.ChallengeValue);
                }
                else if (challengeType == "crown" || challengeType == "magnet")
                {
                    if (_group != null)
                        HandScore.TransferPointsBetweenTeams(groupLucky.GroupName, _group.GroupName, _challenge.ChallengeValue);
                }
                else if (challengeType == "card")
                {
                    var toGroup = InitialData.GetHighestScoringGroup();
                    var temp = toGroup.Score;
                    toGroup.Score = groupLucky.Score;
                    groupLucky.Score = temp;
                }

                // Hiển thị điểm sau khi thay đổi
                MessageBox.Show($"After: Current Group Score: {GroupPlayManager.Instance.CurrentGroup.Score}, New Group Score: {GroupPlayManager.Instance.NewGroup?.Score}");
            }

            this.Close();

            // Mở PlayWindow
            PlayWindow window = new PlayWindow(_context);
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

        private void btnItemShow_Click(object sender, RoutedEventArgs e)
        {
            if (btnitemSaved1.Visibility == Visibility.Hidden && btnitemSaved2.Visibility == Visibility.Hidden)
            {
                btnitemSaved1.Visibility = Visibility.Visible;
            }
            else if (btnitemSaved1.Visibility == Visibility.Visible && btnitemSaved2.Visibility == Visibility.Hidden)
            {
                btnitemSaved1.Visibility = Visibility.Hidden;
                btnitemSaved2.Visibility = Visibility.Visible;
            }
            else if (btnitemSaved1.Visibility == Visibility.Hidden && btnitemSaved2.Visibility == Visibility.Visible)
            {
                btnitemSaved1.Visibility = Visibility.Hidden;
                btnitemSaved2.Visibility = Visibility.Hidden;
            }
        }

        private void btnitemSaved2_Click(object sender, RoutedEventArgs e)
        {
            if (_challenge != null && _challenge.ChallengeType.Equals("magnet"))
            {
                //lay ra gr choi
                var groupLucky = GroupPlayManager.Instance.NewGroup;
                if (groupLucky == null || string.IsNullOrWhiteSpace(groupLucky.GroupName))
                {
                    groupLucky = GroupPlayManager.Instance.CurrentGroup;
                }
                HandScore.AddPoints(groupLucky.GroupName, _challenge.ChallengeValue);
            }
            this.Close();
        }

        private void btnitemSaved1_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

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
            _group = GroupPlayManager.Instance.GetGroupByName(grName);
            grGr.Visibility = Visibility.Hidden;
        }

        private void btnGrShow_Click(object sender, RoutedEventArgs e)
        {
            if (grGr.Visibility == Visibility.Hidden)
            {
                HandleGroup();
                grGr.Visibility = Visibility.Visible;
            }
            else
            {
                grGr.Visibility = Visibility.Hidden;
            }
        }
        private void HandleGroup()
        {
            var groupPlay = GroupPlayManager.Instance.NewGroup;
            if (groupPlay == null)
            {
                groupPlay = GroupPlayManager.Instance.CurrentGroup;
            }
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
    }
}