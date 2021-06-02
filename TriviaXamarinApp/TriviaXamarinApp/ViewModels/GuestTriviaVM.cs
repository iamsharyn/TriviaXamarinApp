using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using TriviaXamarinApp.Models;
using TriviaXamarinApp.Services;
using Xamarin.Forms;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.Linq;

namespace TriviaXamarinApp.ViewModels
{
    public class GuestTriviaVM : BaseVM
    {
        private string userAns;
        private AmericanQuestion que;
        public Command AnswerCommand { get; }
        public ObservableCollection<string> Answers { get; set; }
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


        public GuestTriviaVM(AmericanQuestion q) // the constructor gets first question to present
        {
            AnswerCommand = new Command(Answer, CanInteract);

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
                await App.Current.MainPage.DisplayAlert("Way to go!", $"Your answer \'{UserAns}\' is correct.", "OK");
            else
                await App.Current.MainPage.DisplayAlert("Give it another shot", $"Your answer \'{UserAns}\' is incorrect. "
                    + $"Correct answer is \'{Que.CorrectAnswer}\'.", "OK");

            // load next question
            var q = await FetchQue();
            if (q  == null)
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
