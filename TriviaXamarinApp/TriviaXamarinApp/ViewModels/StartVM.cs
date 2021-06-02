using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;
using TriviaXamarinApp.Views;
using TriviaXamarinApp.Models;
using TriviaXamarinApp.Services;
using System.Threading.Tasks;


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
            try
            {
                var client = TriviaWebAPIProxy.CreateProxy();
                AmericanQuestion q = await client.GetRandomQuestion();
                if (q == null)
                    throw new Exception();
                Page p = new GuestTriviaV(q);
                await Application.Current.MainPage.Navigation.PushAsync(p);
            }
            catch
            {
                await App.Current.MainPage.DisplayAlert("Problem Fetching Question", "Try again and check internet connection.", "OK");
            }
        }

        public StartVM()
        {
            LoginCommand        =    new Command(Login);
            SignUpCommand       =    new Command(SignUp);
            GuestTriviaCommand  =    new Command(GuestTrivia);
        }


    }
}
