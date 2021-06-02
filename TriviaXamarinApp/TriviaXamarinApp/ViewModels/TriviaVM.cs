using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TriviaXamarinApp.Models;
using TriviaXamarinApp.Services;
using Xamarin.Forms;

namespace TriviaXamarinApp.ViewModels
{
    public class TriviaVM : BaseVM
    {
        private int correctAns;
        private string userAns;
        private AmericanQuestion que;
        public Command AnswerCommand { get; }
        public ObservableCollection<string> Answers { get; set; }
        public User User => ((App)Application.Current).User; // property for current logged in user
        public int CorrectAns
        {
            get => correctAns;

            private set
            {
                if (correctAns != value)
                {
                    correctAns = value; // Update page's correctAns variable
                    ((App)Application.Current).CorrectAns = value; // Update app-saved correct answers count
                    OnPropertyChanged(nameof(CorrectAns)); // Notify property changed
                }
            }
        }

        public AmericanQuestion Que
        {
            get => que;

            set
            {
                if (que != value)
                {
                    que = value;
                    OnPropertyChanged(nameof(Que));
                }
            }
        }

        public string UserAns
        {
            get => userAns;

            set
            {
                if (this.userAns != value)
                {
                    this.userAns = value;
                    OnPropertyChanged(nameof(UserAns));
                    AnswerCommand.ChangeCanExecute(); // check if AnswerCommand can be executed
                }
            }
        }


        public TriviaVM(AmericanQuestion q) // the constructor gets first question to present
        {
            AnswerCommand = new Command(Answer, CanInteract);

            CorrectAns = ((App)Application.Current).CorrectAns; // initialize CorrectAns

            LoadQue(q);
        }

        private async Task<AmericanQuestion> FetchQue()
        {
            TriviaWebAPIProxy client = TriviaWebAPIProxy.CreateProxy();

            try
            {
                AmericanQuestion q = await client.GetRandomQuestion();

                return q;
            }
            catch
            {
                return null;
            }
        }

        private async void Answer()
        {
            if (UserAns.Equals(Que.CorrectAnswer))
            {
                CorrectAns++; // Increase user's count of correct answers
                await App.Current.MainPage.DisplayAlert($"Way to go {User.NickName}!", 
                    $"Your answer \'{UserAns}\' is correct.", "OK");
            }
            else
            {
                await App.Current.MainPage.DisplayAlert("Give it another shot", $"Your answer \'{UserAns}\' is incorrect. "
                    + $"Correct answer is \'{Que.CorrectAnswer}\'.", "OK");
            }

            // load next question
            var q = await FetchQue();
            if (q == null)
            {
                await App.Current.MainPage.DisplayAlert("Problem Fetching Question", "Try again and check internet connection.", "OK");
                await App.Current.MainPage.Navigation.PopAsync();
            }
            else
            {
                LoadQue(q);
            }
        }

        private void LoadQue(AmericanQuestion q)
        {
            UserAns = null;
            Que = q;

            var arr = new string[Que.OtherAnswers.Length + 1];
            int ind = 0;
            foreach (string s in Que.OtherAnswers)
            {
                arr[ind++] = s;
            }
            arr[ind] = Que.CorrectAnswer;
            var rnd = new Random();
            arr = arr.OrderBy(x => rnd.Next()).ToArray();
            Answers = new ObservableCollection<string>(arr);
            OnPropertyChanged(nameof(Answers)); // notify  Answers has changed
        }

        private bool CanInteract()
        {
            return UserAns != null;
        }
    }
}
