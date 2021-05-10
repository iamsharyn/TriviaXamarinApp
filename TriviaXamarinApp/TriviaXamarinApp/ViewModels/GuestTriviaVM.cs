using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using TriviaXamarinApp.Models;
using TriviaXamarinApp.Services;
using Xamarin.Forms;
using System.Linq; // For arrays Contain method

namespace TriviaXamarinApp.ViewModels
{
    class GuestTriviaVM
    {
        public AmericanQuestion Que { get; }
        public string UserAns { get; set; }
        public ICommand AnswerCommand;


        public GuestTriviaVM()
        {
            Que = FetchQue(); // Replace
            UserAns = "";
            AnswerCommand = new Command(Answer);
        }

        private AmericanQuestion FetchQue()
        {

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
