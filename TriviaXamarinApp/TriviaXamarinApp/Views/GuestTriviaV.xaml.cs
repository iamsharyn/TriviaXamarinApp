using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TriviaXamarinApp.Models;
using TriviaXamarinApp.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TriviaXamarinApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class GuestTriviaV : ContentPage
    {
        public GuestTriviaV(AmericanQuestion q)
        {
            InitializeComponent();
            this.BindingContext = new GuestTriviaVM(q);
        }
    }
}