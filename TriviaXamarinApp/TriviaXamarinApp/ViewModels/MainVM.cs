using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using TriviaXamarinApp.Models;
using TriviaXamarinApp.Services;
using TriviaXamarinApp.Views;
using Xamarin.Forms;

namespace TriviaXamarinApp.ViewModels
{
    class MainVM
    {
        public ICommand TriviaCommand { get; }
        public Command AddCommand { get; }
        public ICommand EditCommand { get; }
        public ICommand DeleteCommand { get; }
        public ICommand LogoutCommand { get; }
        public User User => ((App)Application.Current).User;
        public int CorrectAns { get; }
        public int PossibleAdds => CorrectAns / App.ANS_FOR_ADD;

        public MainVM()
        {
            TriviaCommand = new Command(Trivia);
            AddCommand = new Command(Add, CanAdd);
            DeleteCommand = new Command(Delete);
            EditCommand = new Command(Edit);
            LogoutCommand = new Command(Logout);

            CorrectAns = ((App)Application.Current).CorrectAns;
        }

        public async void Trivia()
        {
            try
            {
                var client = TriviaWebAPIProxy.CreateProxy();
                AmericanQuestion q = await client.GetRandomQuestion();
                if (q == null)
                    throw new Exception();
                Page p = new TriviaV(q);
                await Application.Current.MainPage.Navigation.PushAsync(p);
            }
            catch
            {
                await App.Current.MainPage.DisplayAlert("Problem Fetching Question", "Try again and check internet connection.", "OK");
            }
        }

        public async void Add() => await Application.Current.MainPage.Navigation.PushAsync(new AddQueV());

        public async void Delete() => await Application.Current.MainPage.Navigation.PushAsync(new DeleteQueV());
        public async void Edit() => await Application.Current.MainPage.Navigation.PushAsync(new EditQueV());

        public void Logout()
        {
            ((App)Application.Current).User = null; // Resetting app-saved logged in user variable
            ((App)Application.Current).CorrectAns = 0; // Resetting user's correct answers count
            Application.Current.MainPage = new NavigationPage(new StartV()); // Create a new navigation page being StartV
        }

        public bool CanAdd()
        {
            return PossibleAdds > 0;
        }
    }
}
