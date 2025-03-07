﻿using System;
using System.Collections.Generic;
using System.Text;
using TriviaXamarinApp.Models;
using Xamarin.Forms;
using TriviaXamarinApp.Services;

namespace TriviaXamarinApp.ViewModels
{
    public class AddQueVM : BaseVM
    {
        private int correctAns; // index for the right answer
        private string otherAnsText; // for storing the user's other answers text (containing each other answer, each in another line)
        private AmericanQuestion que;
        public AmericanQuestion Que
        {
            get => que;

            set
            {
                que = value;
                OnPropertyChanged(nameof(Que)); // notify property has changed
            }
        }
        public User User => ((App)Application.Current).User;
        public int CorrectAns
        {
            get => correctAns;

            private set
            {
                if (correctAns != value)
                {
                    correctAns = value; // update page variable
                    ((App)Application.Current).CorrectAns = value; // update app-saved variable
                    OnPropertyChanged(nameof(CorrectAns)); // notify property has changed
                    OnPropertyChanged(nameof(PossibleAdds)); // also notify here PossibleAdds has changed, since it depends on CorrectAns
                }
            }
        }
        // The number of possible questions is total right-answered questions divided by needed answers to create a question.
        public int PossibleAdds => CorrectAns / App.ANS_FOR_ADD; 
        public string OtherAnsText
        {
            get => otherAnsText;

            set
            {
                otherAnsText = value;
                OnPropertyChanged(OtherAnsText);
                UpdateOtherAns(); // Update Que.OtherAnswers
                AddCommand.ChangeCanExecute(); // check if question may be added or not
            }
        }
        public Command AddCommand { get; }

        public AddQueVM()
        {
            AddCommand = new Command(Add, CanAdd);

            CorrectAns = ((App)Application.Current).CorrectAns;
            Que = new AmericanQuestion()
            {
                QText = "",
                CorrectAnswer = "",
                OtherAnswers = { },
                CreatorNickName = User.NickName
            };
            OtherAnsText = "";
        }

        private async void Add()
        {
            var client = TriviaWebAPIProxy.CreateProxy();
            var success = await client.PostNewQuestion(Que);

            if (success)
            {
                // Question has been added to server successfully

                User.Questions.Add(Que); // add the question to the app-saved user variable
                CorrectAns -= App.ANS_FOR_ADD; // decrease app-saved user's count of correct answers 

                Que = new AmericanQuestion()
                {
                    QText = "",
                    CorrectAnswer = "",
                    OtherAnswers = { },
                    CreatorNickName = User.NickName
                };
                OtherAnsText = "";
            }
            else
            {
                await App.Current.MainPage.DisplayAlert("Add Question Failed",
                    "Check internet connection. Try again later.", "OK");
            }
        }


        private void UpdateOtherAns() => Que.OtherAnswers = 
            OtherAnsText.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);

        private bool CanAdd()
        {
            return (PossibleAdds > 0) && (Que.OtherAnswers.Length > 0) 
                && (Que.QText != "") && (Que.CorrectAnswer != "");
        }
    }
}
