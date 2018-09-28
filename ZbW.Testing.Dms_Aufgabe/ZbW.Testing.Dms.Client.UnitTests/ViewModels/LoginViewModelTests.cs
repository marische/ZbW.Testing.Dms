using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;
using FakeItEasy;
using NUnit.Framework;
using Prism.Commands;
using Prism.Mvvm;
using ZbW.Testing.Dms.Client.Views;

namespace ZbW.Testing.Dms.Client.UnitTests
{
    using ViewModels;

    [TestFixture]
    internal class LoginViewModelTests
    {
        private const string BENUTZER = "Testuser";

        public interface IButton
        {
            event EventHandler Buttonhandler;
        }

        [Test]
        public void login_noUsername_isDisabled()
        {
            //arrange
            var loginViewModel = new LoginViewModel(null);

            //act
            var result = loginViewModel.CmdLogin.CanExecute();

            //assert
            Assert.That(result, Is.False);        
         }

        [Test]
        public void login_Username_isEnabled()
        {
            //arrange
            var loginViewModel = new LoginViewModel(null);

            //act
            loginViewModel.Benutzername = BENUTZER;
            var result = loginViewModel.CmdLogin.CanExecute();
            
            //assert
            Assert.That(result, Is.True);
        }
    }
}
