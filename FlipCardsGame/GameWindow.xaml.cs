﻿using System;
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
using System.Windows.Threading;

namespace FlipCardsGame
{
    /// <summary>
    /// Interaction logic for GameWindow.xaml
    /// </summary>
    public partial class GameWindow : Window
    {
        private readonly QuizGameDBContext _context;

        private DispatcherTimer _timer;
        private int _timeLeft;
        public GameWindow(QuizGameDBContext context)
        {
            InitializeComponent();
            InitializeTimer();
            _context = context;
            HandGroupName();
        }

        private void InitializeTimer()
        {
            _timer = new DispatcherTimer();
            _timer.Interval = TimeSpan.FromSeconds(1);
            _timer.Tick += Timer_Tick;
            _timeLeft = 60;
            UpdateTimerText();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            if (_timeLeft > 0)
            {
                _timeLeft--;
                UpdateTimerText();
            }
            else
            {
                _timer.Stop();
                txtTimer.Visibility = Visibility.Hidden;
                imgHourglass.Visibility = Visibility.Hidden;
            }
        }

        private void UpdateTimerText()
        {
            txtTimer.Text = _timeLeft.ToString();
        }

        private void btnStart_Click(object sender, RoutedEventArgs e)
        {
            _timeLeft = 60;
            UpdateTimerText();
            HandleQuestion();
            _timer.Start();
            StartHourglassAnimation();
            txtTimer.Visibility = Visibility.Visible;
            imgHourglass.Visibility = Visibility.Visible;
            btnSubmit.Visibility = Visibility.Visible;
        }

        private void StartHourglassAnimation()
        {
            var rotateAnimation = new DoubleAnimation
            {
                From = 0,
                To = 360,
                Duration = TimeSpan.FromSeconds(1),
                RepeatBehavior = RepeatBehavior.Forever
            };

            hourglassRotateTransform.BeginAnimation(RotateTransform.AngleProperty, rotateAnimation);
        }

        public void HandGroupName()
        {
            var groupPlay = GroupPlayManager.Instance.CurrentGroup;
            var newGroupPlay = GroupPlayManager.Instance.NewGroup;
            // Kiểm tra nếu newGroupPlay có giá trị hợp lệ
            if (newGroupPlay != null && !string.IsNullOrWhiteSpace(newGroupPlay.GroupName))
            {
                // Hiển thị thông tin của newGroupPlay
                txtGrName.Text = newGroupPlay.GroupName + " | " + newGroupPlay.Score;
            }
            else if (groupPlay != null && !string.IsNullOrWhiteSpace(groupPlay.GroupName))
            {
                // Nếu newGroupPlay không hợp lệ, hiển thị thông tin của CurrentGroup
                txtGrName.Text = groupPlay.GroupName + " | " + groupPlay.Score;
            }

        }

        private void HandleQuestion()
        {
            var question = QuestionManager.Instance.GetCurrentQuestion();

            // Hiển thị câu hỏi
            txblQuestion.Text = question.QuestionText;

            // Hiển thị các câu trả lời
            txblAnwserA.Text = question.AnswerA;
            txblAnwserB.Text = question.AnswerB;
            txblAnwserC.Text = question.AnswerC;
            txblAnwserD.Text = question.AnswerD;

        }
        private void btnBack_Click(object sender, RoutedEventArgs e)
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
                Duration = TimeSpan.FromSeconds(1)
            };
            fadeOutAnimation.Completed += (s, evt) => this.Close();
            this.BeginAnimation(Window.OpacityProperty, fadeOutAnimation);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            // Lấy button được click
            Button clickedButton = sender as Button;

            // Ẩn tất cả các hình ảnh
            imgAnwserA.Visibility = Visibility.Hidden;
            imgAnwserB.Visibility = Visibility.Hidden;
            imgAnwserC.Visibility = Visibility.Hidden;
            imgAnwserD.Visibility = Visibility.Hidden;

