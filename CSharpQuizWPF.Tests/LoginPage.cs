using FlaUI.Core.AutomationElements;
using FlaUI.UIA3;
using System;

namespace CSharpQuizWPF.Tests.PageObjects
{
    public class LoginPage
    {
        private readonly FlaUI.Core.Application _app;
        private readonly Window _window;

        public LoginPage(FlaUI.Core.Application app)
        {
            _app = app;
            using (var automation = new UIA3Automation())
            {
                _window = app.GetMainWindow(automation, TimeSpan.FromSeconds(10));
            }
        }

        private TextBox Username => _window.FindFirstDescendant(cf => cf.ByAutomationId("txtUsername")).AsTextBox();
        private TextBox Password => _window.FindFirstDescendant(cf => cf.ByAutomationId("txtPassword")).AsTextBox();
        private Button LoginBtn => _window.FindFirstDescendant(cf => cf.ByAutomationId("btnLogin")).AsButton();
        private Button RegisterBtn => _window.FindFirstDescendant(cf => cf.ByAutomationId("btnRegister")).AsButton();
        private Label ErrorLbl => _window.FindFirstDescendant(cf => cf.ByAutomationId("lblError")).AsLabel();

        public void EnterCredentials(string user, string pass)
        {
            Username.Click();
            Username.Enter(user);      
            Password.Click();
            Password.Enter(pass);    
        }

        public void ClickLogin() => LoginBtn.Click();
        public string GetErrorMessage() => ErrorLbl.Text;

        public void Register(string user, string pass)
        {
            RegisterBtn.Click();
            var reg = new RegisterPage(_app);
            reg.FillAndSubmit(user, pass, pass);
        }
    }
}