using System;
using System.Windows;
using System.Windows.Media;
using CSharpQuizWPF.Core;

namespace CSharpQuizWPF
{
    public partial class RegisterWindow : Window
    {
        public string RegisteredUsername { get; private set; }
        private readonly AuthManager _auth;

        public RegisterWindow(AuthManager auth)
        {
            InitializeComponent();
            _auth = auth;
            // ❌ Не ставим Owner вручную — ShowDialog() сделает это за нас
        }

        private void BtnRegSubmit_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var username = txtRegUsername.Text.Trim();
                var password = txtRegPassword.Password;
                var confirm = txtRegConfirm.Password;

                if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
                    throw new ArgumentException("Все поля обязательны");

                if (password != confirm)
                    throw new ArgumentException("Пароли не совпадают");

                _auth.Register(username, password);

                lblRegStatus.Foreground = Brushes.Green;
                lblRegStatus.Text = "Успешно!";
                RegisteredUsername = username;

                this.DialogResult = true;
            }
            catch (Exception ex)
            {
                lblRegStatus.Foreground = Brushes.Red;
                lblRegStatus.Text = ex.Message;
            }
        }
    }
}