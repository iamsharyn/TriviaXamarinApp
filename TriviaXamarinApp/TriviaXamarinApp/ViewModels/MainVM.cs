using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using TriviaXamarinApp.Models;
using TriviaXamarinApp.Views;
using Xamarin.Forms;

namespace TriviaXamarinApp.ViewModels
{
    class MainVM
    {
        public ICommand TriviaCommand { get; }
        public ICommand AddCommand { get; }
        public ICommand EditCommand { get; }
        public ICommand DeleteCommand { get; }
        public ICommand LogoutCommand { get; }
        public User User { get; }

        public MainVM()
        {
            TriviaCommand = new Command(Trivia);
            AddCommand = new Command(Add);
            DeleteCommand = new Command(Delete);
            EditCommand = new Command(Edit);
            LogoutCommand = new Command(Logout);
            User = ((App)Application.Current).User;
        }

        public async void Trivia() => await Application.Current.MainPage.Navigation.PushAsync(new TriviaV());

        public async void Add() => await Application.Current.MainPage.Navigation.PushAsync(new AddQueV());

        public async void Delete() => await Application.Current.MainPage.Navigation.PushAsync(new DeleteQueV());
        public async void Edit() => await Application.Current.MainPage.Navigation.PushAsync(new EditQueV());

        public void Logout()
        {
            ((App)Application.Current).User = null;
            ((App)Application.Current).CorrectAns = 0; // Resetting user's correct answers count
            Application.Current.MainPage = new NavigationPage(new StartV()); // Create a new navigation page being StartV
        }
    }
}
