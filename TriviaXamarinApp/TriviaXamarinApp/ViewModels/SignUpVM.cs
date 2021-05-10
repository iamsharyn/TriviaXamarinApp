using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TriviaXamarinApp.Models;
using TriviaXamarinApp.Services;
using TriviaXamarinApp.Views;
using Xamarin.Forms;

namespace TriviaXamarinApp.ViewModels
{
    class SignUpVM
    {
        public string Email { get; set; }
        public string NickName { get; set; }
        public string Password { get; set; }


        public ICommand SignUpCommand { get; }

        public SignUpVM()
        {
            SignUpCommand = new Command(SignUp);
        }

        private async void SignUp()
        {
            try
            {
                TriviaWebAPIProxy client = TriviaWebAPIProxy.CreateProxy();
                User u = new User()
                {
                    Email = this.Email,
                    NickName = this.NickName,
                    Password = this.Password,
                    Questions = new List<AmericanQuestion>()
                };
                
                Task<bool> registerTask = client.RegisterUser(u);
                registerTask.Wait();
                if(!registerTask.Result)
                {
                    await App.Current.MainPage.DisplayAlert("Sign Up Failed", "Try again later.", "OK");
                }
                else
                {
                    Page p = new MainV();

                    // Saving application's data
                    ((App)Application.Current).User = u;

                    Application.Current.MainPage = new NavigationPage(p); // Create a new navigation page being MainV
                }
            }
            catch
            {
                await App.Current.MainPage.DisplayAlert("Sign Up Failed", "Try again later.", "OK");
            }
        }
    }
}
