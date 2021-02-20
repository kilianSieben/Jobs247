using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Jobs247.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class JobFilterPage : ContentPage
    {
        private string matchingJobString;
        public string MatchingJobString
        {
            get { return matchingJobString; }
            set
            {
                matchingJobString = value;
                OnPropertyChanged(nameof(MatchingJobString));
            }
        }
        public JobFilterPage()
        {
            InitializeComponent();
            Title = "";
        }

        private async void OnShowMatchesClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ShowMatchesPage());
        }
    }
}