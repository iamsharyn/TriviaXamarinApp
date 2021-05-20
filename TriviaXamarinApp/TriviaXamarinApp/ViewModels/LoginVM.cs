using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;
using TriviaXamarinApp.Services;
using TriviaXamarinApp.Models;
using System.Threading.Tasks;
using TriviaXamarinApp.Views;
using System.Diagnostics;


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
            try
            {
                TriviaWebAPIProxy client = TriviaWebAPIProxy.CreateProxy();
                Task<User> loginTask = client.LoginAsync(Email, Pass);
                loginTask.Wait();
                User u = loginTask.Result;

                if (u != null)
                {
                    Page p = new MainV(); // Creating next page

                    // Saving application's data
                    ((App)Application.Current).User = u;

                    await Application.Current.MainPage.Navigation.PushAsync(p);
                }
                else
                {
                    await App.Current.MainPage.DisplayAlert("Login Failed", "Wrong credentials. Try again.", "OK");
                }
            }
            catch
            {
                await App.Current.MainPage.DisplayAlert("Login Failed", "Problem login in. Try again later.", "OK");
            }
        }

    }
}
