using Microsoft.VisualStudio.TestTools.UnitTesting;
using CSharpQuizWPF.Core;

namespace CSharpQuizWPF.Tests
{
    [TestClass]
    public class PasswordPolicyTests
    {
        private AuthManager _auth;

        [TestInitialize]
        public void Init() => _auth = new AuthManager();

       
        [TestMethod]
        public void IsPasswordStrong_ValidPassword_ReturnsTrue()
        {
           
            string strongPassword = "SecurePass1";         
            bool result = _auth.IsPasswordStrong(strongPassword);      
            Assert.IsTrue(result, "Надёжный пароль должен возвращать true");
        }
        [TestMethod]
        public void IsPasswordStrong_ShortPassword_ReturnsFalse()
        {
            string weakPassword = "123";
            bool result = _auth.IsPasswordStrong(weakPassword);
            Assert.IsFalse(result, "Короткий пароль должен возвращать false");
        }
        [TestMethod]
        public void IsPasswordStrong_NoDigit_ReturnsFalse()
        {
            string weakPassword = "abcdefgh"; 
            bool result = _auth.IsPasswordStrong(weakPassword);
            Assert.IsFalse(result, "Пароль без цифр должен возвращать false");
        }
    }
}