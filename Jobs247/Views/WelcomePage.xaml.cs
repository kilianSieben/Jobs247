using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Jobs247.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class WelcomePage : ContentPage
    {
        public WelcomePage()
        {
            InitializeComponent();
        }

        private async void OnFindNewJobClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new JobFilterPage());
        }
    }
}