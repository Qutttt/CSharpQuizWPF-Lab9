using CSharpQuizWPF.Core;
using FluentAssertions;
using System;
using TechTalk.SpecFlow;

namespace CSharpQuizWPF.Tests.StepDefinitions
{
    [Binding]
    public class AuthorizationSteps
    {
        private readonly ScenarioContext _scenarioContext;
        private AuthManager _auth;
        private User _currentUser;
        private Exception _lastException;

        public AuthorizationSteps(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
        }

        [BeforeScenario]
        public void Setup()
        {
            _auth = new AuthManager();
            _currentUser = null;
            _lastException = null;
        }

        [Given(@"пользователь зарегистрирован с именем ""(.*)"" и паролем ""(.*)""")]
        public void GivenUserRegistered(string username, string password)
        {
            _auth.Register(username, password);
        }

        [Given(@"в системе нет пользователя с именем ""(.*)""")]
        public void GivenNoUserExists(string username)
        {
           
        }

        [When(@"пользователь вводит логин ""(.*)"" и пароль ""(.*)""")]
        public void WhenUserEntersCredentials(string username, string password)
        {
            _scenarioContext["Username"] = username;
            _scenarioContext["Password"] = password;
        }

    
        [When(@"нажимает кнопку ""Войти""")]
        public void WhenClickLoginButton()
        {
            try
            {
                _currentUser = _auth.Login(
                    _scenarioContext["Username"].ToString(),
                    _scenarioContext["Password"].ToString());
            }
            catch (Exception ex)
            {
                _lastException = ex;
            }
        }

        [Then(@"система должна отобразить главное окно с приветствием ""(.*)""")]
        public void ThenWelcomeMessageShown(string expectedWelcome)
        {
            _currentUser.Should().NotBeNull("Пользователь должен быть авторизован");
            _currentUser.Username.Should().Be(expectedWelcome.Replace("Пользователь: ", ""));
        }

        [Then(@"кнопка ""(.*)"" должна быть доступна")]
        public void ThenButtonShouldBeAvailable(string buttonName)
        {
            _currentUser.Should().NotBeNull();
        }

        [Then(@"система должна отобразить сообщение об ошибке ""(.*)""")]
        public void ThenErrorMessageShown(string expectedError)
        {
            _lastException.Should().NotBeNull("Ожидалось исключение");
            _lastException.Message.Should().Contain(expectedError);
        }

        [Then(@"пользователь должен остаться на экране авторизации")]
        public void ThenUserStaysOnLoginScreen()
        {
            _currentUser.Should().BeNull("Пользователь не должен быть авторизован");
        }
    }
}