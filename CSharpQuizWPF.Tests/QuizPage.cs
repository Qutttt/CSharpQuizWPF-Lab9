using FlaUI.Core.AutomationElements; 
using FlaUI.UIA3;
using System;

namespace CSharpQuizWPF.Tests.PageObjects
{
    public class QuizPage
    {
        private readonly Window _window;
        public QuizPage(FlaUI.Core.Application app)
        {
            using (var automation = new UIA3Automation())
            {
                _window = app.GetMainWindow(automation, TimeSpan.FromSeconds(10));
            }
        }

        public Button Level1 => _window.FindFirstDescendant(cf => cf.ByAutomationId("btnLevel1")).AsButton();
        public Button Level2 => _window.FindFirstDescendant(cf => cf.ByAutomationId("btnLevel2")).AsButton();

        public Label Question => _window.FindFirstDescendant(cf => cf.ByAutomationId("lblQuestion")).AsLabel();
        public Button Next => _window.FindFirstDescendant(cf => cf.ByAutomationId("btnNext")).AsButton();
        public Label Result => _window.FindFirstDescendant(cf => cf.ByAutomationId("lblResult")).AsLabel();
        public Button Logout => _window.FindFirstDescendant(cf => cf.ByAutomationId("btnLogout")).AsButton();

        public void SelectOption(int index)
        {
            var rb = _window.FindFirstDescendant(cf => cf.ByAutomationId($"rbOption{index}")).AsRadioButton();
            rb.Click();
        }

        public bool IsQuizActive() => Next.IsEnabled;
    }
}