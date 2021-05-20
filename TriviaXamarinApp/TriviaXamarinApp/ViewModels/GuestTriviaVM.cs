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
    class GuestTriviaVM
    {
        public AmericanQuestion Que { get; }
        public string UserAns { get; set; }
        public ICommand AnswerCommand;
        /*const AmericanQuestion EMPTY_QUE = new AmericanQuestion
        {
            QText = "",
            CorrectAnswer = "",
            OtherAnswers = { },
            CreatorNickName = ""
        }; */


        public GuestTriviaVM()
        {
            Que = null; // Replace
            UserAns = "";
            AnswerCommand = new Command(Answer);
        }

        private async Task<AmericanQuestion> FetchQueAsync()
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
        
        private bool IsCorrect(AmericanQuestion q, string ans)
        {
            if (q.CorrectAnswer.Equals(ans))
                return true;

            return q.OtherAnswers.Contains(ans);
        }
    }
}
