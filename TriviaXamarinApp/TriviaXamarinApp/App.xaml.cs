using Xamarin.Forms;
using TriviaXamarinApp.Views;
using TriviaXamarinApp.Models;

namespace TriviaXamarinApp
{
    public partial class App : Application
    {
        public User User { get; set; } // Logged in user
        public int CorrectAns { get; set; } // Correct answers

        public App()
        {
            InitializeComponent();

            User = null;
            CorrectAns = 0;

            MainPage = new NavigationPage(new StartV());
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
