using FlaUI.Core.AutomationElements;
using FlaUI.UIA3;
using System;

namespace CSharpQuizWPF.Tests.PageObjects
{
    public class RegisterPage
    {
        private readonly Window _window;
        public RegisterPage(FlaUI.Core.Application app)
        {
            using (var automation = new UIA3Automation())
            {
                _window = app.GetMainWindow(automation, TimeSpan.FromSeconds(5));
            }
        }

        private TextBox User => _window.FindFirstDescendant(cf => cf.ByAutomationId("txtRegUsername")).AsTextBox();
        private TextBox Pass => _window.FindFirstDescendant(cf => cf.ByAutomationId("txtRegPassword")).AsTextBox();
        private TextBox Confirm => _window.FindFirstDescendant(cf => cf.ByAutomationId("txtRegConfirm")).AsTextBox();
        private Button Submit => _window.FindFirstDescendant(cf => cf.ByAutomationId("btnRegSubmit")).AsButton();
        private Label Status => _window.FindFirstDescendant(cf => cf.ByAutomationId("lblRegStatus")).AsLabel();

        public void FillAndSubmit(string user, string pass, string confirm)
        {
            User.Enter(user);         
            Pass.Enter(pass);          
            Confirm.Enter(confirm);   
            Submit.Click();
        }

        public string GetStatus() => Status.Text;
    }
}