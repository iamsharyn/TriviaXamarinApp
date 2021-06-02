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


        public GuestTriviaVM(AmericanQuestion q)
        {
            UserAns = "";
            AnswerCommand = new Command(Answer);
            Que = q;
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
        
        private bool IsCorrect(AmericanQuestion q, string ans)
        {
            if (q.CorrectAnswer.Equals(ans))
                return true;

            return q.OtherAnswers.Contains(ans);
        }
    }
}
