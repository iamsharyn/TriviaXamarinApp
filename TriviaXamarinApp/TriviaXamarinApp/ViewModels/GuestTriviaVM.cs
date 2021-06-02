using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using TriviaXamarinApp.Models;
using TriviaXamarinApp.Services;
using Xamarin.Forms;
using System.Linq; // For arrays Contain method
using System.Threading.Tasks;

namespace TriviaXamarinApp.ViewModels
{
    public class GuestTriviaVM
    {
        public string[] Answers { get; set; }
        public AmericanQuestion Que { get; }
        public string UserAns { get; set; }

        public ICommand AnswerCommand;

        public GuestTriviaVM(AmericanQuestion q)
        {
            UserAns = null;
            AnswerCommand = new Command(Answer, CanInteract);
            Que = q;
            Answers = new string[Que.OtherAnswers.Length + 1];
            int ind = 0;
            foreach(string s in Que.OtherAnswers)
            {
                Answers[ind++] = s;
            }
            Answers[ind] = Que.CorrectAnswer;
            Random random = new Random();
            Answers = Answers.OrderBy(x => random.Next()).ToArray();
        }

        private async Task<AmericanQuestion> FetchQue()
        {
            TriviaWebAPIProxy client = TriviaWebAPIProxy.CreateProxy();

            try
            {
                AmericanQuestion q = await client.GetRandomQuestion();

                if (q != null)
                {
                    return q;
                }
                else
                {
                    await App.Current.MainPage.DisplayAlert("Problem Fetching Question", "Try again and check internet connection.", "OK");
                    await App.Current.MainPage.Navigation.PopAsync();
                }
            }
            catch
            {
                await App.Current.MainPage.DisplayAlert("Problem Fetching Question", "Try again and check internet connection.", "OK");
                await App.Current.MainPage.Navigation.PopAsync();
            }

            return null;
        }

        private async void Answer()
        {
            
        }

        private bool CanInteract()
        {
            return UserAns != null;
        }
    }
}