            // Hiện hình ảnh tương ứng với button được click
            switch (clickedButton.Name)
            {
                case "btnAnwserA":
                    imgAnwserA.Visibility = Visibility.Visible;
                    break;
                case "btnAnwserB":
                    imgAnwserB.Visibility = Visibility.Visible;
                    break;
                case "btnAnwserC":
                    imgAnwserC.Visibility = Visibility.Visible;
                    break;
                case "btnAnwserD":
                    imgAnwserD.Visibility = Visibility.Visible;
                    break;
                default:
                    break;
            }
        }

        private void btnGrName_Click(object sender, RoutedEventArgs e)
        {
            ChooseGroupWindow.Show(this);
        }

        private void btnContinue_Click(object sender, RoutedEventArgs e)
        {
            QuestionManager.Instance.RemoveQuestion(QuestionManager.Instance.GetCurrentQuestion());
            //mo playwindow neu ko doi nao trả loi dung
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
        private void btnChest_Click(object sender, RoutedEventArgs e)
        {
            QuestionManager.Instance.RemoveQuestion(QuestionManager.Instance.GetCurrentQuestion());
            //mo chest
            LuckyChanceWindow window = new LuckyChanceWindow(_context);
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
                Duration = TimeSpan.FromSeconds(2)
            };
            fadeOutAnimation.Completed += (s, evt) => this.Close();
            this.BeginAnimation(Window.OpacityProperty, fadeOutAnimation);
        }
        private void btnSubmit_Click(object sender, RoutedEventArgs e)
        {
            bool result = CustomMessageBox.Show("Bạn chắc chưa :))?");
            var newGroupPlay = GroupPlayManager.Instance.NewGroup;
            if (result)
            {
                string selectedAnswer = "";
                var clickedButton = btnSubmit;
                if (imgAnwserA.Visibility == Visibility.Visible)
                {
                    selectedAnswer = txblAnwserA.Text;
                    clickedButton = btnAnwserA;
                }
                else if (imgAnwserB.Visibility == Visibility.Visible)
                {
                    selectedAnswer = txblAnwserB.Text;
                    clickedButton = btnAnwserB;
                }
                else if (imgAnwserC.Visibility == Visibility.Visible)
                {
                    selectedAnswer = txblAnwserC.Text;
                    clickedButton = btnAnwserC;
                }
                else if (imgAnwserD.Visibility == Visibility.Visible)
                {
                    selectedAnswer = txblAnwserD.Text;
                    clickedButton = btnAnwserD;
                }
                else
                {
                    CustomMessageBox.Show("Bạn cần chọn 1 câu trả lời");
                    return;
                }
                
                // So sánh câu trả lời đã chọn với câu trả lời đúng
                var currentQuestion = QuestionManager.Instance.GetCurrentQuestion(); // Giả sử bạn có phương thức này để lấy câu hỏi hiện tại
                if (currentQuestion != null)
                {
                    bool isCorrect = selectedAnswer.Equals(currentQuestion.AnswerCorrect);

                    if (isCorrect)
                    {
                        clickedButton.BorderBrush = Brushes.Green;
                        clickedButton.BorderThickness = new Thickness(10);

                        // Nhóm đầu tiên trả lời đúng
                        if (newGroupPlay == null || string.IsNullOrWhiteSpace(newGroupPlay.GroupName))
                        {
                            GroupPlayManager.Instance.UpdateScoreCurrentGroup(10);
                        }
                        // Nhóm thứ hai trả lời đúng
                        else
                        {
                            GroupPlayManager.Instance.UpdateScoreNewGroup(10);
                        }
                        btnSubmit.Visibility = Visibility.Hidden;
                        btnContinue.Visibility = Visibility.Hidden;
                        btnChest.Visibility = Visibility.Visible;
                    }
                    else
                    {
                        clickedButton.BorderBrush = Brushes.Red;
                        clickedButton.BorderThickness = new Thickness(10);

                        // Nhóm đầu tiên trả lời sai
                        if (newGroupPlay == null || string.IsNullOrWhiteSpace(newGroupPlay.GroupName))
                        {
                            GroupPlayManager.Instance.UpdateScoreCurrentGroup(-10);
                            btnGrName.Click += btnGrName_Click;
                        }
                        // Nhóm thứ hai trả lời sai
                        else
                        {
                            GroupPlayManager.Instance.UpdateScoreCurrentGroup(10);
                            GroupPlayManager.Instance.UpdateScoreNewGroup(-20);
                        }
                    }

                    this.HandGroupName();
                    this.Show();
                }
            }
        }
    }
}
