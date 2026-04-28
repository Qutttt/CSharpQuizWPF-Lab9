using System.Windows;
using System.Windows.Controls;
using CSharpQuizWPF.Core;

namespace CSharpQuizWPF
{
    public partial class QuizWindow : Window
    {
        private readonly QuizEngine _quiz = new QuizEngine();

        // ← Конструктор ПРИНИМАЕТ username
        public QuizWindow(string username)
        {
            InitializeComponent();
            lblUser.Text = $"Пользователь: {username}";
        }

        private void StartQuiz(int level)
        {
            if (!_quiz.StartLevel(level)) return;
            ShowQuestion();
            lblResult.Text = string.Empty;
            foreach (var rb in new RadioButton[] { rbOption0, rbOption1, rbOption2, rbOption3 })
                rb.IsEnabled = true;
        }

        private void ShowQuestion()
        {
            var q = _quiz.GetCurrent();
            lblQuestion.Text = $"Вопрос {_quiz.GetCurrentNumber() + 1} из {_quiz.GetTotal()}: {q.Text}";

            var options = new RadioButton[] { rbOption0, rbOption1, rbOption2, rbOption3 };
            for (int i = 0; i < 4; i++)
            {
                options[i].Content = q.Options[i];
                options[i].IsChecked = false;
            }
            btnNext.Content = "Ответить";
            btnNext.IsEnabled = true;
        }

        private void SubmitAnswer()
        {
            var options = new RadioButton[] { rbOption0, rbOption1, rbOption2, rbOption3 };
            int selected = -1;
            for (int i = 0; i < 4; i++)
                if (options[i].IsChecked == true) { selected = i; break; }

            if (selected == -1) return;

            bool correct = _quiz.SubmitAnswer(selected);
            lblResult.Text = correct ? "✅ Верно!" : "❌ Неверно.";

            if (_quiz.HasNext())
                ShowQuestion();
            else
            {
                lblQuestion.Text = "Тест завершён!";
                lblResult.Text = $"Результат: {_quiz.GetScore()} из {_quiz.GetTotal()}";
                btnNext.IsEnabled = false;
                foreach (var rb in options) rb.IsEnabled = false;
            }
        }

        private void BtnLevel1_Click(object sender, RoutedEventArgs e) => StartQuiz(1);
        private void BtnLevel2_Click(object sender, RoutedEventArgs e) => StartQuiz(2);
        private void BtnNext_Click(object sender, RoutedEventArgs e) => SubmitAnswer();

        private void BtnLogout_Click(object sender, RoutedEventArgs e)
        {
            var login = new MainWindow();
            login.Show();
            this.Close();
        }
    }
}