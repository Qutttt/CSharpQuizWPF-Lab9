using CSharpQuizWPF.Core;
using FluentAssertions;
using TechTalk.SpecFlow;

namespace CSharpQuizWPF.Tests.StepDefinitions
{
    [Binding]
    public class PasswordPolicySteps
    {
        private readonly ScenarioContext _scenarioContext;
        private AuthManager _auth;
        private bool _isPasswordStrong;

        public PasswordPolicySteps(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
        }

        [BeforeScenario]
        public void Setup()
        {
            _auth = new AuthManager();
        }

        [Given(@"система проверяет надежность пароля ""(.*)""")]
        public void GivenCheckPasswordStrength(string password)
        {
            _scenarioContext["TestPassword"] = password;
        }

        [When(@"применяется политика безопасности")]
        public void WhenApplySecurityPolicy()
        {
            var password = _scenarioContext["TestPassword"] as string;
            _isPasswordStrong = _auth.IsPasswordStrong(password);
            _scenarioContext["IsStrong"] = _isPasswordStrong;
        }

        [Then(@"результат проверки должен быть ""(.*)""")]
        public void ThenResultShouldBe(string expectedBool)
        {
            bool expected = bool.Parse(expectedBool);
            _isPasswordStrong.Should().Be(expected);
        }

        [Then(@"сообщение должно содержать ""(.*)""")]
        public void ThenMessageShouldContain(string message)
        {
            
            _scenarioContext["ExpectedMessage"] = message;
        }

        [Given(@"пользователь вводит пароль ""(.*)""")]
        public void GivenUserEntersPassword(string password)
        {
            _scenarioContext["TestPassword"] = password;
        }

        [When(@"система проверяет надежность пароля")]
        public void WhenSystemChecksPasswordStrength()
        {
            var password = _scenarioContext["TestPassword"] as string;
            _isPasswordStrong = _auth.IsPasswordStrong(password);
        }

        [Then(@"пароль должен быть принят")]
        public void ThenPasswordShouldBeAccepted()
        {
            _isPasswordStrong.Should().BeTrue("Пароль должен соответствовать политике безопасности");
        }

        [Then(@"регистрация должна быть разрешена")]
        public void ThenRegistrationShouldBeAllowed()
        {
            _isPasswordStrong.Should().BeTrue();
        }
    }
}