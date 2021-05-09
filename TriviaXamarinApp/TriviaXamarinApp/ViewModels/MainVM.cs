using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using TriviaXamarinApp.Models;
using Xamarin.Forms;

namespace TriviaXamarinApp.ViewModels
{
    class MainVM
    {
        public User user { get; set; }

        public ICommand TriviaCommand { get; }
        public ICommand AddCommand { get; }
        public ICommand EditCommand { get; }
        public ICommand DeleteCommand { get; }
        public ICommand LogoutCommand { get; }

        public MainVM()
        {
            TriviaCommand = new Command(Trivia);
            AddCommand = new Command(Add);
            DeleteCommand = new Command(Delete);
            EditCommand = new Command(Edit);
            LogoutCommand = new Command(Logout);
        }

        public async void Trivia()
        {

        }

        public async void Add()
        {

        }

        public async void Delete()
        {

        }
        public async void Edit()
        {

        }

        public async void Logout()
        {

        }
    }
}
