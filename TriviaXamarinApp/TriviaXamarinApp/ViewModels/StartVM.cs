using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;
using TriviaXamarinApp.Views;

namespace TriviaXamarinApp.ViewModels
{

    class StartVM
    {
        public ICommand LoginCommand { get; }
        public ICommand SignUpCommand { get; }
        public ICommand GuestTriviaCommand { get; }

        private async void Login()
        {
            Page p = new LoginV();
            await Application.Current.MainPage.Navigation.PushAsync(p);
        }

        private async void SignUp()
        {
            Page p = new SignUpV();
            await Application.Current.MainPage.Navigation.PushAsync(p);
        }

        private async void GuestTrivia()
        {
            Page p = new GuestTriviaV();
            await Application.Current.MainPage.Navigation.PushAsync(p);
        }

        public StartVM()
        {
            LoginCommand        =    new Command(Login);
            SignUpCommand       =    new Command(SignUp);
            GuestTriviaCommand  =    new Command(GuestTrivia);
        }


    }
}
