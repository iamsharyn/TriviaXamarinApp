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
    class MainVM : BaseVM
    {
        private int correctAns;
        public ICommand TriviaCommand { get; }
        public Command AddCommand { get; }
        public ICommand EditCommand { get; }
        public ICommand DeleteCommand { get; }
        public ICommand LogoutCommand { get; }
        public User User => ((App)Application.Current).User;
        public int CorrectAns
        {
            get => correctAns;

            private set
            {
                correctAns = value; // change page CorrectAns-value
                
                // notify property changed
                OnPropertyChanged(nameof(CorrectAns));
                OnPropertyChanged(nameof(PossibleAdds)); // since PossibleAdds is dependant on CorrectAns
                AddCommand.ChangeCanExecute(); // check if command may be executed or not
            }
        }

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

        /* To be called when view is reactivated, so binded values shown might not be up-to-date. */
        public void RefreshView() => CorrectAns = ((App)Application.Current).CorrectAns;

        /* Gets no parameters and returns whether a question may be added by the user */
        public bool CanAdd()
        {
            return PossibleAdds > 0;
        }
    }
}
