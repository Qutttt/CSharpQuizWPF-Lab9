using Microsoft.VisualStudio.TestTools.UnitTesting;
using CSharpQuizWPF.Tests.PageObjects;
using System;
using System.IO;
using System.Threading;

namespace CSharpQuizWPF.Tests
{
    [TestClass]
    public class QuizUITests
    {
        private FlaUI.Core.Application _app;

        [TestInitialize]
        public void Setup()
        {           
            var baseDir = AppDomain.CurrentDomain.BaseDirectory;
            var exePath = Path.Combine(baseDir, @"G:\TSU\8 семестр\ПОКПО (ДЗ) - Пестин\ЛР\7ЛР\CSharpQuizWPF\CSharpQuizWPF\bin\Debug\CSharpQuizWPF.exe");         
            if (!File.Exists(exePath))
                throw new FileNotFoundException($"Приложение не найдено по пути: {exePath}");
            _app = FlaUI.Core.Application.Launch(exePath);
        }

      
        [TestCleanup]
        public void TearDown()
        {
            if (_app != null && !_app.HasExited)
            {
                _app.Close();
            }
        }

      
        [TestMethod]
        public void T1_SuccessfulLogin()
        {
            var loginPage = new LoginPage(_app);          
            loginPage.Register("autotest_user", "Pass123");         
            loginPage.EnterCredentials("autotest_user", "Pass123");
            loginPage.ClickLogin();         
            Thread.Sleep(1000);
            var quizPage = new QuizPage(_app);
            Assert.IsTrue(quizPage.Level1.IsEnabled, "Окно теста не открылось, кнопка уровня неактивна");
            quizPage.Logout.Click();
        }

        // Сценарий 2: Неверный пароль
        [TestMethod]
        public void T2_WrongPasswordShowsError()        {
            var loginPage = new LoginPage(_app);
            loginPage.EnterCredentials("non_existing_user", "WrongPass");
            loginPage.ClickLogin();                   
            string errorText = loginPage.GetErrorMessage();
            Assert.IsTrue(errorText.Contains("Неверное имя пользователя или пароль"),
                $"Ожидалась ошибка 'Неверное имя...', а получено: '{errorText}'");
        }

        // Сценарий 3: Регистрация и запуск теста
        [TestMethod]
        public void T3_RegisterAndStartQuiz()
        {
            var loginPage = new LoginPage(_app);
            loginPage.Register("quiz_tester", "StrongPass1");           
            loginPage.EnterCredentials("quiz_tester", "StrongPass1");
            loginPage.ClickLogin();
            Thread.Sleep(1000);
            var quizPage = new QuizPage(_app);        
            quizPage.Level1.Click();           
            Assert.IsTrue(quizPage.IsQuizActive(), "Тест не запустился после нажатия кнопки уровня");
            quizPage.Logout.Click();
        }
    }
}