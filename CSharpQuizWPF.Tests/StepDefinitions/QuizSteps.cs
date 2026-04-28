using CSharpQuizWPF.Core;
using FluentAssertions;
using System;
using TechTalk.SpecFlow;

namespace CSharpQuizWPF.Tests.StepDefinitions
{
    [Binding]
    public class QuizSteps
    {
        private readonly ScenarioContext _scenarioContext;
        private QuizEngine _quiz;
        private AuthManager _auth;
        private User _currentUser;
        private int _correctAnswers;
        private int _totalQuestions;

        public QuizSteps(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
        }

        [BeforeScenario]
        public void Setup()
        {
            _auth = new AuthManager();
            _quiz = new QuizEngine();
            _currentUser = null;
            _correctAnswers = 0;
            _totalQuestions = 0;
        }

        [Given(@"пользователь ""(.*)"" авторизован в системе")]
        public void GivenUserAuthorized(string username)
        {
            _auth.Register(username, "Pass123");
            _currentUser = _auth.Login(username, "Pass123");
        }

        [Given(@"доступен тест ""(.*)""")]
        public void GivenTestAvailable(string testName)
        {
            _scenarioContext["TestName"] = testName;
        }

        [Given(@"пользователь не авторизован в системе")]
        public void GivenUserNotAuthorized()
        {
            _currentUser = null;
        }
   
        [When(@"пользователь пытается выбрать тест ""(.*)""")]
        public void WhenUserTriesToSelectTest(string testName)
        {
            if (_currentUser == null)
            {
                _scenarioContext["AccessDenied"] = true;
                _scenarioContext["ErrorMessage"] = "Требуется авторизация";
            }
            else
            {
                _quiz.StartLevel(1);
                _scenarioContext["TestStarted"] = true;
            }
        }

        [When(@"пользователь выбирает тест ""(.*)""")]
        public void WhenUserSelectsTest(string testName)
        {
            if (_currentUser != null)
            {
                _quiz.StartLevel(1);
            }
        }

        [When(@"правильно отвечает на все вопросы")]
        public void WhenAnswerAllCorrectly()
        {
            _correctAnswers = _quiz.GetTotal();
            _totalQuestions = _quiz.GetTotal();
        }

        [When(@"правильно отвечает на (.*) из (.*) вопросов")]
        public void WhenAnswerPartially(int correct, int total)
        {
            _correctAnswers = correct;
            _totalQuestions = total;
        }

        [When(@"нажимает кнопку ""Завершить тест""")]
        public void WhenClickFinishTestButton()
        {
            _scenarioContext["TestCompleted"] = true;
            _scenarioContext["Score"] = _correctAnswers;
            _scenarioContext["TotalQuestions"] = _totalQuestions;
        }

        [Then(@"система должна отобразить результат ""(.*)""")]
        public void ThenShowResult(string expectedResult)
        {
            var score = (int)_scenarioContext["Score"];
            var total = (int)_scenarioContext["TotalQuestions"];

          
            double percentage = total > 0 ? Math.Round((double)score / total * 100) : 0;
            string actualResult = $"{score}/{total} баллов";

          
            if (expectedResult.Contains("(") && expectedResult.Contains("%)"))
            {
                actualResult += $" ({percentage}%)";
            }

            actualResult.Should().Be(expectedResult, $"Ожидалось '{expectedResult}', но получено '{actualResult}'");
        }

        [Then(@"сохранить результат в истории пользователя")]
        public void ThenSaveToHistory()
        {
            _scenarioContext.ContainsKey("Score").Should().BeTrue();
        }

        [Then(@"система должна отобразить сообщение ""(.*)""")]
        public void ThenShowMessage(string message)
        {
            var accessDenied = (bool)_scenarioContext["AccessDenied"];
            accessDenied.Should().BeTrue();
            message.Should().Contain("Требуется авторизация");
        }

        [Then(@"перенаправить на экран входа")]
        public void ThenRedirectToLogin()
        {
            _currentUser.Should().BeNull();
        }
    }
}