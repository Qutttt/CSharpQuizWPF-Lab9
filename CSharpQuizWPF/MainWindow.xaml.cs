using System.Windows;
using CSharpQuizWPF.Core;

namespace CSharpQuizWPF
{
    public partial class MainWindow : Window
    {
        private readonly AuthManager _auth = new AuthManager();

        public MainWindow()  // ← Конструктор БЕЗ параметров!
        {
            InitializeComponent();
        }

        private void BtnLogin_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var username = txtUsername.Text.Trim();
                var password = txtPassword.Password;

                if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
                {
                    lblError.Text = "Заполните все поля";
                    return;
                }

                _auth.Login(username, password);

                // ✅ Успех → открываем окно теста с передачей имени
                var quiz = new QuizWindow(username);  // ← QuizWindow принимает username
                quiz.Show();
                this.Close();  // Закрываем окно входа
            }
            catch (System.Exception ex)
            {
                lblError.Text = ex.Message;
            }
        }

        private void BtnRegister_Click(object sender, RoutedEventArgs e)
        {
            var reg = new RegisterWindow(_auth) { Owner = this };
            reg.ShowDialog();
        }
    }
}