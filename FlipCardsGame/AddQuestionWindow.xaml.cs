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
    /// Interaction logic for AddQuestionWindow.xaml
    /// </summary>
    public partial class AddQuestionWindow : Window
    {
        private readonly QuizGameDBContext _context;
        private List<QuizItem>? quizItems;
        public AddQuestionWindow(QuizGameDBContext context)
        {
            InitializeComponent();
            _context = context;
            HandleLoadGirdView();
        }

        private void HandleLoadGirdView()
        {
            quizItems = _context.QuizItems.ToList();
            dataGridQuestions.ItemsSource = quizItems;

        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtQuestion.Text) ||
                string.IsNullOrWhiteSpace(txtAnswerA.Text) ||
                string.IsNullOrWhiteSpace(txtAnswerB.Text) ||
                string.IsNullOrWhiteSpace(txtAnswerC.Text) ||
                string.IsNullOrWhiteSpace(txtAnswerD.Text) ||
                string.IsNullOrWhiteSpace(txtAnswerCorrect.Text))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin cho tất cả các trường.", "Thông báo");
                return;
            }
            //neu du cac dieu kien thi add
            var newQuestion = new QuizItem
            {
                QuestionText = txtQuestion.Text,
                AnswerA = txtAnswerA.Text,
                AnswerB = txtAnswerB.Text,
                AnswerC = txtAnswerC.Text,
                AnswerD = txtAnswerD.Text,
                AnswerCorrect = txtAnswerCorrect.Text
            };

            MessageBox.Show("Thêm câu hỏi thành công!", "Thông báo");
            _context.QuizItems.Add(newQuestion);
            _context.SaveChanges();

            HandleLoadGirdView();
            ClearFields();
        }

        private void CbCorrectAnswer_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cbCorrectAnswer.SelectedItem is ComboBoxItem selectedItem)
            {
                switch (selectedItem.Content.ToString())
                {
                    case "A":
                        txtAnswerCorrect.Text = txtAnswerA.Text;
                        break;
                    case "B":
                        txtAnswerCorrect.Text = txtAnswerB.Text;
                        break;
                    case "C":
                        txtAnswerCorrect.Text = txtAnswerC.Text;
                        break;
                    case "D":
                        txtAnswerCorrect.Text = txtAnswerD.Text;
                        break;
                }
            }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (dataGridQuestions.SelectedItem is QuizItem selectedQuestion)
            {
                var result = MessageBox.Show("Bạn có chắc muốn xóa câu hỏi này không?", "Xác nhận xóa", MessageBoxButton.YesNo, MessageBoxImage.Warning);

                if (result == MessageBoxResult.Yes)
                {
                    _context.QuizItems.Remove(selectedQuestion);
                    _context.SaveChanges();

                    HandleLoadGirdView();
                    ClearFields();
                }
            }
        }

        //private void btnSearch_Click(object sender, RoutedEventArgs e)
        //{
        //    var searchText = txtQuestion.Text.ToLower();
        //    var searchResults = _context.QuizItems.Where(q => q.QuestionText.ToLower().Contains(searchText)).ToList();
        //    dataGridQuestions.ItemsSource = searchResults;
        //}

        private void ClearFields()
        {
            txtQuestion.Clear();
            txtAnswerA.Clear();
            txtAnswerB.Clear();
            txtAnswerC.Clear();
            txtAnswerD.Clear();
            txtAnswerCorrect.Clear();
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
                label.FontSize = 45; // Increase font size by 5
                label.Foreground = new SolidColorBrush(Colors.AliceBlue); // Change color back to original
            }
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
