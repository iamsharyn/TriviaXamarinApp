using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;
using TriviaXamarinApp.Services;
using TriviaXamarinApp.Models;
using System.Threading.Tasks;

namespace TriviaXamarinApp.ViewModels
{
    public class LoginVM
    {
        public string Email { get; set; }
        public string Pass { get; set; }

        public ICommand LoginCommand { get; }

        public LoginVM()
        {
            LoginCommand = new Command(Login);
        }

        private async void Login()
        {
            //Task<User> loginTask = TriviaWebAPIProxy.
        }

    }
}
