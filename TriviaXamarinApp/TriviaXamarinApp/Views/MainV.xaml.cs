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
    public partial class MainV : ContentPage
    {
        public MainV()
        {
            InitializeComponent();
            this.BindingContext = new MainVM();
        }

        protected override void OnAppearing()
        {
            if (this.BindingContext != null)
                ((MainVM)this.BindingContext).RefreshView();
        }
    }
}