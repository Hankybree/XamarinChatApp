using ChatApp.Models.Authentication;
using ChatApp.Services;
using ChatApp.Test.Doubles;
using ChatApp.ViewModels;
using NUnit.Framework;

namespace ChatApp.Test
{
    public class Tests
    {
        private MainPageViewModel _testVm;
        private PreferencesSpy _prefs;
        
        [SetUp]
        public void Setup()
        {
            IAuthApi auth = new AuthFake();
            INavigationService nav = new NavigationStub();
            _prefs = new PreferencesSpy();
            _testVm = new MainPageViewModel(_prefs, nav, auth);
        }

        [Test]
        public void TestLoginSuccessSetsPrefs()
        {
            _testVm.LogInButtonPressed.Execute(null);
            
            Assert.AreEqual(_prefs.Token, "Token");
            Assert.AreEqual(_prefs.User, "User");
        }
    }
}